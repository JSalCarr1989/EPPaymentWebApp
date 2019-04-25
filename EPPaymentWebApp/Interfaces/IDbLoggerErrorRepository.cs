namespace EPPaymentWebApp.Interfaces
{
    public interface IDbLoggerErrorRepository
    {
        void LogSendEndPaymentToTibcoError(string error);
        //void LogGetByServiceRequestError(string error, string serviceRequest);
        //void LogGenerateEnterprisePaymentViewModelRequestPaymentError(string error,string mpReference, string mpOrder);
        void LogCompute256HashError(string error);
        void LogByteToStringError(string error);
        void LogGenerateResponsePaymentDTOError(string error, string mpReference, string mpOrder);
        void LogValidateMultipagosHashError(string error, string mpReference, string mpOrder);
        void LogGetEndPaymentSentToTibcoError(string error);
        void LogCreateResponsePaymentError(string error, string mpReference, string mpOrder);
        void LogGetEndPaymentByResponsePaymentIdError(string error);
        void LogUpdateEndPaymentSentStatusError(string error);
        //void LogGetEnterprisePaymentViewModelError(string error);
    }
}
