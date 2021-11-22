using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartOrdemProducao : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartOrdemProducao()
        {
            this.TableName = "PORDEMPRODUCAO";
            this.Keys = new string[] { "CODEMPRESA", "CODORDEM" };
            this.FormEditName = "PSPartOrdemProducaoEdit";
            this.PSPartData = new PSPartOrdemProducaoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.MasterDetail.Add(new PS.Lib.WinForms.MasterDetail(new PSPartOrdemProducaoItem(), true, "CODEMPRESA", "CODORDEM"));

            this.PSPartApp.Add(new PS.Glb.PSPartOrdemProducaoAndamentoApp());

            this.SecurityID = "PSPartOrdemProducao";
            this.ModuleID = "PR";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("OBSERVACAO", null, typeof(PS.Lib.WinForms.PSMemoBox)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODORDEM", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODUSUARIOCRIACAO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODSTATUS", null, typeof(PS.Lib.WinForms.PSComboBox)));
            objArr.Add(new DataField("DATAHORACRIACAO", null, typeof(PS.Lib.WinForms.PSDateBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}

