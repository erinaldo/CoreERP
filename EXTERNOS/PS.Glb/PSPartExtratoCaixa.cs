using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartExtratoCaixa : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartExtratoCaixa()
        {
            this.TableName = "FEXTRATO";
            this.Keys = new string[] { "CODEMPRESA", "IDEXTRATO"};
            this.FormEditName = "PSPartExtratoCaixaEdit";
            this.PSPartData = new PSPartExtratoCaixaData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VALOR",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CTIPO",
                                                                                "",
                                                                                PS.Lib.DataGridColumnType.Image,
                                                                                1,
                                                                                "TIPO",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter,
                                                                                new PS.Lib.ImageProperties[] { new PS.Lib.ImageProperties(0, "Entrada", Properties.Resources.img_extent),
                                                                                                               new PS.Lib.ImageProperties(1, "Saída", Properties.Resources.img_extsai),
                                                                                                               new PS.Lib.ImageProperties(2, "Transferência Saída", Properties.Resources.img_exttrfsai),
                                                                                                               new PS.Lib.ImageProperties(3, "Transferência Entrada", Properties.Resources.img_exttrfent),
                                                                                                               new PS.Lib.ImageProperties(4, "Cheque Saída", Properties.Resources.cheque_saida),
                                                                                                               new PS.Lib.ImageProperties(5, "Cheque Entrada", Properties.Resources.cheque_entrada)
                                                                                }));

            this.Folder.Add(new PS.Lib.WinForms.Folder(new PSPartLanca(), "CODEMPRESA", "IDEXTRATO"));

            this.PSPartApp.Add(new PS.Glb.PSPartCompensarExtratoApp());
            this.PSPartApp.Add(new PS.Glb.PSPartCancelaCompensacaoExtratoApp());

            this.SecurityID = "PSPartExtratoCaixa";
            this.ModuleID = "PG";

            this.f.GetAnexos().Add(new System.Windows.Forms.ToolStripSeparator());
            this.f.GetAnexos().Add("Imprimir Extrato", null, ImprimirExtrato);
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("COMPENSADO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("DATACOMPENSACAO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("IDEXTRATOTRF", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODFILIALTRF", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODCONTATRF", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODCCUSTO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODNATUREZAORCAMENTO", null, typeof(PS.Lib.WinForms.PSLookup)));

            List<ComboBoxItem> Item1 = new List<ComboBoxItem>();
            Item1.Add(new ComboBoxItem(0, "Entrada"));
            Item1.Add(new ComboBoxItem(1, "Saida"));
            Item1.Add(new ComboBoxItem(1, "Transferência"));

            objArr.Add(new DataField("TIPO", null, typeof(PS.Lib.WinForms.PSComboBox), Item1));

            objArr.Add(new DataField("HISTORICO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DATA", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("VALOR", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("NUMERODOCUMENTO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODCONTA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODFILIAL", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("IDEXTRATO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }

        public void ImprimirExtrato(object sender, EventArgs e)
        {
            // this.f.GetAnexos().Add("Imprimir Extrato", null, ImprimirExtrato);

            String consultaZREPORT = @"SELECT IDREPORT, NOME FROM ZREPORT WHERE CODREPORTTIPO = 'EXTRATO'";
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaZREPORT, new Object[] { });

            if (dt.Rows.Count == 0)
            {
                AppLib.Windows.FormMessageDefault.ShowInfo("Não existe relatório de extrato parametrizado.");
            }

            if (dt.Rows.Count == 1)
            {
                int IDREPORT = Convert.ToInt32(dt.Rows[0]["IDREPORT"]);
                this.ImprimirExtrato(IDREPORT);
            }

            if (dt.Rows.Count > 1)
            {
                AppLib.Windows.FormListaPrompt fLista = new AppLib.Windows.FormListaPrompt();
                fLista.PrimeiroItemNulo = false;
                Object result = fLista.Mostrar("Selecione o relatório:", dt);

                if (fLista.confirmacao == AppLib.Global.Types.Confirmacao.OK)
                {
                    int IDREPORT = Convert.ToInt32(result);
                    this.ImprimirExtrato(IDREPORT);
                }
            }
        }

        public void ImprimirExtrato(int IDREPORT)
        {
            if (this.f.dataGridView1.SelectedRows.Count > 0)
            {
                String ListaCODIGO = "";

                for (int i = 0; i < this.f.dataGridView1.SelectedRows.Count; i++)
                {
                    ListaCODIGO += int.Parse(this.f.dataGridView1.SelectedRows[i].Cells["IDEXTRATO"].Value.ToString());
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
