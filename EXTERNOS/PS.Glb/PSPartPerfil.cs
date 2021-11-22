using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartPerfil : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartPerfil()
        {
            this.TableName = "GPERFIL";
            this.Keys = new string[] { "CODPERFIL" };
            this.FormEditName = "PSPartPerfilEdit";
            this.PSPartData = new PSPartPerfilData();

            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("ATIVO", 1));

            this.MasterDetail.Add(new PS.Lib.WinForms.MasterDetail(new PSPartAcessoMenu(), "CODEMPRESA", "CODPERFIL"));
            this.MasterDetail.Add(new PS.Lib.WinForms.MasterDetail(new PSPartUsuarioPerfil(), "CODEMPRESA", "CODPERFIL"));
            this.MasterDetail.Add(new PS.Lib.WinForms.MasterDetail(new PSPartAcessoTipOper(), "CODEMPRESA", "CODPERFIL"));

            this.SecurityID = "PSPartPerfil";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("ATIVO", null, typeof(PS.Lib.WinForms.PSCheckBox)));
            objArr.Add(new DataField("NOME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("CODPERFIL", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
