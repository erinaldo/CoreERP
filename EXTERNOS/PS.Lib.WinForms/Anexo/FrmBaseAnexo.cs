using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace PS.Lib.WinForms.Anexo
{
    public partial class FrmBaseAnexo : DevExpress.XtraEditors.XtraForm
    {
        public List<DataField> DataField { get; set; } //lista de campos do item selecionado usado na edição
        public string PSPartName { get; set; } // Nome do PSPart
        public int nSeq { get; set; } // Sequencial da Imagem

        private AnexoServices AnxService = new AnexoServices();

        private Global gb = new Global();

        public FrmBaseAnexo()
        {
            InitializeComponent();

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = "O";
            list1[0].DisplayMember = "Outro";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = "D";
            list1[1].DisplayMember = "Documento";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = "I";
            list1[2].DisplayMember = "Imagem";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[3].ValueMember = "V";
            list1[3].DisplayMember = "Video";

            psComboBox1.DataSource = list1;
            psComboBox1.DisplayMember = "DisplayMember";
            psComboBox1.ValueMember = "ValueMember";
        }

        private void CarregaParametros()
        {
            this.Text = "Criar Anexo - BETA";

            TabPage tab = new TabPage();

            tab = tabControl1.SelectedTab;

            tab.Text = this.Text;

            if (nSeq == 0)
            {
                NovoRegistro();
            }
            else
            { 
                CarregaRegistro();            
            }        
        }

        private void NovoRegistro()
        {
            txtNome.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            psComboBox1.Value = "O";

            txtAnexo.Text = string.Empty;
            txtExt.Text = string.Empty;

            label3.Visible = true;
            txtAnexo.Visible = true;
            btnAnexo.Visible = true;
            label4.Visible = true;
            txtExt.Visible = true;

            button3.Visible = false;
            button4.Visible = false;
        }

        private void CarregaRegistro()
        {
            DataTable dt = gb.BuscaAnexos(DataField, PSPartName, nSeq);

            if (dt.Rows.Count > 0)
            {
                txtNome.Text = dt.Rows[0]["NOME"].ToString();
                txtDescricao.Text = dt.Rows[0]["DESCRICAO"].ToString();
                psComboBox1.Value = dt.Rows[0]["TIPO"].ToString();
                txtExt.Text = dt.Rows[0]["EXTENSAO"].ToString();
                txtIdimg.Text = dt.Rows[0]["CODIMAGEM"].ToString();

                label3.Visible = false;
                txtAnexo.Visible = false;
                btnAnexo.Visible = false;
                label4.Visible = false;
                txtExt.Visible = false;

                button3.Visible = true;
                button4.Visible = true;
            }
        }

        private void FrmBaseApp_KeyDown(object sender, KeyEventArgs e)
        {
            Keys flag = e.KeyCode;

            if (flag.Equals(Keys.Enter))
            {
                SelectNextControl(ActiveControl, !e.Shift, true, true, true);
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBaseApp_Load(object sender, EventArgs e)
        {
            CarregaParametros();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Execute();
        }

        public virtual void Execute()
        {
            try
            {
                if (txtNome.Text == string.Empty)
                {
                    PSMessageBox.ShowError("Informe o nome do anexo");
                    txtNome.Focus();
                    return;
                }

                if (txtAnexo.Text == string.Empty && nSeq == 0)
                {
                    PSMessageBox.ShowError("Selecione um arquivo para anexar");
                    txtAnexo.Focus();
                    return;
                }

                string strChave = string.Empty;

                for (int i = 0; i < DataField.Count; i++)
                {
                    if (i == (DataField.Count - 1))
                    {
                        strChave = string.Concat(strChave, DataField[i].Valor);
                    }
                    else
                    {
                        strChave = string.Concat(strChave, DataField[i].Valor, ";");
                    }
                }

                if (nSeq == 0)
                {
                    FileStream fs = new FileStream(txtAnexo.Text, FileMode.OpenOrCreate, FileAccess.Read);

                    if (AnxService.SalverAnexo(PSPartName, strChave, nSeq, txtNome.Text, txtDescricao.Text, txtExt.Text.Replace(".", ""), psComboBox1.Value.ToString(), fs))
                    {
                        PSMessageBox.ShowInfo("Operação realizada com sucesso.");
                    }
                    else
                    {
                        PSMessageBox.ShowInfo("Erro ao realizar operação.");
                    }

                    fs.Close();
                }
                else
                {
                    if (AnxService.SalverAnexo(PSPartName, strChave, nSeq, txtNome.Text, txtDescricao.Text, txtExt.Text.Replace(".", ""), psComboBox1.Value.ToString(), null))
                    {
                        PSMessageBox.ShowInfo("Operação realizada com sucesso.");
                    }
                    else
                    {
                        PSMessageBox.ShowInfo("Erro ao realizar operação.");
                    }
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Selecione o Anexo";
            open.Filter = "Todos os arquivos (*.*)|*.*|Todos as arquivos (*.*)|*.*";
            open.InitialDirectory = @"C:\";
            open.Multiselect = false;

            if (open.ShowDialog() == DialogResult.OK)                
            {
                txtAnexo.Text = open.FileName.ToString();
                txtExt.Text = System.IO.Path.GetExtension(open.FileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Title = "Salvar o Anexo como";
                save.Filter = string.Concat(psComboBox1.Text," (.",txtExt.Text,")|*.",txtExt.Text);
                save.InitialDirectory = @"C:\";
                save.ShowDialog();

                if (save.FileName != "")
                {
                    if (AnxService.Download(int.Parse(txtIdimg.Text), save.FileName))
                    {
                        PSMessageBox.ShowInfo("Operação realizada com sucesso.");
                    }
                    else
                    {
                        PSMessageBox.ShowInfo("Erro ao realizar operação.");
                    }
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                string strChave = string.Empty;

                for (int i = 0; i < DataField.Count; i++)
                {
                    if (i == (DataField.Count - 1))
                    {
                        strChave = string.Concat(strChave, DataField[i].Valor);
                    }
                    else
                    {
                        strChave = string.Concat(strChave, DataField[i].Valor, ";");
                    }
                }

                if (AnxService.ExcluirAnexo(PSPartName, strChave, nSeq, int.Parse(txtIdimg.Text)))
                {
                    PSMessageBox.ShowInfo("Operação realizada com sucesso.");

                    this.Close();
                }
                else
                {
                    PSMessageBox.ShowInfo("Erro ao realizar operação.");
                }

            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }
    }
}
