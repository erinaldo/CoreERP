using Newtonsoft.Json;
using PS.Glb.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.Glb.New.Processos
{
    public partial class FrmSincronizarLogin : Form
    {
        private string Modulo;
        private string Aba;
        private string Botao;
        private string Processo;

        public FrmSincronizarLogin(string _modulo, string _aba, string _botao)
        {
            InitializeComponent();

            Modulo = _modulo;
            Aba = _aba;
            Botao = _botao;

            Processo = Modulo + " - " + Aba + " - " + Botao;
        }

        //criar essa propriedade
        private bool valor;
        public string URI = "http://portal.itinit.com.br:8021/api/login";
        public string URILOG = "http://portal.itinit.com.br:8021/api/log";
        public string URICLIENTE = "http://portal.itinit.com.br:8021/api/cliente";

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string idCliente = "";

            //dados digitados
            string login = txtlogin.Text.Replace("'","");
            string senha = txtsenha.Text.Replace("'", "");


            string cnpj = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "select CGCCPF from GEMPRESA where CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }).ToString();
            cnpj = cnpj.Replace("/", "").Replace("-", "").Replace(".", "");


            //recupera o ID DO Cliente, nome, cnpj e qtdlicença webservice
            using (WebClient wc = new WebClient())
            {
                var j = wc.DownloadString(URICLIENTE + "/" + cnpj);
                dynamic array = JsonConvert.DeserializeObject(j);
                foreach (var item in array)
                {
                    idCliente = item.IDCLIENTE;
                }
            }



            //recupera os dados dos usuarios itinit web
            List<DadosUsuario> listaUsuario = new List<DadosUsuario>();
            using (WebClient wc = new WebClient())
            {
                var j = wc.DownloadString(URI+"/"+idCliente);
                dynamic array = JsonConvert.DeserializeObject(j);
                foreach (var item in array)
                {
                    listaUsuario.Add(new DadosUsuario { IDCLIENTE = item.IDCLIENTE, USUARIO = item.USUARIO, NOME = item.NOME, SENHA = item.SENHA, EMAIL = item.EMAIL, HOMOLOGADOIT = item.HOMOLOGADOIT, CHAVEHOMOLOGADOIT = item.CHAVEHOMOLOGADOIT, CONTROLE = item.CONTROLE, ATIVO = item.ATIVO });
                }
            }

            //verifica se existe o usuario
            Parametro = false;
            string valorSenha = "";
            Criptografia c = new Criptografia();
            foreach (var item in listaUsuario)
            {
                if(item.IDCLIENTE != "36")
                  valorSenha = c.Encoder(Criptografia.OpcoesEncoder.Rijndael, senha, login);
                else
                  valorSenha = c.HashSHA512_itinit(senha);

                if (item.USUARIO == login && item.SENHA == valorSenha)
                {
                    

                    string DATALOGIN = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string MAQUINANOLOGIN = Environment.MachineName;

                    string n = Dns.GetHostName();
                    IPAddress[] ip = Dns.GetHostAddresses(n);
                    string IPLOGIN = ip[1].ToString();
                    string NOMELOGIN = item.NOME;
                    string PROCESSO = Processo;

                    //GravarLog(idCliente, "1", "1", "2018-01-01", "1", "1");
                    GravarLog(idCliente, item.USUARIO, PROCESSO, DATALOGIN, MAQUINANOLOGIN, IPLOGIN, NOMELOGIN);


                    //string sql = "INSERT INTO ZCLIENTEUSUARIOLOG(IDCLIENTE, USUARIO, DATALOGIN, MAQUINANOLOGIN, IPLOGIN, PROCESSO) VALUES('"+idCliente+"','"+item.USUARIO+"','"+ DATALOGIN + "','"+ MAQUINANOLOGIN + "','"+ IPLOGIN + "','"+ PROCESSO + "')";
                    Parametro = true;
                    break;
                }
                    
            }
            this.Close();
        }

        public bool Parametro
        {
            get { return valor; }
            set { valor = value; }
        }


        private async void GravarLog(string IDCLIENTE, string USUARIO, string PROCESSO, string DATALOGIN, string MAQUINALOGIN, string IPLOGIN, string NOMELOGIN)
        {

            Log l = new Log();
            l.IDCLIENTE = IDCLIENTE;
            l.USUARIO = USUARIO;
            l.PROCESSO = PROCESSO;
            l.DATALOGIN = DATALOGIN;
            l.MAQUINANOLOGIN = MAQUINALOGIN;
            l.IPLOGIN = IPLOGIN;
            l.NOMELOGIN = NOMELOGIN;


            using (var client = new HttpClient())
            {
                var serializedUsuario = JsonConvert.SerializeObject(l);
                var content = new StringContent(serializedUsuario, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(URILOG, content);
            }

        }

    }
    class DadosUsuario
    {

        public string IDCLIENTE { get; set; }
        public string USUARIO { get; set; }
        public string NOME { get; set; }
        public string SENHA { get; set; }
        public string EMAIL { get; set; }
        public string HOMOLOGADOIT { get; set; }
        public string CHAVEHOMOLOGADOIT { get; set; }
        public string CONTROLE { get; set; }
        public string ATIVO { get; set; }

    }

    public class Log
    {
        public string IDCLIENTE { get; set; }
        public string USUARIO { get; set; }
        public string PROCESSO { get; set; }
        public string DATALOGIN { get; set; }
        public string MAQUINANOLOGIN { get; set; }
        public string IPLOGIN { get; set; }
        public string NOMELOGIN { get; set; }

    }
}
