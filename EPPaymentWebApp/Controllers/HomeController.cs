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
        private BeginPayment _beginPayment = new BeginPayment();
        private EndPayment _endPayment = new EndPayment();
        private ResponsePayment _responsePayment = new ResponsePayment();
        private readonly ILogger _logger;



        public HomeController(IConfiguration config, IBeginPaymentRepository beginPaymentRepo, IResponsePaymentRepository responsePaymentRepo, ILogPaymentRepository logPaymentRepo,IEndPaymentRepository endPaymentRepo, IResponseBankRequestTypeTibcoRepository responseBankRequestTypeTybcoRepo)
        {
            _beginPaymentRepo = beginPaymentRepo;
            _responsePaymentRepo = responsePaymentRepo;
            _logPaymentRepo = logPaymentRepo;
            _endPaymentRepo = endPaymentRepo;
            _responseBankRequestTypeTybcoRepo = responseBankRequestTypeTybcoRepo;
            _config = config;


            var logger = new LoggerConfiguration()
           .MinimumLevel.Debug()
           .WriteTo.RollingFile(new CompactJsonFormatter(),
                                 @"E:\LOG\EnterprisePaymentLog.json",
                                 shared: true,
                                 retainedFileCountLimit: 30
                                 )
          .CreateLogger();
            _logger = logger;


        }




        [HttpGet]
        public IActionResult Index([FromQuery(Name ="ServiceRequest")]string ServiceRequest)
        {
            

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

            //0.- Validar hash que se recibió de multipagos.
          
          var ValidHash =  EnterprisePaymentHelpers.ValidateMultipagosHash(multiPagosResponse,_config);
          
            //1.- obtener el id del requestpayment previo a la respuesta
            var logPayment = _logPaymentRepo.GetLastRequestPaymentId(
                multiPagosResponse.mp_amount,
                multiPagosResponse.mp_order,
                multiPagosResponse.mp_reference,
                "REQUEST_PAYMENT");

            //si el hash es valido
            if (ValidHash)
            {
                //2.- mapear campos a insertar
                var responsePaymentDTO = EnterprisePaymentHelpers.GenerateResponsePaymentDTO(
                    multiPagosResponse,
                    logPayment.RequestPaymentId,
                    "HASH_VALIDO");

                //3.- guardar el responsepayment en base de datos (endpayment se guarda a su vez con trigger)
                int responsePaymentId = _responsePaymentRepo.CreateResponsePayment(responsePaymentDTO);

                _responsePayment = _responsePaymentRepo.GetResponsePaymentById(responsePaymentId);

                
                //4.- obtener el endpayment de base de datos 
                _endPayment = _endPaymentRepo.GetEndPaymentByServiceRequestAndPaymentReference(_responsePayment.MpOrder, _responsePayment.MpReference);

                //TO DO: VALIDAR POR OTROS CAMPOS QUE NO SEAN EL ENDPAYMENTID
                //validar que el endpayment no tenga estatus de enviado, si tiene estatus de enviado no enviar.
                if (_endPaymentRepo.ValidateEndPaymentSentStatus(_endPayment.EndPaymentId) != true)
                {
                    string resultMessage = await _responseBankRequestTypeTybcoRepo.SendEndPaymentToTibco(_endPayment);
                    //Si la respuesta fue satisfactoria actualiza el estatus de endpayment en bd a enviado.
                    if(resultMessage == "OK")
                    {
                        //TO DO: UPDATE POR OTROS CAMPOS QUE NO SEAN EL ENDPAYMENTID
                        var udpatedEndPaymentId = _endPaymentRepo.UpdateEndPaymentSentStatus(_endPayment.EndPaymentId, "ENVIADO_TIBCO");
                    }
                }


            }
            else // si el hash no es valido.
            {
                var responsePaymentDTO = EnterprisePaymentHelpers.GenerateResponsePaymentDTO(
                                         multiPagosResponse,
                                         logPayment.RequestPaymentId,
                                         "HASH_INVALIDO");


                //3.- guardar el responsepayment en base de datos (endpayment se guarda a su vez con trigger)
                int responsePaymentId = _responsePaymentRepo.CreateResponsePayment(responsePaymentDTO);

                _responsePayment = _responsePaymentRepo.GetResponsePaymentById(responsePaymentId);
                //4.- obtener el endpayment de base de datos
                _endPayment = _endPaymentRepo.GetEndPaymentByServiceRequestAndPaymentReference(_responsePayment.MpOrder, _responsePayment.MpReference);

            }



            _logger.Information(
"Before Setting up object to the session:" +
"Response Code: {ResponseCode}" +
"Response Message: {ResponseMessage}",
_endPayment.ResponseCode,
_endPayment.ResponseMessage
);
            //TO DO: OBTENER TODOS LOS DATOS A MOSTRAR EN LA VISTA EN LA RESPUESTA DEL PAGO, SERA DESDE BASE DE DATOS , NO DESDE SESSION.

            //6.-Cargar los datos de la vista desde la  base de datos ( ¿obtenerlos de la sesion? )
            //var viewModelSessionObject = EnterprisePaymentHelpers.GetObjectFromJson<EnterprisePaymentViewModel>(HttpContext.Session, "viewModelobject");
            //cargamos los datos del inicio del pago.
            //_beginPayment = _beginPaymentRepo.GetByServiceRequest(_endPayment.ServiceRequest);
            //if (viewModelSessionObject != null)
            //{
            //    viewModelSessionObject.BankResponse = $"{_endPayment.ResponseCode} - {_endPayment.ResponseMessage}";
            //    viewModelSessionObject.TransactionNumber = _endPayment.TransactionNumber;
            //    viewModelSessionObject.Token = _endPayment.Token;
            //    viewModelSessionObject.CcLastFour = _endPayment.CcLastFour;
            //    viewModelSessionObject.IssuingBank = _endPayment.IssuingBank;
            //    viewModelSessionObject.CcType = _endPayment.CcType;

            //    //7.-enviar los datos a la vista.
            //    return View(viewModelSessionObject);
            //}else
            //{
            //    return Content("No hay sesion compa!");
            //}

            return Content("no hay nada aun");

            

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
