using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.Glb.New.DDFe
{
    public partial class frmDownloadLote : Form
    {
        Class.DDFeAPI DDFe = new Class.DDFeAPI();

        private List<DDFeEvento> listEvento = new List<DDFeEvento>();
        string PDF;

        public frmDownloadLote()
        {
            InitializeComponent();
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            string Token = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TOKEN FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, lpFilial.txtcodigo.Text }).ToString();

            if (string.IsNullOrEmpty(lpFilial.txtcodigo.Text))
            {
                MessageBox.Show("Para a execução do processo é necessário informar a filial.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(tbUltNSU.Text))
            {
                MessageBox.Show("Para a execução do processo é necessário informar o Número Sequencial.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int NumeroSequencial = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, "SELECT NSU FROM GDDFE WHERE NSU = ?", new object[] { tbUltNSU.Text }));

            if (NumeroSequencial > 0)
            {
                MessageBox.Show("NSU já existente.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormCaption("Processando...");

                DataTable dtFilial = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GFILIAL WHERE CODFILIAL = ?", new object[] { lpFilial.txtcodigo.Text });

                string CNPJ = dtFilial.Rows[0]["CGCCPF"].ToString().Replace(".", "").Replace("-", "").Replace("/", "");

                string Destinatario = dtFilial.Rows[0]["NOME"].ToString();
                string UfDestinatario = dtFilial.Rows[0]["CODETD"].ToString();

                string RetornoLote = DDFe.DownloadLote(Token, CNPJ, Convert.ToInt32(tbUltNSU.Text), 55, false, true, 1, true);

                dynamic JsonRetornoLote = JsonConvert.DeserializeObject(RetornoLote);

                string StatusLote = JsonRetornoLote.status;

                if (StatusLote == "200")
                {
                    List<DDFeLote> listLote = new List<DDFeLote>();
                    Newtonsoft.Json.Linq.JArray Root = new Newtonsoft.Json.Linq.JArray();
                    DataSet ds = new DataSet();

                    Root.Add(JsonRetornoLote.xmls);

                    int nsu;
                    string xml;
                    string chave;
                    string modelo;
                    string Estrutura;
                    string DescricaoEvento;

                    foreach (var Valores in Root.Children())
                    {
                        foreach (JObject obj in Valores.Children<JObject>())
                        {
                            nsu = (int)obj.GetValue("nsu");
                            xml = obj.GetValue("xml").ToString();
                            chave = obj.GetValue("chave").ToString();
                            modelo = obj.GetValue("modelo").ToString();

                            // Valida se o xml foi manifestado ou não
                            if (string.IsNullOrEmpty(xml))
                            {
                                // Obtém o retorno do manifesto 
                                string RetornoManifesto = DDFe.ManifestacaoDDFe(Token, CNPJ, chave, 210210, "Ciência da Operação");

                                dynamic JsonRetornoManifesto = JsonConvert.DeserializeObject(RetornoManifesto);

                                string StatusManifesto = JsonRetornoManifesto.status;

                                if (StatusManifesto == "200")
                                {
                                    dynamic RetEvento = JsonRetornoManifesto.retEvento;

                                    string IdEvento = setIdEvento();
                                    string Cstat = RetEvento.cStat;
                                    string Retorno = RetEvento.xMotivo;
                                    string Protocolo = RetEvento.nProt;
                                    DateTime DataEvento = RetEvento.dhRegEvento;
                                    string XMLRetorno = RetEvento.xml;

                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GDDFEMANIFESTO (IDEVENTOMDNF, CODEMPRESA, CHNFE, TPEVENTO, JUSTIFICATIVA, USUARIOINCLUSAO, DATAEVENTO, RETORNO, NPROT, CSTAT, XMLRETORNO)
                                                                                 VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { IdEvento, AppLib.Context.Empresa, chave, 210210, "Ciência da Operação", AppLib.Context.Usuario, DataEvento, Retorno, Protocolo, Cstat, XMLRetorno });
    
                                    listLote.Add(new DDFeLote(XMLRetorno, chave, nsu, modelo, PDF));

                                    continue;
                                }
                                else
                                {
                                    // 
                                }
                            }
                            else
                            {
                                if (obj.Properties().Count() == 9)
                                {
                                    PDF = obj.GetValue("pdf").ToString();
                                }

                                listLote.Add(new DDFeLote(xml, chave, nsu, modelo, PDF));
                            }             
                        }
                    }

                    listLote = FormataListasDDFe(listLote);

                    for (int i = 0; i < listLote.Count; i++)
                    {
                        int count = int.Parse(AppLib.Context.poolConnection.Get("Start").ExecGetField("0", "select COUNT(1) as 'CONT' from GDDFE where NSU = ? and CHAVE = ?", new Object[] { listLote[i].NSU, listLote[i].CHAVE }).ToString());

                        if (count == 0)
                        {
                            if (!string.IsNullOrEmpty(listLote[i].XML))
                            {
                                ds = DDFe.StringToDataSet(listLote[i].XML);
                            }
                            else
                            {
                                continue;
                            }

                            //ds = DDFe.StringToDataSet(listLote[i].XML);

                            if (listLote[i].XML.Contains("nfeProc"))
                            {
                                Estrutura = "NF-e";
                            }
                            else if (listLote[i].XML.Contains("cteProc"))
                            {
                                Estrutura = "CT-e";
                            }
                            else
                            {
                                continue;
                            }

                            AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GDDFE (CODEMPRESA, NSU, DATA, XMLRECEBIDO, CODESTRUTURA, VERSAO, MODELO, TPAMB, NUMERODOCUMENTO, CHAVE, DATAEMISSAO, EMITENTE, CNPJEMITENTE, UFEMITENTE, DESTINATARIO, CNPJDESTINATARIO, UFDESTINATARIO)
                                                                                     VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, listLote[i].NSU, DateTime.Now, listLote[i].XML, Estrutura, ds.Tables["nfeProc"].Rows[0]["versao"].ToString(), listLote[i].MODELO, 1, ds.Tables["ide"].Rows[0]["nNF"].ToString(), listLote[i].CHAVE, Convert.ToDateTime(ds.Tables["ide"].Rows[0]["dhEmi"]), ds.Tables["emit"].Rows[0]["xNome"].ToString(), ds.Tables["emit"].Rows[0]["CNPJ"].ToString(), ds.Tables["EnderEmit"].Rows[0]["UF"].ToString(), Destinatario, CNPJ, UfDestinatario });
                         
                            
                        }
                    }

                    for (int i = 0; i < listLote.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(listLote[i].PDF))
                        {
                            DDFe.SalvaArquivo(listLote[i].PDF, dtFilial.Rows[0]["PASTADESTINO"].ToString(), listLote[i].CHAVE);
                        }
                    }

                    if (listEvento.Count > 0)
                    {
                        for (int i = 0; i < listEvento.Count; i++)
                        {
                            ds = DDFe.StringToDataSet(listEvento[i].XML);

                            if (listEvento[i].XML.Contains("procEventoNFe"))
                            {
                                DescricaoEvento = setDescricaoEvento(ds.Tables[2].Rows[0]["tpEvento"].ToString());

                                if (ds.Tables[2].Rows.Count == 1)
                                {
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GDDFEEVENTO(CODEMPRESA, CHAVE, TPEVENTO, DATAEVENTO, NPROT, XML, DESCRICAOEVENTO)
                                                                                         VALUES(?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, listEvento[i].CHAVE, ds.Tables[2].Rows[0]["tpEvento"].ToString(), Convert.ToDateTime(ds.Tables[2].Rows[0]["dhEvento"].ToString()), ds.Tables[15].Rows[0]["nProt"].ToString(), listEvento[i].XML, DescricaoEvento });
                                }
                                else
                                {
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GDDFEEVENTO(CODEMPRESA, CHAVE, TPEVENTO, DATAEVENTO, NPROT, XML, DESCRICAOEVENTO)
                                                                                         VALUES(?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, listEvento[i].CHAVE, ds.Tables[2].Rows[0]["tpEvento"].ToString(), Convert.ToDateTime(ds.Tables[2].Rows[0]["dhEvento"].ToString()), ds.Tables[2].Rows[1]["nProt"].ToString(), listEvento[i].XML, DescricaoEvento });
                                }                             
                            }
                            else
                            {
                                DescricaoEvento = setDescricaoEvento(ds.Tables["resEvento"].Rows[0]["tpEvento"].ToString());

                                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GDDFEEVENTO(CODEMPRESA, CHAVE, TPEVENTO, DATAEVENTO, NPROT, XML, DESCRICAOEVENTO)
                                                                                         VALUES(?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, listEvento[i].CHAVE, ds.Tables["resEvento"].Rows[0]["tpEvento"].ToString(), Convert.ToDateTime(ds.Tables["resEvento"].Rows[0]["dhEvento"].ToString()), ds.Tables["resEvento"].Rows[0]["nProt"].ToString(), listEvento[i].XML, DescricaoEvento });
                            }
                        }
                    }
                }
                else
                {
                    splashScreenManager1.CloseWaitForm();
                    MessageBox.Show(JsonRetornoLote.motivo.ToString(), "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                splashScreenManager1.CloseWaitForm();
                MessageBox.Show("Processo realizado com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            catch (Exception ex)
            {
                splashScreenManager1.CloseWaitForm();

                MessageBox.Show("Erro ao inserir documento.\r\nDetalhes: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        public string setIdEvento()
        {
            string IdManifesto = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT ISNULL(MAX(IDEVENTOMDNF), 0) + 1 FROM GDDFEMANIFESTO WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }).ToString();
            return IdManifesto;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Métodos e classes para o Download em Lote

        class DDFeLote
        {
            public string XML { get; set; }
            public string CHAVE { get; set; }
            public int NSU { get; set; }
            public string MODELO { get; set; }
            public string PDF { get; set; }

            public DDFeLote(string _xml, string _chave, int _nsu, string _modelo, string _pdf = "")
            {
                XML = _xml;
                CHAVE = _chave;
                NSU = _nsu;
                MODELO = _modelo;
                PDF = _pdf;
            }
        }

        class DDFeEvento
        {
            public string XML { get; set; }
            public string CHAVE { get; set; }
            public string TPEVENTO { get; set; }

            public DDFeEvento(string _xml, string _chave)
            {
                XML = _xml;
                CHAVE = _chave;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_listDDFe"></param>
        /// <returns></returns>
        private List<DDFeLote> FormataListasDDFe(List<DDFeLote> _listDDFe)
        {
            for (int i = 0; i < _listDDFe.Count; i++)
            {
                if (_listDDFe[i].XML.Contains("resEvento") || _listDDFe[i].XML.Contains("procEventoNFe"))
                {
                    listEvento.Add(new DDFeEvento(_listDDFe[i].XML, _listDDFe[i].CHAVE));
                    _listDDFe.Remove(_listDDFe[i]);
                }
            }

            for (int i = 0; i < _listDDFe.Count; i++)
            {
                if (_listDDFe[i].XML.Contains("resEvento") || _listDDFe[i].XML.Contains("procEventoNFe"))
                {
                    listEvento.Add(new DDFeEvento(_listDDFe[i].XML, _listDDFe[i].CHAVE));
                    _listDDFe.Remove(_listDDFe[i]);
                }
            }

            for (int i = 0; i < _listDDFe.Count; i++)
            {
                if (_listDDFe[i].XML.Contains("resEvento") || _listDDFe[i].XML.Contains("procEventoNFe"))
                {
                    listEvento.Add(new DDFeEvento(_listDDFe[i].XML, _listDDFe[i].CHAVE));
                    _listDDFe.Remove(_listDDFe[i]);
                }
            }

            return _listDDFe;
        }

        private string setDescricaoEvento(string _tpEvento)
        {
            string descricao;

            switch (_tpEvento)
            {
                case "110110":
                    return descricao = "Carta de Correção";
                case "110111":
                    return descricao = "Cancelamento";
                case "210200":
                    return descricao = "Confirmação da Operação";
                case "210210":
                    return descricao = "Ciência da Operação";
                case "210220":
                    return descricao = "Desconhecimento da Operação";
                case "210240":
                    return descricao = "Operação não Realizada";
                case "310610":
                    return descricao = "MDF-e Autorizado para CT-e";
                case "310611":
                    return descricao = "MDF-e Cancelado Vinculado a CT-e";
                case "310620":
                    return descricao = "Registro de Passagem";
                case "510620":
                    return descricao = "Registro de Passagem BRID";
                case "610500":
                    return descricao = "Registro Passagem NF-e";
                case "610510":
                    return descricao = "Registro de Passagem de NFe propagado pelo MDFe";
                case "610514":
                    return descricao = "Registro de Passagem de NFe propagado pelo MDFe/CTe";
                case "610501":
                    return descricao = "Registro de Passagem para NF-e Cancelado";
                case "610550":
                    return descricao = "Registro de Passagem NFe RFID";
                case "610552":
                    return descricao = "Registro de Passagem Automatico MDFe";
                case "610554":
                    return descricao = "Registro de Passagem Automatico MDF-e com CT-e";
                case "610600":
                    return descricao = "CT-e Autorizado para NF-e";
                case "610601":
                    return descricao = "Ct-e Cancelado";
                case "610610":
                    return descricao = "MDF-e Autorizado para NF-e";
                case "610611":
                    return descricao = "MDF-e Cancelado";
                case "610614":
                    return descricao = "MDF-e Autorizado com CT-e";
                case "610615":
                    return descricao = "Cancelamento de MDF-e Autorizado com CT-e";
                case "790700":
                    return descricao = "Averbação para Exportação";
                default:
                    return descricao = string.Empty;
            }
        }

        #endregion

        private void frmDownloadLote_Load(object sender, EventArgs e)
        {
            tbUltNSU.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField("", "select top 1 (NSU + 1) as 'NSU' from GDDFE order by NSU Desc", new Object[] { }).ToString();
            //tbUltNSU.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField("", "select top 1 (NSU + 1) as 'NSU' from GDDFE where NSU < 117557 order by NSU Desc", new Object[] { }).ToString();
        }
    }
}
