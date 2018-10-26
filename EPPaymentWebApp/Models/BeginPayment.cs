using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPPaymentWebApp.Models
{
    public class BeginPayment
    {
        public int BeginPaymentId { get; set; }
        public string BillingAccount { get; set; }
        public string ServiceRequest { get; set; }
        public string PaymentReference { get; set;}
        public string CreateToken { get; set; }

    }
}
