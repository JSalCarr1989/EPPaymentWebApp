namespace EPPaymentWebApp.Interfaces
{
    public interface ISentToTibcoRepository
    {
        bool GetEndPaymentSentToTibco(string endPaymentStatusDescription,string responsePaymentType, int responsePaymentId);
    }
}
