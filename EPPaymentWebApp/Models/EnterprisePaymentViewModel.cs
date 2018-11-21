using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPPaymentWebApp.Models
{
    public class EnterprisePaymentViewModel
    {

        /*
         * Uso de las diferentes propiedades de este modelo:
         * BillingAccount: Solo vista Representa:Cuenta de facturación.
         * Currency: Solo vista Representa:Moneda , Valor definido como fijo en Pesos, puede ser tambien Dolares.
         * CreateToken : Solo vista Representa: Si o No en base a bandera para crear token
         * Mp_account : envio a multipagos como mp_account , identificador unico de cliente proporcionado por bancomer
         * Mp_product : envio a multipagos como mp_product , 1 - Multipagos Avanzado
         * Mp_order : envio a multipagos como mp_order , ServiceRequest que se consulta de BeginPayment
         * Mp_reference : envio a multipagos como mp_reference , PaymentReference que se consulta de BeginPayment
         * Mp_node : envio a multipagos como mp_node , por definir 
         * Mp_concept: envio a multipagos como mp_concept, por definir 
         * Mp_amount :envio a multipagos como mp_amount , cantidad a pagar definida por el usuario en la vista.
         * Mp_customerName : envio a multipagos como mp_customername , nombre del usuario pagador , puede variar del titular de la tarjeta.
         * Mp_signature: envio a multipagos como HASH , valor obtenido haciendo: ("SHA256", mp_order + mp_reference + mp_amount,"LLAVEPRIVADA")
         * Mp_currency: envio a multipagos como mp_currency, nos dice si son dolares o pesos, 1 o 2 en este caso siempre 1 para pesos.
         * Mp_urlsuccess: envio a multipagos como mp_urlsuccess , nos dice la url de retorno donde multipagos enviara el post con la respuesta
         *                satisfactoria del pago.
         * Mp_urlfailure: envio a multipagos como mp_urlfailure, nos dice la url de retorno donde multipagos enviara el post con la respuesta fallida
         *                del pago.
         * Mp_registersb: envio a multipagos como mp_registersb , nos dice si genera o no genera token 1 o 0 , CreateToken que se consulta de BeginPayment
        */


        public string BillingAccount { get; set; } 
        public string Currency { get; set; } 
        public string CreateToken { get; set; }
        public int  BeginPaymentId { get; set; }
        public string Mp_account { get; set; } 
        public string Mp_product { get; set; } 
        public string Mp_order { get; set; }  
        public string Mp_reference { get; set; } 
        public string Mp_node { get; set; } 
        public string Mp_concept { get; set; } 
        public string Mp_amount { get; set; } 
        public string Mp_customername { get; set; }
        public string Mp_signature { get; set; } 
        public string Mp_currency { get; set; }
        public string Mp_urlsuccess { get; set; }
        public string Mp_urlfailure { get; set; }
        public string Mp_registersb { get; set; } 
        public string BankResponse { get; set; } // concatenacion de response code y responseMessage
        public string TransactionNumber { get; set; }
        public string Token { get; set; } // token generado por multipagos.
        public string CcLastFour { get; set; } 
        public string IssuingBank { get; set; }
        public string CcType { get; set; }
    }
}
