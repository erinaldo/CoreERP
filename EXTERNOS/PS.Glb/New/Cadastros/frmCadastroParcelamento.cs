using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroParcelamento : Form
    {
        public string codLanca = string.Empty;
        DataTable dt;

        public frmCadastroParcelamento()
        {
            InitializeComponent();
        }

        private void frmCadastroParcelamento_Load(object sender, EventArgs e)
        {
            CarregaCampos();
            LimpaGrid();
        }

        private void LimpaGrid()
        {
            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT TOP 0 FLANCA.DATAVENCIMENTO, FLANCA.VLORIGINAL FROM FLANCA ", new object[] { });
            gridParcelas.DataSource = dt;

            DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { "FLANCA" });
            DataTable dtvisaousuario = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { "FLANCA", AppLib.Context.Usuario });
            for (int i = 0; i < gridViewParcelas.Columns.Count; i++)
            {
                gridViewParcelas.Columns[i].Width = Convert.ToInt32(dtvisaousuario.Rows[i]["LARGURA"]);
                dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                DataRow result = dic.Rows.Find(new object[] { string.Concat("FLANCA.", gridViewParcelas.Columns[i].FieldName.ToString()) });

                if (result != null)
                {
                    gridViewParcelas.Columns[i].Caption = result["DESCRICAO"].ToString();
                    if (gridViewParcelas.Columns[i].Caption.Contains("Valor Original"))
                    {
                        gridViewParcelas.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gridViewParcelas.Columns[i].DisplayFormat.FormatString = "n2";
                    }
                }
            }
            gridViewParcelas.BestFitColumns();
        }

        private void CarregaCampos()
        {
            if (!string.IsNullOrEmpty(codLanca))
            {
                DataTable dtLancamentoSelecionado = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT FLANCA.CODEMPRESA, FLANCA.CODLANCA, FLANCA.NUMERO, FLANCA.DATAVENCIMENTO, FLANCA.VLORIGINAL, FLANCA.VLLIQUIDO, 0 VALORBAIXADO FROM FLANCA WHERE FLANCA.CODEMPRESA = ? AND FLANCA.CODLANCA = ?", new object[] { AppLib.Context.Empresa, codLanca });
                if (dtLancamentoSelecionado.Rows.Count > 0)
                {
                    codLanca = dtLancamentoSelecionado.Rows[0]["CODLANCA"].ToString();
                    txtNumeroDocumento.Text = dtLancamentoSelecionado.Rows[0]["NUMERO"].ToString();
                    txtValorLiquidoAtual.Text = Convert.ToDecimal(dtLancamentoSelecionado.Rows[0]["VLLIQUIDO"].ToString()).ToString("N2");
                    dtVencimento.Text = Convert.ToDateTime(dtLancamentoSelecionado.Rows[0]["DATAVENCIMENTO"].ToString()).ToShortDateString();
                    txtQtdeParcelas.Text = "0";
                }
                else
                {
                    MessageBox.Show("Erro ao selecionar o registro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Erro ao selecionar o registro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGerarParcelas_Click(object sender, EventArgs e)
        {
            LimpaGrid();

            if (Convert.ToInt16(txtQtdeParcelas.Text) > 0)
            {
                decimal valorParcelas = Convert.ToDecimal(Convert.ToDecimal(txtValorLiquidoAtual.Text) / Convert.ToInt16(txtQtdeParcelas.Text));
                for (int i = 1; i <= Convert.ToInt16(txtQtdeParcelas.Text); i++)
                {
                    DataRow row = dt.NewRow();

                    row["DATAVENCIMENTO"] = Convert.ToDateTime(dtVencimento.Text).AddMonths(i-1);
                    row["VLORIGINAL"] = valorParcelas;

                    dt.Rows.Add(row);
                }
            }
            else
            {
                MessageBox.Show("Favor informar a quantidade de parcelas.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            btnSalvarAtual.Enabled = true;
            btnOKAtual.Enabled = true;
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void gridViewParcelas_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            int index = gridViewParcelas.GetDataSourceRowIndex(gridViewParcelas.FocusedRowHandle);
            decimal valortotal = 0;

            valortotal =  Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT FLANCA.VLLIQUIDO FROM FLANCA WHERE FLANCA.CODEMPRESA = ? AND FLANCA.CODLANCA = ?", new object[] { AppLib.Context.Empresa, codLanca }));

            decimal valorparcelasanteriores = 0;

            for (int i = 0; i < index; i++)
            {
                valorparcelasanteriores = valorparcelasanteriores + Convert.ToDecimal(dt.Rows[i]["VLORIGINAL"].ToString());
            }

            decimal valorparcelaalterada = Convert.ToDecimal(dt.Rows[index]["VLORIGINAL"].ToString());

            decimal novovalor = (valortotal - valorparcelasanteriores) - valorparcelaalterada;
            if (novovalor < 0)
            {
                btnSalvarAtual.Enabled = false;
                btnOKAtual.Enabled = false;

                for (int idx = (index + 1); idx < dt.Rows.Count; idx++)
                {
                    dt.Rows[idx]["VLORIGINAL"] = 0;
                }

                MessageBox.Show("Favor informar um valor válido.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
                return;
            }
            else
            {
                btnSalvarAtual.Enabled = true;
                btnOKAtual.Enabled = true;

                decimal novovalorparcela = novovalor / (dt.Rows.Count - (index + 1));
                for (int idx = (index + 1); idx < dt.Rows.Count; idx++)
                {
                    dt.Rows[idx]["VLORIGINAL"] = novovalorparcela;
                }
            }
        }

        private void gridViewParcelas_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int index = gridViewParcelas.GetDataSourceRowIndex(gridViewParcelas.FocusedRowHandle);

            if (index == (dt.Rows.Count - 1) && gridViewParcelas.FocusedColumn.Name == "colVLORIGINAL")
            {
                gridViewParcelas.OptionsBehavior.Editable = false;
            }
            else
            {
                gridViewParcelas.OptionsBehavior.Editable = true;
            }
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                MessageBox.Show("Cadastro salvo com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private int getCodLanca(AppLib.Data.Connection conn)
        {
            int retorno = Convert.ToInt32(conn.ExecGetField(0, "SELECT IDLOG FROM GLOG WHERE CODTABELA = 'FLANCA' AND CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }));
            if (retorno > 0)
            {
                return retorno + 1;
            }
            else
            {
                return 0;
            }
        }

        private bool Salvar()
        {
            bool _salvar = false;

            if (validacao() == true)
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit FLANCA = new AppLib.ORM.Jit(conn, "FLANCA");
                conn.BeginTransaction();
                try
                {
                    DataTable dtLancamentoCopia = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FLANCA where CODEMPRESA = ? and CODLANCA = ?", new object[] { AppLib.Context.Empresa, codLanca });
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count ; i++)
                        {
                            FLANCA.Set("CODEMPRESA", AppLib.Context.Empresa);
                            FLANCA.Set("CODLANCA", getCodLanca(conn));
                            FLANCA.Set("TIPOPAGREC", dtLancamentoCopia.Rows[0]["TIPOPAGREC"].ToString());

                            DataTable dtnumero = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT USANUMEROSEQ, ULTIMONUMERO, MASKNUMEROSEQ  FROM FTIPDOC INNER JOIN VPARAMETROS ON FTIPDOC.CODEMPRESA = VPARAMETROS.CODEMPRESA WHERE FTIPDOC.CODEMPRESA = ? AND CODTIPDOC = ?", new object[] { AppLib.Context.Empresa, dtLancamentoCopia.Rows[0]["CODTIPDOC"].ToString() });
                            if (dtnumero.Rows.Count > 0)
                            {
                                if (dtnumero.Rows[0]["USANUMEROSEQ"].ToString() == "1")
                                {
                                    string ultnumero = (Convert.ToInt32(dtnumero.Rows[0]["ULTIMONUMERO"])).ToString();
                                    ultnumero  = AppLib.Util.Format.CompletarZeroEsquerda(dtnumero.Rows[0]["MASKNUMEROSEQ"].ToString().Length, ultnumero);
                                    FLANCA.Set("NUMERO", ultnumero);
                                }
                                else
                                {
                                    FLANCA.Set("NUMERO", dtLancamentoCopia.Rows[0]["NUMERO"].ToString() + "-" + Convert.ToInt16(i+1).ToString("00"));
                                }
                            }

                            FLANCA.Set("CODCLIFOR", dtLancamentoCopia.Rows[0]["CODCLIFOR"].ToString());
                            FLANCA.Set("NOMECLIENTE", dtLancamentoCopia.Rows[0]["NOMECLIENTE"].ToString());
                            FLANCA.Set("CODFILIAL", dtLancamentoCopia.Rows[0]["CODFILIAL"].ToString());

                            FLANCA.Set("DATAEMISSAO", conn.GetDateTime());

                            DateTime dtvencimento = Convert.ToDateTime(dt.Rows[i]["DATAVENCIMENTO"].ToString());
                            FLANCA.Set("DATAVENCIMENTO", dtvencimento);
                            FLANCA.Set("DATAPREVBAIXA", dtvencimento);

                            FLANCA.Set("OBSERVACAO", dtLancamentoCopia.Rows[0]["OBSERVACAO"].ToString());
                            FLANCA.Set("CODMOEDA", dtLancamentoCopia.Rows[0]["CODMOEDA"].ToString());
                            FLANCA.Set("VLORIGINAL", Convert.ToDecimal(dt.Rows[i]["VLORIGINAL"].ToString()));
                            FLANCA.Set("PRDESCONTO", 0);
                            FLANCA.Set("VLDESCONTO", 0);
                            FLANCA.Set("PRMULTA",0);
                            FLANCA.Set("VLMULTA", 0);
                            FLANCA.Set("PRJUROS", 0);
                            FLANCA.Set("VLJUROS", 0);
                            FLANCA.Set("CODSTATUS", 0); //ABERTO

                            FLANCA.Set("CODCONTA", string.IsNullOrEmpty(dtLancamentoCopia.Rows[0]["CODCONTA"].ToString()) ? null : dtLancamentoCopia.Rows[0]["CODCONTA"].ToString());
                            FLANCA.Set("CODTIPDOC", string.IsNullOrEmpty(dtLancamentoCopia.Rows[0]["CODTIPDOC"].ToString()) ? null : dtLancamentoCopia.Rows[0]["CODTIPDOC"].ToString());
                            FLANCA.Set("VLBAIXADO", Convert.ToDecimal(0));
                            FLANCA.Set("SEGUNDONUMERO", dtLancamentoCopia.Rows[0]["SEGUNDONUMERO"].ToString());
                            FLANCA.Set("CODCCUSTO", string.IsNullOrEmpty(dtLancamentoCopia.Rows[0]["CODCCUSTO"].ToString()) ? null : dtLancamentoCopia.Rows[0]["CODCCUSTO"].ToString());
                            FLANCA.Set("CODDEPTO", string.IsNullOrEmpty(dtLancamentoCopia.Rows[0]["CODDEPTO"].ToString()) ? null : dtLancamentoCopia.Rows[0]["CODDEPTO"].ToString());
                            FLANCA.Set("CODFORMA", string.IsNullOrEmpty(dtLancamentoCopia.Rows[0]["CODFORMA"].ToString()) ? null : dtLancamentoCopia.Rows[0]["CODFORMA"].ToString());
                            FLANCA.Set("ORIGEM", dtLancamentoCopia.Rows[0]["ORIGEM"].ToString());
                            FLANCA.Set("DATACRIACAO", conn.GetDateTime());
                            FLANCA.Set("CODUSUARIOCRIACAO", AppLib.Context.Usuario);

                            FLANCA.Set("CODNATUREZAORCAMENTO", string.IsNullOrEmpty(dtLancamentoCopia.Rows[0]["CODNATUREZAORCAMENTO"].ToString()) ? null : dtLancamentoCopia.Rows[0]["CODNATUREZAORCAMENTO"].ToString());
                            FLANCA.Set("VLVINCAD", 0);
                            FLANCA.Set("VLVINCDV", 0);
                            
                            FLANCA.Set("NFOUDUP", "3");
                            FLANCA.Set("NSEQLANCA", 1);
                            FLANCA.Set("CODREPRE", string.IsNullOrEmpty(dtLancamentoCopia.Rows[0]["CODREPRE"].ToString()) ? null : dtLancamentoCopia.Rows[0]["CODREPRE"].ToString());
                            FLANCA.Set("CODDEPTO", string.IsNullOrEmpty(dtLancamentoCopia.Rows[0]["CODDEPTO"].ToString()) ? null : dtLancamentoCopia.Rows[0]["CODDEPTO"].ToString());
                            FLANCA.Set("CODVENDEDOR", string.IsNullOrEmpty(dtLancamentoCopia.Rows[0]["CODVENDEDOR"].ToString()) ? null : dtLancamentoCopia.Rows[0]["CODVENDEDOR"].ToString());

                            FLANCA.Set("CODLANCAPARCELAMENTO", codLanca);
                            
                            FLANCA.Save();

                            conn.ExecTransaction("UPDATE GLOG SET IDLOG = ? WHERE CODTABELA = ? AND CODEMPRESA = ?", new object[] { FLANCA.Get("CODLANCA"), "FLANCA", AppLib.Context.Empresa });
                        }
                        conn.ExecTransaction("update FLANCA set CODSTATUS = 5 where CODEMPRESA = ? and CODLANCA = ?", new object[] { AppLib.Context.Empresa, codLanca });

                        conn.Commit();

                        _salvar = true;
                    }
                    else
                    {
                        MessageBox.Show("Erro ao salvar parcelamento, contate o administrador!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Rollback();
                }
            }

            return _salvar;
        }

        private bool validacao()
        {
            bool _validacao = true;
            decimal valorTotal = 0;

            for (int i = 0; i < (dt.Rows.Count); i++)
            {
                valorTotal = valorTotal + Convert.ToDecimal(dt.Rows[i]["VLORIGINAL"].ToString());

                if (dt.Rows[i]["VLORIGINAL"].ToString() == "0")
                {
                    MessageBox.Show("Favor corrigir o valor das parcelas com valor R$ 0,00.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _validacao = false;
                }
            }

            if (valorTotal != Convert.ToDecimal(txtValorLiquidoAtual.Text))
            {
                MessageBox.Show("Valor total das parcelas diferente do valor total do documento.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _validacao = false;
            }

            return _validacao;
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirma os valores do parcelamento?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (Salvar() == true)
                {
                    this.Dispose();
                }
            }
            else
            {
                return;
            }         
        }

        private void gridViewParcelas_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int index = gridViewParcelas.GetDataSourceRowIndex(gridViewParcelas.FocusedRowHandle);

            if (index == (dt.Rows.Count - 1) && gridViewParcelas.FocusedColumn.Name == "colVLORIGINAL")
            {
                gridViewParcelas.OptionsBehavior.Editable = false;
                return;
            }
            else
            {
                gridViewParcelas.OptionsBehavior.Editable = true;
            }
        }
    }
}
