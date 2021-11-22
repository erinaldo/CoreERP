using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartMoeda : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartMoeda()
        {
            this.TableName = "VMOEDA";
            this.Keys = new string[] { "CODEMPRESA", "CODMOEDA" };
            this.FormEditName = "PSPartMoedaEdit";
            this.PSPartData = new PSPartMoedaData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.SecurityID = "PSPartMoeda";
            this.ModuleID = "VR";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODMOEDA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
