using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartOperItemTributoEdit : PS.Lib.WinForms.FrmBaseEdit
    {
        private int codOper, nseq;
        private string codTributo;

        public PSPartOperItemTributoEdit()
        {
            InitializeComponent();
            psCheckBox1.Chave = false;
        }

        public PSPartOperItemTributoEdit(int _codoper, string _codtributo, int _nseq)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "GOPERITEMTRIBUTO");
            psLookup1.PSPart = "PSPartTributo";
            codOper = _codoper;
            codTributo = _codtributo;
            nseq = _nseq;
            zero();
            carregaCampos();
        }

        private void btnCalculaOperacao_Click(object sender, EventArgs e)
        {
            psMoedaBox6.textBox1.Text = string.Format("{0:n4}", (Convert.ToDecimal(psMoedaBox1.textBox1.Text) * Convert.ToDecimal(psMoedaBox2.textBox1.Text) /100) * (Convert.ToDecimal(psMoedaBox5.textBox1.Text) / 100));
            psMoedaBox3.textBox1.Text = string.Format("{0:n4}", (Convert.ToDecimal(psMoedaBox1.textBox1.Text) * Convert.ToDecimal(psMoedaBox2.textBox1.Text) / 100));
            psMoedaBox3.textBox1.Text = string.Format("{0:n4}", (Convert.ToDecimal(psMoedaBox3.textBox1.Text) - Convert.ToDecimal(psMoedaBox6.textBox1.Text)));
        }

        private void zero()
        {
            psMoedaBox1.textBox1.Text = "0,00";
            psMoedaBox2.textBox1.Text = "0,00";
            psMoedaBox3.textBox1.Text = "0,00";
            psMoedaBox4.textBox1.Text = "0,00";
            psMoedaBox5.textBox1.Text = "0,00";
            psMoedaBox6.textBox1.Text = "0,00";
            psMoedaBox7.textBox1.Text = "0,00";
            psMoedaBox8.textBox1.Text = "0,00";
        }

        private void carregaCampos()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GOPERITEMTRIBUTO WHERE CODOPER = ? AND CODEMPRESA = ? AND CODTRIBUTO = ? AND NSEQITEM = ?", new object[] { codOper, AppLib.Context.Empresa, codTributo, nseq });

            if (dt.Rows.Count > 0)
            {
                psLookup1.textBox1.Text = dt.Rows[0]["CODTRIBUTO"].ToString();
                if (!string.IsNullOrEmpty(psLookup1.textBox1.Text))
                {
                    psLookup1.LoadLookup();
                    psLookup1.Enabled = false;
                }
                psTextoBox1.textBox1.Text = dt.Rows[0]["CODCST"].ToString();
                psMoedaBox1.textBox1.Text = dt.Rows[0]["BASECALCULO"].ToString();
                psMoedaBox4.textBox1.Text = dt.Rows[0]["REDUCAOBASEICMS"].ToString();
                psTextoBox2.textBox1.Text = dt.Rows[0]["MODALIDADEBC"].ToString();
                psMoedaBox2.textBox1.Text = dt.Rows[0]["ALIQUOTA"].ToString();
                psMoedaBox3.textBox1.Text = dt.Rows[0]["VALOR"].ToString();
                psMoedaBox5.textBox1.Text = dt.Rows[0]["PDIF"].ToString();
                psMoedaBox6.textBox1.Text = dt.Rows[0]["VICMSDIF"].ToString();
                psTextoBox3.textBox1.Text = dt.Rows[0]["CENQ"].ToString();
                psMoedaBox7.textBox1.Text = dt.Rows[0]["VLDESPADUANA"].ToString();
                psMoedaBox8.textBox1.Text = dt.Rows[0]["VALORIOF"].ToString();
            }
        }

        private bool salvar()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            try
            {
                conn.BeginTransaction();

                AppLib.ORM.Jit TRIBUTO = new AppLib.ORM.Jit(conn, "GOPERITEMTRIBUTO");
                TRIBUTO.Set("CODEMPRESA", AppLib.Context.Empresa);
                TRIBUTO.Set("CODOPER", codOper);
                TRIBUTO.Set("CODTRIBUTO", psLookup1.textBox1.Text);
                TRIBUTO.Set("NSEQITEM", nseq);
                TRIBUTO.Set("CODCST", psTextoBox1.textBox1.Text);
                TRIBUTO.Set("BASECALCULO", Convert.ToDecimal(psMoedaBox1.textBox1.Text));
                TRIBUTO.Set("REDUCAOBASEICMS", Convert.ToDecimal(psMoedaBox4.textBox1.Text));
                TRIBUTO.Set("MODALIDADEBC", psTextoBox2.textBox1.Text);
                TRIBUTO.Set("ALIQUOTA", Convert.ToDecimal(psMoedaBox2.textBox1.Text));
                TRIBUTO.Set("VALOR", Convert.ToDecimal(psMoedaBox3.textBox1.Text));
                TRIBUTO.Set("PDIF", Convert.ToDecimal(psMoedaBox5.textBox1.Text));
                TRIBUTO.Set("VICMSDIF", Convert.ToDecimal(psMoedaBox6.textBox1.Text));
                TRIBUTO.Set("CENQ", psTextoBox3.textBox1.Text);
                TRIBUTO.Set("EDITADO", 1);
                TRIBUTO.Set("VLDESPADUANA", Convert.ToDecimal(psMoedaBox7.textBox1.Text));
                TRIBUTO.Set("VALORIOF", Convert.ToDecimal(psMoedaBox8.textBox1.Text));
                TRIBUTO.Save();

                conn.Commit();
                return true;

            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível salvar os tributos.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                conn.Rollback();
                return false;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            salvar();
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void psMoedaBox8_Load(object sender, EventArgs e)
        {

        }

        private void PSPartOperItemTributoEdit_Load(object sender, EventArgs e)
        {
            psMoedaBox7.textBox1.Text = "0,00";
            psMoedaBox8.textBox1.Text = "0,00";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (salvar() == true)
            {
                this.Dispose();
            }
        }
    }
}
