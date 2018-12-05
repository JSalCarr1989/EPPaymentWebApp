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
    public class LogPaymentRepository : ILogPaymentRepository
    {

        private IConfiguration _config;
        private readonly string _connectionString;
        private readonly ILogger _logger;

        public LogPaymentRepository(IConfiguration config)
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

        public LogPayment GetLastRequestPaymentId(decimal amount, string serviceRequest, string paymentReference, string StatusPayment)
        {
            LogPayment result = new LogPayment();

            using (IDbConnection conn = Connection)
            {
                try
                {

                    var parameters = new DynamicParameters();
                    parameters.Add("@PAYMENT_REQUEST_AMOUNT", amount);
                    parameters.Add("@SERVICE_REQUEST", serviceRequest);
                    parameters.Add("@PAYMENT_REFERENCE", paymentReference);
                    parameters.Add("@STATUS_PAYMENT", StatusPayment);

                    _logger.Information(
        "Before Search Request Payment Id with the following data:" + 
        "amount: {amount}" +
        "service request: {serviceRequest}"+
        "payment reference: {paymentReference}" +
        "status payment: {StatusPayment}",
        amount,
        serviceRequest,
        paymentReference,
        StatusPayment
      );

                    conn.Open();

                    result = conn.QueryFirst<LogPayment>("GET_LAST_REQUESTPAYMENT_ID",parameters, commandType: CommandType.StoredProcedure);


                    _logger.Information(
"Results of search requestppaymentid:" +
"request payment id: {RequestPaymentId}",
 result.RequestPaymentId
);

                    return result;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return result;
                }
            }


        }
    }
}
