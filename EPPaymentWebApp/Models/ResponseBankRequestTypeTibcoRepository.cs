using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using EPPaymentWebApp.Interfaces;
using TibcoServiceReference;

namespace EPPaymentWebApp.Models
{
    public class ResponseBankRequestTypeTibcoRepository : IResponseBankRequestTypeTibcoRepository
    {




        public async Task<string> SendEndPaymentToTibco(EndPayment endPayment)
        {
            ResponseBankRequestType request = new ResponseBankRequestType
            {
                UltimosCuatroDigitos = endPayment.CcLastFour,
                Token = endPayment.Token,
                RespuestaBanco = endPayment.ResponseMessage,
                NumeroTransaccion = endPayment.TransactionNumber,
                TipoTarjeta = endPayment.CcType,
                BancoEmisor = endPayment.IssuingBank,
                SeviceRequest = endPayment.ServiceRequest,
                BillingAccount = endPayment.BillingAccount
            };


            ResponseBankClient responsebank = new ResponseBankClient();

            //try
            //{
                ResponseBankResponse response = await responsebank.ResponseBankAsync(request);

                return response.ResponseBankResponse1.ErrorMessage;
            //}
            //catch(TimeoutException timeProblem)
            //{
            //    Console.WriteLine("The Service operation timed out." + timeProblem.Message);
            //    responsebank.Abort();

            //}
            //catch(FaultException fault)
            //{
            //    Console.WriteLine("An unknown exception was received."
            //        + fault.Message
            //        + fault.StackTrace);
            //}
            //catch(CommunicationException commProblem)
            //{
            //    Console.WriteLine("There was a communication problem." + commProblem.Message + commProblem.StackTrace);
            //}
        }
    }
}
