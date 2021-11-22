using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartCaixa : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartCaixa()
        {
            this.TableName = "VCAIXA";
            this.Keys = new string[] { "CODEMPRESA", "CODCAIXA" };
            this.FormEditName = "PSPartCaixaEdit";
            this.PSPartData = new PSPartCaixaData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.SecurityID = "PSPartCaixa";
            this.ModuleID = "VR";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODIMPRESSORA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODFILIAL", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODCAIXA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTIPOPERPDV", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("SOLCLIPDV", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("TIMEOUTPDV", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("MOSTRAPRODUTOPDV", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("USAENDERECO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("USANOME", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("USACGCCPF", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
