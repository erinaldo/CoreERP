using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PS.Glb.Class;

namespace PS.Glb.New.Visao
{
    public partial class frmVisaoPRJProjeto : Form
    {
        string Condicao = String.Empty;

        public frmVisaoPRJProjeto()
        {
            InitializeComponent();
        }

        private void AtualizaGrid()
        {
            string sql = String.Format(@"select * from AUNIDADE {0}", Condicao);
            gridControl1.DataSource = MetodosSQL.GetDT(sql);
        }

        private void AbrirFiltro()
        {
            PS.Glb.New.Filtro.frmFiltroUnidade frm = new Filtro.frmFiltroUnidade();
            frm.ShowDialog();
            Condicao = frm.condicao;
            AtualizaGrid();
        }

        private void frmVisaoPRJProjeto_Load(object sender, EventArgs e)
        {
            AbrirFiltro();
        }

        private void btnFiltros_Click(object sender, EventArgs e)
        {
            AbrirFiltro();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            PS.Glb.New.Cadastros.frmCadastroUnidadeCliente frm = new Cadastros.frmCadastroUnidadeCliente(null,null);
            frm.ShowDialog();
            AtualizaGrid();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                PS.Glb.New.Cadastros.frmCadastroUnidadeCliente frm = new Cadastros.frmCadastroUnidadeCliente(row1["IDUNIDADE"].ToString(), row1["CODCLIFOR"].ToString());
                frm.ShowDialog();
                AtualizaGrid();
            }
            
        }
    }
}
