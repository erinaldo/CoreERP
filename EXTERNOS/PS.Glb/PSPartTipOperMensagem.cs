using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartTipOperMensagem : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartTipOperMensagem()
        {
            this.TableName = "GOPERMENSAGEM";
            this.Keys = new string[] { "CODEMPRESA", "CODTIPOPER", "CODMENSAGEM" };
            this.FormEditName = "PSPartTipOperMensagemEdit";
            this.PSPartData = new PSPartTipOperMensagemData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODTIPOPER", false));

            this.SecurityID = "PSPartTipOperMensagem";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
