
namespace PS.Glb.New.Processos
{
    partial class frmReenviarEmail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReenviarEmail));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.tsCC = new DevExpress.XtraEditors.ToggleSwitch();
            this.tsCCo = new DevExpress.XtraEditors.ToggleSwitch();
            this.tsEmailsAdicionais = new DevExpress.XtraEditors.ToggleSwitch();
            this.tsPara = new DevExpress.XtraEditors.ToggleSwitch();
            this.btnEnviar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::PS.Glb.WaitForm2), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tsCC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tsCCo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tsEmailsAdicionais.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tsPara.Properties)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.xtraTabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnEnviar);
            this.splitContainer1.Panel2.Controls.Add(this.btnCancelar);
            this.splitContainer1.Size = new System.Drawing.Size(407, 130);
            this.splitContainer1.SplitterDistance = 93;
            this.splitContainer1.TabIndex = 0;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(407, 93);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.tsCC);
            this.xtraTabPage1.Controls.Add(this.tsCCo);
            this.xtraTabPage1.Controls.Add(this.tsEmailsAdicionais);
            this.xtraTabPage1.Controls.Add(this.tsPara);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(401, 65);
            this.xtraTabPage1.Text = "Seleção de Emails";
            // 
            // tsCC
            // 
            this.tsCC.Location = new System.Drawing.Point(233, 3);
            this.tsCC.Name = "tsCC";
            this.tsCC.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.tsCC.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.tsCC.Properties.OffText = "Prestador";
            this.tsCC.Properties.OnText = "Prestador";
            this.tsCC.Size = new System.Drawing.Size(161, 24);
            this.tsCC.TabIndex = 1;
            this.tsCC.Toggled += new System.EventHandler(this.tsCC_Toggled);
            // 
            // tsCCo
            // 
            this.tsCCo.Location = new System.Drawing.Point(233, 33);
            this.tsCCo.Name = "tsCCo";
            this.tsCCo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.tsCCo.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.tsCCo.Properties.OffText = "Usuário";
            this.tsCCo.Properties.OnText = "Usuário";
            this.tsCCo.Size = new System.Drawing.Size(161, 24);
            this.tsCCo.TabIndex = 2;
            this.tsCCo.Toggled += new System.EventHandler(this.tsCCo_Toggled);
            // 
            // tsEmailsAdicionais
            // 
            this.tsEmailsAdicionais.Location = new System.Drawing.Point(11, 33);
            this.tsEmailsAdicionais.Name = "tsEmailsAdicionais";
            this.tsEmailsAdicionais.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.tsEmailsAdicionais.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.tsEmailsAdicionais.Properties.OffText = "Adicionais";
            this.tsEmailsAdicionais.Properties.OnText = "Adicionais";
            this.tsEmailsAdicionais.Size = new System.Drawing.Size(161, 24);
            this.tsEmailsAdicionais.TabIndex = 3;
            this.tsEmailsAdicionais.Toggled += new System.EventHandler(this.tsEmailsAdicionais_Toggled);
            // 
            // tsPara
            // 
            this.tsPara.Location = new System.Drawing.Point(11, 3);
            this.tsPara.Name = "tsPara";
            this.tsPara.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.tsPara.Properties.OffText = "Cliente";
            this.tsPara.Properties.OnText = "Cliente";
            this.tsPara.Size = new System.Drawing.Size(161, 24);
            this.tsPara.TabIndex = 0;
            this.tsPara.Toggled += new System.EventHandler(this.tsPara_Toggled);
            // 
            // btnEnviar
            // 
            this.btnEnviar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEnviar.ImageOptions.Image")));
            this.btnEnviar.Location = new System.Drawing.Point(239, 2);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(75, 23);
            this.btnEnviar.TabIndex = 0;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.ImageOptions.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(320, 2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // frmReenviarEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 130);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmReenviarEmail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Processo de Reenvio de E-mail";
            this.Load += new System.EventHandler(this.frmReenviarEmail_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tsCC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tsCCo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tsEmailsAdicionais.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tsPara.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton btnEnviar;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraEditors.ToggleSwitch tsPara;
        private DevExpress.XtraEditors.ToggleSwitch tsCC;
        private DevExpress.XtraEditors.ToggleSwitch tsCCo;
        private DevExpress.XtraEditors.ToggleSwitch tsEmailsAdicionais;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}