using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCopiarTabela : Form
    {
        private string Idtabela;
        private int codEmpresa;
        private string codCliFor;

        public frmCopiarTabela(string _idTabela, int _codEmpresa, string _codCliFor)
        {

            InitializeComponent();
            Idtabela = _idTabela;
            codEmpresa = _codEmpresa;
            codCliFor = _codCliFor;
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            try
            {
                conn.BeginTransaction();

                //Tabela
                AppLib.ORM.Jit VCLIFORTABPRECO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "VCLIFORTABPRECO");
                VCLIFORTABPRECO.Set("CODEMPRESA", codEmpresa);
                VCLIFORTABPRECO.Set("CODCLIFOR", codCliFor);
                VCLIFORTABPRECO.Select();
                VCLIFORTABPRECO.Set("CODCLIFOR", lpCliente.txtcodigo.Text);
                VCLIFORTABPRECO.Set("USATABELAPORFILIAL", Convert.ToBoolean(VCLIFORTABPRECO.Get("USATABELAPORFILIAL")) == true ? 1 : 0);
                VCLIFORTABPRECO.Save();

                //Item
                DataTable dtItem = conn.ExecQuery("SELECT * FROM VCLIFORTABPRECOITEM WHERE IDTABELA = ? AND CODEMPRESA = ? ", new object[] { Idtabela, codEmpresa });
                for (int i = 0; i < dtItem.Rows.Count; i++)
                {
                    VCLIFORTABPRECO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "VCLIFORTABPRECO");
                    VCLIFORTABPRECO.Set("CODEMPRESA", codEmpresa);
                    VCLIFORTABPRECO.Set("CODCLIFOR", lpCliente.txtcodigo.Text);
                    VCLIFORTABPRECO.Select();
                    AppLib.ORM.Jit VCLIFORTABPRECOITEM = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "VCLIFORTABPRECOITEM");
                    VCLIFORTABPRECOITEM.Set("IDTABELA", VCLIFORTABPRECO.Get("IDTABELA"));
                    VCLIFORTABPRECOITEM.Set("CODEMPRESA", Convert.ToInt32(dtItem.Rows[i]["CODEMPRESA"]));
                    VCLIFORTABPRECOITEM.Set("CODFILIAL", Convert.ToInt32(dtItem.Rows[i]["CODFILIAL"]));
                    VCLIFORTABPRECOITEM.Set("CODPRODUTO", dtItem.Rows[i]["CODPRODUTO"].ToString());
                    VCLIFORTABPRECOITEM.Set("CODMOEDA", dtItem.Rows[i]["CODMOEDA"].ToString());
                    VCLIFORTABPRECOITEM.Set("PRECOUNITARIO", Convert.ToDecimal(dtItem.Rows[i]["PRECOUNITARIO"]));
                    VCLIFORTABPRECOITEM.Set("USUARIOCRIACAO", AppLib.Context.Usuario);
                    VCLIFORTABPRECOITEM.Set("DATACRIACAO", conn.GetDateTime());
                    VCLIFORTABPRECOITEM.Save();
                }
                conn.Commit();
                MessageBox.Show("Cópia realizada com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            catch (Exception ex)
            {
                conn.Rollback();
                throw;
            }
        }
    }
}
