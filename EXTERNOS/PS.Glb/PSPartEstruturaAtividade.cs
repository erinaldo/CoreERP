using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartEstruturaAtividade : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartEstruturaAtividade()
        {
            this.TableName = "PESTRUTURAATIVIDADE";
            this.Keys = new string[] { "CODEMPRESA", "CODESTRUTURA", "CODATIVIDADE" };
            this.FormEditName = "PSPartEstruturaAtividadeEdit";
            this.PSPartData = new PSPartEstruturaAtividadeData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("TEMPOGASTO",
                                                                                PS.Lib.DataGridColumnType.None,
                                                                                "###,###,##",
                                                                                System.Windows.Forms.DataGridViewContentAlignment.MiddleRight));


            this.SecurityID = "PSPartEstruturaAtividade";
            this.ModuleID = "PR";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODEMPRESA", null, typeof(System.Int32)));
            objArr.Add(new DataField("CODESTRUTURA", null, typeof(System.String)));
            objArr.Add(new DataField("TEMPOGASTO", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CODATIVIDADE", null, typeof(PS.Lib.WinForms.PSLookup)));

            return objArr;
        }
    }
}

