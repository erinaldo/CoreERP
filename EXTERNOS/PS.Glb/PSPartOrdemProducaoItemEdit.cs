using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartOrdemProducaoItemEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private DataTable VESTRUTURA;

        public PSPartOrdemProducaoItemEdit()
        {
            InitializeComponent();

            psLookup2.PSPart = "PSPartEstrutura";
            psLookup1.PSPart = "PSPartProduto";
            psLookup6.PSPart = "PSPartUnidade";
        }

        private void BuscaDadosItem()
        {
            try
            {
                string sSql = @"SELECT VPRODUTO.CODPRODUTO, VPRODUTO.CODUNIDCONTROLE
                                FROM PESTRUTURA, VPRODUTO
                                WHERE PESTRUTURA.CODEMPRESA = VPRODUTO.CODEMPRESA
                                AND PESTRUTURA.CODPRODUTO = VPRODUTO.CODPRODUTO
                                AND PESTRUTURA.CODEMPRESA = ? AND PESTRUTURA.CODESTRUTURA = ?";

                VESTRUTURA = dbs.QuerySelect(sSql, PS.Lib.Contexto.Session.Empresa.CodEmpresa, psLookup2.Text);
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
            }
        }

        public override void NovoRegistro()
        {
            base.NovoRegistro();

            psTextoBox1.Text = "0";
            psTextoBox1.Edita = false;

            psLookup1.Chave = false;
            psLookup1.Text = string.Empty;
            psLookup1.LoadLookup();

            psLookup6.Chave = false;
            psLookup6.Text = string.Empty;
            psLookup6.LoadLookup();
        }

        private void psLookup2_AfterLookup(object sender, PS.Lib.LookupEventArgs e)
        {
            BuscaDadosItem();

            psLookup1.Text = VESTRUTURA.Rows[0]["CODPRODUTO"].ToString();
            psLookup1.LoadLookup();

            psLookup6.Text = VESTRUTURA.Rows[0]["CODUNIDCONTROLE"].ToString();
            psLookup6.LoadLookup();
        }
    }
}
