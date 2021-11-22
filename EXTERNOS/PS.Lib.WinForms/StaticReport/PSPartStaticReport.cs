using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PS.Lib.WinForms.StaticReport
{
    public class PSPartStaticReport
    {
        public string ReportName { get; set; }
        public FrmBaseAppStaticReport FormApp { get; set; } //formulário do aplicativo
        public System.Windows.Forms.DataGridView DataGrid { get; set; } //tabela contendo os itens selecionados usado na visao
        public List<DataField> DataField { get; set; } //lista de campos do item selecionado usado na edição
        public AppAccess Access { get; set; } //por onde esta acessando o aplicativo
        public string[] Parameters { get; set; } //lista de campos necessário para execução do relatório

        public virtual void Execute()
        {
            FormApp.psPartAppStaticReport = this;
            FormApp.ReportName = this.ReportName;
            FormApp.Parameters = this.CarregaParametros();
            FormApp.ShowDialog();        
        }

        private List<DataField> CarregaParametros()
        {
            List<DataField> parametros = new List<DataField>();

            if (Access == AppAccess.Edit)
            {
                for (int i = 0; i < Parameters.Length; i++)
                {
                    for (int j = 0; j < DataField.Count; j++)
                    {
                        if (Parameters[i] == DataField[j].Field)
                        {
                            DataField df = new DataField(DataField[j].Field, DataField[j].Valor);

                            parametros.Add(df);
                        }
                    }
                }
            }

            if (Access == AppAccess.View)
            {
                for (int i = 0; i < Parameters.Length; i++)
                {
                    int vSelecionado = DataGrid.CurrentRow.Index;

                    DataField df = new DataField(DataGrid.Columns[Parameters[i]].Name, DataGrid.Rows[vSelecionado].Cells[Parameters[i]].Value);

                    parametros.Add(df);
                }
            }

            return parametros;
        }
    }
}
