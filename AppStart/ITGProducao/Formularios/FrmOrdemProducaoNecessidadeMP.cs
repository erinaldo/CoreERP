using ITGProducao.Class;
using ITGProducao.Visao;
using PS.Glb;
using PS.Glb.Class;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static ITGProducao.Class.Global;

namespace ITGProducao.Formularios
{
    public partial class FrmOrdemProducaoNecessidadeMP : Form
    {
        public string codOrdem = string.Empty; //Variável para armazenamento do código da ordem de produção
        public string seqOrdem = string.Empty; //Variável para armazenamento do número de sequencia da ordem de produção
        public string codEstrutura = string.Empty; //Variável para armazenamento do código da estrutura da ordem de produção
        public string CodRevEstrutura = string.Empty; //Variável para armazenamento do código da revisão da estrutura da ordem de produção

        public DataTable dtFichaEstoque;
        private Hashtable htFichaEstoque; //Variável para armazenamento do estoque dos produtos
        private List<string> _Listacomponentes;
        private string Param_UtilizaReserva = ""; //Variável para armazenamento do parametro UTILIZARESERVA da tabela PPARAM
        private string Param_GeraNecessidade = ""; //Variável para armazenamento do parametro GERARNECESSIDADEAUTO da tabela PPARAM
        DataTable dtNecessidade; //Datatable contendo a quantidade da necessidade

        //Lista de Status para cada produto no planejamento da ordem de produção
        private enum StatusPlanejamentoMP
        {
            ComponenteComsumido = 1, //QTDRESERVAR OU QTDBAIXAR = 100%
            EstoqueInsuficiente = 2, //PORDEMCONSUMO.QTDCOMPONENTE > QTDESTOQUE and QTDSALDO > 0
            EstoqueSuficiente = 3, //QTDESTOQUE > PORDEMCONSUMO.QTDCOMPONENTE and QTDSALDO > 0
            PlanejamentoRealizado = 4, //(QTDRESERVAR OU QTDBAIXAR) <> PORDEMCONSUMO.QTDCOMPONENTE and QTDSALDO > 0
            Erro = 99
        }

        public FrmOrdemProducaoNecessidadeMP(string codOrdem, DataTable dtFichaEstoque, List<string> _Listacomponentes)
        {
            InitializeComponent();
            this.codOrdem = codOrdem;
            this.dtFichaEstoque = dtFichaEstoque;
            this._Listacomponentes = _Listacomponentes;

            Param_UtilizaReserva = VerificaParametro("UTILIZARESERVA");
            Param_GeraNecessidade = VerificaParametro("GERARNECESSIDADEAUTO");

            CarregaNecessidadeOP(codOrdem, dtFichaEstoque);
        }

        private void ValidaGrid()
        {
            CabecalhoGrid();
            gridView1.BestFitColumns();
        }

        private void CriaColunas(DataTable dtFichaEstoque)
        {
            try
            {
                DataColumn colCOMPONENTESUBSTITUTO = new DataColumn("COMPONENTESUBSTITUTO", typeof(string));
                DataColumn colSTATUS = new DataColumn("STATUS", typeof(Int32));
                DataColumn colQTDESTOQUEINICIAL = new DataColumn("QTDESTOQUEINICIAL", typeof(Decimal));
                DataColumn colQTDESTOQUE = new DataColumn("QTDESTOQUE", typeof(Decimal));
                DataColumn colQTDRESERVAR = new DataColumn("QTDRESERVAR", typeof(Decimal));
                DataColumn colQTDBAIXAR = new DataColumn("QTDBAIXAR", typeof(Decimal));
                DataColumn colQTDSOLICITACAOCOMPRAS = new DataColumn("QTDSOLICITACAOCOMPRAS", typeof(Decimal));
                DataColumn colQTDCOMPONENTESUBSTITUTO = new DataColumn("QTDCOMPONENTESUBSTITUTO", typeof(Decimal));
                DataColumn colQTDSALDO = new DataColumn("QTDSALDO", typeof(Decimal));
                DataColumn colFator = new DataColumn("FATOR", typeof(Decimal));

                dtFichaEstoque.Columns.Add(colCOMPONENTESUBSTITUTO);
                dtFichaEstoque.Columns.Add(colSTATUS);
                dtFichaEstoque.Columns.Add(colQTDESTOQUEINICIAL);
                dtFichaEstoque.Columns.Add(colQTDESTOQUE);
                dtFichaEstoque.Columns.Add(colQTDRESERVAR);
                dtFichaEstoque.Columns.Add(colQTDBAIXAR);
                dtFichaEstoque.Columns.Add(colQTDSOLICITACAOCOMPRAS);
                dtFichaEstoque.Columns.Add(colQTDCOMPONENTESUBSTITUTO);
                dtFichaEstoque.Columns.Add(colQTDSALDO);
                dtFichaEstoque.Columns.Add(colFator);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void CabecalhoGrid()
        {
            DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in ('PORDEM','PORDEMROTEIRO','PORDEMCONSUMO','PROTEIROESTRUTURA','VPRODUTO')");
            DataTable dtvisaousuario = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO in ('PORDEM','PORDEMROTEIRO','PORDEMCONSUMO','PROTEIROESTRUTURA','VPRODUTO') AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { AppLib.Context.Usuario });
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = Convert.ToInt32(dtvisaousuario.Rows[i]["LARGURA"]);
                dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                DataRow result = dic.Rows.Find(new object[] { gridView1.Columns[i].FieldName.ToString() });
                gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                if (result != null)
                {
                    gridView1.Columns[i].Caption = result["DESCRICAO"].ToString();
                }

                switch (gridView1.Columns[i].Name)
                {
                    case "colPORDEM.CODEMPRESA":
                        gridView1.Columns[i].Visible = false;
                        break;
                    case "colPORDEM.CODFILIAL":
                        gridView1.Columns[i].Visible = false;
                        break;
                    case "colPORDEM.CODESTRUTURA":
                        gridView1.Columns[i].Visible = false;
                        break;
                    case "colPORDEM.REVESTRUTURA":
                        gridView1.Columns[i].Visible = false;
                        break;
                    case "colPORDEM.CODIGOOP":
                        gridView1.Columns[i].Visible = false;
                        break;
                    case "colPORDEMROTEIRO.CODOPERACAO":
                        gridView1.Columns[i].Visible = false;
                        break;
                    case "colPORDEMROTEIRO.SEQOPERACAO":
                        gridView1.Columns[i].Visible = false;
                        break;
                    case "colPORDEM.QTDEPLANOP":
                        gridView1.Columns[i].Visible = false;
                        break;
                    case "colVPRODUTO.CODUNIDCONTROLE":
                        gridView1.Columns[i].Visible = false;
                        break;
                    case "colPORDEM.DATAINIPLANOP":
                        gridView1.Columns[i].Visible = false;
                        break;
                    case "colCOMPONENTESUBSTITUTO":
                        gridView1.Columns[i].Caption = "Componente Subistituto de";
                        break;
                    case "colSTATUS":
                        gridView1.Columns[i].Caption = "Status";
                        break;
                    case "colQTDESTOQUEINICIAL":
                        gridView1.Columns[i].Caption = "Estoque Inicial";
                        gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                        gridView1.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gridView1.Columns[i].DisplayFormat.FormatString = "n2";
                        break;
                    case "colQTDESTOQUE":
                        gridView1.Columns[i].Caption = "Quantidade Estoque";
                        gridView1.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gridView1.Columns[i].DisplayFormat.FormatString = "n2";
                        break;
                    case "colQTDRESERVAR":
                        if (Param_UtilizaReserva == "1")
                        {
                            gridView1.Columns["QTDRESERVAR"].OptionsColumn.AllowEdit = true;
                        }
                        else if (Param_UtilizaReserva == "2")
                        {
                            gridView1.Columns["QTDRESERVAR"].OptionsColumn.AllowEdit = false;
                            gridView1.Columns["QTDRESERVAR"].Visible = false;
                        }
                        else
                        {
                            gridView1.Columns["QTDRESERVAR"].OptionsColumn.AllowEdit = false;
                        }
                        gridView1.Columns[i].Caption = "Quantidade à Reservar";
                        gridView1.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gridView1.Columns[i].DisplayFormat.FormatString = "n2";
                        break;
                    case "colQTDBAIXAR":
                        if (Param_UtilizaReserva == "1")
                        {
                            gridView1.Columns["QTDBAIXAR"].OptionsColumn.AllowEdit = false;
                        }
                        else if (Param_UtilizaReserva == "2")
                        {
                            gridView1.Columns["QTDBAIXAR"].OptionsColumn.AllowEdit = true;
                        }
                        else
                        {
                            gridView1.Columns["QTDBAIXAR"].OptionsColumn.AllowEdit = false;
                        }
                        gridView1.Columns[i].Caption = "Quantidade à Baixar";
                        gridView1.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gridView1.Columns[i].DisplayFormat.FormatString = "n2";
                        break;
                    case "colQTDSOLICITACAOCOMPRAS":
                        gridView1.Columns[i].Caption = "Quantidade à Solicitar Compra";
                        gridView1.Columns[i].OptionsColumn.AllowEdit = true;
                        gridView1.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gridView1.Columns[i].DisplayFormat.FormatString = "n2";
                        break;
                    case "colQTDCOMPONENTESUBSTITUTO":
                        gridView1.Columns[i].Caption = "Qtd. Componente Subistituto";
                        gridView1.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gridView1.Columns[i].DisplayFormat.FormatString = "n2";
                        break;
                    case "colQTDSALDO":
                        gridView1.Columns[i].Caption = "Saldo";
                        gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                        gridView1.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gridView1.Columns[i].DisplayFormat.FormatString = "n2";
                        break;
                    case "colFATOR":
                        gridView1.Columns[i].Caption = "Fator";
                        gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                        gridView1.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gridView1.Columns[i].DisplayFormat.FormatString = "n2";
                        break;
                }
            }
        }

        private void CarregaNecessidadeOP(string codOrdem, DataTable dtFichaEstoque)
        {
            try
            {
                dtNecessidade = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
                        SELECT  A.CODEMPRESA AS 'PORDEM.CODEMPRESA',A.CODFILIAL AS 'PORDEM.CODFILIAL',A.CODESTRUTURA AS 'PORDEM.CODESTRUTURA',A.REVESTRUTURA AS 'PORDEM.REVESTRUTURA',A.CODIGOOP AS 'PORDEM.CODIGOOP',E.CODUNIDCONTROLE  AS 'VPRODUTO.CODUNIDCONTROLE',A.QTDEPLANOP AS 'PORDEM.QTDEPLANOP', B.CODOPERACAO AS 'PORDEMROTEIRO.CODOPERACAO', B.SEQOPERACAO AS 'PORDEMROTEIRO.SEQOPERACAO',
                                A.SEQOP as 'PORDEM.SEQOP', D.DESCESTRUTURA as 'PROTEIROESTRUTURA.DESCESTRUTURA', C.CODCOMPONENTE as 'PORDEMCONSUMO.CODCOMPONENTE', E.DESCRICAO as 'VPRODUTO.DESCRICAO',C.UNDCOMPONENTE as 'PORDEMCONSUMO.UNDCOMPONENTE',C.QTDCOMPONENTE as 'PORDEMCONSUMO.QTDCOMPONENTE',A.NIVEL,A.DATAINIPLANOP as 'PORDEM.DATAINIPLANOP'
	                      FROM PORDEM A  JOIN PORDEMROTEIRO B ON A.CODEMPRESA = B.CODEMPRESA		
													                     AND A.CODFILIAL = B.CODFILIAL
													                     AND A.CODESTRUTURA = B.CODESTRUTURA
													                     AND A.REVESTRUTURA = B.REVESTRUTURA
													                     AND A.CODIGOOP = B.CODIGOOP
													                     AND A.SEQOP = B.SEQOP
					                     JOIN PORDEMCONSUMO C ON B.CODEMPRESA = C.CODEMPRESA		
													                     AND B.CODFILIAL = C.CODFILIAL
													                     AND B.CODESTRUTURA = C.CODESTRUTURA
													                     AND B.REVESTRUTURA = C.REVESTRUTURA
													                     AND B.CODIGOOP = C.CODIGOOP
													                     AND B.SEQOP = C.SEQOP
													                     AND B.SEQOPERACAO = C.SEQOPERACAO
													                     AND B.CODOPERACAO = C.CODOPERACAO
					                     JOIN PROTEIROESTRUTURA D ON A.CODEMPRESA = D.CODEMPRESA
													                     AND A.CODFILIAL = D.CODFILIAL
													                     AND A.CODESTRUTURA = D.CODESTRUTURA
													                     AND A.REVESTRUTURA = D.REVESTRUTURA
					                      JOIN VPRODUTO E ON C.CODEMPRESA = E.CODEMPRESA
													                     AND C.CODCOMPONENTE = E.CODPRODUTO
                         WHERE A.CODEMPRESA = ? 
                           AND A.CODFILIAL = ?
	                       AND A.CODIGOOP = ?
                           AND C.TIPOCONSUMO = 'N'
                      ORDER BY A.NIVEL DESC"
                    , new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codOrdem });

                CriaColunas(dtNecessidade);

                CarregaEstoqueMP(ref dtNecessidade, dtFichaEstoque);
                grid.DataSource = dtNecessidade;

                ValidaGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Hashtable CarregahtFichaEstoque(DataTable dtFichaEstoque)
        {
            try
            {
                htFichaEstoque = new Hashtable();
                string _componentes = "";

                for (int i = 0; i <= this._Listacomponentes.Count() - 1; i++)
                {
                    htFichaEstoque.Add(_Listacomponentes[i].ToString(), 0);
                    _componentes = _componentes + (i == 0 ? "'" + _Listacomponentes[i].ToString() + "'" : ",'" + _Listacomponentes[i].ToString() + "'");
                }

                for (int i = 0; i <= dtFichaEstoque.Rows.Count - 1; i++)
                {
                    if (!htFichaEstoque.Contains(dtFichaEstoque.Rows[i]["CODPRODUTO"].ToString()))
                    {
                        throw new Exception("Erro ao carregar estoque de produtos da ordem de produção.");
                    }
                    else
                    {
                        htFichaEstoque[dtFichaEstoque.Rows[i]["CODPRODUTO"].ToString()] = Convert.ToDecimal(dtFichaEstoque.Rows[i]["SALDOFINAL"]);
                    }
                }

                //string sql = new PS.Glb.Class.Utilidades().getVisao("PCOMPONENTESUB", string.Empty, null, " WHERE CODPRODUTO in (" + _componentes + ")");
                string sql = "SELECT PCOMPONENTESUB.CODEMPRESA AS 'PCOMPONENTESUB.CODEMPRESA', PCOMPONENTESUB.CODFILIAL AS 'PCOMPONENTESUB.CODFILIAL', PCOMPONENTESUB.CODPRODUTO AS 'PCOMPONENTESUB.CODPRODUTO', PCOMPONENTESUB.CODPRODUTOSUB AS 'PCOMPONENTESUB.CODPRODUTOSUB', PCOMPONENTESUB.FATOR AS 'PCOMPONENTESUB.FATOR', PCOMPONENTESUB.PRIORIDADE AS 'PCOMPONENTESUB.PRIORIDADE' FROM PCOMPONENTESUB WHERE CODPRODUTO in (" + _componentes + ")";
                DataTable dtComponentesSubstitutos = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                string Param_LocalEstoqueMP = VerificaParametro("LOCALESTOQUEMP");
                _componentes = "";

                for (int i = 0; i <= dtComponentesSubstitutos.Rows.Count - 1; i++)
                {
                    _componentes = _componentes + (i == 0 ? "'" + dtComponentesSubstitutos.Rows[i]["PCOMPONENTESUB.CODPRODUTOSUB"].ToString() + "'" : ",'" + dtComponentesSubstitutos.Rows[i]["PCOMPONENTESUB.CODPRODUTOSUB"].ToString() + "'");
                    if (!htFichaEstoque.Contains(dtComponentesSubstitutos.Rows[i]["PCOMPONENTESUB.CODPRODUTOSUB"].ToString()))
                    {
                        htFichaEstoque.Add(dtComponentesSubstitutos.Rows[i]["PCOMPONENTESUB.CODPRODUTOSUB"].ToString(), 0);
                    }
                }

                if (!string.IsNullOrEmpty(_componentes))
                {
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
                ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, Param_LocalEstoqueMP });

                    for (int i = 0; i <= dtEstoqueMP.Rows.Count - 1; i++)
                    {
                        if (!htFichaEstoque.Contains(dtEstoqueMP.Rows[i]["VFICHAESTOQUE.CODPRODUTO"].ToString()))
                        {
                            throw new Exception("Erro ao carregar estoque de produtos da ordem de produção.");
                        }
                        else
                        {
                            htFichaEstoque[dtEstoqueMP.Rows[i]["VFICHAESTOQUE.CODPRODUTO"].ToString()] = Convert.ToDecimal(dtEstoqueMP.Rows[i]["VFICHAESTOQUE.SALDOFINAL"]);
                        }
                    }
                }

                return htFichaEstoque;
            }
            catch (Exception ex)
            {
                throw ex;
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


        private void CarregaEstoqueMP(ref DataTable dtNecessidade, DataTable dtFichaEstoque)
        {
            try
            {
                htFichaEstoque = CarregahtFichaEstoque(dtFichaEstoque);

                if (Param_UtilizaReserva == "1") // Utiliza Reserva = Sim
                {
                    for (int i = 0; i <= dtNecessidade.Rows.Count - 1; i++)
                    {
                        if (htFichaEstoque.Contains(dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()))
                        {
                            Decimal NovoEstoque = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]) - Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString());
                            if (NovoEstoque < 0)
                            {
                                dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"] = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]);
                                dtNecessidade.Rows[i]["QTDESTOQUE"] = Convert.ToDecimal(0);
                                dtNecessidade.Rows[i]["QTDRESERVAR"] = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]);
                                dtNecessidade.Rows[i]["QTDBAIXAR"] = Convert.ToDecimal(0);
                                dtNecessidade.Rows[i]["QTDESTOQUE"] = Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"]);
                                if (Param_GeraNecessidade == "1")
                                {
                                    dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"] = Math.Abs(NovoEstoque);
                                    dtNecessidade.Rows[i]["QTDSALDO"] = Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString()) - Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]) - Convert.ToDecimal(0) - Math.Abs(NovoEstoque);
                                }
                                else
                                {
                                    dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"] = 0;
                                    dtNecessidade.Rows[i]["QTDSALDO"] = (Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]) + Convert.ToDecimal(0) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"])) - Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString());
                                }

                                htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()] = 0;
                            }
                            else
                            {
                                dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"] = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]);
                                dtNecessidade.Rows[i]["QTDRESERVAR"] = Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString());
                                dtNecessidade.Rows[i]["QTDESTOQUE"] = Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"]);
                                dtNecessidade.Rows[i]["QTDBAIXAR"] = Convert.ToDecimal(0);
                                dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"] = Convert.ToDecimal(0);
                                dtNecessidade.Rows[i]["QTDSALDO"] = Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString()) - Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString()) - Convert.ToDecimal(0) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"]);
                                htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()] = NovoEstoque;
                            }
                        }
                        else
                        {
                            throw new Exception("Erro ao carregar estoque");
                        }
                    }
                }
                else if (Param_UtilizaReserva == "2") // Utiliza Reserva = Não
                {
                    for (int i = 0; i <= dtNecessidade.Rows.Count - 1; i++)
                    {
                        if (htFichaEstoque.Contains(dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()))
                        {
                            Decimal NovoEstoque = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]) - Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString());
                            if (NovoEstoque < 0)
                            {
                                dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"] = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]);
                                dtNecessidade.Rows[i]["QTDRESERVAR"] = Convert.ToDecimal(0);
                                dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"] = Convert.ToDecimal(0);
                                dtNecessidade.Rows[i]["QTDBAIXAR"] = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]);
                                dtNecessidade.Rows[i]["QTDESTOQUE"] = Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"]);
                                if (Param_GeraNecessidade == "1")
                                {
                                    dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"] = Math.Abs(NovoEstoque);
                                    dtNecessidade.Rows[i]["QTDSALDO"] = Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString()) - Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]) - Convert.ToDecimal(0) - Math.Abs(NovoEstoque);
                                }
                                else
                                {
                                    dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"] = Convert.ToDecimal(0);
                                    dtNecessidade.Rows[i]["QTDSALDO"] = (Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]) + Convert.ToDecimal(0) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"])) - Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString());
                                }

                                htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()] = 0;
                                dtNecessidade.Rows[i]["STATUS"] = VerificaStatusPlanejamentoMP(dtNecessidade.Rows[i]);
                            }
                            else
                            {
                                dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"] = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]);
                                dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"] = Convert.ToDecimal(0);
                                dtNecessidade.Rows[i]["QTDBAIXAR"] = Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString());
                                dtNecessidade.Rows[i]["QTDESTOQUE"] = Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"]);
                                dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"] = Convert.ToDecimal(0);
                                dtNecessidade.Rows[i]["QTDSALDO"] = Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString()) - Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString()) - Convert.ToDecimal(0) - Convert.ToDecimal(0);
                                htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()] = NovoEstoque;
                                dtNecessidade.Rows[i]["STATUS"] = VerificaStatusPlanejamentoMP(dtNecessidade.Rows[i]);
                            }
                        }
                        else
                        {
                            throw new Exception("Erro ao carregar estoque");
                        }
                    }
                }
                else
                {
                    throw new Exception("Parâmetro Inválido: UTILIZARESERVA");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FrmOrdemProducaoNecessidadeMP_Load(object sender, EventArgs e)
        {

        }

        //private void carregaImagemClassificacao()
        //{
        //    DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl1.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
        //    DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();

        //    DataTable dtImagem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IMAGEM, DESCRICAO, CODSTATUS FROM GSTATUS WHERE TABELA = ?", new object[] { tabela });

        //    for (int i = 0; i < dtImagem.Rows.Count; i++)
        //    {
        //        byte[] imagemEmBytes = (byte[])dtImagem.Rows[i]["IMAGEM"];
        //        System.IO.MemoryStream ms = new System.IO.MemoryStream(imagemEmBytes);
        //        images.AddImage(Image.FromStream(ms));
        //        imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[i]["DESCRICAO"].ToString(), Convert.ToInt32(dtImagem.Rows[i]["CODSTATUS"]), i));
        //    }

        //    imageCombo.SmallImages = images;
        //    imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
        //    gridView1.Columns["VCLIFOR.CODCLASSIFICACAO"].ColumnEdit = imageCombo;
        //}

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            ValidaSaldoGrid();
        }

        private void CalculaReservaPadrao(int i)
        {
            try
            {
                Decimal NovoEstoque = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"].ToString());
                if (NovoEstoque < 0)
                {
                    dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"] = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]);
                    dtNecessidade.Rows[i]["QTDESTOQUE"] = NovoEstoque;
                    dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"] = Convert.ToDecimal(0);
                    dtNecessidade.Rows[i]["QTDSALDO"] = (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"].ToString()) + Convert.ToDecimal(0) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"])) - Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString());
                    htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()] = 0;
                }
                else
                {
                    dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"] = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]);
                    dtNecessidade.Rows[i]["QTDESTOQUE"] = NovoEstoque;
                    dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"] = Convert.ToDecimal(0);
                    dtNecessidade.Rows[i]["QTDSALDO"] = (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"].ToString()) + Convert.ToDecimal(0) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"])) - Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString());
                    htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()] = NovoEstoque;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CalculaBaixaPadrao(int i)
        {
            try
            {
                Decimal NovoEstoque = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"].ToString());
                if (NovoEstoque < 0)
                {
                    dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"] = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]);
                    dtNecessidade.Rows[i]["QTDESTOQUE"] = NovoEstoque;
                    dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"] = Convert.ToDecimal(0);
                    dtNecessidade.Rows[i]["QTDSALDO"] = (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"].ToString()) + Convert.ToDecimal(0) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"])) - Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString());
                    htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()] = 0;
                }
                else
                {
                    dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"] = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]);
                    dtNecessidade.Rows[i]["QTDESTOQUE"] = NovoEstoque;
                    dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"] = Convert.ToDecimal(0);
                    dtNecessidade.Rows[i]["QTDSALDO"] = (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"].ToString()) + Convert.ToDecimal(0) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"])) - Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString());
                    htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()] = NovoEstoque;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private StatusPlanejamentoMP VerificaStatusPlanejamentoMP(DataRow linha)
        {
            try
            {
                if (Param_UtilizaReserva == "1")
                {
                    if ((Convert.ToDecimal(linha["QTDRESERVAR"]) <= Convert.ToDecimal(linha["PORDEMCONSUMO.QTDCOMPONENTE"])) && (Convert.ToDecimal(linha["QTDSOLICITACAOCOMPRAS"]) == 0) && (Convert.ToDecimal(linha["QTDCOMPONENTESUBSTITUTO"]) == 0)) //StatusPlanejamentoMP.ComponenteComsumido
                    {
                        return StatusPlanejamentoMP.ComponenteComsumido;
                    }
                    else if ((Convert.ToDecimal(linha["PORDEMCONSUMO.QTDCOMPONENTE"]) > Convert.ToDecimal(linha["QTDESTOQUE"])) && (Convert.ToDecimal(linha["QTDSALDO"]) >= 0)) //StatusPlanejamentoMP.EstoqueInsuficiente
                    {
                        return StatusPlanejamentoMP.EstoqueInsuficiente;
                    }
                    else if ((Convert.ToDecimal(linha["QTDESTOQUE"]) > Convert.ToDecimal(linha["PORDEMCONSUMO.QTDCOMPONENTE"])) && (Convert.ToDecimal(linha["QTDSALDO"]) >= 0)) //StatusPlanejamentoMP.EstoqueSuficiente
                    {
                        return StatusPlanejamentoMP.EstoqueSuficiente;
                    }
                    else if ((Convert.ToDecimal(linha["QTDRESERVAR"]) != Convert.ToDecimal(linha["PORDEMCONSUMO.QTDCOMPONENTE"])) && (Convert.ToDecimal(linha["QTDSALDO"]) >= 0)) //StatusPlanejamentoMP.PlanejamentoRealizado
                    {
                        return StatusPlanejamentoMP.PlanejamentoRealizado;
                    }
                    else
                    {
                        return StatusPlanejamentoMP.Erro;
                        //throw new Exception("Erro ao atualizar status");
                    }
                }
                else if (Param_UtilizaReserva == "2")
                {
                    if ((Convert.ToDecimal(linha["QTDBAIXAR"]) <= Convert.ToDecimal(linha["PORDEMCONSUMO.QTDCOMPONENTE"])) && (Convert.ToDecimal(linha["QTDSOLICITACAOCOMPRAS"]) == 0) && (Convert.ToDecimal(linha["QTDCOMPONENTESUBSTITUTO"]) == 0)) //StatusPlanejamentoMP.ComponenteComsumido
                    {
                        return StatusPlanejamentoMP.ComponenteComsumido;
                    }
                    else if ((Convert.ToDecimal(linha["PORDEMCONSUMO.QTDCOMPONENTE"]) > Convert.ToDecimal(linha["QTDESTOQUE"])) && (Convert.ToDecimal(linha["QTDSALDO"]) >= 0)) //StatusPlanejamentoMP.EstoqueInsuficiente
                    {
                        return StatusPlanejamentoMP.EstoqueInsuficiente;
                    }
                    else if ((Convert.ToDecimal(linha["QTDESTOQUE"]) > Convert.ToDecimal(linha["PORDEMCONSUMO.QTDCOMPONENTE"])) && (Convert.ToDecimal(linha["QTDSALDO"]) >= 0)) //StatusPlanejamentoMP.EstoqueSuficiente
                    {
                        return StatusPlanejamentoMP.EstoqueSuficiente;
                    }
                    else if ((Convert.ToDecimal(linha["QTDBAIXAR"]) != Convert.ToDecimal(linha["PORDEMCONSUMO.QTDCOMPONENTE"])) && (Convert.ToDecimal(linha["QTDSALDO"]) >= 0)) //StatusPlanejamentoMP.PlanejamentoRealizado
                    {
                        return StatusPlanejamentoMP.PlanejamentoRealizado;
                    }
                    else
                    {
                        return StatusPlanejamentoMP.Erro;
                        //throw new Exception("Erro ao atualizar status");
                    }
                }
                else
                {
                    throw new Exception("Parâmetro Inválido: UTILIZARESERVA");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para recálculo do saldo do planejamento:
        ///     StatusPlanejamentoMP
        ///         ComponenteComsumido = 1, //QTDRESERVAR OU QTDBAIXAR = 100%
        ///         EstoqueInsuficiente = 2, //PORDEMCONSUMO.QTDCOMPONENTE > QTDESTOQUE and QTDSALDO > 0
        ///         EstoqueSuficiente = 3, //QTDESTOQUE > PORDEMCONSUMO.QTDCOMPONENTE and QTDSALDO > 0
        ///         PlanejamentoRealizado = 4 //(QTDRESERVAR OU QTDBAIXAR) <> PORDEMCONSUMO.QTDCOMPONENTE and QTDSALDO > 0
        /// </summary>
        /// <param name="paramAlteraReserva"></param>
        private void ValidaSaldoGrid(AlteraReservaOP paramAlteraReserva = AlteraReservaOP.Estatico)
        {
            try
            {
                int index = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);

                //Carrega o estoque para cada produto e possiveis substitutos de cada produto do planejamento
                htFichaEstoque = CarregahtFichaEstoque(dtFichaEstoque);

                if (Param_UtilizaReserva == "1") //Utiliza Reserva = SIM
                {
                    for (int i = 0; i <= dtNecessidade.Rows.Count - 1; i++)
                    {
                        //Verifica se o componente é um componente substituto
                        if (!string.IsNullOrEmpty(dtNecessidade.Rows[i]["COMPONENTESUBSTITUTO"].ToString()))
                        {
                            //Se for um componente substituto

                            dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"] = dtNecessidade.Select("PORDEMCONSUMO.CODCOMPONENTE = '" + dtNecessidade.Rows[i]["COMPONENTESUBSTITUTO"].ToString() + "' AND PORDEM.SEQOP = '" + dtNecessidade.Rows[i]["PORDEM.SEQOP"].ToString() + "' AND NIVEL = '" + dtNecessidade.Rows[i]["NIVEL"].ToString() + "'")[0]["QTDCOMPONENTESUBSTITUTO"].ToString();

                            DataRow[] rowSubstitutos = dtNecessidade.Select("COMPONENTESUBSTITUTO = '" + dtNecessidade.Rows[i]["COMPONENTESUBSTITUTO"].ToString() + "' AND PORDEM.SEQOP = '" + dtNecessidade.Rows[i]["PORDEM.SEQOP"].ToString() + "' AND NIVEL = '" + dtNecessidade.Rows[i]["NIVEL"].ToString() + "'");
                            DataTable dtvSubs = dtNecessidade.Clone();
                            rowSubstitutos.CopyToDataTable<DataRow>(dtvSubs, LoadOption.Upsert);

                            dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"] = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]);
                            dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"] = Convert.ToDecimal(dtvSubs.Compute("SUM(QTDRESERVAR)", "").ToString()) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"].ToString());
                            Decimal NovoEstoque = (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"]));
                            if (NovoEstoque < 0)
                            {
                                //dtNecessidade.Rows[i]["QTDESTOQUE"] = Convert.ToDecimal(0);
                                dtNecessidade.Rows[i]["QTDESTOQUE"] = NovoEstoque;
                                dtNecessidade.Rows[i]["QTDSALDO"] = (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"].ToString()) + Convert.ToDecimal(0) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"]) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"])) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]);
                                htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()] = Convert.ToDecimal(0);
                            }
                            else
                            {
                                dtNecessidade.Rows[i]["QTDESTOQUE"] = NovoEstoque; //Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"]);
                                dtNecessidade.Rows[i]["QTDSALDO"] = (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"].ToString()) + Convert.ToDecimal(0) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"]) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"])) - Convert.ToDecimal(dtvSubs.Compute("SUM(QTDRESERVAR)", "").ToString());
                                htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()] = Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"]);
                            }
                        }
                        else
                        {
                            //se não for um componente substituto

                            DataRow[] rowSubstitutos = dtNecessidade.Select("COMPONENTESUBSTITUTO = '" + dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString() + "' AND PORDEM.SEQOP = '" + dtNecessidade.Rows[i]["PORDEM.SEQOP"].ToString() + "' AND NIVEL = '" + dtNecessidade.Rows[i]["NIVEL"].ToString() + "'");

                            DataTable dtvSubs = dtNecessidade.Clone();

                            if (rowSubstitutos.Count() > 0)
                            {
                                rowSubstitutos.CopyToDataTable<DataRow>(dtvSubs, LoadOption.Upsert);
                                Decimal NovoEstoque = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]) - Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString());
                                if (NovoEstoque <= 0)
                                {
                                    dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"] = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]);
                                    dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"] = Convert.ToDecimal(dtvSubs.AsEnumerable().Sum(r => r.Field<decimal>("QTDRESERVAR") * r.Field<decimal>("FATOR"))); //Convert.ToDecimal(dtvSubs.Compute("SUM(QTDRESERVAR)", ""));
                                    if (paramAlteraReserva == AlteraReservaOP.Automatico)
                                    {
                                        if (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"].ToString()) > 0)
                                        {
                                            dtNecessidade.Rows[i]["QTDRESERVAR"] = Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"].ToString()) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"]);
                                            dtNecessidade.Rows[i]["QTDESTOQUE"] = Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"]);
                                        }
                                        else
                                        {
                                            if (Param_GeraNecessidade == "1")
                                            {
                                                dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"] = Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"]) - Convert.ToDecimal(0) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"]);
                                            }
                                        }
                                    }

                                    dtNecessidade.Rows[i]["QTDESTOQUE"] = Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"]);
                                    dtNecessidade.Rows[i]["QTDSALDO"] = (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"].ToString()) + Convert.ToDecimal(0) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"]) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"])) - Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString());
                                    htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()] = Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"].ToString());
                                }
                                else
                                {
                                    dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"] = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]);
                                    dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"] = Convert.ToDecimal(dtvSubs.AsEnumerable().Sum(r => r.Field<decimal>("QTDRESERVAR") * r.Field<decimal>("FATOR")));//Convert.ToDecimal(dtvSubs.Compute("SUM(QTDRESERVAR)", ""));
                                    if (paramAlteraReserva == AlteraReservaOP.Automatico)
                                    {
                                        dtNecessidade.Rows[i]["QTDRESERVAR"] = Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString()) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"]);//Convert.ToDecimal(dtvSubs.AsEnumerable().Sum(r => r.Field<decimal>("QTDRESERVAR") * r.Field<decimal>("FATOR"))); 
                                    }
                                    dtNecessidade.Rows[i]["QTDESTOQUE"] = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"].ToString());
                                    dtNecessidade.Rows[i]["QTDSALDO"] = (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"].ToString()) + Convert.ToDecimal(0) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"]) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"])) - Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString());
                                    htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()] = NovoEstoque + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"]);
                                }
                            }
                            else
                            {
                                CalculaReservaPadrao(i);
                            }
                        }
                        dtNecessidade.Rows[i]["STATUS"] = VerificaStatusPlanejamentoMP(dtNecessidade.Rows[i]);
                    }
                }
                else if (Param_UtilizaReserva == "2")
                {
                    for (int i = 0; i <= dtNecessidade.Rows.Count - 1; i++)
                    {
                        if (!string.IsNullOrEmpty(dtNecessidade.Rows[i]["COMPONENTESUBSTITUTO"].ToString()))
                        {
                            dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"] = dtNecessidade.Select("PORDEMCONSUMO.CODCOMPONENTE = '" + dtNecessidade.Rows[i]["COMPONENTESUBSTITUTO"].ToString() + "' AND PORDEM.SEQOP = '" + dtNecessidade.Rows[i]["PORDEM.SEQOP"].ToString() + "' AND NIVEL = '" + dtNecessidade.Rows[i]["NIVEL"].ToString() + "'")[0]["QTDCOMPONENTESUBSTITUTO"].ToString();

                            DataRow[] rowSubstitutos = dtNecessidade.Select("COMPONENTESUBSTITUTO = '" + dtNecessidade.Rows[i]["COMPONENTESUBSTITUTO"].ToString() + "' AND PORDEM.SEQOP = '" + dtNecessidade.Rows[i]["PORDEM.SEQOP"].ToString() + "' AND NIVEL = '" + dtNecessidade.Rows[i]["NIVEL"].ToString() + "'");
                            DataTable dtvSubs = dtNecessidade.Clone();
                            rowSubstitutos.CopyToDataTable<DataRow>(dtvSubs, LoadOption.Upsert);

                            Decimal NovoEstoque = (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"]));

                            if (NovoEstoque < 0)
                            {
                                //dtNecessidade.Rows[i]["QTDESTOQUE"] = Convert.ToDecimal(0);
                                dtNecessidade.Rows[i]["QTDESTOQUE"] = NovoEstoque;
                                dtNecessidade.Rows[i]["QTDSALDO"] = (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"].ToString()) + Convert.ToDecimal(0) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"]) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"])) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]);
                                htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()] = Convert.ToDecimal(0);
                            }
                            else
                            {
                                dtNecessidade.Rows[i]["QTDESTOQUE"] = NovoEstoque; //Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"]);
                                dtNecessidade.Rows[i]["QTDSALDO"] = (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"].ToString()) + Convert.ToDecimal(0) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"]) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"])) - Convert.ToDecimal(dtvSubs.Compute("SUM(QTDBAIXAR)", "").ToString());
                                htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()] = Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"]);
                            }

                            ///
                            //dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"] = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]);
                            //dtNecessidade.Rows[i]["QTDESTOQUE"] = Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"]);
                            //dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"] = Convert.ToDecimal(dtvSubs.Compute("SUM(QTDBAIXAR)", "").ToString()) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"].ToString());
                            //dtNecessidade.Rows[i]["QTDSALDO"] = (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"].ToString()) + Convert.ToDecimal(0) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"]) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"])) - Convert.ToDecimal(dtvSubs.Compute("SUM(QTDBAIXAR)", "").ToString());
                            //htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()] =  Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"]);
                        }
                        else
                        {
                            DataRow[] rowSubstitutos = dtNecessidade.Select("COMPONENTESUBSTITUTO = '" + dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString() + "' AND PORDEM.SEQOP = '" + dtNecessidade.Rows[i]["PORDEM.SEQOP"].ToString() + "' AND NIVEL = '" + dtNecessidade.Rows[i]["NIVEL"].ToString() + "'");

                            DataTable dtvSubs = dtNecessidade.Clone();

                            if (rowSubstitutos.Count() > 0)
                            {
                                rowSubstitutos.CopyToDataTable<DataRow>(dtvSubs, LoadOption.Upsert);
                                Decimal NovoEstoque = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]) - Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString());
                                if (NovoEstoque <= 0)
                                {
                                    dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"] = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]);
                                    dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"] = Convert.ToDecimal(dtvSubs.AsEnumerable().Sum(r => r.Field<decimal>("QTDBAIXAR") * r.Field<decimal>("FATOR")));  //Convert.ToDecimal(dtvSubs.Compute("SUM(QTDBAIXAR)", ""));
                                    if (paramAlteraReserva == AlteraReservaOP.Automatico)
                                    {
                                        if (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"].ToString()) > 0)
                                        {
                                            dtNecessidade.Rows[i]["QTDBAIXAR"] = Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"].ToString()) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"]);
                                            dtNecessidade.Rows[i]["QTDESTOQUE"] = Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"]);
                                        }
                                        else
                                        {
                                            if (Param_GeraNecessidade == "1")
                                            {
                                                dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"] = Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"]) - Convert.ToDecimal(0) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"]);
                                            }
                                        }
                                    }

                                    dtNecessidade.Rows[i]["QTDSALDO"] = (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"].ToString()) + Convert.ToDecimal(0) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"]) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"])) - Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString());
                                    htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()] = Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"].ToString());
                                }
                                else
                                {
                                    dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"] = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]);
                                    dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"] = Convert.ToDecimal(dtvSubs.AsEnumerable().Sum(r => r.Field<decimal>("QTDBAIXAR") * r.Field<decimal>("FATOR"))); //Convert.ToDecimal(dtvSubs.Compute("SUM(QTDBAIXAR)", ""));
                                    if (paramAlteraReserva == AlteraReservaOP.Automatico)
                                    {
                                        dtNecessidade.Rows[i]["QTDBAIXAR"] = Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString()) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"]);
                                    }
                                    dtNecessidade.Rows[i]["QTDESTOQUE"] = Convert.ToDecimal(htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()]) - Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"].ToString());
                                    dtNecessidade.Rows[i]["QTDSALDO"] = (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"].ToString()) + Convert.ToDecimal(0) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"]) + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"])) - Convert.ToDecimal(dtNecessidade.Rows[i]["PORDEMCONSUMO.QTDCOMPONENTE"].ToString());
                                    htFichaEstoque[dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString()] = NovoEstoque + Convert.ToDecimal(dtNecessidade.Rows[i]["QTDCOMPONENTESUBSTITUTO"]);
                                }
                            }
                            else
                            {
                                CalculaBaixaPadrao(i);
                            }
                        }
                        dtNecessidade.Rows[i]["STATUS"] = VerificaStatusPlanejamentoMP(dtNecessidade.Rows[i]);
                    }
                }
                else
                {
                    throw new Exception("Parâmetro Inválido: UTILIZARESERVA");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para verificação de inconsistências no planejamento
        /// </summary>
        /// <returns>True/False</returns>
        private bool existeInconsistencias(string CODLOCAL)
        {
            try
            {
                bool _valida = false;

                String CONTROLASALDOESTQUE = AppLib.Context.poolConnection.Get("Start").ExecGetField("B", "SELECT CONTROLASALDOESTQUE FROM VPARAMETROS WHERE CODEMPRESA = ?", new Object[] { AppLib.Context.Empresa }).ToString();
                bool SALDOESTQUE = false;
                bool COMPONENTES_NAO_PLANEJADOS = false;

                for (int i = 0; i <= dtNecessidade.Rows.Count - 1; i++)
                {
                    if (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDESTOQUE"]) < 0)
                    {
                        if (CONTROLASALDOESTQUE.ToUpper().Equals("A"))
                        {
                            if (SALDOESTQUE == false)
                            {
                                SALDOESTQUE = true;
                                if (MessageBox.Show("Existem componentes sem saldo no estoque '" + CODLOCAL + "', deseja continuar?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    _valida = true;
                                    throw new Exception("Processo Cancelado!");
                                }
                            }
                        }
                        else if (CONTROLASALDOESTQUE.ToUpper().Equals("B"))
                        {
                            SALDOESTQUE = true;
                            throw new Exception("Processo Cancelado, existem componentes sem saldo no estoque '" + CODLOCAL + "'.");
                        }
                        else
                        {
                            throw new Exception("Parâmetro Inválido: CONTROLASALDOESTQUE");
                        }
                    }

                    if (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSALDO"]) != 0)
                    {
                        if (COMPONENTES_NAO_PLANEJADOS == false)
                        {
                            COMPONENTES_NAO_PLANEJADOS = true;
                            if (MessageBox.Show("Existem componentes que não foram planejados, deseja continuar?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                _valida = true;
                                throw new Exception("Processo Cancelado!");
                            }
                        }
                    }
                }

                return _valida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Lista dos tipos de consumo
        enum TipoConsumo
        {
            Necessidade,
            Baixa,
            Reserva
        }

        //Lista dos tipos de operação
        enum TipoGOPER
        {
            Reserva,
            Baixa,
            Requisicao
            //Transferencia,
            //EntradaRefugo,
            //EntradaProducao,
            //Terceiro,
            //CompraAprovada,
            //DevolucaoMP
        }

        /// <summary>
        /// Método para inclusão de registros do planejamento da ordem de produção na tabela PORDEMCOMPRA
        /// </summary>
        /// <param name="conn">Variável referente a conexão</param>
        /// <param name="row">Linha referente ao produto que será inserido na tabela PORDEMCOMPRA</param>
        /// <param name="CODOPER">Código da Operação já gravada na tabela GOPER/GOPERITEM</param>
        /// <param name="NSEQITEM">Numero de sequencia já gravada na tabela GOPERITEM</param>
        private bool IncluirPORDEMCOMPRA(AppLib.Data.Connection conn, DataRow row, int CODOPER, int NSEQITEM)
        {
            try
            {
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PORDEMCOMPRA");

                v.Set("CODEMPRESA", AppLib.Context.Empresa);
                v.Set("CODFILIAL", AppLib.Context.Filial);
                v.Set("CODESTRUTURA", row["PORDEM.CODESTRUTURA"]);
                v.Set("REVESTRUTURA", row["PORDEM.REVESTRUTURA"]);
                v.Set("CODIGOOP", row["PORDEM.CODIGOOP"]);
                v.Set("SEQOP", row["PORDEM.SEQOP"]);
                v.Set("SEQOPERACAO", Convert.ToInt16(row["PORDEMROTEIRO.SEQOPERACAO"]).ToString("000"));
                v.Set("CODOPERACAO", row["PORDEMROTEIRO.CODOPERACAO"]);
                v.Set("TIPOCOMPRA", 1);
                v.Set("CODCOMPONENTE", row["PORDEMCONSUMO.CODCOMPONENTE"].ToString());
                v.Set("CODOPER", CODOPER);
                v.Set("NSEQITEM", NSEQITEM);

                if (Param_UtilizaReserva == "1")
                {
                    v.Set("QTDCOMPONENTE", Convert.ToDecimal(row["QTDRESERVAR"].ToString()));
                }
                else if (Param_UtilizaReserva == "2")
                {
                    v.Set("QTDCOMPONENTE", Convert.ToDecimal(row["QTDBAIXAR"].ToString()));
                }
                else
                {
                    throw new Exception("Parâmetro Inválido: UTILIZARESERVA");
                }

                v.Set("DATASOLICITACAO", conn.GetDateTime());

                v.Insert();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao incluir operação: PORDEMCOMPRA");
            }
        }

        /// <summary>
        /// Método para inclusão de registros do planejamento da ordem de produção na tabela PORDEMCONSUMO
        /// </summary>
        /// <param name="conn">Variável referente a conexão</param>
        /// <param name="row">Linha referente ao produto que será inserido na tabela PORDEMCONSUMO</param>
        /// <param name="tipoconsumo">Tipo da operação a ser inserida na tabela PORDEMCONSUMO</param>
        /// <param name="CODOPER">Código da Operação já gravada na tabela GOPER/GOPERITEM</param>
        /// <param name="NSEQITEM">Numero de sequencia já gravada na tabela GOPERITEM</param>
        private void IncluirPORDEMCONSUMO(AppLib.Data.Connection conn, DataRow row, TipoConsumo tipoconsumo, int CODOPER, int NSEQITEM)
        {
            try
            {
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PORDEMCONSUMO");

                v.Set("CODEMPRESA", AppLib.Context.Empresa);
                v.Set("CODFILIAL", AppLib.Context.Filial);
                v.Set("CODESTRUTURA", row["PORDEM.CODESTRUTURA"]);
                v.Set("REVESTRUTURA", row["PORDEM.REVESTRUTURA"]);
                v.Set("CODIGOOP", row["PORDEM.CODIGOOP"]);
                v.Set("SEQOP", row["PORDEM.SEQOP"]);
                v.Set("SEQOPERACAO", Convert.ToInt16(row["PORDEMROTEIRO.SEQOPERACAO"]).ToString("000"));
                v.Set("CODOPERACAO", row["PORDEMROTEIRO.CODOPERACAO"]);
                v.Set("SEQAPO", null);

                switch (tipoconsumo)
                {
                    case TipoConsumo.Baixa:
                        v.Set("TIPOCONSUMO", "B");
                        break;

                    case TipoConsumo.Reserva:
                        v.Set("TIPOCONSUMO", "R");
                        break;

                    default:
                        throw new Exception("Erro: Parâmetro incorreto: Tipo Consumo");
                        break;
                }

                v.Set("CODOPER", CODOPER);
                v.Set("NSEQITEM", NSEQITEM);
                v.Set("DATAMOVIMENTO", conn.GetDateTime());

                //Verificando se o componente a ser gravado é um componente substituto
                if (!string.IsNullOrEmpty(row["COMPONENTESUBSTITUTO"].ToString()))
                {
                    v.Set("COMPONENTEORIGINAL", 2);
                }
                else
                {
                    v.Set("COMPONENTEORIGINAL", 1);
                }

                v.Set("COMPONENTECANCELADO", 2);

                if (Param_UtilizaReserva == "1")
                {
                    v.Set("QTDCOMPONENTE", Convert.ToDecimal(row["QTDRESERVAR"].ToString()));
                }
                else if (Param_UtilizaReserva == "2")
                {
                    v.Set("QTDCOMPONENTE", Convert.ToDecimal(row["QTDBAIXAR"].ToString()));
                }
                else
                {
                    throw new Exception("Parâmetro Inválido: UTILIZARESERVA");
                }

                v.Set("CODCOMPONENTE", row["PORDEMCONSUMO.CODCOMPONENTE"].ToString());
                v.Set("UNDCOMPONENTE", row["VPRODUTO.CODUNIDCONTROLE"].ToString());

                v.Insert();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao incluir operação: PORDEMCONSUMO");
            }
        }

        private bool IncluirGOPERITEM(int CODOPER, AppLib.Data.Connection conn, DataRow row)
        {
            GoperItem _goperitem = new GoperItem();

            _goperitem.CODTIPOPER = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODTIPOPER FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, CODOPER }).ToString();

            _goperitem.CODEMPRESA = AppLib.Context.Empresa;
            _goperitem.CODPRODUTO = row["PORDEMCONSUMO.CODCOMPONENTE"].ToString();
            _goperitem.DATAENTREGA = conn.GetDateTime();//Convert.ToDateTime(row["PORDEM.DATAINIPLANOP"].ToString()); //select DATAINIPLANOP from pordem where CODEMPRESA = 1 and CODFILIAL = 2 and CODESTRUTURA = '01.01.2093' and REVESTRUTURA = 1 and CODIGOOP = '000001/17' and SEQOP = '001'
            _goperitem.CODUNIDOPER = row["VPRODUTO.CODUNIDCONTROLE"].ToString();
            _goperitem.APLICACAOMATERIAL = "C"; //CONSUMO
            _goperitem.TIPODESCONTO = "U"; // Tipo de Desconto: Valor Unitário.

            if (Param_UtilizaReserva == "1")
            {
                _goperitem.QUANTIDADE = Convert.ToDecimal(row["QTDRESERVAR"].ToString());
            }
            else if (Param_UtilizaReserva == "2")
            {
                _goperitem.QUANTIDADE = Convert.ToDecimal(row["QTDBAIXAR"].ToString());
            }
            else
            {
                throw new Exception("Parâmetro Inválido: UTILIZARESERVA");
            }

            if (_goperitem.QUANTIDADE > 0)
            {
                return _goperitem.setItem(_goperitem, conn, CODOPER);
            }
            else if (_goperitem.QUANTIDADE == 0)
            {
                return true;
            }
            else
            {
                throw new Exception("´Quantidade inválida, valor negativo");
            }
        }

        /// <summary>
        /// Método para Incluir registro na tabela GOPER e GOPERITEM
        /// </summary>
        /// <param name="conn">Conexão Ativa</param>
        /// <param name="tipogoper">tipo da operação permitida na OP</param>
        /// <returns>CODOPER</returns>
        private int IncluirGOPER(AppLib.Data.Connection conn, TipoGOPER tipogoper)
        {
            try
            {
                Goper _goper = new Goper();
                GoperItem _goperitem = new GoperItem();

                _goper.CODEMPRESA = AppLib.Context.Empresa;
                _goper.CODFILIAL = AppLib.Context.Filial;
                _goper.CODUSUARIOCRIACAO = AppLib.Context.Usuario;
                _goper.DATACRIACAO = conn.GetDateTime();
                _goper.CODCLIFOR = conn.ExecGetField("", "select CODCLIFOR from VCLIFOR where  CGCCPF in (select CGCCPF from gfilial where codfilial = ?)", new object[] { AppLib.Context.Filial }).ToString();
                _goper.CODOPERADOR = conn.ExecGetField("", "select CODOPERADOR from VOPERADOR where CODUSUARIO = ?", new object[] { AppLib.Context.Usuario }).ToString();

                switch (tipogoper)
                {
                    case TipoGOPER.Reserva:
                        _goper.CODTIPOPER = VerificaParametro("TIPOPERRESERVA");
                        _goper.CODLOCAL = VerificaParametro("LOCALESTOQUEMP");
                        _goper.CODLOCALENTREGA = VerificaParametro("LOCALESTOQUERESERVA");
                        break;
                    case TipoGOPER.Baixa:
                        _goper.CODTIPOPER = VerificaParametro("TIPOPERBAIXA");
                         _goper.CODLOCAL = VerificaParametro("LOCALESTOQUEMP");
                        break;
                    case TipoGOPER.Requisicao:
                        _goper.CODTIPOPER = VerificaParametro("TIPOPERREQUISICAO");
                        _goper.USUARIOALTERACAO = AppLib.Context.Usuario;
                        _goper.CODSTATUS = "0";//ABERTO
                        break;
                    default:
                        throw new Exception("Erro ao incluir operação.");
                }

                return _goper.setGoper(_goper, conn);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao incluir operação.");
            }
        }

        /// <summary>
        /// Método para inclusão de requisição de compra
        /// </summary>
        /// <param name="conn">Conexão</param>
        /// <param name="indexDTNecessidade">index referente ao item do Datatable do planejamento</param>
        /// <param name="CODOPER_Compra">CODOPER referente a operação de compra</param>
        /// <param name="NSEQITEM_Compra">Número de sequência para inclusão do item</param>
        /// <returns></returns>
        private bool IncluiSolicitacaoCompra(AppLib.Data.Connection conn, int indexDTNecessidade, ref int CODOPER_Compra, ref int NSEQITEM_Compra)
        {
            try
            {
                int i = indexDTNecessidade;

                if (CODOPER_Compra == 0) //Verifica se ja foi criado o CODOPER para solicitações de compras
                {
                    //Se não foi criado, Inclui na tabela GOPER e grava o CODOPER na variavel CODOPER_Compra
                    CODOPER_Compra = IncluirGOPER(conn, TipoGOPER.Requisicao);
                    if (CODOPER_Compra > 0)
                    {
                        //Tentativa de gravar o item na GOPERITEM, retornando true/false
                        if (IncluirGOPERITEM(CODOPER_Compra, conn, dtNecessidade.Rows[i]) == true)
                        {
                            //se o item foi gravado na GOPERITEM, a solicitação também é gravada na tabela ORDEMCOMPRA
                            if (IncluirPORDEMCOMPRA(conn, dtNecessidade.Rows[i], CODOPER_Compra, NSEQITEM_Compra) == true)
                            {
                                NSEQITEM_Compra = NSEQITEM_Compra + 1;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    //Tentativa de gravar o item na GOPERITEM, retornando true/false
                    if (IncluirGOPERITEM(CODOPER_Compra, conn, dtNecessidade.Rows[i]) == true)
                    {
                        //se o item foi gravado na GOPERITEM, a solicitação também é gravada na tabela ORDEMCOMPRA
                        if (IncluirPORDEMCOMPRA(conn, dtNecessidade.Rows[i], CODOPER_Compra, NSEQITEM_Compra) == true)
                        {
                            NSEQITEM_Compra = NSEQITEM_Compra + 1;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();

            // João Pedro Luchiari - 18/01/2018 - Descomentar caso dê erro
            //conn.BeginTransaction();

            try
            {
                //Carregando Parâmetros
                string Param_UtilizaReserva = VerificaParametro("UTILIZARESERVA");
                string Param_MomentoBaixa = VerificaParametro("MOMENTOBAIXA");
                string Param_GeraNecessidade = VerificaParametro("GERARNECESSIDADEAUTO");
                string Param_LocalEstoqueMP = VerificaParametro("LOCALESTOQUEMP");

                //Carregando Parâmetros: Tipo Operação
                string Param_TipoperBaixa = VerificaParametro("TIPOPERBAIXA");
                string Param_TipoperReserva = VerificaParametro("TIPOPERRESERVA");
                string Param_TipoperRequisicao = VerificaParametro("TIPOPERREQUISICAO");

                //Método para verificar as inconsistências
                if (existeInconsistencias(Param_LocalEstoqueMP) == true)
                {
                    throw new Exception("Processo cancelado, verifique as inconsistências!");
                }

                if (Param_UtilizaReserva == "1") //Utiliza Reserva = Sim
                {
                    int CODOPER = IncluirGOPER(conn, TipoGOPER.Reserva);
                    int NSEQITEM = 1;
                    int CODOPER_Compra = 0;
                    int NSEQITEM_Compra = 1;

                    if (CODOPER <= 0)
                    {
                        throw new Exception("Erro ao incluir operação");
                    }

                    //for para correr todos os produtos do planejamento
                    for (int i = 0; i <= dtNecessidade.Rows.Count - 1; i++)
                    {
                        //Método para inclusão do registro de RESERVA na tabela GOPER e GOPERITEM
                        if (IncluirGOPERITEM(CODOPER, conn, dtNecessidade.Rows[i]) == true)
                        {
                            if (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"]) > 0)
                            {
                                PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                                PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque Estoque;
                                Estoque = PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao;

                                psPartLocalEstoqueSaldoData.MovimentaEstoque2(Estoque, AppLib.Context.Empresa, CODOPER, NSEQITEM);
                            }

                            //Método para inclusão do registro de RESERVA na tabela PORDEMCONSUMO
                            IncluirPORDEMCONSUMO(conn, dtNecessidade.Rows[i], TipoConsumo.Reserva, CODOPER, NSEQITEM);

                            //Verifica se existe solicitação de compra para este item 
                            if (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"]) > 0)
                            {
                                if (IncluiSolicitacaoCompra(conn, i, ref CODOPER_Compra, ref NSEQITEM_Compra) == false)
                                {
                                    throw new Exception("Erro ao incluir operação: COMPRA");
                                }
                            }

                            if (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"].ToString()) > 0)
                            {
                                NSEQITEM = NSEQITEM + 1;
                            }
                            else if (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDRESERVAR"].ToString()) == 0)
                            {

                            }
                            else
                            {
                                throw new Exception("´Quantidade inválida, valor negativo");
                            }
                        }
                        else
                        {
                            throw new Exception("Erro ao incluir operação: RESERVA");
                        }
                    }
                }
                else if (Param_UtilizaReserva == "2") //Utiliza Reserva = Não
                {
                    int CODOPER = IncluirGOPER(conn, TipoGOPER.Baixa);
                    int NSEQITEM = 1;
                    int CODOPER_Compra = 0;
                    int NSEQITEM_Compra = 1;

                    if (CODOPER <= 0)
                    {
                        throw new Exception("Erro ao incluir operação");
                    }

                    for (int i = 0; i <= dtNecessidade.Rows.Count - 1; i++)
                    {
                        if (Param_MomentoBaixa == "1") //PLANEJAR ORDEM DE PRODUÇÃO, BAIXAR MATÉRIA PRIMA
                        {
                            //Método para inclusão do registro de BAIXA na tabela GOPER e GOPERITEM
                            if (IncluirGOPERITEM(CODOPER, conn, dtNecessidade.Rows[i]) == true)
                            {
                                if (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"]) > 0)
                                {
                                    PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                                    PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque Estoque;
                                    Estoque = PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao;

                                    psPartLocalEstoqueSaldoData.MovimentaEstoque2(Estoque, AppLib.Context.Empresa, CODOPER, NSEQITEM);
                                }

                                //Método para inclusão do registro de BAIXA na tabela PORDEMCONSUMO
                                IncluirPORDEMCONSUMO(conn, dtNecessidade.Rows[i], TipoConsumo.Baixa, CODOPER, NSEQITEM);

                                //Verifica se existe solicitação de compra para este item 
                                if (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"]) > 0)
                                {
                                    if (IncluiSolicitacaoCompra(conn, i, ref CODOPER_Compra, ref NSEQITEM_Compra) == false)
                                    {
                                        throw new Exception("Erro ao incluir operação: COMPRA");
                                    }
                                }

                                if (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"].ToString()) > 0)
                                {
                                    NSEQITEM = NSEQITEM + 1;
                                }
                                else if (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDBAIXAR"].ToString()) == 0)
                                {

                                }
                                else
                                {
                                    throw new Exception("´Quantidade inválida, valor negativo");
                                }
                            }
                            else
                            {
                                throw new Exception("Erro ao incluir operação: BAIXA");
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Parâmetro Inválido: UTILIZARESERVA");
                }

                //Selecionando registros para atualização da tabela PORDEMROTEIRO
                var itemOrdemRoteiro = from d in dtNecessidade.Select().AsEnumerable()
                                       group d by new
                                       {
                                           CODEMPRESA = d.Field<int>("PORDEM.CODEMPRESA"),
                                           CODFILIAL = d.Field<int>("PORDEM.CODFILIAL"),
                                           CODESTRUTURA = d.Field<string>("PORDEM.CODESTRUTURA"),
                                           REVESTRUTURA = d.Field<int>("PORDEM.REVESTRUTURA"),
                                           SEQOPERACAO = d.Field<string>("PORDEMROTEIRO.SEQOPERACAO"),
                                           CODOPERACAO = d.Field<string>("PORDEMROTEIRO.CODOPERACAO"),
                                           CODIGOOP = d.Field<string>("PORDEM.CODIGOOP"),
                                           SEQOP = d.Field<string>("PORDEM.SEQOP")
                                       } into grp
                                       select grp;

                for (int y = 0; y <= (itemOrdemRoteiro.Count() - 1); y++)
                {
                    //atualiza PORDEMROTEIRO, atualizar data de planejamento e status da operação para 2 = Planejada
                    conn.ExecTransaction("UPDATE PORDEMROTEIRO SET DATAINIPLAN = GETDATE(), DATAFIMPLAN = GETDATE(), STATUSOPERACAO = 2 WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ? AND SEQOPERACAO = ? AND CODOPERACAO = ?", new object[] { itemOrdemRoteiro.ElementAt(y).Key.CODEMPRESA, itemOrdemRoteiro.ElementAt(y).Key.CODFILIAL, itemOrdemRoteiro.ElementAt(y).Key.CODESTRUTURA, itemOrdemRoteiro.ElementAt(y).Key.REVESTRUTURA, itemOrdemRoteiro.ElementAt(y).Key.CODIGOOP, itemOrdemRoteiro.ElementAt(y).Key.SEQOP, itemOrdemRoteiro.ElementAt(y).Key.SEQOPERACAO, itemOrdemRoteiro.ElementAt(y).Key.CODOPERACAO });
                }

                //Atualiza Status da Ordem de Produção = 2 => PLANEJADA
                conn.ExecTransaction("UPDATE PORDEM SET STATUSOP = 2, DATAINIPLANOP = GETDATE(), DATAFIMPLANOP = GETDATE() WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODIGOOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codOrdem });

                // João Pedro Luchiari - 18/01/2018 - Descomentar caso dê erro.
                //conn.Commit();

                // Executar método de Insert para VfichEstoque               

                //string codTipOper = conn.ExecGetField(string.Empty, "SELECT CODTIPOPER FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, CODOPER }).ToString();

                //string tipoEstoque = conn.ExecGetField("N", "SELECT OPERESTOQUE FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ?", new object[] { codTipOper, AppLib.Context.Empresa }).ToString();
                //if (tipoEstoque != "N")
                //{
                //    PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                //    psPartLocalEstoqueSaldoData._tablename = "VLOCALESTOQUESALDO";
                //    psPartLocalEstoqueSaldoData._keys = new string[] { "CODEMPRESA", "CODFILIAL", "CODLOCAL", "CODPRODUTO" };

                //    PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque Estoque;
                //    Estoque = PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao;

                //    DataTable dtLocal = conn.ExecQuery(@"SELECT CODLOCAL, CODLOCALENTREGA FROM GOPER WHERE CODOPER = ? AND CODEMPRESA =? ", new object[] { CODOPER, AppLib.Context.Empresa });

                //    AppLib.Context.poolConnection.Remove(conn.ToString());
                //    //psPartLocalEstoqueSaldoData.MovimentaEstoque(AppLib.Context.Empresa, AppLib.Context.Filial, dtLocal.Rows[0]["CODLOCAL"].ToString(), dtLocal.Rows[0]["CODLOCALENTREGA"].ToString(), GOPERITEM.Get("CODPRODUTO").ToString(), Convert.ToDecimal(GOPERITEM.Get("QUANTIDADE")), GOPERITEM.Get("CODUNIDOPER").ToString(), PSPartLocalEstoqueSaldoData.Tipo.Diminui);
                //    psPartLocalEstoqueSaldoData.MovimentaEstoque2(Estoque, AppLib.Context.Empresa, CODOPER, NSEQITEM);

                //}

                MessageBox.Show("Ordem de produção planejada com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Dispose();
            }
            catch (Exception ex)
            {
                conn.Rollback();
                MessageBox.Show(ex.Message.ToString(), "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnComponenteSubstituto_Click(object sender, EventArgs e)
        {
            int index = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);

            try
            {
                if (string.IsNullOrEmpty(dtNecessidade.Rows[index]["COMPONENTESUBSTITUTO"].ToString()))
                {
                    string Param_LocalEstoqueMP = VerificaParametro("LOCALESTOQUEMP");

                    if (!string.IsNullOrEmpty(Param_LocalEstoqueMP))
                    {
                        FrmVisaoComponenteSubstituto frm = new FrmVisaoComponenteSubstituto();
                        frm.codProduto = dtNecessidade.Rows[index]["PORDEMCONSUMO.CODCOMPONENTE"].ToString();
                        frm.qtdNecessidade = (Convert.ToDecimal(dtNecessidade.Rows[index]["QTDSALDO"].ToString()) < 0 ? Math.Abs(Convert.ToDecimal(dtNecessidade.Rows[index]["QTDSALDO"].ToString())) : 0);
                        frm.codLocal = Param_LocalEstoqueMP;
                        frm.dtNecessidade = dtNecessidade;
                        frm.linhaReferencia = dtNecessidade.Rows[index];

                        frm.ShowDialog();

                        if (frm.dtNecessidade.Rows.Count > 0)
                        {
                            for (int i = 0; i <= frm.dtNecessidade.Rows.Count - 1; i++)
                            {
                                DataRow[] rows = dtNecessidade.Select("PORDEMCONSUMO.CODCOMPONENTE = '" + frm.dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString() + "' AND PORDEM.SEQOP = '" + frm.dtNecessidade.Rows[i]["PORDEM.SEQOP"].ToString() + "' AND NIVEL = '" + frm.dtNecessidade.Rows[i]["NIVEL"].ToString() + "'");
                                if (rows.Count() > 0)
                                {
                                    int indexRow = dtNecessidade.Rows.IndexOf(dtNecessidade.Select("PORDEMCONSUMO.CODCOMPONENTE = '" + frm.dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString() + "' AND PORDEM.SEQOP = '" + frm.dtNecessidade.Rows[i]["PORDEM.SEQOP"].ToString() + "' AND NIVEL = '" + frm.dtNecessidade.Rows[i]["NIVEL"].ToString() + "'")[0]);
                                    dtNecessidade.Rows[indexRow]["QTDESTOQUE"] = frm.dtNecessidade.Rows[i]["QTDESTOQUE"].ToString();
                                    if (Param_UtilizaReserva == "1")
                                    {
                                        dtNecessidade.Rows[indexRow]["QTDRESERVAR"] = Convert.ToDecimal(dtNecessidade.Rows[indexRow]["QTDRESERVAR"]) + Convert.ToDecimal(frm.dtNecessidade.Rows[i]["QTDRESERVAR"].ToString());
                                    }
                                    else if (Param_UtilizaReserva == "2")
                                    {
                                        dtNecessidade.Rows[indexRow]["QTDBAIXAR"] = Convert.ToDecimal(dtNecessidade.Rows[indexRow]["QTDBAIXAR"]) + Convert.ToDecimal(frm.dtNecessidade.Rows[i]["QTDBAIXAR"].ToString());
                                    }
                                    else
                                    {
                                        throw new Exception("Parâmetro Inválido: UTILIZARESERVA");
                                    }
                                }
                                else
                                {
                                    DataRow[] rows2 = dtNecessidade.Select("PORDEMCONSUMO.CODCOMPONENTE = '" + frm.dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString() + "'");
                                    if (rows2.Count() > 0)
                                    {
                                        if (Param_UtilizaReserva == "1")
                                        {
                                            decimal sumReservar = Convert.ToDecimal(dtNecessidade.Select("PORDEMCONSUMO.CODCOMPONENTE = '" + frm.dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString() + "'").Sum(row => row.Field<decimal>("QTDRESERVAR")));
                                            frm.dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"] = Convert.ToDecimal(frm.dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]) - Convert.ToDecimal(sumReservar);
                                        }
                                        else if (Param_UtilizaReserva == "2")
                                        {
                                            decimal sumBaixar = Convert.ToDecimal(dtNecessidade.Select("PORDEMCONSUMO.CODCOMPONENTE = '" + frm.dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString() + "'").Sum(row => row.Field<decimal>("QTDBAIXAR")));
                                            frm.dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"] = Convert.ToDecimal(frm.dtNecessidade.Rows[i]["QTDESTOQUEINICIAL"]) - Convert.ToDecimal(sumBaixar);
                                        }
                                        else
                                        {
                                            throw new Exception("Parâmetro Inválido: UTILIZARESERVA");
                                        }

                                        dtNecessidade.Rows.Add(frm.dtNecessidade.Rows[i].ItemArray);
                                    }
                                    else
                                    {
                                        dtNecessidade.Rows.Add(frm.dtNecessidade.Rows[i].ItemArray);
                                    }
                                }
                            }
                        }

                        ValidaSaldoGrid(AlteraReservaOP.Automatico);
                    }
                    else
                    {
                        throw new Exception("Parâmetro Inválido: Local Estoque Matéria Prima");
                    }
                }
                else
                {
                    MessageBox.Show("Este produto já é um componente substituto.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int index = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);
            FrmComponenteSubstituto frm2 = new FrmComponenteSubstituto();
            frm2.codProduto = dtNecessidade.Rows[index]["PORDEMCONSUMO.CODCOMPONENTE"].ToString();
            frm2.edita = true;
            frm2.ShowDialog();
        }

        /// <summary>
        /// Evento de Duplo click do grid utilizado para excluir componentes substitutos
        /// </summary>
        private void grid_DoubleClick(object sender, EventArgs e)
        {
            int index = gridView1.GetDataSourceRowIndex(gridView1.FocusedRowHandle);

            try
            {
                //Verifica se o componente a ser excluido é um componente substituto
                if (!string.IsNullOrEmpty(dtNecessidade.Rows[index]["COMPONENTESUBSTITUTO"].ToString()))
                {
                    if (MessageBox.Show("Deseja remover este componente substituto?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        dtNecessidade.Rows.RemoveAt(index);
                        ValidaSaldoGrid();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
