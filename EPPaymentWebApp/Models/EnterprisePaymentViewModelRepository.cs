using EPPaymentWebApp.Interfaces;
using EPPaymentWebApp.Utilities;
using System.Data;
using Dapper;


namespace EPPaymentWebApp.Models
{
    public class EnterprisePaymentViewModelRepository : IEnterprisePaymentViewModelRepository
    {

        private readonly IDbConnectionRepository _dbConnectionRepository;
        private readonly IDbLoggerRepository _dbLoggerRepository;
        private readonly IDbConnection _conn;

        public EnterprisePaymentViewModelRepository(IDbConnectionRepository dbConnectionRepository, IDbLoggerRepository dbLoggerRepository)
        {
            _dbConnectionRepository = dbConnectionRepository;
            _dbLoggerRepository = dbLoggerRepository;
            _conn = _dbConnectionRepository.CreateDbConnection();

        }


        public EnterprisePaymentViewModel GetEnterprisePaymentViewModel(string serviceRequest, string paymentReference)
        {
            using (_conn)
            {
                _conn.Open();
                var result = _conn.QueryFirst<EnterprisePaymentViewModel>(
                    StaticEnterprisePaymentViewModelProperties.SP_GET_ENTERPRISEPAYMENT_VIEW_MODEL_BY_SR_PR, 
                    new {SERVICE_REQUEST = serviceRequest,
                        PAYMENT_REFERENCE =paymentReference }, 
                    commandType: CommandType.StoredProcedure);

                _dbLoggerRepository.LogShowedDataInView(result);

                return result;
            }
        }
    }
}
