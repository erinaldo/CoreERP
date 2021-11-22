using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.NotaFiscal
{
    public partial class frmProcessoNotaFiscal : Form
    {
        #region Variáveis comuns para os eventos

        public string Chave = string.Empty;
        public string TpAmb = string.Empty;
        public string TipoEvento = string.Empty;
        public int Codoper;

        Class.NFeAPI NfeAPI = new Class.NFeAPI();
        Class.TratarXML x = new Class.TratarXML();
        string Token = string.Empty;

        #endregion

        #region Cancelamento

        public string Protocolo = string.Empty;

        #endregion

        #region Carta de Correção  

        public string NseqEvento = string.Empty;
        public string Justificativa = string.Empty;

        #endregion

        public frmProcessoNotaFiscal(string Evento)
        {
            InitializeComponent();
            TipoEvento = Evento;


            if (Evento == "Cancelamento")
            {
                lbEvento.Text = "Motivo de cancelamento";
            }
            if (Evento == "Carta de Correção")
            {
                lbEvento.Text = "Motivo da Carta de Correção";
            }
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            int Codfilial = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, "SELECT CODFILIAL FROM GOPER WHERE CODOPER = ? ", new object[] { Codoper }));
            Token = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT GFILIAL.TOKEN
                                                                                                   FROM GOPER
                                                                                                   INNER JOIN GFILIAL ON GOPER.CODEMPRESA = GFILIAL.CODEMPRESA AND GOPER.CODFILIAL = GFILIAL.CODFILIAL
                                                                                                   WHERE GOPER.CODEMPRESA = ? AND GOPER.CODOPER = ? AND GOPER.CODFILIAL = ?", new object[] { AppLib.Context.Empresa, Codoper, Codfilial }).ToString();

            if (TipoEvento == "Cancelamento")
            {
                if (ValidaCancelamento() == false)
                {
                    return;
                }

                try
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormCaption("Solicitando");

                    DateTime DataEvento = DateTime.Now;
                    string DataFormatada = DataEvento.ToString("yyyy-MM-ddTHH:mm:sszzz");

                    //Aumenta o tempo de consulta em 5000 milissegundos(5 segundos)
                    System.Threading.Thread.Sleep(5000);

                    string RetornoCancelamento = NfeAPI.CancelamentoNFe(Token, Chave, TpAmb, DataFormatada, Protocolo, MemoMotivoEvento.Text);

                    dynamic JsonRetornoCancelamento = JsonConvert.DeserializeObject(RetornoCancelamento);

                    string StatusCancelamento = JsonRetornoCancelamento.status;
                    string Motivo = JsonRetornoCancelamento.motivo;

                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GNFESTADUALHISTORICO(CODEMPRESA, CODOPER, IDHISTORICO, DATA, CODUSUARIO, OBSERVACAO) 
                                                                                 VALUES
                                                                                 (?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, Codoper, NfeAPI.getIdHistorico(Codoper), DateTime.Now, AppLib.Context.Usuario, Motivo });

                    if (StatusCancelamento == "200")
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET CODSTATUS = 'U' WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper });
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO GMOTIVOCANCELAMENTO (CODOPER, CODEMPRESA, CODUSUARIO, MOTIVO, DATACRIACAO) VALUES (?, ?, ?, ?, ?)", new object[] { Codoper, AppLib.Context.Empresa, AppLib.Context.Usuario, MemoMotivoEvento.Text, DateTime.Now });

                        int ValidaEvento = (int)AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(IDEVENTO) FROM GNFESTADUALEVENTO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper });

                        if (ValidaEvento == 0)
                        {
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GNFESTADUALEVENTO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper });
                            AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO GNFESTADUALEVENTO (CODEMPRESA, CODOPER, IDEVENTO, DATA, CODUSUARIO, TPEVENTO, JUSTIFICATIVA, CODSTATUS, PROTOCOLO, DATAPROTOCOLO, XMLENV, XMLPROT) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, Codoper, 1, DateTime.Now, AppLib.Context.Usuario, "", MemoMotivoEvento.Text, "U", null, null, null, null });
                        }

                        this.Dispose();
                    }
                    else
                    {
                        this.Dispose();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            if (TipoEvento == "Carta de Correção")
            {
                try
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormCaption("Solicitando");

                    DateTime DataEvento = DateTime.Now;
                    string DataFormatada = DataEvento.ToString("yyyy-MM-ddTHH:mm:sszzz");

                    string RetornoCartaCorrecao = NfeAPI.CartaCorrecaoNFe(Token, Chave, TpAmb, DataFormatada, NseqEvento, MemoMotivoEvento.Text);

                    dynamic JsonRetornoCorrecao = JsonConvert.DeserializeObject(RetornoCartaCorrecao);
                    string StatusCorrecao = JsonRetornoCorrecao.status;
                    string Motivo = JsonRetornoCorrecao.motivo;

                    AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GNFESTADUALHISTORICO(CODEMPRESA, CODOPER, IDHISTORICO, DATA, CODUSUARIO, OBSERVACAO) 
                                                                                 VALUES
                                                                                 (?, ?, ?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, Codoper, NfeAPI.getIdHistorico(Codoper), DateTime.Now, AppLib.Context.Usuario, Motivo });

                    if (StatusCorrecao == "200")
                    {
                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET CODSTATUS = 'F' WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper });
                    }
                    else
                    {
                        this.Dispose();
                    }

                    this.Dispose();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            splashScreenManager1.CloseWaitForm();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Validações

        private bool ValidaCancelamento()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (MemoMotivoEvento.Text.Length < 15)
            {
                errorProvider1.SetIconAlignment(lbEvento, ErrorIconAlignment.MiddleRight);
                errorProvider1.SetError(lbEvento, "A justificativa deve conter no mínimo 15 caracteres.");
                verifica = false;
            }

            return verifica;
        }

        #endregion
    }
}
