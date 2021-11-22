namespace Relatorios
{
    partial class StReportLancamentoApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StReportLancamentoApp));
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
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
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.cmbTipoData = new System.Windows.Forms.ComboBox();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cmbTipoOrdem = new System.Windows.Forms.ComboBox();
            this.cmbGrupo = new System.Windows.Forms.ComboBox();
            this.cmbOrdem = new System.Windows.Forms.ComboBox();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.cmbFilial = new System.Windows.Forms.ComboBox();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.psDateBoxFin = new PS.Lib.WinForms.PSDateBox();
            this.cmbEmpresa = new System.Windows.Forms.ComboBox();
            this.psDateBoxIni = new PS.Lib.WinForms.PSDateBox();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
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
            this.tabControl1.Size = new System.Drawing.Size(894, 401);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Size = new System.Drawing.Size(886, 375);
            this.tabPage1.Text = "Emissão de Relatório";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(382, 29);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(20, 13);
            this.labelControl6.TabIndex = 14;
            this.labelControl6.Text = "Tipo";
            // 
            // cmbTipo
            // 
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "RECEBER",
            "PAGAR",
            "TODOS"});
            this.cmbTipo.Location = new System.Drawing.Point(382, 45);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(97, 21);
            this.cmbTipo.TabIndex = 13;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(485, 29);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(31, 13);
            this.labelControl7.TabIndex = 16;
            this.labelControl7.Text = "Status";
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "ABERTO",
            "BAIXADO",
            "CANCELADO",
            "TODOS"});
            this.cmbStatus.Location = new System.Drawing.Point(485, 45);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(100, 21);
            this.cmbStatus.TabIndex = 15;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
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
            this.groupBox2.Size = new System.Drawing.Size(867, 263);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opicional";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(801, 41);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(27, 23);
            this.btnAdd.TabIndex = 22;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click_1);
            // 
            // btnRemove
            // 
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.Location = new System.Drawing.Point(834, 41);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(27, 23);
            this.btnRemove.TabIndex = 21;
            // 
            // dgvParam
            // 
            this.dgvParam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParam.Location = new System.Drawing.Point(9, 70);
            this.dgvParam.Name = "dgvParam";
            this.dgvParam.Size = new System.Drawing.Size(852, 184);
            this.dgvParam.TabIndex = 20;
            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(430, 43);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(365, 21);
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
            this.groupBox1.Controls.Add(this.labelControl12);
            this.groupBox1.Controls.Add(this.cmbTipoData);
            this.groupBox1.Controls.Add(this.labelControl11);
            this.groupBox1.Controls.Add(this.labelControl5);
            this.groupBox1.Controls.Add(this.cmbTipoOrdem);
            this.groupBox1.Controls.Add(this.cmbGrupo);
            this.groupBox1.Controls.Add(this.labelControl7);
            this.groupBox1.Controls.Add(this.cmbStatus);
            this.groupBox1.Controls.Add(this.cmbOrdem);
            this.groupBox1.Controls.Add(this.cmbTipo);
            this.groupBox1.Controls.Add(this.labelControl6);
            this.groupBox1.Controls.Add(this.labelControl8);
            this.groupBox1.Controls.Add(this.cmbFilial);
            this.groupBox1.Controls.Add(this.labelControl9);
            this.groupBox1.Controls.Add(this.psDateBoxFin);
            this.groupBox1.Controls.Add(this.cmbEmpresa);
            this.groupBox1.Controls.Add(this.psDateBoxIni);
            this.groupBox1.Controls.Add(this.labelControl10);
            this.groupBox1.Location = new System.Drawing.Point(11, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(867, 82);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Obrigatórios";
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(94, 29);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(61, 13);
            this.labelControl12.TabIndex = 30;
            this.labelControl12.Text = "Tipo de Data";
            // 
            // cmbTipoData
            // 
            this.cmbTipoData.FormattingEnabled = true;
            this.cmbTipoData.Items.AddRange(new object[] {
            "EMISSÃO",
            "BAIXA",
            "VENCIMENTO",
            "PREV. BAIXA"});
            this.cmbTipoData.Location = new System.Drawing.Point(94, 45);
            this.cmbTipoData.Name = "cmbTipoData";
            this.cmbTipoData.Size = new System.Drawing.Size(82, 21);
            this.cmbTipoData.TabIndex = 29;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(591, 29);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(47, 13);
            this.labelControl11.TabIndex = 28;
            this.labelControl11.Text = "Agrupado";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(801, 29);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(62, 13);
            this.labelControl5.TabIndex = 24;
            this.labelControl5.Text = "Tp da Ordem";
            // 
            // cmbTipoOrdem
            // 
            this.cmbTipoOrdem.FormattingEnabled = true;
            this.cmbTipoOrdem.Items.AddRange(new object[] {
            "CRES",
            "DECR"});
            this.cmbTipoOrdem.Location = new System.Drawing.Point(801, 45);
            this.cmbTipoOrdem.Name = "cmbTipoOrdem";
            this.cmbTipoOrdem.Size = new System.Drawing.Size(60, 21);
            this.cmbTipoOrdem.TabIndex = 23;
            // 
            // cmbGrupo
            // 
            this.cmbGrupo.FormattingEnabled = true;
            this.cmbGrupo.Items.AddRange(new object[] {
            "TIPO DOCUMENTO",
            "DATA EMISSÃO",
            "DATA VENCIMENTO",
            "DATA PREV. BAIXA",
            "DATA BAIXA",
            "CLIENTE"});
            this.cmbGrupo.Location = new System.Drawing.Point(591, 45);
            this.cmbGrupo.Name = "cmbGrupo";
            this.cmbGrupo.Size = new System.Drawing.Size(104, 21);
            this.cmbGrupo.TabIndex = 27;
            // 
            // cmbOrdem
            // 
            this.cmbOrdem.FormattingEnabled = true;
            this.cmbOrdem.Items.AddRange(new object[] {
            "NÚMERO DOC",
            "TIPO DOCUMENTO",
            "DATA EMISSÃO",
            "DATA VENCIMENTO",
            "DATA PREV. BAIXA",
            "DATA BAIXA",
            "CLIENTE"});
            this.cmbOrdem.Location = new System.Drawing.Point(701, 45);
            this.cmbOrdem.Name = "cmbOrdem";
            this.cmbOrdem.Size = new System.Drawing.Size(94, 21);
            this.cmbOrdem.TabIndex = 22;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(701, 29);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(32, 13);
            this.labelControl8.TabIndex = 21;
            this.labelControl8.Text = "Ordem";
            // 
            // cmbFilial
            // 
            this.cmbFilial.FormattingEnabled = true;
            this.cmbFilial.Location = new System.Drawing.Point(56, 45);
            this.cmbFilial.Name = "cmbFilial";
            this.cmbFilial.Size = new System.Drawing.Size(32, 21);
            this.cmbFilial.TabIndex = 19;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(56, 29);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(20, 13);
            this.labelControl9.TabIndex = 20;
            this.labelControl9.Text = "Filial";
            // 
            // psDateBoxFin
            // 
            this.psDateBoxFin.Caption = "Data Final";
            this.psDateBoxFin.Chave = true;
            this.psDateBoxFin.DataField = "Data Final";
            this.psDateBoxFin.Location = new System.Drawing.Point(282, 29);
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
            this.psDateBoxIni.Location = new System.Drawing.Point(182, 29);
            this.psDateBoxIni.Mascara = "00/00/0000";
            this.psDateBoxIni.MaxLength = 32767;
            this.psDateBoxIni.Name = "psDateBoxIni";
            this.psDateBoxIni.Size = new System.Drawing.Size(94, 37);
            this.psDateBoxIni.TabIndex = 17;
            this.psDateBoxIni.Value = new System.DateTime(2015, 11, 11, 0, 0, 0, 0);
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(9, 29);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(41, 13);
            this.labelControl10.TabIndex = 12;
            this.labelControl10.Text = "Empresa";
            // 
            // StReportLancamentoApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 527);
            this.Name = "StReportLancamentoApp";
            this.Text = "StReportLancamento";
            this.Load += new System.EventHandler(this.StReportLancamentoApp_Load);
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

        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.ComboBox cmbTipo;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
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
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.ComboBox cmbTipoOrdem;
        private System.Windows.Forms.ComboBox cmbOrdem;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private System.Windows.Forms.ComboBox cmbFilial;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private PS.Lib.WinForms.PSDateBox psDateBoxFin;
        private System.Windows.Forms.ComboBox cmbEmpresa;
        private PS.Lib.WinForms.PSDateBox psDateBoxIni;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private System.Windows.Forms.ComboBox cmbGrupo;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private System.Windows.Forms.ComboBox cmbTipoData;


    }
}