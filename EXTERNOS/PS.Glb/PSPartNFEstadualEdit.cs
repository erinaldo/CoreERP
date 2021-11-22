using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartNFEstadualEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartNFEstadualEdit()
        {
            InitializeComponent();

            psLookup2.PSPart = "PSPartCliFor";
            psLookup3.PSPart = "PSPartFilial";

            List<PS.Lib.ComboBoxItem> Item = new List<PS.Lib.ComboBoxItem>();
            Item.Add(new PS.Lib.ComboBoxItem("P", "Pendente"));
            Item.Add(new PS.Lib.ComboBoxItem("U", "Aguardando Processamento"));
            Item.Add(new PS.Lib.ComboBoxItem("E", "Inconsistente"));
            Item.Add(new PS.Lib.ComboBoxItem("A", "Autorizada"));
            Item.Add(new PS.Lib.ComboBoxItem("C", "Cancelada"));
            Item.Add(new PS.Lib.ComboBoxItem("I", "Inutilizado"));
            Item.Add(new PS.Lib.ComboBoxItem("D", "Denegado"));

            psComboBox1.DataSource = Item;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> Item1 = new List<PS.Lib.ComboBoxItem>();
            Item1.Add(new PS.Lib.ComboBoxItem("P", "Paisagem"));
            Item1.Add(new PS.Lib.ComboBoxItem("R", "Retrato"));

            psComboBox2.DataSource = Item1;
            psComboBox2.DisplayMember = "DisplayMember";
            psComboBox2.ValueMember = "ValueMember";
        }

        public override void CarregaRegistro()
        {
            base.CarregaRegistro();

            try
            {
                PSPartNFEstadualData psPartNFEstadualData = new PSPartNFEstadualData();
                System.Data.DataTable dados = psPartNFEstadualData.GetOperacao(PS.Lib.Contexto.Session.Empresa.CodEmpresa, Convert.ToInt32(psTextoBox1.Text));
                if (dados.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in dados.Rows)
                    {
                        psTextoBox2.Text = row["NUMERO"].ToString();
                        psTextoBox3.Text = row["CODSERIE"].ToString();
                        psTextoBox8.Text = row["CODTIPOPER"].ToString();
                        psLookup3.Text = row["CODFILIAL"].ToString();
                        psLookup3.LoadLookup();
                        psLookup2.Text = row["CODCLIFOR"].ToString();
                        psLookup2.LoadLookup();
                        this.GetDefaultCliFor();
                        psDateBox2.Value = Convert.ToDateTime(row["DATAEMISSAO"]);
                    }
                }
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
            }

            psTextoBox1.Edita = false;
            psComboBox2.Chave = false;
            psComboBox1.Chave = false;
            psTextoBox2.Edita = false;
            psTextoBox3.Edita = false;
            psTextoBox8.Edita = false;
            psLookup3.Chave = false;
            psLookup2.Chave = false;
            psMaskedTextBox1.Chave = false;
            psTextoBox4.ReadOnly = true;
            psDateBox2.Chave = false;
            psDateBox1.Chave = false;
            psTextoBox5.Edita = false;
            psDateBox3.Chave = false;
            psTextoBox6.Edita = false;
            psTextoBox7.Edita = false;
            psMemoBox1.ReadOnly = true;
            psMemoBox2.ReadOnly = true;
            psMemoBox3.ReadOnly = true;
            psCheckBox1.Chave = false;
            psCheckBox2.Chave = false;
        }

        private void GetDefaultCliFor()
        {
            PSPartCliForData psPartCliForData = new PSPartCliForData();
            psPartCliForData._tablename = "VCLIFOR";
            psPartCliForData._keys = new string[] { "CODEMPRESA", "CODCLIFOR" };

            DataTable dados = psPartCliForData.ReadRecordEdit(PS.Lib.Contexto.Session.Empresa.CodEmpresa, psLookup2.Text);
            if (dados.Rows.Count > 0)
            {
                foreach (DataRow row in dados.Rows)
                {
                    if (Convert.ToInt32(row["FISICOJURIDICO"]) == 0)
                    {
                        psMaskedTextBox1.Mask = "00,000,000/0000-00";
                        psMaskedTextBox1.Text = row["CGCCPF"].ToString();
                    }

                    if (Convert.ToInt32(row["FISICOJURIDICO"]) == 1)
                    {
                        psMaskedTextBox1.Mask = "000,000,000-00";
                        psMaskedTextBox1.Text = row["CGCCPF"].ToString();
                    }
                }
            }
        }

        private void PSPartNFEstadualEdit_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT IDHISTORICO, DATA, OBSERVACAO FROM GNFESTADUALHISTORICO INNER JOIN GNFESTADUAL ON GNFESTADUALHISTORICO.CODEMPRESA = GNFESTADUAL.CODEMPRESA AND GNFESTADUALHISTORICO.CODOPER = GNFESTADUAL.CODOPER  WHERE GNFESTADUAL.CODOPER = ? AND GNFESTADUAL.IDOUTBOX = ? AND GNFESTADUAL.CODEMPRESA = ?", new object[] { psTextoBox1.Text, psTextoBox7.Text, AppLib.Context.Empresa });
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.OptionsView.BestFitMaxRowCount = -1;
            gridView1.BestFitColumns();
            string sql = @"SELECT XMLPROT FROM GNFESTADUALCANC WHERE CODOPER = ?";
            txtCancelado.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, sql, new object[] { psTextoBox1.Text }).ToString();
            meMotivo.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT MOTIVO FROM GNFESTADUALCANC WHERE CODEMPRESA= ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa,  psTextoBox1.Text }).ToString();

        }

        private void memoEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonSALVAR_Click(object sender, EventArgs e)
        {

        }
    }
}
