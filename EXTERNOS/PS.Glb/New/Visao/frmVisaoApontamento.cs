using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Card;
using PS.Glb.Class;

namespace PS.Glb.New.Visao
{
    public partial class frmVisaoApontamento : Form
    {
        #region Variáveis

        string Condicao = "";
        string sql = "";
        string tabela = "AAPONTAMENTO";
        int contador = 0;

        private Classes.Apontamento apontamento = new Classes.Apontamento();

        #endregion

        public frmVisaoApontamento(String _Condicao)
        {
            InitializeComponent();
            Condicao = _Condicao;
        }

        private void frmVisaoApontamento_Load(object sender, EventArgs e)
        {
            CarregaGrid(Condicao);
            getAcesso("btnApontamento");
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                PS.Glb.New.Cadastros.frmCadastroApontamento frm = new Cadastros.frmCadastroApontamento();
                frm.idApontamento = null;
                frm.ShowDialog();

                CarregaGrid(Condicao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount == 1)
            {
                contador = 1;
                lbResultRegistros.Text = contador.ToString();
                lbTotalResult.Text = new Class.Utilidades().CalculaTotalHoras(gridView1, "AAPONTAMENTO");
                contador = 0;
            }
            else
            {
                for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                {
                    contador = gridView1.SelectedRowsCount;

                    lbResultRegistros.Text = contador.ToString();
                    lbTotalResult.Text = new Class.Utilidades().CalculaTotalHoras(gridView1, "AAPONTAMENTO");
                }
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                PS.Glb.New.Cadastros.frmCadastroApontamento frm = new Cadastros.frmCadastroApontamento();
                frm.idApontamento = row1["AAPONTAMENTO.IDAPONTAMENTO"].ToString();
                frm.StatusApontamento = row1["AAPONTAMENTO.IDSTATUSAPONTAMENTO"].ToString();
                frm.ShowDialog();
                AtualizarColuna(row1);
                //CarregaGrid(Condicao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                if (int.Parse(row1["AAPONTAMENTO.IDSTATUSAPONTAMENTO"].ToString()) != 0)
                {
                    XtraMessageBox.Show("Somente apontamentos com status '0 - Em Digitação' podem ser excluídos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (int.Parse(row1["AAPONTAMENTO.IDSTATUSAPONTAMENTO"].ToString()) > 2)
                {
                    DialogResult r = MessageBox.Show("O Apontamento selecionado já esta integrado. Deseja estorná-lo?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (r == DialogResult.Yes)
                    {
                        CancelaIntegracao(row1["AAPONTAMENTO.IDAPONTAMENTO"].ToString());

                        XtraMessageBox.Show("Apontamento excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    if (MessageBox.Show("Deseja exluir o registro selecionado?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        sql = String.Format(@"DELETE FROM AAPONTAMENTOTAREFA WHERE IDAPONTAMENTO = '{0}'", row1["AAPONTAMENTO.IDAPONTAMENTO"].ToString());
                        MetodosSQL.ExecQuery(sql);

                        sql = String.Format(@"DELETE FROM AAPONTAMENTO WHERE IDAPONTAMENTO = '{0}'", row1["AAPONTAMENTO.IDAPONTAMENTO"].ToString());
                        MetodosSQL.ExecQuery(sql);

                        XtraMessageBox.Show("Apontamento excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CarregaGrid(Condicao);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFiltros_Click(object sender, EventArgs e)
        {
            PS.Glb.New.Filtro.frmFiltroApontamento frm = new Filtro.frmFiltroApontamento();
            frm.ShowDialog();

            if (!String.IsNullOrWhiteSpace(frm.condicao))
            {
                Condicao = frm.condicao;

                CarregaGrid(Condicao);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregaGrid(Condicao);
        }

        private void btnSalvarLayout_Click(object sender, EventArgs e)
        {
            SalvarLayout();
        }

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();

            CarregaGrid(Condicao);
        }

        #region Processos

        // Enviar para Aprovação
        private void btnAprovar_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowCollection drc = GetDataRows(true);
                New.Classes.Apontamento apontamento = new Classes.Apontamento();

                foreach (DataRow row in drc)
                {
                    if (row["AAPONTAMENTO.IDSTATUSAPONTAMENTO"].ToString() == "1")
                    {
                        if (!splashScreenManager1.IsSplashFormVisible)
                        {
                            splashScreenManager1.ShowWaitForm();
                            splashScreenManager1.SetWaitFormCaption("Processando...");
                        }

                        try
                        {
                            apontamento.EnviarEmail(true, row);

                            Aprovar((String)row["AAPONTAMENTO.IDAPONTAMENTO"]);
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Somente Apontamentos com status '1- Concluído' podem ser aprovados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                splashScreenManager1.CloseWaitForm();
                XtraMessageBox.Show("Resumo do processo:\r\n" + apontamento.MensagemFinalEmail, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                splashScreenManager1.CloseWaitForm();
                XtraMessageBox.Show("Erro ao enviar e-mail de Ordem de Serviço.\r\nDetalhes:" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Integração de Faturamento
        private void btnIntegrarApontamento_Click(object sender, EventArgs e)
        {
            int ii = 0;
            List<string> listApontamentosIntegrados = new List<string>();

            for (ii = 0; ii < gridView1.SelectedRowsCount; ii++)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(ii).ToString()));

                if (row["AAPONTAMENTO.IDSTATUSAPONTAMENTO"].ToString() == "2")
                {
                    try
                    {
                        if (!splashScreenManager1.IsSplashFormVisible)
                        {
                            splashScreenManager1.ShowWaitForm();
                            splashScreenManager1.SetWaitFormCaption("Processando...");
                        }

                        apontamento.IntegrarApontamento(row["AAPONTAMENTO.IDAPONTAMENTO"].ToString(), row["AAPONTAMENTO.CODUSUARIO"].ToString());

                        listApontamentosIntegrados.Add(row["AAPONTAMENTO.IDAPONTAMENTO"].ToString());
                    }
                    catch (Exception ex)
                    {
                        splashScreenManager1.CloseWaitForm();
                        XtraMessageBox.Show("Erro ao Integrar o Apontamento " + row["AAPONTAMENTO.IDAPONTAMENTO"].ToString() + ".\r\nDetalhes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }
                }
                else
                {
                    MessageBox.Show(String.Format(@"O apontamento {0} não pode ser integrado, verifique se o mesmo foi aprovado", row["AAPONTAMENTO.IDAPONTAMENTO"].ToString()));
                }
            }

            if (listApontamentosIntegrados.Count > 0)
            {
                if (splashScreenManager1.IsSplashFormVisible)
                {
                    splashScreenManager1.CloseWaitForm();
                }

                XtraMessageBox.Show("Integração realizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregaGrid(Condicao);
                return;
            }

            if (splashScreenManager1.IsSplashFormVisible)
            {
                splashScreenManager1.CloseWaitForm();
            }

            MessageBox.Show("Integração realizada com sucesso!", "Concluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Solicitar Correção
        private void btnReabrirApontamento_Click(object sender, EventArgs e)
        {
            DataRowCollection drc = GetDataRows(true);
            New.Classes.Apontamento apontamento = new Classes.Apontamento();

            try
            {
                foreach (DataRow row in drc)
                {
                    if (row["AAPONTAMENTO.IDSTATUSAPONTAMENTO"].ToString() == "1") //1- concluido
                    {
                        apontamento.ReabrirApontamento(row["AAPONTAMENTO.IDAPONTAMENTO"].ToString());
                    }
                    else if (row["AAPONTAMENTO.IDSTATUSAPONTAMENTO"].ToString() == "4") //4- Reprovado
                    {
                        apontamento.ReabrirApontamento(row["AAPONTAMENTO.IDAPONTAMENTO"].ToString());
                    }
                    else if (row["AAPONTAMENTO.IDSTATUSAPONTAMENTO"].ToString() == "0") // 0- em digitação
                    {
                        XtraMessageBox.Show(String.Format("Apontamento {0} já se encontra em status Em Digitação.", row["AAPONTAMENTO.IDAPONTAMENTO"].ToString()), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (row["AAPONTAMENTO.IDSTATUSAPONTAMENTO"].ToString() == "2") //3 - aprovado
                    {
                        XtraMessageBox.Show(String.Format("Não é possivel reabrir o apontamento {0} pois já foi aprovado.", row["AAPONTAMENTO.IDAPONTAMENTO"].ToString()), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show(String.Format("Não é possivel reabrir o apontamento {0} pois já foi realiazado a integração do mesmo.", row["AAPONTAMENTO.IDAPONTAMENTO"].ToString()), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                CarregaGrid(Condicao);

                XtraMessageBox.Show("Apontamento(s) aberto(s) com sucesso!.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Erro ao solicitar correção do(s) apontamento(s) selecionado(s).\r\nDetalhes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // Reprovar Apontamento
        private void reprovarApontamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                if (row["AAPONTAMENTO.IDSTATUSAPONTAMENTO"].ToString() == "2")
                {
                    New.Processos.frmReprovarApontamento frmReprovarApontamento = new Processos.frmReprovarApontamento();
                    frmReprovarApontamento.idApontamento = Convert.ToInt32(row["AAPONTAMENTO.IDAPONTAMENTO"]);
                    frmReprovarApontamento.ShowDialog();

                    XtraMessageBox.Show("Processo executado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CarregaGrid(Condicao);
                }
            }
        }

        // Estornar Integração
        private void btnCancelaIntegração_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormCaption("Processando...");

                for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                {
                    DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                    if (row["AAPONTAMENTO.IDSTATUSAPONTAMENTO"].ToString() == "3")
                    {
                        try
                        {
                            apontamento.CancelarIntegracao(row["AAPONTAMENTO.IDAPONTAMENTO"].ToString());

                            if (i == gridView1.SelectedRowsCount - 1)
                            {
                                splashScreenManager1.CloseWaitForm();
                                XtraMessageBox.Show("Estorno de Integração realizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                CarregaGrid(Condicao);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            splashScreenManager1.CloseWaitForm();
                            XtraMessageBox.Show("Não foi possível Estornar a Integração do Apontamento " + row["AAPONTAMENTO.IDAPONTAMENTO"].ToString() + ".\r\nDetalhes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }
        }

        // Reenviar Aprovação
        private void btnReenviaEmail_Click(object sender, EventArgs e)
        {
            try
            {
                Processos.frmReenviarEmail frmReenviarEmail = new Processos.frmReenviarEmail();

                DataRowCollection drc = GetDataRows(true);

                string unidade = "";
                string projeto = "";

                foreach (DataRow row in drc)
                {
                    if (!string.IsNullOrEmpty(unidade))
                    {
                        if (unidade != row["AUNIDADE.NOME"].ToString())
                        {
                            XtraMessageBox.Show("Somente um cliente pode ser selecionado por vez.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        unidade = row["AUNIDADE.NOME"].ToString();
                    }

                    //if (!string.IsNullOrEmpty(projeto))
                    //{
                    //    if (projeto != row["APROJETO.DESCRICAO"].ToString())
                    //    {
                    //        XtraMessageBox.Show("Somente um projeto pode ser selecionado por vez.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        return;
                    //    }
                    //}
                    //else
                    //{
                    //    projeto = row["APROJETO.DESCRICAO"].ToString();
                    //}

                    if ((row["AAPONTAMENTO.IDSTATUSAPONTAMENTO"].ToString() != "2") && (row["AAPONTAMENTO.IDSTATUSAPONTAMENTO"].ToString() != "3"))
                    {
                        XtraMessageBox.Show("Somente apontamentos Aprovados e Integrados podem ter seu e-mail reenviado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        frmReenviarEmail.listIDApontamento.Add(Convert.ToInt32(row["AAPONTAMENTO.IDAPONTAMENTO"]));
                    }
                }

                frmReenviarEmail.ShowDialog();
                return;
            }
            catch (Exception ex)
            {
                splashScreenManager1.CloseWaitForm();
                XtraMessageBox.Show("Erro ao enviar e-mail de Ordem de Serviço.\r\nDetalhes:" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Anexos

        private void históricoDeReprovaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                xtpHistoricoReprovacao.PageVisible = true;
                splitContainer1.Panel2Collapsed = false;

                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                DataTable dtReprovacao = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODUSUARIO AS 'Usuário', DATA AS 'Data Processo', MOTIVO AS 'Motivo'
                                                                                                FROM AAPONTAMENTOREPROVACAO
                                                                                                WHERE CODEMPRESA = ? AND IDAPONTAMENTO = ?", new object[] { AppLib.Context.Empresa, Convert.ToInt32(row["AAPONTAMENTO.IDAPONTAMENTO"]) });
                gridControl2.DataSource = dtReprovacao;
            }
        }

        private void fecharAnexosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Histórico de Reprovação
            xtpHistoricoReprovacao.PageVisible = false;
            splitContainer1.Panel2Collapsed = true;
            gridControl2.DataSource = null;
        }

        #endregion

        #region Métodos

        private void getAcesso(string CodMenu)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT EDICAO, EXCLUSAO, CONSULTA, INCLUSAO FROM GPERMISSAOMENU WHERE CODMENU = ? AND CODPERFIL = ?", new object[] { "btnApontamento", AppLib.Context.Perfil });
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dt.Rows[i]["EDICAO"]) == false)
                    {
                        btnEditar.Enabled = false;
                    }
                    if (Convert.ToBoolean(dt.Rows[i]["CONSULTA"]) == true)
                    {
                        btnEditar.Enabled = true;
                    }
                    if (Convert.ToBoolean(dt.Rows[i]["EXCLUSAO"]) == false)
                    {
                        btnExcluir.Enabled = false;
                    }
                    if (Convert.ToBoolean(dt.Rows[i]["INCLUSAO"]) == false)
                    {
                        btnNovo.Enabled = false;
                    }
                }
            }
            else
            {
                btnEditar.Enabled = false;
                btnExcluir.Enabled = false;
                btnNovo.Enabled = false;
            }
        }

        private void CarregaGrid(string condicao)
        {
            try
            {
                DataSet dsApontamento = new DataSet();

                DataTable dtApontamento = new DataTable();
                DataTable dtTarefas = new DataTable();

                string sql = string.Empty;
                List<string> tabelasFilhas = new List<string>();

                tabelasFilhas.Add("AUNIDADE");

                string relacionamento = @"INNER JOIN AUNIDADE 
                                          ON AUNIDADE.IDUNIDADE = AAPONTAMENTO.IDUNIDADE AND AUNIDADE.CODEMPRESA = AAPONTAMENTO.CODEMPRESA

                                          INNER JOIN APROJETO
                                          ON APROJETO.IDPROJETO = AAPONTAMENTO.IDPROJETO AND APROJETO.CODEMPRESA = AAPONTAMENTO.CODEMPRESA AND APROJETO.CODFILIAL = AAPONTAMENTO.CODFILIAL";

                if (Condicao.Contains("AP."))
                {
                    if (Condicao.Contains("BETWEEN 1 AND 15"))
                    {
                        // Primeira Quinzena
                        Condicao = Condicao.Insert(97, @" AND (AAPONTAMENTO.CODUSUARIO = '" + AppLib.Context.Usuario + "' OR APROJETO.CODUSUARIOCLIENTE = '" + AppLib.Context.Usuario + "' OR APROJETO.CODUSUARIOPRESTADOR = '" + AppLib.Context.Usuario + "' OR (SELECT GUSUARIO.SUPERVISOR FROM GUSUARIO WHERE CODUSUARIO = '" + AppLib.Context.Usuario + "') = 1)");
                    }
                    else if (Condicao.Contains("BETWEEN 16"))
                    {
                        // Segunda Quinzena
                        Condicao = Condicao.Insert(156, @" AND (AAPONTAMENTO.CODUSUARIO = '" + AppLib.Context.Usuario + "' OR APROJETO.CODUSUARIOCLIENTE = '" + AppLib.Context.Usuario + "' OR APROJETO.CODUSUARIOPRESTADOR = '" + AppLib.Context.Usuario + "' OR (SELECT GUSUARIO.SUPERVISOR FROM GUSUARIO WHERE CODUSUARIO = '" + AppLib.Context.Usuario + "') = 1)");
                    }
                    else
                    {
                        // Outro filtro
                        Condicao = Condicao + @" AND (AAPONTAMENTO.CODUSUARIO = '" + AppLib.Context.Usuario + "' OR APROJETO.CODUSUARIOCLIENTE = '" + AppLib.Context.Usuario + "' OR APROJETO.CODUSUARIOPRESTADOR = '" + AppLib.Context.Usuario + "' OR (SELECT GUSUARIO.SUPERVISOR FROM GUSUARIO WHERE CODUSUARIO = '" + AppLib.Context.Usuario + "') = 1)";
                    }

                    Condicao = Condicao.Replace("AP.", "AAPONTAMENTO.");
                }

                sql = new Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, Condicao);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                sql = sql.Replace("AAPONTAMENTO.TOTALHORAS AS 'AAPONTAMENTO.TOTALHORAS'", "CAST(AAPONTAMENTO.TOTALHORAS AS TIME) AS 'AAPONTAMENTO.TOTALHORAS'");

                dtApontamento = new DataTable();

                dtApontamento = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);
                dtApontamento.TableName = "Apontamento";

                if (!string.IsNullOrEmpty(Condicao))
                {
                    if (condicao.Contains("AP."))
                    {
                        Condicao = Condicao.Replace("AP.", "AAPONTAMENTO.");

                        if (condicao.Contains("GROUP BY"))
                        {
                            int indexGroupBy = Condicao.IndexOf("GROUP BY");
                            int ultimoIndex = Condicao.LastIndexOf(", AUNIDADE.NOME");

                            int tamanhoCondicao = ultimoIndex - indexGroupBy;

                            Condicao = Condicao.Remove(indexGroupBy, tamanhoCondicao);

                            Condicao = Condicao.Replace(@" , AUNIDADE.NOME", "");
                        }
                    }

                    dtTarefas = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT AAPONTAMENTO.IDAPONTAMENTO AS 'AAPONTAMENTO.IDAPONTAMENTO', IDAPONTAMENTOTAREFA, IDTAREFA, HORAS, OBSERVACAO
                                                                                        FROM AAPONTAMENTOTAREFA
                                                                                        INNER JOIN AAPONTAMENTO 
                                                                                        ON AAPONTAMENTO.CODEMPRESA = AAPONTAMENTOTAREFA.CODEMPRESA AND AAPONTAMENTO.IDAPONTAMENTO = AAPONTAMENTOTAREFA.IDAPONTAMENTO 

                                                                                        INNER JOIN APROJETO ON APROJETO.IDPROJETO = AAPONTAMENTO.IDPROJETO AND APROJETO.CODEMPRESA = AAPONTAMENTO.CODEMPRESA AND APROJETO.CODFILIAL = AAPONTAMENTO.CODFILIAL " + Condicao + " GROUP BY AAPONTAMENTO.IDAPONTAMENTO, IDAPONTAMENTOTAREFA, IDTAREFA, HORAS, OBSERVACAO ORDER BY AAPONTAMENTOTAREFA.IDAPONTAMENTOTAREFA ASC");
                }
                else
                {

                    dtTarefas = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT AAPONTAMENTO.IDAPONTAMENTO AS 'AAPONTAMENTO.IDAPONTAMENTO', IDAPONTAMENTOTAREFA, IDTAREFA, HORAS, OBSERVACAO
                                                                                        FROM AAPONTAMENTOTAREFA
                                                                                        INNER JOIN AAPONTAMENTO 
                                                                                        ON AAPONTAMENTO.CODEMPRESA = AAPONTAMENTOTAREFA.CODEMPRESA AND AAPONTAMENTO.IDAPONTAMENTO = AAPONTAMENTOTAREFA.IDAPONTAMENTO 

                                                                                        INNER JOIN APROJETO ON APROJETO.IDPROJETO = AAPONTAMENTO.IDPROJETO AND APROJETO.CODEMPRESA = AAPONTAMENTO.CODEMPRESA AND APROJETO.CODFILIAL = AAPONTAMENTO.CODFILIAL GROUP BY AAPONTAMENTO.IDAPONTAMENTO, IDAPONTAMENTOTAREFA, IDTAREFA, HORAS, OBSERVACAO ORDER BY AAPONTAMENTOTAREFA.IDAPONTAMENTOTAREFA ASC");
                }

                dtTarefas.TableName = "Tarefa";

                dsApontamento.Tables.Add(dtApontamento);
                dsApontamento.Tables.Add(dtTarefas);

                DataColumn chaveColuna = dsApontamento.Tables["Apontamento"].Columns["AAPONTAMENTO.IDAPONTAMENTO"];
                DataColumn chaveColunaFilha = dsApontamento.Tables["Tarefa"].Columns["AAPONTAMENTO.IDAPONTAMENTO"];

                dsApontamento.Relations.Add("Tarefas do Apontamento", chaveColuna, chaveColunaFilha);

                gridControl1.DataSource = dsApontamento.Tables["Apontamento"];
                gridControl1.ForceInitialize();

                CardView cardView1 = new CardView(gridControl1);

                gridControl1.LevelTree.Nodes.Add("Tarefas do Apontamento", cardView1);
                cardView1.ViewCaption = "Tarefas";
                cardView1.CardCaptionFormat = "Tarefa Nº {0}";
                cardView1.OptionsView.ShowQuickCustomizeButton = false;
                cardView1.OptionsBehavior.FieldAutoHeight = true;
                cardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
                cardView1.PopulateColumns(dsApontamento.Tables["Tarefa"]);

                cardView1.Columns["AAPONTAMENTO.IDAPONTAMENTO"].VisibleIndex = -1;
                cardView1.Columns["IDAPONTAMENTOTAREFA"].VisibleIndex = -1;
                cardView1.Columns["IDTAREFA"].Caption = "ID Tarefa";
                cardView1.Columns["HORAS"].Caption = "Horas";
                cardView1.Columns["OBSERVACAO"].Caption = "Observação";

                cardView1.Columns["HORAS"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                cardView1.Columns["HORAS"].DisplayFormat.FormatString = "HH:mm";

                RepositoryItemMemoEdit memoEdit = new RepositoryItemMemoEdit();
                gridControl1.RepositoryItems.Add(memoEdit);
                cardView1.Columns["OBSERVACAO"].ColumnEdit = memoEdit;

                cardView1.OptionsBehavior.AutoHorzWidth = true;
                if (gridView1.Columns["AAPONTAMENTO.IDSTATUSAPONTAMENTO"] != null)
                {
                    CarregaImagemStatus();
                }

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridView1.Columns.Count; i++)
                {
                    if (dt.Rows.Count <= i)
                    {
                        return;
                    }

                    gridView1.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridView1.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridView1.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregaImagemStatus()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();

            DataTable dtImagem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IMAGEM, DESCRICAO, CODSTATUS FROM GSTATUS WHERE TABELA = '" + tabela + "'");

            for (int i = 0; i < dtImagem.Rows.Count; i++)
            {
                byte[] imagemEmBytes = (byte[])dtImagem.Rows[i]["IMAGEM"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagemEmBytes);
                images.AddImage(Image.FromStream(ms));
                imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[i]["DESCRICAO"].ToString(), Convert.ToInt32(dtImagem.Rows[i]["CODSTATUS"]), i));
            }

            imageCombo.SmallImages = images;
            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns["AAPONTAMENTO.IDSTATUSAPONTAMENTO"].ColumnEdit = imageCombo;
        }

        private void AtualizarColuna(DataRow dr)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM " + tabela + " WHERE IDAPONTAMENTO = ?", new object[] { dr["AAPONTAMENTO.IDAPONTAMENTO"] });

            for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
            {
                if (gridView1.Columns[i].ReadOnly == false)
                {
                    try
                    {
                        dr[gridView1.Columns[i].FieldName] = dr[gridView1.Columns[i].FieldName] = dt.Rows[0][gridView1.Columns[i].FieldName.ToString().Remove(0, gridView1.Columns[i].FieldName.ToString().IndexOf(".") + 1)].ToString();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void IntegraApontamento(String IDAPONTAMENTO, string usuario)
        {
            Classes.Apontamento apontamento = new Classes.Apontamento();

            try
            {
                apontamento.IntegrarApontamento(IDAPONTAMENTO, usuario);

                CarregaGrid(Condicao);
            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible)
                {
                    splashScreenManager1.CloseWaitForm();
                }

                XtraMessageBox.Show(ex.Message);
            }
        }

        private void CancelaIntegracao(String IDAPONTAMENTO)
        {
            New.Classes.Apontamento apontamento = new Classes.Apontamento();

            try
            {
                apontamento.CancelarIntegracao(IDAPONTAMENTO);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            CarregaGrid(Condicao);
        }

        private void Aprovar(String ID)
        {
            try
            {
                sql = String.Format(@"update AAPONTAMENTO 
                                        set IDSTATUSAPONTAMENTO = '2'
                                        where IDAPONTAMENTO = '{0}'", ID);
                MetodosSQL.ExecQuery(sql);

                CarregaGrid(Condicao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataRowCollection GetDataRows(Boolean ValidarSelecao)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataTable dt = new DataTable();
                int[] handles = gridView1.GetSelectedRows();

                for (int i = 0; i < handles.Length; i++)
                {
                    if (i == 0)
                    {
                        for (int col = 0; col < gridView1.GetDataRow(handles[i]).Table.Columns.Count; col++)
                        {
                            dt.Columns.Add(gridView1.GetDataRow(handles[i]).Table.Columns[col].ColumnName);
                        }
                    }

                    if (handles[i] >= 0)
                    {
                        dt.Rows.Add(gridView1.GetDataRow(handles[i]).ItemArray);
                    }
                }

                return dt.Rows;
            }
            else
            {
                if (ValidarSelecao)
                {
                    MessageBox.Show("Selecione o(s) registro(s).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return null;
            }
        }

        private void SalvarLayout()
        {
            try
            {
                AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GVISAOUSUARIO SET VISIVEL = 0 WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, tabela });

                for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
                {
                    AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                    GVISAOUSUARIO.Set("VISAO", tabela);
                    GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
                    GVISAOUSUARIO.Set("COLUNA", gridView1.VisibleColumns[i].FieldName);
                    GVISAOUSUARIO.Set("SEQUENCIA", i);
                    GVISAOUSUARIO.Set("LARGURA", gridView1.VisibleColumns[i].Width);
                    GVISAOUSUARIO.Set("VISIVEL", 1);
                    GVISAOUSUARIO.Save();
                }

                DataTable dtFixo = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ? AND FIXO = 1", new object[] { AppLib.Context.Usuario, tabela });
                if (dtFixo.Rows.Count > 0)
                {
                    for (int i = 0; i < dtFixo.Rows.Count; i++)
                    {
                        AppLib.ORM.Jit FIXO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                        FIXO.Set("VISAO", tabela);
                        FIXO.Set("CODUSUARIO", AppLib.Context.Usuario);
                        FIXO.Set("COLUNA", dtFixo.Rows[i]["COLUNA"]);
                        FIXO.Set("LARGURA", 100);
                        FIXO.Set("VISIVEL", 1);
                        FIXO.Save();
                    }
                }

                CarregaGrid(Condicao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
