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
    public partial class XrDactePaisagem : DevExpress.XtraReports.UI.XtraReport
    {
       PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        canhoto canhoto = new canhoto();
        ArrayList lista = new ArrayList();
        XmlDocument doc = new XmlDocument();
        System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();

        private List<PS.Lib.DataField> Parametros { get; set; }
        public System.Data.DataSet dsXML;
        public string path;

        public XrDactePaisagem(List<PS.Lib.DataField> Params)
        {
            InitializeComponent();
            this.Parametros = Params;
            dsXML = StringToDataSet(Parametros[2].Valor.ToString());
            path = Parametros[2].Valor.ToString();
            //Sub-report
            XrRelComponentesPrestacao relComponentes = new XrRelComponentesPrestacao();
            relComponentes.dsXML = dsXML;
            xrSubreport1.ReportSource = relComponentes;
        }
        public System.Data.DataSet StringToDataSet(string XML)
        {
            try
            {
                xdoc.Load(XML);
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(xdoc.DocumentElement.OuterXml)));

                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (dsXML.Tables["ide"] != null)
            {
                if (dsXML.Tables["ide"].Columns["nCT"] != null)
                {
                    xrLabel9.Text = "Nº DOCUMENTO " + dsXML.Tables["ide"].Rows[0]["nCT"].ToString();
                }
                if (dsXML.Tables["ide"].Columns["serie"] != null)
                {
                    xrLabel2.Text = "SÉRIE" + " " + dsXML.Tables["ide"].Rows[0]["serie"].ToString();
                }
            }
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (dsXML.Tables["ide"] != null)
            {
                if (dsXML.Tables["emit"].Columns["xNome"] != null)
                {
                    xrLabel3.Text = dsXML.Tables["emit"].Rows[0]["xNome"].ToString();
                }

                if (dsXML.Tables["ide"].Columns["modal"] != null)
                {
                    switch (dsXML.Tables["ide"].Rows[0]["modal"].ToString())
                    {
                        case "01":
                            xrLabel16.Text = "Rodoviário";
                            break;
                        case "02":
                            xrLabel16.Text = "Aéreo";
                            break;
                        case "03":
                            xrLabel16.Text = "Aquaviário";
                            break;
                        case "04":
                            xrLabel16.Text = "Ferroviário";
                            break;
                        case "05":
                            xrLabel16.Text = "Dutoviário";
                            break;
                        default:
                            xrLabel16.Text = "Multimodal";
                            break;
                    }
                }
                if (dsXML.Tables["ide"].Columns["mod"] != null)
                {
                    xrLabel20.Text = dsXML.Tables["ide"].Rows[0]["mod"].ToString();
                }

                if (dsXML.Tables["ide"].Columns["serie"] != null)
                {
                    xrLabel17.Text = dsXML.Tables["ide"].Rows[0]["serie"].ToString();
                }
                if (dsXML.Tables["ide"].Columns["nCT"] != null)
                {
                    xrLabel21.Text = dsXML.Tables["ide"].Rows[0]["nCT"].ToString();
                }
                if (dsXML.Tables["ide"].Columns["dhEmi"] != null)
                {
                    xrLabel25.Text = Convert.ToDateTime(dsXML.Tables["ide"].Rows[0]["dhEmi"].ToString().Replace("T", " ")).ToString();
                }
                if (dsXML.Tables["ide"].Columns["tpCTe"] != null)
                {
                    switch (dsXML.Tables["ide"].Rows[0]["tpCTe"].ToString())
                    {
                        case "0":
                            xrLabel30.Text = "CT-e Normal";
                            break;
                        case "1":
                            xrLabel30.Text = "CT-e de Complemento de Valores";
                            break;
                        case "2":
                            xrLabel30.Text = "CT-e de Anulação";
                            break;
                        default:
                            xrLabel30.Text = "CT-e Substituto";
                            break;
                    }
                }
                if (dsXML.Tables["ide"].Columns["tpServ"] != null)
                {
                    switch (dsXML.Tables["ide"].Rows[0]["tpServ"].ToString())
                    {
                        case "0":
                            xrLabel32.Text = "Normal";
                            break;
                        case "1":
                            xrLabel32.Text = "Subcontratação;";
                            break;
                        case "2":
                            xrLabel32.Text = "Redespacho";
                            break;
                        case "3":
                            xrLabel32.Text = "Redespacho Intermediário";
                            break;
                        default:
                            xrLabel32.Text = "Serviço Vinculado a Multimodal";
                            break;
                    }
                }
                if (dsXML.Tables["ide"].Columns["forPag"] != null)
                {
                    switch (dsXML.Tables["ide"].Rows[0]["forPag"].ToString())
                    {
                        case "0":
                            xrLabel37.Text = "Pago";
                            break;
                        case "1":
                            xrLabel37.Text = "A Pagar";
                            break;
                        default:
                            xrLabel37.Text = "Outros";
                            break;
                    }
                }
                //BarcodeLib.Barcode barcode = new BarcodeLib.Barcode();
                //xrPictureBox1.Image = barcode.Encode(BarcodeLib.TYPE.CODE128C, dsXML.Tables["infCte"].Rows[0]["Id"].ToString().Remove(0, 3), Color.Black, Color.White, 300, 100);
                if (dsXML.Tables["enderEmit"] != null)
                {
                    if (dsXML.Tables["enderEmit"].Columns["xLgr"] != null)
                    {
                        xrLabel10.Text = dsXML.Tables["enderEmit"].Rows[0]["xLgr"].ToString();
                        if (dsXML.Tables["enderEmit"].Columns["nro"] != null)
                        {
                            xrLabel10.Text = dsXML.Tables["enderEmit"].Rows[0]["xLgr"].ToString() + ", " + dsXML.Tables["enderEmit"].Rows[0]["nro"].ToString();
                        }
                        if (dsXML.Tables["enderEmit"].Columns["xBairro"] != null)
                        {
                            xrLabel11.Text = dsXML.Tables["enderEmit"].Rows[0]["xBairro"].ToString();
                            if (dsXML.Tables["enderEmit"].Columns["CEP"] != null)
                            {
                                xrLabel11.Text = dsXML.Tables["enderEmit"].Rows[0]["xBairro"].ToString() + " - " + dsXML.Tables["enderEmit"].Rows[0]["CEP"].ToString();
                                if (dsXML.Tables["enderEmit"].Columns["xMun"] != null)
                                {
                                    xrLabel11.Text = dsXML.Tables["enderEmit"].Rows[0]["xBairro"].ToString() + " - " + dsXML.Tables["enderEmit"].Rows[0]["CEP"].ToString() + " - " + dsXML.Tables["enderEmit"].Rows[0]["xMun"].ToString();
                                    if (dsXML.Tables["enderEmit"].Columns["UF"] != null)
                                    {
                                        xrLabel11.Text = dsXML.Tables["enderEmit"].Rows[0]["xBairro"].ToString() + " - " + dsXML.Tables["enderEmit"].Rows[0]["CEP"].ToString() + " - " + dsXML.Tables["enderEmit"].Rows[0]["xMun"].ToString() + " - " + dsXML.Tables["enderEmit"].Rows[0]["UF"].ToString();
                                        if (dsXML.Tables["enderEmit"].Columns["fone"] != null)
                                        {
                                            xrLabel11.Text = dsXML.Tables["enderEmit"].Rows[0]["xBairro"].ToString() + " - " + dsXML.Tables["enderEmit"].Rows[0]["CEP"].ToString() + " - " + dsXML.Tables["enderEmit"].Rows[0]["xMun"].ToString() + " - " + dsXML.Tables["enderEmit"].Rows[0]["UF"].ToString() + " - " + string.Format("{0:(##) ####-####}", Convert.ToUInt64(dsXML.Tables["enderEmit"].Rows[0]["fone"].ToString())); ;
                                        }
                                    }
                                }
                            }
                        }
                        if (dsXML.Tables["emit"].Columns["CNPJ"] != null)
                        {
                            xrLabel12.Text = String.Format(@"CNPJ/CPF: {0:00\.000\.000\/0000\-00}", Convert.ToUInt64(dsXML.Tables["emit"].Rows[0]["CNPJ"].ToString()));
                            if (dsXML.Tables["emit"].Columns["IE"] != null)
                            {
                                xrLabel12.Text = String.Format(@"CNPJ/CPF: {0:00\.000\.000\/0000\-00}    Insc. Estadual: {1}", Convert.ToUInt64(dsXML.Tables["emit"].Rows[0]["CNPJ"].ToString()), dsXML.Tables["emit"].Rows[0]["IE"].ToString());
                            }
                        }
                        if (dsXML.Tables["dest"] != null)
                        {
                            if (dsXML.Tables["dest"].Columns["ISUF"] != null)
                            {
                                xrLabel27.Text = dsXML.Tables["dest"].Rows[0]["ISUF"].ToString();
                            }
                        }
                        if (dsXML.Tables["infCte"] != null)
                        {
                            if (dsXML.Tables["infCte"].Columns["Id"] != null)
                            {
                                xrLabel34.Text = string.Format(@"{0:00\.0000\.00\.000\.000\/0000-}", Convert.ToUInt64(dsXML.Tables["infCte"].Rows[0]["Id"].ToString().Remove(0, 3).Substring(0, 18)));
                                xrLabel34.Text = xrLabel34.Text + string.Format(@"{0:00-00-000-000\.000\.000\.00}", Convert.ToUInt64(dsXML.Tables["infCte"].Rows[0]["Id"].ToString().Remove(0, 3).Substring(18, 18)));
                                xrLabel34.Text = xrLabel34.Text + string.Format(@"{0:0\.000\.000-0}", Convert.ToUInt64(dsXML.Tables["infCte"].Rows[0]["Id"].ToString().Remove(0, 3).Substring(36, 8)));
                            }
                        }
                        if (dsXML.Tables["toma03"] != null)
                        {
                            switch (dsXML.Tables["toma03"].Rows[0]["toma"].ToString())
                            {
                                case "0":
                                    xrLabel35.Text = "Remetente";
                                    break;
                                case "1":
                                    xrLabel35.Text = "Expedidor";
                                    break;
                                case "2":
                                    xrLabel35.Text = "Recebedor";
                                    break;
                                case "3":
                                    xrLabel35.Text = "Destinatário";
                                    break;
                                default:
                                    xrLabel35.Text = "Outro";
                                    break;
                            }
                        }
                        if (dsXML.Tables["toma4"] != null)
                        {
                            switch (dsXML.Tables["toma4"].Rows[0]["toma"].ToString())
                            {
                                case "0":
                                    xrLabel35.Text = "Remetente";
                                    break;
                                case "1":
                                    xrLabel35.Text = "Expedidor";
                                    break;
                                case "2":
                                    xrLabel35.Text = "Recebedor";
                                    break;
                                case "3":
                                    xrLabel35.Text = "Destinatário";
                                    break;
                                default:
                                    xrLabel35.Text = "Outro";
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            #region rem
            if (dsXML.Tables["rem"] != null)
            {
                if (dsXML.Tables["rem"].Columns["xNome"] != null)
                {
                    xrLabel49.Text = dsXML.Tables["rem"].Rows[0]["xNome"].ToString();
                }
                if (dsXML.Tables["rem"].Columns["fone"] != null)
                {
                    xrLabel62.Text = string.Format("{0:(##) ####-####}", Convert.ToUInt64(dsXML.Tables["rem"].Rows[0]["fone"].ToString()));
                }
                if (dsXML.Tables["rem"].Columns["CNPJ"] != null)
                {
                    xrLabel56.Text = string.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToUInt64(dsXML.Tables["rem"].Rows[0]["CNPJ"].ToString()));
                }
                if (dsXML.Tables["rem"].Columns["IE"] != null)
                {
                    xrLabel60.Text = dsXML.Tables["rem"].Rows[0]["IE"].ToString();
                }
            }
            #endregion
            if (dsXML.Tables["ide"].Columns["CFOP"] != null)
            {
                xrLabel41.Text = dsXML.Tables["ide"].Rows[0]["CFOP"].ToString();
                if (dsXML.Tables["ide"].Columns["natOp"] != null)
                {
                    xrLabel41.Text = xrLabel41.Text + " - " + dsXML.Tables["ide"].Rows[0]["natOp"].ToString();
                }
            }
            if (dsXML.Tables["ide"].Columns["xMunIni"] != null)
            {
                xrLabel44.Text = dsXML.Tables["ide"].Rows[0]["xMunIni"].ToString();
                if (dsXML.Tables["ide"].Columns["UFIni"] != null)
                {
                    xrLabel44.Text = xrLabel44.Text + " - " + dsXML.Tables["ide"].Rows[0]["UFIni"].ToString();
                }
            }
            if (dsXML.Tables["ide"].Columns["xMunFim"] != null)
            {
                xrLabel47.Text = dsXML.Tables["ide"].Rows[0]["xMunFim"].ToString();
                if (dsXML.Tables["ide"].Columns["UFFim"] != null)
                {
                    xrLabel47.Text = xrLabel47.Text + " - " + dsXML.Tables["ide"].Rows[0]["UFFim"].ToString();
                }
            }
            if (dsXML.Tables["dest"] != null)
            {
                if (dsXML.Tables["dest"].Columns["xNome"] != null)
                {
                    xrLabel76.Text = dsXML.Tables["dest"].Rows[0]["xNome"].ToString();
                }
                if (dsXML.Tables["dest"].Columns["fone"] != null)
                {
                    xrLabel63.Text = string.Format("{0:(##) ####-####}", Convert.ToUInt64(dsXML.Tables["dest"].Rows[0]["fone"].ToString()));
                }
                if (dsXML.Tables["dest"].Columns["CNPJ"] != null)
                {
                    xrLabel70.Text = string.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToUInt64(dsXML.Tables["dest"].Rows[0]["CNPJ"].ToString()));
                }
                if (dsXML.Tables["dest"].Columns["IE"] != null)
                {
                    xrLabel66.Text = dsXML.Tables["dest"].Rows[0]["IE"].ToString();
                }
            }
            #region infProt
            if (dsXML.Tables["infProt"] != null)
            {
                if (dsXML.Tables["infProt"].Columns["nProt"] != null)
                {
                    dsXML.Tables["infProt"].Rows[0]["nProt"].ToString();
                    if (dsXML.Tables["infProt"].Columns["dhRecbto"] != null)
                    {
                        xrLabel43.Text = dsXML.Tables["infProt"].Rows[0]["nProt"].ToString() + " - " + Convert.ToDateTime(dsXML.Tables["infProt"].Rows[0]["dhRecbto"].ToString().Replace("T", " ")).ToString();
                    }
                }
            }
            #endregion
            #region enderReme
            if (dsXML.Tables["enderReme"] != null)
            {
                if (dsXML.Tables["enderReme"].Columns["xLgr"] != null)
                {
                    xrLabel51.Text = dsXML.Tables["enderReme"].Rows[0]["xLgr"].ToString();
                    if (dsXML.Tables["enderReme"].Columns["xLgr"] != null)
                    {
                        xrLabel51.Text = dsXML.Tables["enderReme"].Rows[0]["xLgr"].ToString() + ", " + dsXML.Tables["enderReme"].Rows[0]["nro"].ToString();
                    }
                }
                if (dsXML.Tables["enderReme"].Columns["xBairro"] != null)
                {
                    xrLabel53.Text = dsXML.Tables["enderReme"].Rows[0]["xBairro"].ToString();
                }
                if (dsXML.Tables["enderReme"].Columns["xMun"] != null)
                {
                    xrLabel54.Text = dsXML.Tables["enderReme"].Rows[0]["xMun"].ToString();
                    if (dsXML.Tables["enderReme"].Columns["UF"] != null)
                    {
                        xrLabel54.Text = dsXML.Tables["enderReme"].Rows[0]["xMun"].ToString() + " - " + dsXML.Tables["enderReme"].Rows[0]["UF"].ToString();
                        if (dsXML.Tables["enderReme"].Columns["CEP"] != null)
                        {
                            xrLabel54.Text = dsXML.Tables["enderReme"].Rows[0]["xMun"].ToString() + " - " + dsXML.Tables["enderReme"].Rows[0]["UF"].ToString() + "      CEP  " + string.Format("{0:00000-000}", Convert.ToInt32(dsXML.Tables["enderReme"].Rows[0]["CEP"].ToString()));
                        }
                    }
                }
                if (dsXML.Tables["enderReme"].Columns["xPais"] != null)
                {
                    xrLabel58.Text = dsXML.Tables["enderReme"].Rows[0]["xPais"].ToString();
                }
                else
                {
                    xrLabel58.Text = "BRASIL";
                }
            }
            #endregion
            #region enderExped
            if (dsXML.Tables["enderExped"] != null)
            {
                if (dsXML.Tables["enderExped"].Columns["xLgr"] != null)
                {
                    xrLabel90.Text = dsXML.Tables["enderExped"].Rows[0]["xLgr"].ToString();
                    if (dsXML.Tables["enderExped"].Columns["nro"] != null)
                    {
                        xrLabel90.Text = dsXML.Tables["enderExped"].Rows[0]["xLgr"].ToString() + ", " + dsXML.Tables["enderExped"].Rows[0]["nro"].ToString();
                    }
                }
                if (dsXML.Tables["enderExped"].Columns["xBairro"] != null)
                {
                    xrLabel88.Text = dsXML.Tables["enderExped"].Rows[0]["xBairro"].ToString();
                }
                if (dsXML.Tables["enderExped"].Columns["xMun"] != null)
                {
                    xrLabel87.Text = dsXML.Tables["enderExped"].Rows[0]["xMun"].ToString();
                    if (dsXML.Tables["enderExped"].Columns["UF"] != null)
                    {
                        xrLabel87.Text = dsXML.Tables["enderExped"].Rows[0]["xMun"].ToString() + " - " + dsXML.Tables["enderExped"].Rows[0]["UF"].ToString();
                        if (dsXML.Tables["enderExped"].Columns["CEP"] != null)
                        {
                            xrLabel87.Text = dsXML.Tables["enderExped"].Rows[0]["xMun"].ToString() + " - " + dsXML.Tables["enderExped"].Rows[0]["UF"].ToString() + "      CEP  " + string.Format("{0:00000-000}", Convert.ToInt32(dsXML.Tables["enderExped"].Rows[0]["CEP"].ToString()));
                        }
                    }
                }
                if (dsXML.Tables["enderExped"].Columns["xPais"] != null)
                {
                    xrLabel83.Text = dsXML.Tables["enderExped"].Rows[0]["xPais"].ToString();
                }
                else
                {
                    xrLabel83.Text = "BRASIL";
                }
            }
            #endregion
            #region enderDest
            if (dsXML.Tables["enderDest"] != null)
            {
                if (dsXML.Tables["enderDest"].Columns["xLgr"] != null)
                {
                    xrLabel73.Text = dsXML.Tables["enderDest"].Rows[0]["xLgr"].ToString();
                    if (dsXML.Tables["enderDest"].Columns["nro"] != null)
                    {
                        xrLabel73.Text = dsXML.Tables["enderDest"].Rows[0]["xLgr"].ToString() + ", " + dsXML.Tables["enderDest"].Rows[0]["nro"].ToString();
                    }
                }
                if (dsXML.Tables["enderDest"].Columns["xBairro"] != null)
                {
                    xrLabel75.Text = dsXML.Tables["enderDest"].Rows[0]["xBairro"].ToString();
                }
                if (dsXML.Tables["enderDest"].Columns["xMun"] != null)
                {
                    xrLabel72.Text = dsXML.Tables["enderDest"].Rows[0]["xMun"].ToString();
                    if (dsXML.Tables["enderDest"].Columns["UF"] != null)
                    {
                        xrLabel72.Text = dsXML.Tables["enderDest"].Rows[0]["xMun"].ToString() + " - " + dsXML.Tables["enderDest"].Rows[0]["UF"].ToString();
                        if (dsXML.Tables["enderDest"].Columns["CEP"] != null)
                        {
                            xrLabel72.Text = dsXML.Tables["enderDest"].Rows[0]["xMun"].ToString() + " - " + dsXML.Tables["enderDest"].Rows[0]["UF"].ToString() + "      CEP  " + string.Format("{0:00000-000}", Convert.ToInt32(dsXML.Tables["enderDest"].Rows[0]["CEP"].ToString()));
                        }
                    }
                }
                if (dsXML.Tables["enderDest"].Columns["xPais"] != null)
                {
                    xrLabel68.Text = dsXML.Tables["enderDest"].Rows[0]["xPais"].ToString();
                }
                else
                {
                    xrLabel68.Text = "BRASIL";
                }
            }
            #endregion
            #region enderReceb
            if (dsXML.Tables["enderReceb"] != null)
            {
                if (dsXML.Tables["enderReceb"].Columns["xLgr"] != null)
                {
                    xrLabel105.Text = dsXML.Tables["enderReceb"].Rows[0]["xLgr"].ToString();
                    if (dsXML.Tables["enderReceb"].Columns["nro"] != null)
                    {
                        xrLabel105.Text = dsXML.Tables["enderReceb"].Rows[0]["xLgr"].ToString() + ", " + dsXML.Tables["enderReceb"].Rows[0]["nro"].ToString();
                    }
                }
                if (dsXML.Tables["enderReceb"].Columns["xBairro"] != null)
                {
                    xrLabel103.Text = dsXML.Tables["enderReceb"].Rows[0]["xBairro"].ToString();
                }
                if (dsXML.Tables["enderReceb"].Columns["xMun"] != null)
                {
                    xrLabel102.Text = dsXML.Tables["enderReceb"].Rows[0]["xMun"].ToString();
                    if (dsXML.Tables["enderReceb"].Columns["UF"] != null)
                    {
                        xrLabel102.Text = dsXML.Tables["enderReceb"].Rows[0]["xMun"].ToString() + " - " + dsXML.Tables["enderReceb"].Rows[0]["UF"].ToString();
                        if (dsXML.Tables["enderReceb"].Columns["CEP"] != null)
                        {
                            xrLabel102.Text = dsXML.Tables["enderReceb"].Rows[0]["xMun"].ToString() + " - " + dsXML.Tables["enderReceb"].Rows[0]["UF"].ToString() + "      CEP  " + string.Format("{0:00000-000}", Convert.ToInt32(dsXML.Tables["enderReceb"].Rows[0]["CEP"].ToString()));
                        }
                    }
                }
                if (dsXML.Tables["enderReceb"].Columns["xPais"] != null)
                {
                    xrLabel98.Text = dsXML.Tables["enderReceb"].Rows[0]["xPais"].ToString();
                }
                else
                {
                    xrLabel98.Text = "BRASIL";
                }
            }
            #endregion
            if (dsXML.Tables["toma03"] != null)
            {
                switch (dsXML.Tables["toma03"].Rows[0]["toma"].ToString())
                {
                    case "0":
                        xrLabel109.Text = dsXML.Tables["rem"].Rows[0]["xNome"].ToString();
                        xrLabel110.Text = dsXML.Tables["enderReme"].Rows[0]["xMun"].ToString();
                        xrLabel113.Text = string.Format("{0:00000-000}", Convert.ToInt32(dsXML.Tables["enderReme"].Rows[0]["CEP"].ToString()));
                        xrLabel115.Text = dsXML.Tables["enderReme"].Rows[0]["xLgr"].ToString() + ", " + dsXML.Tables["enderReme"].Rows[0]["nro"].ToString() + " - " + dsXML.Tables["enderReme"].Rows[0]["xBairro"].ToString();
                        xrLabel117.Text = string.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToUInt64(dsXML.Tables["rem"].Rows[0]["CNPJ"].ToString()));
                        xrLabel118.Text = dsXML.Tables["rem"].Rows[0]["IE"].ToString();

                        if (dsXML.Tables["enderReme"].Columns["xPais"] != null)
                        {
                            xrLabel120.Text = dsXML.Tables["enderReme"].Rows[0]["xPais"].ToString();
                        }
                        else
                        {
                            xrLabel120.Text = "BRASIL";
                        }
                        if (dsXML.Tables["rem"].Columns["fone"] != null)
                        {
                            xrLabel123.Text = string.Format("{0:(##) ####-####}", Convert.ToUInt64(dsXML.Tables["rem"].Rows[0]["fone"].ToString()));
                        }

                        break;
                    case "1":
                        xrLabel109.Text = dsXML.Tables["exped"].Rows[0]["xNome"].ToString();
                        xrLabel110.Text = dsXML.Tables["enderExped"].Rows[0]["xMun"].ToString();
                        xrLabel113.Text = string.Format("{0:00000-000}", Convert.ToInt32(dsXML.Tables["enderExped"].Rows[0]["CEP"].ToString()));
                        xrLabel115.Text = dsXML.Tables["enderExped"].Rows[0]["xLgr"].ToString() + ", " + dsXML.Tables["enderExped"].Rows[0]["nro"].ToString() + " - " + dsXML.Tables["enderExped"].Rows[0]["xBairro"].ToString();
                        xrLabel117.Text = string.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToUInt64(dsXML.Tables["exped"].Rows[0]["CNPJ"].ToString()));
                        xrLabel118.Text = dsXML.Tables["exped"].Rows[0]["IE"].ToString();

                        if (dsXML.Tables["enderExped"].Columns["xPais"] != null)
                        {
                            xrLabel120.Text = dsXML.Tables["enderExped"].Rows[0]["xPais"].ToString();
                        }
                        else
                        {
                            xrLabel120.Text = "BRASIL";
                        }
                        if (dsXML.Tables["exped"].Columns["fone"] != null)
                        {
                            xrLabel123.Text = string.Format("{0:(##) ####-####}", Convert.ToUInt64(dsXML.Tables["exped"].Rows[0]["fone"].ToString()));
                        }
                        break;
                    case "2":
                         xrLabel109.Text = dsXML.Tables["receb"].Rows[0]["xNome"].ToString();
                        xrLabel110.Text = dsXML.Tables["enderReceb"].Rows[0]["xMun"].ToString();
                        xrLabel113.Text = string.Format("{0:00000-000}", Convert.ToInt32(dsXML.Tables["enderReceb"].Rows[0]["CEP"].ToString()));
                        xrLabel115.Text = dsXML.Tables["enderReceb"].Rows[0]["xLgr"].ToString() + ", " + dsXML.Tables["enderReceb"].Rows[0]["nro"].ToString() + " - " + dsXML.Tables["enderReceb"].Rows[0]["xBairro"].ToString();
                        xrLabel117.Text = string.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToUInt64(dsXML.Tables["receb"].Rows[0]["CNPJ"].ToString()));
                        xrLabel118.Text = dsXML.Tables["receb"].Rows[0]["IE"].ToString();

                        if (dsXML.Tables["enderReceb"].Columns["xPais"] != null)
                        {
                            xrLabel120.Text = dsXML.Tables["enderReceb"].Rows[0]["xPais"].ToString();
                        }
                        else
                        {
                            xrLabel120.Text = "BRASIL";
                        }
                        if (dsXML.Tables["receb"].Columns["fone"] != null)
                        {
                            xrLabel123.Text = string.Format("{0:(##) ####-####}", Convert.ToUInt64(dsXML.Tables["receb"].Rows[0]["fone"].ToString()));
                        }
                        break;
                    case "3":
                         xrLabel109.Text = dsXML.Tables["dest"].Rows[0]["xNome"].ToString();
                        xrLabel110.Text = dsXML.Tables["enderDest"].Rows[0]["xMun"].ToString();
                        xrLabel113.Text = string.Format("{0:00000-000}", Convert.ToInt32(dsXML.Tables["enderDest"].Rows[0]["CEP"].ToString()));
                        xrLabel115.Text = dsXML.Tables["enderDest"].Rows[0]["xLgr"].ToString() + ", " + dsXML.Tables["enderDest"].Rows[0]["nro"].ToString() + " - " + dsXML.Tables["enderDest"].Rows[0]["xBairro"].ToString();
                        xrLabel117.Text = string.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToUInt64(dsXML.Tables["dest"].Rows[0]["CNPJ"].ToString()));
                        xrLabel118.Text = dsXML.Tables["dest"].Rows[0]["IE"].ToString();

                        if (dsXML.Tables["enderDest"].Columns["xPais"] != null)
                        {
                            xrLabel120.Text = dsXML.Tables["enderDest"].Rows[0]["xPais"].ToString();
                        }
                        else
                        {
                            xrLabel120.Text = "BRASIL";
                        }
                        if (dsXML.Tables["dest"].Columns["fone"] != null)
                        {
                            xrLabel123.Text = string.Format("{0:(##) ####-####}", Convert.ToUInt64(dsXML.Tables["dest"].Rows[0]["fone"].ToString()));
                        }
                        break;
                    default:
                        break;
                }
            }
            if (dsXML.Tables["toma4"] != null)
            {
                xrLabel109.Text = dsXML.Tables["toma4"].Rows[0]["xNome"].ToString();
                xrLabel117.Text = string.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToUInt64(dsXML.Tables["toma4"].Rows[0]["CNPJ"].ToString()));
                xrLabel118.Text = dsXML.Tables["toma4"].Rows[0]["IE"].ToString();
                xrLabel123.Text = string.Format("{0:(##) ####-####}", Convert.ToUInt64(dsXML.Tables["toma4"].Rows[0]["fone"].ToString()));
                if (dsXML.Tables["enderToma"].Columns["xPais"] != null)
                {
                    xrLabel120.Text = dsXML.Tables["enderToma"].Rows[0]["xPais"].ToString();
                }
                else
                {
                    xrLabel120.Text = "BRASIL";
                }
                xrLabel110.Text = dsXML.Tables["enderToma"].Rows[0]["xMun"].ToString();
                xrLabel113.Text = string.Format("{0:00000-000}", Convert.ToInt32(dsXML.Tables["enderToma"].Rows[0]["CEP"].ToString()));
                if (dsXML.Tables["enderToma"].Columns["xLgr"] != null)
                {
                    xrLabel115.Text = dsXML.Tables["enderToma"].Rows[0]["xLgr"].ToString();
                    if (dsXML.Tables["enderToma"].Columns["nro"] != null)
                    {
                        xrLabel115.Text = dsXML.Tables["enderToma"].Rows[0]["xLgr"].ToString() + ", " + dsXML.Tables["enderToma"].Rows[0]["nro"].ToString();
                    }
                    if (dsXML.Tables["enderToma"].Columns["xBairro"] != null)
                    {
                        xrLabel115.Text = dsXML.Tables["enderToma"].Rows[0]["xLgr"].ToString() + ", " + dsXML.Tables["enderToma"].Rows[0]["nro"].ToString() + " - " + dsXML.Tables["enderToma"].Rows[0]["xBairro"].ToString();
                    }
                }
            }
            #region infCarga
            if (dsXML.Tables["infCarga"] != null)
            {
                if (dsXML.Tables["infCarga"].Columns["proPred"] != null)
                {
                    xrLabel139.Text = dsXML.Tables["infCarga"].Rows[0]["proPred"].ToString();
                }
                if (dsXML.Tables["infCarga"].Columns["xOutCat"] != null)
                {
                    xrLabel125.Text = dsXML.Tables["infCarga"].Rows[0]["xOutCat"].ToString();
                }
                if (dsXML.Tables["infCarga"].Columns["vCarga"] != null)
                {
                    xrLabel126.Text = Convert.ToDecimal(dsXML.Tables["infCarga"].Rows[0]["vCarga"].ToString().Replace(".", ",")).ToString();
                }
            }
            #endregion
            #region infQ
            if (dsXML.Tables["infQ"] != null)
            {
                ArrayList lista = new ArrayList();
                for (int i = 0; i < dsXML.Tables["infQ"].Rows.Count; i++)
                {
                    if (i.Equals(0))
                    {
                        if (dsXML.Tables["infQ"].Columns["tpMed"] != null)
                        {
                            xrLabel129.Text = dsXML.Tables["infQ"].Rows[i]["tpMed"].ToString();
                        }
                        if (dsXML.Tables["infQ"].Columns["qCarga"] != null)
                        {
                            xrLabel130.Text = dsXML.Tables["infQ"].Rows[i]["qCarga"].ToString() + "KG";
                        }

                    }
                    if (i.Equals(1))
                    {
                        if (dsXML.Tables["infQ"].Columns["tpMed"] != null)
                        {
                            xrLabel133.Text = dsXML.Tables["infQ"].Rows[i]["tpMed"].ToString();
                        }
                        if (dsXML.Tables["infQ"].Columns["qCarga"] != null)
                        {
                            int tpMed = dsXML.Tables["infQ"].Rows[i]["tpMed"].ToString().IndexOf("VOLUME");

                            if (!tpMed.Equals(-1))
                            {
                                xrLabel131.Text = dsXML.Tables["infQ"].Rows[i]["qCarga"].ToString() + "UN";
                            }
                            else
                            {
                                xrLabel131.Text = dsXML.Tables["infQ"].Rows[i]["qCarga"].ToString() + "KG";
                            }

                        }
                    }
                    if (i.Equals(2))
                    {
                        if (dsXML.Tables["infQ"].Columns["tpMed"] != null)
                        {
                            xrLabel136.Text = dsXML.Tables["infQ"].Rows[i]["tpMed"].ToString();
                        }
                        if (dsXML.Tables["infQ"].Columns["qCarga"] != null)
                        {
                            xrLabel134.Text = dsXML.Tables["infQ"].Rows[i]["qCarga"].ToString() + "KG";
                        }
                    }
                    if (dsXML.Tables["infQ"].Columns["tpMed"] != null)
                    {
                        if (dsXML.Tables["infQ"].Rows[i]["tpMed"].ToString().Equals("Volume"))
                        {
                            xrLabel142.Text = dsXML.Tables["infQ"].Rows[i]["qCarga"].ToString();
                        }
                    }
                }
            }
            #endregion
            #region seg
            if (dsXML.Tables["seg"] != null)
            {
                if (dsXML.Tables["seg"].Columns["xSeg"] != null)
                {
                    xrLabel146.Text = dsXML.Tables["seg"].Rows[0]["xSeg"].ToString();
                }
                if (dsXML.Tables["seg"].Columns["respSeg"] != null)
                {
                    switch (dsXML.Tables["seg"].Rows[0]["respSeg"].ToString())
                    {
                        case "0":
                            xrLabel148.Text = "Remetente";
                            break;
                        case "1":
                            xrLabel148.Text = "Expedidor";
                            break;
                        case "2":
                            xrLabel148.Text = "Recebedor";
                            break;
                        case "3":
                            xrLabel148.Text = "Destinatário";
                            break;
                        case "4":
                            xrLabel148.Text = "Emitente do CT-e";
                            break;
                        default:
                            xrLabel148.Text = "Tomador de Serviço";
                            break;
                    }
                }
                if (dsXML.Tables["seg"].Columns["nApol"] != null)
                {
                    xrLabel150.Text = dsXML.Tables["seg"].Rows[0]["nApol"].ToString();
                }
                if (dsXML.Tables["seg"].Columns["nAver"] != null)
                {
                    xrLabel152.Text = dsXML.Tables["seg"].Rows[0]["nAver"].ToString();
                }
            }
            #endregion

        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void DetailReport_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            if (dsXML.Tables["infCteComp"] != null)
            {
                item item = new item();
                ArrayList lista = new ArrayList();
                for (int i = 0; i < dsXML.Tables["infCteComp"].Rows.Count; i++)
                {
                    item.chave = dsXML.Tables["infCteComp"].Rows[i]["chave"].ToString();
                    item.vRec = Convert.ToDecimal(dsXML.Tables["vPrest"].Rows[i]["vRec"].ToString().Replace(".", ","));
                    lista.Add(item);
                    item = new item();
                }
                DetailReport.DataSource = lista;
                xrLabel191.DataBindings.Add("Text", null, "chave");
                xrLabel198.DataBindings.Add("Text", null, "vRec");
            }
            else
            {
                xrLabel191.Visible = false;
                xrLabel192.Visible = false;
                xrLabel194.Visible = false;
                xrLabel195.Visible = false;
                xrLabel196.Visible = false;
                xrLabel197.Visible = false;
                xrLabel198.Visible = false;
                //xrCrossBandLine2.Visible = false;
                xrCrossBandBox1.Visible = false;
                
            }
            #region ICMS
            if (dsXML.Tables["ICMS00"] != null)
            {
                xrLabel157.Text = "Classificação Tributária do Serviço";
                xrLabel160.Text = "Valor da BC do ICMS";
                xrLabel162.Text = "Alíquota do ICMS";
                xrLabel164.Text = "Valor do ICMS";
                xrLabel158.Text = dsXML.Tables["ICMS00"].Rows[0]["CST"].ToString() + " - Tributação normal ICMS";
                xrLabel159.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["ICMS00"].Rows[0]["vBC"].ToString().Replace(".", ",")).ToString());
                xrLabel161.Text = string.Format("{0:0.00}", Convert.ToDecimal(dsXML.Tables["ICMS00"].Rows[0]["pICMS"].ToString().Replace(".", ",")));
                xrLabel163.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["ICMS00"].Rows[0]["vICMS"].ToString().Replace(".", ",")).ToString());

            }
            else if (dsXML.Tables["ICMS20"] != null)
            {
                xrLabel157.Text = "Classificação Tributária do Serviço";
                xrLabel160.Text = "Valor da BC do ICMS";
                xrLabel162.Text = "Alíquota do ICMS";
                xrLabel164.Text = "Valor do ICMS";
                xrLabel166.Text = "Percentual de redução da BC";
                xrLabel158.Text = dsXML.Tables["ICMS20"].Rows[0]["CST"].ToString() + " - tributação com BC reduzida do ICMS";
                xrLabel159.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["ICMS20"].Rows[0]["vBC"].ToString().Replace(".", ",")).ToString());
                xrLabel161.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["ICMS20"].Rows[0]["pICMS"].ToString().Replace(".", ",")).ToString());
                xrLabel163.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["ICMS20"].Rows[0]["vICMS"].ToString().Replace(".", ",")).ToString());
                xrLabel165.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["ICMS20"].Rows[0]["pRedBC"].ToString().Replace(".", ",")).ToString());
            }
            else if (dsXML.Tables["ICMS45"] != null)
            {
                xrLabel157.Text = "Classificação Tributária do Serviço";

                if (dsXML.Tables["ICMS45"].Rows[0]["CST"].ToString().Equals("40"))
                {
                    xrLabel158.Text = dsXML.Tables["ICMS45"].Rows[0]["CST"].ToString() + " - ICMS isenção";
                }
                else if (dsXML.Tables["ICMS45"].Rows[0]["CST"].ToString().Equals("41"))
                {
                    xrLabel158.Text = dsXML.Tables["ICMS45"].Rows[0]["CST"].ToString() + " - ICMS não tributada";
                }
                else if (dsXML.Tables["ICMS45"].Rows[0]["CST"].ToString().Equals("51"))
                {
                    xrLabel158.Text = dsXML.Tables["ICMS45"].Rows[0]["CST"].ToString() + " - ICMS diferido";
                }

            }
            else if (dsXML.Tables["ICMS60"] != null)
            {
                xrLabel157.Text = "Classificação Tributária do Serviço";
                xrLabel162.Text = "Alíquota do ICMS";
                xrLabel164.Text = "Valor do Crédito outorgado/Presumido";
                xrLabel166.Text = "Valor da BC do ICMS ST retido";
                xrLabel168.Text = "Valor do ICMS ST retido";
                xrLabel158.Text = dsXML.Tables["ICMS60"].Rows[0]["CST"].ToString() + " - ICMS cobrado anteriormente por substituição tributária";
                xrLabel161.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["ICMS60"].Rows[0]["pICMSSTRet"].ToString().Replace(".", ",")).ToString());
                xrLabel163.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["ICMS60"].Rows[0]["vCred"].ToString().Replace(".", ",")).ToString());
                xrLabel165.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["ICMS60"].Rows[0]["vBCSTRet"].ToString().Replace(".", ",")).ToString());
                xrLabel167.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["ICMS60"].Rows[0]["vICMSSTRet"].ToString().Replace(".", ",")).ToString());
            }
            else if (dsXML.Tables["ICMS90"] != null)
            {
                xrLabel157.Text = "Classificação Tributária do Serviço";
                xrLabel160.Text = "Valor da BC do ICMS";
                xrLabel162.Text = "Alíquota do ICMS";
                xrLabel164.Text = "Valor do ICMS";
                xrLabel166.Text = "Percentual de redução da BC";
                xrLabel168.Text = "Valor do Crédito Outorgado/Presumido";
                xrLabel158.Text = dsXML.Tables["ICMS90"].Rows[0]["CST"].ToString() + " - ICMS Outros";
                xrLabel159.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["ICMS90"].Rows[0]["vBC"].ToString().Replace(".", ",")).ToString());
                xrLabel161.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["ICMS90"].Rows[0]["pICMS"].ToString().Replace(".", ",")).ToString());
                xrLabel163.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["ICMS90"].Rows[0]["vICMS"].ToString().Replace(".", ",")).ToString());
                xrLabel165.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["ICMS90"].Rows[0]["pRedBC"].ToString().Replace(".", ",")).ToString());
                xrLabel167.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["ICMS90"].Rows[0]["vCred"].ToString().Replace(".", ",")).ToString());
            }
            else if (dsXML.Tables["ICMSOutraUF"] != null)
            {
                xrLabel157.Text = "Classificação Tributária do Serviço";
                xrLabel162.Text = "Percentual de redução da BC";
                xrLabel164.Text = "Valor da BC do ICMS";
                xrLabel166.Text = "Alíquota do ICMS";
                xrLabel168.Text = "Valor do ICMS devido outra UF";
                xrLabel158.Text = dsXML.Tables["ICMSOutraUF"].Rows[0]["CST"].ToString() + " - ICMS cobrado anteriormente por substituição tributária";
                xrLabel161.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["ICMSOutraUF"].Rows[0]["pRedBCOutraUF"].ToString().Replace(".", ",")).ToString());
                xrLabel163.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["ICMSOutraUF"].Rows[0]["vBCOutraUF"].ToString().Replace(".", ",")).ToString());
                xrLabel165.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["ICMSOutraUF"].Rows[0]["pICMSOutraUF"].ToString().Replace(".", ",")).ToString());
                xrLabel167.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["ICMSOutraUF"].Rows[0]["vICMSOutraUF"].ToString().Replace(".", ",")).ToString());

            }
            else if (dsXML.Tables["ICMSSN"] != null)
            {
                xrLabel157.Text = "Simples Nacional 1=Sim";
                xrLabel160.Text = "Valor Total dos Tributos";
                xrLabel162.Text = "Informações adicionais de interesse do Fisco";
                xrLabel158.Text = dsXML.Tables["ICMSSN"].Rows[0]["indSN"].ToString();
                xrLabel161.Text = string.Format("{0:n2}", Convert.ToDecimal(dsXML.Tables["ICMSSN"].Rows[0]["vTotTrib"].ToString().Replace(".", ",")).ToString());
                xrLabel163.Text = dsXML.Tables["ICMSSN"].Rows[0]["infAdFisco"].ToString();
            }
            #endregion
            #region vPrest
            if (dsXML.Tables["vPrest"] != null)
            {
                if (dsXML.Tables["vPrest"].Columns["vTPrest"] != null)
                {
                    xrLabel153.Text = Convert.ToDecimal(dsXML.Tables["vPrest"].Rows[0]["vTPrest"].ToString().Replace(".", ",")).ToString();
                }
                if (dsXML.Tables["vPrest"].Columns["vRec"] != null)
                {
                    xrLabel154.Text = Convert.ToDecimal(dsXML.Tables["vPrest"].Rows[0]["vRec"].ToString().Replace(".", ",")).ToString();
                }
            }
            #endregion
        }

        private void DetailReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            item item = new item();
            lista = new ArrayList();
            if (dsXML.Tables["infDoc"] != null)
            {
                if (dsXML.Tables["infNF"] != null)
                {
                    for (int i = 0; i < dsXML.Tables["infNF"].Rows.Count; i++)
                    {
                        item.tipoDoc = dsXML.Tables["infNF"].Rows[i]["mod"].ToString();
                        if (dsXML.Tables["rem"].Columns["CPF"] != null)
                        {
                            item.cpf_cnpj = dsXML.Tables["rem"].Rows[i]["CPF"].ToString();
                        }
                        else
                        {
                            item.cpf_cnpj = dsXML.Tables["rem"].Rows[i]["CNPJ"].ToString();
                        }
                        item.serie_numeroDoc = dsXML.Tables["infNF"].Rows[i]["serie"].ToString() + "/" + dsXML.Tables["infNF"].Rows[i]["nDoc"].ToString();
                        lista.Add(item);
                        item = new item();
                    }
                    DetailReport1.DataSource = lista;
                    xrLabel189.DataBindings.Add("Text", null, "tipoDoc");
                    xrLabel190.DataBindings.Add("Text", null, "cpf_cnpj");
                    xrLabel193.DataBindings.Add("Text", null, "serie_numeroDoc");
                }
                else if (dsXML.Tables["infNFe"] != null)
                {
                    for (int i = 0; i < dsXML.Tables["infNFe"].Rows.Count; i++)
                    {
                        item.tipoDoc = "NFE";
                        item.chave = dsXML.Tables["infNFe"].Rows[i]["chave"].ToString();
                        item.serie_numeroDoc = dsXML.Tables["infNFe"].Rows[i]["chave"].ToString().Remove(0, 22);
                        item.serie_numeroDoc = item.serie_numeroDoc.Substring(0, 12);
                        item.serie_numeroDoc = item.serie_numeroDoc.Insert(3, "/");
                        lista.Add(item);
                        item = new item();
                    }
                    DetailReport1.DataSource = lista;
                    xrLabel189.DataBindings.Add("Text", null, "tipoDoc");
                    xrLabel190.DataBindings.Add("Text", null, "chave");
                    xrLabel193.DataBindings.Add("Text", null, "serie_numeroDoc");
                }
            }
        }

        private void PageFooter_AfterPrint(object sender, EventArgs e)
        {
            if (lista.Count > 4)
            {
                PageFooter.Visible = false;
            }
        }

        private void PageFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (dsXML.Tables["compl"].Columns["xObs"] != null)
            {
                xrLabel186.Text = dsXML.Tables["compl"].Rows[0]["xObs"].ToString();
            }
            if (dsXML.Tables["rodo"] != null)
            {
                if (dsXML.Tables["rodo"].Columns["RNTRC"] != null)
                {
                    xrLabel174.Text = dsXML.Tables["rodo"].Rows[0]["RNTRC"].ToString();
                }
                if (dsXML.Tables["rodo"].Columns["CIOT"] != null)
                {
                    xrLabel175.Text = dsXML.Tables["rodo"].Rows[0]["CIOT"].ToString();
                }
                if (dsXML.Tables["rodo"].Columns["dPrev"] != null)
                {
                    xrLabel177.Text = Convert.ToDateTime(dsXML.Tables["rodo"].Rows[0]["dPrev"].ToString()).ToShortDateString();
                }

            }
        }

    }
}
