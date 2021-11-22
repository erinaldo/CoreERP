using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace ValidateLib
{
    public class Certificado
    {
        private AppLib.Data.Connection conn;

        public X509Certificate2 GetCertificado(string p_Subject)
        {
            X509Certificate2 X509Certif = new X509Certificate2();
            try
            {
                // Criando e abrindo lista com os certificados disponíveis na máquina
                X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection collection2 = (X509Certificate2Collection)collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);

                // Tratando o retorno da busca do certificado
                if (p_Subject == "")
                {
                    X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection1, "Certificado digital", "Selecione um certificado digital", X509SelectionFlag.SingleSelection);

                    if (scollection.Count == 0)
                    {
                        X509Certif.Reset();
                    }
                    else
                    {
                        X509Certif = scollection[0];
                    }
                }
                else
                {
                    X509Certificate2Collection scollection = (X509Certificate2Collection)collection1.Find(X509FindType.FindBySubjectDistinguishedName, p_Subject, false);
                    if (scollection.Count == 0)
                    {
                        X509Certif.Reset();
                    }
                    else
                    {
                        X509Certif = scollection[0];
                    }
                }
                store.Close();
                return X509Certif;
            }
            catch (Exception ex)
            {
                throw new Exception("Certificado Digital: " + ex.Message);
            }

            return null;
        }

        public X509Certificate2 GetCertificado(int IdEmpresa)
        {
            conn = AppLib.Context.poolConnection.Get("Start").Clone();
            //Dirlei
            //string CERTIFICADOTIPO = conn.ExecGetField(string.Empty, "SELECT CERTIFICADOTIPO FROM ZCONFIGEMP WHERE IDEMPRESA = ?", new Object[] { IdEmpresa }).ToString();
            string CERTIFICADOTIPO = string.Empty;
            if (CERTIFICADOTIPO == "C")
            {
                X509Certificate2 X509Certif = new X509Certificate2();
                try
                {
                    // Criando e abrindo lista com os certificados disponíveis na máquina
                    X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
                    store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                    X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                    X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                    X509Certificate2Collection collection2 = (X509Certificate2Collection)collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);

                    // Tratando o retorno da busca do certificado
                    //if (p_Subject == "")
                    //{
                        X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection1, "Certificado digital", "Selecione um certificado digital", X509SelectionFlag.SingleSelection);

                        if (scollection.Count == 0)
                        {
                            X509Certif.Reset();
                        }
                        else
                        {
                            X509Certif = scollection[0];
                        }
                    /*
                    }
                    else
                    {
                        X509Certificate2Collection scollection = (X509Certificate2Collection)collection1.Find(X509FindType.FindBySubjectDistinguishedName, p_Subject, false);
                        if (scollection.Count == 0)
                        {
                            X509Certif.Reset();
                        }
                        else
                        {
                            X509Certif = scollection[0];
                        }
                    }
                    */
                    store.Close();
                    return X509Certif;
                }
                catch (Exception ex)
                {
                    throw new Exception("Certificado Digital: " + ex.Message);
                }
            }

            if (CERTIFICADOTIPO == "A")
            {
                try
                {
                    //DIrlei
                    /*
                    string CERTIFICADOPATH = conn.ExecGetField(string.Empty, "SELECT CERTIFICADOPATH FROM ZCONFIGEMP WHERE IDEMPRESA = ?", new Object[] { IdEmpresa }).ToString();
                    string CERTIFICADOSENHA = conn.ExecGetField(string.Empty, "SELECT CERTIFICADOSENHA FROM ZCONFIGEMP WHERE IDEMPRESA = ?", new Object[] { IdEmpresa }).ToString();
                    */
                    
                    string CERTIFICADOPATH = string.Empty;
                    string CERTIFICADOSENHA = string.Empty;
                    

                    System.Security.Cryptography.X509Certificates.X509Certificate2 X509Certif = new System.Security.Cryptography.X509Certificates.X509Certificate2(
                        CERTIFICADOPATH, CERTIFICADOSENHA, System.Security.Cryptography.X509Certificates.X509KeyStorageFlags.MachineKeySet);
                    return X509Certif;
                }
                catch (Exception ex)
                {
                    throw new Exception("Certificado Digital: " + ex.Message);
                }           
            }

            return null;
        }


        //Dirlei
        /*
        public X509Certificate2 GetCertificado(ValidateLib.EmpresaParams EmpresaPar)
        {
            if (EmpresaPar.CertificadoTipo == "C")
            {
                X509Certificate2 X509Certif = new X509Certificate2();
                try
                {
                    // Criando e abrindo lista com os certificados disponíveis na máquina
                    X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
                    store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                    X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
                    X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                    X509Certificate2Collection collection2 = (X509Certificate2Collection)collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);

                    // Tratando o retorno da busca do certificado
                    //if (p_Subject == "")
                    //{
                    X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection1, "Certificado digital", "Selecione um certificado digital", X509SelectionFlag.SingleSelection);

                    if (scollection.Count == 0)
                    {
                        X509Certif.Reset();
                    }
                    else
                    {
                        X509Certif = scollection[0];
                    }
                    /*
                    }
                    else
                    {
                        X509Certificate2Collection scollection = (X509Certificate2Collection)collection1.Find(X509FindType.FindBySubjectDistinguishedName, p_Subject, false);
                        if (scollection.Count == 0)
                        {
                            X509Certif.Reset();
                        }
                        else
                        {
                            X509Certif = scollection[0];
                        }
                    }
                    //
                    store.Close();
                    return X509Certif;
                }
                catch (Exception ex)
                {
                    throw new Exception("Certificado Digital: " + ex.Message);
                }  
            }

            if (EmpresaPar.CertificadoTipo == "A")
            {
                try
                {
                    System.Security.Cryptography.X509Certificates.X509Certificate2 X509Certif = new System.Security.Cryptography.X509Certificates.X509Certificate2(
                        EmpresaPar.CertificadoPath, EmpresaPar.CertificadoSenha, System.Security.Cryptography.X509Certificates.X509KeyStorageFlags.MachineKeySet);
                    return X509Certif;
                }
                catch (Exception ex)
                {
                    throw new Exception("Certificado Digital: " + ex.Message);
                }  
            }

            return null;        
        }
        */
    }
}
