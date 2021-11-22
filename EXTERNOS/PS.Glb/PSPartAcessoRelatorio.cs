using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartAcessoRelatorio : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartAcessoRelatorio()
        {
            this.TableName = "GREPORTPERFIL";
            this.Keys = new string[] { "CODEMPRESA", "CODREPORT", "CODPERFIL" };
            this.FormEditName = "PSPartAcessoRelatorioEdit";
            this.PSPartData = new PSPartAcessoRelatorioData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("CODREPORT", false));

            this.SecurityID = "PSPartAcessoRelatorio";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
