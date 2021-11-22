using ITGProducao.Class;
using ITGProducao.Controles;
using ITGProducao.Visao;
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
    public partial class FrmTerminalApontamento : Form
    {
        int proximoSEQAPO = 1;
        int revisaoEstrutura = 0;
        string CODOPERACAO = "";
        string SEQOPERACAO = "";
        bool existeOP = false;
        bool editaTrabalho = false;
        bool editaRecursos = false;
        bool editaConsumo = false;
        bool editaSubProdutos = false;
        bool editaParadas = false;

        int IDApontamento = 0;
        int IDRecurso = 0;
        int IDConsumo= 0;
        int IDSubProduto = 0;
        int IDParada = 0;

        bool existeBaixa = false;
        bool existeNecessidade = false;
        bool existeReserva = false;
        bool existeCODOPERnull = false;

        DataTable dtTrabalho;
        DataTable dtRecursos;
        DataTable dtParada;
        DataTable dtSubproduto;
        DataTable dtConsumo;

        //Aba Consumo
        public DataTable dtFichaEstoque;
        private Hashtable htFichaEstoque; //Variável para armazenamento do estoque dos produtos
        private List<string> _Listacomponentes;
        private string Param_UtilizaReserva = ""; //Variável para armazenamento do parametro UTILIZARESERVA da tabela PPARAM
        private string Param_MomentoBaixa = "";
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

        public FrmTerminalApontamento()
        {
            InitializeComponent();

            lookupgruporecurso.txtcodigo.Leave += new System.EventHandler(lookupgruporecurso_txtcodigo_Leave);
            lookupgruporecurso.btnprocurar.Click += new System.EventHandler(lookupgruporecurso_txtcodigo_Leave);
            lookupgruporecurso.txtconteudo.Click += new System.EventHandler(lookupgruporecurso_txtcodigo_Leave);

            lookupEstrutura.txtcodigo.Leave += new System.EventHandler(lookupEstrutura_txtcodigo_Leave);
            lookupEstrutura.btnprocurar.Click += new System.EventHandler(lookupEstrutura_txtcodigo_Leave);
            lookupEstrutura.txtconteudo.Click += new System.EventHandler(lookupEstrutura_txtcodigo_Leave);

            lookupproduto.txtcodigo.Leave += new System.EventHandler(lookupproduto_txtcodigo_Leave);
            lookupproduto.btnprocurar.Click += new System.EventHandler(lookupproduto_txtcodigo_Leave);
            lookupproduto.txtconteudo.Click += new System.EventHandler(lookupproduto_txtcodigo_Leave);

            lookupunidadeproduto.Edita(false);

            LimpaAbas();

            new PS.Glb.Class.Utilidades().criaCamposComplementares("PORDEMAPONTAMENTOCOMPL", tabCamposComplementares);
        }

        private void ValidaGridConsumo(OpcaoGridConsumo opcao)
        {
            CabecalhoGridConsumo(opcao);
            gridView1.BestFitColumns();
        }

        private void CriaColunasGridConsumo(DataTable dtFichaEstoque)
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

        private void CabecalhoGridConsumo(OpcaoGridConsumo opcao)
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
                    case "colPORDEMCONSUMO.ID":
                        gridView1.Columns[i].Visible = false;
                        break;
                    case "colCOMPONENTESUBSTITUTO":
                        gridView1.Columns[i].Caption = "Componente Subistituto de";
                        break;
                    case "colPORDEM.DATAINIPLANOP":
                        gridView1.Columns[i].Visible = false;
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
                        gridView1.Columns[i].Caption = "Quantidade Reservada";
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
                        gridView1.Columns[i].Caption = "Quantidade Baixada";
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

                if (opcao == OpcaoGridConsumo.GridPORDEMCONSUMO)
                {
                    existeCODOPERnulo();
                    if (existeCODOPERnull == false)
                    {
                        gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                    }
                }
            }
        }

        private enum OpcaoGridConsumo
        {
            GridIgualPlanejamentoOP = 1,
            GridPORDEMCONSUMO = 2
        }

        private DataTable sqlOrdemProducaoEstrutura(string codigoOP)
        {
            try
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
                    SELECT A.CODEMPRESA,A.CODFILIAL,A.CODESTRUTURA,A.REVESTRUTURA,A.CODIGOOP,A.SEQOP,A.CODSEQPAI,A.QTDEPLANOP,A.QTDEPLANOP,A.PERCCONCLUIDOORDEM, 
		                    B.CODOPERACAO, B.SEQOPERACAO,B.STATUSOPERACAO,B.OPERACAOEXTERNA,
		                    B.TEMPOSETUP,B.TEMPOOPERACAO, B.TEMPOEXTRA,B.TEMPOTOTAL,B.TEMPOSETUPREAL,TEMPOOPERACAOREAL,B.TEMPOEXTRAREAL,B.TEMPOTOTALREAL,B.PERCCONCLUIDOROTEIRO,
		                    C.CODCOMPONENTE,C.QTDCOMPONENTE,C.UNDCOMPONENTE,C.TIPOCONSUMO,C.COMPONENTEORIGINAL,C.COMPONENTECANCELADO,
		                    A.NIVEL,A.CODESTRUTURA
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
                         WHERE A.CODEMPRESA = ? 
                           AND A.CODFILIAL = ?
	                       AND A.CODIGOOP = ?
                      ORDER BY A.NIVEL DESC", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codigoOP });

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void verificaVFICHAESTOQUE(string produto, OpcaoGridConsumo opcao, string TIPOCONSUMO)
        {
            try
            {
                Param_UtilizaReserva = VerificaParametro("UTILIZARESERVA");
                Param_GeraNecessidade = VerificaParametro("GERARNECESSIDADEAUTO");
                string Param_LocalEstoqueMP = VerificaParametro("LOCALESTOQUEMP");

                if (!string.IsNullOrEmpty(Param_LocalEstoqueMP))
                {
                    DataTable dtRecursivo = sqlOrdemProducaoEstrutura(txtNroOp.Text);

                    _Listacomponentes = new List<string>();

                    for (int x = 0; x <= (dtRecursivo.Rows.Count - 1); x++)
                    {
                        if (!_Listacomponentes.Contains(dtRecursivo.Rows[x]["CODCOMPONENTE"].ToString()))
                        {
                            _Listacomponentes.Add(dtRecursivo.Rows[x]["CODCOMPONENTE"].ToString());
                        }

                        if (!_Listacomponentes.Contains(dtRecursivo.Rows[x]["CODESTRUTURA"].ToString()))
                        {
                            _Listacomponentes.Add(dtRecursivo.Rows[x]["CODESTRUTURA"].ToString());
                        }
                    }

                    string _componentes = "";

                    for (int x = 0; x <= (_Listacomponentes.Count() - 1); x++)
                    {
                        _componentes = _componentes + (x == 0 ? "'" + _Listacomponentes[x].ToString() + "'" : ",'" + _Listacomponentes[x].ToString() + "'");
                    }

                    dtFichaEstoque = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
                        with estoque as (
                                           select CODEMPRESA, CODPRODUTO, CODLOCAL, SEQUENCIAL, SALDOFINAL
                                           , RowDesc = ROW_NUMBER() over(partition by CODPRODUTO order by SEQUENCIAL desc)
                                             from VFICHAESTOQUE
                                            where CODEMPRESA = ?
                                              and CODLOCAL = ?
                                              and CODFILIAL = ?" +
                            (string.IsNullOrEmpty(_componentes) ? "" : "AND CODPRODUTO in (" + _componentes + ")") + @"
                                         )
                        select * from estoque where RowDesc = 1"
                    , new object[] { AppLib.Context.Empresa, Param_LocalEstoqueMP, AppLib.Context.Filial });

                    CarregaNecessidadeOP(txtNroOp.Text,txtSeqOp.Text, dtFichaEstoque, opcao, TIPOCONSUMO);
                }
                else
                {
                    throw new Exception("Parâmetro Inválido: Local Estoque Matéria Prima");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CarregaNecessidadeOP(string codOrdem, string SeqOP, DataTable dtFichaEstoque, OpcaoGridConsumo opcao, string TIPOCONSUMO)
        {
            try
            {
                dtNecessidade = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
                        SELECT  A.CODEMPRESA AS 'PORDEM.CODEMPRESA',A.CODFILIAL AS 'PORDEM.CODFILIAL',A.CODESTRUTURA AS 'PORDEM.CODESTRUTURA',A.REVESTRUTURA AS 'PORDEM.REVESTRUTURA',A.CODIGOOP AS 'PORDEM.CODIGOOP',E.CODUNIDCONTROLE  AS 'VPRODUTO.CODUNIDCONTROLE',A.QTDEPLANOP AS 'PORDEM.QTDEPLANOP', B.CODOPERACAO AS 'PORDEMROTEIRO.CODOPERACAO', B.SEQOPERACAO AS 'PORDEMROTEIRO.SEQOPERACAO',
                                A.SEQOP as 'PORDEM.SEQOP', D.DESCESTRUTURA as 'PROTEIROESTRUTURA.DESCESTRUTURA', C.CODCOMPONENTE as 'PORDEMCONSUMO.CODCOMPONENTE', E.DESCRICAO as 'VPRODUTO.DESCRICAO',C.UNDCOMPONENTE as 'PORDEMCONSUMO.UNDCOMPONENTE',C.QTDCOMPONENTE as 'PORDEMCONSUMO.QTDCOMPONENTE',A.NIVEL,C.ID as 'PORDEMCONSUMO.ID',A.DATAINIPLANOP as 'PORDEM.DATAINIPLANOP'
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
                           AND A.SEQOP = ?
                           AND C.TIPOCONSUMO = ?
                      ORDER BY A.NIVEL DESC"
                   , new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codOrdem, SeqOP,TIPOCONSUMO });

                    CriaColunasGridConsumo(dtNecessidade);

                    CarregaEstoqueMP(ref dtNecessidade, dtFichaEstoque);
                    grid.DataSource = dtNecessidade;

                    ValidaGridConsumo(opcao);
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

                List<string> TabelasFilhas = new List<string>();


                TabelasFilhas.Clear();

                string sql = new PS.Glb.Class.Utilidades().getVisao("PCOMPONENTESUB", string.Empty, TabelasFilhas, " WHERE CODPRODUTO in (" + _componentes + ")");
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
                throw;
            }
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
        private void ValidaSaldoGridConsumo(AlteraReservaOP paramAlteraReserva = AlteraReservaOP.Estatico)
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
        private bool existeInconsistenciasGridConsumo(string CODLOCAL)
        {
            try
            {
                bool _valida = false;

                String CONTROLASALDOESTQUE = AppLib.Context.poolConnection.Get("Start").ExecGetField("B", "SELECT CONTROLASALDOESTQUE FROM VPARAMETROS WHERE CODEMPRESA = ?", new Object[] { AppLib.Context.Empresa }).ToString();
                bool SALDOESTQUE = false;

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
                        if (MessageBox.Show("Existem componentes que não foram planejados, deseja continuar?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            _valida = true;
                            throw new Exception("Processo Cancelado!");
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

        //Lista dos tipos de consumo: Grid Consumo
        enum TipoConsumo
        {
            Necessidade,
            Baixa,
            Reserva
        }

        //Lista dos tipos de operação: Grid Consumo
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

        private void alteraPORDEMCONSUMO(AppLib.Data.Connection conn, DataRow row, TipoConsumo tipoconsumo, int CODOPER, int NSEQITEM, int SEQAPO)
        {
            try
            {
                conn.ExecTransaction("UPDATE PORDEMCONSUMO SET CODOPER = ?, NSEQITEM = ?, SEQAPO = ? WHERE ID = ? and CODEMPRESA = ? AND CODFILIAL = ?", new object[] { CODOPER,NSEQITEM,SEQAPO, row["PORDEMAPONTAMENTO.ID"].ToString(), AppLib.Context.Empresa, AppLib.Context.Filial  });
            }
            catch (Exception ex)
            {
                throw ex;
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
        private void IncluirPORDEMCONSUMO(AppLib.Data.Connection conn, DataRow row, TipoConsumo tipoconsumo, int CODOPER, int NSEQITEM, int SEQAPO)
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

                if (SEQAPO > 0)
                {
                    v.Set("SEQAPO", SEQAPO);
                }
                else
                {
                    v.Set("SEQAPO", null);
                }

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

                if (CODOPER > 0)
                {
                    v.Set("CODOPER", CODOPER);
                }
                else
                {
                    v.Set("CODOPER", null);
                }

                if (NSEQITEM > 0)
                {
                    v.Set("NSEQITEM", NSEQITEM);
                }
                else
                {
                    v.Set("NSEQITEM", null);
                }
                
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

            _goperitem.CODEMPRESA = AppLib.Context.Empresa;
            _goperitem.CODPRODUTO = row["PORDEMCONSUMO.CODCOMPONENTE"].ToString();
            _goperitem.DATAENTREGA = Convert.ToDateTime(row["PORDEM.DATAINIPLANOP"].ToString()); //select DATAINIPLANOP from pordem where CODEMPRESA = 1 and CODFILIAL = 2 and CODESTRUTURA = '01.01.2093' and REVESTRUTURA = 1 and CODIGOOP = '000001/17' and SEQOP = '001'
            _goperitem.CODUNIDOPER = row["VPRODUTO.CODUNIDCONTROLE"].ToString();

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

                _goper.CODEMPRESA = AppLib.Context.Empresa;
                _goper.CODFILIAL = AppLib.Context.Filial;
                _goper.CODUSUARIOCRIACAO = AppLib.Context.Usuario;
                _goper.DATACRIACAO = conn.GetDateTime();

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
        private bool IncluiSolicitacaoCompra(AppLib.Data.Connection conn, int indexDTNecessidade, ref int CODOPER_Compra, ref int NSEQITEM_Compra, DataTable dt)
        {
            try
            {
                int i = indexDTNecessidade;

                if (CODOPER_Compra == 0) //Verifica se ja foi criado o CODOPER para solicitações de compras
                {
                    //Se não foi criado, Inclui na tabela GOPER e grava o CODOPER na variavel CODOPER_Compra
                    CODOPER_Compra = IncluirGOPER(conn, TipoGOPER.Requisicao);
                    if (CODOPER_Compra <= 0)
                    {
                        //Tentativa de gravar o item na GOPERITEM, retornando true/false
                        if (IncluirGOPERITEM(CODOPER_Compra, conn, dt.Rows[i]) == true)
                        {
                            //se o item foi gravado na GOPERITEM, a solicitação também é gravada na tabela ORDEMCOMPRA
                            if (IncluirPORDEMCOMPRA(conn, dt.Rows[i], CODOPER_Compra, NSEQITEM_Compra) == true)
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

        private void LimpaAbas()
        {
            editaTrabalho = false;
            editaRecursos = false;
            editaConsumo = false;
            editaSubProdutos = false;
            editaParadas = false;

            proximoSEQAPO = 1;

            Global gl = new Global();
            gl.EnableTab(tabApontamento.TabPages["tabRecursos"], false);
            gl.EnableTab(tabApontamento.TabPages["tabConsumo"], false);
            gl.EnableTab(tabApontamento.TabPages["tabSubProdutos"], false);
            gl.EnableTab(tabApontamento.TabPages["tabParadas"], false);

            LimpaCamposAbaTrabalho();
            LimpaCamposAbaRecurso();
            LimpaCamposAbaParada();
            LimpaCamposAbaSubProduto();
            //Adicionar outras Abas:  Funçao Limpar

            CarregaGridApontamento(true);
            CarregaGridRecurso(true);
            CarregaGridParada(true);
            CarregaGridConsumo(true);
            CarregaGridSubProduto(true);
        }

        void LimpaCamposAbaSubProduto()
        {
            IDSubProduto = 0;
            lookupproduto.Clear();
            lookupunidadeproduto.Clear();
            txtQuantidadeSubProduto.Text = "0";
        }

        void LimpaCamposAbaParada()
        {
            IDParada = 0;
            //dteApontamentoParadas.Text = "";
            //txtSeqApontamentoParadas.Text = "";
            tspHrInicioParada.EditValue = "00:00:00";
            tspHrFimParada.EditValue = "00:00:00";
            txtTempoParada.Text = "";
            lookupMotivoParada.Clear();

        }
        void LimpaCamposAbaRecurso()
        {
            IDRecurso = 0;
            //dteApontamentoRecursos.Text = "";
            //txtSeqApontamentoRecursos.Text = "";
            txtQuantidadeRecursos.Text = "1";
            lookupgruporecurso.Clear();
            lookuprecurso.Clear();

            CarregaGridRecurso(true);
        }

        void LimpaCamposAbaTrabalho()
        {
            IDApontamento = 0;
            editaTrabalho = false;

            dteApontamento.Text = "";
            txtQtdeApontamento.Text = "";
            tspHrInicioSetup.EditValue = "00:00:00";
            tspHrFimSetup.EditValue = "00:00:00";
            txtTempoSetup.Text = "0";
            tspHrInicioTrabalho.EditValue = "00:00:00";
            tspHrFimTrabalho.EditValue = "00:00:00";
            txtTempoTrabalho.Text = "0";
            tspHrInicioExtra.EditValue = "00:00:00";
            tspHrFimExtra.EditValue = "00:00:00";
            txtTempoExtra.Text = "0";

            HabilitaDesabilitaCamposAbaTrabalho(false);
        }

        void HabilitaDesabilitaCamposAbaTrabalho(bool valida)
        {
            dteApontamento.Enabled = valida;
            txtQtdeApontamento.Enabled = valida;
            tspHrInicioSetup.Enabled = valida;
            tspHrFimSetup.Enabled = valida;
            //txtTempoSetup.Enabled = valida;
            tspHrInicioTrabalho.Enabled = valida;
            tspHrFimTrabalho.Enabled = valida;
            //txtTempoTrabalho.Enabled = valida;
            tspHrInicioExtra.Enabled = valida;
            tspHrFimExtra.Enabled = valida;
            //txtTempoExtra.Enabled = valida;
            btnSalvarTrabalho.Enabled = valida;
            btnSalvarApontamentoComplementar.Enabled = valida;
            btnConcluir.Enabled = valida;
        }

        private void FrmTerminalApontamento_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void lookupproduto_txtcodigo_Leave(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(lookupproduto.ValorCodigoInterno))
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODUNIDCONTROLE FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ? ", new object[] { AppLib.Context.Empresa, lookupproduto.ValorCodigoInterno });
                if (dt.Rows.Count > 0)
                {
                    lookupunidadeproduto.txtcodigo.Text = dt.Rows[0]["CODUNIDCONTROLE"].ToString();
                    lookupunidadeproduto.CarregaDescricao();
                    lookupunidadeproduto.Edita(false);
                }
            }
        }

        private void lookupEstrutura_txtcodigo_Leave(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(lookupEstrutura.ValorCodigoInterno))
            {
                if (lookupEstrutura.OutrasChaves.Any(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA") == true)
                {
                    int indexRevisao = lookupEstrutura.OutrasChaves.IndexOf(lookupEstrutura.OutrasChaves.Where(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA").FirstOrDefault());

                    revisaoEstrutura = Convert.ToInt16(lookupEstrutura.OutrasChaves[0].ValorColunaChave.ToString());

                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PROTEIROESTRUTURA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno.ToString(), revisaoEstrutura});
                    if (dt.Rows.Count > 0)
                    {
                        lookupunidade.txtcodigo.Text = dt.Rows[0]["UNDCONTROLE"].ToString();
                        lookupunidade.CarregaDescricao();
                    }
                }
            }
        }

        private void lookupgruporecurso_txtcodigo_Leave(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(lookupgruporecurso.ValorCodigoInterno))
            {
                lookuprecurso.Grid_WhereVisao[3].ValorFixo = @"Select '" + lookupgruporecurso.ValorCodigoInterno.ToString() +"' as GRUPORECURSO ";
                lookuprecurso.Clear();
            }
            string TipoRecurso = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TIPORECURSO FROM PGRUPORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODGRUPORECURSO = ?", new object[] {AppLib.Context.Empresa, AppLib.Context.Filial, lookupgruporecurso.ValorCodigoInterno }).ToString();

            if (TipoRecurso == "EQ")
            {
                txtQuantidadeRecursos.Enabled = false;
            }
            else
            {
                txtQuantidadeRecursos.Enabled = true;
            }
        }

        private void FrmTerminalApontamento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.Shift && e.KeyCode == Keys.F12)
            {
                this.Dispose();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FrmTerminalApontamento_Load(object sender, EventArgs e)
        {
            splitContainer2.SplitterDistance = 80;
            splitContainer4.SplitterDistance = 80;
            splitContainer6.SplitterDistance = 80;
            splitContainer8.SplitterDistance = 80;
            splitContainer10.SplitterDistance = 80;

            lookupEstrutura.Edita(false);

            string Mask = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT MASCCODBARRA FROM PPARAM WHERE CODFILIAL = ?", new object[] { AppLib.Context.Filial }).ToString();

            txtCodigoBarras.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            txtCodigoBarras.Properties.Mask.EditMask = Mask;
        }

        void LimpaPesquisa()
        {
            existeOP = false;

            txtNroOp.Text = "";
            txtSeqOp.Text = "";
            txtDescricao.Text = "";
            txtQtPlan.Text = "";
            lookupEstrutura.Clear();
            lookupunidade.Clear();

            cmbSeqOperacao.DataSource = null;
            cmbSeqOperacao.SelectedIndex = -1;
        }

        private void ValidaCodigoBarras()
        {
            if (!string.IsNullOrEmpty(txtCodigoBarras.Text))
            {
                string[] codigobarras;
                codigobarras = txtCodigoBarras.Text.Split(';');

                if (codigobarras.Count() >= 4)
                {
                    DataTable dtOrdem = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
                                select A.CODESTRUTURA,A.REVESTRUTURA,A.CODIGOOP,A.SEQOP,A.DESCRICAOOP,A.QTDEPLANOP,A.STATUSOP,B.CODUNIDCONTROLE,C.SEQOPERACAO,C.CODOPERACAO as 'CODOPERACAO'
	                              from PORDEM A JOIN VPRODUTO B ON A.CODESTRUTURA = B.CODPRODUTO AND A.CODEMPRESA = B.CODEMPRESA
					                          JOIN PORDEMROTEIRO C ON A.CODEMPRESA = C.CODEMPRESA 
								                                  AND A.CODFILIAL = C.CODFILIAL
									                              AND A.CODESTRUTURA = C.CODESTRUTURA
									                              AND A.REVESTRUTURA = C.REVESTRUTURA
									                         AND A.CODESTRUTURA = C.CODESTRUTURA
									                         AND A.REVESTRUTURA = C.REVESTRUTURA
									                         AND A.CODIGOOP = C.CODIGOOP
									                         AND A.SEQOP = C.SEQOP
	                             where A.CODIGOOP = ? 
                                   and A.SEQOP = ? 
                                   and A.CODEMPRESA = ? 
                                   and A.CODFILIAL = ?
                                   " + (codigobarras.Count() < 5 ? "" : " and C.SEQOPERACAO = '" + codigobarras[4].ToString() + "'"),
                                   new object[] { codigobarras[2].ToString(), codigobarras[3].ToString(), codigobarras[0].ToString(), codigobarras[1].ToString() });
                    if (dtOrdem.Rows.Count <= 0)
                    {
                        MessageBox.Show("OP não encontrada!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (Convert.ToInt16(dtOrdem.Rows[0]["STATUSOP"]) == 1) //2 = PLANEJADA
                        {
                            MessageBox.Show("A OP não foi planejada", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            LimpaPesquisa();
                            LimpaAbas();

                            existeOP = true;

                            txtNroOp.Text = dtOrdem.Rows[0]["CODIGOOP"].ToString();
                            txtSeqOp.Text = dtOrdem.Rows[0]["SEQOP"].ToString();
                            txtDescricao.Text = dtOrdem.Rows[0]["DESCRICAOOP"].ToString();
                            txtQtPlan.Text = dtOrdem.Rows[0]["QTDEPLANOP"].ToString();

                            lookupEstrutura.txtcodigo.Text = dtOrdem.Rows[0]["CODESTRUTURA"].ToString();
                            lookupEstrutura.OutrasChaves.Clear();
                            lookupEstrutura.OutrasChaves.Add(new Newlookup_OutrasChaves { NomeColunaChave = "REVESTRUTURA", ValorColunaChave = dtOrdem.Rows[0]["REVESTRUTURA"].ToString() });
                            lookupEstrutura.CarregaDescricao();

                            lookupunidade.txtcodigo.Text = dtOrdem.Rows[0]["CODUNIDCONTROLE"].ToString();
                            lookupunidade.CarregaDescricao();

                            cmbSeqOperacao.DataSource = dtOrdem;
                            cmbSeqOperacao.DisplayMember = "CODOPERACAO";
                            cmbSeqOperacao.ValueMember = "SEQOPERACAO";

                            if (codigobarras.Count() >= 5)
                            {
                                cmbSeqOperacao.SelectedValue = codigobarras[4].ToString();
                                CarregaGridApontamento();
                            }
                            else
                            {
                                cmbSeqOperacao.SelectedIndex = -1;
                                //CarregaGridApontamento(true);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Código de barras inválido", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtNroOp.Text))
                {
                    MessageBox.Show("Informe o número da OP", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (string.IsNullOrEmpty(txtSeqOp.Text))
                {
                    MessageBox.Show("Informe a sequência da OP", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DataTable dtOrdem = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"
                                select A.CODESTRUTURA,A.REVESTRUTURA,A.CODIGOOP,A.SEQOP,A.DESCRICAOOP,A.QTDEPLANOP,A.STATUSOP,B.CODUNIDCONTROLE,C.SEQOPERACAO,C.CODOPERACAO as 'CODOPERACAO'
	                              from PORDEM A JOIN VPRODUTO B ON A.CODESTRUTURA = B.CODPRODUTO AND A.CODEMPRESA = B.CODEMPRESA
					                          JOIN PORDEMROTEIRO C ON A.CODEMPRESA = C.CODEMPRESA 
								                                  AND A.CODFILIAL = C.CODFILIAL
									                              AND A.CODESTRUTURA = C.CODESTRUTURA
									                              AND A.REVESTRUTURA = C.REVESTRUTURA
									                         AND A.CODESTRUTURA = C.CODESTRUTURA
									                         AND A.REVESTRUTURA = C.REVESTRUTURA
									                         AND A.CODIGOOP = C.CODIGOOP
									                         AND A.SEQOP = C.SEQOP
	                             where A.CODIGOOP = ? and A.SEQOP = ? and A.CODEMPRESA = ? and A.CODFILIAL = ?", new object[] { txtNroOp.Text, txtSeqOp.Text, AppLib.Context.Empresa, AppLib.Context.Filial });
                    if (dtOrdem.Rows.Count <= 0)
                    {
                        MessageBox.Show("OP não encontrada!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (Convert.ToInt16(dtOrdem.Rows[0]["STATUSOP"]) != 2) //2 = PLANEJADA
                        {
                            MessageBox.Show("A OP não foi planejada", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            existeOP = true;

                            txtDescricao.Text = dtOrdem.Rows[0]["DESCRICAOOP"].ToString();
                            txtQtPlan.Text = dtOrdem.Rows[0]["QTDEPLANOP"].ToString();
                            lookupEstrutura.txtcodigo.Text = dtOrdem.Rows[0]["CODESTRUTURA"].ToString();

                            lookupEstrutura.OutrasChaves.Clear();
                            lookupEstrutura.OutrasChaves.Add(new Newlookup_OutrasChaves { NomeColunaChave = "REVESTRUTURA", ValorColunaChave = dtOrdem.Rows[0]["REVESTRUTURA"].ToString() });
                            lookupEstrutura.CarregaDescricao();

                            lookupunidade.txtcodigo.Text = dtOrdem.Rows[0]["CODUNIDCONTROLE"].ToString();
                            lookupunidade.CarregaDescricao();

                            cmbSeqOperacao.DataSource = dtOrdem;
                            cmbSeqOperacao.DisplayMember = "CODOPERACAO";
                            cmbSeqOperacao.ValueMember = "SEQOPERACAO";

                            cmbSeqOperacao.SelectedIndex = -1;
                            CarregaGridApontamento(true);
                        }
                    }
                }
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

        private void existeCODOPERnulo()
        {
            try
            {
                existeCODOPERnull = false;

                string sql = @"  select * from PORDEMCONSUMO
                              WHERE CODEMPRESA = ?
                                AND CODFILIAL = ?
                                AND CODESTRUTURA =?
                                AND REVESTRUTURA =?
                                AND CODIGOOP = ?
                                AND SEQOP = ?
                                AND TIPOCONSUMO = 'B'
                                AND CODOPER is null";
                //AND SEQOPERACAO = ?
                //AND CODOPERACAO = ?
                //DataTable dtExiste = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno, revisaoEstrutura, txtNroOp.Text, txtSeqOp.Text, SEQOPERACAO, CODOPERACAO });
                DataTable dtExiste = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno, revisaoEstrutura, txtNroOp.Text, txtSeqOp.Text });

                if (dtExiste.Rows.Count > 0)
                {
                    existeCODOPERnull = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool verificaTIPOCONSUMO(string TIPOCONSUMO)
        {
            bool verifica = false;

            string sql = @"  select * from PORDEMCONSUMO
                              WHERE CODEMPRESA = ?
                                AND CODFILIAL = ?
                                AND CODESTRUTURA =?
                                AND REVESTRUTURA =?
                                AND CODIGOOP = ?
                                AND SEQOP = ?
                                AND SEQOPERACAO = ?
                                AND CODOPERACAO = ?
                                AND TIPOCONSUMO = ?";

            DataTable dtExiste = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno, revisaoEstrutura, txtNroOp.Text, txtSeqOp.Text, SEQOPERACAO, CODOPERACAO, TIPOCONSUMO });

            if (dtExiste.Rows.Count > 0)
            {
                verifica = true;
            }

            return verifica;
        }

        private void existePORDEMCONSUMO()
        {
            existeCODOPERnull = false;
            existeBaixa = false;
            existeNecessidade = false;
            existeReserva = false;

            string sql = @"  select TIPOCONSUMO,Count(TIPOCONSUMO) as TotalTIPOCONSUMO from PORDEMCONSUMO 
                              WHERE CODEMPRESA = ?
                                AND CODFILIAL = ?
                                AND CODESTRUTURA =?
                                AND REVESTRUTURA =?
                                AND CODIGOOP = ?
                                AND SEQOP = ?
                              group by TIPOCONSUMO";
            //AND SEQOPERACAO = ?
            //AND CODOPERACAO = ?

            //DataTable dtExiste = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno, revisaoEstrutura, txtNroOp.Text, txtSeqOp.Text, SEQOPERACAO, CODOPERACAO });   
            DataTable dtExiste = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno, revisaoEstrutura, txtNroOp.Text, txtSeqOp.Text});

            for (int i = 0; i <= dtExiste.Rows.Count - 1 ;i++)
            {
                switch (dtExiste.Rows[i]["TIPOCONSUMO"].ToString())
                {
                    case "N":
                        existeNecessidade = true;
                        break;
                    case "R":
                        existeReserva = true;
                        break;
                    case "B":
                        existeBaixa = true;
                        existeCODOPERnulo();
                        break;
                    default:
                        throw new Exception("Erro ao verificar consumo de matéria prima");
                }
            }
        }

        private DataTable retornaTempoTotalApontamento()
        {
            string sql = @"SELECT  SUM(A.TEMPOSETUP) as TOTALTEMPOSETUP
                                 , SUM(A.TEMPOOPERACAO) as TOTALTEMPOOPERACAO 
                                 , SUM(A.TEMPOEXTRA) as TOTALTEMPOEXTRA
                                 , SUM(A.TEMPOTOTAL) as TOTALTEMPOTOTAL
                                 , SUM(A.QTDEAPO) as TOTALQTDEAPO
                             FROM PORDEMAPONTAMENTO A WITH (NOLOCK)
                            WHERE A.CODEMPRESA = ?
                              AND A.CODFILIAL = ?
                              AND A.CODESTRUTURA = ?
                              AND A.REVESTRUTURA = ?
                              AND A.CODIGOOP = ?
                              AND A.SEQOP = ?
                              AND A.SEQOPERACAO = ?
                              AND A.CODOPERACAO = ?";
            return AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno, revisaoEstrutura, txtNroOp.Text, txtSeqOp.Text, SEQOPERACAO, CODOPERACAO });
        }

        private void retornaRefugoPadrao(int SEQAPO,decimal QTDEAPO, AppLib.Data.Connection conn)
        {
            try
            {
                string sql = "SELECT * FROM PROTEIROESTRUTURA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?";
                DataTable dtrefugo = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno, revisaoEstrutura });

                if (dtrefugo.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtrefugo.Rows[0]["GERARREFUGO"]) == true) //PRODUTOREFUGO,PORCENTAGEMREFUGO,ARREDREFUGO
                    {
                        Decimal QtdEntrada = (Convert.ToDecimal(dtrefugo.Rows[0]["PORCENTAGEMREFUGO"])/100) * QTDEAPO;

                        string UndRefugo = AppLib.Context.poolConnection.Get("Start").ExecGetField("",@"SELECT CODUNIDCONTROLE FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ? ", new object[] { AppLib.Context.Empresa, dtrefugo.Rows[0]["PRODUTOREFUGO"].ToString() }).ToString();
                        if (SalvarPORDEMENTRADArefugo(SEQAPO, QtdEntrada, dtrefugo.Rows[0]["PRODUTOREFUGO"].ToString(), UndRefugo, conn) == false)
                        {
                            throw new Exception("Erro ao gravar refugo padrão");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void CarregaGridSubProduto(bool LimpaGrid = false)
        {
            string tabela = "PORDEMENTRADA"; 
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();

            try
            {
                string campos = @"  PORDEMENTRADA.ID as 'PORDEMENTRADA.ID'
                                    ,PORDEMAPONTAMENTO.DATAAPO as 'PORDEMAPONTAMENTO.DATAAPO'
                                    ,PORDEMENTRADA.SEQAPO as 'PORDEMENTRADA.SEQAPO'
                                    ,PORDEMENTRADA.CODPRODUTO AS 'PORDEMENTRADA.CODPRODUTO'
                                    ,VPRODUTO.DESCRICAO AS 'VPRODUTO.DESCRICAO'
	                                ,PORDEMENTRADA.UNDPRODUTO AS 'PORDEMENTRADA.UNDPRODUTO'
	                                ,PORDEMENTRADA.QTDENTRADA AS 'PORDEMENTRADA.QTDENTRADA'
	                                ,(CASE PORDEMENTRADA.TIPOENTRADA WHEN 1 THEN 'Produto Acabado'
                                                                     WHEN 2 THEN 'Produto Intermediário'
                                                                     WHEN 3 THEN 'Refugo'
                                                                     WHEN 4 THEN 'Devolução'
                                     END ) AS 'PORDEMENTRADA.TIPOENTRADA'
	                                ,PORDEMENTRADA.CODOPER AS 'PORDEMENTRADA.CODOPER'
	                                ,PORDEMENTRADA.NSEQITEM AS 'PORDEMENTRADA.NSEQITEM'
	                                ,PORDEMENTRADA.DATAMOVIMENTO AS 'PORDEMENTRADA.DATAMOVIMENTO'";

                string sql = @"SELECT " + (LimpaGrid == false ? campos : " TOP 0 " + campos) + @"
                                         FROM PORDEMENTRADA JOIN VPRODUTO ON PORDEMENTRADA.CODEMPRESA = VPRODUTO.CODEMPRESA 
                                                                         AND PORDEMENTRADA.CODPRODUTO = VPRODUTO.CODPRODUTO 
                                                            JOIN PORDEMAPONTAMENTO ON PORDEMAPONTAMENTO.CODEMPRESA = PORDEMENTRADA.CODEMPRESA
                			                                              AND PORDEMAPONTAMENTO.CODFILIAL = PORDEMENTRADA.CODFILIAL
                				                                          AND PORDEMAPONTAMENTO.CODESTRUTURA = PORDEMENTRADA.CODESTRUTURA
                				                                          AND PORDEMAPONTAMENTO.REVESTRUTURA = PORDEMENTRADA .REVESTRUTURA
                				                                          AND PORDEMAPONTAMENTO.CODIGOOP = PORDEMENTRADA.CODIGOOP
                				                                          AND PORDEMAPONTAMENTO.SEQOP = PORDEMENTRADA.SEQOP
                				                                          AND PORDEMAPONTAMENTO.CODOPERACAO = PORDEMENTRADA.CODOPERACAO 
                				                                          AND PORDEMAPONTAMENTO.SEQOPERACAO = PORDEMENTRADA.SEQOPERACAO
                				                                          AND PORDEMAPONTAMENTO.SEQAPO = PORDEMENTRADA.SEQAPO
                                        WHERE PORDEMENTRADA.CODEMPRESA = ?
                                          AND PORDEMENTRADA.CODFILIAL = ? "
                            + (LimpaGrid == false ? @" AND PORDEMENTRADA.CODESTRUTURA = ?
                                          AND PORDEMENTRADA.REVESTRUTURA = ?
                                          AND PORDEMENTRADA.CODIGOOP = ?
                                          AND PORDEMENTRADA.SEQOP = ?
                                          AND PORDEMENTRADA.SEQOPERACAO = ?
                                          AND PORDEMENTRADA.CODOPERACAO = ? 
                                          AND PORDEMENTRADA.SEQAPO = ? " : "");

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

                if (lookupEstrutura.OutrasChaves.Any(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA") == true)
                {
                    int indexRevisao = lookupEstrutura.OutrasChaves.IndexOf(lookupEstrutura.OutrasChaves.Where(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA").FirstOrDefault());

                    revisaoEstrutura = Convert.ToInt16(lookupEstrutura.OutrasChaves[0].ValorColunaChave.ToString());
                }

                gridSubproduto.DataSource = null;
                gridViewSubproduto.Columns.Clear();
                if (LimpaGrid == false)
                {
                    dtSubproduto = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno, revisaoEstrutura, txtNroOp.Text, txtSeqOp.Text, SEQOPERACAO, CODOPERACAO, proximoSEQAPO });
                }
                else
                {
                    dtSubproduto = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial });
                }

                gridSubproduto.DataSource = dtSubproduto;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?)", new object[] { tabela, "VPRODUTO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewSubproduto.Columns.Count; i++)
                {
                    //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewSubproduto.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        if (gridViewSubproduto.Columns[i].FieldName == "PORDEMENTRADA.ID" || gridViewSubproduto.Columns[i].FieldName == "PORDEMAPONTAMENTO.DATAAPO")
                        {
                            gridViewSubproduto.Columns[i].Visible = false;
                        }
                        gridViewSubproduto.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewSubproduto.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
            
        private void CarregaGridConsumo(bool LimpaGrid = false)
        {
            try
            {
                string Param_UtilizaReserva = VerificaParametro("UTILIZARESERVA");
                string Param_MomentoBaixa = VerificaParametro("MOMENTOBAIXA");

                //Verifica o tipo dos registros existentes na tabela PORDEMCONSUMO(B=Baixa,R=Reserva,N=Necessidade)
                existePORDEMCONSUMO();

                if (LimpaGrid == true)
                {
                    return;
                }
                 
                //if (existeNecessidade == true || existeBaixa == false || existeReserva == false)
                //{
                //    //Acontecerá se não for planejada a OP antes do Apontamento, isto é permitido?
                //    verificaVFICHAESTOQUE(lookupEstrutura.ValorCodigoInterno , OpcaoGridConsumo.GridIgualPlanejamentoOP,"N");
                //}
                //else 
                if(existeNecessidade == true || existeBaixa == true || existeReserva == false)
                {
                    verificaVFICHAESTOQUE(lookupEstrutura.ValorCodigoInterno, OpcaoGridConsumo.GridPORDEMCONSUMO, "B");
                }
                else if(existeNecessidade == true || existeBaixa == false || existeReserva == true)
                {
                    if (Param_UtilizaReserva == "1")
                    {
                        if (Param_MomentoBaixa != "1") //2=APONTAR ULTIMA OPERAÇÃO / 3=ANTES DE DAR ENTRADA NO ESTOQUE
                        {
                            //Ao gravar Baixas deixar CODOPER e NSEQITEM = null, pode alterar qtde, tirar e incluir item
                            verificaVFICHAESTOQUE(lookupEstrutura.ValorCodigoInterno, OpcaoGridConsumo.GridPORDEMCONSUMO, "N");
                        }
                        else //1 = PLANEJAR ORDEM DE PRODUÇÃO, BAIXAR MATÉRIA PRIMA
                        {
                            //carrega grid bloqueado
                            verificaVFICHAESTOQUE(lookupEstrutura.ValorCodigoInterno, OpcaoGridConsumo.GridPORDEMCONSUMO, "N");
                        }
                    }
                    else if (Param_UtilizaReserva == "2")
                    {
                        //Parametro alterado depois de planejado a OP, conforme orientação do Marcelo, Ignorar qualquer tipo de processamento.
                    }
                    else
                    {
                        throw new Exception("Parâmetro Inválido: UTILIZARESERVA");
                    }
                }
                else
                {
                    throw new Exception("Erro ao carregar consumo");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CarregaGridParada(bool LimpaGrid = false)
        {
            string tabela = "PORDEMAPTOPARADA";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();

            try
            {
                string campos = @" PORDEMAPTOPARADA.ID as 'PORDEMAPTOPARADA.ID'
                                  ,PORDEMAPTOPARADA.IDAPONTAMENTO as 'PORDEMAPTOPARADA.IDAPONTAMENTO'
                                  ,PORDEMAPTOPARADA.SEQAPO as 'PORDEMAPTOPARADA.SEQAPO'
                                  ,PORDEMAPONTAMENTO.DATAAPO as 'PORDEMAPONTAMENTO.DATAAPO'
                                  ,PORDEMAPTOPARADA.HRINICIOPARADA as 'PORDEMAPTOPARADA.HRINICIOPARADA'
	                              ,PORDEMAPTOPARADA.HRFIMPARADA as 'PORDEMAPTOPARADA.HRFIMPARADA'
                                  ,PORDEMAPTOPARADA.TEMPOPARADA as 'PORDEMAPTOPARADA.TEMPOPARADA'
	                              ,PORDEMAPTOPARADA.CODMOTIVOPARADA as 'PORDEMAPTOPARADA.CODMOTIVOPARADA'
                                  ,PORDEMMOTIVOPARADA.DESCMOTIVOPARADA as 'Descrição da parada'";

                string sql = @"SELECT " + (LimpaGrid == false ? campos : " TOP 0 " + campos) + @"
                                         FROM PORDEMAPTOPARADA JOIN PORDEMAPONTAMENTO on PORDEMAPTOPARADA.IDAPONTAMENTO = PORDEMAPONTAMENTO.ID
                                        LEFT OUTER JOIN PORDEMMOTIVOPARADA ON PORDEMMOTIVOPARADA.CODEMPRESA = PORDEMAPTOPARADA.CODEMPRESA AND PORDEMMOTIVOPARADA.CODFILIAL = PORDEMAPTOPARADA.CODFILIAL AND PORDEMMOTIVOPARADA.CODMOTIVOPARADA = PORDEMAPTOPARADA.CODMOTIVOPARADA 
                                        WHERE PORDEMAPTOPARADA.CODEMPRESA = ?
                                          AND PORDEMAPTOPARADA.CODFILIAL = ? "
                            + (LimpaGrid == false ? @" AND PORDEMAPTOPARADA.CODESTRUTURA = ?
                                          AND PORDEMAPTOPARADA.REVESTRUTURA = ?
                                          AND PORDEMAPTOPARADA.CODIGOOP = ?
                                          AND PORDEMAPTOPARADA.SEQOP = ?
                                          AND PORDEMAPTOPARADA.SEQOPERACAO = ?
                                          AND PORDEMAPTOPARADA.CODOPERACAO = ?
                                          AND PORDEMAPTOPARADA.SEQAPO = ?" : "");

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

                if (lookupEstrutura.OutrasChaves.Any(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA") == true)
                {
                    int indexRevisao = lookupEstrutura.OutrasChaves.IndexOf(lookupEstrutura.OutrasChaves.Where(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA").FirstOrDefault());

                    revisaoEstrutura = Convert.ToInt16(lookupEstrutura.OutrasChaves[0].ValorColunaChave.ToString());
                }

                gridParada.DataSource = null;
                gridViewParada.Columns.Clear();
                if (LimpaGrid == false)
                {
                    dtParada = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno, revisaoEstrutura, txtNroOp.Text, txtSeqOp.Text, SEQOPERACAO, CODOPERACAO,proximoSEQAPO });
                }
                else
                {
                    dtParada = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial });
                }

                gridParada.DataSource = dtParada;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?)", new object[] { tabela, "PORDEMAPONTAMENTO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewParada.Columns.Count; i++)
                {
                    //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewParada.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        if (gridViewParada.Columns[i].FieldName == "PORDEMAPTORECURSO.ID" || gridViewParada.Columns[i].FieldName == "PORDEMAPTOPARADA.IDAPONTAMENTO" || gridViewParada.Columns[i].FieldName == "PORDEMAPTOPARADA.ID" || gridViewParada.Columns[i].FieldName == "PORDEMAPONTAMENTO.DATAAPO")
                        {
                            gridViewParada.Columns[i].Visible = false;
                        }
                        gridViewParada.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewParada.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregaGridRecurso(bool LimpaGrid = false)
        {
            string tabela = "PORDEMAPTORECURSO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string campos = @"PGRUPORECURSO.TIPORECURSO AS 'PGRUPORECURSO.TIPORECURSO'
                               ,PORDEMAPONTAMENTO.DATAAPO AS 'PORDEMAPONTAMENTO.DATAAPO'
                               ,PGRUPORECURSO.CODGRUPORECURSO AS 'PGRUPORECURSO.CODGRUPORECURSO'
                               ,PORDEMAPTORECURSO.ID as 'PORDEMAPTORECURSO.ID'
                               ,PORDEMAPTORECURSO.SEQAPO as 'PORDEMAPTORECURSO.SEQAPO'
                               ,PORDEMAPTORECURSO.SEQOPERACAO as 'PORDEMAPTORECURSO.SEQOPERACAO'
                               ,PORDEMAPTORECURSO.CODOPERACAO as 'PORDEMAPTORECURSO.CODOPERACAO'
                               ,PORDEMAPTORECURSO.CODRECURSO as 'PORDEMAPTORECURSO.CODRECURSO'
                               ,PORDEMAPTORECURSO.TIPORECURSO as 'PORDEMAPTORECURSO.TIPORECURSO'
                               ,PORDEMAPTORECURSO.QTDRECURSO as 'PORDEMAPTORECURSO.QTDRECURSO'";

                string sql = @"SELECT " + (LimpaGrid == false ? campos : " TOP 0 " + campos) + @"
                                         FROM PORDEMAPTORECURSO JOIN PRECURSO ON PORDEMAPTORECURSO.CODEMPRESA = PRECURSO.CODEMPRESA
                				  AND PORDEMAPTORECURSO.CODFILIAL = PRECURSO.CODFILIAL
                				  AND PORDEMAPTORECURSO.CODRECURSO = PRECURSO.CODRECURSO
                JOIN PGRUPORECURSO ON PRECURSO.CODEMPRESA = PGRUPORECURSO.CODEMPRESA
                				  AND PRECURSO.CODFILIAL = PGRUPORECURSO.CODFILIAL
                				  AND PRECURSO.CODGRUPORECURSO = PGRUPORECURSO.CODGRUPORECURSO
                JOIN PORDEMAPONTAMENTO ON PORDEMAPONTAMENTO.CODEMPRESA = PORDEMAPTORECURSO.CODEMPRESA
                			      AND PORDEMAPONTAMENTO.CODFILIAL = PORDEMAPTORECURSO.CODFILIAL
                				  AND PORDEMAPONTAMENTO.CODESTRUTURA = PORDEMAPTORECURSO.CODESTRUTURA
                				  AND PORDEMAPONTAMENTO.REVESTRUTURA = PORDEMAPTORECURSO .REVESTRUTURA
                				  AND PORDEMAPONTAMENTO.CODIGOOP = PORDEMAPTORECURSO.CODIGOOP
                				  AND PORDEMAPONTAMENTO.SEQOP = PORDEMAPTORECURSO.SEQOP
                				  AND PORDEMAPONTAMENTO.CODOPERACAO = PORDEMAPTORECURSO.CODOPERACAO 
                				  AND PORDEMAPONTAMENTO.SEQOPERACAO = PORDEMAPTORECURSO.SEQOPERACAO
                				  AND PORDEMAPONTAMENTO.SEQAPO = PORDEMAPTORECURSO.SEQAPO 
                                        WHERE PORDEMAPTORECURSO.CODEMPRESA = ?
                                          AND PORDEMAPTORECURSO.CODFILIAL = ? "
                            + (LimpaGrid == false ? @" AND PORDEMAPTORECURSO.CODESTRUTURA = ?
                                          AND PORDEMAPTORECURSO.REVESTRUTURA = ?
                                          AND PORDEMAPTORECURSO.CODIGOOP = ?
                                          AND PORDEMAPTORECURSO.SEQOP = ?
                                          AND PORDEMAPTORECURSO.SEQOPERACAO = ?
                                          AND PORDEMAPTORECURSO.CODOPERACAO = ? 
                                          AND PORDEMAPTORECURSO.SEQAPO = ?" : "");

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

                if (lookupEstrutura.OutrasChaves.Any(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA") == true)
                {
                    int indexRevisao = lookupEstrutura.OutrasChaves.IndexOf(lookupEstrutura.OutrasChaves.Where(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA").FirstOrDefault());

                    revisaoEstrutura = Convert.ToInt16(lookupEstrutura.OutrasChaves[0].ValorColunaChave.ToString());
                }

                gridRecursos.DataSource = null;
                gridViewRecursos.Columns.Clear();
                if (LimpaGrid == false)
                {
                    dtRecursos = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno, revisaoEstrutura, txtNroOp.Text, txtSeqOp.Text, SEQOPERACAO, CODOPERACAO, proximoSEQAPO }); ;
                }
                else
                {
                    dtRecursos = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial });
                }

                gridRecursos.DataSource = dtRecursos;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?,?)", new object[] { tabela,"PGRUPORECURSO", "PORDEMAPONTAMENTO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewRecursos.Columns.Count; i++)
                {
                    //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewRecursos.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        if (gridViewRecursos.Columns[i].FieldName == "PORDEMAPTORECURSO.ID" || gridViewRecursos.Columns[i].FieldName == "PORDEMAPONTAMENTO.DATAAPO" || gridViewRecursos.Columns[i].FieldName == "PGRUPORECURSO.TIPORECURSO" || gridViewRecursos.Columns[i].FieldName == "PGRUPORECURSO.CODGRUPORECURSO")
                        {
                            gridViewRecursos.Columns[i].Visible = false;
                        }
                        gridViewRecursos.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewRecursos.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregaGridApontamento(bool LimpaGrid = false)
        {
            string tabela = "PORDEMAPONTAMENTO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                //,PORDEMAPONTAMENTO.HRINICIOPARADA as 'PORDEMAPONTAMENTO.HRINICIOPARADA'
                //,PORDEMAPONTAMENTO.HRFIMPARADA as 'PORDEMAPONTAMENTO.HRFIMPARADA'
                //,PORDEMAPONTAMENTO.TEMPOPARADA as 'PORDEMAPONTAMENTO.TEMPOPARADA'
                //,PORDEMAPONTAMENTO.CODMOTIVOPARADA as 'PORDEMAPONTAMENTO.CODMOTIVOPARADA'
                string campos = @"PORDEMAPONTAMENTO.ID as 'PORDEMAPONTAMENTO.ID'
                               ,PORDEMAPONTAMENTO.SEQAPO as 'PORDEMAPONTAMENTO.SEQAPO'
                               ,PORDEMAPONTAMENTO.DATAAPO as 'PORDEMAPONTAMENTO.DATAAPO'
                               ,PORDEMAPONTAMENTO.QTDEAPO as 'PORDEMAPONTAMENTO.QTDEAPO'
                               ,PORDEMAPONTAMENTO.SEQOPERACAO as 'PORDEMAPONTAMENTO.SEQOPERACAO'
                               ,PORDEMAPONTAMENTO.CODOPERACAO as 'PORDEMAPONTAMENTO.CODOPERACAO'
                               ,PORDEMAPONTAMENTO.HRINICIOSETUP as 'PORDEMAPONTAMENTO.HRINICIOSETUP'
                               ,PORDEMAPONTAMENTO.HRFIMSETUP as 'PORDEMAPONTAMENTO.HRFIMSETUP'
                               ,PORDEMAPONTAMENTO.TEMPOSETUP as 'PORDEMAPONTAMENTO.TEMPOSETUP'
                               ,PORDEMAPONTAMENTO.HRINICIOTRABALHO as 'PORDEMAPONTAMENTO.HRINICIOTRABALHO'
                               ,PORDEMAPONTAMENTO.HRFIMTRABALHO as 'PORDEMAPONTAMENTO.HRFIMTRABALHO'
                               ,PORDEMAPONTAMENTO.TEMPOOPERACAO as 'PORDEMAPONTAMENTO.TEMPOOPERACAO'
                               ,PORDEMAPONTAMENTO.HRINICIOEXTRA as 'PORDEMAPONTAMENTO.HRINICIOEXTRA'
                               ,PORDEMAPONTAMENTO.HRFIMEXTRA as 'PORDEMAPONTAMENTO.HRFIMEXTRA'
                               ,PORDEMAPONTAMENTO.TEMPOEXTRA as 'PORDEMAPONTAMENTO.TEMPOEXTRA'
                               ,PORDEMAPONTAMENTO.TEMPOTOTAL as 'PORDEMAPONTAMENTO.TEMPOTOTAL'
                               ,PORDEMAPONTAMENTO.USUARIOAPO as 'PORDEMAPONTAMENTO.USUARIOAPO'";

                string sql = @"SELECT " + (LimpaGrid == false ? campos : " TOP 0 " + campos ) + @"
                                 FROM PORDEMAPONTAMENTO 
                                WHERE PORDEMAPONTAMENTO.CODEMPRESA = ?
                                  AND PORDEMAPONTAMENTO.CODFILIAL = ? "
                            + (LimpaGrid == false ? @" AND PORDEMAPONTAMENTO.CODESTRUTURA = ?
                                  AND PORDEMAPONTAMENTO.REVESTRUTURA = ?
                                  AND PORDEMAPONTAMENTO.CODIGOOP = ?
                                  AND PORDEMAPONTAMENTO.SEQOP = ?
                                  AND PORDEMAPONTAMENTO.SEQOPERACAO = ?
                                  AND PORDEMAPONTAMENTO.CODOPERACAO = ? " : "");
                                  
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

                if (lookupEstrutura.OutrasChaves.Any(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA") == true)
                {
                    int indexRevisao = lookupEstrutura.OutrasChaves.IndexOf(lookupEstrutura.OutrasChaves.Where(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA").FirstOrDefault());

                    revisaoEstrutura = Convert.ToInt16(lookupEstrutura.OutrasChaves[0].ValorColunaChave.ToString());
                }

                gridApontamento.DataSource = null;
                gridViewApontamento.Columns.Clear();
                if (LimpaGrid == false)
                {
                    dtTrabalho = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno, revisaoEstrutura, txtNroOp.Text, txtSeqOp.Text, SEQOPERACAO, CODOPERACAO }); ;
                }
                else
                {
                    dtTrabalho = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial });
                }
                
                gridApontamento.DataSource = dtTrabalho;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?)", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewApontamento.Columns.Count; i++)
                {
                    //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewApontamento.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        if (gridViewApontamento.Columns[i] .FieldName == "PORDEMAPONTAMENTO.ID")
                        {
                            gridViewApontamento.Columns[i].Visible = false;
                        }
                        gridViewApontamento.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewApontamento.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            ValidaCodigoBarras();
        }

        private void txtCodigoBarras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ValidaCodigoBarras();
            }
        }

        private string Calculatempo(DateTime hrinicial, DateTime hrfinal)
        {
            TimeSpan ts = hrfinal - hrinicial;

            return ts.TotalMinutes.ToString();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                if(existeOP == true)
                {
                    LimpaCamposAbaTrabalho();
                    LimpaCamposAbaRecurso();
                    HabilitaDesabilitaCamposAbaTrabalho(true);

                    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();

                    dteApontamento.Text = Convert.ToDateTime(conn.GetDateTime()).ToShortDateString();
                    txtQtdeApontamento.Focus();

                    proximoSEQAPO = Convert.ToInt32(conn.ExecGetField(1, @" SELECT MAX(A.SEQAPO) + 1 AS SEQAPO
                                                      FROM PORDEMAPONTAMENTO A
                                                     WHERE A.CODEMPRESA = ?
                                                       AND A.CODFILIAL = ?
                                                       AND A.CODESTRUTURA = ?
                                                       AND A.REVESTRUTURA = ?
                                                       AND A.CODIGOOP = ? 
                                                       AND A.SEQOP = ?
                                                       AND A.SEQOPERACAO = ?
                                                       AND A.CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno, revisaoEstrutura, txtNroOp.Text, txtSeqOp.Text, SEQOPERACAO, CODOPERACAO }));
                }
                else
                {
                    MessageBox.Show("Informe a OP", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool SalvarRecurso()
        {
            bool _salvar = false;

            if (validacaoRecursos() == true)
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PORDEMAPTORECURSO");
                conn.BeginTransaction();

                try
                {
                    v.Set("CODEMPRESA", AppLib.Context.Empresa);
                    v.Set("CODFILIAL", AppLib.Context.Filial);

                    v.Set("CODESTRUTURA", lookupEstrutura.ValorCodigoInterno);

                    if (lookupEstrutura.OutrasChaves.Any(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA") == true)
                    {
                        int indexRevisao = lookupEstrutura.OutrasChaves.IndexOf(lookupEstrutura.OutrasChaves.Where(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA").FirstOrDefault());
                        v.Set("REVESTRUTURA", lookupEstrutura.OutrasChaves[0].ValorColunaChave.ToString());
                    }

                    v.Set("CODIGOOP", txtNroOp.Text);
                    v.Set("SEQOP", txtSeqOp.Text);
                    v.Set("SEQOPERACAO", SEQOPERACAO);
                    v.Set("CODOPERACAO", CODOPERACAO);

                    v.Set("SEQAPO", txtSeqApontamentoRecursos.Text);

                    v.Set("CODRECURSO", lookuprecurso.ValorCodigoInterno);
                    string tipoRecurso = conn.ExecGetField("", "select TIPORECURSO from PGRUPORECURSO where CODGRUPORECURSO = ?", new object[] { lookupgruporecurso.ValorCodigoInterno }).ToString();
                    v.Set("TIPORECURSO", tipoRecurso);
                    v.Set("QTDRECURSO", txtQuantidadeRecursos.Text);
                    
                    if (editaRecursos == true)
                    {
                        v.Set("ID", IDRecurso);
                        v.Update();
                    }
                    else
                    {
                        v.Insert();
                    }

                    conn.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    conn.Rollback();
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                _salvar = false;
            }
            return _salvar;
        }


        private bool SalvarPORDEMENTRADArefugo(int SEQAPO, decimal QTDENTRADA, string CODPRODUTO, string UNDPRODUTO, AppLib.Data.Connection conn)
        {
            bool _salvar = false;

            AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PORDEMENTRADA");

            try
            {
                v.Set("CODEMPRESA", AppLib.Context.Empresa);
                v.Set("CODFILIAL", AppLib.Context.Filial);

                v.Set("CODESTRUTURA", lookupEstrutura.ValorCodigoInterno);

                if (lookupEstrutura.OutrasChaves.Any(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA") == true)
                {
                    int indexRevisao = lookupEstrutura.OutrasChaves.IndexOf(lookupEstrutura.OutrasChaves.Where(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA").FirstOrDefault());
                    v.Set("REVESTRUTURA", lookupEstrutura.OutrasChaves[0].ValorColunaChave.ToString());
                }

                v.Set("CODIGOOP", txtNroOp.Text);
                v.Set("SEQOP", txtSeqOp.Text);
                v.Set("SEQOPERACAO", SEQOPERACAO);
                v.Set("CODOPERACAO", CODOPERACAO);
                v.Set("SEQAPO", SEQAPO);

                v.Set("QTDENTRADA", QTDENTRADA);
                v.Set("TIPOENTRADA", 3);

                v.Set("CODOPER", null);
                v.Set("NSEQITEM", null);

                v.Set("CODPRODUTO", CODPRODUTO);
                v.Set("UNDPRODUTO", UNDPRODUTO);

                v.Set("DATAMOVIMENTO", conn.GetDateTime());

                v.Insert();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _salvar;
        }

        private bool SalvarTrabalho()
        {
            bool _salvar = false;

            if (validacaoTrabalho() == true)
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PORDEMAPONTAMENTO");
                conn.BeginTransaction();

                try
                {
                    if (MessageBox.Show("Deseja concluir o apontamento?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    { 
                        v.Set("CODEMPRESA", AppLib.Context.Empresa);
                        v.Set("CODFILIAL", AppLib.Context.Filial);

                        v.Set("CODESTRUTURA", lookupEstrutura.ValorCodigoInterno);

                        if (lookupEstrutura.OutrasChaves.Any(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA") == true)
                        {
                            int indexRevisao = lookupEstrutura.OutrasChaves.IndexOf(lookupEstrutura.OutrasChaves.Where(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA").FirstOrDefault());
                            v.Set("REVESTRUTURA", lookupEstrutura.OutrasChaves[0].ValorColunaChave.ToString());
                        }

                        v.Set("CODIGOOP", txtNroOp.Text);
                        v.Set("SEQOP", txtSeqOp.Text);
                        v.Set("SEQOPERACAO", SEQOPERACAO);

                        if (editaTrabalho == true)
                        {
                            v.Set("SEQAPO", proximoSEQAPO);
                        }
                        else
                        {
                            proximoSEQAPO = Convert.ToInt32(conn.ExecGetField(1, @" SELECT (MAX(A.SEQAPO) + 1) AS SEQAPO
                                                          FROM PORDEMAPONTAMENTO A
                                                         WHERE A.CODEMPRESA = ?
                                                           AND A.CODFILIAL = ?
                                                           AND A.CODESTRUTURA = ?
                                                           AND A.REVESTRUTURA = ?
                                                           AND A.CODIGOOP = ? 
                                                           AND A.SEQOP = ?
                                                           AND A.SEQOPERACAO = ?
                                                           AND A.CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno, revisaoEstrutura, txtNroOp.Text, txtSeqOp.Text, SEQOPERACAO, CODOPERACAO }));

                            v.Set("SEQAPO", proximoSEQAPO);
                        }

                        v.Set("DATAAPO", Convert.ToDateTime(dteApontamento.Text));
                        v.Set("TEMPOSETUP", Convert.ToDecimal(txtTempoSetup.Text));
                        v.Set("TEMPOOPERACAO", Convert.ToDecimal(txtTempoTrabalho.Text));
                        v.Set("TEMPOEXTRA", Convert.ToDecimal(txtTempoExtra.Text));
                        v.Set("TEMPOPARADA", 0);

                        decimal TTotal = Convert.ToDecimal(txtTempoSetup.Text) + Convert.ToDecimal(txtTempoTrabalho.Text) + Convert.ToDecimal(txtTempoExtra.Text);
                        v.Set("TEMPOTOTAL", TTotal);

                        v.Set("USUARIOAPO", AppLib.Context.Usuario.ToString());

                        v.Set("CODOPERACAO", CODOPERACAO);
                        v.Set("CODMOTIVOPARADA", null);
                        v.Set("HRINICIOTRABALHO", Convert.ToDateTime(tspHrInicioTrabalho.Text));
                        v.Set("HRFIMTRABALHO", Convert.ToDateTime(tspHrFimTrabalho.Text));
                        v.Set("HRINICIOSETUP", Convert.ToDateTime(tspHrInicioSetup.Text));
                        v.Set("HRFIMSETUP", Convert.ToDateTime(tspHrFimSetup.Text));

                        if (Convert.ToDateTime(tspHrInicioExtra.Text).TimeOfDay.ToString() == "00:00:00")
                        {
                            v.Set("HRINICIOEXTRA", null);
                        }
                        else
                        {
                            v.Set("HRINICIOEXTRA", Convert.ToDateTime(tspHrInicioExtra.Text));
                        }

                        if (Convert.ToDateTime(tspHrFimExtra.Text).TimeOfDay.ToString() == "00:00:00")
                        {
                            v.Set("HRFIMEXTRA", null);
                        }
                        else
                        {
                            v.Set("HRFIMEXTRA", Convert.ToDateTime(tspHrFimExtra.Text));
                        }

                        v.Set("HRINICIOPARADA", null);
                        v.Set("HRFIMPARADA", null);
                        v.Set("QTDEAPO", Convert.ToDecimal(txtQtdeApontamento.Text));

                        if (editaTrabalho == true)
                        {
                            v.Set("ID", IDApontamento);
                            v.Update();
                        }
                        else
                        {
                            v.Insert();
                        }

                        //Atualiza TEMPOTOTALREAL e DATAINIREAL e DATAFIMREAL da PORDEMROTEIRO
                        DataTable dtTempoApontamentos = retornaTempoTotalApontamento();

                        if (dtTempoApontamentos.Rows.Count > 0)
                        {
                            string sqlPORDEMROTEIRO = @"UPDATE PORDEMROTEIRO SET 
                                                           DATAINIREAL = CASE WHEN DATAINIREAL IS NULL THEN GETDATE() ELSE DATAINIREAL END, 
                                                           DATAFIMREAL = GETDATE(),
                                                           TEMPOSETUPREAL = " + Convert.ToDecimal(dtTempoApontamentos.Rows[0]["TOTALTEMPOSETUP"]).ToString().Replace(".", "").Replace(",", ".") + @",
                                                           TEMPOOPERACAOREAL = " + Convert.ToDecimal(dtTempoApontamentos.Rows[0]["TOTALTEMPOOPERACAO"]).ToString().Replace(".", "").Replace(",", ".") + @",
                                                           TEMPOEXTRAREAL = " + Convert.ToDecimal(dtTempoApontamentos.Rows[0]["TOTALTEMPOEXTRA"]).ToString().Replace(".", "").Replace(",", ".") + @",
                                                           TEMPOTOTALREAL = " + Convert.ToDecimal(dtTempoApontamentos.Rows[0]["TOTALTEMPOTOTAL"]).ToString().Replace(".", "").Replace(",", ".") + @",
                                                           QTDEREAL = " + Convert.ToDecimal(dtTempoApontamentos.Rows[0]["TOTALQTDEAPO"]).ToString().Replace(".", "").Replace(",", ".") + @"
                                                     WHERE CODEMPRESA = ?
                                                       AND CODFILIAL = ?
                                                       AND CODESTRUTURA = ?
                                                       AND REVESTRUTURA = ?
                                                       AND CODIGOOP = ?
                                                       AND SEQOP = ?
                                                       AND SEQOPERACAO = ?
                                                       AND CODOPERACAO = ?";

                            conn.ExecTransaction(sqlPORDEMROTEIRO, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno, revisaoEstrutura, txtNroOp.Text, txtSeqOp.Text, SEQOPERACAO, CODOPERACAO });
                            //DataTable dtExiste = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqlPORDEMROTEIRO, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno, revisaoEstrutura, txtNroOp.Text, txtSeqOp.Text, SEQOPERACAO, CODOPERACAO });
                        }

                        if (editaTrabalho == false)
                        {
                            //Verifica se existe refugo padrão cadastrado na estrutura
                            retornaRefugoPadrao(proximoSEQAPO, Convert.ToDecimal(txtQtdeApontamento.Text), conn);
                        }

                        if (todasOperacoesApontadas() == true)
                        {
                            if (MessageBox.Show("Deseja encerrar a ordem de produção?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                ConcluirOP(conn);
                            }
                        }

                        if (SalvaCompl(conn) == true)
                        {
                            conn.Commit();
                        }
                        else
                        {
                            throw new Exception("Erro ao gravar apontamento");
                        }

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    conn.Rollback();
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                _salvar = false;
            }
            return _salvar;
        }

        private bool SalvaCompl(AppLib.Data.Connection conn)
        {
            int ID = Convert.ToInt32(conn.ExecGetField(0, @" SELECT A.ID
                                                          FROM PORDEMAPONTAMENTO A
                                                         WHERE A.CODEMPRESA = ?
                                                           AND A.CODFILIAL = ?
                                                           AND A.CODESTRUTURA = ?
                                                           AND A.REVESTRUTURA = ?
                                                           AND A.CODIGOOP = ? 
                                                           AND A.SEQOP = ?
                                                           AND A.SEQOPERACAO = ?
                                                           AND A.CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno, revisaoEstrutura, txtNroOp.Text, txtSeqOp.Text, SEQOPERACAO, CODOPERACAO }));
            if (ID > 0)
            {
                List<PS.Glb.Class.Parametro> param = new List<PS.Glb.Class.Parametro>();

                PS.Glb.Class.Parametro parametro = new PS.Glb.Class.Parametro();

                parametro.nomeParametro = "CODEMPRESA";
                parametro.valorParametro = AppLib.Context.Empresa.ToString();

                param.Add(parametro);

                parametro = new PS.Glb.Class.Parametro();

                parametro.nomeParametro = "CODFILIAL";
                parametro.valorParametro = AppLib.Context.Filial.ToString();

                param.Add(parametro);

                parametro = new PS.Glb.Class.Parametro();

                parametro.nomeParametro = "ID";
                parametro.valorParametro = IDApontamento.ToString();

                param.Add(parametro);

                PS.Glb.Class.Utilidades util = new PS.Glb.Class.Utilidades();

                if (tabCamposComplementares.Controls.Count > 0)
                {
                    //util.salvaCamposComplementares(this, "PORDEMAPONTAMENTOCOMPL", tabCamposComplementares, param, AppLib.Context.poolConnection.Get("Start"));
                    if (util.salvaCamposComplementares(this, "PORDEMAPONTAMENTOCOMPL", tabCamposComplementares, param, AppLib.Context.poolConnection.Get("Start")) == false)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                    // João Pedro Luchiari 18/10/2017

                    //bool retorno = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT COUNT(CODIGOOP) FROM PORDEMAPONTAMENTOCOMPL WHERE CODEMPRESA = ? AND CODFILIAL = ? AND ID = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, ID }));
                    //if (retorno == false)
                    //{
                    //    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO PORDEMAPONTAMENTOCOMPL (CODEMPRESA,CODFILIAL,ID) VALUES (?, ?, ?)", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, ID });
                    //}

                    //PS.Glb.Class.Utilidades util = new PS.Glb.Class.Utilidades();
                    //string sql = util.update(this, tabCamposComplementares, "PORDEMCOMPL");
                    //if (!string.IsNullOrEmpty(sql))
                    //{
                    //    sql = sql.Remove(sql.Length - 2, 1) + " WHERE CODEMPRESA = ? AND CODFILIAL = ? AND ID = ?";
                    //    AppLib.Context.poolConnection.Get("Start").ExecTransaction(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, ID });
                    //}
                    return true;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        private bool validacaoRecursos()
        {
            //IDRecurso
            bool verifica = true;

            errorProvider.Clear();

            if (existeOP == false)
            {
                errorProvider.SetError(txtCodigoBarras, "Informe o código de barras");
                return false;
            }
            else
            {
                lookupgruporecurso.mensagemErrorProvider = "";
                lookuprecurso.mensagemErrorProvider = "";

                if (string.IsNullOrEmpty(txtSeqApontamentoRecursos.Text))
                {
                    errorProvider.SetError(txtSeqApontamentoRecursos, "Selecione um apontamento");
                    verifica = false;
                }

                if (string.IsNullOrEmpty(lookupgruporecurso.ValorCodigoInterno))
                {
                    lookupgruporecurso.mensagemErrorProvider = "Selecione o grupo do recurso";
                    verifica = false;
                }

                if (string.IsNullOrEmpty(lookuprecurso.ValorCodigoInterno))
                {
                    lookuprecurso.mensagemErrorProvider = "Selecione o recurso";
                    verifica = false;
                }

                if (string.IsNullOrEmpty(txtQuantidadeRecursos.Text))
                {
                    errorProvider.SetError(txtQuantidadeRecursos, "Informe a quantidade do recurso");
                    verifica = false;
                }

                return verifica;
            }
        }
        private bool validacaoTrabalho()
        {
            bool verifica = true;

            errorProvider.Clear();

            if (existeOP == false)
            {
                errorProvider.SetError(txtCodigoBarras, "Informe o código de barras");
                return false;
            }
            else
            {
                //AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();

                if (lookupEstrutura.OutrasChaves.Any(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA") == true)
                {
                    int indexRevisao = lookupEstrutura.OutrasChaves.IndexOf(lookupEstrutura.OutrasChaves.Where(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA").FirstOrDefault());

                    revisaoEstrutura = Convert.ToInt16(lookupEstrutura.OutrasChaves[0].ValorColunaChave.ToString());
                }

                //if (Convert.ToDateTime(tspHrInicioSetup.Text).TimeOfDay.ToString() == "00:00:00")
                //{
                //    errorProvider.SetError(tspHrInicioSetup, "Informe o horário de início de setup");
                //    verifica = false;
                //}

                //if (Convert.ToDateTime(tspHrFimSetup.Text).TimeOfDay.ToString() == "00:00:00")
                //{
                //    errorProvider.SetError(tspHrFimSetup, "Informe o horário final de setup");
                //    verifica = false;
                //}

                if(cmbSeqOperacao.SelectedIndex == -1)
                {
                    errorProvider.SetError(cmbSeqOperacao, "Selecione uma operação");
                    verifica = false;
                }

                if (Convert.ToDateTime(tspHrInicioSetup.Text).TimeOfDay.ToString() != "00:00:00" && Convert.ToDateTime(tspHrFimSetup.Text).TimeOfDay.ToString() != "00:00:00")
                {
                    if (Convert.ToDateTime(tspHrFimSetup.Text).TimeOfDay <= Convert.ToDateTime(tspHrInicioSetup.Text).TimeOfDay)
                    {
                        errorProvider.SetError(tspHrFimSetup, "Horario final do setup deve ser maior que o horário inicial");
                        verifica = false;
                    }
                    else
                    {
                        txtTempoSetup.Text = Calculatempo(Convert.ToDateTime(tspHrInicioSetup.Text), Convert.ToDateTime(tspHrFimSetup.Text));
                    }
                }

                if (Convert.ToDateTime(tspHrInicioTrabalho.Text).TimeOfDay.ToString() == "00:00:00")
                {
                    errorProvider.SetError(tspHrInicioTrabalho, "Informe o horário de início de trabalho");
                    verifica = false;
                }

                if (Convert.ToDateTime(tspHrFimTrabalho.Text).TimeOfDay.ToString() == "00:00:00")
                {
                    errorProvider.SetError(tspHrFimTrabalho, "Informe o horário final de trabalho");
                    verifica = false;
                }

                if (Convert.ToDateTime(tspHrInicioTrabalho.Text).TimeOfDay.ToString() != "00:00:00" && Convert.ToDateTime(tspHrFimTrabalho.Text).TimeOfDay.ToString() != "00:00:00")
                {
                    if (Convert.ToDateTime(tspHrFimTrabalho.Text).TimeOfDay <= Convert.ToDateTime(tspHrInicioTrabalho.Text).TimeOfDay)
                    {
                        errorProvider.SetError(tspHrFimTrabalho, "Horario final do trabalho deve ser maior que o horário inicial");
                        verifica = false;
                    }
                    else
                    {
                        txtTempoTrabalho.Text = Calculatempo(Convert.ToDateTime(tspHrInicioTrabalho.Text), Convert.ToDateTime(tspHrFimTrabalho.Text));
                    }
                }

                //if (Convert.ToDateTime(tspHrInicioExtra.Text).TimeOfDay.ToString() == "00:00:00")
                //{
                //    errorProvider.SetError(tspHrInicioExtra, "Informe o horário de início extra");
                //    verifica = false;
                //}

                //if (Convert.ToDateTime(tspHrFimExtra.Text).TimeOfDay.ToString() == "00:00:00")
                //{
                //    errorProvider.SetError(tspHrFimExtra, "Informe o horário de fim extra");
                //    verifica = false;
                //}

                if (Convert.ToDateTime(tspHrInicioExtra.Text).TimeOfDay.ToString() != "00:00:00" && Convert.ToDateTime(tspHrFimExtra.Text).TimeOfDay.ToString() != "00:00:00")
                {
                    if (Convert.ToDateTime(tspHrFimExtra.Text).TimeOfDay <= Convert.ToDateTime(tspHrInicioExtra.Text).TimeOfDay)
                    {
                        errorProvider.SetError(tspHrFimExtra, "Horario final extra deve ser maior que o horário inicial");
                        verifica = false;
                    }
                    else
                    {
                        txtTempoExtra.Text = Calculatempo(Convert.ToDateTime(tspHrInicioExtra.Text), Convert.ToDateTime(tspHrFimExtra.Text));
                    }
                }

                if (Convert.ToDecimal(txtQtdeApontamento.Text) <= 0)
                {
                    errorProvider.SetError(txtQtdeApontamento, "Informe a quantidade do apontamento");
                    verifica = false;
                }

                if (string.IsNullOrEmpty(dteApontamento.Text))
                {
                    errorProvider.SetError(dteApontamento, "Informe a data do apontamento");
                    verifica = false;
                }



                //tspHrInicioSetup.Enabled = valida;
                //tspHrFimSetup.Enabled = valida;
                ////txtTempoSetup.Enabled = valida;
                //tspHrInicioTrabalho.Enabled = valida;
                //tspHrFimTrabalho.Enabled = valida;
                ////txtTempoTrabalho.Enabled = valida;
                //tspHrInicioExtra.Enabled = valida;
                //tspHrFimExtra.Enabled = valida;

                return verifica;
            }   
        }

        private bool SalvarConsumo()
        {
            bool _salvar = false;

            if (validacaoConsumo() == true)
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PORDEMCONSUMO");
                conn.BeginTransaction();

                try
                {

                }
                catch (Exception ex)
                {
                    conn.Rollback();
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                _salvar = false;
            }
            return _salvar;
        }

        private bool validacaoConsumo()
        {
            bool verifica = true;



            return verifica;
        }

        private bool SalvarSubProdutos()
        {
            bool _salvar = false;

            if (validacaoSubProdutos() == true)
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PORDEMENTRADA");
                conn.BeginTransaction();

                try
                {
                    v.Set("CODEMPRESA", AppLib.Context.Empresa);
                    v.Set("CODFILIAL", AppLib.Context.Filial);

                    v.Set("CODESTRUTURA", lookupEstrutura.ValorCodigoInterno);

                    if (lookupEstrutura.OutrasChaves.Any(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA") == true)
                    {
                        int indexRevisao = lookupEstrutura.OutrasChaves.IndexOf(lookupEstrutura.OutrasChaves.Where(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA").FirstOrDefault());
                        v.Set("REVESTRUTURA", lookupEstrutura.OutrasChaves[0].ValorColunaChave.ToString());
                    }

                    v.Set("CODIGOOP", txtNroOp.Text);
                    v.Set("SEQOP", txtSeqOp.Text);
                    v.Set("SEQOPERACAO", SEQOPERACAO);
                    v.Set("CODOPERACAO", CODOPERACAO);
                    v.Set("SEQAPO", txtSeqApontamentoSubProdutos.Text);

                    v.Set("QTDENTRADA", Convert.ToDecimal(txtQuantidadeSubProduto.Text));

                    v.Set("CODOPER", null);
                    v.Set("NSEQITEM", null);

                    v.Set("CODPRODUTO", lookupproduto.ValorCodigoInterno);
                    v.Set("UNDPRODUTO", lookupunidadeproduto.ValorCodigoInterno);

                    v.Set("DATAMOVIMENTO", conn.GetDateTime());

                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PROTEIROESTRUTURA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno.ToString(), revisaoEstrutura });
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["TIPOESTRUTURA"].ToString() == "A") //Acabado
                        {
                            v.Set("TIPOENTRADA", 1);
                        }
                        else if (dt.Rows[0]["TIPOESTRUTURA"].ToString() =="S") //Semi-Acabado
                        {
                            v.Set("TIPOENTRADA", 2);
                        }
                        else
                        {
                            throw new Exception("Erro, campo TIPOESTRUTURA incorreto");
                        }
                    }
                    else
                    {
                        v.Set("TIPOENTRADA", 3);
                    }

                    if (editaSubProdutos == true)
                    {
                        v.Set("ID", IDSubProduto);
                        v.Update();
                    }
                    else
                    {
                        v.Insert();
                    }

                    conn.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    conn.Rollback();
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                _salvar = false;
            }
            return _salvar;
        }

        private bool validacaoSubProdutos()
        {
            bool verifica = true;

            lookupproduto.mensagemErrorProvider = "";

            if (existeOP == false)
            {
                errorProvider.SetError(txtCodigoBarras, "Informe o código de barras");
                return false;
            }
            else
            {
                if (string.IsNullOrEmpty(txtQuantidadeSubProduto.Text))
                {
                    errorProvider.SetError(txtQuantidadeSubProduto, "Informe a quantidade");
                    verifica = false;
                }

                if (string.IsNullOrEmpty(lookupproduto.ValorCodigoInterno))
                {
                    lookupproduto.mensagemErrorProvider = "Informe o Produto";
                    verifica = false;
                }

                return verifica;
            }

            return verifica;
        }

        private bool SalvarParada()
        {
            bool _salvar = false;

            if (validacaoParada() == true)
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PORDEMAPTOPARADA");
                conn.BeginTransaction();

                try
                {
                    v.Set("CODEMPRESA", AppLib.Context.Empresa);
                    v.Set("CODFILIAL", AppLib.Context.Filial);

                    v.Set("CODESTRUTURA", lookupEstrutura.ValorCodigoInterno);

                    if (lookupEstrutura.OutrasChaves.Any(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA") == true)
                    {
                        int indexRevisao = lookupEstrutura.OutrasChaves.IndexOf(lookupEstrutura.OutrasChaves.Where(estrutura => estrutura.NomeColunaChave == "REVESTRUTURA").FirstOrDefault());
                        v.Set("REVESTRUTURA", lookupEstrutura.OutrasChaves[0].ValorColunaChave.ToString());
                    }

                    v.Set("CODIGOOP", txtNroOp.Text);
                    v.Set("SEQOP", txtSeqOp.Text);
                    v.Set("SEQOPERACAO", SEQOPERACAO);
                    v.Set("CODOPERACAO", CODOPERACAO);

                    v.Set("SEQAPO", txtSeqApontamentoParadas.Text);
                    v.Set("IDAPONTAMENTO", IDApontamento);

                    v.Set("TEMPOPARADA", Convert.ToDecimal(txtTempoParada.Text));
                    v.Set("HRINICIOPARADA", Convert.ToDateTime(tspHrInicioParada.Text));
                    v.Set("HRFIMPARADA", Convert.ToDateTime(tspHrFimParada.Text));
                    v.Set("CODMOTIVOPARADA", lookupMotivoParada.ValorCodigoInterno);

                    if (editaParadas == true)
                    {
                        v.Set("ID", IDParada);
                        v.Update();
                    }
                    else
                    {
                        v.Insert();
                    }

                    conn.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    conn.Rollback();
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                _salvar = false;
            }
            return _salvar;
        }

        private bool validacaoParada()
        {
            bool verifica = true;

            errorProvider.Clear();

            lookupMotivoParada.mensagemErrorProvider = "";

            if (existeOP == false)
            {
                errorProvider.SetError(txtCodigoBarras, "Informe o código de barras");
                return false;
            }
            else
            {
                if (string.IsNullOrEmpty(txtSeqApontamentoParadas.Text))
                {
                    errorProvider.SetError(txtSeqApontamentoRecursos, "Selecione um apontamento");
                    verifica = false;
                }

                if (Convert.ToDateTime(tspHrInicioParada.Text).TimeOfDay.ToString() == "00:00:00")
                {
                    errorProvider.SetError(tspHrInicioParada, "Informe o horário de início da parada");
                    verifica = false;
                }

                if (Convert.ToDateTime(tspHrFimParada.Text).TimeOfDay.ToString() == "00:00:00")
                {
                    errorProvider.SetError(tspHrFimParada, "Informe o horário final da parada");
                    verifica = false;
                }

                if (string.IsNullOrEmpty(lookupMotivoParada.ValorCodigoInterno))
                {
                    lookupMotivoParada.mensagemErrorProvider = "Informe o Motivo";
                    verifica = false;
                }

                return verifica;
            }

            return verifica;
        }

        private void cmbSeqOperacao_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbSeqOperacao.SelectedIndex >= 0)
            {
                LimpaAbas();
                //LimpaCamposAbaTrabalho();
                //LimpaCamposAbaRecurso();
                CarregaGridApontamento(true);
                CarregaGridRecurso(true);
                CarregaGridParada(true);
                CarregaGridConsumo(true);
                CarregaGridSubProduto(true);

                CODOPERACAO = cmbSeqOperacao.Text;
                SEQOPERACAO = cmbSeqOperacao.SelectedValue.ToString();

                CarregaGridApontamento();
            }
        }

        private void tspHrFimTrabalho_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(tspHrInicioTrabalho.Text).TimeOfDay.ToString() != "00:00:00" && Convert.ToDateTime(tspHrFimTrabalho.Text).TimeOfDay.ToString() != "00:00:00")
            {
                if (Convert.ToDateTime(tspHrFimTrabalho.Text) <= Convert.ToDateTime(tspHrInicioTrabalho.Text))
                {
                    txtTempoTrabalho.Text = "0";
                }
                else
                {
                    txtTempoTrabalho.Text = Calculatempo(Convert.ToDateTime(tspHrInicioTrabalho.Text), Convert.ToDateTime(tspHrFimTrabalho.Text));
                }
            }
            else
            {
                txtTempoTrabalho.Text = "0";
            }
        }

        private void tspHrFimSetup_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(tspHrInicioSetup.Text).TimeOfDay.ToString() != "00:00:00" && Convert.ToDateTime(tspHrFimSetup.Text).TimeOfDay.ToString() != "00:00:00")
            {
                if (Convert.ToDateTime(tspHrFimSetup.Text) <= Convert.ToDateTime(tspHrInicioSetup.Text))
                {
                    txtTempoSetup.Text = "0";
                }
                else
                {
                    txtTempoSetup.Text = Calculatempo(Convert.ToDateTime(tspHrInicioSetup.Text), Convert.ToDateTime(tspHrFimSetup.Text));
                }
            }
            else
            {
                txtTempoSetup.Text = "0";
            }
        }

        private void tspHrFimExtra_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(tspHrInicioExtra.Text).TimeOfDay.ToString() != "00:00:00" && Convert.ToDateTime(tspHrFimExtra.Text).TimeOfDay.ToString() != "00:00:00")
            {
                if (Convert.ToDateTime(tspHrFimExtra.Text) <= Convert.ToDateTime(tspHrInicioExtra.Text))
                {
                    txtTempoExtra.Text = "0";
                }
                else
                {
                    txtTempoExtra.Text = Calculatempo(Convert.ToDateTime(tspHrInicioExtra.Text), Convert.ToDateTime(tspHrFimExtra.Text));
                }
            }
            else
            {
                txtTempoExtra.Text = "0";
            }
        }

        private void EditaParada()
        {
            int row = gridViewParada.GetDataSourceRowIndex(gridViewParada.FocusedRowHandle);

            try
            {
                editaParadas = true;

                IDParada = Convert.ToInt32(dtParada.Rows[row]["PORDEMAPTOPARADA.ID"].ToString());
                dteApontamentoParadas.Text = Convert.ToDateTime(dtParada.Rows[row]["PORDEMAPONTAMENTO.DATAAPO"].ToString()).ToShortDateString();
                txtSeqApontamentoParadas.Text = dtParada.Rows[row]["PORDEMAPTOPARADA.SEQAPO"].ToString();
                tspHrInicioParada.EditValue = dtParada.Rows[row]["PORDEMAPTOPARADA.HRINICIOPARADA"].ToString(); 
                tspHrFimParada.EditValue = dtParada.Rows[row]["PORDEMAPTOPARADA.HRFIMPARADA"].ToString(); 
                txtTempoParada.Text = dtParada.Rows[row]["PORDEMAPTOPARADA.TEMPOPARADA"].ToString(); 
                lookupMotivoParada.txtcodigo.Text = dtParada.Rows[row]["PORDEMAPTOPARADA.CODMOTIVOPARADA"].ToString();
                lookupMotivoParada.CarregaDescricao();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void EditaSubProduto()
        {
            int row = gridViewSubproduto.GetDataSourceRowIndex(gridViewSubproduto.FocusedRowHandle);

            try
            {
                editaSubProdutos = true;

                IDSubProduto = Convert.ToInt32(dtSubproduto.Rows[row]["PORDEMENTRADA.ID"].ToString());
                dteApontamentoSubProdutos.Text = Convert.ToDateTime(dtSubproduto.Rows[row]["PORDEMAPONTAMENTO.DATAAPO"].ToString()).ToShortDateString();
                txtSeqApontamentoSubProdutos.Text = dtSubproduto.Rows[row]["PORDEMENTRADA.SEQAPO"].ToString();

                lookupproduto.txtcodigo.Text = dtSubproduto.Rows[row]["PORDEMENTRADA.CODPRODUTO"].ToString();
                lookupproduto.CarregaDescricao();
                lookupunidadeproduto.txtcodigo.Text = dtSubproduto.Rows[row]["PORDEMENTRADA.UNDPRODUTO"].ToString();
                lookupunidadeproduto.CarregaDescricao();
                txtQuantidadeSubProduto.Text = dtSubproduto.Rows[row]["PORDEMENTRADA.QTDENTRADA"].ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void EditaRecurso()
        {
            int row = gridViewRecursos.GetDataSourceRowIndex(gridViewRecursos.FocusedRowHandle);

            try
            {
                editaRecursos = true;

                IDRecurso = Convert.ToInt32(dtRecursos.Rows[row]["PORDEMAPTORECURSO.ID"].ToString());

                dteApontamentoRecursos.Text = Convert.ToDateTime(dtRecursos.Rows[row]["PORDEMAPONTAMENTO.DATAAPO"].ToString()).ToShortDateString();
                txtSeqApontamentoRecursos.Text = dtRecursos.Rows[row]["PORDEMAPTORECURSO.SEQAPO"].ToString();

                VerificaGruposPermitidos();

                lookupgruporecurso.txtcodigo.Text = dtRecursos.Rows[row]["PGRUPORECURSO.CODGRUPORECURSO"].ToString();
                lookupgruporecurso.CarregaDescricao();
                lookuprecurso.Grid_WhereVisao[3].ValorFixo = @"Select '" + lookupgruporecurso.ValorCodigoInterno.ToString() + "' as GRUPORECURSO ";

                lookuprecurso.txtcodigo.Text = dtRecursos.Rows[row]["PORDEMAPTORECURSO.CODRECURSO"].ToString();
                lookuprecurso.CarregaDescricao();

                txtQuantidadeRecursos.Text = dtRecursos.Rows[row]["PORDEMAPTORECURSO.QTDRECURSO"].ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void EditaTrabalho()
        {
            int row = gridViewApontamento.GetDataSourceRowIndex(gridViewApontamento.FocusedRowHandle);
            try
            {
                editaTrabalho = true;

                proximoSEQAPO = Convert.ToInt32(dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.SEQAPO"].ToString());

                IDApontamento = Convert.ToInt32(dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.ID"].ToString());

                dteApontamento.Text = Convert.ToDateTime(dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.DATAAPO"].ToString()).ToShortDateString();
                txtQtdeApontamento.Text = dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.QTDEAPO"].ToString();
                tspHrInicioSetup.EditValue = dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.HRINICIOSETUP"].ToString();
                tspHrFimSetup.EditValue = dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.HRFIMSETUP"].ToString();
                txtTempoSetup.Text = dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.TEMPOSETUP"].ToString();
                tspHrInicioTrabalho.EditValue = dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.HRINICIOTRABALHO"].ToString();
                tspHrFimTrabalho.EditValue = dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.HRFIMTRABALHO"].ToString();
                txtTempoTrabalho.Text = dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.TEMPOOPERACAO"].ToString();
                txtTempoExtra.Text = dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.TEMPOEXTRA"].ToString();

                if (dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.HRINICIOEXTRA"] == DBNull.Value)
                {
                    tspHrInicioExtra.EditValue = "00:00:00";
                }
                else
                {
                    tspHrInicioExtra.EditValue = dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.HRINICIOEXTRA"].ToString();
                }

                if (dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.HRFIMEXTRA"] == DBNull.Value)
                {
                    tspHrFimExtra.EditValue = "00:00:00";
                }
                else
                {
                    tspHrFimExtra.EditValue = dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.HRFIMEXTRA"].ToString();
                }
                dteApontamentoRecursos.Text = Convert.ToDateTime(dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.DATAAPO"].ToString()).ToShortDateString();
                txtSeqApontamentoRecursos.Text = dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.SEQAPO"].ToString();

                dteApontamentoConsumo.Text = Convert.ToDateTime(dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.DATAAPO"].ToString()).ToShortDateString();
                txtSeqApontamentoConsumo.Text = dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.SEQAPO"].ToString();

                dteApontamentoSubProdutos.Text = Convert.ToDateTime(dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.DATAAPO"].ToString()).ToShortDateString();
                txtSeqApontamentoSubProdutos.Text = dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.SEQAPO"].ToString();

                dteApontamentoParadas.Text = Convert.ToDateTime(dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.DATAAPO"].ToString()).ToShortDateString();
                txtSeqApontamentoParadas.Text = dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.SEQAPO"].ToString();

                HabilitaDesabilitaCamposAbaTrabalho(true);

                Global gl = new Global();
                gl.EnableTab(tabApontamento.TabPages["tabRecursos"], true);
                gl.EnableTab(tabApontamento.TabPages["tabConsumo"], true);
                gl.EnableTab(tabApontamento.TabPages["tabSubProdutos"], true);
                gl.EnableTab(tabApontamento.TabPages["tabParadas"], true);

                dteApontamentoRecursos.Enabled = false;
                txtSeqApontamentoRecursos.Enabled = false;

                dteApontamentoConsumo.Enabled = false;
                txtSeqApontamentoConsumo.Enabled = false;

                dteApontamentoSubProdutos.Enabled = false;
                txtSeqApontamentoSubProdutos.Enabled = false;

                dteApontamentoParadas.Enabled = false;
                txtSeqApontamentoParadas.Enabled = false;

                VerificaGruposPermitidos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void VerificaGruposPermitidos()
        {
            //Verifica os possiveis grupo de recursos para estar apontando
            string sqlRecursos = @"SELECT   PORDEMALOCACAO.CODGRUPORECURSO AS 'PORDEMALOCACAO.CODGRUPORECURSO'
                                FROM PORDEMALOCACAO                   JOIN PGRUPORECURSO ON  PORDEMALOCACAO.CODEMPRESA = PGRUPORECURSO.CODEMPRESA
                                                                       AND PORDEMALOCACAO.CODFILIAL = PGRUPORECURSO.CODFILIAL
                                                                       AND PORDEMALOCACAO.CODGRUPORECURSO = PGRUPORECURSO.CODGRUPORECURSO
                                WHERE PORDEMALOCACAO.CODEMPRESA = " + AppLib.Context.Empresa +
                              " AND PORDEMALOCACAO.CODFILIAL = " + AppLib.Context.Filial +
                              " AND PORDEMALOCACAO.CODESTRUTURA = '" + lookupEstrutura.ValorCodigoInterno + "' " +
                              " AND PORDEMALOCACAO.REVESTRUTURA = '" + revisaoEstrutura + "' " +
                              " AND PORDEMALOCACAO.CODIGOOP = '" + txtNroOp.Text + "' " +
                              " AND PORDEMALOCACAO.SEQOP = '" + txtSeqOp.Text + "' " +
                              " AND PORDEMALOCACAO.CODOPERACAO = '" + CODOPERACAO + "' " +
                              " AND PORDEMALOCACAO.SEQOPERACAO = '" + SEQOPERACAO + "' ";
            DataTable dtRecursos = AppLib.Context.poolConnection.Get("Start").ExecQuery(sqlRecursos);
            if (dtRecursos.Rows.Count > 0)
            {
                string recursosIN = "";
                for (int x = 0; x <= dtRecursos.Rows.Count - 1; x++)
                {
                    recursosIN = recursosIN + (x == 0 ? "" : ",") + "'" + dtRecursos.Rows[x]["PORDEMALOCACAO.CODGRUPORECURSO"].ToString() + "'";
                }
                lookupgruporecurso.Grid_WhereVisao[3].ValorFixo = @"select CODGRUPORECURSO as 'GRUPORECURSO' from PGRUPORECURSO where CODEMPRESA = " + AppLib.Context.Empresa + " and CODFILIAL = " + AppLib.Context.Filial + " AND CODGRUPORECURSO in (" + recursosIN + ")";
            }
        }
        private void gridApontamento_DoubleClick(object sender, EventArgs e)
        {
            LimpaCamposAbaRecurso();
            EditaTrabalho();
            CarregaGridRecurso();
            CarregaGridParada();
            CarregaGridConsumo();
            CarregaGridSubProduto();
            carregaCamposComplementares();
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            editaParadas = false;
            LimpaCamposAbaParada();
        }

        private void btnSalvarRecursos_Click(object sender, EventArgs e)
        {
            if (SalvarRecurso() == true)
            {
                LimpaCamposAbaRecurso();
                CarregaGridRecurso();
                editaRecursos = false;
                MessageBox.Show("Cadastro salvo com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dtTrabalho.Rows.Count > 0)
            {
                int row = gridViewApontamento.GetDataSourceRowIndex(gridViewApontamento.FocusedRowHandle);

                //Verifica se tem movimentação de estoque neste apontamento
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PORDEMCONSUMO A 
                                                                                   WHERE CODEMPRESA = ?
                                                                                     AND CODFILIAL = ?
                                                                                     AND CODIGOOP =?
                                                                                     AND SEQOP = ?
                                                                                     AND CODESTRUTURA = ?
                                                                                     AND REVESTRUTURA = ?
                                                                                     AND SEQOPERACAO = ?
                                                                                     AND CODOPERACAO = ?
                                                                                     AND TIPOCONSUMO = 'B'
                                                                                     AND SEQAPO = ?",
                new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtNroOp.Text, txtSeqOp.Text, lookupEstrutura.ValorCodigoInterno, revisaoEstrutura, SEQOPERACAO, CODOPERACAO, dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.ID"].ToString() });
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Este apontamento esta vinculado a uma baixa de matéria prima e não pode ser excluído.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                        conn.BeginTransaction();
                        try
                        {
                            conn.ExecTransaction("DELETE FROM PORDEMAPONTAMENTO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND ID = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, dtTrabalho.Rows[row]["PORDEMAPONTAMENTO.ID"].ToString() });
                            conn.Commit();
                            CarregaGridApontamento();
                            MessageBox.Show("Registro excluído com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception)
                        {
                            conn.Rollback();
                            throw;
                        }
                    }
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            LimpaCamposAbaRecurso();
            EditaTrabalho();
            CarregaGridRecurso();
            CarregaGridParada();
            CarregaGridConsumo();
            CarregaGridSubProduto();
            carregaCamposComplementares();
        }

        private void carregaCamposComplementares()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM PORDEMAPONTAMENTOCOMPL WHERE ID = ? AND CODEMPRESA = ?", new object[] { IDApontamento, AppLib.Context.Empresa });
            DataTable dtColunas = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT NOMECAMPO FROM GTABCAMPOCOMPL WHERE CODENTIDADE = ? AND ATIVO = ? ORDER BY ORDEM", new object[] { "PORDEMAPONTAMENTOCOMPL", 1 });
            if (dt.Rows.Count > 0)
            {
                Control controle;
                for (int iColunas = 0; iColunas < dtColunas.Rows.Count; iColunas++)
                {
                    for (int i = 0; i < tabCamposComplementares.Controls.Count; i++)
                    {
                        controle = tabCamposComplementares.Controls[i];
                        if (controle.GetType().Name.Equals("TextEdit") || controle.GetType().Name.Equals("ComboBox") || controle.GetType().Name.Equals("DateEdit") || controle.GetType().Name.Equals("MemoEdit"))
                        {
                            if (controle.Name.Remove(0, 4) == dtColunas.Rows[iColunas]["NOMECAMPO"].ToString())
                            {
                                controle.Text = dt.Rows[0][dtColunas.Rows[iColunas]["NOMECAMPO"].ToString()].ToString();
                            }
                        }
                        else if (controle.GetType().Name.Equals("CheckEdit"))
                        {
                            if (controle.Name.Remove(0, 4) == dtColunas.Rows[iColunas]["NOMECAMPO"].ToString())
                            {
                                if (dt.Rows[0][dtColunas.Rows[iColunas]["NOMECAMPO"].ToString()].ToString() == "1")
                                {
                                    DevExpress.XtraEditors.CheckEdit ck = (DevExpress.XtraEditors.CheckEdit)controle;
                                    ck.Checked = true;
                                }
                                else
                                {
                                    DevExpress.XtraEditors.CheckEdit ck = (DevExpress.XtraEditors.CheckEdit)controle;
                                    ck.Checked = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnSalvarTrabalho_Click_1(object sender, EventArgs e)
        {
            
        }

        private void gridRecursos_DoubleClick(object sender, EventArgs e)
        {
            //IDRecurso
            EditaRecurso();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            EditaRecurso();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dtRecursos.Rows.Count > 0)
            {
                int row = gridViewRecursos.GetDataSourceRowIndex(gridViewRecursos.FocusedRowHandle);

                if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                    conn.BeginTransaction();
                    try
                    {
                        conn.ExecTransaction("DELETE FROM PORDEMAPTORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND ID = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, dtRecursos.Rows[row]["PORDEMAPTORECURSO.ID"].ToString() });
                        conn.Commit();
                        LimpaCamposAbaRecurso();
                        CarregaGridRecurso();
                        MessageBox.Show("Registro excluído com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        conn.Rollback();
                        throw;
                    }
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            editaRecursos = false;
            LimpaCamposAbaRecurso();
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            if (dtParada.Rows.Count > 0)
            {
                int row = gridViewParada.GetDataSourceRowIndex(gridViewParada.FocusedRowHandle);

                if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                    conn.BeginTransaction();
                    try
                    {
                        conn.ExecTransaction("DELETE FROM PORDEMAPTOPARADA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND ID = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, dtParada.Rows[row]["PORDEMAPTOPARADA.ID"].ToString() });
                        conn.Commit();
                        LimpaCamposAbaParada();
                        CarregaGridParada();
                        MessageBox.Show("Registro excluído com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        conn.Rollback();
                        throw;
                    }
                }
            }
        }

        private void btnSalvarParada_Click(object sender, EventArgs e)
        {
            if (SalvarParada() == true)
            {
                LimpaCamposAbaParada();
                CarregaGridParada();
                editaParadas = false;
                MessageBox.Show("Cadastro salvo com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tspHrFimParada_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(tspHrInicioParada.Text).TimeOfDay.ToString() != "00:00:00" && Convert.ToDateTime(tspHrFimParada.Text).TimeOfDay.ToString() != "00:00:00")
            {
                if (Convert.ToDateTime(tspHrFimParada.Text) <= Convert.ToDateTime(tspHrInicioParada.Text))
                {
                    txtTempoParada.Text = "0";
                }
                else
                {
                    txtTempoParada.Text = Calculatempo(Convert.ToDateTime(tspHrInicioParada.Text), Convert.ToDateTime(tspHrFimParada.Text));
                }
            }
            else
            {
                txtTempoParada.Text = "0";
            }
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            EditaParada();
        }

        private void gridParada_DoubleClick(object sender, EventArgs e)
        {
            EditaParada();
        }

        private void btnSalvarTrabalho_Click(object sender, EventArgs e)
        {
        
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (SalvarTrabalho() == true)
            {
                LimpaAbas();
                CarregaGridApontamento();
                MessageBox.Show("Cadastro salvo com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSalvarSubProdutos_Click(object sender, EventArgs e)
        {
            if (SalvarSubProdutos() == true)
            {
                LimpaCamposAbaSubProduto();
                CarregaGridSubProduto();
                editaSubProdutos = false;
                MessageBox.Show("Cadastro salvo com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            editaSubProdutos = false;
            LimpaCamposAbaSubProduto();
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            EditaSubProduto();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {

        }

        private void gridSubproduto_DoubleClick(object sender, EventArgs e)
        {
            EditaSubProduto();
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            if (dtSubproduto.Rows.Count > 0)
            {
                int row = gridViewSubproduto.GetDataSourceRowIndex(gridViewSubproduto.FocusedRowHandle);

                if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                    conn.BeginTransaction();
                    try
                    {
                        conn.ExecTransaction("DELETE FROM PORDEMENTRADA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND ID = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, dtSubproduto.Rows[row]["PORDEMENTRADA.ID"].ToString() });
                        conn.Commit();
                        LimpaCamposAbaSubProduto();
                        CarregaGridSubProduto();
                        MessageBox.Show("Registro excluído com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        conn.Rollback();
                        throw;
                    }
                }
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            ValidaSaldoGridConsumo();
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

                        ValidaSaldoGridConsumo(AlteraReservaOP.Automatico);
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
                        ValidaSaldoGridConsumo();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnSalvarConsumo_Click(object sender, EventArgs e)
        {
            Param_UtilizaReserva = VerificaParametro("UTILIZARESERVA");
            Param_MomentoBaixa = VerificaParametro("MOMENTOBAIXA");

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PORDEMCONSUMO");
            conn.BeginTransaction();

            try
            {
                int _STATUSOP = Convert.ToInt16(conn.ExecGetField(0, @"SELECT STATUSOP
                  FROM PORDEM
                  WHERE CODEMPRESA = ?
                    AND CODFILIAL = ?
                    AND CODESTRUTURA = ?
                    AND REVESTRUTURA = ?
                    AND CODIGOOP = ?
                    AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno, revisaoEstrutura,  txtNroOp.Text, txtSeqOp.Text }));

                if (_STATUSOP != 2)
                {
                    throw new Exception("Esta ordem de produção não esta com status de planejada.");
                }

                existePORDEMCONSUMO();
                
                if (existeNecessidade == true || existeBaixa == true || existeReserva == false)
                {
                    string inconsistencias = "";

                    int CODOPER = 0;
                    int NSEQITEM = 1;
                    int CODOPER_Compra = 0;
                    int NSEQITEM_Compra = 1;

                    for (int i = 0; i <= dtNecessidade.Rows.Count - 1; i++)
                    {
                        //Verifica CODOPER e NSEQITEM se = null
                        string vCODOPER = AppLib.Context.poolConnection.Get("Start").ExecGetField("", "select CODOPER from PORDEMCONSUMO where ID = ?", new object[] { dtNecessidade.Rows[i]["PORDEMCONSUMO.ID"] }).ToString();

                        if (string.IsNullOrEmpty(vCODOPER.ToString()))
                        {
                            if (CODOPER == 0)
                            {
                                CODOPER = IncluirGOPER(conn, TipoGOPER.Baixa);
                            }

                            if (CODOPER <= 0)
                            {
                                throw new Exception("Erro ao incluir operação");
                            }
                            else
                            {
                                if (IncluirGOPERITEM(CODOPER_Compra, conn, dtNecessidade.Rows[i]) == true)
                                {
                                    //Método para alteração do registro de BAIXA na tabela PORDEMCONSUMO, incluindo os campos CODOPER, NSEQITEM e SEQAPO
                                    alteraPORDEMCONSUMO(conn, dtNecessidade.Rows[i], TipoConsumo.Baixa, CODOPER, NSEQITEM,Convert.ToInt16(txtSeqApontamentoConsumo.Text));
                                    //Verifica se existe solicitação de compra para este item 
                                    if (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"]) > 0)
                                    {
                                        if (IncluiSolicitacaoCompra(conn, i, ref CODOPER_Compra, ref NSEQITEM_Compra, dtNecessidade) == false)
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
                        else
                        {
                            inconsistencias = inconsistencias + "O produto '" + dtNecessidade.Rows[i]["PORDEMCONSUMO.CODCOMPONENTE"].ToString() + "' já foi baixado e não pode ser alterado" + System.Environment.NewLine;
                            continue;
                        }
                    }

                    if (!string.IsNullOrEmpty(inconsistencias))
                    {
                        MessageBox.Show(inconsistencias, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (existeNecessidade == true || existeBaixa == false || existeReserva == true)
                {
                    int CODOPER_Compra = 0;
                    int NSEQITEM_Compra = 1;

                    if (Param_UtilizaReserva == "1")
                    {
                        if (Param_MomentoBaixa != "1") //2=APONTAR ULTIMA OPERAÇÃO / 3=ANTES DE DAR ENTRADA NO ESTOQUE
                        {
                            //Ao gravar Baixas deixar CODOPER e NSEQITEM = null, pode alterar qtde, tirar e incluir item
                            for (int i = 0; i <= dtNecessidade.Rows.Count - 1; i++)
                            {
                                //Método para inclusão do registro de RESERVA na tabela PORDEMCONSUMO
                                IncluirPORDEMCONSUMO(conn, dtNecessidade.Rows[i], TipoConsumo.Baixa, 0, 0, Convert.ToInt16(txtSeqApontamentoConsumo.Text));

                                //Verifica se existe solicitação de compra para este item 
                                if (Convert.ToDecimal(dtNecessidade.Rows[i]["QTDSOLICITACAOCOMPRAS"]) > 0)
                                {
                                    if (IncluiSolicitacaoCompra(conn, i, ref CODOPER_Compra, ref NSEQITEM_Compra, dtNecessidade) == false)
                                    {
                                        throw new Exception("Erro ao incluir operação: COMPRA");
                                    }
                                }
                            }
                        }
                        else //1 = PLANEJAR ORDEM DE PRODUÇÃO, BAIXAR MATÉRIA PRIMA
                        {
                            //Não Fazer nada pois os produtos já foram baixados ao planejar a OP
                        }
                    }
                    else if (Param_UtilizaReserva == "2")
                    {
                        //Parametro alterado depois de planejado a OP, conforme orientação do Marcelo, Ignorar qualquer tipo de processamento.
                    }
                    else
                    {
                        throw new Exception("Parâmetro Inválido: UTILIZARESERVA");
                    }
                }

                conn.Commit();
                MessageBox.Show("Processo finalizado com sucesso", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                conn.Rollback();
                MessageBox.Show(ex.Message.ToString(), "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool todasOperacoesApontadas()
        {
            try
            {
                bool verifica = true;

                string sql = @"SELECT * FROM PORDEMROTEIRO A WITH (NOLOCK) LEFT JOIN PORDEMAPONTAMENTO B WITH (NOLOCK) ON A.CODEMPRESA = B.CODEMPRESA
                                                      AND A.CODFILIAL = B.CODFILIAL
                                                      AND A.CODESTRUTURA = B.CODESTRUTURA
                                                      AND A.REVESTRUTURA = B.REVESTRUTURA
                                                      AND A.CODIGOOP = B.CODIGOOP
                                                      AND A.SEQOP = B.SEQOP
               WHERE A.CODEMPRESA = ?
                 AND A.CODFILIAL = ?
                 AND A.CODESTRUTURA = ?
                 AND A.REVESTRUTURA = ?
                 AND A.CODIGOOP = ?
                 AND A.SEQOP = ?
                 AND B.SEQOPERACAO IS NULL";

                DataTable dtExiste = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno, revisaoEstrutura, txtNroOp.Text, txtSeqOp.Text });

                if (dtExiste.Rows.Count > 0)
                {
                    verifica = false;
                }

                return verifica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        private void ConcluirOP(AppLib.Data.Connection conn)
        {
            try
            {
                conn.ExecTransaction("UPDATE PORDEM SET STATUSOP = 6 WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODIGOOP = ? AND SEQOP = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupEstrutura.ValorCodigoInterno , revisaoEstrutura, txtNroOp.Text, txtSeqOp.Text});
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnConcluir_Click(object sender, EventArgs e)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PORDEM");
            conn.BeginTransaction();

            try
            {
                if (MessageBox.Show("Deseja encerrar a ordem de produção?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (todasOperacoesApontadas() == true)
                    {
                        ConcluirOP(conn);
                        conn.Commit();
                    }
                    else
                    {
                        MessageBox.Show("Esta ordem de produção possui operações não apontadas.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                        
                }

                MessageBox.Show("Ordem de produção concluída com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            catch (Exception ex)
            {
                conn.Rollback();
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvarApontamentoComplementar_Click(object sender, EventArgs e)
        {
            if (SalvarTrabalho() == true)
            {
                LimpaAbas();
                CarregaGridApontamento();
                MessageBox.Show("Cadastro salvo com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tabApontamento_Click(object sender, EventArgs e)
        {
            if (tabApontamento.SelectedTab == tabCamposComplementares)
            {
                btnSalvarApontamentoComplementar.Visible = true;
                btnSalvarApontamentoComplementar.Location = new Point(1035, 20);
            }
            else
            {
                btnSalvarApontamentoComplementar.Visible = false;
            }
        }

        private void txtCodigoBarras_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtCodigoBarras_Leave(object sender, EventArgs e)
        {
            //string mask = "###;##;######/##;###";
            //txtCodigoBarras.Text = mascara(txtCodigoBarras.Text, mask);

            //string Mask = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT MASCCODBARRA FROM PPARAM", new object[] { }).ToString();

            //txtCodigoBarras.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            //txtCodigoBarras.Properties.Mask.EditMask = Mask;
        }

        public string mascara(string valor, string mascara)
        {
            string saida = "";

            string caracter_mascara = "";
            for (int i = 0; i < mascara.Length; i++)
            {
                caracter_mascara = mascara[i].ToString();
                if (caracter_mascara == ";" || caracter_mascara == "/" || caracter_mascara == "-" || caracter_mascara == ".")
                    valor = valor.Insert(i, caracter_mascara);

            }
            return valor;

        }
    }
}