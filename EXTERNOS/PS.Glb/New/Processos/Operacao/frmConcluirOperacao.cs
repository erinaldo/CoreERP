using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Processos.Operacao
{
    public partial class frmConcluirOperacao : Form
    {
        private List<int> codOper = new List<int>();

        public frmConcluirOperacao(List<int> _codOper)
        {
            InitializeComponent();
            codOper = _codOper;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();
            try
            {
                if (!string.IsNullOrEmpty(meMotivoConclusao.Text))
                {
                    if (cmbCodMotivo.SelectedIndex == -1)
                    {
                        MessageBox.Show("Favor selecionar o Motivo da conclusão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    for (int i = 0; i < codOper.Count; i++)
                    {
                        conn.ExecTransaction("INSERT INTO GOPERMOTIVOCONCLUSAO (CODEMPRESA, CODOPER, CODUSUARIO, DESCRICAO, CODMOTIVO) VALUES (?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, codOper[i].ToString(), AppLib.Context.Usuario, meMotivoConclusao.Text, cmbCodMotivo.SelectedValue });
                        conn.ExecTransaction("UPDATE GOPER SET CODSTATUS = '8' WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { codOper[i].ToString(), AppLib.Context.Empresa });

                        int CountFlanca = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(CODLANCA) FROM FLANCA WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codOper[i] }));

                        if (CountFlanca > 0)
                        {
                            string Classificacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TOP 1 FTIPDOC.CLASSIFICACAO FROM FTIPDOC INNER JOIN FLANCA ON FLANCA.CODEMPRESA = FTIPDOC.CODEMPRESA AND FLANCA.CODTIPDOC = FTIPDOC.CODTIPDOC WHERE FLANCA.CODEMPRESA = ? AND FLANCA.CODOPER = ?", new object[] { AppLib.Context.Empresa, codOper[i] }).ToString();

                            if (Classificacao == string.Empty)
                            {
                               
                            }
                            else
                            {
                                if (Classificacao.ToString() == "3")
                                {
                                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM FLANCA WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codOper[i] });
                                }

                                else
                                {
                                    if (MessageBox.Show("Deseja excluir o lançamento financeiro?", "Informação do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM FLANCA WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, codOper[i] });
                                    }
                                }
                            } 
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Favor preencher o motivo corretamente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    conn.Rollback();
                    return;
                }
                conn.Commit();
                this.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível concluir a operação.", "Informação do Sistema.", MessageBoxButtons.OK,  MessageBoxIcon.Error);
                conn.Rollback();
                return;
            }
           
        }

        private void frmConcluirOperacao_Load(object sender, EventArgs e)
        {
            cmbCodMotivo.SelectedIndex = -1;
            cmbCodMotivo.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT GMOTIVO.CODMOTIVO , GMOTIVO.DESCRICAO  FROM GMOTIVO INNER JOIN GMOTIVOUTILIZAÇÃO ON GMOTIVO.CODMOTIVO = GMOTIVOUTILIZACAO.CODMOTIVO WHERE GMOTIVOUTILIZACAO.UTILIZACAO = 'Conclusão de Operação'", new object[] { });
            cmbCodMotivo.ValueMember = "CODMOTIVO";
            cmbCodMotivo.DisplayMember = "DESCRICAO";
        }
    }
}
