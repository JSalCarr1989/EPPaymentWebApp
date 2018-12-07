using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EPPaymentWebApp.Models;
using EPPaymentWebApp.Interfaces;
using EPPaymentWebApp.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Compact;
using System.IO;

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

            var logger = new LoggerConfiguration()
           .MinimumLevel.Debug()
           .WriteTo.RollingFile(new CompactJsonFormatter(),
                                 //@"E:\LOG\EnterprisePaymentLog.json",
                                 Path.Combine(@"E:\LOG\", @"EnterprisePaymentLog.json"),
                                 shared: true,
                                 retainedFileCountLimit: 30
                                 )
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

                    //EnterprisePaymentHelpers.SetObjectAsJson(HttpContext.Session,"viewModelobject",viewModel);
                    
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
            

            //1.- Validamos el hash
            var ValidHash =  EnterprisePaymentHelpers.ValidateMultipagosHash(multiPagosResponse,_config,_logger);

            var hashStatus = (ValidHash) ? "HASH_VALIDO" : "HASH_INVALIDO";

            //2.- Obtenemos el requestpaymentid que previo a la respuesta.
            var logPayment = _logPaymentRepo.GetLastRequestPaymentId(
                multiPagosResponse.mp_amount,
                multiPagosResponse.mp_order,
                multiPagosResponse.mp_reference,
                "REQUEST_PAYMENT");

            //3.- mapear campos a insertar
            var responsePaymentDTO = EnterprisePaymentHelpers.GenerateResponsePaymentDTO(
                    multiPagosResponse,
                    logPayment.RequestPaymentId,
                    hashStatus);

                //TODO: SI EL HASH NO ES VALIDO se debe crear un endpayment con el resultado negativo. (movimiento en trigger)
            //4.- guardar el responsepayment en base de datos (endpayment se guarda a su vez con trigger)
            int responsePaymentId = _responsePaymentRepo.CreateResponsePayment(responsePaymentDTO);

            //5.- TODO: consultar si server2server ya envio.

            var sentExists =_sentToTibcoRepo.GetEndPaymentSentToTibco("ENVIADO_TIBCO", "MULTIPAGOS_SERVER2SERVER", responsePaymentId);

            _endPayment = _endPaymentRepo.GetEndPaymentByResponsePaymentId(responsePaymentId);

            //TODO: si server2server ya envio ya no enviamos (if)
            if (!sentExists)
            { 
                string resultMessage = await _responseBankRequestTypeTybcoRepo.SendEndPaymentToTibco(_endPayment);
                //Si la respuesta fue satisfactoria actualiza el estatus de endpayment en bd a enviado.
                if (resultMessage == "OK")
                {
                    //TO DO: UPDATE POR OTROS CAMPOS QUE NO SEAN EL ENDPAYMENTID
                    var udpatedEndPaymentId = _endPaymentRepo.UpdateEndPaymentSentStatus(_endPayment.EndPaymentId, "ENVIADO_TIBCO");
                }

            }


            var enterprisePaymentViewModel = _enterprisePaymentViewModelRepository.GetEnterprisePaymentViewModel(_endPayment.ServiceRequest, _endPayment.PaymentReference);

            return View(enterprisePaymentViewModel);

            

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
