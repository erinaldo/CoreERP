using DevExpress.XtraGrid.Views.Grid;
using ITGProducao.Class;
using ITGProducao.Controles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITGProducao.Formularios
{
    public partial class FrmOrdemProducao : Form
    {

        public bool edita = false;
        public string codOrdem = string.Empty;
        public string seqOrdem = string.Empty;
        public string codEstrutura = string.Empty;
        public string CodRevEstrutura = string.Empty;

        private List<string> tabelasFilhas = new List<string>();

        //Variaveis para NewLookup
        private NewLookup lookup;

        private DataTable dtOperacao;
        private DataTable dtResumo;

        private DataTable dtRecursosRecursos;
        private DataTable dt2RecursosRecursos;
        private DataTable dtRecursosMaodeObra;
        private DataTable dtRecursosFerramentas;

        private DataTable dtRecursosComponentes;
        private DataTable dt2RecursosComponentes;

        private DataTable dtGrid2Recursos;

        private DataTable dt1Apontamento;
        private DataTable dt2Apontamento;

        private DataTable dt1Entrada;
        private DataTable dt2Entrada;

        private DataTable dt1Compra;
        private DataTable dt2Compra;

        public FrmOrdemProducao()
        {
            InitializeComponent();

            lookupEstrutura.txtcodigo.Leave += new System.EventHandler(lookupEstrutura_txtcodigo_Leave);
            lookupEstrutura.btnprocurar.Click += new System.EventHandler(lookupEstrutura_txtcodigo_Leave);
            lookupEstrutura.txtconteudo.Click += new System.EventHandler(lookupEstrutura_txtcodigo_Leave);

            lookupoperacao.txtcodigo.Leave += new System.EventHandler(lookupoperacao_txtcodigo_Leave);
            lookupoperacao.btnprocurar.Click += new System.EventHandler(lookupoperacao_txtcodigo_Leave);
            lookupoperacao.txtconteudo.Click += new System.EventHandler(lookupoperacao_txtcodigo_Leave);

            new PS.Glb.Class.Utilidades().criaCamposComplementares("PORDEMCOMPL", tabCamposComplementares);
        }

        public FrmOrdemProducao(ref NewLookup lookup)
        {
            this.codOrdem = lookup.txtcodigo.Text;
            this.edita = true;

            this.lookup = lookup;

            lookupEstrutura.txtcodigo.Leave += new System.EventHandler(lookupEstrutura_txtcodigo_Leave);
            lookupEstrutura.btnprocurar.Click += new System.EventHandler(lookupEstrutura_txtcodigo_Leave);
            lookupEstrutura.txtconteudo.Click += new System.EventHandler(lookupEstrutura_txtcodigo_Leave);

            lookupoperacao.txtcodigo.Leave += new System.EventHandler(lookupoperacao_txtcodigo_Leave);
            lookupoperacao.btnprocurar.Click += new System.EventHandler(lookupoperacao_txtcodigo_Leave);
            lookupoperacao.txtconteudo.Click += new System.EventHandler(lookupoperacao_txtcodigo_Leave);

            new PS.Glb.Class.Utilidades().criaCamposComplementares("PORDEMCOMPL", tabCamposComplementares);
        }

        private void lookupoperacao_txtcodigo_Leave(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(lookupoperacao.ValorCodigoInterno))
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM POPERACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupoperacao.ValorCodigoInterno.ToString() });
                if (dt.Rows.Count > 0)
                {
                    lookupcentrotrabalho.txtcodigo.Text = dt.Rows[0]["CODCTRABALHO"].ToString();
                    lookupcentrotrabalho.CarregaDescricao();
                }

                DataRow row1 = gridViewOperacao.GetDataRow(Convert.ToInt32(gridViewOperacao.GetSelectedRows().GetValue(0).ToString()));
                DataTable dt2 = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPERACAO = ? AND SEQOPERACAO = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupoperacao.ValorCodigoInterno.ToString(), row1[0].ToString(), codEstrutura, CodRevEstrutura });
                if (dt2.Rows.Count > 0)
                {
                    var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "," };

                    txtRoteiroPlanejadoSetup.Text = dt2.Rows[0]["TEMPOSETUP"].ToString();

                    switch (dt2.Rows[0]["TIPOTEMPOEXTRA"].ToString())
                    {
                        case "1": //FIXO
                            txtRoteiroPlanejadoExtra.Text = dt2.Rows[0]["TEMPOEXTRA"].ToString();
                            break;
                        case "2": //PROPORCIONAL
                            txtRoteiroPlanejadoExtra.Text = Convert.ToString(Math.Ceiling(Decimal.Parse(dt2.Rows[0]["TEMPOEXTRA"].ToString(), numberFormatInfo) * Decimal.Parse(txtQtPlan.Text, numberFormatInfo)));
                            break;
                        default:
                            txtRoteiroPlanejadoExtra.Text = "0";
                            break;
                    }

                    switch (dt2.Rows[0]["TIPOTEMPO"].ToString())
                    {
                        case "1": //FIXO
                            txtRoteiroPlanejadoOperacao.Text = dt2.Rows[0]["TEMPOOPERACAO"].ToString();
                            break;
                        case "2": //PROPORCIONAL
                            txtRoteiroPlanejadoOperacao.Text = Convert.ToString(Math.Ceiling(Decimal.Parse(dt2.Rows[0]["TEMPOOPERACAO"].ToString(), numberFormatInfo) * Decimal.Parse(txtQtPlan.Text, numberFormatInfo)));
                            break;
                        case "3": //INTERVALO
                            DataTable dt3 = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PROTEIROPORINTERVALO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPERACAO = ? AND SEQOPERACAO = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupoperacao.ValorCodigoInterno.ToString(), row1[0].ToString(), codEstrutura, CodRevEstrutura });
                            if (dt3.Rows.Count > 0)
                            {
                                DataRow[] rows = dt3.Select("FAIXAINICIAL < '" + Convert.ToInt16(Math.Ceiling(Convert.ToDecimal(txtQtPlan.Text.ToString()))) + "' AND FAIXAFINAL >= '" + Convert.ToInt16(Math.Ceiling(Convert.ToDecimal(txtQtPlan.Text.ToString()))) + "'");

                                if (rows.Count() == 0)
                                {
                                    txtRoteiroPlanejadoOperacao.Text = "0";
                                    MessageBox.Show("Quantidade da ordem de produção ultrapassa limites das faixas.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    txtRoteiroPlanejadoOperacao.Text = Convert.ToInt16(rows[0]["TEMPOOPERACAO"]).ToString();
                                }
                            }
                            break;
                        default:
                            txtRoteiroPlanejadoExtra.Text = "0";
                            break;
                    }

                    txtRoteiroPlanejadoTotal.Text = Convert.ToString(Convert.ToDecimal(txtRoteiroPlanejadoSetup.Text.ToString()) + Convert.ToDecimal(txtRoteiroPlanejadoOperacao.Text.ToString()) + Convert.ToDecimal(txtRoteiroPlanejadoExtra.Text.ToString()));
                }
            }
        }

        private void lookupEstrutura_txtcodigo_Leave(object sender, System.EventArgs e)
        {
            //POSSIBILIDADE DE CRIAR PROCESSO PARA ALTERACAO DO PRODUTO DA ESTRUTURA
            //if (edita == true)
            //{
            //    if (cmbStatus.SelectedValue == "1")
            //    {
            //        if (Existe_Ordem() == true)
            //        {
            //            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();

            //            conn.BeginTransaction();

            //            conn.ExecTransaction("DELETE FROM PORDEMENTRADA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, txtNroOp.Text, txtSeqOp.Text });
            //            conn.ExecTransaction("DELETE FROM PORDEMCONSUMO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, txtNroOp.Text, txtSeqOp.Text });
            //            conn.ExecTransaction("DELETE FROM PORDEMAPTORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, txtNroOp.Text, txtSeqOp.Text });
            //            conn.ExecTransaction("DELETE FROM PORDEMAPONTAMENTO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, txtNroOp.Text, txtSeqOp.Text });
            //            conn.ExecTransaction("DELETE FROM PORDEMALOCACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, txtNroOp.Text, txtSeqOp.Text });
            //            conn.ExecTransaction("DELETE FROM PORDEMROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, txtNroOp.Text, txtSeqOp.Text });

            //            conn.Commit();

            //            LIMPAR ABAS
            //        }
            //    }
            //}

            if (!string.IsNullOrEmpty(lookupEstrutura.ValorCodigoInterno))
            {
                txtDescricao.Text = lookupEstrutura.txtconteudo.Text;

                if (lookupEstrutura.OutrasChaves.Any(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA") == true)
                {
                    int indexRevisao = lookupEstrutura.OutrasChaves.IndexOf(lookupEstrutura.OutrasChaves.Where(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA").FirstOrDefault());

                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PROTEIROESTRUTURA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno.ToString(), lookupEstrutura.OutrasChaves[0].ValorColunaChave.ToString() });
                    if (dt.Rows.Count > 0)
                    {
                        lookupunidade.txtcodigo.Text = dt.Rows[0]["UNDCONTROLE"].ToString();
                        lookupunidade.CarregaDescricao();

                        CarregaGrid_Operacao(lookupEstrutura.ValorCodigoInterno.ToString(), lookupEstrutura.OutrasChaves[0].ValorColunaChave.ToString());

                        cmbTipo.SelectedValue = dt.Rows[0]["TIPOESTRUTURA"].ToString();
                    }
                }
            }
        }

        void LimpaGrid_OrdemResumo()
        {
            string tabela = "PORDEMROTEIRO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql = @"SELECT	TOP 0  
                                        PORDEMROTEIRO.SEQOPERACAO AS 'PORDEMROTEIRO.SEQOPERACAO',
		                                POPERACAO.DESCOPERACAO AS 'POPERACAO.DESCOPERACAO',
		                                PORDEMROTEIRO.CODOPERACAO AS 'PORDEMROTEIRO.CODOPERACAO',
		                                PORDEMROTEIRO.CODCTRABALHO AS 'PORDEMROTEIRO.CODCTRABALHO',
		                                PCENTROTRABALHO.DESCCTRABALHO AS 'PCENTROTRABALHO.DESCCTRABALHO',
		                                CASE PORDEMROTEIRO.STATUSOPERACAO WHEN 1 THEN '1 - Aguardando Liberação' 
									                                      WHEN 2 THEN '2 - Planejada'
										                                  WHEN 3 THEN '3 - Em Produção'
										                                  WHEN 4 THEN '4 - Paralisada'
										                                  WHEN 5 THEN '5 - Cancelada'
										                                  WHEN 6 THEN '6 - Concluída' END AS 'PORDEMROTEIRO.STATUSOPERACAO',
		                                PORDEMROTEIRO.QTDEREAL AS 'PORDEMROTEIRO.QTDEREAL',
PORDEMROTEIRO.PERCCONCLUIDOROTEIRO AS 'PORDEMROTEIRO.PERCCONCLUIDOROTEIRO',
                                        PORDEMROTEIRO.CODESTRUTURA AS 'PORDEMROTEIRO.CODESTRUTURA',
                                        PORDEMROTEIRO.REVESTRUTURA AS 'PORDEMROTEIRO.REVESTRUTURA',
                                        PORDEMROTEIRO.CODIGOOP AS 'PORDEMROTEIRO.CODIGOOP',
                                        PORDEMROTEIRO.SEQOP AS 'PORDEMROTEIRO.SEQOP',
                                        PORDEMROTEIRO.DATAINIPLAN AS 'PORDEMROTEIRO.DATAINIPLAN',
                                        PORDEMROTEIRO.DATAFIMPLAN AS 'PORDEMROTEIRO.DATAFIMPLAN',
                                        PORDEMROTEIRO.DATAINIREAL AS 'PORDEMROTEIRO.DATAINIREAL',
                                        PORDEMROTEIRO.DATAFIMREAL AS 'PORDEMROTEIRO.DATAFIMREAL',
                                        PORDEMROTEIRO.DATAINIORIG AS 'PORDEMROTEIRO.DATAINIORIG',
                                        PORDEMROTEIRO.DATAFIMORIG AS 'PORDEMROTEIRO.DATAFIMORIG',
                                        PORDEMROTEIRO.OPERACAOEXTERNA AS 'PORDEMROTEIRO.OPERACAOEXTERNA',
                                        PORDEMROTEIRO.TEMPOSETUP AS 'PORDEMROTEIRO.TEMPOSETUP',
                                        PORDEMROTEIRO.TEMPOOPERACAO AS 'PORDEMROTEIRO.TEMPOOPERACAO',
                                        PORDEMROTEIRO.TEMPOEXTRA AS 'PORDEMROTEIRO.TEMPOEXTRA',
                                        PORDEMROTEIRO.TEMPOTOTAL AS 'PORDEMROTEIRO.TEMPOTOTAL',
                                        PORDEMROTEIRO.TEMPOSETUPREAL AS 'PORDEMROTEIRO.TEMPOSETUPREAL',
                                        PORDEMROTEIRO.TEMPOOPERACAOREAL AS 'PORDEMROTEIRO.TEMPOOPERACAOREAL',
                                        PORDEMROTEIRO.TEMPOEXTRAREAL AS 'PORDEMROTEIRO.TEMPOEXTRAREAL',
                                        PORDEMROTEIRO.TEMPOTOTALREAL AS 'PORDEMROTEIRO.TEMPOTOTALREAL'
                                  FROM PORDEMROTEIRO JOIN POPERACAO ON PORDEMROTEIRO.CODEMPRESA = POPERACAO.CODEMPRESA 
				                                 AND PORDEMROTEIRO.CODFILIAL = POPERACAO.CODFILIAL 
				                                 AND PORDEMROTEIRO.CODOPERACAO = POPERACAO.CODOPERACAO
				                                JOIN PCENTROTRABALHO ON PORDEMROTEIRO.CODEMPRESA = PCENTROTRABALHO.CODEMPRESA
				                                 AND PORDEMROTEIRO.CODFILIAL = PCENTROTRABALHO.CODFILIAL 
				                                 AND PORDEMROTEIRO.CODCTRABALHO = PCENTROTRABALHO.CODCTRABALHO";

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {
                    sql = sql + filtroUsuario;
                }

                gridResumo.DataSource = null;
                gridViewResumo.Columns.Clear();
                dtResumo = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql); ;
                gridResumo.DataSource = dtResumo;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?,?)", new object[] { tabela, "POPERACAO", "PCENTROTRABALHO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewResumo.Columns.Count; i++)
                {
                    //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewResumo.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewResumo.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewResumo.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void CarregaGrid_OrdemResumo(string codEstrutura, string codRevEstrutura)
        {
            string tabela = "PORDEMROTEIRO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql = @"SELECT	
                                        PORDEMROTEIRO.SEQOPERACAO AS 'PORDEMROTEIRO.SEQOPERACAO',
		                                POPERACAO.DESCOPERACAO AS 'POPERACAO.DESCOPERACAO',
		                                PORDEMROTEIRO.CODOPERACAO AS 'PORDEMROTEIRO.CODOPERACAO',
		                                PORDEMROTEIRO.CODCTRABALHO AS 'PORDEMROTEIRO.CODCTRABALHO',
		                                PCENTROTRABALHO.DESCCTRABALHO AS 'PCENTROTRABALHO.DESCCTRABALHO',
		                                CASE PORDEMROTEIRO.STATUSOPERACAO WHEN 1 THEN '1 - Aguardando Liberação' 
									                                      WHEN 2 THEN '2 - Planejada'
										                                  WHEN 3 THEN '3 - Em Produção'
										                                  WHEN 4 THEN '4 - Paralisada'
										                                  WHEN 5 THEN '5 - Cancelada'
										                                  WHEN 6 THEN '6 - Concluída' END AS 'PORDEMROTEIRO.STATUSOPERACAO',
		                                PORDEMROTEIRO.QTDEREAL AS 'PORDEMROTEIRO.QTDEREAL',
PORDEMROTEIRO.PERCCONCLUIDOROTEIRO AS 'PORDEMROTEIRO.PERCCONCLUIDOROTEIRO',
                                        PORDEMROTEIRO.CODESTRUTURA AS 'PORDEMROTEIRO.CODESTRUTURA',
                                        PORDEMROTEIRO.REVESTRUTURA AS 'PORDEMROTEIRO.REVESTRUTURA',
                                        PORDEMROTEIRO.CODIGOOP AS 'PORDEMROTEIRO.CODIGOOP',
                                        PORDEMROTEIRO.SEQOP AS 'PORDEMROTEIRO.SEQOP',
                                        PORDEMROTEIRO.DATAINIPLAN AS 'PORDEMROTEIRO.DATAINIPLAN',
                                        PORDEMROTEIRO.DATAFIMPLAN AS 'PORDEMROTEIRO.DATAFIMPLAN',
                                        PORDEMROTEIRO.DATAINIREAL AS 'PORDEMROTEIRO.DATAINIREAL',
                                        PORDEMROTEIRO.DATAFIMREAL AS 'PORDEMROTEIRO.DATAFIMREAL',
                                        PORDEMROTEIRO.DATAINIORIG AS 'PORDEMROTEIRO.DATAINIORIG',
                                        PORDEMROTEIRO.DATAFIMORIG AS 'PORDEMROTEIRO.DATAFIMORIG',
                                        PORDEMROTEIRO.OPERACAOEXTERNA AS 'PORDEMROTEIRO.OPERACAOEXTERNA',
                                        PORDEMROTEIRO.TEMPOSETUP AS 'PORDEMROTEIRO.TEMPOSETUP',
                                        PORDEMROTEIRO.TEMPOOPERACAO AS 'PORDEMROTEIRO.TEMPOOPERACAO',
                                        PORDEMROTEIRO.TEMPOEXTRA AS 'PORDEMROTEIRO.TEMPOEXTRA',
                                        PORDEMROTEIRO.TEMPOTOTAL AS 'PORDEMROTEIRO.TEMPOTOTAL',
                                        PORDEMROTEIRO.TEMPOSETUPREAL AS 'PORDEMROTEIRO.TEMPOSETUPREAL',
                                        PORDEMROTEIRO.TEMPOOPERACAOREAL AS 'PORDEMROTEIRO.TEMPOOPERACAOREAL',
                                        PORDEMROTEIRO.TEMPOEXTRAREAL AS 'PORDEMROTEIRO.TEMPOEXTRAREAL',
                                        PORDEMROTEIRO.TEMPOTOTALREAL AS 'PORDEMROTEIRO.TEMPOTOTALREAL'
                                  FROM PORDEMROTEIRO JOIN POPERACAO ON PORDEMROTEIRO.CODEMPRESA = POPERACAO.CODEMPRESA 
				                                 AND PORDEMROTEIRO.CODFILIAL = POPERACAO.CODFILIAL 
				                                 AND PORDEMROTEIRO.CODOPERACAO = POPERACAO.CODOPERACAO
				                                JOIN PCENTROTRABALHO ON PORDEMROTEIRO.CODEMPRESA = PCENTROTRABALHO.CODEMPRESA
				                                 AND PORDEMROTEIRO.CODFILIAL = PCENTROTRABALHO.CODFILIAL 
				                                 AND PORDEMROTEIRO.CODCTRABALHO = PCENTROTRABALHO.CODCTRABALHO
                                 WHERE PORDEMROTEIRO.CODEMPRESA = " + AppLib.Context.Empresa + @"
                                   AND PORDEMROTEIRO.CODFILIAL = " + AppLib.Context.Filial + @"
                                   AND PORDEMROTEIRO.CODESTRUTURA = '" + codEstrutura + @"'
                                   AND PORDEMROTEIRO.REVESTRUTURA = " + CodRevEstrutura + @"
                                   AND PORDEMROTEIRO.CODIGOOP = '" + codOrdem + @"'
                                   AND PORDEMROTEIRO.SEQOP = " + seqOrdem;

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {
                    sql = sql + filtroUsuario;
                }

                gridResumo.DataSource = null;
                gridViewResumo.Columns.Clear();
                dtResumo = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql); ;
                gridResumo.DataSource = dtResumo;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?,?)", new object[] { tabela, "POPERACAO", "PCENTROTRABALHO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewResumo.Columns.Count; i++)
                {
                    //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewResumo.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewResumo.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewResumo.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void CarregaGrid_Operacao(string codEstrutura, string codRevEstrutura)
        {
            string tabela = "PROTEIRO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql = "SELECT PROTEIRO.SEQOPERACAO AS 'PROTEIRO.SEQOPERACAO', CASE POPERACAO.OPERACAOEXTERNA WHEN 1 THEN '(E) ' + POPERACAO.DESCOPERACAO  WHEN 0 THEN POPERACAO.DESCOPERACAO END AS 'POPERACAO.DESCOPERACAO'FROM PROTEIRO JOIN POPERACAO ON PROTEIRO.CODEMPRESA = POPERACAO.CODEMPRESA AND PROTEIRO.CODFILIAL = POPERACAO.CODFILIAL AND PROTEIRO.CODOPERACAO = POPERACAO.CODOPERACAO WHERE PROTEIRO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PROTEIRO.CODFILIAL = " + AppLib.Context.Filial + " AND PROTEIRO.CODESTRUTURA = '" + codEstrutura + "' AND PROTEIRO.REVESTRUTURA = " + codRevEstrutura;

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                gridOperacao.DataSource = null;
                gridViewOperacao.Columns.Clear();
                dtOperacao = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql); ;
                gridOperacao.DataSource = dtOperacao;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?)", new object[] { tabela, "POPERACAO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewOperacao.Columns.Count; i++)
                {
                    //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewOperacao.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewOperacao.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewOperacao.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LimpaGrid_Operacao()
        {
            string tabela = "PROTEIRO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql = "SELECT TOP 0 PROTEIRO.SEQOPERACAO AS 'PROTEIRO.SEQOPERACAO',POPERACAO.DESCOPERACAO AS 'POPERACAO.DESCOPERACAO' FROM PROTEIRO JOIN POPERACAO ON PROTEIRO.CODEMPRESA = POPERACAO.CODEMPRESA AND PROTEIRO.CODFILIAL = POPERACAO.CODFILIAL AND PROTEIRO.CODOPERACAO = POPERACAO.CODOPERACAO WHERE PROTEIRO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PROTEIRO.CODFILIAL = " + AppLib.Context.Filial;

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                gridOperacao.DataSource = null;
                gridViewOperacao.Columns.Clear();
                dtOperacao = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql); ;
                gridOperacao.DataSource = dtOperacao;

                gridOperacao.DataSource = dtOperacao;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?)", new object[] { tabela, "POPERACAO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewOperacao.Columns.Count; i++)
                {
                    //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewOperacao.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewOperacao.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewOperacao.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LimpaGrid1_Recursos()
        {
            string tabela = "PORDEMALOCACAO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql = @"SELECT   TOP 0
                                         PORDEMALOCACAO.CODESTRUTURA AS 'PORDEMALOCACAO.CODESTRUTURA'
		                                ,PORDEMALOCACAO.CODIGOOP AS 'PORDEMALOCACAO.CODIGOOP'
		                                ,PORDEMALOCACAO.SEQOP AS 'PORDEMALOCACAO.SEQOP'
		                                ,PORDEMALOCACAO.SEQOPERACAO AS 'PORDEMALOCACAO.SEQOPERACAO'
		                                ,PORDEMALOCACAO.CODOPERACAO AS 'PORDEMALOCACAO.CODOPERACAO'
		                                ,PORDEMALOCACAO.TIPORECURSO AS 'PORDEMALOCACAO.TIPORECURSO'
		                                ,PORDEMALOCACAO.CODGRUPORECURSO AS 'PO RDEMALOCACAO.CODGRUPORECURSO'
		                                ,PGRUPORECURSO.DESCGRUPORECURSO AS 'PGRUPORECURSO.DESCGRUPORECURSO'
		                                ,PORDEMALOCACAO.CODRECURSO AS 'PORDEMALOCACAO.CODRECURSO'
		                                ,PRECURSO.DESCRECURSO AS 'PRECURSO.DESCRECURSO'
		                                ,PORDEMALOCACAO.DATAALOCACAO AS 'PORDEMALOCACAO.DATAALOCACAO'
		                                ,PORDEMALOCACAO.TEMPOALOCACAO AS 'PORDEMALOCACAO.TEMPOALOCACAO'
		                                ,PORDEMALOCACAO.REVESTRUTURA AS 'PORDEMALOCACAO.REVESTRUTURA'
		                                ,PORDEMALOCACAO.ID AS 'PORDEMALOCACAO.ID'
		                                ,PORDEMALOCACAO.TIPOALOC AS 'PORDEMALOCACAO.TIPOALOC'
                                FROM PORDEMALOCACAO JOIN PRECURSO ON  PORDEMALOCACAO.CODEMPRESA = PRECURSO.CODEMPRESA
                                                                  AND PORDEMALOCACAO.CODFILIAL = PRECURSO.CODFILIAL
                                                                  AND PORDEMALOCACAO.CODRECURSO = PRECURSO.CODRECURSO
                                                    JOIN PGRUPORECURSO ON  PORDEMALOCACAO.CODEMPRESA = PGRUPORECURSO.CODEMPRESA
                                                                       AND PORDEMALOCACAO.CODFILIAL = PGRUPORECURSO.CODFILIAL
                                                                       AND PORDEMALOCACAO.CODGRUPORECURSO = PGRUPORECURSO.CODGRUPORECURSO";
                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                gridRecursosRecursos.DataSource = null;
                gridViewRecursosRecursos.Columns.Clear();

                dtRecursosRecursos = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);
                dtRecursosMaodeObra = dtRecursosRecursos;
                dtRecursosFerramentas = dtRecursosRecursos;

                gridRecursosRecursos.DataSource = dtRecursosRecursos;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?,?)", new object[] { tabela, "PRECURSO", "PGRUPORECURSO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewRecursosRecursos.Columns.Count; i++)
                {
                    //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewRecursosRecursos.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewRecursosRecursos.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewRecursosRecursos.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void CarregaGrid_Componentes(string seqOperacao, string codOperacao)
        {
            string tabela = "PORDEMCONSUMO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {

                string sql = @"SELECT    PORDEMCONSUMO.CODEMPRESA AS 'PORDEMCONSUMO.CODEMPRESA',
				                         PORDEMCONSUMO.CODFILIAL AS 'PORDEMCONSUMO.CODFILIAL',
				                         PORDEMCONSUMO.CODESTRUTURA AS 'PORDEMCONSUMO.CODESTRUTURA',
				                         PORDEMCONSUMO.REVESTRUTURA AS 'PORDEMCONSUMO.REVESTRUTURA',
				                         PORDEMCONSUMO.CODIGOOP AS 'PORDEMCONSUMO.CODIGOOP',
				                         PORDEMCONSUMO.SEQOP AS 'PORDEMCONSUMO.SEQOP',
				                         PORDEMCONSUMO.SEQOPERACAO AS 'PORDEMCONSUMO.SEQOPERACAO',
				                         PORDEMCONSUMO.CODOPERACAO AS 'PORDEMCONSUMO.CODOPERACAO',
                                         PORDEMCONSUMO.CODCOMPONENTE AS 'PORDEMCONSUMO.CODCOMPONENTE',
                                         VPRODUTO.NOME AS 'VPRODUTO.NOME',
				                         VPRODUTO.CODUNIDCONTROLE  AS  'VPRODUTO.CODUNIDCONTROLE',
				                         PORDEMCONSUMO.COMPONENTEORIGINAL AS 'PORDEMCONSUMO.COMPONENTEORIGINAL',
				                         PORDEMCONSUMO.COMPONENTECANCELADO AS 'PORDEMCONSUMO.COMPONENTECANCELADO'
		                        FROM PORDEMCONSUMO JOIN VPRODUTO ON PORDEMCONSUMO.CODCOMPONENTE = VPRODUTO.CODPRODUTO
										                        AND PORDEMCONSUMO.CODEMPRESA = VPRODUTO.CODEMPRESA
                               WHERE PORDEMCONSUMO.CODEMPRESA = " + AppLib.Context.Empresa +
                                  " AND PORDEMCONSUMO.CODFILIAL = " + AppLib.Context.Filial +
                                  " AND PORDEMCONSUMO.CODESTRUTURA = '" + codEstrutura + "' " +
                                  " AND PORDEMCONSUMO.REVESTRUTURA = '" + CodRevEstrutura + "' " +
                                  " AND PORDEMCONSUMO.CODIGOOP = '" + codOrdem + "' " +
                                  " AND PORDEMCONSUMO.SEQOP = '" + seqOrdem + "' " +
                                  " AND PORDEMCONSUMO.CODOPERACAO = '" + codOperacao + "' " +
                                  " AND PORDEMCONSUMO.SEQOPERACAO = '" + seqOperacao + "' " +
                                " GROUP BY PORDEMCONSUMO.CODEMPRESA, " +
                                       "  PORDEMCONSUMO.CODFILIAL, " +
                                       "  PORDEMCONSUMO.CODESTRUTURA, " +
                                       "  PORDEMCONSUMO.REVESTRUTURA, " +
                                       "  PORDEMCONSUMO.CODIGOOP, " +
                                       "  PORDEMCONSUMO.SEQOP, " +
                                       "  PORDEMCONSUMO.SEQOPERACAO, " +
                                       "  PORDEMCONSUMO.CODOPERACAO," +
                                       "  PORDEMCONSUMO.CODCOMPONENTE, " +
                                       "  VPRODUTO.NOME, " +
                                       "  VPRODUTO.CODUNIDCONTROLE, " +
                                       "  PORDEMCONSUMO.COMPONENTEORIGINAL, " +
                                       "  PORDEMCONSUMO.COMPONENTECANCELADO ";
                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                gridComponentes.DataSource = null;
                gridViewComponentes.Columns.Clear();

                dtRecursosComponentes = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                CarregaGrid2_Componentes(seqOperacao, codOperacao);

                DataSet DS = new DataSet();

                dtRecursosComponentes.TableName = "PORDEMCONSUMO";
                dt2RecursosComponentes.TableName = "PORDEMCONSUMO2";

                DS.Tables.Add(dtRecursosComponentes);
                DS.Tables.Add(dt2RecursosComponentes);

                DataColumn[] parent = new DataColumn[] { DS.Tables[0].Columns[0],
                                                         DS.Tables[0].Columns[1],
                                                         DS.Tables[0].Columns[2],
                                                         DS.Tables[0].Columns[3],
                                                         DS.Tables[0].Columns[4],
                                                         DS.Tables[0].Columns[5],
                                                         DS.Tables[0].Columns[6],
                                                         DS.Tables[0].Columns[7],
                                                         DS.Tables[0].Columns[8]};

                DataColumn[] child = new DataColumn[] { DS.Tables[1].Columns[0],
                                                         DS.Tables[1].Columns[1],
                                                         DS.Tables[1].Columns[2],
                                                         DS.Tables[1].Columns[3],
                                                         DS.Tables[1].Columns[4],
                                                         DS.Tables[1].Columns[5],
                                                         DS.Tables[1].Columns[6],
                                                         DS.Tables[1].Columns[7],
                                                         DS.Tables[1].Columns[8]};

                DS.Relations.Add("Detalhes", parent, child);

                gridComponentes.DataSource = DS.Tables[0];

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?)", new object[] { tabela, "VPRODUTO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewComponentes.Columns.Count; i++)
                {
                    //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewComponentes.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewComponentes.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }

                gridViewComponentes.Columns["PORDEMCONSUMO.CODEMPRESA"].Visible = false;
                gridViewComponentes.Columns["PORDEMCONSUMO.CODFILIAL"].Visible = false;
                gridViewComponentes.Columns["PORDEMCONSUMO.CODESTRUTURA"].Visible = false;
                gridViewComponentes.Columns["PORDEMCONSUMO.REVESTRUTURA"].Visible = false;
                gridViewComponentes.Columns["PORDEMCONSUMO.CODIGOOP"].Visible = false;
                gridViewComponentes.Columns["PORDEMCONSUMO.SEQOP"].Visible = false;
                gridViewComponentes.Columns["PORDEMCONSUMO.SEQOPERACAO"].Visible = false;
                gridViewComponentes.Columns["PORDEMCONSUMO.CODOPERACAO"].Visible = false;

                gridViewComponentes.BestFitColumns();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void CarregaGrid_Recursos(string seqOperacao, string codOperacao)
        {
            string tabela = "PORDEMALOCACAO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {

                string sql = @"SELECT   PORDEMALOCACAO.CODEMPRESA AS 'PORDEMALOCACAO.CODEMPRESA'
                                        ,PORDEMALOCACAO.CODFILIAL AS 'PORDEMALOCACAO.CODFILIAL'
                                        ,PORDEMALOCACAO.CODESTRUTURA AS 'PORDEMALOCACAO.CODESTRUTURA'
                                        ,PORDEMALOCACAO.REVESTRUTURA AS 'PORDEMALOCACAO.REVESTRUTURA'
		                                ,PORDEMALOCACAO.CODIGOOP AS 'PORDEMALOCACAO.CODIGOOP'
		                                ,PORDEMALOCACAO.SEQOP AS 'PORDEMALOCACAO.SEQOP'
		                                ,PORDEMALOCACAO.SEQOPERACAO AS 'PORDEMALOCACAO.SEQOPERACAO'
		                                ,PORDEMALOCACAO.CODOPERACAO AS 'PORDEMALOCACAO.CODOPERACAO'
		                                ,PORDEMALOCACAO.TIPORECURSO AS 'PORDEMALOCACAO.TIPORECURSO'
		                                ,PORDEMALOCACAO.CODGRUPORECURSO AS 'PORDEMALOCACAO.CODGRUPORECURSO'
		                                ,PGRUPORECURSO.DESCGRUPORECURSO AS 'PGRUPORECURSO.DESCGRUPORECURSO'
                                FROM PORDEMALOCACAO                   JOIN PGRUPORECURSO ON  PORDEMALOCACAO.CODEMPRESA = PGRUPORECURSO.CODEMPRESA
                                                                       AND PORDEMALOCACAO.CODFILIAL = PGRUPORECURSO.CODFILIAL
                                                                       AND PORDEMALOCACAO.CODGRUPORECURSO = PGRUPORECURSO.CODGRUPORECURSO
                                WHERE PORDEMALOCACAO.CODEMPRESA = " + AppLib.Context.Empresa +
                                  " AND PORDEMALOCACAO.CODFILIAL = " + AppLib.Context.Filial +
                                  " AND PORDEMALOCACAO.CODESTRUTURA = '" + codEstrutura + "' " +
                                  " AND PORDEMALOCACAO.REVESTRUTURA = '" + CodRevEstrutura + "' " +
                                  " AND PORDEMALOCACAO.CODIGOOP = '" + codOrdem + "' " +
                                  " AND PORDEMALOCACAO.SEQOP = '" + seqOrdem + "' " +
                                  " AND PORDEMALOCACAO.CODOPERACAO = '" + codOperacao + "' " +
                                  " AND PORDEMALOCACAO.SEQOPERACAO = '" + seqOperacao + "' " +
                                  " GROUP BY PORDEMALOCACAO.CODEMPRESA " +
                                  "         ,PORDEMALOCACAO.CODFILIAL " +
                                  "         ,PORDEMALOCACAO.CODESTRUTURA " +
                                  "         ,PORDEMALOCACAO.REVESTRUTURA" +
                                  "         ,PORDEMALOCACAO.CODIGOOP" +
                                  "         ,PORDEMALOCACAO.SEQOP" +
                                  "         ,PORDEMALOCACAO.CODOPERACAO" +
                                  "         ,PORDEMALOCACAO.SEQOPERACAO" +
                                  "         ,PORDEMALOCACAO.TIPORECURSO" +
                                  "         ,PORDEMALOCACAO.CODGRUPORECURSO" +
                                  "         ,PGRUPORECURSO.DESCGRUPORECURSO" +
                             " ORDER BY PORDEMALOCACAO.TIPORECURSO,PORDEMALOCACAO.CODGRUPORECURSO";

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                gridRecursosRecursos.DataSource = null;
                gridViewRecursosRecursos.Columns.Clear();

                dtRecursosRecursos = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                CarregaGrid2_Recursos(seqOperacao, codOperacao);

                DataSet DS = new DataSet();

                dtRecursosRecursos.TableName = "PORDEMALOCACAO";
                dt2RecursosRecursos.TableName = "PORDEMALOCACAO2";

                DS.Tables.Add(dtRecursosRecursos);
                DS.Tables.Add(dt2RecursosRecursos);

                DataColumn[] parent = new DataColumn[] { DS.Tables[0].Columns[0],
                                                         DS.Tables[0].Columns[1],
                                                         DS.Tables[0].Columns[2],
                                                         DS.Tables[0].Columns[3],
                                                         DS.Tables[0].Columns[4],
                                                         DS.Tables[0].Columns[5],
                                                         DS.Tables[0].Columns[6],
                                                         DS.Tables[0].Columns[7],
                                                         DS.Tables[0].Columns[8],
                                                         DS.Tables[0].Columns[9]};

                DataColumn[] child = new DataColumn[] {  DS.Tables[1].Columns[0],
                                                         DS.Tables[1].Columns[1],
                                                         DS.Tables[1].Columns[2],
                                                         DS.Tables[1].Columns[3],
                                                         DS.Tables[1].Columns[4],
                                                         DS.Tables[1].Columns[5],
                                                         DS.Tables[1].Columns[6],
                                                         DS.Tables[1].Columns[7],
                                                         DS.Tables[1].Columns[8],
                                                         DS.Tables[1].Columns[9]};

                DS.Relations.Add("Detalhes", parent, child);

                gridRecursosRecursos.DataSource = DS.Tables[0];

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?,?)", new object[] { tabela, "PRECURSO", "PGRUPORECURSO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewRecursosRecursos.Columns.Count; i++)
                {
                    //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewRecursosRecursos.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewRecursosRecursos.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewRecursosRecursos.Columns["PORDEMALOCACAO.CODEMPRESA"].Visible = false;

                gridViewRecursosRecursos.Columns["PORDEMALOCACAO.CODFILIAL"].Visible = false;
                gridViewRecursosRecursos.Columns["PORDEMALOCACAO.CODESTRUTURA"].Visible = false;
                gridViewRecursosRecursos.Columns["PORDEMALOCACAO.REVESTRUTURA"].Visible = false;
                gridViewRecursosRecursos.Columns["PORDEMALOCACAO.CODIGOOP"].Visible = false;
                gridViewRecursosRecursos.Columns["PORDEMALOCACAO.SEQOP"].Visible = false;

                gridViewRecursosRecursos.Columns["PORDEMALOCACAO.SEQOPERACAO"].Visible = false;
                gridViewRecursosRecursos.Columns["PORDEMALOCACAO.CODOPERACAO"].Visible = false;

                gridViewRecursosRecursos.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregaGrid1_Compra(string seqOperacao, string codOperacao)
        {
            string tabela = "PORDEMCOMPRA";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                //DataRow row1 = gridViewOperacao.GetDataRow(Convert.ToInt32(gridViewOperacao.GetSelectedRows().GetValue(0).ToString()));

                //string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                string sql = @"SELECT	PORDEMCOMPRA.SEQOP AS 'Seq.',
		                                PORDEMCOMPRA.CODOPER AS 'ID',
		                                PORDEMCOMPRA.TIPOCOMPRA AS 'Tipo',
		                                PORDEMCOMPRA.DATASOLICITACAO AS 'Data Solic.',
		                                PORDEMCOMPRA.CODCOMPONENTE AS 'Cód. Comp.',
		                                VPRODUTO.NOME AS 'Desc. Componente',
		                                PORDEMCOMPRA.QTDCOMPONENTE AS 'Qtde.',
		                                VPRODUTO.CODUNIDCOMPRA AS 'UM',
		                                PORDEMCOMPRA.IDCOMPRAOP AS 'Núm. Solicit.',
		                                PORDEMCOMPRA.DATARETORNOSOL AS 'Data Retorno'
                                  FROM PORDEMCOMPRA JOIN VPRODUTO ON PORDEMCOMPRA.CODEMPRESA = VPRODUTO.CODEMPRESA
								                                  AND PORDEMCOMPRA.CODCOMPONENTE = VPRODUTO.CODPRODUTO
                                 WHERE  PORDEMCOMPRA.CODEMPRESA = " + AppLib.Context.Empresa +
                                  " AND PORDEMCOMPRA.CODFILIAL = " + AppLib.Context.Filial +
                                  " AND PORDEMCOMPRA.CODESTRUTURA = '" + codEstrutura + "' " +
                                  " AND PORDEMCOMPRA.REVESTRUTURA = '" + CodRevEstrutura + "' " +
                                  " AND PORDEMCOMPRA.CODIGOOP = '" + codOrdem + "' " +
                                  " AND PORDEMCOMPRA.SEQOP = '" + seqOrdem + "' " +
                                  " AND PORDEMCOMPRA.CODOPERACAO = '" + codOperacao + "' " +
                                  " AND PORDEMCOMPRA.SEQOPERACAO = '" + seqOperacao + "' ";

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                gridCompras.DataSource = null;
                gridViewCompras.Columns.Clear();

                dt1Compra = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);
                gridCompras.DataSource = dt1Compra;

                gridViewCompras.BestFitColumns();

                CarregaGrid2_Compra(seqOperacao, codOperacao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregaGrid2_Compra(string seqOperacao, string codOperacao)
        {
            string tabela = "PORDEMENTRADA";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql = @" SELECT  PORDEMCOMPRAAPROV.DATAAPROVACAO AS 'Data Aprovação',
		                                PORDEMCOMPRAAPROV.QTDAPROVADA AS 'Qtde.',
		                                PORDEMCOMPRAAPROV.DATARETORNOPLAN AS 'Data Retorno Planejado',
		                                PORDEMCOMPRAAPROV.IDCOMPRAOP AS 'Número Pedido',
		                                PORDEMCOMPRAAPROV.NSEQITEM AS 'Nseq'
                                 FROM	PORDEMCOMPRAAPROV 
                                WHERE   PORDEMCOMPRAAPROV.CODEMPRESA = " + AppLib.Context.Empresa +
                                  " AND PORDEMCOMPRAAPROV.CODFILIAL = " + AppLib.Context.Filial +
                                  " AND PORDEMCOMPRAAPROV.CODESTRUTURA = '" + codEstrutura + "' " +
                                  " AND PORDEMCOMPRAAPROV.REVESTRUTURA = '" + CodRevEstrutura + "' " +
                                  " AND PORDEMCOMPRAAPROV.CODIGOOP = '" + codOrdem + "' " +
                                  " AND PORDEMCOMPRAAPROV.SEQOP = '" + seqOrdem + "' " +
                                  " AND PORDEMCOMPRAAPROV.CODOPERACAO = '" + codOperacao + "' " +
                                  " AND PORDEMCOMPRAAPROV.SEQOPERACAO = '" + seqOperacao + "' ";

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                gridComprasPedido.DataSource = null;
                gridViewComprasPedido.Columns.Clear();

                dt2Entrada = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);
                gridComprasPedido.DataSource = dt2Entrada;

                gridViewComprasPedido.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregaGrid1_Entrada(string seqOperacao, string codOperacao)
        {
            string tabela = "PORDEMENTRADA";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                //DataRow row1 = gridViewOperacao.GetDataRow(Convert.ToInt32(gridViewOperacao.GetSelectedRows().GetValue(0).ToString()));

                //string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                string sql = @"SELECT   PORDEMENTRADA.SEQOPERACAO AS 'Seq. Op',
		                                PORDEMENTRADA.TIPOENTRADA AS 'Tipo',
		                                PORDEMENTRADA.CODPRODUTO AS 'Código',
		                                VPRODUTO.NOME AS 'Descrição',
		                                null AS 'Planejado',
		                                PORDEMENTRADA.UNDPRODUTO AS 'Unidade de Medida',
		                                SUM(PORDEMENTRADA.QTDENTRADA) AS 'Entrada'
                                FROM    PORDEMENTRADA JOIN VPRODUTO ON PORDEMENTRADA.CODEMPRESA = VPRODUTO.CODEMPRESA
								                                  AND PORDEMENTRADA.CODPRODUTO = VPRODUTO.CODPRODUTO
                               WHERE    PORDEMENTRADA.CODEMPRESA = " + AppLib.Context.Empresa +
                                  " AND PORDEMENTRADA.CODFILIAL = " + AppLib.Context.Filial +
                                  " AND PORDEMENTRADA.CODESTRUTURA = '" + codEstrutura + "' " +
                                  " AND PORDEMENTRADA.REVESTRUTURA = '" + CodRevEstrutura + "' " +
                                  " AND PORDEMENTRADA.CODIGOOP = '" + codOrdem + "' " +
                                  " AND PORDEMENTRADA.SEQOP = '" + seqOrdem + "' " +
                                  " AND PORDEMENTRADA.CODOPERACAO = '" + codOperacao + "' " +
                                  " AND PORDEMENTRADA.SEQOPERACAO = '" + seqOperacao + "' " +
                            @"GROUP BY  PORDEMENTRADA.SEQOPERACAO,
		                                  PORDEMENTRADA.TIPOENTRADA,
		                                  PORDEMENTRADA.CODPRODUTO,
		                                  VPRODUTO.NOME,
		                                  PORDEMENTRADA.UNDPRODUTO";

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                gridEntradas.DataSource = null;
                gridViewEntradas.Columns.Clear();

                dt1Entrada = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);
                gridEntradas.DataSource = dt1Entrada;

                gridViewEntradas.BestFitColumns();

                CarregaGrid2_Entrada(seqOperacao, codOperacao);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregaGrid2_Entrada(string seqOperacao, string codOperacao)
        {
            string tabela = "PORDEMENTRADA";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql = @" SELECT   PORDEMENTRADA.DATAMOVIMENTO AS 'Data',
 		                                 PORDEMENTRADA.QTDENTRADA AS 'Quantidade',
		                                 PORDEMENTRADA.SEQOPERACAO AS 'Seq.',
		                                 PORDEMENTRADA.SEQAPO AS 'Apont.'
                                   FROM  PORDEMENTRADA
                                  WHERE  PORDEMENTRADA.CODEMPRESA = " + AppLib.Context.Empresa +
                                  " AND  PORDEMENTRADA.CODFILIAL = " + AppLib.Context.Filial +
                                  " AND  PORDEMENTRADA.CODESTRUTURA = '" + codEstrutura + "' " +
                                  " AND  PORDEMENTRADA.REVESTRUTURA = '" + CodRevEstrutura + "' " +
                                  " AND  PORDEMENTRADA.CODIGOOP = '" + codOrdem + "' " +
                                  " AND  PORDEMENTRADA.SEQOP = '" + seqOrdem + "' " +
                                  " AND  PORDEMENTRADA.CODOPERACAO = '" + codOperacao + "' " +
                                  " AND  PORDEMENTRADA.SEQOPERACAO = '" + seqOperacao + "' ";

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                gridEntradasApontamentos.DataSource = null;
                gridViewEntradasApontamentos.Columns.Clear();

                dt2Entrada = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);
                gridEntradasApontamentos.DataSource = dt2Entrada;

                gridViewEntradasApontamentos.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable CarregaGrid2_Componentes(string seqOperacao, string codOperacao)
        {
            string tabela = "PORDEMCONSUMO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql = @"SELECT    PORDEMCONSUMO.CODEMPRESA AS 'PORDEMCONSUMO.CODEMPRESA',
				                         PORDEMCONSUMO.CODFILIAL AS 'PORDEMCONSUMO.CODFILIAL',
				                         PORDEMCONSUMO.CODESTRUTURA AS 'PORDEMCONSUMO.CODESTRUTURA',
				                         PORDEMCONSUMO.REVESTRUTURA AS 'PORDEMCONSUMO.REVESTRUTURA',
				                         PORDEMCONSUMO.CODIGOOP AS 'PORDEMCONSUMO.CODIGOOP',
				                         PORDEMCONSUMO.SEQOP AS 'PORDEMCONSUMO.SEQOP',
				                         PORDEMCONSUMO.SEQOPERACAO AS 'PORDEMCONSUMO.SEQOPERACAO',
				                         PORDEMCONSUMO.CODOPERACAO AS 'PORDEMCONSUMO.CODOPERACAO',
                                         PORDEMCONSUMO.CODCOMPONENTE AS 'PORDEMCONSUMO.CODCOMPONENTE',
                                         CASE PORDEMCONSUMO.TIPOCONSUMO 
			                                 WHEN 'R' THEN 'Reserva'
			                                 WHEN 'B' THEN 'Baixa'
			                                 WHEN 'N' THEN 'Necessidade'
			                             END AS 'PORDEMCONSUMO.TIPOCONSUMO',
				                         PORDEMCONSUMO.QTDCOMPONENTE AS 'PORDEMCONSUMO.QTDCOMPONENTE',
                                         PORDEMCONSUMO.UNDCOMPONENTE AS 'PORDEMCONSUMO.UNDCOMPONENTE',
                                         PORDEMCONSUMO.CODOPER AS 'PORDEMCONSUMO.CODOPER' ,
                                         PORDEMCONSUMO.NSEQITEM AS 'PORDEMCONSUMO.NSEQITEM'
		                        FROM PORDEMCONSUMO JOIN VPRODUTO ON PORDEMCONSUMO.CODCOMPONENTE = VPRODUTO.CODPRODUTO
										                        AND PORDEMCONSUMO.CODEMPRESA = VPRODUTO.CODEMPRESA
                               WHERE PORDEMCONSUMO.CODEMPRESA = " + AppLib.Context.Empresa +
                                  " AND PORDEMCONSUMO.CODFILIAL = " + AppLib.Context.Filial +
                                  " AND PORDEMCONSUMO.CODESTRUTURA = '" + codEstrutura + "' " +
                                  " AND PORDEMCONSUMO.REVESTRUTURA = '" + CodRevEstrutura + "' " +
                                  " AND PORDEMCONSUMO.CODIGOOP = '" + codOrdem + "' " +
                                  " AND PORDEMCONSUMO.SEQOP = '" + seqOrdem + "' " +
                                  " AND PORDEMCONSUMO.CODOPERACAO = '" + codOperacao + "' " +
                                  " AND PORDEMCONSUMO.SEQOPERACAO = '" + seqOperacao + "' ";
                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                dt2RecursosComponentes = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                return dt2RecursosComponentes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private DataTable CarregaGrid2_Apontamento(string seqOperacao, string codOperacao)
        {
            string tabela = "PORDEMAPTORECURSO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql1 = @"SELECT  CASE PORDEMAPTORECURSO.TIPORECURSO 
			                             WHEN 'EQ' THEN 'Equipamento'
			                             WHEN 'MO' THEN 'Mão de Obra'
			                             WHEN 'FE' THEN 'Ferramenta'
			                             END
			                             AS 'PORDEMAPTORECURSO.TIPORECURSO'
			                             ,PRECURSO.CODGRUPORECURSO AS 'PRECURSO.CODGRUPORECURSO'
			                            ,PGRUPORECURSO.DESCGRUPORECURSO AS 'PGRUPORECURSO.DESCGRUPORECURSO'
			                            ,PORDEMAPTORECURSO.CODRECURSO AS 'PORDEMAPTORECURSO.CODRECURSO'
			                            ,PRECURSO.DESCRECURSO AS 'PRECURSO.DESCRECURSO'
			                            ,null AS 'PORDEMENTRADA.QTDENTRADA'
			                            ,null AS 'PORDEMENTRADA.UNDPRODUTO'
                              FROM PORDEMAPTORECURSO JOIN PRECURSO ON PORDEMAPTORECURSO.CODEMPRESA = PRECURSO.CODEMPRESA
									  AND PORDEMAPTORECURSO.CODFILIAL = PRECURSO.CODFILIAL
									  AND PORDEMAPTORECURSO.CODRECURSO = PRECURSO.CODRECURSO
					     JOIN PGRUPORECURSO ON PGRUPORECURSO.CODEMPRESA = PRECURSO.CODEMPRESA
									  AND PGRUPORECURSO.CODFILIAL = PRECURSO.CODFILIAL
									  AND PGRUPORECURSO.CODGRUPORECURSO = PRECURSO.CODGRUPORECURSO 
                               WHERE PORDEMAPTORECURSO.CODEMPRESA = " + AppLib.Context.Empresa +
                               " AND PORDEMAPTORECURSO.CODFILIAL = " + AppLib.Context.Filial +
                               " AND PORDEMAPTORECURSO.CODESTRUTURA = '" + codEstrutura + "' " +
                               " AND PORDEMAPTORECURSO.REVESTRUTURA = '" + CodRevEstrutura + "' " +
                               " AND PORDEMAPTORECURSO.CODIGOOP = '" + codOrdem + "' " +
                               " AND PORDEMAPTORECURSO.SEQOP = '" + seqOrdem + "' " +
                               " AND PORDEMAPTORECURSO.CODOPERACAO = '" + codOperacao + "' " +
                               " AND PORDEMAPTORECURSO.SEQOPERACAO = '" + seqOperacao + "' ";

                string sql2 = @"SELECT      CASE PORDEMENTRADA.TIPOENTRADA 
			                                 WHEN 1 THEN 'Produto Acabado'
			                                 WHEN 2 THEN 'Produto Intermediário'
			                                 WHEN 3 THEN 'Refugo'
			                                 WHEN 4 THEN 'Devolução'
			                                 END
			                                ,null AS 'PRECURSO.CODGRUPORECURSO'
			                                ,null AS 'PGRUPORECURSO.DESCGRUPORECURSO'
			                                ,PORDEMENTRADA.CODPRODUTO AS 'PORDEMAPTORECURSO.CODRECURSO'
			                                ,VPRODUTO.NOME AS 'PRECURSO.DESCRECURSO'
			                                ,PORDEMENTRADA.QTDENTRADA AS 'PORDEMENTRADA.QTDENTRADA'
			                                ,PORDEMENTRADA.UNDPRODUTO AS 'PORDEMENTRADA.UNDPRODUTO'
                                  FROM PORDEMENTRADA JOIN VPRODUTO ON PORDEMENTRADA.CODEMPRESA = VPRODUTO.CODEMPRESA
									                                  AND PORDEMENTRADA.CODPRODUTO = VPRODUTO.CODPRODUTO
                                 WHERE PORDEMENTRADA.CODEMPRESA = " + AppLib.Context.Empresa +
                                 " AND PORDEMENTRADA.CODFILIAL = " + AppLib.Context.Filial +
                                 " AND PORDEMENTRADA.CODESTRUTURA = '" + codEstrutura + "' " +
                                 " AND PORDEMENTRADA.REVESTRUTURA = '" + CodRevEstrutura + "' " +
                                 " AND PORDEMENTRADA.CODIGOOP = '" + codOrdem + "' " +
                                 " AND PORDEMENTRADA.SEQOP = '" + seqOrdem + "' " +
                                 " AND PORDEMENTRADA.CODOPERACAO = '" + codOperacao + "' " +
                                 " AND PORDEMENTRADA.SEQOPERACAO = '" + seqOperacao + "' ";


                string sql3 = @"SELECT      CASE PORDEMCONSUMO.TIPOCONSUMO 
			                                 WHEN 'R' THEN 'Reserva'
			                                 WHEN 'B' THEN 'Baixa'
			                                 WHEN 'N' THEN 'Necessidade'
			                                 END
			                                ,null AS 'PRECURSO.CODGRUPORECURSO'
			                                ,null AS 'PGRUPORECURSO.DESCGRUPORECURSO'
			                                ,PORDEMCONSUMO.CODCOMPONENTE AS 'PORDEMAPTORECURSO.CODRECURSO'
			                                ,VPRODUTO.NOME AS 'PRECURSO.DESCRECURSO'
			                                ,PORDEMCONSUMO.QTDCOMPONENTE AS 'PORDEMENTRADA.QTDENTRADA'
			                                ,PORDEMCONSUMO.UNDCOMPONENTE AS 'PORDEMENTRADA.UNDPRODUTO'
                                  FROM PORDEMCONSUMO JOIN VPRODUTO ON PORDEMCONSUMO.CODEMPRESA = VPRODUTO.CODEMPRESA
									                                  AND PORDEMCONSUMO.CODCOMPONENTE = VPRODUTO.CODPRODUTO
                                 WHERE PORDEMCONSUMO.CODEMPRESA = " + AppLib.Context.Empresa +
                                 " AND PORDEMCONSUMO.CODFILIAL = " + AppLib.Context.Filial +
                                 " AND PORDEMCONSUMO.CODESTRUTURA = '" + codEstrutura + "' " +
                                 " AND PORDEMCONSUMO.REVESTRUTURA = '" + CodRevEstrutura + "' " +
                                 " AND PORDEMCONSUMO.CODIGOOP = '" + codOrdem + "' " +
                                 " AND PORDEMCONSUMO.SEQOP = '" + seqOrdem + "' " +
                                 " AND PORDEMCONSUMO.CODOPERACAO = '" + codOperacao + "' " +
                                 " AND PORDEMCONSUMO.SEQOPERACAO = '" + seqOperacao + "' ";

                string sqlCompleto = "";

                sqlCompleto = string.Concat(sql1, " UNION ALL ", sql2, " UNION ALL ", sql3);

                gridApontamentosRecursos.DataSource = null;
                gridViewApontamentosRecursos.Columns.Clear();

                dt2Apontamento = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqlCompleto);
                gridApontamentosRecursos.DataSource = dt2Apontamento;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?,?,?,?,?)", new object[] { tabela, "PRECURSO", "PGRUPORECURSO", "VPRODUTO", "PORDEMCONSUMO", "PORDEMENTRADA" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewApontamentosRecursos.Columns.Count; i++)
                {
                    //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewApontamentosRecursos.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewApontamentosRecursos.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewApontamentosRecursos.BestFitColumns();

                return dt2Apontamento;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        private DataTable CarregaGrid1_Apontamento(string seqOperacao, string codOperacao)
        {
            string tabela = "PORDEMAPONTAMENTO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                //DataRow row1 = gridViewOperacao.GetDataRow(Convert.ToInt32(gridViewOperacao.GetSelectedRows().GetValue(0).ToString()));

                //string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                //lookupoperacao.txtcodigo.Text = dt.Rows[0]["CODOPERACAO"].ToString();

                string sql = @"  SELECT  PORDEMAPONTAMENTO.SEQOPERACAO AS 'PORDEMAPONTAMENTO.SEQOPERACAO'
		                                ,PORDEMAPONTAMENTO.ID AS 'PORDEMAPONTAMENTO.ID'
		                                ,PORDEMAPONTAMENTO.DATAAPO AS 'PORDEMAPONTAMENTO.DATAAPO'
                                        ,PORDEMAPONTAMENTO.QTDEAPO as 'PORDEMAPONTAMENTO.QTDEAPO'
                                        ,PORDEMAPONTAMENTO.HRINICIOSETUP as 'PORDEMAPONTAMENTO.HRINICIOSETUP'
                                        ,PORDEMAPONTAMENTO.HRFIMSETUP as 'PORDEMAPONTAMENTO.HRFIMSETUP'
                                        ,PORDEMAPONTAMENTO.TEMPOSETUP as 'PORDEMAPONTAMENTO.TEMPOSETUP'
                                        ,PORDEMAPONTAMENTO.HRINICIOTRABALHO as 'PORDEMAPONTAMENTO.HRINICIOTRABALHO'
                                        ,PORDEMAPONTAMENTO.HRFIMTRABALHO as 'PORDEMAPONTAMENTO.HRFIMTRABALHO'
                                        ,PORDEMAPONTAMENTO.TEMPOOPERACAO as 'PORDEMAPONTAMENTO.TEMPOOPERACAO'
                                        ,PORDEMAPONTAMENTO.HRINICIOEXTRA as 'PORDEMAPONTAMENTO.HRINICIOEXTRA'
                                        ,PORDEMAPONTAMENTO.HRFIMEXTRA as 'PORDEMAPONTAMENTO.HRFIMEXTRA'
                                        ,PORDEMAPONTAMENTO.TEMPOEXTRA as 'PORDEMAPONTAMENTO.TEMPOEXTRA'
                                        ,PORDEMAPONTAMENTO.TEMPOTOTAL as 'PORDEMAPONTAMENTO.TEMPOTOTAL'                               
		                                ,PORDEMAPONTAMENTO.USUARIOAPO AS 'PORDEMAPONTAMENTO.USUARIOAPO'
                                  FROM PORDEMAPONTAMENTO  
                                 WHERE PORDEMAPONTAMENTO.CODEMPRESA = " + AppLib.Context.Empresa +
                             " AND PORDEMAPONTAMENTO.CODFILIAL = " + AppLib.Context.Filial +
                             " AND PORDEMAPONTAMENTO.CODESTRUTURA = '" + codEstrutura + "' " +
                             " AND PORDEMAPONTAMENTO.REVESTRUTURA = '" + CodRevEstrutura + "' " +
                             " AND PORDEMAPONTAMENTO.CODIGOOP = '" + codOrdem + "' " +
                             " AND PORDEMAPONTAMENTO.SEQOP = '" + seqOrdem + "' " +
                             " AND PORDEMAPONTAMENTO.SEQOPERACAO = '" + seqOperacao + "' ";

                //                             " AND PORDEMAPONTAMENTO.CODOPERACAO = '" + codOperacao + "' " +

                //DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { });

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }



                gridApontamentos.DataSource = null;
                gridViewApontamentos.Columns.Clear();

                dt1Apontamento = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);
                gridApontamentos.DataSource = dt1Apontamento;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?)", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewApontamentos.Columns.Count; i++)
                {
                    //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewApontamentos.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewApontamentos.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewApontamentos.BestFitColumns();

                CarregaGrid2_Apontamento(seqOperacao, codOperacao);

                return dt1Apontamento;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private DataTable CarregaGrid2_Recursos(string seqOperacao, string codOperacao)
        {
            //tipoRecurso = 0 = Todos

            string tabela = "PORDEMALOCACAO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql = @"SELECT    PORDEMALOCACAO.CODEMPRESA AS 'PORDEMALOCACAO.CODEMPRESA'
                                        ,PORDEMALOCACAO.CODFILIAL AS 'PORDEMALOCACAO.CODFILIAL'
                                        ,PORDEMALOCACAO.CODESTRUTURA AS 'PORDEMALOCACAO.CODESTRUTURA'
                                        ,PORDEMALOCACAO.REVESTRUTURA AS 'PORDEMALOCACAO.REVESTRUTURA'
		                                ,PORDEMALOCACAO.CODIGOOP AS 'PORDEMALOCACAO.CODIGOOP'
		                                ,PORDEMALOCACAO.SEQOP AS 'PORDEMALOCACAO.SEQOP'
		                                ,PORDEMALOCACAO.SEQOPERACAO AS 'PORDEMALOCACAO.SEQOPERACAO'
		                                ,PORDEMALOCACAO.CODOPERACAO AS 'PORDEMALOCACAO.CODOPERACAO'
		                                ,PORDEMALOCACAO.TIPORECURSO AS 'PORDEMALOCACAO.TIPORECURSO'
		                                ,PORDEMALOCACAO.CODGRUPORECURSO AS 'PORDEMALOCACAO.CODGRUPORECURSO'
                                        ,PORDEMALOCACAO.DATAALOCACAO AS 'PORDEMALOCACAO.DATAALOCACAO'
                                        --,PORDEMALOCACAO.TEMPOALOCACAO AS 'PORDEMALOCACAO.TEMPOALOCACAO'
                                        ,RIGHT('00' + CONVERT(varchar,(CONVERT(int,PORDEMALOCACAO.TEMPOALOCACAO) /60)),2)+':'+RIGHT('00' + CONVERT(varchar,(CONVERT(int,PORDEMALOCACAO.TEMPOALOCACAO) % 60)),2) AS 'PORDEMALOCACAO.TEMPOALOCACAO_HORA'
                                        ,PORDEMALOCACAO.CODRECURSO AS 'PORDEMALOCACAO.CODRECURSO'
                                        ,PORDEMALOCACAO.TIPOALOC AS 'PORDEMALOCACAO.TIPOALOC'
                                FROM PORDEMALOCACAO  JOIN PGRUPORECURSO ON PORDEMALOCACAO.CODEMPRESA = PGRUPORECURSO.CODEMPRESA
                                                                       AND PORDEMALOCACAO.CODFILIAL = PGRUPORECURSO.CODFILIAL
                                                                       AND PORDEMALOCACAO.CODGRUPORECURSO = PGRUPORECURSO.CODGRUPORECURSO
                                WHERE PORDEMALOCACAO.CODEMPRESA = " + AppLib.Context.Empresa +
                                  " AND PORDEMALOCACAO.CODFILIAL = " + AppLib.Context.Filial +
                                  " AND PORDEMALOCACAO.CODESTRUTURA = '" + codEstrutura + "' " +
                                  " AND PORDEMALOCACAO.REVESTRUTURA = '" + CodRevEstrutura + "' " +
                                  " AND PORDEMALOCACAO.CODIGOOP = '" + codOrdem + "' " +
                                  " AND PORDEMALOCACAO.SEQOP = '" + seqOrdem + "' " +
                                  " AND PORDEMALOCACAO.CODOPERACAO = '" + codOperacao + "' " +
                                  " AND PORDEMALOCACAO.SEQOPERACAO = '" + seqOperacao + "' ";

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                dt2RecursosRecursos = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                return dt2RecursosRecursos;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        void CarregaGridOperacoes()
        {
            DataTable dt = new DataTable();
            dt.Clear();

            dt.Columns.Add("Seq");
            dt.Columns.Add("Operação");

            DataRow row = dt.NewRow();
            row["Seq"] = "OP";
            row["Operação"] = "";
        }

        void CarregaCombos()
        {
            List<PS.Lib.ComboBoxItem> listStatus = new List<PS.Lib.ComboBoxItem>();

            listStatus.Add(new PS.Lib.ComboBoxItem());
            listStatus[0].ValueMember = "0";
            listStatus[0].DisplayMember = "";

            listStatus.Add(new PS.Lib.ComboBoxItem());
            listStatus[1].ValueMember = "1";
            listStatus[1].DisplayMember = "1 - Aguardando Liberação";

            listStatus.Add(new PS.Lib.ComboBoxItem());
            listStatus[2].ValueMember = "2";
            listStatus[2].DisplayMember = "2 - Planejada";

            listStatus.Add(new PS.Lib.ComboBoxItem());
            listStatus[3].ValueMember = "3";
            listStatus[3].DisplayMember = "3 - Em Produção";

            listStatus.Add(new PS.Lib.ComboBoxItem());
            listStatus[4].ValueMember = "4";
            listStatus[4].DisplayMember = "4 - Paralisada";

            listStatus.Add(new PS.Lib.ComboBoxItem());
            listStatus[5].ValueMember = "5";
            listStatus[5].DisplayMember = "5 - Cancelada";

            listStatus.Add(new PS.Lib.ComboBoxItem());
            listStatus[6].ValueMember = "6";
            listStatus[6].DisplayMember = "6 - Concluída";

            cmbStatus.DataSource = listStatus;
            cmbStatus.DisplayMember = "DisplayMember";
            cmbStatus.ValueMember = "ValueMember";

            cmbStatus.SelectedIndex = -1;

            List<PS.Lib.ComboBoxItem> listTipo = new List<PS.Lib.ComboBoxItem>();

            listTipo.Add(new PS.Lib.ComboBoxItem());
            listTipo[0].ValueMember = "0";
            listTipo[0].DisplayMember = "";

            listTipo.Add(new PS.Lib.ComboBoxItem());
            listTipo[1].ValueMember = "A";
            listTipo[1].DisplayMember = "A - Acabado";

            listTipo.Add(new PS.Lib.ComboBoxItem());
            listTipo[2].ValueMember = "S";
            listTipo[2].DisplayMember = "S - Semi-Acabado";

            cmbTipo.DataSource = listTipo;
            cmbTipo.DisplayMember = "DisplayMember";
            cmbTipo.ValueMember = "ValueMember";

            cmbTipo.SelectedIndex = -1;

            List<PS.Lib.ComboBoxItem> listStatusRoteiro = new List<PS.Lib.ComboBoxItem>();

            listStatusRoteiro.Add(new PS.Lib.ComboBoxItem());
            listStatusRoteiro[0].ValueMember = "0";
            listStatusRoteiro[0].DisplayMember = "";

            listStatusRoteiro.Add(new PS.Lib.ComboBoxItem());
            listStatusRoteiro[1].ValueMember = "1";
            listStatusRoteiro[1].DisplayMember = "1 - Aguardando Liberação";

            listStatusRoteiro.Add(new PS.Lib.ComboBoxItem());
            listStatusRoteiro[2].ValueMember = "2";
            listStatusRoteiro[2].DisplayMember = "2 - Planejada";

            listStatusRoteiro.Add(new PS.Lib.ComboBoxItem());
            listStatusRoteiro[3].ValueMember = "3";
            listStatusRoteiro[3].DisplayMember = "3 - Aguardando Envio a Terceiro";

            listStatusRoteiro.Add(new PS.Lib.ComboBoxItem());
            listStatusRoteiro[4].ValueMember = "4";
            listStatusRoteiro[4].DisplayMember = "4 - Apontamento Parcial";

            listStatusRoteiro.Add(new PS.Lib.ComboBoxItem());
            listStatusRoteiro[5].ValueMember = "5";
            listStatusRoteiro[5].DisplayMember = "5 - Enviado a Terceiro";

            listStatusRoteiro.Add(new PS.Lib.ComboBoxItem());
            listStatusRoteiro[6].ValueMember = "6";
            listStatusRoteiro[6].DisplayMember = "6 - Paralisada";

            listStatusRoteiro.Add(new PS.Lib.ComboBoxItem());
            listStatusRoteiro[7].ValueMember = "7";
            listStatusRoteiro[7].DisplayMember = "7 - Cancelada";

            listStatusRoteiro.Add(new PS.Lib.ComboBoxItem());
            listStatusRoteiro[8].ValueMember = "8";
            listStatusRoteiro[8].DisplayMember = "8 - Concluída";

            cmbRoteiroStatus.DataSource = listStatusRoteiro;
            cmbRoteiroStatus.DisplayMember = "DisplayMember";
            cmbRoteiroStatus.ValueMember = "ValueMember";

            cmbRoteiroStatus.SelectedIndex = -1;
        }

        private void FrmOrdemProducao_Load(object sender, EventArgs e)
        {
            CarregaCombos();
            tabRecursoOperacao.TabPages.Remove(tabRecursoOperacao.TabPages["tabCustos"]);

            if (edita == true)
            {
                carregaCampos();
            }
            else
            {
                btnNovo.PerformClick();
            }
        }

        private bool validacao()
        {
            bool verifica = true;

            errorProvider.Clear();

            lookupEstrutura.mensagemErrorProvider = "";
            lookupunidade.mensagemErrorProvider = "";

            if (string.IsNullOrEmpty(lookupEstrutura.txtconteudo.Text))
            {
                lookupEstrutura.mensagemErrorProvider = "Favor Selecionar uma Estrutura";
                verifica = false;
            }
            else
            {
                lookupEstrutura.mensagemErrorProvider = "";
            }

            if (string.IsNullOrEmpty(txtDescricao.Text))
            {
                errorProvider.SetError(txtDescricao, "Favor preencher a Descrição");
                verifica = false;
            }

            if (string.IsNullOrEmpty(txtQtPlan.Text))
            {
                errorProvider.SetError(txtQtPlan, "Favor preencher a Quantidade Planejada");
                verifica = false;
            }
            else
            {
                if (Convert.ToDecimal(txtQtPlan.Text) <= 0)
                {
                    errorProvider.SetError(txtQtPlan, "A Quantidade Planejada deve ser maior que zero");
                    verifica = false;
                }
            }

            return verifica;
        }

        private void labelControl35_Click(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            //lookupEstrutura.OutrasChaves.Contains(new Newlookup_OutrasChaves { NomeColunaChave = "REVESTRUTURA" });
            codOrdem = txtNroOp.Text;
            if (Salvar() == true)
            {
                if (this.lookup == null)
                {
                    MessageBox.Show("Cadastro salvo com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    codOrdem = txtNroOp.Text;
                    seqOrdem = txtSeqOp.Text;

                    carregaCampos();
                }
                else
                {
                    codOrdem = txtNroOp.Text;
                    seqOrdem = txtSeqOp.Text;

                    carregaCampos();

                    lookup.txtcodigo.Text = txtNroOp.Text;
                    lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();

                    this.Dispose();
                }
            }
        }

        private void GravaNovaOP(AppLib.Data.Connection conn, string codEstrutura, string codRevEstrutura, string codOrdem, string seqOrdem, string descricao, string status, Decimal QtPlan, int SeqPai, int nivel)
        {
            try
            {

                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PORDEM");

                v.Set("CODEMPRESA", AppLib.Context.Empresa);
                v.Set("CODFILIAL", AppLib.Context.Filial);
                v.Set("CODESTRUTURA", codEstrutura);
                v.Set("REVESTRUTURA", codRevEstrutura);

                v.Set("CODIGOOP", codOrdem.ToString());
                v.Set("SEQOP", seqOrdem);

                v.Set("DESCRICAOOP", descricao);

                v.Set("STATUSOP", status);

                v.Set("DATACRIOP", conn.GetDateTime());
                v.Set("DATALIBOP", null);

                if (SeqPai == 0)
                {
                    v.Set("CODSEQPAI", null);
                }
                else
                {
                    v.Set("CODSEQPAI", SeqPai.ToString("000"));
                }

                v.Set("DATAALTSTATUSOP", null);

                v.Set("DATAINIPLANOP", null);
                v.Set("DATAFIMPLANOP", null);
                v.Set("DATAINIREALOP", null);
                v.Set("DATAFIMREALOP", null);
                v.Set("DATAINIORIGOP", conn.GetDateTime());
                v.Set("DATAFIMORIGOP", conn.GetDateTime());

                v.Set("QTDEPLANOP", QtPlan);
                v.Set("QTDEFETOP", 0);
                v.Set("PERCCONCLUIDOORDEM", 0);

                if (nivel == 0)
                {
                    v.Set("NIVEL", null);
                }
                else
                {
                    v.Set("NIVEL", nivel);
                }

                v.Save();

                SalvarRoteiro(conn, false, codEstrutura, codRevEstrutura, codOrdem, seqOrdem, QtPlan);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable sqlOrdemProducaoEstrutura(string codigoOP)
        {
            try
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
                    SELECT A.CODEMPRESA,A.CODFILIAL,A.CODESTRUTURA,A.REVESTRUTURA,A.CODIGOOP,A.SEQOP,A.CODSEQPAI,A.QTDEPLANOP,A.QTDEPLANOP,A.PERCCONCLUIDOORDEM, 
		                    B.CODOPERACAO, B.SEQOPERACAO,B.STATUSOPERACAO,B.OPERACAOEXTERNA,
		                    B.TEMPOSETUP,B.TEMPOOPERACAO, B.TEMPOEXTRA,B.TEMPOTOTAL,B.TEMPOSETUPREAL,TEMPOOPERACAOREAL,B.TEMPOEXTRAREAL,B.TEMPOTOTALREAL,B.PERCCONCLUIDOROTEIRO,
		                    C.CODCOMPONENTE,C.QTDCOMPONENTE,C.UNDCOMPONENTE,C.TIPOCONSUMO,C.COMPONENTEORIGINAL,C.COMPONENTECANCELADO,
		                    A.NIVEL,A.CODESTRUTURA
	                      FROM PORDEM A  JOIN PORDEMROTEIRO B ON A.CODEMPRESA = B.CODEMPRESA		
													                     AND A.CODFILIAL = B.CODFILIAL
													                     AND A.CODESTRUTURA = B.CODESTRUTURA
													                     AND A.REVESTRUTURA = B.REVESTRUTURA
													                     AND A.CODIGOOP = B.CODIGOOP
													                     AND A.SEQOP = B.SEQOP
					                     JOIN PORDEMCONSUMO C ON B.CODEMPRESA = C.CODEMPRESA		
													                     AND B.CODFILIAL = C.CODFILIAL
													                     AND B.CODESTRUTURA = C.CODESTRUTURA
													                     AND B.REVESTRUTURA = C.REVESTRUTURA
													                     AND B.CODIGOOP = C.CODIGOOP
													                     AND B.SEQOP = C.SEQOP
													                     AND B.SEQOPERACAO = C.SEQOPERACAO
													                     AND B.CODOPERACAO = C.CODOPERACAO
                         WHERE A.CODEMPRESA = ? 
                           AND A.CODFILIAL = ?
	                       AND A.CODIGOOP = ?
                      ORDER BY A.NIVEL DESC", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigoOP });

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable sqlRecursivoEstrutura(string codEstrutura, string revEstrutura)
        {
            try
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
                    WITH Registros (CODEMPRESA,CODFILIAL,CODESTRUTURA,REVESTRUTURA,SEQOPERACAO,CODOPERACAO,DESCESTRUTURA,TIPOESTRUTURA,LOTEMINIMO,LOTEMULTIPLO,FATORUNDGERENCIAL,UNDCONTROLE,CODCOMPONENTE,QTDCOMPONENTE,Nivel,CODESTRUTURAPAI)
                    AS
                    (
	                    SELECT A.CODEMPRESA,A.CODFILIAL,A.CODESTRUTURA,A.REVESTRUTURA,B.SEQOPERACAO,B.CODOPERACAO,A.DESCESTRUTURA,A.TIPOESTRUTURA,A.LOTEMINIMO,A.LOTEMULTIPLO,A.FATORUNDGERENCIAL,A.UNDCONTROLE,B.CODCOMPONENTE,B.QTDCOMPONENTE,0,A.CODESTRUTURA
	                      FROM PROTEIROESTRUTURA A JOIN PROTEIRORECURSO B ON A.CODEMPRESA = B.CODEMPRESA		
													                     AND A.CODFILIAL = B.CODFILIAL
													                     AND A.CODESTRUTURA = B.CODESTRUTURA
													                     AND A.REVESTRUTURA = B.REVESTRUTURA
                         WHERE A.CODEMPRESA = ?
                           AND A.CODFILIAL = ?
                           AND A.ATIVO = 1
	                       AND B.TIPORECURSO = 1
	                       AND A.CODESTRUTURA = ?
	                       AND A.REVESTRUTURA = ?
	   
	                     UNION ALL 

	                     SELECT A.CODEMPRESA,A.CODFILIAL,A.CODESTRUTURA,A.REVESTRUTURA,C.SEQOPERACAO,C.CODOPERACAO,A.DESCESTRUTURA,A.TIPOESTRUTURA,A.LOTEMINIMO,A.LOTEMULTIPLO,A.FATORUNDGERENCIAL,A.UNDCONTROLE,B.CODCOMPONENTE,B.QTDCOMPONENTE,C.Nivel + 1,C.CODESTRUTURA
	                      FROM PROTEIROESTRUTURA A 
								                    JOIN PROTEIRORECURSO B ON A.CODEMPRESA = B.CODEMPRESA		
													                     AND A.CODFILIAL = B.CODFILIAL
													                     AND A.CODESTRUTURA = B.CODESTRUTURA
													                     AND A.REVESTRUTURA = B.REVESTRUTURA
							                        JOIN Registros C  ON B.CODESTRUTURA = C.CODCOMPONENTE

	                     WHERE B.CODEMPRESA = C.CODEMPRESA 
                           AND B.CODFILIAL = C.CODFILIAL
                           AND A.ATIVO = 1
	                       AND B.TIPORECURSO = 1
                    )

                    select A.*,  ISNULL(TemEstruturaAtiva,0) AS TemEstruturaAtiva, D.REVESTRUTURACOMPONENTE
                      from Registros  A left JOIN 
						                    (
						                    SELECT COUNT(D.CODESTRUTURA) AS TemEstruturaAtiva
							                    , D.CODESTRUTURA
							                    , D.REVESTRUTURA AS REVESTRUTURACOMPONENTE
							                    , D.CODEMPRESA
							                    , D.CODFILIAL
						                    FROM PROTEIROESTRUTURA D 
						                    WHERE D.ATIVO = 1 
						                    GROUP BY D.CODESTRUTURA, D.REVESTRUTURA, D.CODEMPRESA, D.CODFILIAL
						                    ) D 
						                    ON D.CODEMPRESA = A.CODEMPRESA 
						                    AND D.CODFILIAL = A.CODFILIAL
						                    AND D.CODESTRUTURA = A.CODCOMPONENTE
                    order by A.SEQOPERACAO,A.Nivel desc
            ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, revEstrutura });

                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool Salvar()
        {
            bool _salvar = false;

            if (validacao() == true)
            {
                string Param_PRODUCAOMESMAOP = VerificaParametro("PRODUCAOMESMAOP");

                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PORDEM");
                conn.BeginTransaction();
                try
                {
                    if (edita == true)
                    {
                        if (cmbStatus.SelectedValue.ToString() == "1")
                        {
                            v.Set("CODEMPRESA", AppLib.Context.Empresa);
                            v.Set("CODFILIAL", AppLib.Context.Filial);
                            codEstrutura = lookupEstrutura.ValorCodigoInterno;
                            v.Set("CODESTRUTURA", codEstrutura);

                            if (lookupEstrutura.OutrasChaves.Any(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA") == true)
                            {
                                int indexRevisao = lookupEstrutura.OutrasChaves.IndexOf(lookupEstrutura.OutrasChaves.Where(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA").FirstOrDefault());

                                CodRevEstrutura = lookupEstrutura.OutrasChaves[0].ValorColunaChave.ToString();

                                v.Set("REVESTRUTURA", lookupEstrutura.OutrasChaves[0].ValorColunaChave.ToString());
                            }

                            codOrdem = txtNroOp.Text;
                            v.Set("CODIGOOP", txtNroOp.Text);

                            seqOrdem = txtSeqOp.Text;
                            v.Set("SEQOP", txtSeqOp.Text);

                            v.Set("DESCRICAOOP", txtDescricao.Text);

                            v.Set("STATUSOP", cmbStatus.SelectedValue);

                            // --------------
                            //v.Set("DATALIBOP", conn.GetDateTime());
                            v.Set("DATALIBOP", null);

                            if (string.IsNullOrEmpty(txtSeqPai.Text))
                            {
                                v.Set("CODSEQPAI", null);
                            }
                            else
                            {
                                v.Set("CODSEQPAI", txtSeqPai.Text);
                            }

                            if (string.IsNullOrEmpty(txtPlanejadaIni.Text))
                            {
                                v.Set("DATAINIPLANOP", null);
                            }
                            else
                            {
                                v.Set("DATAINIPLANOP", Convert.ToDateTime(txtPlanejadaIni.Text));
                            }

                            if (string.IsNullOrEmpty(txtPlanejadaFim.Text))
                            {
                                v.Set("DATAFIMPLANOP", null);
                            }
                            else
                            {
                                v.Set("DATAFIMPLANOP", Convert.ToDateTime(txtPlanejadaFim.Text));
                            }


                            if (string.IsNullOrEmpty(txtRealizadaIni.Text))
                            {
                                v.Set("DATAINIREALOP", null);
                            }
                            else
                            {
                                v.Set("DATAINIREALOP", Convert.ToDateTime(txtRealizadaIni.Text));
                            }

                            if (string.IsNullOrEmpty(txtRealizadaFim.Text))
                            {
                                v.Set("DATAFIMREALOP", null);
                            }
                            else
                            {
                                v.Set("DATAFIMREALOP", Convert.ToDateTime(txtRealizadaFim.Text));
                            }

                            //if (string.IsNullOrEmpty(txtOriginalIni.Text))
                            //{
                            //    v.Set("DATAINIORIGOP", conn.GetDateTime());
                            //}
                            //else
                            //{
                            //    v.Set("DATAINIORIGOP", txtOriginalIni.Text);
                            //}

                            if (string.IsNullOrEmpty(txtOriginalFim.Text))
                            {
                                v.Set("DATAFIMORIGOP", null);
                            }
                            else
                            {
                                v.Set("DATAFIMORIGOP", Convert.ToDateTime(txtOriginalFim.Text));
                            }

                            v.Set("QTDEPLANOP", Convert.ToDecimal(txtQtPlan.Text.Replace(".","").Replace(".", ",")));
                            v.Set("QTDEFETOP", Convert.ToDecimal(txtEfetiv.Text));
                            v.Set("PERCCONCLUIDOORDEM", Convert.ToDecimal(txtPorcCanc.Text));

                            v.Save();

                            if (cmbStatus.SelectedValue == "1")
                            {
                                if (Existe_Ordem() == true)
                                {
                                    //conn.ExecTransaction("DELETE FROM PORDEMENTRADA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, txtNroOp.Text, txtSeqOp.Text });
                                    //conn.ExecTransaction("DELETE FROM PORDEMCONSUMO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, txtNroOp.Text, txtSeqOp.Text });
                                    //conn.ExecTransaction("DELETE FROM PORDEMAPTORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, txtNroOp.Text, txtSeqOp.Text });
                                    //conn.ExecTransaction("DELETE FROM PORDEMAPONTAMENTO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, txtNroOp.Text, txtSeqOp.Text });
                                    //conn.ExecTransaction("DELETE FROM PORDEMALOCACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, txtNroOp.Text, txtSeqOp.Text });
                                    //conn.ExecTransaction("DELETE FROM PORDEMROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, txtNroOp.Text, txtSeqOp.Text });

                                    conn.ExecTransaction("DELETE FROM PORDEMENTRADA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODIGOOP = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtNroOp.Text });
                                    conn.ExecTransaction("DELETE FROM PORDEMCONSUMO WHERE CODEMPRESA = ? AND CODFILIAL = ?  AND CODIGOOP = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial,  txtNroOp.Text});
                                    conn.ExecTransaction("DELETE FROM PORDEMAPTORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODIGOOP = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial,txtNroOp.Text });
                                    conn.ExecTransaction("DELETE FROM PORDEMAPONTAMENTO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODIGOOP = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial,txtNroOp.Text });
                                    conn.ExecTransaction("DELETE FROM PORDEMALOCACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODIGOOP = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtNroOp.Text});
                                    conn.ExecTransaction("DELETE FROM PORDEMROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODIGOOP = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtNroOp.Text });
                                }

                                SalvarRoteiro(conn, false, codEstrutura, CodRevEstrutura, codOrdem, seqOrdem, Convert.ToDecimal(txtQtPlan.Text.Replace(".","").Replace(".", ",")));

                                if (!string.IsNullOrEmpty(Param_PRODUCAOMESMAOP))
                                {
                                    switch (Param_PRODUCAOMESMAOP)
                                    {
                                        case "1": //SIM
                                            CriaOrdemRecursiva(conn);
                                            break;

                                        case "2": //NÃO
                                            break;

                                        default:
                                            throw new Exception("Parâmetro Inválido: PRODUCAOMESMAOP");
                                            break;
                                    }
                                }
                                
                            }
                        }
                        else
                        {
                            MessageBox.Show("Esta Ordem de Produção não pode ser alterada.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }
                    else
                    {
                        if (lookupEstrutura.OutrasChaves.Any(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA") == true)
                        {
                            int indexRevisao = lookupEstrutura.OutrasChaves.IndexOf(lookupEstrutura.OutrasChaves.Where(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA").FirstOrDefault());

                            CodRevEstrutura = lookupEstrutura.OutrasChaves[0].ValorColunaChave.ToString();
                        }

                        codEstrutura = lookupEstrutura.ValorCodigoInterno;
                        GravaNovaOP(conn, lookupEstrutura.ValorCodigoInterno, CodRevEstrutura, txtNroOp.Text, txtSeqOp.Text, txtDescricao.Text, cmbStatus.SelectedValue.ToString(), Convert.ToDecimal(txtQtPlan.Text.Replace(".","").Replace(".", ",")), 0, 0);

                        if (!string.IsNullOrEmpty(Param_PRODUCAOMESMAOP))
                        {
                            switch (Param_PRODUCAOMESMAOP)
                            {
                                case "1": //SIM
                                    CriaOrdemRecursiva(conn);
                                    break;

                                case "2": //NÃO
                                    break;

                                default:
                                    throw new Exception("Parâmetro Inválido: PRODUCAOMESMAOP");
                                    break;
                            }
                        }

                        conn.ExecQuery("update pparam set VALOR = VALOR + 1 where NOME = 'IDORDEMOPERACAO'", new object[] { });
                    }

                    if (SalvaCompl() == true)
                    {
                        conn.Commit();
                    }
                    else
                    {
                        throw new Exception("Erro ao gravar ordem de produação");
                    }

                    codOrdem = txtNroOp.Text;
                    seqOrdem = txtSeqOp.Text;

                    carregaCampos();
                    this.edita = true;

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

        private bool SalvaCompl()
        {
            List<PS.Glb.Class.Parametro> param = new List<PS.Glb.Class.Parametro>();

            PS.Glb.Class.Parametro parametro = new PS.Glb.Class.Parametro();

            parametro.nomeParametro = "CODEMPRESA";
            parametro.valorParametro = AppLib.Context.Empresa.ToString();

            param.Add(parametro);

            parametro = new PS.Glb.Class.Parametro();

            parametro.nomeParametro = "CODFILIAL";
            parametro.valorParametro = AppLib.Context.Filial.ToString();

            param.Add(parametro);

            parametro = new PS.Glb.Class.Parametro();

            parametro.nomeParametro = "CODIGOOP";
            parametro.valorParametro = txtNroOp.Text;

            param.Add(parametro);

            parametro = new PS.Glb.Class.Parametro();

            parametro.nomeParametro = "REVESTRUTURA";
            parametro.valorParametro = CodRevEstrutura;

            param.Add(parametro);

            parametro = new PS.Glb.Class.Parametro();

            parametro.nomeParametro = "SEQOP";
            parametro.valorParametro = txtSeqOp.Text;

            param.Add(parametro);

            PS.Glb.Class.Utilidades util = new PS.Glb.Class.Utilidades();

            if (tabCamposComplementares.Controls.Count > 0)
            {
                util.salvaCamposComplementares(this, "PORDEMCOMPL", tabCamposComplementares, param, AppLib.Context.poolConnection.Get("Start"));
                //bool retorno = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT COUNT(CODIGOOP) FROM PORDEMCOMPL WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, txtNroOp.Text, txtSeqOp.Text }));
                //if (retorno == false)
                //{
                //    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO PORDEMCOMPL (CODEMPRESA,CODFILIAL,CODESTRUTURA,REVESTRUTURA,CODIGOOP,SEQOP) VALUES (?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, txtNroOp.Text, txtSeqOp.Text });

                //    string query = util.update(this, tabCamposComplementares, "PORDEMCOMPL");
                //    if (!string.IsNullOrEmpty(query))
                //    {
                //        query = query.Remove(query.Length - 2, 1) + " WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?";
                //        AppLib.Context.poolConnection.Get("Start").ExecTransaction(query, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, txtNroOp.Text, txtSeqOp.Text });
                //    }
                //}

                //// 11/10/2017 João Pedro
                ////PS.Glb.Class.Utilidades util = new PS.Glb.Class.Utilidades();
                //string sql = util.update(this, tabCamposComplementares, "PORDEMCOMPL");
                //if (!string.IsNullOrEmpty(sql))
                //{
                //    sql = sql.Remove(sql.Length - 2, 1) + " WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?";
                //    AppLib.Context.poolConnection.Get("Start").ExecTransaction(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, codOrdem, seqOrdem });
                //}
                return true;
            }
            else
            {
                return true;
            }
        }

        private void CriaOrdemRecursiva(AppLib.Data.Connection conn)
        {
            try
            {
                DataTable dtRecursivo = sqlRecursivoEstrutura(lookupEstrutura.ValorCodigoInterno, CodRevEstrutura);

                if (dtRecursivo.Rows.Count > 0)
                {
                    var itemPai = from d in dtRecursivo.Select("Nivel = 0").AsEnumerable()
                                  group d by new
                                  {
                                      CODESTRUTURA = d.Field<string>("CODESTRUTURA"),
                                      SEQOPERACAO = d.Field<string>("SEQOPERACAO")
                                  } into grp
                                  select grp;

                    Hashtable controlaSeq = new Hashtable();
                    for (int y = 0; y <= (itemPai.Count() - 1); y++)
                    {
                        controlaSeq.Add(string.Concat(lookupEstrutura.ValorCodigoInterno, ";", itemPai.ElementAt(y).Key.SEQOPERACAO, ";", "0"), new ClsSeqOP(lookupEstrutura.ValorCodigoInterno, "", Convert.ToInt16(txtSeqOp.Text), 0, itemPai.ElementAt(y).Key.SEQOPERACAO));
                    }

                    int SeqOp = Convert.ToInt16(txtSeqOp.Text);

                    var groupby = from d in dtRecursivo.Select("Nivel > 0", "SEQOPERACAO,Nivel asc").AsEnumerable()
                                  group d by new
                                  {
                                      CODESTRUTURAPAI = d.Field<string>("CODESTRUTURAPAI"),
                                      CODESTRUTURA = d.Field<string>("CODESTRUTURA"),
                                      SEQOPERACAO = d.Field<string>("SEQOPERACAO"),
                                      Nivel = d.Field<int>("Nivel")
                                  } into grp
                                  select grp;

                    for (int x = 0; x <= (groupby.Count() - 1); x++)
                    {
                        DataRow[] row = dtRecursivo.Select("CODESTRUTURA = '" + groupby.ElementAt(x).Key.CODESTRUTURAPAI + "' AND SEQOPERACAO = '" + groupby.ElementAt(x).Key.SEQOPERACAO + "' AND CODCOMPONENTE = '" + groupby.ElementAt(x).Key.CODESTRUTURA + "' AND Nivel = " + (Convert.ToInt16(groupby.ElementAt(x).Key.Nivel) - 1));

                        if (row.Count() == 1)
                        {
                            //decimal qtd = Convert.ToDecimal(txtQtPlan.Text.Replace(".", ",")) * Convert.ToDecimal(row[0]["QTDCOMPONENTE"]);
                            decimal qtd = Convert.ToDecimal(txtQtPlan.Text) * Convert.ToDecimal(row[0]["QTDCOMPONENTE"]);

                            SeqOp = SeqOp + 1;

                            ClsSeqOP item = (ClsSeqOP)controlaSeq[string.Concat(row[0]["CODESTRUTURA"].ToString(), ";", row[0]["SEQOPERACAO"].ToString(), ";", (Convert.ToInt16(row[0]["Nivel"].ToString())))];

                            controlaSeq.Add(string.Concat(row[0]["CODCOMPONENTE"].ToString(), ";", row[0]["SEQOPERACAO"].ToString(), ";", Convert.ToInt16(row[0]["Nivel"].ToString()) + 1), new ClsSeqOP(row[0]["CODCOMPONENTE"].ToString(), row[0]["CODESTRUTURA"].ToString(), SeqOp, item.SeqOp, row[0]["SEQOPERACAO"].ToString()));

                            GravaNovaOP(conn, row[0]["CODCOMPONENTE"].ToString(), row[0]["REVESTRUTURACOMPONENTE"].ToString(), txtNroOp.Text, SeqOp.ToString("000"), row[0]["DESCESTRUTURA"].ToString(), cmbStatus.SelectedValue.ToString(), qtd, item.SeqOp, Convert.ToInt16(row[0]["Nivel"].ToString()) + 1);
                        }
                        else
                        {
                            MessageBox.Show("Erro ao Criar Ordem de Produção, Contate o Administrador!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            conn.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private bool Existe_Ordem()
        {
            try
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PORDEM WITH (NOLOCK) WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, txtNroOp.Text, txtSeqOp.Text });

                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        private void carregaCampos()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PORDEM WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, codOrdem, seqOrdem });
            if (dt.Rows.Count > 0)
            {
                tabRecursoOperacao.Enabled = true;

                Global gl = new Global();

                gl.EnableTab(tabRecursoOperacao.TabPages["tabRoteiro"], false);

                codEstrutura = dt.Rows[0]["CODESTRUTURA"].ToString();
                CodRevEstrutura = dt.Rows[0]["REVESTRUTURA"].ToString();

                txtNroOp.Text = dt.Rows[0]["CODIGOOP"].ToString();
                txtSeqOp.Text = dt.Rows[0]["SEQOP"].ToString();
                txtDescricao.Text = dt.Rows[0]["DESCRICAOOP"].ToString();
                txtSeqPai.Text = dt.Rows[0]["CODSEQPAI"].ToString();

                lookupEstrutura.txtcodigo.Text = dt.Rows[0]["CODESTRUTURA"].ToString();
                lookupEstrutura.OutrasChaves.Clear();
                lookupEstrutura.OutrasChaves.Add(new Newlookup_OutrasChaves { NomeColunaChave = "REVESTRUTURA", ValorColunaChave = CodRevEstrutura });
                lookupEstrutura.CarregaDescricao();

                if (lookupEstrutura.OutrasChaves.Any(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA") == true)
                {
                    int indexRevisao = lookupEstrutura.OutrasChaves.IndexOf(lookupEstrutura.OutrasChaves.Where(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA").FirstOrDefault());

                    DataTable dtestrutura = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PROTEIROESTRUTURA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura });
                    if (dtestrutura.Rows.Count > 0)
                    {
                        lookupunidade.txtcodigo.Text = dtestrutura.Rows[0]["UNDCONTROLE"].ToString();
                        lookupunidade.CarregaDescricao();

                        CarregaGrid_Operacao(codEstrutura, CodRevEstrutura);

                        cmbTipo.SelectedValue = dtestrutura.Rows[0]["TIPOESTRUTURA"].ToString();
                    }
                }

                if (string.IsNullOrEmpty(lookupEstrutura.ValorCodigoInterno))
                {
                    //validar quando a estrutura nao estiver ativa... oque fazer!!
                    DataTable dtestrutura = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PROTEIROESTRUTURA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura });
                    if (dtestrutura.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dtestrutura.Rows[0]["ATIVO"]))
                        {
                            CarregaGrid_Operacao(dtestrutura.Rows[0]["CODESTRUTURA"].ToString(), dtestrutura.Rows[0]["REVESTRUTURA"].ToString());
                        }
                        else
                        {
                            lookupEstrutura.txtcodigo.Text = dtestrutura.Rows[0]["CODESTRUTURA"].ToString();
                            lookupEstrutura.ValorCodigoInterno = dtestrutura.Rows[0]["CODESTRUTURA"].ToString();
                            lookupEstrutura.txtconteudo.Text = dtestrutura.Rows[0]["DESCESTRUTURA"].ToString();

                            CarregaGrid_Operacao(dtestrutura.Rows[0]["CODESTRUTURA"].ToString(), dtestrutura.Rows[0]["REVESTRUTURA"].ToString());
                        }
                    }

                }
                else
                {
                    CarregaGrid_Operacao(lookupEstrutura.ValorCodigoInterno.ToString(), lookupEstrutura.OutrasChaves[0].ValorColunaChave.ToString());
                }

                if (string.IsNullOrEmpty(dt.Rows[0]["DATALIBOP"].ToString()))
                {
                    txtModificadoem.Text = "";
                }
                else
                {
                    txtModificadoem.Text = Convert.ToDateTime(dt.Rows[0]["DATALIBOP"].ToString()).ToString("dd/MM/yyyy");
                }

                cmbStatus.SelectedValue = dt.Rows[0]["STATUSOP"].ToString();

                lookupEstrutura.Edita(false);

                if (string.IsNullOrEmpty(dt.Rows[0]["DATAINIPLANOP"].ToString()))
                {
                    txtPlanejadaIni.Text = "";
                }
                else
                {
                    txtPlanejadaIni.Text = Convert.ToDateTime(dt.Rows[0]["DATAINIPLANOP"].ToString()).ToString("dd/MM/yyyy");
                }

                if (string.IsNullOrEmpty(dt.Rows[0]["DATAFIMPLANOP"].ToString()))
                {
                    txtPlanejadaFim.Text = "";
                }
                else
                {
                    txtPlanejadaFim.Text = Convert.ToDateTime(dt.Rows[0]["DATAFIMPLANOP"].ToString()).ToString("dd/MM/yyyy");
                }

                if (string.IsNullOrEmpty(dt.Rows[0]["DATAINIREALOP"].ToString()))
                {
                    txtRealizadaIni.Text = "";
                }
                else
                {
                    txtRealizadaIni.Text = Convert.ToDateTime(dt.Rows[0]["DATAINIREALOP"].ToString()).ToString("dd/MM/yyyy");
                }

                if (string.IsNullOrEmpty(dt.Rows[0]["DATAFIMREALOP"].ToString()))
                {
                    txtRealizadaFim.Text = "";
                }
                else
                {
                    txtRealizadaFim.Text = Convert.ToDateTime(dt.Rows[0]["DATAFIMREALOP"].ToString()).ToString("dd/MM/yyyy");
                }

                if (string.IsNullOrEmpty(dt.Rows[0]["DATAINIORIGOP"].ToString()))
                {
                    txtOriginalIni.Text = "";
                }
                else
                {
                    txtOriginalIni.Text = Convert.ToDateTime(dt.Rows[0]["DATAINIORIGOP"].ToString()).ToString("dd/MM/yyyy");
                }

                if (string.IsNullOrEmpty(dt.Rows[0]["DATAFIMORIGOP"].ToString()))
                {
                    txtOriginalFim.Text = "";
                }
                else
                {
                    txtOriginalFim.Text = Convert.ToDateTime(dt.Rows[0]["DATAFIMORIGOP"].ToString()).ToString("dd/MM/yyyy");
                }

                txtQtPlan.Text = dt.Rows[0]["QTDEPLANOP"].ToString();

                if (cmbStatus.SelectedValue != "1")
                {
                    txtQtPlan.Enabled = false;
                }
                else
                {
                    txtQtPlan.Enabled = true;
                }

                if (!string.IsNullOrEmpty(txtSeqPai.Text))
                {
                    txtQtPlan.Enabled = false;
                    btnSalvar.Enabled = false;
                    btnOk.Enabled = false;
                }
                else
                {
                    txtQtPlan.Enabled = true;
                    btnSalvar.Enabled = true;
                    btnOk.Enabled = true;
                }

                txtEfetiv.Text = dt.Rows[0]["QTDEFETOP"].ToString();
                txtPorcCanc.Text = dt.Rows[0]["PERCCONCLUIDOORDEM"].ToString();

                DataRow row1 = gridViewOperacao.GetDataRow(Convert.ToInt32(gridViewOperacao.GetSelectedRows().GetValue(0).ToString()));

                string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                CarregaGrid_OrdemResumo(codEstrutura, CodRevEstrutura);

                CarregaGrid_Recursos(row1[0].ToString(), codOperacao);

                CarregaGrid_Componentes(row1[0].ToString(), codOperacao);

                CarregaGrid1_Apontamento(row1[0].ToString(), codOperacao);

                CarregaGrid1_Entrada(row1[0].ToString(), codOperacao);

                CarregaGrid1_Compra(row1[0].ToString(), codOperacao);
            }

            carregaCamposComplementares();

        }

        private void carregaCamposComplementares()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM PORDEMCOMPL WHERE CODIGOOP = ? AND CODEMPRESA = ? AND CODFILIAL = ? AND SEQOP = ?", new object[] { txtNroOp.Text, AppLib.Context.Empresa, AppLib.Context.Filial, txtSeqOp.Text });
            DataTable dtColunas = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT NOMECAMPO FROM GTABCAMPOCOMPL WHERE CODENTIDADE = ? AND ATIVO = ? ORDER BY ORDEM", new object[] { "PORDEMCOMPL", 1 });
            if (dt.Rows.Count > 0)
            {
                Control controle;
                for (int iColunas = 0; iColunas < dtColunas.Rows.Count; iColunas++)
                {
                    for (int i = 0; i < tabCamposComplementares.Controls.Count; i++)
                    {
                        controle = tabCamposComplementares.Controls[i];
                        if (controle.GetType().Name.Equals("TextEdit") || controle.GetType().Name.Equals("ComboBox") || controle.GetType().Name.Equals("DateEdit") || controle.GetType().Name.Equals("MemoEdit"))
                        {
                            if (controle.Name.Remove(0, 4) == dtColunas.Rows[iColunas]["NOMECAMPO"].ToString())
                            {
                                controle.Text = dt.Rows[0][dtColunas.Rows[iColunas]["NOMECAMPO"].ToString()].ToString();
                            }
                        }
                        else if (controle.GetType().Name.Equals("CheckEdit"))
                        {
                            if (controle.Name.Remove(0, 4) == dtColunas.Rows[iColunas]["NOMECAMPO"].ToString())
                            {
                                if (dt.Rows[0][dtColunas.Rows[iColunas]["NOMECAMPO"].ToString()].ToString() == "1")
                                {
                                    DevExpress.XtraEditors.CheckEdit ck = (DevExpress.XtraEditors.CheckEdit)controle;
                                    ck.Checked = true;
                                }
                                else
                                {
                                    DevExpress.XtraEditors.CheckEdit ck = (DevExpress.XtraEditors.CheckEdit)controle;
                                    ck.Checked = false;
                                }

                            }
                        }
                    }
                }

            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (Salvar() == true)
                {
                    this.Dispose();
                }
            }
            else
            {
                btnSalvar.PerformClick();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            LimpaOrdemProducao();
        }

        void LimpaAbaRoteiro()
        {
            lookupoperacao.Clear();
            lookupcentrotrabalho.Clear();

            cmbRoteiroStatus.SelectedValue = "1";

            txtRoteiroPlanejadoSetup.Text = "";
            txtRoteiroPlanejadoOperacao.Text = "";
            txtRoteiroPlanejadoExtra.Text = "";
            txtRoteiroPlanejadoTotal.Text = "";

            txtRoteiroPorcCanc.Text = "0";
            tabRecursoOperacao.Text = "";
            txtRoteiroReal.Text = "0";

            txtRoteiroRealizadoSetup.Text = "";
            txtRoteiroRealizadoOperacao.Text = "";
            txtRoteiroRealizadoExtra.Text = "";
            txtRoteiroRealizadoTotal.Text = "";


            txtRoteiroPlanejadaIni.Text = "";
            txtRoteiroPlanejadaFim.Text = "";
            txtRoteiroOriginalIni.Text = "";
            txtRoteiroOriginalFim.Text = "";
            txtRoteiroRealizadaIni.Text = "";
            txtRoteiroRealizadaFim.Text = "";


            cmbRoteiroStatus.Enabled = false;
            txtRoteiroReal.Enabled = false;
            txtRoteiroPorcCanc.Enabled = false;
            txtRoteiroRealizadoSetup.Enabled = false;
            txtRoteiroRealizadoOperacao.Enabled = false;
            txtRoteiroRealizadoExtra.Enabled = false;
            txtRoteiroRealizadoTotal.Enabled = false;

            txtRoteiroPlanejadoSetup.Enabled = false;
            txtRoteiroPlanejadoOperacao.Enabled = false;
            txtRoteiroPlanejadoExtra.Enabled = false;
            txtRoteiroPlanejadoTotal.Enabled = false;

            txtRoteiroPlanejadaIni.Enabled = false;
            txtRoteiroPlanejadaFim.Enabled = false;
            txtRoteiroOriginalIni.Enabled = false;
            txtRoteiroOriginalFim.Enabled = false;
            txtRoteiroRealizadaIni.Enabled = false;
            txtRoteiroRealizadaFim.Enabled = false;

        }

        void LimpaOrdemProducao()
        {
            edita = false;

            txtNroOp.Text = "";
            VerificaParametro_NROOP();

            txtSeqOp.Text = "001";
            txtSeqPai.Text = "";
            txtDescricao.Text = "";

            lookupEstrutura.Clear();
            lookupunidade.Clear();

            cmbTipo.SelectedIndex = -1;
            cmbStatus.SelectedValue = "1";

            txtModificadoem.Text = "";
            txtQtPlan.Text = "";
            txtEfetiv.Text = "";
            txtPorcCanc.Text = "";

            txtPlanejadaIni.Text = "";
            txtPlanejadaFim.Text = "";
            txtOriginalIni.Text = "";
            txtOriginalFim.Text = "";
            txtRealizadaIni.Text = "";
            txtRealizadaFim.Text = "";

            tabRecursoOperacao.Enabled = false;

            LimpaGrid_Operacao();

            //ABA RESUMO
            LimpaGrid_OrdemResumo();

            //ABA ROTEIRO
            LimpaAbaRoteiro();

            //ABA RECURSO
            LimpaGrid1_Recursos();

            VerificaParametro_MASCARAORDEM();
        }

        public string VerificaParametro(string parametro)
        {
            try
            {
                Global gl = new Global();
                string _PARAM = gl.VerificaParametroString(parametro);

                return _PARAM;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        public void VerificaParametro_NROOP()
        {
            try
            {
                Global gl = new Global();
                string _NROOP = gl.VerificaParametroString("CUSTOFORMA");

                if (_NROOP == "1") //IMPLEMENTAR
                {

                }
                else if (_NROOP == "2") //IMPLEMENTAR
                {

                }
                else
                {
                    MessageBox.Show("Parâmetro não definido, contate o administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

        }

        public void VerificaParametro_MASCARAORDEM()
        {
            try
            {
                Global gl = new Global();
                string _MASKORDEM = gl.VerificaParametroString("MASCARAORDEM");

                if (!string.IsNullOrEmpty(_MASKORDEM))
                {
                    int _IDORDEM = Convert.ToInt32(string.IsNullOrEmpty(gl.VerificaParametroString("IDORDEMOPERACAO")) ? "0" : gl.VerificaParametroString("IDORDEMOPERACAO")) + 1;

                    List<string> mascara = new List<string>();

                    string param = "";
                    for (int i = 0; i < _MASKORDEM.ToCharArray().Count(); i++)
                    {
                        if (i == (_MASKORDEM.ToCharArray().Count() - 1))
                        {
                            param = param + _MASKORDEM.ToCharArray()[i];
                            mascara.Add(param);
                            param = "";
                        }
                        else
                        {
                            if (_MASKORDEM.ToCharArray()[i] == _MASKORDEM.ToCharArray()[i + 1])
                            {
                                param = param + _MASKORDEM.ToCharArray()[i];
                            }
                            else
                            {
                                param = param + _MASKORDEM.ToCharArray()[i];
                                mascara.Add(param);
                                param = "";
                            }
                        }
                    }

                    string regex = "";
                    string idopregex = "";
                    for (int i = 0; i < mascara.Count(); i++)
                    {
                        if (mascara[i].Contains("#"))
                        {
                            regex = regex + mascara[i].ToString().Replace("#", "[0-9]");
                            idopregex = idopregex + string.Join("", Enumerable.Repeat("0", (mascara[i].Length - _IDORDEM.ToString().Length))).ToString() + _IDORDEM;
                        }
                        else if (mascara[i].Contains("a"))
                        {
                            if (mascara[i].Length == 2)
                            {
                                regex = regex + "([1-9][0-9])";
                                idopregex = idopregex + AppLib.Context.poolConnection.Get().GetDateTime().Date.ToString("yy");
                            }
                            else if (mascara[i].Length == 4)
                            {
                                regex = regex + "([1][9][0-9][0-9])|([2][0-9][0-9][0-9])";
                                idopregex = idopregex + AppLib.Context.poolConnection.Get().GetDateTime().Date.ToString("yyyy");
                            }
                            else
                            {
                                regex = regex + string.Concat("([", mascara[i].ToString(), "])");
                                idopregex = idopregex + mascara[i].ToString();
                            }
                        }
                        else
                        {
                            regex = regex + string.Concat("([", mascara[i].ToString(), "])");
                            idopregex = idopregex + mascara[i].ToString();
                        }
                    }

                    txtNroOp.Properties.Mask.EditMask = regex;
                    txtNroOp.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;

                    txtNroOp.Text = idopregex;
                    txtNroOp.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Parâmetro não definido, contate o administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public int VerificaAlocacaoRecursoMesmoDia()
        {
            try
            {
                Global gl = new Global();
                //string _NROOP = gl.VerificaParametroString("CUSTOFORMA");

                //if (_NROOP == "1") //IMPLEMENTAR
                //{

                //}
                //else if (_NROOP == "2") //IMPLEMENTAR
                //{

                //}
                //else
                //{
                //    MessageBox.Show("Parâmetro não definido, contate o administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}

                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelarOperacao_Click(object sender, EventArgs e)
        {


        }

        private void btnIncluirOperacao_Click(object sender, EventArgs e)
        {

        }

        private bool validacaoRoteiro()
        {
            bool verifica = true;

            errorProvider.Clear();

            lookupoperacao.mensagemErrorProvider = "";
            lookupcentrotrabalho.mensagemErrorProvider = "";

            if (string.IsNullOrEmpty(lookupoperacao.txtconteudo.Text))
            {
                lookupoperacao.mensagemErrorProvider = "Favor Selecionar uma Operação";
                verifica = false;
            }
            else
            {
                lookupoperacao.mensagemErrorProvider = "";
            }

            //if (string.IsNullOrEmpty(txtDescricao.Text))
            //{
            //    errorProvider.SetError(txtDescricao, "Favor preencher a Descrição");
            //    verifica = false;
            //}


            return verifica;
        }
        private void Incluir_PORDEMALOCACAO(AppLib.Data.Connection conn, string codEstrutura, int codRevEstrutura, string codOrdem, string seqOrdem, string seqOperacao, string codOperacao, int tiporecurso, int tipoaloc, DateTime dataalocacao, decimal TempoAlocacao, string codgruporecurso, string codrecurso)
        {
            try
            {
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PORDEMALOCACAO");

                v.Set("CODEMPRESA", AppLib.Context.Empresa);
                v.Set("CODFILIAL", AppLib.Context.Filial);

                v.Set("CODESTRUTURA", codEstrutura);
                v.Set("REVESTRUTURA", codRevEstrutura);

                v.Set("CODIGOOP", codOrdem);
                v.Set("SEQOP", seqOrdem);

                v.Set("SEQOPERACAO", seqOperacao);
                v.Set("CODOPERACAO", codOperacao);

                v.Set("TIPORECURSO", tiporecurso);

                v.Set("TIPOALOC", tipoaloc);

                v.Set("DATAALOCACAO", dataalocacao);

                v.Set("TEMPOALOCACAO", Convert.ToDecimal(TempoAlocacao));

                v.Set("CODGRUPORECURSO", codgruporecurso);

                v.Set("CODRECURSO", codrecurso);

                v.Insert();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IncluirRecurso(AppLib.Data.Connection conn, DataRow row, string codOperacao, string seqOperacao, int tiporecurso, decimal TempoTotal, string codEstrutura, string codRevEstrutura, string codOrdem, string seqOrdem, Decimal QtPlan)
        {
            try
            {
                Incluir_PORDEMALOCACAO(conn, codEstrutura, Convert.ToInt16(codRevEstrutura), codOrdem, seqOrdem, seqOperacao, codOperacao, tiporecurso, 0, conn.GetDateTime(), TempoTotal, row["PROTEIRORECURSO.CODGRUPORECURSO"].ToString(), null);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void IncluirRecursoComponente(AppLib.Data.Connection conn, DataRow row, string codOperacao, string seqOperacao, string codEstrutura, string codRevEstrutura, string codOrdem, string seqOrdem, Decimal QtPlan)
        {
            try
            {
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PORDEMCONSUMO");

                v.Set("CODEMPRESA", AppLib.Context.Empresa);
                v.Set("CODFILIAL", AppLib.Context.Filial);

                v.Set("CODESTRUTURA", codEstrutura);
                v.Set("REVESTRUTURA", codRevEstrutura);

                v.Set("CODIGOOP", codOrdem);
                v.Set("SEQOP", seqOrdem);

                v.Set("SEQOPERACAO", Convert.ToInt16(seqOperacao).ToString("000"));
                v.Set("CODOPERACAO", codOperacao);

                v.Set("SEQAPO", null);

                v.Set("TIPOCONSUMO", "N");

                v.Set("CODOPER", null);
                v.Set("NSEQITEM", null);
                v.Set("DATAMOVIMENTO", null);

                v.Set("COMPONENTEORIGINAL", 1);
                v.Set("COMPONENTECANCELADO", 2);

                v.Set("QTDCOMPONENTE", Convert.ToDecimal(Convert.ToDecimal(row["PROTEIRORECURSO.QTDCOMPONENTE"].ToString()) * QtPlan));
                v.Set("CODCOMPONENTE", row["PROTEIRORECURSO.CODCOMPONENTE"].ToString());
                v.Set("UNDCOMPONENTE", row["PROTEIRORECURSO.UNDCOMPONENTE"].ToString());

                v.Insert();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private bool SalvarRecurso(AppLib.Data.Connection conn, decimal TempoTotal, string codOperacao, string seqOperacao, string codEstrutura, string codRevEstrutura, string codOrdem, string seqOrdem, Decimal QtPlan)
        {
            bool _salvarRecurso = false;

            try
            {
                int tiporecurso = 1; //Componentes
                string sql = "SELECT PROTEIRORECURSO.CODCOMPONENTE AS 'PROTEIRORECURSO.CODCOMPONENTE',VPRODUTO.NOME AS 'VPRODUTO.NOME',PROTEIRORECURSO.QTDCOMPONENTE AS 'PROTEIRORECURSO.QTDCOMPONENTE',PROTEIRORECURSO.UNDCOMPONENTE AS 'PROTEIRORECURSO.UNDCOMPONENTE' FROM PROTEIRORECURSO JOIN VPRODUTO ON PROTEIRORECURSO.CODEMPRESA = VPRODUTO.CODEMPRESA AND PROTEIRORECURSO.CODCOMPONENTE  = VPRODUTO.CODPRODUTO WHERE PROTEIRORECURSO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PROTEIRORECURSO.CODFILIAL = " + AppLib.Context.Filial + " AND PROTEIRORECURSO.CODESTRUTURA = '" + codEstrutura + "' AND PROTEIRORECURSO.REVESTRUTURA = '" + codRevEstrutura + "' AND PROTEIRORECURSO.TIPORECURSO = " + tiporecurso + " AND PROTEIRORECURSO.CODOPERACAO = '" + codOperacao + "' AND PROTEIRORECURSO.SEQOPERACAO = '" + seqOperacao + "'";
                foreach (DataRow row in AppLib.Context.poolConnection.Get("Start").ExecQuery(sql).Rows)
                {
                    IncluirRecursoComponente(conn, row, codOperacao, seqOperacao, codEstrutura, codRevEstrutura, codOrdem, seqOrdem, QtPlan);
                }

                tiporecurso = 2; //Equipamentos
                sql = "SELECT PROTEIRORECURSO.CODGRUPORECURSO AS 'PROTEIRORECURSO.CODGRUPORECURSO',PGRUPORECURSO.DESCGRUPORECURSO AS 'PGRUPORECURSO.DESCGRUPORECURSO', PROTEIRORECURSO.CODRECROTEIRO AS 'PROTEIRORECURSO.CODRECROTEIRO' FROM PROTEIRORECURSO JOIN PGRUPORECURSO ON PROTEIRORECURSO.CODEMPRESA = PGRUPORECURSO.CODEMPRESA AND PROTEIRORECURSO.CODFILIAL = PGRUPORECURSO.CODFILIAL AND PROTEIRORECURSO.CODGRUPORECURSO = PGRUPORECURSO.CODGRUPORECURSO  WHERE PROTEIRORECURSO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PROTEIRORECURSO.CODFILIAL = " + AppLib.Context.Filial + " AND PROTEIRORECURSO.CODESTRUTURA = '" + codEstrutura + "' AND PROTEIRORECURSO.REVESTRUTURA = '" + codRevEstrutura + "' AND PROTEIRORECURSO.TIPORECURSO = " + tiporecurso + " AND PROTEIRORECURSO.CODOPERACAO = '" + codOperacao + "' AND PROTEIRORECURSO.SEQOPERACAO = '" + seqOperacao + "'";
                foreach (DataRow row in AppLib.Context.poolConnection.Get("Start").ExecQuery(sql).Rows)
                {
                    IncluirRecurso(conn, row, codOperacao, seqOperacao, tiporecurso, TempoTotal, codEstrutura, codRevEstrutura, codOrdem, seqOrdem, QtPlan);
                }

                tiporecurso = 3; //MaodeObra
                sql = "SELECT PROTEIRORECURSO.CODGRUPORECURSO AS 'PROTEIRORECURSO.CODGRUPORECURSO',PGRUPORECURSO.DESCGRUPORECURSO AS 'PGRUPORECURSO.DESCGRUPORECURSO',PROTEIRORECURSO.QTDRECURSO  AS 'PROTEIRORECURSO.QTDRECURSO' FROM PROTEIRORECURSO JOIN PGRUPORECURSO ON PROTEIRORECURSO.CODEMPRESA = PGRUPORECURSO.CODEMPRESA AND PROTEIRORECURSO.CODFILIAL = PGRUPORECURSO.CODFILIAL AND PROTEIRORECURSO.CODGRUPORECURSO = PGRUPORECURSO.CODGRUPORECURSO  WHERE PROTEIRORECURSO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PROTEIRORECURSO.CODFILIAL = " + AppLib.Context.Filial + " AND PROTEIRORECURSO.CODESTRUTURA = '" + codEstrutura + "' AND PROTEIRORECURSO.REVESTRUTURA = '" + codRevEstrutura + "' AND PROTEIRORECURSO.TIPORECURSO = " + tiporecurso + " AND PROTEIRORECURSO.CODOPERACAO = '" + codOperacao + "' AND PROTEIRORECURSO.SEQOPERACAO = '" + seqOperacao + "'";
                foreach (DataRow row in AppLib.Context.poolConnection.Get("Start").ExecQuery(sql).Rows)
                {
                    IncluirRecurso(conn, row, codOperacao, seqOperacao, tiporecurso, TempoTotal, codEstrutura, codRevEstrutura, codOrdem, seqOrdem, QtPlan);
                }

                tiporecurso = 4; //Ferramentas
                sql = "SELECT PROTEIRORECURSO.CODGRUPORECURSO AS 'PROTEIRORECURSO.CODGRUPORECURSO',PGRUPORECURSO.DESCGRUPORECURSO AS 'PGRUPORECURSO.DESCGRUPORECURSO',PROTEIRORECURSO.QTDRECURSO  AS 'PROTEIRORECURSO.QTDRECURSO' FROM PROTEIRORECURSO JOIN PGRUPORECURSO ON PROTEIRORECURSO.CODEMPRESA = PGRUPORECURSO.CODEMPRESA AND PROTEIRORECURSO.CODFILIAL = PGRUPORECURSO.CODFILIAL AND PROTEIRORECURSO.CODGRUPORECURSO = PGRUPORECURSO.CODGRUPORECURSO  WHERE PROTEIRORECURSO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PROTEIRORECURSO.CODFILIAL = " + AppLib.Context.Filial + " AND PROTEIRORECURSO.CODESTRUTURA = '" + codEstrutura + "' AND PROTEIRORECURSO.REVESTRUTURA = '" + codRevEstrutura + "' AND PROTEIRORECURSO.TIPORECURSO = " + tiporecurso + " AND PROTEIRORECURSO.CODOPERACAO = '" + codOperacao + "' AND PROTEIRORECURSO.SEQOPERACAO = '" + seqOperacao + "'";
                foreach (DataRow row in AppLib.Context.poolConnection.Get("Start").ExecQuery(sql).Rows)
                {
                    IncluirRecurso(conn, row, codOperacao, seqOperacao, tiporecurso, TempoTotal, codEstrutura, codRevEstrutura, codOrdem, seqOrdem, QtPlan);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //conn.Rollback();
                throw;
            }

            return _salvarRecurso;
        }

        private bool SalvarRoteiro(AppLib.Data.Connection conn, bool edita, string codEstrutura, string codRevEstrutura, string codOrdem, string seqOrdem, Decimal QtPlan)
        {
            bool _salvarRoteiro = false;

            AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PORDEMROTEIRO");

            try
            {
                DataTable dtOperacao = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT  POPERACAO.CODOPERACAO
		                                                                                             ,PROTEIRO.SEQOPERACAO
		                                                                                             ,POPERACAO.OPERACAOEXTERNA
		                                                                                             ,POPERACAO.DESCOPERACAO
		                                                                                             ,PROTEIRO.CODCTRABALHO
                                                                                                     , PROTEIRO.*
                                                                                               FROM PROTEIRO JOIN POPERACAO ON PROTEIRO.CODEMPRESA = POPERACAO.CODEMPRESA

                                                                                                                           AND PROTEIRO.CODFILIAL = POPERACAO.CODFILIAL

                                                                                                                           AND PROTEIRO.CODOPERACAO = POPERACAO.CODOPERACAO
                                                                                              WHERE PROTEIRO.CODEMPRESA = ?
                                                                                                AND PROTEIRO.CODFILIAL = ?

                                                                                                AND PROTEIRO.CODESTRUTURA = ?

                                                                                                AND PROTEIRO.REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, codRevEstrutura });

                foreach (DataRow linha in dtOperacao.Rows)
                {
                    if (edita == false)
                    {
                        v.Set("CODEMPRESA", AppLib.Context.Empresa);
                        v.Set("CODFILIAL", AppLib.Context.Filial);

                        v.Set("CODESTRUTURA", codEstrutura);
                        v.Set("REVESTRUTURA", Convert.ToInt16(codRevEstrutura));

                        v.Set("CODIGOOP", codOrdem.ToString());
                        v.Set("SEQOP", seqOrdem.ToString());

                        v.Set("SEQOPERACAO", linha["SEQOPERACAO"].ToString());

                        v.Set("CODOPERACAO", linha["CODOPERACAO"].ToString());

                        v.Set("CODCTRABALHO", linha["CODCTRABALHO"].ToString());

                        v.Set("DATAINIPLAN", null);
                        v.Set("DATAFIMPLAN", null);
                        v.Set("DATAINIREAL", null);
                        v.Set("DATAFIMREAL", null);
                        v.Set("DATAINIORIG", conn.GetDateTime());
                        v.Set("DATAFIMORIG", conn.GetDateTime());

                        v.Set("STATUSOPERACAO", 1);

                        v.Set("OPERACAOEXTERNA", (Convert.ToBoolean(linha["OPERACAOEXTERNA"].ToString()) == false ? 0 : 1));

                        v.Set("TEMPOSETUPREAL", Convert.ToDecimal(0));
                        v.Set("TEMPOOPERACAOREAL", Convert.ToDecimal(0));
                        v.Set("TEMPOEXTRAREAL", Convert.ToDecimal(0));
                        v.Set("TEMPOTOTALREAL", Convert.ToDecimal(0));

                        v.Set("QTDEREAL", Convert.ToDecimal(0));
                        v.Set("QTDEPLANEJADA", Convert.ToDecimal(txtQtPlan.Text));
                        v.Set("PERCCONCLUIDOROTEIRO", Convert.ToDecimal(0));

                        var numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "," };

                        v.Set("TEMPOSETUP", Convert.ToDecimal(linha["TEMPOSETUP"].ToString()));


                        switch (linha["TIPOTEMPOEXTRA"].ToString())
                        {
                            case "1": //FIXO
                                v.Set("TEMPOEXTRA", linha["TEMPOEXTRA"].ToString());
                                break;
                            case "2": //PROPORCIONAL
                                v.Set("TEMPOEXTRA", Convert.ToString(Math.Ceiling(Decimal.Parse(linha["TEMPOEXTRA"].ToString(), numberFormatInfo) * Decimal.Parse(QtPlan.ToString(), numberFormatInfo))));
                                break;
                            default:
                                v.Set("TEMPOEXTRA", 0);
                                break;
                        }

                        switch (linha["TIPOTEMPO"].ToString())
                        {
                            case "1": //FIXO
                                v.Set("TEMPOOPERACAO", Convert.ToDecimal(linha["TEMPOOPERACAO"].ToString()));
                                break;
                            case "2": //PROPORCIONAL
                                v.Set("TEMPOOPERACAO", Convert.ToDecimal(Math.Ceiling(Decimal.Parse(linha["TEMPOOPERACAO"].ToString(), numberFormatInfo) * Decimal.Parse(QtPlan.ToString(), numberFormatInfo))));
                                break;
                            case "3": //INTERVALO
                                DataTable dt3 = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PROTEIROPORINTERVALO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPERACAO = ? AND SEQOPERACAO = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, linha["CODOPERACAO"].ToString(), linha["SEQOPERACAO"].ToString(), codEstrutura, codRevEstrutura });
                                if (dt3.Rows.Count > 0)
                                {
                                    DataRow[] rows = dt3.Select("FAIXAINICIAL < '" + Convert.ToInt16(Math.Ceiling(Convert.ToDecimal(QtPlan.ToString()))) + "' AND FAIXAFINAL >= '" + Convert.ToInt16(Math.Ceiling(Convert.ToDecimal(QtPlan.ToString()))) + "'");

                                    if (rows.Count() == 0)
                                    {
                                        v.Set("TEMPOOPERACAO", 0);
                                        throw new Exception("Processo Cancelado!, Quantidade da ordem de produção ultrapassa limites das faixas.");
                                    }
                                    else
                                    {
                                        v.Set("TEMPOOPERACAO", Convert.ToDecimal(rows[0]["TEMPOOPERACAO"]).ToString());
                                    }
                                }
                                break;
                            default:
                                v.Set("TEMPOOPERACAO", 0);
                                break;
                        }

                        v.Set("TEMPOTOTAL", Convert.ToDecimal(v.Get("TEMPOSETUP")) + Convert.ToDecimal(v.Get("TEMPOOPERACAO")) + Convert.ToDecimal(v.Get("TEMPOEXTRA")));

                        v.Save();

                        SalvarRecurso(conn, Convert.ToDecimal(v.Get("TEMPOTOTAL").ToString()), linha["CODOPERACAO"].ToString(), Convert.ToInt16(linha["SEQOPERACAO"]).ToString("000"), codEstrutura, codRevEstrutura, codOrdem, seqOrdem, QtPlan);

                        _salvarRoteiro = true;
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //conn.Rollback();
                throw;
            }

            return _salvarRoteiro;
        }

        private void gridOperacao_Click(object sender, EventArgs e)
        {
            carregaDadosAbaRecurso();
        }

        private void carregaDadosAbaRecurso()
        {
            try
            {
                DataRow row1 = gridViewOperacao.GetDataRow(Convert.ToInt32(gridViewOperacao.GetSelectedRows().GetValue(0).ToString()));

                string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PORDEMROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? AND CODOPERACAO = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString(), codOperacao, codOrdem, seqOrdem });
                if (dt.Rows.Count > 0)
                {
                    lookupoperacao.txtcodigo.Text = dt.Rows[0]["CODOPERACAO"].ToString();
                    lookupoperacao.CarregaDescricao();

                    lookupcentrotrabalho.txtcodigo.Text = dt.Rows[0]["CODCTRABALHO"].ToString();
                    lookupcentrotrabalho.CarregaDescricao();

                    cmbRoteiroStatus.SelectedValue = dt.Rows[0]["STATUSOPERACAO"].ToString();

                    txtRoteiroReal.Text = dt.Rows[0]["QTDEREAL"].ToString();
                    txtRoteiroPlanejada.Text = dt.Rows[0]["QTDEPLANEJADA"].ToString();

                    txtRoteiroPorcCanc.Text = dt.Rows[0]["PERCCONCLUIDOROTEIRO"].ToString();

                    if (dt.Rows[0]["DATAINIPLAN"] == DBNull.Value)
                    {
                        txtRoteiroPlanejadaIni.Text = "";
                    }
                    else
                    {
                        txtRoteiroPlanejadaIni.Text = Convert.ToDateTime(dt.Rows[0]["DATAINIPLAN"].ToString()).ToShortDateString();
                    }

                    if (dt.Rows[0]["DATAFIMPLAN"] == DBNull.Value)
                    {
                        txtRoteiroPlanejadaFim.Text = "";
                    }
                    else
                    {
                        txtRoteiroPlanejadaFim.Text = Convert.ToDateTime(dt.Rows[0]["DATAFIMPLAN"].ToString()).ToShortDateString();
                    }

                    if (dt.Rows[0]["DATAINIREAL"] == DBNull.Value)
                    {
                        txtRoteiroRealizadaIni.Text = "";
                    }
                    else
                    {
                        txtRoteiroRealizadaIni.Text = Convert.ToDateTime(dt.Rows[0]["DATAINIREAL"].ToString()).ToShortDateString();
                    }

                    if (dt.Rows[0]["DATAFIMREAL"] == DBNull.Value)
                    {
                        txtRoteiroRealizadaFim.Text = "";
                    }
                    else
                    {
                        txtRoteiroRealizadaFim.Text = Convert.ToDateTime(dt.Rows[0]["DATAFIMREAL"].ToString()).ToShortDateString();
                    }

                    if (dt.Rows[0]["DATAINIORIG"] == DBNull.Value)
                    {
                        txtRoteiroOriginalIni.Text = "";
                    }
                    else
                    {
                        txtRoteiroOriginalIni.Text = Convert.ToDateTime(dt.Rows[0]["DATAINIORIG"].ToString()).ToShortDateString();
                    }

                    if (dt.Rows[0]["DATAFIMORIG"] == DBNull.Value)
                    {
                        txtRoteiroOriginalFim.Text = "";
                    }
                    else
                    {
                        txtRoteiroOriginalFim.Text = Convert.ToDateTime(dt.Rows[0]["DATAFIMORIG"].ToString()).ToShortDateString();
                    }

                    if (Convert.ToDecimal(dt.Rows[0]["TEMPOSETUP"].ToString()) > 0)
                    {
                        decimal horas = (Convert.ToDecimal(dt.Rows[0]["TEMPOSETUP"].ToString()) / 60);
                        decimal minutos = (Convert.ToDecimal(dt.Rows[0]["TEMPOSETUP"].ToString()) % 60);
                        txtRoteiroPlanejadoSetup.Text = Convert.ToInt16(horas).ToString("00") + ":" + Convert.ToInt16(minutos).ToString("00");
                    }
                    else
                    {
                        txtRoteiroPlanejadoSetup.Text = "00:00";
                    }

                    if (Convert.ToDecimal(dt.Rows[0]["TEMPOOPERACAO"].ToString()) > 0)
                    {
                        decimal horas = (Convert.ToDecimal(dt.Rows[0]["TEMPOOPERACAO"].ToString()) / 60);
                        decimal minutos = (Convert.ToDecimal(dt.Rows[0]["TEMPOOPERACAO"].ToString()) % 60);
                        txtRoteiroPlanejadoOperacao.Text = Convert.ToInt16(horas).ToString("00") + ":" + Convert.ToInt16(minutos).ToString("00");
                    }
                    else
                    {
                        txtRoteiroPlanejadoOperacao.Text = "00:00";
                    }

                    if (Convert.ToDecimal(dt.Rows[0]["TEMPOEXTRA"].ToString()) > 0)
                    {
                        decimal horas = (Convert.ToDecimal(dt.Rows[0]["TEMPOEXTRA"].ToString()) / 60);
                        decimal minutos = (Convert.ToDecimal(dt.Rows[0]["TEMPOEXTRA"].ToString()) % 60);
                        txtRoteiroPlanejadoExtra.Text = Convert.ToInt16(horas).ToString("00") + ":" + Convert.ToInt16(minutos).ToString("00");
                    }
                    else
                    {
                        txtRoteiroPlanejadoExtra.Text = "00:00";
                    }

                    if (Convert.ToDecimal(dt.Rows[0]["TEMPOTOTAL"].ToString()) > 0)
                    {
                        decimal horas = (Convert.ToDecimal(dt.Rows[0]["TEMPOTOTAL"].ToString()) / 60);
                        decimal minutos = (Convert.ToDecimal(dt.Rows[0]["TEMPOTOTAL"].ToString()) % 60);
                        txtRoteiroPlanejadoTotal.Text = Convert.ToInt16(horas).ToString("00") + ":" + Convert.ToInt16(minutos).ToString("00");
                    }
                    else
                    {
                        txtRoteiroPlanejadoTotal.Text = "00:00";
                    }

                    if (Convert.ToDecimal(dt.Rows[0]["TEMPOSETUPREAL"].ToString()) > 0)
                    {
                        decimal horas = (Convert.ToDecimal(dt.Rows[0]["TEMPOSETUPREAL"].ToString()) / 60);
                        decimal minutos = (Convert.ToDecimal(dt.Rows[0]["TEMPOSETUPREAL"].ToString()) % 60);
                        txtRoteiroRealizadoSetup.Text = Convert.ToInt16(horas).ToString("00") + ":" + Convert.ToInt16(minutos).ToString("00");
                    }
                    else
                    {
                        txtRoteiroRealizadoSetup.Text = "00:00";
                    }

                    if (Convert.ToDecimal(dt.Rows[0]["TEMPOOPERACAOREAL"].ToString()) > 0)
                    {
                        decimal horas = (Convert.ToDecimal(dt.Rows[0]["TEMPOOPERACAOREAL"].ToString()) / 60);
                        decimal minutos = (Convert.ToDecimal(dt.Rows[0]["TEMPOOPERACAOREAL"].ToString()) % 60);
                        txtRoteiroRealizadoOperacao.Text = Convert.ToInt16(horas).ToString("00") + ":" + Convert.ToInt16(minutos).ToString("00");
                    }
                    else
                    {
                        txtRoteiroRealizadoOperacao.Text = "00:00";
                    }

                    if (Convert.ToDecimal(dt.Rows[0]["TEMPOEXTRAREAL"].ToString()) > 0)
                    {
                        decimal horas = (Convert.ToDecimal(dt.Rows[0]["TEMPOEXTRAREAL"].ToString()) / 60);
                        decimal minutos = (Convert.ToDecimal(dt.Rows[0]["TEMPOEXTRAREAL"].ToString()) % 60);
                        txtRoteiroRealizadoExtra.Text = Convert.ToInt16(horas).ToString("00") + ":" + Convert.ToInt16(minutos).ToString("00");
                    }
                    else
                    {
                        txtRoteiroRealizadoExtra.Text = "00:00";
                    }

                    if (Convert.ToDecimal(dt.Rows[0]["TEMPOTOTALREAL"].ToString()) > 0)
                    {
                        decimal horas = (Convert.ToDecimal(dt.Rows[0]["TEMPOTOTALREAL"].ToString()) / 60);
                        decimal minutos = (Convert.ToDecimal(dt.Rows[0]["TEMPOTOTALREAL"].ToString()) % 60);
                        txtRoteiroRealizadoTotal.Text = Convert.ToInt16(horas).ToString("00") + ":" + Convert.ToInt16(minutos).ToString("00");
                    }
                    else
                    {
                        txtRoteiroRealizadoTotal.Text = "00:00";
                    }

                    CarregaGrid_OrdemResumo(codEstrutura, CodRevEstrutura);

                    CarregaGrid_Recursos(row1[0].ToString(), codOperacao);

                    CarregaGrid_Componentes(row1[0].ToString(), codOperacao);

                    CarregaGrid1_Apontamento(row1[0].ToString(), codOperacao);

                    CarregaGrid1_Entrada(row1[0].ToString(), codOperacao);

                    CarregaGrid1_Compra(row1[0].ToString(), codOperacao);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gridViewRecursosRecursos_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            try
            {
                GridView dView = gridViewRecursosRecursos.GetDetailView(e.RowHandle, 0) as GridView;

                dView.Columns["PORDEMALOCACAO.CODEMPRESA"].Visible = false;
                dView.Columns["PORDEMALOCACAO.CODFILIAL"].Visible = false;
                dView.Columns["PORDEMALOCACAO.CODESTRUTURA"].Visible = false;
                dView.Columns["PORDEMALOCACAO.REVESTRUTURA"].Visible = false;
                dView.Columns["PORDEMALOCACAO.CODIGOOP"].Visible = false;
                dView.Columns["PORDEMALOCACAO.SEQOP"].Visible = false;
                dView.Columns["PORDEMALOCACAO.SEQOPERACAO"].Visible = false;
                dView.Columns["PORDEMALOCACAO.CODOPERACAO"].Visible = false;
                dView.Columns["PORDEMALOCACAO.TIPORECURSO"].Visible = false;
                dView.Columns["PORDEMALOCACAO.CODGRUPORECURSO"].Visible = false;
                
                dView.Columns["PORDEMALOCACAO.CODRECURSO"].Caption = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA in (?) AND COLUNA = ?", new object[] { "PORDEMALOCACAO", "PORDEMALOCACAO.CODRECURSO" }).ToString();
                dView.Columns["PORDEMALOCACAO.TIPOALOC"].Caption = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA in (?) AND COLUNA = ?", new object[] { "PORDEMALOCACAO", "PORDEMALOCACAO.TIPOALOC" }).ToString();

                dView.Columns["PORDEMALOCACAO.DATAALOCACAO"].Caption = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA in (?) AND COLUNA = ?", new object[] { "PORDEMALOCACAO", "PORDEMALOCACAO.DATAALOCACAO" }).ToString();
                dView.Columns["PORDEMALOCACAO.TEMPOALOCACAO_HORA"].Caption = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA in (?) AND COLUNA = ?", new object[] { "PORDEMALOCACAO", "PORDEMALOCACAO.TEMPOALOCACAO" }).ToString();

                dView.BestFitColumns();

                dView.OptionsView.ShowAutoFilterRow = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void gridViewComponentes_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            try
            {
                GridView dView = gridViewComponentes.GetDetailView(e.RowHandle, 0) as GridView;

                dView.Columns["PORDEMCONSUMO.CODEMPRESA"].Visible = false;
                dView.Columns["PORDEMCONSUMO.CODFILIAL"].Visible = false;
                dView.Columns["PORDEMCONSUMO.CODESTRUTURA"].Visible = false;
                dView.Columns["PORDEMCONSUMO.REVESTRUTURA"].Visible = false;
                dView.Columns["PORDEMCONSUMO.CODIGOOP"].Visible = false;
                dView.Columns["PORDEMCONSUMO.SEQOP"].Visible = false;
                dView.Columns["PORDEMCONSUMO.SEQOPERACAO"].Visible = false;
                dView.Columns["PORDEMCONSUMO.CODOPERACAO"].Visible = false;
                dView.Columns["PORDEMCONSUMO.SEQOP"].Visible = false;
                dView.Columns["PORDEMCONSUMO.CODCOMPONENTE"].Visible = false;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA,DESCRICAO FROM GDICIONARIO WHERE TABELA in (?,?)", new object[] { "PORDEMCONSUMO", "VPRODUTO" });

                dView.Columns["PORDEMCONSUMO.TIPOCONSUMO"].Caption = dic.Select("COLUNA = 'PORDEMCONSUMO.TIPOCONSUMO'")[0]["DESCRICAO"].ToString();
                dView.Columns["PORDEMCONSUMO.QTDCOMPONENTE"].Caption = dic.Select("COLUNA = 'PORDEMCONSUMO.QTDCOMPONENTE'")[0]["DESCRICAO"].ToString();
                dView.Columns["PORDEMCONSUMO.CODCOMPONENTE"].Caption = dic.Select("COLUNA = 'PORDEMCONSUMO.CODCOMPONENTE'")[0]["DESCRICAO"].ToString();
                dView.Columns["PORDEMCONSUMO.UNDCOMPONENTE"].Caption = dic.Select("COLUNA = 'PORDEMCONSUMO.UNDCOMPONENTE'")[0]["DESCRICAO"].ToString();

                dView.BestFitColumns();

                dView.OptionsView.ShowAutoFilterRow = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (cmbStatus.SelectedValue.ToString() != "1")
            {
                MessageBox.Show("Esta Ordem de Produção não pode ser excluída", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                    conn.BeginTransaction();
                    try
                    {
                        if (seqOrdem == "001")
                        {

                            conn.ExecTransaction("DELETE FROM PORDEMENTRADA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODIGOOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codOrdem });
                            conn.ExecTransaction("DELETE FROM PORDEMCONSUMO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODIGOOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codOrdem });
                            conn.ExecTransaction("DELETE FROM PORDEMAPTORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODIGOOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codOrdem });
                            conn.ExecTransaction("DELETE FROM PORDEMAPONTAMENTO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODIGOOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codOrdem });
                            conn.ExecTransaction("DELETE FROM PORDEMALOCACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODIGOOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codOrdem });
                            conn.ExecTransaction("DELETE FROM PORDEMROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODIGOOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codOrdem });
                            conn.ExecTransaction("DELETE FROM PORDEM WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODIGOOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codOrdem });
                        }
                        else
                        {
                            conn.ExecTransaction("DELETE FROM PORDEMENTRADA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, codOrdem, seqOrdem });
                            conn.ExecTransaction("DELETE FROM PORDEMCONSUMO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, codOrdem, seqOrdem });
                            conn.ExecTransaction("DELETE FROM PORDEMAPTORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, codOrdem, seqOrdem });
                            conn.ExecTransaction("DELETE FROM PORDEMAPONTAMENTO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, codOrdem, seqOrdem });
                            conn.ExecTransaction("DELETE FROM PORDEMALOCACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, codOrdem, seqOrdem });
                            conn.ExecTransaction("DELETE FROM PORDEMROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, codOrdem, seqOrdem });
                            conn.ExecTransaction("DELETE FROM PORDEM WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, codOrdem, seqOrdem });
                        }
                        conn.Commit();
                        MessageBox.Show("Cadastro Excluído com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                    }
                    catch (Exception)
                    {
                        conn.Rollback();
                        throw;
                    }
                }
            }
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {

        }

        private string VerificaRecursoDisponivelAlocacao(AppLib.Data.Connection conn, DataRow drRecursos, DataRow drOperacao, string codGrupoRecurso)
        {
            string diaAlocacao = VerificaDiaAlocacaoAntiga(drOperacao, drRecursos);

            if (Convert.IsDBNull(diaAlocacao))
            {
                return null;
            }
            else
            {
                conn.ExecTransaction("UPDATE PORDEMALOCACAO SET CODRECURSO = ?, DATAALOCACAO = ? WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ? AND CODGRUPORECURSO = ?", new object[] { drRecursos["CODRECURSO"].ToString(), Convert.ToDateTime(diaAlocacao), AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, codOrdem, seqOrdem, codGrupoRecurso });
                return diaAlocacao;
            }
        }
        //private bool VerificaAlocacaoProximoRecurso(AppLib.Data.Connection conn, string diaAlocacao, DataRow drRecursos, DataRow drOperacao, string codGrupoRecurso)
        //{
        //    bool retorno = false;

        //    DataTable dtCalendario = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PCALENDARIOTRABALHO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCALEND = ? AND DIAPRODUTIVO = 1 AND DATA = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, drOperacao["CODCALEND"].ToString(), Convert.ToDateTime(diaAlocacao).ToString("yyyy-MM-dd") });

        //    if (dtCalendario.Rows.Count > 0)
        //    {
        //        ///////////

        //        int TotalHorasTotais = Convert.ToInt16(dtCalendario.Rows[0]["HORASDISPONIVEIS"].ToString()) + (Convert.IsDBNull(dtCalendario.Rows[0]["HORASEXTRAS"]) ? 0 : Convert.ToInt16(dtCalendario.Rows[0]["HORASEXTRAS"].ToString()));

        //        int TotalHorasDisponiveis = TotalHorasTotais;

        //        int TempoAolocacao = Convert.ToInt16(drOperacao["TEMPOTOTAL"].ToString());

        //        DataTable dtAlocacaoRecurso = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PORDEMALOCACAO WITH (NOLOCK) WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ? AND CODOPERACAO = ? AND SEQOPERACAO = ? AND CODRECURSO = ? AND DATAALOCACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, codOrdem, seqOrdem, drOperacao["CODOPERACAO"].ToString(), drOperacao["SEQOPERACAO"].ToString(), drRecursos["CODRECURSO"].ToString(), Convert.ToDateTime(dtCalendario.Rows[0]["DATA"].ToString()).ToString("yyyy-MM-dd") });
        //        if (dtAlocacaoRecurso.Rows.Count > 0)
        //        {
        //            //VERIFICAR HORAS DISPONIVEIS MAIOR QUE O TEMPO NECESSARIO PARA O DIA
        //            if ((TotalHorasDisponiveis - TempoAolocacao) > 0)
        //            {
        //                //DIA DISPONIVEL PARA ALOCACAO
        //                conn.ExecTransaction("UPDATE PORDEMALOCACAO SET CODRECURSO = ?, DATAALOCACAO = ? WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ? AND CODGRUPORECURSO = ?", new object[] { drRecursos["CODRECURSO"].ToString(), Convert.ToDateTime(diaAlocacao), AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, codOrdem, seqOrdem, codGrupoRecurso });
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            if ((TotalHorasTotais - TempoAolocacao) > 0)
        //            {
        //                //DIA DISPONIVEL PARA ALOCACAO
        //                conn.ExecTransaction("UPDATE PORDEMALOCACAO SET CODRECURSO = ?, DATAALOCACAO = ? WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ? AND CODGRUPORECURSO = ?", new object[] { drRecursos["CODRECURSO"].ToString(), Convert.ToDateTime(diaAlocacao), AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, codOrdem, seqOrdem, codGrupoRecurso });
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }

        //        //if (x == (dtCalendario.Rows.Count - 1))
        //        //{
        //        //    //VERIFICAR PROXIMO ANO DE CALENDARIO
        //        //}

        //        ///////////
        //    }
        //    else
        //    {
        //        retorno = false;
        //    }

        //    return retorno;
        //}

        private DataTable VerificaDiaCalendarioAlocacao(string calendario, string grupo, string recurso)
        {
            DataTable dtCalendario = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
            SELECT
	              DATA, CODRECURSO,CODGRUPORECURSO,TEMPODISPONIVEL AS TEMPODISPONIVELCALENDARIO,TEMPOTOTALDISPONIVEL 
	              FROM 
	              (
		              SELECT CAT.DATA
				             ,REC.CODRECURSO
				             ,REC.CODGRUPORECURSO
				             ,(ISNULL(CAT.HORASDISPONIVEIS,0) + ISNULL(CAT.HORASEXTRAS,0)) AS TEMPOTOTALDISPONIVEL
				             , ISNULL(
				              (SELECT CASE WHEN ((ISNULL(CAT.HORASDISPONIVEIS,0) + ISNULL(CAT.HORASEXTRAS,0)) - ISNULL(ALC.TEMPOALOCACAO,0)) <= 0
									            THEN 0
							               WHEN ((ISNULL(CAT.HORASDISPONIVEIS,0) + ISNULL(CAT.HORASEXTRAS,0)) - ISNULL(ALC.TEMPOALOCACAO,0)) > 0 
									            THEN ((ISNULL(HORASDISPONIVEIS,0) + ISNULL(HORASEXTRAS,0)) - ISNULL(ALC.TEMPOALOCACAO,0))
						              END
				              FROM PORDEMALOCACAO ALC WITH (NOLOCK)
				              WHERE ALC.CODEMPRESA = REC.CODEMPRESA
						            AND ALC.CODFILIAL = REC.CODFILIAL
						            AND ALC.CODRECURSO = REC.CODRECURSO
						            AND ALC.CODGRUPORECURSO = REC.CODGRUPORECURSO
						            AND ALC.DATAALOCACAO = CAT.DATA 
					             ),(ISNULL(CAT.HORASDISPONIVEIS,0) + ISNULL(CAT.HORASEXTRAS,0))) AS TEMPODISPONIVEL
			            FROM PCALENDARIOTRABALHO CAT  WITH (NOLOCK) JOIN PRECURSO REC  WITH (NOLOCK) ON REC.CODEMPRESA = CAT.CODEMPRESA 
														              AND REC.CODFILIAL = CAT.CODFILIAL
		               WHERE CAT.CODEMPRESA = ? 
			             AND CAT.CODFILIAL = ? 
			             AND CAT.CODCALEND = '" + calendario + @"' 
			             AND CAT.DIAPRODUTIVO = 1 
			             AND DATA > GETDATE()
			             AND REC.ATIVO = 1
			             AND REC.CODGRUPORECURSO = '" + grupo + @"'
                         AND REC.CODRECURSO = '" + recurso + @"'
		            ) TOT
		            WHERE  TEMPODISPONIVEL <= TEMPOTOTALDISPONIVEL
		             AND   TEMPODISPONIVEL > 0
	               ORDER BY DATA
            ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial });

            if (dtCalendario.Rows.Count > 0)
                return dtCalendario;
            else
            {
                return null;
            }
        }

        private Hashtable VerificaDiaAlocacao(string calendario, string grupo, string top = " TOP 1 ")
        {
            DataTable dtCalendario = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
            SELECT " + top + @"
	              MIN(DATA) as DATA, CODRECURSO,CODGRUPORECURSO,TEMPODISPONIVEL AS TEMPODISPONIVELCALENDARIO,TEMPOTOTALDISPONIVEL 
	              FROM 
	              (
		              SELECT CAT.DATA
				             ,REC.CODRECURSO
				             ,REC.CODGRUPORECURSO
				             ,(ISNULL(CAT.HORASDISPONIVEIS,0) + ISNULL(CAT.HORASEXTRAS,0)) AS TEMPOTOTALDISPONIVEL
				             , ISNULL(
				              (SELECT CASE WHEN ((ISNULL(CAT.HORASDISPONIVEIS,0) + ISNULL(CAT.HORASEXTRAS,0)) - ISNULL(ALC.TEMPOALOCACAO,0)) <= 0
									            THEN 0
							               WHEN ((ISNULL(CAT.HORASDISPONIVEIS,0) + ISNULL(CAT.HORASEXTRAS,0)) - ISNULL(ALC.TEMPOALOCACAO,0)) > 0 
									            THEN ((ISNULL(HORASDISPONIVEIS,0) + ISNULL(HORASEXTRAS,0)) - ISNULL(ALC.TEMPOALOCACAO,0))
						              END
				              FROM PORDEMALOCACAO ALC WITH (NOLOCK) 
				              WHERE ALC.CODEMPRESA = REC.CODEMPRESA
						            AND ALC.CODFILIAL = REC.CODFILIAL
						            AND ALC.CODRECURSO = REC.CODRECURSO
						            AND ALC.CODGRUPORECURSO = REC.CODGRUPORECURSO
						            AND ALC.DATAALOCACAO = CAT.DATA 
					             ),(ISNULL(CAT.HORASDISPONIVEIS,0) + ISNULL(CAT.HORASEXTRAS,0))) AS TEMPODISPONIVEL
			            FROM PCALENDARIOTRABALHO CAT  WITH (NOLOCK) JOIN PRECURSO REC WITH (NOLOCK) ON REC.CODEMPRESA = CAT.CODEMPRESA 
														              AND REC.CODFILIAL = CAT.CODFILIAL
		               WHERE CAT.CODEMPRESA = ? 
			             AND CAT.CODFILIAL = ? 
			             AND CAT.CODCALEND = '" + calendario + @"' 
			             AND CAT.DIAPRODUTIVO = 1 
			             AND DATA > GETDATE()
			             AND REC.ATIVO = 1
			             AND REC.CODGRUPORECURSO = '" + grupo + @"'
		            ) TOT
		            WHERE  TEMPODISPONIVEL <= TEMPOTOTALDISPONIVEL
		             AND   TEMPODISPONIVEL > 0
	               GROUP BY CODRECURSO,CODGRUPORECURSO,TEMPODISPONIVEL,TEMPOTOTALDISPONIVEL
	               ORDER BY MIN(DATA), TEMPOTOTALDISPONIVEL DESC
            ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial });

            if (dtCalendario.Rows.Count > 0)
            {
                Hashtable item = new Hashtable();
                item.Add("DATA", dtCalendario.Rows[0]["DATA"].ToString());
                item.Add("CODRECURSO", dtCalendario.Rows[0]["CODRECURSO"].ToString());

                return item;
            }
            else
            {
                return null;
            }
        }
        private string VerificaDiaAlocacaoAntiga(DataRow drOperacao, DataRow drRecursos)
        {
            DataTable dtCalendario = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PCALENDARIOTRABALHO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODCALEND = ? AND DIAPRODUTIVO = 1 AND DATA > GETDATE() ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, drOperacao["CODCALEND"].ToString() });

            if (dtCalendario.Rows.Count > 0)
            {
                for (int x = 0; x <= (dtCalendario.Rows.Count - 1); x++)
                {
                    int HorasTotais = Convert.ToInt16(dtCalendario.Rows[x]["HORASDISPONIVEIS"].ToString()) + (Convert.IsDBNull(dtCalendario.Rows[x]["HORASEXTRAS"]) ? 0 : Convert.ToInt16(dtCalendario.Rows[x]["HORASEXTRAS"].ToString()));

                    int TotalHorasDisponiveis = HorasTotais;

                    int TempoAolocacao = Convert.ToInt16(drOperacao["TEMPOTOTAL"].ToString());

                    DataTable dtAlocacaoRecurso = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PORDEMALOCACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ? AND CODOPERACAO = ? AND SEQOPERACAO = ? AND CODRECURSO = ? AND DATAALOCACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, codOrdem, seqOrdem, drOperacao["CODOPERACAO"].ToString(), drOperacao["SEQOPERACAO"].ToString(), drRecursos["CODRECURSO"].ToString(), Convert.ToDateTime(dtCalendario.Rows[x]["DATA"].ToString()).ToString("yyyy-MM-dd") });
                    if (dtAlocacaoRecurso.Rows.Count > 0)
                    {
                        //VERIFICAR HORAS DISPONIVEIS MAIOR QUE O TEMPO NECESSARIO PARA O DIA
                        if ((TotalHorasDisponiveis - TempoAolocacao) > 0)
                        {
                            //DIA DISPONIVEL PARA ALOCACAO
                            return dtCalendario.Rows[x]["DATA"].ToString();
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if ((HorasTotais - TempoAolocacao) > 0)
                        {
                            //DIA DISPONIVEL PARA ALOCACAO
                            return dtCalendario.Rows[x]["DATA"].ToString();
                        }
                        else
                        {
                            continue;
                        }
                    }

                    //if (x == (dtCalendario.Rows.Count - 1))
                    //{
                    //    //VERIFICAR PROXIMO ANO DE CALENDARIO
                    //}
                }
            }
            return null;
        }

        private void processoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dtOperacao = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PORDEMROTEIRO  A   JOIN PCENTROTRABALHO B ON A.CODCTRABALHO = B.CODCTRABALHO
										                                                                                                      AND A.CODEMPRESA = B.CODEMPRESA
											                                                                                                AND A.CODFILIAL = B.CODFILIAL
						                                                                                            JOIN PCENTROTRABALHOCALENDARIO C ON A.CODCTRABALHO = C.CODCTRABALHO
														                                                                                            AND A.CODEMPRESA = C.CODEMPRESA
														                                                                                            AND A.CODFILIAL = C.CODFILIAL
						                                                                                            JOIN PCALENDARIO D ON C.CODEMPRESA = D.CODEMPRESA
										                                                                                                AND C.CODFILIAL = D.CODFILIAL
										                                                                                                AND C.CODCALEND = D.CODCALEND
                                                                                          WHERE A.CODEMPRESA = ? 
                                                                                            AND A. CODFILIAL = ? 
                                                                                            AND A.CODESTRUTURA = ? 
                                                                                            AND A.REVESTRUTURA = ? 
                                                                                            AND A.CODIGOOP = ? 
                                                                                            AND A.SEQOP = ? 
                                                                                            AND D.ANOCALEND = YEAR(GETDATE())", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, codOrdem, seqOrdem });

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();

            try
            {
                if (dtOperacao.Rows.Count > 0)
                {
                    Hashtable PrimeirodiaAlocacao = null;
                    foreach (DataRow drOperacao in dtOperacao.Rows)
                    {
                        //string codGrupoRecurso = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODGRUPORECURSO FROM PORDEMALOCACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?  AND CODOPERACAO = ? AND SEQOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, codOrdem, seqOrdem, drOperacao["CODOPERACAO"].ToString(), drOperacao["SEQOPERACAO"].ToString() }).ToString();
                        DataTable dtAlocacao = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PORDEMALOCACAO WITH (NOLOCK) WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?  AND CODOPERACAO = ? AND SEQOPERACAO = ? AND TIPOALOC = 0", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, codOrdem, seqOrdem, drOperacao["CODOPERACAO"].ToString(), drOperacao["SEQOPERACAO"].ToString() });
                        if (dtAlocacao.Rows.Count > 0)
                        {
                            Hashtable DatasNecessarias = new Hashtable();
                            int controlaIDDatasNecessarias = 0;

                            for (int y = 0; y <= (dtAlocacao.Rows.Count - 1); y++)
                            {
                                DataTable dtComponentes = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PORDEMCONSUMO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?  AND CODOPERACAO = ? AND SEQOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, codOrdem, seqOrdem, drOperacao["CODOPERACAO"].ToString(), drOperacao["SEQOPERACAO"].ToString() });

                                if (y == 0)
                                {
                                    PrimeirodiaAlocacao = VerificaDiaAlocacao(drOperacao["CODCALEND"].ToString(), dtAlocacao.Rows[y]["CODGRUPORECURSO"].ToString());

                                    if (Convert.IsDBNull(PrimeirodiaAlocacao))
                                    {
                                        MessageBox.Show("Não foi possivel alocar o recurso", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    else
                                    {
                                        DataTable DtCalendario = VerificaDiaCalendarioAlocacao(drOperacao["CODCALEND"].ToString(), dtAlocacao.Rows[y]["CODGRUPORECURSO"].ToString(), PrimeirodiaAlocacao["CODRECURSO"].ToString());

                                        int TotalHorasNecessarias = Convert.ToInt16(drOperacao["TEMPOTOTAL"].ToString());

                                        if (DtCalendario.Rows.Count > 0)
                                        {
                                            for (int c = 0; c <= (DtCalendario.Rows.Count - 1); c++)
                                            {

                                                if (Convert.ToInt16(DtCalendario.Rows[c]["TEMPODISPONIVELCALENDARIO"].ToString()) >= TotalHorasNecessarias)
                                                {
                                                    DatasNecessarias.Add(controlaIDDatasNecessarias, Convert.ToDateTime(DtCalendario.Rows[c]["DATA"].ToString()));
                                                    controlaIDDatasNecessarias = controlaIDDatasNecessarias + 1;

                                                    Incluir_PORDEMALOCACAO(conn, dtAlocacao.Rows[y]["CODESTRUTURA"].ToString(), Convert.ToInt16(dtAlocacao.Rows[y]["REVESTRUTURA"].ToString()), dtAlocacao.Rows[y]["CODIGOOP"].ToString(), dtAlocacao.Rows[y]["SEQOP"].ToString(), dtAlocacao.Rows[y]["SEQOPERACAO"].ToString(), dtAlocacao.Rows[y]["CODOPERACAO"].ToString(), Convert.ToInt16(dtAlocacao.Rows[y]["TIPORECURSO"].ToString()), 1, Convert.ToDateTime(DtCalendario.Rows[c]["DATA"].ToString()), TotalHorasNecessarias, dtAlocacao.Rows[y]["CODGRUPORECURSO"].ToString(), PrimeirodiaAlocacao["CODRECURSO"].ToString());
                                                    break;
                                                }
                                                else
                                                {
                                                    DatasNecessarias.Add(controlaIDDatasNecessarias, Convert.ToDateTime(DtCalendario.Rows[c]["DATA"].ToString()));
                                                    controlaIDDatasNecessarias = controlaIDDatasNecessarias + 1;

                                                    TotalHorasNecessarias = (TotalHorasNecessarias - Convert.ToInt16(DtCalendario.Rows[c]["TEMPODISPONIVELCALENDARIO"].ToString()));
                                                    Incluir_PORDEMALOCACAO(conn, dtAlocacao.Rows[y]["CODESTRUTURA"].ToString(), Convert.ToInt16(dtAlocacao.Rows[y]["REVESTRUTURA"].ToString()), dtAlocacao.Rows[y]["CODIGOOP"].ToString(), dtAlocacao.Rows[y]["SEQOP"].ToString(), dtAlocacao.Rows[y]["SEQOPERACAO"].ToString(), dtAlocacao.Rows[y]["CODOPERACAO"].ToString(), Convert.ToInt16(dtAlocacao.Rows[y]["TIPORECURSO"].ToString()), 1, Convert.ToDateTime(DtCalendario.Rows[c]["DATA"].ToString()), Convert.ToInt16(DtCalendario.Rows[c]["TEMPODISPONIVELCALENDARIO"].ToString()), dtAlocacao.Rows[y]["CODGRUPORECURSO"].ToString(), PrimeirodiaAlocacao["CODRECURSO"].ToString());
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Hashtable PrimeirodiaAlocacaoProximoRecurso = VerificaDiaAlocacao(drOperacao["CODCALEND"].ToString(), dtAlocacao.Rows[y]["CODGRUPORECURSO"].ToString());

                                    int ParametroAlocacao = VerificaAlocacaoRecursoMesmoDia();

                                    if (Convert.IsDBNull(PrimeirodiaAlocacaoProximoRecurso) && ParametroAlocacao == 1)
                                    {
                                        MessageBox.Show("Não foi possivel alocar o recurso", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        conn.Rollback();
                                        return;
                                    }
                                    else
                                    {
                                        DataTable DtCalendario = VerificaDiaCalendarioAlocacao(drOperacao["CODCALEND"].ToString(), dtAlocacao.Rows[y]["CODGRUPORECURSO"].ToString(), PrimeirodiaAlocacaoProximoRecurso["CODRECURSO"].ToString());

                                        int TotalHorasNecessarias = Convert.ToInt16(drOperacao["TEMPOTOTAL"].ToString());

                                        //VERIFICA PARAMETRO SE RECURSO PODE SER ALOCADO EM DIA DIFERENTE
                                        if (ParametroAlocacao == 1) //NAO PODE SER EM DIA DIFERENTE
                                        {
                                            if (PrimeirodiaAlocacaoProximoRecurso["DATA"].ToString() != PrimeirodiaAlocacao["DATA"].ToString())
                                            {
                                                MessageBox.Show("Não foi possivel alocar o recurso", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                conn.Rollback();
                                                return;
                                            }
                                            else
                                            {
                                                if (DtCalendario.Rows.Count > 0)
                                                {
                                                    for (int dt = 0; dt <= (DatasNecessarias.Count - 1); dt++)
                                                    {
                                                        DataRow[] row = DtCalendario.Select("DATA = '" + Convert.ToDateTime(DatasNecessarias[dt].ToString()).ToString("yyyy-MM-dd") + "'");

                                                        if (row.Count() > 0)
                                                        {
                                                            if (Convert.ToInt16(row[0]["TEMPODISPONIVELCALENDARIO"].ToString()) >= TotalHorasNecessarias)
                                                            {
                                                                Incluir_PORDEMALOCACAO(conn, dtAlocacao.Rows[y]["CODESTRUTURA"].ToString(), Convert.ToInt16(dtAlocacao.Rows[y]["REVESTRUTURA"].ToString()), dtAlocacao.Rows[y]["CODIGOOP"].ToString(), dtAlocacao.Rows[y]["SEQOP"].ToString(), dtAlocacao.Rows[y]["SEQOPERACAO"].ToString(), dtAlocacao.Rows[y]["CODOPERACAO"].ToString(), Convert.ToInt16(dtAlocacao.Rows[y]["TIPORECURSO"].ToString()), 1, Convert.ToDateTime(DtCalendario.Rows[dt]["DATA"].ToString()), TotalHorasNecessarias, dtAlocacao.Rows[y]["CODGRUPORECURSO"].ToString(), PrimeirodiaAlocacaoProximoRecurso["CODRECURSO"].ToString());
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                TotalHorasNecessarias = (TotalHorasNecessarias - Convert.ToInt16(DtCalendario.Rows[dt]["TEMPODISPONIVELCALENDARIO"].ToString()));
                                                                Incluir_PORDEMALOCACAO(conn, dtAlocacao.Rows[y]["CODESTRUTURA"].ToString(), Convert.ToInt16(dtAlocacao.Rows[y]["REVESTRUTURA"].ToString()), dtAlocacao.Rows[y]["CODIGOOP"].ToString(), dtAlocacao.Rows[y]["SEQOP"].ToString(), dtAlocacao.Rows[y]["SEQOPERACAO"].ToString(), dtAlocacao.Rows[y]["CODOPERACAO"].ToString(), Convert.ToInt16(dtAlocacao.Rows[y]["TIPORECURSO"].ToString()), 1, Convert.ToDateTime(DtCalendario.Rows[dt]["DATA"].ToString()), Convert.ToInt16(DtCalendario.Rows[dt]["TEMPODISPONIVELCALENDARIO"].ToString()), dtAlocacao.Rows[y]["CODGRUPORECURSO"].ToString(), PrimeirodiaAlocacao["CODRECURSO"].ToString());
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Não foi possivel alocar o recurso", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            conn.Rollback();
                                                            return;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (DtCalendario.Rows.Count > 0)
                                            {
                                                for (int dt = 0; dt <= (DtCalendario.Rows.Count - 1); dt++)
                                                {
                                                    if (Convert.ToInt16(DtCalendario.Rows[dt]["TEMPODISPONIVELCALENDARIO"].ToString()) >= TotalHorasNecessarias)
                                                    {
                                                        Incluir_PORDEMALOCACAO(conn, dtAlocacao.Rows[y]["CODESTRUTURA"].ToString(), Convert.ToInt16(dtAlocacao.Rows[y]["REVESTRUTURA"].ToString()), dtAlocacao.Rows[y]["CODIGOOP"].ToString(), dtAlocacao.Rows[y]["SEQOP"].ToString(), dtAlocacao.Rows[y]["SEQOPERACAO"].ToString(), dtAlocacao.Rows[y]["CODOPERACAO"].ToString(), Convert.ToInt16(dtAlocacao.Rows[y]["TIPORECURSO"].ToString()), 1, Convert.ToDateTime(DtCalendario.Rows[dt]["DATA"].ToString()), TotalHorasNecessarias, dtAlocacao.Rows[y]["CODGRUPORECURSO"].ToString(), PrimeirodiaAlocacaoProximoRecurso["CODRECURSO"].ToString());
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        TotalHorasNecessarias = (TotalHorasNecessarias - Convert.ToInt16(DtCalendario.Rows[dt]["TEMPODISPONIVELCALENDARIO"].ToString()));
                                                        Incluir_PORDEMALOCACAO(conn, dtAlocacao.Rows[y]["CODESTRUTURA"].ToString(), Convert.ToInt16(dtAlocacao.Rows[y]["REVESTRUTURA"].ToString()), dtAlocacao.Rows[y]["CODIGOOP"].ToString(), dtAlocacao.Rows[y]["SEQOP"].ToString(), dtAlocacao.Rows[y]["SEQOPERACAO"].ToString(), dtAlocacao.Rows[y]["CODOPERACAO"].ToString(), Convert.ToInt16(dtAlocacao.Rows[y]["TIPORECURSO"].ToString()), 1, Convert.ToDateTime(DtCalendario.Rows[dt]["DATA"].ToString()), Convert.ToInt16(DtCalendario.Rows[dt]["TEMPODISPONIVELCALENDARIO"].ToString()), dtAlocacao.Rows[y]["CODGRUPORECURSO"].ToString(), PrimeirodiaAlocacao["CODRECURSO"].ToString());
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                conn.Commit();
                MessageBox.Show("ACABOU");
            }
            catch (Exception ex)
            {
                conn.Rollback();
                throw;
            }
        }

        private void verificaVFICHAESTOQUE(string produto)
        {
            try
            {
                string Param_LocalEstoqueMP = VerificaParametro("LOCALESTOQUEMP");

                if (!string.IsNullOrEmpty(Param_LocalEstoqueMP))
                {
                    DataTable dtRecursivo = sqlOrdemProducaoEstrutura(codOrdem);

                    List<string> _Listacomponentes = new List<string>();

                    for(int x =0; x <= (dtRecursivo.Rows.Count -1); x++)
                    {
                        if (!_Listacomponentes.Contains(dtRecursivo.Rows[x]["CODCOMPONENTE"].ToString()))
                        {
                            _Listacomponentes.Add(dtRecursivo.Rows[x]["CODCOMPONENTE"].ToString());
                        }

                        if (!_Listacomponentes.Contains(dtRecursivo.Rows[x]["CODESTRUTURA"].ToString()))
                        {
                            _Listacomponentes.Add(dtRecursivo.Rows[x]["CODESTRUTURA"].ToString());
                        }
                    }

                    string _componentes = "";

                    for (int x = 0; x <= (_Listacomponentes.Count() - 1); x++)
                    {
                        _componentes = _componentes + (x == 0 ? "'" + _Listacomponentes[x].ToString() + "'" : ",'" + _Listacomponentes[x].ToString() + "'");
                    }

                    DataTable dtFichaEstoque = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
                        with estoque as (
                                           select CODEMPRESA, CODPRODUTO, CODLOCAL, SEQUENCIAL, SALDOFINAL
                                           , RowDesc = ROW_NUMBER() over(partition by CODPRODUTO order by SEQUENCIAL desc)
                                             from VFICHAESTOQUE
                                            where CODEMPRESA = ?
                                              and CODLOCAL = ?
                                              and CODFILIAL = ?" +
                            (string.IsNullOrEmpty(_componentes) ? "" : "AND CODPRODUTO in (" + _componentes + ")") + @"
                                         )
                        select * from estoque where RowDesc = 1"
                    , new object[] { AppLib.Context.Empresa, Param_LocalEstoqueMP, AppLib.Context.Filial });

                    FrmOrdemProducaoNecessidadeMP frm = new FrmOrdemProducaoNecessidadeMP(codOrdem, dtFichaEstoque, _Listacomponentes);
                    frm.ShowDialog();
                }
                else
                {
                    throw new Exception("Parâmetro Inválido: Local Estoque Matéria Prima");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void planejarOrdemDeProduçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cmbStatus.SelectedValue == "1")
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                conn.BeginTransaction();

                try
                {
                    verificaVFICHAESTOQUE(codEstrutura);
                }
                catch (Exception ex)
                {
                    conn.Rollback();
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Esta ordem de produção não pode ser planejada devido ao status", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ConverteMinutoemHora(string tempo)
        {
            string _hora = "";
            if (Convert.ToDecimal(tempo.ToString()) > 0)
            {
                decimal horas = (Convert.ToDecimal(tempo.ToString()) / 60);
                decimal minutos = (Convert.ToDecimal(tempo.ToString()) % 60);
                _hora = Convert.ToInt16(horas).ToString("00") + ":" + Convert.ToInt16(minutos).ToString("00");
            }
            else
            {
                _hora = "00:00";
            }
            return _hora;
        }
        private void gridViewResumo_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            try
            {
                switch (e.Column.FieldName.ToString())
                {
                    case "PORDEMROTEIRO.TEMPOSETUP":
                        e.DisplayText = ConverteMinutoemHora(e.Value.ToString());
                        break;
                    case "PORDEMROTEIRO.TEMPOOPERACAO":
                        e.DisplayText = ConverteMinutoemHora(e.Value.ToString());
                        break;
                    case "PORDEMROTEIRO.TEMPOEXTRA":
                        e.DisplayText = ConverteMinutoemHora(e.Value.ToString());
                        break;
                    case "PORDEMROTEIRO.TEMPOTOTAL":
                        e.DisplayText = ConverteMinutoemHora(e.Value.ToString());
                        break;
                    case "PORDEMROTEIRO.TEMPOSETUPREAL":
                        e.DisplayText = ConverteMinutoemHora(e.Value.ToString());
                        break;
                    case "PORDEMROTEIRO.TEMPOOPERACAOREAL":
                        e.DisplayText = ConverteMinutoemHora(e.Value.ToString());
                        break;
                    case "PORDEMROTEIRO.TEMPOEXTRAREAL":
                        e.DisplayText = ConverteMinutoemHora(e.Value.ToString());
                        break;
                    case "PORDEMROTEIRO.TEMPOTOTALREAL":
                        e.DisplayText = ConverteMinutoemHora(e.Value.ToString());
                        break;
                }
            }
            catch (Exception)
            {

            }
        }

        private void gridViewRecursosRecursos_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {

        }

        private void gerarEntradasDaOPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOrdemProducaoEntradas frm = new FrmOrdemProducaoEntradas(txtNroOp.Text,txtSeqOp.Text,codEstrutura,CodRevEstrutura);
            frm.ShowDialog();
        }
    }
}

