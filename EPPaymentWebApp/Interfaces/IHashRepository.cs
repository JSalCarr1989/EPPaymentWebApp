using EPPaymentWebApp.Models;


namespace EPPaymentWebApp.Interfaces
{
    public interface IHashRepository
    {
        string GetHashStatus(MultiPagosResponsePaymentDTO multipagosResponse);
    }
}
