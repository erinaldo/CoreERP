using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartOrdemProducaoData : PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private PS.Glb.Formula.Function function = new PS.Glb.Formula.Function();
        private PS.Lib.Interpretador interpreta = new PS.Lib.Interpretador();
        private PS.Lib.Valida vl = new PS.Lib.Valida();

        public string GeraCodigo()
        {
            System.Data.DataRow PARAMVAREJO = gb.RetornaParametrosVarejo();

            string novoNumStr = "";

            #region Ordem Produção

            if (int.Parse(PARAMVAREJO["CODORDEMUSANUMEROSEQ"].ToString()) == 1)
            {
                string mask = PARAMVAREJO["CODORDEMMASKNUMEROSEQ"].ToString();
                string ultimo = PARAMVAREJO["CODORDEMULTIMONUMERO"].ToString();

                int num = 0;
                string str = "";

                for (int i = 0; i < mask.Length; i++)
                {
                    if (mask[i] == '?')
                    {
                        num++;
                    }
                    else
                    {
                        str = string.Concat(str, mask[i]);
                    }
                }

                string ultimoNum = "";
                int novoNum = 0;

                if (ultimo == "")
                {
                    ultimo = ultimoNum.PadLeft(num, '0');
                }
                else
                {
                    ultimo = ultimo.Substring(str.Length, num);
                }

                novoNum = int.Parse(ultimo) + 1;
                novoNumStr = string.Concat(str, novoNum.ToString().PadLeft(num, '0'));

                string sSql = @"UPDATE VPARAMETROS SET CODORDEMULTIMONUMERO = ? WHERE CODEMPRESA = ?";

                dbs.QueryExec(sSql, novoNumStr, PS.Lib.Contexto.Session.Empresa.CodEmpresa);
            }

            #endregion

            return novoNumStr;
        }

        public override void ValidateRecord(List<PS.Lib.DataField> objArr)
        {
            PS.Lib.DataField dtCODORDEM = gb.RetornaDataFieldByCampo(objArr, "CODORDEM");

            #region Gera Código Ordem Produção

            if (dtCODORDEM.Valor == null)
            {
                dtCODORDEM.Valor = GeraCodigo();

                for (int i = 0; i < objArr.Count; i++)
                {
                    if (objArr[i].Field == "CODORDEM")
                    {
                        objArr[i].Valor = dtCODORDEM.Valor;
                    }
                }
            }

            #endregion

            base.ValidateRecord(objArr);
        }
    }
}
