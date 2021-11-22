using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartEstrutura : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartEstrutura()
        {
            this.TableName = "PESTRUTURA";
            this.Keys = new string[] { "CODEMPRESA", "CODESTRUTURA" };
            this.FormEditName = "PSPartEstruturaEdit";
            this.PSPartData = new PSPartEstruturaData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));

            this.Folder.Add(new PS.Lib.WinForms.Folder(new PSPartComponente(), true, "CODEMPRESA", "CODESTRUTURA" ));
            this.Folder.Add(new PS.Lib.WinForms.Folder(new PSPartEstruturaAtividade(), true, "CODEMPRESA", "CODESTRUTURA"));

            this.SecurityID = "PSPartEstrutura";
            this.ModuleID = "PR";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("CODPRODUTO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("CODESTRUTURA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}