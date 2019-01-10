using System;
using EPPaymentWebApp.Models;

namespace EPPaymentWebApp.Interfaces
{
    public interface IEndPaymentRepository
    {
        EndPayment GetEndPaymentByResponsePaymentId(int responsePaymentId);
        void UpdateEndPaymentSentStatus(int endPaymentId,string endPaymentSentStatus);
    }
}
