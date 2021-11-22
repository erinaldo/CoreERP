using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartRegraIPI : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartRegraIPI()
        {
            this.TableName = "VREGRAIPI";
            this.Keys = new string[] { "CODEMPRESA", "IDREGRA" };
            this.FormEditName = "PSPartRegraIPIEdit";
            this.PSPartData = new PSPartRegraIPIData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));

            this.SecurityID = "PSPartRegraIPI";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("IDREGRA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("CODCSTENT", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODCSTSAI", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CENQ", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
