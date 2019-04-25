using EPPaymentWebApp.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EPPaymentWebApp.Models
{
    public class DbConnectionRepository : IDbConnectionRepository
    {
        private readonly IConfiguration _config;
        private readonly IEnvironmentSettingsRepository _environmentSettingsRepository;
        private readonly string _environmentConnectionString;

        public DbConnectionRepository(IConfiguration config,IEnvironmentSettingsRepository environmentSettingsRepository)
        {
            _environmentSettingsRepository = environmentSettingsRepository;
            _environmentConnectionString = _environmentSettingsRepository.GetConnectionStringSetting();
            _config = config;
        }

        public IDbConnection CreateDbConnection()
        {
            return new SqlConnection(GetEpPaymentConnectionString());
        }

        public string GetEpPaymentConnectionString()
        {
            var connectionString = !string.IsNullOrEmpty(_environmentConnectionString)
                                          ? _environmentConnectionString
                                          : _config.GetConnectionString("EpPaymentDevConnectionString");
            return connectionString;
        }


    }
}
