using System;
using System.Text;
using System.Data;


namespace PS.Glb.Class
{
   public class gTipOper
    {
        public gTipOper() { }

        #region Propriedades

        public int CODEMPRESA { get; set; }
        public string CODTIPOPER { get; set; }
        public string DESCRICAO { get; set; }
        public int USANUMEROSEQ { get; set; }
        public string ULTIMONUMERO { get; set; }
        public int USAABATRANSP { get; set; }
        public int USAABAOBSERV { get; set; }
        public int USAABATRIBUTOS { get; set; }
        public int GERAFINANCEIRO { get; set; }
        public int GERATRIBUTOS { get; set; }
        public int VLUNITARIOEDITA { get; set; }
        public string CODTIPDOC { get; set; }
        public string CODMOEDA { get; set; }
        public string OPERESTOQUE { get; set; }
        public string OPERSERIE { get; set; }
        public string SERIEDEFAULT { get; set; }
        public string LOCAL1 { get; set; }
        public string LOCAL2 { get; set; }
        public string USACAMPOOBJETO { get; set; }
        public string USACAMPOCONDPGTO { get; set; }
        public string CODCONDICAOPADRAO { get; set; }
        public string USACAMPOOPERADOR { get; set; }
        public string CODOPERADORPADRAO { get; set; }
        public string CODCONTA { get; set; }
        public int ULTIMONIVEL { get; set; }
        public string TIPENTSAI { get; set; }
        public string CODCLIFOR { get; set; }
        public string CODCLIFORPADRAO { get; set; }
        public string CODFORMA { get; set; }
        public string CODFORMAPADRAO { get; set; }
        public string USACONTA { get; set; }
        public string CODCONTAPADRAO { get; set; }
        public string USANATUREZA { get; set; }
        public string USAOPERACAONFE { get; set; }
        public string USACODREPRE { get; set; }
        public int TIPOPAGREC { get; set; }
        public int CRIAROPFAT { get; set; }
        public int SERIELIVRE { get; set; }
        public int CLIFORAMB { get; set; }
        public string USAVALORBRUTO { get; set; }
        public string USAVALORLIQUIDO { get; set; }
        public int PRODSERV { get; set; }
        public string USAVLUNITARIO { get; set; }
        public string USAPRDESCONTO { get; set; }
        public string USAVLDESCONTO { get; set; }
        public string USAVLTOTALITEM { get; set; }
        public string USADATAEMISSAO { get; set; }
        public string USADATAENTREGA { get; set; }
        public string USADATAENTSAI { get; set; }
        public string CODFORMULAVALORBRUTO { get; set; }
        public string CODFORMULAVALORLIQUIDO { get; set; }
        public string USACAMPOLIVRE1 { get; set; }
        public string USACAMPOLIVRE2 { get; set; }
        public string USACAMPOLIVRE3 { get; set; }
        public string USACAMPOLIVRE4 { get; set; }
        public string USACAMPOLIVRE5 { get; set; }
        public string USACAMPOLIVRE6 { get; set; }
        public string TEXTOCAMPOLIVRE1 { get; set; }
        public string TEXTOCAMPOLIVRE2 { get; set; }
        public string TEXTOCAMPOLIVRE3 { get; set; }
        public string TEXTOCAMPOLIVRE4 { get; set; }
        public string TEXTOCAMPOLIVRE5 { get; set; }
        public string TEXTOCAMPOLIVRE6 { get; set; }
        public string USADATAEXTRA1 { get; set; }
        public string USADATAEXTRA2 { get; set; }
        public string USADATAEXTRA3 { get; set; }
        public string USADATAEXTRA4 { get; set; }
        public string USADATAEXTRA5 { get; set; }
        public string USADATAEXTRA6 { get; set; }
        public string TEXTODATAEXTRA1 { get; set; }
        public string TEXTODATAEXTRA2 { get; set; }
        public string TEXTODATAEXTRA3 { get; set; }
        public string TEXTODATAEXTRA4 { get; set; }
        public string TEXTODATAEXTRA5 { get; set; }
        public string TEXTODATAEXTRA6 { get; set; }
        public string USACAMPOVALOR1 { get; set; }
        public string USACAMPOVALOR2 { get; set; }
        public string USACAMPOVALOR3 { get; set; }
        public string USACAMPOVALOR4 { get; set; }
        public string USACAMPOVALOR5 { get; set; }
        public string USACAMPOVALOR6 { get; set; }
        public string TEXTOCAMPOVALOR1 { get; set; }
        public string TEXTOCAMPOVALOR2 { get; set; }
        public string TEXTOCAMPOVALOR3 { get; set; }
        public string TEXTOCAMPOVALOR4 { get; set; }
        public string TEXTOCAMPOVALOR5 { get; set; }
        public string TEXTOCAMPOVALOR6 { get; set; }
        public string USAITEMCAMPOLIVRE1 { get; set; }
        public string USAITEMCAMPOLIVRE2 { get; set; }
        public string USAITEMCAMPOLIVRE3 { get; set; }
        public string USAITEMCAMPOLIVRE4 { get; set; }
        public string USAITEMCAMPOLIVRE5 { get; set; }
        public string USAITEMCAMPOLIVRE6 { get; set; }
        public string TEXTOITEMCAMPOLIVRE1 { get; set; }
        public string TEXTOITEMCAMPOLIVRE2 { get; set; }
        public string TEXTOITEMCAMPOLIVRE3 { get; set; }
        public string TEXTOITEMCAMPOLIVRE4 { get; set; }
        public string TEXTOITEMCAMPOLIVRE5 { get; set; }
        public string TEXTOITEMCAMPOLIVRE6 { get; set; }
        public string USAITEMDATAEXTRA1 { get; set; }
        public string USAITEMDATAEXTRA2 { get; set; }
        public string USAITEMDATAEXTRA3 { get; set; }
        public string USAITEMDATAEXTRA4 { get; set; }
        public string USAITEMDATAEXTRA5 { get; set; }
        public string USAITEMDATAEXTRA6 { get; set; }
        public string TEXTOITEMDATAEXTRA1 { get; set; }
        public string TEXTOITEMDATAEXTRA2 { get; set; }
        public string TEXTOITEMDATAEXTRA3 { get; set; }
        public string TEXTOITEMDATAEXTRA4 { get; set; }
        public string TEXTOITEMDATAEXTRA5 { get; set; }
        public string TEXTOITEMDATAEXTRA6 { get; set; }
        public string USAITEMCAMPOVALOR1 { get; set; }
        public string USAITEMCAMPOVALOR2 { get; set; }
        public string USAITEMCAMPOVALOR3 { get; set; }
        public string USAITEMCAMPOVALOR4 { get; set; }
        public string USAITEMCAMPOVALOR5 { get; set; }
        public string USAITEMCAMPOVALOR6 { get; set; }
        public string TEXTOITEMCAMPOVALOR1 { get; set; }
        public string TEXTOITEMCAMPOVALOR2 { get; set; }
        public string TEXTOITEMCAMPOVALOR3 { get; set; }
        public string TEXTOITEMCAMPOVALOR4 { get; set; }
        public string TEXTOITEMCAMPOVALOR5 { get; set; }
        public string TEXTOITEMCAMPOVALOR6 { get; set; }
        public string VLUNITARIOEM { get; set; }
        public int USARATEIOCC { get; set; }
        public int USARATEIODP { get; set; }
        public int USARATEIOCCITEM { get; set; }
        public int USARATEIODPITEM { get; set; }
        public int COPIACAMPOLIVRE1 { get; set; }
        public int COPIACAMPOLIVRE2 { get; set; }
        public int COPIACAMPOLIVRE3 { get; set; }
        public int COPIACAMPOLIVRE4 { get; set; }
        public int COPIACAMPOLIVRE5 { get; set; }
        public int COPIACAMPOLIVRE6 { get; set; }
        public int COPIADATAEXTRA1 { get; set; }
        public int COPIADATAEXTRA2 { get; set; }
        public int COPIADATAEXTRA3 { get; set; }
        public int COPIADATAEXTRA4 { get; set; }
        public int COPIADATAEXTRA5 { get; set; }
        public int COPIADATAEXTRA6 { get; set; }
        public int COPIACAMPOVALOR1 { get; set; }
        public int COPIACAMPOVALOR2 { get; set; }
        public int COPIACAMPOVALOR3 { get; set; }
        public int COPIACAMPOVALOR4 { get; set; }
        public int COPIACAMPOVALOR5 { get; set; }
        public int COPIACAMPOVALOR6 { get; set; }
        public int COPIAITEMCAMPOLIVRE1 { get; set; }
        public int COPIAITEMCAMPOLIVRE2 { get; set; }
        public int COPIAITEMCAMPOLIVRE3 { get; set; }
        public int COPIAITEMCAMPOLIVRE4 { get; set; }
        public int COPIAITEMCAMPOLIVRE5 { get; set; }
        public int COPIAITEMCAMPOLIVRE6 { get; set; }
        public int COPIAITEMDATAEXTRA1 { get; set; }
        public int COPIAITEMDATAEXTRA2 { get; set; }
        public int COPIAITEMDATAEXTRA3 { get; set; }
        public int COPIAITEMDATAEXTRA4 { get; set; }
        public int COPIAITEMDATAEXTRA5 { get; set; }
        public int COPIAITEMDATAEXTRA6 { get; set; }
        public int COPIAITEMCAMPOVALOR1 { get; set; }
        public int COPIAITEMCAMPOVALOR2 { get; set; }
        public int COPIAITEMCAMPOVALOR3 { get; set; }
        public int COPIAITEMCAMPOVALOR4 { get; set; }
        public int COPIAITEMCAMPOVALOR5 { get; set; }
        public int COPIAITEMCAMPOVALOR6 { get; set; }
        public int TIPOPER { get; set; }
        public int UNDMEDPADRAO { get; set; }
        public string CODFORMULAVALIDAOPERACAO { get; set; }
        public string CODFORMULAVALIDAOPERITEM { get; set; }
        public int GERAORDEMPRODUCAO { get; set; }
        public int CODFILIALDEFUALT { get; set; }
        public string LOCAL1DEFAULT { get; set; }
        public string CODNATDENTRO { get; set; }
        public string CODNATFORA { get; set; }
        public int TIPOIMPRESSAODANFE { get; set; }
        public string MODDOCFISCAL { get; set; }
        public string TEXTOPRODNFE { get; set; }
        public int FINEMISSAONFE { get; set; }
        public string USAVALORFRETE { get; set; }
        public string USAVALORDESCONTO { get; set; }
        public string USAVALORDESPESA { get; set; }
        public string USAVALORSEGURO { get; set; }
        public string USACODNATUREZAORCAMENTO { get; set; }
        public string USACODCCUSTO { get; set; }
        public int MASKNUMEROSEQ { get; set; }
        public string CODREPREPADRAO { get; set; }
        public string VERIFICAVENCIMENTO { get; set; }
        public int QTDDIASVENCIDOS { get; set; }
        public int IDGRUPOREL { get; set; }
        public bool VERIFICAFATURAMENTOPARCIAL { get; set; }
        public bool PERMITEFATURAMENTOPARCIAL { get; set; }
        public string CODIGOPRODUTO { get; set; }
        public int AUTOSELECAONATUREZA { get; set; }
        public string USACODVENDEDOR { get; set; }
        public string CODVENDEDORPADRAO { get; set; }
        public string USAORDEMDECOMPRA { get; set; }
        public string USATABELAPRECO { get; set; }
        public string USAPRACRESCIMO { get; set; }
        public string USAVLACRESCIMO { get; set; }
        public string OPERESTOQUE2 { get; set; }
        public int CODFILIAL2DEFAULT { get; set; }
        public string LOCAL2DEFAULT { get; set; }
        public string USABLOQUEIODESCONTO { get; set; }
        public string DEFAULTBLOQUEIODESC { get; set; }
        public string FRMCUSTOUNITARIO { get; set; }
        public string DEFAULTMARCA { get; set; }
        public string DEFAULTESPECIE { get; set; }
        public string USADATAENTREGAITEM { get; set; }
        public string USASELECAOBLOQUEIODESCONTO { get; set; }
        public string USACAMPONFE { get; set; }
        public string USADESCRICAOCOMPLEMENTAR { get; set; }
        public string TIPODESCONTOITEM { get; set; }
        public string CODMENSAGEMIBPTAX { get; set; }
        public bool USASEGUNDONUMERO { get; set; }
        public string BASEVENCIMENTO { get; set; }
        public string CODQUERYNATUREZA { get; set; }
        public bool USACODAUXNFE { get; set; }
        public string BUSCAPRODUTOPOR { get; set; }
        public string ACEITAPRECOZERO { get; set; }
        public string USALIMITECREDITO { get; set; }
        public bool UTILIZACOPIAREFERENCIA { get; set; }
        public string UTILIZAPRODCOPMOSTO { get; set; }
        public string PERMITEEXPANDIRITENS { get; set; }
        public string MANTEMPRODUTOPRINCEXPANSAO { get; set; }
        public bool USABAIXADEPRODUCAO { get; set; }
        public string CODTIPOPERENTRADA { get; set; }
        public string CODTIPOPERBAIXA { get; set; }
        public int CLASSIFICACAOCLIFOR { get; set; }
        public bool USANFEIMPORTACAO { get; set; }
        public int DECIMALVLUNITARIO { get; set; }
        public vTipOperSerie VTIPOPERSERIE { get; set; }

        #endregion

        #region Busca a tabela GTIPOPER

        /// <summary>
        /// Método para buscar os parametros da operação.
        /// </summary>
        /// <param name="codTipOper">Código do Tipo da Operação</param>
        /// <returns>Classe populada gTipOper</returns>
        public gTipOper getgTipOper(string codTipOper)
        {
            gTipOper tipOper = new gTipOper();
            try
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM GTIPOPER WHERE CODTIPOPER = ? AND CODEMPRESA = ? ", new object[] {codTipOper, AppLib.Context.Empresa });

                if (dt.Rows.Count > 0)
                {
                    tipOper.CODEMPRESA = Convert.ToInt32(dt.Rows[0]["CODEMPRESA"]);
                    tipOper.CODTIPOPER = dt.Rows[0]["CODTIPOPER"].ToString();
                    tipOper.DESCRICAO = dt.Rows[0]["DESCRICAO"].ToString();
                    tipOper.USANUMEROSEQ = Convert.ToInt32(dt.Rows[0]["USANUMEROSEQ"]);
                    tipOper.ULTIMONUMERO = dt.Rows[0]["ULTIMONUMERO"].ToString();
                    tipOper.USAABATRANSP = Convert.ToInt32(dt.Rows[0]["USAABATRANSP"]);
                    tipOper.USAABAOBSERV = Convert.ToInt32(dt.Rows[0]["USAABAOBSERV"]);
                    tipOper.USAABATRIBUTOS = Convert.ToInt32(dt.Rows[0]["USAABATRIBUTOS"]);
                    tipOper.GERAFINANCEIRO = Convert.ToInt32(dt.Rows[0]["GERAFINANCEIRO"]);
                    tipOper.GERATRIBUTOS = Convert.ToInt32(dt.Rows[0]["GERATRIBUTOS"]);
                    tipOper.VLUNITARIOEDITA = Convert.ToInt32(dt.Rows[0]["VLUNITARIOEDITA"]);
                    tipOper.CODTIPDOC = dt.Rows[0]["CODTIPDOC"].ToString();
                    tipOper.CODMOEDA = dt.Rows[0]["CODMOEDA"].ToString();
                    tipOper.OPERESTOQUE = dt.Rows[0]["OPERESTOQUE"].ToString();
                    tipOper.OPERSERIE = dt.Rows[0]["OPERSERIE"].ToString();
                    tipOper.SERIEDEFAULT = dt.Rows[0]["SERIEDEFAULT"].ToString();
                    tipOper.LOCAL1 = dt.Rows[0]["LOCAL1"].ToString();
                    tipOper.LOCAL2 = dt.Rows[0]["LOCAL2"].ToString();
                    tipOper.USACAMPOOBJETO = dt.Rows[0]["USACAMPOOBJETO"].ToString();
                    tipOper.USACAMPOCONDPGTO = dt.Rows[0]["USACAMPOCONDPGTO"].ToString();
                    tipOper.CODCONDICAOPADRAO = dt.Rows[0]["CODCONDICAOPADRAO"].ToString();
                    tipOper.USACAMPOOPERADOR = dt.Rows[0]["USACAMPOOPERADOR"].ToString();
                    tipOper.CODOPERADORPADRAO = dt.Rows[0]["CODOPERADORPADRAO"].ToString();
                    tipOper.CODCONTA = dt.Rows[0]["CODCONTA"].ToString();
                    tipOper.ULTIMONIVEL = Convert.ToInt32(dt.Rows[0]["ULTIMONIVEL"]);
                    tipOper.TIPENTSAI = dt.Rows[0]["TIPENTSAI"].ToString();
                    tipOper.CODCLIFOR = dt.Rows[0]["CODCLIFOR"].ToString();
                    tipOper.CODCLIFORPADRAO = dt.Rows[0]["CODCLIFORPADRAO"].ToString();
                    tipOper.CODFORMA = dt.Rows[0]["CODFORMA"].ToString();
                    tipOper.CODFORMAPADRAO = dt.Rows[0]["CODFORMAPADRAO"].ToString();
                    tipOper.USACONTA = dt.Rows[0]["USACONTA"].ToString();
                    tipOper.CODCONTAPADRAO = dt.Rows[0]["CODCONTAPADRAO"].ToString();
                    tipOper.USANATUREZA = dt.Rows[0]["USANATUREZA"].ToString();
                    tipOper.USAOPERACAONFE = dt.Rows[0]["USAOPERACAONFE"].ToString();
                    tipOper.USACODREPRE = dt.Rows[0]["USAOPERACAONFE"].ToString();
                    tipOper.TIPOPAGREC = Convert.ToInt32(dt.Rows[0]["TIPOPAGREC"]);
                    tipOper.CRIAROPFAT = Convert.ToInt32(dt.Rows[0]["CRIAROPFAT"]);
                    tipOper.SERIELIVRE = Convert.ToInt32(dt.Rows[0]["SERIELIVRE"]);
                    tipOper.CLIFORAMB = Convert.ToInt32(dt.Rows[0]["CLIFORAMB"]);
                    tipOper.USAVALORBRUTO = dt.Rows[0]["USAVALORBRUTO"].ToString();
                    tipOper.USAVALORLIQUIDO = dt.Rows[0]["USAVALORLIQUIDO"].ToString();
                    tipOper.PRODSERV = Convert.ToInt32(dt.Rows[0]["PRODSERV"]);
                    tipOper.USAVLUNITARIO = dt.Rows[0]["USAVLUNITARIO"].ToString();
                    tipOper.USAPRDESCONTO = dt.Rows[0]["USAPRDESCONTO"].ToString();
                    tipOper.USAVLDESCONTO = dt.Rows[0]["USAVLDESCONTO"].ToString();
                    tipOper.USAVLTOTALITEM = dt.Rows[0]["USAVLTOTALITEM"].ToString();
                    tipOper.USADATAEMISSAO = dt.Rows[0]["USADATAEMISSAO"].ToString();
                    tipOper.USADATAENTREGA = dt.Rows[0]["USADATAENTREGA"].ToString();
                    tipOper.USADATAENTSAI = dt.Rows[0]["USADATAENTSAI"].ToString();
                    tipOper.CODFORMULAVALORBRUTO = dt.Rows[0]["CODFORMULAVALORBRUTO"].ToString();
                    tipOper.CODFORMULAVALORLIQUIDO = dt.Rows[0]["CODFORMULAVALORLIQUIDO"].ToString();
                    tipOper.USACAMPOLIVRE1 = dt.Rows[0]["USACAMPOLIVRE1"].ToString();
                    tipOper.USACAMPOLIVRE2 = dt.Rows[0]["USACAMPOLIVRE2"].ToString();
                    tipOper.USACAMPOLIVRE3 = dt.Rows[0]["USACAMPOLIVRE3"].ToString();
                    tipOper.USACAMPOLIVRE4 = dt.Rows[0]["USACAMPOLIVRE4"].ToString();
                    tipOper.USACAMPOLIVRE5 = dt.Rows[0]["USACAMPOLIVRE5"].ToString();
                    tipOper.USACAMPOLIVRE6 = dt.Rows[0]["USACAMPOLIVRE6"].ToString();
                    tipOper.TEXTOCAMPOLIVRE1 = dt.Rows[0]["TEXTOCAMPOLIVRE1"].ToString();
                    tipOper.TEXTOCAMPOLIVRE2 = dt.Rows[0]["TEXTOCAMPOLIVRE2"].ToString();
                    tipOper.TEXTOCAMPOLIVRE3 = dt.Rows[0]["TEXTOCAMPOLIVRE2"].ToString();
                    tipOper.TEXTOCAMPOLIVRE4 = dt.Rows[0]["TEXTOCAMPOLIVRE4"].ToString();
                    tipOper.TEXTOCAMPOLIVRE5 = dt.Rows[0]["TEXTOCAMPOLIVRE5"].ToString();
                    tipOper.TEXTOCAMPOLIVRE6 = dt.Rows[0]["TEXTOCAMPOLIVRE6"].ToString();
                    tipOper.USADATAEXTRA1 = dt.Rows[0]["USADATAEXTRA1"].ToString();
                    tipOper.USADATAEXTRA2 = dt.Rows[0]["USADATAEXTRA2"].ToString();
                    tipOper.USADATAEXTRA3 = dt.Rows[0]["USADATAEXTRA3"].ToString();
                    tipOper.USADATAEXTRA4 = dt.Rows[0]["USADATAEXTRA4"].ToString();
                    tipOper.USADATAEXTRA5 = dt.Rows[0]["USADATAEXTRA5"].ToString();
                    tipOper.USADATAEXTRA6 = dt.Rows[0]["USADATAEXTRA6"].ToString();
                    tipOper.TEXTODATAEXTRA1 = dt.Rows[0]["TEXTODATAEXTRA1"].ToString();
                    tipOper.TEXTODATAEXTRA2 = dt.Rows[0]["TEXTODATAEXTRA2"].ToString();
                    tipOper.TEXTODATAEXTRA3 = dt.Rows[0]["TEXTODATAEXTRA3"].ToString();
                    tipOper.TEXTODATAEXTRA4 = dt.Rows[0]["TEXTODATAEXTRA4"].ToString();
                    tipOper.TEXTODATAEXTRA5 = dt.Rows[0]["TEXTODATAEXTRA5"].ToString();
                    tipOper.TEXTODATAEXTRA6 = dt.Rows[0]["TEXTODATAEXTRA6"].ToString();
                    tipOper.USACAMPOVALOR1 = dt.Rows[0]["USACAMPOVALOR1"].ToString();
                    tipOper.USACAMPOVALOR2 = dt.Rows[0]["USACAMPOVALOR2"].ToString();
                    tipOper.USACAMPOVALOR3 = dt.Rows[0]["USACAMPOVALOR3"].ToString();
                    tipOper.USACAMPOVALOR4 = dt.Rows[0]["USACAMPOVALOR4"].ToString();
                    tipOper.USACAMPOVALOR5 = dt.Rows[0]["USACAMPOVALOR5"].ToString();
                    tipOper.USACAMPOVALOR6 = dt.Rows[0]["USACAMPOVALOR6"].ToString();
                    tipOper.TEXTOCAMPOVALOR1 = dt.Rows[0]["TEXTOCAMPOVALOR1"].ToString();
                    tipOper.TEXTOCAMPOVALOR2 = dt.Rows[0]["TEXTOCAMPOVALOR2"].ToString();
                    tipOper.TEXTOCAMPOVALOR3 = dt.Rows[0]["TEXTOCAMPOVALOR3"].ToString();
                    tipOper.TEXTOCAMPOVALOR4 = dt.Rows[0]["TEXTOCAMPOVALOR4"].ToString();
                    tipOper.TEXTOCAMPOVALOR5 = dt.Rows[0]["TEXTOCAMPOVALOR5"].ToString();
                    tipOper.TEXTOCAMPOVALOR6 = dt.Rows[0]["TEXTOCAMPOVALOR6"].ToString();
                    tipOper.USAITEMCAMPOLIVRE1 = dt.Rows[0]["USAITEMCAMPOLIVRE1"].ToString();
                    tipOper.USAITEMCAMPOLIVRE2 = dt.Rows[0]["USAITEMCAMPOLIVRE2"].ToString();
                    tipOper.USAITEMCAMPOLIVRE3 = dt.Rows[0]["USAITEMCAMPOLIVRE3"].ToString();
                    tipOper.USAITEMCAMPOLIVRE4 = dt.Rows[0]["USAITEMCAMPOLIVRE4"].ToString();
                    tipOper.USAITEMCAMPOLIVRE5 = dt.Rows[0]["USAITEMCAMPOLIVRE5"].ToString();
                    tipOper.USAITEMCAMPOLIVRE6 = dt.Rows[0]["USAITEMCAMPOLIVRE6"].ToString();
                    tipOper.TEXTOITEMCAMPOLIVRE1 = dt.Rows[0]["TEXTOITEMCAMPOLIVRE1"].ToString();
                    tipOper.TEXTOITEMCAMPOLIVRE2 = dt.Rows[0]["TEXTOITEMCAMPOLIVRE2"].ToString();
                    tipOper.TEXTOITEMCAMPOLIVRE3 = dt.Rows[0]["TEXTOITEMCAMPOLIVRE3"].ToString();
                    tipOper.TEXTOITEMCAMPOLIVRE4 = dt.Rows[0]["TEXTOITEMCAMPOLIVRE4"].ToString();
                    tipOper.TEXTOITEMCAMPOLIVRE5 = dt.Rows[0]["TEXTOITEMCAMPOLIVRE5"].ToString();
                    tipOper.TEXTOITEMCAMPOLIVRE6 = dt.Rows[0]["TEXTOITEMCAMPOLIVRE6"].ToString();
                    tipOper.USAITEMDATAEXTRA1 = dt.Rows[0]["USAITEMDATAEXTRA1"].ToString();
                    tipOper.USAITEMDATAEXTRA2 = dt.Rows[0]["USAITEMDATAEXTRA2"].ToString();
                    tipOper.USAITEMDATAEXTRA3 = dt.Rows[0]["USAITEMDATAEXTRA3"].ToString();
                    tipOper.USAITEMDATAEXTRA4 = dt.Rows[0]["USAITEMDATAEXTRA4"].ToString();
                    tipOper.USAITEMDATAEXTRA5 = dt.Rows[0]["USAITEMDATAEXTRA5"].ToString();
                    tipOper.USAITEMDATAEXTRA6 = dt.Rows[0]["USAITEMDATAEXTRA6"].ToString();
                    tipOper.TEXTOITEMDATAEXTRA1 = dt.Rows[0]["TEXTOITEMDATAEXTRA1"].ToString();
                    tipOper.TEXTOITEMDATAEXTRA2 = dt.Rows[0]["TEXTOITEMDATAEXTRA2"].ToString();
                    tipOper.TEXTOITEMDATAEXTRA3 = dt.Rows[0]["TEXTOITEMDATAEXTRA3"].ToString();
                    tipOper.TEXTOITEMDATAEXTRA4 = dt.Rows[0]["TEXTOITEMDATAEXTRA4"].ToString();
                    tipOper.TEXTOITEMDATAEXTRA5 = dt.Rows[0]["TEXTOITEMDATAEXTRA5"].ToString();
                    tipOper.TEXTOITEMDATAEXTRA6 = dt.Rows[0]["TEXTOITEMDATAEXTRA6"].ToString();
                    tipOper.USAITEMCAMPOVALOR1 = dt.Rows[0]["USAITEMCAMPOVALOR1"].ToString();
                    tipOper.USAITEMCAMPOVALOR2 = dt.Rows[0]["USAITEMCAMPOVALOR2"].ToString();
                    tipOper.USAITEMCAMPOVALOR3 = dt.Rows[0]["USAITEMCAMPOVALOR3"].ToString();
                    tipOper.USAITEMCAMPOVALOR4 = dt.Rows[0]["USAITEMCAMPOVALOR4"].ToString();
                    tipOper.USAITEMCAMPOVALOR5 = dt.Rows[0]["USAITEMCAMPOVALOR5"].ToString();
                    tipOper.USAITEMCAMPOVALOR6 = dt.Rows[0]["USAITEMCAMPOVALOR6"].ToString();
                    tipOper.TEXTOITEMCAMPOVALOR1 = dt.Rows[0]["TEXTOITEMCAMPOVALOR1"].ToString();
                    tipOper.TEXTOITEMCAMPOVALOR2 = dt.Rows[0]["TEXTOITEMCAMPOVALOR2"].ToString();
                    tipOper.TEXTOITEMCAMPOVALOR3 = dt.Rows[0]["TEXTOITEMCAMPOVALOR3"].ToString();
                    tipOper.TEXTOITEMCAMPOVALOR4 = dt.Rows[0]["TEXTOITEMCAMPOVALOR4"].ToString();
                    tipOper.TEXTOITEMCAMPOVALOR5 = dt.Rows[0]["TEXTOITEMCAMPOVALOR5"].ToString();
                    tipOper.TEXTOITEMCAMPOVALOR6 = dt.Rows[0]["TEXTOITEMCAMPOVALOR6"].ToString();
                    tipOper.VLUNITARIOEM = dt.Rows[0]["VLUNITARIOEM"].ToString();
                    tipOper.USARATEIOCC = Convert.ToInt32(dt.Rows[0]["USARATEIOCC"]);
                    tipOper.USARATEIODP = Convert.ToInt32(dt.Rows[0]["USARATEIODP"]);
                    tipOper.USARATEIOCCITEM = Convert.ToInt32(dt.Rows[0]["USARATEIOCCITEM"]);
                    tipOper.USARATEIODPITEM = Convert.ToInt32(dt.Rows[0]["USARATEIODPITEM"]);
                    tipOper.COPIACAMPOLIVRE1 = Convert.ToInt32(dt.Rows[0]["COPIACAMPOLIVRE1"]);
                    tipOper.COPIACAMPOLIVRE2 = Convert.ToInt32(dt.Rows[0]["COPIACAMPOLIVRE2"]);
                    tipOper.COPIACAMPOLIVRE3 = Convert.ToInt32(dt.Rows[0]["COPIACAMPOLIVRE3"]);
                    tipOper.COPIACAMPOLIVRE4 = Convert.ToInt32(dt.Rows[0]["COPIACAMPOLIVRE4"]);
                    tipOper.COPIACAMPOLIVRE5 = Convert.ToInt32(dt.Rows[0]["COPIACAMPOLIVRE5"]);
                    tipOper.COPIACAMPOLIVRE6 = Convert.ToInt32(dt.Rows[0]["COPIACAMPOLIVRE6"]);
                    tipOper.COPIADATAEXTRA1 = Convert.ToInt32(dt.Rows[0]["COPIADATAEXTRA1"]);
                    tipOper.COPIADATAEXTRA2 = Convert.ToInt32(dt.Rows[0]["COPIADATAEXTRA2"]);
                    tipOper.COPIADATAEXTRA3 = Convert.ToInt32(dt.Rows[0]["COPIADATAEXTRA3"]);
                    tipOper.COPIADATAEXTRA4 = Convert.ToInt32(dt.Rows[0]["COPIADATAEXTRA4"]);
                    tipOper.COPIADATAEXTRA5 = Convert.ToInt32(dt.Rows[0]["COPIADATAEXTRA5"]);
                    tipOper.COPIADATAEXTRA6 = Convert.ToInt32(dt.Rows[0]["COPIADATAEXTRA6"]);
                    tipOper.COPIACAMPOVALOR1 = Convert.ToInt32(dt.Rows[0]["COPIACAMPOVALOR1"]);
                    tipOper.COPIACAMPOVALOR2 = Convert.ToInt32(dt.Rows[0]["COPIACAMPOVALOR2"]);
                    tipOper.COPIACAMPOVALOR3 = Convert.ToInt32(dt.Rows[0]["COPIACAMPOVALOR3"]);
                    tipOper.COPIACAMPOVALOR4 = Convert.ToInt32(dt.Rows[0]["COPIACAMPOVALOR4"]);
                    tipOper.COPIACAMPOVALOR5 = Convert.ToInt32(dt.Rows[0]["COPIACAMPOVALOR5"]);
                    tipOper.COPIACAMPOVALOR6 = Convert.ToInt32(dt.Rows[0]["COPIACAMPOVALOR6"]);
                    tipOper.COPIAITEMCAMPOLIVRE1 = Convert.ToInt32(dt.Rows[0]["COPIAITEMCAMPOLIVRE1"]);
                    tipOper.COPIAITEMCAMPOLIVRE2 = Convert.ToInt32(dt.Rows[0]["COPIAITEMCAMPOLIVRE2"]);
                    tipOper.COPIAITEMCAMPOLIVRE3 = Convert.ToInt32(dt.Rows[0]["COPIAITEMCAMPOLIVRE3"]);
                    tipOper.COPIAITEMCAMPOLIVRE4 = Convert.ToInt32(dt.Rows[0]["COPIAITEMCAMPOLIVRE4"]);
                    tipOper.COPIAITEMCAMPOLIVRE5 = Convert.ToInt32(dt.Rows[0]["COPIAITEMCAMPOLIVRE5"]);
                    tipOper.COPIAITEMCAMPOLIVRE6 = Convert.ToInt32(dt.Rows[0]["COPIAITEMCAMPOLIVRE6"]);
                    tipOper.COPIAITEMDATAEXTRA1 = Convert.ToInt32(dt.Rows[0]["COPIAITEMDATAEXTRA1"]);
                    tipOper.COPIAITEMDATAEXTRA2 = Convert.ToInt32(dt.Rows[0]["COPIAITEMDATAEXTRA2"]);
                    tipOper.COPIAITEMDATAEXTRA3 = Convert.ToInt32(dt.Rows[0]["COPIAITEMDATAEXTRA3"]);
                    tipOper.COPIAITEMDATAEXTRA4 = Convert.ToInt32(dt.Rows[0]["COPIAITEMDATAEXTRA4"]);
                    tipOper.COPIAITEMDATAEXTRA5 = Convert.ToInt32(dt.Rows[0]["COPIAITEMDATAEXTRA5"]);
                    tipOper.COPIAITEMDATAEXTRA6 = Convert.ToInt32(dt.Rows[0]["COPIAITEMDATAEXTRA6"]);
                    tipOper.COPIAITEMCAMPOVALOR1 = Convert.ToInt32(dt.Rows[0]["COPIAITEMCAMPOVALOR1"]);
                    tipOper.COPIAITEMCAMPOVALOR2 = Convert.ToInt32(dt.Rows[0]["COPIAITEMCAMPOVALOR2"]);
                    tipOper.COPIAITEMCAMPOVALOR3 = Convert.ToInt32(dt.Rows[0]["COPIAITEMCAMPOVALOR3"]);
                    tipOper.COPIAITEMCAMPOVALOR4 = Convert.ToInt32(dt.Rows[0]["COPIAITEMCAMPOVALOR4"]);
                    tipOper.COPIAITEMCAMPOVALOR5 = Convert.ToInt32(dt.Rows[0]["COPIAITEMCAMPOVALOR5"]);
                    tipOper.COPIAITEMCAMPOVALOR6 = Convert.ToInt32(dt.Rows[0]["COPIAITEMCAMPOVALOR6"]);
                    tipOper.TIPOPER = Convert.ToInt32(dt.Rows[0]["TIPOPER"]);
                    tipOper.UNDMEDPADRAO = Convert.ToInt32(dt.Rows[0]["UNDMEDPADRAO"]);
                    tipOper.CODFORMULAVALIDAOPERACAO = dt.Rows[0]["CODFORMULAVALIDAOPERACAO"].ToString();
                    tipOper.CODFORMULAVALIDAOPERITEM = dt.Rows[0]["CODFORMULAVALIDAOPERITEM"].ToString();
                    tipOper.GERAORDEMPRODUCAO = Convert.ToInt32(dt.Rows[0]["GERAORDEMPRODUCAO"]);
                    tipOper.CODFILIALDEFUALT = Convert.ToInt32(dt.Rows[0]["CODFILIALDEFUALT"]);
                    tipOper.LOCAL1DEFAULT = dt.Rows[0]["LOCAL1DEFAULT"].ToString();
                    tipOper.CODNATDENTRO = dt.Rows[0]["CODNATDENTRO"].ToString();
                    tipOper.CODNATFORA = dt.Rows[0]["CODNATFORA"].ToString();
                    tipOper.TIPOIMPRESSAODANFE = Convert.ToInt32(dt.Rows[0]["TIPOIMPRESSAODANFE"]);
                    tipOper.MODDOCFISCAL = dt.Rows[0]["MODDOCFISCAL"].ToString();
                    tipOper.TEXTOPRODNFE = dt.Rows[0]["TEXTOPRODNFE"].ToString();
                    if (dt.Rows[0]["FINEMISSAONFE"] != DBNull.Value)
                    {
                        tipOper.FINEMISSAONFE = Convert.ToInt32(dt.Rows[0]["FINEMISSAONFE"]);
                    }
                    tipOper.USAVALORFRETE = dt.Rows[0]["USAVALORFRETE"].ToString();
                    tipOper.USAVALORDESCONTO = dt.Rows[0]["USAVALORDESCONTO"].ToString();
                    tipOper.USAVALORDESPESA = dt.Rows[0]["USAVALORDESPESA"].ToString();
                    tipOper.USAVALORSEGURO = dt.Rows[0]["USAVALORSEGURO"].ToString();
                    tipOper.USACODNATUREZAORCAMENTO = dt.Rows[0]["USACODNATUREZAORCAMENTO"].ToString();
                    tipOper.USACODCCUSTO = dt.Rows[0]["USACODCCUSTO"].ToString();
                    tipOper.MASKNUMEROSEQ = Convert.ToInt32(dt.Rows[0]["MASKNUMEROSEQ"]);
                    tipOper.CODREPREPADRAO = dt.Rows[0]["CODREPREPADRAO"].ToString();
                    tipOper.VERIFICAVENCIMENTO = dt.Rows[0]["VERIFICAVENCIMENTO"].ToString();
                    if (dt.Rows[0]["QTDDIASVENCIDOS"] != DBNull.Value)
                    {
                        tipOper.QTDDIASVENCIDOS = Convert.ToInt32(dt.Rows[0]["QTDDIASVENCIDOS"]);
                    }

                    if (dt.Rows[0]["IDGRUPOREL"] != DBNull.Value)
                    {
                        tipOper.IDGRUPOREL = Convert.ToInt32(dt.Rows[0]["IDGRUPOREL"]);
                    }
                    
                    tipOper.VERIFICAFATURAMENTOPARCIAL = dt.Rows[0]["VERIFICAFATURAMENTOPARCIAL"].ToString() == "0" ? false : true;
                    tipOper.PERMITEFATURAMENTOPARCIAL = dt.Rows[0]["PERMITEFATURAMENTOPARCIAL"].ToString() == "0" ? false : true;
                    tipOper.CODIGOPRODUTO = dt.Rows[0]["CODIGOPRODUTO"].ToString();
                    tipOper.AUTOSELECAONATUREZA = Convert.ToInt32(dt.Rows[0]["AUTOSELECAONATUREZA"]);
                    tipOper.USACODVENDEDOR = dt.Rows[0]["USACODVENDEDOR"].ToString();
                    tipOper.CODVENDEDORPADRAO = dt.Rows[0]["CODVENDEDORPADRAO"].ToString();
                    tipOper.USAORDEMDECOMPRA = dt.Rows[0]["USAORDEMDECOMPRA"].ToString();
                    tipOper.USATABELAPRECO = dt.Rows[0]["USATABELAPRECO"].ToString();
                    tipOper.USAPRACRESCIMO = dt.Rows[0]["USAPRACRESCIMO"].ToString();
                    tipOper.USAVLACRESCIMO = dt.Rows[0]["USAVLACRESCIMO"].ToString();
                    tipOper.OPERESTOQUE2 = dt.Rows[0]["OPERESTOQUE2"].ToString();
                    if (dt.Rows[0]["CODFILIAL2DEFAULT"] != DBNull.Value)
                    {
                        tipOper.CODFILIAL2DEFAULT = Convert.ToInt32(dt.Rows[0]["CODFILIAL2DEFAULT"]);
                    }
                    
                    tipOper.LOCAL2DEFAULT = dt.Rows[0]["LOCAL2DEFAULT"].ToString();
                    tipOper.USABLOQUEIODESCONTO = dt.Rows[0]["USABLOQUEIODESCONTO"].ToString();
                    tipOper.DEFAULTBLOQUEIODESC = dt.Rows[0]["DEFAULTBLOQUEIODESC"].ToString();
                    tipOper.FRMCUSTOUNITARIO = dt.Rows[0]["FRMCUSTOUNITARIO"].ToString();
                    tipOper.DEFAULTMARCA = dt.Rows[0]["DEFAULTMARCA"].ToString();
                    tipOper.DEFAULTESPECIE = dt.Rows[0]["DEFAULTESPECIE"].ToString();
                    tipOper.USADATAENTREGAITEM = dt.Rows[0]["USADATAENTREGAITEM"].ToString();
                    tipOper.USASELECAOBLOQUEIODESCONTO = dt.Rows[0]["USASELECAOBLOQUEIODESCONTO"].ToString();
                    tipOper.USACAMPONFE = dt.Rows[0]["USACAMPONFE"].ToString();
                    tipOper.USADESCRICAOCOMPLEMENTAR = dt.Rows[0]["USADESCRICAOCOMPLEMENTAR"].ToString();
                    tipOper.TIPODESCONTOITEM = dt.Rows[0]["TIPODESCONTOITEM"].ToString();
                    tipOper.CODMENSAGEMIBPTAX = dt.Rows[0]["CODMENSAGEMIBPTAX"].ToString();
                    tipOper.USASEGUNDONUMERO = dt.Rows[0]["USASEGUNDONUMERO"].ToString() == "0" ? false : true;
                    tipOper.BASEVENCIMENTO = dt.Rows[0]["BASEVENCIMENTO"].ToString();
                    tipOper.CODQUERYNATUREZA = dt.Rows[0]["CODQUERYNATUREZA"].ToString();
                    tipOper.USACODAUXNFE = dt.Rows[0]["USACODAUXNFE"].ToString() == "0" ? false : true;
                    tipOper.BUSCAPRODUTOPOR = dt.Rows[0]["BUSCAPRODUTOPOR"].ToString();
                    tipOper.ACEITAPRECOZERO = dt.Rows[0]["ACEITAPRECOZERO"].ToString();
                    tipOper.USALIMITECREDITO = dt.Rows[0]["USALIMITECREDITO"].ToString();
                    tipOper.UTILIZACOPIAREFERENCIA = dt.Rows[0]["UTILIZACOPIAREFERENCIA"].ToString() == "0" ? false : true;
                    tipOper.UTILIZAPRODCOPMOSTO = dt.Rows[0]["UTILIZAPRODCOPMOSTO"].ToString();
                    tipOper.PERMITEEXPANDIRITENS = dt.Rows[0]["PERMITEEXPANDIRITENS"].ToString();
                    tipOper.MANTEMPRODUTOPRINCEXPANSAO = dt.Rows[0]["MANTEMPRODUTOPRINCEXPANSAO"].ToString();
                    tipOper.USABAIXADEPRODUCAO = dt.Rows[0]["USABAIXADEPRODUCAO"].ToString() == "0" ? false : true;
                    tipOper.CODTIPOPERENTRADA = dt.Rows[0]["CODTIPOPERENTRADA"].ToString();
                    tipOper.CODTIPOPERBAIXA = dt.Rows[0]["CODTIPOPERBAIXA"].ToString();
                    tipOper.CLASSIFICACAOCLIFOR = Convert.ToInt32(dt.Rows[0]["CLASSIFICACAOCLIFOR"]);
                    tipOper.USANFEIMPORTACAO = dt.Rows[0]["USANFEIMPORTACAO"].ToString() == "0" ? false : true;
                    tipOper.DECIMALVLUNITARIO = Convert.ToInt32(dt.Rows[0]["DECIMALVLUNITARIO"]);
                    tipOper.VTIPOPERSERIE = new vTipOperSerie().getVtipOperSerie(tipOper.CODTIPOPER, AppLib.Context.Filial, tipOper.CODEMPRESA);

                }

                return tipOper;
            }
            catch (Exception ex)
            {
                tipOper.CODTIPOPER = string.Empty;
                return tipOper;
            }
        }

        #endregion
    }
}
