using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartFiscalImp : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartFiscalImp()
        {
            this.TableName = "VFISCALIMP";
            this.Keys = new string[] { "CODEMPRESA", "CODIMPRESSORA" };
            this.FormEditName = "PSPartFiscalImpEdit";
            this.PSPartData = new PSPartFiscalImpData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));

            this.SecurityID = "PSPartFiscalImp";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("MODELO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("MARCA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODIMPRESSORA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
