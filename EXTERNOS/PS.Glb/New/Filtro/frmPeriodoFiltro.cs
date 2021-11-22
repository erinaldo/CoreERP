using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Filtro
{
    public partial class frmPeriodoFiltro : Form
    {
        public string tabela;

        public DateTime dataInicial, datafinal;
        public List<Relatorios.parametro> parametro = new List<Relatorios.parametro>();
        public string campo;

        

        public frmPeriodoFiltro()
        {
            InitializeComponent();

        }

        private void frmPeriodoFiltro_Load(object sender, EventArgs e)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = ? AND DATA_TYPE = 'DateTime'", new object[] { tabela });


            List<PS.Lib.ComboBoxItem> lista = new List<PS.Lib.ComboBoxItem>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataTable dtDic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA, DESCRICAO FROM GDICIONARIO WHERE TABELA = ? AND COLUNA = TABELA + '.' + ?", new object[] { tabela, dt.Rows[i]["COLUMN_NAME"] });
           

                lista.Add(new PS.Lib.ComboBoxItem());
                lista[i].ValueMember = dtDic.Rows[0]["COLUNA"];
                lista[i].DisplayMember = dtDic.Rows[0]["DESCRICAO"];
            }

            cmbCampo.DataSource = lista;
            cmbCampo.DisplayMember = "DisplayMember";
            cmbCampo.ValueMember = "ValueMember";

          
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Relatorios.parametro param = new Relatorios.parametro();
            param.OPERADOR = cmbOperador.Text;
            param.CAMPO = cmbCampo.Text;
            param.CONDICAO = cmbCondicao.Text;
            param.VALOR = dteValor.Text;
            parametro.Add(param);
            gridControl1.DataSource = null;
            gridControl1.DataSource = parametro;
            limparCampos();
        }
        private void limparCampos()
        {
            cmbOperador.SelectedIndex = -1;
            cmbCampo.SelectedIndex = -1;
            cmbCondicao.SelectedIndex = -1;
            dteValor.Text = "";
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
			{
                gridView1.DeleteRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
			}

            //DataRow[] rows = new DataRow[gridView1.SelectedRowsCount];

            //foreach (DataRow row in rows)
            //{
            //    row.Delete();
            //}
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dteInicial.Text) && !string.IsNullOrEmpty(dteFinal.Text))
            {
                dataInicial = Convert.ToDateTime(dteInicial.Text);
                datafinal = Convert.ToDateTime(dteFinal.Text);
                campo = cmbCampo.Text;
            }
            this.Dispose();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }
    }
}
