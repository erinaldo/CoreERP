using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartOperador : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartOperador()
        {
            this.TableName = "VOPERADOR";
            this.Keys = new string[] { "CODEMPRESA", "CODOPERADOR" };
            this.FormEditName = "PSPartOperadorEdit";
            this.PSPartData = new PSPartOperadorData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.SecurityID = "PSPartOperador";
            this.ModuleID = "VR";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODOPERADOR", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODUSUARIO", null, typeof(PS.Lib.WinForms.PSLookup)));

            return objArr;
        }
    }
}
