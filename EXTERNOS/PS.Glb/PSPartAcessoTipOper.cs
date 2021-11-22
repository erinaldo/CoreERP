using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartAcessoTipOper : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartAcessoTipOper()
        {
            this.TableName = "GPERFILTIPOPER";
            this.Keys = new string[] { "CODEMPRESA", "CODPERFIL", "CODTIPOPER" };
            this.FormEditName = "PSPartAcessoTipOperEdit";
            this.PSPartData = new PSPartAcessoTipOperData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODPERFIL", false));

            this.SecurityID = "PSPartAcessoTipOper";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODEMPRESA", null, typeof(System.Int32)));
            objArr.Add(new DataField("CODPERFIL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTIPOPER", null, typeof(PS.Lib.WinForms.PSLookup)));

            return objArr;
        }
    }
}
