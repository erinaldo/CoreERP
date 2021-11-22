using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PS.Lib;
using PS.Glb.New.Cadastros;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace PS.Glb.New.Visao
{
    public partial class frmVisaoLancamento : Form
    {
        string tabela = "FLANCA";
        string query = string.Empty;
        string Codlanca = string.Empty;
        int Contador = 0;

        public frmVisaoLancamento() { }

        public frmVisaoLancamento(string _query, Form frmprin)
        {
            InitializeComponent();
            this.MdiParent = (Form)frmprin;
            query = _query;
            carregaGrid(query);
        }

        public void carregaGrid(string where)
        {
            string relacionamento = @" INNER JOIN VCLIFOR ON FLANCA.CODCLIFOR = VCLIFOR.CODCLIFOR AND FLANCA.CODEMPRESA = VCLIFOR.CODEMPRESA";
            List<string> tabelasFilhas = new List<string>();
            tabelasFilhas.Add("VCLIFOR");

            try
            {
                string sql = new Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, where);
                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                sql = sql.Replace("FLANCA.CODSTATUS AS 'FLANCA.CODSTATUS'", @"CASE
 WHEN ((CONVERT(DATE,FLANCA.DATAVENCIMENTO) < CONVERT(DATE, GETDATE())) AND FLANCA.CODSTATUS = 0) THEN '3' 
 WHEN ((CONVERT(DATE,FLANCA.DATAVENCIMENTO) = CONVERT(DATE, GETDATE())) AND FLANCA.CODSTATUS = 0)  THEN '4'  
 ELSE FLANCA.CODSTATUS 
 END AS 'FLANCA.CODSTATUS'");

                DataTable dtGrid = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);
                gridControl1.DataSource = dtGrid;

                carregaImagemPagRec();

                if (gridView1.Columns["FLANCA.CODSTATUS"] != null)
                {
                    carregaImagemStatus();
                }

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridView1.Columns.Count; i++)
                {
                    gridView1.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridView1.Columns[i].FieldName.ToString() });

                    if (result != null)
                    {
                        gridView1.Columns[i].Caption = result["DESCRICAO"].ToString();
                        if (gridView1.Columns[i].Caption.Contains("Valor"))
                        {
                            gridView1.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                            gridView1.Columns[i].DisplayFormat.FormatString = "n2";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void carregaImagemStatus()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();
            DataTable dtImagem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IMAGEM, DESCRICAO, CODSTATUS FROM GSTATUS WHERE TABELA = 'FLANCA'");
            for (int i = 0; i < dtImagem.Rows.Count; i++)
            {
                byte[] imagemEmBytes = (byte[])dtImagem.Rows[i]["IMAGEM"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagemEmBytes);
                images.AddImage(Image.FromStream(ms));
                imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[i]["DESCRICAO"].ToString(), dtImagem.Rows[i]["CODSTATUS"].ToString(), i));
            }
            imageCombo.SmallImages = images;
            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns["FLANCA.CODSTATUS"].ColumnEdit = imageCombo;
        }

        private void carregaImagemPagRec()
        {
            //DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            //DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();

            //images.AddImage(Image.FromFile(@"Icones\Financeiro\Seta-verde.png"));
            //images.AddImage(Image.FromFile(@"Icones\Financeiro\Seta-vermelha.png"));

            //imageCombo.SmallImages = images;
            //imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Pagar", 0, 1));
            //imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Receber", 1, 0));
            //imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            //gridView1.Columns["FLANCA.TIPOPAGREC"].ColumnEdit = imageCombo;

            // João Pedro Luchiari - 22/12/2017 - Buscar imagem da tabela GSITUACAO.

            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();
            DataTable dtImagem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IMAGEM, DESCRICAO, CODSITUACAO FROM GSITUACAO WHERE TABELA = 'FLANCA'");
            for (int i = 0; i < dtImagem.Rows.Count; i++)
            {
                byte[] imagemEmBytes = (byte[])dtImagem.Rows[i]["IMAGEM"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagemEmBytes);
                images.AddImage(Image.FromStream(ms));
            }
            imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[0]["DESCRICAO"].ToString(), 1, 0));
            imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[1]["DESCRICAO"].ToString(), 0, 1));

            imageCombo.SmallImages = images;
            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns["FLANCA.TIPOPAGREC"].ColumnEdit = imageCombo;
        }


        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            carregaGrid(query);
        }

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            carregaGrid(query);
        }

        private void btnSalvarLayout_Click(object sender, EventArgs e)
        {
            salvarLayout();
        }

        private void salvarLayout()
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
                    //FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
                carregaGrid(query);
            }

        }

        private void btnFiltros_Click(object sender, EventArgs e)
        {
            Filtro.frmFiltroLancamento frm = new Filtro.frmFiltroLancamento();
            frm.aberto = true;
            frm.ShowDialog();

            query = frm.condicao;
            carregaGrid(query);
        }

        private void btnAgrupar_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsView.ShowGroupPanel == true)
            {
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.ClearGrouping();
                btnAgrupar.Text = "Agrupar";
            }
            else
            {
                gridView1.OptionsView.ShowGroupPanel = true;
                btnAgrupar.Text = "Desagrupar";
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsFind.AlwaysVisible == true)
            {
                gridView1.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gridView1.OptionsFind.AlwaysVisible = true;
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            PSPartLancaEdit frm = new PSPartLancaEdit();
            frm.edita = false;
            frm.ShowDialog();
            carregaGrid(query);
        }

        private void atualizaColuna(DataRow dr)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FLANCA WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { dr["FLANCA.CODLANCA"], AppLib.Context.Empresa });

            for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
            {
                if (gridView1.Columns[i].ReadOnly == false)
                {
                    try
                    {
                        string a = dt.Rows[0][gridView1.Columns[i].FieldName.ToString().Remove(0, gridView1.Columns[i].FieldName.ToString().IndexOf(".") + 1)].ToString();
                        dr[gridView1.Columns[i].FieldName] = a;
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                PSPartLancaEdit frm = new PSPartLancaEdit();
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                if (row1 != null)
                {
                    frm.codlanca = Convert.ToInt32(row1["FLANCA.CODLANCA"]);
                    frm.edita = true;
                    frm.ShowDialog();
                    atualizaColuna(row1);
                }

            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                PSPartLancaEdit frm = new PSPartLancaEdit();
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                if (row1 != null)
                {
                    frm.codlanca = Convert.ToInt32(row1["FLANCA.CODLANCA"]);
                    frm.edita = true;
                    frm.ShowDialog();
                    atualizaColuna(row1);
                }
            }
        }

        private void btnCancelarLancamento_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja cancelar o lançamento?", "Informação do Sistema.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                CancelaLancamento();
            }
        }

        public void CancelaLancamento()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();

            try
            {

                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                if (row1["FLANCA.CODSTATUS"].ToString() == "2" || row1["FLANCA.CODSTATUS"].ToString() == "1")
                {
                    MessageBox.Show(@"Somente lançamentos com Status ""Aberto"" podem ser cancelados.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (row1["FLANCA.ORIGEM"].ToString() == "O")
                {
                    MessageBox.Show("Não foi possível realizar o cancelamento " + row1["FLANCA.NUMERO"].ToString());
                    return;
                }

                if (PossuiOperacaoRelacionada(Convert.ToInt32(row1["FLANCA.CODLANCA"]), conn) == true)
                {
                    MessageBox.Show("Não foi possível realizar o cancelamento " + row1["FLANCA.NUMERO"].ToString() + " pois o mesmo está relacionado a uma operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (PossuiAntecessorRelacionamento(Convert.ToInt32(row1["FLANCA.CODLANCA"]), conn) == true)
                {
                    throw new Exception("Não foi possível realizar o cancelamento do lançamento [ " + row1["FLANCA.NUMERO"].ToString() + " ] pois o mesmo está relacionado a um outro lançamento.");
                }
                conn.ExecTransaction("UPDATE FLANCA SET CODSTATUS = ? WHERE CODEMPRESA = ? AND CODLANCA = ?", new object[] { 2, AppLib.Context.Empresa, row1["FLANCA.CODLANCA"] });
                conn.Commit();
                carregaGrid(query);
            }
            catch (Exception ex)
            {
                conn.Rollback();
                MessageBox.Show(ex.Message);
            }
        }

        public bool PossuiOperacaoRelacionada(int codLanca, AppLib.Data.Connection conn)
        {
            string sSql = string.Empty;

            object retorno = conn.ExecGetField(null, @"SELECT CODOPER FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ? AND ORIGEM = 'O'", AppLib.Context.Empresa, codLanca);

            if (retorno == null)
                return false;
            else
                if (retorno.ToString() == string.Empty)
                return false;
            else
                return true;
        }

        public bool PossuiAntecessorRelacionamento(int codLanca, AppLib.Data.Connection conn)
        {
            object retorno = conn.ExecGetField(null, @"SELECT CODLANCA FROM FLANCARELAC WHERE CODEMPRESA = ? AND CODLANCARELAC = ?", AppLib.Context.Empresa, codLanca);
            if (retorno == null)
            {
                return false;
            }
            return true;
        }

        private void btnBaixaLancamento_Click(object sender, EventArgs e)
        {
            List<int> ListaCodLanca = new List<int>();
            string tipoPagRec = string.Empty;
            string codConta = string.Empty;
            string dtVencimento = string.Empty;
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                if ((row["FLANCA.CODSTATUS"].ToString() != "0") && (row["FLANCA.CODSTATUS"].ToString() != "3") && (row["FLANCA.CODSTATUS"].ToString() != "4"))
                {
                    MessageBox.Show("Somente lançamentos abertos podem ser baixados.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ListaCodLanca.Add(Convert.ToInt32(row["FLANCA.CODLANCA"]));

                if (i == 0)
                {
                    tipoPagRec = row["FLANCA.TIPOPAGREC"].ToString();
                }
                else
                {
                    if (tipoPagRec != row["FLANCA.TIPOPAGREC"].ToString())
                    {
                        MessageBox.Show("Lançamentos com classificação diferentes não podem ser baixados.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                dtVencimento = string.Format("{0:dd/MM/yyyy}", (row["FLANCA.DATAVENCIMENTO"]));
                codConta = string.IsNullOrEmpty(row["FLANCA.CODCONTA"].ToString()) ? string.Empty : row["FLANCA.CODCONTA"].ToString();
            }
            Processos.Financeiro.frmBaixaLancamento frm = new Processos.Financeiro.frmBaixaLancamento(ListaCodLanca, tipoPagRec, codConta, dtVencimento);
            frm.ShowDialog();
            carregaGrid(query);
        }

        private int getUltimoNumeroFextrato(AppLib.Data.Connection conn)
        {
            return Convert.ToInt32(conn.ExecGetField(0, "SELECT MAX(IDLOG) FROM GLOG WHERE CODTABELA = ?", new object[] { "FEXTRATO" }));
        }

        private bool inseriExtrato(int codLanca, AppLib.Data.Connection conn)
        {
            try
            {
                DataTable dt = new DataTable();

                int idextrato = getUltimoNumeroFextrato(conn) + 1;

                dt = conn.ExecQuery("SELECT FEXTRATO.* FROM FLANCA INNER JOIN FEXTRATO ON FLANCA.CODEMPRESA = FEXTRATO.CODEMPRESA AND FLANCA.IDEXTRATO = FEXTRATO.IDEXTRATO WHERE FLANCA.CODLANCA = ?", new object[] { codLanca });
                if (dt.Rows[0]["TIPO"].ToString().Equals("0"))
                {
                    conn.ExecTransaction("INSERT INTO FEXTRATO (CODEMPRESA, IDEXTRATO, CODFILIAL, CODCONTA, NUMERODOCUMENTO, DATA, VALOR, HISTORICO, TIPO, CODCCUSTO, CODNATUREZAORCAMENTO) VALUES (?,?,?,?,?,?,?,?,?,?,?)", new object[] { dt.Rows[0]["CODEMPRESA"], idextrato, dt.Rows[0]["CODFILIAL"], dt.Rows[0]["CODCONTA"], dt.Rows[0]["NUMERODOCUMENTO"], dt.Rows[0]["DATA"], dt.Rows[0]["VALOR"], dt.Rows[0]["HISTORICO"], "1", dt.Rows[0]["CODCCUSTO"], dt.Rows[0]["CODNATUREZAORCAMENTO"], });

                }
                else if (dt.Rows[0]["TIPO"].ToString().Equals("5"))
                {
                    conn.ExecTransaction("INSERT INTO FEXTRATO (CODEMPRESA, IDEXTRATO, CODFILIAL, CODCONTA, NUMERODOCUMENTO, DATA, VALOR, HISTORICO, TIPO, CODCCUSTO, CODNATUREZAORCAMENTO) VALUES (?,?,?,?,?,?,?,?,?,?,?)", new object[] { dt.Rows[0]["CODEMPRESA"], idextrato, dt.Rows[0]["CODFILIAL"], dt.Rows[0]["CODCONTA"], dt.Rows[0]["NUMERODOCUMENTO"], dt.Rows[0]["DATA"], dt.Rows[0]["VALOR"], dt.Rows[0]["HISTORICO"], "4", dt.Rows[0]["CODCCUSTO"], dt.Rows[0]["CODNATUREZAORCAMENTO"], });
                }
                else
                {
                    conn.ExecTransaction("INSERT INTO FEXTRATO (CODEMPRESA, IDEXTRATO, CODFILIAL, CODCONTA, NUMERODOCUMENTO, DATA, VALOR, HISTORICO, TIPO, CODCCUSTO, CODNATUREZAORCAMENTO) VALUES (?,?,?,?,?,?,?,?,?,?,?)", new object[] { dt.Rows[0]["CODEMPRESA"], idextrato, dt.Rows[0]["CODFILIAL"], dt.Rows[0]["CODCONTA"], dt.Rows[0]["NUMERODOCUMENTO"], dt.Rows[0]["DATA"], dt.Rows[0]["VALOR"], dt.Rows[0]["HISTORICO"], "0", dt.Rows[0]["CODCCUSTO"], dt.Rows[0]["CODNATUREZAORCAMENTO"], });

                }

                conn.ExecTransaction("UPDATE GLOG SET IDLOG = ? WHERE CODTABELA = ?", new object[] { idextrato, "FEXTRATO" });
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Boolean TrataChequeCancelamentoBaixa(int CODEMPRESA, int CODLANCA, AppLib.Data.Connection conn)
        {
            int CODCHEQUE = this.ExisteChequeLancamento(AppLib.Context.Empresa, CODLANCA, conn);
            if (CODCHEQUE != 0)
            {
                List<int> listaCODLANCA = ObtemLancamentosMesmoCODCHEQUE(CODEMPRESA, CODCHEQUE, conn);

                int TotalLancamentoCheque = listaCODLANCA.Count;

                int desvinculouLancamento = DesvincularChequeLancamentos(CODEMPRESA, CODCHEQUE, conn);

                //int excluiuCheque = ExcluirCheque(CODEMPRESA, (int)CODCHEQUE);
                if (cancelaCheque(CODEMPRESA, CODCHEQUE, conn) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public int DesvincularChequeLancamentos(int CODEMPRESA, int CODCHEQUE, AppLib.Data.Connection conn)
        {
            String comando = "UPDATE FLANCA SET CODCHEQUE = NULL WHERE CODEMPRESA = ? AND CODCHEQUE = ?";
            return conn.ExecTransaction(comando, CODEMPRESA, CODCHEQUE);
        }

        private bool verificaCompensado(int codLanca, AppLib.Data.Connection conn)
        {
            try
            {
                int idextrato = Convert.ToInt32(conn.ExecGetField(0, "SELECT IDEXTRATO FROM FLANCA WHERE CODLANCA = ?", new object[] { codLanca }));
                if (!string.IsNullOrEmpty(conn.ExecGetField(string.Empty, "SELECT COMPENSADO FROM FEXTRATO WHERE IDEXTRATO = ?", new object[] { idextrato }).ToString()))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<int> ObtemLancamentosMesmoCODCHEQUE(int CODEMPRESA, int CODCHEQUE, AppLib.Data.Connection conn)
        {
            String consulta = "SELECT CODLANCA FROM FLANCA WHERE CODEMPRESA = ? AND CODCHEQUE = ?";
            System.Data.DataTable dt = conn.ExecQuery(consulta, CODEMPRESA, CODCHEQUE);
            List<int> listaCODLANCA = new List<int>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listaCODLANCA.Add(int.Parse(dt.Rows[i]["FLANCA.CODLANCA"].ToString()));
            }

            return listaCODLANCA;
        }

        private bool cancelaCheque(int codEmpresa, int codCheque, AppLib.Data.Connection conn)
        {
            try
            {
                if (conn.ExecTransaction("UPDATE FCHEQUE SET CANCELADO = 1 WHERE CODEMPRESA = ? AND CODCHEQUE = ?", new object[] { codEmpresa, codCheque }).Equals(1))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public int ExisteChequeLancamento(int CODEMPRESA, int CODLANCA, AppLib.Data.Connection conn)
        {
            return Convert.ToInt32(conn.ExecGetField(0, "SELECT CODCHEQUE FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?", CODEMPRESA, CODLANCA));
        }

        private bool verificaNFOUDUP(int codlanca, AppLib.Data.Connection conn)
        {
            try
            {
                if (conn.ExecGetField(string.Empty, @"SELECT NFOUDUP FROM FLANCA WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { codlanca, AppLib.Context.Empresa }).ToString().Equals("1"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private void btnGeraFatura_Click(object sender, EventArgs e)
        {
            List<DataRow> listaDataRow = new List<DataRow>();

            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                listaDataRow.Add(row1);
            }
            try
            {
                Processos.Financeiro.frmFaturaFinanceiro frm = new Processos.Financeiro.frmFaturaFinanceiro(listaDataRow);
                frm.ShowDialog();
                carregaGrid(query);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível gerar a fatura, favor adicionar os campos obrigatórios na Visão.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnCancelarBaixa_Click(object sender, EventArgs e)
        {
            List<DataRow> listaDataRow = new List<DataRow>();
            if (gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("Favor selecione um registro para realizar o cancelamento da baixa.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                listaDataRow.Add(row1);
            }
            try
            {
                Processos.Financeiro.frmCancelaBaixaFinanceiro frm = new Processos.Financeiro.frmCancelaBaixaFinanceiro(listaDataRow);
                frm.ShowDialog();
                carregaGrid(query);

            }
            catch (Exception)
            {

            }
        }

        private void btnGeraBoleto_Click(object sender, EventArgs e)
        {
            int env = 0, nEnv = 0;
            PS.Lib.Global gb = new PS.Lib.Global();
            List<DataRow> lista = new List<DataRow>();
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                lista.Add(gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString())));
            }
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                //string status = row["FLANCA.CODTIPDOC"].ToString();

                //Verificar se o mesmo já foi gerado

                Codlanca = row["FLANCA.CODLANCA"].ToString();

                bool GeraBoleto = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT FTIPDOC.GERABOLETO FROM FTIPDOC INNER JOIN FLANCA ON FLANCA.CODEMPRESA = FTIPDOC.CODEMPRESA AND FLANCA.CODTIPDOC = FTIPDOC.CODTIPDOC WHERE FLANCA.CODEMPRESA = ? AND FLANCA.CODLANCA = ?", new object[] { AppLib.Context.Empresa, Codlanca }));

                if (Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT Count(*) FROM FBOLETO WHERE CODLANCA = ? AND CODFILIAL = ?", new object[] { row["FLANCA.CODLANCA"], AppLib.Context.Filial })).Equals(0))
                {
                    if (GeraBoleto == true)
                    {
                        GerarBoleto(gb.RetornaDataFieldByDataRow(row));
                        env = env + 1;
                    }
                    else
                    {
                        nEnv = nEnv + 1;
                    }
                }
                else
                {
                    nEnv = nEnv + 1;
                }
                MessageBox.Show("Operação Concluída. \nQtde de Boletos Gerados: " + env + "\nQtde de Boletos não Gerados: " + nEnv + "\n", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void GerarBoleto(List<DataField> objArr)
        {
            PSPartGerarBoletoData psPartGerarBoletoData = new PSPartGerarBoletoData();
            psPartGerarBoletoData._tablename = "FBOLETO";
            psPartGerarBoletoData._keys = new string[] { "CODEMPRESA", "CODLANCA" };

            psPartGerarBoletoData.GerarBoleto(objArr);
        }

        private void btnCancelarFatura_Click(object sender, EventArgs e)
        {
            int sim = 0, nao = 0;
            if (PS.Lib.PSMessageBox.ShowQuestion("Confirma o cancelamento da(s) faturas(s) selecionada(s) ?") == System.Windows.Forms.DialogResult.Yes)
            {
                for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                {
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                    if (verificaStatusLancamento(Convert.ToInt32(row1["FLANCA.CODLANCA"])).Equals(false))
                    {
                        MessageBox.Show("Somente faturas em aberto podem ser canceladas.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string nfoudup = verificaNFOUDUP(Convert.ToInt32(row1["FLANCA.CODLANCA"]));
                    if (string.IsNullOrEmpty(nfoudup))
                    {
                        MessageBox.Show("Somente lançamento de fatura podem ser cancelados.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (nfoudup.Equals("0"))
                    {

                        DataTable dtCodLanca = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODLANCA, NFOUDUP FROM FLANCA WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { Convert.ToInt32(row1["FLANCA.CODLANCA"]), AppLib.Context.Empresa });
                        for (int ii = 0; ii < dtCodLanca.Rows.Count; ii++)
                        {
                            if (CancelaFatura(Convert.ToInt32(dtCodLanca.Rows[ii]["CODLANCA"].ToString()), Convert.ToInt32(row1["FLANCA.CODLANCA"])))
                            {
                                if (row1["FLANCA.NFOUDUP"].ToString() == "0")
                                {
                                    sim++;
                                }
                            }
                            else
                            {
                                nao++;
                            }
                        }
                    }
                    else
                    {
                        int CodFatura = VerificaCodFatura(Convert.ToInt32(row1["FLANCA.CODLANCA"]));
                        if (!CodFatura.Equals(0))
                        {
                            if (CancelaFatura(Convert.ToInt32(row1["FLANCA.CODLANCA"]), CodFatura))
                            {
                                if (row1["FLANCA.NFOUDUP"].ToString() == "0")
                                {
                                    sim++;
                                }
                            }
                            else
                            {
                                nao++;
                            }
                        }
                        else
                        {
                            nao++;
                        }
                    }
                }
                MessageBox.Show("Total de Faturas Canceladas com sucesso: " + sim + "\n Total de Faturas não Canceladas: " + nao + "", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return;
        }

        /// <summary>
        /// Verifica o status do lançamento, retorna true para aberto
        /// </summary>
        /// <param name="codlanca">Número do Lançamento</param>
        /// <returns>True ou False</returns>
        private bool verificaStatusLancamento(int codlanca)
        {
            try
            {
                string codStatus = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODSTATUS FROM FLANCA WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { codlanca, AppLib.Context.Empresa }).ToString();
                if (codStatus.Equals("0"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// Verifica o campo NFOUDUP, retorna true para 0
        /// </summary>
        /// <param name="codlanca">Número do Lançamento</param>
        /// <returns>True ou False</returns>
        private string verificaNFOUDUP(int codlanca)
        {
            try
            {
                return AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT NFOUDUP FROM FLANCA WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { codlanca, AppLib.Context.Empresa }).ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private bool CancelaFatura(int codLanca, int codFatura)
        {
            try
            {
                if (!AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FLANCA SET CODFATURA = NULL, NFOUDUP = ? WHERE CODFATURA = ? AND CODEMPRESA = ?", new object[] { "", codFatura, AppLib.Context.Empresa }).Equals(0))
                {
                    if (AlteraStatusFatura(codFatura))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool AlteraStatusFatura(int codFatura)
        {
            try
            {
                if (!AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FLANCA SET CODSTATUS = 2 WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { codFatura, AppLib.Context.Empresa }).Equals(0))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private int VerificaCodFatura(int codLanca)
        {
            return Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT CODFATURA FROM FLANCA WHERE CODLANCA = ?", new object[] { codLanca }));
        }

        private void btnVinculaLancamento_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                Processos.Financeiro.frmVinculoLancamento frm = new Processos.Financeiro.frmVinculoLancamento(Convert.ToInt32(row1["FLANCA.CODLANCA"]));
                frm.ShowDialog();
                carregaGrid(query);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            int sim = 0;
            int nao = 0;

            if (MessageBox.Show("Deseja excluir o lançamento?", "Informação do Sistema.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                {
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                    if (row1["FLANCA.CODSTATUS"].ToString() == "2" && row1["FLANCA.ORIGEM"].ToString() == "F")
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?", new object[] { AppLib.Context.Empresa, row1["FLANCA.CODLANCA"] });
                        sim = sim + 1;
                    }
                    else
                    {
                        MessageBox.Show("Lançamento " + row1["FLANCA.CODLANCA"].ToString() + " não pode ser excluído.\r\nApenas lançamentos cancelados podem ser excluídos.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        nao = nao + 1;
                    }
                }
                MessageBox.Show("Qtd. de Lançamentos excluídos com sucesso: " + sim + " \nQtd. de Lançamentos não excluídos: " + nao, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                carregaGrid(query);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            string reportPath = string.Empty;
            FolderBrowserDialog open = new FolderBrowserDialog();
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                reportPath = open.SelectedPath + "Clientes.xlsx";
                DevExpress.XtraPrinting.XlsExportOptions xlsxOptions = new DevExpress.XtraPrinting.XlsExportOptions();
                xlsxOptions.ShowGridLines = true;
                xlsxOptions.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value;
                xlsxOptions.ExportHyperlinks = true;
                xlsxOptions.SheetName = "Clientes";
                gridView1.ExportToXlsx(reportPath);
                StartProcess(reportPath);
            }
        }
        private void StartProcess(string path)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            try
            {
                process.StartInfo.FileName = path;
                process.Start();
                process.WaitForInputIdle();
            }
            catch { }
        }

        private void btnlançamentoPadrao_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                PSPartLancaEdit frm = new PSPartLancaEdit(true);
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                if (row1 != null)
                {
                    frm.codlanca = Convert.ToInt32(row1["FLANCA.CODLANCA"]);
                    frm.edita = false;
                    frm.lancamentopadrao = true;
                    frm.chklancamentopadrao.Checked = true;
                    frm.CarregaCampos();
                    frm.ValidaNumeracao();
                    frm.ShowDialog();
                    atualizaColuna(row1);
                }
            }
        }

        private void btnparcelarlancamento_Click(object sender, EventArgs e)
        {
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            if (gridView1.SelectedRowsCount > 0)
            {
                List<int> codStatus = new List<int>();
                codStatus.Add(0); //Aberto
                codStatus.Add(3); //Vencido
                codStatus.Add(4); //Vencendo Hoje

                if (codStatus.Contains(Convert.ToInt16(row1["FLANCA.CODSTATUS"].ToString())))
                {
                    frmCadastroParcelamento frm = new frmCadastroParcelamento();
                    frm.codLanca = row1["FLANCA.CODLANCA"].ToString();
                    frm.ShowDialog();
                    atualizaColuna(row1);
                }
                else
                {
                    MessageBox.Show("Somente lançamentos abertos podem ser parcelados.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Favor selecionar o registro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                getIdReport();
            }
            else
            {
                MessageBox.Show("Nenhum registro selecionado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void getIdReport()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT *
FROM 
ZREPORT 
WHERE 
CODREPORTTIPO = 'BOLETO'", new Object[] { });

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Não existe relatório parametrizado para este tipo de operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dt.Rows.Count == 1)
            {
                int IDREPORT = Convert.ToInt32(dt.Rows[0]["IDREPORT"]);
                this.ImprimirLancamento(IDREPORT);
            }

            if (dt.Rows.Count > 1)
            {
                AppLib.Windows.FormListaPrompt fLista = new AppLib.Windows.FormListaPrompt();
                fLista.PrimeiroItemNulo = false;
                Object result = fLista.Mostrar("Selecione o relatório:", dt);

                if (fLista.confirmacao == AppLib.Global.Types.Confirmacao.OK)
                {
                    int IDREPORT = Convert.ToInt32(result);
                    this.ImprimirLancamento(IDREPORT);
                }
            }
        }

        public void ImprimirLancamento(int IDREPORT)
        {
            String ListCODLANCA = "";

            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                ListCODLANCA += Convert.ToInt32(row1["FLANCA.CODLANCA"]);
                ListCODLANCA += ", ";
            }

            ListCODLANCA = ListCODLANCA.Substring(0, ListCODLANCA.Length - 2);

            AppLib.Padrao.FormReportVisao f = new AppLib.Padrao.FormReportVisao();
            f.grid1.Conexao = "Start";
            f.Visualizar(IDREPORT, ListCODLANCA);
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount == 1)
            {
                Contador = 1;
                lbResultRegistros.Text = Contador.ToString();
                lbTotalResult.Text = string.Concat("R$", new Class.Utilidades().CalculaTotal(gridView1, "FLANCA").ToString());
                Contador = 0;
            }
            else
            {
                for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                {
                    Contador = gridView1.SelectedRowsCount;

                    lbResultRegistros.Text = Contador.ToString();
                    lbTotalResult.Text = string.Concat("R$", new Class.Utilidades().CalculaTotal(gridView1, "FLANCA").ToString());
                }
            }
        }

        private void extratosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void frmVisaoLancamento_Load(object sender, EventArgs e)
        {

        }
    }
}
