using PS.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb
{
    public class PSPartConvenio : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartConvenio()
        {
            this.TableName = "FCONVENIO";
            this.Keys = new string[] { "CODEMPRESA", "CODCONVENIO" };
            this.FormEditName = "PSPartConvenioEdit";
            this.PSPartData = new PSPartBancoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.SecurityID = "PSPartConvenio";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODCONVENIO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DIGITOCONVENIO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODCONTA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CARTEIRA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("TIPOCARTEIRA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("COMREGISTRO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODIGOCEDENTE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DIGITOCEDENTE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("REGRANOSSONUMERO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("PROXIMOSEQUENCIAL", null, typeof(PS.Lib.WinForms.PSTextoBox)));            

            return objArr;
        }
    }
}
