using System;
using EPPaymentWebApp.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Serilog;
using Serilog.Formatting.Compact;


namespace EPPaymentWebApp.Models
{
    public class EnterprisePaymentViewModelRepository : IEnterprisePaymentViewModelRepository
    {

        private IConfiguration _config;
        private readonly string _connectionString;
        private readonly ILogger _logger;

        public EnterprisePaymentViewModelRepository(IConfiguration config)
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

        public EnterprisePaymentViewModel GetEnterprisePaymentViewModel(string serviceRequest, string paymentReference)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = conn.QueryFirst<EnterprisePaymentViewModel>("SP_GET_ENTERPRISEPAYMENT_VIEW_MODEL_BY_SR_PR", new {SERVICE_REQUEST = serviceRequest, PAYMENT_REFERENCE=paymentReference }, commandType: CommandType.StoredProcedure);

                return result;
            }
        }
    }
}
