//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PS.Validate.Services.WSGinfes {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://homologacao.ginfes.com.br", ConfigurationName="WSGinfes.ServiceGinfesImpl")]
    public interface ServiceGinfesImpl {
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string CancelarNfse(string arg0);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string CancelarNfseV3(string arg0, string arg1);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string ConsultarLoteRps(string arg0);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string ConsultarLoteRpsV3(string arg0, string arg1);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string ConsultarNfse(string arg0);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string ConsultarNfsePorRps(string arg0);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string ConsultarNfsePorRpsV3(string arg0, string arg1);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string ConsultarNfseV3(string arg0, string arg1);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string ConsultarSituacaoLoteRps(string arg0);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string ConsultarSituacaoLoteRpsV3(string arg0, string arg1);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string RecepcionarLoteRps(string arg0);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        string RecepcionarLoteRpsV3(string arg0, string arg1);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ServiceGinfesImplChannel : PS.Validate.Services.WSGinfes.ServiceGinfesImpl, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceGinfesImplClient : System.ServiceModel.ClientBase<PS.Validate.Services.WSGinfes.ServiceGinfesImpl>, PS.Validate.Services.WSGinfes.ServiceGinfesImpl {
        
        public ServiceGinfesImplClient() {
        }
        
        public ServiceGinfesImplClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceGinfesImplClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceGinfesImplClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceGinfesImplClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string CancelarNfse(string arg0) {
            return base.Channel.CancelarNfse(arg0);
        }
        
        public string CancelarNfseV3(string arg0, string arg1) {
            return base.Channel.CancelarNfseV3(arg0, arg1);
        }
        
        public string ConsultarLoteRps(string arg0) {
            return base.Channel.ConsultarLoteRps(arg0);
        }
        
        public string ConsultarLoteRpsV3(string arg0, string arg1) {
            return base.Channel.ConsultarLoteRpsV3(arg0, arg1);
        }
        
        public string ConsultarNfse(string arg0) {
            return base.Channel.ConsultarNfse(arg0);
        }
        
        public string ConsultarNfsePorRps(string arg0) {
            return base.Channel.ConsultarNfsePorRps(arg0);
        }
        
        public string ConsultarNfsePorRpsV3(string arg0, string arg1) {
            return base.Channel.ConsultarNfsePorRpsV3(arg0, arg1);
        }
        
        public string ConsultarNfseV3(string arg0, string arg1) {
            return base.Channel.ConsultarNfseV3(arg0, arg1);
        }
        
        public string ConsultarSituacaoLoteRps(string arg0) {
            return base.Channel.ConsultarSituacaoLoteRps(arg0);
        }
        
        public string ConsultarSituacaoLoteRpsV3(string arg0, string arg1) {
            return base.Channel.ConsultarSituacaoLoteRpsV3(arg0, arg1);
        }
        
        public string RecepcionarLoteRps(string arg0) {
            return base.Channel.RecepcionarLoteRps(arg0);
        }
        
        public string RecepcionarLoteRpsV3(string arg0, string arg1) {
            return base.Channel.RecepcionarLoteRpsV3(arg0, arg1);
        }
    }
}
