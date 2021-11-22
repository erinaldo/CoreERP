using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartUsuario : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartUsuario()
        {
            this.TableName = "GUSUARIO";
            this.Keys = new string[] { "CODUSUARIO" };
            this.FormEditName = "PSPartUsuarioEdit";
            this.PSPartData = new PSPartUsuarioData();

            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("SENHA", false));

            this.SecurityID = "PSPartUsuario";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("EMAIL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("SENHA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DTEXPIRACAO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODUSUARIO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NUNCAEXPIRA", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("ULTIMOLOGIN", null, typeof(PS.Lib.WinForms.PSDateBox)));

            return objArr;
        }
    }
}
