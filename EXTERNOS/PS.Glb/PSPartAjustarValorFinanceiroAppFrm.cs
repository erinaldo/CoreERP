using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartAjustarValorFinanceiroAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        public string codOper = string.Empty;

        public PSPartAjustarValorFinanceiroAppFrm()
        {
            InitializeComponent();
        }
        public override bool Execute()
        {
            if (!string.IsNullOrEmpty(txtNumeroDocumento.Text))
            {
                decimal valorTotal = getValorTotal(codOper);
                if (valorTotal != 0)
                {
                    decimal valorParcelas = getValorParcelas(codOper, dataGridView1.SelectedRows[0].Cells["CODLANCA"].Value.ToString());

                    DataTable parcelasRestantes = getParcelasRestantes(codOper, dataGridView1.SelectedRows[0].Cells["CODLANCA"].Value.ToString());
                    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                    conn.BeginTransaction();

                    if (parcelasRestantes.Rows.Count > 0)
                    {
                        if (Convert.ToDecimal(txtValorParcela.Text) != 0)
                        {
                            decimal valorNovoParcelas = ((valorTotal - valorParcelas) - Convert.ToDecimal(txtValorParcela.Text)) / parcelasRestantes.Rows.Count;
                            try
                            {
                                alteraValorItemSelecionado(Convert.ToDecimal(txtValorParcela.Text), dataGridView1.SelectedRows[0].Cells["CODLANCA"].Value.ToString(), conn);
                                alteraValorParcelasRestantes(valorNovoParcelas, parcelasRestantes, conn);
                                //Verifica se a data do vencimento não é menor que a data de emissão do lançamento
                                if (!string.IsNullOrEmpty(dtVencimento.Text))
                                {
                                    alteraDataParcela(conn, Convert.ToDateTime(dtVencimento.Text), dataGridView1.SelectedRows[0].Cells["CODLANCA"].Value.ToString());
                                }


                            }
                            catch (Exception)
                            {
                                conn.Rollback();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        alteraDataParcela(conn, Convert.ToDateTime(dtVencimento.Text), dataGridView1.SelectedRows[0].Cells["CODLANCA"].Value.ToString());
                    }
                    conn.Commit();
                    CarregaListaLancamento();
                }
            }
            return false;
        }
        private void PSPartAjustarValorFinanceiroAppFrm_Load(object sender, EventArgs e)
        {
            CarregaListaLancamento();
            txtValorTotal.Text = string.Format("{0:n2}", getValorTotal(codOper));
        }
        private void CarregaListaLancamento()
        {
            if (string.IsNullOrEmpty(codOper))
            {
                if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
                {
                    codOper = gb.RetornaDataFieldByCampo(this.psPartApp.DataField, "CODOPER").Valor.ToString();
                }
            }

            DataTable Lancamentos = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT FLANCA.CODEMPRESA, FLANCA.CODLANCA, FLANCA.NUMERO, FLANCA.DATAVENCIMENTO, FLANCA.VLORIGINAL, FLANCA.VLLIQUIDO, 0 VALORBAIXADO FROM FLANCA WHERE FLANCA.CODEMPRESA = ? AND FLANCA.CODOPER = ?", new object[] { AppLib.Context.Empresa, codOper });
            AtualizaGrid(Lancamentos);
            txtValorParcela.Text = "0";
        }
        private void AtualizaGrid(DataTable dt)
        {


            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;

            if (dataGridView1.Columns.Count > 1)
            {
                dataGridView1.Columns["CODEMPRESA"].Visible = false;
                dataGridView1.Columns["CODLANCA"].HeaderText = "Lançamento";
                dataGridView1.Columns["NUMERO"].HeaderText = "Numero";
                dataGridView1.Columns["DATAVENCIMENTO"].HeaderText = "Data de Vencimento";

                dataGridView1.Columns["VLORIGINAL"].HeaderText = "Valor Original";
                dataGridView1.Columns["VLORIGINAL"].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns["VLORIGINAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dataGridView1.Columns["VLLIQUIDO"].HeaderText = "Valor Liquido";
                dataGridView1.Columns["VLLIQUIDO"].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns["VLLIQUIDO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dataGridView1.Columns["VALORBAIXADO"].HeaderText = "Valor da Baixa";
                dataGridView1.Columns["VALORBAIXADO"].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns["VALORBAIXADO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                txtNumeroDocumento.Text = dataGridView1.SelectedRows[0].Cells["NUMERO"].Value.ToString();
                txtValorAtual.Text = string.Format("{0:n2}", dataGridView1.SelectedRows[0].Cells["VLORIGINAL"].Value);
                dtVencimento.Text = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["DATAVENCIMENTO"].Value).ToShortDateString();
                txtValorParcela.Text = txtValorAtual.Text;
            }
        }
        private void btnAplicar_Click(object sender, EventArgs e)
        {

        }
        private decimal getValorTotal(string codoper)
        {
            return Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT SUM(VLORIGINAL) VLORIGINAL FROM FLANCA WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { codoper, AppLib.Context.Empresa }));
        }
        private decimal getValorParcelas(string codoper, string codlanca)
        {
            return Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT SUM(VLORIGINAL) VLPARCELAS FROM FLANCA WHERE CODOPER = ? AND CODEMPRESA = ? AND CODLANCA < ?", new object[] { codoper, AppLib.Context.Empresa, codlanca }));
        }
        private DataTable getParcelasRestantes(string codoper, string codlanca)
        {
            return AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODLANCA FROM FLANCA WHERE CODOPER = ? AND CODEMPRESA = ? AND CODLANCA > ?", new object[] { codoper, AppLib.Context.Empresa, codlanca });
        }
        private void alteraValorItemSelecionado(decimal valor, string codlanca, AppLib.Data.Connection conn)
        {
            conn.ExecTransaction(@"UPDATE FLANCA SET VLORIGINAL = ? WHERE CODLANCA = ?", new object[] { valor, codlanca });
        }
        private void alteraValorParcelasRestantes(decimal valor, DataTable listaCodLanca, AppLib.Data.Connection conn)
        {
            for (int i = 0; i < listaCodLanca.Rows.Count; i++)
            {
                alteraValorItemSelecionado(valor, listaCodLanca.Rows[i]["CODLANCA"].ToString(), conn);

            }
        }

        private void txtValorParcela_Validating(object sender, CancelEventArgs e)
        {
            txtValorParcela.Text = string.Format("{0:n2}", txtValorParcela.Text);
        }

        private void txtValorParcela_Enter(object sender, EventArgs e)
        {
            var edit = ((DevExpress.XtraEditors.TextEdit)sender);
            BeginInvoke(new MethodInvoker(() =>
            {
                edit.SelectionStart = 0;
                edit.SelectionLength = edit.Text.Length;
            }));
        }

        private void alteraDataParcela(AppLib.Data.Connection conn, DateTime dataVencimento, string codLanca)
        {
            try
            {
                DateTime? dataEmissao = Convert.ToDateTime(conn.ExecGetField(null, @"SELECT DATAEMISSAO FROM FLANCA WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { codLanca, AppLib.Context.Empresa }));
                if (dataEmissao != null)
                {

                    if (Convert.ToDateTime(dataVencimento) > Convert.ToDateTime(dataEmissao.Value.ToShortDateString()))
                    {
                        conn.ExecTransaction(@"UPDATE FLANCA SET DATAVENCIMENTO = ? WHERE CODLANCA = ?", new object[] { dataVencimento, codLanca });
                        //alteraDataParcela(conn, Convert.ToDateTime(dtVencimento.Text), dataGridView1.SelectedRows[0].Cells["CODLANCA"].Value.ToString());
                    }
                }
            }
            catch (Exception)
            {
                
            }
           

        }




    }
}
