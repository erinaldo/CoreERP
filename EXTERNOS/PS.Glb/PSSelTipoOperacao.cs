using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSSelTipoOperacao : DevExpress.XtraEditors.XtraForm
    {
        private PS.Lib.Constantes ct = new PS.Lib.Constantes();
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public int Tipo;
        public int CodFilial;
        public string TipoOper;
        public Form pai;
        public string codMenu;

        public PSSelTipoOperacao()
        {
            InitializeComponent();

            psLookup1.PSPart = "PSPartFilial";
        }

        private void FrmSelecionaTipoOperacao_Load(object sender, EventArgs e)
        {
            CarregaGrid();
            CarregaFilialDefault();
        }

        private void CarregaGrid()
        {
            try
            {
                dataGridView1.DefaultCellStyle.SelectionBackColor = ct.corSelecaoGrid;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ct.corImparGrid;

                dataGridView1.DataSource = gb.RetornaOperacaoUsuario(this.Tipo);

                AlteraNomeColuna();
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError("Ocorreu um erro: " + ex.Message);
            }
        }

        private void AlteraNomeColuna()
        {
            DataTable dt = new DataTable();
            dt = gb.NomeDosCampos("GTIPOPER");

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dataGridView1.Columns[i].Name == dt.Rows[j]["COLUNA"].ToString())
                    {
                        dataGridView1.Columns[i].HeaderText = dt.Rows[j]["DESCRICAO"].ToString();
                        dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                }
            }
        }

        private void CarregaFilialDefault()
        {
            string sSql = "SELECT MIN(CODFILIAL) CODFILIAL FROM GFILIAL WHERE CODEMPRESA = ?";

            psLookup1.Text = dbs.QueryValue(0, sSql, AppLib.Context.Empresa).ToString();
            psLookup1.LoadLookup();
        }

        private void FrmSelecionaTipoOperacao_KeyDown(object sender, KeyEventArgs e)
        {
            Keys flag = e.KeyCode;

            if (flag.Equals(Keys.Enter))
            {
                SelectNextControl(ActiveControl, !e.Shift, true, true, true);
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TipoOper= null;
            CodFilial = 0;
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AppLib.Context.Filial = Convert.ToInt32(psLookup1.textBox1.Text);

            New.Filtro.frmFiltroOperacao frm = new New.Filtro.frmFiltroOperacao();
            frm.codFilial = Convert.ToInt32(psLookup1.textBox1.Text);
            frm.tipOPer = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            frm.codMenu = codMenu;
            frm.pai = pai;
            frm.ShowDialog();
            this.Dispose();
            //New.frmFiltro frm = new New.frmFiltro("GOPER");
            //frm.tipOPer = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            //frm.pai = pai;
            //frm.ShowDialog();
            //this.Dispose();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            button1_Click(this, null);
        }

        private void psLookup1_Load(object sender, EventArgs e)
        {

        }
    }
}
