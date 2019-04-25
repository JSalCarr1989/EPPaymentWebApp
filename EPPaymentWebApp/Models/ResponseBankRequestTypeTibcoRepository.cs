using System;
using System.Threading.Tasks;
using EPPaymentWebApp.Interfaces;
using TibcoServiceReference;

namespace EPPaymentWebApp.Models
{
    public class ResponseBankRequestTypeTibcoRepository : IResponseBankRequestTypeTibcoRepository
    {

        private readonly IDbLoggerRepository _dbLoggerRepository;
        private readonly IDbLoggerErrorRepository _dbLoggerErrorRepository;

        public ResponseBankRequestTypeTibcoRepository(IDbLoggerRepository dbLoggerRepository,
                                                      IDbLoggerErrorRepository dbLoggerErrorRepository)
        {
            _dbLoggerRepository = dbLoggerRepository;
            _dbLoggerErrorRepository = dbLoggerErrorRepository;
        }


        public async Task<string> SendEndPaymentToTibco(EndPayment endPayment)
        {

            //validamos si se envio

            string responseMsg = string.Empty;

            try
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
                    BillingAccount = endPayment.BillingAccount,
                    Monto = endPayment.Amount.ToString(),
                    TarjetaHabiente = endPayment.CardHolderName
                };

                ResponseBankClient responsebank = new ResponseBankClient();

                ResponseBankResponse response = await responsebank.ResponseBankAsync(request);

                _dbLoggerRepository.LogSendEndPaymentToTibco(request, response);

                responseMsg = response.ResponseBankResponse1.ErrorMessage;
            }
            catch (Exception ex)
            { 
                _dbLoggerErrorRepository.LogSendEndPaymentToTibcoError(ex.ToString());
            }


            return responseMsg;
        }
    }
}
