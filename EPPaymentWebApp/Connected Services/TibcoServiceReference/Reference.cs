﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     //
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TibcoServiceReference
{
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.tibco.com/schemas/CCPayment_root_root/SharedResources/Schemas/PCI.xsd")]
    public partial class GenericFaultType
    {
        
        private string errorCodeField;
        
        private string errorMessageField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string ErrorCode
        {
            get
            {
                return this.errorCodeField;
            }
            set
            {
                this.errorCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ErrorMessage
        {
            get
            {
                return this.errorMessageField;
            }
            set
            {
                this.errorMessageField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.tibco.com/schemas/CCPayment_root_root/SharedResources/Schemas/PCI.xsd")]
    public partial class ResponseBankResponseType
    {
        
        private string errorCodeField;
        
        private string errorMessageField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string ErrorCode
        {
            get
            {
                return this.errorCodeField;
            }
            set
            {
                this.errorCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ErrorMessage
        {
            get
            {
                return this.errorMessageField;
            }
            set
            {
                this.errorMessageField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.tibco.com/schemas/CCPayment_root_root/SharedResources/Schemas/PCI.xsd")]
    public partial class ResponseBankRequestType
    {
        
        private string ultimosCuatroDigitosField;
        
        private string tokenField;
        
        private string respuestaBancoField;
        
        private string numeroTransaccionField;
        
        private string tipoTarjetaField;
        
        private string bancoEmisorField;
        
        private string seviceRequestField;
        
        private string billingAccountField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string UltimosCuatroDigitos
        {
            get
            {
                return this.ultimosCuatroDigitosField;
            }
            set
            {
                this.ultimosCuatroDigitosField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string Token
        {
            get
            {
                return this.tokenField;
            }
            set
            {
                this.tokenField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string RespuestaBanco
        {
            get
            {
                return this.respuestaBancoField;
            }
            set
            {
                this.respuestaBancoField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string NumeroTransaccion
        {
            get
            {
                return this.numeroTransaccionField;
            }
            set
            {
                this.numeroTransaccionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string TipoTarjeta
        {
            get
            {
                return this.tipoTarjetaField;
            }
            set
            {
                this.tipoTarjetaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string BancoEmisor
        {
            get
            {
                return this.bancoEmisorField;
            }
            set
            {
                this.bancoEmisorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string SeviceRequest
        {
            get
            {
                return this.seviceRequestField;
            }
            set
            {
                this.seviceRequestField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string BillingAccount
        {
            get
            {
                return this.billingAccountField;
            }
            set
            {
                this.billingAccountField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://xmlns.example.com/1541782272354", ConfigurationName="TibcoServiceReference.ResponseBank")]
    public interface ResponseBank
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="/BancomerInterface/Exposure/Service.serviceagent/ResponseBankEndpoint1/ResponseBa" +
            "nk", ReplyAction="*")]
        [System.ServiceModel.FaultContractAttribute(typeof(TibcoServiceReference.GenericFaultType), Action="/BancomerInterface/Exposure/Service.serviceagent/ResponseBankEndpoint1/ResponseBa" +
            "nk", Name="ResponseBankFault", Namespace="http://www.tibco.com/schemas/CCPayment_root_root/SharedResources/Schemas/PCI.xsd")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<TibcoServiceReference.ResponseBankResponse> ResponseBankAsync(TibcoServiceReference.ResponseBankRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ResponseBankRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ResponseBankRequest", Namespace="http://www.tibco.com/schemas/CCPayment_root_root/SharedResources/Schemas/PCI.xsd", Order=0)]
        public TibcoServiceReference.ResponseBankRequestType ResponseBankRequest1;
        
        public ResponseBankRequest()
        {
        }
        
        public ResponseBankRequest(TibcoServiceReference.ResponseBankRequestType ResponseBankRequest1)
        {
            this.ResponseBankRequest1 = ResponseBankRequest1;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ResponseBankResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ResponseBankResponse", Namespace="http://www.tibco.com/schemas/CCPayment_root_root/SharedResources/Schemas/PCI.xsd", Order=0)]
        public TibcoServiceReference.ResponseBankResponseType ResponseBankResponse1;
        
        public ResponseBankResponse()
        {
        }
        
        public ResponseBankResponse(TibcoServiceReference.ResponseBankResponseType ResponseBankResponse1)
        {
            this.ResponseBankResponse1 = ResponseBankResponse1;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public interface ResponseBankChannel : TibcoServiceReference.ResponseBank, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public partial class ResponseBankClient : System.ServiceModel.ClientBase<TibcoServiceReference.ResponseBank>, TibcoServiceReference.ResponseBank
    {
        
    /// <summary>
    /// Implement this partial method to configure the service endpoint.
    /// </summary>
    /// <param name="serviceEndpoint">The endpoint to configure</param>
    /// <param name="clientCredentials">The client credentials</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public ResponseBankClient() : 
                base(ResponseBankClient.GetDefaultBinding(), ResponseBankClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.HTTPEndpoint.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ResponseBankClient(EndpointConfiguration endpointConfiguration) : 
                base(ResponseBankClient.GetBindingForEndpoint(endpointConfiguration), ResponseBankClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ResponseBankClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(ResponseBankClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ResponseBankClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(ResponseBankClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ResponseBankClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<TibcoServiceReference.ResponseBankResponse> TibcoServiceReference.ResponseBank.ResponseBankAsync(TibcoServiceReference.ResponseBankRequest request)
        {
            return base.Channel.ResponseBankAsync(request);
        }
        
        public System.Threading.Tasks.Task<TibcoServiceReference.ResponseBankResponse> ResponseBankAsync(TibcoServiceReference.ResponseBankRequestType ResponseBankRequest1)
        {
            TibcoServiceReference.ResponseBankRequest inValue = new TibcoServiceReference.ResponseBankRequest();
            inValue.ResponseBankRequest1 = ResponseBankRequest1;
            return ((TibcoServiceReference.ResponseBank)(this)).ResponseBankAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.HTTPEndpoint))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.HTTPEndpoint))
            {
                return new System.ServiceModel.EndpointAddress("http://172.26.50.85:12901/BancomerInterface/Exposure/Service.serviceagent/HTTPEnd" +
                        "point");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return ResponseBankClient.GetBindingForEndpoint(EndpointConfiguration.HTTPEndpoint);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return ResponseBankClient.GetEndpointAddress(EndpointConfiguration.HTTPEndpoint);
        }
        
        public enum EndpointConfiguration
        {
            
            HTTPEndpoint,
        }
    }
}
