using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartAtividadeProducao : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartAtividadeProducao()
        {
            this.TableName = "PATIVIDADE";
            this.Keys = new string[] { "CODEMPRESA", "CODATIVIDADE" };
            this.FormEditName = "PSPartAtividadeProducaoEdit";
            this.PSPartData = new PSPartAtividadeProducaoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.SecurityID = "PSPartAtividadeProducao";
            this.ModuleID = "PR";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODATIVIDADE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
