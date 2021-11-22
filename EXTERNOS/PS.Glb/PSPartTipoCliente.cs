using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartTipoCliente : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartTipoCliente()
        {
            this.TableName = "VTIPOCLIENTE";
            this.Keys = new string[] { "IDTIPOCLIENTE"};
            this.FormEditName = "PSPartTipoClienteEdit";
            this.PSPartData = new PSPartTipoClienteData();
            this.SecurityID = "PSPartTipoCliente";
            this.ModuleID = "PG";
        }
        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("IDTIPOCLIENTE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
