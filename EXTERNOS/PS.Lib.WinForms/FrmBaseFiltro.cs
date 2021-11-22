using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PS.Lib;
using PS.Lib.WinForms;

namespace PS.Lib.WinForms
{
    public partial class FrmBaseFiltro : DevExpress.XtraEditors.XtraForm
    {
        // OBJETOS
        Global gb = new Global();

        public FrmBaseVisao FrmBaseVisao1;
        public List<Filter> ListaDeFiltros;
        public string _tabela;

        public FrmBaseFiltro(FrmBaseVisao ojbFrmBaseVisao)
        {
            InitializeComponent();
            FrmBaseVisao1 = ojbFrmBaseVisao;
            ListaDeFiltros = FrmBaseVisao1._filtros;
            _tabela = FrmBaseVisao1._psPart.TableName;
        }

        private void CarregaTreeView()
        {
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add("Meus Filtros", "Meus Filtros", 1, 1);
            treeView1.Nodes.Add("Filtros Globais", "Filtros Globais", 0, 0);

            for (int i = 0; i < ListaDeFiltros.Count; i++)
            {
                if (ListaDeFiltros[i].padrao == 0)
                {
                    if (ListaDeFiltros[i].codUsuario == null)
                        treeView1.Nodes[1].Nodes.Add(ListaDeFiltros[i].id.ToString(), ListaDeFiltros[i].descricao, 2, 2);
                    else
                        treeView1.Nodes[0].Nodes.Add(ListaDeFiltros[i].id.ToString(), ListaDeFiltros[i].descricao, 2, 2);
                }
            }

            treeView1.ExpandAll();
            dataGridView1.DataSource = null;
        }

        private void Filtro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                this.Close();
            }
        }

        private void Filtro_Load(object sender, EventArgs e)
        {
            CarregaTreeView();
        }

        private List<Filter> RetornaFiltro()
        {
            int key = 0;

            TreeNode node = new TreeNode();
            Filter ft = new Filter();

            try
            {
                node = treeView1.SelectedNode;

                if (node.Level > 0)
                {
                    key = int.Parse(node.Name);

                    for (int i = 0; i < ListaDeFiltros.Count; i++)
                    {
                        if (ListaDeFiltros[i].id == key)
                        {
                            ft = ListaDeFiltros[i];
                            ListaDeFiltros.RemoveAt(i);
                        }
                    }

                    ft.selecionado = 1;
                    ListaDeFiltros.Add(ft);
                }

                return ListaDeFiltros;
            }
            catch
            {
                return ListaDeFiltros;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmBaseVisao1._filtros = RetornaFiltro();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int key = 0;

            TreeNode node = new TreeNode();
            Filter ft = new Filter();

            try
            {
                node = treeView1.SelectedNode;

                key = int.Parse(node.Name);

                // Baseado na lista de filtros verifica as condições de cada filtro para popular o DataGrid
                for (int i = 0; i < ListaDeFiltros.Count; i++)
                {
                    if (ListaDeFiltros[i].id == key)
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = ListaDeFiltros[i].listaCondicao;

                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            if ((dataGridView1.Columns[j].HeaderText == "condicaoText") || (dataGridView1.Columns[j].HeaderText == "operadorlogicoText"))
                                dataGridView1.Columns[j].Visible = true;
                            else
                                dataGridView1.Columns[j].Visible = false;
                        }
                    }
                }

                // Modifica o tamanho das colunas
                dataGridView1.Columns[6].Width = 40;
                dataGridView1.Columns[12].Width = 301;
            }
            catch
            {
                return;
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            button6_Click(this, null);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FrmBaseFiltroManutencao f = new FrmBaseFiltroManutencao(null, this);
            f.ShowDialog();

            CarregaTreeView();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            int key = 0;

            TreeNode node = new TreeNode();
            Filter ft = new Filter();

            try
            {
                node = treeView1.SelectedNode;

                key = int.Parse(node.Name);

                for (int i = 0; i < ListaDeFiltros.Count; i++)
                {
                    if (ListaDeFiltros[i].id == key)
                    {
                        ft = ListaDeFiltros[i];
                    }
                }

                if (MessageBox.Show("Deseja excluir o filtro selecionado?", "Menssagem", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ft.Excluir();
                    ListaDeFiltros.Remove(ft);
                    CarregaTreeView();
                }
            }
            catch
            {
                return;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            int key = 0;

            TreeNode node = new TreeNode();
            Filter ft = new Filter();

            try
            {
                node = treeView1.SelectedNode;

                key = int.Parse(node.Name);

                for (int i = 0; i < ListaDeFiltros.Count; i++)
                {
                    if (ListaDeFiltros[i].id == key)
                    {
                        ft = ListaDeFiltros[i];
                    }
                }

                FrmBaseFiltroManutencao f = new FrmBaseFiltroManutencao(ft, this);
                f.ShowDialog();

                CarregaTreeView();
            }
            catch
            {
                return;
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            int key = 0;

            TreeNode node = new TreeNode();
            Filter ft = new Filter();

            try
            {
                node = treeView1.SelectedNode;

                key = int.Parse(node.Name);

                for (int i = 0; i < ListaDeFiltros.Count; i++)
                {
                    if (ListaDeFiltros[i].id == key)
                    {
                        ft = ListaDeFiltros[i];
                    }
                }

                FrmBaseFiltroRenomear f = new FrmBaseFiltroRenomear(ft, this, false);
                f.ShowDialog();

                CarregaTreeView();

            }
            catch
            {
                return;
            }
        }

        private void FrmBaseFiltro_KeyDown(object sender, KeyEventArgs e)
        {
            Keys flag = e.KeyCode;

            if (flag.Equals(Keys.Enter))
            {
                SelectNextControl(ActiveControl, !e.Shift, true, true, true);
                e.Handled = true;
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            bool Flag = false;

            for (int i = 0; i < ListaDeFiltros.Count; i++)
            {
                if (ListaDeFiltros[i].padrao == 1)
                {
                    Flag = true;
                    ListaDeFiltros[i].selecionado = 1;
                }
            }

            if (!Flag)
            {
                PSMessageBox.ShowError("Filtro Padrão não encontrado.");
                return;
            }

            this.Close();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            int key = 0;

            TreeNode node = new TreeNode();
            Filter ft = new Filter();

            try
            {
                node = treeView1.SelectedNode;

                key = int.Parse(node.Name);

                for (int i = 0; i < ListaDeFiltros.Count; i++)
                {
                    if (ListaDeFiltros[i].id == key)
                    {
                        ft = ListaDeFiltros[i];
                    }
                }

                if (ft.id > 0)
                {
                    FrmBaseFiltroRenomear f = new FrmBaseFiltroRenomear(ft, this, true);
                    f.ShowDialog();

                    CarregaTreeView();
                }
            }
            catch
            {
                return;
            }
        }

        private void FrmBaseFiltro_FormClosed(object sender, FormClosedEventArgs e)
        {
            // PSMemoryManager.ReleaseUnusedMemory(false);
        }

        public void simpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
