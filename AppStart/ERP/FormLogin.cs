using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PS.Lib;
using PS.Lib.WinForms;

namespace ERP
{
    //Teste commit
    public partial class FormLogin : DevExpress.XtraEditors.XtraForm
    {
        Arquivo arq = new Arquivo();
        XMLParse xml = new XMLParse();
        AliasList alias = new AliasList();

        public String Versao { get; set; }

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

        public FormLogin()
        {
            InitializeComponent();
            Versao = GetVersao();
        }

        private void GetAlias()
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

            if ( ! System.IO.File.Exists("Alias.xml"))
            {
                System.IO.File.WriteAllText("Alias.xml", AliasDefault);
            }

            alias = (AliasList)xml.Read(arq.Ler("Alias.xml"), new AliasList());
            
        }

        private void Clear()
        {
            psTextoBoxUSUARIO.Text = string.Empty;
            psTextoBoxSENHA.Text = string.Empty;
            psTextoBoxUSUARIO.Focus();
            LoadAlias();
        }

        private void LoadAlias()
        {
            try
            {
                if (comboBox1.DataSource == null)
                {
                    GetAlias();

                    comboBox1.Items.Clear();
                    comboBox1.DisplayMember = "name";
                    comboBox1.ValueMember = "name";

                    comboBox1.DataSource = alias.AliasGroup;

                    comboBox1.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
            }
        }

        private void buttonSAIR_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            lblVersao.Text = Versao;

            Clear();

            //if (System.Windows.Forms.SystemInformation.ComputerName.ToUpper().Equals("AHOIII"))
            //{
            //    psTextoBoxUSUARIO.Text = "master";
            //    psTextoBoxSENHA.Text = "master";
            //    buttonENTRAR_Click(this, null);
            //}
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            Keys flag = e.KeyCode;

            if (flag.Equals(Keys.Enter))
            {
                SelectNextControl(ActiveControl, !e.Shift, true, true, true);
                e.Handled = true;
            }
            */
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.itinit.com.br");
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.itinit.com.br");
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.itinit.com.br/atendimento.html");
        }

        private void psTextoBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                psTextoBoxSENHA.Focus();
            }
        }

        private void psTextoBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonENTRAR_Click_1(this, null);
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonENTRAR_Click_1(this, null);
            }
        }

        private void btnAlias_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PS.AliasManager.FormAliasManager form = new PS.AliasManager.FormAliasManager();
            form.ShowDialog();
            comboBox1.DataSource = null;
            LoadAlias();
        }

        private void buttonENTRAR_Click_1(object sender, EventArgs e)
        {
            PS.Lib.Login login = new PS.Lib.Login();

            try
            {
                login.Autenticar(alias.AliasGroup[comboBox1.SelectedIndex], psTextoBoxUSUARIO.Text, psTextoBoxSENHA.Text, "PV");
                this.DialogResult = DialogResult.OK;
                AppLib.Context.Usuario = psTextoBoxUSUARIO.Text;
                PS.Lib.Contexto context = new Contexto();

                PS.Lib.Contexto.Session.CodUsuario = psTextoBoxUSUARIO.Text;
               
                //Controle controle = new Controle();
                //if (controle.TrataLicencaNoLogin(psTextoBoxUSUARIO.Text))
                //{
                //    this.Close();
                //}
                //else
                //{
                //    AppLib.Windows.FormMessageDefault.ShowError("Você atingiu o total de licenças disponíveis ;-(");
                //}
            }
            catch (Exception ex)
            {
                PSMessageBox.ShowError(ex.Message);
                Clear();
            }
        }

        private void buttonSAIR_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            EnviarEmail(); 
        }
        public void EnviarEmail()
        {
            string De = "workflow@itinit.com.br";
            string Para = @"fabio.campos1809@gmail.com";
            string Assunto = @"Workflow IT INIT";
            string Host = "smtp.itinit.com.br";
            int Porta = 587;
            string Usuario = De;
            string Senha = @"it@101@2015%12";
            int Timeout = 3600;
            string Mensagem = @"teste";

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(Host, Porta);
            client.Timeout = Timeout;
            client.UseDefaultCredentials = false;

            System.Net.NetworkCredential netcred = new System.Net.NetworkCredential(Usuario, Senha);
            client.Credentials = netcred;

            client.EnableSsl = false;

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(De, Para, Assunto, Mensagem);
            message.IsBodyHtml = true;

            client.Send(message);
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void psTextoBoxSENHA_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
