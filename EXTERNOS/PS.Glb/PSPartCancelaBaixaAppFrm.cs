using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PS.Lib;

namespace PS.Glb
{
    public partial class PSPartCancelaBaixaAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        PS.Lib.Constantes ct = new PS.Lib.Constantes();
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        PS.Lib.Valida vl = new PS.Lib.Valida();

        private int ItensSelecionados = 0;

        public PSPartCancelaBaixaAppFrm()
        {
            InitializeComponent();
        }

        private void LimpaFormulario()
        {
            psTextoBox1.Text = string.Empty;
            QuantosItensSelecionados();
        }

        private void VerificarPagarReceber()
        {
            string ContItens = null;

            if (this.psPartApp.Access == PS.Lib.AppAccess.View)
            {
                if (psPartApp.DataGrid != null)
                {
                    for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                    {
                        if (psPartApp.DataGrid.Rows[i].Selected)
                        {
                            if (ContItens == null)
                            {
                                ContItens = this.psPartApp.DataGrid.Rows[i].Cells["TIPOPAGREC"].Value.ToString();
                            }
                            else
                            {
                                if (this.psPartApp.DataGrid.Rows[i].Cells["TIPOPAGREC"].Value.ToString() != ContItens)
                                {
                                    throw new Exception("Cancelamento da Baixa de Contas a Pagar e Contas a Receber devem ser realizada separadamente.");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void QuantosItensSelecionados()
        {
            ItensSelecionados = 0;

            if (this.psPartApp.Access == PS.Lib.AppAccess.View)
            {
                if (psPartApp.DataGrid != null)
                {
                    for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                    {
                        if (psPartApp.DataGrid.Rows[i].Selected)
                        {
                            ItensSelecionados = ItensSelecionados + 1;
                        }
                    }
                }
            }

            if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
            {
                ItensSelecionados = 1;
            }
        }

        private void PSPartCancelaBaixaAppFrm_Load(object sender, EventArgs e)
        {
            LimpaFormulario();
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

        public override Boolean Execute()
        {
            for (int i = 0; i < psPartApp.DataGrid.SelectedRows.Count; i++)
            {
                if (verificaNFOUDUP(Convert.ToInt32(psPartApp.DataGrid.SelectedRows[i].Cells["CODLANCA"].Value)))
                {
                    MessageBox.Show("Não é permitido baixar lançamentos de origem de faturas.", "Informação Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }
            if (PS.Lib.PSMessageBox.ShowQuestion("Confirma o cancelamento da baixa do(s) lançamento(s) selecionado(s)?") == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    // CRIA A LISTA DE LANÇAMENTOS Á SEREM CANCELADOS OS DEMAIS LANÇAMENTOS DE CHEQUE
                    List<int> listaCODLANCA = new List<int>();

                    VerificarPagarReceber();

                    Support.FinLanCanBaixaPar finLanCanBaixaPar = new Support.FinLanCanBaixaPar();
                    finLanCanBaixaPar.CodEmpresa = PS.Lib.Contexto.Session.Empresa.CodEmpresa;
                    finLanCanBaixaPar.CodLanca = new int[ItensSelecionados];
                    finLanCanBaixaPar.DataCancelamento = DateTime.Today;
                    finLanCanBaixaPar.MotivoCancelamento = psTextoBox1.Text;
                    finLanCanBaixaPar.UsuarioCancelamento = PS.Lib.Contexto.Session.CodUsuario;

                    if (this.psPartApp.Access == PS.Lib.AppAccess.View)
                    {
                        int cont = 0;

                        if (psPartApp.DataGrid != null)
                        {
                            for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                            {
                                if (psPartApp.DataGrid.Rows[i].Selected)
                                {
                                    int CODLANCA = Convert.ToInt32(psPartApp.DataGrid.Rows[i].Cells["CODLANCA"].Value);
                                    listaCODLANCA.Add(CODLANCA);

                                    List<int> listaTEMP = new List<int>();
                                    int CODCHEQUE = this.ExisteChequeLancamento(finLanCanBaixaPar.CodEmpresa, CODLANCA);

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

                                    if (verificaCompensado(CODLANCA).Equals(false))
                                    {
                                        if (this.TrataChequeCancelamentoBaixa(finLanCanBaixaPar.CodEmpresa, CODLANCA))
                                        {
                                            finLanCanBaixaPar.CodLanca[cont] = CODLANCA;
                                            cont++;
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        inseriExtrato(CODLANCA);
                                    }
                                }
                            }
                        }
                    }

                    if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
                    {
                        finLanCanBaixaPar.CodLanca = new int[] { Convert.ToInt32(gb.RetornaDataFieldByCampo(this.psPartApp.DataField, "CODLANCA").Valor) };
                    }

                    finLanCanBaixaPar.CodLanca = listaCODLANCA.ToArray();

                    CancelaBaixaLancamento(finLanCanBaixaPar);
                }
                catch (Exception ex)
                {
                    PS.Lib.PSMessageBox.ShowError(ex.Message);
                    return false;
                }
            }

            return true;
        }
        private int getUltimoNumeroFextrato()
        {
            return Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT MAX(IDLOG) FROM GLOG WHERE CODTABELA = ?", new object[] { "FEXTRATO" }));
        }
        private bool inseriExtrato(int codLanca)
        {
            try
            {
                DataTable dt = new DataTable();

                int idextrato = getUltimoNumeroFextrato() + 1;

                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT FEXTRATO.* FROM FLANCA INNER JOIN FEXTRATO ON FLANCA.CODEMPRESA = FEXTRATO.CODEMPRESA AND FLANCA.IDEXTRATO = FEXTRATO.IDEXTRATO WHERE FLANCA.CODLANCA = ?", new object[] { codLanca });
                if (dt.Rows[0]["TIPO"].ToString().Equals("0"))
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO FEXTRATO (CODEMPRESA, IDEXTRATO, CODFILIAL, CODCONTA, NUMERODOCUMENTO, DATA, VALOR, HISTORICO, TIPO, CODCCUSTO, CODNATUREZAORCAMENTO) VALUES (?,?,?,?,?,?,?,?,?,?,?)", new object[] { dt.Rows[0]["CODEMPRESA"], idextrato, dt.Rows[0]["CODFILIAL"], dt.Rows[0]["CODCONTA"], dt.Rows[0]["NUMERODOCUMENTO"], dt.Rows[0]["DATA"], dt.Rows[0]["VALOR"], dt.Rows[0]["HISTORICO"], "1", dt.Rows[0]["CODCCUSTO"], dt.Rows[0]["CODNATUREZAORCAMENTO"], });

                }
                else if (dt.Rows[0]["TIPO"].ToString().Equals("5"))
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO FEXTRATO (CODEMPRESA, IDEXTRATO, CODFILIAL, CODCONTA, NUMERODOCUMENTO, DATA, VALOR, HISTORICO, TIPO, CODCCUSTO, CODNATUREZAORCAMENTO) VALUES (?,?,?,?,?,?,?,?,?,?,?)", new object[] { dt.Rows[0]["CODEMPRESA"], idextrato, dt.Rows[0]["CODFILIAL"], dt.Rows[0]["CODCONTA"], dt.Rows[0]["NUMERODOCUMENTO"], dt.Rows[0]["DATA"], dt.Rows[0]["VALOR"], dt.Rows[0]["HISTORICO"], "4", dt.Rows[0]["CODCCUSTO"], dt.Rows[0]["CODNATUREZAORCAMENTO"], });
                }
                else
                {
                    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO FEXTRATO (CODEMPRESA, IDEXTRATO, CODFILIAL, CODCONTA, NUMERODOCUMENTO, DATA, VALOR, HISTORICO, TIPO, CODCCUSTO, CODNATUREZAORCAMENTO) VALUES (?,?,?,?,?,?,?,?,?,?,?)", new object[] { dt.Rows[0]["CODEMPRESA"], idextrato, dt.Rows[0]["CODFILIAL"], dt.Rows[0]["CODCONTA"], dt.Rows[0]["NUMERODOCUMENTO"], dt.Rows[0]["DATA"], dt.Rows[0]["VALOR"], dt.Rows[0]["HISTORICO"], "0", dt.Rows[0]["CODCCUSTO"], dt.Rows[0]["CODNATUREZAORCAMENTO"], });

                }

                AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GLOG SET IDLOG = ? WHERE CODTABELA = ?", new object[] { idextrato, "FEXTRATO" });
                return true;
            }
            catch (Exception)
            {

                return false;
            }
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
        private void CancelaBaixaLancamento(Support.FinLanCanBaixaPar finLanCanBaixaPar)
        {
            PSPartLancaData psPartLancaData = new PSPartLancaData();
            psPartLancaData._tablename = this.psPartApp.TableName;
            psPartLancaData._keys = this.psPartApp.Keys;

            psPartLancaData.CancelaBaixaLancamento(finLanCanBaixaPar);
        }

        public int ExisteChequeLancamento(int CODEMPRESA, int CODLANCA)
        {
            return Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT CODCHEQUE FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?", CODEMPRESA, CODLANCA));
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

        public int ExisteChequeExtrato(int CODEMPRESA, int CODCHEQUE)
        {
            String NUMERODOCUMENTO = this.getFCHEQUE_NUMERODOCUMENTO(CODEMPRESA, CODCHEQUE);

            return Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT IDEXTRATO FROM FEXTRATO WHERE CODEMPRESA = ? AND NUMERODOCUMENTO = ? AND TIPO IN (4, 5)", CODEMPRESA, NUMERODOCUMENTO));
        }

        public String getFCHEQUE_NUMERODOCUMENTO(int CODEMPRESA, int CODCHEQUE)
        {
            return dbs.QueryValue(null, "SELECT NUMERO FROM FCHEQUE WHERE CODEMPRESA = ? AND CODCHEQUE = ?", CODEMPRESA, CODCHEQUE).ToString();
        }

        public int ExcluirChequeExtrato(int CODEMPRESA, int IDEXTRATO)
        {
            return AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FEXTRATO WHERE CODEMPRESA = ? AND IDEXTRATO = ?", CODEMPRESA, IDEXTRATO.ToString());
        }

        public int DesvincularChequeLancamentos(int CODEMPRESA, int CODCHEQUE)
        {
            String comando = "UPDATE FLANCA SET CODCHEQUE = NULL WHERE CODEMPRESA = ? AND CODCHEQUE = ?";
            return AppLib.Context.poolConnection.Get("Start").ExecTransaction(comando, CODEMPRESA, CODCHEQUE);
        }

        public int ExcluirCheque(int CODEMPRESA, int CODCHEQUE)
        {
            String comando = "DELETE FCHEQUE WHERE CODEMPRESA = ? AND CODCHEQUE = ?";
            return AppLib.Context.poolConnection.Get("Start").ExecTransaction(comando, CODEMPRESA, CODCHEQUE);
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

    }
}
