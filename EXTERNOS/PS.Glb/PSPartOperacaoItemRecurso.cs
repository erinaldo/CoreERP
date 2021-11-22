using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartOperacaoItemRecurso : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartOperacaoItemRecurso()
        {
            this.TableName = "GOPERITEMRECURSO";
            this.Keys = new string[] { "CODEMPRESA", "CODOPER", "NSEQITEM", "CODOPERADOR" };
            this.FormEditName = "PSPartOperacaoItemRecursoEdit";
            this.PSPartData = new PSPartOperacaoItemRecursoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.SecurityID = "PSPartOperacaoItemRecurso";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
