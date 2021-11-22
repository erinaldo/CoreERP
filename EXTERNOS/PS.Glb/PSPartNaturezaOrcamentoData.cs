using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Glb
{
    public class PSPartNaturezaOrcamentoData: PS.Lib.WinForms.PSPartData
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public override string ReadView()
        {
            return @"SELECT CODEMPRESA,
CODNATUREZA,
DESCRICAO,
CONVERT(BIT, ATIVO) ATIVO,
CONVERT(BIT, PERMITELANCAMENTO) PERMITELANCAMENTO,
CONTROLEFISCAL,
NATUREZA
FROM VNATUREZAORCAMENTO WHERE ";
        }

        public override void ValidateRecord(List<PS.Lib.DataField> objArr)
        {
            base.ValidateRecord(objArr);

            PS.Lib.DataField dtCODEMPRESA = gb.RetornaDataFieldByCampo(objArr, "CODEMPRESA");
            PS.Lib.DataField dtCODNATUREZA = gb.RetornaDataFieldByCampo(objArr, "CODNATUREZA");

            #region Valida Máscara

            System.Data.DataRow PARAMVAREJO = gb.RetornaParametrosVarejo();
            if (PARAMVAREJO == null)
            {
                throw new Exception("Não foi possível carregar os parâmetros do módulo.");
            }
            else
            {
                string mask = (PARAMVAREJO["MASKNATUREZAORCAMENTO"] == DBNull.Value) ? string.Empty : PARAMVAREJO["MASKNATUREZAORCAMENTO"].ToString();
                if (mask != string.Empty)
                {
                    string CodCCusto = dtCODNATUREZA.Valor.ToString();
                    if (CodCCusto.Length <= mask.Length)
                    {
                        for (int i = 0; i < CodCCusto.Length; i++)
                        {
                            if (PS.Lib.Utils.MascaraCaracterValido(CodCCusto[i], mask, i))
                            {
                                if (i == (CodCCusto.Length - 1) && CodCCusto[i] == '.')
                                {
                                    throw new Exception("O código da natureza orçamentária não pode terminar com o caracter separador da máscara parametrizada.");
                                }
                            }
                            else
                            {
                                throw new Exception("O código da natureza orçamentária não está consistente com a máscara parametrizada.");
                            }
                        }

                        if (CodCCusto.Length < mask.Length)
                        {
                            if (mask.Substring(CodCCusto.Length, 1) != ".")
                            {
                                throw new Exception("O código da natureza orçamentária não está consistente com a máscara parametrizada.");
                            }
                        }

                        string CodNaturezaPai = PS.Lib.Utils.MascaraNivelAnterior(CodCCusto);
                        if (!string.IsNullOrEmpty(CodNaturezaPai))
                        {
                            string sSql = @"SELECT CODNATUREZA FROM VNATUREZAORCAMENTO WHERE CODEMPRESA = ? AND CODNATUREZA = ?";
                            if (!dbs.QueryFind(sSql, dtCODEMPRESA.Valor, CodNaturezaPai))
                            {
                                throw new Exception("O código da natureza orçamentária informado não possui nível anterior.");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("O código da natureza orçamentária não pode ser maior que a máscara parametrizada.");
                    }
                }
            }

            #endregion
        }
    }
}
