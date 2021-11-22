namespace PS.Glb
{
    partial class PSPartContatoCliForEdit
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
            this.psDateBox1 = new PS.Lib.WinForms.PSDateBox();
            this.psTextoBox15 = new PS.Lib.WinForms.PSTextoBox();
            this.psTextoBox14 = new PS.Lib.WinForms.PSTextoBox();
            this.psMaskedTextBox1 = new PS.Lib.WinForms.PSMaskedTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(588, 259);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psMaskedTextBox1);
            this.tabPage1.Controls.Add(this.psTextoBox15);
            this.tabPage1.Controls.Add(this.psTextoBox14);
            this.tabPage1.Controls.Add(this.psDateBox1);
            this.tabPage1.Controls.Add(this.psTextoBox4);
            this.tabPage1.Controls.Add(this.psTextoBox3);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(580, 233);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 321);
            this.panel2.Size = new System.Drawing.Size(588, 54);
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(588, 30);
            // 
            // buttonSALVAR
            // 
            this.buttonSALVAR.Location = new System.Drawing.Point(330, 19);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(411, 19);
            // 
            // buttonCANCELAR
            // 
            this.buttonCANCELAR.Location = new System.Drawing.Point(492, 19);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.AutoIncremento = PS.Lib.Global.TypeAutoinc.Max;
            this.psTextoBox1.Caption = "CODCONTATO";
            this.psTextoBox1.DataField = "CODCONTATO";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 32767;
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
            this.psTextoBox2.Location = new System.Drawing.Point(11, 49);
            this.psTextoBox2.MaxLength = 80;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox2.TabIndex = 1;
            // 
            // psTextoBox3
            // 
            this.psTextoBox3.Caption = "EMAIL";
            this.psTextoBox3.DataField = "EMAIL";
            this.psTextoBox3.Edita = true;
            this.psTextoBox3.Location = new System.Drawing.Point(11, 92);
            this.psTextoBox3.MaxLength = 35;
            this.psTextoBox3.Name = "psTextoBox3";
            this.psTextoBox3.PasswordChar = '\0';
            this.psTextoBox3.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox3.TabIndex = 2;
            // 
            // psTextoBox4
            // 
            this.psTextoBox4.Caption = "TELEFONE";
            this.psTextoBox4.DataField = "TELEFONE";
            this.psTextoBox4.Edita = true;
            this.psTextoBox4.Location = new System.Drawing.Point(418, 92);
            this.psTextoBox4.MaxLength = 15;
            this.psTextoBox4.Name = "psTextoBox4";
            this.psTextoBox4.PasswordChar = '\0';
            this.psTextoBox4.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox4.TabIndex = 3;
            // 
            // psDateBox1
            // 
            this.psDateBox1.Caption = "DATANASCIMENTO";
            this.psDateBox1.Chave = true;
            this.psDateBox1.DataField = "DATANASCIMENTO";
            this.psDateBox1.Location = new System.Drawing.Point(162, 135);
            this.psDateBox1.Mascara = "00/00/0000";
            this.psDateBox1.MaxLength = 32767;
            this.psDateBox1.Name = "psDateBox1";
            this.psDateBox1.Size = new System.Drawing.Size(146, 37);
            this.psDateBox1.TabIndex = 5;
            this.psDateBox1.Value = new System.DateTime(2016, 2, 20, 16, 5, 32, 801);
            // 
            // psTextoBox15
            // 
            this.psTextoBox15.Caption = "OREMISSOR";
            this.psTextoBox15.DataField = "OREMISSOR";
            this.psTextoBox15.Edita = true;
            this.psTextoBox15.Location = new System.Drawing.Point(162, 178);
            this.psTextoBox15.MaxLength = 15;
            this.psTextoBox15.Name = "psTextoBox15";
            this.psTextoBox15.PasswordChar = '\0';
            this.psTextoBox15.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox15.TabIndex = 7;
            // 
            // psTextoBox14
            // 
            this.psTextoBox14.Caption = "NUMERORG";
            this.psTextoBox14.DataField = "NUMERORG";
            this.psTextoBox14.Edita = true;
            this.psTextoBox14.Location = new System.Drawing.Point(11, 178);
            this.psTextoBox14.MaxLength = 25;
            this.psTextoBox14.Name = "psTextoBox14";
            this.psTextoBox14.PasswordChar = '\0';
            this.psTextoBox14.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox14.TabIndex = 6;
            // 
            // psMaskedTextBox1
            // 
            this.psMaskedTextBox1.Caption = "CPF";
            this.psMaskedTextBox1.Chave = true;
            this.psMaskedTextBox1.DataField = "CPF";
            this.psMaskedTextBox1.Location = new System.Drawing.Point(11, 135);
            this.psMaskedTextBox1.Mask = "000,000,000-00";
            this.psMaskedTextBox1.MaxLength = 32767;
            this.psMaskedTextBox1.Name = "psMaskedTextBox1";
            this.psMaskedTextBox1.PasswordChar = '\0';
            this.psMaskedTextBox1.Size = new System.Drawing.Size(145, 37);
            this.psMaskedTextBox1.TabIndex = 4;
            // 
            // PSPartContatoCliForEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 375);
            this.Name = "PSPartContatoCliForEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartContatoCliForEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox4;
        private PS.Lib.WinForms.PSTextoBox psTextoBox3;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private PS.Lib.WinForms.PSDateBox psDateBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox15;
        private PS.Lib.WinForms.PSTextoBox psTextoBox14;
        private PS.Lib.WinForms.PSMaskedTextBox psMaskedTextBox1;
    }
}