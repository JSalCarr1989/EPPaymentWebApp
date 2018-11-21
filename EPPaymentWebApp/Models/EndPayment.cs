using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPPaymentWebApp.Models
{
    public class EndPayment
    {
        public int EndPaymentId { get; set; }
        public string CcLastFour { get; set; }
        public string Token { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string TransactionNumber { get; set; }
        public string CcType { get; set; }
        public string IssuingBank { get; set; }
        public string ServiceRequest { get; set; }
        public string BillingAccount { get; set; }
        public string PaymentReference { get; set; }
    }
}
