using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartObjetoMedidor : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartObjetoMedidor()
        {
            this.TableName = "OOBJETOMEDIDOR";
            this.Keys = new string[] { "CODEMPRESA", "CODOBJETO", "DATA" };
            this.FormEditName = "PSPartObjetoMedidorEdit";
            this.PSPartData = new PSPartObjetoMedidorData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.SecurityID = "PSPartObjetoMedidor";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
