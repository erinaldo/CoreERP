using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PS.Glb.ERP.Comercial
{
    public partial class FormConsultaReceitaCPF : Form
    {
        public string CPF;
        public string NOME;
        public string DATA_DE_NASCIMENTO;
        public string SITUACAO_CADASTRAL;
        public string DATA_DA_INSCRICAO;
        public string DIGITO_VERIFICADOR;

        public FormConsultaReceitaCPF()
        {
            InitializeComponent();
        }

        private void FormConsultaSintegraCPF_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://www.receita.fazenda.gov.br/Aplicacoes/ATCTA/cpf/ConsultaPublica.asp");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.ToString() == "about:blank")
            {
                return;
            }

            if (e.Url.AbsoluteUri.Contains("https://www.receita.fazenda.gov.br/Aplicacoes/SSL/ATCTA/CPF/ConsultaPublica.asp"))
            {
                mshtml.HTMLWindow2 w2 = (mshtml.HTMLWindow2)webBrowser1.Document.Window.DomWindow;
                w2.execScript("var ctrlRange = document.body.createControlRange();ctrlRange.add(document.getElementById('imgCaptcha'));ctrlRange.execCommand('Copy');", "javascript");
                pictureBox1.Image = Clipboard.GetImage();
                Clipboard.Clear();

                maskedTextBox1.Enabled = true;
                maskedTextBox2.Enabled = true;
                textBox1.Enabled = true;
                btnConsultar.Enabled = true;
            }

            if (e.Url.AbsoluteUri.Contains("https://www.receita.fazenda.gov.br/Aplicacoes/SSL/ATCTA/CPF/ConsultaPublicaExibir.asp"))
            {
                string[] SplitString;
                string[] lines = Regex.Split(webBrowser1.Document.GetElementById("content-core").OuterText, "\r\n");

                foreach (string line in lines)
                {
                    if (line.Contains("No do CPF"))
                    {
                        SplitString = line.Split(':');
                        CPF = SplitString[1].Replace(" ", "");
                    }
                    if (line.Contains("Nome da Pessoa Física: "))
                    {
                        SplitString = line.Split(':');
                        NOME = SplitString[1].Remove(SplitString[1].Length - 1).Remove(0, 1);
                    }
                    if (line.Contains("Data de Nascimento: "))
                    {
                        SplitString = line.Split(':');
                        DATA_DE_NASCIMENTO = SplitString[1].Remove(SplitString[1].Length - 1).Remove(0, 1);
                    }
                    if (line.Contains("Situação Cadastral: "))
                    {
                        SplitString = line.Split(':');
                        SITUACAO_CADASTRAL = SplitString[1].Remove(SplitString[1].Length - 1).Remove(0, 1);
                    }
                    if (line.Contains("Data da Inscrição: "))
                    {
                        SplitString = line.Split(':');
                        DATA_DA_INSCRICAO = SplitString[1].Remove(SplitString[1].Length - 1).Remove(0, 1);
                    }
                    if (line.Contains("Digito Verificador: "))
                    {
                        SplitString = line.Split(':');
                        DIGITO_VERIFICADOR = SplitString[1].Remove(SplitString[1].Length - 1).Remove(0, 1);
                    }
                }

                textBox2.Text = CPF;
                textBox3.Text = NOME;
                textBox4.Text = DATA_DE_NASCIMENTO;
                textBox5.Text = SITUACAO_CADASTRAL;
                textBox6.Text = DATA_DA_INSCRICAO;
                textBox7.Text = DIGITO_VERIFICADOR;

                maskedTextBox1.Select();
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.GetElementById("txtCPF").InnerText = maskedTextBox1.Text;
            webBrowser1.Document.GetElementById("txtDataNascimento").InnerText = maskedTextBox2.Text;
            webBrowser1.Document.GetElementById("id_submit").InvokeMember("click");

            webBrowser1.Document.GetElementById("txtTexto_captcha_serpro_gov_br").InnerText = textBox1.Text;
            webBrowser1.Document.GetElementById("btnAR2").InvokeMember("click");
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            maskedTextBox1.Enabled = true;
            maskedTextBox2.Enabled = true;
            textBox1.Enabled = true;
            btnConsultar.Enabled = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        
        
    }
}
