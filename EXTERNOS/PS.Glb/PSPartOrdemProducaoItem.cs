using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartOrdemProducaoItem : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartOrdemProducaoItem()
        {
            this.TableName = "PORDEMPRODUCAOITEM";
            this.Keys = new string[] { "CODEMPRESA", "CODORDEM", "NSEQITEM" };
            this.FormEditName = "PSPartOrdemProducaoItemEdit";
            this.PSPartData = new PSPartOrdemProducaoItemData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODESTRUTURA", false));
            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODORDEM", false));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("QUANTIDADE",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,#########0.000000000",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.SecurityID = "PSPartOrdemProducaoItem";
            this.ModuleID = "PR";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
