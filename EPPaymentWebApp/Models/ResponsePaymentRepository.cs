using EPPaymentWebApp.Interfaces;
using System.Data;
using Dapper;

namespace EPPaymentWebApp.Models
{
    public class ResponsePaymentRepository : IResponsePaymentRepository
    {

        private readonly IDbConnectionRepository _dbConnectionRepository;
        private readonly IDbLoggerRepository _dbLoggerRepository;
        private readonly IDbConnection _conn;

        public ResponsePaymentRepository(
                                        IDbConnectionRepository dbConnectionRepository,
                                        IDbLoggerRepository dbLoggerRepository
                                        )
        {
            _dbConnectionRepository = dbConnectionRepository;
            _dbLoggerRepository = dbLoggerRepository;
            _conn = _dbConnectionRepository.CreateDbConnection();

        }


        public int CreateResponsePayment(ResponsePaymentDTO responseDTO)
        {
            int responsePaymentId;
            using (_conn)
            {
                var parameters = new DynamicParameters();

                parameters.Add("@MP_ORDER", responseDTO.MpOrder);
                parameters.Add("@MP_REFERENCE", responseDTO.MpReference);
                parameters.Add("@MP_AMOUNT", responseDTO.MpAmount);
                parameters.Add("@MP_PAYMENT_METHOD", responseDTO.MpPaymentMethod);
                parameters.Add("@MP_RESPONSE", responseDTO.MpResponse);
                parameters.Add("@MP_RESPONSE_MSG", responseDTO.MpResponseMsg);
                parameters.Add("@MP_AUTHORIZATION", responseDTO.MpAuthorization);
                parameters.Add("@MP_SIGNATURE", responseDTO.MpSignature);
                parameters.Add("@MP_PAN", responseDTO.MpPan);
                parameters.Add("@MP_DATE", responseDTO.MpDate);
                parameters.Add("@MP_BANK_NAME", responseDTO.MpBankName);
                parameters.Add("@MP_FOLIO", responseDTO.MpFolio);
                parameters.Add("@MP_SB_TOKEN", responseDTO.MpSbToken);
                parameters.Add("@MP_SALE_ID", responseDTO.MpSaleId);
                parameters.Add("@MP_CARHOLDER_NAME", responseDTO.MpCardHolderName);
                parameters.Add("@RESPONSE_PAYMENT_TYPE_LIST_DESCRIPTION", responseDTO.ResponsePaymentTypeDescription);
                parameters.Add("@RESPONSE_PAYMENT_HASH_STATUS_DESCRIPTION", responseDTO.ResponsePaymentHashStatusDescription);
                parameters.Add("@REQUEST_PAYMENT_ID", responseDTO.PaymentRequestId);
                parameters.Add("@RESPONSE_PAYMENT_ID",
                                dbType: DbType.Int32,
                                direction: ParameterDirection.Output);

                _conn.Open();

                _conn.Query(
                    "SP_CREATE_RESPONSE_ENTERPRISE_PAYMENT",
                    parameters,
                    commandType: CommandType.StoredProcedure
                    );

                responsePaymentId = parameters.Get<int>("RESPONSE_PAYMENT_ID");

                _dbLoggerRepository.LogCreateResponsePayment(responseDTO, responsePaymentId);

                return responsePaymentId;

            }
        }

        public ResponsePayment GetResponsePaymentById(int responsePaymentId)
        {
            using (_conn)
            {
                _conn.Open();
                var result = _conn.QueryFirst<ResponsePayment>("SP_EP_GET_RESPONSEPAYMENT_BY_ID", new { RESPONSE_PAYMENT_ID = responsePaymentId}, commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }


    
}
