using System;

namespace EPPaymentWebApp.Models
{
    public class ResponsePayment
    {
        public int ResponsePaymentId { get; set; }
        public string MpOrder { get; set; }
        public string MpReference { get; set; }
        public decimal MpAmount { get; set; }
        public string MpPaymentMethod { get; set; }
        public string MpResponse { get; set; }
        public string MpResponseMsg { get; set; }
        public string MpAuthorization { get; set; }
        public string MpSignature { get; set; }
        public string MpPan { get; set; }
        public DateTime MpDate { get; set; }
        public string MpBankName { get; set; }
        public string MpFolio { get; set; }
        public string MpSbToken { get; set; }
        public int MpSaleId { get; set; }
        public string MpCardHolderName { get; set; }
        public int ResponsePaymentTypeListId { get; set; }
        public int ResponsePaymentHashStatusId { get; set; }
        public int PaymentRequestId { get; set; }
    }
}
