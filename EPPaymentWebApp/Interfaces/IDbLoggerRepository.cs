using EPPaymentWebApp.Models;
using TibcoServiceReference;

namespace EPPaymentWebApp.Interfaces
{
   public interface IDbLoggerRepository
    {
        void LogResponsedDataToDb(MultiPagosResponsePaymentDTO multiPagosResponse);
        void LogHashValidationToDb(MultiPagosResponsePaymentDTO multiPagosResponse, string concatenation, string generatedHash, string result);
        void LogGetLastRequestPaymentId(decimal amount,string serviceRequest,string paymentReference,string statusPayment, int requestPaymentId);
        void LogCreateResponsePayment(ResponsePaymentDTO responsePaymentDTO, int responsePaymentId);
        void LogGetSentExists(string endPaymentStatusDescription, string responsePaymentType, int responsePaymentId, bool serverHasSentData);
        void LogGetEndPayment(int responsePaymentId, EndPayment endPayment);
        void LogUpdateEndPaymentSentStatus(int endPayment, string endPaymentSentStatus);
        void LogSendEndPaymentToTibco(ResponseBankRequestType requestToTibco, ResponseBankResponse responseFromTibco);
        void LogShowedDataInView(EnterprisePaymentViewModel viewModel);
    }
}
