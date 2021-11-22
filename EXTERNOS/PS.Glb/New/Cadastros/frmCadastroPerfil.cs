using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroPerfil : Form
    {
        #region Variáveis

        private New.Classes.Perfil perfil = new Classes.Perfil();
        private DataTable dtUsuario = new DataTable();
        private DataTable dtTipoOperacao = new DataTable();

        public string codPerfil = "";
        public bool edita = false;

        #endregion

        public frmCadastroPerfil()
        {
            InitializeComponent();
        }

        private void frmCadastroPerfil_Load(object sender, EventArgs e)
        {
            if (edita == false)
            {
                // Usuários/Perfil
                CriarTabelaUsuario(edita);
                CarregaLookupUsuario();

                // Tipo de Operação
                CriarTabelaTipoOperacao(edita);

                // Acesso aos Menus
                CriarTreeListMenus();
            }
            else
            {
                // Perfil
                CarregaPerfil();
                DesabilitaCampos();

                // Usuários/Perfil
                CriarTabelaUsuario(edita, codPerfil);
                CarregaLookupUsuario();

                // Tipo de Operação
                CriarTabelaTipoOperacao(edita, codPerfil);

                // Acesso aos Menus
                CriarTreeListMenus();
                CarregaAcessoMenus();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Salvar();

            edita = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Salvar();
            this.Dispose();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Usuário/Perfil

        private void btnAdicionarUsuario_Click(object sender, EventArgs e)
        {
            dtUsuario.Rows.Add
                (
                AppLib.Context.Empresa,
                lpUsuario.EditValue.ToString(),
                lpUsuario.Text
                );
        }

        private void btnExcluirUsuario_Click(object sender, EventArgs e)
        {
            if (gvUsuario.SelectedRowsCount > 0)
            {
                DataRow row = gvUsuario.GetDataRow(Convert.ToInt32(gvUsuario.GetSelectedRows().GetValue(0).ToString()));

                if (perfil.ExisteUsuarioPerfil(row["Código do Usuário"].ToString(), tbCodPerfil.Text))
                {
                    try
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM GUSUARIOPERFIL WHERE CODEMPRESA = ? AND CODPERFIL = ? AND CODUSUARIO = ?", new object[] { AppLib.Context.Empresa, tbCodPerfil.Text, row["Código do Usuário"].ToString() });

                        dtUsuario.Rows.Remove(row);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("Não foi possível excluir o usuário selecionado.\r\nDetalhes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    dtUsuario.Rows.Remove(row);
                }
            }
        }

        private void btnPesquisarUsuario_Click(object sender, EventArgs e)
        {
            if (gvUsuario.OptionsFind.AlwaysVisible == true)
            {
                gvUsuario.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gvUsuario.OptionsFind.AlwaysVisible = true;
            }
        }

        private void btnAgruparUsuario_Click(object sender, EventArgs e)
        {
            if (gvUsuario.OptionsView.ShowGroupPanel == true)
            {
                gvUsuario.OptionsView.ShowGroupPanel = false;
                gvUsuario.ClearGrouping();
                btnAgruparUsuario.Text = "Agrupar";
            }
            else
            {
                gvUsuario.OptionsView.ShowGroupPanel = true;
                btnAgruparUsuario.Text = "Desagrupar";
            }
        }

        #endregion

        #region Tipo de Operação/Perfil

        private void btnPesquisarTipoOperacao_Click(object sender, EventArgs e)
        {
            if (gvTipoOperacao.OptionsFind.AlwaysVisible == true)
            {
                gvTipoOperacao.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gvTipoOperacao.OptionsFind.AlwaysVisible = true;
            }
        }

        private void btnAgruparTipoOperacao_Click(object sender, EventArgs e)
        {
            if (gvTipoOperacao.OptionsView.ShowGroupPanel == true)
            {
                gvTipoOperacao.OptionsView.ShowGroupPanel = false;
                gvTipoOperacao.ClearGrouping();
                btnAgruparTipoOperacao.Text = "Agrupar";
            }
            else
            {
                gvTipoOperacao.OptionsView.ShowGroupPanel = true;
                btnAgruparTipoOperacao.Text = "Desagrupar";
            }
        }

        private void btnSelecionarTodos_Click(object sender, EventArgs e)
        {
            SelecionarTipoOperacao(true);
        }

        private void selecionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gvTipoOperacao.SelectedRowsCount > 0)
            {
                DataRow rowTipoOperacao = gvTipoOperacao.GetDataRow(Convert.ToInt32(gvTipoOperacao.GetSelectedRows().GetValue(0).ToString()));

                SelecionarTipoOperacao(true, rowTipoOperacao);
            }
        }

        private void desselecionarLinhaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gvTipoOperacao.SelectedRowsCount > 0)
            {
                DataRow rowTipoOperacao = gvTipoOperacao.GetDataRow(Convert.ToInt32(gvTipoOperacao.GetSelectedRows().GetValue(0).ToString()));

                SelecionarTipoOperacao(false, rowTipoOperacao);
            }
        }

        private void btnDesselecionarTodos_Click(object sender, EventArgs e)
        {
            SelecionarTipoOperacao(false);
        }

        private void gvTipoOperacao_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (Convert.ToBoolean(e.Value) == true)
            {
                gvTipoOperacao.SetRowCellValue(e.RowHandle, e.Column, true);
                gvTipoOperacao.SetRowCellValue(e.RowHandle, gvTipoOperacao.Columns["Consultar"], true);

                return;
            }
            else
            {
                gvTipoOperacao.SetRowCellValue(e.RowHandle, e.Column, false);

                return;
            }
        }

        #endregion

        #region Acesso ao Menu

        private void treeList1_AfterCheckNode(object sender, NodeEventArgs e)
        {
            if (e.Node.CheckState == CheckState.Checked)
            {
                ValidarHierarquiaNos(e.Node);
            }
        }

        #region Configurações

        private void btnExpandirTudo_Click(object sender, EventArgs e)
        {
            treeList1.ExpandAll();
        }

        private void btnRecolherTudo_Click(object sender, EventArgs e)
        {
            treeList1.CollapseAll();
        }

        private void btnExpandirGrupo_Click(object sender, EventArgs e)
        {
            treeList1.FocusedNode.ExpandAll();
        }

        private void btnRecolherGrupo_Click(object sender, EventArgs e)
        {
            treeList1.FocusedNode.Collapse();
        }

        private void btnPermitirAcesso_Click(object sender, EventArgs e)
        {
            treeList1.FocusedNode.CheckState = CheckState.Checked;
        }

        private void btnProibirAcesso_Click(object sender, EventArgs e)
        {
            treeList1.FocusedNode.CheckState = CheckState.Unchecked;
        }

        #endregion

        #endregion

        #region Métodos

        private List<string> ConverterDataTableParaListString(DataTable dtOrigem, string coluna)
        {
            List<string> listString = dtOrigem.Rows.OfType<DataRow>().Select(dr => (string)dr[coluna]).ToList();

            return listString;
        }

        private List<int> ConverterDataTableParaListInt(DataTable dtOrigem, string coluna)
        {
            List<int> listInt = dtOrigem.Rows.OfType<DataRow>().Select(dr => Convert.ToInt32(dr[coluna])).ToList();

            return listInt;
        }

        private List<string> ObterModulos()
        {
            List<string> listString = new List<string> { };

            string modulo = "";

            // Obtém a lista contendo todos os nós de acesso do sistema.
            var nodes = treeList1.GetNodeList();

            // Módulos selecionados
            foreach (var node in nodes.Where(l => l.Level == 1))
            {
                string descricaoModulo = node.GetValue("Permissões").ToString();
                modulo = ObterModuloPorDescricao(descricaoModulo);

                listString.Add(modulo);
            }

            return listString;
        }

        private List<int> ObterAcessoModulos()
        {
            List<int> listAcessoModulo = new List<int> { };

            // Obtém a lista contendo todos os nós de acesso do sistema.
            var nodes = treeList1.GetNodeList();

            foreach (var node in nodes.Where(l => l.Level == 1))
            {
                if (node.Checked)
                {
                    listAcessoModulo.Add(1);
                }
                else
                {
                    listAcessoModulo.Add(0);
                }
            }

            return listAcessoModulo;
        }

        private List<Classes.Models.GPERMISSAOMENU> ObterAcessoMenus()
        {
            List<Classes.Models.GPERMISSAOMENU> listPermissaoMenu = new List<Classes.Models.GPERMISSAOMENU> { };
            Classes.Models.GPERMISSAOMENU permissaoMenu;

            // Obtém a lista contendo todos os nós de acesso do sistema.
            var nodes = treeList1.GetNodeList();

            // Menus selecionados
            foreach (var node in nodes.Where(l => l.Level == 4))
            {
                permissaoMenu = new Classes.Models.GPERMISSAOMENU();

                string descricaoMenu = node.GetValue("Permissões").ToString();
                string menu = ObterMenuPorDescricao(descricaoMenu);

                permissaoMenu.IDPermissaoMenu = 0;
                permissaoMenu.CodPerfil = tbCodPerfil.Text;
                permissaoMenu.CodMenu = menu;

                foreach (var nodeAcesso in node.Nodes.Where(l => l.Level == 5))
                {
                    string tipoPermissao = nodeAcesso.GetValue("Permissões").ToString();

                    switch (tipoPermissao)
                    {
                        case "Acessar":

                            if (nodeAcesso.Checked)
                            {
                                permissaoMenu.Acesso = 1;
                            }

                            break;

                        case "Cadastrar":

                            if (nodeAcesso.Checked)
                            {
                                permissaoMenu.Inclusao = 1;
                            }

                            break;

                        case "Editar":

                            if (nodeAcesso.Checked)
                            {
                                permissaoMenu.Edicao = 1;
                            }

                            break;

                        case "Excluir":

                            if (nodeAcesso.Checked)
                            {
                                permissaoMenu.Exclusao = 1;
                            }

                            break;

                        default:
                            break;
                    }
                }

                listPermissaoMenu.Add(permissaoMenu);
            }

            return listPermissaoMenu;
        }

        private string ObterModuloPorDescricao(string descricao)
        {
            switch (descricao)
            {
                case "Gestão de Administração":
                    return "ADM";
                case "Gestão de Arquivos Digitais":
                    return "ARQ";
                case "Gestão de Contabilidade":
                    return "CON";
                case "Gestão de Atendimento (CRM)":
                    return "CRM";
                case "Gestão de Finanças":
                    return "FIN";
                case "Gestão de Tributação":
                    return "FIS";
                case "Global":
                    return "GLO";
                case "Gestão de Imóveis":
                    return "INC";
                case "Gestão de Materiais":
                    return "MAT";
                case "Gestão de Patrimônio":
                    return "PAT";
                case "Módulo de Personalização do Usuário":
                    return "PER";
                case "Gestão de Projetos":
                    return "PRJ";
                case "Gestão de Produção":
                    return "PRO";
                case "Gestão de Serviços":
                    return "SER";
                default:
                    return "";
            }
        }

        private string ObterMenuPorDescricao(string descricao)
        {
            string menu = AppLib.Context.poolConnection.Get("Start").ExecGetField("", @"SELECT CODMENU FROM GMENU WHERE DESCRICAO = ?", new object[] { descricao }).ToString();

            return menu;
        }

        private void Salvar()
        {
            // Perfil
            perfil.GetPerfil
                (
                tbCodPerfil.Text,
                tbNome.Text,
                Convert.ToInt32(chkAtivo.Checked)
                );

            // Usuário/Perfil
            perfil.GetUsuarioPerfil
                (
                AppLib.Context.Empresa,
                tbCodPerfil.Text,
                ConverterDataTableParaListString(dtUsuario, "Código do Usuário")
                );

            // Tipo de Operação
            perfil.GetPerfilTipOper
                (
                AppLib.Context.Empresa,
                tbCodPerfil.Text,
                ConverterDataTableParaListString(dtTipoOperacao, "Código do Tipo de Operação"),
                ConverterDataTableParaListInt(dtTipoOperacao, "Incluir"),
                ConverterDataTableParaListInt(dtTipoOperacao, "Excluir"),
                ConverterDataTableParaListInt(dtTipoOperacao, "Alterar"),
                ConverterDataTableParaListInt(dtTipoOperacao, "Faturar"),
                ConverterDataTableParaListInt(dtTipoOperacao, "Incluir Fatura"),
                ConverterDataTableParaListInt(dtTipoOperacao, "Consultar"),
                ConverterDataTableParaListInt(dtTipoOperacao, "Cancelar"),
                ConverterDataTableParaListInt(dtTipoOperacao, "Concluir"),
                ConverterDataTableParaListInt(dtTipoOperacao, "Aprovar"),
                ConverterDataTableParaListInt(dtTipoOperacao, "Aprovar Financeiro"),
                ConverterDataTableParaListInt(dtTipoOperacao, "Aprovar Desconto"),
                ConverterDataTableParaListInt(dtTipoOperacao, "Aprovar Limite Crédito"),
                ConverterDataTableParaListInt(dtTipoOperacao, "Reprovar"),
                ConverterDataTableParaListInt(dtTipoOperacao, "Gerar Boleto")
                );

            // Permissão do Módulo
            perfil.GetPermissaoModulo
                (0,
                ObterModulos(),
                tbCodPerfil.Text,
                ObterAcessoModulos()
                );

            // Permissão do Menu
            perfil.GetPermissaoMenu(ObterAcessoMenus());

            try
            {
                perfil.Salvar(edita);

                XtraMessageBox.Show("Perfil salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Não foi possível salvar o Perfil.\r\nDetalhes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        #region Perfil 

        private void CarregaPerfil()
        {
            DataTable dtPerfil = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM GPERFIL WHERE CODPERFIL = ?", new object[] { codPerfil });

            tbCodPerfil.Text = dtPerfil.Rows[0]["CODPERFIL"].ToString();
            tbNome.Text = dtPerfil.Rows[0]["NOME"].ToString();
            chkAtivo.Checked = Convert.ToBoolean(dtPerfil.Rows[0]["ATIVO"]);
        }

        private void DesabilitaCampos()
        {
            tbCodPerfil.Enabled = false;
        }

        #endregion

        #region Usuários/Perfil

        private void CriarTabelaUsuario(bool edita, string codPerfil = "")
        {
            dtUsuario = perfil.CriarSchemaUsuario();

            if (edita == true)
            {
                dtUsuario = perfil.CriarTabelaUsuarioPerfil(codPerfil);
            }

            gcUsuario.DataSource = dtUsuario;
            gvUsuario.BestFitColumns();
        }

        private void CarregaLookupUsuario()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODUSUARIO AS 'Código', NOME AS 'Nome'
                                                                                    FROM GUSUARIO
                                                                                    WHERE ATIVO = 1 ORDER BY NOME ASC", new object[] { });

            lpUsuario.Properties.DataSource = dt;
            lpUsuario.Properties.DisplayMember = dt.Columns["Nome"].ToString();
            lpUsuario.Properties.ValueMember = dt.Columns["Código"].ToString();
            lpUsuario.Properties.NullText = "Selecione...";

            lpUsuario.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
        }

        #endregion

        #region Tipo de Operação

        private void CriarTabelaTipoOperacao(bool edita, string codPerfil = "")
        {
            dtTipoOperacao = perfil.CriarTabelaTipoOperacao(edita, codPerfil);

            gcTipoOperacao.DataSource = dtTipoOperacao;
            gvTipoOperacao.OptionsView.ColumnAutoWidth = false;
            gvTipoOperacao.BestFitColumns();

            perfil.ConfigurarEdicaoColunasGridView(gvTipoOperacao);
        }

        private void SelecionarTipoOperacao(bool marcar, DataRow rowTipoOperacao = null)
        {
            if (rowTipoOperacao == null)
            {
                // Selecionar/Desmarcar todos

                for (int i = 0; i < dtTipoOperacao.Rows.Count; i++)
                {
                    dtTipoOperacao.Rows[i]["Incluir"] = marcar;
                    dtTipoOperacao.Rows[i]["Excluir"] = marcar;
                    dtTipoOperacao.Rows[i]["Alterar"] = marcar;
                    dtTipoOperacao.Rows[i]["Faturar"] = marcar;
                    dtTipoOperacao.Rows[i]["Incluir Fatura"] = marcar;
                    dtTipoOperacao.Rows[i]["Consultar"] = marcar;
                    dtTipoOperacao.Rows[i]["Cancelar"] = marcar;
                    dtTipoOperacao.Rows[i]["Concluir"] = marcar;
                    dtTipoOperacao.Rows[i]["Aprovar"] = marcar;
                    dtTipoOperacao.Rows[i]["Aprovar Financeiro"] = marcar;
                    dtTipoOperacao.Rows[i]["Aprovar Desconto"] = marcar;
                    dtTipoOperacao.Rows[i]["Aprovar Limite Crédito"] = marcar;
                    dtTipoOperacao.Rows[i]["Reprovar"] = marcar;
                    dtTipoOperacao.Rows[i]["Gerar Boleto"] = marcar;
                }
            }
            else
            {
                rowTipoOperacao["Incluir"] = marcar;
                rowTipoOperacao["Excluir"] = marcar;
                rowTipoOperacao["Alterar"] = marcar;
                rowTipoOperacao["Faturar"] = marcar;
                rowTipoOperacao["Incluir Fatura"] = marcar;
                rowTipoOperacao["Consultar"] = marcar;
                rowTipoOperacao["Cancelar"] = marcar;
                rowTipoOperacao["Concluir"] = marcar;
                rowTipoOperacao["Aprovar"] = marcar;
                rowTipoOperacao["Aprovar Financeiro"] = marcar;
                rowTipoOperacao["Aprovar Desconto"] = marcar;
                rowTipoOperacao["Aprovar Limite Crédito"] = marcar;
                rowTipoOperacao["Reprovar"] = marcar;
                rowTipoOperacao["Gerar Boleto"] = marcar;
            }
        }

        #endregion

        #region Acesso aos módulos/menus

        private void CriarColunaArvoreMenus(TreeList treeList)
        {
            treeList.BeginUpdate();

            TreeListColumn coluna = treeList.Columns.Add();

            coluna.Caption = "Permissões";
            coluna.VisibleIndex = 0;

            treeList1.EndUpdate();
        }

        private void CriarNoModulo(TreeList treeList)
        {
            TreeListNode nodeTituloModulo = null;
            TreeListNode nodeModulo = null;

            DataTable dtModulos = new DataTable();

            treeList.BeginUnboundLoad();

            nodeTituloModulo = treeList.AppendNode(null, null);
            nodeTituloModulo.SetValue("Permissões", "Módulos");

            dtModulos = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT M.DESCRICAO AS 'MODULO', M.CODMODULO FROM GMODULO M INNER JOIN GMODULOEMPRESA E ON E.CODMODULO = M.CODMODULO WHERE E.ATIVO = 1 AND E.CODEMPRESA = ? ORDER BY M.CODMODULO ASC", new object[] { AppLib.Context.Empresa });

            foreach (DataRow row in dtModulos.Rows)
            {
                nodeModulo = new TreeListNode();

                // Adiciona os Módulos 
                nodeModulo = treeList1.AppendNode(null, nodeTituloModulo);
                nodeModulo.SetValue("Permissões", row["MODULO"].ToString());

                // Adiciona as páginas
                CriarNoPagina(treeList1, nodeModulo, row["CODMODULO"].ToString());
            }

            treeList.EndUnboundLoad();
        }

        private void CriarNoPagina(TreeList treeList, TreeListNode nodeModulo, string codModulo)
        {
            TreeListNode nodePagina = null;

            DataTable dtPaginas = new DataTable();

            dtPaginas = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODMODULO AS 'MODULO', PAGINA FROM GMENU WHERE CODMODULO = '" + codModulo + "' GROUP BY CODMODULO, PAGINA ORDER BY CODMODULO ASC");

            foreach (DataRow row in dtPaginas.Rows)
            {
                nodePagina = new TreeListNode();

                nodePagina = treeList.AppendNode(null, nodeModulo);
                nodePagina.SetValue("Permissões", row["PAGINA"].ToString());

                CriarNoGrupo(treeList1, nodePagina, codModulo, row["PAGINA"].ToString());
            }
        }

        private void CriarNoGrupo(TreeList treeList, TreeListNode nodePagina, string codModulo, string pagina)
        {
            TreeListNode nodeGrupo = null;

            DataTable dtGrupo = new DataTable();

            dtGrupo = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODMODULO AS 'MODULO', PAGINA, GRUPO FROM GMENU WHERE CODMODULO = '" + codModulo + "' AND PAGINA = '" + pagina + "' GROUP BY CODMODULO, PAGINA, GRUPO ORDER BY CODMODULO ASC");

            foreach (DataRow row in dtGrupo.Rows)
            {
                nodeGrupo = new TreeListNode();

                nodeGrupo = treeList.AppendNode(null, nodePagina);
                nodeGrupo.SetValue("Permissões", row["GRUPO"].ToString());

                CriarNoMenus(treeList1, nodeGrupo, codModulo, pagina, row["GRUPO"].ToString());
            }
        }

        private void CriarNoMenus(TreeList treeList, TreeListNode nodeGrupo, string codModulo, string pagina, string grupo)
        {
            TreeListNode nodeMenu = null;

            DataTable dtGrupo = new DataTable();

            dtGrupo = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODMODULO AS 'MODULO', PAGINA, GRUPO, CODMENU, DESCRICAO AS 'MENU' FROM GMENU WHERE CODMODULO = '" + codModulo + "' AND PAGINA = '" + pagina + "' AND GRUPO = '" + grupo + "' GROUP BY CODMODULO, PAGINA, GRUPO, CODMENU, DESCRICAO ORDER BY CODMODULO, PAGINA, GRUPO ASC");

            foreach (DataRow row in dtGrupo.Rows)
            {
                nodeMenu = new TreeListNode();

                nodeMenu = treeList.AppendNode(null, nodeGrupo);
                nodeMenu.SetValue("Permissões", row["MENU"].ToString());

                CriarNosPermissoes(treeList1, nodeMenu, row["CODMENU"].ToString());
            }
        }

        private void CriarNosPermissoes(TreeList treeList, TreeListNode nodeMenu, string codMenu)
        {
            TreeListNode nodePermissaoAcesso = null;
            TreeListNode nodePermissaoCadastro = null;
            TreeListNode nodePermissaoEdicao = null;
            TreeListNode nodePermissaoExclusao = null;

            DataTable dtPermissao = new DataTable();

            dtPermissao = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
                                                                            P.ACESSO AS 'Acessar', 
                                                                            P.INCLUSAO AS 'Cadastrar', 
                                                                            P.EDICAO AS 'Editar', 
                                                                            P.EXCLUSAO AS 'Excluir'
                                                                            FROM GPERMISSAOMENU P
                                                                            INNER JOIN GMENU M
                                                                            ON P.CODMENU = M.CODMENU WHERE M.CODMENU = ?", new object[] { codMenu });

            foreach (DataRow row in dtPermissao.Rows)
            {
                nodePermissaoAcesso = new TreeListNode();
                nodePermissaoCadastro = new TreeListNode();
                nodePermissaoEdicao = new TreeListNode();
                nodePermissaoExclusao = new TreeListNode();

                nodePermissaoAcesso = treeList.AppendNode(null, nodeMenu);
                nodePermissaoCadastro = treeList.AppendNode(null, nodeMenu);
                nodePermissaoEdicao = treeList.AppendNode(null, nodeMenu);
                nodePermissaoExclusao = treeList.AppendNode(null, nodeMenu);

                nodePermissaoAcesso.SetValue("Permissões", row.Table.Columns["Acessar"].ToString());
                nodePermissaoCadastro.SetValue("Permissões", row.Table.Columns["Cadastrar"].ToString());
                nodePermissaoEdicao.SetValue("Permissões", row.Table.Columns["Editar"].ToString());
                nodePermissaoExclusao.SetValue("Permissões", row.Table.Columns["Excluir"].ToString());

                // Encerra o loop para inserir apenas um grupo de permissões
                break;
            }
        }

        private void CriarTreeListMenus()
        {
            CriarColunaArvoreMenus(treeList1);
            CriarNoModulo(treeList1);
        }

        private void ValidarHierarquiaNos(TreeListNode node)
        {
            // Módulos (Todos)
            if (node.Level == 0)
            {
                treeList1.CheckAll();
            }
            // Módulo
            else if (node.Level == 1)
            {
                // Módulos
                treeList1.FindNodeByFieldValue("Permissões", "Módulos").Checked = true;

                // Módulo
                node.CheckAll();
            }
            // Página
            else if (node.Level == 2)
            {
                // Módulos
                treeList1.FindNodeByFieldValue("Permissões", "Módulos").Checked = true;

                // Módulo
                treeList1.FindNodeByFieldValue("Permissões", node.ParentNode.GetValue("Permissões")).Checked = true;

                // Página
                node.CheckAll();
            }
            // Grupo
            else if (node.Level == 3)
            {
                // Módulos
                treeList1.FindNodeByFieldValue("Permissões", "Módulos").Checked = true;

                // Módulo
                var modulo = node.ParentNode.ParentNode.GetValue("Permissões");
                treeList1.FindNodeByFieldValue("Permissões", modulo).Checked = true;

                // Página
                var pagina = node.ParentNode.GetValue("Permissões");
                treeList1.FindNodeByFieldValue("Permissões", pagina).Checked = true;

                node.CheckAll();
            }
            // Menu
            else if (node.Level == 4)
            {
                // Módulos
                treeList1.FindNodeByFieldValue("Permissões", "Módulos").Checked = true;

                // Módulo
                var modulo = node.ParentNode.ParentNode.ParentNode.GetValue("Permissões");
                treeList1.FindNodeByFieldValue("Permissões", modulo).Checked = true;

                // Página
                var pagina = node.ParentNode.ParentNode.GetValue("Permissões");
                treeList1.FindNodeByFieldValue("Permissões", pagina).Checked = true;

                // Grupo
                var grupo = node.ParentNode.GetValue("Permissões");
                treeList1.FindNodeByFieldValue("Permissões", grupo).Checked = true;

                node.CheckAll();
            }
            // Permissões
            else if (node.Level == 5)
            {
                // Módulos
                treeList1.FindNodeByFieldValue("Permissões", "Módulos").Checked = true;

                // Módulo
                var modulo = node.ParentNode.ParentNode.ParentNode.ParentNode.GetValue("Permissões");
                treeList1.FindNodeByFieldValue("Permissões", modulo).Checked = true;

                // Página
                var pagina = node.ParentNode.ParentNode.ParentNode.GetValue("Permissões");
                treeList1.FindNodeByFieldValue("Permissões", pagina).Checked = true;

                // Grupo
                var grupo = node.ParentNode.ParentNode.GetValue("Permissões");
                treeList1.FindNodeByFieldValue("Permissões", grupo).Checked = true;

                // Menu
                var menu = node.ParentNode.GetValue("Permissões");
                treeList1.FindNodeByFieldValue("Permissões", menu).Checked = true;

                node.Checked = true;
            }
        }

        private void CarregaAcessoMenus()
        {
            DataTable dtMenus;

            var nodes = treeList1.GetNodeList();

            // Menus
            dtMenus = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT P.CODMENU, P.ACESSO, P.EDICAO, P.INCLUSAO, P.EXCLUSAO, P.CONSULTA
                                                                                FROM GPERMISSAOMENU P
                                                                                WHERE P.CODPERFIL = ?", new object[] { codPerfil });


            foreach (var nodeMenu in nodes.Where(l => l.Level == 4))
            {
                string descricaoMenu = nodeMenu.GetValue("Permissões").ToString();
                string menu = ObterMenuPorDescricao(descricaoMenu);

                dtMenus = dtMenus = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT P.CODMENU, P.ACESSO, P.EDICAO, P.INCLUSAO, P.EXCLUSAO
                                                                                            FROM GPERMISSAOMENU P
                                                                                            WHERE P.CODPERFIL = ? AND P.CODMENU = ?", new object[] { codPerfil, menu });

                // Acesso
                if (Convert.ToInt32(dtMenus.Rows[0]["ACESSO"]) == 1)
                {
                    // Título Módulo
                    nodeMenu.ParentNode.ParentNode.ParentNode.ParentNode.Checked = true;

                    // Módulo
                    nodeMenu.ParentNode.ParentNode.ParentNode.Checked = true;

                    // Página
                    nodeMenu.ParentNode.ParentNode.Checked = true;

                    // Grupo
                    nodeMenu.ParentNode.Checked = true;

                    // Menu
                    nodeMenu.Checked = true;

                    foreach (var nodeAcesso in nodeMenu.Nodes.Where(l => l.Level == 5))
                    {
                        string tipoPermissao = nodeAcesso.GetValue("Permissões").ToString();

                        switch (tipoPermissao)
                        {
                            case "Acessar":

                                if (Convert.ToInt32(dtMenus.Rows[0]["ACESSO"]) == 1)
                                {
                                    nodeAcesso.Checked = true;
                                }

                                break;

                            case "Cadastrar":

                                if (Convert.ToInt32(dtMenus.Rows[0]["INCLUSAO"]) == 1)
                                {
                                    nodeAcesso.Checked = true;
                                }

                                break;

                            case "Editar":

                                if (Convert.ToInt32(dtMenus.Rows[0]["EDICAO"]) == 1)
                                {
                                    nodeAcesso.Checked = true;
                                }

                                break;

                            case "Excluir":

                                if (Convert.ToInt32(dtMenus.Rows[0]["EXCLUSAO"]) == 1)
                                {
                                    nodeAcesso.Checked = true;
                                }

                                break;

                            default:
                                break;
                        }
                    }
                }
            }
        }

        #endregion

        #endregion
    }
}
