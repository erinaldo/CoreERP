using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartOperItemTributoData : PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private PS.Glb.Formula.Function function = new PS.Glb.Formula.Function();
        private PS.Lib.Interpretador interpreta = new PS.Lib.Interpretador();

        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODOPER,
NSEQITEM,
CODTRIBUTO,
BASECALCULO,
ALIQUOTA,
VALOR,
VALORICMSST, 
CODCST,
MODALIDADEBC,
REDUCAOBASEICMS,
CONVERT(BIT, EDITADO) EDITADO,
CENQ,
FATORMVA,
BCORIGINAL, 
REDUCAOBASEICMSST, 
PDIF,
VICMSDIF
FROM GOPERITEMTRIBUTO WHERE ";
        }

        public int VerificaSituacao(List<PS.Lib.DataField> objArr)
        {
            PSPartOperacaoData psPartOperacaoData = new PSPartOperacaoData();
            psPartOperacaoData._tablename = "GOPER";
            psPartOperacaoData._keys = new string[] { "CODEMPRESA", "CODOPER" };
            return psPartOperacaoData.VerificaSituacao(objArr);
        }

        public bool PossuiNFEstadual(int CodEmpresa, int CodOper)
        {
            PSPartOperacaoData psPartOperacaoData = new PSPartOperacaoData();
            psPartOperacaoData._tablename = "GOPER";
            psPartOperacaoData._keys = new string[] { "CODEMPRESA", "CODOPER" };
            return psPartOperacaoData.PossuiNFEstadual(CodEmpresa, CodOper, false);
        }

        public void CalculaOperacao(List<PS.Lib.DataField> objArr)
        {
            PSPartOperacaoData _psPartOperacaoData = new PSPartOperacaoData();
            _psPartOperacaoData._tablename = "GOPERCAO";
            _psPartOperacaoData._keys = new string[] { "CODEMPRESA", "CODOPER" };
            _psPartOperacaoData.CalculaOperacao(objArr);
        }

        public void InsertRecordItemOper(List<PS.Lib.DataField> objArr)
        {
            this.ValidateRecord(objArr);
            List<Lib.DataField> temp = base.InsertRecord(objArr);
        }

        public override void ValidateRecord(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

            base.ValidateRecord(objArr);
            int retorno = VerificaSituacao(objArr);
            if (retorno != 0 && retorno != 5)
            {
                throw new Exception("Operação não pode ser modificada.");
            }

            if (PossuiNFEstadual(Convert.ToInt32(dtCODEMPRESA.Valor), Convert.ToInt32(dtCODOPER.Valor)))
            {
                throw new Exception("Operação não pode ser modificada pois esta vinculada a uma NF-e.");
            }
        }

        public override List<PS.Lib.DataField> InsertRecord(List<PS.Lib.DataField> objArr)
        {
            List<Lib.DataField> temp = base.InsertRecord(objArr);

            this.CalculaOperacao(objArr);

            return temp;
        }

        public override List<PS.Lib.DataField> EditRecord(List<Lib.DataField> objArr)
        {
            PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");
            PS.Lib.DataField dfNSEQITEM = gb.RetornaDataFieldByCampo(objArr, "NSEQITEM");
            PS.Lib.DataField dfCODTRIBUTO = gb.RetornaDataFieldByCampo(objArr, "CODTRIBUTO");

            PS.Lib.DataField dfALIQUOTA = gb.RetornaDataFieldByCampo(objArr, "ALIQUOTA");
            PS.Lib.DataField dfVALOR = gb.RetornaDataFieldByCampo(objArr, "VALOR");
            PS.Lib.DataField dfCODCST = gb.RetornaDataFieldByCampo(objArr, "CODCST");
            PS.Lib.DataField dfBASECALCULO = gb.RetornaDataFieldByCampo(objArr, "BASECALCULO");
            PS.Lib.DataField dfMODALIDADEBC = gb.RetornaDataFieldByCampo(objArr, "MODALIDADEBC");
            PS.Lib.DataField dfREDUCAOBASEICMS = gb.RetornaDataFieldByCampo(objArr, "REDUCAOBASEICMS");
            PS.Lib.DataField dfCENQ = gb.RetornaDataFieldByCampo(objArr, "CENQ");

            string sSql = @"SELECT * FROM GOPERITEMTRIBUTO WHERE CODEMPRESA = ? AND CODOPER = ? AND NSEQITEM = ? AND CODTRIBUTO = ?";

            List<PS.Lib.DataField> temp = gb.RetornaDataFieldByDataRow(dbs.QuerySelect(sSql, dfCODEMPRESA.Valor, dfCODOPER.Valor, dfNSEQITEM.Valor, dfCODTRIBUTO.Valor).Rows[0]);

            PS.Lib.DataField dfALIQUOTAOLD = gb.RetornaDataFieldByCampo(temp, "ALIQUOTA");
            PS.Lib.DataField dfVALOROLD = gb.RetornaDataFieldByCampo(temp, "VALOR");
            PS.Lib.DataField dfCODCSTOLD = gb.RetornaDataFieldByCampo(temp, "CODCST");
            PS.Lib.DataField dfBASECALCULOOLD = gb.RetornaDataFieldByCampo(temp, "BASECALCULO");
            PS.Lib.DataField dfMODALIDADEBCOLD = gb.RetornaDataFieldByCampo(temp, "MODALIDADEBC");
            PS.Lib.DataField dfREDUCAOBASEICMSOLD = gb.RetornaDataFieldByCampo(temp, "REDUCAOBASEICMS");
            PS.Lib.DataField dfCENQOLD = gb.RetornaDataFieldByCampo(temp, "CENQ");

            bool Editado = false;
            if (dfALIQUOTA.Valor != dfALIQUOTAOLD.Valor)
                Editado = true;
            else
            {
                if (dfVALOR.Valor != dfVALOROLD.Valor)
                    Editado = true;
                else
                {
                    if (dfCODCST.Valor != dfCODCSTOLD.Valor)
                        Editado = true;
                    else
                    {
                        if (dfBASECALCULO.Valor != dfBASECALCULOOLD.Valor)
                            Editado = true;
                        else
                        {
                            if (dfMODALIDADEBC.Valor != dfMODALIDADEBCOLD.Valor)
                                Editado = true;
                            else
                            {
                                if (dfREDUCAOBASEICMS.Valor != dfREDUCAOBASEICMSOLD.Valor)
                                    Editado = true;
                                else
                                {
                                    if (dfCENQ.Valor != dfCENQOLD.Valor)
                                        Editado = true;
                                }
                            }
                        }
                    }
                }
            }

            if (Editado)
            {
                for (int i = 0; i < objArr.Count; i++)
                {
                    if (objArr[i].Field.Equals("EDITADO"))
                    {
                        objArr[i].Valor = 1;
                    }
                }
            }


            List<Lib.DataField> temp1 = base.EditRecord(objArr);

            this.CalculaOperacao(objArr);

            return temp1;
        }

        public void DeleteRecordItemOper(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

            if (VerificaSituacao(objArr) != 0)
            {
                throw new Exception("Operação não pode ser modificada.");
            }

            if (PossuiNFEstadual(Convert.ToInt32(dtCODEMPRESA.Valor), Convert.ToInt32(dtCODOPER.Valor)))
            {
                throw new Exception("Operação não pode ser modificada pois esta vinculada a uma NF-e.");
            }

            base.DeleteRecord(objArr);
        }

        public override void DeleteRecord(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODOPER = gb.RetornaDataFieldByCampo(objArr, "CODOPER");

            if (VerificaSituacao(objArr) != 0)
            {
                throw new Exception("Operação não pode ser modificada.");
            }

            if (PossuiNFEstadual(Convert.ToInt32(dtCODEMPRESA.Valor), Convert.ToInt32(dtCODOPER.Valor)))
            {
                throw new Exception("Operação não pode ser modificada pois esta vinculada a uma NF-e.");
            }

            base.DeleteRecord(objArr);

            this.CalculaOperacao(objArr);
        }

        public void DeleteRecordOper(List<PS.Lib.DataField> objArr)
        {
            if (VerificaSituacao(objArr) == 2)
            {
                base.DeleteRecord(objArr);
            }
        }
    }
}
