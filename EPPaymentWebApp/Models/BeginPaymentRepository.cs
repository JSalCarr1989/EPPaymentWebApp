using Dapper;
using EPPaymentWebApp.Interfaces;
using EPPaymentWebApp.Utilities;
using System;
using System.Data;

namespace EPPaymentWebApp.Models
{
    public class BeginPaymentRepository : IBeginPaymentRepository
    {

        private readonly IDbConnectionRepository _dbConnectionRepository;
        private readonly IDbLoggerRepository _dbLoggerRepository;
        private readonly IDbConnection _conn;

        public BeginPaymentRepository(IDbConnectionRepository dbConnectionRepository, 
                                      IDbLoggerRepository dbLoggerRepository)
        {
            _dbConnectionRepository = dbConnectionRepository;
            _dbLoggerRepository = dbLoggerRepository;
            _conn = _dbConnectionRepository.CreateDbConnection();
        }

       
        public BeginPayment GetByServiceRequest(string ServiceRequest)
        {
            BeginPayment result = new BeginPayment();

            using (_conn)
                {
               
                  try
                  {
                    _conn.Open();
                     result =
                               _conn.QueryFirstOrDefault
                               <BeginPayment>
                               (
                                   StaticBeginPaymentProperties.SP_EP_GET_BEGINPAYMENT_BY_SERVICEREQUEST,
                                   new { SERVICE_REQUEST = ServiceRequest },
                                   commandType: CommandType.StoredProcedure);
                    
                   }
                
                    catch (Exception ex)
                    {
                       Console.WriteLine(ex.ToString());
                    }
                return result;
            }
            


        }  
     
       
    }
}
