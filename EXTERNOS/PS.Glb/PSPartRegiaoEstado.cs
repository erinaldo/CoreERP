using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartRegiaoEstado : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartRegiaoEstado()
        {
            this.TableName = "VREGIAOESTADO";
            this.Keys = new string[] { "CODEMPRESA", "CODREGIAO", "CODETD" };
            this.FormEditName = "PSPartRegiaoEstadoEdit";
            this.PSPartData = new PSPartRegiaoEstadoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODREGIAO", false));

            this.SecurityID = "PSPartRegiaoEstado";
            this.ModuleID = "VR";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODEMPRESA", null, typeof(System.Int32)));
            objArr.Add(new DataField("CODREGIAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODETD", null, typeof(PS.Lib.WinForms.PSLookup)));

            return objArr;
        }
    }
}
