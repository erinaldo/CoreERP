using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartGerTarefaLog : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartGerTarefaLog()
        {
            this.TableName = "GJOBEXEC";
            this.Keys = new string[] { "CODEMPRESA", "IDJOB", "NSEQ" };
            this.FormEditName = "PSPartGerTarefaLogEdit";
            this.PSPartData = new PSPartGerTarefaLogData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new PS.Lib.CustomDataColumn("LOGEXECUCAO", false));

            this.AllowInsert = false;
            this.AllowDelete = false;
            this.AllowSave = false;

            this.SecurityID = "PSPartGerTarefaLog";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("IDJOB", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NSEQ", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CODUSUARIOEXECUCAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DATAHORAEXECINICIAL", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("DATAHORAEXECFINAL", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("LOGEXECUCAO", null, typeof(PS.Lib.WinForms.PSMemoBox)));

            return objArr;
        }
    }
}
