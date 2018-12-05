using Dapper;
using EPPaymentWebApp.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EPPaymentWebApp.Models
{
    public class BeginPaymentRepository : IBeginPaymentRepository
    {

        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public BeginPaymentRepository(IConfiguration config)
        {
            _config = config;

            var environmentConnectionString = Environment.GetEnvironmentVariable("EpPaymentDevConnectionStringEnvironment", EnvironmentVariableTarget.Machine);

            var connectionString = !string.IsNullOrEmpty(environmentConnectionString)
                                   ? environmentConnectionString
                                   : _config.GetConnectionString("EpPaymentDevConnectionString");

            _connectionString = connectionString;
        }

        

        public IDbConnection Connection
        {
           

            get
            {
                return new SqlConnection(_connectionString);
                //return new SqlConnection("server=100.125.0.119; Database=EnterprisePaymentWebDB; Integrated Security = false; Password =U_d3s_4dm1_#00; User Id = co_jcarrillog;");
            }
        }

        public BeginPayment GetByServiceRequest(string ServiceRequest)
        {
            BeginPayment result = new BeginPayment();

            using (IDbConnection conn = Connection)
                {
               
                  try
                  {
                    conn.Open();
                     result =
                               conn.QueryFirstOrDefault
                               <BeginPayment>
                               (
                                   "SP_EP_GET_BEGINPAYMENT_BY_SERVICEREQUEST",
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
