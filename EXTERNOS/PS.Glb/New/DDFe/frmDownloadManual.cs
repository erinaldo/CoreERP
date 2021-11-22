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
    public partial class frmDownloadManual : Form
    {
        Class.DDFeAPI DDFe = new Class.DDFeAPI();

        public frmDownloadManual()
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

            if (string.IsNullOrEmpty(tbChave.Text))
            {
                MessageBox.Show("Para a execução do processo é necessário informar a chave.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ChaveDDFe = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CHAVE FROM GDDFE WHERE CHAVE = ?", new object[] { tbChave.Text }).ToString();

            if (!string.IsNullOrEmpty(ChaveDDFe))
            {
                MessageBox.Show("Chave já existente.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                string RetornoManual = DDFe.DownloadUnico(Token, CNPJ, tbChave.Text, null, 1, 57, true, true, true);

                dynamic JsonRetornoManual = JsonConvert.DeserializeObject(RetornoManual);

                string StatusManual = JsonRetornoManual.status;

                if (StatusManual == "200")
                {
                    string NSU;
                    string XML;
                    string Versao = string.Empty;
                    string Estrutura = string.Empty;
                    string Chave = string.Empty;
                    int Modelo;

                    string ListDocs = JsonRetornoManual.listaDocs;

                    if (ListDocs == "True")
                    {

                        List<DDFeManual> listDDFe = new List<DDFeManual>();
                        Newtonsoft.Json.Linq.JArray Root = new Newtonsoft.Json.Linq.JArray();

                        Root.Add(JsonRetornoManual.xmls);

                        foreach (var Valores in Root.Children())
                        {
                            foreach (JObject obj in Valores.Children<JObject>())
                            {
                                NSU = obj.GetValue("nsu").ToString();
                                XML = obj.GetValue("xml").ToString();

                                listDDFe.Add(new DDFeManual(XML, NSU));
                            }
                        }

                        DataSet ds = new DataSet();

                        for (int i = 0; i < listDDFe.Count; i++)
                        {
                            if (listDDFe[i].XML.Contains("procEventoCTe"))
                            {
                                continue;
                            }

                            ds = DDFe.StringToDataSet(listDDFe[i].XML);

                            if (listDDFe[i].XML.Contains("3.10"))
                            {
                                Versao = "3.10";
                            }
                            else if (listDDFe[i].XML.Contains("3.00"))
                            {
                                Versao = "3.00";
                            }
                            else if (listDDFe[i].XML.Contains("4.00"))
                            {
                                Versao = "4.00";
                            }
                            else
                            {
                                Versao = "1.00";
                            }

                            if (listDDFe[i].XML.Contains("nfeProc"))
                            {
                                Estrutura = "NF-e";
                                Chave = ds.Tables["infProt"].Rows[i]["chNFe"].ToString();
                                Modelo = 55;

                                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GDDFE (CODEMPRESA, NSU, DATA, XMLRECEBIDO, CODESTRUTURA, VERSAO, MODELO, TPAMB, NUMERODOCUMENTO, CHAVE, DATAEMISSAO, EMITENTE, CNPJEMITENTE, UFEMITENTE, DESTINATARIO, CNPJDESTINATARIO, UFDESTINATARIO)
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, listDDFe[i].NSU, DateTime.Now, listDDFe[i].XML, Estrutura, Versao, Modelo, 1, ds.Tables["ide"].Rows[0]["nNF"].ToString(), Chave, Convert.ToDateTime(ds.Tables["ide"].Rows[0]["dhEmi"]), ds.Tables["emit"].Rows[0]["xNome"].ToString(), ds.Tables["emit"].Rows[0]["CNPJ"].ToString(), ds.Tables["EnderEmit"].Rows[0]["UF"].ToString(), Destinatario, CNPJ, UfDestinatario });
                            }
                            else if (listDDFe[i].XML.Contains("cteProc")) 
                            {
                                Estrutura = "CT-e";
                                Chave = ds.Tables["infProt"].Rows[0]["chCTe"].ToString();
                                Modelo = 57;

                                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GDDFE (CODEMPRESA, NSU, DATA, XMLRECEBIDO, CODESTRUTURA, VERSAO, MODELO, TPAMB, NUMERODOCUMENTO, CHAVE, DATAEMISSAO, EMITENTE, CNPJEMITENTE, UFEMITENTE, DESTINATARIO, CNPJDESTINATARIO, UFDESTINATARIO)
                                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, listDDFe[i].NSU, DateTime.Now, listDDFe[i].XML, Estrutura, Versao, Modelo, 1, ds.Tables["infProt"].Rows[0]["nProt"].ToString(), Chave, Convert.ToDateTime(ds.Tables["ide"].Rows[0]["dhEmi"]), ds.Tables["emit"].Rows[0]["xNome"].ToString(), ds.Tables["emit"].Rows[0]["CNPJ"].ToString(), ds.Tables["EnderEmit"].Rows[0]["UF"].ToString(), Destinatario, CNPJ, UfDestinatario });
                            }
                            else if (listDDFe[i].XML.Contains("resEvento"))
                            {
                                string DescricaoEvento = setDescricaoEvento(ds.Tables["resEvento"].Rows[0]["tpEvento"].ToString());

                                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GDDFEEVENTO(CODEMPRESA, CHAVE, TPEVENTO, DATAEVENTO, NPROT, XML, DESCRICAOEVENTO)
                                                                                         VALUES(?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, ds.Tables["resEvento"].Rows[0]["chNFe"].ToString(), ds.Tables["resEvento"].Rows[0]["tpEvento"].ToString(), Convert.ToDateTime(ds.Tables["resEvento"].Rows[0]["dhEvento"].ToString()), ds.Tables["resEvento"].Rows[0]["nProt"].ToString(), string.Empty, DescricaoEvento });
                            }
                        }
                    }
                    else
                    {
                        NSU = JsonRetornoManual.nsu;
                        XML = JsonRetornoManual.xml;
                        Chave = JsonRetornoManual.chave;
                        string PDF = JsonRetornoManual.pdf;

                        if (!string.IsNullOrEmpty(PDF))
                        {
                            DDFe.SalvaArquivo(PDF, dtFilial.Rows[0]["PASTADESTINO"].ToString(), Chave);
                        }
            
                        //System.Diagnostics.Process.Start(dtFilial.Rows[0]["PASTADESTINO"].ToString() + "\\" + Chave + "-procDDfe.pdf");

                        if (XML.Contains("3.10"))
                        {
                            Versao = "3.10";
                        }
                        else if (XML.Contains("4.00"))
                        {
                            Versao = "4.00";
                        }

                        else if (XML.Contains("3.00"))
                        {
                            Versao = "3.00";
                        }
                        else
                        {
                            Versao = "1.00";
                        }

                        DataSet ds = DDFe.StringToDataSet(XML);

                        if (XML.Contains("nfeProc"))
                        {
                            Estrutura = "NF-e";

                            AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GDDFE (CODEMPRESA, NSU, DATA, XMLRECEBIDO, CODESTRUTURA, VERSAO, MODELO, TPAMB, NUMERODOCUMENTO, CHAVE, DATAEMISSAO, EMITENTE, CNPJEMITENTE, UFEMITENTE, DESTINATARIO, CNPJDESTINATARIO, UFDESTINATARIO)
                                                                                     VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, NSU, DateTime.Now, XML, Estrutura, Versao, 55, 1, ds.Tables["ide"].Rows[0]["nNF"].ToString(), Chave, Convert.ToDateTime(ds.Tables["ide"].Rows[0]["dhEmi"]), ds.Tables["emit"].Rows[0]["xNome"].ToString(), ds.Tables["emit"].Rows[0]["CNPJ"].ToString(), ds.Tables["EnderEmit"].Rows[0]["UF"].ToString(), Destinatario, CNPJ, UfDestinatario });
                        }

                        else if (XML.Contains("cteProc"))
                        {
                            Estrutura = "CT-e";

                            AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GDDFE (CODEMPRESA, NSU, DATA, XMLRECEBIDO, CODESTRUTURA, VERSAO, MODELO, TPAMB, NUMERODOCUMENTO, CHAVE, DATAEMISSAO, EMITENTE, CNPJEMITENTE, UFEMITENTE, DESTINATARIO, CNPJDESTINATARIO, UFDESTINATARIO)
                                                                                     VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, NSU, DateTime.Now, XML, Estrutura, Versao, 55, 1, ds.Tables["ide"].Rows[0]["nCT"].ToString(), Chave, Convert.ToDateTime(ds.Tables["ide"].Rows[0]["dhEmi"]), ds.Tables["emit"].Rows[0]["xNome"].ToString(), ds.Tables["emit"].Rows[0]["CNPJ"].ToString(), ds.Tables["EnderEmit"].Rows[0]["UF"].ToString(), Destinatario, CNPJ, UfDestinatario });
                        }

                        else if (XML.Contains("resEvento"))
                        {
                            string DescricaoEvento = setDescricaoEvento(ds.Tables["resEvento"].Rows[0]["tpEvento"].ToString());

                            AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GDDFEEVENTO(CODEMPRESA, CHAVE, TPEVENTO, DATAEVENTO, NPROT, XML, DESCRICAOEVENTO)
                                                                                         VALUES(?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, ds.Tables["resEvento"].Rows[0]["chNFe"].ToString(), ds.Tables["resEvento"].Rows[0]["tpEvento"].ToString(), Convert.ToDateTime(ds.Tables["resEvento"].Rows[0]["dhEvento"].ToString()), ds.Tables["resEvento"].Rows[0]["nProt"].ToString(), string.Empty, DescricaoEvento });
                        }                    
                    }
                }
                else
                {
                    MessageBox.Show("Não foi possível realizar a requisição.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    if (splashScreenManager1.IsSplashFormVisible)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }

                    return;
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible)
                {
                    splashScreenManager1.CloseWaitForm();
                }

                return;
            }

            splashScreenManager1.CloseWaitForm();
            MessageBox.Show("Processo realizado com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Dispose();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Métodos e rotinas 

        class DDFeManual
        {
            public string XML { get; set; }
            public string NSU { get; set; }

            public DDFeManual(string _xml, string _nsu)
            {
                XML = _xml;
                NSU = _nsu;
            }
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
    }
}
