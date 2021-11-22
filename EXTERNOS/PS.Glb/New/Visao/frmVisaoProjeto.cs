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
using PS.Glb.Class;

namespace PS.Glb.New.Visao
{
    public partial class frmVisaoProjeto : Form
    {
        string Condicao = String.Empty;
        string sql = String.Empty;
        string tabela = "APROJETO";

        public frmVisaoProjeto(String _Condicao)
        {
            InitializeComponent();
            Condicao = _Condicao;
        }

        private void AtualizaGrid()
        {
            try
            {
                sql = String.Format(@"SELECT APROJETO.CODEMPRESA,
	                                     APROJETO.CODFILIAL,
	                                     APROJETO.IDPROJETO, 
	                                     APROJETO.IDUNIDADE,
	                                     AUNIDADE.NOME,
	                                     APROJETO.DESCRICAO,
										 GUSUARIO.NOME AS 'COODENADOR',
	                                     APROJETO.ESCOPO,
	                                     APROJETO.DATACRIACAO,
	                                     APROJETO.CODUSUARIOPRESTADOR,
	                                     APROJETO.CODUSUARIOCLIENTE,
	                                     APROJETO.VALORHORA,
	                                     APROJETO.NIVEL,
	                                     APROJETO.DATACONCLUSAO,
	                                     APROJETO.STATUS,
										 APT.PREVISAOHORAS,
										 ISNULL(AATD.REALIZADAS_DEMANDA,0) AS 'REALIZADAS_DEMANDA',
										 ISNULL(AATP.REALIZADAS_PROJETO,0) AS 'REALIZADAS_PROJETO'
										 
                                  FROM APROJETO

                                  INNER JOIN AUNIDADE
                                  ON AUNIDADE.CODEMPRESA = APROJETO.CODEMPRESA
                                  AND AUNIDADE.IDUNIDADE = APROJETO.IDUNIDADE

								  INNER JOIN GUSUARIO
								  ON GUSUARIO.CODUSUARIO = APROJETO.CODUSUARIOPRESTADOR

								  LEFT JOIN 
								  (SELECT IDPROJETO, 
								  SUM(PREVISAOHORAS) AS 'PREVISAOHORAS'
								  FROM APROJETOTAREFA
								  GROUP BY IDPROJETO) APT
								  ON APT.IDPROJETO = APROJETO.IDPROJETO

								  LEFT JOIN 
								  (SELECT IDPROJETO, SUM(DATEDIFF_BIG(SECOND, '', ISNULL(HORAS,0)))/3600 AS 'REALIZADAS_DEMANDA' 
								  FROM AAPONTAMENTOTAREFA

								  INNER JOIN APROJETOTAREFA
								  ON APROJETOTAREFA.IDTAREFA = AAPONTAMENTOTAREFA.IDTAREFA
								  
								  WHERE TIPOFATURAMENTO = 'D'

								  GROUP BY IDPROJETO) AATD
								  ON AATD.IDPROJETO = APROJETO.IDPROJETO

								  LEFT JOIN 
								  (SELECT IDPROJETO, SUM(DATEDIFF_BIG(SECOND, '', ISNULL(HORAS,0)))/3600 AS 'REALIZADAS_PROJETO' 
								  FROM AAPONTAMENTOTAREFA

								  INNER JOIN APROJETOTAREFA
								  ON APROJETOTAREFA.IDTAREFA = AAPONTAMENTOTAREFA.IDTAREFA
								  
								  WHERE TIPOFATURAMENTO = 'P'

								  GROUP BY IDPROJETO) AATP
								  ON AATP.IDPROJETO = APROJETO.IDPROJETO 
                                  {0}", Condicao);
                gridControl1.DataSource = MetodosSQL.GetDT(sql);

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void frmVisaoProjeto_Load(object sender, EventArgs e)
        {
            AtualizaGrid();
            getAcesso("btnProjeto");
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                //CodUsuario = row1["GUSUARIO.CODUSUARIO"].ToString();
                //Nome = row1["GUSUARIO.CODUSUARIO"].ToString();
                PS.Glb.New.Cadastros.frmCadastroProjeto frm = new Cadastros.frmCadastroProjeto();
                frm.IDPROJETO = row1["IDPROJETO"].ToString();
                frm.CODEMPRESA = row1["CODEMPRESA"].ToString();
                frm.ShowDialog();
                AtualizaGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                PS.Glb.New.Cadastros.frmCadastroProjeto frm = new Cadastros.frmCadastroProjeto();
                frm.IDPROJETO = null;
                frm.CODEMPRESA = AppLib.Context.Empresa.ToString();
                frm.ShowDialog();
                AtualizaGrid();
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
                if (MessageBox.Show("Deseja exluir o registro selecionado?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                    New.Classes.Projeto projeto = new Classes.Projeto();

                    if (projeto.Excluir(Convert.ToInt32(row1["IDPROJETO"])))
                    {
                        XtraMessageBox.Show("Registro excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        AtualizaGrid();
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            AtualizaGrid();
        }

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            AtualizaGrid();
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
                    AtualizaGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvarLayout_Click(object sender, EventArgs e)
        {
            SalvarLayout();
        }

        private void btnFiltros_Click(object sender, EventArgs e)
        {
            PS.Glb.New.Filtro.frmFiltroProjeto frm = new Filtro.frmFiltroProjeto();
            frm.ShowDialog();

            if (String.IsNullOrWhiteSpace(frm.condicao))
            {
                Condicao = frm.condicao;
                AtualizaGrid();
            }
        }

        private void ReabrirProjeto()
        {
            try
            {
                int i = 0; 

                int[] selectedrows = gridView1.GetSelectedRows();

                foreach (int x in selectedrows)
                {
                    String IDPPROJETO = gridView1.GetRowCellValue(x, "IDPROJETO").ToString();
                    String CODEMPRESA = gridView1.GetRowCellValue(x, "CODEMPRESA").ToString();
                    String CODFILIAL = gridView1.GetRowCellValue(x, "CODFILIAL").ToString();

                    sql = String.Format(@"select CODUSUARIOPRESTADOR from APROJETO where IDPROJETO = '{0}'", IDPPROJETO);

                    if (MetodosSQL.GetField(sql, "CODUSUARIOPRESTADOR").Equals(AppLib.Context.Usuario))
                    {
                        sql = String.Format(@"update APROJETO
                                        set DATACONCLUSAO = null,
                                            STATUS = 'ABERTO'
                                        where IDPROJETO = '{0}'
                                        and CODEMPRESA = '{1}'
                                        and CODFILIAL = '{2}'", IDPPROJETO, CODEMPRESA, CODFILIAL);

                        MetodosSQL.ExecQuery(sql);

                        sql = String.Format(@"update APROJETOTAREFA
                                        set DATACONCLUSAO = null
                                        where IDPROJETO = '{0}'
                                        and CODEMPRESA = '{1}'
                                        and CODFILIAL = '{2}'", IDPPROJETO, CODEMPRESA, CODFILIAL);

                        MetodosSQL.ExecQuery(sql);

                        i++;
                    }
                    else
                    {
                        MessageBox.Show(String.Format("Processo permitido para projetos de sua coordenação. Projeto {0}", IDPPROJETO), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                AtualizaGrid();

                MessageBox.Show(String.Format("Processo finalizado. {0} projetos reabertos.", i), "Concluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConcluirProjeto()
        {
            try
            {
                int i = 0;

                int[] selectedrows = gridView1.GetSelectedRows();

                foreach (int x in selectedrows)
                {
                    String IDPPROJETO = gridView1.GetRowCellValue(x, "IDPROJETO").ToString();
                    String CODEMPRESA = gridView1.GetRowCellValue(x, "CODEMPRESA").ToString();
                    String CODFILIAL = gridView1.GetRowCellValue(x, "CODFILIAL").ToString();

                    sql = String.Format(@"select CODUSUARIOPRESTADOR from APROJETO where IDPROJETO = '{0}'", IDPPROJETO);

                    if (MetodosSQL.GetField(sql, "CODUSUARIOPRESTADOR").Equals(AppLib.Context.Usuario))
                    {
                        using (Form form = new Form())
                        {
                            form.Text = "About Us";
                            form.Width = 250;
                            form.Height = 120;
                            form.Icon = this.Icon;
                            form.Text = "Data";
                            form.StartPosition = FormStartPosition.CenterScreen;

                            Label mylabel = new Label();
                            mylabel.Location = new Point(25, 20);
                            mylabel.Text = "Data";
                            mylabel.Height = 15;


                            AppLib.Windows.CampoData mydata = new AppLib.Windows.CampoData();
                            mydata.Location = new Point(25, 40);

                            Button mybuttonn = new Button();
                            mybuttonn.Text = "OK";
                            mybuttonn.Location = new Point(130, 38);
                            mybuttonn.Click += (sender, args) => { form.Close(); };

                            form.Controls.Add(mylabel);
                            form.Controls.Add(mydata);
                            form.Controls.Add(mybuttonn);

                            form.ShowDialog();

                            if (mydata != null)
                            {
                                sql = String.Format(@"update APROJETO
                                        set DATACONCLUSAO = convert(datetime, '{0}', 103),
                                            STATUS = 'CONCLUIDO'
                                        where IDPROJETO = '{1}'
                                        and CODEMPRESA = '{2}'
                                        and CODFILIAL = '{3}'", mydata.Get(), IDPPROJETO, CODEMPRESA, CODFILIAL);

                                MetodosSQL.ExecQuery(sql);

                                sql = String.Format(@"update APROJETOTAREFA
                                        set DATACONCLUSAO = convert(datetime, '{0}', 103)
                                        where IDPROJETO = '{1}'
                                        and CODEMPRESA = '{2}'
                                        and CODFILIAL = '{3}'", mydata.Get(), IDPPROJETO, CODEMPRESA, CODFILIAL);

                                MetodosSQL.ExecQuery(sql);

                                i++;
                            }
                            else
                            {
                                MessageBox.Show("Data selecionada é invalida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                    }else
                    {
                        MessageBox.Show(String.Format("Processo permitido para projetos de sua coordenação. Projeto {0}", IDPPROJETO), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                AtualizaGrid();

                MessageBox.Show(String.Format("Processo finalizado. {0} projetos concluidos.", i), "Concluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConcluirProjeto_Click(object sender, EventArgs e)
        {
            ConcluirProjeto();
        }

        private void btnReabrirProjeto_Click(object sender, EventArgs e)
        {
            ReabrirProjeto();
        }

        private void getAcesso(string CodMenu)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT EDICAO, EXCLUSAO, CONSULTA, INCLUSAO FROM GPERMISSAOMENU WHERE CODMENU = ? AND CODPERFIL = ?", new object[] { "btnProjeto", AppLib.Context.Perfil });
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
    }
}
