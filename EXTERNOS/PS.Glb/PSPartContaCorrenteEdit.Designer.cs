namespace PS.Glb
{
    partial class PSPartContaCorrenteEdit
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
            this.psTextoBox2 = new PS.Lib.WinForms.PSTextoBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "NUMCONTA";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.DataField = "NUMCONTA";
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 15;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 0;
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "DIGCONTA";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.DataField = "DIGCONTA";
            this.psTextoBox2.Location = new System.Drawing.Point(162, 6);
            this.psTextoBox2.MaxLength = 2;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(61, 37);
            this.psTextoBox2.TabIndex = 1;
            // 
            // PSPartContaCorrenteEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartContaCorrenteEdit";
            this.PermiteEditar = true;
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartContaCorrenteEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
    }
}