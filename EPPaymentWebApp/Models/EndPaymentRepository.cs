using System;
using System.Data;
using EPPaymentWebApp.Interfaces;
using EPPaymentWebApp.Utilities;
using Dapper;

namespace EPPaymentWebApp.Models
{
    public class EndPaymentRepository : IEndPaymentRepository
    {

        private readonly IDbConnectionRepository _dbConnectionRepository;
        private readonly IDbLoggerRepository _dbLoggerRepository;
        private readonly IDbLoggerErrorRepository _dbLoggerErrorRepository;
        private readonly IDbConnection _conn;

        public EndPaymentRepository(IDbConnectionRepository dbConnectionRepository, 
            IDbLoggerRepository dbLoggerRepository,
            IDbLoggerErrorRepository dbLoggerErrorRepository)
        {
            _dbConnectionRepository = dbConnectionRepository;
            _dbLoggerRepository = dbLoggerRepository;
            _dbLoggerErrorRepository = dbLoggerErrorRepository;
            _conn = _dbConnectionRepository.CreateDbConnection();
        }


        public EndPayment GetEndPaymentByResponsePaymentId(int responsePaymentId)
        {
            EndPayment endPayment = null;

            try
            {
                using (_conn)
                {
                    _conn.Open();

                    endPayment = _conn.QueryFirst<EndPayment>(
                        StaticEndPaymentProperties.SP_EP_GET_ENDPAYMENT_BY_RESPONSEPAYMENT_ID,
                        new { RESPONSEPAYMENT_ID = responsePaymentId },
                        commandType: CommandType.StoredProcedure
                        );

                    _dbLoggerRepository.LogGetEndPayment(responsePaymentId, endPayment);

                    
                }
            }
            catch (Exception ex)
            {
                _dbLoggerErrorRepository.LogGetEndPaymentByResponsePaymentIdError(ex.ToString());
            }

            return endPayment;

        }

        public void UpdateEndPaymentSentStatus(int endPaymentId,string endPaymentSentStatus)
        {
            try
            {
                using (_conn)
                {

                    var parameters = new DynamicParameters();

                    parameters.Add(StaticEndPaymentProperties.ENDPAYMENT_ID, endPaymentId);
                    parameters.Add(StaticEndPaymentProperties.ENDPAYMENT_SENT_STATUS, endPaymentSentStatus);

                    _conn.Open();

                    _conn.Query(StaticEndPaymentProperties.UPDATE_ENDPAYMENT_SENT_STATUS, parameters, commandType: CommandType.StoredProcedure);

                    _dbLoggerRepository.LogUpdateEndPaymentSentStatus(endPaymentId, endPaymentSentStatus);

                }
            }
            catch(Exception ex)
            {
                _dbLoggerErrorRepository.LogUpdateEndPaymentSentStatusError(ex.ToString());
            }
        }
    }
}
