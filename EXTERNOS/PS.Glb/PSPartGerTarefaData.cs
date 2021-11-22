using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartGerTarefaData : PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private PS.Lib.Global gb = new PS.Lib.Global();

        public override List<PS.Lib.DataField> InsertRecord(List<PS.Lib.DataField> objArr)
        {
            //PS.Lib.DataField objCODEMPRESA = new Lib.DataField("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.codEmpresa);
            PS.Lib.DataField objCODUSUARIOCRIACAO = new Lib.DataField("CODUSUARIOCRIACAO", PS.Lib.Contexto.Session.CodUsuario);
            PS.Lib.DataField objDATACRIACAO = new Lib.DataField("DATACRIACAO", DateTime.Now);

            //objArr.Add(objCODEMPRESA);
            objArr.Add(objCODUSUARIOCRIACAO);
            objArr.Add(objDATACRIACAO);

            return base.InsertRecord(objArr);
        }
    }
}
