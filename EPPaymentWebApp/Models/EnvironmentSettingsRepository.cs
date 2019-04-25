using System;
using EPPaymentWebApp.Enums;
using EPPaymentWebApp.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace EPPaymentWebApp.Models
{
    public class EnvironmentSettingsRepository : IEnvironmentSettingsRepository
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly EnvironmentSettings _envSettings;
        

        public EnvironmentSettingsRepository(
            IHostingEnvironment hostingEnvironment
            
            )
        {
            _hostingEnvironment = hostingEnvironment;
            _envSettings = GetSettingsObject(_hostingEnvironment.EnvironmentName);

        }

        public string GetConnectionStringSetting()
        {
            return _envSettings.ConnectionString;
        }

        public string GetMpSK()
        {
            return _envSettings.MpSk;
        }

        public string GetUrlSuccess()
        {
            return _envSettings.UrlSuccess;
        }

        public string GetUrlFailure()
        {
            return _envSettings.UrlFailure;
        }



        private EnvironmentSettings GetSettingsObject(string environmentName)
        {
            return CreateSettingsObject(environmentName);
        }

        private EnvironmentSettings CreateSettingsObject(string hostingEnvironmentName)
        {

            EnvironmentSettings settingsObject = null;

            settingsObject = new EnvironmentSettings
            {
                MpSk = SelectSettingByEnvironment(
                    hostingEnvironmentName, 
                    EnvironmentSettingsEnum.MpSkDev.ToString(),
                    EnvironmentSettingsEnum.MpSkProd.ToString()),

                ConnectionString = SelectSettingByEnvironment(
                    hostingEnvironmentName,
                    EnvironmentSettingsEnum.ConnectionStringDev.ToString(),
                    EnvironmentSettingsEnum.ConnectionStringProd.ToString()),

                UrlSuccess = SelectSettingByEnvironment(
                    hostingEnvironmentName,
                    EnvironmentSettingsEnum.UrlSuccessDev.ToString(),
                    EnvironmentSettingsEnum.UrlSuccessProd.ToString()),

                UrlFailure = SelectSettingByEnvironment(
                    hostingEnvironmentName,
                    EnvironmentSettingsEnum.UrlFailureDev.ToString(),
                    EnvironmentSettingsEnum.UrlFailureProd.ToString()),
                    
            };

            //_dbLoggerRepository.LogCreatedEnvironmentSettingsObject(settingsObject);

            return settingsObject;
        }

        private string GetEnvironmentSetting(string envVariableName)
        {
            string envVariableString = null;

            return envVariableString ?? Environment.GetEnvironmentVariable(envVariableName,
                              EnvironmentVariableTarget.Machine);

        }

        private string SelectSettingByEnvironment(
            string hostingEnvironmentName , 
            string envSettingDevName,
            string envSettingDevProd)
        {

            return (hostingEnvironmentName == EnvironmentSettingsEnum.Development.ToString())
                ? GetEnvironmentSetting(envSettingDevName) :
                GetEnvironmentSetting(envSettingDevProd);
              
        }

    }
}








    

