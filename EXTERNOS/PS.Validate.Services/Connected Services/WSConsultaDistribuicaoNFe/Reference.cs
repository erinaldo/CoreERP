//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PS.Validate.Services.WSConsultaDistribuicaoNFe {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeDistribuicaoDFe", ConfigurationName="WSConsultaDistribuicaoNFe.NFeDistribuicaoDFeSoap")]
    public interface NFeDistribuicaoDFeSoap {
        
        // CODEGEN: Generating message contract since element name nfeDadosMsg from namespace http://www.portalfiscal.inf.br/nfe/wsdl/NFeDistribuicaoDFe is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://www.portalfiscal.inf.br/nfe/wsdl/NFeDistribuicaoDFe/nfeDistDFeInteresse", ReplyAction="*")]
        PS.Validate.Services.WSConsultaDistribuicaoNFe.nfeDistDFeInteresseResponse nfeDistDFeInteresse(PS.Validate.Services.WSConsultaDistribuicaoNFe.nfeDistDFeInteresseRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class nfeDistDFeInteresseRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="nfeDistDFeInteresse", Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeDistribuicaoDFe", Order=0)]
        public PS.Validate.Services.WSConsultaDistribuicaoNFe.nfeDistDFeInteresseRequestBody Body;
        
        public nfeDistDFeInteresseRequest() {
        }
        
        public nfeDistDFeInteresseRequest(PS.Validate.Services.WSConsultaDistribuicaoNFe.nfeDistDFeInteresseRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeDistribuicaoDFe")]
    public partial class nfeDistDFeInteresseRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.Linq.XElement nfeDadosMsg;
        
        public nfeDistDFeInteresseRequestBody() {
        }
        
        public nfeDistDFeInteresseRequestBody(System.Xml.Linq.XElement nfeDadosMsg) {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class nfeDistDFeInteresseResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="nfeDistDFeInteresseResponse", Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeDistribuicaoDFe", Order=0)]
        public PS.Validate.Services.WSConsultaDistribuicaoNFe.nfeDistDFeInteresseResponseBody Body;
        
        public nfeDistDFeInteresseResponse() {
        }
        
        public nfeDistDFeInteresseResponse(PS.Validate.Services.WSConsultaDistribuicaoNFe.nfeDistDFeInteresseResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeDistribuicaoDFe")]
    public partial class nfeDistDFeInteresseResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.Linq.XElement nfeDistDFeInteresseResult;
        
        public nfeDistDFeInteresseResponseBody() {
        }
        
        public nfeDistDFeInteresseResponseBody(System.Xml.Linq.XElement nfeDistDFeInteresseResult) {
            this.nfeDistDFeInteresseResult = nfeDistDFeInteresseResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface NFeDistribuicaoDFeSoapChannel : PS.Validate.Services.WSConsultaDistribuicaoNFe.NFeDistribuicaoDFeSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class NFeDistribuicaoDFeSoapClient : System.ServiceModel.ClientBase<PS.Validate.Services.WSConsultaDistribuicaoNFe.NFeDistribuicaoDFeSoap>, PS.Validate.Services.WSConsultaDistribuicaoNFe.NFeDistribuicaoDFeSoap {
        
        public NFeDistribuicaoDFeSoapClient() {
        }
        
        public NFeDistribuicaoDFeSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public NFeDistribuicaoDFeSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NFeDistribuicaoDFeSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NFeDistribuicaoDFeSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        PS.Validate.Services.WSConsultaDistribuicaoNFe.nfeDistDFeInteresseResponse PS.Validate.Services.WSConsultaDistribuicaoNFe.NFeDistribuicaoDFeSoap.nfeDistDFeInteresse(PS.Validate.Services.WSConsultaDistribuicaoNFe.nfeDistDFeInteresseRequest request) {
            return base.Channel.nfeDistDFeInteresse(request);
        }
        
        public System.Xml.Linq.XElement nfeDistDFeInteresse(System.Xml.Linq.XElement nfeDadosMsg) {
            PS.Validate.Services.WSConsultaDistribuicaoNFe.nfeDistDFeInteresseRequest inValue = new PS.Validate.Services.WSConsultaDistribuicaoNFe.nfeDistDFeInteresseRequest();
            inValue.Body = new PS.Validate.Services.WSConsultaDistribuicaoNFe.nfeDistDFeInteresseRequestBody();
            inValue.Body.nfeDadosMsg = nfeDadosMsg;
            PS.Validate.Services.WSConsultaDistribuicaoNFe.nfeDistDFeInteresseResponse retVal = ((PS.Validate.Services.WSConsultaDistribuicaoNFe.NFeDistribuicaoDFeSoap)(this)).nfeDistDFeInteresse(inValue);
            return retVal.Body.nfeDistDFeInteresseResult;
        }
    }
}
