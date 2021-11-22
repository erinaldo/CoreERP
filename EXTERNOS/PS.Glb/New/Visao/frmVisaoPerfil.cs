using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.Glb.New.Visao
{
    public partial class frmVisaoPerfil : Form
    {
        #region Variáveis

        private New.Classes.Perfil perfil = new Classes.Perfil();

        #endregion

        public frmVisaoPerfil()
        {
            InitializeComponent();
        }

        private void frmVisaoPerfil_Load(object sender, EventArgs e)
        {
            CarregaGrid();
            DesabilitaBotoes();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            New.Cadastros.frmCadastroPerfil f = new Cadastros.frmCadastroPerfil();
            f.ShowDialog();

            CarregaGrid();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                New.Cadastros.frmCadastroPerfil frmCadastroPerfil = new Cadastros.frmCadastroPerfil();
                frmCadastroPerfil.edita = true;
                frmCadastroPerfil.codPerfil = row["CODPERFIL"].ToString();

                frmCadastroPerfil.ShowDialog();

                CarregaGrid();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsFind.AlwaysVisible == true)
            {
                gridView1.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gridView1.OptionsFind.AlwaysVisible = true;
            }
        }

        private void btnAgrupar_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsView.ShowGroupPanel == true)
            {
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.ClearGrouping();
                btnAgrupar.Text = "Agrupar";
            }
            else
            {
                gridView1.OptionsView.ShowGroupPanel = true;
                btnAgrupar.Text = "Desagrupar";
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregaGrid();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                New.Cadastros.frmCadastroPerfil frmCadastroPerfil = new Cadastros.frmCadastroPerfil();
                frmCadastroPerfil.edita = true;
                frmCadastroPerfil.codPerfil = row["CODPERFIL"].ToString();

                frmCadastroPerfil.ShowDialog();

                CarregaGrid();
            }
        }

        #region Métodos

        private void CarregaGrid()
        {
            gridControl1.DataSource = perfil.CarregaGrid();
            gridView1.BestFitColumns();
        }

        private void DesabilitaBotoes()
        {
            btnFiltros.Enabled = false;
            toolStripDropDownButton2.Enabled = false;
            toolStripDropDownButton3.Enabled = false;
            toolStripDropDownButton4.Enabled = false;
        }

        #endregion
    }
}
