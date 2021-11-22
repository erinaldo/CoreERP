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
    public partial class PSPartGerTarefaEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartGerTarefaEdit()
        {
            InitializeComponent();
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psTextoBox1.Text = "0";
            psTextoBox1.Edita = false;
        }

        public override void CarregaRegistro()
        {
            base.CarregaRegistro();

            psTextoBox1.Edita = false;
            //psTextoBox2.Chave = false;
            //psTextoBox3.Chave = false;
            //psTextoBox4.Chave = false;
            //psTextoBox5.Chave = false;

            //psCheckBox1.Chave = false;
            //psCheckBox2.Chave = false;
            //psCheckBox3.Chave = false;
            //psCheckBox4.Chave = false;
            //psCheckBox5.Chave = false;
            //psCheckBox6.Chave = false;
            //psCheckBox7.Chave = false;

            //psTextoBox6.Chave = false;
        }
    }
}
