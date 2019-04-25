using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EPPCIDAL.Interfaces;
using EPPCIDAL.Models;
using EPPCIDAL.DTO;
using EPPaymentWebApp.Models;
using AutoMapper;
using EPPCIDAL.Services;
using EPPCIDAL.Constants;
using System;

namespace EPPaymentWebApp.Controllers
{
    public class HomeController : Controller
    {


        private readonly IMapper _mapper;
        private readonly IResponsePaymentService _responsePaymentService;
        private readonly IEndPaymentService _endPaymentService;
        private readonly IRequestPaymentService _requestPaymentService;
     
        



        public HomeController(
                             IMapper mapper,
                             IResponsePaymentService responsePaymentService,
                             IEndPaymentService endPaymentService,
                             IRequestPaymentService requestPaymentService
                             )
        {
            _mapper = mapper;
            _responsePaymentService = responsePaymentService;
            _endPaymentService = endPaymentService;
            _requestPaymentService = requestPaymentService;

        }




        [HttpGet]
        public async Task<IActionResult> Index([FromQuery(Name ="ServiceRequest")]string ServiceRequest)
        {
            PaymentViewModelDTO viewModelDTO = null;
            PaymentViewModel viewModel = null;

            try
            {
               viewModelDTO = await _requestPaymentService.GetRequestPaymentPaymentViewModelAsync(ServiceRequest);

               viewModel = _mapper.Map<PaymentViewModel>(viewModelDTO);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            if (viewModel != null)
            {
                return View(viewModel);
            }

            return StatusCode(404);
        }

        [HttpPost]
        public async Task<IActionResult> HandleResponse([FromForm] MultiPagosResponsePaymentDTO multiPagosResponse)
        {

            PaymentViewModel viewModel = null;

            try
            {
                int responsePaymentId = await _responsePaymentService.CreateResponsePayment(multiPagosResponse,ResponsePaymentConstants.RESPONSEPAYMENT_TYPE_POST);

                EndPayment endPayment = await _endPaymentService.SendEndPaymentToTibco(responsePaymentId);

                viewModel = _mapper.Map<PaymentViewModel>(await _responsePaymentService.GetResponsePaymentViewModelAsync(endPayment.ServiceRequest,endPayment.PaymentReference));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return View(viewModel);

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
