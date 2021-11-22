namespace PS.Glb.ERP.Financeiro
{
    partial class FormChequeCadastro
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            AppLib.Windows.CodigoNome codigoNome1 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome2 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.Query query1 = new AppLib.Windows.Query();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.campoTexto1 = new AppLib.Windows.CampoTexto();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.campoData1 = new AppLib.Windows.CampoData();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.campoDecimal1 = new AppLib.Windows.CampoDecimal();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.campoListaPAGREC = new AppLib.Windows.CampoLista();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiroNUMERO = new AppLib.Windows.CampoInteiro();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.campoLookupCODCONTA = new AppLib.Windows.CampoLookup();
            this.campoInteiroCODEMPRESA = new AppLib.Windows.CampoInteiro();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiro1 = new AppLib.Windows.CampoInteiro();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.campoImagem1 = new AppLib.Windows.CampoImagem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(732, 327);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelControl8);
            this.tabPage1.Controls.Add(this.campoTexto1);
            this.tabPage1.Controls.Add(this.labelControl7);
            this.tabPage1.Controls.Add(this.campoData1);
            this.tabPage1.Controls.Add(this.labelControl6);
            this.tabPage1.Controls.Add(this.campoDecimal1);
            this.tabPage1.Controls.Add(this.labelControl5);
            this.tabPage1.Controls.Add(this.campoListaPAGREC);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.campoInteiroNUMERO);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.campoLookupCODCONTA);
            this.tabPage1.Controls.Add(this.campoInteiroCODEMPRESA);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.campoInteiro1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(724, 301);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(20, 180);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(58, 13);
            this.labelControl8.TabIndex = 16;
            this.labelControl8.Text = "Observação";
            // 
            // campoTexto1
            // 
            this.campoTexto1.Campo = "OBSERVACAO";
            this.campoTexto1.Default = null;
            this.campoTexto1.Edita = true;
            this.campoTexto1.Location = new System.Drawing.Point(20, 199);
            this.campoTexto1.MaximoCaracteres = null;
            this.campoTexto1.Name = "campoTexto1";
            this.campoTexto1.Query = 0;
            this.campoTexto1.Size = new System.Drawing.Size(686, 20);
            this.campoTexto1.Tabela = "FCHEQUE";
            this.campoTexto1.TabIndex = 15;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(232, 125);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(44, 13);
            this.labelControl7.TabIndex = 14;
            this.labelControl7.Text = "Data boa";
            // 
            // campoData1
            // 
            this.campoData1.Campo = "DATABOA";
            this.campoData1.Default = null;
            this.campoData1.Edita = true;
            this.campoData1.Location = new System.Drawing.Point(232, 143);
            this.campoData1.Name = "campoData1";
            this.campoData1.Query = 0;
            this.campoData1.Size = new System.Drawing.Size(100, 21);
            this.campoData1.Tabela = "FCHEQUE";
            this.campoData1.TabIndex = 13;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(126, 125);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(24, 13);
            this.labelControl6.TabIndex = 12;
            this.labelControl6.Text = "Valor";
            // 
            // campoDecimal1
            // 
            this.campoDecimal1.Campo = "VALOR";
            this.campoDecimal1.Decimais = 2;
            this.campoDecimal1.Default = null;
            this.campoDecimal1.Edita = true;
            this.campoDecimal1.Location = new System.Drawing.Point(126, 144);
            this.campoDecimal1.Name = "campoDecimal1";
            this.campoDecimal1.Query = 0;
            this.campoDecimal1.Size = new System.Drawing.Size(100, 20);
            this.campoDecimal1.Tabela = "FCHEQUE";
            this.campoDecimal1.TabIndex = 11;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(20, 124);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(78, 13);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "Pagar / Receber";
            // 
            // campoListaPAGREC
            // 
            this.campoListaPAGREC.Campo = "TIPOPAGREC";
            this.campoListaPAGREC.Default = null;
            this.campoListaPAGREC.Edita = true;
            codigoNome1.Codigo = "0";
            codigoNome1.Nome = "Pagar";
            codigoNome2.Codigo = "1";
            codigoNome2.Nome = "Receber";
            this.campoListaPAGREC.Lista = new AppLib.Windows.CodigoNome[] {
        codigoNome1,
        codigoNome2};
            this.campoListaPAGREC.Location = new System.Drawing.Point(20, 143);
            this.campoListaPAGREC.Name = "campoListaPAGREC";
            this.campoListaPAGREC.Query = 0;
            this.campoListaPAGREC.Size = new System.Drawing.Size(100, 21);
            this.campoListaPAGREC.Tabela = "FCHEQUE";
            this.campoListaPAGREC.TabIndex = 9;
            this.campoListaPAGREC.AposSelecao += new AppLib.Windows.CampoLista.AposSelecaoHandler(this.campoListaPAGREC_AposSelecao);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(606, 68);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(37, 13);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Número";
            // 
            // campoInteiroNUMERO
            // 
            this.campoInteiroNUMERO.Campo = "NUMERO";
            this.campoInteiroNUMERO.Default = null;
            this.campoInteiroNUMERO.Edita = true;
            this.campoInteiroNUMERO.Location = new System.Drawing.Point(606, 88);
            this.campoInteiroNUMERO.Name = "campoInteiroNUMERO";
            this.campoInteiroNUMERO.Query = 0;
            this.campoInteiroNUMERO.Size = new System.Drawing.Size(100, 20);
            this.campoInteiroNUMERO.Tabela = "FCHEQUE";
            this.campoInteiroNUMERO.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(20, 68);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(59, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Conta Caixa";
            // 
            // campoLookupCODCONTA
            // 
            this.campoLookupCODCONTA.Campo = "CODCONTA";
            this.campoLookupCODCONTA.ColunaCodigo = "CODCONTA";
            this.campoLookupCODCONTA.ColunaDescricao = "DESCRICAO";
            this.campoLookupCODCONTA.ColunaIdentificador = null;
            this.campoLookupCODCONTA.ColunaTabela = "FCONTA";
            this.campoLookupCODCONTA.Conexao = "Start";
            this.campoLookupCODCONTA.Default = null;
            this.campoLookupCODCONTA.Edita = true;
            this.campoLookupCODCONTA.EditaLookup = false;
            this.campoLookupCODCONTA.Location = new System.Drawing.Point(20, 87);
            this.campoLookupCODCONTA.MaximoCaracteres = null;
            this.campoLookupCODCONTA.Name = "campoLookupCODCONTA";
            this.campoLookupCODCONTA.NomeGrid = null;
            this.campoLookupCODCONTA.Query = 0;
            this.campoLookupCODCONTA.Size = new System.Drawing.Size(580, 21);
            this.campoLookupCODCONTA.Tabela = "FCHEQUE";
            this.campoLookupCODCONTA.TabIndex = 3;
            this.campoLookupCODCONTA.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookupCODCONTA_SetFormConsulta);
            this.campoLookupCODCONTA.SetDescricao += new AppLib.Windows.CampoLookup.SetDescricaoHandler(this.campoLookupCODCONTA_SetDescricao);
            this.campoLookupCODCONTA.AposSelecao += new AppLib.Windows.CampoLookup.AposSelecaoHandler(this.campoLookupCODCONTA_AposSelecao);
            // 
            // campoInteiroCODEMPRESA
            // 
            this.campoInteiroCODEMPRESA.Campo = "CODEMPRESA";
            this.campoInteiroCODEMPRESA.Default = null;
            this.campoInteiroCODEMPRESA.Edita = true;
            this.campoInteiroCODEMPRESA.Location = new System.Drawing.Point(126, 33);
            this.campoInteiroCODEMPRESA.Name = "campoInteiroCODEMPRESA";
            this.campoInteiroCODEMPRESA.Query = 0;
            this.campoInteiroCODEMPRESA.Size = new System.Drawing.Size(100, 20);
            this.campoInteiroCODEMPRESA.Tabela = "FCHEQUE";
            this.campoInteiroCODEMPRESA.TabIndex = 2;
            this.campoInteiroCODEMPRESA.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(63, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Cód. Cheque";
            // 
            // campoInteiro1
            // 
            this.campoInteiro1.Campo = "CODCHEQUE";
            this.campoInteiro1.Default = null;
            this.campoInteiro1.Edita = false;
            this.campoInteiro1.Location = new System.Drawing.Point(20, 33);
            this.campoInteiro1.Name = "campoInteiro1";
            this.campoInteiro1.Query = 0;
            this.campoInteiro1.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro1.Tabela = "FCHEQUE";
            this.campoInteiro1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.campoImagem1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(724, 301);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Imagem";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // campoImagem1
            // 
            this.campoImagem1.ColunaImagem = "IMAGEM";
            this.campoImagem1.ColunaNomeImagem = "NOMEIMAGEM";
            this.campoImagem1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.campoImagem1.Edita = true;
            this.campoImagem1.Location = new System.Drawing.Point(3, 3);
            this.campoImagem1.Name = "campoImagem1";
            this.campoImagem1.Query = 0;
            this.campoImagem1.Size = new System.Drawing.Size(718, 295);
            this.campoImagem1.Tabela = "FCHEQUE";
            this.campoImagem1.TabIndex = 0;
            // 
            // FormChequeCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 427);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormChequeCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM FCHEQUE",
        "WHERE CODEMPRESA = ?",
        "  AND CODCHEQUE = ?"};
            query1.Parametros = new string[] {
        "CODEMPRESA",
        "CODCHEQUE"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "FCHEQUE";
            this.Text = "Cadastro de Cheque";
            this.DuranteSalvar += new AppLib.Windows.FormCadastroData.DuranteSalvarHandler(this.FormChequeCadastro_Load);
            this.AposSalvar += new AppLib.Windows.FormCadastroData.AposSalvarHandler(this.FormChequeCadastro_AposSalvar);
            this.Load += new System.EventHandler(this.FormChequeCadastro_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl2;
        private AppLib.Windows.CampoLookup campoLookupCODCONTA;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private AppLib.Windows.CampoInteiro campoInteiro1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private AppLib.Windows.CampoInteiro campoInteiroNUMERO;
        private AppLib.Windows.CampoInteiro campoInteiroCODEMPRESA;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private AppLib.Windows.CampoLista campoListaPAGREC;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private AppLib.Windows.CampoDecimal campoDecimal1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private AppLib.Windows.CampoData campoData1;
        private AppLib.Windows.CampoImagem campoImagem1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private AppLib.Windows.CampoTexto campoTexto1;
        public System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.TabPage tabPage2;
    }
}