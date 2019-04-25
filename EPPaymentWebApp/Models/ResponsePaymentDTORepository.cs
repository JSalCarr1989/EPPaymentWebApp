using System;
using EPPaymentWebApp.Interfaces;
using EPPaymentWebApp.Utilities;

namespace EPPaymentWebApp.Models
{
    public class ResponsePaymentDTORepository : IResponsePaymentDTORepository
    {

        private readonly IDbLoggerErrorRepository _dbLoggerErrorRepository;

        public ResponsePaymentDTORepository(IDbLoggerErrorRepository dbLoggerErrorRepository)
        {
            _dbLoggerErrorRepository = dbLoggerErrorRepository;
        }

        public ResponsePaymentDTO GenerateResponsePaymentDTO(MultiPagosResponsePaymentDTO multiPagosResponse, int requestPaymentId, string hashStatus)
        {
            ResponsePaymentDTO _responsePaymentDTO = null;

            try
            {
                _responsePaymentDTO = new ResponsePaymentDTO
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
                    MpDate = (string.IsNullOrWhiteSpace(multiPagosResponse.mp_date)) ? DateTime.Now : Convert.ToDateTime(multiPagosResponse.mp_date),
                    MpBankName = multiPagosResponse.mp_bankname,
                    MpFolio = string.IsNullOrWhiteSpace(multiPagosResponse.mp_folio) ? "NO_GENERADO" : multiPagosResponse.mp_folio,
                    MpSbToken = string.IsNullOrWhiteSpace(multiPagosResponse.mp_sbtoken) ? "NO_GENERADO" : multiPagosResponse.mp_sbtoken,
                    MpSaleId = multiPagosResponse.mp_saleid,
                    MpCardHolderName = multiPagosResponse.mp_cardholdername,
                    ResponsePaymentTypeDescription = StaticResponsePaymentProperties.RESPONSEPAYMENT_TYPE_POST,
                    ResponsePaymentHashStatusDescription = hashStatus,
                    PaymentRequestId = requestPaymentId
                };
            }
            catch (Exception ex)
            {
                _dbLoggerErrorRepository.LogGenerateResponsePaymentDTOError(ex.ToString(), multiPagosResponse.mp_reference, multiPagosResponse.mp_order);
            }

            return _responsePaymentDTO;
        }
    }
}
