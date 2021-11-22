using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartNFEstadualEvento : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartNFEstadualEvento()
        {
            this.TableName = "GNFESTADUALEVENTO";
            this.Keys = new string[] { "CODEMPRESA", "CODOPER", "IDEVENTO" };
            this.FormEditName = "PSPartNFEstadualEventoEdit";
            this.PSPartData = new PSPartNFEstadualEventoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.AllowDelete = false;
            this.AllowInsert = false;
            this.AllowSave = false;

            this.SecurityID = "PSPartNFEstadualEvento";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            

            return objArr;
        }
    }
}
