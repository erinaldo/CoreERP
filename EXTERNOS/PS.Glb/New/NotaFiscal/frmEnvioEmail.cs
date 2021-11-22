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
    public partial class frmEnvioEmail : Form
    {
        public int Codoper;
        public int CodEmp;
        public int CodFilial;
        string Token = string.Empty;
        public string chave;
        public string CodStatus;
        private string anexo1, anexo2, anexo3, anexo4, anexo5, anexo6;

        public frmEnvioEmail()
        {
            InitializeComponent();
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            string[] arrTrans = null;
            string[] arrAdicional = null;
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VPARAMETROS");
            string emailTransportadora = "";
            string sqlTransp = "";
            string sqlCliente = "";
            string tpEventoCAN = "";
            string tpEventoCCE = "";
            Class.NFeAPI NfeAPI = new Class.NFeAPI();

            Token = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TOKEN FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, CodFilial }).ToString();

            //CONFIGURAÇÃO
            string smtp = dt.Rows[0]["EMAILHOST"].ToString();
            bool ssl = (Convert.ToInt32(dt.Rows[0]["EMAILUSASSL"]) == 0) ? false : true;
            string usuario = dt.Rows[0]["EMAILUSUARIO"].ToString();
            string senha = dt.Rows[0]["EMAILSENHA"].ToString();
            bool credenciais = false;
            int porta = Convert.ToInt32(dt.Rows[0]["EMAILPORTA"]);

            string Caminho = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT PASTADESTINO FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, CodFilial }).ToString();
            string TpAmb = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TPAMB FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, CodFilial }).ToString();
            string CaminhoEventoCAN = "";
            string CaminhoEventoCCE = "";
            /*
            string smtp = "smtp.itinit.com.br";
            bool ssl = false;
            string usuario = "workflow@itinit.com.br";
            string senha = "aiti!2017@naite";
            bool credenciais = false;
            int porta = 587;
            string Caminho = @"C:\Windows\Temp";
            */

            //DADOS DO CLIENTE
            sqlCliente = @"SELECT
                         VCLIFOR.EMAILNFE
                        FROM
                        GNFESTADUAL 
                         INNER JOIN GOPER ON GNFESTADUAL.CODOPER = GOPER.CODOPER 
                         INNER JOIN VCLIFOR ON VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR
                        WHERE 
                         GNFESTADUAL.CODOPER = ? AND GNFESTADUAL.CODEMPRESA= ? AND GOPER.CODFILIAL = ?";

            string emailCliente = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, sqlCliente, new object[] { Codoper, CodEmp, CodFilial }).ToString();
            string TextoEmailNfe = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT TEXTOEMAILNFE FROM GFILIAL WHERE CODEMPRESA = ? AND CODFILIAL = ?", new object[] { AppLib.Context.Empresa, CodFilial }).ToString();

            string assunto = "Nota Fiscal Eletrônica";
            string mensagem = TextoEmailNfe;


            //RECUPERA EMAILS DA TRANSPORTADORA
            sqlTransp = @"SELECT
                         VTRANSPORTADORA.EMAIL
                        FROM
                        GNFESTADUAL 
                         INNER JOIN GOPER ON GNFESTADUAL.CODOPER = GOPER.CODOPER 
                         INNER JOIN VTRANSPORTADORA ON VTRANSPORTADORA.CODTRANSPORTADORA = GOPER.CODTRANSPORTADORA 
                         WHERE 
                         GNFESTADUAL.CODOPER = ? AND GNFESTADUAL.CODEMPRESA= ? AND GOPER.CODFILIAL = ?";

            emailTransportadora = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, sqlTransp, new object[] { Codoper, CodEmp, CodFilial }).ToString();

            //CRIA OS ARRAYS
            arrTrans = emailTransportadora.Split(';');
            arrAdicional = tbEmail.Text.Split(';');

            //JUNTA OS DOIS ARRAYS
            var arr = arrTrans.Union(arrAdicional).ToArray();

            //VERIFICA SE ENVIARÁ PARA OS ADICIONAIS
            if (chkEnviarTransp.Checked)
            {

                //VALIDA SE OS EMAILS INFORMADOS SÃO VALIDOS
                foreach (var item in arr)
                {
                    if (String.IsNullOrEmpty(item))
                        continue;

                    if (!PS.Glb.Class.Validar.isEmail(item.Trim()))
                    {
                        MessageBox.Show("O e-mail " + item.ToUpper() + " parece incorreto ou o separador informado não é valido!");
                        return;
                    }
                }
            }

            if (CodStatus == "A")
            {
                //retorno 1
                string retorno = NfeAPI.DownloadNFeAndSave(Token, chave, "P", TpAmb, Caminho, false);


                int ValidaCorrecao = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(CODOPER) FROM GNFESTADUALCORRECAO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper }));

                if (ValidaCorrecao > 0)
                {
                    string NseqEvento = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NSEQITEM FROM GNFESTADUALCORRECAO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper }).ToString();
                    System.Threading.Thread.Sleep(1000);

                    tpEventoCCE = "CCE";
                    string retornoEvento = NfeAPI.DownloadNFeAndSaveEvento(Token, chave, "P", TpAmb, tpEventoCCE, NseqEvento, Caminho, false);

                }
            }
            else if (CodStatus == "C")
            {
                //retorno 3
                string retorno = NfeAPI.DownloadNFeAndSave(Token, chave, "P", TpAmb ,Caminho, false);
                System.Threading.Thread.Sleep(1000);


                int ValidaCorrecao = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT COUNT(CODOPER) FROM GNFESTADUALCORRECAO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper }));

                if (ValidaCorrecao > 0)
                {
                    string NseqEvento = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NSEQITEM FROM GNFESTADUALCORRECAO WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper }).ToString();
                    System.Threading.Thread.Sleep(1000);

                    tpEventoCCE = "CCE";
                    string RetornoEvento = NfeAPI.DownloadNFeAndSaveEvento(Token, chave, "P", TpAmb, tpEventoCCE, NseqEvento, Caminho, false);
                }

                tpEventoCAN = "CANC";
                string retornoEvento = NfeAPI.DownloadNFeAndSaveEvento(Token, chave, "P", TpAmb, tpEventoCAN, "1", Caminho, false);
            }

            PS.Glb.Class.Email em = new PS.Glb.Class.Email(smtp, ssl, usuario, senha, credenciais, porta);
            PS.Glb.Class.NFeAPI nfeapi = new Class.NFeAPI();

            if (!String.IsNullOrEmpty(tpEventoCCE))
            {
                CaminhoEventoCCE = Caminho + "\\" + tpEventoCCE + "-";
            }

            if (!String.IsNullOrEmpty(tpEventoCAN))
            {
                CaminhoEventoCAN = Caminho + "\\" + tpEventoCAN + "-";
            }

            string RetornoDownload = nfeapi.DownloadNFeAndSave(Token, chave, "XP", TpAmb ,Caminho, false);
            anexo1 = Caminho + "\\" + chave + "-procNfe.pdf";
            anexo2 = Caminho + "\\" + chave + "-procNfe.xml";

            string RetornoDownloadEventoCCE = "";
            if (!string.IsNullOrEmpty(CaminhoEventoCCE))
            {
                RetornoDownloadEventoCCE = nfeapi.DownloadNFeAndSaveEvento(Token, chave, "XP", TpAmb, tpEventoCCE, "1", Caminho, false);
                anexo3 = CaminhoEventoCCE + chave + "-procNfe.pdf";
                anexo4 = CaminhoEventoCCE + chave + "-procNfe.xml";
            }

            string RetornoDownloadEventoCAN = "";
            if (!string.IsNullOrEmpty(CaminhoEventoCAN))
            {
                RetornoDownloadEventoCCE = nfeapi.DownloadNFeAndSaveEvento(Token, chave, "XP", TpAmb, tpEventoCAN, "1", Caminho, false);
                anexo5 = CaminhoEventoCAN + chave + "-procNfe.pdf";
                anexo6 = CaminhoEventoCAN + chave + "-procNfe.xml";
            }

            string[] anexos = { anexo1, anexo2, anexo3, anexo4, anexo5, anexo6 };

            int i = em.envio(usuario, emailCliente, assunto, mensagem, anexos, arr);
            if (i > 0)
            {
                AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GNFESTADUAL SET EMAILENVIADO = 1 WHERE CODEMPRESA= ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, Codoper });
                MessageBox.Show("E-mail(s) enviado(s) com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro ao enviar e-mail.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
