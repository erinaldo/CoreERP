using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartNFEstadualHistorico : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartNFEstadualHistorico()
        {
            this.TableName = "GNFESTADUALHISTORICO";
            this.Keys = new string[] { "CODEMPRESA", "CODOPER", "IDHISTORICO" };
            this.FormEditName = "PSPartNFEstadualHistoricoEdit";
            this.PSPartData = new PSPartNFEstadualHistoricoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.AllowDelete = false;
            this.AllowInsert = false;

            this.SecurityID = "PSPartNFEstadualHistorico";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("OBSERVACAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODUSUARIO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DATA", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("IDHISTORICO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODOPER", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
