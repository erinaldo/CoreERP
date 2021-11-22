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
    public partial class frmAprovaOperacao : Form
    {
        public List<string> ListaCodOper = new List<string>();

        public frmAprovaOperacao()
        {
            InitializeComponent();
        }

        private void txtCodMotivo_Validated(object sender, EventArgs e)
        {
            getMotivo(txtCodMotivo.Text);
        }

        public void getMotivo(string codMotivo)
        {
            if (!string.IsNullOrEmpty(codMotivo))
            {
                DataTable dtMotivo = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT GMOTIVO.CODMOTIVO, GMOTIVO.DESCRICAO FROM GMOTIVO INNER JOIN GMOTIVOUTILIZACAO ON GMOTIVO.CODMOTIVO = GMOTIVOUTILIZACAO.CODMOTIVO WHERE GMOTIVOUTILIZACAO.UTILIZACAO = 'Aprovação de Desconto' AND  GMOTIVO.CODMOTIVO LIKE '%" + codMotivo + "%'", new object[] { });
                if (dtMotivo.Rows.Count > 1)
                {
                    PS.Glb.New.Visao.Globais.frmVisaoMotivo frm = new PS.Glb.New.Visao.Globais.frmVisaoMotivo(@"WHERE GMOTIVO.CODMOTIVO LIKE '%" + codMotivo + "%'");
                    frm.consulta = true;
                    frm.ShowDialog();
                    if (!string.IsNullOrEmpty(frm.codMotivo))
                    {
                        txtCodMotivo.Text = frm.codMotivo;
                        txtDescricaoMotivo.Text = frm.Descricao;
                    }
                }
                else if (dtMotivo.Rows.Count == 1)
                {
                    txtCodMotivo.Text = dtMotivo.Rows[0]["CODMOTIVO"].ToString();
                    txtDescricaoMotivo.Text = dtMotivo.Rows[0]["DESCRICAO"].ToString();
                }
                else
                {
                    txtCodMotivo.Text = string.Empty;
                    txtDescricaoMotivo.Text = string.Empty;
                }
            }
        }

        private void btnLookupMotivo_Click(object sender, EventArgs e)
        {
            PS.Glb.New.Visao.Globais.frmVisaoMotivo frm = new PS.Glb.New.Visao.Globais.frmVisaoMotivo(" WHERE GMOTIVOUTILIZACAO.UTILIZACAO = 'Aprovação da Operação' ");
            frm.consulta = true;
            frm.WindowState = FormWindowState.Normal;
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.codMotivo))
            {
                txtCodMotivo.Text = frm.codMotivo;
                txtDescricaoMotivo.Text = frm.Descricao;
            }
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListaCodOper.Count; i++)
            {

                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GOPERAPROV (CODEMPRESA, CODOPER, USUARIO, DATA, CODMOTIVO, OBS, TIPO) VALUES (?,?,?,?,?, ?, ?)", new object[] { AppLib.Context.Empresa, ListaCodOper[i], AppLib.Context.Usuario, AppLib.Context.poolConnection.Get("Start").GetDateTime(), txtCodMotivo.Text, txtObservacao.Text, "APROVAÇÃO DA OPERAÇÃO" });
                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE GOPER SET APROVACAO = '1' WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { ListaCodOper[i], AppLib.Context.Empresa });
            }

            MessageBox.Show("Operação(ões) selecionada(s) aprovadas(s) com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Dispose();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
