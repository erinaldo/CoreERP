using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Lib.WinForms
{
    public partial class FormParametro : Form
    {
        public String Result { get; set; }

        public FormParametro()
        {
            InitializeComponent();
        }

        private void FormParametro_Load(object sender, EventArgs e)
        {

        }

        public void Set(String titulo, String mensagem, String valorDefaul)
        {
            this.Text = titulo;
            this.label1.Text = mensagem;
            this.textBox1.Text = valorDefaul;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Result = textBox1.Text;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Result = "";
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, null);
            }
        }      


    }
}
