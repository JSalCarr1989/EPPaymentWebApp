using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPPaymentWebApp.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EPPaymentWebApp.Models
{
    public class ResponsePaymentRepository : IResponsePaymentRepository
    {

        private IConfiguration _config;

        public ResponsePaymentRepository(IConfiguration config)
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

        public int CreateResponsePayment(ResponsePaymentDTO responseDTO)
        {
            throw new NotImplementedException();
        }
    }


    
}
