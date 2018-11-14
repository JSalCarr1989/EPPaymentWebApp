using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPPaymentWebApp.Models
{
    public class LogPayment
    {
        public int LogPaymentId { get; set; }
        public string PaymentReference { get; set; }
        public string ServiceRequest { get; set; }
        public DateTime StatusDateTime { get; set; }
        public decimal Amount { get; set; }
        public string MpAccount { get; set; }
        public int StatusPaymentListId { get; set; }
        public int BeginPaymentId { get; set; }
        public int RequestPaymentId { get; set; }
        public int ResponsePaymentId { get; set; }
        public int EndPaymentId { get; set; }
     }
}
