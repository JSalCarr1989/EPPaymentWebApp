using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EPPaymentWebApp.Interfaces;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace EPPaymentWebApp.Models
{
    public class LogPaymentRepository : ILogPaymentRepository
    {

        private IConfiguration _config;

        public LogPaymentRepository(IConfiguration config)
        {
            _config = config;

        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("EpPaymentDevConnectionString"));
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

                    conn.Open();

                    result = conn.QueryFirstOrDefault("GET_LAST_REQUESTPAYMENT_ID",parameters, commandType: CommandType.StoredProcedure);

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
