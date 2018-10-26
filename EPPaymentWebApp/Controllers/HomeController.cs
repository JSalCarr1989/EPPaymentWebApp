using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EPPaymentWebApp.Models;
using EPPaymentWebApp.Interfaces;
using EPPaymentWebApp.Helpers;

namespace EPPaymentWebApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly IBeginPaymentRepository _beginPaymentRepo;
        private BeginPayment _beginPayment = new BeginPayment();
        

        public HomeController(IBeginPaymentRepository beginPaymentRepo) => _beginPaymentRepo = beginPaymentRepo;

        [HttpGet]
        public  IActionResult Index([FromQuery(Name ="ServiceRequest")]string ServiceRequest)
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
        public IActionResult HandleResponse()
        {
            return Content("Hola");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
