using System;
using EPPaymentWebApp.Models;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace EPPaymentWebApp.Helpers
{
    public static class EnterprisePaymentHelpers
    {
        public static EnterprisePaymentViewModel GenerateEnterprisePaymentViewModelRequestPayment(BeginPayment beginPayment)
        {
            
            var viewModel = new EnterprisePaymentViewModel
            {

                BillingAccount = beginPayment.BillingAccount, // mostrado
                Currency = "Pesos", // mostrado
                CreateToken = (beginPayment.CreateToken == "1") ? "SI" : "NO", // mostrado
                BeginPaymentId = beginPayment.BeginPaymentId, // incluido en la vista como hiden
                Mp_account = "7581", //mp_account de preproduccion.
                Mp_product = "1", //incluido en la vista como hidden
                Mp_order = beginPayment.ServiceRequest, // mostrado
                Mp_reference = beginPayment.PaymentReference, // incluido en la vista como hidden, PaymentReference BeginPayment. 
                Mp_node = "0", // incluido en la vista como hidden.
                Mp_concept = "1", // incluido en la vista como hidden
                Mp_amount = "", // mostrado 
                Mp_customername = "", // mostrado
                Mp_signature = "", // incluido en la vista como hidden
                Mp_currency = "1", //Incluido en la vista como hidden 
                Mp_urlsuccess = "https://100.125.0.119:443/Home/HandleResponse", //Incluido en la vista como hidden 
                Mp_urlfailure = "https://100.125.0.119:443/Home/HandleResponse", //Incluido en la vista como hidden
                Mp_registersb = beginPayment.CreateToken, //Incluido en la vista como hidden 
                BankResponse = string.Empty,
                TransactionNumber = string.Empty,
                Token = string.Empty,
                CcLastFour = string.Empty,
                IssuingBank = string.Empty,
                CcType = string.Empty
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
                    return ByteToString(hashmessage);
                }
            }
        }

        public static string ByteToString(byte[] buff)
        {
            string sbinary = "";


            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary).ToLower();
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
                MpDate =  (string.IsNullOrWhiteSpace(multiPagosResponse.mp_date)) ? DateTime.Now :Convert.ToDateTime(multiPagosResponse.mp_date),
                MpBankName = multiPagosResponse.mp_bankname,
                MpFolio = string.IsNullOrWhiteSpace(multiPagosResponse.mp_folio) ? "NO_GENERADO" : multiPagosResponse.mp_folio,
                MpSbToken = string.IsNullOrWhiteSpace(multiPagosResponse.mp_sbtoken) ? "NO_GENERADO" : multiPagosResponse.mp_sbtoken,
                MpSaleId = multiPagosResponse.mp_saleid,
                MpCardHolderName = multiPagosResponse.mp_cardholdername,
                ResponsePaymentTypeDescription = "MULTIPAGOS_POST",
                ResponsePaymentHashStatusDescription = hashStatus,
                PaymentRequestId = requestPaymentId
            };

            return _responsePaymentDTO;
        }

        public static Boolean ValidateMultipagosHash(MultiPagosResponsePaymentDTO multipagosResponse,IConfiguration config,ILogger logger)
        {
            bool result;
            
            var rawData = multipagosResponse.mp_order + multipagosResponse.mp_reference + multipagosResponse.mp_amount + multipagosResponse.mp_authorization;

            var environmentMpSk = Environment.GetEnvironmentVariable("MpSk", EnvironmentVariableTarget.Machine);

            var _mpsk = !string.IsNullOrEmpty(environmentMpSk)
                                   ? environmentMpSk
                                   : config["MpSk"];

            var generatedHash = ComputeSha256Hash(rawData, _mpsk);

            result = (generatedHash == multipagosResponse.mp_signature) ? true : false;

            EnterprisePaymentDbLogHelpers.LogHashValidationToDb(logger,multipagosResponse, rawData,generatedHash, (generatedHash == multipagosResponse.mp_signature) ? "HASH_VALIDO" : "HASH_INVALIDO");

            return result;
        }
    }
}
