using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartEstruturaEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private DataTable VPRODUTO;

        public PSPartEstruturaEdit()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartProduto";
            psLookup6.PSPart = "PSPartUnidade";

            psLookup6.Chave = false;
        }

        private void BuscaDadosItem()
        {
            try
            {
                string sSql = @"SELECT * FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ?";

                VPRODUTO = dbs.QuerySelect(sSql, PS.Lib.Contexto.Session.Empresa.CodEmpresa, psLookup1.Text);
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
            }
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            textBox2.Text = string.Empty;
            psLookup6.Text = string.Empty;
            psLookup6.LoadLookup();
        }

        private void psLookup1_AfterLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            BuscaDadosItem();

            textBox2.Text = VPRODUTO.Rows[0]["DESCRICAO"].ToString();
            psLookup6.Text = VPRODUTO.Rows[0]["CODUNIDCONTROLE"].ToString();
            psLookup6.LoadLookup();
        }
    }
}
