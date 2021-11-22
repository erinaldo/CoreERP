namespace PS.Glb.New.Filtro
{
    partial class frmFiltroOperacao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFiltroOperacao));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtValor = new DevExpress.XtraEditors.TextEdit();
            this.dtData = new DevExpress.XtraEditors.DateEdit();
            this.lblValor = new DevExpress.XtraEditors.LabelControl();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbAprovacao = new System.Windows.Forms.RadioButton();
            this.rbSituacao = new System.Windows.Forms.RadioButton();
            this.rbNumeroOperacao = new System.Windows.Forms.RadioButton();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.rbStatus = new System.Windows.Forms.RadioButton();
            this.rbMes = new System.Windows.Forms.RadioButton();
            this.rbPeriodo = new System.Windows.Forms.RadioButton();
            this.rbHoje = new System.Windows.Forms.RadioButton();
            this.rbEmissao = new System.Windows.Forms.RadioButton();
            this.rbCliente = new System.Windows.Forms.RadioButton();
            this.btnFechar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalvar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtValor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtData.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtData.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(316, 316);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(308, 290);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtValor);
            this.groupBox2.Controls.Add(this.dtData);
            this.groupBox2.Controls.Add(this.lblValor);
            this.groupBox2.Controls.Add(this.cmbStatus);
            this.groupBox2.Location = new System.Drawing.Point(8, 146);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(290, 135);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Valores";
            // 
            // txtValor
            // 
            this.txtValor.Location = new System.Drawing.Point(63, 40);
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(146, 20);
            this.txtValor.TabIndex = 3;
            this.txtValor.Visible = false;
            // 
            // dtData
            // 
            this.dtData.EditValue = null;
            this.dtData.Location = new System.Drawing.Point(63, 40);
            this.dtData.Name = "dtData";
            this.dtData.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtData.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtData.Size = new System.Drawing.Size(146, 20);
            this.dtData.TabIndex = 2;
            this.dtData.Visible = false;
            // 
            // lblValor
            // 
            this.lblValor.Location = new System.Drawing.Point(63, 17);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(24, 13);
            this.lblValor.TabIndex = 1;
            this.lblValor.Text = "Valor";
            this.lblValor.Visible = false;
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(63, 39);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(146, 21);
            this.cmbStatus.TabIndex = 0;
            this.cmbStatus.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbAprovacao);
            this.groupBox1.Controls.Add(this.rbSituacao);
            this.groupBox1.Controls.Add(this.rbNumeroOperacao);
            this.groupBox1.Controls.Add(this.rbTodos);
            this.groupBox1.Controls.Add(this.rbStatus);
            this.groupBox1.Controls.Add(this.rbMes);
            this.groupBox1.Controls.Add(this.rbPeriodo);
            this.groupBox1.Controls.Add(this.rbHoje);
            this.groupBox1.Controls.Add(this.rbEmissao);
            this.groupBox1.Controls.Add(this.rbCliente);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 136);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // rbAprovacao
            // 
            this.rbAprovacao.AutoSize = true;
            this.rbAprovacao.Location = new System.Drawing.Point(198, 65);
            this.rbAprovacao.Name = "rbAprovacao";
            this.rbAprovacao.Size = new System.Drawing.Size(77, 17);
            this.rbAprovacao.TabIndex = 9;
            this.rbAprovacao.TabStop = true;
            this.rbAprovacao.Text = "Aprovação";
            this.rbAprovacao.UseVisualStyleBackColor = true;
            this.rbAprovacao.CheckedChanged += new System.EventHandler(this.rbAprovacao_CheckedChanged);
            // 
            // rbSituacao
            // 
            this.rbSituacao.AutoSize = true;
            this.rbSituacao.Location = new System.Drawing.Point(31, 65);
            this.rbSituacao.Name = "rbSituacao";
            this.rbSituacao.Size = new System.Drawing.Size(67, 17);
            this.rbSituacao.TabIndex = 8;
            this.rbSituacao.TabStop = true;
            this.rbSituacao.Text = "Situação";
            this.rbSituacao.UseVisualStyleBackColor = true;
            this.rbSituacao.CheckedChanged += new System.EventHandler(this.rbSituacao_CheckedChanged);
            // 
            // rbNumeroOperacao
            // 
            this.rbNumeroOperacao.AutoSize = true;
            this.rbNumeroOperacao.Location = new System.Drawing.Point(31, 111);
            this.rbNumeroOperacao.Name = "rbNumeroOperacao";
            this.rbNumeroOperacao.Size = new System.Drawing.Size(112, 17);
            this.rbNumeroOperacao.TabIndex = 7;
            this.rbNumeroOperacao.TabStop = true;
            this.rbNumeroOperacao.Text = "Número Operação";
            this.rbNumeroOperacao.UseVisualStyleBackColor = true;
            this.rbNumeroOperacao.CheckedChanged += new System.EventHandler(this.rbNumeroOperacao_CheckedChanged);
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Location = new System.Drawing.Point(198, 111);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(55, 17);
            this.rbTodos.TabIndex = 6;
            this.rbTodos.TabStop = true;
            this.rbTodos.Text = "Todos";
            this.rbTodos.UseVisualStyleBackColor = true;
            // 
            // rbStatus
            // 
            this.rbStatus.AutoSize = true;
            this.rbStatus.Location = new System.Drawing.Point(198, 88);
            this.rbStatus.Name = "rbStatus";
            this.rbStatus.Size = new System.Drawing.Size(55, 17);
            this.rbStatus.TabIndex = 5;
            this.rbStatus.TabStop = true;
            this.rbStatus.Text = "Status";
            this.rbStatus.UseVisualStyleBackColor = true;
            this.rbStatus.CheckedChanged += new System.EventHandler(this.rbStatus_CheckedChanged);
            // 
            // rbMes
            // 
            this.rbMes.AutoSize = true;
            this.rbMes.Location = new System.Drawing.Point(198, 42);
            this.rbMes.Name = "rbMes";
            this.rbMes.Size = new System.Drawing.Size(64, 17);
            this.rbMes.TabIndex = 4;
            this.rbMes.TabStop = true;
            this.rbMes.Text = "Por Mês";
            this.rbMes.UseVisualStyleBackColor = true;
            this.rbMes.CheckedChanged += new System.EventHandler(this.rbMes_CheckedChanged);
            // 
            // rbPeriodo
            // 
            this.rbPeriodo.AutoSize = true;
            this.rbPeriodo.Location = new System.Drawing.Point(198, 19);
            this.rbPeriodo.Name = "rbPeriodo";
            this.rbPeriodo.Size = new System.Drawing.Size(63, 17);
            this.rbPeriodo.TabIndex = 3;
            this.rbPeriodo.TabStop = true;
            this.rbPeriodo.Text = "Período";
            this.rbPeriodo.UseVisualStyleBackColor = true;
            // 
            // rbHoje
            // 
            this.rbHoje.AutoSize = true;
            this.rbHoje.Location = new System.Drawing.Point(31, 88);
            this.rbHoje.Name = "rbHoje";
            this.rbHoje.Size = new System.Drawing.Size(47, 17);
            this.rbHoje.TabIndex = 2;
            this.rbHoje.TabStop = true;
            this.rbHoje.Text = "Hoje";
            this.rbHoje.UseVisualStyleBackColor = true;
            // 
            // rbEmissao
            // 
            this.rbEmissao.AutoSize = true;
            this.rbEmissao.Location = new System.Drawing.Point(31, 42);
            this.rbEmissao.Name = "rbEmissao";
            this.rbEmissao.Size = new System.Drawing.Size(64, 17);
            this.rbEmissao.TabIndex = 1;
            this.rbEmissao.TabStop = true;
            this.rbEmissao.Text = "Emissão";
            this.rbEmissao.UseVisualStyleBackColor = true;
            this.rbEmissao.CheckedChanged += new System.EventHandler(this.rbEmissao_CheckedChanged);
            // 
            // rbCliente
            // 
            this.rbCliente.AutoSize = true;
            this.rbCliente.Location = new System.Drawing.Point(31, 19);
            this.rbCliente.Name = "rbCliente";
            this.rbCliente.Size = new System.Drawing.Size(57, 17);
            this.rbCliente.TabIndex = 0;
            this.rbCliente.TabStop = true;
            this.rbCliente.Text = "Cliente";
            this.rbCliente.UseVisualStyleBackColor = true;
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
            // frmFiltroOperacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 382);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFiltroOperacao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtro de Operações";
            this.Load += new System.EventHandler(this.frmFiltroOperacao_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtValor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtData.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtData.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbMes;
        private System.Windows.Forms.RadioButton rbPeriodo;
        private System.Windows.Forms.RadioButton rbHoje;
        private System.Windows.Forms.RadioButton rbEmissao;
        private System.Windows.Forms.RadioButton rbCliente;
        private DevExpress.XtraEditors.DateEdit dtData;
        private DevExpress.XtraEditors.LabelControl lblValor;
        private System.Windows.Forms.ComboBox cmbStatus;
        private DevExpress.XtraEditors.SimpleButton btnFechar;
        private DevExpress.XtraEditors.SimpleButton btnSalvar;
        private System.Windows.Forms.RadioButton rbTodos;
        private System.Windows.Forms.RadioButton rbStatus;
        private System.Windows.Forms.RadioButton rbNumeroOperacao;
        private DevExpress.XtraEditors.TextEdit txtValor;
        private System.Windows.Forms.RadioButton rbAprovacao;
        private System.Windows.Forms.RadioButton rbSituacao;
    }
}