using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartNFERegCab : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartNFERegCab()
        {
            this.TableName = "NREGCAB";
            this.Keys = new string[] { "CODEMPRESA", "IDREGCAB" };
            this.FormEditName = "PSPartNFERegCabEdit";
            this.PSPartData = new PSPartNFERegCabData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.PSPartApp.Add(new PS.Glb.PSPartNFERegImpApp());

            this.SecurityID = "PSPartNFERegCab";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
