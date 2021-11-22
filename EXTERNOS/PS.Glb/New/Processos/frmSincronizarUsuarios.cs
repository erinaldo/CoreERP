using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.Glb.New.Processos
{
    public partial class frmSincronizarUsuarios : Form
    {
        public frmSincronizarUsuarios()
        {
            InitializeComponent();
        }

        public string URI = "http://portal.itinit.com.br:8021/api/usuario";
        public string URICLIENTE = "http://portal.itinit.com.br:8021/api/cliente";

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            //label1.Text = "Sincronizando...";
            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("Sincronizando...");

            Thread t = new Thread(sincronizar);
            t.Start();

            splashScreenManager1.CloseWaitForm();
            MessageBox.Show("Sincronização realizada com sucesso!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Dispose();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Métodos 

        private void sincronizar()
        {
            Random r = new Random();

            string controleITINIT = "";
            string idCliente = "";
            string nomeCliente = "";
            string cnpjCliente = "";
            string QTDLICENCA = "";

            //recupera o CNPJ da empresa LOCAL
            string cnpj = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "select CGCCPF from GEMPRESA where CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }).ToString();
            cnpj = cnpj.Replace("/", "").Replace("-", "").Replace(".", "");

            //recupera o ID do Cliente, nome, cnpj e qtdlicença webservice
            using (WebClient wc = new WebClient())
            {
                var j = wc.DownloadString(URICLIENTE + "/" + cnpj);
                dynamic array = JsonConvert.DeserializeObject(j);
                foreach (var item in array)
                {
                    idCliente = item.IDCLIENTE;
                    nomeCliente = item.NOMECLIENTE;
                    cnpjCliente = item.CNPJ;
                    QTDLICENCA = item.QTDLICENCA;

                }
            }

            //recupera os dados do usuario local
            List<DadosUsuario> listaUsuarioLocal = new List<DadosUsuario>();
            DataTable dtUsuarioLocal = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODUSUARIO,NOME,SENHA,CONTROLE,EMAIL, ATIVO FROM GUSUARIO order by CODUSUARIO", new object[] { });
            foreach (DataRow item in dtUsuarioLocal.Rows)
            {
                if (String.IsNullOrEmpty(item["CONTROLE"].ToString()))
                    controleITINIT = "";//ITService.ControleITINIT.GerarControle(idCliente,cnpj,item["CODUSUARIO"].ToString()).ToString(); 
                else
                    controleITINIT = item["CONTROLE"].ToString();

                listaUsuarioLocal.Add(new DadosUsuario { USUARIO = item["CODUSUARIO"].ToString(), NOME = item["NOME"].ToString(), SENHA = item["SENHA"].ToString(), EMAIL = item["EMAIL"].ToString(), ATIVO = item["ATIVO"].ToString(), CONTROLE = controleITINIT });
            }

            //recupera os dados dos usuarios itinit web
            List<DadosUsuarioITINIT> listaUsuarioAppItInit = new List<DadosUsuarioITINIT>();
            using (WebClient wc = new WebClient())
            {
                var j = wc.DownloadString(URI);
                dynamic array = JsonConvert.DeserializeObject(j);
                foreach (var item in array)
                {
                    listaUsuarioAppItInit.Add(new DadosUsuarioITINIT { IDCLIENTE = item.IDCLIENTE, USUARIO = item.USUARIO, NOME = item.NOME, SENHA = item.SENHA, EMAIL = item.EMAIL, HOMOLOGADOIT = item.HOMOLOGADOIT, CHAVEHOMOLOGADOIT = item.CHAVEHOMOLOGADOIT, CONTROLE = item.CONTROLE, ATIVO = item.ATIVO });
                }
            }

            //para cada usuario do core local, verificar se existe no appitinit
            foreach (var item in listaUsuarioLocal)
            {
                //já existe entao atualiza
                if (buscaUsuario(item.USUARIO, listaUsuarioAppItInit))
                {
                    AtualizaUsuario(idCliente, item.USUARIO, item.NOME, item.SENHA, item.EMAIL, item.HOMOLOGADOIT, item.CHAVEHOMOLOGADOIT, item.CONTROLE, item.ATIVO);
                }
                else
                {
                    AdicionaUsuario(idCliente, item.USUARIO, item.NOME, item.SENHA, item.EMAIL, item.HOMOLOGADOIT, item.CHAVEHOMOLOGADOIT, item.CONTROLE, item.ATIVO);
                }

            }

            string sqlAtualiza = "";
            foreach (var item in listaUsuarioAppItInit)
            {
                sqlAtualiza = "UPDATE GUSUARIO SET CONTROLE='" + item.CONTROLE + "' where CODUSUARIO = '" + item.USUARIO + "'";
                gravaNaBaseLocal(sqlAtualiza);

            }

            //label1.Invoke((Action)delegate { label1.Text = "Fim da Sincronização"; });
        }

        private bool buscaUsuario(string usuario, List<DadosUsuarioITINIT> ls)
        {
            bool r = false;
            foreach (var item in ls)
            {
                if (item.USUARIO == usuario)
                {
                    r = true;
                    break;
                }
                else
                    r = false;

            }
            return r;
        }

        private void gravaNaBaseLocal(string sql)
        {
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(AppLib.Context.poolConnection.Get("Start").ConnectionString);
            System.Data.SqlClient.SqlCommand command;
            try
            {
                conn.Open();

                command = new System.Data.SqlClient.SqlCommand(sql, conn);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        private async void AdicionaUsuario(string IDCLIENTE, string USUARIO, string NOME, string SENHA, string EMAIL, string HOMOLOGADOIT, string CHAVEHOMOLOGADOIT, string CONTROLE, string ATIVO)
        {
            DadosUsuario us = new DadosUsuario();
            us.IDCLIENTE = IDCLIENTE;
            us.USUARIO = USUARIO;
            us.NOME = NOME;
            us.SENHA = SENHA;
            us.EMAIL = EMAIL;
            us.HOMOLOGADOIT = HOMOLOGADOIT;
            us.CHAVEHOMOLOGADOIT = CHAVEHOMOLOGADOIT;
            us.CONTROLE = CONTROLE;
            us.ATIVO = ATIVO;

            using (var client = new HttpClient())
            {
                var serializedUsuario = JsonConvert.SerializeObject(us);
                var content = new StringContent(serializedUsuario, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(URI, content);
            }
        }

        private async void AtualizaUsuario(string IDCLIENTE, string USUARIO, string NOME, string SENHA, string EMAIL, string HOMOLOGADOIT, string CHAVEHOMOLOGADOIT, string CONTROLE, string ATIVO)
        {
            DadosUsuario us = new DadosUsuario();
            us.IDCLIENTE = IDCLIENTE;
            us.USUARIO = USUARIO.Replace('.', '_');
            us.NOME = NOME;
            us.SENHA = SENHA;
            us.EMAIL = EMAIL;
            us.HOMOLOGADOIT = HOMOLOGADOIT;
            us.CHAVEHOMOLOGADOIT = CHAVEHOMOLOGADOIT;
            us.CONTROLE = CONTROLE;
            us.ATIVO = ATIVO;

            using (var client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.PutAsJsonAsync(URI + "/" + USUARIO.Replace('.', '_'), us);
            }
        }

        private string buscaControle(string usuario, List<DadosUsuarioITINIT> ls)
        {
            string r = "";
            foreach (var item in ls)
            {
                if (item.USUARIO == usuario)
                {
                    r = item.CONTROLE;
                    break;
                }
                else
                    r = "";
            }
            return r;
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

        class DadosUsuarioITINIT
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

        #endregion
    }
}
