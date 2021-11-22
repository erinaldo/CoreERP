using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartBoleto : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartBoleto()
        {
            this.TableName = "FBOLETO";
            this.Keys = new string[] { "CODEMPRESA", "CODLANCA"};
            this.FormEditName = "PSPartBoletoEdit";
            this.PSPartData = new PSPartBoletoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            //this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));

            this.SecurityID = "PSPartBoleto";
            this.ModuleID = "PG";
            this.PSPartApp.Add(new PS.Glb.PSPartGeraRemessaApp());
            this.PSPartApp.Add(new PS.Glb.PSPartCancelarRemessaApp());
            this.PSPartApp.Add(new PS.Glb.PSPartImprimirBoletoApp());


            this.f.GetAnexos().Add(new System.Windows.Forms.ToolStripSeparator());
            this.f.GetAnexos().Add("Imprimir Boleto", null, ImprimirBoleto);
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("ACEITE", null, typeof(PS.Lib.WinForms.PSComboBox)));
            objArr.Add(new DataField("CODLANCA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NUMERO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODMOEDA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("VALOR", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("NOSSONUMERO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODIGOBARRAS", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("IPTE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("IDBOLETOSTATUS", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DVNOSSONUMERO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODFILIAL", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODCLIFOR", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODCONTA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODTIPDOC", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODCONVENIO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODREMESSA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("DATAEMISSAO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DATAVENCIMENTO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DATABOLETO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DATAREMESSA", null, typeof(PS.Lib.WinForms.PSDateBox)));


            return objArr;
        }

        public void ImprimirBoleto(object sender, EventArgs e)
        {
            // this.f.GetAnexos().Add("Imprimir Boleto", null, ImprimirBoleto);

            String consultaZREPORT = @"SELECT IDREPORT, NOME FROM ZREPORT WHERE CODREPORTTIPO = 'BOLETO'";
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaZREPORT, new Object[] { });

            if (dt.Rows.Count == 0)
            {
                AppLib.Windows.FormMessageDefault.ShowInfo("Não existe relatório de boleto parametrizado.");
            }

            if (dt.Rows.Count == 1)
            {
                int IDREPORT = Convert.ToInt32(dt.Rows[0]["IDREPORT"]);
                this.ImprimirBoleto(IDREPORT);
            }

            if (dt.Rows.Count > 1)
            {
                AppLib.Windows.FormListaPrompt fLista = new AppLib.Windows.FormListaPrompt();
                fLista.PrimeiroItemNulo = false;
                Object result = fLista.Mostrar("Selecione o relatório:", dt);

                if (fLista.confirmacao == AppLib.Global.Types.Confirmacao.OK)
                {
                    int IDREPORT = Convert.ToInt32(result);
                    this.ImprimirBoleto(IDREPORT);
                }
            }
        }

        public void ImprimirBoleto(int IDREPORT)
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
