namespace PS.Glb
{
    partial class PSPartFeriadoEdit
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
            this.psTextoBox1 = new PS.Lib.WinForms.PSTextoBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Controls.Add(this.psDateBox1);
            // 
            // psDateBox1
            // 
            this.psDateBox1.Caption = "DATA";
            this.psDateBox1.Chave = true;
            this.psDateBox1.DataField = "DATA";
            this.psDateBox1.Location = new System.Drawing.Point(11, 6);
            this.psDateBox1.Mascara = "00/00/0000";
            this.psDateBox1.Name = "psDateBox1";
            this.psDateBox1.Size = new System.Drawing.Size(146, 37);
            this.psDateBox1.TabIndex = 0;
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "NOME";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.DataField = "NOME";
            this.psTextoBox1.Location = new System.Drawing.Point(12, 49);
            this.psTextoBox1.MaxLength = 80;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(400, 37);
            this.psTextoBox1.TabIndex = 1;
            // 
            // PSPartFeriadoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartFeriadoEdit";
            this.PermiteEditar = true;
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartFeriadoEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSDateBox psDateBox1;

    }
}