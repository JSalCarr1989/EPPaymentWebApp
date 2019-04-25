using EPPaymentWebApp.Interfaces;
using Serilog;
using TibcoServiceReference;

namespace EPPaymentWebApp.Models
{
    public class DbLoggerRepository : IDbLoggerRepository
    {
        //private readonly IDbConnectionRepository _connectionStringRepo;
        private readonly IEnvironmentSettingsRepository _environmentSettingsRepository;
        private readonly ILogger _logger;

        public DbLoggerRepository(IEnvironmentSettingsRepository environmentSettingsRepository)
        {
            //_connectionStringRepo = connectionStringRepo;
            _environmentSettingsRepository = environmentSettingsRepository;

            var logger = new LoggerConfiguration()
               .MinimumLevel.Information()
               //.WriteTo.MSSqlServer(_connectionStringRepo.GetEpPaymentConnectionString(), EpLogTable)
               .WriteTo.MSSqlServer(_environmentSettingsRepository.GetConnectionStringSetting(), EpLogTable)
               .CreateLogger();

            _logger = logger;
        }


        public static string EpLogTable => "EpLog";

        public static string ResponsePaymentStage => "ResponsePayment";

        public static string EndPaymentStage => "EndPayment";

        public static string ComunicationStep => "FROM MULTIPAGOS TO .NET CORE MVC WEB APP POST RESPONSE";

        public static string Application => ".NET CORE MVC WEB APP";

        public static string ResponseDataMessageTemplate => @"Data from Multipagos: amount {@mp_amount} 
                                       authorization: {@mp_authorization}  
                                       bankname: {@mp_bankname} 
                                       cardholdername: {@mp_cardholdername} 
                                       date: {@mp_date} 
                                       folio: {@mp_folio} 
                                       order: {@mp_order} 
                                       pan: {@mp_pan}  
                                       paymentmethod: {@mp_paymentmethod} 
                                       reference: {@mp_reference} 
                                       response:{@mp_response} 
                                       responsemsg: {@mp_responsemsg} 
                                       saleid: {@mp_saleid} 
                                       token:{@mp_sbtoken} 
                                       signature:{@mp_signature} 
                                       PaymentStage:{@ResponsePaymentStage} 
                                       ComunicationStep:{@LogComunicationStep} 
                                       Application:{@Application}";

        public static string HashValidationMessageTemplate
        {
            get
            {
                return "Hash Validation Process: mp_order: {@mp_order} " +
                                                "mp_reference: {@mp_reference} " +
                                                "mp_amount: {@mp_amount} " +
                                                "mp_authorization: {@mp_authorization} " +
                                                "GeneratedString:{@concatenation} " +
                                                "GeneratedHash: {@generatedHash} " +
                                                "MultipagosHash:{@mp_signature} " +
                                                "result: {@result} " +
                                                "PaymentStage:{@ResponsePaymentStage} " +
                                                "ComunicationStep:{@LogComunicationStep} " +
                                                "Application:{@Application}";
            }
        }

        public static string GetLastRequestPaymentIdMessageTemplate => "LastRequestPaymentId Search: mp_amount: {@mp_amount} " +
                                                    "mp_order: {@mp_order} " +
                                                    "mp_reference: {@mp_reference} " +
                                                    "StatusPayment: {@statusPayment} " +
                                                    "Obtained Request Payment Id : {@requestPaymentId}" +
                                                    "PaymentStage:{@ResponsePaymentStage} " +
                                                    "ComunicationStep:{@LogComunicationStep} " +
                                                    "Application:{@Application}";

        public static string CreateResponsePaymentMessageTemplate => "ResponsePaymentDTO Data: MpAmount:{@MpAmount} " +
                                                "MpAuthorization: {@MpAuthorization} " +
                                                "MpBankName: {@MpBankName} " +
                                                "MpCardHolderName: {@MpCardHolderName} " +
                                                "Date: {@MpDate} " +
                                                "Folio: {@MpFolio} " +
                                                "Order: {@MpOrder} " +
                                                "Pan: {@MpPan} " +
                                                "PaymentMethod: {@MpPaymentMethod} " +
                                                "Reference: {@MpReference} " +
                                                "Response: {@MpResponse} " +
                                                "ResponseMessage: {@MpResponseMsg} " +
                                                "SaleId: {@MpSaleId} " +
                                                "Token: {@MpSbToken} " +
                                                "Signature:{@MpSignature} " +
                                                "PaymentRequestId: {@PaymentRequestId}  " +
                                                "ResponsePaymentHashStatusDescription: {@ResponsePaymentHashStatusDescription} " +
                                                "ResponsePaymentTypeDescription: {@ResponsePaymentTypeDescription} " +
                                                "GeneratedResponsePaymentId: {@responsePaymentId} " +
                                                "PaymentStage:{@ResponsePaymentStage} " +
                                                "ComunicationStep:{@LogComunicationStep} " +
                                                "Application:{@Application}";

        public static string GetSentExistsMessageTemplate => "Sent to Tibco from Server2Server Search: EndPaymentStatusDescription: {@endPaymentStatusDescription} " +
                                                                "ResponsePaymentType: {@responsePaymentType} " +
                                                                "ResponsePaymentId: {@responsePaymentId} " +
                                                                "ServerHasSentData: {@serverHasSentData}" +
                                                                "PaymentStage:{@EndPaymentStage} " +
                                                                "ComunicationStep:{@LogComunicationStep} " +
                                                                "Application:{@Application}";

        public static string GetEndPaymentMessageTemplate => @"EndPayment Search: ResponsePaymentId: {@responsePaymentId} 
                         Searched EndPayment: EndPaymentId: {@EndPaymentId} 
                                             CcLastFour: {@CcLastFour} 
                                             CcBin: {@CcBin}
                                             Token: {@Token} 
                                             ResponseCode: {@ResponseCode} 
                                             ResponseMessage: {@ResponseMessage} 
                                             TransactionNumber: {@TransactionNumber} 
                                             CcType: {@CcType} 
                                             IssuingBank: {@IssuingBank} 
                                             Amount: {@Amount}
                                             CardHolderName: {@CardHolderName}
                                             ServiceRequest: {@ServiceRequest} 
                                             BillingAccount: {@BillingAccount} 
                                             PaymentReference: {@PaymentReference} 
                                             ResponsePaymentId: {@ResponsePaymentId}
                                             EndPaymentStatusId: {@EndPaymentStatusId}
                                             PaymentStage:{@EndPaymentStage} 
                                             ComunicationStep:{@LogComunicationStep} 
                                             Application:{@Application}";

        public static string UpdateEndPaymentSentStatusMessageTemplate => @"Updated Endpayment: EndPaymentId: {@endPaymentId} " +
                                           "EndPaymentSentStatus: {@endPaymentSentStatus}" +
                                           "PaymentStage:{@EndPaymentStage} " +
                                           "ComunicationStep:{@LogComunicationStep} " +
                                           "Application:{@Application}";

        public static string SendEndPaymentToTibcoMessageTemplate => @"Data that has sent to Tibco: UltimosCuatroDigitos: {@UltimosCuatroDigitos} 
                                                     Token: {@Token} 
                                                     RespuestaBanco: {@RespuestaBanco} 
                                                     NumeroTransaccion: {@NumeroTransaccion} 
                                                     TipoTarjeta:{@TipoTarjeta} 
                                                     BancoEmisor: {@BancoEmisor} 
                                                     SeviceRequest:{@SeviceRequest} 
                                                     BillingAccount: {@BillingAccount} 
                                                     Amount: {@Amount} 
                                                     CardHolderName: {@CardHolderName} 
                                                     Response From Tibco: ErrorMessage: {@ErrorMessage} 
                                                     ErrorCode:{@ErrorCode} 
                                                     PaymentStage:{@EndPaymentStage} 
                                                     ComunicationStep:{@LogComunicationStep} 
                                                     Application:{@Application}";

        //public static string ShowedDataInViewMessageTemplate => @"Data that has passed to the view: Request Data: 
        //                                            Mp_order: {@Mp_order} 
        //                                            BillingAcount: {@BillingAccount} 
        //                                            Mp_amount: {@Mp_amount} 
        //                                            Currency: {@Currency} 
        //                                            Mp_customername: {@Mp_customername} 
        //                                            CreateToken: {@CreateToken}
        //                                            Response Data: 
        //                                            BankResponse: {@BankResponse}
        //                                            TransactionNumber: {@TransactionNumber}
        //                                            Token: {@Token}
        //                                            CcLastFour: {@CcLastFour}
        //                                            IssuingBank: {@IssuingBank}
        //                                            CcType: {@CcType}
        //                                            PaymentStage:{@ResponsePaymentStage} 
        //                                            ComunicationStep:{@LogComunicationStep} 
        //                                            Application:{@Application}";

        public static string CreatedEnvironmentSettingsObjectTemplate => @"The EnvironmenSettings Was read with the following Data:
                                                                           ConnectionString{@ConnectionString}
                                                                           MpSk:{@MpSk}
                                                                           UrlSuccess:{@UrlSuccess}
                                                                           UrlFailure:{@UrlFailure}";

        public void LogResponsedDataToDb(MultiPagosResponsePaymentDTO multiPagosResponse)
        {
            _logger.Information(
                 ResponseDataMessageTemplate,
                 multiPagosResponse.mp_amount,
                 multiPagosResponse.mp_authorization,
                 multiPagosResponse.mp_bankname,
                 multiPagosResponse.mp_cardholdername,
                 multiPagosResponse.mp_date,
                 multiPagosResponse.mp_folio,
                 multiPagosResponse.mp_order,
                 multiPagosResponse.mp_pan,
                 multiPagosResponse.mp_paymentmethod,
                 multiPagosResponse.mp_reference,
                 multiPagosResponse.mp_response,
                 multiPagosResponse.mp_responsemsg,
                 multiPagosResponse.mp_saleid,
                 multiPagosResponse.mp_sbtoken,
                 multiPagosResponse.mp_signature,
                 ResponsePaymentStage,
                 ComunicationStep,
                 Application
             );
        }

        public void LogHashValidationToDb(MultiPagosResponsePaymentDTO multiPagosResponse, string concatenation, string generatedHash, string result)
        {
            _logger.Information(
                HashValidationMessageTemplate,
                multiPagosResponse.mp_order,
                multiPagosResponse.mp_reference,
                multiPagosResponse.mp_amount,
                multiPagosResponse.mp_authorization,
                concatenation,
                generatedHash,
                multiPagosResponse.mp_signature,
                result,
                ResponsePaymentStage,
                ComunicationStep,
                Application
              );
        }

        public void LogGetLastRequestPaymentId(decimal amount,string serviceRequest,string paymentReference,string statusPayment, int requestPaymentId)
        {
            _logger.Information(
                GetLastRequestPaymentIdMessageTemplate,
                amount,
                serviceRequest,
                paymentReference,
                statusPayment,
                requestPaymentId,
                ResponsePaymentStage,
                ComunicationStep,
                Application
                );
        }

        public void LogCreateResponsePayment(ResponsePaymentDTO responsePaymentDTO, int responsePaymentId)
        {
            _logger.Information(
                 CreateResponsePaymentMessageTemplate,
                 responsePaymentDTO.MpAmount,
                 responsePaymentDTO.MpAuthorization,
                 responsePaymentDTO.MpBankName,
                 responsePaymentDTO.MpCardHolderName,
                 responsePaymentDTO.MpDate,
                 responsePaymentDTO.MpFolio,
                 responsePaymentDTO.MpOrder,
                 responsePaymentDTO.MpPan,
                 responsePaymentDTO.MpPaymentMethod,
                 responsePaymentDTO.MpReference,
                 responsePaymentDTO.MpResponse,
                 responsePaymentDTO.MpResponseMsg,
                 responsePaymentDTO.MpSaleId,
                 responsePaymentDTO.MpSbToken,
                 responsePaymentDTO.MpSignature,
                 responsePaymentDTO.PaymentRequestId,
                 responsePaymentDTO.ResponsePaymentHashStatusDescription,
                 responsePaymentDTO.ResponsePaymentTypeDescription,
                 responsePaymentId,
                 ResponsePaymentStage,
                 ComunicationStep,
                 Application
                );
        }

        public void LogGetSentExists(string endPaymentStatusDescription, string responsePaymentType, int responsePaymentId, bool serverHasSentData)
        {
            _logger.Information(
                GetSentExistsMessageTemplate,
                endPaymentStatusDescription,
                responsePaymentType,
                responsePaymentId,
                serverHasSentData,
                EndPaymentStage,
                ComunicationStep,
                Application
                );
        }

        public void LogGetEndPayment(int responsePaymentId, EndPayment endPayment)
        {
            _logger.Information(
                GetEndPaymentMessageTemplate,
                responsePaymentId,
                endPayment.EndPaymentId,
                endPayment.CcLastFour,
                endPayment.CcBin,
                endPayment.Token,
                endPayment.ResponseCode,
                endPayment.ResponseMessage,
                endPayment.TransactionNumber,
                endPayment.CcType,
                endPayment.IssuingBank,
                endPayment.Amount,
                endPayment.CardHolderName,
                endPayment.ServiceRequest,
                endPayment.BillingAccount,
                endPayment.PaymentReference,
                endPayment.ResponsePaymentId,
                endPayment.EndPaymentStatusId,
                EndPaymentStage,
                ComunicationStep,
                Application
                );
        }

        public void LogUpdateEndPaymentSentStatus(int endPayment, string endPaymentSentStatus)
        {
            _logger.Information(
                UpdateEndPaymentSentStatusMessageTemplate,
                endPayment,
                endPaymentSentStatus,
                EndPaymentStage,
                ComunicationStep,
                Application
                );
        }

        public void LogSendEndPaymentToTibco(ResponseBankRequestType requestToTibco, ResponseBankResponse responseFromTibco)
        {
            _logger.Information(
                SendEndPaymentToTibcoMessageTemplate,
                requestToTibco.UltimosCuatroDigitos,
                requestToTibco.Token,
                requestToTibco.RespuestaBanco,
                requestToTibco.NumeroTransaccion,
                requestToTibco.TipoTarjeta,
                requestToTibco.BancoEmisor,
                requestToTibco.SeviceRequest,
                requestToTibco.BillingAccount,
                requestToTibco.Monto,
                requestToTibco.TarjetaHabiente,
                responseFromTibco.ResponseBankResponse1.ErrorMessage,
                responseFromTibco.ResponseBankResponse1.ErrorCode,
                EndPaymentStage,
                ComunicationStep,
                Application
                );
        }

        //public void LogShowedDataInView(EnterprisePaymentViewModel viewModel)
        //{
        //    _logger.Information(
        //        ShowedDataInViewMessageTemplate,
        //        viewModel.Mp_order,
        //        viewModel.BillingAccount,
        //        viewModel.Mp_amount,
        //        viewModel.Currency,
        //        viewModel.Mp_customername,
        //        viewModel.CreateToken,
        //        viewModel.BankResponse,
        //        viewModel.TransactionNumber,
        //        viewModel.Token,
        //        viewModel.CcLastFour,
        //        viewModel.IssuingBank,
        //        viewModel.CcType,
        //        ResponsePaymentStage,
        //        ComunicationStep,
        //        Application
        //        );

        //}

        public void LogCreatedEnvironmentSettingsObject(EnvironmentSettings envSettingsObject)
        {
            _logger.Information(
                CreatedEnvironmentSettingsObjectTemplate,
                envSettingsObject.ConnectionString,
                envSettingsObject.MpSk,
                envSettingsObject.UrlSuccess,
                envSettingsObject.UrlFailure
                );
        }
    }
}
