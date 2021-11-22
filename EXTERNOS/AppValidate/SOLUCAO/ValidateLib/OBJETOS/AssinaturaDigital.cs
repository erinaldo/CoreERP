using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Windows.Forms;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;

namespace ValidateLib
{
    public class AssinaturaDigital
    {
        public bool ValidarAssinatura(string fileNFe, string tag)
        {
            XmlDocument xml = new XmlDocument();
            xml.PreserveWhitespace = true;
            xml.LoadXml(fileNFe);

            if (xml.GetElementsByTagName(tag).Count != 0)
            {
                return CheckSignature(xml.GetElementsByTagName(tag));
            }
            else
            {
                return false;
            }
        }

        private bool CheckSignature(XmlNodeList tagVal)
        {
            XmlNodeList nodeNFe = tagVal;
            string NFe = nodeNFe[0].OuterXml.ToString();

            XmlDocument xmlNFe = new XmlDocument();
            xmlNFe.PreserveWhitespace = true;
            xmlNFe.LoadXml(NFe);

            SignedXml signedXml = new SignedXml(xmlNFe);
            XmlNodeList nodeList = xmlNFe.GetElementsByTagName("Signature");
            signedXml.LoadXml((XmlElement)nodeList[0]);

            System.Collections.IEnumerator keyInfoItems = signedXml.KeyInfo.GetEnumerator();
            keyInfoItems.MoveNext();
            KeyInfoX509Data keyInfoX509 = (KeyInfoX509Data)keyInfoItems.Current;

            X509Certificate2 keyInfoCert = (X509Certificate2)keyInfoX509.Certificates[0];

            return signedXml.CheckSignature(keyInfoCert.PublicKey.Key);
        }

        public string AssinarNFSe(string arqXMLAssinar, string tagAssinatura, X509Certificate2 certificado)
        {
            string StringXMLAssinado = string.Empty;
            if (String.IsNullOrEmpty(tagAssinatura))
                return StringXMLAssinado;

            try
            {
                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.LoadXml(arqXMLAssinar);

                XmlNodeList listaTagsAssinar = XmlDoc.GetElementsByTagName(tagAssinatura);
                SignedXml signedXml;
                //SignedXmlWithId signedXml;

                foreach (XmlElement infNFe in listaTagsAssinar)
                {
                    // obter o valor da propriedade "Id" da tag que será assinada
                    string id = "";
                    if (infNFe.HasAttribute("id"))
                        id = infNFe.Attributes.GetNamedItem("id").Value;
                    else if (infNFe.HasAttribute("Id"))
                        id = infNFe.Attributes.GetNamedItem("Id").Value;
                    else if (infNFe.HasAttribute("ID"))
                        id = infNFe.Attributes.GetNamedItem("ID").Value;
                    else if (infNFe.HasAttribute("iD"))
                        id = infNFe.Attributes.GetNamedItem("iD").Value;
                    else
                        throw new Exception("Tag " + tagAssinatura + " não tem atributo Id");

                    signedXml = new SignedXml(infNFe);
                    //signedXml = new SignedXmlWithId(infNFe);
                    signedXml.SigningKey = certificado.PrivateKey;

                    // Transformações p/ DigestValue da Nota
                    Reference reference = new Reference("#" + id);
                    reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());

                    signedXml.SignedInfo.CanonicalizationMethod += "#WithComments";

                    //reference.AddTransform(new XmlDsigC14NTransform());
                    signedXml.AddReference(reference);

                    KeyInfo keyInfo = new KeyInfo();
                    keyInfo.AddClause(new KeyInfoX509Data(certificado));
                    signedXml.KeyInfo = keyInfo;
                    
                    // gerar o valor da assinatura
                    
                    signedXml.ComputeSignature();

                    // criar elemento <Signature>
                    XmlElement xmlSignature = XmlDoc.CreateElement("Signature", "http://www.w3.org/2000/09/xmldsig#");

                    // criar Id da tag <Signature>
                    XmlAttribute idAssinatura = XmlDoc.CreateAttribute("Id");
                    idAssinatura.Value = "Ass_" + id.Replace(":", "_");
                    xmlSignature.Attributes.InsertAfter(idAssinatura, xmlSignature.GetAttributeNode("xmlns"));

                    // gerar elemento <SignedInfo>
                    XmlElement xmlSignedInfo = signedXml.SignedInfo.GetXml();

                    // gerar elemento <KeyInfo>
                    XmlElement xmlKeyInfo = signedXml.KeyInfo.GetXml();

                    // compor nó <SignatureValue>
                    XmlElement xmlSignatureValue = XmlDoc.CreateElement("SignatureValue", xmlSignature.NamespaceURI);
                    string signBase64 = Convert.ToBase64String(signedXml.Signature.SignatureValue);
                    XmlText text = XmlDoc.CreateTextNode(signBase64);
                    xmlSignatureValue.AppendChild(text);

                    // incluir nós filhos da assinatura
                    xmlSignature.AppendChild(XmlDoc.ImportNode(xmlSignedInfo, true));
                    xmlSignature.AppendChild(xmlSignatureValue);
                    xmlSignature.AppendChild(XmlDoc.ImportNode(xmlKeyInfo, true));

                    // incluir assinatura no documento
                    infNFe.ParentNode.AppendChild(xmlSignature);

                    StringXMLAssinado = XmlDoc.OuterXml;
                }
            }
            catch (Exception caught)
            {
                throw (caught);
            }

            return StringXMLAssinado;
        }


        public string Assinar(string arqXMLAssinar, string tagAssinatura, string tagAtributoId, X509Certificate2 x509Cert)
        {
            string StringXMLAssinado = string.Empty;
            if (String.IsNullOrEmpty(tagAssinatura))
                return StringXMLAssinado;

            try
            {
                try
                {
                    // Create a new XML document.
                    XmlDocument doc = new XmlDocument();

                    // Format the document to ignore white spaces.
                    doc.PreserveWhitespace = false;

                    // Load the passed XML file using it’s name.
                    try
                    {
                        doc.LoadXml(arqXMLAssinar);

                        if (doc.GetElementsByTagName(tagAssinatura).Count == 0)
                        {
                            throw new Exception("A tag de assinatura " + tagAssinatura.Trim() + " não existe no XML. (Código do Erro: 5)");
                        }
                        else if (doc.GetElementsByTagName(tagAtributoId).Count == 0)
                        {
                            throw new Exception("A tag de assinatura " + tagAtributoId.Trim() + " não existe no XML. (Código do Erro: 4)");
                        }
                        // Existe mais de uma tag a ser assinada
                        else
                        {
                            try
                            {
                                XmlDocument XMLDoc;

                                XmlNodeList lists = doc.GetElementsByTagName(tagAssinatura);
                                foreach (XmlNode nodes in lists)
                                {
                                    foreach (XmlNode childNodes in nodes.ChildNodes)
                                    {
                                        if (!childNodes.Name.Equals(tagAtributoId))
                                            continue;

                                        if (childNodes.NextSibling != null && childNodes.NextSibling.Name.Equals("Signature"))
                                            continue;

                                        // Create a reference to be signed
                                        Reference reference = new Reference();
                                        reference.Uri = "";

                                        // pega o uri que deve ser assinada                                       
                                        XmlElement childElemen = (XmlElement)childNodes;
                                        if (childElemen.GetAttributeNode("Id") != null)
                                        {
                                            reference.Uri = "#" + childElemen.GetAttributeNode("Id").Value;
                                        }
                                        else if (childElemen.GetAttributeNode("id") != null)
                                        {
                                            reference.Uri = "#" + childElemen.GetAttributeNode("id").Value;
                                        }
                                        /*
                                        XmlAttributeCollection _Uri = childElemen.GetElementsByTagName(tagAtributoId).Item(0).Attributes;

                                        if (_Uri.Count > 0)
                                            foreach (XmlAttribute _atributo in _Uri)
                                            {
                                                if (_atributo.Name == "Id" || _atributo.Name == "id")
                                                {
                                                    reference.Uri = "#" + _atributo.InnerText;
                                                }
                                            }
                                        */

                                        // Create a SignedXml object.
                                        SignedXml signedXml = new SignedXml(doc);

                                        // Add the key to the SignedXml document
                                        signedXml.SigningKey = x509Cert.PrivateKey;

                                        // Add an enveloped transformation to the reference.
                                        XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                                        reference.AddTransform(env);

                                        XmlDsigC14NTransform c14 = new XmlDsigC14NTransform();
                                        reference.AddTransform(c14);

                                        // Add the reference to the SignedXml object.
                                        signedXml.AddReference(reference);

                                        // Create a new KeyInfo object
                                        KeyInfo keyInfo = new KeyInfo();

                                        // Load the certificate into a KeyInfoX509Data object
                                        // and add it to the KeyInfo object.
                                        keyInfo.AddClause(new KeyInfoX509Data(x509Cert));

                                        // Add the KeyInfo object to the SignedXml object.
                                        signedXml.KeyInfo = keyInfo;
                                        signedXml.ComputeSignature();

                                        // Get the XML representation of the signature and save
                                        // it to an XmlElement object.
                                        XmlElement xmlDigitalSignature = signedXml.GetXml();

                                        // Gravar o elemento no documento XML
                                        nodes.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
                                    }
                                }

                                XMLDoc = new XmlDocument();
                                XMLDoc.PreserveWhitespace = false;
                                XMLDoc = doc;

                                // Atualizar a string do XML já assinada
                                StringXMLAssinado = XMLDoc.OuterXml;
                            }
                            catch (Exception caught)
                            {
                                throw (caught);
                            }
                        }
                    }
                    catch (Exception caught)
                    {
                        throw (caught);
                    }
                }
                catch (Exception caught)
                {
                    throw (caught);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return StringXMLAssinado;
        }
    }
}