using EPPaymentWebApp.Models;

namespace EPPaymentWebApp.Interfaces
{
    public interface IEnterprisePaymentViewModelRepository
    {
        EnterprisePaymentViewModel GetEnterprisePaymentViewModel(string serviceRequest, string paymentReference);
    }
}
