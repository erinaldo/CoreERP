using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using PS.Lib;
using PS.Lib.WinForms;
using System.Xml.Serialization;
using System.Xml;
using System.Net;
using Newtonsoft.Json;

namespace ERP
{
    public partial class MDILogin : Form
    {
        Dados dados = new Dados();
        UtilitariosXML UtilXML = new UtilitariosXML();

        Arquivo arq = new Arquivo();
        XMLParse xml = new XMLParse();
        AliasList alias = new AliasList();

        public String Versao { get; set; }

        public MDILogin()
        {
            InitializeComponent();
            Versao = GetVersao();
        }

        private void MDILogin_Load(object sender, EventArgs e)
        {
            //Carrega via XML
            if (File.Exists("User.xml"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Dados));
                FileStream fs = new FileStream("User.xml", FileMode.Open);
                dados = (Dados)xs.Deserialize(fs);

                if (dados.Utiliza_Usuario == "1")
                {
                    tb_Usuario.Text = dados.Ultimo_Usuario;
                    tb_Senha.Select();
                }
                else
                {
                    tb_Usuario.Select();
                }
                fs.Close();
            }
            lblVersao.Text = Versao;
            CarregaAlias();
        }

        private void tb_Usuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tb_Senha.Focus();
            }
        }

        private void tb_Senha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                panel_Login_Click(this, null);
            }
        }

        private void cb_Alias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                panel_Login_Click(this, null);
            }
        }

        private void panel_Alias_Click(object sender, EventArgs e)
        {
            PS.AliasManager.FormAliasManager form = new PS.AliasManager.FormAliasManager();
            form.ShowDialog();
            cb_Alias.DataSource = null;
            LoadAlias();
        }

        private void panel_Login_Click(object sender, EventArgs e)
        {
            // Atribui o valor dos campos aos respectivos atributosf
            dados.Ultimo_Usuario = tb_Usuario.Text;
            dados.Ultimo_Alias = cb_Alias.Text;
            dados.Utiliza_Usuario = "1";

            // Atribui o valor digitado ao campo no XML
            SalvarXML.Salvar(dados, "User.xml");
            SalvarXML.Salvar(UtilXML, "CoreERP.config.xml");

            PS.Lib.Login login = new PS.Lib.Login();

            try
            {
                Timeout();

                AppLib.Context.poolConnection.Remove("Start");
                login.Autenticar(alias.AliasGroup[cb_Alias.SelectedIndex], tb_Usuario.Text, tb_Senha.Text, "PV");
                this.DialogResult = DialogResult.OK;
                AppLib.Context.Usuario = tb_Usuario.Text;
                PS.Lib.Contexto context = new Contexto();
                PS.Lib.Contexto.Session.CodUsuario = tb_Usuario.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                tb_Senha.SelectAll();
                return;
            }
        }

        private void panel_Sair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel_Suporte_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://portal.itinit.com.br:8018/site/Default.aspx");
        }

        #region Evento - Mouse Enter / Mouse Leave

        private void panel_Sair_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void panel_Sair_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void panel_Alias_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void panel_Alias_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void panel_Login_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void panel_Login_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void panel_Suporte_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void panel_Suporte_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        #endregion

        #region Métodos

        public String GetVersao()
        {
            String[] versao = Application.ProductVersion.Split('.');

            DateTime data = new DateTime(2000, 1, 1);
            data = data.AddDays(int.Parse(versao[2]));
            data = data.AddSeconds(int.Parse(versao[3]) * 2);

            String result = String.Format("{0:yyyy.MM.dd}", data);
            result += "." + int.Parse(versao[3]);
            return result;
        }

        public void GetAlias()
        {
            String AliasDefault = @"<?xml version=""1.0"" encoding=""utf-16""?>
<AliasList xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <AliasGroup>
    <Alias>
      <Name>Oficial</Name>
      <DbType>SqlServer</DbType>
      <DbProviderType>SQLOleDb</DbProviderType>
      <ServerName>localhost</ServerName>
      <DbName>AppStart_Oficial</DbName>
      <UserName>sa</UserName>
      <Password>123456</Password>
      <SyncService>false</SyncService>
    </Alias>
    <Alias>
      <Name>Teste</Name>
      <DbType>SqlServer</DbType>
      <DbProviderType>SQLOleDb</DbProviderType>
      <ServerName>localhost</ServerName>
      <DbName>AppStart_Teste</DbName>
      <UserName>sa</UserName>
      <Password>123456</Password>
      <SyncService>false</SyncService>
    </Alias>
  </AliasGroup>
</AliasList>";
            if (System.IO.File.Exists("Alias.xml"))
            {
                if (System.IO.File.ReadAllText("Alias.xml").Length < 10)
                {
                    System.IO.File.Delete("Alias.xml");
                }
            }

            if (!System.IO.File.Exists("Alias.xml"))
            {
                System.IO.File.WriteAllText("Alias.xml", AliasDefault);
            }
            alias = (AliasList)xml.Read(arq.Ler("Alias.xml"), new AliasList());
        }

        private void LoadAlias()
        {
            try
            {
                if (cb_Alias.DataSource == null)
                {
                    GetAlias();

                    cb_Alias.Items.Clear();
                    cb_Alias.DisplayMember = "name";
                    cb_Alias.ValueMember = "name";

                    cb_Alias.DataSource = alias.AliasGroup;

                    cb_Alias.SelectedIndex = 0;
                }
                else
                {
                    GetAlias();
                }
                if (File.Exists("User.xml"))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Dados));
                    FileStream fs = new FileStream("User.xml", FileMode.Open);
                    dados = (Dados)xs.Deserialize(fs);
                    cb_Alias.Text = dados.Ultimo_Alias;
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CarregaAlias()
        {
            LoadAlias();
        }

        private void ValidarUsuario()
        {
            if (PS.Lib.Contexto.Session.CodUsuario != tb_Senha.Text && AppLib.Context.Usuario != tb_Usuario.Text)
            {
                MessageBox.Show("Usuário ou senha inválidos.", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            tb_Usuario.Text = "";
            tb_Senha.Text = "";
            tb_Usuario.Select();
        }

        private void Timeout()
        {
            AppLib.Data.SqlClient sql = new AppLib.Data.SqlClient();

            if (File.Exists("CoreERP.config.xml"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(UtilitariosXML));
                FileStream fs = new FileStream("CoreERP.config.xml", FileMode.Open);
                UtilXML = (UtilitariosXML)xs.Deserialize(fs);

                sql.Timeout = UtilXML.Timeout;

                fs.Close();
            }
        }

        #endregion
    }
}
