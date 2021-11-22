using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib
{
    public class Empresa
    {
        public int CodEmpresa { get; set; }
        public string NomeFantasia { get; set; }
        public string Nome { get; set; }
        public string CNPJCPF { get; set; }
        public string InscricaoEstadual { get; set; }
        public string CodControle { get; set; }
        public string CodChave1 { get; set; }
        public string CodChave2 { get; set; }

        private Data.DBS dbs;

        public void GetPerfilList()
        {
            dbs = new Data.DBS();

            System.Data.DataTable dt = new System.Data.DataTable();

            dt = dbs.QuerySelect("SELECT CODPERFIL FROM GUSUARIOPERFIL WHERE CODEMPRESA = ? AND CODUSUARIO = ? ", this.CodEmpresa, Contexto.Session.CodUsuario);

            if (dt.Rows.Count > 0)
            {
                string[] arr = new string[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    arr[i] = dt.Rows[i]["CODPERFIL"].ToString().Trim();
                }

                Contexto.Session.CodPerfil = arr;
            }
        }

        public bool VaidateCod()
        { 
            bool Flag = false;

            Security seg = new Security();
            if (seg.ValidaControle(this.CodControle, this.CNPJCPF, this.CodChave1, this.CodChave2))
                Flag = true;
            else
                Flag = false;

            return Flag;
        }

        public bool ImportCod()
        {
            bool Flag = false;
            int rows = 0;

            dbs = new Data.DBS();

            rows = dbs.QueryExec("INSERT INTO GEMPRESA (CODEMPRESA, NOMEFANTASIA, NOME, CGCCPF, INSCRICAOESTADUAL, CODCONTROLE, CODCHAVE1, CODCHAVE2) VALUES(? ,?, ?, ?, ?, ?, ?, ?)", this.CodEmpresa, this.NomeFantasia, this.Nome, this.CNPJCPF, this.InscricaoEstadual, this.CodControle, this.CodChave1, this.CodChave2);

            if (rows > 0)
                Flag = true;
            else
                Flag = false;

            return Flag;                    
        }
    }
}
