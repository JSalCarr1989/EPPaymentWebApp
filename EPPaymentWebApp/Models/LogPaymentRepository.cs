using System;
using System.Data;
using EPPaymentWebApp.Interfaces;
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
            LogPayment result = new LogPayment();

            using (_conn)
            {
                try
                {

                    var parameters = new DynamicParameters();
                    parameters.Add("@PAYMENT_REQUEST_AMOUNT", amount);
                    parameters.Add("@SERVICE_REQUEST", serviceRequest);
                    parameters.Add("@PAYMENT_REFERENCE", paymentReference);
                    parameters.Add("@STATUS_PAYMENT", StatusPayment);

                    _conn.Open();

                    result = _conn.QueryFirst<LogPayment>(
                                           "GET_LAST_REQUESTPAYMENT_ID",
                                           parameters, 
                                           commandType: CommandType.StoredProcedure
                                           );


                    _dbLoggerRepository.LogGetLastRequestPaymentId(amount, serviceRequest, paymentReference, StatusPayment, result.RequestPaymentId);

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
