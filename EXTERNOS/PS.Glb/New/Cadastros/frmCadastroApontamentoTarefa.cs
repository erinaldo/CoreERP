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

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroApontamentoTarefa : Form
    {
        string sql = String.Empty;
        string sqlParalelo = String.Empty;
        string IDFILIAL = String.Empty;

        public String IDTAREFA { get; set; }
        public String IDAPONTAMENTO { get; set; }
        public String IDAPONTAMENTOTAREFA { get; set; }
        public String CODEMPRESA { get; set; }
        public String IDPROJETO { get; set; }
        public DateTime HORA { get; set; }
        public DateTime DATA { get; set; }

        public frmCadastroApontamentoTarefa()
        {
            InitializeComponent();
        }

        private void SetConsultaLookUp()
        {
            campoLookupIDTAREFA.ColunaTabela = String.Format(@"(select * from APROJETOTAREFA where IDPROJETO = '{0}') Y", IDPROJETO);
        }

        private void AtualizaVisao()
        {
            try
            {
                SetConsultaLookUp();
                if (String.IsNullOrWhiteSpace(IDTAREFA))
                {
                    int hora = HORA.Hour;
                    int minuto = HORA.Minute;

                    DateTime horaCalculada = new DateTime(DATA.Year, DATA.Month, DATA.Day, hora, minuto, 0);

                    teHora.EditValue = horaCalculada;
                }
                else
                {
                    sql = String.Format(@"select CODEMPRESA,
	                                             IDAPONTAMENTO,
	                                             IDAPONTAMENTOTAREFA,
	                                             IDTAREFA,
	                                             convert(varchar(10), HORAS, 108) as 'HORAS',
	                                             PERCENTUAL,
	                                             OBSERVACAO,
	                                             MINUTOS
                                          from AAPONTAMENTOTAREFA where IDTAREFA = '{0}' and CODEMPRESA = '{1}' and IDAPONTAMENTOTAREFA = '{2}'", IDTAREFA, CODEMPRESA, IDAPONTAMENTOTAREFA);

                    tbIdentificador.EditValue = int.Parse(MetodosSQL.GetField(sql, "IDAPONTAMENTOTAREFA"));

                    campoLookupIDTAREFA.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "IDTAREFA");

                    teHora.EditValue = Convert.ToDateTime(MetodosSQL.GetField(sql, "HORAS"));

                    tbPercentual.EditValue = int.Parse(MetodosSQL.GetField(sql, "PERCENTUAL"));

                    tbDescricao.Text = MetodosSQL.GetField(sql, "OBSERVACAO");

                    sql = String.Format(@"select NOMETAREFA from APROJETOTAREFA where IDTAREFA = '{0}'", IDTAREFA);
                    campoLookupIDTAREFA.textBoxDESCRICAO.Text = MetodosSQL.GetField(sql, "NOMETAREFA");

                    sql = String.Format(@"select IDSTATUSAPONTAMENTO from AAPONTAMENTO where IDAPONTAMENTO = '{0}'", IDAPONTAMENTO);

                    if (int.Parse(MetodosSQL.GetField(sql, "IDSTATUSAPONTAMENTO")) > 0)
                    {
                        campoLookupIDTAREFA.Enabled = false;
                        teHora.Enabled = false;
                        tbPercentual.Enabled = false;
                        tbDescricao.Enabled = false;
                    }
                }

                sql = String.Format(@"select CODFILIAL from AAPONTAMENTO where IDAPONTAMENTO = '{0}'", IDAPONTAMENTO);
                IDFILIAL = MetodosSQL.GetField(sql, "CODFILIAL");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private Boolean ValidaHora()
        {
            try
            {
                sql = String.Format(@"SELECT CASE WHEN DATEPART(HOUR, (CONVERT(DATETIME,'{2}',103)+ ISNULL(X.APONTADO,0))) > X.TOTAL THEN 'NAO' ELSE 'SIM' END AS 'INSERT', 
                                           CASE WHEN DATEPART(HOUR, ((ISNULL(X.APONTADO,0) - HORAS) + CONVERT(DATETIME,'{2}',103))) > X.TOTAL THEN 'NAO' ELSE 'SIM' END AS 'UPDATE'
                                    FROM AAPONTAMENTOTAREFA
                                    
                                    RIGHT JOIN (
                                    SELECT AP.IDAPONTAMENTO, 
                                    		ISNULL(DATEADD(MINUTE, SUM(DATEDIFF(MINUTE, '', APT.HORAS)), ''),0) AS 'APONTADO', 
                                    		(DATEDIFF(HOUR, INICIO, TERMINO) - DATEPART(HOUR, ABONO)) AS 'TOTAL'
                                    
                                    FROM AAPONTAMENTO AP
                                    
                                    				   LEFT JOIN AAPONTAMENTOTAREFA APT
                                    				   ON APT.IDAPONTAMENTO = AP.IDAPONTAMENTO
                                    
                                    				   GROUP BY AP.IDAPONTAMENTO, AP.INICIO, AP.TERMINO, AP.ABONO) X
                                    ON X.IDAPONTAMENTO = AAPONTAMENTOTAREFA.IDAPONTAMENTO
                                    
                                    WHERE (IDAPONTAMENTOTAREFA = '{0}' OR X.IDAPONTAMENTO = '{1}')", IDAPONTAMENTOTAREFA, IDAPONTAMENTO, teHora.EditValue);

                if (teHora.EditValue == null)
                {
                    return false;
                }

                if (String.IsNullOrWhiteSpace(IDAPONTAMENTOTAREFA))
                {
                    if (MetodosSQL.GetField(sql, "INSERT") == "SIM")
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
                    if (MetodosSQL.GetField(sql, "UPDATE") == "SIM")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool Salvar()
        {
            try
            {
                if (string.IsNullOrEmpty(tbDescricao.Text))
                {
                    XtraMessageBox.Show("A descrição da Tarefa deve ser preenchida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                sql = String.Format(@"select IDSTATUSAPONTAMENTO from AAPONTAMENTO where IDAPONTAMENTO = '{0}'", IDAPONTAMENTO);

                if (int.Parse(MetodosSQL.GetField(sql, "IDSTATUSAPONTAMENTO")) > 0)
                {
                    XtraMessageBox.Show("Não é permitido alterar apontamento concluído!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(IDAPONTAMENTOTAREFA))
                    {
                        sql = String.Format(@"insert into AAPONTAMENTOTAREFA 
											 values ('{0}', '{6}','{1}','{2}', CONVERT(datetime, '{3}', 103), '{4}', '{5}')
                                             select SCOPE_IDENTITY()",
                                                 /*{0}*/ CODEMPRESA,
                                                 /*{1}*/ IDAPONTAMENTO,
                                                 /*{2}*/ IDTAREFA,
                                                 /*{3}*/ teHora.EditValue,
                                                 /*{4}*/ Convert.ToInt32(tbPercentual.EditValue),
                                                 /*{5}*/ tbDescricao.Text.ToUpper(),
                                                 /*{6}*/ IDFILIAL);

                        IDAPONTAMENTOTAREFA = MetodosSQL.ExecScalar(sql).ToString();
                        tbIdentificador.EditValue = int.Parse(IDAPONTAMENTOTAREFA);
                    }
                    else
                    {
                        sql = String.Format(@"update AAPONTAMENTOTAREFA
										  set IDTAREFA = '{0}',
											  HORAS = CONVERT(datetime, '{1}', 103),
											  PERCENTUAL = '{2}',
											  OBSERVACAO = '{3}',
                                              CODFILIAL = '{5}'
										  where IDAPONTAMENTOTAREFA = '{4}'",
                                               /*{0}*/ campoLookupIDTAREFA.textBoxCODIGO.Text,
                                               /*{1}*/ teHora.EditValue,
                                               /*{2}*/ Convert.ToInt32(tbPercentual.EditValue),
                                               /*{3}*/ tbDescricao.Text.ToUpper(),
                                               /*{4}*/ tbIdentificador.EditValue,
                                               /*{5}*/ IDFILIAL);
                        MetodosSQL.ExecQuery(sql);
                    }

                    AtualizaVisao();

                    return true;
                }

                //else
                //{
                //    XtraMessageBox.Show("Verifique o campo horas, você não pode apontar 0 ou mais horas do que o restante.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return false;
            }
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            if (Salvar())
            {
                this.Dispose();
            }
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmCadastroApontamentoTarefa_Load(object sender, EventArgs e)
        {
            AtualizaVisao();
        }

        private void campoLookupIDCLIENTE_AposSelecao(object sender, EventArgs e)
        {
            SetConsultaLookUp();
        }

        private void campoLookupIDTAREFA_AposSelecao(object sender, EventArgs e)
        {
            IDTAREFA = campoLookupIDTAREFA.textBoxCODIGO.Text;
        }
    }
}
