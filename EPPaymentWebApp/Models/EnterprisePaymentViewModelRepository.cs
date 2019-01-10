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

        public EnterprisePaymentViewModelRepository(
            IDbConnectionRepository dbConnectionRepository, 
            IDbLoggerRepository dbLoggerRepository,
            IDbLoggerErrorRepository dbLoggerErrorRepository)
        {
            _dbConnectionRepository = dbConnectionRepository;
            _dbLoggerRepository = dbLoggerRepository;
            _dbLoggerErrorRepository = dbLoggerErrorRepository;
            _conn = _dbConnectionRepository.CreateDbConnection();

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
