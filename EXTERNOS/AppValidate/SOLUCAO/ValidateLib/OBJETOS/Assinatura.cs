using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.Xml;
using System.IO;

namespace ValidateLib
{
    public class Assinatura
    {
        public String RecalcularHash(String CODESTRUTURA, String TextoXML, String Chave, int IdEmpresa)
        {
            String TextoTemp = "";

            if (CODESTRUTURA.Equals("NF-e"))
            {
                TextoTemp = this.RemoverAssinaturaNFE(TextoXML);
            }

            if (CODESTRUTURA.Equals("CT-e"))
            {
                TextoTemp = this.RemoverAssinaturaCTE(TextoXML);
            }

            String TextoAssinado = this.AssinarXML(TextoTemp, "#" + CODESTRUTURA.Replace("-", "") + Chave, IdEmpresa);
            String HashCalculado = this.ObterHash(TextoAssinado);
            return HashCalculado;
        }

        public String RemoverAssinaturaNFE(String TextoXML)
        {
            String temp = TextoXML;
            String result = "";

            temp = temp.Substring(temp.IndexOf("<infNFe"), temp.IndexOf("</infNFe>") -
                                                                       temp.IndexOf("<infNFe") +
                                                                       "</infNFe>".Length);

            result = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><enviNFe xmlns=\"http://www.portalfiscal.inf.br/nfe\" versao=\"1.10\"><idLote>45</idLote><NFe>" +
                     temp +
                     "</NFe></enviNFe>";

            return result;
        }

        public String RemoverAssinaturaCTE(String TextoXML)
        {
            String temp = TextoXML;
            String result = "";

            temp = temp.Substring(temp.IndexOf("<infCte"), temp.IndexOf("</infCte>") -
                                                                       temp.IndexOf("<infCte") +
                                                                       "</infCte>".Length);

            result = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><enviCTe xmlns=\"http://www.portalfiscal.inf.br/cte\" versao=\"1.04\"><idLote>45</idLote><CTe xmlns=\"http://www.portalfiscal.inf.br/cte\">" +
                     temp +
                     "</CTe></enviCTe>";

            return result;
        }
        
        public String AssinarXML(String TextoXML, String Uri, int IdEmpresa)
        {
            try
            {
                // Obter o documento XML
                System.Xml.XmlDocument oDocNFE = new System.Xml.XmlDocument();
                oDocNFE.LoadXml(TextoXML);

                // Obter o certificado digital
                X509Certificate2 oCertificado;
                Certificado certificado = new Certificado();
                oCertificado = certificado.GetCertificado(IdEmpresa);

                if (oCertificado == null)
                {
                    throw new Exception("Certificado Digital não encontrado");
                }
                if ( ! oCertificado.HasPrivateKey)
                {
                    throw new Exception("Certificado Digital deve possuir chave privada.");
                }

                // Identificar e referenciar o "bloco" dentro do documento XML
                Reference oReference = new Reference();
                oReference.Uri = Uri;

                // Aplicar os algoritmos de transformação
                oReference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
                oReference.AddTransform(new XmlDsigC14NTransform());

                // Definir a chave de criptografia do algoritmo de assinatura assimétrica
                SignedXml oSignedXml = new SignedXml(oDocNFE);
                oSignedXml.SigningKey = oCertificado.PrivateKey;
                oSignedXml.AddReference(oReference);

                // Calcular a assinatura digital
                oSignedXml.ComputeSignature();

                // Adicionar o certificado ao documento NF-e assinado,
                KeyInfo keyInfo = new KeyInfo();
                keyInfo.AddClause(new KeyInfoX509Data(oCertificado));
                oSignedXml.KeyInfo = keyInfo;

                // Obter o "bloco" que representa o XML da assinatura
                System.Xml.XmlElement oValidatelementoAssinatura = oSignedXml.GetXml();

                // Adicionar o Elemento de assinatura ao documento NF-e
                oDocNFE.DocumentElement.AppendChild(oDocNFE.ImportNode(oValidatelementoAssinatura, true));

                return oDocNFE.InnerXml;
            }
            catch (System.Exception)
            {
                return "ERRO";
            }
        }

        private String ObterHash(String TextoXML)
        {
            String hash = TextoXML;
            hash = hash.Substring(hash.IndexOf("<DigestValue>") + "<DigestValue>".Length,
                                  hash.IndexOf("</DigestValue>") - hash.IndexOf("<DigestValue>") - "<DigestValue>".Length);
            return hash;
        }
        
    }
}
