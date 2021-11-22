using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITGProducao.Formularios
{
    public partial class FrmEstruturaRecursoComponentes : Form
    {
        public string codEstrutura = string.Empty;
        public string CodRevEstrutura = string.Empty;
        public string Operacao = string.Empty;
        public string seqOperacao = string.Empty;
        public bool edita = false;

        public TipoRecurso tipo;

        public enum TipoRecurso
        {
            Componente = 1,
            Equipamento = 2,
            MaodeObra = 3,
            Ferramenta = 4,
            ServicoIndustrializacao = 5
        }

        public FrmEstruturaRecursoComponentes(string codEstrutura, string CodRevEstrutura, string seqOperacao, string Operacao, TipoRecurso tipo, bool edita, string CodProduto)
        {
            InitializeComponent();

            this.codEstrutura = codEstrutura;
            this.CodRevEstrutura = CodRevEstrutura;
            this.seqOperacao = seqOperacao;
            this.Operacao = Operacao;
            this.tipo = tipo;

            lookupproduto.txtcodigo.Leave += new System.EventHandler(lookupproduto_txtcodigo_Leave);
            lookupproduto.btnprocurar.Click += new System.EventHandler(lookupproduto_txtcodigo_Leave);
            lookupproduto.txtconteudo.Click += new System.EventHandler(lookupproduto_txtcodigo_Leave);

            if (edita == true)
            {
                CarregaCampos(codEstrutura, CodRevEstrutura, seqOperacao, Operacao, tipo, CodProduto);
            }

        }

        private void CarregaCampos(string codEstrutura, string CodRevEstrutura, string seqOperacao, string Operacao, TipoRecurso tipo, string CodProduto)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PROTEIRORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND TIPORECURSO = ? AND CODRECROTEIRO = ? AND CODOPERACAO = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura,Convert.ToInt16(tipo),CodProduto, Operacao,seqOperacao });
            if (dt.Rows.Count > 0)
            {
                lookupproduto.txtcodigo.Text = dt.Rows[0]["CODCOMPONENTE"].ToString();
                lookupproduto.CarregaDescricao();

                lookupunidade.txtcodigo.Text = dt.Rows[0]["UNDCOMPONENTE"].ToString();
                lookupunidade.CarregaDescricao();

                txtquantidade.Text = dt.Rows[0]["QTDCOMPONENTE"].ToString();
                txtCodRecRoteiro.Text = dt.Rows[0]["CODRECROTEIRO"].ToString();
            }
        }

        private void lookupproduto_txtcodigo_Leave(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(lookupproduto.ValorCodigoInterno))
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODUNIDCONTROLE FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ? ", new object[] { AppLib.Context.Empresa, lookupproduto.ValorCodigoInterno });
                if (dt.Rows.Count > 0)
                {
                    lookupunidade.txtcodigo.Text = dt.Rows[0]["CODUNIDCONTROLE"].ToString();
                    lookupunidade.CarregaDescricao();
                    lookupunidade.Edita(false);
                }
            }
        }

        private bool validacao()
        {
            bool verifica = true;

            errorProvider.Clear();

            lookupproduto.mensagemErrorProvider = "";
            lookupunidade.mensagemErrorProvider = "";
           

            if (string.IsNullOrEmpty(lookupproduto.txtconteudo.Text))
            {
                lookupproduto.mensagemErrorProvider = "Favor Selecionar um Produto";
                verifica = false;
            }
            else
            {
                lookupproduto.mensagemErrorProvider = "";
            }

            if (string.IsNullOrEmpty(txtquantidade.Text))
            {
                errorProvider.SetError(txtquantidade, "Favor preencher a Quantidade");
                verifica = false;
            }

            return verifica;

        }
        
        private void LimpaCampos()
        {
            lookupunidade.Clear();
            lookupproduto.Clear();
            txtquantidade.Text = "";
            txtCodRecRoteiro.Text = "";
        }

        private bool Salvar()
        {
            bool _salvar = false;

            if (validacao() == true)
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PROTEIRORECURSO");
                conn.BeginTransaction();
                try
                {
                    v.Set("CODEMPRESA", AppLib.Context.Empresa);
                    v.Set("CODFILIAL", AppLib.Context.Filial);
                    v.Set("CODESTRUTURA", codEstrutura.ToString());
                    v.Set("REVESTRUTURA", Convert.ToInt16(CodRevEstrutura));
                    v.Set("SEQOPERACAO", Convert.ToInt16(seqOperacao).ToString("000"));
                    v.Set("CODOPERACAO", Operacao.ToString());

                    switch (this.tipo)
                    {
                        case  TipoRecurso.Componente: //1
                            v.Set("TIPORECURSO", Convert.ToInt16(this.tipo));
                            v.Set("CODGRUPORECURSO", null);
                            v.Set("CODRECROTEIRO", lookupproduto.ValorCodigoInterno.ToString());
                            v.Set("CODCOMPONENTE", lookupproduto.ValorCodigoInterno.ToString());
                            v.Set("QTDRECURSO", null);
                            v.Set("UNDCOMPONENTE", lookupunidade.ValorCodigoInterno.ToString());
                            v.Set("QTDCOMPONENTE", Convert.ToDecimal(txtquantidade.Text));

                            break;

                        //case  TipoRecurso.Equipamento: //2
                        //    break;

                        //case TipoRecurso.MaodeObra: //3
                        //    break;

                        //case  TipoRecurso.Ferramenta: //4
                        //    break;

                        case  TipoRecurso.ServicoIndustrializacao: //5
                            v.Set("TIPORECURSO", Convert.ToInt16(this.tipo));
                            v.Set("CODGRUPORECURSO", null);
                            v.Set("CODRECROTEIRO", lookupproduto.ValorCodigoInterno.ToString());
                            v.Set("CODCOMPONENTE", lookupproduto.ValorCodigoInterno.ToString());
                            v.Set("QTDRECURSO", null);
                            v.Set("UNDCOMPONENTE", lookupunidade.ValorCodigoInterno.ToString());
                            v.Set("QTDCOMPONENTE", Convert.ToDecimal(txtquantidade.Text));
                            break;

                        default:
                            break;

                    }
                    v.Save();
                    
                    conn.Commit();

                    LimpaCampos();
                    _salvar = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Rollback();
                }
            }
            else
            {
                _salvar = false;
            }

            return _salvar;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                this.Dispose();
            }
        }

        private void FrmEstruturaRecursoComponentes_Load(object sender, EventArgs e)
        {
            lookupunidade.Edita(false);

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
