using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPPaymentWebApp.Models;

namespace EPPaymentWebApp.Interfaces
{
    public interface ILogPaymentRepository
    {
       LogPayment GetLastRequestPaymentId(decimal amount, string serviceRequest, string paymentReference, string StatusPayment);
    }
}
