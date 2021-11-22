using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartCentroCusto : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartCentroCusto()
        {
            this.TableName = "GCENTROCUSTO";
            this.Keys = new string[] { "CODEMPRESA", "CODCCUSTO" };
            this.FormEditName = "PSPartCentroCustoEdit";
            this.PSPartData = new PSPartCentroCustoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("PERMITELANCAMENTO", 1));

            this.SecurityID = "PSPartCentroCusto";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODCCUSTO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("PERMITELANCAMENTO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSMemoBox)));
            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
