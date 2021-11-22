using System;
using System.Collections.Generic;
using System.Text;
using PS.Lib;

namespace PS.Glb
{
    public class PsPartRepreUsuario : PS.Lib.WinForms.PSPartGridView
    {
        public PsPartRepreUsuario()
        {
            this.TableName = "VREPREUSUARIO";
            this.Keys = new string[] { "CODEMPRESA", "CODREPRE", "CODUSUARIO" };
            this.FormEditName = "PsPartRepreUsuarioEdit";
            this.PSPartData = new PsPartRepreUsuarioData();

            this.DefaultFilter.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));
            this.DefaultFilterLookup.Add(new PS.Lib.PSFilter("CODEMPRESA", PS.Lib.Contexto.Session.Empresa.CodEmpresa));

            this.SecurityID = "PsPartRepreUsuario";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            return objArr;
        }
    }
}
