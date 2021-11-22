using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb.Formula
{
    public class Function
    {
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private Global gb = new Global();

        public List<PS.Lib.DataField> current;

        public Function()
        { 
        
        }

        public string RetornaTextoFormula(int CodEmpresa, string CodFormula)
        {
            string sSql = "SELECT TEXTO FROM GFORMULA WHERE CODEMPRESA = ? AND CODFORMULA = ?";

            return dbs.QueryValue(string.Empty, sSql, CodEmpresa, CodFormula).ToString();
        }

        public string PreparaFormula(int CodEmpresa, string CodFormula)
        {
            return PreparaFormula(RetornaTextoFormula(CodEmpresa, CodFormula));
        }

        public string PreparaFormula(string comando)
        {
            string texto = @"using System;
                             using System.Collections.Generic;
                             using System.Text;
                             using PS.Lib;
                             using PS.Lib.Data;
                             using PS.Glb.Formula;

                             namespace TempNamespace
                             {
	                             public class TempClass
	                             {
                                     private PS.Glb.Formula.Function function = new PS.Glb.Formula.Function();
                                     private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

                                     private object key1 = PS.Lib.Contexto.Session.key1;
                                     private object key2 = PS.Lib.Contexto.Session.key2;
                                     private object key3 = PS.Lib.Contexto.Session.key3;
                                     private object key4 = PS.Lib.Contexto.Session.key4;
                                     private object key5 = PS.Lib.Contexto.Session.key5;

		                             public object TempMethod(params object[] Parameters)
		                             {
                                        try
                                        {
                                            function.current = PS.Lib.Contexto.Session.Current;

                                            " + comando + @"

                                        }
                                        catch(Exception ex)
                                        {
                                            throw new Exception(ex.Message);
                                        }
		                             }
	                             }
                             }";

            return texto;
        }

        public object CampoVREGRAICMS(string campo)
        {
            try
            {
                object CodNat = this.CampoGOPERITEM("CODNATUREZA");
                if (CodNat == null)
                    throw new Exception("CFOP não localizada no item da operação.");

                string sSql = string.Concat("SELECT ", campo, " FROM VREGRAICMS, VNATUREZA WHERE VREGRAICMS.CODEMPRESA = VNATUREZA.CODEMPRESA AND VREGRAICMS.IDREGRA = VNATUREZA.IDREGRAICMS AND VNATUREZA.CODEMPRESA = ? AND VNATUREZA.CODNATUREZA = ?");
                object retorno;

                if (this.current == null)
                {
                    return retorno = dbs.QueryValue(null, sSql, PS.Lib.Contexto.Session.key1, CodNat);
                }
                else
                {
                    PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(current, "CODEMPRESA");
                    return retorno = Convert.ToDecimal(dbs.QueryValue(0, sSql, dtCODEMPRESA.Valor, CodNat));
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw new Exception("Função CampoVREGRAICMS: "  + err);
            }
        }

        public object CampoGOPER(string campo)
        {
            try
            {
                if (this.current == null)
                {
                    object retorno;
                    string sSql = string.Concat("SELECT ", campo, " FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?");
                    return retorno = dbs.QueryValue(null, sSql, PS.Lib.Contexto.Session.key1, PS.Lib.Contexto.Session.key2);
                }
                else
                {
                    PS.Lib.DataField dt = gb.RetornaDataFieldByCampo(current, campo);
                    return dt.Valor;
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw new Exception("Função CampoGOPER: " + err);
            }
        }

        public object CampoGOPERCOMPL(string campo)
        {
            try
            {
                if (this.current == null)
                {
                    object retorno;
                    string sSql = string.Concat("SELECT ", campo, " FROM GOPERCOMPL WHERE CODEMPRESA = ? AND CODOPER = ?");
                    return retorno = dbs.QueryValue(null, sSql, PS.Lib.Contexto.Session.key1, PS.Lib.Contexto.Session.key2);
                }
                else
                {
                    PS.Lib.DataField dt = gb.RetornaDataFieldByCampo(current, campo);
                    return dt.Valor;
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw new Exception("Função CampoGOPERCOMPL: " + err);
            }
        }

        public object CampoGOPERITEM(string campo)
        {
            try
            {
                if (this.current == null)
                {
                    object retorno;
                    string sSql = string.Concat("SELECT ", campo, " FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?");
                    return retorno = dbs.QueryValue(null, sSql, PS.Lib.Contexto.Session.key1, PS.Lib.Contexto.Session.key2, PS.Lib.Contexto.Session.key3);
                }
                else
                {
                    PS.Lib.DataField dt = gb.RetornaDataFieldByCampo(current, campo);
                    return dt.Valor;
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw new Exception("Função CampoGOPERITEM: " + err);
            }
        }

        public object CampoGOPERITEMCOMPL(string campo)
        {
            try
            {
                if (this.current == null)
                {
                    object retorno;
                    string sSql = string.Concat("SELECT ", campo, " FROM GOPERITEMCOMPL WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?");
                    return retorno = dbs.QueryValue(null, sSql, PS.Lib.Contexto.Session.key1, PS.Lib.Contexto.Session.key2, PS.Lib.Contexto.Session.key3);
                }
                else
                {
                    PS.Lib.DataField dt = gb.RetornaDataFieldByCampo(current, campo);
                    return dt.Valor;
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw new Exception("Função CampoGOPERITEMCOMPL: " + err);
            }
        }

        public object CampoFLANCA(string campo)
        {
            try
            {
                if (this.current == null)
                {
                    object retorno;
                    string sSql = string.Concat("SELECT ", campo, " FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?");
                    return retorno = dbs.QueryValue(null, sSql, PS.Lib.Contexto.Session.key1, PS.Lib.Contexto.Session.key2);
                }
                else
                {
                    PS.Lib.DataField dt = gb.RetornaDataFieldByCampo(current, campo);
                    return dt.Valor;
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw new Exception("Função CampoFLANCA: " + err);
            }
        }

        public object CampoFLANCACOMPL(string campo)
        {
            try
            {
                if (this.current == null)
                {
                    object retorno;
                    string sSql = string.Concat("SELECT ", campo, " FROM FLANCACOMPL WHERE CODEMPRESA = ? AND CODLANCA = ?");
                    return retorno = dbs.QueryValue(null, sSql, PS.Lib.Contexto.Session.key1, PS.Lib.Contexto.Session.key2);
                }
                else
                {
                    PS.Lib.DataField dt = gb.RetornaDataFieldByCampo(current, campo);
                    return dt.Valor;
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw new Exception("Função CampoFLANCACOMPL: " + err);
            }
        }

        public object CampoVPRODUTO(string campo, string CodProduto)
        {
            try
            {
                if (this.current == null)
                {
                    string sSql = string.Concat("SELECT ", campo, " FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ?");
                    object retorno;

                    return retorno = dbs.QueryValue(null, sSql, PS.Lib.Contexto.Session.key1, CodProduto);
                }
                else
                {
                    PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(current, "CODEMPRESA");

                    string sSql = string.Concat("SELECT ", campo, " FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ?");
                    object retorno;

                    return retorno = dbs.QueryValue(null, sSql, dtCODEMPRESA.Valor, CodProduto);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw new Exception("Função CampoVPRODUTO: " + err);
            }
        }

        public object CampoVPRODUTOCOMPL(string campo, string CodProduto)
        {
            try
            {
                if (this.current == null)
                {
                    string sSql = string.Concat("SELECT ", campo, " FROM VPRODUTOCOMPL WHERE CODEMPRESA = ? AND CODPRODUTO = ?");
                    object retorno;

                    return retorno = dbs.QueryValue(null, sSql, PS.Lib.Contexto.Session.key1, CodProduto);
                }
                else
                {
                    PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(current, "CODEMPRESA");

                    string sSql = string.Concat("SELECT ", campo, " FROM VPRODUTOCOMPL WHERE CODEMPRESA = ? AND CODPRODUTO = ?");
                    object retorno;

                    return retorno = dbs.QueryValue(null, sSql, dtCODEMPRESA.Valor, CodProduto);
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw new Exception("Função CampoVPRODUTOCOMPL: " + err);
            }
        }

        public decimal ValorTotalTributoOper(string CodTributo)
        {
            try
            {
                if (this.current == null)
                {
                    string sSql = "SELECT VALOR VALOR FROM GOPERTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND CODTRIBUTO = ?";
                    decimal retorno;

                    return retorno = Convert.ToDecimal(dbs.QueryValue(0, sSql, PS.Lib.Contexto.Session.key1, PS.Lib.Contexto.Session.key2, CodTributo));
                }
                else
                {
                    PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(current, "CODEMPRESA");
                    PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(current, "CODOPER");

                    string sSql = "SELECT VALOR VALOR FROM GOPERTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND CODTRIBUTO = ?";
                    decimal retorno;

                    return retorno = Convert.ToDecimal(dbs.QueryValue(0, sSql, dtCODEMPRESA.Valor, dtCODOPER.Valor, CodTributo));
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw new Exception("Função ValorTotalTributoOper: " + err);
            }
        }

        public decimal ValorTotalTributoItemOper(string CodTributo)
        {
            try
            {
                if (this.current == null)
                {
                    string sSql = "SELECT SUM(VALOR) VALOR FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND CODTRIBUTO = ?";
                    decimal retorno;

                    return retorno = Convert.ToDecimal(dbs.QueryValue(0, sSql, PS.Lib.Contexto.Session.key1, PS.Lib.Contexto.Session.key2, CodTributo));
                }
                else
                {
                    PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(current, "CODEMPRESA");
                    PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(current, "CODOPER");

                    string sSql = "SELECT SUM(VALOR) VALOR FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND CODTRIBUTO = ?";
                    decimal retorno;

                    return retorno = Convert.ToDecimal(dbs.QueryValue(0, sSql, dtCODEMPRESA.Valor, dtCODOPER.Valor, CodTributo));
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw new Exception("Função ValorTotalTributoItemOper: " + err);
            }
        }

        public decimal ValorTributoItemOper(string CodTributo)
        {
            try
            {
                if (this.current == null)
                {
                    string sSql = "SELECT SUM(VALOR) VALOR FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ? AND CODTRIBUTO = ?";
                    decimal retorno;

                    return retorno = Convert.ToDecimal(dbs.QueryValue(0, sSql, PS.Lib.Contexto.Session.key1, PS.Lib.Contexto.Session.key2, PS.Lib.Contexto.Session.key3, CodTributo));
                }
                else
                {
                    PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(current, "CODEMPRESA");
                    PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(current, "CODOPER");
                    PS.Lib.DataField dtNSEQITEM = gb.RetornaDataFieldByCampo(current, "NSEQITEM");

                    string sSql = "SELECT SUM(VALOR) VALOR FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ? AND CODTRIBUTO = ?";
                    decimal retorno;

                    return retorno = Convert.ToDecimal(dbs.QueryValue(0, sSql, dtCODEMPRESA.Valor, dtCODOPER.Valor, dtNSEQITEM.Valor, CodTributo));
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw new Exception("Função ValorTributoItemOper: " + err);
            }
        }

        public decimal ValorTotalItem()
        {
            try
            {
                if (this.current == null)
                {
                    string sSql = "SELECT SUM(VLTOTALITEM) VLTOTALITEM FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?";
                    decimal retorno;

                    return retorno = Convert.ToDecimal(dbs.QueryValue(0, sSql, PS.Lib.Contexto.Session.key1, PS.Lib.Contexto.Session.key2));
                }
                else
                {
                    PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(current, "CODEMPRESA");
                    PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(current, "CODOPER");

                    string sSql = "SELECT SUM(VLTOTALITEM) VLTOTALITEM FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?";
                    decimal retorno;

                    return retorno = Convert.ToDecimal(dbs.QueryValue(0, sSql, dtCODEMPRESA.Valor, dtCODOPER.Valor));
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw new Exception("Função ValorTotalItem: " + err);
            }
        }

        public decimal ValorTotalItemOper()
        {
            try
            {
                if (this.current == null)
                {
                    string sSql = "SELECT ISNULL(VLTOTALITEM,0) VLTOTALITEM FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?";
                    decimal retorno;

                    return retorno = Convert.ToDecimal(dbs.QueryValue(0, sSql, PS.Lib.Contexto.Session.key1, PS.Lib.Contexto.Session.key2, PS.Lib.Contexto.Session.key3));
                }
                else
                {
                    PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(current, "CODEMPRESA");
                    PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(current, "CODOPER");
                    PS.Lib.DataField dtNSEQITEM = gb.RetornaDataFieldByCampo(current, "NSEQITEM");

                    string sSql = "SELECT ISNULL(VLTOTALITEM,0) VLTOTALITEM FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?";
                    decimal retorno;

                    return retorno = Convert.ToDecimal(dbs.QueryValue(0, sSql, dtCODEMPRESA.Valor, dtCODOPER.Valor, dtNSEQITEM.Valor));
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw new Exception("Função ValorTotalItemOper: " + err);
            }
        }
        public decimal ValorUnitarioItemOper()
        {
            try
            {
                if (this.current == null)
                {
                    string sSql = "SELECT ISNULL(VLUNITARIO,0) VLUNITARIO FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?";
                    decimal retorno;

                    return retorno = Convert.ToDecimal(dbs.QueryValue(0, sSql, PS.Lib.Contexto.Session.key1, PS.Lib.Contexto.Session.key2, PS.Lib.Contexto.Session.key3));
                }
                else
                {
                    PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(current, "CODEMPRESA");
                    PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(current, "CODOPER");
                    PS.Lib.DataField dtNSEQITEM = gb.RetornaDataFieldByCampo(current, "NSEQITEM");

                    string sSql = "SELECT ISNULL(VLUNITARIO,0) VLUNITARIO FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ?";
                    decimal retorno;

                    return retorno = Convert.ToDecimal(dbs.QueryValue(0, sSql, dtCODEMPRESA.Valor, dtCODOPER.Valor, dtNSEQITEM.Valor));
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw new Exception("Função ValorUnitarioItemOper: " + err);
            }
        }
        public decimal ValorBrutoItemOper()
        {
            try
            {
                if (this.current == null)
                {
                    string sSql = "SELECT ISNULL(SUM(GOPERITEM.QUANTIDADE * GOPERITEM.VLUNITARIO),0) VALORBRUTOITEM FROM GOPERITEM WHERE GOPERITEM.CODEMPRESA = ? AND GOPERITEM.CODOPER = ?";
                    decimal retorno;

                    return retorno = Convert.ToDecimal(dbs.QueryValue(0, sSql, PS.Lib.Contexto.Session.key1, PS.Lib.Contexto.Session.key2));
                }
                else
                {
                    PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(current, "CODEMPRESA");
                    PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(current, "CODOPER");
                    PS.Lib.DataField dtNSEQITEM = gb.RetornaDataFieldByCampo(current, "NSEQITEM");

                    string sSql = "SELECT ISNULL(SUM(GOPERITEM.QUANTIDADE * GOPERITEM.VLUNITARIO),0) VALORBRUTOITEM FROM GOPERITEM WHERE GOPERITEM.CODEMPRESA = ? AND GOPERITEM.CODOPER = ?";
                    decimal retorno;

                    return retorno = Convert.ToDecimal(dbs.QueryValue(0, sSql, dtCODEMPRESA.Valor, dtCODOPER.Valor));
                }
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                throw new Exception("Função ValorBrutoItemOper: " + err);
            }
        }
    }
}
