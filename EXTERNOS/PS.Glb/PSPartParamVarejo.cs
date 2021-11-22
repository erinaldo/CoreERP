using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PSPartParamVarejo : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartParamVarejo()
        {
            this.TableName = "VPARAMETROS";
            this.Keys = new string[] { "CODEMPRESA" };
            this.FormEditName = "PSPartParamVarejoEdit";
            this.PSPartData = new PSPartParamVarejoData();

            this.AllowInsert = false;
            this.AllowDelete = false;

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.DefaultCustomDataColumns.Add(new CustomDataColumn("TEXTOEMAILNFE", false));

            this.SecurityID = "PSPartParamVarejo";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODEMPRESA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("IDPARAMETRO", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            //objArr.Add(new DataField("CLIFORCONSISTECGCCPF", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("CLIFORMASKNUMEROSEQ", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("CLIFORULTIMONUMERO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("CLIFORUSANUMEROSEQ", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("CODMOEDAPADRAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("CODORDEMMASKNUMEROSEQ", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("CODORDEMULTIMONUMERO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("CODORDEMUSANUMEROSEQ", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("CONTROLALIMITECREDITO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("EMAILAUTENTICA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("EMAILHOST", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("EMAILPORTA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("EMAILREMETENTE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("EMAILSENHA", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("EMAILUSASSL", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("EMAILUSUARIO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("IDPARAMETRO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("MASKCENTROCUSTO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("MASKDEPARTAMENTO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("MASKNATUREZAORCAMENTO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("MASKNUMEROSEQ", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("MASKPRODSERV", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("PRDTEXTOPRECO1", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("PRDTEXTOPRECO2", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("PRDTEXTOPRECO3", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("PRDTEXTOPRECO4", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("PRDTEXTOPRECO5", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("PRDUSAPRECO1", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("PRDUSAPRECO2", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("PRDUSAPRECO3", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("PRDUSAPRECO4", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("PRDUSAPRECO5", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("TEXTOEMAILNFE", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("VALIDATEDATABASENAME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("VALIDATEPASSWORD", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("VALIDATESERVERNAME", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            //objArr.Add(new DataField("VALIDATEUSERNAME", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
