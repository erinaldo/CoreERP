using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartLocalEstoqueEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartLocalEstoqueEdit()
        {
            InitializeComponent();

            psLookup6.PSPart = "PSPartTipoRua";
            psLookup2.PSPart = "PSPartTipoBairro";
            psLookup1.PSPart = "PSPartPais";
            psLookup4.PSPart = "PSPartEstado";
            psLookup5.PSPart = "PSPartCidade";

            psLookup3.PSPart = "PSPartFilial";

            #region TIPO DE LOCAL DE ESTOQUE

            List<PS.Lib.ComboBoxItem> listCODTIPLOC = new List<PS.Lib.ComboBoxItem>();

            listCODTIPLOC.Add(new PS.Lib.ComboBoxItem());
            listCODTIPLOC[0].ValueMember = "NN";
            listCODTIPLOC[0].DisplayMember = "Nosso em nosso poder";

            listCODTIPLOC.Add(new PS.Lib.ComboBoxItem());
            listCODTIPLOC[1].ValueMember = "NT";
            listCODTIPLOC[1].DisplayMember = "Nosso em poder de terceiro";

            listCODTIPLOC.Add(new PS.Lib.ComboBoxItem());
            listCODTIPLOC[2].ValueMember = "TN";
            listCODTIPLOC[2].DisplayMember = "De terceiro em nosso poder";

            psComboBoxCODTIPLOC.DataSource = listCODTIPLOC;
            psComboBoxCODTIPLOC.DisplayMember = "DisplayMember";
            psComboBoxCODTIPLOC.ValueMember = "ValueMember";

            #endregion
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psCheckBox1.Checked = true;

            psLookup6.Text = "1";
            psLookup6.LoadLookup();

            psLookup2.Text = "1";
            psLookup2.LoadLookup();
        }

        private void psLookup5_BeforeLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODETD", psLookup4.Text));
        }

        private void PSPartLocalEstoqueEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
