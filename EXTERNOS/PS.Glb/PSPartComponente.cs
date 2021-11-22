using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartComponente : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartComponente()
        {
            this.TableName = "PCOMPONENTE";
            this.Keys = new string[] { "CODEMPRESA", "CODESTRUTURA", "CODPRODUTO", "IDCOMPONENTE" };
            this.FormEditName = "PSPartComponenteEdit";
            this.PSPartData = new PSPartComponenteData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODESTRUTURA", false));
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("IDCOMPONENTE", false));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("QUANTIDADE",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,#########0.000000000",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.SecurityID = "PSPartComponente";
            this.ModuleID = "PR";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODEMPRESA", null, typeof(System.Int32)));
            objArr.Add(new DataField("CODESTRUTURA", null, typeof(System.String)));
            objArr.Add(new DataField("QUANTIDADE", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CODPRODUTO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("IDCOMPONENTE", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}