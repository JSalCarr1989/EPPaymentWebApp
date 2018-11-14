using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPPaymentWebApp.Models;
using System.Text;
using System.Security.Cryptography;

namespace EPPaymentWebApp.Helpers
{
    public static class BeginPaymentHelpers
    {
        public static EnterprisePaymentViewModel GenerateEnterprisePaymentViewModel(BeginPayment beginPayment)
        {
            var viewModel = new EnterprisePaymentViewModel
            {

                BillingAccount = beginPayment.BillingAccount, // mostrado
                Currency = "Pesos", // mostrado
                CreateToken = "SI", // mostrado
                BeginPaymentId = beginPayment.BeginPaymentId, // incluido en la vista como hiden
                Mp_account = "1", //incluido en la vista como hidden
                Mp_product = "1", //incluido en la vista como hidden
                Mp_order = beginPayment.ServiceRequest, // mostrado
                Mp_reference = beginPayment.PaymentReference, // incluido en la vista como hidden, PaymentReference BeginPayment. 
                Mp_node = "node", // incluido en la vista como hidden.
                Mp_concept = "concept", // incluido en la vista como hidden
                Mp_amount = "", // mostrado 
                Mp_customername = "", // mostrado
                Mp_signature = "", // incluido en la vista como hidden
                Mp_currency = "1", //Incluido en la vista como hidden 
                Mp_urlsuccess = "asa", //Incluido en la vista como hidden 
                Mp_urlfailure = "asa", //Incluido en la vista como hidden
                Mp_registersb = beginPayment.CreateToken //Incluido en la vista como hidden 
            };

            return viewModel;
        }

        private static string ComputeSha256Hash(string rawData,string secret)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                secret = secret ?? "";
                var encoding = new System.Text.ASCIIEncoding();
                byte[] keyByte = encoding.GetBytes(secret);
                byte[] messageBytes = encoding.GetBytes(rawData);

                using (var hmacsha256 = new HMACSHA256(keyByte))
                {
                    byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                    return Convert.ToBase64String(hashmessage);
                }
            }
        }
    }
}
