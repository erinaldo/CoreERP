namespace Relatorios
{
    partial class StReportExtratoBancarioApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StReportExtratoBancarioApp));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnRemove = new DevExpress.XtraEditors.SimpleButton();
            this.dgvParam = new System.Windows.Forms.DataGridView();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.lstLista = new System.Windows.Forms.ListView();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cmbCondicao = new System.Windows.Forms.ComboBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cmbCampo = new System.Windows.Forms.ComboBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbOperador = new System.Windows.Forms.ComboBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.cmbTipoOrdem = new System.Windows.Forms.ComboBox();
            this.cmbOrdem = new System.Windows.Forms.ComboBox();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.cmbFilial = new System.Windows.Forms.ComboBox();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.psContaCaixa = new PS.Lib.WinForms.PSLookup();
            this.psDateBoxFin = new PS.Lib.WinForms.PSDateBox();
            this.cmbEmpresa = new System.Windows.Forms.ComboBox();
            this.psDateBoxIni = new PS.Lib.WinForms.PSDateBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParam)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(777, 401);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Size = new System.Drawing.Size(769, 375);
            this.tabPage1.Text = "Emissão de Relatório";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.btnRemove);
            this.groupBox2.Controls.Add(this.dgvParam);
            this.groupBox2.Controls.Add(this.txtValor);
            this.groupBox2.Controls.Add(this.lstLista);
            this.groupBox2.Controls.Add(this.labelControl4);
            this.groupBox2.Controls.Add(this.cmbCondicao);
            this.groupBox2.Controls.Add(this.labelControl3);
            this.groupBox2.Controls.Add(this.cmbCampo);
            this.groupBox2.Controls.Add(this.labelControl2);
            this.groupBox2.Controls.Add(this.cmbOperador);
            this.groupBox2.Controls.Add(this.labelControl1);
            this.groupBox2.Location = new System.Drawing.Point(11, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(747, 263);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opicional";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(678, 41);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(27, 23);
            this.btnAdd.TabIndex = 22;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click_1);
            // 
            // btnRemove
            // 
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.Location = new System.Drawing.Point(711, 41);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(27, 23);
            this.btnRemove.TabIndex = 21;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click_1);
            // 
            // dgvParam
            // 
            this.dgvParam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParam.Location = new System.Drawing.Point(9, 70);
            this.dgvParam.Name = "dgvParam";
            this.dgvParam.Size = new System.Drawing.Size(729, 184);
            this.dgvParam.TabIndex = 20;
            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(430, 43);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(241, 21);
            this.txtValor.TabIndex = 19;
            // 
            // lstLista
            // 
            this.lstLista.Location = new System.Drawing.Point(59, 90);
            this.lstLista.Name = "lstLista";
            this.lstLista.Size = new System.Drawing.Size(29, 16);
            this.lstLista.TabIndex = 0;
            this.lstLista.UseCompatibleStateImageBehavior = false;
            this.lstLista.View = System.Windows.Forms.View.Details;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(430, 24);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 13);
            this.labelControl4.TabIndex = 17;
            this.labelControl4.Text = "Valor";
            // 
            // cmbCondicao
            // 
            this.cmbCondicao.FormattingEnabled = true;
            this.cmbCondicao.Items.AddRange(new object[] {
            "IGUAL A",
            "IGUAL A VÁRIOS",
            "DIFERENTE DE",
            "DIFERENTE DE VÁRIOS",
            "MAIOR QUE",
            "MENOR QUE",
            "MAIOR OU IGUAL",
            "MENOR OU IGUAL",
            "NULO",
            "NÃO NULO",
            "CONTÉM",
            "NÃO CONTÉM"});
            this.cmbCondicao.Location = new System.Drawing.Point(303, 43);
            this.cmbCondicao.Name = "cmbCondicao";
            this.cmbCondicao.Size = new System.Drawing.Size(121, 21);
            this.cmbCondicao.TabIndex = 16;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(303, 24);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(44, 13);
            this.labelControl3.TabIndex = 15;
            this.labelControl3.Text = "Condição";
            // 
            // cmbCampo
            // 
            this.cmbCampo.FormattingEnabled = true;
            this.cmbCampo.Items.AddRange(new object[] {
            "NUMERODOC",
            "HISTORICO"});
            this.cmbCampo.Location = new System.Drawing.Point(103, 43);
            this.cmbCampo.Name = "cmbCampo";
            this.cmbCampo.Size = new System.Drawing.Size(194, 21);
            this.cmbCampo.TabIndex = 14;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(103, 24);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(33, 13);
            this.labelControl2.TabIndex = 13;
            this.labelControl2.Text = "Campo";
            // 
            // cmbOperador
            // 
            this.cmbOperador.FormattingEnabled = true;
            this.cmbOperador.Items.AddRange(new object[] {
            "E",
            "OU"});
            this.cmbOperador.Location = new System.Drawing.Point(9, 43);
            this.cmbOperador.Name = "cmbOperador";
            this.cmbOperador.Size = new System.Drawing.Size(88, 21);
            this.cmbOperador.TabIndex = 12;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 24);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(46, 13);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "Operador";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelControl7);
            this.groupBox1.Controls.Add(this.cmbTipoOrdem);
            this.groupBox1.Controls.Add(this.cmbOrdem);
            this.groupBox1.Controls.Add(this.labelControl6);
            this.groupBox1.Controls.Add(this.cmbFilial);
            this.groupBox1.Controls.Add(this.labelControl8);
            this.groupBox1.Controls.Add(this.psContaCaixa);
            this.groupBox1.Controls.Add(this.psDateBoxFin);
            this.groupBox1.Controls.Add(this.cmbEmpresa);
            this.groupBox1.Controls.Add(this.psDateBoxIni);
            this.groupBox1.Controls.Add(this.labelControl5);
            this.groupBox1.Location = new System.Drawing.Point(11, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(747, 82);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Obrigatórios";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(645, 29);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(70, 13);
            this.labelControl7.TabIndex = 24;
            this.labelControl7.Text = "Tipo da Ordem";
            this.labelControl7.Visible = false;
            // 
            // cmbTipoOrdem
            // 
            this.cmbTipoOrdem.FormattingEnabled = true;
            this.cmbTipoOrdem.Items.AddRange(new object[] {
            "CRESCENTE",
            "DECRESCENTE"});
            this.cmbTipoOrdem.Location = new System.Drawing.Point(644, 45);
            this.cmbTipoOrdem.Name = "cmbTipoOrdem";
            this.cmbTipoOrdem.Size = new System.Drawing.Size(94, 21);
            this.cmbTipoOrdem.TabIndex = 23;
            this.cmbTipoOrdem.Visible = false;
            // 
            // cmbOrdem
            // 
            this.cmbOrdem.FormattingEnabled = true;
            this.cmbOrdem.Items.AddRange(new object[] {
            "NUMERODOC",
            "HISTORICO"});
            this.cmbOrdem.Location = new System.Drawing.Point(545, 45);
            this.cmbOrdem.Name = "cmbOrdem";
            this.cmbOrdem.Size = new System.Drawing.Size(94, 21);
            this.cmbOrdem.TabIndex = 22;
            this.cmbOrdem.Visible = false;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(545, 29);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(32, 13);
            this.labelControl6.TabIndex = 21;
            this.labelControl6.Text = "Ordem";
            this.labelControl6.Visible = false;
            // 
            // cmbFilial
            // 
            this.cmbFilial.FormattingEnabled = true;
            this.cmbFilial.Location = new System.Drawing.Point(56, 45);
            this.cmbFilial.Name = "cmbFilial";
            this.cmbFilial.Size = new System.Drawing.Size(41, 21);
            this.cmbFilial.TabIndex = 19;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(56, 29);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(20, 13);
            this.labelControl8.TabIndex = 20;
            this.labelControl8.Text = "Filial";
            // 
            // psContaCaixa
            // 
            this.psContaCaixa.Caption = "Conta Caixa";
            this.psContaCaixa.Chave = true;
            this.psContaCaixa.DataField = "Conta Caixa";
            this.psContaCaixa.Description = "";
            this.psContaCaixa.DinamicTable = null;
            this.psContaCaixa.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psContaCaixa.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psContaCaixa.KeyField = "CODCONTA";
            this.psContaCaixa.Location = new System.Drawing.Point(303, 28);
            this.psContaCaixa.LookupField = "CODCONTA;DESCRICAO";
            this.psContaCaixa.LookupFieldResult = "CODCONTA;DESCRICAO";
            this.psContaCaixa.MaxLength = 15;
            this.psContaCaixa.Name = "psContaCaixa";
            this.psContaCaixa.PSPart = "PSPartConta";
            this.psContaCaixa.Size = new System.Drawing.Size(435, 38);
            this.psContaCaixa.TabIndex = 6;
            this.psContaCaixa.ValorRetorno = null;
            // 
            // psDateBoxFin
            // 
            this.psDateBoxFin.Caption = "Data Final";
            this.psDateBoxFin.Chave = true;
            this.psDateBoxFin.DataField = "Data Final";
            this.psDateBoxFin.Location = new System.Drawing.Point(203, 29);
            this.psDateBoxFin.Mascara = "00/00/0000";
            this.psDateBoxFin.MaxLength = 32767;
            this.psDateBoxFin.Name = "psDateBoxFin";
            this.psDateBoxFin.Size = new System.Drawing.Size(94, 37);
            this.psDateBoxFin.TabIndex = 18;
            this.psDateBoxFin.Value = new System.DateTime(2015, 11, 11, 0, 0, 0, 0);
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(9, 45);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(41, 21);
            this.cmbEmpresa.TabIndex = 11;
            // 
            // psDateBoxIni
            // 
            this.psDateBoxIni.Caption = "Data Inicial";
            this.psDateBoxIni.Chave = true;
            this.psDateBoxIni.DataField = "Data Inicial";
            this.psDateBoxIni.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.psDateBoxIni.Location = new System.Drawing.Point(103, 29);
            this.psDateBoxIni.Mascara = "00/00/0000";
            this.psDateBoxIni.MaxLength = 32767;
            this.psDateBoxIni.Name = "psDateBoxIni";
            this.psDateBoxIni.Size = new System.Drawing.Size(94, 37);
            this.psDateBoxIni.TabIndex = 17;
            this.psDateBoxIni.Value = new System.DateTime(2015, 11, 11, 0, 0, 0, 0);
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(9, 29);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(41, 13);
            this.labelControl5.TabIndex = 12;
            this.labelControl5.Text = "Empresa";
            // 
            // StReportExtratoBancarioApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 527);
            this.Name = "StReportExtratoBancarioApp";
            this.Text = "StReportExtratoBancarioApp";
            this.Load += new System.EventHandler(this.StReportExtratoBancarioApp_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParam)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnRemove;
        private System.Windows.Forms.DataGridView dgvParam;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.ListView lstLista;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.ComboBox cmbCondicao;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.ComboBox cmbCampo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.ComboBox cmbOperador;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private System.Windows.Forms.ComboBox cmbTipoOrdem;
        private System.Windows.Forms.ComboBox cmbOrdem;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private System.Windows.Forms.ComboBox cmbFilial;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private PS.Lib.WinForms.PSLookup psContaCaixa;
        private PS.Lib.WinForms.PSDateBox psDateBoxFin;
        private System.Windows.Forms.ComboBox cmbEmpresa;
        private PS.Lib.WinForms.PSDateBox psDateBoxIni;
        private DevExpress.XtraEditors.LabelControl labelControl5;


    }
}