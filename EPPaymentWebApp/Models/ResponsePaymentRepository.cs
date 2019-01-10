using EPPaymentWebApp.Interfaces;
using EPPaymentWebApp.Utilities;
using System.Data;
using Dapper;
using System;

namespace EPPaymentWebApp.Models
{
    public class ResponsePaymentRepository : IResponsePaymentRepository
    {

        private readonly IDbConnectionRepository _dbConnectionRepository;
        private readonly IDbLoggerRepository _dbLoggerRepository;
        private readonly IDbLoggerErrorRepository _dbLoggerErrorRepository;
        private readonly IDbConnection _conn;

        public ResponsePaymentRepository(
                                        IDbConnectionRepository dbConnectionRepository,
                                        IDbLoggerRepository dbLoggerRepository,
                                        IDbLoggerErrorRepository dbLoggerErrorRepository
                                        )
        {
            _dbConnectionRepository = dbConnectionRepository;
            _dbLoggerRepository = dbLoggerRepository;
            _dbLoggerErrorRepository = dbLoggerErrorRepository;
            _conn = _dbConnectionRepository.CreateDbConnection();

        }


        public int CreateResponsePayment(ResponsePaymentDTO responseDTO)
        {
            int responsePaymentId = 0;


            try
            {
                using (_conn)
                {


                    var parameters = new DynamicParameters();

                    parameters.Add(StaticResponsePaymentProperties.MP_ORDER, responseDTO.MpOrder);
                    parameters.Add(StaticResponsePaymentProperties.MP_REFERENCE, responseDTO.MpReference);
                    parameters.Add(StaticResponsePaymentProperties.MP_AMOUNT, responseDTO.MpAmount);
                    parameters.Add(StaticResponsePaymentProperties.MP_PAYMENT_METHOD, responseDTO.MpPaymentMethod);
                    parameters.Add(StaticResponsePaymentProperties.MP_RESPONSE, responseDTO.MpResponse);
                    parameters.Add(StaticResponsePaymentProperties.MP_RESPONSE_MSG, responseDTO.MpResponseMsg);
                    parameters.Add(StaticResponsePaymentProperties.MP_AUTHORIZATION, responseDTO.MpAuthorization);
                    parameters.Add(StaticResponsePaymentProperties.MP_SIGNATURE, responseDTO.MpSignature);
                    parameters.Add(StaticResponsePaymentProperties.MP_PAN, responseDTO.MpPan);
                    parameters.Add(StaticResponsePaymentProperties.MP_DATE, responseDTO.MpDate);
                    parameters.Add(StaticResponsePaymentProperties.MP_BANK_NAME, responseDTO.MpBankName);
                    parameters.Add(StaticResponsePaymentProperties.MP_FOLIO, responseDTO.MpFolio);
                    parameters.Add(StaticResponsePaymentProperties.MP_SB_TOKEN, responseDTO.MpSbToken);
                    parameters.Add(StaticResponsePaymentProperties.MP_SALE_ID, responseDTO.MpSaleId);
                    parameters.Add(StaticResponsePaymentProperties.MP_CARDHOLDER_NAME, responseDTO.MpCardHolderName);
                    parameters.Add(StaticResponsePaymentProperties.RESPONSE_PAYMENT_TYPE_LIST_DESCRIPTION, responseDTO.ResponsePaymentTypeDescription);
                    parameters.Add(StaticResponsePaymentProperties.RESPONSE_PAYMENT_HASH_STATUS_DESCRIPTION, responseDTO.ResponsePaymentHashStatusDescription);
                    parameters.Add(StaticResponsePaymentProperties.REQUEST_PAYMENT_ID, responseDTO.PaymentRequestId);
                    parameters.Add(StaticResponsePaymentProperties.RESPONSE_PAYMENT_ID,
                                    dbType: DbType.Int32,
                                    direction: ParameterDirection.Output);

                    _conn.Open();

                    _conn.Query(
                        StaticResponsePaymentProperties.SP_CREATE_RESPONSE_ENTERPRISE_PAYMENT,
                        parameters,
                        commandType: CommandType.StoredProcedure
                        );

                    responsePaymentId = parameters.Get<int>(StaticResponsePaymentProperties.RESPONSE_PAYMENT_ID_OUTPUT_SEARCH);

                    _dbLoggerRepository.LogCreateResponsePayment(responseDTO, responsePaymentId);

                    

                }
            }
            catch (Exception ex)
            {
                _dbLoggerErrorRepository.LogCreateResponsePaymentError(ex.ToString(), responseDTO.MpReference, responseDTO.MpOrder);
            }

            return responsePaymentId;

        }
    }


    
}
