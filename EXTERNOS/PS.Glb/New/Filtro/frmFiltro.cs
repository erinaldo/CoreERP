using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New
{
    public partial class frmFiltro : Form
    {
        private DataTable dt;
        public string condicao = string.Empty;
        public Form pai = null;
        public string tabela = string.Empty;
        public bool aberto = false;

        public string tipOPer = string.Empty;

        public frmFiltro(string _tabela)
        {
            InitializeComponent();
            tabela = _tabela;
            CarregaTreeView();
        }
        private void CarregaTreeView()
        {
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add("Meus Filtros", "Meus Filtros", 1, 1);
            treeView1.Nodes.Add("Filtros Globais", "Filtros Globais", 0, 0);
            dt = getFiltro();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                treeView1.Nodes[0].Nodes.Add(dt.Rows[i]["ID"].ToString(), dt.Rows[i]["DESCRICAO"].ToString(), 2, 2);
            }
            treeView1.ExpandAll();
        }
        private DataTable getFiltro()
        {
            return AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT GFILTRO.ID, GFILTRO.DESCRICAO FROM GFILTRO  WHERE GFILTRO.CODEMPRESA = ? AND GFILTRO.TABELA = ? AND GFILTRO.CODUSUARIO = ?", new object[] { AppLib.Context.Empresa, tabela, AppLib.Context.Usuario });
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            executeFiltro();
            // this.Dispose();
        }
        private void getCondicao(TreeNode node)
        {
            try
            {
                DataTable dtCondicoes = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CAMPO, OPERADOR, VALOR, OPERADORLOGICO FROM GCONDICAO WHERE IDFILTRO = ? AND CODEMPRESA = ?", new object[] { node.Name, AppLib.Context.Empresa });

                for (int i = 0; i < dtCondicoes.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(condicao))
                    {
                        if (dtCondicoes.Rows[i]["VALOR"].ToString() == "$hoje" || dtCondicoes.Rows[i]["VALOR"].ToString() == "$HOJE" || dtCondicoes.Rows[i]["VALOR"].ToString() == "$Hoje")
                        {
                            condicao = "WHERE " + dtCondicoes.Rows[i]["CAMPO"].ToString() + " " + dtCondicoes.Rows[i]["OPERADOR"].ToString() + " '" + string.Format("{0:yyyy-MM-dd}", AppLib.Context.poolConnection.Get("Start").GetDateTime()) + "'";
                        }
                        else
                        {
                            condicao = "WHERE " + dtCondicoes.Rows[i]["CAMPO"].ToString() + " " + dtCondicoes.Rows[i]["OPERADOR"].ToString() + " " + dtCondicoes.Rows[i]["VALOR"].ToString();
                        }

                    }
                    else
                    {
                        condicao = condicao + dtCondicoes.Rows[i]["OPERADORLOGICO"].ToString() + " " + dtCondicoes.Rows[i]["CAMPO"].ToString() + " " + dtCondicoes.Rows[i]["OPERADOR"].ToString() + " " + dtCondicoes.Rows[i]["VALOR"].ToString();
                    }
                }
                switch (tabela)
                {
                    case "GOPER":
                        condicao = condicao + " AND CODTIPOPER = " + "'" + tipOPer + "'";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {


            }

        }
        private void executeFiltro()
        {
            if (treeView1.SelectedNode != null)
            {
                if (aberto == false)
                {
                    TreeNode node = new TreeNode();
                    node = treeView1.SelectedNode;

                    getCondicao(node);
                    switch (tabela)
                    {
                        case "GOPER":
                           Glb.New.Visao.frmVisaoOperacao frmOper = new Glb.New.Visao.frmVisaoOperacao(condicao, pai, tipOPer, "");
                            frmOper.Show();
                            break;
                        case "FLANCA":
                            New.Visao.frmVisaoLancamento frmLanca = new Visao.frmVisaoLancamento(condicao, pai);
                            frmLanca.Show();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    TreeNode node = new TreeNode();
                    node = treeView1.SelectedNode;
                    getCondicao(node);
                }
                this.Dispose();
            }

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            executeFiltro();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }
    }
}
