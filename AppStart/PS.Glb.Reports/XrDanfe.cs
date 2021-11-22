using System.Collections.Generic;
using System.Data;
using System;
using System.Drawing;
using System.Collections;
using System.Xml;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
namespace PS.Glb.Reports
{
     
    public partial class XrDanfe : DevExpress.XtraReports.UI.XtraReport
    {
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        canhoto canhoto = new canhoto();
        ArrayList lista = new ArrayList();
        XmlDocument doc = new XmlDocument();
        System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();

        System.Xml.XmlTextReader reader;
        System.IO.StringReader stringReader;
        private List<PS.Lib.DataField> Parametros { get; set; }
        public System.Data.DataSet dsXML;
        string[] split;
        int itensCount;

        public XrDanfe(List<PS.Lib.DataField> Params)
        {
            InitializeComponent();

            this.Parametros = Params;

            try
            {
                string Texto = dbs.QueryValue(string.Empty, "SELECT XMLNFE FROM GNFESTADUAL WHERE CODEMPRESA = ? AND CODOPER = ?", this.Parametros[0].Valor, this.Parametros[1].Valor).ToString();
                int indexi = Texto.IndexOf('<', 0);
                int indexf = Texto.IndexOf('>', 0);

                string substituir = Texto.Substring(indexi, indexf + 1);

                if (substituir.Contains("xml version"))
                    Texto = Texto.Replace(substituir, "<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                else
                    Texto = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Texto;

                dsXML = StringToDataSet(Texto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public System.Data.DataSet StringToDataSet(string XML)
        {
            try
            {
                xdoc.LoadXml(XML);
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(xdoc.DocumentElement.OuterXml)));

                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void TopMargin_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lista = new ArrayList();
            bool fat = false;
            if (dsXML.Tables.Contains("dup"))
            {
                for (int i = 0; i < dsXML.Tables["dup"].Rows.Count; i++)
                {
                     item item = new item();
                    item.nDup = dsXML.Tables["dup"].Rows[i]["nDup"].ToString();
                    item.dVenc = Convert.ToDateTime(dsXML.Tables["dup"].Rows[i]["dVenc"].ToString());
                    item.vDup = Convert.ToDecimal(dsXML.Tables["dup"].Rows[i]["vDup"].ToString().Replace(".", ","));
                    lista.Add(item);
                    fat = false;
                }
            }
            else if (dsXML.Tables.Contains("fat"))
            {
                for (int i = 0; i < dsXML.Tables["fat"].Rows.Count; i++)
                {
                    item item = new item();
                    item.nFat = dsXML.Tables["fat"].Rows[i]["nFat"].ToString();
                    item.vOrig = Convert.ToDecimal(dsXML.Tables["fat"].Rows[i]["vOrig"].ToString().Replace(".", ","));
                    item.vLiq = Convert.ToDecimal(dsXML.Tables["fat"].Rows[i]["vLiq"].ToString().Replace(".", ","));
                    lista.Add(item);
                    fat = true;
                }
            }

            if (lista.Count > 0)
            {
                this.DetailReport.DataSource = lista;
                if (fat)
                {
                    xrLabel102.Text = "NUM. FAT.:";
                    xrLabel22.Text = "VALOR ORIG.:";
                    xrLabel21.Text = "VALOR LIQ.:";
                    xrLabel103.DataBindings.Add("Text", null, "nFat");
                    xrLabel89.DataBindings.Add("Text", null, "vOrig", "{0:n2}");
                    xrLabel20.DataBindings.Add("Text", null, "vLiq", "{0:n2}");
                }
                else
                {
                    xrLabel102.Text = "NUM.:";
                    xrLabel22.Text = "VENC.:";
                    xrLabel21.Text = "VALOR.:";
                    xrLabel103.DataBindings.Add("Text", null, "nDup");
                    xrLabel89.DataBindings.Add("Text", null, "dVenc", "{0: dd/MM/yyyy}");
                    xrLabel20.DataBindings.Add("Text", null, "vDup", "{0:n2}");
                }
            }
            else
            {
                xrLabel19.Visible = false;
                xrLabel102.Visible = false;
                xrLabel103.Visible = false;
                xrLabel22.Visible = false;
                xrLabel89.Visible = false;
                xrLabel21.Visible = false;
                xrLabel20.Visible = false;
                xrPanel23.Visible = false;
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (dsXML.Tables["ide"] != null)
            {
                if (dsXML.Tables["ide"].Columns["dhEmi"] != null)
                {
                    txtDataEmissao.Text = Convert.ToDateTime(dsXML.Tables["ide"].Rows[0]["dhEmi"].ToString()).ToShortDateString();
                }
                if (dsXML.Tables["ide"].Columns["dhSaiEnt"] != null)
                {
                    txtDataSaidaDest.Text = Convert.ToDateTime(dsXML.Tables["ide"].Rows[0]["dhSaiEnt"].ToString()).ToShortDateString();
                }
                if (dsXML.Tables["ide"].Columns["dhSaiEnt"] != null)
                {
                    split = dsXML.Tables["ide"].Rows[0]["dhSaiEnt"].ToString().Split('T');
                    split = split[1].ToString().Split('-');
                    txtHoraDest.Text = Convert.ToDateTime(split[0].ToString()).ToLongTimeString();
                }
            }

            if (dsXML.Tables["dest"] != null)
            {
                if (dsXML.Tables["dest"].Columns["xNome"] != null)
                {
                    txtNomeRazao.Text = dsXML.Tables["dest"].Rows[0]["xNome"].ToString();
                }
                if (dsXML.Tables["dest"].Columns["CNPJ"] != null)
                {
                    txtCnpjDest.Text = String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToUInt64(dsXML.Tables["dest"].Rows[0]["CNPJ"].ToString()));
                }
                if (dsXML.Tables["dest"].Columns["CPF"] != null)
                {
                    txtCnpjDest.Text = String.Format(@"{0:000\.000\.000\-00}", Convert.ToUInt64(dsXML.Tables["dest"].Rows[0]["CPF"].ToString()));
                }
                if (dsXML.Tables["dest"].Columns["IE"] != null)
                {
                    txtInscrDest.Text = dsXML.Tables["dest"].Rows[0]["IE"].ToString();
                }
            }
            if (dsXML.Tables["enderDest"] != null)
            {
                if (dsXML.Tables["enderDest"].Columns["xLgr"] != null)
                {
                    txtEndDest.Text = dsXML.Tables["enderDest"].Rows[0]["xLgr"].ToString() + ", ";
                }
                if (dsXML.Tables["enderDest"].Columns["nro"] != null)
                {
                    txtEndDest.Text = txtEndDest.Text + dsXML.Tables["enderDest"].Rows[0]["nro"].ToString();
                }
                if (dsXML.Tables["enderDest"].Columns["xCpl"] != null)
                {
                    txtEndDest.Text = txtEndDest.Text + " - " + dsXML.Tables["enderDest"].Rows[0]["xCpl"].ToString();
                }
                if (dsXML.Tables["enderDest"].Columns["xBairro"] != null)
                {
                    txtBairroDest.Text = dsXML.Tables["enderDest"].Rows[0]["xBairro"].ToString();
                }
                if (dsXML.Tables["enderDest"].Columns["CEP"] != null)
                {
                    txtCepDest.Text = string.Format("{0:00000-000}", Convert.ToUInt64(dsXML.Tables["enderDest"].Rows[0]["CEP"].ToString()));
                }
                if (dsXML.Tables["enderDest"].Columns["xMun"] != null)
                {
                    txtMunicipioDest.Text = dsXML.Tables["enderDest"].Rows[0]["xMun"].ToString();
                }
                if (dsXML.Tables["enderDest"].Columns["UF"] != null)
                {
                    txtUfDest.Text = dsXML.Tables["enderDest"].Rows[0]["UF"].ToString();
                }
                if (dsXML.Tables["enderDest"].Columns["fone"] != null)
                {
                    txtFoneDest.Text = string.Format("{0:(00) 0000-0000}", Convert.ToUInt64(dsXML.Tables["enderDest"].Rows[0]["fone"].ToString()));
                }
            }
            if (dsXML.Tables["ICMSTot"] != null)
            {
                if (dsXML.Tables["ICMSTot"].Columns["vBC"] != null)
                {
                    xrLabel105.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vBC"].ToString().Replace(".", ",")));
                }
                if (dsXML.Tables["ICMSTot"].Columns["vICMS"] != null)
                {
                    xrLabel106.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vICMS"].ToString().Replace(".", ",")));
                }
                if (dsXML.Tables["ICMSTot"].Columns["vBCST"] != null)
                {
                    xrLabel107.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vBCST"].ToString().Replace(".", ",")));
                }
                if (dsXML.Tables["ICMSTot"].Columns["vST"] != null)
                {
                    xrLabel108.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vST"].ToString().Replace(".", ",")));
                }
                if (dsXML.Tables["ICMSTot"].Columns["vII"] != null)
                {
                    xrLabel109.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vII"].ToString().Replace(".", ",")));
                }
                if (dsXML.Tables["ICMSTot"].Columns["vProd"] != null)
                {
                    xrLabel110.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vProd"].ToString().Replace(".", ",")));
                }
                if (dsXML.Tables["ICMSTot"].Columns["vFrete"] != null)
                {
                    xrLabel116.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vFrete"].ToString().Replace(".", ",")));
                }
                if (dsXML.Tables["ICMSTot"].Columns["vSeg"] != null)
                {
                    xrLabel115.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vSeg"].ToString().Replace(".", ",")));
                }
                if (dsXML.Tables["ICMSTot"].Columns["vDesc"] != null)
                {
                    xrLabel114.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vDesc"].ToString().Replace(".", ",")));
                }
                if (dsXML.Tables["ICMSTot"].Columns["vOutro"] != null)
                {
                    xrLabel113.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vOutro"].ToString().Replace(".", ",")));
                }
                if (dsXML.Tables["ICMSTot"].Columns["vIPI"] != null)
                {
                    xrLabel112.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vIPI"].ToString().Replace(".", ",")));
                }
                if (dsXML.Tables["ICMSTot"].Columns["vNF"] != null)
                {
                    xrLabel111.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vNF"].ToString().Replace(".", ",")));
                }
                if (dsXML.Tables["ICMSTot"].Columns["vPIS"] != null)
                {
                    xrLabel83.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vPIS"].ToString().Replace(".", ",")));
                }
                if (dsXML.Tables["ICMSTot"].Columns["vCOFINS"] != null)
                {
                    xrLabel4.Text = string.Format("{0:n}", Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vCOFINS"].ToString().Replace(".", ",")));
                }
            }
            if (dsXML.Tables["transp"].Columns["modFrete"] != null)
            {
                switch (dsXML.Tables["transp"].Rows[0]["modFrete"].ToString())
                {
                    case "1":
                        xrLabel117.Text = "(1) Dest/Rem";
                        break;
                    case "2":
                        xrLabel117.Text = "(2) Terceiros";
                        break;
                    case "9":
                        xrLabel117.Text = "(9) Sem Frete";
                        break;
                    default:
                        xrLabel117.Text = "(0) Emitente";
                        break;
                }
            }
            if (dsXML.Tables["transporta"] != null)
            {
                if (dsXML.Tables["transporta"].Columns["xNome"] != null)
                {
                    xrLabel118.Text = dsXML.Tables["transporta"].Rows[0]["xNome"].ToString();
                }
                if (dsXML.Tables["transporta"].Columns["CNPJ"] != null)
                {
                    xrLabel122.Text = String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToUInt64(dsXML.Tables["transporta"].Rows[0]["CNPJ"].ToString()));
                }
                else if (dsXML.Tables["transporta"].Columns["CPF"] != null)
                {
                    xrLabel122.Text = String.Format(@"{0:000\.000\.000\-00}", Convert.ToUInt64(dsXML.Tables["transporta"].Rows[0]["CPF"].ToString()));
                }
                if (dsXML.Tables["transporta"].Columns["xEnder"] != null)
                {
                    xrLabel123.Text = dsXML.Tables["transporta"].Rows[0]["xEnder"].ToString();
                }
                if (dsXML.Tables["transporta"].Columns["xMun"] != null)
                {
                    xrLabel124.Text = dsXML.Tables["transporta"].Rows[0]["xMun"].ToString();
                }
                if (dsXML.Tables["transporta"].Columns["UF"] != null)
                {
                    xrLabel125.Text = dsXML.Tables["transporta"].Rows[0]["UF"].ToString();
                }
                if (dsXML.Tables["transporta"].Columns["IE"] != null)
                {
                    xrLabel126.Text = dsXML.Tables["transporta"].Rows[0]["IE"].ToString();
                }
            }
            if (dsXML.Tables["veicTransp"] != null)
            {
                if (dsXML.Tables["veicTransp"].Columns["placa"] != null)
                {
                    xrLabel120.Text = dsXML.Tables["veicTransp"].Rows[0]["placa"].ToString();
                }
                if (dsXML.Tables["veicTransp"].Columns["UF"] != null)
                {
                    xrLabel121.Text = dsXML.Tables["veicTransp"].Rows[0]["UF"].ToString();
                }
            }
            if (dsXML.Tables["vol"] != null)
            {

                if (dsXML.Tables["vol"].Columns["qVol"] != null)
                {
                    xrLabel127.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["vol"].Rows[0]["qVol"].ToString()));
                }
                if (dsXML.Tables["vol"].Columns["esp"] != null)
                {
                    xrLabel128.Text = dsXML.Tables["vol"].Rows[0]["esp"].ToString();
                }
                if (dsXML.Tables["vol"].Columns["marca"] != null)
                {
                    xrLabel129.Text = dsXML.Tables["vol"].Rows[0]["marca"].ToString();
                }
                if (dsXML.Tables["vol"].Columns["pesoL"] != null)
                {
                    xrLabel131.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["vol"].Rows[0]["pesoL"].ToString().Replace(".", ",")));
                }
                if (dsXML.Tables["vol"].Columns["pesoB"] != null)
                {
                    xrLabel132.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["vol"].Rows[0]["pesoB"].ToString().Replace(".", ",")));
                }
            }
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (dsXML.Tables["infAdic"] != null)
            {
                if (dsXML.Tables["infAdic"].Columns["infCpl"] != null)
                {
                    xrLabel85.Text = "Inf. Contribuinte: " + dsXML.Tables["infAdic"].Rows[0]["infCpl"].ToString();
                }
            }
        }

        private void DetailReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            itens();
            this.DetailReport1.DataSource = lista;
            xrLabel1.DataBindings.Add("Text", null, "cProd");
            xrLabel68.DataBindings.Add("Text", null, "xProd");
            xrLabel75.DataBindings.Add("Text", null, "vProd", "{0:n2}");
            xrLabel69.DataBindings.Add("Text", null, "NCM");
            xrLabel70.DataBindings.Add("Text", null, "orig");
            xrLabel82.DataBindings.Add("Text", null, "CST");
            xrLabel71.DataBindings.Add("Text", null, "CFOP");
            xrLabel72.DataBindings.Add("Text", null, "uCom");
            xrLabel73.DataBindings.Add("Text", null, "qCom", "{0:n2}");
            xrLabel74.DataBindings.Add("Text", null, "vUnCom", "{0:n2}");
            xrLabel76.DataBindings.Add("Text", null, "vBC", "{0:n2}");
            xrLabel77.DataBindings.Add("Text", null, "vICMS", "{0:n2}");
            xrLabel78.DataBindings.Add("Text", null, "vIPI", "{0:n2}");
            xrLabel79.DataBindings.Add("Text", null, "pICMS", "{0:n2}");
            xrLabel80.DataBindings.Add("Text", null, "pIPI", "{0:n2}");
        }

        private void itens()
        {
            lista = new ArrayList();
            stringReader = new System.IO.StringReader(xdoc.InnerXml);
            reader = new XmlTextReader(stringReader);
            string elemento;
            bool icms = false;
            item item = new item();
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        elemento = reader.Name.ToString();
                        switch (elemento)
                        {
                            case "cProd":
                                item.cProd = reader.ReadString();
                                break;
                            case "xProd":
                                item.xProd = reader.ReadString() + " - " + item.cEan;
                                break;
                            case "cEAN":
                                item.cEan = reader.ReadString();
                                break;
                            case "vProd":
                                item.vProd = Convert.ToDecimal(reader.ReadString().Replace(".", ","));
                                break;
                            case "NCM":
                                item.NCM = reader.ReadString();
                                break;
                            case "orig":
                                item.orig = Convert.ToDecimal(reader.ReadString().Replace(".", ","));
                                break;
                            case "CFOP":
                                item.CFOP = reader.ReadString();
                                break;
                            case "uCom":
                                item.uCom = reader.ReadString();
                                break;
                            case "qCom":
                                item.qCom = Convert.ToDecimal(reader.ReadString().Replace(".", ","));
                                break;
                            case "vUnCom":
                                item.vUnCom = Convert.ToDecimal(reader.ReadString().Replace(".", ","));
                                break;
                            case "vBC":
                                item.vBC = Convert.ToDecimal(reader.ReadString().Replace(".", ","));
                                break;
                            case "vICMS":
                                item.vICMS = Convert.ToDecimal(reader.ReadString().Replace(".", ","));
                                break;
                            case "vIPI":
                                item.vIPI = Convert.ToDecimal(reader.ReadString().Replace(".", ","));
                                break;
                            case "pICMS":
                                item.pICMS = Convert.ToDecimal(reader.ReadString().Replace(".", ","));
                                break;
                            case "pIPI":
                                item.pIPI = Convert.ToDecimal(reader.ReadString().Replace(".", ","));
                                break;
                            case "CSOSN":
                                item.CSOSN = Convert.ToDecimal(reader.ReadString().Replace(".", ","));
                                break;
                            case "ICMS":
                                icms = true;
                                break;
                            case "CST":
                                if (icms.Equals(true))
                                {
                                    item.CST = reader.ReadString();
                                    icms = false;
                                }
                                
                                break;
                            default:
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name.Equals("det"))
                        {
                            item.orig = item.orig + item.CSOSN;
                            lista.Add(item);
                            item = new item();
                        }
                        break;
                }
            }
            itensCount = lista.Count;
            reader.Close();
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sSql = @"SELECT IMAGEM FROM GIMAGEM WHERE CODIMAGEM = (SELECT CODIMAGEM FROM GEMPRESA WHERE CODEMPRESA = ?)";
            byte[] arrayimagem = (byte[])dbs.QueryValue(null, sSql, this.Parametros[0].Valor);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(arrayimagem);
         //   logo.Image = Image.FromStream(ms);

            if (dsXML.Tables["enderEmit"] != null)
            {
                if (dsXML.Tables["enderEmit"].Columns["IEST"] != null)
                {
                    txtInscricaoEstadualSubst.Text = dsXML.Tables["enderEmit"].Rows[0]["IEST"].ToString();
                }
            }
            if (dsXML.Tables["emit"].Columns["xNome"] != null)
            {
                txtNomeEmitente.Text = dsXML.Tables["emit"].Rows[0]["xNome"].ToString();
            }
            if (dsXML.Tables["enderEmit"].Columns["xLgr"] != null)
            {
                txtEndEmitente.Text = dsXML.Tables["enderEmit"].Rows[0]["xLgr"].ToString();
                if (dsXML.Tables["enderEmit"].Columns["nro"] != null)
                {
                    txtEndEmitente.Text = txtEndEmitente.Text + ", " + dsXML.Tables["enderEmit"].Rows[0]["nro"].ToString();
                }
            }
            if (dsXML.Tables["enderEmit"].Columns["xBairro"] != null)
            {
                txtBairroEmitente.Text = dsXML.Tables["enderEmit"].Rows[0]["xBairro"].ToString();
                if (dsXML.Tables["enderEmit"].Columns["CEP"] != null)
                {
                    txtBairroEmitente.Text = txtBairroEmitente.Text + " - " + string.Format("{0:#####-###}", Convert.ToUInt64(dsXML.Tables["enderEmit"].Rows[0]["CEP"].ToString()));
                }
            }
            if (dsXML.Tables["enderEmit"].Columns["xMun"] != null)
            {
                txtCidadeEmitente.Text = dsXML.Tables["enderEmit"].Rows[0]["xMun"].ToString();
                if (dsXML.Tables["enderEmit"].Columns["UF"] != null)
                {
                    txtCidadeEmitente.Text = txtCidadeEmitente.Text + " - " + dsXML.Tables["enderEmit"].Rows[0]["UF"].ToString();
                    if (dsXML.Tables["enderEmit"].Columns["fone"] != null)
                    {
                        txtCidadeEmitente.Text = txtCidadeEmitente.Text + " Fone/Fax: " + string.Format("{0:(##) ####-####}", Convert.ToUInt64(dsXML.Tables["enderEmit"].Rows[0]["fone"].ToString()));
                    }
                }
            }
            if (dsXML.Tables["ide"].Columns["tpNF"] != null)
            {
                txtTipoNota.Text = dsXML.Tables["ide"].Rows[0]["tpNF"].ToString();
            }
            txtNumero1.Text = txtNumero.Text;
            txtSerie1.Text = txtNumeroSerie.Text;
            BarcodeLib.Barcode barcode = new BarcodeLib.Barcode();
            xrPictureBox1.Image = barcode.Encode(BarcodeLib.TYPE.CODE128C, dsXML.Tables["infNFe"].Rows[0]["Id"].ToString().Remove(0, 3), Color.Black, Color.White, 306, 40);
            //Montagem da chave de acesso
            if (dsXML.Tables["infNFe"].Columns["Id"] != null)
            {
                string bloco1 = string.Format(@"{0:0000 0000 0000 0000}", Convert.ToUInt64(dsXML.Tables["infNFe"].Rows[0]["Id"].ToString().Remove(0, 3).Substring(0, 16)));
                string bloco2 = string.Format(@"{0: 0000 0000 0000 0000}", Convert.ToUInt64(dsXML.Tables["infNFe"].Rows[0]["Id"].ToString().Remove(0, 3).Substring(16, 16)));
                string bloco3 = string.Format(@"{0: 0000 0000 0000}", Convert.ToUInt64(dsXML.Tables["infNFe"].Rows[0]["Id"].ToString().Remove(0, 3).Substring(32, 12)));
                txtChaveAcesso.Text = bloco1 + bloco2 + bloco3;
            }
            if (dsXML.Tables["infProt"].Columns["dhRecbto"] != null)
            {
                split = dsXML.Tables["infProt"].Rows[0]["dhRecbto"].ToString().Split('T');
                DateTime dataProtolocolo = Convert.ToDateTime(split[0].ToString());
                txtProtocoloAutorizacao.Text = dsXML.Tables["infProt"].Rows[0]["nProt"].ToString() + " - " + Convert.ToDateTime(split[0].ToString()).ToShortDateString() + " " + Convert.ToDateTime(split[1].ToString()).ToShortTimeString();
            }
            if (dsXML.Tables["emit"].Columns["CNPJ"] != null)
            {
                txtCnpj.Text = String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToUInt64(dsXML.Tables["emit"].Rows[0]["CNPJ"].ToString()));
            }
            if (dsXML.Tables["emit"].Columns["IE"] != null)
            {
                txtInscricaoEstadual.Text = dsXML.Tables["emit"].Rows[0]["IE"].ToString();
            }
            if (dsXML.Tables["ide"].Columns["natOp"] != null)
            {
                txtNaturezaOperacao.Text = dsXML.Tables["ide"].Rows[0]["natOp"].ToString();
            }
        }

        private void ReportHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (dsXML.Tables["emit"] != null)
            //{
            //    if (dsXML.Tables["emit"].Columns["xNome"] != null)
            //    {
            //        canhoto.eXNome = dsXML.Tables["emit"].Rows[0]["xNome"].ToString();
            //    }
            //}
            //if (dsXML.Tables["ide"] != null)
            //{
            //    if (dsXML.Tables["ide"].Columns["dhEmi"] != null)
            //    {
            //        canhoto.dEmi = Convert.ToDateTime(dsXML.Tables["ide"].Rows[0]["dhEmi"].ToString()).ToShortDateString();
            //    }
            //}
            //if (dsXML.Tables["total"] != null)
            //{
            //    if (dsXML.Tables["ICMSTot"].Columns["vNF"] != null)
            //    {
            //        canhoto.vLiq = Convert.ToDecimal(dsXML.Tables["ICMSTot"].Rows[0]["vNF"].ToString().Replace(".", ","));
            //    }
            //}
            //if (dsXML.Tables["dest"] != null)
            //{
            //    if (dsXML.Tables["dest"].Columns["xNome"] != null)
            //    {
            //        canhoto.dXnome = dsXML.Tables["dest"].Rows[0]["xNome"].ToString();
            //    }
            //}
            //if (dsXML.Tables["enderDest"] != null)
            //{
            //    if (dsXML.Tables["enderDest"].Columns["xLgr"] != null)
            //    {
            //        canhoto.xLgr = dsXML.Tables["enderDest"].Rows[0]["xLgr"].ToString();
            //    }
            //    if (dsXML.Tables["enderDest"].Columns["nro"] != null)
            //    {
            //        canhoto.nro = dsXML.Tables["enderDest"].Rows[0]["nro"].ToString();
            //    }
            //    if (dsXML.Tables["enderDest"].Columns["xCpl"] != null)
            //    {
            //        canhoto.xCpl = dsXML.Tables["enderDest"].Rows[0]["xCpl"].ToString();
            //    }
            //    if (dsXML.Tables["enderDest"].Columns["xBairro"] != null)
            //    {
            //        canhoto.xBairro = dsXML.Tables["enderDest"].Rows[0]["xBairro"].ToString();
            //    }
            //    if (dsXML.Tables["enderDest"].Columns["xMun"] != null)
            //    {
            //        canhoto.xMun = dsXML.Tables["enderDest"].Rows[0]["xMun"].ToString();
            //    }
            //}
            //txtEmpresa.Text = string.Format(@"RECEBEMOS DE {0} OS PRODUTOS E/OU SERVIÇOS CONSTANTES DA NOTA FISCAL ELETRÔNICA INDICADA ABAIXO. EMISSÃO {1: dd/MM/yyyy} VALOR TOTAL {2:c} DESTINATÁRIO: {3}, {4} - {5} - {6}. {7} {8}", canhoto.eXNome, canhoto.dEmi, canhoto.vLiq, canhoto.dXnome, canhoto.xLgr, canhoto.nro, canhoto.xCpl, canhoto.xBairro, canhoto.xMun);
            //if (dsXML.Tables["ide"].Columns["nNF"] != null)
            //{
            //    txtNumero.Text = dsXML.Tables["ide"].Rows[0]["nNF"].ToString();
            //    switch (txtNumero.Text.Length)
            //    {
            //        case 1:
            //            txtNumero.Text = "Nº 000.000.00" + txtNumero.Text;
            //            break;
            //        case 2:
            //            txtNumero.Text = "Nº 000.000.0" + txtNumero.Text;
            //            break;
            //        case 3:
            //            txtNumero.Text = "Nº 000.000." + txtNumero.Text;
            //            break;
            //        case 4:
            //            txtNumero.Text = "Nº 000.00" + txtNumero.Text.Substring(0, 1) + "." + txtNumero.Text.Substring(1, 3);
            //            break;
            //        case 5:
            //            txtNumero.Text = "Nº 000.0" + txtNumero.Text.Substring(0, 2) + "." + txtNumero.Text.Substring(2, 3);
            //            break;
            //        case 6:
            //            txtNumero.Text = "Nº 000." + txtNumero.Text.Substring(0, 3) + "." + txtNumero.Text.Substring(3, 3);
            //            break;
            //        case 7:
            //            txtNumero.Text = "Nº 00" + txtNumero.Text.Substring(0, 1) + "." + txtNumero.Text.Substring(1, 3) + "." + txtNumero.Text.Substring(4, 3);
            //            break;
            //        case 8:
            //            txtNumero.Text = "Nº 0" + txtNumero.Text.Substring(0, 2) + "." + txtNumero.Text.Substring(2, 3) + "." + txtNumero.Text.Substring(5, 3);
            //            break;
            //        case 9:
            //            txtNumero.Text = "Nº 0" + txtNumero.Text.Substring(0, 3) + "." + txtNumero.Text.Substring(3, 3) + "." + txtNumero.Text.Substring(6, 3);
            //            break;

            //        default:
            //            break;
            //    }
            //}
            //if (dsXML.Tables["ide"].Columns["serie"] != null)
            //{
            //    txtNumeroSerie.Text = dsXML.Tables["ide"].Rows[0]["serie"].ToString();
            //    if (txtNumeroSerie.Text.Length.Equals(1))
            //    {
            //        txtNumeroSerie.Text = "Série 00" + txtNumeroSerie.Text;
            //    }
            //    else if (txtNumeroSerie.Text.Length.Equals(2))
            //    {
            //        txtNumeroSerie.Text = "Série 0" + txtNumeroSerie.Text;
            //    }
            //}
        }

        private void PageFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (dsXML.Tables["infAdic"] != null)
            {
                if (dsXML.Tables["infAdic"].Columns["infCpl"] != null)
                {
                    xrLabel85.Text = "Inf. Contribuinte: " + dsXML.Tables["infAdic"].Rows[0]["infCpl"].ToString();
                }    
            }
            
        }

        private void PageFooter_AfterPrint(object sender, EventArgs e)
        {
            if (lista.Count > 16)
            {
                PageFooter.Visible = false;
            }
        }

        private void xrLabel84_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void XrDanfe_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }
    }
}
