using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPPaymentWebApp.Models
{
    public class MultiPagosResponsePaymentDTO
    {
        public string mp_order { get; set; }
        public string mp_reference { get; set; }
        public decimal mp_amount { get; set; }
        public string mp_paymentmethod { get; set; }
        public string mp_response { get; set; }
        public string mp_responsemsg { get; set; }
        public string mp_authorization { get; set; }
        public string mp_signature { get; set; }
        public string mp_pan { get; set; }
        public string mp_date { get; set; }
        public string mp_bankname { get; set; }
        public string mp_folio { get; set; }
        public string mp_sbtoken { get; set; }
        public int mp_saleid { get; set; }
        public string mp_cardholdername { get; set; }

    }
}
