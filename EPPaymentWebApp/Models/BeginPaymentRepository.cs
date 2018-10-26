﻿using Dapper;
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

        public BeginPaymentRepository(IConfiguration config)
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
