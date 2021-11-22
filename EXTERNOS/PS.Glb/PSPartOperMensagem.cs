using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartOperMensagem : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartOperMensagem()
        {
            this.TableName = "VOPERMENSAGEM";
            this.Keys = new string[] { "CODEMPRESA", "CODMENSAGEM" };
            this.FormEditName = "PSPartOperMensagemEdit";
            this.PSPartData = new PSPartOperMensagemData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.SecurityID = "PSPartOperMensagem";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODFORMULAMENSAGEM", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("MENSAGEM", null, typeof(PS.Lib.WinForms.PSMemoBox)));
            objArr.Add(new DataField("CODMENSAGEM", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
