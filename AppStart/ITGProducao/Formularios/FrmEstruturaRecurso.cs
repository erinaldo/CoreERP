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
    public partial class FrmEstruturaRecurso : Form
    {
        public string codEstrutura = string.Empty;
        public string CodRevEstrutura = string.Empty;
        public string Operacao = string.Empty;
        public string seqOperacao = string.Empty;
        public string CentroTrabalho = string.Empty;
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

        public FrmEstruturaRecurso(string codEstrutura, string CodRevEstrutura, string seqOperacao, string Operacao, TipoRecurso tipo, bool edita, string CodGrupoRecurso,string CentroTrabalho)
        {
            InitializeComponent();

            this.codEstrutura = codEstrutura;
            this.CodRevEstrutura = CodRevEstrutura;
            this.seqOperacao = seqOperacao;
            this.Operacao = Operacao;
            this.CentroTrabalho = CentroTrabalho;
            this.tipo = tipo;

            switch (tipo)
            {
                //case TipoRecurso.Componente:
                //    break;
                case TipoRecurso.Equipamento:
                    lookgruporecurso.Grid_WhereVisao[3].ValorFixo = "EQ";
                    break;
                case TipoRecurso.Ferramenta:
                    lookgruporecurso.Grid_WhereVisao[3].ValorFixo = "FE";
                    break;
                case TipoRecurso.MaodeObra:
                    lookgruporecurso.Grid_WhereVisao[3].ValorFixo = "MO";
                    break;

                //case TipoRecurso.ServicoIndustrializacao:
                //    break;
            }

            if (edita == true)
            {
                CarregaCampos(codEstrutura, CodRevEstrutura, seqOperacao, Operacao, tipo, CodGrupoRecurso);
            }
        }

        private void CarregaCampos(string codEstrutura, string CodRevEstrutura, string seqOperacao, string Operacao, TipoRecurso tipo, string CodGrupoRecurso)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PROTEIRORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND TIPORECURSO = ? AND CODRECROTEIRO = ? AND CODOPERACAO = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, Convert.ToInt16(tipo), CodGrupoRecurso, Operacao, seqOperacao });
            if (dt.Rows.Count > 0)
            {
                lookgruporecurso.txtcodigo.Text = dt.Rows[0]["CODGRUPORECURSO"].ToString();
                lookgruporecurso.CarregaDescricao();

                txtquantidade.Text = dt.Rows[0]["QTDRECURSO"].ToString();
                txtCodRecRoteiro.Text = dt.Rows[0]["CODRECROTEIRO"].ToString();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                this.Dispose();
            }
        }

        private bool validacao()
        {
            bool verifica = true;

            errorProvider.Clear();

            lookgruporecurso.mensagemErrorProvider = "";


            if (string.IsNullOrEmpty(lookgruporecurso.txtconteudo.Text))
            {
                lookgruporecurso.mensagemErrorProvider = "Favor Selecionar um Grupo de Recurso";
                verifica = false;
            }
            else
            {
                lookgruporecurso.mensagemErrorProvider = "";
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PGRUPORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODGRUPORECURSO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookgruporecurso.ValorCodigoInterno.ToString() });
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dt.Rows[0]["CTRABALHOFIXO"]) == true)
                    {
                        if (dt.Rows[0]["CODCTRABALHO"].ToString() != CentroTrabalho)
                        {
                            lookgruporecurso.mensagemErrorProvider = "O Centro de Trabalho para a Operação selecionada é fixo e diferente da escolhida para este roteiro e não pode ser incluida";
                            verifica = false;
                        }
                    }
                }
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
            lookgruporecurso.Clear();
            txtquantidade.Text = "";
            txtCodRecRoteiro.Text = "";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
                        //case TipoRecurso.Componente: //1
                        //    break;

                        case  TipoRecurso.Equipamento: //2
                            v.Set("TIPORECURSO", Convert.ToInt16(this.tipo));
                            v.Set("CODGRUPORECURSO", lookgruporecurso.ValorCodigoInterno.ToString());
                            v.Set("CODRECROTEIRO", lookgruporecurso.ValorCodigoInterno.ToString());
                            v.Set("CODCOMPONENTE", null);
                            v.Set("QTDRECURSO", Convert.ToInt16(txtquantidade.Text));
                            v.Set("UNDCOMPONENTE", null);
                            v.Set("QTDCOMPONENTE", null);
                            break;

                        case TipoRecurso.MaodeObra: //3
                            v.Set("TIPORECURSO", Convert.ToInt16(this.tipo));
                            v.Set("CODGRUPORECURSO", lookgruporecurso.ValorCodigoInterno.ToString());
                            v.Set("CODRECROTEIRO", lookgruporecurso.ValorCodigoInterno.ToString());
                            v.Set("CODCOMPONENTE", null);
                            v.Set("QTDRECURSO", Convert.ToDecimal(txtquantidade.Text));
                            v.Set("UNDCOMPONENTE", null);
                            v.Set("QTDCOMPONENTE", null);
                            break;

                        case  TipoRecurso.Ferramenta: //4
                            v.Set("TIPORECURSO", Convert.ToInt16(this.tipo));
                            v.Set("CODGRUPORECURSO", lookgruporecurso.ValorCodigoInterno.ToString());
                            v.Set("CODRECROTEIRO", lookgruporecurso.ValorCodigoInterno.ToString());
                            v.Set("CODCOMPONENTE", null);
                            v.Set("QTDRECURSO", Convert.ToDecimal(txtquantidade.Text));
                            v.Set("UNDCOMPONENTE", null);
                            v.Set("QTDCOMPONENTE", null);
                            break;

                        //case TipoRecurso.ServicoIndustrializacao: //5
                        //    break;

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

        private void FrmEstruturaRecurso_Load(object sender, EventArgs e)
        {

        }
    }
}
