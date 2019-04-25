using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPPaymentWebApp.Models
{
    public class PaymentViewModel
    {
        public string BillingAccount { get; set; }
        public string Currency { get; set; }
        public string CreateToken { get; set; }
        public int BeginPaymentId { get; set; }
        public string Mp_account { get; set; }
        public string Mp_product { get; set; }
        public string Mp_order { get; set; }
        public string Mp_reference { get; set; }
        public string Mp_node { get; set; }
        public string Mp_concept { get; set; }
        public string Mp_amount { get; set; }
        public string Mp_customername { get; set; }
        public string Mp_signature { get; set; }
        public string Mp_currency { get; set; }
        public string Mp_urlsuccess { get; set; }
        public string Mp_urlfailure { get; set; }
        public string Mp_registersb { get; set; }
        public string BankResponse { get; set; } // concatenacion de response code y responseMessage
        public string TransactionNumber { get; set; }
        public string Token { get; set; } // token generado por multipagos.
        public string CcLastFour { get; set; }
        public string IssuingBank { get; set; }
        public string CcType { get; set; }
        public string MpEndpoint { get; set; }
        public string NetValuesEndPoint { get; set; }
        public string NetHashEndpoint { get; set; }
        public string NetRequestPaymentEndpoint { get; set; }
    }
}
