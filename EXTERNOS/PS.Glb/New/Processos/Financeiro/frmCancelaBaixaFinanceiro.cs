using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Processos.Financeiro
{
    public partial class frmCancelaBaixaFinanceiro : Form
    {
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        List<DataRow> listaRow = new List<DataRow>();
        private int Insert;

        public frmCancelaBaixaFinanceiro(List<DataRow> _listaRow)
        {
            InitializeComponent();
            listaRow = _listaRow;
        }

        private bool verificaNFOUDUP(int codlanca)
        {
            try
            {
                if (AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT NFOUDUP FROM FLANCA WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { codlanca, AppLib.Context.Empresa }).ToString().Equals("1"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public int ExisteChequeLancamento(int CODEMPRESA, int CODLANCA)
        {
            return Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT CODCHEQUE FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?", CODEMPRESA, CODLANCA));
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dteDataCompensacao.Text))
            {
                MessageBox.Show("Favor preencher a data de Cancelamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for (int i = 0; i < listaRow.Count; i++)
            {
                if (verificaNFOUDUP(Convert.ToInt32(listaRow[i]["FLANCA.CODLANCA"])))
                {
                    MessageBox.Show("Não é permitido baixar lançamentos de origem de faturas.", "Informação Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (PS.Lib.PSMessageBox.ShowQuestion("Confirma o cancelamento da baixa do(s) lançamento(s) selecionado(s)?") == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    // CRIA A LISTA DE LANÇAMENTOS Á SEREM CANCELADOS OS DEMAIS LANÇAMENTOS DE CHEQUE
                    List<int> listaCODLANCA = new List<int>();

                    Support.FinLanCanBaixaPar finLanCanBaixaPar = new Support.FinLanCanBaixaPar();
                    finLanCanBaixaPar.CodEmpresa = PS.Lib.Contexto.Session.Empresa.CodEmpresa;
                    finLanCanBaixaPar.CodLanca = new int[listaRow.Count];
                    finLanCanBaixaPar.DataCancelamento = dteDataCompensacao.DateTime;
                    finLanCanBaixaPar.MotivoCancelamento = memoEdit1.Text;
                    finLanCanBaixaPar.UsuarioCancelamento = PS.Lib.Contexto.Session.CodUsuario;

                    for (int i = 0; i < listaRow.Count; i++)
                    {
                        int cont = 0;
                        listaCODLANCA.Add(Convert.ToInt32(listaRow[i]["FLANCA.CODLANCA"]));

                        List<int> listaTEMP = new List<int>();
                        int CODCHEQUE = this.ExisteChequeLancamento(finLanCanBaixaPar.CodEmpresa, Convert.ToInt32(listaRow[i]["FLANCA.CODLANCA"]));

                        if (CODCHEQUE != 0)
                        {
                            if (MessageBox.Show("Deseja Cancelar o Cheque?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                            {
                                cancelaCheque(finLanCanBaixaPar.CodEmpresa, CODCHEQUE);
                            }
                            listaTEMP = this.ObtemLancamentosMesmoCODCHEQUE(finLanCanBaixaPar.CodEmpresa, CODCHEQUE);

                            for (int x = 0; x < listaTEMP.Count; x++)
                            {
                                if (!listaCODLANCA.Contains(listaTEMP[x]))
                                {
                                    listaCODLANCA.Add(listaTEMP[x]);
                                }
                            }
                        }

                        if (verificaCompensado(Convert.ToInt32(listaRow[i]["FLANCA.CODLANCA"])).Equals(false))
                        {
                            if (this.TrataChequeCancelamentoBaixa(finLanCanBaixaPar.CodEmpresa, Convert.ToInt32(listaRow[i]["FLANCA.CODLANCA"])))
                            {
                                finLanCanBaixaPar.CodLanca[cont] = Convert.ToInt32(listaRow[i]["FLANCA.CODLANCA"]);
                                cont++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            if (ValidaInsertComCheque(Convert.ToInt32(listaRow[i]["FLANCA.CODLANCA"])) == true)
                            {
                                Insert = InsertExtato(Convert.ToInt32(listaRow[i]["FLANCA.CODLANCA"]));
                            }
                            else
                            {
                                if (Insert > 0)
                                {
                                    //
                                }
                                else
                                {
                                    Insert = InsertExtato(Convert.ToInt32(listaRow[i]["FLANCA.CODLANCA"]));
                                }
                            }
                        }
                    }

                    finLanCanBaixaPar.CodLanca = listaCODLANCA.ToArray();

                    CancelaBaixaLancamento(finLanCanBaixaPar);
                    MessageBox.Show("Cancelamento realizado com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                catch (Exception ex)
                {
                    PS.Lib.PSMessageBox.ShowError(ex.Message);
                    return;
                }
            }
        }

        private bool cancelaCheque(int codEmpresa, int codCheque)
        {
            try
            {
                if (AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FCHEQUE SET CANCELADO = 1 WHERE CODEMPRESA = ? AND CODCHEQUE = ?", new object[] { codEmpresa, codCheque }).Equals(1))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private int getUltimoNumeroFextrato()
        {
            return Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT MAX(IDLOG) FROM GLOG WHERE CODTABELA = ?", new object[] { "FEXTRATO" }));
        }

        private bool ValidaInsertComCheque(int codLanca)
        {
            int CodCheque = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, "SELECT COUNT(CODCHEQUE) FROM FEXTRATOLANCA WHERE CODEMPRESA = ? AND CODLANCA = ? ", new object[] { AppLib.Context.Empresa, codLanca }));

            if (CodCheque > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidaInsertExtrato(int codLanca)
        {
            DataTable dtExtratoLanca = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM FEXTRATOLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?", new object[] { AppLib.Context.Empresa, codLanca });

            int ExtratoLanca = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, "SELECT COUNT(IDEXTRATO) FROM FEXTRATOLANCA WHERE CODEMPRESA = ? AND IDEXTRATO = ?", new object[] { AppLib.Context.Empresa, dtExtratoLanca.Rows[0]["IDEXTRATO"] }));

            if (ExtratoLanca > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int InsertExtato(int codLanca)
        {
            try
            {
                DataTable dtCheque = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * 
FROM FEXTRATOLANCA 
INNER JOIN FEXTRATO ON FEXTRATOLANCA.CODEMPRESA = FEXTRATO.CODEMPRESA 
AND FEXTRATOLANCA.IDEXTRATO = FEXTRATO.IDEXTRATO 
WHERE FEXTRATOLANCA.CODEMPRESA = ? AND FEXTRATOLANCA.CODLANCA = ?", new object[] { AppLib.Context.Empresa, codLanca });
                int tipo;
                DateTime? DataCompensacao;
                for (int i = 0; i < dtCheque.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(dtCheque.Rows[i]["DATACOMPENSACAO"].ToString()))
                    {
                        DataCompensacao = null;
                    }
                    else
                    {
                        DataCompensacao = dteDataCompensacao.DateTime;
                    }

                    if (Convert.ToInt32(dtCheque.Rows[i]["TIPO"]) == 4)
                    {
                        tipo = 5;
                    }
                    else if(Convert.ToInt32(dtCheque.Rows[i]["TIPO"]) == 5)
                    {
                        tipo = 4;
                    }
                    else if(Convert.ToInt32(dtCheque.Rows[i]["TIPO"]) == 0)
                    {
                        tipo = 1;
                    }
                    else
                    {
                        tipo = 0;
                    }
                    int NovoExtrato = getUltimoNumeroFextrato() + 1;

                    if (AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO FEXTRATO (CODEMPRESA, IDEXTRATO, CODFILIAL, CODCONTA, NUMERODOCUMENTO, DATA, VALOR, HISTORICO, COMPENSADO, DATACOMPENSACAO, TIPO, CODCCUSTO, CODNATUREZAORCAMENTO) 
                                                                                VALUES 
                                                                                (?,?,?,?,?,?,?,?,?,?,?,?,?)", new object[] { AppLib.Context.Empresa, NovoExtrato, Convert.ToInt32(dtCheque.Rows[i]["CODFILIAL"]), dtCheque.Rows[i]["CODCONTA"].ToString(), dtCheque.Rows[i]["NUMERODOCUMENTO"].ToString(), DateTime.Now, Convert.ToDecimal(dtCheque.Rows[i]["VALOR"]), dtCheque.Rows[i]["HISTORICO"].ToString(), Convert.ToInt32(dtCheque.Rows[i]["COMPENSADO"]), DataCompensacao, tipo, dtCheque.Rows[i]["CODCCUSTO"].ToString(), dtCheque.Rows[i]["CODNATUREZAORCAMENTO"].ToString() }) > 0)
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GLOG SET IDLOG = ? WHERE CODTABELA = ?", new object[] { NovoExtrato, "FEXTRATO" });
                    }
                    else
                    {
                        return 0;
                    }
                }

                return 1;
            }

            catch (Exception ex)
            {
                return 0;
            }
        }

        private bool inseriExtrato(int codLanca)
        {
            try
            {
                DataTable dt = new DataTable();

                int idextrato = getUltimoNumeroFextrato() + 1;

                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT FEXTRATO.* FROM FLANCA INNER JOIN FEXTRATO ON FLANCA.CODEMPRESA = FEXTRATO.CODEMPRESA AND FLANCA.IDEXTRATO = FEXTRATO.IDEXTRATO WHERE FLANCA.CODLANCA = ?", new object[] { codLanca });
                DateTime? DataCompensacao;

                if (string.IsNullOrEmpty(dt.Rows[0]["DATACOMPENSACAO"].ToString()))
                {
                    DataCompensacao = null;
                }
                else
                {
                    DataCompensacao = dteDataCompensacao.DateTime;
                }

                if (dt.Rows[0]["TIPO"].ToString().Equals("0"))
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO FEXTRATO (CODEMPRESA, IDEXTRATO, CODFILIAL, CODCONTA, NUMERODOCUMENTO, DATA, VALOR, HISTORICO, COMPENSADO, DATACOMPENSACAO, TIPO, CODCCUSTO, CODNATUREZAORCAMENTO) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?)", new object[] { dt.Rows[0]["CODEMPRESA"], idextrato, dt.Rows[0]["CODFILIAL"], dt.Rows[0]["CODCONTA"], dt.Rows[0]["NUMERODOCUMENTO"], dt.Rows[0]["DATA"], dt.Rows[0]["VALOR"], dt.Rows[0]["HISTORICO"], Convert.ToInt32(dt.Rows[0]["COMPENSADO"]), DataCompensacao, "1", dt.Rows[0]["CODCCUSTO"], dt.Rows[0]["CODNATUREZAORCAMENTO"], });

                }
                else
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO FEXTRATO (CODEMPRESA, IDEXTRATO, CODFILIAL, CODCONTA, NUMERODOCUMENTO, DATA, VALOR, HISTORICO, COMPENSADO, DATACOMPENSACAO, TIPO, CODCCUSTO, CODNATUREZAORCAMENTO) VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?)", new object[] { dt.Rows[0]["CODEMPRESA"], idextrato, dt.Rows[0]["CODFILIAL"], dt.Rows[0]["CODCONTA"], dt.Rows[0]["NUMERODOCUMENTO"], dt.Rows[0]["DATA"], dt.Rows[0]["VALOR"], dt.Rows[0]["HISTORICO"], Convert.ToInt32(dt.Rows[0]["COMPENSADO"]), DataCompensacao, "0", dt.Rows[0]["CODCCUSTO"], dt.Rows[0]["CODNATUREZAORCAMENTO"], });
                }

                AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GLOG SET IDLOG = ? WHERE CODTABELA = ?", new object[] { idextrato, "FEXTRATO" });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void CancelaBaixaLancamento(Support.FinLanCanBaixaPar finLanCanBaixaPar)
        {
            PSPartLancaData psPartLancaData = new PSPartLancaData();
            psPartLancaData._tablename = "FLANCA";
            psPartLancaData._keys = new string[] { "CODEMPRESA", "CODLANCA" };

            psPartLancaData.CancelaBaixaLancamento(finLanCanBaixaPar);
        }

        public List<int> ObtemLancamentosMesmoCODCHEQUE(int CODEMPRESA, int CODCHEQUE)
        {
            String consulta = "SELECT CODLANCA FROM FLANCA WHERE CODEMPRESA = ? AND CODCHEQUE = ?";
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(consulta, CODEMPRESA, CODCHEQUE);
            List<int> listaCODLANCA = new List<int>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listaCODLANCA.Add(int.Parse(dt.Rows[i]["CODLANCA"].ToString()));
            }

            return listaCODLANCA;
        }

        private bool verificaCompensado(int codLanca)
        {
            try
            {
                int idextrato = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT IDEXTRATO FROM FLANCA WHERE CODLANCA = ?", new object[] { codLanca }));
                if (!string.IsNullOrEmpty(AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT COMPENSADO FROM FEXTRATO WHERE IDEXTRATO = ?", new object[] { idextrato }).ToString()))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Boolean TrataChequeCancelamentoBaixa(int CODEMPRESA, int CODLANCA)
        {
            int CODCHEQUE = ExisteChequeLancamento(CODEMPRESA, CODLANCA);
            if (CODCHEQUE != 0)
            {
                List<int> listaCODLANCA = ObtemLancamentosMesmoCODCHEQUE(CODEMPRESA, CODCHEQUE);

                int TotalLancamentoCheque = listaCODLANCA.Count;

                int desvinculouLancamento = DesvincularChequeLancamentos(CODEMPRESA, (int)CODCHEQUE);

                //int excluiuCheque = ExcluirCheque(CODEMPRESA, (int)CODCHEQUE);
                if (cancelaCheque(CODEMPRESA, Convert.ToInt32(CODCHEQUE)).Equals(false))
                {
                    return false;
                }
            }

            return true;
        }

        public int DesvincularChequeLancamentos(int CODEMPRESA, int CODCHEQUE)
        {
            String comando = "UPDATE FLANCA SET CODCHEQUE = NULL WHERE CODEMPRESA = ? AND CODCHEQUE = ?";
            return AppLib.Context.poolConnection.Get("Start").ExecTransaction(comando, CODEMPRESA, CODCHEQUE);
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
