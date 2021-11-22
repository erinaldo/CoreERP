using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartTipoTributo : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartTipoTributo()
        {
            this.TableName = "VTIPOTRIBUTO";
            this.Keys = new string[] { "CODEMPRESA", "CODTIPOTRIBUTO" };
            this.FormEditName = "PSPartTipoTributoEdit";
            this.PSPartData = new PSPartTipoTributoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));

            this.SecurityID = "PSPartTipoTributo";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("TIPOALIQUOTA", null, typeof(PS.Lib.WinForms.PSComboBox)));
            objArr.Add(new DataField("DTINIVIGENCIA", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DTFIMVIGENCIA", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("PERIODICIDADE", null, typeof(PS.Lib.WinForms.PSComboBox)));
            objArr.Add(new DataField("ABRANGENCIA", null, typeof(PS.Lib.WinForms.PSComboBox)));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTIPOTRIBUTO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
