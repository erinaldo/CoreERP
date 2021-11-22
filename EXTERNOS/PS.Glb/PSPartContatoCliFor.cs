using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartContatoCliFor : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartContatoCliFor()
        {
            this.TableName = "VCLIFORCONTATO";
            this.Keys = new string[] { "CODEMPRESA", "CODCLIFOR", "CODCONTATO" };
            this.FormEditName = "PSPartContatoCliForEdit";
            this.PSPartData = new PSPartContatoCliForData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODCLIFOR", false));

            this.SecurityID = "PSPartContatoCliFor";
            this.ModuleID = "VR";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();


            objArr.Add(new DataField("CODEMPRESA", null, typeof(System.Int32)));
            objArr.Add(new DataField("CODCLIFOR", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CPF", null, typeof(PS.Lib.WinForms.PSMaskedTextBox)));
            objArr.Add(new DataField("OREMISSOR", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NUMERORG", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DATANASCIMENTO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("TELEFONE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("EMAIL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODCONTATO", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
