using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ITGProducao.Controles;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroItensInventario : Form
    {
        public bool edita = false;
        //Variáveis para consulta de dados
        public string CodInventario = string.Empty;
        public string Codproduto = string.Empty;
        public string CodFilial = string.Empty;
        private string Descricao = string.Empty;
        public string codMenu = string.Empty;
        private string Diferenca1 = string.Empty;
        private string Diferenca2 = string.Empty;
        private string Diferenca3 = string.Empty;

        DataTable dtValores;

        //Variáveis para NewLookup
        private NewLookup lookup;

        //Variáveis para validações
        private string Codigo = string.Empty;
        private decimal Diferencas;
        private decimal DiferencaContagem1;
        private decimal DiferencaContagem2;
        private decimal DiferencaContagem3;
        private int Contagem;
        private string Digitacao1 = string.Empty;
        private string Digitacao2 = string.Empty;
        private string Digitacao3 = string.Empty;

        public frmCadastroItensInventario()
        {
            InitializeComponent();

            new Class.Utilidades().getDicionario(this, tabControl1, "GITEMINVENTARIO");
        }

        private void frmCadastroItensInventario_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                Codigo = getCodigo();
                carregaCampos();
                ValidaContagens();
                ValidaStatus();
                CustomizaComponentes();
            }
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GITEMINVENTARIO WHERE CODINVENTARIO = ? AND CODPRODUTO = ?", new object[] { CodInventario, Codproduto });
                if (dt.Rows.Count > 0)
                {
                    dtValores = dt;

                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GITEMINVENTARIO WHERE CODINVENTARIO = ? AND CODPRODUTO = ?", new object[] { CodInventario, Codproduto });
                if (dt.Rows.Count > 0)
                {
                    dtValores = dt;

                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbCodInventario.Text = dt.Rows[0]["CODINVENTARIO"].ToString();
            lpLocalEstoque.txtcodigo.Text = dt.Rows[0]["CODLOCAL"].ToString();
            lpLocalEstoque.CarregaDescricao();

            if (Codigo == Codproduto)
            {
                tbCodProduto.Text = Codproduto;
            }
            else
            {
                tbCodProduto.Text = Codigo;
            }

            Descricao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT DESCRICAO FROM VPRODUTO WHERE CODPRODUTO =? AND CODEMPRESA = ?", new object[] { Codproduto, AppLib.Context.Empresa }).ToString();
            tbDescricao.Text = Descricao;
            tbUnidadeControle.Text = dt.Rows[0]["CODUNIDCONTROLE"].ToString();

            tbContagem1.Text = dt.Rows[0]["CONTAGEM1"].ToString();

            Diferenca1 = dt.Rows[0]["DIFERENCACONTAGEM1"].ToString();

            tbContagem2.Text = dt.Rows[0]["CONTAGEM2"].ToString();

            Diferenca2 = dt.Rows[0]["DIFERENCACONTAGEM2"].ToString();

            tbContagem3.Text = dt.Rows[0]["CONTAGEM3"].ToString();

            Diferenca3 = dt.Rows[0]["DIFERENCACONTAGEM3"].ToString();

            if (!string.IsNullOrEmpty(dt.Rows[0]["DATA1CONTAGEM"].ToString()))
            {
                dtContagem1.DateTime = Convert.ToDateTime(dt.Rows[0]["DATA1CONTAGEM"]);
            }

            if (!string.IsNullOrEmpty(dt.Rows[0]["DATA2CONTAGEM"].ToString()))
            {
                dtContagem2.DateTime = Convert.ToDateTime(dt.Rows[0]["DATA2CONTAGEM"]);
            }

            if (!string.IsNullOrEmpty(dt.Rows[0]["DATA3CONTAGEM"].ToString()))
            {
                dtContagem3.DateTime = Convert.ToDateTime(dt.Rows[0]["DATA3CONTAGEM"]);
            }

            tbUsuarioDigitacao1.Text = dt.Rows[0]["USUARIOCONTAGEM1"].ToString();
            tbUsuarioDigitacao2.Text = dt.Rows[0]["USUARIOCONTAGEM2"].ToString();
            tbUsuarioDigitacao3.Text = dt.Rows[0]["USUARIOCONTAGEM3"].ToString();
            tbObservacao.Text = dt.Rows[0]["OBSERVACAO"].ToString();
        }

        #region Validações

        private void ValidaContagens()
        {
            bool Primeira = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT ENCERRARCONTAGEM1 FROM GINVENTARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODINVENTARIO = ?", new object[] { AppLib.Context.Empresa, CodFilial, lpLocalEstoque.txtcodigo.Text, CodInventario }));

            if (Primeira == true)
            {
                tbContagem1.Enabled = false;
                tbContagem2.Enabled = true;
            }

            bool Segunda = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT ENCERRARCONTAGEM2 FROM GINVENTARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODINVENTARIO = ?", new object[] { AppLib.Context.Empresa, CodFilial, lpLocalEstoque.txtcodigo.Text, CodInventario }));

            if (Segunda == true)
            {
                tbContagem2.Enabled = false;
                tbContagem3.Enabled = true;
            }

            bool Terceira = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT ENCERRARCONTAGEM3 FROM GINVENTARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODLOCAL = ? AND CODINVENTARIO = ?", new object[] { AppLib.Context.Empresa, CodFilial, lpLocalEstoque.txtcodigo.Text, CodInventario }));

            if (Terceira == true)
            {
                tbContagem3.Enabled = false;
            }
        }

        private void ValidaStatus()
        {
            string Status = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT STATUS FROM GINVENTARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODINVENTARIO = ? AND CODLOCAL = ?", new object[] { AppLib.Context.Empresa, CodFilial, CodInventario, lpLocalEstoque.txtcodigo.Text }).ToString();

            if (Status == "Encerrado")
            {
                btnCancelarAtual.Text = "Fechar";
                btnOKAtual.Enabled = false;
                btnSalvarAtual.Enabled = false;

                tbContagem1.Enabled = false;
                tbContagem2.Enabled = false;
                tbContagem3.Enabled = false;

                tbObservacao.Enabled = false;
            }
            if (Status == "Aberto")
            {
                tbContagem1.Enabled = false;
                tbContagem2.Enabled = false;
                tbContagem3.Enabled = false;
            }

            if (Status == "Cancelado")
            {
                btnCancelarAtual.Text = "Fechar";
                btnOKAtual.Enabled = false;
                btnSalvarAtual.Enabled = false;

                tbContagem1.Enabled = false;
                tbContagem2.Enabled = false;
                tbContagem3.Enabled = false;

                tbObservacao.Enabled = false;
            }
        }

        #endregion

        private void tbContagem1_Leave(object sender, EventArgs e)
        {
            if (Digitacao1 == tbContagem1.Text)
            {
                return;
            }
            Contagem = 1;
            dtContagem1.Text = Convert.ToDateTime(DateTime.Now).ToString();
            tbUsuarioDigitacao1.Text = AppLib.Context.Usuario;
            DiferencaContagem1 = ApuracaoDiferenca();
            Digitacao1 = tbContagem1.Text;
        }

        private void tbContagem2_Leave(object sender, EventArgs e)
        {
            if (Digitacao2 == tbContagem2.Text)
            {
                return;
            }
            Contagem = 2;
            dtContagem2.Text = Convert.ToDateTime(DateTime.Now).ToString();
            tbUsuarioDigitacao2.Text = AppLib.Context.Usuario;
            DiferencaContagem2 = ApuracaoDiferenca();
            Digitacao2 = tbContagem2.Text;
        }

        private void tbContagem3_Leave(object sender, EventArgs e)
        {
            if (Digitacao3 == tbContagem3.Text)
            {
                return;
            }
            Contagem = 3;
            dtContagem3.Text = Convert.ToDateTime(DateTime.Now).ToString();
            tbUsuarioDigitacao3.Text = AppLib.Context.Usuario;
            DiferencaContagem3 = ApuracaoDiferenca();
            Digitacao3 = tbContagem3.Text;
        }

        private decimal ApuracaoDiferenca()
        {
            decimal SaldoInicial = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT SALDOINICIAL FROM GITEMINVENTARIO WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND CODINVENTARIO = ?", new object[] { AppLib.Context.Empresa, Codproduto, CodInventario }));

            switch (Contagem)
            {
                case 1:

                    if (!string.IsNullOrEmpty(tbContagem1.Text))
                    {
                        Diferencas = Convert.ToDecimal(tbContagem1.Text) - SaldoInicial;
                        return Diferencas;
                    }
                    else
                    {
                        return Diferencas;
                    }

                case 2:

                    if (!string.IsNullOrEmpty(tbContagem2.Text))
                    {
                        Diferencas = Convert.ToDecimal(tbContagem2.Text) - SaldoInicial;
                        return Diferencas;
                    }
                    else
                    {
                        return Diferencas;
                    }

                case 3:

                    if (!string.IsNullOrEmpty(tbContagem3.Text))
                    {
                        Diferencas = Convert.ToDecimal(tbContagem3.Text) - SaldoInicial;
                        return Diferencas;
                    }
                    else
                    {
                        return Diferencas;
                    }
                default:
                    break;
            }
            return Diferencas;
        }

        /// <summary>
        /// Método para verificar se buscará ou não o Codigo Auxiliar
        /// </summary>
        /// <returns>Retorna o Código Auxiliar/Retorna o Código do Produto</returns>
        private string getCodigo()
        {
            int Codigo;
            string CodigoProduto = string.Empty;

            Codigo = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT BUSCAPRODUTOPOR FROM VPARAMETROS"));

            if (Codigo == 1)
            {
                CodigoProduto = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODIGOAUXILIAR FROM VPRODUTO WHERE CODPRODUTO = ?", new object[] { Codproduto }).ToString();
            }
            return CodigoProduto;
        }

        /// <summary>
        /// Método para customização dos componentes
        /// </summary>
        private void CustomizaComponentes()
        {
            tbCodInventario.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;

            tbCodProduto.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            tbDescricao.BorderStyle = BorderStyle.None;
            tbDescricao.ReadOnly = true;

            tbUnidadeControle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        }

        private DataTable ValidaEncerramento()
        {
            DataTable dtValidacao = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT ENCERRARCONTAGEM1, ENCERRARCONTAGEM2, ENCERRARCONTAGEM3 FROM GINVENTARIO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODINVENTARIO = ? AND CODLOCAL = ?", new object[] { AppLib.Context.Empresa, CodFilial, CodInventario, lpLocalEstoque.txtcodigo.Text });
            return dtValidacao;
        }

        private bool Salvar()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit GITEMINVENTARIO = new AppLib.ORM.Jit(conn, "GITEMINVENTARIO");
            conn.BeginTransaction();

            DataTable dt = ValidaEncerramento();

            try
            {
                // Primeira contagem

                if (!string.IsNullOrEmpty(tbContagem1.Text))
                {
                    GITEMINVENTARIO.Set("CODEMPRESA", AppLib.Context.Empresa);
                    GITEMINVENTARIO.Set("CODFILIAL", CodFilial);
                    GITEMINVENTARIO.Set("CODINVENTARIO", CodInventario);
                    GITEMINVENTARIO.Set("CODLOCAL", lpLocalEstoque.txtcodigo.Text);
                    GITEMINVENTARIO.Set("CODPRODUTO", Codproduto);
                    GITEMINVENTARIO.Set("CONTAGEM1", Convert.ToDecimal(tbContagem1.Text));

                    if (string.IsNullOrEmpty(dtValores.Rows[0]["DATA1CONTAGEM"].ToString()))
                    {
                        GITEMINVENTARIO.Set("DATA1CONTAGEM", Convert.ToDateTime(DateTime.Now));
                    }
                    else
                    {
                        GITEMINVENTARIO.Set("DATA1CONTAGEM", Convert.ToDateTime(dtValores.Rows[0]["DATA1CONTAGEM"]));
                    }

                    GITEMINVENTARIO.Set("USUARIOCONTAGEM1", AppLib.Context.Usuario);
                    GITEMINVENTARIO.Set("USUARIOALTERACAO", AppLib.Context.Usuario);
                    GITEMINVENTARIO.Set("DATAALTERACAO", Convert.ToDateTime(DateTime.Now));
                    GITEMINVENTARIO.Set("OBSERVACAO", tbObservacao.Text);

                    if (Convert.ToBoolean(dt.Rows[0]["ENCERRARCONTAGEM1"]) == true)
                    {
                        GITEMINVENTARIO.Set("DIFERENCACONTAGEM1", Convert.ToDecimal(dtValores.Rows[0]["DIFERENCACONTAGEM1"]));
                        GITEMINVENTARIO.Set("DIFERENCAFINAL", Convert.ToDecimal(dtValores.Rows[0]["DIFERENCAFINAL"]));
                    }
                    else
                    {
                        GITEMINVENTARIO.Set("DIFERENCACONTAGEM1", DiferencaContagem1);
                        GITEMINVENTARIO.Set("DIFERENCAFINAL", DiferencaContagem1);
                    }
                }
                else
                {
                    GITEMINVENTARIO.Set("CODEMPRESA", AppLib.Context.Empresa);
                    GITEMINVENTARIO.Set("CODFILIAL", CodFilial);
                    GITEMINVENTARIO.Set("CODINVENTARIO", CodInventario);
                    GITEMINVENTARIO.Set("CODLOCAL", lpLocalEstoque.txtcodigo.Text);
                    GITEMINVENTARIO.Set("CODPRODUTO", Codproduto);
                    GITEMINVENTARIO.Set("CONTAGEM1", null);
                    GITEMINVENTARIO.Set("DATA1CONTAGEM", null);
                    GITEMINVENTARIO.Set("USUARIOCONTAGEM1", null);
                }

                // Segunda contagem
                if (!string.IsNullOrEmpty(tbContagem2.Text))
                {
                    GITEMINVENTARIO.Set("CODEMPRESA", AppLib.Context.Empresa);
                    GITEMINVENTARIO.Set("CODFILIAL", CodFilial);
                    GITEMINVENTARIO.Set("CODINVENTARIO", CodInventario);
                    GITEMINVENTARIO.Set("CODLOCAL", lpLocalEstoque.txtcodigo.Text);
                    GITEMINVENTARIO.Set("CODPRODUTO", Codproduto);

                    GITEMINVENTARIO.Set("CONTAGEM2", Convert.ToDecimal(tbContagem2.Text));

                    if (string.IsNullOrEmpty(dtValores.Rows[0]["DATA2CONTAGEM"].ToString()))
                    {
                        GITEMINVENTARIO.Set("DATA2CONTAGEM", Convert.ToDateTime(DateTime.Now));
                    }
                    else
                    {
                        GITEMINVENTARIO.Set("DATA2CONTAGEM", Convert.ToDateTime(dtValores.Rows[0]["DATA2CONTAGEM"]));
                    }

                    GITEMINVENTARIO.Set("USUARIOCONTAGEM2", AppLib.Context.Usuario);
                    GITEMINVENTARIO.Set("USUARIOALTERACAO", AppLib.Context.Usuario);
                    GITEMINVENTARIO.Set("DATAALTERACAO", Convert.ToDateTime(DateTime.Now));
                    GITEMINVENTARIO.Set("OBSERVACAO", tbObservacao.Text);

                    if (Convert.ToBoolean(dt.Rows[0]["ENCERRARCONTAGEM2"]) == true)
                    {
                        GITEMINVENTARIO.Set("DIFERENCACONTAGEM2", Convert.ToDecimal(dtValores.Rows[0]["DIFERENCACONTAGEM2"]));
                        GITEMINVENTARIO.Set("DIFERENCAFINAL", Convert.ToDecimal(dtValores.Rows[0]["DIFERENCAFINAL"]));
                    }
                    else
                    {
                        GITEMINVENTARIO.Set("DIFERENCACONTAGEM2", DiferencaContagem2);
                        GITEMINVENTARIO.Set("DIFERENCAFINAL", DiferencaContagem2);
                    }
                }
                else
                {
                    GITEMINVENTARIO.Set("CODEMPRESA", AppLib.Context.Empresa);
                    GITEMINVENTARIO.Set("CODFILIAL", CodFilial);
                    GITEMINVENTARIO.Set("CODINVENTARIO", CodInventario);
                    GITEMINVENTARIO.Set("CODLOCAL", lpLocalEstoque.txtcodigo.Text);
                    GITEMINVENTARIO.Set("CODPRODUTO", Codproduto);
                    GITEMINVENTARIO.Set("CONTAGEM2", null);
                    GITEMINVENTARIO.Set("DATA2CONTAGEM", null);
                    GITEMINVENTARIO.Set("USUARIOCONTAGEM2", null);
                }

                // Terceira contagem
                if (!string.IsNullOrEmpty(tbContagem3.Text))
                {
                    GITEMINVENTARIO.Set("CODEMPRESA", AppLib.Context.Empresa);
                    GITEMINVENTARIO.Set("CODFILIAL", CodFilial);
                    GITEMINVENTARIO.Set("CODINVENTARIO", CodInventario);
                    GITEMINVENTARIO.Set("CODLOCAL", lpLocalEstoque.txtcodigo.Text);
                    GITEMINVENTARIO.Set("CODPRODUTO", Codproduto);

                    GITEMINVENTARIO.Set("CONTAGEM3", Convert.ToDecimal(tbContagem3.Text));

                    if (string.IsNullOrEmpty(dtValores.Rows[0]["DATA3CONTAGEM"].ToString()))
                    {
                        GITEMINVENTARIO.Set("DATA3CONTAGEM", Convert.ToDateTime(DateTime.Now));
                    }
                    else
                    {
                        GITEMINVENTARIO.Set("DATA3CONTAGEM", Convert.ToDateTime(dtValores.Rows[0]["DATA3CONTAGEM"]));
                    }

                    GITEMINVENTARIO.Set("USUARIOCONTAGEM3", AppLib.Context.Usuario);
                    GITEMINVENTARIO.Set("USUARIOALTERACAO", AppLib.Context.Usuario);
                    GITEMINVENTARIO.Set("DATAALTERACAO", Convert.ToDateTime(DateTime.Now));
                    GITEMINVENTARIO.Set("OBSERVACAO", tbObservacao.Text);

                    if (Convert.ToBoolean(dt.Rows[0]["ENCERRARCONTAGEM3"]) == true)
                    {
                        GITEMINVENTARIO.Set("DIFERENCACONTAGEM3", Convert.ToDecimal(dtValores.Rows[0]["DIFERENCACONTAGEM3"]));
                        GITEMINVENTARIO.Set("DIFERENCAFINAL", Convert.ToDecimal(dtValores.Rows[0]["DIFERENCAFINAL"]));
                    }
                    else
                    {
                        GITEMINVENTARIO.Set("DIFERENCACONTAGEM3", DiferencaContagem3);
                        GITEMINVENTARIO.Set("DIFERENCAFINAL", DiferencaContagem3);
                    }
                }
                else
                {
                    GITEMINVENTARIO.Set("CODEMPRESA", AppLib.Context.Empresa);
                    GITEMINVENTARIO.Set("CODFILIAL", CodFilial);
                    GITEMINVENTARIO.Set("CODINVENTARIO", CodInventario);
                    GITEMINVENTARIO.Set("CODLOCAL", lpLocalEstoque.txtcodigo.Text);
                    GITEMINVENTARIO.Set("CODPRODUTO", Codproduto);
                    GITEMINVENTARIO.Set("CONTAGEM3", null);
                    GITEMINVENTARIO.Set("DATA3CONTAGEM", null);
                    GITEMINVENTARIO.Set("USUARIOCONTAGEM3", null);
                }

                GITEMINVENTARIO.Save();
                conn.Commit();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                this.Dispose();
            }
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
