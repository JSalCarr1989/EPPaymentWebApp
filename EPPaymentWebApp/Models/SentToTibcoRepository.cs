using System.Data;
using EPPaymentWebApp.Interfaces;
using EPPaymentWebApp.Utilities;
using Dapper;

namespace EPPaymentWebApp.Models
{
    public class SentToTibcoRepository : ISentToTibcoRepository
    {

        private readonly IDbConnectionRepository _dbConnectionRepository;
        private readonly IDbLoggerRepository _dbLoggerRepository;
        private readonly IDbConnection _conn;

        public SentToTibcoRepository(IDbConnectionRepository dbConnectionRepository, IDbLoggerRepository dbLoggerRepository)
        {
            _dbConnectionRepository = dbConnectionRepository;
            _dbLoggerRepository = dbLoggerRepository;
            _conn = _dbConnectionRepository.CreateDbConnection();
        }



        public bool GetEndPaymentSentToTibco(
            string endPaymentStatusDescription, 
            string responsePaymentType, int responsePaymentId)
        {
            int sentToTibcoCount;
            bool sentToTibco;
            using (_conn)
            {

                var parameters = new DynamicParameters();


                parameters.Add(StaticSentToTibcoRepositoryProperties.END_PAYMENT_STATUS_DESCRIPTION, endPaymentStatusDescription);
                parameters.Add(StaticSentToTibcoRepositoryProperties.RESPONSE_PAYMENT_TYPE, responsePaymentType);
                parameters.Add(StaticSentToTibcoRepositoryProperties.RESPONSE_PAYMENT_ID, responsePaymentId);
                parameters.Add(StaticSentToTibcoRepositoryProperties.SENT_NUM,
    dbType: DbType.Int32,
    direction: ParameterDirection.Output);

                _conn.Open();

                _conn.Query(
                    StaticSentToTibcoRepositoryProperties.SP_GET_SENT_TO_TIBCO, 
                    parameters, 
                    commandType: CommandType.StoredProcedure
                    );

                sentToTibcoCount = parameters.Get<int>(StaticSentToTibcoRepositoryProperties.SENT_NUM_OUTPUT_SEARCH);

                sentToTibco = (sentToTibcoCount > 0) ? true : false;

                _dbLoggerRepository.LogGetSentExists(endPaymentStatusDescription, responsePaymentType, responsePaymentId, sentToTibco);

                return sentToTibco;

            }
        }
    }
}
