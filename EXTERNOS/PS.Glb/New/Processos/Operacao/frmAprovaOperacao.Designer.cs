namespace PS.Glb.New.Processos.Operacao
{
    partial class frmAprovaOperacao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAprovaLimiteCredito));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescricaoMotivo = new DevExpress.XtraEditors.TextEdit();
            this.btnLookupMotivo = new DevExpress.XtraEditors.SimpleButton();
            this.txtCodMotivo = new DevExpress.XtraEditors.TextEdit();
            this.txtObservacao = new DevExpress.XtraEditors.MemoEdit();
            this.btnFechar = new DevExpress.XtraEditors.SimpleButton();
            this.btnExecutar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricaoMotivo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodMotivo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservacao.Properties)).BeginInit();
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
            this.splitContainer1.Panel2.Controls.Add(this.btnExecutar);
            this.splitContainer1.Size = new System.Drawing.Size(532, 314);
            this.splitContainer1.SplitterDistance = 244;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(532, 244);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.labelControl8);
            this.tabPage1.Controls.Add(this.txtDescricaoMotivo);
            this.tabPage1.Controls.Add(this.btnLookupMotivo);
            this.tabPage1.Controls.Add(this.txtCodMotivo);
            this.tabPage1.Controls.Add(this.txtObservacao);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(524, 218);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(24, 69);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(68, 13);
            this.labelControl1.TabIndex = 36;
            this.labelControl1.Text = "OBSERVACAO";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(24, 21);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(62, 13);
            this.labelControl8.TabIndex = 35;
            this.labelControl8.Text = "CODMOTIVO";
            // 
            // txtDescricaoMotivo
            // 
            this.txtDescricaoMotivo.Enabled = false;
            this.txtDescricaoMotivo.Location = new System.Drawing.Point(164, 38);
            this.txtDescricaoMotivo.Name = "txtDescricaoMotivo";
            this.txtDescricaoMotivo.Size = new System.Drawing.Size(341, 20);
            this.txtDescricaoMotivo.TabIndex = 34;
            // 
            // btnLookupMotivo
            // 
            this.btnLookupMotivo.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.btnLookupMotivo.Appearance.Options.UseBorderColor = true;
            this.btnLookupMotivo.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnLookupMotivo.Location = new System.Drawing.Point(126, 38);
            this.btnLookupMotivo.Name = "btnLookupMotivo";
            this.btnLookupMotivo.Size = new System.Drawing.Size(36, 20);
            this.btnLookupMotivo.TabIndex = 33;
            this.btnLookupMotivo.Text = "...";
            this.btnLookupMotivo.Click += new System.EventHandler(this.btnLookupMotivo_Click);
            // 
            // txtCodMotivo
            // 
            this.txtCodMotivo.Location = new System.Drawing.Point(24, 38);
            this.txtCodMotivo.Name = "txtCodMotivo";
            this.txtCodMotivo.Size = new System.Drawing.Size(100, 20);
            this.txtCodMotivo.TabIndex = 32;
            this.txtCodMotivo.Validated += new System.EventHandler(this.txtCodMotivo_Validated);
            // 
            // txtObservacao
            // 
            this.txtObservacao.Location = new System.Drawing.Point(24, 88);
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.Size = new System.Drawing.Size(480, 115);
            this.txtObservacao.TabIndex = 37;
            // 
            // btnFechar
            // 
            this.btnFechar.Location = new System.Drawing.Point(445, 21);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(75, 23);
            this.btnFechar.TabIndex = 3;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // btnExecutar
            // 
            this.btnExecutar.Location = new System.Drawing.Point(364, 21);
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Size = new System.Drawing.Size(75, 23);
            this.btnExecutar.TabIndex = 2;
            this.btnExecutar.Text = "Executar";
            this.btnExecutar.Click += new System.EventHandler(this.btnExecutar_Click);
            // 
            // frmAprovaLimiteCredito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 314);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAprovaLimiteCredito";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aprovação de Limite de Crédito";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricaoMotivo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodMotivo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservacao.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.SimpleButton btnFechar;
        private DevExpress.XtraEditors.SimpleButton btnExecutar;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtDescricaoMotivo;
        private DevExpress.XtraEditors.SimpleButton btnLookupMotivo;
        private DevExpress.XtraEditors.TextEdit txtCodMotivo;
        private DevExpress.XtraEditors.MemoEdit txtObservacao;
    }
}