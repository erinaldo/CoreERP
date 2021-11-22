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
    public partial class frmSelecaoOpCTRCRelac : Form
    {
        string tabela = "GOPER";
        public string codTipOper = string.Empty;
        public int codFilial = 0;
        public int Codoper;
        DataTable dtOperacoes = new DataTable();

        public frmSelecaoOpCTRCRelac()
        {
            InitializeComponent();
        }

        private void btnLookupCodTipOper_Click(object sender, EventArgs e)
        {
            PS.Glb.New.Visao.frmVisaoTipoOperacao frm = new PS.Glb.New.Visao.frmVisaoTipoOperacao();
            frm.consulta = true;
            frm.ShowDialog();
            if (!string.IsNullOrEmpty(frm.codTipOper))
            {
                txtCodTipOper.Text = frm.codTipOper;
                txtDescricaoTipOper.Text = frm.descricao;
                carregaGrid("WHERE GOPER.CODTIPOPER = '" + frm.codTipOper + "' AND GOPER.CODEMPRESA = " + AppLib.Context.Empresa + " AND GOPER.CODFILIAL = "+ codFilial + "", frm.codTipOper);
            }
        }

        public void carregaGrid(string where, string codtipOper)
        {

            string relacionamento = @"INNER JOIN VCLIFOR ON GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR AND GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA
LEFT OUTER JOIN GNFESTADUAL ON GOPER.CODOPER = GNFESTADUAL.CODOPER AND GOPER.CODEMPRESA = GNFESTADUAL.CODEMPRESA 
LEFT OUTER JOIN VVENDEDOR ON GOPER.CODEMPRESA = VVENDEDOR.CODEMPRESA AND GOPER.CODVENDEDOR = VVENDEDOR.CODVENDEDOR
LEFT OUTER JOIN VREPRE ON GOPER.CODEMPRESA = VREPRE.CODEMPRESA AND GOPER.CODREPRE = VREPRE.CODREPRE
LEFT OUTER JOIN VOPERADOR ON GOPER.CODEMPRESA = VOPERADOR.CODEMPRESA AND GOPER.CODOPERADOR = VOPERADOR.CODOPERADOR
LEFT OUTER JOIN GCIDADE ON GCIDADE.CODETD = VCLIFOR.CODETD AND GCIDADE.CODCIDADE = VCLIFOR.CODCIDADE";
            List<string> tabelasFilhas = new List<string>();
            tabelasFilhas.Add("VCLIFOR");
            tabelasFilhas.Add("GNFESTADUAL");
            tabelasFilhas.Add("VVENDEDOR");
            tabelasFilhas.Add("VREPRE");
            tabelasFilhas.Add("VOPERADOR");
            try
            {
                string sql = new Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {
                    sql = sql + filtroUsuario;
                }

                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);
                dtOperacoes = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);
                if (gridView1.Columns["GOPER.CODSTATUS"] != null)
                {
                    carregaImagemStatus();
                }
                if (gridView1.Columns["GOPER.CODSITUACAO"] != null)
                {
                    carregaImagemSituacao();
                }
                if (AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODTIPDOC FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { codtipOper, AppLib.Context.Empresa }).ToString() == "NFV")
                {
                    if (gridView1.Columns.Contains(gridView1.Columns["GNFESTADUAL.CODSTATUS"]))
                    {
                        carregaStatusNFE();
                    }
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
            DataTable dtImagem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IMAGEM, DESCRICAO, CODSTATUS FROM GSTATUS WHERE TABELA = 'GOPER'");
            for (int i = 0; i < dtImagem.Rows.Count; i++)
            {
                byte[] imagemEmBytes = (byte[])dtImagem.Rows[i]["IMAGEM"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagemEmBytes);
                images.AddImage(Image.FromStream(ms));
                imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[i]["DESCRICAO"].ToString(), dtImagem.Rows[i]["CODSTATUS"].ToString(), i));
            }
            imageCombo.SmallImages = images;
            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns["GOPER.CODSTATUS"].ColumnEdit = imageCombo;
        }

        private void carregaStatusNFE()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();
            DataTable dtImagem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IMAGEM, DESCRICAO, CODSTATUS FROM GSTATUS WHERE TABELA = 'GNFESTADUAL'");
            for (int i = 0; i < dtImagem.Rows.Count; i++)
            {
                byte[] imagemEmBytes = (byte[])dtImagem.Rows[i]["IMAGEM"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagemEmBytes);
                images.AddImage(Image.FromStream(ms));
                imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[i]["DESCRICAO"].ToString(), dtImagem.Rows[i]["CODSTATUS"].ToString(), i));
            }
            imageCombo.SmallImages = images;
            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns["GNFESTADUAL.CODSTATUS"].ColumnEdit = imageCombo;
        }
        private void carregaImagemSituacao()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();
            DataTable dtImagem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IMAGEM, DESCRICAO, CODSITUACAO FROM GSITUACAO WHERE TABELA = 'GOPER'");
            for (int i = 0; i < dtImagem.Rows.Count; i++)
            {
                byte[] imagemEmBytes = (byte[])dtImagem.Rows[i]["IMAGEM"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagemEmBytes);
                images.AddImage(Image.FromStream(ms));
                imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[i]["DESCRICAO"].ToString(), dtImagem.Rows[i]["CODSITUACAO"].ToString(), i));
            }
            imageCombo.SmallImages = images;
            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView1.Columns["GOPER.CODSITUACAO"].ColumnEdit = imageCombo;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private bool Salvar()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit GOPERRELACCTRC = new AppLib.ORM.Jit(conn, "GOPERRELACCTRC");
            conn.BeginTransaction();

            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            try
            {
                for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                {
                    GOPERRELACCTRC.Set("CODEMPRESA", AppLib.Context.Empresa);
                    GOPERRELACCTRC.Set("CODOPER", Codoper);
                    GOPERRELACCTRC.Set("CODOPERRELAC", Convert.ToInt32(dtOperacoes.Rows[i]["GOPER.CODOPER"]));
                    GOPERRELACCTRC.Insert();
                }

                conn.Commit();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void txtCodTipOper_Validated(object sender, EventArgs e)
        {
            if ( !string.IsNullOrEmpty(txtCodTipOper.Text))
            {
                DataTable dtCodTipOPer = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COTIPOPER, DESCRICAO FROM GTIPOPER WHERE COTIPOPER LIKE '%" + txtCodTipOper.Text + "%' AND CODEMPRESA = ? ", new object[] { txtCodTipOper.Text, AppLib.Context.Empresa });
                if (dtCodTipOPer.Rows.Count > 1)
                {
                    PS.Glb.New.Visao.frmVisaoTipoOperacao frm = new PS.Glb.New.Visao.frmVisaoTipoOperacao(@"WHERE COTIPOPER LIKE '%" + txtCodTipOper.Text + "%' AND CODEMPRESA = " + AppLib.Context.Empresa + " AND CODFILIAL = " + codFilial + "");
                    frm.consulta = true;
                    frm.ShowDialog();
                    if (!string.IsNullOrEmpty(frm.codTipOper))
                    {
                        txtCodTipOper.Text = frm.codTipOper;
                        txtDescricaoTipOper.Text = frm.descricao;
                    }
                }
                else if (dtCodTipOPer.Rows.Count == 1)
                {
                    txtCodTipOper.Text = dtCodTipOPer.Rows[0]["COTIPOPER"].ToString();
                    txtDescricaoTipOper.Text = dtCodTipOPer.Rows[0]["DESCRICAO"].ToString();
                }
                else
                {
                    txtCodTipOper.Text = string.Empty;
                    txtDescricaoTipOper.Text = string.Empty;
                }
            }
        }

        private void btnIncluir_Click_1(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                this.Dispose();
            }
        }
    }
}
