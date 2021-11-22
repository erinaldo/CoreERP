using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartBoletoStatus : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartBoletoStatus()
        {
            this.TableName = "FBOLETOSTATUS";
            this.Keys = new string[] { "IDBOLETOSTATUS" };
            //this.FormEditName = "PSPartBoletoStatusEdit";
            this.PSPartData = new PSPartBoletoStatusData();

           // this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            //this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.SecurityID = "PSPartBoletoStatus";
            this.ModuleID = "PG";
        }
        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("IDBOLETOSTATUS", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
