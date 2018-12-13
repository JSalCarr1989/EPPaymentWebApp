using System.Threading.Tasks;
using EPPaymentWebApp.Interfaces;
using TibcoServiceReference;
using EPPaymentWebApp.Helpers;
using Serilog;

namespace EPPaymentWebApp.Models
{
    public class ResponseBankRequestTypeTibcoRepository : IResponseBankRequestTypeTibcoRepository
    {




        public async Task<string> SendEndPaymentToTibco(EndPayment endPayment,ILogger log)
        {
            ResponseBankRequestType request = new ResponseBankRequestType
            {
                UltimosCuatroDigitos = endPayment.CcLastFour,
                Token = endPayment.Token,
                RespuestaBanco = endPayment.ResponseMessage,
                NumeroTransaccion = endPayment.TransactionNumber,
                TipoTarjeta = endPayment.CcType,
                BancoEmisor = endPayment.IssuingBank,
                SeviceRequest = endPayment.ServiceRequest,
                BillingAccount = endPayment.BillingAccount
            };

            ResponseBankClient responsebank = new ResponseBankClient();

            ResponseBankResponse response = await responsebank.ResponseBankAsync(request);

            EnterprisePaymentDbLogHelpers.LogSendEndPaymentToTibco(log,request,response);

            return response.ResponseBankResponse1.ErrorMessage;
        }
    }
}
