using System;
using System.Data;
using EPPaymentWebApp.Interfaces;
using EPPaymentWebApp.Utilities;
using Dapper;

namespace EPPaymentWebApp.Models
{
    public class LogPaymentRepository : ILogPaymentRepository
    {

       
        
        private readonly IDbLoggerRepository _dbLoggerRepository;
        private readonly IDbConnectionRepository _connectionRepository;
        private readonly IDbConnection _conn;
        

        public LogPaymentRepository(
                                    IDbLoggerRepository dbLoggerRepository,
                                    IDbConnectionRepository connectionRepository
                                   )
        {
            
            _connectionRepository = connectionRepository;
            _dbLoggerRepository = dbLoggerRepository;

            _conn = _connectionRepository.CreateDbConnection();

        }

        public LogPayment GetLastRequestPaymentId(
                                                  decimal amount, 
                                                  string serviceRequest, 
                                                  string paymentReference, 
                                                  string StatusPayment
                                                 )
        {
            LogPayment result = null;



            try
            {
                using (_conn)
                {

                    var parameters = new DynamicParameters();
                    parameters.Add(StaticLogPaymentProperties.PAYMENT_REQUEST_AMOUNT, amount);
                    parameters.Add(StaticLogPaymentProperties.SERVICE_REQUEST, serviceRequest);
                    parameters.Add(StaticLogPaymentProperties.PAYMENT_REFERENCE, paymentReference);
                    parameters.Add(StaticLogPaymentProperties.STATUS_PAYMENT, StatusPayment);

                    _conn.Open();
                    result = _conn.QueryFirst<LogPayment>(
                                           StaticLogPaymentProperties.GET_LAST_REQUESTPAYMENT_ID,
                                           parameters,
                                           commandType: CommandType.StoredProcedure
                                           );

                    _dbLoggerRepository.LogGetLastRequestPaymentId(amount, serviceRequest, paymentReference, StatusPayment, result.RequestPaymentId);
                }
            }
            catch (Exception ex)
            {
                //log de error
                ex.ToString();
            }


            return result;

        }
    }
}
