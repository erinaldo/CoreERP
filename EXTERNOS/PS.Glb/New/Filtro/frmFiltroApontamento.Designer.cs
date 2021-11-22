namespace PS.Glb.New.Filtro
{
    partial class frmFiltroApontamento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFiltroApontamento));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gbValores = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbOntem = new System.Windows.Forms.RadioButton();
            this.rbNaoIntegrado = new System.Windows.Forms.RadioButton();
            this.rbQuinzenal = new System.Windows.Forms.RadioButton();
            this.rbHoje = new System.Windows.Forms.RadioButton();
            this.rbStatus = new System.Windows.Forms.RadioButton();
            this.rbIdApontamento = new System.Windows.Forms.RadioButton();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.rbAnalistaUsuario = new System.Windows.Forms.RadioButton();
            this.rbPeriodo = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gbPeriodo = new System.Windows.Forms.GroupBox();
            this.rbSegundaQuinzena = new System.Windows.Forms.RadioButton();
            this.rbPrimeiraQuinzena = new System.Windows.Forms.RadioButton();
            this.clAnalistaUsuario = new AppLib.Windows.CampoLookup();
            this.lblAnalistaUsuario = new System.Windows.Forms.Label();
            this.lpStatus = new DevExpress.XtraEditors.LookUpEdit();
            this.lblStatus = new System.Windows.Forms.Label();
            this.dteFinal = new DevExpress.XtraEditors.DateEdit();
            this.dteInicial = new DevExpress.XtraEditors.DateEdit();
            this.lblFinal = new System.Windows.Forms.Label();
            this.lblInicial = new System.Windows.Forms.Label();
            this.lbValor = new System.Windows.Forms.Label();
            this.tbValor = new DevExpress.XtraEditors.TextEdit();
            this.btnFechar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalvar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.gbPeriodo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lpStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFinal.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFinal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteInicial.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteInicial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbValor.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnFechar);
            this.splitContainer1.Panel2.Controls.Add(this.btnSalvar);
            this.splitContainer1.Size = new System.Drawing.Size(316, 382);
            this.splitContainer1.SplitterDistance = 316;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(316, 316);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gbValores);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(308, 290);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gbValores
            // 
            this.gbValores.Location = new System.Drawing.Point(8, 140);
            this.gbValores.Name = "gbValores";
            this.gbValores.Size = new System.Drawing.Size(290, 141);
            this.gbValores.TabIndex = 1;
            this.gbValores.TabStop = false;
            this.gbValores.Text = "Valores";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbOntem);
            this.groupBox1.Controls.Add(this.rbNaoIntegrado);
            this.groupBox1.Controls.Add(this.rbQuinzenal);
            this.groupBox1.Controls.Add(this.rbHoje);
            this.groupBox1.Controls.Add(this.rbStatus);
            this.groupBox1.Controls.Add(this.rbIdApontamento);
            this.groupBox1.Controls.Add(this.rbTodos);
            this.groupBox1.Controls.Add(this.rbAnalistaUsuario);
            this.groupBox1.Controls.Add(this.rbPeriodo);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 134);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // rbOntem
            // 
            this.rbOntem.AutoSize = true;
            this.rbOntem.Location = new System.Drawing.Point(6, 111);
            this.rbOntem.Name = "rbOntem";
            this.rbOntem.Size = new System.Drawing.Size(56, 17);
            this.rbOntem.TabIndex = 14;
            this.rbOntem.TabStop = true;
            this.rbOntem.Text = "Ontem";
            this.rbOntem.UseVisualStyleBackColor = true;
            this.rbOntem.CheckedChanged += new System.EventHandler(this.rbOntem_CheckedChanged);
            // 
            // rbNaoIntegrado
            // 
            this.rbNaoIntegrado.AutoSize = true;
            this.rbNaoIntegrado.Location = new System.Drawing.Point(181, 88);
            this.rbNaoIntegrado.Name = "rbNaoIntegrado";
            this.rbNaoIntegrado.Size = new System.Drawing.Size(93, 17);
            this.rbNaoIntegrado.TabIndex = 13;
            this.rbNaoIntegrado.Text = "Não Integrado";
            this.rbNaoIntegrado.UseVisualStyleBackColor = true;
            this.rbNaoIntegrado.CheckedChanged += new System.EventHandler(this.rbNaoIntegrado_CheckedChanged);
            // 
            // rbQuinzenal
            // 
            this.rbQuinzenal.AutoSize = true;
            this.rbQuinzenal.Location = new System.Drawing.Point(181, 65);
            this.rbQuinzenal.Name = "rbQuinzenal";
            this.rbQuinzenal.Size = new System.Drawing.Size(72, 17);
            this.rbQuinzenal.TabIndex = 12;
            this.rbQuinzenal.Text = "Quinzenal";
            this.rbQuinzenal.UseVisualStyleBackColor = true;
            this.rbQuinzenal.CheckedChanged += new System.EventHandler(this.rbQuinzenal_CheckedChanged);
            // 
            // rbHoje
            // 
            this.rbHoje.AutoSize = true;
            this.rbHoje.Location = new System.Drawing.Point(6, 88);
            this.rbHoje.Name = "rbHoje";
            this.rbHoje.Size = new System.Drawing.Size(47, 17);
            this.rbHoje.TabIndex = 11;
            this.rbHoje.TabStop = true;
            this.rbHoje.Text = "Hoje";
            this.rbHoje.UseVisualStyleBackColor = true;
            this.rbHoje.CheckedChanged += new System.EventHandler(this.rbHoje_CheckedChanged);
            // 
            // rbStatus
            // 
            this.rbStatus.AutoSize = true;
            this.rbStatus.Location = new System.Drawing.Point(181, 19);
            this.rbStatus.Name = "rbStatus";
            this.rbStatus.Size = new System.Drawing.Size(55, 17);
            this.rbStatus.TabIndex = 8;
            this.rbStatus.Text = "Status";
            this.rbStatus.UseVisualStyleBackColor = true;
            this.rbStatus.CheckedChanged += new System.EventHandler(this.rbStatus_CheckedChanged);
            // 
            // rbIdApontamento
            // 
            this.rbIdApontamento.AutoSize = true;
            this.rbIdApontamento.Location = new System.Drawing.Point(6, 42);
            this.rbIdApontamento.Name = "rbIdApontamento";
            this.rbIdApontamento.Size = new System.Drawing.Size(83, 17);
            this.rbIdApontamento.TabIndex = 7;
            this.rbIdApontamento.Text = "Identificador";
            this.rbIdApontamento.UseVisualStyleBackColor = true;
            this.rbIdApontamento.CheckedChanged += new System.EventHandler(this.rbIdApontamento_CheckedChanged);
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Checked = true;
            this.rbTodos.Location = new System.Drawing.Point(6, 19);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(55, 17);
            this.rbTodos.TabIndex = 6;
            this.rbTodos.TabStop = true;
            this.rbTodos.Text = "Todos";
            this.rbTodos.UseVisualStyleBackColor = true;
            this.rbTodos.CheckedChanged += new System.EventHandler(this.rbTodos_CheckedChanged);
            // 
            // rbAnalistaUsuario
            // 
            this.rbAnalistaUsuario.AutoSize = true;
            this.rbAnalistaUsuario.Location = new System.Drawing.Point(181, 42);
            this.rbAnalistaUsuario.Name = "rbAnalistaUsuario";
            this.rbAnalistaUsuario.Size = new System.Drawing.Size(103, 17);
            this.rbAnalistaUsuario.TabIndex = 1;
            this.rbAnalistaUsuario.Text = "Analista/Usuário";
            this.rbAnalistaUsuario.UseVisualStyleBackColor = true;
            this.rbAnalistaUsuario.CheckedChanged += new System.EventHandler(this.rbAnalistaUsuario_CheckedChanged);
            // 
            // rbPeriodo
            // 
            this.rbPeriodo.AutoSize = true;
            this.rbPeriodo.Location = new System.Drawing.Point(6, 65);
            this.rbPeriodo.Name = "rbPeriodo";
            this.rbPeriodo.Size = new System.Drawing.Size(63, 17);
            this.rbPeriodo.TabIndex = 0;
            this.rbPeriodo.Text = "Período";
            this.rbPeriodo.UseVisualStyleBackColor = true;
            this.rbPeriodo.CheckedChanged += new System.EventHandler(this.rbPeriodo_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gbPeriodo);
            this.tabPage2.Controls.Add(this.clAnalistaUsuario);
            this.tabPage2.Controls.Add(this.lblAnalistaUsuario);
            this.tabPage2.Controls.Add(this.lpStatus);
            this.tabPage2.Controls.Add(this.lblStatus);
            this.tabPage2.Controls.Add(this.dteFinal);
            this.tabPage2.Controls.Add(this.dteInicial);
            this.tabPage2.Controls.Add(this.lblFinal);
            this.tabPage2.Controls.Add(this.lblInicial);
            this.tabPage2.Controls.Add(this.lbValor);
            this.tabPage2.Controls.Add(this.tbValor);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(308, 290);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Filtros";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gbPeriodo
            // 
            this.gbPeriodo.Controls.Add(this.rbSegundaQuinzena);
            this.gbPeriodo.Controls.Add(this.rbPrimeiraQuinzena);
            this.gbPeriodo.Location = new System.Drawing.Point(23, 124);
            this.gbPeriodo.Name = "gbPeriodo";
            this.gbPeriodo.Size = new System.Drawing.Size(257, 45);
            this.gbPeriodo.TabIndex = 32;
            this.gbPeriodo.TabStop = false;
            this.gbPeriodo.Text = "Período";
            // 
            // rbSegundaQuinzena
            // 
            this.rbSegundaQuinzena.AutoSize = true;
            this.rbSegundaQuinzena.Location = new System.Drawing.Point(183, 19);
            this.rbSegundaQuinzena.Name = "rbSegundaQuinzena";
            this.rbSegundaQuinzena.Size = new System.Drawing.Size(68, 17);
            this.rbSegundaQuinzena.TabIndex = 3;
            this.rbSegundaQuinzena.TabStop = true;
            this.rbSegundaQuinzena.Text = "Segunda";
            this.rbSegundaQuinzena.UseVisualStyleBackColor = true;
            // 
            // rbPrimeiraQuinzena
            // 
            this.rbPrimeiraQuinzena.AutoSize = true;
            this.rbPrimeiraQuinzena.Location = new System.Drawing.Point(6, 19);
            this.rbPrimeiraQuinzena.Name = "rbPrimeiraQuinzena";
            this.rbPrimeiraQuinzena.Size = new System.Drawing.Size(62, 17);
            this.rbPrimeiraQuinzena.TabIndex = 2;
            this.rbPrimeiraQuinzena.TabStop = true;
            this.rbPrimeiraQuinzena.Text = "Primeira";
            this.rbPrimeiraQuinzena.UseVisualStyleBackColor = true;
            // 
            // clAnalistaUsuario
            // 
            this.clAnalistaUsuario.Campo = null;
            this.clAnalistaUsuario.ColunaCodigo = "CODUSUARIO";
            this.clAnalistaUsuario.ColunaDescricao = "NOME";
            this.clAnalistaUsuario.ColunaIdentificador = null;
            this.clAnalistaUsuario.ColunaTabela = "(select CODUSUARIO, NOME from GUSUARIO) I";
            this.clAnalistaUsuario.Conexao = "Start";
            this.clAnalistaUsuario.Default = null;
            this.clAnalistaUsuario.Edita = true;
            this.clAnalistaUsuario.EditaLookup = false;
            this.clAnalistaUsuario.Location = new System.Drawing.Point(20, 19);
            this.clAnalistaUsuario.MaximoCaracteres = null;
            this.clAnalistaUsuario.Name = "clAnalistaUsuario";
            this.clAnalistaUsuario.NomeGrid = null;
            this.clAnalistaUsuario.Query = 0;
            this.clAnalistaUsuario.Size = new System.Drawing.Size(278, 21);
            this.clAnalistaUsuario.Tabela = null;
            this.clAnalistaUsuario.TabIndex = 31;
            this.clAnalistaUsuario.Visible = false;
            // 
            // lblAnalistaUsuario
            // 
            this.lblAnalistaUsuario.AutoSize = true;
            this.lblAnalistaUsuario.Location = new System.Drawing.Point(20, 3);
            this.lblAnalistaUsuario.Name = "lblAnalistaUsuario";
            this.lblAnalistaUsuario.Size = new System.Drawing.Size(85, 13);
            this.lblAnalistaUsuario.TabIndex = 30;
            this.lblAnalistaUsuario.Text = "Analista/Usuário";
            this.lblAnalistaUsuario.Visible = false;
            // 
            // lpStatus
            // 
            this.lpStatus.Location = new System.Drawing.Point(116, 98);
            this.lpStatus.Name = "lpStatus";
            this.lpStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lpStatus.Size = new System.Drawing.Size(138, 20);
            this.lpStatus.TabIndex = 29;
            this.lpStatus.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(113, 82);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 28;
            this.lblStatus.Text = "Status";
            this.lblStatus.Visible = false;
            // 
            // dteFinal
            // 
            this.dteFinal.EditValue = null;
            this.dteFinal.Location = new System.Drawing.Point(154, 59);
            this.dteFinal.Name = "dteFinal";
            this.dteFinal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFinal.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFinal.Size = new System.Drawing.Size(100, 20);
            this.dteFinal.TabIndex = 27;
            this.dteFinal.Visible = false;
            // 
            // dteInicial
            // 
            this.dteInicial.EditValue = null;
            this.dteInicial.Location = new System.Drawing.Point(35, 59);
            this.dteInicial.Name = "dteInicial";
            this.dteInicial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteInicial.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteInicial.Size = new System.Drawing.Size(100, 20);
            this.dteInicial.TabIndex = 26;
            this.dteInicial.Visible = false;
            // 
            // lblFinal
            // 
            this.lblFinal.AutoSize = true;
            this.lblFinal.Location = new System.Drawing.Point(151, 43);
            this.lblFinal.Name = "lblFinal";
            this.lblFinal.Size = new System.Drawing.Size(29, 13);
            this.lblFinal.TabIndex = 25;
            this.lblFinal.Text = "Final";
            this.lblFinal.Visible = false;
            // 
            // lblInicial
            // 
            this.lblInicial.AutoSize = true;
            this.lblInicial.Location = new System.Drawing.Point(32, 43);
            this.lblInicial.Name = "lblInicial";
            this.lblInicial.Size = new System.Drawing.Size(34, 13);
            this.lblInicial.TabIndex = 24;
            this.lblInicial.Text = "Inicial";
            this.lblInicial.Visible = false;
            // 
            // lbValor
            // 
            this.lbValor.AutoSize = true;
            this.lbValor.Location = new System.Drawing.Point(8, 82);
            this.lbValor.Name = "lbValor";
            this.lbValor.Size = new System.Drawing.Size(31, 13);
            this.lbValor.TabIndex = 17;
            this.lbValor.Text = "Valor";
            this.lbValor.Visible = false;
            // 
            // tbValor
            // 
            this.tbValor.Location = new System.Drawing.Point(8, 98);
            this.tbValor.Name = "tbValor";
            this.tbValor.Size = new System.Drawing.Size(100, 20);
            this.tbValor.TabIndex = 16;
            this.tbValor.Visible = false;
            // 
            // btnFechar
            // 
            this.btnFechar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnFechar.ImageOptions.Image")));
            this.btnFechar.Location = new System.Drawing.Point(227, 21);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(75, 29);
            this.btnFechar.TabIndex = 13;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvar.ImageOptions.Image")));
            this.btnSalvar.Location = new System.Drawing.Point(140, 21);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(81, 29);
            this.btnSalvar.TabIndex = 12;
            this.btnSalvar.Text = "Executar";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // frmFiltroApontamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 382);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmFiltroApontamento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtro de Apontamentos";
            this.Load += new System.EventHandler(this.frmFiltroOperacao_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.gbPeriodo.ResumeLayout(false);
            this.gbPeriodo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lpStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFinal.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFinal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteInicial.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteInicial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbValor.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton btnFechar;
        public DevExpress.XtraEditors.SimpleButton btnSalvar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox gbValores;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbNaoIntegrado;
        private System.Windows.Forms.RadioButton rbQuinzenal;
        private System.Windows.Forms.RadioButton rbHoje;
        private System.Windows.Forms.RadioButton rbStatus;
        private System.Windows.Forms.RadioButton rbIdApontamento;
        private System.Windows.Forms.RadioButton rbTodos;
        private System.Windows.Forms.RadioButton rbAnalistaUsuario;
        private System.Windows.Forms.RadioButton rbPeriodo;
        private System.Windows.Forms.TabPage tabPage2;
        private DevExpress.XtraEditors.LookUpEdit lpStatus;
        private System.Windows.Forms.Label lblStatus;
        private DevExpress.XtraEditors.DateEdit dteFinal;
        private DevExpress.XtraEditors.DateEdit dteInicial;
        private System.Windows.Forms.Label lblFinal;
        private System.Windows.Forms.Label lblInicial;
        private System.Windows.Forms.Label lbValor;
        private DevExpress.XtraEditors.TextEdit tbValor;
        private AppLib.Windows.CampoLookup clAnalistaUsuario;
        private System.Windows.Forms.Label lblAnalistaUsuario;
        private System.Windows.Forms.GroupBox gbPeriodo;
        private System.Windows.Forms.RadioButton rbSegundaQuinzena;
        private System.Windows.Forms.RadioButton rbPrimeiraQuinzena;
        private System.Windows.Forms.RadioButton rbOntem;
    }
}