using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EPPaymentWebApp.Models;
using EPPaymentWebApp.Interfaces;
using EPPaymentWebApp.Helpers;
using EPPaymentWebApp.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;


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
        private readonly ISentToTibcoRepository _sentToTibcoRepo;
        private readonly IDbConnectionRepository _connectionStringRepo;
        private readonly IDbLoggerRepository _dbLoggerRepository;
        private readonly IDbLoggerErrorRepository _dbLoggerErrorRepository;
     
        



        public HomeController(
                             IConfiguration config, 
                             IBeginPaymentRepository beginPaymentRepo, 
                             IResponsePaymentRepository responsePaymentRepo, 
                             ILogPaymentRepository logPaymentRepo,
                             IEndPaymentRepository endPaymentRepo, 
                             IResponseBankRequestTypeTibcoRepository responseBankRequestTypeTybcoRepo, 
                             IEnterprisePaymentViewModelRepository enterprisePaymentViewModelRepository, 
                             ISentToTibcoRepository sentToTibcoRepo,
                             IDbConnectionRepository connectionStringRepo,
                             IDbLoggerRepository dbLoggerRepository,
                             IDbLoggerErrorRepository dbLoggerErrorRepository

            )
        {
            _connectionStringRepo = connectionStringRepo;
            _beginPaymentRepo = beginPaymentRepo;
            _responsePaymentRepo = responsePaymentRepo;
            _logPaymentRepo = logPaymentRepo;
            _endPaymentRepo = endPaymentRepo;
            _responseBankRequestTypeTybcoRepo = responseBankRequestTypeTybcoRepo;
            _config = config;
            _enterprisePaymentViewModelRepository = enterprisePaymentViewModelRepository;
            _sentToTibcoRepo = sentToTibcoRepo;
            _dbLoggerRepository = dbLoggerRepository;
            _dbLoggerErrorRepository = dbLoggerErrorRepository;


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
                    var viewModel = EnterprisePaymentHelpers.GenerateEnterprisePaymentViewModelRequestPayment(_beginPayment,_dbLoggerErrorRepository);
                    return View(viewModel);

                }
                else
                {
                    return StatusCode(404);
                }

            }else
            {

                return StatusCode(404);
            }

            
        }

        [HttpPost]
        public async Task<IActionResult> HandleResponse([FromForm] MultiPagosResponsePaymentDTO multiPagosResponse)
        {

            EndPayment _endPayment = new EndPayment();
            ResponsePayment _responsePayment = new ResponsePayment();

            _dbLoggerRepository.LogResponsedDataToDb(multiPagosResponse);
           
            
            var ValidHash =  EnterprisePaymentHelpers.ValidateMultipagosHash(
                multiPagosResponse,
                _config,
                _dbLoggerRepository,
                _dbLoggerErrorRepository
                );

            var hashStatus = (ValidHash) ? StaticResponsePaymentProperties.VALID_HASH 
                                         : StaticResponsePaymentProperties.INVALID_HASH;

            
            var logPayment = _logPaymentRepo.GetLastRequestPaymentId(
                multiPagosResponse.mp_amount,
                multiPagosResponse.mp_order,
                multiPagosResponse.mp_reference,
                StaticResponsePaymentProperties.REQUEST_PAYMENT_STATUS);


            var responsePaymentDTO = EnterprisePaymentHelpers.GenerateResponsePaymentDTO(
                    multiPagosResponse,
                    logPayment.RequestPaymentId,
                    hashStatus,
                    _dbLoggerErrorRepository);

            int responsePaymentId = _responsePaymentRepo.CreateResponsePayment(responsePaymentDTO);


            var sentExists =_sentToTibcoRepo.GetEndPaymentSentToTibco(
                StaticResponsePaymentProperties.ENDPAYMENT_SENTED_STATUS,
                StaticResponsePaymentProperties.RESPONSEPAYMENT_TYPE_S2S, 
                responsePaymentId
                );


            _endPayment = _endPaymentRepo.GetEndPaymentByResponsePaymentId(responsePaymentId);


            if (sentExists != true)
            {
                string resultMessage = await _responseBankRequestTypeTybcoRepo.SendEndPaymentToTibco(_endPayment);

                if (resultMessage == StaticResponsePaymentProperties.TIBCO_OK_RESULT_MESSAGE)
                {
                    _endPaymentRepo.UpdateEndPaymentSentStatus(_endPayment.EndPaymentId, StaticResponsePaymentProperties.ENDPAYMENT_SENTED_STATUS);

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
