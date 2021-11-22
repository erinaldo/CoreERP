using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartFormaPgto : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartFormaPgto()
        {
            this.TableName = "VFORMAPGTO";
            this.Keys = new string[] { "CODEMPRESA", "CODFORMA" };
            this.FormEditName = "PSPartFormaPgtoEdit";
            this.PSPartData = new PSPartFormaPgtoData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));

            this.SecurityID = "PSPartFormaPgto";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("TAXAADM", null, typeof(PS.Lib.WinForms.PSMoedaBox)));
            objArr.Add(new DataField("CODREDECARTAO", null, typeof(PS.Lib.WinForms.PSLookup)));
            objArr.Add(new DataField("TIPOTRANSACAO", null, typeof(PS.Lib.WinForms.PSComboBox)));
            objArr.Add(new DataField("TIPO", null, typeof(PS.Lib.WinForms.PSComboBox)));
            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODFORMA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
