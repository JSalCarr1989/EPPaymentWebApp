using Serilog;
using EPPaymentWebApp.Models;
using TibcoServiceReference;

namespace EPPaymentWebApp.Helpers
{
    public static class EnterprisePaymentDbLogHelpers
    {

        public static string ResponsePaymentStage
        {
            get
            {
                return "ResponsePayment";
            }
        }

        public static string EndPaymentStage
        {
            get
            {
                return "EndPayment";
            }
        }

        public static string ComunicationStep
        {
            get
            {
                return "FROM MULTIPAGOS TO .NET CORE MVC WEB APP POST RESPONSE";
            }
        }

        public static string Application
        {
            get
            {
                return ".NET CORE MVC WEB APP";
            }
        }

        public static string ResponseDataMessageTemplate
        {
            get
            {
                return "Data from Multipagos: amount {@mp_amount}" +
                                      "authorization: {@mp_authorization}" +
                                      "bankname: {@mp_bankname}" +
                                      "cardholdername: {@mp_cardholdername} " +
                                      "date: {@mp_date} " +
                                      "folio: {@mp_folio} " +
                                      "order: {@mp_order} " +
                                      "pan: {@mp_pan} " +
                                      "paymentmethod: {@mp_paymentmethod} " +
                                      "reference: {@mp_reference} " +
                                      "response:{@mp_response} " +
                                      "responsemsg: {@mp_responsemsg} " +
                                      "saleid: {@mp_saleid} " +
                                      "token:{@mp_sbtoken} " +
                                      "signature:{@mp_signature} "+
                                      "PaymentStage:{@ResponsePaymentStage} " +
                                      "ComunicationStep:{@LogComunicationStep} " +
                                      "Application:{@Application}";
            }
        }

        public static string HashValidationMessageTemplate
        {
            get
            {
                return "Hash Validation Process: mp_order: {@mp_order} "+
                                                "mp_reference: {@mp_reference} "+
                                                "mp_amount: {@mp_amount} "+
                                                "mp_authorization: {@mp_authorization} "+
                                                "GeneratedString:{@concatenation} "+
                                                "GeneratedHash: {@generatedHash} "+
                                                "MultipagosHash:{@mp_signature} "+
                                                "result: {@result} " +
                                                "PaymentStage:{@ResponsePaymentStage} " +
                                                "ComunicationStep:{@LogComunicationStep} " +
                                                "Application:{@Application}";
            }
        }

        public static string GetLastRequestPaymentIdMessageTemplate
        {
            get
            {
                return "LastRequestPaymentId Search: mp_amount: {@mp_amount} " +
                                                    "mp_order: {@mp_order} " +
                                                    "mp_reference: {@mp_reference} " +
                                                    "StatusPayment: {@statusPayment} " +
                                                    "Obtained Request Payment Id : {@requestPaymentId}" +
                                                    "PaymentStage:{@ResponsePaymentStage} " +
                                                    "ComunicationStep:{@LogComunicationStep} " +
                                                    "Application:{@Application}";
            }
        }

        public static string CreateResponsePaymentMessageTemplate
        {
            get
            {
                return "ResponsePaymentDTO Data: MpAmount:{@MpAmount} "+
                                                "MpAuthorization: {@MpAuthorization} "+
                                                "MpBankName: {@MpBankName} "+
                                                "MpCardHolderName: {@MpCardHolderName} "+
                                                "Date: {@MpDate} "+
                                                "Folio: {@MpFolio} "+
                                                "Order: {@MpOrder} "+
                                                "Pan: {@MpPan} "+
                                                "PaymentMethod: {@MpPaymentMethod} "+
                                                "Reference: {@MpReference} "+
                                                "Response: {@MpResponse} "+
                                                "ResponseMessage: {@MpResponseMsg} "+
                                                "SaleId: {@MpSaleId} "+
                                                "Token: {@MpSbToken} "+
                                                "Signature:{@MpSignature} "+
                                                "PaymentRequestId: {@PaymentRequestId}  "+
                                                "ResponsePaymentHashStatusDescription: {@ResponsePaymentHashStatusDescription} "+
                                                "ResponsePaymentTypeDescription: {@ResponsePaymentTypeDescription} "+
                                                "GeneratedResponsePaymentId: {@responsePaymentId} " +
                                                "PaymentStage:{@ResponsePaymentStage} " +
                                                "ComunicationStep:{@LogComunicationStep} " +
                                                "Application:{@Application}";
            }
        }

        public static string GetSentExistsMessageTemplate
        {
            get
            {
                return "Sent to Tibco from Server2Server Search: EndPaymentStatusDescription: {@endPaymentStatusDescription} " +
                                                                "ResponsePaymentType: {@responsePaymentType} " +
                                                                "ResponsePaymentId: {@responsePaymentId} " +
                                                                "ServerHasSentData: {@serverHasSentData}" +
                                                                "PaymentStage:{@EndPaymentStage} " +
                                                                "ComunicationStep:{@LogComunicationStep} " +
                                                                "Application:{@Application}";
            }
        }

        public static string GetEndPaymentMessageTemplate
        {
            get
            {
                return "EndPayment Search: ResponsePaymentId: {@responsePaymentId} "+
                        "Searched EndPayment: EndPaymentId: {@EndPaymentId} "+
                                             "CcLastFour: {@CcLastFour} "+
                                             "Token: {@Token} "+
                                             "ResponseCode: {@ResponseCode} "+
                                             "ResponseMessage: {@ResponseMessage} "+
                                             "TransactionNumber: {@TransactionNumber} "+
                                             "CcType: {@CcType} "+
                                             "IssuingBank: {@IssuingBank} "+
                                             "ServiceRequest: {@ServiceRequest} "+
                                             "BillingAccount: {@BillingAccount} "+
                                             "PaymentReference: {@PaymentReference} " +
                                             "PaymentStage:{@EndPaymentStage} " +
                                             "ComunicationStep:{@LogComunicationStep} " +
                                             "Application:{@Application}";
            }
        }

        public static string SendEndPaymentToTibcoMessageTemplate
        {
            get
            {
                return "Data that has sent to Tibco: UltimosCuatroDigitos: {@UltimosCuatroDigitos} "+
                                                    "Token: {@Token} "+
                                                    "RespuestaBanco: {@RespuestaBanco} "+
                                                    "NumeroTransaccion: {@NumeroTransaccion} "+
                                                    "TipoTarjeta:{@TipoTarjeta} "+
                                                    "BancoEmisor: {@BancoEmisor} "+
                                                    "SeviceRequest:{@SeviceRequest} "+
                                                    "BillingAccount: {@BillingAccount} " +
                                                    "Response From Tibco: ErrorMessage: {@ErrorMessage} "+
                                                    "ErrorCode:{@ErrorCode}" +
                                                    "PaymentStage:{@EndPaymentStage} " +
                                                    "ComunicationStep:{@LogComunicationStep} " +
                                                    "Application:{@Application}";
            }
        }

        public static string UpdateEndPaymentSentStatusMessageTemplate
        {
            get
            {
                return "Updated Endpayment: EndPaymentId: {@endPaymentId} "+
                                           "EndPaymentSentStatus: {@endPaymentSentStatus}" +
                                           "PaymentStage:{@EndPaymentStage} " +
                                           "ComunicationStep:{@LogComunicationStep} " +
                                           "Application:{@Application}";
            }
        }

        public static string ShowedDataInViewMessageTemplate
        {
            get
            {
                return @"Data that has passed to the view: Request Data: 
                                                    Mp_order: {@Mp_order} 
                                                    BillingAcount: {@BillingAccount} 
                                                    Mp_amount: {@Mp_amount} 
                                                    Currency: {@Currency} 
                                                    Mp_customername: {@Mp_customername} 
                                                    CreateToken: {@CreateToken}
                                                    Response Data: 
                                                    BankResponse: {@BankResponse}
                                                    TransactionNumber: {@TransactionNumber}
                                                    Token: {@Token}
                                                    CcLastFour: {@CcLastFour}
                                                    IssuingBank: {@IssuingBank}
                                                    CcType: {@CcType}
                                                    PaymentStage:{@ResponsePaymentStage} 
                                                    ComunicationStep:{@LogComunicationStep} 
                                                    Application:{@Application}";
            }
        }


        public static void LogResponsedDataToDb(ILogger log, MultiPagosResponsePaymentDTO multiPagosResponse)
        {
            log.Information(
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

        public static void LogHashValidationToDb(ILogger log, MultiPagosResponsePaymentDTO multiPagosResponse, string concatenation ,string generatedHash, string result)
        {
            log.Information(
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

        public static void LogGetLastRequestPaymentId(ILogger log, MultiPagosResponsePaymentDTO multipagosResponse,string statusPayment, int requestPaymentId)
        {
            log.Information(
                GetLastRequestPaymentIdMessageTemplate,
                multipagosResponse.mp_amount,
                multipagosResponse.mp_order,
                multipagosResponse.mp_reference,
                statusPayment,
                requestPaymentId,
                ResponsePaymentStage,
                ComunicationStep,
                Application
                );
        }

        public static void LogCreateResponsePayment(ILogger log, ResponsePaymentDTO responsePaymentDTO, int responsePaymentId)
        {
            log.Information(
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

        public static void LogGetSentExists(ILogger log, string endPaymentStatusDescription, string responsePaymentType, int responsePaymentId, bool serverHasSentData)
        {
            log.Information(
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

        public static void LogGetEndPayment(ILogger log, int responsePaymentId, EndPayment endPayment)
        {
            log.Information(
                GetEndPaymentMessageTemplate,
                responsePaymentId,
                endPayment.EndPaymentId,
                endPayment.CcLastFour,
                endPayment.Token,
                endPayment.ResponseCode,
                endPayment.ResponseMessage,
                endPayment.TransactionNumber,
                endPayment.CcType,
                endPayment.IssuingBank,
                endPayment.ServiceRequest,
                endPayment.BillingAccount,
                endPayment.PaymentReference,
                EndPaymentStage,
                ComunicationStep,
                Application
                );
        }

        public static void LogSendEndPaymentToTibco(ILogger log, ResponseBankRequestType requestToTibco, ResponseBankResponse responseFromTibco)
        {
            log.Information(
                SendEndPaymentToTibcoMessageTemplate,
                requestToTibco.UltimosCuatroDigitos,
                requestToTibco.Token,
                requestToTibco.RespuestaBanco,
                requestToTibco.NumeroTransaccion,
                requestToTibco.TipoTarjeta,
                requestToTibco.BancoEmisor,
                requestToTibco.SeviceRequest,
                requestToTibco.BillingAccount,
                responseFromTibco.ResponseBankResponse1.ErrorMessage,
                responseFromTibco.ResponseBankResponse1.ErrorCode,
                EndPaymentStage,
                ComunicationStep,
                Application
                );
        }

        public static void LogUpdateEndPaymentSentStatus(ILogger log, int endPayment, string endPaymentSentStatus)
        {
            log.Information(
                UpdateEndPaymentSentStatusMessageTemplate,
                endPayment,
                endPaymentSentStatus,
                EndPaymentStage,
                ComunicationStep,
                Application
                );
        }

        public static void LogShowedDataInView(ILogger log, EnterprisePaymentViewModel viewModel)
        {
            log.Information(
                ShowedDataInViewMessageTemplate,
                viewModel.Mp_order,
                viewModel.BillingAccount,
                viewModel.Mp_amount,
                viewModel.Currency,
                viewModel.Mp_customername,
                viewModel.CreateToken,
                viewModel.BankResponse,
                viewModel.TransactionNumber,
                viewModel.Token,
                viewModel.CcLastFour,
                viewModel.IssuingBank,
                viewModel.CcType,
                ResponsePaymentStage,
                ComunicationStep,
                Application
                );

        }
    }
}
