using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartBoletoData : PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public override string ReadView()
        {
            return @"SELECT FBOLETO.*,

( SELECT NOME FROM VCLIFOR WHERE CODEMPRESA = FBOLETO.CODEMPRESA AND CODCLIFOR = FBOLETO.CODCLIFOR ) CLIFOR,
( SELECT DESCRICAO FROM FCONTA WHERE CODEMPRESA = FBOLETO.CODEMPRESA AND CODCONTA = FBOLETO.CODCONTA ) CONTA,
( SELECT NOME FROM FTIPDOC WHERE CODEMPRESA = FBOLETO.CODEMPRESA AND CODTIPDOC = FBOLETO.CODTIPDOC ) TIPDOC,
( SELECT DESCRICAO FROM FCONVENIO WHERE CODEMPRESA = FBOLETO.CODEMPRESA AND CODCONVENIO = FBOLETO.CODCONVENIO ) CONVENIO,
( SELECT DESCRICAO FROM FBOLETOSTATUS WHERE IDBOLETOSTATUS = FBOLETO.IDBOLETOSTATUS ) REMESSASTATUS

FROM FBOLETO

WHERE ";
        }

        public override void ValidateRecord(List<PS.Lib.DataField> objArr)
        {
            objArr[0].Valor = objArr[0].Valor.ToString().Replace(",", ".");

            base.ValidateRecord(objArr);

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODLANCA = gb.RetornaDataFieldByCampo(objArr, "CODLANCA");
        }

        public override void DeleteRecord(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODLANCA = gb.RetornaDataFieldByCampo(objArr, "CODLANCA");
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FLANCA SET BOLETOGERADO = ? WHERE CODEMPRESA = ? AND CODLANCA =?", new object[] { "NÃO", dtCODEMPRESA.Valor, dtCODLANCA.Valor });
            base.DeleteRecord(objArr);

        }

    }
}
