using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartRegCaixa : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartRegCaixa()
        {
            this.TableName = "VREGCAIXA";
            this.Keys = new string[] { "CODEMPRESA", "CODCAIXA", "DATAABERTURA" };
            this.FormEditName = "PSPartRegCaixaEdit";
            this.PSPartData = new PSPartRegCaixaData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VALORABERTURA",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VALORFECHAMENTO",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.SecurityID = "PSPartRegCaixa";
            this.ModuleID = "VR";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODFILIAL", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODOPERADOR", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("VALORFECHAMENTO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("DATAFECHAMENTO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("VALORABERTURA", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("DATAABERTURA", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("CODCAIXA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
