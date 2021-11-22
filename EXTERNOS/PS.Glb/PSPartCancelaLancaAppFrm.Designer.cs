namespace PS.Glb
{
    partial class PSPartCancelaLancaAppFrm
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
            this.psTextoBox1 = new PS.Lib.WinForms.PSTextoBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Text = "FrmBaseApp";
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "Motivo";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.DataField = "Motivo";
            this.psTextoBox1.Location = new System.Drawing.Point(8, 6);
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox1.TabIndex = 1;
            // 
            // PSPartCancelaLancaAppFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 364);
            this.Name = "PSPartCancelaLancaAppFrm";
            this.Text = "PSPartCancelaLancaAppFrm";
            this.Load += new System.EventHandler(this.PSPartCancelaLancaAppFrm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
    }
}