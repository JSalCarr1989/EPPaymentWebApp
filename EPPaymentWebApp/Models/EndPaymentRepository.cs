using System;
using System.Data;
using EPPaymentWebApp.Interfaces;
using Dapper;

namespace EPPaymentWebApp.Models
{
    public class EndPaymentRepository : IEndPaymentRepository
    {

        private readonly IDbConnectionRepository _dbConnectionRepository;
        private readonly IDbLoggerRepository _dbLoggerRepository;
        private readonly IDbConnection _conn;

        public EndPaymentRepository(IDbConnectionRepository dbConnectionRepository, 
            IDbLoggerRepository dbLoggerRepository)
        {
            _dbConnectionRepository = dbConnectionRepository;
            _dbLoggerRepository = dbLoggerRepository;
            _conn = _dbConnectionRepository.CreateDbConnection();
        }


        public EndPayment GetEndPaymentByResponsePaymentId(int responsePaymentId)
        {
            using (_conn)
            {
                _conn.Open();

                var result = _conn.QueryFirst<EndPayment>(
                    "SP_EP_GET_ENDPAYMENT_BY_RESPONSEPAYMENT_ID", 
                    new { RESPONSEPAYMENT_ID = responsePaymentId},
                    commandType: CommandType.StoredProcedure
                    );

                _dbLoggerRepository.LogGetEndPayment(responsePaymentId,result);

                return result;
            }
        }

        public void UpdateEndPaymentSentStatus(int endPaymentId,string endPaymentSentStatus)
        {
            using (_conn)
            {
              
               var parameters = new DynamicParameters();

               parameters.Add("@ENDPAYMENT_ID", endPaymentId);
               parameters.Add("@ENDPAYMENT_SENT_STATUS", endPaymentSentStatus);

                _conn.Open();

                _conn.Query("UPDATE_ENDPAYMENT_SENT_STATUS",parameters,commandType: CommandType.StoredProcedure);

                _dbLoggerRepository.LogUpdateEndPaymentSentStatus(endPaymentId, endPaymentSentStatus);
                
            }
        }
        //Codigo en desuso
        public bool ValidateEndPaymentSentStatus(int endPaymentId)
        {
            using (_conn)
            {
                _conn.Open();
                var result = _conn.QueryFirstOrDefault<String>("GET_ENDPAYMENT_SENT_STATUS_BY_ID", new { ENDPAYMENT_ID = endPaymentId }, commandType: CommandType.StoredProcedure);
                return (result == "ENVIADO_TIBCO") ? true : false;
            }
        }
    }
}
