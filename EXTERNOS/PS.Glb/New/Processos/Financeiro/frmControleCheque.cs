using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Processos.Financeiro
{
    public partial class frmControleCheque : Form
    {
        public List<Class.ControleCheque> ListCheque = new List<Class.ControleCheque>();
        private DataTable dtCheque;
        public DateTime DataBaixa;
        public int PagRec;
        public string CodConta = string.Empty;
        public decimal ValorBaixa;
        public bool ExecutaControleCheque = false;

        public frmControleCheque()
        {
            InitializeComponent();
        }

        private void frmControleCheque_Load(object sender, EventArgs e)
        {
            tbTotalBaixa.Text = ValorBaixa.ToString();
            tbDiferenca.Text = ValorBaixa.ToString();
            tbDiferenca.ForeColor = Color.Red;
            tbTotalPagamento.Text = "0,00";
            btnExecutar.Enabled = false;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            PS.Glb.New.Cadastros.frmCadastroCheque frm = new Cadastros.frmCadastroCheque(true, this);
            frm.PagRec = PagRec;
            frm.CodConta = CodConta;
            frm.Valor = Convert.ToDecimal(tbDiferenca.Text);
            frm.ValorBaixa = ValorBaixa;
            frm.Databaixa = DataBaixa;
            frm.ShowDialog();

            if (tbDiferenca.Text == "0,00")
            {
                tbDiferenca.ForeColor = Color.Black;
                btnNovo.Enabled = false;
                btnExecutar.Enabled = true;
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            int index = gridView1.GetDataSourceRowIndex(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            tbTotalPagamento.Text = (Convert.ToDecimal(tbTotalPagamento.Text) - Convert.ToDecimal(row["Valor"])).ToString();
            tbDiferenca.Text = row["Valor"].ToString();

            row.Delete();         
            ListCheque.Remove(ListCheque[index]);
            gridView1.DeleteSelectedRows();

            if (tbDiferenca.Text == "0,00")
            {
                tbDiferenca.ForeColor = Color.Black;
                btnNovo.Enabled = false;
                btnExecutar.Enabled = true;
            }
            else
            {
                tbDiferenca.ForeColor = Color.Red;
                btnNovo.Enabled = true;
                btnExecutar.Enabled = false;
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsFind.AlwaysVisible == true)
            {
                gridView1.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gridView1.OptionsFind.AlwaysVisible = true;
            }
        }

        private void btnAgrupar_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsView.ShowGroupPanel == true)
            {
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.ClearGrouping();
                btnAgrupar.Text = "Agrupar";
            }
            else
            {
                gridView1.OptionsView.ShowGroupPanel = true;
                btnAgrupar.Text = "Desagrupar";
            }
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            ExecutaControleCheque = true;
            this.Dispose();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            ExecutaControleCheque = false;
            this.Dispose();
        }

        //public void AdicionarLista(int _codempresa, int _codCheque, string _codConta, int _numero, int _pagRec, decimal _valor, DateTime? _dataCriacao, string _usuarioCriacao, DateTime? _dataAlteracao, string _usuarioAlteracao, DateTime? _dataBoa, string _observacao, string _codBanco, string _codAgencia, string _codCorrente, DateTime? _dataEmissao, DateTime? _datCompensacao, int _compensado)
        //{
        //    ListCheque.Add(new Class.ControleCheque(_codempresa, _codCheque, _codConta, _numero, _pagRec, _valor, _dataCriacao, _usuarioCriacao, _dataAlteracao, _usuarioAlteracao, _dataBoa, _observacao, _codBanco, _codAgencia, _codCorrente, _dataEmissao, _datCompensacao, _compensado));

        //    var dados = from c in ListCheque.AsEnumerable()
        //                select new { CódigoDoCheque = c.CODCHEQUE, CódigoDaConta = c.CODCONTA, Número = c.NUMERO, Valor = c.VALOR };

        //    var ValorCheque = ListCheque.Sum(x => x.VALOR);

        //    tbTotalPagamento.Text = ValorCheque.ToString();
        //    tbDiferenca.Text = (Convert.ToDecimal(tbTotalBaixa.Text) - ValorCheque).ToString();

        //    gridControl1.DataSource = dados.ToList();
        //    gridView1.BestFitColumns();
        //}

        public void AdicionarLista(int _codempresa, int _codCheque, string _codConta, int _numero, int _pagRec, decimal _valor, DateTime? _dataCriacao, string _usuarioCriacao, DateTime? _dataAlteracao, string _usuarioAlteracao, DateTime? _dataBoa, string _observacao, string _codBanco, string _codAgencia, string _codCorrente, DateTime? _dataEmissao, DateTime? _datCompensacao, int _compensado)
        {
            ListCheque.Add(new Class.ControleCheque(_codempresa, _codCheque, _codConta, _numero, _pagRec, _valor, _dataCriacao, _usuarioCriacao, _dataAlteracao, _usuarioAlteracao, _dataBoa, _observacao, _codBanco, _codAgencia, _codCorrente, _dataEmissao, _datCompensacao, _compensado));

            var dados = from c in ListCheque.AsEnumerable()
                        select new { CódigoDoCheque = c.CODCHEQUE, CódigoDaConta = c.CODCONTA, Número = c.NUMERO, Valor = c.VALOR };

            var ValorCheque = ListCheque.Sum(x => x.VALOR);

            tbTotalPagamento.Text = ValorCheque.ToString();
            tbDiferenca.Text = (Convert.ToDecimal(tbTotalBaixa.Text) - ValorCheque).ToString();

            dtCheque = LINQToDataTable(dados);

            gridControl1.DataSource = dtCheque;
            gridView1.BestFitColumns();
        }

        public DataTable LINQToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();

            System.Reflection.PropertyInfo[] columns = null;

            if (Linqlist == null)
            {
                return dt;
            }

            foreach (T Record in Linqlist)
            {
                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();

                    foreach (System.Reflection.PropertyInfo GeProperty in columns)
                    {
                        Type ColumnType = GeProperty.PropertyType;

                        if ((ColumnType.IsGenericType) && (ColumnType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            ColumnType = ColumnType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GeProperty.Name, ColumnType));
                    }
                }

                DataRow row = dt.NewRow();

                foreach (System.Reflection.PropertyInfo PInfo in columns)
                {
                    row[PInfo.Name] = PInfo.GetValue(Record, null) == null ? DBNull.Value : PInfo.GetValue(Record, null);
                }

                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}

