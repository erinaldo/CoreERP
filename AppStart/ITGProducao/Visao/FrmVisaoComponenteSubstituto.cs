using DevExpress.XtraGrid.Views.Grid;
using ITGProducao.Class;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITGProducao.Visao
{
    public partial class FrmVisaoComponenteSubstituto : Form
    {
        public string codProduto = string.Empty;
        public string codLocal = string.Empty;
        public decimal qtdNecessidade = 0;
        public DataTable dtNecessidade;
        public DataRow linhaReferencia;

        private string Param_UtilizaReserva = "";
        private string Param_GeraNecessidade = "";

        DataTable dtComponentesSubstitutos;
        DataTable dtEstoqueComponentesSubstitutos;

        public Hashtable htEstoqueInicial;

        public FrmVisaoComponenteSubstituto()
        {
            InitializeComponent();

            htEstoqueInicial = new Hashtable();
        }

        private void carregaObj(DataTable dt, string produto,decimal fator)
        {
            lookupproduto.txtcodigo.Text = produto;
            lookupproduto.CarregaDescricao();
            newLookupprodutosubstituto.txtcodigo.Text = dt.Rows[0]["VFICHAESTOQUE.CODPRODUTO"].ToString();
            newLookupprodutosubstituto.CarregaDescricao();
            txtQuantidade.Text = "0";

            txtFator.Text = fator.ToString();

            txtSaldoDisponivel.Text = dt.Rows[0]["SALDOFINAL"].ToString();

            lookupproduto.Edita(false);
            newLookupprodutosubstituto.Edita(false);
            txtQuantidade.Focus();
        }

        private void gridComponentes_DoubleClick(object sender, EventArgs e)
        {
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            DataTable dt = new DataTable();
            dt = dtEstoqueComponentesSubstitutos.Clone();

            dtEstoqueComponentesSubstitutos.Select("VFICHAESTOQUE.CODPRODUTO ='" + row1["PCOMPONENTESUB.CODPRODUTOSUB"].ToString() + "'").CopyToDataTable<DataRow>(dt, LoadOption.Upsert);
            if (dt.Rows.Count > 0)
            {
                carregaObj(dt, row1["PCOMPONENTESUB.CODPRODUTO"].ToString(), Convert.ToDecimal(row1["PCOMPONENTESUB.FATOR"].ToString()));
            }
        }

        private void carregaCampos()
        {
            if (!string.IsNullOrEmpty(codProduto))
            {
                lookupproduto.txtcodigo.Text = codProduto;
                lookupproduto.CarregaDescricao();

                lookupproduto.Edita(false);
                newLookupprodutosubstituto.Edita(false);

                CarregaGrid(codProduto);
                this.dtNecessidade = dtNecessidade.Clone();

                txtNecessidade.Text = qtdNecessidade.ToString();
            }
        }

        void CarregaGrid(string codproduto)
        {
            string tabela = "PCOMPONENTESUB";
            string relacionamento = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql = new PS.Glb.Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, " WHERE CODPRODUTO = '" + codproduto + "'");

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                gridComponentes.DataSource = null;
                gridView1.Columns.Clear();

                dtComponentesSubstitutos = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                if (dtComponentesSubstitutos.Rows.Count > 0)
                {
                    dtEstoqueComponentesSubstitutos = retornaEstoqueComponentes(dtComponentesSubstitutos);

                    DataColumn colQTD_OP = new DataColumn("QTD_OP", typeof(Decimal));
                    DataColumn colSALDOFINAL = new DataColumn("SALDOFINAL", typeof(Decimal));
                    dtEstoqueComponentesSubstitutos.Columns.Add(colQTD_OP);
                    dtEstoqueComponentesSubstitutos.Columns.Add(colSALDOFINAL);

                    DataRow[] rowSubstitutos = dtNecessidade.Select("COMPONENTESUBSTITUTO = '" + codproduto + "'");
                    DataTable dtvSubs = dtNecessidade.Clone();
                    rowSubstitutos.CopyToDataTable<DataRow>(dtvSubs, LoadOption.Upsert);

                    for (int x = 0; x <= dtEstoqueComponentesSubstitutos.Rows.Count - 1; x++)
                    {
                        htEstoqueInicial.Add(dtEstoqueComponentesSubstitutos.Rows[x]["VFICHAESTOQUE.CODPRODUTO"].ToString(), Convert.ToDecimal(dtEstoqueComponentesSubstitutos.Rows[x]["VFICHAESTOQUE.SALDOFINAL"].ToString()));

                        if (dtvSubs.Rows.Count > 0)
                        {
                            if (Param_UtilizaReserva == "1")
                            {
                                decimal sumReservar = Convert.ToDecimal(dtvSubs.Select("PORDEMCONSUMO.CODCOMPONENTE = '" + dtEstoqueComponentesSubstitutos.Rows[x]["VFICHAESTOQUE.CODPRODUTO"].ToString() + "'").Sum(row => row.Field<decimal>("QTDRESERVAR")));
                                dtEstoqueComponentesSubstitutos.Rows[x]["QTD_OP"] = sumReservar;
                                dtEstoqueComponentesSubstitutos.Rows[x]["SALDOFINAL"] = Convert.ToDecimal(dtEstoqueComponentesSubstitutos.Rows[x]["VFICHAESTOQUE.SALDOFINAL"].ToString()) - sumReservar;
                            }
                            else if (Param_UtilizaReserva == "2")
                            {
                                decimal sumBaixar = Convert.ToDecimal(dtvSubs.Select("PORDEMCONSUMO.CODCOMPONENTE = '" + dtEstoqueComponentesSubstitutos.Rows[x]["VFICHAESTOQUE.CODPRODUTO"].ToString() + "'").Sum(row => row.Field<decimal>("QTDBAIXAR")));
                                dtEstoqueComponentesSubstitutos.Rows[x]["QTD_OP"] = sumBaixar;
                                dtEstoqueComponentesSubstitutos.Rows[x]["SALDOFINAL"] = Convert.ToDecimal(dtEstoqueComponentesSubstitutos.Rows[x]["VFICHAESTOQUE.SALDOFINAL"].ToString()) - sumBaixar;
                            }
                            else
                            {
                                throw new Exception("Parâmetro Inválido: UTILIZARESERVA");
                            }
                        }
                        else
                        {
                            dtEstoqueComponentesSubstitutos.Rows[x]["QTD_OP"] = 0;
                            dtEstoqueComponentesSubstitutos.Rows[x]["SALDOFINAL"] = Convert.ToDecimal(dtEstoqueComponentesSubstitutos.Rows[x]["VFICHAESTOQUE.SALDOFINAL"]);
                        }
                    }

                    DataSet DS = new DataSet();

                    dtComponentesSubstitutos.TableName = "TbComponentesSubstitutos";
                    dtEstoqueComponentesSubstitutos.TableName = "TbEstoqueComponentesSubstitutos";

                    DS.Tables.Add(dtComponentesSubstitutos);
                    DS.Tables.Add(dtEstoqueComponentesSubstitutos);

                    DataColumn[] parent = new DataColumn[] { DS.Tables[0].Columns["PCOMPONENTESUB.CODEMPRESA"],
                                                         DS.Tables[0].Columns["PCOMPONENTESUB.CODFILIAL"],
                                                         DS.Tables[0].Columns["PCOMPONENTESUB.CODPRODUTOSUB"]};

                    DataColumn[] child = new DataColumn[] { DS.Tables[1].Columns["VFICHAESTOQUE.CODEMPRESA"],
                                                         DS.Tables[1].Columns["VFICHAESTOQUE.CODFILIAL"],
                                                         DS.Tables[1].Columns["VFICHAESTOQUE.CODPRODUTO"]};

                    DS.Relations.Add("Fk_Estoque_ComponentesSubstitutos", parent, child);

                    gridComponentes.DataSource = DS.Tables[0];

                    DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?)", new object[] { tabela });
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
                else
                {
                    this.Dispose();
                    MessageBox.Show("Este produto não possui componente substituto cadastrado.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable retornaEstoqueComponentes(DataTable dtComponentesSubstitutos)
        {
            try
            {
                string _componentes = "";

                for (int x = 0; x <= (dtComponentesSubstitutos.Rows.Count - 1); x++)
                {
                    _componentes = _componentes + (x == 0 ? "'" + dtComponentesSubstitutos.Rows[x]["PCOMPONENTESUB.CODPRODUTOSUB"].ToString() + "'" : ",'" + dtComponentesSubstitutos.Rows[x]["PCOMPONENTESUB.CODPRODUTOSUB"].ToString() + "'");
                }
                
                DataTable dtEstoqueMP = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
                    with estoque as (
						select CODEMPRESA, CODPRODUTO, CODFILIAL, CODLOCAL, SEQUENCIAL, SALDOFINAL
						, RowDesc = ROW_NUMBER() over(partition by CODPRODUTO order by SEQUENCIAL desc)
							from VFICHAESTOQUE
						where CODEMPRESA = ?
                            and CODFILIAL = ?
							and CODLOCAL = ?" +
                                (string.IsNullOrEmpty(_componentes) ? "" : "AND CODPRODUTO in (" + _componentes + ")") + @"
                                         )
	                select CODEMPRESA as 'VFICHAESTOQUE.CODEMPRESA', CODPRODUTO as 'VFICHAESTOQUE.CODPRODUTO', CODFILIAL as 'VFICHAESTOQUE.CODFILIAL', CODLOCAL as 'VFICHAESTOQUE.CODLOCAL', SEQUENCIAL as 'VFICHAESTOQUE.SEQUENCIAL', SALDOFINAL as 'VFICHAESTOQUE.SALDOFINAL', RowDesc from estoque where RowDesc = 1
            ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codLocal });

                return dtEstoqueMP;
            }
            catch (Exception ex)
            {
                throw ex ;
            }
        }

        public string VerificaParametro(string parametro)
        {
            try
            {
                Global gl = new Global();
                string _PARAM = gl.VerificaParametroString(parametro);

                return _PARAM;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void FrmVisaoComponenteSubstituto_Load(object sender, EventArgs e)
        {
            txtQuantidade.Text = "0";
            txtFator.Text = "0";

            Param_UtilizaReserva = VerificaParametro("UTILIZARESERVA");
            Param_GeraNecessidade = VerificaParametro("GERARNECESSIDADEAUTO");

            carregaCampos();
        }

        private void gridView1_MasterRowExpanded(object sender, DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs e)
        {
            try
            {
                GridView dView = gridView1.GetDetailView(e.RowHandle, 0) as GridView;

                dView.Columns["VFICHAESTOQUE.CODEMPRESA"].Visible = false;
                dView.Columns["VFICHAESTOQUE.CODFILIAL"].Visible = false;
                dView.Columns["VFICHAESTOQUE.SEQUENCIAL"].Visible = false;
                dView.Columns["VFICHAESTOQUE.CODPRODUTO"].Visible = false;
                dView.Columns["VFICHAESTOQUE.SALDOFINAL"].Visible = false;
                
                dView.Columns["RowDesc"].Visible = false;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA,DESCRICAO FROM GDICIONARIO WHERE TABELA in (?)", new object[] { "VFICHAESTOQUE" });
                for (int i = 0; i < dView.Columns.Count; i++)
                {
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { dView.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        dView.Columns[i].Caption = result["DESCRICAO"].ToString();
                       
                    }else
                    {
                        if (dView.Columns[i].FieldName.ToString() == "QTD_OP")
                        {
                            if (Param_UtilizaReserva == "1")
                            {
                                dView.Columns[i].Caption = "Qtd. à reservar na OP";
                            }
                            else if (Param_UtilizaReserva == "2")
                            {
                                dView.Columns[i].Caption = "Qtd. à baixar na OP";
                            }
                            else
                            {
                                throw new Exception("Parâmetro Inválido: UTILIZARESERVA");
                            }
                        }else if (dView.Columns[i].FieldName.ToString() == "SALDOFINAL")
                        {
                            dView.Columns[i].Caption = "Saldo Final";
                        }
                    }
                }

                dView.BestFitColumns();
                dView.OptionsView.ShowAutoFilterRow = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool Validacao(decimal qtdEstoqueMP)
        {
            try
            {
                bool valida = true;

                errorProvider.Clear();

                if (string.IsNullOrEmpty(txtQuantidade.Text))
                {
                    errorProvider.SetError(txtQuantidade, "Favor informar a quantidade");
                    valida = false;
                }
                else
                {
                    if (Convert.ToDecimal(txtQuantidade.Text) == 0)
                    {
                        errorProvider.SetError(txtQuantidade, "Favor informar a quantidade");
                        valida = false;
                    }
                    else
                    {
                        if (Convert.ToDecimal(txtQuantidade.Text) > qtdEstoqueMP)
                        {
                            errorProvider.SetError(txtQuantidade, "Quantidade informada maior que estoque disponível");
                            valida = false;
                        }
                    }
                }

                return valida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnIncluiComponenteOP_Click(object sender, EventArgs e)
        {
            try
            {
                newLookupprodutosubstituto.mensagemErrorProvider = "";

                if (!string.IsNullOrEmpty(newLookupprodutosubstituto.ValorCodigoInterno.ToString()))
                {
                    decimal qtdEstoqueMP = Convert.ToDecimal(txtSaldoDisponivel.Text);

                    if (Validacao(qtdEstoqueMP) == true)
                    {
                        DataRow row = dtNecessidade.NewRow();

                        DataTable dtProduto = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"select * from vproduto where CODEMPRESA = ? and CODPRODUTO = ?", new object[] { AppLib.Context.Empresa, newLookupprodutosubstituto.ValorCodigoInterno.ToString() });

                        if (dtProduto.Rows.Count > 0)
                        {
                            row["PORDEM.CODEMPRESA"] = linhaReferencia["PORDEM.CODEMPRESA"];
                            row["PORDEM.CODFILIAL"] = linhaReferencia["PORDEM.CODFILIAL"];
                            row["PORDEM.CODESTRUTURA"] = linhaReferencia["PORDEM.CODESTRUTURA"];
                            row["PORDEM.REVESTRUTURA"] = linhaReferencia["PORDEM.REVESTRUTURA"];
                            row["PORDEM.CODIGOOP"] = linhaReferencia["PORDEM.CODIGOOP"];
                            row["VPRODUTO.CODUNIDCONTROLE"] = linhaReferencia["VPRODUTO.CODUNIDCONTROLE"];
                            row["PORDEM.SEQOP"] = linhaReferencia["PORDEM.SEQOP"];
                            row["PROTEIROESTRUTURA.DESCESTRUTURA"] = linhaReferencia["PROTEIROESTRUTURA.DESCESTRUTURA"];
                            row["PORDEMCONSUMO.CODCOMPONENTE"] = newLookupprodutosubstituto.ValorCodigoInterno.ToString();
                            row["VPRODUTO.DESCRICAO"] = dtProduto.Rows[0]["DESCRICAO"];
                            row["QTDCOMPONENTESUBSTITUTO"] = 0;
                            row["NIVEL"] = linhaReferencia["NIVEL"];
                            row["PORDEMCONSUMO.UNDCOMPONENTE"] = dtProduto.Rows[0]["CODUNIDCONTROLE"];
                            row["PORDEMCONSUMO.QTDCOMPONENTE"] = linhaReferencia["QTDSALDO"];
                            row["COMPONENTESUBSTITUTO"] = linhaReferencia["PORDEMCONSUMO.CODCOMPONENTE"];
                            row["STATUS"] = DBNull.Value;
                            row["FATOR"] = Convert.ToDecimal(txtFator.Text);
                            row["PORDEM.QTDEPLANOP"] = linhaReferencia["PORDEM.QTDEPLANOP"];
                            row["PORDEMROTEIRO.CODOPERACAO"] = linhaReferencia["PORDEMROTEIRO.CODOPERACAO"];
                            row["PORDEMROTEIRO.SEQOPERACAO"] = linhaReferencia["PORDEMROTEIRO.SEQOPERACAO"];

                            if (Param_UtilizaReserva == "1")
                            {
                                if (htEstoqueInicial.Contains(newLookupprodutosubstituto.ValorCodigoInterno.ToString()))
                                {
                                    row["QTDESTOQUEINICIAL"] = htEstoqueInicial[newLookupprodutosubstituto.ValorCodigoInterno.ToString()];
                                }
                                else
                                {
                                    row["QTDESTOQUEINICIAL"] = qtdEstoqueMP;
                                    htEstoqueInicial.Add(newLookupprodutosubstituto.ValorCodigoInterno.ToString(), qtdEstoqueMP);
                                }
                                
                                row["QTDSOLICITACAOCOMPRAS"] = 0;
                                row["QTDBAIXAR"] = Convert.ToDecimal(0);

                                DataRow[] rows = dtNecessidade.Select("PORDEMCONSUMO.CODCOMPONENTE = '" + newLookupprodutosubstituto.ValorCodigoInterno.ToString() + "'");
                                if (rows.Count() > 0)
                                {
                                    row["QTDESTOQUE"] = Convert.ToDecimal(rows[0]["QTDESTOQUE"]) - Convert.ToDecimal(txtQuantidade.Text);
                                    row["QTDRESERVAR"] = Convert.ToDecimal(rows[0]["QTDRESERVAR"]) + Convert.ToDecimal(txtQuantidade.Text);
                                    row["QTDSALDO"] = Convert.ToDecimal(row["PORDEMCONSUMO.QTDCOMPONENTE"]) - Convert.ToDecimal(txtQuantidade.Text);

                                    dtNecessidade.Rows.Remove(rows[0]);
                                    dtNecessidade.Rows.Add(row);
                                }
                                else
                                {
                                    row["QTDESTOQUE"] = qtdEstoqueMP - Convert.ToDecimal(txtQuantidade.Text);
                                    row["QTDRESERVAR"] = Convert.ToDecimal(txtQuantidade.Text);
                                    row["QTDSALDO"] = Convert.ToDecimal(linhaReferencia["QTDSALDO"]) - Convert.ToDecimal(txtQuantidade.Text);

                                    dtNecessidade.Rows.Add(row);
                                }
                            }
                            else if (Param_UtilizaReserva == "2")
                            {
                                if (htEstoqueInicial.Contains(newLookupprodutosubstituto.ValorCodigoInterno.ToString()))
                                {
                                    row["QTDESTOQUEINICIAL"] = htEstoqueInicial[newLookupprodutosubstituto.ValorCodigoInterno.ToString()];
                                }
                                else
                                {
                                    row["QTDESTOQUEINICIAL"] = qtdEstoqueMP;
                                    htEstoqueInicial.Add(newLookupprodutosubstituto.ValorCodigoInterno.ToString(), qtdEstoqueMP);
                                }

                                row["QTDSOLICITACAOCOMPRAS"] = 0;
                                row["QTDRESERVAR"] = Convert.ToDecimal(0);

                                DataRow[] rows = dtNecessidade.Select("PORDEMCONSUMO.CODCOMPONENTE = '" + newLookupprodutosubstituto.ValorCodigoInterno.ToString() + "'");
                                if (rows.Count() > 0)
                                {
                                    row["QTDESTOQUE"] = Convert.ToDecimal(rows[0]["QTDESTOQUE"]) - Convert.ToDecimal(txtQuantidade.Text);
                                    row["QTDBAIXAR"] = Convert.ToDecimal(rows[0]["QTDBAIXAR"]) + Convert.ToDecimal(txtQuantidade.Text); 
                                    row["QTDSALDO"] = Convert.ToDecimal(row["PORDEMCONSUMO.QTDCOMPONENTE"]) - Convert.ToDecimal(txtQuantidade.Text); 

                                    dtNecessidade.Rows.Remove(rows[0]);
                                    dtNecessidade.Rows.Add(row);
                                }
                                else
                                {
                                    row["QTDESTOQUE"] = qtdEstoqueMP - Convert.ToDecimal(txtQuantidade.Text);
                                    row["QTDBAIXAR"] = Convert.ToDecimal(txtQuantidade.Text);
                                    row["QTDSALDO"] = Convert.ToDecimal(linhaReferencia["QTDSALDO"]) - Convert.ToDecimal(txtQuantidade.Text);

                                    dtNecessidade.Rows.Add(row);
                                }
                            }
                            else
                            {
                                throw new Exception("Parâmetro Inválido: UTILIZARESERVA");
                            }

                            int indexRow = dtEstoqueComponentesSubstitutos.Rows.IndexOf(dtEstoqueComponentesSubstitutos.Select("VFICHAESTOQUE.CODPRODUTO = '" + newLookupprodutosubstituto.ValorCodigoInterno.ToString() + "'")[0]);
                            dtEstoqueComponentesSubstitutos.Rows[indexRow]["SALDOFINAL"] = Convert.ToDecimal(dtEstoqueComponentesSubstitutos.Rows[indexRow]["SALDOFINAL"]) - Convert.ToDecimal(txtQuantidade.Text);
                            dtEstoqueComponentesSubstitutos.Rows[indexRow]["QTD_OP"] = Convert.ToDecimal(dtEstoqueComponentesSubstitutos.Rows[indexRow]["QTD_OP"]) + Convert.ToDecimal(txtQuantidade.Text);

                            MessageBox.Show("Produto substituto incluído com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            newLookupprodutosubstituto.Clear();
                            txtQuantidade.Text = "0";
                            txtSaldoCalculado.Text = "0";
                            txtFator.Text = "0";
                            txtSaldoDisponivel.Text = "0";
                        }
                        else
                        {
                            throw new Exception("Erro Ao Incluir Componente Substituto");
                        }
                    }
                }
                else
                {
                    newLookupprodutosubstituto.mensagemErrorProvider = "Favor selecionar um produto.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void txtQuantidade_KeyUp(object sender, KeyEventArgs e)
        {
            txtSaldoCalculado.Text = Convert.ToString(Convert.ToDecimal(txtQuantidade.Text) * Convert.ToDecimal(txtFator.Text));
        }
    }
}
