using EPPaymentWebApp.Interfaces;
using EPPaymentWebApp.Utilities;
using System.Data;
using Dapper;
using System;

namespace EPPaymentWebApp.Models
{
    public class EnterprisePaymentViewModelRepository : IEnterprisePaymentViewModelRepository
    {

        private readonly IDbConnectionRepository _dbConnectionRepository;
        private readonly IDbLoggerRepository _dbLoggerRepository;
        private readonly IDbLoggerErrorRepository _dbLoggerErrorRepository;
        private readonly IDbConnection _conn;
        private readonly IEnvironmentSettingsRepository _environmentSettingsRepository;

        public EnterprisePaymentViewModelRepository(
            IDbConnectionRepository dbConnectionRepository, 
            IDbLoggerRepository dbLoggerRepository,
            IDbLoggerErrorRepository dbLoggerErrorRepository,
            IEnvironmentSettingsRepository environmentSettingsRepository)
        {
            _dbConnectionRepository = dbConnectionRepository;
            _dbLoggerRepository = dbLoggerRepository;
            _dbLoggerErrorRepository = dbLoggerErrorRepository;
            _conn = _dbConnectionRepository.CreateDbConnection();
            _environmentSettingsRepository = environmentSettingsRepository;

        }

        public EnterprisePaymentViewModel GenerateEnterprisePaymentViewModelRequestPayment(BeginPayment beginPayment)
        {
            EnterprisePaymentViewModel viewModel = null;

            try
            {
                viewModel = new EnterprisePaymentViewModel
                {

                    BillingAccount = beginPayment.BillingAccount, // mostrado
                    Currency = "Pesos", // mostrado
                    CreateToken = (beginPayment.CreateToken == "1") ? "SI" : "NO", // mostrado
                    BeginPaymentId = beginPayment.BeginPaymentId, // incluido en la vista como hiden
                    Mp_account = "7581", //mp_account de preproduccion.
                    Mp_product = "1", //incluido en la vista como hidden
                    Mp_order = beginPayment.ServiceRequest, // mostrado
                    Mp_reference = beginPayment.PaymentReference, // incluido en pola vista como hidden, PaymentReference BeginPayment. 
                    Mp_node = "0", // incluido en la vista como hidden.
                    Mp_concept = "1", // incluido en la vista como hidden
                    Mp_amount = "", // mostrado 
                    Mp_customername = "", // mostrado
                    Mp_signature = "", // incluido en la vista como hidden
                    Mp_currency = "1", //Incluido en la vista como hidden 
                    Mp_urlsuccess = _environmentSettingsRepository.GetUrlSuccess(), //Incluido en la vista como hidden 
                    Mp_urlfailure = _environmentSettingsRepository.GetUrlSuccess(), //Incluido en la vista como hidden
                    Mp_registersb = beginPayment.CreateToken, //Incluido en la vista como hidden 
                    BankResponse = string.Empty,
                    TransactionNumber = string.Empty,
                    Token = string.Empty,
                    CcLastFour = string.Empty,
                    IssuingBank = string.Empty,
                    CcType = string.Empty
                };
            }
            catch (Exception ex)
            {
                _dbLoggerErrorRepository.LogGenerateEnterprisePaymentViewModelRequestPaymentError(ex.ToString(), beginPayment.PaymentReference, beginPayment.ServiceRequest);
            }

            return viewModel;
        }

        public EnterprisePaymentViewModel GetEnterprisePaymentViewModel(string serviceRequest, string paymentReference)
        {

            EnterprisePaymentViewModel result = null;

            try
            {
                using (_conn)
                {
                    _conn.Open();
                    result = _conn.QueryFirst<EnterprisePaymentViewModel>(
                        StaticEnterprisePaymentViewModelProperties.SP_GET_ENTERPRISEPAYMENT_VIEW_MODEL_BY_SR_PR,
                        new
                        {
                            SERVICE_REQUEST = serviceRequest,
                            PAYMENT_REFERENCE = paymentReference
                        },
                        commandType: CommandType.StoredProcedure);

                    _dbLoggerRepository.LogShowedDataInView(result);

               
                }
            }
            catch(Exception ex)
            {
                _dbLoggerErrorRepository.LogGetEnterprisePaymentViewModelError(ex.ToString());
            }
            return result;
        }
    }
}
