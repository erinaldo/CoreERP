namespace PS.Glb
{
    partial class PSPartBancoEdit
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
            this.psTextoBox3 = new PS.Lib.WinForms.PSTextoBox();
            this.psImageBox1 = new PS.Lib.WinForms.PSImageBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(647, 258);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psImageBox1);
            this.tabPage1.Controls.Add(this.psTextoBox3);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(639, 232);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 320);
            this.panel2.Size = new System.Drawing.Size(647, 54);
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(647, 30);
            // 
            // buttonSALVAR
            // 
            this.buttonSALVAR.Location = new System.Drawing.Point(370, 15);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(461, 15);
            // 
            // buttonCANCELAR
            // 
            this.buttonCANCELAR.Location = new System.Drawing.Point(551, 15);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODBANCO";
            this.psTextoBox1.DataField = "CODBANCO";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 5;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 0;
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "NOME";
            this.psTextoBox2.DataField = "NOME";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.Location = new System.Drawing.Point(11, 50);
            this.psTextoBox2.MaxLength = 80;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox2.TabIndex = 1;
            // 
            // psTextoBox3
            // 
            this.psTextoBox3.Caption = "CODFEBRABAN";
            this.psTextoBox3.DataField = "CODFEBRABAN";
            this.psTextoBox3.Edita = true;
            this.psTextoBox3.Location = new System.Drawing.Point(11, 93);
            this.psTextoBox3.MaxLength = 3;
            this.psTextoBox3.Name = "psTextoBox3";
            this.psTextoBox3.PasswordChar = '\0';
            this.psTextoBox3.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox3.TabIndex = 2;
            // 
            // psImageBox1
            // 
            this.psImageBox1.DataField = "CODIMAGEM";
            this.psImageBox1.IdImagem = 0;
            this.psImageBox1.Location = new System.Drawing.Point(453, 20);
            this.psImageBox1.Name = "psImageBox1";
            this.psImageBox1.Size = new System.Drawing.Size(150, 150);
            this.psImageBox1.TabIndex = 3;
            this.psImageBox1.Load += new System.EventHandler(this.psImageBox1_Load);
            // 
            // PSPartBancoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartBancoEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartBancoEdit";
            this.Load += new System.EventHandler(this.PSPartBancoEdit_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox3;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private Lib.WinForms.PSImageBox psImageBox1;
    }
}