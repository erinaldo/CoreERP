using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using PS.Lib;

namespace PS.AliasManager
{
    public partial class FormAliasManager : Form
    {
        Arquivo arq = new Arquivo();
        XMLParse xml = new XMLParse();
        AliasList alias = new AliasList();
        bool edicao = false;
        Login login = new Login();

        public FormAliasManager()
        {
            InitializeComponent();
        }

        private void GetAlias()
        {
            alias = (AliasList)xml.Read(arq.Ler("Alias.xml"), new AliasList());
        }

        private void FormAliasManager_Load(object sender, EventArgs e)
        {
            if (arq.Existe("Alias.xml"))
            {
                carregaLista();
                txtName.Enabled = false;
                txtName.Text = alias.AliasGroup[Convert.ToInt32(lstLista.Items[0].Text)].Name;
                txtPassword.Text = alias.AliasGroup[Convert.ToInt32(lstLista.Items[0].Text)].Password;
                txtServerName.Text = alias.AliasGroup[Convert.ToInt32(lstLista.Items[0].Text)].ServerName;
                txtDbName.Text = alias.AliasGroup[Convert.ToInt32(lstLista.Items[0].Text)].DbName;
                txtUserName.Text = alias.AliasGroup[Convert.ToInt32(lstLista.Items[0].Text)].UserName;
                edicao = true;
            }
        }

        private void carregaLista()
        {
            GetAlias();
            propertiesList();
            for (int i = 0; i < alias.AliasGroup.Count; i++)
            {
                lstLista.Items.Add(i.ToString()).SubItems.Add(alias.AliasGroup[i].Name);
            }
        }

        #region Propriedades do listView
        private void propertiesList()
        {
            //Limpa a lista
            this.lstLista.Clear();

            this.lstLista.GridLines = true;
            this.lstLista.FullRowSelect = true;

            this.lstLista.Columns.Add("Alias").DisplayIndex = 0;
            this.lstLista.Columns[0].Width = 0;
            this.lstLista.Columns.Add("Alias").DisplayIndex = 1;
            this.lstLista.Columns[1].Width = 205;
        }
        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtServerName.Text) || string.IsNullOrEmpty(txtDbName.Text) || string.IsNullOrEmpty(txtUserName.Text))
            {
                PS.Lib.PSMessageBox.ShowError("Favor preencher os campos corretamente.");
                return;
            }

            try
            {
                login.TestarConexao(alias.AliasGroup[Convert.ToInt32(lstLista.SelectedItems[0].Index)]);
                PS.Lib.PSMessageBox.ShowInfo("Teste realizado com sucesso.");
            }
            catch (Exception)
            {
                PS.Lib.PSMessageBox.ShowError("Não foi possível conectar ao banco de dados.");
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtServerName.Text) || string.IsNullOrEmpty(txtDbName.Text) || string.IsNullOrEmpty(txtUserName.Text))
            {
                PS.Lib.PSMessageBox.ShowError("Favor preencher os campos corretamente.");
                return;
            }

            if (lstLista.SelectedItems.Count == 0)
            {
                PS.Lib.PSMessageBox.ShowInfo("Favor selecionar um Alias.");
                return;
            }

            if (edicao)
            {
                //alias.AliasGroup[Convert.ToInt32(lstLista.SelectedItems[0].Text)].Password = txtPassword.Text; 

                //criptografa a senha
                alias.AliasGroup[Convert.ToInt32(lstLista.SelectedItems[0].Text)].Password = new PS.Lib.Criptografia().Encoder(PS.Lib.Criptografia.OpcoesEncoder.Rijndael, txtPassword.Text, txtUserName.Text);

                alias.AliasGroup[Convert.ToInt32(lstLista.SelectedItems[0].Text)].ServerName = txtServerName.Text;
                alias.AliasGroup[Convert.ToInt32(lstLista.SelectedItems[0].Text)].UserName = txtUserName.Text;
                alias.AliasGroup[Convert.ToInt32(lstLista.SelectedItems[0].Text)].DbName = txtDbName.Text;
                string aa = xml.Write(alias);
                arq.Escrever("Alias.xml", aa);

                PS.Lib.PSMessageBox.ShowInfo("Alteração realizada com sucesso.");
                carregaLista();
            }
            else
            {
                Alias item = new Alias();
                item.Name = txtName.Text;
                //item.Password = txtPassword.Text; 

                //criptografa a senha
                item.Password = new PS.Lib.Criptografia().Encoder(PS.Lib.Criptografia.OpcoesEncoder.Rijndael, txtPassword.Text, txtUserName.Text);

                item.ServerName = txtServerName.Text;
                item.UserName = txtUserName.Text;
                item.DbName = txtDbName.Text;
                alias.AliasGroup.Add(item);
                string aa = xml.Write(alias);
                arq.Escrever("Alias.xml", aa);

                PS.Lib.PSMessageBox.ShowInfo("Alias incluido com sucesso.");
                carregaLista();
            }
            clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            clear();
            txtName.Enabled = true;
        }

        private void clear()
        {
            txtDbName.Text = "";
            txtName.Text = "";
            txtPassword.Text = "";
            txtServerName.Text = "";
            txtUserName.Text = "";
            edicao = false;
            txtName.Focus();
        }

        private void lstLista_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            alias.AliasGroup.RemoveAt(Convert.ToInt32(lstLista.SelectedItems[0].Text));
            string conteudo = xml.Write(alias);
            arq.Escrever("Alias.xml", conteudo);

            PS.Lib.PSMessageBox.ShowInfo("Alias excluído com sucesso.");
            carregaLista();
            clear();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void lstLista_Click(object sender, EventArgs e)
        {
            txtName.Enabled = false;
            txtName.Text = alias.AliasGroup[Convert.ToInt32(lstLista.SelectedItems[0].Text)].Name;
            txtPassword.Text = alias.AliasGroup[Convert.ToInt32(lstLista.SelectedItems[0].Text)].Password;
            txtServerName.Text = alias.AliasGroup[Convert.ToInt32(lstLista.SelectedItems[0].Text)].ServerName;
            txtDbName.Text = alias.AliasGroup[Convert.ToInt32(lstLista.SelectedItems[0].Text)].DbName;
            txtUserName.Text = alias.AliasGroup[Convert.ToInt32(lstLista.SelectedItems[0].Text)].UserName;
            edicao = true;
        }
    }
}
