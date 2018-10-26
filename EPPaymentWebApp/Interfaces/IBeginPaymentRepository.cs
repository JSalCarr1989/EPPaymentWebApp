using EPPaymentWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPPaymentWebApp.Interfaces
{
    public interface IBeginPaymentRepository
    {
        BeginPayment GetByServiceRequest(string ServiceRequest);
    }
}
