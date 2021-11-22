using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartOperItemComplData : PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

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
