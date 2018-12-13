using System.Threading.Tasks;
using EPPaymentWebApp.Models;
using Serilog;

namespace EPPaymentWebApp.Interfaces
{
   public  interface IResponseBankRequestTypeTibcoRepository
    {
       Task<string> SendEndPaymentToTibco(EndPayment endPayment, ILogger log);
    }
}
