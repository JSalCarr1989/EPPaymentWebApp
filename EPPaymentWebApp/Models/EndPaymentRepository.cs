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
    public class EndPaymentRepository : IEndPaymentRepository
    {
        private readonly IConfiguration _config;

        public EndPaymentRepository(IConfiguration config)
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

        public EndPayment GetEndPaymentByResponsePaymentId(int responsePaymentId)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = conn.QueryFirstOrDefault<EndPayment>("SP_EP_GET_ENDPAYMENT_BY_RESPONSEPAYMENT_ID", new { RESPONSEPAYMENT_ID = responsePaymentId },commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public int UpdateEndPaymentSentStatus(int endPaymentId,string endPaymentSentStatus)
        {
            using (IDbConnection conn = Connection)
            {
              
               var parameters = new DynamicParameters();

               parameters.Add("@ENDPAYMENT_ID", endPaymentId);
               parameters.Add("@ENDPAYMENT_SENT_STATUS", endPaymentSentStatus);
               parameters.Add("@UPDATED_ENDPAYMENT_ID", 
                   dbType: DbType.Int32, 
                   direction: ParameterDirection.Output);

                conn.Open();

                var result = conn.QueryFirstOrDefault<int>("UPDATE_ENDPAYMENT_SENT_STATUS",parameters,commandType: CommandType.StoredProcedure);

                return result;
            }
        }

        public bool ValidateEndPaymentSentStatus(int endPaymentId)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = conn.QueryFirstOrDefault<String>("GET_ENDPAYMENT_SENT_STATUS_BY_ID", new { ENDPAYMENT_ID = endPaymentId }, commandType: CommandType.StoredProcedure);
                return (result == "ENVIADO_TIBCO") ? true : false;
            }
        }
    }
}
