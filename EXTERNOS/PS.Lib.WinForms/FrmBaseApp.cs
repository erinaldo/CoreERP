using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Lib.WinForms
{
    public partial class FrmBaseApp : System.Windows.Forms.Form
    {
        public PSPartApp psPartApp { get; set; } //classe do aplicativo
        
        // Variáveis criadas para criar componente em tempo de execucação
        public DevExpress.XtraEditors.LabelControl Label;
        public DevExpress.XtraEditors.TextEdit TextEdit;
        public DevExpress.XtraEditors.CheckEdit CheckBoxCliente;
        public DevExpress.XtraEditors.CheckEdit CheckBoxTransportadora;

        // Variáveis para validação
        public string EMAIL { get; set; }
        public bool CLIENTE { get; set; }

        public FrmBaseApp()
        {
            InitializeComponent();
        }

        private void FrmBaseApp_KeyDown(object sender, KeyEventArgs e)
        {
            //Keys flag = e.KeyCode;

            //if (flag.Equals(Keys.Enter))
            //{
            //    SelectNextControl(ActiveControl, !e.Shift, true, true, true);
            //    e.Handled = true;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBaseApp_Load(object sender, EventArgs e)
        { 
            if (psPartApp._ValorSelecionado == "Enviar XML da NF-e por E-mail")
            {
                int x, y;

                x = 8;
                y = 6;

                TabPage tab = new TabPage();

                tab = tabControl1.SelectedTab;

                tab.Text = this.Text;

                //Cria o componente em tempo de execução

                Label = new DevExpress.XtraEditors.LabelControl();

                Label.Location = new Point(x, y);
                Label.Text = "E-mail";
                tab.Controls.Add(Label);
                y = y + 17;

                TextEdit = new DevExpress.XtraEditors.TextEdit();

                TextEdit.Location = new Point(x, y);
                TextEdit.Size = new Size(500, 20);
                TextEdit.Properties.NullValuePromptShowForEmptyValue = true;
                TextEdit.Properties.NullValuePrompt = "Use vírgula para a separação dos e-mails.";
                tab.Controls.Add(TextEdit);

                //
                EMAIL = TextEdit.Text;

                y = y + 26;

                CheckBoxCliente = new DevExpress.XtraEditors.CheckEdit();

                CheckBoxCliente.Location = new Point(x, y);
                CheckBoxCliente.Size = new Size(500, 20);
                CheckBoxCliente.Text = "Enviar para o endereço de cadastro do cliente?";
                CheckBoxCliente.Checked = true;
                tab.Controls.Add(CheckBoxCliente);

                //
                CLIENTE = CheckBoxCliente.Checked;

                y = y + 26;               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (psPartApp._ValorSelecionado == "Enviar XML da NF-e por E-mail")
            {
                EMAIL = TextEdit.Text;
                CLIENTE = CheckBoxCliente.Checked;

                // Zera o campo TextEdit para inserir novos e-mails.
                TextEdit.Text = string.Empty;
            }
           
            if (this.Execute())
            {               
                this.Close();
            }
        }

        public virtual Boolean Execute()
        {
            return false;
        }

        private void FrmBaseApp_FormClosed(object sender, FormClosedEventArgs e)
        {
            // PSMemoryManager.ReleaseUnusedMemory(false);
        }
    }
}
