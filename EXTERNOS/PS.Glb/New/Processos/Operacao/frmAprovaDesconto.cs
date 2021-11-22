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
    public partial class frmAprovaDesconto : Form
    {
        public List<string> ListaCodOper = new List<string>();

        public frmAprovaDesconto()
        {
            InitializeComponent();

        }

        public void getMotivo()
        {
            if (!string.IsNullOrEmpty(txtCodMotivo.Text))
            {
                DataTable dtMotivo = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT GMOTIVO.CODMOTIVO, GMOTIVO.DESCRICAO FROM GMOTIVO INNER JOIN GMOTIVOUTILIZACAO ON GMOTIVO.CODMOTIVO = GMOTIVOUTILIZACAO.CODMOTIVO WHERE GMOTIVOUTILIZACAO.UTILIZACAO = 'Aprovação de Desconto' AND  GMOTIVO.CODMOTIVO LIKE '%" + txtCodMotivo.Text + "%'", new object[] { });
                if (dtMotivo.Rows.Count > 1)
                {
                    PS.Glb.New.Visao.Globais.frmVisaoMotivo frm = new PS.Glb.New.Visao.Globais.frmVisaoMotivo(@"WHERE GMOTIVO.CODMOTIVO LIKE '%" + txtCodMotivo.Text + "%'");
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

            PS.Glb.New.Visao.Globais.frmVisaoMotivo frm = new PS.Glb.New.Visao.Globais.frmVisaoMotivo(" WHERE GMOTIVOUTILIZACAO.UTILIZACAO = 'Aprovação de Desconto' ");
            frm.consulta = true;
            frm.WindowState = FormWindowState.Normal;
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.codMotivo))
            {
                txtCodMotivo.Text = frm.codMotivo;
                txtDescricaoMotivo.Text = frm.Descricao;
            }
        }

        private void txtCodMotivo_Validated(object sender, EventArgs e)
        {
            getMotivo();
        }

        private void carregaGridOperacao()
        {
            string codoper = string.Empty;
            for (int i = 0; i < ListaCodOper.Count; i++)
            {
                if (string.IsNullOrEmpty(codoper))
                {
                    codoper = ListaCodOper[i];
                }
                else
                {
                    codoper = codoper + ", " + ListaCodOper[i];
                }
            }

       
            DataTable dtOperacao = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
 GOPER.CODTIPOPER
,GOPER.NUMERO
,GOPER.DATAEMISSAO
,GOPER.CODCLIFOR
,VCLIFOR.NOME
,GOPER.CODOPERADOR
,VOPERADOR.NOME
,GOPER.CODVENDEDOR 
,VVENDEDOR.NOME
,GOPER.CODREPRE 
,VREPRE.NOME
,GOPER.CODEMPRESA
,GOPER.CODFILIAL
,GOPER.CODOPER

FROM GOPER
INNER JOIN VCLIFOR ON VCLIFOR.CODEMPRESA = GOPER.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR
LEFT OUTER JOIN VOPERADOR ON VOPERADOR.CODEMPRESA = GOPER.CODEMPRESA AND VOPERADOR.CODOPERADOR = GOPER.CODOPERADOR
LEFT OUTER JOIN VVENDEDOR ON VVENDEDOR.CODEMPRESA = GOPER.CODEMPRESA AND VVENDEDOR.CODVENDEDOR = GOPER.CODVENDEDOR
LEFT OUTER JOIN VREPRE ON VREPRE.CODEMPRESA = GOPER.CODEMPRESA AND VREPRE.CODREPRE = GOPER.CODREPRE
WHERE
    GOPER.CODEMPRESA = ?
AND GOPER.CODOPER IN (" + codoper + ") ", new object[] { AppLib.Context.Empresa });
            gridControl1.DataSource = dtOperacao;

        }

        private void frmAprovaDesconto_Load(object sender, EventArgs e)
        {
            carregaGridOperacao();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            
            DataTable dtOperacao = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
 GOPERITEM.NSEQITEM
,GOPERITEM.CODPRODUTO
,VPRODUTO.CODIGOAUXILIAR
,VPRODUTO.DESCRICAO
,GOPER.DATAEMISSAO
,GOPERITEM.VLUNITORIGINAL
,GOPERITEM.VLUNITARIO
,GOPERITEM.PRDESCONTO
,GOPERITEM.VLDESCONTO
,GOPER.CODEMPRESA
,GOPER.CODFILIAL

FROM GOPER
INNER JOIN GOPERITEM ON GOPERITEM.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEM.CODOPER = GOPER.CODOPER
INNER JOIN VPRODUTO ON VPRODUTO.CODEMPRESA = GOPERITEM.CODEMPRESA AND VPRODUTO.CODPRODUTO = GOPERITEM.CODPRODUTO

WHERE
    GOPER.CODEMPRESA = ?
AND GOPER.CODOPER = ?
AND GOPERITEM.PRDESCONTO > GOPER.LIMITEDESC ", new object[] { AppLib.Context.Empresa, row1["CODOPER"] });
            gridControl2.DataSource = dtOperacao;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GOPERAPROV (CODEMPRESA, CODOPER, USUARIO, DATA, CODMOTIVO, OBS) VALUES (?,?,?,?,?, ?)", new object[] { AppLib.Context.Empresa, row1["CODOPER"], AppLib.Context.Usuario, AppLib.Context.poolConnection.Get("Start").GetDateTime(), txtCodMotivo.Text, txtObservacao.Text });

                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE GOPER SET CODSTATUS = 0 WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { row1["CODOPER"], AppLib.Context.Empresa });
            }
            if (gridView1.SelectedRowsCount > 1)
            {
                MessageBox.Show("Operações selecionadas desbloqueadas com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Operação selecionada desbloqueadas com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Dispose();
        }
    }
}
