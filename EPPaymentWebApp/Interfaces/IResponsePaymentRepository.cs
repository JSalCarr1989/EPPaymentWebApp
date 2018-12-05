using EPPaymentWebApp.Models;

namespace EPPaymentWebApp.Interfaces
{
    public interface IResponsePaymentRepository
    {
        int CreateResponsePayment(ResponsePaymentDTO responseDTO);
        ResponsePayment GetResponsePaymentById(int responsePaymentId);

    }
}
