using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.Glb.New.Processos
{
    public partial class frmReprovarApontamento : Form
    {
        #region Variáveis

        public int idApontamento;
        private Classes.Apontamento apontamento = new Classes.Apontamento();

        #endregion

        public frmReprovarApontamento()
        {
            InitializeComponent();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                apontamento.ReprovarApontamento(idApontamento.ToString(), tbMotivo.Text);

                this.Dispose();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
