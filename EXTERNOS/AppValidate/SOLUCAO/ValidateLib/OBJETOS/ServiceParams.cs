using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib
{
    [Serializable]
    public class ServiceParams : ParamsBase
    {
        private List<ValidateLib.EmpresaParams> _empresas;
        private List<ValidateLib.EmailParams> _emails;
        private List<ValidateLib.FTPParams> _ftps;
        private List<ValidateLib.PastaParams> _pastas;

        private int _limiteXmlAno;
        private DateTime _validadeLicenca;

        private int _idParametro;
        private string _licenca;
        private int _tempoEmail;
        private int _tempoSefaz;

        private int _tempoConsultaNFDestinatario;
        private int _tempoEventoManifestoNFDestinatario;
        private int _tempoDownloadNF;

        private int _prazoCancelamento;

        [DataMember]
        public List<ValidateLib.EmpresaParams> Empresas
        {
            get
            {
                return this._empresas;
            }
            set
            {
                this._empresas = value;
            }
        }

        [DataMember]
        public List<ValidateLib.EmailParams> Emails
        {
            get
            {
                return this._emails;
            }
            set
            {
                this._emails = value;
            }
        }

        [DataMember]
        public List<ValidateLib.FTPParams> FTPs
        {
            get
            {
                return this._ftps;
            }
            set
            {
                this._ftps = value;
            }
        }

        [DataMember]
        public List<ValidateLib.PastaParams> Pastas
        {
            get
            {
                return this._pastas;
            }
            set
            {
                this._pastas = value;
            }
        }

        [DataMember]
        public int LimiteXmlAno
        {
            get
            {
                return this._limiteXmlAno;
            }
            set
            {
                this._limiteXmlAno = value;
            }
        }

        [DataMember]
        public DateTime ValidadeLicenca
        {
            get
            {
                return this._validadeLicenca;
            }
            set
            {
                this._validadeLicenca = value;
            }
        }

        [ParamsAttribute("IDPARAMETRO")]
        [DataMember]
        public int IdParametro
        {
            get
            {
                return this._idParametro;
            }
            set
            {
                this._idParametro = value;
            }
        }

        [ParamsAttribute("LICENCA")]
        [DataMember]
        public string Licenca
        {
            get
            {
                return this._licenca;
            }
            set
            {
                this._licenca = value;
            }
        }

        [ParamsAttribute("TEMPOEMAIL")]
        [DataMember]
        public int TempoEmail
        {
            get
            {
                return this._tempoEmail;
            }
            set
            {
                this._tempoEmail = value;
            }
        }

        [ParamsAttribute("TEMPOSEFAZ")]
        [DataMember]
        public int TempoSefaz
        {
            get
            {
                return this._tempoSefaz;
            }
            set
            {
                this._tempoSefaz = value;
            }
        }

        [ParamsAttribute("TEMPOCONSULTANFDESTINATARIO")]
        [DataMember]
        public int TempoConsultaNFDestinatario
        {
            get
            {
                return this._tempoConsultaNFDestinatario;
            }
            set
            {
                this._tempoConsultaNFDestinatario = value;
            }
        }

        [ParamsAttribute("TEMPOEVENTOMANIFESTONFDESTINATARIO")]
        [DataMember]
        public int TempoEventoManifestoNFDestinatario
        {
            get
            {
                return this._tempoEventoManifestoNFDestinatario;
            }
            set
            {
                this._tempoEventoManifestoNFDestinatario = value;
            }
        }

        [ParamsAttribute("TEMPODOWNLOADNF")]
        [DataMember]
        public int TempoDownloadNF
        {
            get
            {
                return this._tempoDownloadNF;
            }
            set
            {
                this._tempoDownloadNF = value;
            }
        }

        [ParamsAttribute("PRAZOCANCELAMENTO")]
        [DataMember]
        public int PrazoCancelamento
        {
            get
            {
                return this._prazoCancelamento;
            }
            set
            {
                this._prazoCancelamento = value;
            }
        }

        public ServiceParams()
        { 
                    
        }

        public void TemLicenca()
        {
            try
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                System.Data.DataTable dt = conn.ExecQuery("SELECT LICENCA, DATEPART(YYYY, GETDATE()) ANO, GETDATE() HOJE FROM ZPARAM WHERE IDPARAMETRO = 1", new Object[] { });

                if (dt.Rows.Count > 0)
                {
                    String Licenca = dt.Rows[0]["LICENCA"].ToString();
                    int Ano = int.Parse(dt.Rows[0]["ANO"].ToString());
                    DateTime Hoje = DateTime.Parse(dt.Rows[0]["HOJE"].ToString());

                    String Result = new AppLib.Util.Criptografia().Decoder(AppLib.Util.Criptografia.OpcoesEncoder.Rijndael, Licenca, "merbibuziberbibonzi");

                    if (Result != null)
                    {
                        String[] temp = Result.Split(';');

                        if (temp.Length > 2)
                        {
                            int LimiteXMLAno = int.Parse(temp[0]);

                            DateTime validadeLicenca = DateTime.Parse(temp[1]);

                            int TotalXMLAno = int.Parse(conn.ExecGetField(0, "SELECT COUNT(IDINBOX) TOTAL FROM ZINBOX WHERE DATEPART(YYYY, DATA) = ? AND CODSTATUS IN ('PRE', 'VAL')", new Object[] { Ano }).ToString());

                            if (TotalXMLAno <= LimiteXMLAno)
                            {
                                if (validadeLicenca > Hoje)
                                {
                                    Contexto.Session.TemSaldoXMLAno = true;
                                }
                                else
                                {
                                    Contexto.Session.TemSaldoXMLAno = false;
                                    Log.Salvar("Erro XAN006");
                                }
                            }
                            else
                            {
                                Contexto.Session.TemSaldoXMLAno = false;
                                Log.Salvar("Limite de XML/Ano excedido.");
                            }
                        }
                        else
                        {
                            Contexto.Session.TemSaldoXMLAno = false;
                            Log.Salvar("Licença em formato desconhecido.");
                        }
                    }
                    else
                    {
                        Contexto.Session.TemSaldoXMLAno = false;
                        Log.Salvar("Erro ao decodificar licença.");
                    }
                }
                else
                {
                    Contexto.Session.TemSaldoXMLAno = false;
                    Log.Salvar("Parâmetro de licença não encontrado.");
                }
            }
            catch (Exception ex)
            {
                Contexto.Session.TemSaldoXMLAno = false;
                Log.Salvar("Erro desconhecido no backgroundWorker de licença.\r\n" + ex.Message);
            }
        }

        public void RemoveDuplicidade()
        {
            try
            {
                string sSql = @"DELETE ZINBOX
                                WHERE IDINBOX IN (

                                SELECT IDINBOX FROM (

                                SELECT IDINBOX, CHAVE, CODSTATUS,
                                ( SELECT MAX(IDINBOX) FROM ZINBOX Z WHERE CHAVE = ZINBOX.CHAVE AND CODSTATUS IN ('PRE', 'VAL') AND CODORIGEM <> 'CON') MAIOR

                                FROM ZINBOX
                                WHERE CODSTATUS IN ('PRE', 'VAL') AND CODORIGEM <> 'CON'

                                ) X
                                WHERE IDINBOX <>  MAIOR)";

                AppLib.Data.Connection _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                _conn.ExecTransaction(sSql, new Object[] { });
            }
            catch (Exception ex)
            {
                Log.SalvarLog("RemoveDuplicidade", ex.Message);
            }
        }

        private void PrepareLicenca()
        {
            try
            {
                if(_empresas == null)
                    _empresas = new List<ValidateLib.EmpresaParams>();

                string arrayLicenca = new AppLib.Util.Criptografia().Decoder(AppLib.Util.Criptografia.OpcoesEncoder.Rijndael, _licenca, "merbibuziberbibonzi");
                string[] parameters = arrayLicenca.Split(';');

                if(parameters.Length > 0)
                {
                    _limiteXmlAno = Convert.ToInt32(parameters[0]);
                    _validadeLicenca = Convert.ToDateTime(parameters[1]);

                    int cont = 0;
                    for (int i = 2; i < parameters.Length; i++)
                    {
                        _empresas.Add(ValidateLib.EmpresaParams.ReadByCNPJ( parameters[i]));
                        cont++;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.SalvarLog("PrepareLicenca", ex.Message);
            }
        }

        private void PrepareEmails()
        {
            try
            {
                if (_emails == null)
                    _emails = new List<ValidateLib.EmailParams>();

                AppLib.Data.Connection _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                System.Data.DataTable _dados = _conn.ExecQuery("SELECT * FROM ZCONFIGPOP");
                foreach (System.Data.DataRow row in _dados.Rows)
                {
                    _emails.Add(ValidateLib.EmailParams.ReadByIdConfigPOP(row["IDCONFIGPOP"]));
                }
            }
            catch (Exception ex)
            {
                Log.SalvarLog("PrepareEmails", ex.Message);
            }
        }

        private void PrepareFTPs()
        {
            try
            {
                if (_ftps == null)
                    _ftps = new List<ValidateLib.FTPParams>();

                AppLib.Data.Connection _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                System.Data.DataTable _dados = _conn.ExecQuery("SELECT * FROM ZCONFIGFTP");
                foreach (System.Data.DataRow row in _dados.Rows)
                {
                    _ftps.Add(ValidateLib.FTPParams.ReadByIdConfigFTP(row["IDCONFIGFTP"]));
                }
            }
            catch (Exception ex)
            {
                Log.SalvarLog("PrepareFTPs", ex.Message);
            }
        }

        private void PreparePastas()
        {
            try
            {
                if (_pastas == null)
                    _pastas = new List<ValidateLib.PastaParams>();

                AppLib.Data.Connection _conn = AppLib.Context.poolConnection.Get("Start").Clone();
                System.Data.DataTable _dados = _conn.ExecQuery("SELECT * FROM ZCONFIGDIR");
                foreach (System.Data.DataRow row in _dados.Rows)
                {
                    _pastas.Add(ValidateLib.PastaParams.ReadByIdConfigDIR(row["IDCONFIGDIR"]));
                }
            }
            catch (Exception ex)
            {
                Log.SalvarLog("PreparePastas", ex.Message);
            }
        }

        public static ServiceParams Read()
        {
            string sSql = @"SELECT * FROM ZPARAM";
            ServiceParams _serviceParams = new ServiceParams();
            _serviceParams.ReadFromCommand(sSql, null);
            _serviceParams.PrepareLicenca();
            _serviceParams.PrepareEmails();
            _serviceParams.PrepareFTPs();
            _serviceParams.PreparePastas();
            return _serviceParams;
        }
    }
}
