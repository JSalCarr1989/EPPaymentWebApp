using System;
using EPPaymentWebApp.Models;

namespace EPPaymentWebApp.Interfaces
{
    public interface IEndPaymentRepository
    {
        EndPayment GetEndPaymentByResponsePaymentId(int responsePaymentId);

        //codigo en desuso
        Boolean ValidateEndPaymentSentStatus(int endPaymentId);
        //codigo en desuso

        void UpdateEndPaymentSentStatus(int endPaymentId,string endPaymentSentStatus);
    }
}
