using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PS.Glb.Class
{
    public class vTipOperSerie
    {
        #region Propriedades

        public int CODEMPRESA { get; set; }
        public string CODTIPOPER { get; set; }
        public string CODSERIE { get; set; }
        public int CODFILIAL { get; set; }
        public bool PRINCIPAL { get; set; }

        #endregion

        public vTipOperSerie() { }

        #region Busca a tabela VTIPOPSERSERIE

        /// <summary>
        /// Método para buscar a tabela VTIPOPSERSERIE
        /// </summary>
        /// <param name="_codtipOper">Código do tipo da operação</param>
        /// <param name="_codFilial">Códgio da filial</param>
        /// <param name="_codEmpresa">Código da empresa</param>
        /// <returns></returns>
        public vTipOperSerie getVtipOperSerie(string _codtipOper, int _codFilial, int _codEmpresa)
        {
            vTipOperSerie _vTipOperSerie = new vTipOperSerie();
            try
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VTIPOPERSERIE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODTIPOPER = ?", new object[] { _codEmpresa, _codFilial, _codtipOper });
                if (dt.Rows.Count > 0)
                {
                    _vTipOperSerie.CODEMPRESA = Convert.ToInt32(dt.Rows[0]["CODEMPRESA"]);
                    _vTipOperSerie.CODTIPOPER = dt.Rows[0]["CODTIPOPER"].ToString();
                    _vTipOperSerie.CODSERIE = dt.Rows[0]["CODSERIE"].ToString();
                    _vTipOperSerie.CODFILIAL = Convert.ToInt32(dt.Rows[0]["CODFILIAL"]);
                    _vTipOperSerie.PRINCIPAL = dt.Rows[0]["PRINCIPAL"].ToString() == "1" ? true : false;
                }
            }
            catch (Exception)
            {
                _vTipOperSerie.CODTIPOPER = string.Empty;
            }
            return _vTipOperSerie;
        }
        #endregion
    }
}
