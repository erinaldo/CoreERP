using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartTipoTransporte : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartTipoTransporte()
        {
            this.TableName = "VTIPOTRANSPORTE";
            this.Keys = new string[] { "CODTIPOTRANSPORTE", "CODEMPRESA" };
            this.FormEditName = "PSPartTipoTransporteEdit";
            this.PSPartData = new PSPartTipoTransporteData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));


            this.SecurityID = "PSPartTipoTransporte";
            this.ModuleID = "PG";
        }
        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTRANSPORTADORA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            return objArr;
        }
    }
}
