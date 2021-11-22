using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITGProducao.Formularios
{
    public partial class FrmExemploNewLookup : Form
    {
        public FrmExemploNewLookup()
        {
            InitializeComponent();
        }

        private void FrmExemploNewLookup_Load(object sender, EventArgs e)
        {
            List<PS.Lib.ComboBoxItem> listTipo = new List<PS.Lib.ComboBoxItem>();

            listTipo.Add(new PS.Lib.ComboBoxItem());
            listTipo[0].ValueMember = "0";
            listTipo[0].DisplayMember = "";

            listTipo.Add(new PS.Lib.ComboBoxItem());
            listTipo[1].ValueMember = "codigo1";
            listTipo[1].DisplayMember = "codigo1";

            listTipo.Add(new PS.Lib.ComboBoxItem());
            listTipo[2].ValueMember = "codigo2";
            listTipo[2].DisplayMember = "codigo2";

            //cmbTipo.DataSource = listTipo;
            //cmbTipo.DisplayMember = "DisplayMember";
            //cmbTipo.ValueMember = "ValueMember";

            //cmbTipo.SelectedIndex = -1;

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT TOP 4 CODMARCA,DESCMARCA FROM PMARCA WHERE CODEMPRESA = 1 AND CODFILIAL = 1 ", new object[] { AppLib.Context.Empresa });
            //grid.DataSource = dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(newLookup1.txtconteudo.Text))
            {
                newLookup1.mensagemErrorProvider = "Favor Selecionar a Marca";
            }
            else
            {
                newLookup1.mensagemErrorProvider = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(newLookup1.ValorCodigoInterno))
            {
                MessageBox.Show(newLookup1.ValorCodigoInterno.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(newLookup5.ValorCodigoInterno))
            {
                MessageBox.Show(newLookup5.ValorCodigoInterno.ToString());
            }
        }
    }
}
