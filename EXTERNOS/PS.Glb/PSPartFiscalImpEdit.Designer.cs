namespace PS.Glb
{
    partial class PSPartFiscalImpEdit
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
            this.psCheckBox1 = new PS.Lib.WinForms.PSCheckBox();
            this.psTextoBox3 = new PS.Lib.WinForms.PSTextoBox();
            this.psTextoBox4 = new PS.Lib.WinForms.PSTextoBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psTextoBox4);
            this.tabPage1.Controls.Add(this.psTextoBox3);
            this.tabPage1.Controls.Add(this.psCheckBox1);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODIMPRESSORA";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.DataField = "CODIMPRESSORA";
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 15;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 0;
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "DESCRICAO";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.DataField = "DESCRICAO";
            this.psTextoBox2.Location = new System.Drawing.Point(11, 49);
            this.psTextoBox2.MaxLength = 80;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox2.TabIndex = 2;
            // 
            // psCheckBox1
            // 
            this.psCheckBox1.Caption = "ATIVO";
            this.psCheckBox1.Chave = true;
            this.psCheckBox1.Checked = false;
            this.psCheckBox1.DataField = "ATIVO";
            this.psCheckBox1.Location = new System.Drawing.Point(262, 21);
            this.psCheckBox1.Name = "psCheckBox1";
            this.psCheckBox1.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox1.TabIndex = 1;
            // 
            // psTextoBox3
            // 
            this.psTextoBox3.Caption = "MARCA";
            this.psTextoBox3.Edita = true;
            this.psTextoBox3.DataField = "MARCA";
            this.psTextoBox3.Location = new System.Drawing.Point(11, 92);
            this.psTextoBox3.MaxLength = 35;
            this.psTextoBox3.Name = "psTextoBox3";
            this.psTextoBox3.PasswordChar = '\0';
            this.psTextoBox3.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox3.TabIndex = 3;
            // 
            // psTextoBox4
            // 
            this.psTextoBox4.Caption = "MODELO";
            this.psTextoBox4.Edita = true;
            this.psTextoBox4.DataField = "MODELO";
            this.psTextoBox4.Location = new System.Drawing.Point(267, 92);
            this.psTextoBox4.MaxLength = 35;
            this.psTextoBox4.Name = "psTextoBox4";
            this.psTextoBox4.PasswordChar = '\0';
            this.psTextoBox4.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox4.TabIndex = 4;
            // 
            // PSPartFiscalImpEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartFiscalImpEdit";
            this.PermiteEditar = true;
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartFiscalImpEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSCheckBox psCheckBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private PS.Lib.WinForms.PSTextoBox psTextoBox4;
        private PS.Lib.WinForms.PSTextoBox psTextoBox3;
    }
}