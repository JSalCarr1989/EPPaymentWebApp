using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EPPaymentWebApp.Models;
using EPPaymentWebApp.Interfaces;
using EPPaymentWebApp.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace EPPaymentWebApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly IBeginPaymentRepository _beginPaymentRepo;
        private readonly IResponsePaymentRepository _responsePaymentRepo;
        private readonly ILogPaymentRepository _logPaymentRepo;
        private readonly IEndPaymentRepository _endPaymentRepo;
        private readonly IConfiguration _config;
        private readonly IResponseBankRequestTypeTibcoRepository _responseBankRequestTypeTybcoRepo;
        private readonly IEnterprisePaymentViewModelRepository _enterprisePaymentViewModelRepository;
        private readonly ILogger _logger;
        private readonly ISentToTibcoRepository _sentToTibcoRepo;



        public HomeController(IConfiguration config, IBeginPaymentRepository beginPaymentRepo, IResponsePaymentRepository responsePaymentRepo, ILogPaymentRepository logPaymentRepo,IEndPaymentRepository endPaymentRepo, IResponseBankRequestTypeTibcoRepository responseBankRequestTypeTybcoRepo, IEnterprisePaymentViewModelRepository enterprisePaymentViewModelRepository, ISentToTibcoRepository sentToTibcoRepo)
        {
            _beginPaymentRepo = beginPaymentRepo;
            _responsePaymentRepo = responsePaymentRepo;
            _logPaymentRepo = logPaymentRepo;
            _endPaymentRepo = endPaymentRepo;
            _responseBankRequestTypeTybcoRepo = responseBankRequestTypeTybcoRepo;
            _config = config;
            _enterprisePaymentViewModelRepository = enterprisePaymentViewModelRepository;
            _sentToTibcoRepo = sentToTibcoRepo;

            //  var logger = new LoggerConfiguration()
            // .MinimumLevel.Information()
            // .WriteTo.RollingFile(new CompactJsonFormatter(),
            //                       Path.Combine(@"E:\LOG\", @"EnterprisePaymentLog.json"),
            //                       shared: true,
            //                       retainedFileCountLimit: 30
            //                       )
            //.CreateLogger();
            var environmentConnectionString = Environment.GetEnvironmentVariable("EpPaymentDevConnectionStringEnvironment", EnvironmentVariableTarget.Machine);

            var connectionString = !string.IsNullOrEmpty(environmentConnectionString)
                       ? environmentConnectionString
                       : _config.GetConnectionString("EpPaymentDevConnectionString");

            var logger = new LoggerConfiguration()
.MinimumLevel.Information()
.WriteTo.MSSqlServer(connectionString, "Log")
.CreateLogger();


            _logger = logger;



        }




        [HttpGet]
        public IActionResult Index([FromQuery(Name ="ServiceRequest")]string ServiceRequest)
        {
            BeginPayment _beginPayment = new BeginPayment();

            if (!string.IsNullOrEmpty(ServiceRequest))
            {
                 _beginPayment = _beginPaymentRepo.GetByServiceRequest(ServiceRequest);

                if  (_beginPayment != null)
                {
                    var viewModel = EnterprisePaymentHelpers.GenerateEnterprisePaymentViewModelRequestPayment(_beginPayment);
                    return View(viewModel);

                }
                else
                {
                    return Content("404 not found dude.");
                }

            }else
            {

                return Content("There is nothing here");
            }

            
        }

        [HttpPost]
        public async Task<IActionResult> HandleResponse([FromForm] MultiPagosResponsePaymentDTO multiPagosResponse)
        {

            EndPayment _endPayment = new EndPayment();
            ResponsePayment _responsePayment = new ResponsePayment();

            EnterprisePaymentDbLogHelpers.LogResponsedDataToDb(
                _logger, 
                multiPagosResponse);
           
            
            var ValidHash =  EnterprisePaymentHelpers.ValidateMultipagosHash(
                multiPagosResponse,
                _config,
                _logger);

            var hashStatus = (ValidHash) ? "HASH_VALIDO" : "HASH_INVALIDO";

            
            var logPayment = _logPaymentRepo.GetLastRequestPaymentId(
                multiPagosResponse.mp_amount,
                multiPagosResponse.mp_order,
                multiPagosResponse.mp_reference,
                "REQUEST_PAYMENT");

            EnterprisePaymentDbLogHelpers.LogGetLastRequestPaymentId(
                _logger, 
                multiPagosResponse, 
                "REQUEST_PAYMENT", 
                logPayment.RequestPaymentId
                );

            
            var responsePaymentDTO = EnterprisePaymentHelpers.GenerateResponsePaymentDTO(
                    multiPagosResponse,
                    logPayment.RequestPaymentId,
                    hashStatus);

            int responsePaymentId = _responsePaymentRepo.CreateResponsePayment(responsePaymentDTO);

            EnterprisePaymentDbLogHelpers.LogCreateResponsePayment(
                _logger, 
                responsePaymentDTO, 
                responsePaymentId
                );

            var sentExists =_sentToTibcoRepo.GetEndPaymentSentToTibco(
                "ENVIADO_TIBCO", 
                "MULTIPAGOS_SERVER2SERVER", 
                responsePaymentId
                );

            EnterprisePaymentDbLogHelpers.LogGetSentExists(
                _logger,
                "ENVIADO_TIBCO", 
                "MULTIPAGOS_SERVER2SERVER",
                responsePaymentId,
                sentExists
                );

            _endPayment = _endPaymentRepo.GetEndPaymentByResponsePaymentId(responsePaymentId);

            EnterprisePaymentDbLogHelpers.LogGetEndPayment(
                _logger, 
                responsePaymentId, 
                _endPayment
                );
            
            if (sentExists != true)
            { 
                string resultMessage = await _responseBankRequestTypeTybcoRepo.SendEndPaymentToTibco(_endPayment,_logger);
                
                if (resultMessage == "OK")
                {
                     _endPaymentRepo.UpdateEndPaymentSentStatus(_endPayment.EndPaymentId, "ENVIADO_TIBCO");
                    EnterprisePaymentDbLogHelpers.LogUpdateEndPaymentSentStatus(
                        _logger, 
                        _endPayment.EndPaymentId, 
                        "ENVIADO_TIBCO"
                        );
                }

            }

            var enterprisePaymentViewModel = _enterprisePaymentViewModelRepository.GetEnterprisePaymentViewModel(_endPayment.ServiceRequest, _endPayment.PaymentReference);

            EnterprisePaymentDbLogHelpers.LogShowedDataInView(_logger, enterprisePaymentViewModel);

            return View(enterprisePaymentViewModel);

            

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
