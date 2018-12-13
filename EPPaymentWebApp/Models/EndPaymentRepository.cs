using System;
using System.Data;
using System.Data.SqlClient;
using EPPaymentWebApp.Interfaces;
using Microsoft.Extensions.Configuration;
using Dapper;
using Serilog;
using Serilog.Formatting.Compact;

namespace EPPaymentWebApp.Models
{
    public class EndPaymentRepository : IEndPaymentRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;
        private readonly ILogger _logger;

        public EndPaymentRepository(IConfiguration config)
        {
            _config = config;

            var environmentConnectionString = Environment.GetEnvironmentVariable("EpPaymentDevConnectionStringEnvironment", EnvironmentVariableTarget.Machine);

            var connectionString = !string.IsNullOrEmpty(environmentConnectionString)
                       ? environmentConnectionString
                       : _config.GetConnectionString("EpPaymentDevConnectionString");

            _connectionString = connectionString;

            var logger = new LoggerConfiguration()
           .MinimumLevel.Debug()
           .WriteTo.RollingFile(new CompactJsonFormatter(),
                                 @"E:\LOG\EnterprisePaymentLog.json",
                                 shared: true,
                                 retainedFileCountLimit: 30
                                 )
          .CreateLogger();
            _logger = logger;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public EndPayment GetEndPaymentByResponsePaymentId(int responsePaymentId)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();

                _logger.Information(
"Before Search EndPayment:" +
"responsePaymentId: {responsePaymentId}",
responsePaymentId

);

                var result = conn.QueryFirst<EndPayment>("SP_EP_GET_ENDPAYMENT_BY_RESPONSEPAYMENT_ID", new { RESPONSEPAYMENT_ID = responsePaymentId},commandType: CommandType.StoredProcedure);

                _logger.Information(
"Results of search EndPayment:" +
"Response Code: {ResponseCode}"+
"Response Message: {ResponseMessage}",
result.ResponseCode,
result.ResponseMessage
);

                return result;
            }
        }

        public void UpdateEndPaymentSentStatus(int endPaymentId,string endPaymentSentStatus)
        {
            using (IDbConnection conn = Connection)
            {
              
               var parameters = new DynamicParameters();

               parameters.Add("@ENDPAYMENT_ID", endPaymentId);
               parameters.Add("@ENDPAYMENT_SENT_STATUS", endPaymentSentStatus);

                conn.Open();

                conn.Query("UPDATE_ENDPAYMENT_SENT_STATUS",parameters,commandType: CommandType.StoredProcedure);

                
            }
        }

        public bool ValidateEndPaymentSentStatus(int endPaymentId)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = conn.QueryFirstOrDefault<String>("GET_ENDPAYMENT_SENT_STATUS_BY_ID", new { ENDPAYMENT_ID = endPaymentId }, commandType: CommandType.StoredProcedure);
                return (result == "ENVIADO_TIBCO") ? true : false;
            }
        }
    }
}
