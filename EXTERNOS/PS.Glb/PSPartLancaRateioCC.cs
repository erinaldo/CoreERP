using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartLancaRateioCC : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartLancaRateioCC()
        {
            this.TableName = "FLANCARATEIOCC";
            this.Keys = new string[] { "CODEMPRESA", "CODLANCA", "CODCCUSTO" };
            this.FormEditName = "PSPartLancaRateioCCEdit";
            this.PSPartData = new PSPartLancaRateioCCData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODLANCA", false));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("PERCENTUAL",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VALOR",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.SecurityID = "PSPartLancaRateioCC";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODEMPRESA", null, typeof(System.Int32)));
            objArr.Add(new DataField("CODLANCA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("VALOR", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("PERCENTUAL", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CODCCUSTO", null, typeof(PS.Lib.WinForms.PSLookup)));

            return objArr;
        }
    }
}

