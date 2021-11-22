using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITGProducao.Controles;

namespace PS.Glb.New.Filtro
{
    public partial class frmFiltroInventarioItens : Form
    {
        public string condicao = string.Empty;
        public string query = string.Empty;
        public bool aberto = false;

        // Variáveis para Consulta
        public string CODINVENTARIO = string.Empty;
        public string CODFILIAL = string.Empty;
        public string CODLOCAL = string.Empty;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmFiltroInventarioItens()
        {
            InitializeComponent();
        }

        private void rbLocalEstocagem_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLocalEstocagem.Checked == true)
            {
                lbLocal.Visible = true;
                tbLocalEstocagem.Visible = true;
            }
            else
            {
                lbLocal.Visible = false;
                tbLocalEstocagem.Visible = false;
            }
        }

        private void getCondicao()
        {
            try
            {
                if (rbTodos.Checked == true)
                {
                    condicao = @" WHERE VPRODUTO.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND VPRODUTO.ULTIMONIVEL = 1 AND VPRODUTO.ATIVO = 1 AND (VPRODUTO.CODPRODUTO NOT IN (SELECT CODPRODUTO FROM GITEMINVENTARIO WHERE CODEMPRESA = '" + AppLib.Context.Empresa + "' AND CODFILIAL = '" + CODFILIAL + "' AND CODLOCAL = '" + CODLOCAL + "' AND CODINVENTARIO = '" + CODINVENTARIO + "' OR VPRODUTO.CODPRODUTO IN (SELECT GITEMINVENTARIO.CODPRODUTO FROM GITEMINVENTARIO INNER JOIN GINVENTARIO ON GINVENTARIO.CODEMPRESA = GITEMINVENTARIO.CODEMPRESA AND GINVENTARIO.CODFILIAL = GITEMINVENTARIO.CODFILIAL AND GINVENTARIO.CODLOCAL = GITEMINVENTARIO.CODLOCAL AND GINVENTARIO.CODINVENTARIO = GITEMINVENTARIO.CODINVENTARIO WHERE GITEMINVENTARIO.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND GITEMINVENTARIO.CODFILIAL = '" + CODFILIAL + "' AND GITEMINVENTARIO.CODLOCAL = '" + CODLOCAL + "' AND GINVENTARIO.STATUS NOT IN('Encerrado', 'Cancelado'))))";
                }
                else if (rbLocalEstocagem.Checked == true)
                {
                    condicao = @" WHERE VPRODUTO.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND VPRODUTO.ULTIMONIVEL = 1 AND VPRODUTO.ATIVO = 1 AND VPRODUTO.LOCALESTOCAGEM LIKE '" + tbLocalEstocagem.Text + "' AND (VPRODUTO.CODPRODUTO NOT IN (SELECT CODPRODUTO FROM GITEMINVENTARIO WHERE CODEMPRESA = '" + AppLib.Context.Empresa + "' AND CODFILIAL = '" + CODFILIAL + "' AND CODLOCAL = '" + CODLOCAL + "' AND CODINVENTARIO = '" + CODINVENTARIO + "' OR VPRODUTO.CODPRODUTO IN (SELECT GITEMINVENTARIO.CODPRODUTO FROM GITEMINVENTARIO INNER JOIN GINVENTARIO ON GINVENTARIO.CODEMPRESA = GITEMINVENTARIO.CODEMPRESA AND GINVENTARIO.CODFILIAL = GITEMINVENTARIO.CODFILIAL AND GINVENTARIO.CODLOCAL = GITEMINVENTARIO.CODLOCAL AND GINVENTARIO.CODINVENTARIO = GITEMINVENTARIO.CODINVENTARIO WHERE GITEMINVENTARIO.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND GITEMINVENTARIO.CODFILIAL = '" + CODFILIAL + "' AND GITEMINVENTARIO.CODLOCAL = '" + CODLOCAL + "' AND GINVENTARIO.STATUS NOT IN('Encerrado', 'Cancelado'))))";
                }
                else if (rbProdutosSaldo.Checked == true)
                {
                    condicao = @" WHERE VPRODUTO.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND VPRODUTO.ULTIMONIVEL = 1 AND VPRODUTO.ATIVO = 1  AND VPRODUTO.CODPRODUTO IN (SELECT VSALDOESTOQUE.CODPRODUTO FROM VSALDOESTOQUE WHERE VSALDOESTOQUE.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND VSALDOESTOQUE.CODFILIAL = '" + CODFILIAL + "' AND VSALDOESTOQUE.CODLOCAL = '" + CODLOCAL + "' AND VSALDOESTOQUE.SALDOFINAL <> 0 AND VSALDOESTOQUE.CODPRODUTO NOT IN (SELECT DISTINCT(GITEMINVENTARIO.CODPRODUTO) FROM GITEMINVENTARIO INNER JOIN GINVENTARIO ON GINVENTARIO.CODEMPRESA = GITEMINVENTARIO.CODEMPRESA AND GINVENTARIO.CODFILIAL = GITEMINVENTARIO.CODFILIAL AND GINVENTARIO.CODLOCAL = GITEMINVENTARIO.CODLOCAL AND GINVENTARIO.CODINVENTARIO = GITEMINVENTARIO.CODINVENTARIO WHERE GITEMINVENTARIO.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND GITEMINVENTARIO.CODFILIAL = '" + CODFILIAL + "' AND GITEMINVENTARIO.CODLOCAL = '" + CODLOCAL + "'  AND GINVENTARIO.STATUS NOT IN('Encerrado', 'Cancelado')))";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível carregar as condições do filtro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (aberto == false)
            {
                getCondicao();
                if (this.lookup == null)
                {
                    PS.Glb.New.Visao.frmInventarioProdutos frm = new Visao.frmInventarioProdutos(condicao);
                    frm.Codinventario = CODINVENTARIO;
                    frm.ShowDialog();
                }
            }
            else
            {
                getCondicao();
                if (!string.IsNullOrEmpty(condicao))
                {
                    this.Dispose();
                }
            }
            this.Dispose();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
