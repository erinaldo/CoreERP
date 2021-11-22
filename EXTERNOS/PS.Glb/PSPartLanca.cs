using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartLanca : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartLanca()
        {
            this.TableName = "FLANCA";
            this.Keys = new string[] { "CODEMPRESA", "CODLANCA"};
            this.FormEditName = "PSPartLancaEdit";
            this.PSPartData = new PSPartLancaData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VLORIGINAL",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("PRDESCONTO",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VLDESCONTO",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("PRMULTA",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VLMULTA",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("PRJUROS",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VLJUROS",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VLLIQUIDO",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VLBAIXADO",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CCODSTATUS",
                                                                                "",
                                                                                PS.Lib.DataGridColumnType.Image,
                                                                                2,
                                                                                "CODSTATUS",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter,
                                                                                new PS.Lib.ImageProperties[] { new PS.Lib.ImageProperties(0, "Aberto", Properties.Resources.fin_aberto),
                                                                                                                    new PS.Lib.ImageProperties(1, "Baixado", Properties.Resources.fin_baixado),
                                                                                                                    new PS.Lib.ImageProperties(2, "Cancelado", Properties.Resources.fin_cancelado)}));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CTIPOPAGREC",
                                                                                "",
                                                                                PS.Lib.DataGridColumnType.Image,
                                                                                1,
                                                                                "TIPOPAGREC",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter,
                                                                                new PS.Lib.ImageProperties[] { new PS.Lib.ImageProperties(0, "Pagar", Properties.Resources.img_lanpag),
                                                                                                               new PS.Lib.ImageProperties(1, "Receber", Properties.Resources.img_lanrec)}));

            this.MasterDetail.Add(new PS.Lib.WinForms.MasterDetail(new PSPartLancaRateioCC(), "CODEMPRESA", "CODLANCA"));
            this.MasterDetail.Add(new PS.Lib.WinForms.MasterDetail(new PSPartLancaRateioDP(), "CODEMPRESA", "CODLANCA"));

            this.Folder.Add(new PS.Lib.WinForms.Folder(new PSPartLancaCompl(), "CODEMPRESA", "CODLANCA"));

            this.PSPartApp.Add(new PS.Glb.PSPartCancelaLancaApp());
            this.PSPartApp.Add(new PS.Glb.PSPartFaturaApp());
            this.PSPartApp.Add(new PS.Glb.PSPartBaixaLancaApp());
            this.PSPartApp.Add(new PS.Glb.PSPartCancelaBaixaApp());
            this.PSPartApp.Add(new PS.Glb.PSPartGerarBoletoApp());
            this.PSPartApp.Add(new PS.Glb.PSPartCancelarFaturaApp());




            this.PSPartApp.Add(new PS.Glb.PSPartVinculaLancaApp());

           


            this.PSPartApp.Add(new PSPartImprimirCopiaChequeApp());

            this.SecurityID = "PSPartLanca";
            this.ModuleID = "PG";

            this.f.GetAnexos().Add(new System.Windows.Forms.ToolStripSeparator());
            this.f.GetAnexos().Add("Imprimir Lançamento", null, ImprimirLancamento);
        }

        public override void CustomDataGridView(System.Windows.Forms.DataGridView DataGrid)
        {
            base.CustomDataGridView(DataGrid);

            for (int i = 0; i < DataGrid.Rows.Count; i++)
            {
                if (int.Parse(DataGrid.Rows[i].Cells["CODSTATUS"].Value.ToString()) == 0)
                {
                    if (Convert.ToDateTime(DataGrid.Rows[i].Cells["DATAVENCIMENTO"].Value.ToString()) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                    {
                        DataGrid.Rows[i].Cells["CCODSTATUS"].Value = Properties.Resources.fin_vencido;
                        DataGrid.Rows[i].Cells["CCODSTATUS"].ToolTipText = "Vencido";
                    }

                    if (Convert.ToDateTime(DataGrid.Rows[i].Cells["DATAVENCIMENTO"].Value.ToString()) == Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                    {
                        DataGrid.Rows[i].Cells["CCODSTATUS"].Value = Properties.Resources.fin_vencehoje;
                        DataGrid.Rows[i].Cells["CCODSTATUS"].ToolTipText = "Vence Hoje";
                    }
                }
            }
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODUSUARIOCRIACAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODUSUARIOCANCELAMENTO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODUSUARIOCANCELAMENTOBAIXA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DATACRIACAO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DATACANCELAMENTO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DATACANCELAMENTOBAIXA", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("MOTIVOCANCELAMENTO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("MOTIVOCANCELAMENTOBAIXA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            objArr.Add(new DataField("SEGUNDONUMERO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTIPDOC", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("DATAPREVBAIXA", null, typeof(PS.Lib.WinForms.PSDateBox)));

            List<ComboBoxItem> Item = new List<ComboBoxItem>();
            Item.Add(new ComboBoxItem(0, "Aberto"));
            Item.Add(new ComboBoxItem(1, "Baixado"));
            Item.Add(new ComboBoxItem(2, "Cancelado"));

            objArr.Add(new DataField("CODSTATUS", null, typeof(PS.Lib.WinForms.PSComboBox), Item));
            objArr.Add(new DataField("OBSERVACAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DATABAIXA", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DATAVENCIMENTO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DATAEMISSAO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("CODFILIAL", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODCLIFOR", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("NUMERO", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            List<ComboBoxItem> Item1 = new List<ComboBoxItem>();
            Item1.Add(new ComboBoxItem(0, "Pagar"));
            Item1.Add(new ComboBoxItem(1, "Receber"));

            objArr.Add(new DataField("TIPOPAGREC", null, typeof(PS.Lib.WinForms.PSComboBox), Item1));

            List<ComboBoxItem> Item2 = new List<ComboBoxItem>();
            Item2.Add(new ComboBoxItem("O", "Operação"));
            Item2.Add(new ComboBoxItem("F", "Financeiro"));

            objArr.Add(new DataField("ORIGEM", null, typeof(PS.Lib.WinForms.PSComboBox), Item2));

            objArr.Add(new DataField("CODLANCA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("VLBAIXADO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CODCONTA", null, typeof(PS.Lib.WinForms.PSLookup)));
            // objArr.Add(new DataField("VLLIQUIDO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("VLJUROS", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("PRJUROS", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("VLMULTA", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("PRMULTA", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("VLDESCONTO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("PRDESCONTO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("VLORIGINAL", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CODMOEDA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODFORMA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODDEPTO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODCCUSTO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODNATUREZAORCAMENTO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODOPER", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NFOUDUP", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CNOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("IDEXTRATO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("VLLIQUIDO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODREPRE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            return objArr;
        }

        public void ImprimirLancamento(object sender, EventArgs e)
        {
            // this.f.GetAnexos().Add("Imprimir Lançamento", null, ImprimirLancamento);

            String consultaZREPORT = @"SELECT IDREPORT, NOME FROM ZREPORT WHERE CODREPORTTIPO = 'LANCA'";
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaZREPORT, new Object[] { });

            if (dt.Rows.Count == 0)
            {
                AppLib.Windows.FormMessageDefault.ShowInfo("Não existe relatório de lançamento parametrizado.");
            }

            if (dt.Rows.Count == 1)
            {
                int IDREPORT = Convert.ToInt32(dt.Rows[0]["IDREPORT"]);
                this.ImprimirLancamento(IDREPORT);
            }

            if (dt.Rows.Count > 1)
            {
                AppLib.Windows.FormListaPrompt fLista = new AppLib.Windows.FormListaPrompt();
                fLista.PrimeiroItemNulo = false;
                Object result = fLista.Mostrar("Selecione o relatório:", dt);

                if (fLista.confirmacao == AppLib.Global.Types.Confirmacao.OK)
                {
                    int IDREPORT = Convert.ToInt32(result);
                    this.ImprimirLancamento(IDREPORT);
                }
            }
        }

        public void ImprimirLancamento(int IDREPORT)
        {
            if (this.f.dataGridView1.SelectedRows.Count > 0)
            {
                String ListaCODIGO = "";

                for (int i = 0; i < this.f.dataGridView1.SelectedRows.Count; i++)
                {
                    ListaCODIGO += int.Parse(this.f.dataGridView1.SelectedRows[i].Cells["CODLANCA"].Value.ToString());
                    ListaCODIGO += ", ";
                }

                ListaCODIGO = ListaCODIGO.Substring(0, ListaCODIGO.Length - 2);

                AppLib.Padrao.FormReportVisao f = new AppLib.Padrao.FormReportVisao();
                f.grid1.Conexao = "Start";
                f.Visualizar(IDREPORT, ListaCODIGO);
            }
            else
            {
                AppLib.Windows.FormMessageDefault.ShowInfo("Selecione o(s) registro(s).");
            }
        }

    }
}
