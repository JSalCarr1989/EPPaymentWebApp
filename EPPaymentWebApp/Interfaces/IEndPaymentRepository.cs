using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPPaymentWebApp.Models;

namespace EPPaymentWebApp.Interfaces
{
    public interface IEndPaymentRepository
    {
        EndPayment GetEndPaymentByResponsePaymentId(int responsePaymentId);
        Boolean ValidateEndPaymentSentStatus(int endPaymentId);
        int UpdateEndPaymentSentStatus(int endPaymentId,string endPaymentSentStatus);
    }
}
