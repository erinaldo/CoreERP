using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartNFEstadual : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartNFEstadual()
        {
            this.TableName = "GNFESTADUAL";
            this.Keys = new string[] { "CODEMPRESA", "CODOPER" };
            this.FormEditName = "PSPartNFEstadualEdit";
            this.PSPartData = new PSPartNFEstadualData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("XMLREC", false));
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("XMLPROT", false));
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("XMLNFE", false));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CCODSTATUS",
                                                                    "",
                                                                    PS.Lib.DataGridColumnType.Image,
                                                                    1,
                                                                    "CODSTATUS",
                                                                    System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter,
                                                                    new PS.Lib.ImageProperties[] { 
                                                                        new PS.Lib.ImageProperties("P", "Pendente", Properties.Resources.nf_warning),
                                                                        new PS.Lib.ImageProperties("U", "Aguardando Processamento", Properties.Resources.nf_wait),
                                                                        new PS.Lib.ImageProperties("E", "Inconsistente", Properties.Resources.nf_erro),
                                                                        new PS.Lib.ImageProperties("A", "Autorizada", Properties.Resources.nf_ok),
                                                                        new PS.Lib.ImageProperties("C", "Cancelada", Properties.Resources.nf_cancel),
                                                                        new PS.Lib.ImageProperties("I", "Inutilizado", Properties.Resources.nf_inutilizado),
                                                                        new PS.Lib.ImageProperties("D", "Denegado", Properties.Resources.nf_denegado),
                                                                                }));

            this.PSPartApp.Add(new PSPartEnviarNFeApp());
            this.PSPartApp.Add(new PSPartConsultarAutorizacaoNFeApp());
            this.PSPartApp.Add(new PSPartImprimirDANFENFeApp());
            this.PSPartApp.Add(new PSPartEnviarXMLNFeApp());
            this.PSPartApp.Add(new PSPartExportarXMLNFeApp());
            this.PSPartApp.Add(new PSPartCancelarNFeApp());
            //this.PSPartApp.Add(new PSPartCartaCorrecaoNFeApp());
            this.PSPartApp.Add(new PSPartInutilizarNFeApp());
            this.PSPartApp.Add(new PSPartGerarBoletoApp(true));

            this.Folder.Add(new Lib.WinForms.Folder(new PSPartNFEstadualHistorico(), "CODEMPRESA", "CODOPER"));
            this.Folder.Add(new Lib.WinForms.Folder(new PSPartNFEstadualEvento(), "CODEMPRESA", "CODOPER"));


            this.f.GetProcessos().Add("Imprimir Boleto", null, ImprimirBoleto);

            this.AllowDelete = false;
            this.AllowInsert = false;
            this.AllowSave = false;

            this.SecurityID = "PSPartNFEstadual";
            this.ModuleID = "PG";
        }

        public void ImprimirBoleto(object sender, EventArgs e)
        {
            try
            {
                string CODLANCA = "( ";
                for (int i = 0; i < f.dataGridView1.SelectedRows.Count; i++)
                {
                    bool boleto = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT COUNT(*) FROM FBOLETO INNER JOIN FLANCA ON FBOLETO.CODLANCA = FLANCA.CODLANCA AND FBOLETO.CODEMPRESA = FLANCA.CODEMPRESA AND FBOLETO.CODFILIAL = FLANCA.CODFILIAL  WHERE FLANCA.CODOPER = ? AND FLANCA.CODEMPRESA = ?", new object[] { f.dataGridView1.SelectedRows[i].Cells["CODOPER"].Value.ToString(), AppLib.Context.Empresa }));

                    if (boleto == true)
                    {
                        string status = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODSTATUS FROM GNFESTADUAL WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { f.dataGridView1.SelectedRows[i].Cells["CODOPER"].Value.ToString(), AppLib.Context.Empresa }).ToString();
                        if (status == "A")
                        {
                            CODLANCA += f.dataGridView1.SelectedRows[i].Cells["CODOPER"].Value.ToString();
                            CODLANCA += ", ";
                        }
                    }
                }
                if (CODLANCA.Length > 2)
                {
                    CODLANCA = CODLANCA.Substring(0, CODLANCA.Length - 2);

                }
                CODLANCA += " )";
                if (CODLANCA != "(  )")
                {
                    Relatorios.XrBoletoBancario rel = new Relatorios.XrBoletoBancario(AppLib.Context.Empresa, CODLANCA);
                    new DevExpress.XtraReports.UI.ReportPrintTool(rel).ShowPreviewDialog();
                }
            }
            catch (Exception ex)
            {
                AppLib.Windows.FormMessageDefault.ShowError(ex.Message);
            }
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("IDOUTBOX", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DATAEMISSAO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("CODFILIAL", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODCLIFOR", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODTIPOPER", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODOPER", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NUMERO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("RECIBO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("PROTOCOLO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DATARECIBO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DATAPROTOCOLO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("CHAVEACESSO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("XMLREC", null, typeof(PS.Lib.WinForms.PSMemoBox)));
            objArr.Add(new DataField("XMLPROT", null, typeof(PS.Lib.WinForms.PSMemoBox)));
            objArr.Add(new DataField("XMLNFE", null, typeof(PS.Lib.WinForms.PSMemoBox)));
            objArr.Add(new DataField("DANFEIMPRESSA", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("EMAILENVIADO", null, typeof(PS.Lib.WinForms.PSCheckBox)));

            List<ComboBoxItem> Item1 = new List<ComboBoxItem>();
            Item1.Add(new ComboBoxItem("P", "Paisagem"));
            Item1.Add(new ComboBoxItem("R", "Retrato"));

            objArr.Add(new DataField("FORMATOIMPRESSAO", null, typeof(PS.Lib.WinForms.PSComboBox), Item1));

            List<ComboBoxItem> Item2 = new List<ComboBoxItem>();

            Item2.Add(new ComboBoxItem("P", "Pendente"));
            Item2.Add(new ComboBoxItem("U", "Aguardando Processamento"));
            Item2.Add(new ComboBoxItem("E", "Inconsistente"));
            Item2.Add(new ComboBoxItem("A", "Autorizada"));
            Item2.Add(new ComboBoxItem("C", "Cancelada"));
            Item2.Add(new ComboBoxItem("I", "Inutilizado"));
            Item2.Add(new ComboBoxItem("D", "Denegado"));

            objArr.Add(new DataField("CODSTATUS", null, typeof(PS.Lib.WinForms.PSComboBox), Item2));
            objArr.Add(new DataField("CODSERIE", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NOMEFANTASIA", null, typeof(PS.Lib.WinForms.PSTextoBox)));


            return objArr;
        }
    }
}
