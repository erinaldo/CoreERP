using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartExtratoCaixaData : PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private PS.Glb.Formula.Function function = new PS.Glb.Formula.Function();
        private PS.Lib.Interpretador interpreta = new PS.Lib.Interpretador();

        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CASE 
WHEN (SELECT COUNT(F.IDEXTRATOTRF) FROM FEXTRATO F WHERE F.CODEMPRESA = FEXTRATO.CODEMPRESA AND F.IDEXTRATOTRF = FEXTRATO.IDEXTRATO) > 0 THEN 3
ELSE TIPO
END TIPO,
IDEXTRATO,
CODFILIAL,
CODCONTA,
NUMERODOCUMENTO,
DATA,
VALOR,
HISTORICO,
CONVERT(BIT, COMPENSADO) COMPENSADO,
DATACOMPENSACAO,
IDEXTRATOTRF,
CODFILIALTRF,
CODCONTATRF,
CODCCUSTO,
CODNATUREZAORCAMENTO,
CODNATUREZAORCAMENTOTRANSF,
CODCCUSTOTRANSF
FROM FEXTRATO WHERE ";
        }

        public override void ValidateRecord(List<Lib.DataField> objArr)
        {
            base.ValidateRecord(objArr);

            PS.Lib.DataField dtNUMERODOCUMENTO = gb.RetornaDataFieldByCampo(objArr, "NUMERODOCUMENTO");
            PS.Lib.DataField dtCODFILIAL = gb.RetornaDataFieldByCampo(objArr, "CODFILIAL");
            PS.Lib.DataField dtCODCONTA = gb.RetornaDataFieldByCampo(objArr, "CODCONTA");
            PS.Lib.DataField dtDATA = gb.RetornaDataFieldByCampo(objArr, "DATA");
            PS.Lib.DataField dtVALOR = gb.RetornaDataFieldByCampo(objArr, "VALOR");
            PS.Lib.DataField dtCOMPENSADO = gb.RetornaDataFieldByCampo(objArr, "COMPENSADO");
            PS.Lib.DataField dtDATACOMPENSACAO = gb.RetornaDataFieldByCampo(objArr, "DATACOMPENSACAO");
            PS.Lib.DataField dtTIPO = gb.RetornaDataFieldByCampo(objArr, "TIPO");
            PS.Lib.DataField dtCODFILIALTRF = gb.RetornaDataFieldByCampo(objArr, "CODFILIALTRF");
            PS.Lib.DataField dtCODCONTATRF = gb.RetornaDataFieldByCampo(objArr, "CODCONTATRF");

            if (PossuiFinanceiroVinculado(objArr))
            {
                throw new Exception("Extrato  não pode ser modificado pois está relacionado a uma baixa.");
            }

            if (Convert.ToInt32(dtCOMPENSADO.Valor) == 1)
            {
                throw new Exception("Extrato não pode ser modificado pois está compensado.");
            }

            if (PossuiExtratoOrigem(objArr))
            {
                throw new Exception("Extrato não pode ser modificado pois está relacionado a outro extrato.");
            }

            if (dtNUMERODOCUMENTO.Valor == null || dtNUMERODOCUMENTO.Valor.ToString() == string.Empty)
            {
                throw new Exception("Informe o número do extrato.");
            }

            if (dtCODFILIAL.Valor == null || dtCODFILIAL.Valor.ToString() == string.Empty)
            {
                throw new Exception("Informe a filial.");
            }

            if (dtCODCONTA.Valor == null || dtCODCONTA.Valor.ToString() == string.Empty)
            {
                throw new Exception("Informe a conta/caixa.");
            }

            if(dtDATA.Valor == null)
            {
                throw new Exception("Informe a data.");
            }

            try
            {
                Decimal valor = Convert.ToDecimal(dtVALOR.Valor);
                if(valor <= 0)
                    throw new Exception("O valor do extrato deve ser maior que 0 (zero).");
            }
            catch
            {
                throw new Exception("O valor do extrato deve ser maior que 0 (zero).");
            }

            if (Convert.ToInt32(dtCOMPENSADO.Valor) == 1)
            {
                if (dtDATACOMPENSACAO.Valor == null)
                {
                    throw new Exception("Informe a data de compensação.");
                }   
            }

            if (Convert.ToInt32(dtTIPO.Valor) == 2)
            {
                if (dtCODFILIALTRF.Valor == null || dtCODFILIALTRF.Valor.ToString() == string.Empty)
                {
                    throw new Exception("Informe a filial de transferência.");
                }

                if (dtCODCONTATRF.Valor == null || dtCODCONTATRF.Valor.ToString() == string.Empty)
                {
                    throw new Exception("Informe a conta/caixa de transferência.");
                }

                if (dtCODCONTA.Valor.ToString() == dtCODCONTATRF.Valor.ToString())
                {
                    throw new Exception("A conta/caixa de origem não pode ser igual a conta/caixa de destino.");
                }
            }
        }

        public override List<PS.Lib.DataField> InsertRecord(List<PS.Lib.DataField> objArr)
        {   
            List<PS.Lib.DataField>  temp = base.InsertRecord(objArr);

            PS.Lib.DataField dtTIPO = gb.RetornaDataFieldByCampo(objArr, "TIPO");

            if (Convert.ToInt32(dtTIPO.Valor) == 2)
            {
                #region Atributos

                PS.Lib.DataField EX_CODEMPRESA = new PS.Lib.DataField("CODEMPRESA", null);
                PS.Lib.DataField EX_IDEXTRATO = new PS.Lib.DataField("IDEXTRATO", null, typeof(int), PS.Lib.Global.TypeAutoinc.AutoInc);
                PS.Lib.DataField EX_CODFILIAL = new PS.Lib.DataField("CODFILIAL", null);
                PS.Lib.DataField EX_CODCONTA = new PS.Lib.DataField("CODCONTA", null);
                PS.Lib.DataField EX_NUMERODOCUMENTO = new PS.Lib.DataField("NUMERODOCUMENTO", null);
                PS.Lib.DataField EX_DATA = new PS.Lib.DataField("DATA", null);
                PS.Lib.DataField EX_VALOR = new PS.Lib.DataField("VALOR", null);
                PS.Lib.DataField EX_HISTORICO = new PS.Lib.DataField("HISTORICO", null);
                PS.Lib.DataField EX_COMPENSADO = new PS.Lib.DataField("COMPENSADO", null);
                PS.Lib.DataField EX_DATACOMPENSACAO = new PS.Lib.DataField("DATACOMPENSACAO", null);
                PS.Lib.DataField EX_TIPO = new PS.Lib.DataField("TIPO", null);
                PS.Lib.DataField EX_CODCCUSTO = new PS.Lib.DataField("CODCCUSTO", null);
                PS.Lib.DataField EX_CODNATUREZAORCAMENTO = new PS.Lib.DataField("CODNATUREZAORCAMENTO", null);
                PS.Lib.DataField CODCCUSTOTRANSF = new PS.Lib.DataField("CODCCUSTOTRANSF", null);
                PS.Lib.DataField CODNATUREZAORCAMENTO = new PS.Lib.DataField("CODNATUREZAORCAMENTOTRANSF", null);
                #endregion

                #region Sets

                EX_CODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
                EX_IDEXTRATO.Valor = 0;
                EX_CODFILIAL.Valor = gb.RetornaDataFieldByCampo(objArr, "CODFILIALTRF").Valor;
                EX_CODCONTA.Valor = gb.RetornaDataFieldByCampo(objArr, "CODCONTATRF").Valor;
                EX_NUMERODOCUMENTO = gb.RetornaDataFieldByCampo(objArr, "NUMERODOCUMENTO");
                EX_DATA = gb.RetornaDataFieldByCampo(objArr, "DATA");
                EX_VALOR = gb.RetornaDataFieldByCampo(objArr, "VALOR");
                EX_HISTORICO = gb.RetornaDataFieldByCampo(objArr, "HISTORICO");
                EX_COMPENSADO = gb.RetornaDataFieldByCampo(objArr, "COMPENSADO");
                EX_DATACOMPENSACAO = gb.RetornaDataFieldByCampo(objArr, "DATACOMPENSACAO");
                EX_TIPO.Valor = 0;
                CODCCUSTOTRANSF = gb.RetornaDataFieldByCampo(objArr, "CODCCUSTOTRANSF");
                EX_CODCCUSTO.Valor = CODCCUSTOTRANSF.Valor;
                CODNATUREZAORCAMENTO = gb.RetornaDataFieldByCampo(objArr, "CODNATUREZAORCAMENTOTRANSF");
                EX_CODNATUREZAORCAMENTO.Valor = CODNATUREZAORCAMENTO.Valor;

                #endregion

                List<PS.Lib.DataField> objArrExTrf = new List<Lib.DataField>();
                objArrExTrf.Add(EX_CODEMPRESA);
                objArrExTrf.Add(EX_IDEXTRATO);
                objArrExTrf.Add(EX_CODFILIAL);
                objArrExTrf.Add(EX_CODCONTA);
                objArrExTrf.Add(EX_NUMERODOCUMENTO);
                objArrExTrf.Add(EX_DATA);
                objArrExTrf.Add(EX_VALOR);
                objArrExTrf.Add(EX_HISTORICO);
                objArrExTrf.Add(EX_COMPENSADO);
                objArrExTrf.Add(EX_DATACOMPENSACAO);
                objArrExTrf.Add(EX_TIPO);
                objArrExTrf.Add(EX_CODCCUSTO);
                objArrExTrf.Add(EX_CODNATUREZAORCAMENTO);
                objArrExTrf = this.InsertRecord(objArrExTrf);             

                PS.Lib.DataField DF_CODEMPRESA = gb.RetornaDataFieldByCampo(temp, "CODEMPRESA");
                PS.Lib.DataField DF_IDEXTRATO = gb.RetornaDataFieldByCampo(temp, "IDEXTRATO");
                PS.Lib.DataField DF_IDEXTRATOTRF = gb.RetornaDataFieldByCampo(objArrExTrf, "IDEXTRATO");
                string sSql = @"UPDATE FEXTRATO SET IDEXTRATOTRF = ? WHERE CODEMPRESA = ? AND IDEXTRATO = ?";
                dbs.QueryExec(sSql, DF_IDEXTRATOTRF.Valor, DF_CODEMPRESA.Valor, DF_IDEXTRATO.Valor);
            }

            return temp;
        }

        public override List<PS.Lib.DataField> EditRecord(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtIDEXTRATO = gb.RetornaDataFieldByCampo(objArr, "IDEXTRATO");
            PS.Lib.DataField dtTIPO = gb.RetornaDataFieldByCampo(objArr, "TIPO");

            bool Flag = false;
            if (dbs.QueryFind("SELECT IDEXTRATO FROM FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATO = ? AND TIPO = 2", dtCODEMPRESA.Valor, dtIDEXTRATO.Valor))
            {
                Flag = true;
            }

            if (Convert.ToInt32(dtTIPO.Valor) == 2)
            {
                PS.Lib.DataField dtIDEXTRATOTRF = gb.RetornaDataFieldByCampo(objArr, "IDEXTRATOTRF");

                if (dbs.QueryFind("SELECT IDEXTRATO FROM FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATO = ?", dtCODEMPRESA.Valor, dtIDEXTRATOTRF.Valor))
                {
                    List<PS.Lib.DataField> objArrTrf = gb.RetornaDataFieldByDataRow(dbs.QuerySelect("SELECT * FROM FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATO = ?", dtCODEMPRESA.Valor, dtIDEXTRATOTRF.Valor).Rows[0]);

                    if (Convert.ToInt32(gb.RetornaDataFieldByCampo(objArrTrf, "COMPENSADO").Valor) == 1)
                    {
                        throw new Exception("Extrato não pode ser modificado pois está compensado.");
                    }

                    #region Atributos

                    PS.Lib.DataField EX_CODEMPRESA = new PS.Lib.DataField("CODEMPRESA", null);
                    PS.Lib.DataField EX_IDEXTRATO = new PS.Lib.DataField("IDEXTRATO", null);
                    PS.Lib.DataField EX_CODFILIAL = new PS.Lib.DataField("CODFILIAL", null);
                    PS.Lib.DataField EX_CODCONTA = new PS.Lib.DataField("CODCONTA", null);
                    PS.Lib.DataField EX_NUMERODOCUMENTO = new PS.Lib.DataField("NUMERODOCUMENTO", null);
                    PS.Lib.DataField EX_DATA = new PS.Lib.DataField("DATA", null);
                    PS.Lib.DataField EX_VALOR = new PS.Lib.DataField("VALOR", null);
                    PS.Lib.DataField EX_HISTORICO = new PS.Lib.DataField("HISTORICO", null);
                    PS.Lib.DataField EX_COMPENSADO = new PS.Lib.DataField("COMPENSADO", null);
                    PS.Lib.DataField EX_DATACOMPENSACAO = new PS.Lib.DataField("DATACOMPENSACAO", null);
                    PS.Lib.DataField EX_TIPO = new PS.Lib.DataField("TIPO", null);
                    PS.Lib.DataField EX_CODCCUSTO = new PS.Lib.DataField("CODCCUSTO", null);

                    #endregion

                    #region Sets

                    EX_CODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
                    EX_IDEXTRATO.Valor = gb.RetornaDataFieldByCampo(objArrTrf, "IDEXTRATO").Valor;
                    EX_CODFILIAL.Valor = gb.RetornaDataFieldByCampo(objArr, "CODFILIALTRF").Valor;
                    EX_CODCONTA.Valor = gb.RetornaDataFieldByCampo(objArr, "CODCONTATRF").Valor;
                    EX_NUMERODOCUMENTO = gb.RetornaDataFieldByCampo(objArr, "NUMERODOCUMENTO");
                    EX_DATA = gb.RetornaDataFieldByCampo(objArr, "DATA");
                    EX_VALOR = gb.RetornaDataFieldByCampo(objArr, "VALOR");
                    EX_HISTORICO = gb.RetornaDataFieldByCampo(objArr, "HISTORICO");
                    EX_COMPENSADO = gb.RetornaDataFieldByCampo(objArr, "COMPENSADO");
                    EX_DATACOMPENSACAO = gb.RetornaDataFieldByCampo(objArr, "DATACOMPENSACAO");
                    EX_TIPO.Valor = 0;
                    EX_CODCCUSTO = gb.RetornaDataFieldByCampo(objArr, "CODCCUSTO");

                    #endregion

                    objArrTrf.Clear();
                    objArrTrf.Add(EX_CODEMPRESA);
                    objArrTrf.Add(EX_IDEXTRATO);
                    objArrTrf.Add(EX_CODFILIAL);
                    objArrTrf.Add(EX_CODCONTA);
                    objArrTrf.Add(EX_NUMERODOCUMENTO);
                    objArrTrf.Add(EX_DATA);
                    objArrTrf.Add(EX_VALOR);
                    objArrTrf.Add(EX_HISTORICO);
                    objArrTrf.Add(EX_COMPENSADO);
                    objArrTrf.Add(EX_DATACOMPENSACAO);
                    objArrTrf.Add(EX_TIPO);
                    objArrTrf.Add(EX_CODCCUSTO);

                    objArrTrf = this.EditRecord(objArrTrf);
                }
            }
            else
            {
                if (Flag)
                {
                    if (dbs.QueryFind("SELECT IDEXTRATOTRF FROM FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATO = ? AND NOT(IDEXTRATOTRF IS NULL)", dtCODEMPRESA.Valor, dtIDEXTRATO.Valor))
                    {
                        List<PS.Lib.DataField> objArrExc = new List<Lib.DataField>();
                        PS.Lib.DataField dtIDEXTRATOTRF = new Lib.DataField("IDEXTRATO", dbs.QueryValue(0, "SELECT IDEXTRATOTRF FROM FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATO = ?", dtCODEMPRESA.Valor, dtIDEXTRATO.Valor));

                        objArrExc.Add(dtCODEMPRESA);
                        objArrExc.Add(dtIDEXTRATOTRF);

                        this.DeleteRecordTransferencia(objArrExc);

                        for (int i = 0; i < objArr.Count; i++)
                        {
                            if (objArr[i].Field == "IDEXTRATOTRF")
                            {
                                objArr[i].Valor = null;
                            }
                        }
                    }
                }
            }

            objArr = base.EditRecord(objArr);

            return objArr;
        }

        public void DeleteRecordNaBaixa(List<PS.Lib.DataField> objArr)
        { 
            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtIDEXTRATO = gb.RetornaDataFieldByCampo(objArr, "IDEXTRATO");

            if (dbs.QueryFind("SELECT IDEXTRATO FROM FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATO = ?", dtCODEMPRESA.Valor, dtIDEXTRATO.Valor))
            {
                List<PS.Lib.DataField> objArr1 = gb.RetornaDataFieldByDataRow(dbs.QuerySelect("SELECT * FROM FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATO = ?", dtCODEMPRESA.Valor, dtIDEXTRATO.Valor).Rows[0]);

                PS.Lib.DataField dtNUMERODOCUMENTO = gb.RetornaDataFieldByCampo(objArr1, "NUMERODOCUMENTO");
                PS.Lib.DataField dtCOMPENSADO = gb.RetornaDataFieldByCampo(objArr1, "COMPENSADO");
                PS.Lib.DataField dtTIPO = gb.RetornaDataFieldByCampo(objArr1, "TIPO");

                if (Convert.ToInt32(dtCOMPENSADO.Valor) == 1)
                {
                    throw new Exception("Extrato [" + dtNUMERODOCUMENTO.Valor + "] não pode ser excluído pois está compensado.");
                }

                if (PossuiExtratoOrigem(objArr))
                {
                    throw new Exception("Extrato [" + dtNUMERODOCUMENTO.Valor + "] não pode ser excluído pois está relacionado a outro extrato.");
                }
            }

            base.DeleteRecord(objArr);
        }

        public void DeleteRecordTransferencia(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtIDEXTRATO = gb.RetornaDataFieldByCampo(objArr, "IDEXTRATO");

            if (dbs.QueryFind("SELECT IDEXTRATO FROM FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATO = ?", dtCODEMPRESA.Valor, dtIDEXTRATO.Valor))
            {
                List<PS.Lib.DataField> objArr1 = gb.RetornaDataFieldByDataRow(dbs.QuerySelect("SELECT * FROM FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATO = ?", dtCODEMPRESA.Valor, dtIDEXTRATO.Valor).Rows[0]);

                PS.Lib.DataField dtNUMERODOCUMENTO = gb.RetornaDataFieldByCampo(objArr1, "NUMERODOCUMENTO");
                PS.Lib.DataField dtCOMPENSADO = gb.RetornaDataFieldByCampo(objArr1, "COMPENSADO");

                if (Convert.ToInt32(dtCOMPENSADO.Valor) == 1)
                {
                    throw new Exception("Extrato [" + dtNUMERODOCUMENTO.Valor + "] não pode ser excluído pois está compensado.");
                }

                string sSql = @"UPDATE FEXTRATO SET IDEXTRATOTRF = NULL WHERE CODEMPRESA = ? AND IDEXTRATOTRF = ?";
                dbs.QueryExec(sSql, dtCODEMPRESA.Valor, dtIDEXTRATO.Valor); 
            }

            base.DeleteRecord(objArr);
        }

        public override void DeleteRecord(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtIDEXTRATO = gb.RetornaDataFieldByCampo(objArr, "IDEXTRATO");

            if (dbs.QueryFind("SELECT IDEXTRATO FROM FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATO = ?", dtCODEMPRESA.Valor, dtIDEXTRATO.Valor))
            {
                List<PS.Lib.DataField> objArr1 = gb.RetornaDataFieldByDataRow(dbs.QuerySelect("SELECT * FROM FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATO = ?", dtCODEMPRESA.Valor, dtIDEXTRATO.Valor).Rows[0]);

                PS.Lib.DataField dtNUMERODOCUMENTO = gb.RetornaDataFieldByCampo(objArr1, "NUMERODOCUMENTO");
                PS.Lib.DataField dtCOMPENSADO = gb.RetornaDataFieldByCampo(objArr1, "COMPENSADO");
                PS.Lib.DataField dtTIPO = gb.RetornaDataFieldByCampo(objArr1, "TIPO");

                if (Convert.ToInt32(dtCOMPENSADO.Valor) == 1)
                {
                    throw new Exception("Extrato [" + dtNUMERODOCUMENTO.Valor + "] não pode ser excluído pois está compensado.");
                }

                if (PossuiExtratoOrigem(objArr))
                {
                    throw new Exception("Extrato [" + dtNUMERODOCUMENTO.Valor + "] não pode ser excluído pois está relacionado a outro extrato.");
                }

                if (PossuiFinanceiroVinculado(objArr))
                {
                    throw new Exception("Extrato [" + dtNUMERODOCUMENTO.Valor + "] não pode ser excluído pois está relacionado a uma baixa.");
                }

                if (Convert.ToInt32(dtTIPO.Valor) == 2)
                {
                    List<PS.Lib.DataField> objArrExc = new List<Lib.DataField>();
                    PS.Lib.DataField dtIDEXTRATOTRF = new PS.Lib.DataField("IDEXTRATO", dbs.QueryValue(0, "SELECT IDEXTRATOTRF FROM FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATO = ?", dtCODEMPRESA.Valor, dtIDEXTRATO.Valor));

                    objArrExc.Add(dtCODEMPRESA);
                    objArrExc.Add(dtIDEXTRATOTRF);

                    this.DeleteRecordTransferencia(objArrExc);
                }
            }

            base.DeleteRecord(objArr);
        }

        public bool PossuiExtratoOrigem(List<PS.Lib.DataField> objArr)
        { 
            string sSql = string.Empty;

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtIDEXTRATO = gb.RetornaDataFieldByCampo(objArr, "IDEXTRATO");

            sSql = @"SELECT IDEXTRATO FROM FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATOTRF = ?";

            return dbs.QueryFind(sSql, dtCODEMPRESA.Valor, dtIDEXTRATO.Valor);            
        }

        public bool PossuiFinanceiroVinculado(List<PS.Lib.DataField> objArr)
        {
            string sSql = string.Empty;

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtIDEXTRATO = gb.RetornaDataFieldByCampo(objArr, "IDEXTRATO");

            sSql = @"SELECT CODLANCA FROM FLANCA WHERE CODEMPRESA = ? AND IDEXTRATO = ?";

            return dbs.QueryFind(sSql, dtCODEMPRESA.Valor, dtIDEXTRATO.Valor); 
        }

        public void CompensaExtrato(Support.FinExtCompPar finExtCompPar)
        {
            try
            {
                if (finExtCompPar == null)
                {
                    throw new Exception("Atenção. Nenhum extrato foi informado para compensação");
                }

                if (finExtCompPar.IdExtrato == null)
                {
                    throw new Exception("Atenção. Nenhum extrato foi informado para compensação");
                }

                foreach (int IdExtrato in finExtCompPar.IdExtrato)
                {
                    string sSql = string.Empty;

                    try
                    {
                        sSql = @"SELECT COMPENSADO FROM FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATO = ?";
                        int compensado = Convert.ToInt32(dbs.QueryValue(0, sSql, finExtCompPar.CodEmpresa, IdExtrato));
                        if (compensado == 1)
                        {
                            sSql = @"SELECT NUMERODOCUMENTO FROM FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATO = ?";
                            string numerodocumento = dbs.QueryValue(string.Empty, sSql, finExtCompPar.CodEmpresa, IdExtrato).ToString();
                            throw new Exception("Não foi possível compensar o extrato [" + numerodocumento + "] pois o mesmo já está compensado");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    try
                    {
                        dbs.Begin();

                        sSql = @"UPDATE FEXTRATO SET COMPENSADO = ?, DATACOMPENSACAO = ? WHERE CODEMPRESA = ? AND IDEXTRATO = ?";
                        dbs.QueryExec(sSql, 1, finExtCompPar.DataCompensacao, finExtCompPar.CodEmpresa, IdExtrato);

                        dbs.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbs.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CancelaCompensacaoExtrato(Support.FinExtCanCompPar finExtCanCompPar)
        {
            try
            {
                if (finExtCanCompPar == null)
                {
                    throw new Exception("Atenção. Nenhum extrato foi informado para cancelamento da compensação");
                }

                if (finExtCanCompPar.IdExtrato == null)
                {
                    throw new Exception("Atenção. Nenhum extrato foi informado para cancelamento da compensação");
                }

                foreach (int IdExtrato in finExtCanCompPar.IdExtrato)
                {
                    string sSql = string.Empty;

                    try
                    {
                        dbs.Begin();

                        sSql = @"UPDATE FEXTRATO SET COMPENSADO = ?, DATACOMPENSACAO = ? WHERE CODEMPRESA = ? AND IDEXTRATO = ?";
                        dbs.QueryExec(sSql, 0, null, finExtCanCompPar.CodEmpresa, IdExtrato);

                        dbs.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbs.Rollback();
                        throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
