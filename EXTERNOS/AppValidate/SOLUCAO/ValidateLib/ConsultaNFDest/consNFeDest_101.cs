﻿//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.36213
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------
using System.Xml.Serialization;
// 
// This source code was auto-generated by xsd, Version=4.0.30319.17929.
// 
namespace ValidateLib.consNFeDest_101
{   
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.portalfiscal.inf.br/nfe")]
    [System.Xml.Serialization.XmlRootAttribute("consNFeDest", Namespace="http://www.portalfiscal.inf.br/nfe", IsNullable=false)]
    public partial class TConsNFeDest {
        
        private TAmb tpAmbField;
        
        private TConsNFeDestXServ xServField;
        
        private string cNPJField;
        
        private TConsNFeDestIndNFe indNFeField;
        
        private TConsNFeDestIndEmi indEmiField;
        
        private string ultNSUField;
        
        private TVeConsNFeDest versaoField;
        
        /// <remarks/>
        public TAmb tpAmb {
            get {
                return this.tpAmbField;
            }
            set {
                this.tpAmbField = value;
            }
        }
        
        /// <remarks/>
        public TConsNFeDestXServ xServ {
            get {
                return this.xServField;
            }
            set {
                this.xServField = value;
            }
        }
        
        /// <remarks/>
        public string CNPJ {
            get {
                return this.cNPJField;
            }
            set {
                this.cNPJField = value;
            }
        }
        
        /// <remarks/>
        public TConsNFeDestIndNFe indNFe {
            get {
                return this.indNFeField;
            }
            set {
                this.indNFeField = value;
            }
        }
        
        /// <remarks/>
        public TConsNFeDestIndEmi indEmi {
            get {
                return this.indEmiField;
            }
            set {
                this.indEmiField = value;
            }
        }
        
        /// <remarks/>
        public string ultNSU {
            get {
                return this.ultNSUField;
            }
            set {
                this.ultNSUField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public TVeConsNFeDest versao {
            get {
                return this.versaoField;
            }
            set {
                this.versaoField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.portalfiscal.inf.br/nfe")]
    public enum TAmb {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.portalfiscal.inf.br/nfe")]
    public enum TConsNFeDestXServ {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("CONSULTAR NFE DEST")]
        CONSULTARNFEDEST,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.portalfiscal.inf.br/nfe")]
    public enum TConsNFeDestIndNFe {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.portalfiscal.inf.br/nfe")]
    public enum TConsNFeDestIndEmi {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.portalfiscal.inf.br/nfe")]
    public enum TVeConsNFeDest {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1.01")]
        Item101,
    }
}
