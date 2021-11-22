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
    public partial class PSPartGerTarefaLogEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartGerTarefaLogEdit()
        {
            InitializeComponent();
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psMoedaBox1.Edita = false;
            psDateBox1.Chave = false;
            psDateBox2.Chave = false;
            //psMemoBox1.Chave = false;
        }

        public override void CarregaRegistro()
        {
            base.CarregaRegistro();

            psMoedaBox1.Edita = false;
            psDateBox1.Chave = false;
            psDateBox2.Chave = false;
            //psMemoBox1.Chave = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = Guid.NewGuid().ToString();
            string path = string.Concat(Convert.ToString(AppDomain.CurrentDomain.BaseDirectory), @"\Log\", name,".txt");

            PS.Lib.Arquivo arq = new PS.Lib.Arquivo();
            arq.Escrever(path, psMemoBox1.Text);
            System.Diagnostics.Process.Start("notepad", path);
        }
    }
}
