using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EPPaymentWebApp.Models;
using EPPaymentWebApp.Interfaces;
using EPPaymentWebApp.Helpers;
using System.ServiceModel;

namespace EPPaymentWebApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly IBeginPaymentRepository _beginPaymentRepo;
        private readonly IResponsePaymentRepository _responsePaymentRepo;
        private readonly ILogPaymentRepository _logPaymentRepo;
        private readonly IEndPaymentRepository _endPaymentRepo;
        private readonly IResponseBankRequestTypeTibcoRepository _responseBankRequestTypeTybcoRepo;
        private BeginPayment _beginPayment = new BeginPayment();



        public HomeController(IBeginPaymentRepository beginPaymentRepo, IResponsePaymentRepository responsePaymentRepo, ILogPaymentRepository logPaymentRepo,IEndPaymentRepository endPaymentRepo, IResponseBankRequestTypeTibcoRepository responseBankRequestTypeTybcoRepo)
        {
            _beginPaymentRepo = beginPaymentRepo;
            _responsePaymentRepo = responsePaymentRepo;
            _logPaymentRepo = logPaymentRepo;
            _endPaymentRepo = endPaymentRepo;
            _responseBankRequestTypeTybcoRepo = responseBankRequestTypeTybcoRepo;
        }




        [HttpGet]
        public IActionResult Index([FromQuery(Name ="ServiceRequest")]string ServiceRequest)
        {
            

            if (!string.IsNullOrEmpty(ServiceRequest))
            {
                 _beginPayment = _beginPaymentRepo.GetByServiceRequest(ServiceRequest);

                if  (_beginPayment != null)
                {
                    var viewModel = BeginPaymentHelpers.GenerateEnterprisePaymentViewModel(_beginPayment);

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
        public async Task<IActionResult> HandleResponse()
        { 
            //1.- obtener el id del requestpayment 
            var logPayment = _logPaymentRepo.GetLastRequestPaymentId(0,"","","");
            //2.- mapear campos a insertar
            ResponsePaymentDTO responsePaymentDTO = new ResponsePaymentDTO
            {
                MpOrder = "",
                MpReference = "",
                MpAmount = 0.00M,
                MpPaymentMethod = "",
                MpResponse = "",
                MpResponseMsg = "",
                MpAuthorization = "",
                MpSignature = "",
                MpPan = "",
                MpDate = "",
                MpBankName = "",
                MpFolio = "",
                MpSbToken = "",
                MpSaleId = 1,
                MpCardHolderName = "Salvatore",
                ResponsePaymentTypeDescription = "",
                RequestPaymentId = logPayment.RequestPaymentId
            };
            //3.- guardar el responsepayment en base de datos (endpayment se guarda a su vez con trigger)
            int responsePaymentId = _responsePaymentRepo.CreateResponsePayment(responsePaymentDTO);
            //4.- obtener el endpayment de base de datos
            var endPayment = _endPaymentRepo.GetEndPaymentByResponsePaymentId(responsePaymentId);
            //5.- enviar el endpayment a tibco.
            string resultMessage = await _responseBankRequestTypeTybcoRepo.SendEndPaymentToTibco(endPayment);
            //6.-enviar los datos a la vista.
            return Content("Hola");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
