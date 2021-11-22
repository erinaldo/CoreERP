using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PS.Lib
{
    public class Login
    {
        private Data.DBS dbs;
        private Global gb;

        public Login()
        { 
        
        }

        public bool InitializeFile()
        {
            bool Flag = false;
            dbs = new Data.DBS();

            Flag = dbs.QueryFind(@"SELECT DISTINCT GEMPRESA.CODEMPRESA, GEMPRESA.NOMEFANTASIA, GEMPRESA.NOME, GEMPRESA.CGCCPF 
                                    FROM GUSUARIOPERFIL, GEMPRESA 
                                    WHERE GUSUARIOPERFIL.CODUSUARIO = ? 
                                    AND GEMPRESA.CODEMPRESA = GUSUARIOPERFIL.CODEMPRESA",
                                    Contexto.Session.CodUsuario);

            return Flag;
                   
        }

        public void SetReferenciaClasse()
        {
            gb.MontaReferenciaClasse();
        }

        public void SetAlias(Alias alias)
        {
            if (IsBase64Encoded(alias.Password))
            {
                alias.Password = new PS.Lib.Criptografia().Decoder(PS.Lib.Criptografia.OpcoesEncoder.Rijndael, alias.Password, alias.UserName);
            }

            Contexto.Alias.Name = alias.Name;
            Contexto.Alias.DbType = alias.DbType;
            Contexto.Alias.DbProviderType = alias.DbProviderType;
            Contexto.Alias.DbName = alias.DbName;
            Contexto.Alias.ServerName = alias.ServerName;
            Contexto.Alias.UserName = alias.UserName;
            Contexto.Alias.Password = alias.Password;

            //criptografia
            //Contexto.Alias.Password = new PS.Lib.Criptografia().Decoder(PS.Lib.Criptografia.OpcoesEncoder.Rijndael, alias.Password, alias.UserName);

            Contexto.Alias.SyncService = alias.SyncService;

            //atribui a criptografia
            alias.Password = Contexto.Alias.Password;

            Contexto.Alias.ConnectionString = new Data.StringConnBuilder().Build(alias);        
        }

        public bool IsBase64Encoded(String str)
        {
            try
            {
                // If no exception is caught, then it is possibly a base64 encoded string
                byte[] data = Convert.FromBase64String(str);
                // The part that checks if the string was properly padded to the
                // correct length was borrowed from d@anish's solution
                return (str.Replace(" ", "").Length % 4 == 0);
            }
            catch
            {
                // If exception is caught, then it is not a base64 encoded string
                return false;
            }
        }

        public System.Data.DataTable EmpresaList()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dbs = new Data.DBS();

            dt = dbs.QuerySelect(@"SELECT GEMPRESA.CODEMPRESA, GEMPRESA.NOMEFANTASIA, GEMPRESA.NOME, GEMPRESA.CGCCPF, GEMPRESA.INSCRICAOESTADUAL, GEMPRESA.CODCONTROLE, GEMPRESA.CODCHAVE1, GEMPRESA.CODCHAVE2
                                    FROM GEMPRESA");

            return dt;
        }

        public System.Data.DataTable EmpresaUserList()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dbs = new Data.DBS();
            
            dt = dbs.QuerySelect(@"SELECT DISTINCT GEMPRESA.CODEMPRESA, GEMPRESA.NOMEFANTASIA, GEMPRESA.NOME, GEMPRESA.CGCCPF, GEMPRESA.INSCRICAOESTADUAL, GEMPRESA.CODCONTROLE, GEMPRESA.CODCHAVE1, GEMPRESA.CODCHAVE2
                                    FROM GUSUARIOPERFIL, GEMPRESA 
                                    WHERE GUSUARIOPERFIL.CODUSUARIO = ? 
                                    AND GEMPRESA.CODEMPRESA = GUSUARIOPERFIL.CODEMPRESA", 
                                    Contexto.Session.CodUsuario);

            return dt;
        }

        public void Autenticar(Alias alias)
        {
            this.SetAlias(alias);

            dbs = new Data.DBS(true);
        }

        public void TestarConexao(Alias alias)
        {
            try
            {
                if (alias == null)
                {
                    throw new Exception("Informe o alias.");
                }

                this.SetAlias(alias);

                dbs = new Data.DBS(true);
                gb = new Global();

                dbs.QuerySelect(@"SELECT 1 FROM GEMPRESA WHERE 1 = 0");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Autenticar(Alias alias, string codUsuario, string senha, string codModulo)
        {
            try
            {
                if (alias == null)
                {
                    throw new Exception("Informe o alias.");
                }

                if (string.IsNullOrEmpty(codUsuario))
                {
                    throw new Exception("Informe o usuário.");
                }

                if (string.IsNullOrEmpty(senha))
                {
                    throw new Exception("Informe a senha.");
                }

                this.SetAlias(alias);

                dbs = new Data.DBS(true);

                #region INTEGRAÇÃO DO PS FRAMEWORK COM A APP LIB V2

                AppLib.Context.poolConnection.Add("Start", AppLib.Global.Types.Database.SqlClient, dbs.ConnectionString.Substring(18));

                if (AppLib.Context.poolConnection.Get("Start").Test() == false)
                {
                    throw new Exception("Erro de conexão com a camada AppLibV2");
                }

                #endregion

                #region EXECUTA OS MÉTODOS DA LIB

                #endregion

                gb = new Global();

                PSVersion vrs = new PSVersion();
                Controle ctl = new Controle();

                object nome = dbs.QueryValue(string.Empty, @"SELECT NOME FROM GUSUARIO WHERE CODUSUARIO = ? AND SENHA = ?", codUsuario, new PS.Lib.Criptografia().Encoder(PS.Lib.Criptografia.OpcoesEncoder.Rijndael, senha, codUsuario));
                int ativo = (int)dbs.QueryValue(0, @"SELECT ATIVO FROM GUSUARIO WHERE CODUSUARIO = ? ", codUsuario);
                int perfil = (int)dbs.QueryValue(0, @"SELECT COUNT(CODUSUARIO) FROM GUSUARIOPERFIL WHERE CODUSUARIO = ? ", codUsuario);
                int nucaexpira = (int)dbs.QueryValue(0, @"SELECT NUNCAEXPIRA FROM GUSUARIO WHERE CODUSUARIO = ? ", codUsuario);
                DateTime expira = (DateTime)dbs.QueryValue(DateTime.Today, @"SELECT DTEXPIRACAO FROM GUSUARIO WHERE CODUSUARIO = ? ", codUsuario);

                if (!nome.Equals(string.Empty))
                {
                    if (ativo > 0)
                    {
                        if (perfil > 0)
                        {
                            if (nucaexpira == 0)
                            {
                                if (expira < DateTime.Today)
                                {
                                    throw new Exception("Usuário expirado.");
                                }
                            }

                            if (!ctl.ValidaIntegridade())
                            {
                                throw new Exception("Erro de Integridade do Banco de Dados.");
                            }

                            if (!vrs.VerificaVersaoBase())
                            {
                                throw new Exception("Versão do aplicativo diferente da versão da base de dados.");
                            }

                            dbs.QueryExec("UPDATE GUSUARIO SET ULTIMOLOGIN = ? WHERE CODUSUARIO = ?", DateTime.Now, codUsuario);

                            this.SetReferenciaClasse();

                            Contexto.Session.CodUsuario = codUsuario;
                            Contexto.Session.Caixa = null;
                            Contexto.Session.CodModulo = null;
                            Contexto.Session.CodPerfil = null;
                            Contexto.Session.Empresa = null;
                            Contexto.Session.Current = null;
                        }
                        else
                        {
                            throw new Exception("Usuário não possui um perfil vinculado.");
                        }
                    }
                    else
                    {
                        throw new Exception("Usuário Inativo.");
                    }
                }
                else
                {
                    throw new Exception("Usuário ou Senha inválidos.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
