using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb.Formula
{
    public class PSPartFormula : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartFormula()
        {
            this.TableName = "GFORMULA";
            this.Keys = new string[] { "CODEMPRESA", "CODFORMULA" };
            this.FormEditName = "PSPartFormulaEdit";
            this.PSPartData = new PSPartFormulaData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("TEXTO", false));

            this.PSPartApp.Add(new PSPartFormulaApp());

            this.SecurityID = "PSPartFormula";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODFORMULA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            objArr.Add(new DataField("TEXTO", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
