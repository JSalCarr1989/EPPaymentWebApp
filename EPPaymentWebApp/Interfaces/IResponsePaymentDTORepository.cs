using EPPaymentWebApp.Models;

namespace EPPaymentWebApp.Interfaces
{
    public interface IResponsePaymentDTORepository
    {
        ResponsePaymentDTO GenerateResponsePaymentDTO(MultiPagosResponsePaymentDTO multiPagosResponse, int requestPaymentId, string hashStatus);
    }
}
