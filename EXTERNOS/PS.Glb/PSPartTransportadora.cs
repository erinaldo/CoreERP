using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartTransportadora : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartTransportadora()
        {
            this.TableName = "VTRANSPORTADORA";
            this.Keys = new string[] { "CODEMPRESA", "CODTRANSPORTADORA" };
            this.FormEditName = "PSPartTransportadoraEdit";
            this.PSPartData = new PSPartTransportadoraData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));


            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODCIDADE", false));

            this.SecurityID = "PSPartTransportadora";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("CGCCPF", null, typeof(PS.Lib.WinForms.PSMaskedTextBox)));
            objArr.Add(new DataField("INSCRICAOESTADUAL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("FISICOJURIDICO", null, typeof(PS.Lib.WinForms.PSComboBox)));
            objArr.Add(new DataField("NOMEFANTASIA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTRANSPORTADORA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODPAIS", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODETD", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODCIDADE", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("BAIRRO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTIPOBAIRRO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("COMPLEMENTO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NUMERO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("RUA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODTIPORUA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CEP", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("HOMEPAGE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CONTATO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("EMAIL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("TELFAX", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("TELCELULAR", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("TELEFONE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            objArr.Add(new DataField("CCIDADE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CGCCPF", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("FISICOJURIDICO", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
