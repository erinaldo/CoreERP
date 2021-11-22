using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Processos
{
    public partial class frmCancelaOperacao : Form
    {
        private List<int> m_codOper;
        
        private AppLib.Data.Connection conn;
        public bool statusExecucao = false;

        public frmCancelaOperacao(List<int> _m_codOper, AppLib.Data.Connection _conn)
        {
            InitializeComponent();
            m_codOper = _m_codOper;
            conn = _conn;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < m_codOper.Count; i++)
                {
                    // Alteração do Status da operação atual.
                    conn.ExecTransaction(@"UPDATE GOPER SET CODSTATUS = (SELECT CODSTATUS FROM GSTATUS WHERE TABELA = 'GOPER' AND DESCRICAO = 'CANCELADO') WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, m_codOper[i] });
                    // 
                    conn.ExecTransaction(@"INSERT INTO GMOTIVOCANCELAMENTO (CODOPER, CODEMPRESA, CODUSUARIO, MOTIVO, DATACRIACAO) VALUES (?, ?, ?, ?, ?)", new object[] {m_codOper[i], AppLib.Context.Empresa, AppLib.Context.Usuario, memoEdit1.Text, conn.GetDateTime() });
                }
                statusExecucao = true;
                this.Dispose();
            }
            catch (Exception)
            {
                statusExecucao = false;
                this.Dispose();
            }
        }
    }
}
