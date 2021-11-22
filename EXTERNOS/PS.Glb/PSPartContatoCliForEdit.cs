using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartContatoCliForEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        public PSPartContatoCliForEdit()
        {
            InitializeComponent();

            psMaskedTextBox1.Mask = "000,000,000-00";
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psTextoBox1.Text = "0";
            psTextoBox1.Edita = false;

            psDateBox1.Text = string.Empty;
        }
        //public override void SalvaRegistro()
        //{
        //    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
        //    conn.BeginTransaction();
        //    try
        //    {
        //        AppLib.ORM.Jit VCLIFORCONTATO = new AppLib.ORM.Jit(conn, " VCLIFORCONTATO");
        //        VCLIFORCONTATO.Set("CODEMPRESA", AppLib.Context.Empresa);
        //        VCLIFORCONTATO.Set("CODCLIFOR", AppLib.Context.Empresa);


        //        conn.Commit();
        //    }
        //    catch (Exception)
        //    {
        //        conn.Rollback();
        //        MessageBox.Show("Não foi possível completar o cadastro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }


        //}
    }
}
