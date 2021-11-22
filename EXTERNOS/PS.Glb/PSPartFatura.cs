using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartFatura : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartFatura()
        {
            this.TableName = "FFATURA";
            this.Keys = new string[] { "CODEMPRESA", "CODFATURA" };
            this.FormEditName = "PSPartFaturaEdit";
            this.PSPartData = new PSPartFaturaData();

            this.AllowInsert = false;
            this.AllowEdit = false;

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("VLORIGINAL",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##0.00",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CTIPOPAGREC",
                                                                                "",
                                                                                PS.Lib.DataGridColumnType.Image,
                                                                                1,
                                                                                "TIPOPAGREC",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter,
                                                                                new PS.Lib.ImageProperties[] { new PS.Lib.ImageProperties(0, "Pagar", Properties.Resources.img_lanpag),
                                                                                                                    new PS.Lib.ImageProperties(1, "Receber", Properties.Resources.img_lanrec)}));

            this.SecurityID = "PSPartFatura";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODFATURA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("TIPOPAGREC", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NUMERO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODCLIFOR", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODFILIAL", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("DATAEMISSAO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DATAVENCIMENTO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DATABAIXA", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DATAPREVBAIXA", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("OBSERVACAO", null, typeof(PS.Lib.WinForms.PSMemoBox)));
            objArr.Add(new DataField("CODMOEDA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("VLORIGINAL", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CODCONTA", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODTIPDOC", null, typeof(PS.Lib.WinForms.PSLookup)));




            return objArr;
        }
    }
}
