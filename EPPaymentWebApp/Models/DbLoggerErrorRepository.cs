using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPPaymentWebApp.Interfaces;
using Serilog;

namespace EPPaymentWebApp.Models
{
    public class DbLoggerErrorRepository : IDbLoggerErrorRepository
    {

        private readonly IDbConnectionRepository _connectionStringRepo;
        private readonly ILogger _logger;

        public DbLoggerErrorRepository(IDbConnectionRepository connectionStringRepo)
        {
            _connectionStringRepo = connectionStringRepo;

            var logger = new LoggerConfiguration()
               .MinimumLevel.Information()
               .WriteTo.MSSqlServer(_connectionStringRepo.GetEpPaymentConnectionString(), EpLogErrorTable)
               .CreateLogger();

            _logger = logger;
        }

        public static string EpLogErrorTable => "EpLogError";

        public static string LogSendEndpaymentToTibcoMessageTemplateError => @"Error en SendEndPaymentToTibco: {@error}";

        //public static string GetByServiceRequestMessageTemplateError => @"The error: {@error} has ocurred in GetByServiceRequestFunction for 
        //                                                                       the ServiceRequest:{@serviceRequest}";
        //public static string GenerateEnterprisePaymentViewModelRequestPaymentMessageTemplateError => @"The error: {@error} has ocurred in GenerateEnterprisePaymentViewModelRequestPayment
        //                                                                                               for the MpReference:{@mpReference} and MpOrder: {@mpOrder}";

        public static string LogCompute256HashMessageTemplateError => @"The error: {@error} has ocurred in Compute256Hash function";

        public static string LogByteToStringMessageTemplateError => @"The error: {@error} has ocurred in ByteToString function";

        public static string LogGenerateResponsePaymentDTOMessageTemplateError => @"The error: {@error} has ocurred in GenerateResponsePaymentDTO function 
                                                                                             for the MpReference:{@mpReference} and MpOrder:{@mpOrder}";

        public static string ValidateMultipagosHashMessageTemplateError => "The error: {@error} has ocurred for the MpReference:{@mpReference} and MpOrder:{@mpOrder}";

        public static string LogGetEndPaymentSentToTibcoMessageTemplateError => @"The error: {@error} has ocurred in GetEndPaymentSentToTibco function";

        public static string LogCreateResponsePaymentMessageTemplateError => @"The error: {@error} has ocurred in CreateResponsePayment function
                                                                                          for the MpReference:{@mpReference} and MpOrder: {@mpOrder}";

        public static string LogGetEndPaymentByResponsePaymentIdMessageTemplateError => @"The error: {@error} has ocurred in GetEndPaymentByResponsePaymentId function";

        public static string LogUpdateEndPaymentSentStatusMessageTemplateError => @"The error: {@error} has ocurred in UpdateEndPaymentSentStatus function";

        //public static string LogGetEnterprisePaymentViewModelMessageTemplateError => @"The error: {@error} has ocurred in GetEnterprisePaymentViewModel function";

        //public void (string error, string mpReference, string mpOrder)
        //{
        //    _logger.Error(GenerateEnterprisePaymentViewModelRequestPaymentMessageTemplateError, error, mpReference, mpOrder);
        //}

        //public void LogGetByServiceRequestError(string error, string serviceRequest)
        //{
        //    _logger.Error(GetByServiceRequestMessageTemplateError, error, serviceRequest);
        //}
        
        public void LogSendEndPaymentToTibcoError(string error)
        {
            _logger.Error(LogSendEndpaymentToTibcoMessageTemplateError, error);
        }

        public void LogCompute256HashError(string error)
        {
            _logger.Error(LogCompute256HashMessageTemplateError, error);
        }

        public void LogByteToStringError(string error)
        {
            _logger.Error(LogByteToStringMessageTemplateError, error);
        }

        public void LogGenerateResponsePaymentDTOError(string error, string mpReference, string mpOrder)
        {
            _logger.Error(LogGenerateResponsePaymentDTOMessageTemplateError, error, mpReference, mpOrder);
        }

        public void LogValidateMultipagosHashError(string error, string mpReference, string mpOrder)
        {
            _logger.Error(ValidateMultipagosHashMessageTemplateError, error, mpReference, mpOrder);
        }

        public void LogGetEndPaymentSentToTibcoError(string error)
        {
            _logger.Error(LogGetEndPaymentSentToTibcoMessageTemplateError, error);
        }

        public void LogCreateResponsePaymentError(string error, string mpReference, string mpOrder)
        {
            _logger.Error(LogCreateResponsePaymentMessageTemplateError, error, mpReference, mpOrder);
        }

        public void LogGetEndPaymentByResponsePaymentIdError(string error)
        {
            _logger.Error(LogGetEndPaymentByResponsePaymentIdMessageTemplateError, error);
        }

        public void LogUpdateEndPaymentSentStatusError(string error)
        {
            _logger.Error(LogUpdateEndPaymentSentStatusMessageTemplateError, error);
        }

        //public void LogGetEnterprisePaymentViewModelError(string error)
        //{
        //    _logger.Error(LogGetEnterprisePaymentViewModelMessageTemplateError, error);
        //}
    }
}
