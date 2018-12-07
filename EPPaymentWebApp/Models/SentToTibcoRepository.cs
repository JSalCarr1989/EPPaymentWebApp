using System;
using System.Data;
using System.Data.SqlClient;
using EPPaymentWebApp.Interfaces;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace EPPaymentWebApp.Models
{
    public class SentToTibcoRepository : ISentToTibcoRepository
    {

        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public SentToTibcoRepository(IConfiguration config)
        {
            _config = config;

            var environmentConnectionString = Environment.GetEnvironmentVariable("EpPaymentDevConnectionStringEnvironment", EnvironmentVariableTarget.Machine);

            var connectionString = !string.IsNullOrEmpty(environmentConnectionString)
                                   ? environmentConnectionString
                                   : _config.GetConnectionString("EpPaymentDevConnectionString");

            _connectionString = connectionString;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
                
            }
        }



        public bool GetEndPaymentSentToTibco(string endPaymentStatusDescription, string responsePaymentType, int responsePaymentId)
        {
            int sentToTibcoCount;
            using (IDbConnection conn = Connection)
            {

                var parameters = new DynamicParameters();


                parameters.Add("@END_PAYMENT_STATUS_DESCRIPTION", endPaymentStatusDescription);
                parameters.Add("@RESPONSE_PAYMENT_TYPE", responsePaymentType);
                parameters.Add("@RESPONSE_PAYMENT_ID", responsePaymentId);
                parameters.Add("@SENT_NUM",
    dbType: DbType.Int32,
    direction: ParameterDirection.Output);

                conn.Open();

                conn.Query("SP_GET_SENT_TO_TIBCO", parameters, commandType: CommandType.StoredProcedure);

                sentToTibcoCount = parameters.Get<int>("SENT_NUM");

                return (sentToTibcoCount > 0) ? true : false;

            }
        }
    }
}
