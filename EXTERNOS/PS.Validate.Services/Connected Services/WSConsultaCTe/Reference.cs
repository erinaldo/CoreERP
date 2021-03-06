//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PS.Validate.Services.WSConsultaCTe {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.portalfiscal.inf.br/cte/wsdl/CteConsulta", ConfigurationName="WSConsultaCTe.CteConsultaSoap12")]
    public interface CteConsultaSoap12 {
        
        // CODEGEN: Generating message contract since the operation cteConsultaCT is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="http://www.portalfiscal.inf.br/cte/wsdl/CteConsulta/cteConsultaCT", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        PS.Validate.Services.WSConsultaCTe.cteConsultaCTResponse cteConsultaCT(PS.Validate.Services.WSConsultaCTe.cteConsultaCTRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.portalfiscal.inf.br/cte/wsdl/CteConsulta")]
    public partial class cteCabecMsg : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string cUFField;
        
        private string versaoDadosField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string cUF {
            get {
                return this.cUFField;
            }
            set {
                this.cUFField = value;
                this.RaisePropertyChanged("cUF");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string versaoDados {
            get {
                return this.versaoDadosField;
            }
            set {
                this.versaoDadosField = value;
                this.RaisePropertyChanged("versaoDados");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
                this.RaisePropertyChanged("AnyAttr");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class cteConsultaCTRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.portalfiscal.inf.br/cte/wsdl/CteConsulta")]
        public PS.Validate.Services.WSConsultaCTe.cteCabecMsg cteCabecMsg;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.portalfiscal.inf.br/cte/wsdl/CteConsulta", Order=0)]
        public System.Xml.XmlNode cteDadosMsg;
        
        public cteConsultaCTRequest() {
        }
        
        public cteConsultaCTRequest(PS.Validate.Services.WSConsultaCTe.cteCabecMsg cteCabecMsg, System.Xml.XmlNode cteDadosMsg) {
            this.cteCabecMsg = cteCabecMsg;
            this.cteDadosMsg = cteDadosMsg;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class cteConsultaCTResponse {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.portalfiscal.inf.br/cte/wsdl/CteConsulta")]
        public PS.Validate.Services.WSConsultaCTe.cteCabecMsg cteCabecMsg;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.portalfiscal.inf.br/cte/wsdl/CteConsulta", Order=0)]
        public System.Xml.XmlNode cteConsultaCTResult;
        
        public cteConsultaCTResponse() {
        }
        
        public cteConsultaCTResponse(PS.Validate.Services.WSConsultaCTe.cteCabecMsg cteCabecMsg, System.Xml.XmlNode cteConsultaCTResult) {
            this.cteCabecMsg = cteCabecMsg;
            this.cteConsultaCTResult = cteConsultaCTResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CteConsultaSoap12Channel : PS.Validate.Services.WSConsultaCTe.CteConsultaSoap12, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CteConsultaSoap12Client : System.ServiceModel.ClientBase<PS.Validate.Services.WSConsultaCTe.CteConsultaSoap12>, PS.Validate.Services.WSConsultaCTe.CteConsultaSoap12 {
        
        public CteConsultaSoap12Client() {
        }
        
        public CteConsultaSoap12Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CteConsultaSoap12Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CteConsultaSoap12Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CteConsultaSoap12Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        PS.Validate.Services.WSConsultaCTe.cteConsultaCTResponse PS.Validate.Services.WSConsultaCTe.CteConsultaSoap12.cteConsultaCT(PS.Validate.Services.WSConsultaCTe.cteConsultaCTRequest request) {
            return base.Channel.cteConsultaCT(request);
        }
        
        public System.Xml.XmlNode cteConsultaCT(ref PS.Validate.Services.WSConsultaCTe.cteCabecMsg cteCabecMsg, System.Xml.XmlNode cteDadosMsg) {
            PS.Validate.Services.WSConsultaCTe.cteConsultaCTRequest inValue = new PS.Validate.Services.WSConsultaCTe.cteConsultaCTRequest();
            inValue.cteCabecMsg = cteCabecMsg;
            inValue.cteDadosMsg = cteDadosMsg;
            PS.Validate.Services.WSConsultaCTe.cteConsultaCTResponse retVal = ((PS.Validate.Services.WSConsultaCTe.CteConsultaSoap12)(this)).cteConsultaCT(inValue);
            cteCabecMsg = retVal.cteCabecMsg;
            return retVal.cteConsultaCTResult;
        }
    }
}
