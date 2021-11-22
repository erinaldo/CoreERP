using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Glb.Class
{
    public class GeraExtrato
    {
        public List<Class.ControleCheque> ListCheque = new List<ControleCheque>();
        private List<string> ListCodLanca = new List<string>();
        private List<int> ListExtrato = new List<int>();
        private string Codcusto;
        private string CodNatureza;
        private string NumeroDoc;

        public GeraExtrato(List<Class.ControleCheque> _list)
        {
            ListCheque = _list;
        }

        public int GerarExtratoLanca(bool _controleCheque)
        {
            if (_controleCheque == false)
            {
                for (int i = 0; i < ListCodLanca.Count; i++)
                {
                    int IdExtrato = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, "SELECT IDEXTRATO FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?", new object[] { AppLib.Context.Empresa, ListCodLanca[i] }));

                    if (IdExtrato > 0)
                    {
                        if (ListExtrato.Contains(IdExtrato))
                        {
                            continue;
                        }
                        else
                        {
                            ListExtrato.Add(IdExtrato);
                        }               
                    }
                }

                try
                {
                    AppLib.ORM.Jit FEXTRATOLANCA = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "FEXTRATOLANCA");

                    for (int X = 0; X < ListExtrato.Count; X++)
                    {
                        int Verifiica = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, "SELECT COUNT(IDEXTRATO) FROM FEXTRATOLANCA WHERE CODEMPRESA = ? AND IDEXTRATO = ?", new object[] { AppLib.Context.Empresa, ListExtrato[X] }));
                        if (Verifiica > 0)
                        {
                            continue;
                        }
                        FEXTRATOLANCA.Set("CODEMPRESA", AppLib.Context.Empresa);

                        int codFilial = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT CODFILIAL FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?", new object[] { AppLib.Context.Empresa, ListCodLanca[X] }));
                        FEXTRATOLANCA.Set("CODFILIAL", codFilial);

                        FEXTRATOLANCA.Set("IDEXTRATO", ListExtrato[X]);

                        FEXTRATOLANCA.Set("CODLANCA", ListCodLanca[X]);

                        FEXTRATOLANCA.Set("CODCHEQUE", null);

                        FEXTRATOLANCA.Insert();
                    }
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            else
            {
                try
                {
                    for (int i = 0; i < ListCodLanca.Count; i++)
                    {
                        AppLib.ORM.Jit FEXTRATOLANCA = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "FEXTRATOLANCA");

                        for (int X = 0; X < ListCheque.Count; X++)
                        {
                            FEXTRATOLANCA.Set("CODEMPRESA", AppLib.Context.Empresa);

                            if (ListCodLanca.Count > 1)
                            {
                                int codFilial = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT CODFILIAL FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?", new object[] { AppLib.Context.Empresa, ListCodLanca[X] }));
                                FEXTRATOLANCA.Set("CODFILIAL", codFilial);
                            }
                            else
                            {
                                int codFilial = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT CODFILIAL FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?", new object[] { AppLib.Context.Empresa, ListCodLanca[i] }));
                                FEXTRATOLANCA.Set("CODFILIAL", codFilial);
                            }

                            FEXTRATOLANCA.Set("IDEXTRATO", ListExtrato[X]);

                            FEXTRATOLANCA.Set("CODLANCA", ListCodLanca[i]);

                            FEXTRATOLANCA.Set("CODCHEQUE", ListCheque[X].CODCHEQUE);

                            FEXTRATOLANCA.Insert();
                        }
                    }
                    return 1;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        public int GerarExtrato()
        {
            try
            {
                for (int i = 0; i < ListCheque.Count; i++)
                {
                    AppLib.ORM.Jit FEXTRATO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "FEXTRATO");

                    FEXTRATO.Set("CODEMPRESA", AppLib.Context.Empresa);
                    FEXTRATO.Set("IDEXTRATO", setIdExtrato());

                    if (ListCodLanca.Count > 1)
                    {
                        int codFilial = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT CODFILIAL FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?", new object[] { AppLib.Context.Empresa, ListCodLanca[i] }));
                        FEXTRATO.Set("CODFILIAL", codFilial);
                    }
                    else
                    {
                        int codFilial = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT CODFILIAL FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?", new object[] { AppLib.Context.Empresa, ListCodLanca[0] }));
                        FEXTRATO.Set("CODFILIAL", codFilial);
                    }

                    FEXTRATO.Set("CODCONTA", ListCheque[i].CODCONTA);
                    FEXTRATO.Set("NUMERODOCUMENTO", setNumeroDocumento(ListCheque[i].NUMERO.ToString()));
                    FEXTRATO.Set("DATA", Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                    FEXTRATO.Set("VALOR", ListCheque[i].VALOR);
                    FEXTRATO.Set("HISTORICO", ListCheque[i].OBSERVACAO);
                    FEXTRATO.Set("COMPENSADO", ListCheque[i].COMPENSADO);
                    FEXTRATO.Set("DATACOMPENSACAO", ListCheque[i].DATACOMPENSACAO);
                    FEXTRATO.Set("TIPO", ListCheque[i].PAGREC == 0 ? 4 : 5);
                    FEXTRATO.Set("CODCCUSTO", Codcusto);
                    FEXTRATO.Set("CODNATUREZAORCAMENTO", CodNatureza);
                    FEXTRATO.Set("CODCHEQUE", ListCheque[i].CODCHEQUE);

                    if (FEXTRATO.Insert() > 0)
                    {
                        if (ListCodLanca.Count > 1)
                        {
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FLANCA SET IDEXTRATO = ? WHERE CODEMPRESA = ? AND CODLANCA = ?", new object[] { FEXTRATO.CampoValor[1].Valor, AppLib.Context.Empresa, ListCodLanca[i] });
                        }
                        else
                        {
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FLANCA SET IDEXTRATO = ? WHERE CODEMPRESA = ? AND CODLANCA = ?", new object[] { FEXTRATO.CampoValor[1].Valor, AppLib.Context.Empresa, ListCodLanca[0] });
                        }

                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GLOG SET IDLOG = ? WHERE CODEMPRESA = ? AND CODTABELA = 'FEXTRATO'", new object[] { FEXTRATO.CampoValor[1].Valor, AppLib.Context.Empresa });
                    }
                }
                return 1;
            }
            catch (Exception EX)
            {
                return 0;
            }
        }

        public List<string> getLancamento(List<string> _list)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                ListCodLanca.Add(_list[i].ToString());
            }
            return ListCodLanca;
        }

        public string getCodCusto(string _codCusto)
        {
            Codcusto = _codCusto;
            return Codcusto;
        }

        public string getCodNatureza(string _codNatureza)
        {
            CodNatureza = _codNatureza;
            return CodNatureza;
        }

        public string setIdExtrato()
        {
            string IdExtrato = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT ISNULL(MAX(IDEXTRATO), 0) + 1 FROM FEXTRATO WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }).ToString();
            ListExtrato.Add(Convert.ToInt32(IdExtrato));
            return IdExtrato;
        }

        public string setNumeroDocumento(string _Numero)
        {
            int MASKNUMEROSEQ = int.Parse(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT MASKNUMEROSEQ FROM VPARAMETROS WHERE CODEMPRESA = ?", new Object[] { AppLib.Context.Empresa }).ToString());
            NumeroDoc = AppLib.Util.Format.CompletarZeroEsquerda(MASKNUMEROSEQ, _Numero.ToString());
            return NumeroDoc;
        }
    }
}
