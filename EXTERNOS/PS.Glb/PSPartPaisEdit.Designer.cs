namespace PS.Glb
{
    partial class PSPartPaisEdit
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
            this.psTextoBox4 = new PS.Lib.WinForms.PSTextoBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(647, 258);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psTextoBox4);
            this.tabPage1.Controls.Add(this.psTextoBox3);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(639, 232);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODPAIS";
            this.psTextoBox1.DataField = "CODPAIS";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 7);
            this.psTextoBox1.MaxLength = 3;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 0;
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "CODREDUZIDO";
            this.psTextoBox2.DataField = "CODREDUZIDO";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.Location = new System.Drawing.Point(162, 7);
            this.psTextoBox2.MaxLength = 5;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox2.TabIndex = 1;
            // 
            // psTextoBox3
            // 
            this.psTextoBox3.Caption = "NOME";
            this.psTextoBox3.DataField = "NOME";
            this.psTextoBox3.Edita = true;
            this.psTextoBox3.Location = new System.Drawing.Point(11, 50);
            this.psTextoBox3.MaxLength = 255;
            this.psTextoBox3.Name = "psTextoBox3";
            this.psTextoBox3.PasswordChar = '\0';
            this.psTextoBox3.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox3.TabIndex = 2;
            // 
            // psTextoBox4
            // 
            this.psTextoBox4.Caption = "CODBACEN";
            this.psTextoBox4.DataField = "CODBACEN";
            this.psTextoBox4.Edita = true;
            this.psTextoBox4.Location = new System.Drawing.Point(313, 7);
            this.psTextoBox4.MaxLength = 5;
            this.psTextoBox4.Name = "psTextoBox4";
            this.psTextoBox4.PasswordChar = '\0';
            this.psTextoBox4.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox4.TabIndex = 3;
            // 
            // PSPartPaisEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartPaisEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartPaisEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox3;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private Lib.WinForms.PSTextoBox psTextoBox4;
    }
}