using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PS.Lib;
using PS.Glb;
using ITGProducao.Formularios;
using ITGProducao.Filtros;
using DevExpress.XtraTabbedMdi;
using DevExpress.XtraBars;
using ITGProducao.Visao;
using PS.Glb.Class;
using System.Collections;
using DevExpress.XtraBars.Ribbon;
using DevExpress.Utils.Menu;

namespace ERP
{
    public partial class MDIPrincipal : Form
    {
        public string condicao = string.Empty;
        public string Codmenu = string.Empty;
        public bool Cancel { get; set; }

        DllCustomizadoTeste.ClsCustomizado clsCustomizado = new DllCustomizadoTeste.ClsCustomizado();

        private Global gb;

        // Recálculo CFOP
        private decimal vlFrete = 0, vlDesconto = 0, vlDespesa = 0, vlSeguro = 0;
        public bool faturamento = false;
        private PS.Lib.Interpretador interpreta = new PS.Lib.Interpretador();
        private PS.Glb.Formula.Function function = new PS.Glb.Formula.Function();

        private string Modulo;

        public MDIPrincipal()
        {
            InitializeComponent();
        }

        private void btnCadastros_Calendario_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmFiltroCalendario frm = new FrmFiltroCalendario();
            frm.pai = this;
            frm.Show();
        }

        private void btnCadastros_CentroTrabalho_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmFiltroCentroTrabalho frm = new FrmFiltroCentroTrabalho();
            frm.pai = this;
            frm.Show();
        }

        private void btnCadastros_Marca_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmFiltroMarca frm = new FrmFiltroMarca();
            frm.pai = this;
            frm.Show();
        }

        private void btnCadastros_Operacao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmFiltroOperacao frm = new FrmFiltroOperacao();
            frm.pai = this;
            frm.Show();
        }

        private void btnCadastros_GrupoRecurso_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmFiltroGrupoRecurso frm = new FrmFiltroGrupoRecurso();
            frm.pai = this;
            frm.Show();
        }

        private void btnCadastros_Recurso_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmFiltroRecurso frm = new FrmFiltroRecurso();
            frm.pai = this;
            frm.Show();
        }

        private void MDIPrincipal_Load(object sender, EventArgs e)
        {
            XtraTabbedMdiManager mdiManager = new XtraTabbedMdiManager();
            mdiManager.MdiParent = this;

            ValidaSessao();

            DesabilitaBotoes();
            DesabilitaModulo();

            verificaModulo();
            PermissaoModulo();
            desabilitaMenu();
            verificaAcessoMenu();
            HabilitaModulo();
            habilitaMenu(Modulo);
        }

        private void btnCadastros_Estrutura_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmFiltroEstrutura frm = new FrmFiltroEstrutura();
            frm.pai = this;
            frm.Show();
        }

        private void btnCadastros_OrdemProducao_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmOrdemProducao frm = new FrmOrdemProducao();
            //frm.pai = this;
            frm.Show();
        }

        private void ValidaSessao()
        {
            if (Contexto.Session.CodUsuario == null)
            {
                return;
            }
            else
            {
                PS.Lib.Login login = new PS.Lib.Login();
                gb = new Global();

                DataTable dt = login.EmpresaUserList();

                // Se o usuário tem permissão para mais de uma empresa então abre a tela de selação
                if (dt.Rows.Count > 1)
                {
                    FormSelecaoEmpresa frmSelecionaEmpresa = new FormSelecaoEmpresa();
                    frmSelecionaEmpresa.ShowDialog();
                }
                else
                {
                    // Se o usuário tem acesso a apenas uma empresa então não abre a tela de seleção
                    if (dt.Rows.Count == 1)
                    {
                        Empresa emp = new Empresa();
                        emp.CodEmpresa = int.Parse(dt.Rows[0][0].ToString());
                        emp.NomeFantasia = dt.Rows[0][1].ToString();
                        emp.Nome = dt.Rows[0][2].ToString();
                        emp.CNPJCPF = dt.Rows[0][3].ToString();
                        emp.InscricaoEstadual = dt.Rows[0][4].ToString();
                        emp.CodControle = dt.Rows[0][5].ToString();
                        emp.CodChave1 = dt.Rows[0][6].ToString();
                        emp.CodChave2 = dt.Rows[0][7].ToString();

                        Contexto.Session.Empresa = emp;
                        AppLib.Context.Empresa = emp.CodEmpresa;
                        Contexto.Session.Empresa.GetPerfilList();
                    }
                }

                if (Contexto.Session.Empresa != null)
                {
                    //this.Text = string.Concat(" | ", Contexto.Session.Empresa.nomeFantasia);
                    Empresa.Caption = string.Concat("Empresa: ", Contexto.Session.Empresa.NomeFantasia);
                    Usuário.Caption = string.Concat("Usuário: ", Contexto.Session.CodUsuario);
                    Alias.Caption = string.Concat("Alias: ", PS.Lib.Contexto.Alias.Name);

                }
                else
                {
                    //this.Text = "ERP";
                    Empresa.Caption = string.Concat("Empresa: ", string.Empty);
                    Usuário.Caption = string.Concat("Usuário: ", Contexto.Session.CodUsuario);
                    Alias.Caption = string.Concat("Alias: ", PS.Lib.Contexto.Alias.Name);
                }
            }
        }

        private bool verificaModulo()
        {
            string[] codperfil;
            codperfil = new string[1];
            string a = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODPERFIL FROM GUSUARIOPERFIL WHERE CODUSUARIO = ? AND CODEMPRESA = ?", new object[] { AppLib.Context.Usuario, AppLib.Context.Empresa }).ToString();
            codperfil[0] = a;

            if (!string.IsNullOrEmpty(codperfil[0]))
            {
                Contexto.Session.CodPerfil = codperfil;
                AppLib.Context.Perfil = a;
            }

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GMODULOEMPRESA WHERE CODEMPRESA = ? AND ATIVO = 1", new object[] { AppLib.Context.Empresa });
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (dt.Rows[i]["CODMODULO"].ToString())
                    {
                        case "ADM":
                            bsiGestaoAdministracao.Enabled = true;
                            bsiPersonalizado.Enabled = true;
                            break;
                        case "CON":
                            bsiGestaoContabil.Enabled = true;
                            break;
                        case "FIN":
                            bsiGestaoFinanceira.Enabled = true;
                            break;
                        case "FIS":
                            bsiGestaoFiscal.Enabled = true;
                            break;
                        case "INC":
                            bsiGestaoIncorporacoes.Enabled = true;
                            break;
                        case "MAT":
                            bsiGestaoMateriais.Enabled = true;
                            break;
                        case "PRO":
                            bsiGestaoProducao.Enabled = true;
                            break;
                        case "RH":
                            bsiGestaoRh.Enabled = true;
                            break;
                        case "SER":
                            bsiGestaoServicos.Enabled = true;
                            break;
                        case "PAT":
                            bsiGestaoPatrimonial.Enabled = true;
                            break;
                        case "PRJ":
                            bsiGestaoProjetos.Enabled = true;
                            break;
                        case "CRM":
                            bsiGestaoAtendimento.Enabled = true;
                            break;
                        default:
                            break;
                    }
                }
                return true;
            }
            else
            {
                MessageBox.Show("Não foi possível verificar os modulos de acesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void DesabilitaModulo()
        {
            bsiGestaoAdministracao.Visibility = BarItemVisibility.Never;
            bsiGestaoContabil.Visibility = BarItemVisibility.Never;
            bsiGestaoFinanceira.Visibility = BarItemVisibility.Never;
            bsiGestaoFiscal.Visibility = BarItemVisibility.Never;
            bsiGestaoIncorporacoes.Visibility = BarItemVisibility.Never;
            bsiGestaoMateriais.Visibility = BarItemVisibility.Never;
            bsiGestaoPatrimonial.Visibility = BarItemVisibility.Never;
            bsiGestaoProducao.Visibility = BarItemVisibility.Never;
            bsiGestaoRh.Visibility = BarItemVisibility.Never;
            bsiGestaoServicos.Visibility = BarItemVisibility.Never;
            bsiPersonalizado.Visibility = BarItemVisibility.Never;
            bsiGestaoProjetos.Visibility = BarItemVisibility.Never;
            bsiGestaoAtendimento.Visibility = BarItemVisibility.Never;
        }

        private void PermissaoModulo()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GPERMISSAOMODULO WHERE CODPERFIL = ? AND ACESSO = 1", new object[] { AppLib.Context.Perfil });

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (dt.Rows[i]["CODMODULO"].ToString())
                    {
                        case "ADM":
                            bsiGestaoAdministracao.Visibility = BarItemVisibility.Always;
                            break;
                        case "CON":
                            bsiGestaoContabil.Visibility = BarItemVisibility.Always;
                            break;
                        case "FIN":
                            bsiGestaoFinanceira.Visibility = BarItemVisibility.Always;
                            break;
                        case "FIS":
                            bsiGestaoFiscal.Visibility = BarItemVisibility.Always;
                            break;
                        case "INC":
                            bsiGestaoIncorporacoes.Visibility = BarItemVisibility.Always;
                            break;
                        case "MAT":
                            bsiGestaoMateriais.Visibility = BarItemVisibility.Always;
                            break;
                        case "PRO":
                            bsiGestaoProducao.Visibility = BarItemVisibility.Always;
                            break;
                        case "RH":
                            bsiGestaoRh.Visibility = BarItemVisibility.Always;
                            break;
                        case "SER":
                            bsiGestaoServicos.Visibility = BarItemVisibility.Always;
                            break;
                        case "PAT":
                            bsiGestaoPatrimonial.Visibility = BarItemVisibility.Always;
                            break;
                        case "PER":
                            bsiPersonalizado.Visibility = BarItemVisibility.Always;
                            break;
                        case "ARQ":
                            bsiGestaoArquivos.Visibility = BarItemVisibility.Always;
                            break;
                        case "PRJ":
                            bsiGestaoProjetos.Visibility = BarItemVisibility.Always;
                            break;
                        case "CRM":
                            bsiGestaoAtendimento.Visibility = BarItemVisibility.Always;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void verificaAcessoMenu()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GPERMISSAOMENU WHERE CODPERFIL = ? AND ACESSO = 1", new object[] { AppLib.Context.Perfil });
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (dt.Rows[i]["CODMENU"].ToString())
                    {
                        case "btnMovimentacoesBancariass_TipoDocumento":
                            btnMovimentacoesBancariass_TipoDocumento.Enabled = true;
                            break;
                        case "btnClassificacao_FormaPagamento":
                            btnClassificacao_FormaPagamento.Enabled = true;
                            break;
                        case "btnLancamentos_ExtratoCaixa":
                            btnLancamentos_ExtratoCaixa.Enabled = true;
                            break;
                        case "btnCadastros_Calendario":
                            btnCadastros_Calendario.Enabled = true;
                            break;
                        case "btnCadastrosGlobais_ClientesFornecedores":
                            btnCadastrosGlobais_ClientesFornecedores.Enabled = true;
                            break;
                        case "btnCadastrosGlobais_Produtos":
                            btnCadastrosGlobais_Produtos.Enabled = true;
                            break;
                        case "btnCadastrosGlobais_Estado":
                            btnCadastrosGlobais_Estado.Enabled = true;
                            break;
                        case "btnCadastrosGlobais_Municipio":
                            btnCadastrosGlobais_Municipio.Enabled = true;
                            break;
                        case "btnCadastrosGlobais_Pais":
                            btnCadastrosGlobais_Pais.Enabled = true;
                            break;
                        case "btnCadastrosGlobais_CentrodeCusto":
                            btnCadastrosGlobais_CentrodeCusto.Enabled = true;
                            break;
                        case "btnCadastrosGlobais_NaturezaOrcamentaria":
                            btnCadastrosGlobais_NaturezaOrcamentaria.Enabled = true;
                            break;
                        case "btnGlobais_Departamentos":
                            btnGlobais_Departamentos.Enabled = true;
                            break;
                        case "btnGlobais_Calendario":
                            btnGlobais_Calendario.Enabled = true;
                            break;
                        case "btnCadastrosGlobais_MoedaseIndices":
                            btnCadastrosGlobais_MoedaseIndices.Enabled = true;
                            break;
                        case "btnCadastrosGlobais_Operador":
                            btnCadastrosGlobais_Operador.Enabled = true;
                            break;
                        case "btnLancamentos_PagarReceber":
                            btnLancamentos_PagarReceber.Enabled = true;
                            break;
                        case "btnMovimentacoesBancarias_ContaCaixa":
                            btnMovimentacoesBancarias_ContaCaixa.Enabled = true;
                            break;
                        case "btnMovimentacoesBancarias_Convenio":
                            btnMovimentacoesBancarias_Convenio.Enabled = true;
                            break;
                        case "btnMovimentacoesBancarias_Bancos":
                            btnMovimentacoesBancarias_Bancos.Enabled = true;
                            break;
                        case "btnMovimentacoesBancarias_Agencia":
                            btnMovimentacoesBancarias_Agencia.Enabled = true;
                            break;
                        case "btnMovimentacoesBancarias_ContaCorrente":
                            btnMovimentacoesBancarias_ContaCorrente.Enabled = true;
                            break;
                        case "btnMovimentacoesBancarias_CobrancaEletronica":
                            btnMovimentacoesBancarias_CobrancaEletronica.Enabled = true;
                            break;
                        case "btnMovimentacoesBancarias_PagamentoEletronico":
                            btnMovimentacoesBancarias_PagamentoEletronico.Enabled = true;
                            break;
                        case "btnSessaoCaixa_Abertura":
                            btnSessaoCaixa_Abertura.Enabled = true;
                            break;
                        case "btnSessaoCaixa_Movimentacao":
                            btnSessaoCaixa_Movimentacao.Enabled = true;
                            break;
                        case "btnSessaoCaixa_Encerramento":
                            btnSessaoCaixa_Encerramento.Enabled = true;
                            break;
                        case "btnSessaoCaixa_Gerencial":
                            btnSessaoCaixa_Gerencial.Enabled = true;
                            break;
                        case "btnGestaoFinanceira_RelatoriosModulo":
                            btnGestaoFinanceira_RelatorioInterno.Enabled = true;
                            break;
                        case "btnGestaoFinanceira_Utilitarios":
                            btnGestaoFinanceira_Utilitarios.Enabled = true;
                            break;
                        case "btnCadastrosFiscais_NaturezaOperacao":
                            btnCadastrosFiscais_NaturezaOperacao.Enabled = true;
                            break;
                        case "btnCadastrosFiscais_Tributos":
                            btnCadastrosFiscais_Tributos.Enabled = true;
                            break;
                        case "btnCadastrosFiscais_RegrasIcms":
                            btnCadastrosFiscais_RegrasIcms.Enabled = true;
                            break;
                        case "btnCadastrosFiscais_RegrasIpi":
                            btnCadastrosFiscais_RegrasIpi.Enabled = true;
                            break;
                        case "btnCadastrosFiscais_Regra":
                            btnCadastrosFiscais_Regra.Enabled = true;
                            break;
                        case "btnCadastrosFiscais_Regiao":
                            btnCadastrosFiscais_Regiao.Enabled = true;
                            break;
                        case "btnCadastro_CondicaoPagamento":
                            btnCadastro_CondicaoPagamento.Enabled = true;
                            break;
                        case "btnCadastro_Fabricante":
                            btnCadastro_Fabricante.Enabled = true;
                            break;
                        case "btnClassificacao_LocalEstoque":
                            btnClassificacao_LocalEstoque.Enabled = true;
                            break;
                        case "btnCadastro_MensagensOperacao":
                            btnCadastro_MensagensOperacao.Enabled = true;
                            break;
                        case "btnCadastro_Motivo":
                            btnCadastro_Motivo.Enabled = true;
                            break;
                        case "btnCadastro_Representante":
                            btnCadastro_Representante.Enabled = true;
                            break;
                        case "btnCadastro_Serie":
                            btnCadastro_Serie.Enabled = true;
                            break;
                        case "btnCadastro_Transportadora":
                            btnCadastro_Transportadora.Enabled = true;
                            break;
                        case "btnCadastro_TipoTransporte":
                            btnCadastro_TipoTransporte.Enabled = true;
                            break;
                        case "btnCadastro_UnidadeMedida":
                            btnCadastro_UnidadeMedida.Enabled = true;
                            break;
                        case "btnCadastro_Vendedor":
                            btnCadastro_Vendedor.Enabled = true;
                            break;
                        case "btnOperacoes_Estoque":
                            btnOperacoes_Estoque.Enabled = true;
                            break;
                        case "btnOperacoes_Entradas":
                            btnOperacoes_Entradas.Enabled = true;
                            break;
                        case "btnOperacoes_Saidas":
                            btnOperacoes_Saidas.Enabled = true;
                            break;
                        case "btnOperacoes_OutrasOperacoes":
                            btnOperacoes_OutrasOperacoes.Enabled = true;
                            break;
                        case "btnOperacoes_NotaFiscalEstadual":
                            btnOperacoes_NotaFiscalEstadual.Enabled = true;
                            break;
                        case "btnGestaoMateriais_RelatoriosModulo":
                            btnGestaoMateriais_RelatoriosModulo.Enabled = true;
                            break;
                        case "btnUtilitarios_ImportadorIbptax":
                            btnUtilitarios_ImportadorIbptax.Enabled = true;
                            break;
                        case "btnGestaoContabil_RelatoriosModulo":
                            btnGestaoContabil_RelatoriosModulo.Enabled = true;
                            break;
                        case "btnGestaoContabil_Utilitarios":
                            btnGestaoContabil_Utilitarios.Enabled = true;
                            break;
                        case "btnGestaoRh_RelatoriosModulo":
                            btnGestaoRh_RelatoriosModulo.Enabled = true;
                            break;
                        case "btnGestaoRh_Utilitarios":
                            btnGestaoRh_Utilitarios.Enabled = true;
                            break;
                        case "btnCadastros_CentroTrabalho":
                            btnCadastros_CentroTrabalho.Enabled = true;
                            break;
                        case "btnCadastros_Marca":
                            btnCadastros_Marca.Enabled = true;
                            break;
                        case "btnCadastros_Operacao":
                            btnCadastros_Operacao.Enabled = true;
                            break;
                        case "btnCadastros_GrupoRecurso":
                            btnCadastros_GrupoRecurso.Enabled = true;
                            break;
                        case "btnCadastros_Recurso":
                            btnCadastros_Recurso.Enabled = true;
                            break;
                        case "btnCadastros_MotivoParada":
                            btnCadastros_MotivoParada.Enabled = true;
                            break;
                        case "btnCadastros_Estrutura":
                            btnCadastros_Estrutura.Enabled = true;
                            break;
                        case "btnControleProducao_OrdemProducao":
                            btnControleProducao_OrdemProducao.Enabled = true;
                            break;
                        case "btnControleProducao_SequenciamentoProducao":
                            btnControleProducao_SequenciamentoProducao.Enabled = true;
                            break;
                        case "btnControleProducao_ApontamentoProducao":
                            btnControleProducao_ApontamentoProducao.Enabled = true;
                            break;
                        case "btnControleProducao_ApontamentoRefugo":
                            btnControleProducao_ApontamentoRefugo.Enabled = true;
                            break;
                        case "btnControleProducao_TerminalApontamento":
                            btnControleProducao_TerminalApontamento.Enabled = true;
                            break;
                        case "btnControleProducao_MonitorRecursos":
                            btnControleProducao_MonitorRecursos.Enabled = true;
                            break;
                        case "btnGestaoProducao_RelatorioInterno":
                            btnGestaoProducao_RelatorioInterno.Enabled = true;
                            break;
                        case "btnGestaoProducao_RelatorioCustomizado":
                            btnGestaoProducao_RelatorioCustomizado.Enabled = true;
                            break;
                        case "btnGestaoProducao_ConsultaSql":
                            btnGestaoProducao_ConsultaSql.Enabled = true;
                            break;
                        case "btnGestaoProducao_Planilha":
                            btnGestaoProducao_Planilha.Enabled = true;
                            break;
                        case "btnGestaoProducao_Cubo":
                            btnGestaoProducao_Cubo.Enabled = true;
                            break;
                        case "btnGestaoProducao_Dashboard":
                            btnGestaoProducao_Dashboard.Enabled = true;
                            break;
                        case "btnGestaoProducao_Modulos":
                            btnGestaoProducao_Modulos.Enabled = true;
                            break;
                        case "btnGestaoProducao_Utilitarios":
                            btnGestaoProducao_Utilitarios.Enabled = true;
                            break;
                        case "btnContratos_Vendas":
                            btnContratos_Vendas.Enabled = true;
                            break;
                        case "btnContratos_Alugueis":
                            btnContratos_Alugueis.Enabled = true;
                            break;
                        case "btnContratos_Aditivos":
                            btnContratos_Aditivos.Enabled = true;
                            break;
                        case "btnGestaoIncorporacoes_RelatoriosModulo":
                            btnGestaoIncorporacoes_RelatoriosModulo.Enabled = true;
                            break;
                        case "btnUtilitarios_Dimob":
                            btnUtilitarios_Dimob.Enabled = true;
                            break;
                        case "btnOperacoes_Ticket":
                            btnOperacoes_Ticket.Enabled = true;
                            break;
                        case "btnOperacoes_Propostas":
                            btnOperacoes_Propostas.Enabled = true;
                            break;
                        case "btnOperacoes_Projetos":
                            btnOperacoes_Projetos.Enabled = true;
                            break;
                        case "btnOperacoes_Mensalidades":
                            btnOperacoes_Mensalidades.Enabled = true;
                            break;
                        case "btnAtendimento_Agendamento":
                            btnAtendimento_Agendamento.Enabled = true;
                            break;
                        case "btnAtendimento_Agendas":
                            btnAtendimento_Agendas.Enabled = true;
                            break;
                        case "btnGestaoServico_RelatoriosModulo":
                            btnGestaoServico_RelatoriosModulo.Enabled = true;
                            break;
                        case "btnGestaoServico_Utilitarios":
                            btnGestaoServico_Utilitarios.Enabled = true;
                            break;
                        case "btnCadastros_Empresa":
                            btnCadastros_Empresa.Enabled = true;
                            break;
                        case "btnCadastros_Filial":
                            btnCadastros_Filial.Enabled = true;
                            break;
                        case "btnCadastros_Status":
                            btnCadastros_Status.Enabled = true;
                            break;
                        case "btnCadastros_Situacao":
                            btnCadastros_Situacao.Enabled = true;
                            break;
                        case "btnSeguranca_Usuarios":
                            btnSeguranca_Usuarios.Enabled = true;
                            break;
                        case "btnSeguranca_Perfis":
                            btnSeguranca_Perfis.Enabled = true;
                            break;
                        case "btnParametros_ParametrosModulos":
                            btnParametros_ParametrosModulos.Enabled = true;
                            break;
                        case "btnParametros_ParametrosOperacoes":
                            btnParametros_ParametrosOperacoes.Enabled = true;
                            break;
                        case "btnGestaoAdminstracao_TodosRelatorios":
                            btnGestaoAdminstracao_RelatorioInterno.Enabled = true;
                            break;
                        case "btnUtilitarios_AlterarSenha":
                            btnUtilitarios_AlterarSenha.Enabled = true;
                            break;
                        case "btnLancamentos_Fatura":
                            btnLancamentos_Fatura.Enabled = true;
                            break;
                        case "btnLancamentos_FormaPagamento":
                            btnLancamentos_FormaPagamento.Enabled = true;
                            break;
                        case "btnLancamentos_Cheques":
                            btnLancamentos_Cheques.Enabled = true;
                            break;
                        case "btnGestaoFinanceira_RelatorioInterno":
                            btnGestaoFinanceira_RelatorioInterno.Enabled = true;
                            break;
                        case "btnGestaoFinanceira_RelatorioCustomizado":
                            btnGestaoFinanceira_RelatorioCustomizado.Enabled = true;
                            break;
                        case "btnGestaoFinanceira_ConsultaSql":
                            btnGestaoFinanceira_ConsultaSql.Enabled = true;
                            break;
                        case "btnGestaoFinanceira_Planilha":
                            btnGestaoFinanceira_Planilha.Enabled = true;
                            break;
                        case "btnGestaoFinanceira_Cubo":
                            btnGestaoFinanceira_Cubo.Enabled = true;
                            break;
                        case "btnGestaoFinanceira_Dashboard":
                            btnGestaoFinanceira_Dashboard.Enabled = true;
                            break;
                        case "btnSeguranca_UsuariosLib":
                            btnSeguranca_UsuariosLib.Enabled = true;
                            break;
                        case "btnSeguranca_PerfisLib":
                            btnSeguranca_PerfisLib.Enabled = true;
                            break;
                        case "btnGestaoMateriais_RelatorioCustomizado":
                            btnGestaoMateriais_RelatorioCustomizado.Enabled = true;
                            break;
                        case "btnGestaoMateriais_ConsultaSql":
                            btnGestaoMateriais_ConsultaSql.Enabled = true;
                            break;
                        case "btnGestaoMateriais_Planilha":
                            btnGestaoMateriais_Planilha.Enabled = true;
                            break;
                        case "btnGestaoMateriais_Cubo":
                            btnGestaoMateriais_Cubo.Enabled = true;
                            break;
                        case "btnGestaoMateriais_Dashboard":
                            btnGestaoMateriais_Dashboard.Enabled = true;
                            break;
                        case "btnGestaoMateriais_Modulos":
                            btnGestaoMateriais_Modulos.Enabled = true;
                            break;
                        case "btnGestaoMateriais_Operacoes":
                            btnGestaoMateriais_Operacoes.Enabled = true;
                            break;
                        case "btnGestaoAdminstracao_RelatorioInterno":
                            btnGestaoAdminstracao_RelatorioInterno.Enabled = true;
                            break;
                        case "btnGestaoAdminstracao_RelatorioCustomizado":
                            btnGestaoAdminstracao_RelatorioCustomizado.Enabled = true;
                            break;
                        case "btnGestaoAdminstracao_ConsultaSql":
                            btnGestaoAdminstracao_ConsultaSql.Enabled = true;
                            break;
                        case "btnGestaoAdminstracao_Planilha":
                            btnGestaoAdminstracao_Planilha.Enabled = true;
                            break;
                        case "btnGestaoAdminstracao_Cubo":
                            btnGestaoAdminstracao_Cubo.Enabled = true;
                            break;
                        case "btnGestaoAdminstracao_Dashboard":
                            btnGestaoAdminstracao_Dashboard.Enabled = true;
                            break;
                        case "btnGestaoAdminstracao_Modulos":
                            btnGestaoAdminstracao_Modulos.Enabled = true;
                            break;
                        case "btnGestaoFiscal_RelatorioInterno":
                            btnGestaoFiscal_RelatorioInterno.Enabled = true;
                            break;
                        case "btnGestaoFiscal_RelatorioCustomizado":
                            btnGestaoFiscal_RelatorioCustomizado.Enabled = true;
                            break;
                        case "btnGestaoFiscal_ConsultaSql":
                            btnGestaoFiscal_ConsultaSql.Enabled = true;
                            break;
                        case "btnGestaoFiscal_Planilha":
                            btnGestaoFiscal_Planilha.Enabled = true;
                            break;
                        case "btnGestaoFiscal_Cubo":
                            btnGestaoFiscal_Cubo.Enabled = true;
                            break;
                        case "btnGestaoFiscal_Dashboard":
                            btnGestaoFiscal_Dashboard.Enabled = true;
                            break;
                        case "btnGestaoFiscal_Modulo":
                            btnGestaoFiscal_Modulo.Enabled = true;
                            break;
                        case "btnCadastros_ConsultaSql":
                            btnCadastros_ConsultaSql.Enabled = true;
                            break;
                        case "btnCadastros_Formula":
                            btnCadastros_Formula.Enabled = true;
                            break;
                        case "btnCadastros_Campos":
                            btnCadastros_Campos.Enabled = true;
                            break;
                        case "btnCadastros_TabelaDinamica":
                            btnCadastros_TabelaDinamica.Enabled = true;
                            break;
                        case "btnUtilitarios_RecalculoSaldo":
                            btnUtilitarios_RecalculoSaldo.Enabled = true;
                            break;
                        case "btnGestaoAdminstracao_CamposObrigatorio":
                            btnGestaoAdminstracao_CamposObrigatorio.Enabled = true;
                            break;
                        case "btnUtilitarios_TabelaPreco":
                            btnUtilitarios_TabelaPreco.Enabled = true;
                            break;
                        case "btnUtilitarios_Inventario":
                            btnUtilitarios_Inventario.Enabled = true;
                            break;
                        case "btnUtilitarios_AtualizacaoEstoqueMinimo":
                            btnUtilitarios_AtualizacaoEstoqueMinimo.Enabled = true;
                            break;
                        case "btnCadastro_FatorConversao":
                            btnCadastro_FatorConversao.Enabled = true;
                            break;
                        case "btnUtilitarios_Lote":
                            btnUtilitarios_Lote.Enabled = true;
                            break;
                        case "btnOperacao_DDFe":
                            btnOperacao_DDFe.Enabled = true;
                            break;
                        case "btnAtendimento_Atendimento":
                            btnAtendimento_Atendimento.Enabled = true;
                            break;
                        case "btnUnidade":
                            btnUnidade.Enabled = true;
                            break;
                        case "btnProjeto":
                            btnProjeto.Enabled = true;
                            break;
                        case "btnApontamento":
                            btnApontamento.Enabled = true;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                //MessageBox.Show("Não foi possível carregar os acessos do seu usuário.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void InativaAcesso()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GPERMISSAOMENU WHERE CODPERFIL = ? AND ACESSO = 0", new object[] { AppLib.Context.Perfil });
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (dt.Rows[i]["CODMENU"].ToString())
                    {
                        case "btnMovimentacoesBancariass_TipoDocumento":
                            btnMovimentacoesBancariass_TipoDocumento.Enabled = false;
                            break;
                        case "btnClassificacao_FormaPagamento":
                            btnClassificacao_FormaPagamento.Enabled = false;
                            break;
                        case "btnMovimentacoesBancarias_ExtratoCaixa":
                            btnLancamentos_ExtratoCaixa.Enabled = false;
                            break;
                        case "btnCadastros_Calendario":
                            btnCadastros_Calendario.Enabled = false;
                            break;
                        case "btnCadastrosGlobais_ClientesFornecedores":
                            btnCadastrosGlobais_ClientesFornecedores.Enabled = false;
                            break;
                        case "btnCadastrosGlobais_Produtos":
                            btnCadastrosGlobais_Produtos.Enabled = false;
                            break;
                        case "btnCadastrosGlobais_Estado":
                            btnCadastrosGlobais_Estado.Enabled = false;
                            break;
                        case "btnCadastrosGlobais_Municipio":
                            btnCadastrosGlobais_Municipio.Enabled = false;
                            break;
                        case "btnCadastrosGlobais_Pais":
                            btnCadastrosGlobais_Pais.Enabled = false;
                            break;
                        case "btnCadastrosGlobais_CentrodeCusto":
                            btnCadastrosGlobais_CentrodeCusto.Enabled = false;
                            break;
                        case "btnCadastrosGlobais_NaturezaOrcamentaria":
                            btnCadastrosGlobais_NaturezaOrcamentaria.Enabled = false;
                            break;
                        case "btnGlobais_Departamentos":
                            btnGlobais_Departamentos.Enabled = false;
                            break;
                        case "btnGlobais_Calendario":
                            btnGlobais_Calendario.Enabled = false;
                            break;
                        case "btnCadastrosGlobais_MoedaseIndices":
                            btnCadastrosGlobais_MoedaseIndices.Enabled = false;
                            break;
                        case "btnCadastrosGlobais_Operador":
                            btnCadastrosGlobais_Operador.Enabled = false;
                            break;
                        case "btnLancamentos_PagarReceber":
                            btnLancamentos_PagarReceber.Enabled = false;
                            break;
                        case "btnMovimentacoesBancarias_ContaCaixa":
                            btnMovimentacoesBancarias_ContaCaixa.Enabled = false;
                            break;
                        case "btnMovimentacoesBancarias_Convenio":
                            btnMovimentacoesBancarias_Convenio.Enabled = false;
                            break;
                        case "btnMovimentacoesBancarias_Bancos":
                            btnMovimentacoesBancarias_Bancos.Enabled = false;
                            break;
                        case "btnMovimentacoesBancarias_Agencia":
                            btnMovimentacoesBancarias_Agencia.Enabled = false;
                            break;
                        case "btnMovimentacoesBancarias_ContaCorrente":
                            btnMovimentacoesBancarias_ContaCorrente.Enabled = false;
                            break;
                        case "btnMovimentacoesBancarias_CobrancaEletronica":
                            btnMovimentacoesBancarias_CobrancaEletronica.Enabled = false;
                            break;
                        case "btnMovimentacoesBancarias_PagamentoEletronico":
                            btnMovimentacoesBancarias_PagamentoEletronico.Enabled = false;
                            break;
                        case "btnSessaoCaixa_Abertura":
                            btnSessaoCaixa_Abertura.Enabled = false;
                            break;
                        case "btnSessaoCaixa_Movimentacao":
                            btnSessaoCaixa_Movimentacao.Enabled = false;
                            break;
                        case "btnSessaoCaixa_Encerramento":
                            btnSessaoCaixa_Encerramento.Enabled = false;
                            break;
                        case "btnSessaoCaixa_Gerencial":
                            btnSessaoCaixa_Gerencial.Enabled = false;
                            break;
                        case "btnGestaoFinanceira_RelatoriosModulo":
                            btnGestaoFinanceira_RelatorioInterno.Enabled = false;
                            break;
                        case "btnGestaoFinanceira_Utilitarios":
                            btnGestaoFinanceira_Utilitarios.Enabled = false;
                            break;
                        case "btnCadastrosFiscais_NaturezaOperacao":
                            btnCadastrosFiscais_NaturezaOperacao.Enabled = false;
                            break;
                        case "btnCadastrosFiscais_Tributos":
                            btnCadastrosFiscais_Tributos.Enabled = false;
                            break;
                        case "btnCadastrosFiscais_RegrasIcms":
                            btnCadastrosFiscais_RegrasIcms.Enabled = false;
                            break;
                        case "btnCadastrosFiscais_RegrasIpi":
                            btnCadastrosFiscais_RegrasIpi.Enabled = false;
                            break;
                        case "btnCadastrosFiscais_Regra":
                            btnCadastrosFiscais_Regra.Enabled = false;
                            break;
                        case "btnCadastrosFiscais_Regiao":
                            btnCadastrosFiscais_Regiao.Enabled = false;
                            break;
                        case "btnCadastro_CondicaoPagamento":
                            btnCadastro_CondicaoPagamento.Enabled = false;
                            break;
                        case "btnCadastro_Fabricante":
                            btnCadastro_Fabricante.Enabled = false;
                            break;
                        case "btnClassificacao_LocalEstoque":
                            btnClassificacao_LocalEstoque.Enabled = false;
                            break;
                        case "btnCadastro_MensagensOperacao":
                            btnCadastro_MensagensOperacao.Enabled = false;
                            break;
                        case "btnCadastro_Motivo":
                            btnCadastro_Motivo.Enabled = false;
                            break;
                        case "btnCadastro_Representante":
                            btnCadastro_Representante.Enabled = false;
                            break;
                        case "btnCadastro_Serie":
                            btnCadastro_Serie.Enabled = false;
                            break;
                        case "btnCadastro_Transportadora":
                            btnCadastro_Transportadora.Enabled = false;
                            break;
                        case "btnCadastro_TipoTransporte":
                            btnCadastro_TipoTransporte.Enabled = false;
                            break;
                        case "btnCadastro_UnidadeMedida":
                            btnCadastro_UnidadeMedida.Enabled = false;
                            break;
                        case "btnCadastro_Vendedor":
                            btnCadastro_Vendedor.Enabled = false;
                            break;
                        case "btnOperacoes_Estoque":
                            btnOperacoes_Estoque.Enabled = false;
                            break;
                        case "btnOperacoes_Entradas":
                            btnOperacoes_Entradas.Enabled = false;
                            break;
                        case "btnOperacoes_Saidas":
                            btnOperacoes_Saidas.Enabled = false;
                            break;
                        case "btnOperacoes_OutrasOperacoes":
                            btnOperacoes_OutrasOperacoes.Enabled = false;
                            break;
                        case "btnOperacoes_NotaFiscalEstadual":
                            btnOperacoes_NotaFiscalEstadual.Enabled = false;
                            break;
                        case "btnGestaoMateriais_RelatoriosModulo":
                            btnGestaoMateriais_RelatoriosModulo.Enabled = false;
                            break;
                        case "btnUtilitarios_ImportadorIbptax":
                            btnUtilitarios_ImportadorIbptax.Enabled = false;
                            break;
                        case "btnGestaoContabil_RelatoriosModulo":
                            btnGestaoContabil_RelatoriosModulo.Enabled = false;
                            break;
                        case "btnGestaoContabil_Utilitarios":
                            btnGestaoContabil_Utilitarios.Enabled = false;
                            break;
                        case "btnGestaoRh_RelatoriosModulo":
                            btnGestaoRh_RelatoriosModulo.Enabled = false;
                            break;
                        case "btnGestaoRh_Utilitarios":
                            btnGestaoRh_Utilitarios.Enabled = false;
                            break;
                        case "btnCadastros_CentroTrabalho":
                            btnCadastros_CentroTrabalho.Enabled = false;
                            break;
                        case "btnCadastros_Marca":
                            btnCadastros_Marca.Enabled = false;
                            break;
                        case "btnCadastros_Operacao":
                            btnCadastros_Operacao.Enabled = false;
                            break;
                        case "btnCadastros_GrupoRecurso":
                            btnCadastros_GrupoRecurso.Enabled = false;
                            break;
                        case "btnCadastros_Recurso":
                            btnCadastros_Recurso.Enabled = false;
                            break;
                        case "btnCadastros_MotivoParada":
                            btnCadastros_MotivoParada.Enabled = false;
                            break;
                        case "btnCadastros_Estrutura":
                            btnCadastros_Estrutura.Enabled = false;
                            break;
                        case "btnControleProducao_OrdemProducao":
                            btnControleProducao_OrdemProducao.Enabled = false;
                            break;
                        case "btnControleProducao_SequenciamentoProducao":
                            btnControleProducao_SequenciamentoProducao.Enabled = false;
                            break;
                        case "btnControleProducao_ApontamentoProducao":
                            btnControleProducao_ApontamentoProducao.Enabled = false;
                            break;
                        case "btnControleProducao_ApontamentoRefugo":
                            btnControleProducao_ApontamentoRefugo.Enabled = false;
                            break;
                        case "btnControleProducao_TerminalApontamento":
                            btnControleProducao_TerminalApontamento.Enabled = false;
                            break;
                        case "btnControleProducao_MonitorRecursos":
                            btnControleProducao_MonitorRecursos.Enabled = false;
                            break;
                        case "btnGestaoProducao_RelatorioInterno":
                            btnGestaoProducao_RelatorioInterno.Enabled = false;
                            break;
                        case "btnGestaoProducao_RelatorioCustomizado":
                            btnGestaoProducao_RelatorioCustomizado.Enabled = false;
                            break;
                        case "btnGestaoProducao_ConsultaSql":
                            btnGestaoProducao_ConsultaSql.Enabled = false;
                            break;
                        case "btnGestaoProducao_Planilha":
                            btnGestaoProducao_Planilha.Enabled = false;
                            break;
                        case "btnGestaoProducao_Cubo":
                            btnGestaoProducao_Cubo.Enabled = false;
                            break;
                        case "btnGestaoProducao_Dashboard":
                            btnGestaoProducao_Dashboard.Enabled = false;
                            break;
                        case "btnGestaoProducao_Modulos":
                            btnGestaoProducao_Modulos.Enabled = false;
                            break;
                        case "btnGestaoProducao_Utilitarios":
                            btnGestaoProducao_Utilitarios.Enabled = false;
                            break;
                        case "btnContratos_Vendas":
                            btnContratos_Vendas.Enabled = false;
                            break;
                        case "btnContratos_Alugueis":
                            btnContratos_Alugueis.Enabled = false;
                            break;
                        case "btnContratos_Aditivos":
                            btnContratos_Aditivos.Enabled = false;
                            break;
                        case "btnGestaoIncorporacoes_RelatoriosModulo":
                            btnGestaoIncorporacoes_RelatoriosModulo.Enabled = false;
                            break;
                        case "btnUtilitarios_Dimob":
                            btnUtilitarios_Dimob.Enabled = false;
                            break;
                        case "btnOperacoes_Ticket":
                            btnOperacoes_Ticket.Enabled = false;
                            break;
                        case "btnOperacoes_Propostas":
                            btnOperacoes_Propostas.Enabled = false;
                            break;
                        case "btnOperacoes_Projetos":
                            btnOperacoes_Projetos.Enabled = false;
                            break;
                        case "btnOperacoes_Mensalidades":
                            btnOperacoes_Mensalidades.Enabled = false;
                            break;
                        case "btnAtendimento_Agendamento":
                            btnAtendimento_Agendamento.Enabled = false;
                            break;
                        case "btnAtendimento_Agendas":
                            btnAtendimento_Agendas.Enabled = false;
                            break;
                        case "btnGestaoServico_RelatoriosModulo":
                            btnGestaoServico_RelatoriosModulo.Enabled = false;
                            break;
                        case "btnGestaoServico_Utilitarios":
                            btnGestaoServico_Utilitarios.Enabled = false;
                            break;
                        case "btnCadastros_Empresa":
                            btnCadastros_Empresa.Enabled = false;
                            break;
                        case "btnCadastros_Filial":
                            btnCadastros_Filial.Enabled = false;
                            break;
                        case "btnCadastros_Status":
                            btnCadastros_Status.Enabled = false;
                            break;
                        case "btnCadastros_Situacao":
                            btnCadastros_Situacao.Enabled = false;
                            break;
                        case "btnSeguranca_Usuarios":
                            btnSeguranca_Usuarios.Enabled = false;
                            break;
                        case "btnSeguranca_Perfis":
                            btnSeguranca_Perfis.Enabled = false;
                            break;
                        case "btnParametros_ParametrosModulos":
                            btnParametros_ParametrosModulos.Enabled = false;
                            break;
                        case "btnParametros_ParametrosOperacoes":
                            btnParametros_ParametrosOperacoes.Enabled = false;
                            break;
                        case "btnGestaoAdminstracao_TodosRelatorios":
                            btnGestaoAdminstracao_RelatorioInterno.Enabled = false;
                            break;
                        case "btnUtilitarios_AlterarSenha":
                            btnUtilitarios_AlterarSenha.Enabled = false;
                            break;
                        case "btnLancamentos_Fatura":
                            btnLancamentos_Fatura.Enabled = false;
                            break;
                        case "btnLancamentos_FormaPagamento":
                            btnLancamentos_FormaPagamento.Enabled = false;
                            break;
                        case "btnLancamentos_Cheques":
                            btnLancamentos_Cheques.Enabled = false;
                            break;
                        case "btnGestaoFinanceira_RelatorioInterno":
                            btnGestaoFinanceira_RelatorioInterno.Enabled = false;
                            break;
                        case "btnGestaoFinanceira_RelatorioCustomizado":
                            btnGestaoFinanceira_RelatorioCustomizado.Enabled = false;
                            break;
                        case "btnGestaoFinanceira_ConsultaSql":
                            btnGestaoFinanceira_ConsultaSql.Enabled = false;
                            break;
                        case "btnGestaoFinanceira_Planilha":
                            btnGestaoFinanceira_Planilha.Enabled = false;
                            break;
                        case "btnGestaoFinanceira_Cubo":
                            btnGestaoFinanceira_Cubo.Enabled = false;
                            break;
                        case "btnGestaoFinanceira_Dashboard":
                            btnGestaoFinanceira_Dashboard.Enabled = false;
                            break;
                        case "btnSeguranca_UsuariosLib":
                            btnSeguranca_UsuariosLib.Enabled = false;
                            break;
                        case "btnSeguranca_PerfisLib":
                            btnSeguranca_PerfisLib.Enabled = false;
                            break;
                        case "btnGestaoMateriais_RelatorioCustomizado":
                            btnGestaoMateriais_RelatorioCustomizado.Enabled = false;
                            break;
                        case "btnGestaoMateriais_ConsultaSql":
                            btnGestaoMateriais_ConsultaSql.Enabled = false;
                            break;
                        case "btnGestaoMateriais_Planilha":
                            btnGestaoMateriais_Planilha.Enabled = false;
                            break;
                        case "btnGestaoMateriais_Cubo":
                            btnGestaoMateriais_Cubo.Enabled = false;
                            break;
                        case "btnGestaoMateriais_Dashboard":
                            btnGestaoMateriais_Dashboard.Enabled = false;
                            break;
                        case "btnGestaoMateriais_Modulos":
                            btnGestaoMateriais_Modulos.Enabled = false;
                            break;
                        case "btnGestaoMateriais_Operacoes":
                            btnGestaoMateriais_Operacoes.Enabled = false;
                            break;
                        case "btnGestaoAdminstracao_RelatorioInterno":
                            btnGestaoAdminstracao_RelatorioInterno.Enabled = false;
                            break;
                        case "btnGestaoAdminstracao_RelatorioCustomizado":
                            btnGestaoAdminstracao_RelatorioCustomizado.Enabled = false;
                            break;
                        case "btnGestaoAdminstracao_ConsultaSql":
                            btnGestaoAdminstracao_ConsultaSql.Enabled = false;
                            break;
                        case "btnGestaoAdminstracao_Planilha":
                            btnGestaoAdminstracao_Planilha.Enabled = false;
                            break;
                        case "btnGestaoAdminstracao_Cubo":
                            btnGestaoAdminstracao_Cubo.Enabled = false;
                            break;
                        case "btnGestaoAdminstracao_Dashboard":
                            btnGestaoAdminstracao_Dashboard.Enabled = false;
                            break;
                        case "btnGestaoAdminstracao_Modulos":
                            btnGestaoAdminstracao_Modulos.Enabled = false;
                            break;
                        case "btnGestaoFiscal_RelatorioInterno":
                            btnGestaoFiscal_RelatorioInterno.Enabled = false;
                            break;
                        case "btnGestaoFiscal_RelatorioCustomizado":
                            btnGestaoFiscal_RelatorioCustomizado.Enabled = false;
                            break;
                        case "btnGestaoFiscal_ConsultaSql":
                            btnGestaoFiscal_ConsultaSql.Enabled = false;
                            break;
                        case "btnGestaoFiscal_Planilha":
                            btnGestaoFiscal_Planilha.Enabled = false;
                            break;
                        case "btnGestaoFiscal_Cubo":
                            btnGestaoFiscal_Cubo.Enabled = false;
                            break;
                        case "btnGestaoFiscal_Dashboard":
                            btnGestaoFiscal_Dashboard.Enabled = false;
                            break;
                        case "btnGestaoFiscal_Modulo":
                            btnGestaoFiscal_Modulo.Enabled = false;
                            break;
                        case "btnCadastros_ConsultaSql":
                            btnCadastros_ConsultaSql.Enabled = false;
                            break;
                        case "btnCadastros_Formula":
                            btnCadastros_Formula.Enabled = false;
                            break;
                        case "btnCadastros_Campos":
                            btnCadastros_Campos.Enabled = false;
                            break;
                        case "btnCadastros_TabelaDinamica":
                            btnCadastros_TabelaDinamica.Enabled = false;
                            break;
                        case "btnUtilitarios_RecalculoSaldo":
                            btnUtilitarios_RecalculoSaldo.Enabled = false;
                            break;
                        case "btnGestaoAdminstracao_CamposObrigatorio":
                            btnGestaoAdminstracao_CamposObrigatorio.Enabled = false;
                            break;
                        case "btnUtilitarios_TabelaPreco":
                            btnUtilitarios_TabelaPreco.Enabled = false;
                            break;
                        case "btnUtilitarios_Inventario":
                            btnUtilitarios_Inventario.Enabled = false;
                            break;
                        case "btnUtilitarios_AtualizacaoEstoqueMinimo":
                            btnUtilitarios_AtualizacaoEstoqueMinimo.Enabled = false;
                            break;
                        case "btnCadastro_FatorConversao":
                            btnCadastro_FatorConversao.Enabled = false;
                            break;
                        case "btnUtilitarios_Lote":
                            btnUtilitarios_Lote.Enabled = false;
                            break;
                        case "btnOperacao_DDFe":
                            btnOperacao_DDFe.Enabled = false;
                            break;
                        case "btnUnidade":
                            btnUnidade.Enabled = false;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {

            }
        }

        private void verificaAcessoAnexo()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GPERMISSAOANEXO WHERE CODUSUARIO = ? AND ACESSO = 1", new object[] { AppLib.Context.Usuario });
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (dt.Rows[i]["CODMENUANEXO"].ToString())
                    {
                        case "":
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Não foi possível carregar os acessos do seu usuário.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void verificaAcessoProcesso()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GPERMISSAOPROCESSO WHERE CODUSUARIO = ? AND ACESSO = 1", new object[] { AppLib.Context.Usuario });
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (dt.Rows[i]["CODMENUPROCESSO"].ToString())
                    {
                        case "":
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Não foi possível carregar os acessos do seu usuário.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void desabilitaMenu()
        {
            //Fiscal
            ribFiscal_CadastrosFiscais.Visible = false;
            ribFiscal_Gestao.Visible = false;

            //Financeiro
            ribFinanceiro_Lancamentos.Visible = false;
            ribFinanceiro_MovimentacoesBancarias.Visible = false;
            ribFinanceiro_Gestao.Visible = false;

            //Materiais
            ribMateriais_Cadastros.Visible = false;
            ribMateriais_Operacoes.Visible = false;
            ribMateriais_Gestao.Visible = false;
            ribMateriais_Utilitarios.Visible = false;

            //Produção
            ribProducao_Cadastros.Visible = false;
            ribProducao_ControleProducao.Visible = false;
            ribProducao_Gestao.Visible = false;

            //Adminstração
            ribAdminstracao_Cadastros.Visible = false;
            ribAdminstracao_Seguranca.Visible = false;
            ribAdminstracao_Gestao.Visible = false;

            //Projetos
            ribGestaoProjetos_Operacao.Visible = false;

            // Personalizado
            clsCustomizado.ColecaoMenu.Clear();

        }
        private void habilitaMenu(string modulo)
        {
            switch (modulo)
            {
                case "FIN":
                    ribFinanceiro_Lancamentos.Visible = true;
                    ribFinanceiro_MovimentacoesBancarias.Visible = true;
                    ribFinanceiro_Gestao.Visible = true;
                    ribControlMenu.Pages.Clear();
                    ribControlMenu.Pages.Insert(0, this.ribFinanceiro_Lancamentos);
                    ribControlMenu.Pages.Insert(1, this.ribFinanceiro_MovimentacoesBancarias);
                    ribControlMenu.Pages.Insert(2, this.ribCadastrosGlobais);
                    ribControlMenu.Pages.Insert(3, this.ribFinanceiro_Gestao);
                    break;

                case "ADM":
                    ribAdminstracao_Cadastros.Visible = true;
                    ribAdminstracao_Seguranca.Visible = true;
                    ribAdminstracao_Gestao.Visible = true;
                    ribControlMenu.Pages.Clear();
                    ribControlMenu.Pages.Insert(0, this.ribAdminstracao_Cadastros);
                    ribControlMenu.Pages.Insert(1, this.ribAdminstracao_Seguranca);
                    ribControlMenu.Pages.Insert(2, this.ribCadastrosGlobais);
                    ribControlMenu.Pages.Insert(3, this.ribAdminstracao_Gestao);
                    break;

                case "FIS":
                    ribFiscal_CadastrosFiscais.Visible = true;
                    ribFiscal_Gestao.Visible = true;
                    ribControlMenu.Pages.Clear();
                    ribControlMenu.Pages.Insert(0, this.ribFiscal_CadastrosFiscais);
                    ribControlMenu.Pages.Insert(1, this.ribCadastrosGlobais);
                    ribControlMenu.Pages.Insert(2, this.ribFiscal_Gestao);
                    break;

                case "MAT":
                    ribFiscal_CadastrosFiscais.Visible = true;
                    ribMateriais_Cadastros.Visible = true;
                    ribMateriais_Operacoes.Visible = true;
                    ribMateriais_Gestao.Visible = true;
                    ribMateriais_Utilitarios.Visible = true;
                    ribControlMenu.Pages.Clear();
                    ribControlMenu.Pages.Insert(0, this.ribMateriais_Cadastros);
                    ribControlMenu.Pages.Insert(1, this.ribMateriais_Operacoes);
                    ribControlMenu.Pages.Insert(2, this.ribCadastrosGlobais);
                    ribControlMenu.Pages.Insert(3, this.ribMateriais_Utilitarios);
                    ribControlMenu.Pages.Insert(4, this.ribMateriais_Gestao);
                    break;
                case "PRO":
                    ribProducao_Cadastros.Visible = true;
                    ribProducao_ControleProducao.Visible = true;
                    ribProducao_Gestao.Visible = true;
                    ribControlMenu.Pages.Clear();
                    ribControlMenu.Pages.Insert(0, this.ribProducao_Cadastros);
                    ribControlMenu.Pages.Insert(1, this.ribProducao_ControleProducao);
                    ribControlMenu.Pages.Insert(2, this.ribCadastrosGlobais);
                    ribControlMenu.Pages.Insert(3, this.ribProducao_Gestao);
                    FrmVisaoFiliais f = new FrmVisaoFiliais("WHERE GFILIAL.CODEMPRESA = '" + AppLib.Context.Empresa + "'");

                    if (!f.ValidaNumeroFiliais())
                    {
                        f.ShowDialog();
                    }
                    break;

                case "ARQ":
                    ribControlMenu.Pages.Clear();
                    ribControlMenu.Pages.Insert(0, this.ribArquivosDigitais_Operacao);
                    FrmVisaoFiliais frmFilial = new FrmVisaoFiliais("WHERE GFILIAL.CODEMPRESA = '" + AppLib.Context.Empresa + "'");

                    if (!frmFilial.ValidaNumeroFiliais())
                    {
                        frmFilial.ShowDialog();
                    }

                    break;
                case "PRJ":
                    ribGestaoProjetos_Operacao.Visible = true;
                    ribMateriais_Gestao.Visible = true;
                    ribMateriais_Utilitarios.Visible = true;
                    ribControlMenu.Pages.Clear();
                    ribControlMenu.Pages.Insert(0, this.ribGestaoProjetos_Operacao);
                    ribControlMenu.Pages.Insert(1, this.ribCadastrosGlobais);
                    FrmVisaoFiliais frmFilia = new FrmVisaoFiliais("WHERE GFILIAL.CODEMPRESA = '" + AppLib.Context.Empresa + "'");

                    if (!frmFilia.ValidaNumeroFiliais())
                    {
                        frmFilia.ShowDialog();
                    }

                    break;

                case "CRM":
                    ribAtendimento.Visible = true;
                    ribControlMenu.Pages.Clear();
                    ribControlMenu.Pages.Insert(0, this.ribAtendimento);
                    ribControlMenu.Pages.Insert(1, this.ribCadastrosGlobais);
                    break;

                default:
                    break;
            }
        }

        private void DesabilitaBotoes()
        {
            #region Fiscal

            btnCadastrosFiscais_Regra.Visibility = BarItemVisibility.Never;

            #endregion

            #region Produção

            btnControleProducao_SequenciamentoProducao.Visibility = BarItemVisibility.Never;
            btnControleProducao_ApontamentoProducao.Visibility = BarItemVisibility.Never;
            btnControleProducao_ApontamentoProducao.Visibility = BarItemVisibility.Never;

            btnControleProducao_ApontamentoRefugo.Visibility = BarItemVisibility.Never;

            btnControleProducao_MonitorRecursos.Visibility = BarItemVisibility.Never;
            ribbonPageGroup51.Visible = false;

            #endregion
        }

        private void bsiGestaoFinanceira_ItemClick(object sender, ItemClickEventArgs e)
        {
            desabilitaMenu();
            habilitaMenu("FIN");
            applicationMenu1.HidePopup();
            Modulo = "FIN";
        }

        private void bsiGestaoAdministracao_ItemClick(object sender, ItemClickEventArgs e)
        {
            desabilitaMenu();
            habilitaMenu("ADM");
            applicationMenu1.HidePopup();
            Modulo = "ADM";
        }

        private void bsiGestaoContabil_ItemClick(object sender, ItemClickEventArgs e)
        {
            desabilitaMenu();
            habilitaMenu("CON");
            applicationMenu1.HidePopup();
            Modulo = "CON";
        }

        private void bsiGestaoFiscal_ItemClick(object sender, ItemClickEventArgs e)
        {
            desabilitaMenu();
            habilitaMenu("FIS");
            applicationMenu1.HidePopup();
            Modulo = "FIS";
        }

        private void bsiGestaoIncorporacoes_ItemClick(object sender, ItemClickEventArgs e)
        {
            desabilitaMenu();
            habilitaMenu("INC");
            applicationMenu1.HidePopup();
        }

        private void bsiGestaoMateriais_ItemClick(object sender, ItemClickEventArgs e)
        {
            desabilitaMenu();
            habilitaMenu("MAT");
            applicationMenu1.HidePopup();
            Modulo = "MAT";
        }

        private void bsiGestaoProducao_ItemClick(object sender, ItemClickEventArgs e)
        {
            desabilitaMenu();
            habilitaMenu("PRO");
            applicationMenu1.HidePopup();
            Modulo = "PRO";
        }

        private void bsiGestaoRh_ItemClick(object sender, ItemClickEventArgs e)
        {
            desabilitaMenu();
            habilitaMenu("RH");
            applicationMenu1.HidePopup();
            Modulo = "RH";
        }

        private void bsiGestaoServicos_ItemClick(object sender, ItemClickEventArgs e)
        {
            desabilitaMenu();
            habilitaMenu("SER");
            applicationMenu1.HidePopup();
            Modulo = "SER";
        }
        private void bsiGestaoPatrimonial_ItemClick(object sender, ItemClickEventArgs e)
        {
            desabilitaMenu();
            habilitaMenu("PAT");
            applicationMenu1.HidePopup();
            Modulo = "PAT";
        }

        private void bsiGestaoArquivos_ItemClick(object sender, ItemClickEventArgs e)
        {
            desabilitaMenu();
            habilitaMenu("ARQ");
            //applicationMenu1.HidePopup();
            Modulo = "ARQ";
        }

        private void bsiPersonalizado_ItemClick(object sender, ItemClickEventArgs e)
        {
            desabilitaMenu();
            habilitaMenu("PER");
            applicationMenu1.HidePopup();
            Modulo = "PER";
        }

        private void bsiGestaoAtendimento_ItemClick(object sender, ItemClickEventArgs e)
        {
            desabilitaMenu();
            habilitaMenu("CRM");
            applicationMenu1.HidePopup();
            Modulo = "CRM";
        }

        private void btnOperacoes_Estoque_ItemClick(object sender, ItemClickEventArgs e)
        {
            CarregaOperacao(1, "btnOperacoes_Estoque");
        }
        private void CarregaOperacao(int Tipo, string codMenu)
        {
            using (PSSelTipoOperacao psSelTipoOperacao = new PSSelTipoOperacao())
            {
                psSelTipoOperacao.Tipo = Tipo;
                psSelTipoOperacao.pai = this;
                psSelTipoOperacao.TipoOper = string.Empty;
                psSelTipoOperacao.CodFilial = 0;
                psSelTipoOperacao.codMenu = codMenu;
                switch (psSelTipoOperacao.ShowDialog())
                {
                    case DialogResult.OK:
                        if (!string.IsNullOrEmpty(psSelTipoOperacao.TipoOper))
                        {
                            PSPartOperacao psPartOperacao = new PSPartOperacao();
                            psPartOperacao.DefaultFilter.Add(new PS.Lib.PSFilter("CODFILIAL", psSelTipoOperacao.CodFilial));
                            psPartOperacao.DefaultFilter.Add(new PS.Lib.PSFilter("CODTIPOPER", psSelTipoOperacao.TipoOper));
                            psPartOperacao.MainForm = this;

                            psPartOperacao.Execute();
                            break;
                        }
                        else
                        {
                            break;
                        }
                    case DialogResult.Abort:
                        break;
                }
            }
        }

        private void btnOperacoes_Entradas_ItemClick(object sender, ItemClickEventArgs e)
        {
            CarregaOperacao(2, "btnOperacoes_Entradas");
        }

        private void btnOperacoes_Saidas_ItemClick(object sender, ItemClickEventArgs e)
        {
            CarregaOperacao(3, "btnOperacoes_Saidas");
        }

        private void btnOperacoes_NotaFiscalEstadual_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Filtro.frmFiltroNotaFiscal frm = new PS.Glb.New.Filtro.frmFiltroNotaFiscal();
            frm.pai = this;
            frm.Show();

            //PS.Glb.New.Visao.frmVisaoNotaFiscalEstadual frm = new PS.Glb.New.Visao.frmVisaoNotaFiscalEstadual(condicao, this, Codmenu);
            //frm.Show();

            //PSPartNFEstadual psPSPartNFEstadual = new PSPartNFEstadual();
            //psPSPartNFEstadual.MainForm = this;
            //psPSPartNFEstadual.Execute();
        }
        private void btnLancamentos_Cheques_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Filtro.frmFiltroCheque frm = new PS.Glb.New.Filtro.frmFiltroCheque();
            frm.pai = this;
            frm.Show();
        }

        private void btnMovimentacoesBancarias_ExtratoCaixa_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Filtro.frmFiltroExtrato frm = new PS.Glb.New.Filtro.frmFiltroExtrato();
            frm.pai = this;
            frm.Show();
        }

        private void btnLancamentos_Fatura_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoFatura frm = new PS.Glb.New.Visao.frmVisaoFatura(condicao, this);
            frm.Show();
        }

        private void btnLancamentos_PagarReceber_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Filtro.frmFiltroLancamento frm = new PS.Glb.New.Filtro.frmFiltroLancamento();
            frm.pai = this;
            frm.ShowDialog();
        }
        private bool verificaAcessoMenu(string psPart)
        {
            bool permissao = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, @"SELECT ACESSO FROM GACESSOMENU INNER JOIN GUSUARIOPERFIL ON GACESSOMENU.CODPERFIL = GUSUARIOPERFIL.CODPERFIL AND GACESSOMENU.CODEMPRESA = GUSUARIOPERFIL.CODEMPRESA WHERE CODPSPART = ? AND GACESSOMENU.CODEMPRESA = ? AND GUSUARIOPERFIL.CODUSUARIO = ? ", new object[] { psPart, AppLib.Context.Empresa, AppLib.Context.Usuario }));
            if (permissao == false)
            {
                MessageBox.Show("Usuário sem permissão de acesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnClassificacao_LocalEstoque_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoLocalEstoque frm = new PS.Glb.New.Visao.frmVisaoLocalEstoque(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnMovimentacoesBancarias_ContaCorrente_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoContaCorrente frm = new PS.Glb.New.Visao.frmVisaoContaCorrente(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnMovimentacoesBancarias_Agencia_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoAgencia frm = new PS.Glb.New.Visao.frmVisaoAgencia(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnMovimentacoesBancarias_Bancos_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoBanco frm = new PS.Glb.New.Visao.frmVisaoBanco(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnMovimentacoesBancarias_Convenio_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.ERP.Financeiro.FormConvenioVisao f = PS.Glb.ERP.Financeiro.FormConvenioVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        private void btnLancamentos_FormaPagamento_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoFormaPagamento frm = new PS.Glb.New.Visao.frmVisaoFormaPagamento(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnMovimentacoesBancarias_ContaCaixa_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoContaCaixa frm = new PS.Glb.New.Visao.frmVisaoContaCaixa(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnMovimentacoesBancariass_TipoDocumento_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoTipoDocumento frm = new PS.Glb.New.Visao.frmVisaoTipoDocumento(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnCadastro_CondicaoPagamento_ItemClick(object sender, ItemClickEventArgs e)
        {
            PSPartCondicaoPgto psPartCondicaoPgto = new PSPartCondicaoPgto();
            psPartCondicaoPgto.MainForm = this;
            psPartCondicaoPgto.Execute();
        }

        private void btnCadastro_Fabricante_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoFabricante frm = new PS.Glb.New.Visao.frmVisaoFabricante(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnCadastro_Serie_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoSerie frm = new PS.Glb.New.Visao.frmVisaoSerie(condicao, this, Codmenu);
            frm.Show();
        }
        private void btnCadastro_MensagensOperacao_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoMensagensPadrao frm = new PS.Glb.New.Visao.frmVisaoMensagensPadrao(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnCadastro_Motivo_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.Globais.frmVisaoMotivo frm = new PS.Glb.New.Visao.Globais.frmVisaoMotivo(this);
            frm.Show();
        }

        private void btnCadastro_UnidadeMedida_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoUnidadeBase frm = new PS.Glb.New.Visao.frmVisaoUnidadeBase(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnCadastro_Transportadora_ItemClick(object sender, ItemClickEventArgs e)
        {
            //PS.Glb.New.Visao.frmVisaoTransportadora frm = new PS.Glb.New.Visao.frmVisaoTransportadora(condicao, this, Codmenu);
            //frm.Show();

            PSPartTransportadora psPartTransportadora = new PSPartTransportadora();
            psPartTransportadora.MainForm = this;
            psPartTransportadora.Execute();
        }

        private void btnCadastro_TipoTransporte_ItemClick(object sender, ItemClickEventArgs e)
        {
            //PS.Glb.New.Visao.frmVisaoTipoTransporte frm = new PS.Glb.New.Visao.frmVisaoTipoTransporte(condicao, this, Codmenu);
            //frm.Show();

            PSPartTipoTransporte PSPartTipoTransporte = new PSPartTipoTransporte();
            PSPartTipoTransporte.MainForm = this;
            PSPartTipoTransporte.Execute();
        }

        private void btnCadastro_Representante_ItemClick(object sender, ItemClickEventArgs e)
        {
            //PS.Glb.New.Visao.frmVisaoRepresentante frm = new PS.Glb.New.Visao.frmVisaoRepresentante(condicao, this, Codmenu);
            //frm.Show();
            PSPartRepre psPartRepre = new PSPartRepre();
            psPartRepre.MainForm = this;
            psPartRepre.Execute();
        }

        private void btnCadastro_Vendedor_ItemClick(object sender, ItemClickEventArgs e)
        {
            //PS.Glb.New.Visao.frmVisaoVendedor frm = new PS.Glb.New.Visao.frmVisaoVendedor(condicao, this, Codmenu);
            //frm.Show();
            PSPartVendedor psPartVendedor = new PSPartVendedor();
            psPartVendedor.MainForm = this;
            psPartVendedor.Execute();
        }

        private void btnCadastrosFiscais_Natureza_ItemClick(object sender, ItemClickEventArgs e)
        {
            PSPartNatureza psPartNatureza = new PSPartNatureza();
            psPartNatureza.MainForm = this;
            psPartNatureza.Execute();
        }

        private void btnCadastrosFiscais_Tributos_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoTributos frm = new PS.Glb.New.Visao.frmVisaoTributos(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnCadastrosFiscais_Regiao_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoRegiao frm = new PS.Glb.New.Visao.frmVisaoRegiao(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnCadastros_Empresa_ItemClick(object sender, ItemClickEventArgs e)
        {
            PSPartEmpresa psEmpresa = new PSPartEmpresa();
            psEmpresa.MainForm = this;
            psEmpresa.Execute();
        }

        private void btnCadastros_Filial_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoFilial frm = new PS.Glb.New.Visao.frmVisaoFilial(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnCadastros_Status_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.Globais.frmVisaoStatus frm = new PS.Glb.New.Visao.Globais.frmVisaoStatus(this);
            frm.Show();
        }

        private void btnCadastros_Situacao_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.Globais.frmVisaoSituacao frm = new PS.Glb.New.Visao.Globais.frmVisaoSituacao(this);
            frm.Show();
        }

        private void btnMovimentacoesBancarias_CobrancaEletronica_ItemClick(object sender, ItemClickEventArgs e)
        {
            PSPartBoleto psPartBoleto = new PSPartBoleto();
            psPartBoleto.MainForm = this;
            psPartBoleto.Execute();
        }

        private void btnGestaoFiscal_RelatorioInterno_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.Report.PSPartReport psPartReport = new PS.Glb.Report.PSPartReport();
            psPartReport.MainForm = this;
            psPartReport.Execute();
        }

        private void btnGestaoFiscal_RelatorioCustomizado_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormReportVisao f = AppLib.Padrao.FormReportVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnGestaoFiscal_ConsultaSql_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormSQLVisao f = AppLib.Padrao.FormSQLVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnGestaoFiscal_Planilha_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormExcelVisao f = AppLib.Padrao.FormExcelVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }
        private void btnGestaoFiscal_Cubo_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormCuboVisao f = AppLib.Padrao.FormCuboVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }
        private void btnGestaoFiscal_Dashboard_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormDashboardVisao f = AppLib.Padrao.FormDashboardVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }
        private void btnGestaoFinanceira_ConsultaSql_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormSQLVisao f = AppLib.Padrao.FormSQLVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnGestaoFinanceira_Planilha_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormExcelVisao f = AppLib.Padrao.FormExcelVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnGestaoFinanceira_Cubo_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormCuboVisao f = AppLib.Padrao.FormCuboVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnGestaoFinanceira_Dashboard_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormDashboardVisao f = AppLib.Padrao.FormDashboardVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnGestaoMateriais_RelatoriosModulo_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.Report.PSPartReport psPartReport = new PS.Glb.Report.PSPartReport();
            psPartReport.MainForm = this;
            psPartReport.Execute();
        }

        private void btnGestaoMateriais_RelatorioCustomizado_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormReportVisao f = AppLib.Padrao.FormReportVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnGestaoMateriais_ConsultaSql_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormSQLVisao f = AppLib.Padrao.FormSQLVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnGestaoMateriais_Planilha_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormExcelVisao f = AppLib.Padrao.FormExcelVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnGestaoMateriais_Cubo_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormCuboVisao f = AppLib.Padrao.FormCuboVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnGestaoMateriais_Dashboard_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormDashboardVisao f = AppLib.Padrao.FormDashboardVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnGestaoMateriais_Modulos_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoModulos frm = new PS.Glb.New.Visao.frmVisaoModulos(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnGestaoMateriais_Operacoes_ItemClick(object sender, ItemClickEventArgs e)
        {
            PSPartTipOper psPartTipOper = new PSPartTipOper();
            psPartTipOper.MainForm = this;
            psPartTipOper.Execute();
        }

        private void btnSeguranca_Usuarios_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoUsuarios frm = new PS.Glb.New.Visao.frmVisaoUsuarios(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnSeguranca_Perfis_ItemClick(object sender, ItemClickEventArgs e)
        {
            //PSPartPerfil psPerfil = new PSPartPerfil();
            //psPerfil.MainForm = this;
            //psPerfil.Execute();

            //PS.Glb.New.Cadastros.Form1 f = new PS.Glb.New.Cadastros.Form1();
            //f.MdiParent = this;
            //f.Show();

            PS.Glb.New.Visao.frmVisaoPerfil f = new PS.Glb.New.Visao.frmVisaoPerfil();
            f.MdiParent = this;
            f.Show();
        }

        private void btnSeguranca_PerfisLib_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormPerfilVisao f = AppLib.Padrao.FormPerfilVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.MdiParent = this;
            f.Mostrar();
        }

        private void btnSeguranca_UsuariosLib_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormUsuarioVisao f = AppLib.Padrao.FormUsuarioVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.MdiParent = this;
            f.Mostrar();
        }

        private void btnUtilitarios_AlterarSenha_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormAlteraSenha form = new FormAlteraSenha();
            form.Show();
        }

        private void btnGestaoAdminstracao_RelatorioInterno_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.Report.PSPartReport psPartReport = new PS.Glb.Report.PSPartReport();
            psPartReport.MainForm = this;
            psPartReport.Execute();
        }

        private void btnGestaoAdminstracao_RelatorioCustomizado_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormReportVisao f = AppLib.Padrao.FormReportVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnGestaoAdminstracao_ConsultaSql_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormSQLVisao f = AppLib.Padrao.FormSQLVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnGestaoAdminstracao_Planilha_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormExcelVisao f = AppLib.Padrao.FormExcelVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnGestaoAdminstracao_Cubo_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormCuboVisao f = AppLib.Padrao.FormCuboVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnGestaoAdminstracao_Dashboard_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormDashboardVisao f = AppLib.Padrao.FormDashboardVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnCadastrosGlobais_ClientesFornecedores_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (verificaAcessoMenu("PSPartCliFor") == true)
            {
                PS.Glb.New.Filtro.frmFiltroCliente frm = new PS.Glb.New.Filtro.frmFiltroCliente();
                frm.pai = this;
                frm.ShowDialog();
            }
        }

        private void btnCadastrosGlobais_Produtos_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (verificaAcessoMenu("PSPartProduto") == true)
            {
                PS.Glb.New.Filtro.frmFiltroProduto frm = new PS.Glb.New.Filtro.frmFiltroProduto();
                frm.pai = this;
                frm.ShowDialog();
            }
        }

        private void btnCadastrosGlobais_Pais_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoPais frm = new PS.Glb.New.Visao.frmVisaoPais(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnCadastrosGlobais_Estado_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoEstado frm = new PS.Glb.New.Visao.frmVisaoEstado(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnCadastrosGlobais_Municipio_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoMunicipio frm = new PS.Glb.New.Visao.frmVisaoMunicipio(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnCadastrosGlobais_CentrodeCusto_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoCentroCusto frm = new PS.Glb.New.Visao.frmVisaoCentroCusto(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnCadastrosGlobais_NaturezaOrcamentaria_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoNaturezaOrcamentaria frm = new PS.Glb.New.Visao.frmVisaoNaturezaOrcamentaria(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnCadastrosGlobais_MoedaseIndices_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoMoeda frm = new PS.Glb.New.Visao.frmVisaoMoeda(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnCadastrosGlobais_Operador_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Cadastros.frmVisaoOperador frm = new PS.Glb.New.Cadastros.frmVisaoOperador(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnGestaoFinanceira_RelatorioCustomizado_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormReportVisao f = AppLib.Padrao.FormReportVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnGestaoFinanceira_RelatorioInterno_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.Report.PSPartReport psPartReport = new PS.Glb.Report.PSPartReport();
            psPartReport.MainForm = this;
            psPartReport.Execute();
        }

        private void btnGestaoProducao_RelatorioInterno_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.Report.PSPartReport psPartReport = new PS.Glb.Report.PSPartReport();
            psPartReport.MainForm = this;
            psPartReport.Execute();
        }
        private void btnGestaoProducao_RelatorioCustomizado_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormReportVisao f = AppLib.Padrao.FormReportVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnGestaoProducao_ConsultaSql_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormSQLVisao f = AppLib.Padrao.FormSQLVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnGestaoProducao_Planilha_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormExcelVisao f = AppLib.Padrao.FormExcelVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnGestaoProducao_Cubo_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormCuboVisao f = AppLib.Padrao.FormCuboVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnGestaoProducao_Dashboard_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppLib.Padrao.FormDashboardVisao f = AppLib.Padrao.FormDashboardVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void btnCadastrosFiscais_RegraCfop_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.ERP.Comercial.FormRegraCFOPVisao f = new PS.Glb.ERP.Comercial.FormRegraCFOPVisao();
            f.Mostrar(this);
        }

        private void btnGestaoAdminstracao_TabelaDinamica_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.PSPartTabDinamica psPartTabDinamica = new PS.Glb.PSPartTabDinamica();
            psPartTabDinamica.MainForm = this;
            psPartTabDinamica.Execute();
        }

        private void btnGestaoAdminstracao_CamposComplementares_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.PSPartTabCampoCompl psPartTabCampoCompl = new PS.Glb.PSPartTabCampoCompl();
            psPartTabCampoCompl.MainForm = this;
            psPartTabCampoCompl.Execute();
        }

        private void btnOperacoes_OutrasOperacoes_ItemClick(object sender, ItemClickEventArgs e)
        {
            CarregaOperacao(0, "btnOperacoes_OutrasOperacoes");
        }

        private void bsiSelecaoEmpresa_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.ValidaFormularioAberto();
                FormSelecaoEmpresa frmSelecionaEmpresa = new FormSelecaoEmpresa();
                frmSelecionaEmpresa.ShowDialog();
                this.ValidaSessao();

                if (Contexto.Session.Empresa != null)
                {
                    //this.Text = string.Concat(" | ", Contexto.Session.Empresa.nomeFantasia);
                    Empresa.Caption = string.Concat("Empresa: ", Contexto.Session.Empresa.NomeFantasia);
                    Usuário.Caption = string.Concat("Usuário: ", Contexto.Session.CodUsuario);
                    Alias.Caption = string.Concat("Alias: ", PS.Lib.Contexto.Alias.Name);
                }
                else
                {
                    //this.Text = "ERP";
                    Empresa.Caption = string.Concat("Empresa: ", string.Empty);
                    Usuário.Caption = string.Concat("Usuário: ", Contexto.Session.CodUsuario);
                    Alias.Caption = string.Concat("Alias: ", PS.Lib.Contexto.Alias.Name);
                }

            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
            }
        }
        private void ValidaFormularioAberto()
        {
            bool Flag = false;
            foreach (Form form in this.MdiChildren)
            {
                Flag = true;
            }

            if (Flag)
            {
                throw new Exception("Para executar esta ação é necessário fechar todas as janelas.");
            }
        }

        private void btnControleProducao_OrdemProducao_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmFiltroOrdemProducao frm = new FrmFiltroOrdemProducao();
            //FrmOrdemProducao frm = new FrmOrdemProducao();
            frm.pai = this;
            frm.Show();
        }

        private void btnSelecaoEmpresa_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.ValidaFormularioAberto();
                FormSelecaoEmpresa frmSelecionaEmpresa = new FormSelecaoEmpresa();
                frmSelecionaEmpresa.ShowDialog();

                if (Contexto.Session.Empresa != null)
                {
                    //this.Text = string.Concat(" | ", Contexto.Session.Empresa.nomeFantasia);
                    Empresa.Caption = string.Concat("Empresa: ", Contexto.Session.Empresa.NomeFantasia);
                    Usuário.Caption = string.Concat("Usuário: ", Contexto.Session.CodUsuario);
                    Alias.Caption = string.Concat("Alias: ", PS.Lib.Contexto.Alias.Name);
                }
                else
                {
                    //this.Text = "ERP";
                    Empresa.Caption = string.Concat("Empresa: ", string.Empty);
                    Usuário.Caption = string.Concat("Usuário: ", Contexto.Session.CodUsuario);
                    Alias.Caption = string.Concat("Alias: ", PS.Lib.Contexto.Alias.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Para fazer esta ação é necessário fechar todas as janelas.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void barButtonItem62_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                this.ValidaFormularioAberto();
                applicationMenu1.HidePopup();
                AtribuirModulo();
                MDILogin frmLogin = new MDILogin();
                frmLogin.ShowDialog();
                this.ValidaSessao();
                this.DesabilitaBotoes();
                DesabilitaModulo();
                PermissaoModulo();
                this.verificaModulo();
                this.desabilitaMenu();
                this.verificaAcessoMenu();
                this.HabilitaModulo();
                this.habilitaMenu(Modulo);
                this.InativaAcesso();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Para executar esta ação é necessário fechar todas as janelas.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnCadastros_TabelaDinamica_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.PSPartTabDinamica psPartTabDinamica = new PS.Glb.PSPartTabDinamica();
            psPartTabDinamica.MainForm = this;
            psPartTabDinamica.Execute();
        }

        private void btnCadastros_ConsultaSql_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.Globais.frmVisaoQuery frm = new PS.Glb.New.Visao.Globais.frmVisaoQuery(this);
            frm.Show();
        }

        private void btnCadastros_Formula_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.Formula.PSPartFormula psPartFormula = new PS.Glb.Formula.PSPartFormula();
            psPartFormula.MainForm = this;
            psPartFormula.Execute();
        }

        private void btnCadastros_Campos_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.PSPartTabCampoCompl psPartTabCampoCompl = new PS.Glb.PSPartTabCampoCompl();
            psPartTabCampoCompl.MainForm = this;
            psPartTabCampoCompl.Execute();
        }

        private void btnCadastrosFiscais_NaturezaOperacao_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (verificaAcessoMenu("PsPartNatureza") == true)
            {
                PS.Glb.New.Filtro.frmFiltroNaturezaOperacao frm = new PS.Glb.New.Filtro.frmFiltroNaturezaOperacao();
                frm.pai = this;
                frm.Show();
            }
        }

        private void btnCadastrosFiscais_RegrasIcms_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoRegraIcms frm = new PS.Glb.New.Visao.frmVisaoRegraIcms(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnGestaoFinanceira_Utilitarios_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnUtilitarios_ImportadorIbptax_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.ERP.Comercial.FormIBPTaxVisao f = PS.Glb.ERP.Comercial.FormIBPTaxVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        private void btnCadastrosFiscais_Regra_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.ERP.Comercial.FormRegraCFOPVisao f = new PS.Glb.ERP.Comercial.FormRegraCFOPVisao();
            f.Mostrar(this);
        }

        private void btnMovimentacoesBancarias_PagamentoEletronico_ItemClick(object sender, ItemClickEventArgs e)
        {
            //PS.Glb.New.Visao.frmVisaoTerceiros frm = new PS.Glb.New.Visao.frmVisaoTerceiros(condicao, this, Codmenu);
            //frm.Show(); 
        }

        private void MDIPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult opcao;
            opcao = MessageBox.Show("Deseja sair do sistema?", "Mensagem", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (opcao.Equals(DialogResult.No))
            {
                e.Cancel = true;
            }

            AtribuirModulo();
        }
        public void AtribuirModulo()
        {
            AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE GUSUARIO SET CODMODULO = ? WHERE CODUSUARIO = ? ", new object[] { Modulo, AppLib.Context.Usuario });
        }
        public void HabilitaModulo()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODMODULO FROM GUSUARIO WHERE CODUSUARIO = ?", new object[] { AppLib.Context.Usuario });
            if (dt.Rows.Count > 0)
            {
                Modulo = dt.Rows[0]["CODMODULO"].ToString();
            }
            else
            {
                return;
            }
        }

        private void btnUtilitarios_RecalculoSaldo_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Cadastros.frmRecalculaSaldo f = new PS.Glb.New.Cadastros.frmRecalculaSaldo();
            //f.MdiParent = this;
            f.Show();
        }

        private void btnSessaoCaixa_Abertura_ItemClick(object sender, ItemClickEventArgs e)
        {
            // TERCEIROS
            //PS.Glb.New.Visao.frmVisaoTerceiros frm = new PS.Glb.New.Visao.frmVisaoTerceiros(condicao, this, condicao);
            //frm.Show();

            // UNIDADE DE MEDIDA
            //PS.Glb.New.Visao.frmVisaoUnidadeBase frm = new PS.Glb.New.Visao.frmVisaoUnidadeBase(condicao, this, Codmenu);
            //frm.Show();

            // REDE CARTÃO
            //PS.Glb.New.Visao.frmVisaoRedeCartao frm = new PS.Glb.New.Visao.frmVisaoRedeCartao(condicao, this, condicao);
            //frm.Show();

            // TESTES
            //PS.Glb.New.Cadastros.frmCadastroTeste frm = new PS.Glb.New.Cadastros.frmCadastroTeste();
            //frm.Show();

            // CONVERSÃO DE UNIDADE
            //PS.Glb.New.Visao.frmVisaoConversaoUnidade frm = new PS.Glb.New.Visao.frmVisaoConversaoUnidade(string.Empty, this);
            //frm.Show();

            // VISAO TESTE
            //PS.Glb.New.Visao.frmVisaoTeste frm = new PS.Glb.New.Visao.frmVisaoTeste();
            //frm.Show();

            //PS.Glb.New.Visao.frmVisaoFilial frm = new PS.Glb.New.Visao.frmVisaoFilial(condicao, this, Codmenu);
            //frm.Show();

            //VISAO DDFE
            //PS.Glb.New.Visao.frmVisaoDDFe frm = new PS.Glb.New.Visao.frmVisaoDDFe(condicao, this, Codmenu);
            //frm.Show();
        }
        private void btnCadastrosFiscais_RegrasIpi_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoRegraIpi frm = new PS.Glb.New.Visao.frmVisaoRegraIpi(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnGestaoAdminstracao_CamposObrigatorio_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoCamposObrigatorios frm = new PS.Glb.New.Visao.frmVisaoCamposObrigatorios(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnUtilitarios_TabelaPreco_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoTabelaPreco frm = new PS.Glb.New.Visao.frmVisaoTabelaPreco(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnCadastros_MotivoParada_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmFiltroOrdemMotivoParada frm = new FrmFiltroOrdemMotivoParada();
            frm.pai = this;
            frm.Show();
        }

        private void btnControleProducao_TerminalApontamento_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmTerminalApontamento frm = new FrmTerminalApontamento();
            frm.ShowDialog();
        }

        private void btnUtilitarios_Inventario_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoInventario frm = new PS.Glb.New.Visao.frmVisaoInventario(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnUtilitarios_AtualizacaoEstoqueMinimo_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Filtro.frmFiltroEstoqueMinimo frm = new PS.Glb.New.Filtro.frmFiltroEstoqueMinimo();
            frm.pai = this;
            frm.Show();
        }

        private void btnCadastro_FatorConversao_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoConversaoUnidade frm = new PS.Glb.New.Visao.frmVisaoConversaoUnidade(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnRecalCFOP_ItemClick(object sender, ItemClickEventArgs e)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT GOPER.CODEMPRESA, GOPER.CODOPER, GOPER.CODTIPOPER, GOPER.CODFILIAL FROM GOPER INNER JOIN GOPERITEM ON GOPERITEM.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEM.CODOPER = GOPER.CODOPER WHERE CODSTATUS = 0 AND CODTIPOPER IN ('2.1.01', '2.1.02') GROUP BY GOPER.CODEMPRESA, GOPER.CODOPER, GOPER.CODTIPOPER, GOPER.CODFILIAL", new object[] { });

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    geraTributo(Convert.ToInt32(dt.Rows[i]["CODOPER"]), dt.Rows[i]["CODTIPOPER"].ToString(), Convert.ToInt32(dt.Rows[i]["CODFILIAL"]));
                    calculaOperacao(Convert.ToInt32(dt.Rows[i]["CODOPER"]), dt.Rows[i]["CODTIPOPER"].ToString());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void btnUtilitarios_Lote_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoLote frm = new PS.Glb.New.Visao.frmVisaoLote(condicao, this, Codmenu);
            frm.Show();
        }

        private void btnOperacao_DDFe_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Filtro.frmFiltroDDFe frm = new PS.Glb.New.Filtro.frmFiltroDDFe();
            frm.pai = this;
            frm.Show();
        }

        private void btnGestaoProjetos_ItemClick(object sender, ItemClickEventArgs e)
        {
            desabilitaMenu();
            habilitaMenu("PRJ");
            applicationMenu1.HidePopup();
            Modulo = "PRJ";
        }

        private void btnUnidade_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Filtro.frmFiltroUnidade f = new PS.Glb.New.Filtro.frmFiltroUnidade();
            f.ShowDialog();

            if (!String.IsNullOrWhiteSpace(f.condicao))
            {
                PS.Glb.New.Visao.frmVisaoUnidade frm = new PS.Glb.New.Visao.frmVisaoUnidade();
                frm.Condicao = f.condicao;
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void btnProjeto_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Filtro.frmFiltroProjeto frm = new PS.Glb.New.Filtro.frmFiltroProjeto();
            frm.ShowDialog();

            if (!String.IsNullOrWhiteSpace(frm.condicao))
            {
                PS.Glb.New.Visao.frmVisaoProjeto form = new PS.Glb.New.Visao.frmVisaoProjeto(frm.condicao);
                form.MdiParent = this;
                form.Show();
            }
        }

        private void btnApontamento_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Filtro.frmFiltroApontamento frm = new PS.Glb.New.Filtro.frmFiltroApontamento();
            frm.ShowDialog();

            if (!String.IsNullOrWhiteSpace(frm.condicao))
            {
                PS.Glb.New.Visao.frmVisaoApontamento form = new PS.Glb.New.Visao.frmVisaoApontamento(frm.condicao);
                form.MdiParent = this;
                form.Show();
            }
        }

        #region Atendimento (CRM)

        private void btnAtendimento_Atendimento_ItemClick(object sender, ItemClickEventArgs e)
        {
            PS.Glb.New.Filtro.frmFiltroAtendimento frmFiltroAtendimento = new PS.Glb.New.Filtro.frmFiltroAtendimento();
            frmFiltroAtendimento.ShowDialog();

            if (!string.IsNullOrEmpty(frmFiltroAtendimento.condicao))
            {
                PS.Glb.New.Visao.frmVisaoAtendimento frmVisaoAtendimento = new PS.Glb.New.Visao.frmVisaoAtendimento();
                frmVisaoAtendimento.Condicao = frmFiltroAtendimento.condicao;
                frmVisaoAtendimento.MdiParent = this;
                frmVisaoAtendimento.Show();
            }
        }

        #endregion

        private void btnSessaoCaixa_Movimentacao_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnSessaoCaixa_Encerramento_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnSessaoCaixa_Gerencial_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        public void geraTributo(int codOper, string codTipOper, int codFilial)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();

            try
            {

                conn.BeginTransaction();

                #region variavel
                string insert = string.Empty;
                bool Editado = false;
                decimal TRB_VALORICMSST = 0;
                decimal TRB_FATORMVA = 0;
                decimal TRB_BCORIGINAL = 0;
                decimal TRB_REDUCAOBASEICMSST = 0;
                decimal pdif = 0;
                decimal TRB_ALIQUOTA = 0;
                decimal TRB_VALOR_ALIQUOTA = 0;
                string TRB_CST = string.Empty;
                int? TRB_MODBC = null;
                decimal TRB_REDBC = 0;
                string TRB_CENQ = string.Empty;
                string sSql = "";
                decimal icms = 0;
                //List<Class.Tributo> ListaTributo = new List<Class.Tributo>();
                decimal TRB_BASE_CALCULO = 0;

                DataTable dtTipOper = conn.ExecQuery("SELECT GERATRIBUTOS, TIPENTSAI FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { codTipOper, AppLib.Context.Empresa });
                DataTable dtItens = conn.ExecQuery(@"SELECT GOPERITEM.NSEQITEM, VLTOTALITEM, GOPERITEM.CODPRODUTO, GOPERITEM.CODNATUREZA, VPRODUTO.CODNCM, VCLIFOR.CODETD
FROM GOPER 
INNER JOIN GOPERITEM ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER 
INNER JOIN VPRODUTO ON GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO AND GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA
INNER JOIN VCLIFOR ON GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR AND VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA 
WHERE GOPER.CODOPER = ? AND GOPER.CODFILIAL = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, codFilial, AppLib.Context.Empresa });
                DataTable dtTipoTributo = conn.ExecQuery(@"SELECT CODTRIBUTO, ALIQUOTA, CODCST FROM GTIPOPERTRIBUTO WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOper });
                DataTable VTRIBUTO = conn.ExecQuery(@"SELECT ALIQUOTAEM, ALIQUOTA, CODTIPOTRIBUTO FROM VTRIBUTO WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa });
                DataTable VREGRAICMS = conn.ExecQuery(@"SELECT VREGRAICMS.ALIQUOTA, VNATUREZA.CODNATUREZA
FROM VREGRAICMS 
INNER JOIN VNATUREZA ON VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS
WHERE VNATUREZA.CODEMPRESA = ?", new object[] { AppLib.Context.Empresa });

                VREGRAICMS.PrimaryKey = new DataColumn[] { VREGRAICMS.Columns["CODNATUREZA"] };
                VTRIBUTO.PrimaryKey = new DataColumn[] { VTRIBUTO.Columns["CODTIPOTRIBUTO"] };

                if (VTRIBUTO.Rows.Count <= 0)
                {
                    MessageBox.Show("Não foi possível carregar os tributos do Tipo da Operação selecionada.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dtTipOper.Rows.Count <= 0)
                {
                    MessageBox.Show("Não foi possível carregar os parâmetros do Tipo da Operação selecionada.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dtItens.Rows.Count <= 0)
                {
                    MessageBox.Show("Não foi possível carregar os itens da Operação selecionada.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dtTipoTributo.Rows.Count <= 0)
                {
                    MessageBox.Show("Não foi possível carregar os tributos da Operação selecionada.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                #endregion

                //Verificar se altera rateio
                if (verificaValores(codOper) == false)
                {
                    conn.ExecTransaction("DELETE FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codOper });
                }
                else
                {
                    conn.ExecTransaction("DELETE FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND EDITADO = 0", new object[] { AppLib.Context.Empresa, codOper });
                }

                if (dtTipOper.Rows[0]["GERATRIBUTOS"].ToString() == "1")
                {
                    string UFDESTINATARIO = conn.ExecGetField(string.Empty, @"SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }).ToString();
                    //pra cada item
                    for (int iItens = 0; iItens < dtItens.Rows.Count; iItens++)
                    {
                        //pra cada tributo
                        for (int iTributo = 0; iTributo < dtTipoTributo.Rows.Count; iTributo++)
                        {
                            if (faturamento == false)
                            {
                                Editado = Convert.ToBoolean(conn.ExecGetField(false, @"SELECT EDITADO FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ? AND CODTRIBUTO = ?", new object[] { AppLib.Context.Empresa, codOper, dtItens.Rows[iItens]["NSEQITEM"], dtTipoTributo.Rows[iTributo]["CODTRIBUTO"] }));
                            }
                            if (Editado == false)
                            {
                                #region Base de Calculo


                                string query = conn.ExecGetField(string.Empty, "SELECT QUERY FROM GTIPOPERTRIBUTO INNER JOIN GQUERY ON GTIPOPERTRIBUTO.CODEMPRESA = GQUERY.CODEMPRESA AND GTIPOPERTRIBUTO.CODQUERY = GQUERY.CODQUERY WHERE GTIPOPERTRIBUTO.CODTIPOPER  = ? AND GTIPOPERTRIBUTO.CODEMPRESA = ? AND GTIPOPERTRIBUTO.CODTRIBUTO = ?", new object[] { codTipOper, AppLib.Context.Empresa, dtTipoTributo.Rows[iTributo]["CODTRIBUTO"] }).ToString();

                                query = query.Replace("@CODEMPRESA", "'" + AppLib.Context.Empresa + "'").Replace("@CODNATUREZA", "'" + dtItens.Rows[iItens]["CODNATUREZA"] + "'").Replace("@CODTRIBUTO", "'" + dtTipoTributo.Rows[iTributo]["CODTRIBUTO"].ToString() + "'").Replace("@CODOPER", "'" + codOper + "'").Replace("@NSEQITEM", "'" + dtItens.Rows[iItens]["NSEQITEM"] + "'").Replace("@UFDESTINO", "'" + UFDESTINATARIO + "'");

                                TRB_BASE_CALCULO = Convert.ToDecimal(conn.ExecGetField(0, query, new object[] { }));
                                TRB_BCORIGINAL = TRB_BASE_CALCULO;

                                #endregion

                                #region Aliquota
                                DataRow result = VTRIBUTO.Rows.Find(new object[] { dtTipoTributo.Rows[iTributo]["CODTRIBUTO"].ToString() });
                                switch (result["ALIQUOTAEM"].ToString())
                                {
                                    case "0":
                                        TRB_ALIQUOTA = Convert.ToDecimal(result["ALIQUOTA"].ToString());
                                        break;
                                    //Produto
                                    case "1":
                                        TRB_ALIQUOTA = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT ALIQUOTA FROM VPRODUTOTRIBUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND CODTRIBUTO = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODPRODUTO"], dtTipoTributo.Rows[iTributo]["CODTRIBUTO"] }));
                                        break;
                                    //Tipo Operação
                                    case "2":
                                        TRB_ALIQUOTA = Convert.ToDecimal(dtTipoTributo.Rows[iTributo]["ALIQUOTA"].ToString());
                                        break;
                                    //Natureza
                                    case "3":
                                        TRB_ALIQUOTA = 0;

                                        if (string.IsNullOrEmpty(dtItens.Rows[iItens]["CODNATUREZA"].ToString()))
                                        {
                                            MessageBox.Show("CFOP não localizada no item da operação " + codOper + ".", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }

                                        if (result["CODTIPOTRIBUTO"] == DBNull.Value)
                                        {
                                            MessageBox.Show("Tipo de tributo para o tributo " + dtTipoTributo.Rows[iTributo]["CODTRIBUTO"].ToString() + " não informado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                        else
                                        {
                                            if (result["CODTIPOTRIBUTO"].ToString() == "ICMS")
                                            {
                                                DataRow ALIQUOTA = VREGRAICMS.Rows.Find(new object[] { dtItens.Rows[iItens]["CODNATUREZA"].ToString() });
                                                TRB_ALIQUOTA = Convert.ToDecimal(ALIQUOTA["ALIQUOTA"]);
                                                //TRB_ALIQUOTA = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT ALIQUOTA FROM VREGRAICMS, VNATUREZA WHERE VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"] }));
                                            }
                                            else
                                            {

                                                TRB_ALIQUOTA = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT ALIQUOTA FROM VNATUREZATRIBUTO WHERE CODEMPRESA = ? AND CODNATUREZA = ? AND CODTRIBUTO = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"].ToString(), dtTipoTributo.Rows[iTributo]["CODTRIBUTO"] }));
                                            }
                                        }
                                        break;
                                    //Estado
                                    case "4":

                                        string codetd = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }).ToString();

                                        if (!string.IsNullOrEmpty(codetd))
                                        {
                                            TRB_ALIQUOTA = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT ALIQUOTA FROM VPRODUTOTRIBUTOESTADO WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND CODTRIBUTO = ? AND CODESTADO = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODPRODUTO"], dtTipoTributo.Rows[iTributo]["CODTRIBUTO"], codetd }));
                                        }
                                        else
                                        {
                                            TRB_ALIQUOTA = 0;
                                        }
                                        break;
                                    case "5":
                                        string codetd1 = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }).ToString();
                                        string ncm = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODNCM FROM VPRODUTO INNER JOIN GOPERITEM ON VPRODUTO.CODEMPRESA = GOPERITEM.CODEMPRESA AND VPRODUTO.CODPRODUTO = GOPERITEM.CODPRODUTO WHERE GOPERITEM.CODEMPRESA = ? AND GOPERITEM.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODPRODUTO"] }).ToString();

                                        TRB_ALIQUOTA = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT VREGRAVARCFOP.ALIQINTERNA FROM VREGRAVARCFOP WHERE NCM = ? AND UFDESTINO = ?", new object[] { ncm, codetd1 }));

                                        break;
                                    default:
                                        break;
                                }


                                #endregion

                                #region Valor Aliquota

                                bool utilizaST = Convert.ToBoolean(conn.ExecGetField(false, "SELECT VREGRAICMS.UTILIZAST FROM VREGRAICMS INNER JOIN VNATUREZA ON VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA WHERE VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"] }));

                                if (TRB_BASE_CALCULO == 0 || TRB_ALIQUOTA == 0)
                                {
                                    TRB_VALOR_ALIQUOTA = 0;
                                }
                                else
                                {
                                    TRB_VALOR_ALIQUOTA = ((TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100);
                                }
                                if (result["CODTIPOTRIBUTO"].ToString() == "ICMS-ST")
                                {
                                    if (utilizaST == false)
                                    {
                                        TRB_VALOR_ALIQUOTA = 0;
                                    }
                                }

                                if (dtTipoTributo.Rows[iTributo]["CODTRIBUTO"].ToString() == "ICMS")
                                {
                                    icms = TRB_VALOR_ALIQUOTA;
                                }
                                #endregion

                                #region ICMS-ST
                                decimal TRB_VICMSDIF = 0;
                                // Verificação do ICMS - ST


                                if (utilizaST == true)
                                {
                                    if (result["ALIQUOTAEM"].ToString() == "5")
                                    {
                                        DataTable selecaoMVA = conn.ExecQuery(@"SELECT REDUCAOBCST, SELECAOMVAST FROM 
                                                                    VREGRAICMS 
                                                                    INNER JOIN VNATUREZA ON VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA
                                                                    WHERE 
                                                                    VNATUREZA.CODNATUREZA = ?
                                                                    AND VNATUREZA.CODEMPRESA = ?", new object[] { dtItens.Rows[iItens]["CODNATUREZA"], AppLib.Context.Empresa });

                                        if (selecaoMVA.Rows.Count > 0)
                                        {
                                            TRB_REDUCAOBASEICMSST = Convert.ToDecimal(selecaoMVA.Rows[0]["REDUCAOBCST"]);
                                            string codetd1 = conn.ExecGetField(string.Empty, "SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }).ToString();
                                            string ncm = conn.ExecGetField(string.Empty, "SELECT CODNCM FROM VPRODUTO INNER JOIN GOPERITEM ON VPRODUTO.CODEMPRESA = GOPERITEM.CODEMPRESA AND VPRODUTO.CODPRODUTO = GOPERITEM.CODPRODUTO WHERE GOPERITEM.CODEMPRESA = ? AND GOPERITEM.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODPRODUTO"] }).ToString();
                                            DataTable dt = conn.ExecQuery(@"SELECT MVAAJUSTADO, MVAORIGINAL, MVAAJUSTADOMATIMPORT FROM VREGRAVARCFOP WHERE NCM = ? AND UFDESTINO = ?", new object[] { ncm, codetd1 });

                                            switch (selecaoMVA.Rows[0]["SELECAOMVAST"].ToString())
                                            {
                                                case "O":
                                                    //TRB_BASE_CALCULO = TRB_BASE_CALCULO + (TRB_BASE_CALCULO * (Convert.ToDecimal(dt.Rows[0]["MVAORIGINAL"]) / 100));
                                                    TRB_BASE_CALCULO = TRB_BASE_CALCULO * (1 - (Convert.ToDecimal(selecaoMVA.Rows[0]["REDUCAOBCST"]) / 100));
                                                    TRB_VALORICMSST = ((TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100) - icms;
                                                    TRB_VALOR_ALIQUOTA = (TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100;
                                                    TRB_FATORMVA = Convert.ToDecimal(dt.Rows[0]["MVAORIGINAL"]);
                                                    break;
                                                case "A":
                                                    //TRB_BASE_CALCULO = TRB_BASE_CALCULO + (TRB_BASE_CALCULO * (Convert.ToDecimal(dt.Rows[0]["MVAAJUSTADO"]) / 100));
                                                    TRB_BASE_CALCULO = TRB_BASE_CALCULO * (1 - (Convert.ToDecimal(selecaoMVA.Rows[0]["REDUCAOBCST"]) / 100));
                                                    TRB_VALORICMSST = ((TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100) - icms;
                                                    TRB_VALOR_ALIQUOTA = (TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100;
                                                    TRB_FATORMVA = Convert.ToDecimal(dt.Rows[0]["MVAAJUSTADO"]);
                                                    break;
                                                case "I":
                                                    //TRB_BASE_CALCULO = TRB_BASE_CALCULO + (TRB_BASE_CALCULO * (Convert.ToDecimal(dt.Rows[0]["MVAAJUSTADOMATIMPORT"]) / 100));
                                                    TRB_BASE_CALCULO = TRB_BASE_CALCULO * (1 - (Convert.ToDecimal(selecaoMVA.Rows[0]["REDUCAOBCST"]) / 100));
                                                    TRB_VALORICMSST = ((TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100) - icms;
                                                    TRB_VALOR_ALIQUOTA = (TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100;
                                                    TRB_FATORMVA = Convert.ToDecimal(dt.Rows[0]["MVAAJUSTADOMATIMPORT"]);
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                }

                                #endregion

                                #region DIF
                                if (result["CODTIPOTRIBUTO"].ToString() == "ICMS")
                                {

                                    string tipoTributacao = conn.ExecGetField(string.Empty, @"SELECT VREGRAICMS.TIPOTRIBUTACAO 
                                                                    FROM VREGRAICMS 
                                                                    INNER JOIN VNATUREZA ON VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA
                                                                    WHERE VNATUREZA.CODNATUREZA = ? AND VNATUREZA.CODEMPRESA = ?", new object[] { dtItens.Rows[iItens]["CODNATUREZA"].ToString(), AppLib.Context.Empresa }).ToString();
                                    if (tipoTributacao == "D")
                                    {
                                        pdif = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT PDIF FROM VPRODUTOFISCAL 
                                                                    WHERE 
                                                                    CODETD = (SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR AND VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA WHERE CODOPER = ?)
                                                                    AND CODEMPRESA = ?
                                                                    AND CODPRODUTO = ?", new object[] { codOper, AppLib.Context.Empresa, dtItens.Rows[iItens]["CODPRODUTO"] }));
                                        TRB_VICMSDIF = ((TRB_BASE_CALCULO * TRB_ALIQUOTA) / 100) * (pdif / 100);
                                        TRB_VALOR_ALIQUOTA = TRB_VALOR_ALIQUOTA - TRB_VICMSDIF;
                                    }
                                }
                                #endregion

                                #region CST

                                if (result["CODTIPOTRIBUTO"].ToString() == "ICMS")
                                {
                                    TRB_CST = conn.ExecGetField(string.Empty, @"SELECT CODCST FROM VREGRAICMS, VNATUREZA WHERE VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"].ToString() }).ToString();
                                }
                                else
                                {
                                    if (result["CODTIPOTRIBUTO"].ToString() == "IPI")
                                    {
                                        if (dtTipOper.Rows[0]["TIPENTSAI"].ToString() == "E")
                                        {
                                            TRB_CST = conn.ExecGetField(string.Empty, @"SELECT CODCSTENT FROM VREGRAIPI, VNATUREZA WHERE VREGRAIPI.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAIPI.IDREGRA = VNATUREZA.IDREGRAIPI AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"].ToString() }).ToString();
                                        }
                                        else
                                        {
                                            TRB_CST = conn.ExecGetField(string.Empty, @"SELECT CODCSTSAI FROM VREGRAIPI, VNATUREZA WHERE VREGRAIPI.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAIPI.IDREGRA = VNATUREZA.IDREGRAIPI AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"].ToString() }).ToString();
                                        }
                                    }
                                    else
                                    {
                                        TRB_CST = conn.ExecGetField(string.Empty, @"SELECT CODCST FROM VNATUREZATRIBUTO, VNATUREZA WHERE VNATUREZATRIBUTO.CODEMPRESA = VNATUREZA.CODEMPRESA AND VNATUREZATRIBUTO.CODNATUREZA = VNATUREZA.CODNATUREZA AND VNATUREZATRIBUTO.CODEMPRESA = ? AND VNATUREZATRIBUTO.CODNATUREZA = ? AND VNATUREZATRIBUTO.CODTRIBUTO = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"].ToString(), dtTipoTributo.Rows[iTributo]["CODTRIBUTO"] }).ToString();

                                        if (string.IsNullOrEmpty(TRB_CST))
                                        {
                                            TRB_CST = conn.ExecGetField(0, @"SELECT CODCST FROM VPRODUTOTRIBUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ? AND CODTRIBUTO = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODPRODUTO"], dtTipoTributo.Rows[iTributo]["CODTRIBUTO"] }).ToString();
                                        }

                                        if (string.IsNullOrEmpty(TRB_CST))
                                        {
                                            TRB_CST = dtTipoTributo.Rows[iTributo]["CODCST"].ToString();
                                        }
                                    }
                                }

                                if (string.IsNullOrEmpty(TRB_CST))
                                {
                                    MessageBox.Show(string.Concat("CST não encontrada para o Tributo: ", dtTipoTributo.Rows[iTributo]["CODTRIBUTO"].ToString()), "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                #endregion

                                #region Modalidade BC ICMS/ ICMS ST

                                if (result["CODTIPOTRIBUTO"].ToString() == "ICMS" || result["CODTIPOTRIBUTO"].ToString() == "ICMS-ST")
                                {
                                    string campo = string.Empty;
                                    if (result["CODTIPOTRIBUTO"].ToString() == "ICMS")
                                        campo = "MODALIDADEICMS";
                                    if (result["CODTIPOTRIBUTO"].ToString() == "ICMS-ST")
                                        campo = "MODALIDADEICMSST";
                                    string codetd1 = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODETD FROM VCLIFOR INNER JOIN GOPER ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa }).ToString();
                                    string ncm = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODNCM FROM VPRODUTO INNER JOIN GOPERITEM ON VPRODUTO.CODEMPRESA = GOPERITEM.CODEMPRESA AND VPRODUTO.CODPRODUTO = GOPERITEM.CODPRODUTO WHERE GOPERITEM.CODEMPRESA = ? AND GOPERITEM.CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODPRODUTO"] }).ToString();
                                    sSql = "SELECT " + campo + @" FROM VREGRAVARCFOP WHERE NCM = ? AND UFDESTINO = ?";
                                    TRB_MODBC = Convert.ToInt32(conn.ExecGetField(-1, sSql, new object[] { ncm, codetd1 }));
                                    if (TRB_MODBC < 0)
                                        TRB_MODBC = null;

                                    #region Difal
                                    sSql = @"SELECT 
                                                                    VCLIFOR.CODETD UFDEST,
                                                                    GFILIAL.CODETD UFREM, 
                                                                    GESTADO.ALIQUOTAICMSINTERNADEST, 
                                                                    VREGRAICMS.ALIQUOTA ALIQUOTAINTERESTADUAL, 
                                                                    PERCICMSUFDEST, 
                                                                    VREGRAICMS.PERCFCP, 
                                                                    VREGRAICMS.DIFERENCIALALIQUOTA, 
                                                                    VCLIFOR.CONTRIBICMS, 
                                                                    GOPER.TIPOPERCONSFIN,
                                                                    GOPER.CLIENTERETIRA,
                                                                    VCLIFOR.PRODUTORRURAL
                                                                    FROM GOPER 
                                                                    INNER JOIN VCLIFOR ON GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA AND GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR 
                                                                    INNER JOIN GFILIAL ON GOPER.CODEMPRESA = GFILIAL.CODEMPRESA
                                                                    INNER JOIN GESTADO ON VCLIFOR.CODETD = GESTADO.CODETD
                                                                    INNER JOIN GOPERITEM ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER
                                                                    INNER JOIN VNATUREZA ON GOPERITEM.CODEMPRESA = VNATUREZA.CODEMPRESA AND GOPERITEM.CODNATUREZA = VNATUREZA.CODNATUREZA
                                                                    INNER JOIN VREGRAICMS ON VNATUREZA.CODEMPRESA = VREGRAICMS.CODEMPRESA AND VNATUREZA.IDREGRAICMS = VREGRAICMS.IDREGRA
                                                                    WHERE GOPER.CODOPER = ? AND GOPER.CODEMPRESA = ?";
                                    DataTable dtDifal = conn.ExecQuery(sSql, new object[] { codOper, AppLib.Context.Empresa });
                                    if (dtDifal.Rows.Count > 0)
                                    {
                                        AppLib.ORM.Jit reg = new AppLib.ORM.Jit(conn, "GOPERITEMDIFAL");
                                        reg.Set("CODEMPRESA", AppLib.Context.Empresa);
                                        reg.Set("CODOPER", codOper);
                                        reg.Set("NSEQITEM", dtItens.Rows[iItens]["NSEQITEM"].ToString());
                                        if (dtDifal.Rows[0]["DIFERENCIALALIQUOTA"].Equals(true))
                                        {
                                            if (dtDifal.Rows[0]["CONTRIBICMS"].ToString() == "2" && dtDifal.Rows[0]["TIPOPERCONSFIN"].Equals(true))
                                            {
                                                if (dtDifal.Rows[0]["PRODUTORRURAL"].Equals(false) || dtDifal.Rows[0]["CLIENTERETIRA"].Equals(true))
                                                {
                                                    decimal diferencial = Convert.ToDecimal(dtDifal.Rows[0]["ALIQUOTAICMSINTERNADEST"]) - Convert.ToDecimal(dtDifal.Rows[0]["ALIQUOTAINTERESTADUAL"]);
                                                    decimal difal = TRB_BASE_CALCULO * (diferencial / 100);
                                                    decimal partilhaDest = Convert.ToDecimal(dtDifal.Rows[0]["PERCICMSUFDEST"]);
                                                    partilhaDest = difal * (partilhaDest / 100);
                                                    decimal partilhaRem = difal - partilhaDest;
                                                    decimal fcp = TRB_BASE_CALCULO * (Convert.ToDecimal(dtDifal.Rows[0]["PERCFCP"]) / 100);
                                                    //Busca pra saber o que executar


                                                    reg.Set("VBCUFDEST", TRB_BASE_CALCULO);
                                                    reg.Set("PFCPUFDEST", dtDifal.Rows[0]["PERCFCP"]);
                                                    reg.Set("PICMSINTER", dtDifal.Rows[0]["ALIQUOTAINTERESTADUAL"]);
                                                    reg.Set("PICMSUFDEST", dtDifal.Rows[0]["ALIQUOTAICMSINTERNADEST"]);
                                                    reg.Set("PICMSINTERPART", dtDifal.Rows[0]["PERCICMSUFDEST"]);
                                                    reg.Set("VFCPUFDEST", fcp);
                                                    reg.Set("VICMSUFDEST", partilhaDest);
                                                    reg.Set("VICMSUFREMET", partilhaRem);
                                                    reg.Save();
                                                }
                                                else
                                                {
                                                    reg.Delete();
                                                }
                                            }
                                            else
                                            {
                                                reg.Delete();
                                            }
                                        }
                                        else
                                        {
                                            reg.Delete();
                                        }

                                    }
                                    #endregion
                                }
                                #endregion

                                #region Redução da BC

                                if (result["CODTIPOTRIBUTO"].ToString() == "ICMS")
                                {
                                    TRB_REDBC = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT REDUCAOBASEICMS FROM VREGRAICMS, VNATUREZA WHERE VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"].ToString() }));
                                }
                                if (result["CODTIPOTRIBUTO"].ToString() == "ICMS-ST")
                                {
                                    TRB_REDBC = Convert.ToDecimal(conn.ExecGetField(0, @"SELECT REDUCAOBCST 
                                                                    FROM 
                                                                    VREGRAICMS 
                                                                    INNER JOIN VNATUREZA ON VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA
                                                                    WHERE	VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"].ToString() }));
                                }

                                #endregion

                                #region Enquadramento IPI

                                if (result["CODTIPOTRIBUTO"].ToString() == "IPI")
                                {
                                    TRB_CENQ = conn.ExecGetField(string.Empty, @"SELECT CENQ FROM VREGRAIPI, VNATUREZA WHERE VREGRAIPI.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAIPI.IDREGRA = VNATUREZA.IDREGRAIPI AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"].ToString() }).ToString();
                                }

                                #endregion

                                #region Percentual de crédito de ICMS
                                decimal VCREDICMSSN = 0;
                                decimal pCREDSN = 0;
                                if (result["CODTIPOTRIBUTO"].ToString() == "ICMS")
                                {
                                    if (Convert.ToInt32(conn.ExecGetField(0, @"SELECT CREDITOICMS FROM VREGRAICMS, VNATUREZA WHERE VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?", new object[] { AppLib.Context.Empresa, dtItens.Rows[iItens]["CODNATUREZA"].ToString() })) == 1)
                                    {
                                        pCREDSN = Convert.ToDecimal(conn.ExecGetField(0, "SELECT PCREDSN FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, codFilial }));
                                        VCREDICMSSN = TRB_BASE_CALCULO * (pCREDSN / 100);
                                    }
                                }

                                #endregion

                                #region Popula Classe

                                string modalidadeBC = string.Empty;
                                string cenq = string.Empty;
                                if (string.IsNullOrEmpty(TRB_MODBC.ToString()))
                                {
                                    modalidadeBC = "''";
                                }
                                else
                                {
                                    modalidadeBC = TRB_MODBC.ToString();
                                }

                                if (string.IsNullOrEmpty(TRB_CENQ.ToString()))
                                {
                                    cenq = "''";
                                }
                                else
                                {
                                    cenq = TRB_CENQ;
                                }

                                insert = insert + @"INSERT INTO GOPERITEMTRIBUTO (CODEMPRESA, CODOPER, NSEQITEM, CODTRIBUTO, ALIQUOTA, VALOR, CODCST, BASECALCULO, MODALIDADEBC, REDUCAOBASEICMS, CENQ, FATORMVA, BCORIGINAL, REDUCAOBASEICMSST, VALORICMSST, PDIF, VICMSDIF, PCREDSN, VCREDICMSSN) VALUES (" + AppLib.Context.Empresa + ", " + codOper + "," + Convert.ToInt32(dtItens.Rows[iItens]["NSEQITEM"]) + ",'" + dtTipoTributo.Rows[iTributo]["CODTRIBUTO"].ToString() + "','" + string.Format("{0:n2}", TRB_ALIQUOTA).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", TRB_VALOR_ALIQUOTA).Replace(".", "").Replace(",", ".") + "','" + TRB_CST + "','" + string.Format("{0:n2}", TRB_BASE_CALCULO).Replace(".", "").Replace(",", ".") + "'," + modalidadeBC + ",'" + string.Format("{0:n2}", TRB_REDBC).Replace(".", "").Replace(",", ".") + "'," + cenq + ",'" + string.Format("{0:n2}", TRB_FATORMVA).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", TRB_BCORIGINAL).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", TRB_REDUCAOBASEICMSST).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", TRB_VALORICMSST).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", pdif).Replace(".", "").Replace(",", ".") + "','" + string.Format("{0:n2}", TRB_VICMSDIF).Replace(".", "").Replace(",", ".") + "', '" + string.Format("{0:n2}", pCREDSN).Replace(".", "").Replace(",", ".") + "', '" + string.Format("{0:n2}", VCREDICMSSN).Replace(".", "").Replace(",", ".") + "');\n\r";

                                TRB_ALIQUOTA = 0;
                                TRB_VALOR_ALIQUOTA = 0;
                                TRB_BASE_CALCULO = 0;
                                TRB_REDBC = 0;
                                TRB_CENQ = string.Empty;
                                TRB_FATORMVA = 0;
                                TRB_BCORIGINAL = 0;
                                TRB_REDUCAOBASEICMSST = 0;
                                TRB_VALORICMSST = 0;
                                pdif = 0;
                                pCREDSN = 0;
                                VCREDICMSSN = 0;
                                TRB_VICMSDIF = 0;
                                TRB_CST = string.Empty;
                                TRB_MODBC = null;
                                #endregion
                            }
                        }
                    }
                    conn.ExecTransaction(insert, new object[] { });
                    conn.Commit();
                }
            }
            catch (Exception)
            {
                conn.Rollback();
            }
        }

        private bool verificaValores(int codOper)
        {
            decimal valorFrete = 0, valorDesconto = 0, valorDespesa = 0, valorSeguro = 0;
            if (faturamento == true)
            {
                DataTable dtValoresOrigem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT VALORFRETE, VALORDESCONTO, VALORDESPESA, VALORSEGURO FROM GOPER WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { codOper, AppLib.Context.Empresa });
                if (dtValoresOrigem.Rows.Count > 0)
                {
                    valorFrete = Convert.ToDecimal(dtValoresOrigem.Rows[0]["VALORFRETE"]);
                    valorDesconto = Convert.ToDecimal(dtValoresOrigem.Rows[0]["VALORDESCONTO"]);
                    valorDespesa = Convert.ToDecimal(dtValoresOrigem.Rows[0]["VALORDESPESA"]);
                    valorSeguro = Convert.ToDecimal(dtValoresOrigem.Rows[0]["VALORSEGURO"]);
                }
            }

            if (vlFrete == valorFrete)
            {
                if (vlDesconto == valorDesconto)
                {
                    if (vlDespesa == valorDespesa)
                    {
                        if (vlSeguro == valorSeguro)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void calculaOperacao(int codOper, string codTipOper)
        {
            PS.Lib.Contexto.Session.key1 = AppLib.Context.Empresa;
            PS.Lib.Contexto.Session.key2 = codOper;
            PS.Lib.Contexto.Session.key3 = null;
            PS.Lib.Contexto.Session.key4 = null;
            PS.Lib.Contexto.Session.key5 = null;

            int FRM_CODEMPRESA = 0;
            string FRM_CODFORMULA = string.Empty;
            string FRM_CODFORMULAVALORBRUTO = string.Empty;
            string FRM_CODFORMULAVALORLIQUIDO = string.Empty;
            decimal OP_VALORBRUTO = 0;
            decimal OP_VALORLIQUIDO = 0;

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODFORMULAVALORBRUTO, CODFORMULAVALORLIQUIDO FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, codTipOper });

            if (dt.Rows.Count > 0)
            {
                FRM_CODEMPRESA = AppLib.Context.Empresa;
                FRM_CODFORMULAVALORBRUTO = dt.Rows[0]["CODFORMULAVALORBRUTO"].ToString();
                FRM_CODFORMULAVALORLIQUIDO = dt.Rows[0]["CODFORMULAVALORLIQUIDO"].ToString();
                try
                {
                    interpreta.comando = function.PreparaFormula(FRM_CODEMPRESA, FRM_CODFORMULAVALORBRUTO);
                    OP_VALORBRUTO = Convert.ToDecimal(interpreta.Executar().ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao executar a fórmula: " + FRM_CODFORMULAVALORBRUTO + ".\r\n" + ex.Message);
                }
                try
                {
                    interpreta.comando = function.PreparaFormula(FRM_CODEMPRESA, FRM_CODFORMULAVALORLIQUIDO);
                    OP_VALORLIQUIDO = Convert.ToDecimal(interpreta.Executar().ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao executar a fórmula: " + FRM_CODFORMULAVALORLIQUIDO + ".\r\n" + ex.Message);
                }
                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE GOPER SET VALORBRUTO = ?, VALORLIQUIDO = ? WHERE CODEMPRESA = ? AND CODOPER = ?", OP_VALORBRUTO, OP_VALORLIQUIDO, AppLib.Context.Empresa, codOper);

            }

            PS.Lib.Contexto.Session.key1 = null;
            PS.Lib.Contexto.Session.key2 = null;
            PS.Lib.Contexto.Session.key3 = null;
            PS.Lib.Contexto.Session.key4 = null;
            PS.Lib.Contexto.Session.key5 = null;
        }
    }
}