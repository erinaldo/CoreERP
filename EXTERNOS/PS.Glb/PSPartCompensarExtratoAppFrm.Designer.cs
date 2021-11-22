namespace PS.Glb
{
    partial class PSPartCompensarExtratoAppFrm
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
            this.psDateBox1 = new PS.Lib.WinForms.PSDateBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psDateBox1);
            this.tabPage1.Text = "FrmBaseApp";
            // 
            // psDateBox1
            // 
            this.psDateBox1.Caption = "Data da Compensação";
            this.psDateBox1.Chave = true;
            this.psDateBox1.DataField = "Data da Compensação";
            this.psDateBox1.Location = new System.Drawing.Point(11, 6);
            this.psDateBox1.Mascara = "00/00/0000";
            this.psDateBox1.MaxLength = 32767;
            this.psDateBox1.Name = "psDateBox1";
            this.psDateBox1.Size = new System.Drawing.Size(146, 37);
            this.psDateBox1.TabIndex = 0;
            this.psDateBox1.Value = new System.DateTime(2015, 10, 9, 9, 36, 36, 914);
            // 
            // PSPartCompensarExtratoAppFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 364);
            this.Name = "PSPartCompensarExtratoAppFrm";
            this.Text = "PSPartCompensarExtratoAppFrm";
            this.Load += new System.EventHandler(this.PSPartCompensarExtratoAppFrm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Lib.WinForms.PSDateBox psDateBox1;
    }
}