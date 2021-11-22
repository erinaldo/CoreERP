using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PS.Glb.Class;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroProjeto : Form
    {
        string sql = string.Empty;
        string sqlParalelo = string.Empty;
        string sqlEmail = string.Empty;
        public string IDPROJETO { get; set; }
        public string CODEMPRESA { get; set; }

        string tabela = "APROJETOTAREFA";
        public frmCadastroProjeto()
        {
            InitializeComponent();
        }
        private void frmCadastroProjeto_Load(object sender, EventArgs e)
        {
            CarregaComboBoxNivel();
            CarregaComboBoxTipo();
            AtualizaVisao();

            dteDataCriacao.EditValue = DateTime.Now.ToString("yyy-MM-dd");
            dteDataCriacao.Properties.DisplayFormat.FormatString = "dd-MM-yyy";
        }
        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            Salvar();
            this.Dispose();
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void campoLookupIDCLIENTE_AposSelecao(object sender, EventArgs e)
        {
            try
            {
                SetConsultaLookUp();

                sql = String.Format(@"select VALORHORAD from AUNIDADE where IDUNIDADE = '{0}'", campoLookupIDCLIENTE.textBoxCODIGO.Text);

                txtValorHora.Text = MetodosSQL.GetField(sql, "VALORHORAD");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            // Salvar projeto para habilitar as Tarefas do Projeto
            if (tabControl1.SelectedTab.Text == "Tarefas")
            {
                Salvar();
            }
        }

        #region Tarefas do Projeto

        private void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                PS.Glb.New.Cadastros.frmCadastroProjetoTarefa frm = new Cadastros.frmCadastroProjetoTarefa();
                frm.IDPROJETO = IDPROJETO;
                frm.IDUNIDADE = Convert.ToInt32(campoLookupIDCLIENTE.Get());
                frm.CODEMPRESA = AppLib.Context.Empresa.ToString();
                frm.CODFILIAL = AppLib.Context.Filial.ToString();
                frm.IDTAREFA = null;
                frm.ShowDialog();
                AtualizaVisao();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            AtualizaVisao();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Deseja excluir o registro selecionado?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    sql = String.Format(@"delete from APROJETOTAREFA where IDTAREFA = '{0}'", row1["IDTAREFA"].ToString());
                    MetodosSQL.ExecQuery(sql);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            AtualizaVisao();
        }
        private void btnSalvarLayout_Click(object sender, EventArgs e)
        {
            SalvarLayout();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                PS.Glb.New.Cadastros.frmCadastroProjetoTarefa frm = new Cadastros.frmCadastroProjetoTarefa();

                frm.IDPROJETO = IDPROJETO;
                frm.IDUNIDADE = Convert.ToInt32(campoLookupIDCLIENTE.Get());
                frm.CODEMPRESA = row1["CODEMPRESA"].ToString();
                frm.CODFILIAL = row1["CODFILIAL"].ToString();
                frm.IDTAREFA = row1["IDTAREFA"].ToString();
                frm.ShowDialog();
                AtualizaVisao();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Métodos
        private void AtualizaVisao()
        {
            try
            {
                cbNivel.SelectedValue = 1;
                clFilial.ColunaTabela = String.Format("(select * from GFILIAL where CODEMPRESA = '{0}') I", AppLib.Context.Empresa);

                if (String.IsNullOrWhiteSpace(IDPROJETO))
                {
                    sql = String.Format(@"select NOME from GFILIAL where CODEMPRESA = '{0}' and CODFILIAL = '{1}'", AppLib.Context.Empresa, AppLib.Context.Filial);
                    clFilial.textBoxCODIGO.Text = AppLib.Context.Filial.ToString();
                    clFilial.textBoxDESCRICAO.Text = MetodosSQL.GetField(sql, "NOME");
                    cbTipo.SelectedValue = "A";
                }
                else
                {
                    sql = String.Format(@"SELECT AP.DATACRIACAO, 
	                                             AP.DATACONCLUSAO,
	                                             AU.CODCLIFOR,
                                                 AU.IDUNIDADE,
	                                             AU.NOME, 
	                                             AP.NIVEL,
	                                             AP.DESCRICAO,
	                                             AP.CODUSUARIOPRESTADOR,
	                                             AP.CODUSUARIOCLIENTE,
	                                             AP.CODCCUSTO,
	                                             AP.CODNATUREZA,
	                                             AP.VALORHORA,
                                                 AP.CODFILIAL,
                                                 AP.CODPRODUTO,   
                                                 AP.TIPO
                                          FROM APROJETO AP

                                  inner join AUNIDADE AU
                                  on AU.IDUNIDADE = AP.IDUNIDADE
                                  and AU.CODEMPRESA = AP.CODEMPRESA

                                  where AP.IDPROJETO = '{0}'
                                  and AP.CODEMPRESA = '{1}'", IDPROJETO, CODEMPRESA);

                    tbIdProjeto.EditValue = int.Parse(IDPROJETO);

                    if (!String.IsNullOrWhiteSpace(MetodosSQL.GetField(sql, "DATACRIACAO")))

                        dteDataCriacao.DateTime = Convert.ToDateTime(MetodosSQL.GetField(sql, "DATACRIACAO"));

                    if (!String.IsNullOrWhiteSpace(MetodosSQL.GetField(sql, "DATACONCLUSAO")))
                        dteDataConclusao.DateTime = Convert.ToDateTime(MetodosSQL.GetField(sql, "DATACONCLUSAO"));

                    campoLookupIDCLIENTE.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "IDUNIDADE");
                    campoLookupIDCLIENTE.textBoxDESCRICAO.Text = MetodosSQL.GetField(sql, "NOME");

                    SetConsultaLookUp();

                    cbTipo.SelectedValue = MetodosSQL.GetField(sql, "TIPO");
                    clDemanda.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "CODPRODUTO");
                    clDemanda.textBox1_Leave(null, null);

                    campoLookupCOORDENADOR.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "CODUSUARIOPRESTADOR");
                    sqlParalelo = String.Format(@"select CODUSUARIO, NOME from GUSUARIO where CODUSUARIO = '{0}'", campoLookupCOORDENADOR.textBoxCODIGO.Text);
                    campoLookupCOORDENADOR.textBoxDESCRICAO.Text = MetodosSQL.GetField(sqlParalelo, "NOME");

                    campoLookupIDCONTATO.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "CODUSUARIOCLIENTE");
                    sqlParalelo = String.Format(@"select CODUSUARIO, NOME from GUSUARIO where CODUSUARIO = '{0}'", campoLookupIDCONTATO.textBoxCODIGO.Text);
                    campoLookupIDCONTATO.textBoxDESCRICAO.Text = MetodosSQL.GetField(sqlParalelo, "NOME");

                    cbNivel.SelectedValue = Convert.ToInt32(MetodosSQL.GetField(sql, "NIVEL"));

                    tbDescricao.Text = MetodosSQL.GetField(sql, "DESCRICAO");

                    clCodCusto.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "CODCCUSTO");
                    sqlParalelo = String.Format(@"select CODCCUSTO, NOME from GCENTROCUSTO where CODCCUSTO = '{0}'", clCodCusto.textBoxCODIGO.Text);
                    clCodCusto.textBoxDESCRICAO.Text = MetodosSQL.GetField(sqlParalelo, "NOME");

                    clNaturezaOrcamentaria.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "CODNATUREZA");
                    sqlParalelo = String.Format(@"select CODNATUREZA, DESCRICAO from VNATUREZAORCAMENTO where CODNATUREZA = '{0}'", clNaturezaOrcamentaria.textBoxCODIGO.Text);
                    clNaturezaOrcamentaria.textBoxDESCRICAO.Text = MetodosSQL.GetField(sqlParalelo, "DESCRICAO");
                                
                    clFilial.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "CODFILIAL");
                    sqlParalelo = String.Format(@"select NOME from GFILIAL where CODEMPRESA = '{0}' and CODFILIAL = '{1}'", AppLib.Context.Empresa, MetodosSQL.GetField(sql, "CODFILIAL"));
                    clFilial.textBoxDESCRICAO.Text = MetodosSQL.GetField(sqlParalelo, "NOME");

                    txtValorHora.Text = MetodosSQL.GetField(sql, "VALORHORA");

                    sqlEmail = string.Format(@"SELECT EMAIL FROM APROJETOEMAIL WHERE CODCOLIGADA = {0} AND CODFILIAL = {1} AND IDPROJETO = {2}", AppLib.Context.Empresa, clFilial.Get(), IDPROJETO);
                    tbEmailsAdicionais.Text = MetodosSQL.GetField(sqlEmail, "EMAIL");

                    sql = String.Format(@"select CODEMPRESA, CODFILIAL, IDTAREFA, NOMETAREFA, PREVISAOENTREGA, PRIORIDADE, INLOCO, DATACONCLUSAO, TIPOFATURAMENTO, PREVISAOHORAS from APROJETOTAREFA where IDPROJETO = '{0}'", IDPROJETO);

                    gridControl1.DataSource = MetodosSQL.GetDT(sql);

                    if (!String.IsNullOrWhiteSpace(IDPROJETO)) { toolStrip1.Enabled = true; gridControl1.Enabled = true; }

                    SetConsultaLookUp();

                    try
                    {
                        sql = String.Format(@"SELECT COUNT(1) as 'TOTAL' FROM GVISAOUSUARIO WHERE VISAO = '{0}' AND CODUSUARIO = '{1}' AND VISIVEL = 1", tabela, AppLib.Context.Usuario);
                        if (MetodosSQL.GetField(sql, "TOTAL") != "0")
                        {
                            DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
                            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                            for (int i = 0; i < gridView1.Columns.Count; i++)
                            {
                                gridView1.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                                dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                                DataRow result = dic.Rows.Find(new object[] { gridView1.Columns[i].FieldName.ToString() });
                                if (result != null)
                                {
                                    gridView1.Columns[i].Caption = result["DESCRICAO"].ToString();
                                }
                            }
                        }
                        else
                        {
                            SalvarLayout();
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                    AtualizaVisao();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Boolean ValidarSalvar()
        {
            try
            {
                if (String.IsNullOrWhiteSpace(campoLookupIDCONTATO.textBoxCODIGO.Text))
                {
                    throw new Exception("Preencher o campo Coordenador Cliente.");
                }

                if (String.IsNullOrWhiteSpace(txtValorHora.Text))
                {
                    throw new Exception("Preencher o campo Valor Hora.");
                }

                if (cbTipo.SelectedValue.ToString() == "A" && clDemanda.Get() == null)
                {
                    throw new Exception("Preencher o campo Produto/Demanda.");
                }

                if (cbTipo.SelectedValue.ToString() == "F" && clDemanda.Get() == null)
                {
                    throw new Exception("Preencher o campo Produto/Projeto.");
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private void Salvar()
        {
            try
            {
                if (ValidarSalvar())
                {
                    if (String.IsNullOrWhiteSpace(IDPROJETO))
                    {
                        sql = String.Format(@"insert into APROJETO (CODEMPRESA, CODFILIAL, IDUNIDADE, DESCRICAO, ESCOPO, DATACRIACAO, CODUSUARIOPRESTADOR, CODUSUARIOCLIENTE, CODPRODUTO, CODCCUSTO, CODNATUREZA, VALORHORA, NIVEL, DATACONCLUSAO, STATUS, TIPO) values ('{0}', '{1}', '{2}', '{3}', '{4}', GETDATE(), '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', null, '{12}', '{13}') select SCOPE_IDENTITY()",
                                              /*CODEMPRESA*/ CODEMPRESA,
                                              /*CODFILIAL*/ clFilial.textBoxCODIGO.Text,
                                              /*IDUNIDADE*/ campoLookupIDCLIENTE.textBoxCODIGO.Text,
                                              /*DESCRICAO*/ tbDescricao.Text,
                                              /*ESCOPO*/ "ESCOPO",
                                              /*CODUSUARIOPRESTADOR*/ campoLookupCOORDENADOR.textBoxCODIGO.Text,
                                              /*CODUSUARIOCLIENTE*/ campoLookupIDCONTATO.textBoxCODIGO.Text,
                                              /*CODPRODUTO*/ clDemanda.textBoxCODIGO.Text,
                                              /*CODCCUSTO*/ clCodCusto.textBoxCODIGO.Text,
                                              /*CODNATUREZA*/ clNaturezaOrcamentaria.textBoxCODIGO.Text,
                                              /*VALORHORA*/ txtValorHora.Text.Replace(",", "."),
                                              /*NIVEL*/ Convert.ToInt32(cbNivel.SelectedValue),
                                              /*STATUS*/ "ABERTO",
                                              /*TIPO*/ cbTipo.SelectedValue.ToString());

                        IDPROJETO = MetodosSQL.ExecScalar(sql).ToString();
                    }
                    else
                    {
                        sql = String.Format(@"update APROJETO
                                          set NIVEL = '{0}',
                                          	  IDUNIDADE = '{1}',
	                                          DESCRICAO = '{2}',
	                                          CODUSUARIOPRESTADOR = '{3}',
                                          	  CODUSUARIOCLIENTE = '{4}',
                                          	  CODCCUSTO = '{5}',
                                          	  CODNATUREZA ='{6}',
	                                          VALORHORA = '{7}',
                                              CODFILIAL = '{8}',
                                              CODPRODUTO = '{9}',
                                              TIPO = '{10}'
                                          where IDPROJETO = '{11}'",
                                              /*{0}*/ Convert.ToInt32(cbNivel.SelectedValue),
                                              /*{1}*/ campoLookupIDCLIENTE.textBoxCODIGO.Text,
                                              /*{2}*/ tbDescricao.Text,
                                              /*{3}*/ campoLookupCOORDENADOR.textBoxCODIGO.Text,
                                              /*{4}*/ campoLookupIDCONTATO.textBoxCODIGO.Text,
                                              /*{5}*/ clCodCusto.textBoxCODIGO.Text,
                                              /*{6}*/ clNaturezaOrcamentaria.textBoxCODIGO.Text,
                                              /*{7}*/ txtValorHora.Text.Replace(",", "."),
                                              /*{8}*/ clFilial.textBoxCODIGO.Text,
                                              /*{9}*/ clDemanda.textBoxCODIGO.Text,
                                              /*{10}*/ cbTipo.SelectedValue.ToString(),
                                              /*{11}*/ IDPROJETO);

                        MetodosSQL.ExecQuery(sql);
                    }

                    SalvarEmailsAdicionais();

                    AtualizaVisao();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SalvarEmailsAdicionais()
        {
            sql = "";

            try
            {
                if (Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, @"SELECT COUNT(IDPROJETO) FROM APROJETOEMAIL WHERE CODCOLIGADA = ? AND CODFILIAL = ? AND IDPROJETO = ?", new object[] { AppLib.Context.Empresa, clFilial.Get(), IDPROJETO })) == false)
                {
                    sql = "INSERT INTO APROJETOEMAIL VALUES (?,?,?,?)";

                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(sql, new object[] { AppLib.Context.Empresa, clFilial.Get(), IDPROJETO, tbEmailsAdicionais.Text });
                }
                else
                {
                    sql = "UPDATE APROJETOEMAIL SET EMAIL = ? WHERE CODCOLIGADA = ? AND CODFILIAL = ? AND IDPROJETO = ?";

                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(sql, new object[] { tbEmailsAdicionais.Text, AppLib.Context.Empresa, clFilial.Get(), IDPROJETO });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetConsultaLookUp()
        {
            sqlParalelo = String.Format(@"select IDUNIDADE from AUNIDADE where CODCLIFOR = '{0}'", campoLookupIDCLIENTE.textBoxCODIGO.Text);

            campoLookupCOORDENADOR.ColunaTabela = String.Format(@"(SELECT AUR.CODUSUARIO, GU.NOME FROM AUNIDADEREEMBOLSO AUR

                                                                  inner join GUSUARIO GU
                                                                  on GU.CODUSUARIO = AUR.CODUSUARIO

                                                                  where AUR.IDUNIDADE = '{0}') Y", campoLookupIDCLIENTE.textBoxCODIGO.Text);

            campoLookupIDCONTATO.ColunaTabela = String.Format(@"(SELECT AUR.CODUSUARIO, GU.NOME FROM AUNIDADEREEMBOLSO AUR

                                                                  inner join GUSUARIO GU
                                                                  on GU.CODUSUARIO = AUR.CODUSUARIO

                                                                  where AUR.IDUNIDADE = '{0}'
                                                                  and AUR.COORDCLIENTE = '1') Y", campoLookupIDCLIENTE.textBoxCODIGO.Text);

            campoLookupCOORDENADOR.Enabled = true;
            campoLookupIDCONTATO.Enabled = true;
        }

        private void CarregaComboBoxNivel()
        {
            DataTable dtNivel = new DataTable();

            dtNivel.Columns.Add("Codigo", typeof(string));
            dtNivel.Columns.Add("Nome", typeof(string));

            dtNivel.Rows.Add(1, "Baixo");
            dtNivel.Rows.Add(2, "Médio");
            dtNivel.Rows.Add(3, "Alto");

            cbNivel.DataSource = dtNivel;
            cbNivel.ValueMember = "Codigo";
            cbNivel.DisplayMember = "Nome";
        }

        private void CarregaComboBoxTipo()
        {
            DataTable dtTipo = new DataTable();

            dtTipo.Columns.Add("Codigo", typeof(string));
            dtTipo.Columns.Add("Nome", typeof(string));

            dtTipo.Rows.Add("A", "Aberto");
            dtTipo.Rows.Add("F", "Fechado");

            cbTipo.DataSource = dtTipo;
            cbTipo.ValueMember = "Codigo";
            cbTipo.DisplayMember = "Nome";
        }

        #endregion
    }
}
