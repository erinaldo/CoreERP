using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartDepartamentoData : PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODFILIAL,
CODDEPTO,
NOME,
CONVERT(BIT, ATIVO) ATIVO
FROM GDEPARTAMENTO WHERE ";
        }

        public override void ValidateRecord(List<PS.Lib.DataField> objArr)
        {
            base.ValidateRecord(objArr);

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODFILIAL = gb.RetornaDataFieldByCampo(objArr, "CODFILIAL");
            PS.Lib.DataField dtCODDEPTO = gb.RetornaDataFieldByCampo(objArr, "CODDEPTO");

            #region Valida Máscara

            System.Data.DataRow PARAMVAREJO = gb.RetornaParametrosVarejo();
            if (PARAMVAREJO == null)
            {
                throw new Exception("Não foi possível carregar os parâmetros do módulo.");
            }
            else
            {
                string mask = (PARAMVAREJO["MASKDEPARTAMENTO"] == DBNull.Value) ? string.Empty : PARAMVAREJO["MASKDEPARTAMENTO"].ToString();
                if (mask != string.Empty)
                {
                    string CodDepartamento = dtCODDEPTO.Valor.ToString();
                    if (CodDepartamento.Length <= mask.Length)
                    {
                        for (int i = 0; i < CodDepartamento.Length; i++)
                        {
                            if (PS.Lib.Utils.MascaraCaracterValido(CodDepartamento[i], mask, i))
                            {
                                if (i == (CodDepartamento.Length - 1) && CodDepartamento[i] == '.')
                                {
                                    throw new Exception("O código do departamento não pode terminar com o caracter separador da máscara parametrizada.");
                                }
                            }
                            else
                            {
                                throw new Exception("O código do departamento não está consistente com a máscara parametrizada.");
                            }
                        }

                        if (CodDepartamento.Length < mask.Length)
                        {
                            if (mask.Substring(CodDepartamento.Length, 1) != ".")
                            {
                                throw new Exception("O código do departamento não está consistente com a máscara parametrizada.");
                            }
                        }

                        string CodDepartamentoPai = PS.Lib.Utils.MascaraNivelAnterior(CodDepartamento);
                        if (!string.IsNullOrEmpty(CodDepartamentoPai))
                        {
                            string sSql = @"SELECT CODDEPTO FROM GDEPARTAMENTO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODDEPTO = ?";
                            if (!dbs.QueryFind(sSql, dtCODEMPRESA.Valor, dtCODFILIAL.Valor, CodDepartamentoPai))
                            {
                                throw new Exception("O código do departamento informado não possui nível anterior.");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("O código do departamento não pode ser maior que a máscara parametrizada.");
                    }
                }
            }

            #endregion
        }
    }
}
