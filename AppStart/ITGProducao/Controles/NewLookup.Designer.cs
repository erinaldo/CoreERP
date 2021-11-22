namespace ITGProducao.Controles
{
    partial class NewLookup
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtcodigo = new DevExpress.XtraEditors.TextEdit();
            this.txtconteudo = new DevExpress.XtraEditors.TextEdit();
            this.btnprocurar = new DevExpress.XtraEditors.SimpleButton();
            this.lbltitulo = new DevExpress.XtraEditors.LabelControl();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblerrorprovider = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtcodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtconteudo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // txtcodigo
            // 
            this.txtcodigo.Location = new System.Drawing.Point(4, 23);
            this.txtcodigo.Name = "txtcodigo";
            this.txtcodigo.Size = new System.Drawing.Size(99, 20);
            this.txtcodigo.TabIndex = 0;
            this.txtcodigo.EditValueChanged += new System.EventHandler(this.txtcodigo_EditValueChanged);
            this.txtcodigo.Leave += new System.EventHandler(this.txtcodigo_Leave);
            // 
            // txtconteudo
            // 
            this.txtconteudo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtconteudo.Location = new System.Drawing.Point(149, 23);
            this.txtconteudo.Name = "txtconteudo";
            this.txtconteudo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 7.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtconteudo.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtconteudo.Properties.Appearance.Options.UseFont = true;
            this.txtconteudo.Properties.Appearance.Options.UseForeColor = true;
            this.txtconteudo.Properties.ReadOnly = true;
            this.txtconteudo.Size = new System.Drawing.Size(214, 20);
            this.txtconteudo.TabIndex = 2;
            this.txtconteudo.Click += new System.EventHandler(this.txtconteudo_Click);
            this.txtconteudo.MouseLeave += new System.EventHandler(this.txtconteudo_MouseLeave);
            this.txtconteudo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtconteudo_MouseMove);
            // 
            // btnprocurar
            // 
            this.btnprocurar.Location = new System.Drawing.Point(110, 23);
            this.btnprocurar.Name = "btnprocurar";
            this.btnprocurar.Size = new System.Drawing.Size(32, 20);
            this.btnprocurar.TabIndex = 1;
            this.btnprocurar.Text = "...";
            this.btnprocurar.Click += new System.EventHandler(this.btnprocurar_Click);
            // 
            // lbltitulo
            // 
            this.lbltitulo.Location = new System.Drawing.Point(4, 3);
            this.lbltitulo.Name = "lbltitulo";
            this.lbltitulo.Size = new System.Drawing.Size(36, 13);
            this.lbltitulo.TabIndex = 3;
            this.lbltitulo.Text = "TITULO";
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // lblerrorprovider
            // 
            this.lblerrorprovider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblerrorprovider.Location = new System.Drawing.Point(347, 6);
            this.lblerrorprovider.Name = "lblerrorprovider";
            this.lblerrorprovider.Size = new System.Drawing.Size(0, 13);
            this.lblerrorprovider.TabIndex = 4;
            // 
            // NewLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblerrorprovider);
            this.Controls.Add(this.lbltitulo);
            this.Controls.Add(this.btnprocurar);
            this.Controls.Add(this.txtconteudo);
            this.Controls.Add(this.txtcodigo);
            this.Name = "NewLookup";
            this.Size = new System.Drawing.Size(366, 46);
            this.Load += new System.EventHandler(this.NewLookup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtcodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtconteudo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl lbltitulo;
        public DevExpress.XtraEditors.TextEdit txtcodigo;
        public DevExpress.XtraEditors.TextEdit txtconteudo;
        public System.Windows.Forms.ErrorProvider errorProvider;
        private DevExpress.XtraEditors.LabelControl lblerrorprovider;
        public DevExpress.XtraEditors.SimpleButton btnprocurar;
    }
}
