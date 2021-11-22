using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartRedeCartao : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartRedeCartao()
        {
            this.TableName = "VREDECARTAO";
            this.Keys = new string[] { "CODEMPRESA", "CODREDECARTAO" };
            this.FormEditName = "PSPartRedeCartaoEdit";
            this.PSPartData = new PSPartRedeCartaoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.SecurityID = "PSPartRedeCartao";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
