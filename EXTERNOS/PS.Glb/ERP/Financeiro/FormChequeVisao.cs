using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.ERP.Financeiro
{
    public partial class FormChequeVisao : AppLib.Windows.FormVisao
    {
        private static FormChequeVisao _instance = null;

        public static FormChequeVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormChequeVisao();

            return _instance;
        }

        private void FormChequeVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public FormChequeVisao()
        {
            InitializeComponent();
        }

        private void FormChequeVisao_Load(object sender, EventArgs e)
        {
            this.grid1.GetAnexos().Add(new System.Windows.Forms.ToolStripSeparator());
            this.grid1.GetAnexos().Add("Imprimir Cheque", null, ImprimirCheque);

            this.grid1.GetProcessos().Add("Cancelar Cheque", null, cancelaCheque);
        }

        private void cancelaCheque(object sender, EventArgs e)
        {
            int canceladoSim = 0, canceladoNao = 0;
            DataRowCollection drc = grid1.GetDataRows();
            for (int i = 0; i < drc.Count; i++)
            {
                int retorno = AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FCHEQUE SET CANCELADO = ? WHERE CODCHEQUE = ? AND CODEMPRESA = ?", new object[] {1,  drc[i]["CODCHEQUE"], drc[i]["CODEMPRESA"] });
                if (retorno.Equals(1))
                {
                    canceladoSim = canceladoSim + 1;
                }
                else
                {
                    canceladoSim = canceladoNao + 1;
                }
            }
            MessageBox.Show("Operação Realizada com Sucesso. \nQtde de Cheques Cancelados: " + canceladoSim + "\nQtde de Cheques não Cancelados: " + canceladoNao + "\n", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            grid1.Atualizar(false);
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormChequeCadastro f = new FormChequeCadastro();
            f.Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormChequeCadastro f = new FormChequeCadastro();
            f.Editar(grid1);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            FormChequeCadastro f = new FormChequeCadastro();
            f.Excluir(grid1);
        }

        public void ImprimirCheque(object sender, EventArgs e)
        {
            // this.f.GetAnexos().Add("Imprimir Cheque", null, ImprimirCheque);

            String consultaZREPORT = @"SELECT IDREPORT, NOME FROM ZREPORT WHERE CODREPORTTIPO = 'CHEQUE'";
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaZREPORT, new Object[] { });

            if (dt.Rows.Count == 0)
            {
                AppLib.Windows.FormMessageDefault.ShowInfo("Não existe relatório de cheque parametrizado.");
            }

            if (dt.Rows.Count == 1)
            {
                int IDREPORT = Convert.ToInt32(dt.Rows[0]["IDREPORT"]);
                this.ImprimirCheque(IDREPORT);
            }

            if (dt.Rows.Count > 1)
            {
                AppLib.Windows.FormListaPrompt fLista = new AppLib.Windows.FormListaPrompt();
                fLista.PrimeiroItemNulo = false;
                Object result = fLista.Mostrar("Selecione o relatório:", dt);

                if (fLista.confirmacao == AppLib.Global.Types.Confirmacao.OK)
                {
                    int IDREPORT = Convert.ToInt32(result);
                    this.ImprimirCheque(IDREPORT);
                }
            }
        }

        public void ImprimirCheque(int IDREPORT)
        {
            System.Data.DataRowCollection drc = grid1.GetDataRows(true);

            if (drc != null)
            {
                if (drc.Count > 0)
                {
                    String ListaCODIGO = "";

                    for (int i = 0; i < drc.Count; i++)
                    {
                        ListaCODIGO += int.Parse(drc[i]["CODCHEQUE"].ToString());
                        ListaCODIGO += ", ";
                    }

                    ListaCODIGO = ListaCODIGO.Substring(0, ListaCODIGO.Length - 2);

                    AppLib.Padrao.FormReportVisao f = new AppLib.Padrao.FormReportVisao();
                    f.grid1.Conexao = "Start";
                    f.Visualizar(IDREPORT, ListaCODIGO);
                }
            }
        }

    }
}
