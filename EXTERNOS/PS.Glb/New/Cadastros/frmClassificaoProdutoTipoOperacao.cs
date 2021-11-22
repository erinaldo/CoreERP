using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Cadastros
{
    public partial class frmClassificaoProdutoTipoOperacao : Form
    {
        private string codTipOPer = string.Empty;

        public frmClassificaoProdutoTipoOperacao(string _codTipOper)
        {
            InitializeComponent();
            codTipOPer = _codTipOper;
        }

        private void frmClassificaoProdutoTipoOperacao_Load(object sender, EventArgs e)
        {
            carregaGrid();
        }
        private void carregaGrid()
        {
            gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT  CODCLASSIFICACAO as 'Cód. Classificação', DESCRICAO as 'Descrição' FROM VPRODUTOCLASSIFICACAO", new object[] { });

        }

        private void salvar()
        {
            int contatdor = 0;

            if (gridView1.SelectedRowsCount > 0)
            {
                for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                {
                    DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                    bool retorno = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(*) FROM GTIPOPERCLASSIFICAOPRODUTO WHERE CODTIPOPER = ? AND CODEMPRESA = ? AND CODCLASSIFICACAO = ?", new object[] { codTipOPer, AppLib.Context.Empresa, row[0] }));

                    if (retorno == false)
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO GTIPOPERCLASSIFICAOPRODUTO (CODCLASSIFICACAO, CODEMPRESA, CODTIPOPER) VALUES (?, ?, ?)", new object[] { row[0], AppLib.Context.Empresa, codTipOPer });
                        contatdor++;
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione pelo menos um registro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (contatdor == 0)
            {
                MessageBox.Show("Nenhum item foi inserido ao tipo de operação selecionado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            salvar();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            salvar();
            this.Dispose();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
