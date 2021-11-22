using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PS.Glb.Class
{
    public class FLANCA
    {
        public FLANCA() { }

        public int CODEMPRESA { get; set; }
        public int CODLANCA { get; set; }
        public int TIPOPAGREC { get; set; }
        public string NUMERO { get; set; }
        public string CODCLIFOR { get; set; }
        public int CODFILIAL { get; set; }
        public DateTime DATAEMISSAO { get; set; }
        public DateTime DATAVENCIMENTO { get; set; }
        public DateTime DATAPREVBAIXA { get; set; }
        public DateTime? DATABAIXA { get; set; }
        public string OBSERVACAO { get; set; }
        public string CODMOEDA { get; set; }
        public decimal VLORIGINAL { get; set; }
        public decimal PRDESCONTO { get; set; }
        public decimal VLDESCONTO { get; set; }
        public decimal PRMULTA { get; set; }
        public decimal VLMULTA { get; set; }
        public decimal PRJUROS { get; set; }
        public decimal VLJUROS { get; set; }
        public int CODOPER { get; set; }
        public string CODCONTA { get; set; }
        public string CODTIPDOC { get; set; }
        public decimal VLBAIXADO { get; set; }
        public string SEGUNDONUMERO { get; set; }
        public string CODCCUSTO { get; set; }
        public string CODDEPTO { get; set; }
        public string CODFORMA { get; set; }
        public int? IDEXTRATO { get; set; }
        public string ORIGEM { get; set; }
        public DateTime? DATACRIACAO { get; set; }
        public string CODUSUARIOCRIACAO { get; set; }
        public DateTime? DATACANCELAMENTO { get; set; }
        public string CODUSUARIOCANCELAMENTO { get; set; }
        public string MOTIVOCANCELAMENTO { get; set; }
        public DateTime? DATACANCELAMENTOBAIXA { get; set; }
        public string CODUSUARIOCANCELAMENTOBAIXA { get; set; }
        public string MOTIVOCANCELAMENTOBAIXA { get; set; }
        public string CODNATUREZAORCAMENTO { get; set; }
        public decimal VLVINCAD { get; set; }
        public decimal VLVINCDV { get; set; }
        public decimal VLLIQUIDO { get; set; }
        public int? CODFATURA { get; set; }
        public string NFOUDUP { get; set; }
        public int? CODCHEQUE { get; set; }
        public string CODREPRE { get; set; }
        public string NOMECLIENTE { get; set; }
        public string CODSTATUS { get; set; }
        public int NSEQLANCA { get; set; }
        public string CODVENDEDOR { get; set; }
        public int? NPARCELA { get; set; }
        public int? DIAFIXO { get; set; }
        public string PRAZOVENCTO { get; set; }
        public int? CODLANCAPARCELAMENTO { get; set; }


        public FLANCA getFlanca(int codlanca, AppLib.Data.Connection conn)
        {
            FLANCA flanca = new FLANCA();
            try
            {
                DataTable dtFlanca = conn.ExecQuery("SELECT * FROM FLANCA WHERE CODLANCA = ? AND CODEMPRESA = ? AND CODFILIAL = ?", new object[] {codlanca, AppLib.Context.Empresa, AppLib.Context.Filial });
                if (dtFlanca.Rows.Count > 0)
                {
                    flanca.CODEMPRESA = Convert.ToInt32(dtFlanca.Rows[0]["CODEMPRESA"]);
                    flanca.CODLANCA = Convert.ToInt32(dtFlanca.Rows[0]["CODLANCA"]);
                    flanca.TIPOPAGREC = Convert.ToInt32(dtFlanca.Rows[0]["TIPOPAGREC"]);
                    flanca.NUMERO = dtFlanca.Rows[0]["NUMERO"].ToString();
                    flanca.CODCLIFOR = dtFlanca.Rows[0]["CODCLIFOR"].ToString();
                    flanca.CODFILIAL = Convert.ToInt32(dtFlanca.Rows[0]["CODFILIAL"]);
                    flanca.DATAEMISSAO = Convert.ToDateTime(dtFlanca.Rows[0]["DATAEMISSAO"]);
                    flanca.DATAVENCIMENTO = Convert.ToDateTime(dtFlanca.Rows[0]["DATAVENCIMENTO"]);
                    flanca.DATAPREVBAIXA = Convert.ToDateTime(dtFlanca.Rows[0]["DATAPREVBAIXA"]);
                    flanca.DATABAIXA = Convert.ToDateTime(dtFlanca.Rows[0]["DATABAIXA"]);
                    flanca.OBSERVACAO = dtFlanca.Rows[0]["OBSERVACAO"].ToString();
                    flanca.CODMOEDA = dtFlanca.Rows[0]["CODMOEDA"].ToString();
                    flanca.VLORIGINAL = Convert.ToDecimal(dtFlanca.Rows[0]["VLORIGINAL"]);
                    flanca.PRDESCONTO = Convert.ToDecimal(dtFlanca.Rows[0]["PRDESCONTO"]);
                    flanca.VLDESCONTO = Convert.ToDecimal(dtFlanca.Rows[0]["VLDESCONTO"]);
                    flanca.PRMULTA = Convert.ToDecimal(dtFlanca.Rows[0]["PRMULTA"]);
                    flanca.VLMULTA = Convert.ToDecimal(dtFlanca.Rows[0]["VLMULTA"]);
                    flanca.PRJUROS = Convert.ToDecimal(dtFlanca.Rows[0]["PRJUROS"]);
                    flanca.VLJUROS = Convert.ToDecimal(dtFlanca.Rows[0]["VLJUROS"]);
                    flanca.CODOPER = Convert.ToInt32(dtFlanca.Rows[0]["CODOPER"]);
                    flanca.CODCONTA = dtFlanca.Rows[0]["CODCONTA"].ToString();
                    flanca.CODTIPDOC = dtFlanca.Rows[0]["CODTIPDOC"].ToString();
                    flanca.VLBAIXADO = Convert.ToDecimal(dtFlanca.Rows[0]["VLBAIXADO"]);
                    flanca.SEGUNDONUMERO = dtFlanca.Rows[0]["SEGUNDONUMERO"].ToString();
                    flanca.CODCCUSTO = dtFlanca.Rows[0]["CODCCUSTO"].ToString();
                    flanca.CODDEPTO = dtFlanca.Rows[0]["CODDEPTO"].ToString();
                    flanca.CODFORMA = dtFlanca.Rows[0]["CODFORMA"].ToString();
                    flanca.IDEXTRATO = Convert.ToInt32(dtFlanca.Rows[0]["IDEXTRATO"]);
                    flanca.ORIGEM = dtFlanca.Rows[0]["ORIGEM"].ToString();
                    flanca.DATACRIACAO = Convert.ToDateTime(dtFlanca.Rows[0]["DATACRIACAO"]);
                    flanca.CODUSUARIOCRIACAO = dtFlanca.Rows[0]["CODUSUARIOCRIACAO"].ToString();
                    flanca.DATACANCELAMENTO = Convert.ToDateTime(dtFlanca.Rows[0]["DATACANCELAMENTO"]);
                    flanca.CODUSUARIOCANCELAMENTO = dtFlanca.Rows[0]["CODUSUARIOCANCELAMENTO"].ToString();
                    flanca.MOTIVOCANCELAMENTO = dtFlanca.Rows[0]["MOTIVOCANCELAMENTO"].ToString();
                    flanca.DATACANCELAMENTOBAIXA = Convert.ToDateTime(dtFlanca.Rows[0]["DATACANCELAMENTOBAIXA"]);
                    flanca.CODUSUARIOCANCELAMENTOBAIXA = dtFlanca.Rows[0]["CODUSUARIOCANCELAMENTOBAIXA"].ToString();
                    flanca.MOTIVOCANCELAMENTOBAIXA = dtFlanca.Rows[0]["MOTIVOCANCELAMENTOBAIXA"].ToString();
                    flanca.CODNATUREZAORCAMENTO = dtFlanca.Rows[0]["CODNATUREZAORCAMENTO"].ToString();
                    flanca.VLVINCAD = Convert.ToDecimal(dtFlanca.Rows[0]["VLVINCAD"]);
                    flanca.VLVINCDV = Convert.ToDecimal(dtFlanca.Rows[0]["VLVINCDV"]);
                    flanca.VLLIQUIDO = Convert.ToDecimal(dtFlanca.Rows[0]["VLLIQUIDO"]);
                    flanca.CODFATURA = Convert.ToInt32(dtFlanca.Rows[0]["CODFATURA"]);
                    flanca.NFOUDUP = dtFlanca.Rows[0]["NFOUDUP"].ToString();
                    flanca.CODCHEQUE = Convert.ToInt32(dtFlanca.Rows[0]["CODCHEQUE"]);
                    flanca.CODREPRE = dtFlanca.Rows[0]["CODREPRE"].ToString();
                    flanca.NOMECLIENTE = dtFlanca.Rows[0]["NOMECLIENTE"].ToString();
                    flanca.CODSTATUS = dtFlanca.Rows[0]["CODSTATUS"].ToString();
                    flanca.NSEQLANCA = Convert.ToInt32(dtFlanca.Rows[0]["NSEQLANCA"]);
                    flanca.CODVENDEDOR = dtFlanca.Rows[0]["CODVENDEDOR"].ToString();
                    flanca.NPARCELA = Convert.ToInt32(dtFlanca.Rows[0]["NPARCELA"]);
                    flanca.DIAFIXO = Convert.ToInt32(dtFlanca.Rows[0]["DIAFIXO"]);
                    flanca.PRAZOVENCTO = dtFlanca.Rows[0]["PRAZOVENCTO"].ToString();
                    flanca.CODLANCAPARCELAMENTO = Convert.ToInt32(dtFlanca.Rows[0]["CODLANCAPARCELAMENTO"]);
                }
                return flanca;
            }
            catch (Exception)
            {

                return flanca;
            }
        }

        public List<FLANCA> getFlancaOper(int codoper, AppLib.Data.Connection conn)
        {
            List<FLANCA> listaFLANCA = new List<FLANCA>();
            

            try
            {
                DataTable dtFlanca = conn.ExecQuery("SELECT * FROM FLANCA WHERE CODOPER = ? AND CODEMPRESA = ? AND CODFILIAL = ?", new object[] { codoper, AppLib.Context.Empresa, AppLib.Context.Filial });
                for (int i = 0; i < dtFlanca.Rows.Count; i++)
                {
                    FLANCA flanca = new FLANCA();
                    flanca.CODEMPRESA = Convert.ToInt32(dtFlanca.Rows[i]["CODEMPRESA"]);
                    flanca.CODLANCA = Convert.ToInt32(dtFlanca.Rows[i]["CODLANCA"]);
                    flanca.TIPOPAGREC = Convert.ToInt32(dtFlanca.Rows[i]["TIPOPAGREC"]);
                    flanca.NUMERO = dtFlanca.Rows[i]["NUMERO"].ToString();
                    flanca.CODCLIFOR = dtFlanca.Rows[i]["CODCLIFOR"].ToString();
                    flanca.CODFILIAL = Convert.ToInt32(dtFlanca.Rows[i]["CODFILIAL"]);
                    flanca.DATAEMISSAO = Convert.ToDateTime(dtFlanca.Rows[i]["DATAEMISSAO"]);
                    flanca.DATAVENCIMENTO = Convert.ToDateTime(dtFlanca.Rows[i]["DATAVENCIMENTO"]);
                    flanca.DATAPREVBAIXA = Convert.ToDateTime(dtFlanca.Rows[i]["DATAPREVBAIXA"]);
                    if (!string.IsNullOrEmpty(dtFlanca.Rows[i]["DATABAIXA"].ToString()))
                    {
                        flanca.DATABAIXA = Convert.ToDateTime(dtFlanca.Rows[i]["DATABAIXA"]);
                    }
                    else
                    {
                        flanca.DATABAIXA = null;
                    }
                    
                    flanca.OBSERVACAO = dtFlanca.Rows[i]["OBSERVACAO"].ToString();
                    flanca.CODMOEDA = dtFlanca.Rows[i]["CODMOEDA"].ToString();
                    flanca.VLORIGINAL = Convert.ToDecimal(dtFlanca.Rows[i]["VLORIGINAL"]);
                    flanca.PRDESCONTO = Convert.ToDecimal(dtFlanca.Rows[i]["PRDESCONTO"]);
                    flanca.VLDESCONTO = Convert.ToDecimal(dtFlanca.Rows[i]["VLDESCONTO"]);
                    flanca.PRMULTA = Convert.ToDecimal(dtFlanca.Rows[i]["PRMULTA"]);
                    flanca.VLMULTA = Convert.ToDecimal(dtFlanca.Rows[i]["VLMULTA"]);
                    flanca.PRJUROS = Convert.ToDecimal(dtFlanca.Rows[i]["PRJUROS"]);
                    flanca.VLJUROS = Convert.ToDecimal(dtFlanca.Rows[i]["VLJUROS"]);
                    flanca.CODOPER = Convert.ToInt32(dtFlanca.Rows[i]["CODOPER"]);
                    flanca.CODCONTA = dtFlanca.Rows[i]["CODCONTA"].ToString();
                    flanca.CODTIPDOC = dtFlanca.Rows[i]["CODTIPDOC"].ToString();
                    flanca.VLBAIXADO = Convert.ToDecimal(dtFlanca.Rows[i]["VLBAIXADO"]);
                    flanca.SEGUNDONUMERO = dtFlanca.Rows[i]["SEGUNDONUMERO"].ToString();
                    flanca.CODCCUSTO = dtFlanca.Rows[i]["CODCCUSTO"].ToString();
                    flanca.CODDEPTO = dtFlanca.Rows[i]["CODDEPTO"].ToString();
                    flanca.CODFORMA = dtFlanca.Rows[i]["CODFORMA"].ToString();
                    if (!string.IsNullOrEmpty(dtFlanca.Rows[i]["IDEXTRATO"].ToString()))
                    {
                        flanca.IDEXTRATO = Convert.ToInt32(dtFlanca.Rows[i]["IDEXTRATO"]);
                    }
                    else
                    {
                        flanca.IDEXTRATO = null;
                    }
                    
                    flanca.ORIGEM = dtFlanca.Rows[i]["ORIGEM"].ToString();
                    if (!string.IsNullOrEmpty(dtFlanca.Rows[i]["DATACRIACAO"].ToString()))
                    {
                        flanca.DATACRIACAO = Convert.ToDateTime(dtFlanca.Rows[i]["DATACRIACAO"]);
                    }
                    else
                    {
                        flanca.DATACRIACAO = null;
                    }
                    
                    flanca.CODUSUARIOCRIACAO = dtFlanca.Rows[i]["CODUSUARIOCRIACAO"].ToString();
                    if (!string.IsNullOrEmpty(dtFlanca.Rows[i]["DATACANCELAMENTO"].ToString()))
                    {
                        flanca.DATACANCELAMENTO = Convert.ToDateTime(dtFlanca.Rows[i]["DATACANCELAMENTO"]);
                    }
                    else
                    {
                        flanca.DATACANCELAMENTO = null;
                    }

                
                    flanca.CODUSUARIOCANCELAMENTO = dtFlanca.Rows[i]["CODUSUARIOCANCELAMENTO"].ToString();
                    flanca.MOTIVOCANCELAMENTO = dtFlanca.Rows[i]["MOTIVOCANCELAMENTO"].ToString();

                    if (!string.IsNullOrEmpty(dtFlanca.Rows[i]["DATACANCELAMENTOBAIXA"].ToString()))
                    {
                        flanca.DATACANCELAMENTOBAIXA = Convert.ToDateTime(dtFlanca.Rows[i]["DATACANCELAMENTOBAIXA"]);
                    }
                    else
                    {
                        flanca.DATACANCELAMENTOBAIXA = null;
                    }

                    flanca.CODUSUARIOCANCELAMENTOBAIXA = dtFlanca.Rows[i]["CODUSUARIOCANCELAMENTOBAIXA"].ToString();
                    flanca.MOTIVOCANCELAMENTOBAIXA = dtFlanca.Rows[i]["MOTIVOCANCELAMENTOBAIXA"].ToString();
                    flanca.CODNATUREZAORCAMENTO = dtFlanca.Rows[i]["CODNATUREZAORCAMENTO"].ToString();
                    flanca.VLVINCAD = Convert.ToDecimal(dtFlanca.Rows[i]["VLVINCAD"]);
                    flanca.VLVINCDV = Convert.ToDecimal(dtFlanca.Rows[i]["VLVINCDV"]);
                    flanca.VLLIQUIDO = Convert.ToDecimal(dtFlanca.Rows[i]["VLLIQUIDO"]);

                    

                    if (!string.IsNullOrEmpty(dtFlanca.Rows[i]["CODFATURA"].ToString()))
                    {
                        flanca.CODFATURA = Convert.ToInt32(dtFlanca.Rows[i]["CODFATURA"]);
                    }
                    else
                    {
                        flanca.CODFATURA = null;
                    }



                    flanca.NFOUDUP = dtFlanca.Rows[i]["NFOUDUP"].ToString();



                    if (!string.IsNullOrEmpty(dtFlanca.Rows[i]["CODCHEQUE"].ToString()))
                    {
                        flanca.CODCHEQUE = Convert.ToInt32(dtFlanca.Rows[i]["CODCHEQUE"]);
                    }
                    else
                    {
                        flanca.CODCHEQUE = null;
                    }
           
                    flanca.CODREPRE = dtFlanca.Rows[i]["CODREPRE"].ToString();
                    flanca.NOMECLIENTE = dtFlanca.Rows[i]["NOMECLIENTE"].ToString();
                    flanca.CODSTATUS = dtFlanca.Rows[i]["CODSTATUS"].ToString();
                    flanca.NSEQLANCA = Convert.ToInt32(dtFlanca.Rows[i]["NSEQLANCA"]);
                    flanca.CODVENDEDOR = dtFlanca.Rows[i]["CODVENDEDOR"].ToString();

                    if (!string.IsNullOrEmpty(dtFlanca.Rows[i]["NPARCELA"].ToString()))
                    {
                        flanca.NPARCELA = Convert.ToInt32(dtFlanca.Rows[i]["NPARCELA"]);
                    }
                    else
                    {
                        flanca.NPARCELA = null;
                    }

                    if (!string.IsNullOrEmpty(dtFlanca.Rows[i]["DIAFIXO"].ToString()))
                    {
                        flanca.DIAFIXO = Convert.ToInt32(dtFlanca.Rows[i]["DIAFIXO"]);
                    }
                    else
                    {
                        flanca.DIAFIXO = null;
                    }

                   
                    flanca.PRAZOVENCTO = dtFlanca.Rows[i]["PRAZOVENCTO"].ToString();


                    if (!string.IsNullOrEmpty(dtFlanca.Rows[i]["CODLANCAPARCELAMENTO"].ToString()))
                    {
                        flanca.CODLANCAPARCELAMENTO = Convert.ToInt32(dtFlanca.Rows[i]["CODLANCAPARCELAMENTO"]);
                    }
                    else
                    {
                        flanca.CODLANCAPARCELAMENTO = null;
                    }
                    listaFLANCA.Add(flanca);
                }
                return listaFLANCA;
            }
            catch (Exception ex)
            {

                return listaFLANCA;
            }
        }
    }
}
