using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPPaymentWebApp.Models;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EPPaymentWebApp.Helpers
{
    public static class EnterprisePaymentHelpers
    {

        public static void SetObjectAsJson(this ISession session, string key, object value)
        { 
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session,string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);

        }

        public static EnterprisePaymentViewModel GenerateEnterprisePaymentViewModel(BeginPayment beginPayment)
        {
            var viewModel = new EnterprisePaymentViewModel
            {

                BillingAccount = beginPayment.BillingAccount, // mostrado
                Currency = "Pesos", // mostrado
                CreateToken = (beginPayment.CreateToken == "1") ? "SI" : "NO", // mostrado
                BeginPaymentId = beginPayment.BeginPaymentId, // incluido en la vista como hiden
                Mp_account = "7581", //incluido en la vista como hidden
                Mp_product = "1", //incluido en la vista como hidden
                Mp_order = beginPayment.ServiceRequest, // mostrado
                Mp_reference = beginPayment.PaymentReference, // incluido en la vista como hidden, PaymentReference BeginPayment. 
                Mp_node = "0", // incluido en la vista como hidden.
                Mp_concept = "1", // incluido en la vista como hidden
                Mp_amount = "", // mostrado 
                Mp_customername = "", // mostrado
                Mp_signature = "", // incluido en la vista como hidden
                Mp_currency = "1", //Incluido en la vista como hidden 
                Mp_urlsuccess = "asa", //Incluido en la vista como hidden 
                Mp_urlfailure = "asa", //Incluido en la vista como hidden
                Mp_registersb = beginPayment.CreateToken, //Incluido en la vista como hidden 
                BankResponse = "123456",
                TransactionNumber = "12345678910",
                Token = "TOKEN",
                CcLastFour = "1111",
                IssuingBank = "BANCOMER",
                CcType = "VISA"
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

        public static ResponsePaymentDTO GenerateResponsePaymentDTO(MultiPagosResponsePaymentDTO multiPagosResponse,int requestPaymentId, string hashStatus)
        {
            ResponsePaymentDTO _responsePaymentDTO = new ResponsePaymentDTO
            {
                MpOrder = multiPagosResponse.mp_order,
                MpReference = multiPagosResponse.mp_reference,
                MpAmount = multiPagosResponse.mp_amount,
                MpPaymentMethod = multiPagosResponse.mp_paymentmethod,
                MpResponse = multiPagosResponse.mp_response,
                MpResponseMsg = multiPagosResponse.mp_responsemsg,
                MpAuthorization = multiPagosResponse.mp_authorization,
                MpSignature = multiPagosResponse.mp_signature,
                MpPan = multiPagosResponse.mp_pan,
                MpDate = multiPagosResponse.mp_date,
                MpBankName = multiPagosResponse.mp_bankname,
                MpFolio = multiPagosResponse.mp_folio,
                MpSbToken = multiPagosResponse.mp_sbtoken,
                MpSaleId = multiPagosResponse.mp_saleid,
                MpCardHolderName = multiPagosResponse.mp_cardholdername,
                ResponsePaymentTypeDescription = "MULTIPAGOS_POST",
                ResponsePaymentHashStatusDescription = hashStatus,
                RequestPaymentId = requestPaymentId
            };

            return _responsePaymentDTO;
        }

        public static Boolean ValidateMultipagosHash(MultiPagosResponsePaymentDTO multipagosResponse)
        {
            var rawData = multipagosResponse.mp_order + multipagosResponse.mp_reference + multipagosResponse.mp_amount + multipagosResponse.mp_authorization;
            var myHash = ComputeSha256Hash(rawData,"secretkey");

            if(myHash == multipagosResponse.mp_signature)
            {

                return true;

            }

            return false;
        }
    }
}
