namespace PS.Glb
{
    partial class PSPartCancelarNFeAppFrm
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
            this.psMemoBox1 = new PS.Lib.WinForms.PSMemoBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psMemoBox1);
            this.tabPage1.Text = "FrmBaseApp";
            // 
            // psMemoBox1
            // 
            this.psMemoBox1.Caption = "Motivo do Cancelamento";
            this.psMemoBox1.Chave = true;
            this.psMemoBox1.DataField = "Motivo do Cancelamento";
            this.psMemoBox1.Location = new System.Drawing.Point(11, 6);
            this.psMemoBox1.Name = "psMemoBox1";
            this.psMemoBox1.Size = new System.Drawing.Size(609, 200);
            this.psMemoBox1.TabIndex = 0;
            // 
            // PSPartCancelarNFeAppFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 364);
            this.Name = "PSPartCancelarNFeAppFrm";
            this.Text = "PSPartCancelarNFeAppFrm";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Lib.WinForms.PSMemoBox psMemoBox1;

    }
}