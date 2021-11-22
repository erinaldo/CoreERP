using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartGerTarefa : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartGerTarefa()
        {
            this.TableName = "GJOB";
            this.Keys = new string[] { "CODEMPRESA", "IDJOB"};
            this.FormEditName = "PSPartGerTarefaEdit";
            this.PSPartData = new PSPartGerTarefaData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));

            this.Folder.Add(new PS.Lib.WinForms.Folder(new PSPartGerTarefaLog(), "CODEMPRESA", "IDJOB"));

            this.AllowDelete = false;

            this.SecurityID = "PSPartGerTarefa";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("IDJOB", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("ASSEMBLYNAME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("NAMESPACE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CLASSNAME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODUSUARIOCRIACAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DATACRIACAO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("EXECSEG", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("EXECTER", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("EXECQUA", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("EXECQUI", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("EXECSEX", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("EXECSAB", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("EXECDOM", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("EXECHOR", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));

            return objArr;
        }
    }
}
