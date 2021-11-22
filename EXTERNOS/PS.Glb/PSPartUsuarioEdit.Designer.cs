namespace PS.Glb
{
    partial class PSPartUsuarioEdit
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
            this.psDateBox1 = new PS.Lib.WinForms.PSDateBox();
            this.psTextoBox3 = new PS.Lib.WinForms.PSTextoBox();
            this.psCheckBox2 = new PS.Lib.WinForms.PSCheckBox();
            this.psDateBox2 = new PS.Lib.WinForms.PSDateBox();
            this.psTextoBox4 = new PS.Lib.WinForms.PSTextoBox();
            this.psCheckBox3 = new PS.Lib.WinForms.PSCheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(647, 329);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psCheckBox3);
            this.tabPage1.Controls.Add(this.psTextoBox4);
            this.tabPage1.Controls.Add(this.psDateBox2);
            this.tabPage1.Controls.Add(this.psCheckBox2);
            this.tabPage1.Controls.Add(this.psTextoBox3);
            this.tabPage1.Controls.Add(this.psDateBox1);
            this.tabPage1.Controls.Add(this.psCheckBox1);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(639, 303);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODUSUARIO";
            this.psTextoBox1.DataField = "CODUSUARIO";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(8, 6);
            this.psTextoBox1.MaxLength = 20;
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
            this.psTextoBox2.Location = new System.Drawing.Point(8, 49);
            this.psTextoBox2.MaxLength = 255;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox2.TabIndex = 1;
            // 
            // psCheckBox1
            // 
            this.psCheckBox1.Caption = "ATIVO";
            this.psCheckBox1.Chave = true;
            this.psCheckBox1.Checked = false;
            this.psCheckBox1.DataField = "ATIVO";
            this.psCheckBox1.Location = new System.Drawing.Point(312, 21);
            this.psCheckBox1.Name = "psCheckBox1";
            this.psCheckBox1.Size = new System.Drawing.Size(100, 22);
            this.psCheckBox1.TabIndex = 3;
            // 
            // psDateBox1
            // 
            this.psDateBox1.Caption = "DTEXPIRACAO";
            this.psDateBox1.Chave = true;
            this.psDateBox1.DataField = "DTEXPIRACAO";
            this.psDateBox1.Location = new System.Drawing.Point(8, 172);
            this.psDateBox1.Mascara = "00/00/0000";
            this.psDateBox1.MaxLength = 32767;
            this.psDateBox1.Name = "psDateBox1";
            this.psDateBox1.Size = new System.Drawing.Size(145, 37);
            this.psDateBox1.TabIndex = 6;
            this.psDateBox1.Value = new System.DateTime(2015, 2, 5, 0, 0, 0, 0);
            // 
            // psTextoBox3
            // 
            this.psTextoBox3.Caption = "SENHA";
            this.psTextoBox3.DataField = "SENHA";
            this.psTextoBox3.Edita = true;
            this.psTextoBox3.Location = new System.Drawing.Point(8, 92);
            this.psTextoBox3.MaxLength = 32767;
            this.psTextoBox3.Name = "psTextoBox3";
            this.psTextoBox3.PasswordChar = '*';
            this.psTextoBox3.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox3.TabIndex = 4;
            // 
            // psCheckBox2
            // 
            this.psCheckBox2.Caption = "NUNCAEXPIRA";
            this.psCheckBox2.Chave = true;
            this.psCheckBox2.Checked = false;
            this.psCheckBox2.DataField = "NUNCAEXPIRA";
            this.psCheckBox2.Location = new System.Drawing.Point(8, 144);
            this.psCheckBox2.Name = "psCheckBox2";
            this.psCheckBox2.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox2.TabIndex = 5;
            this.psCheckBox2.CheckedChanged += new PS.Lib.WinForms.PSCheckBox.CheckedChangedDelegate(this.psCheckBox2_CheckedChanged);
            // 
            // psDateBox2
            // 
            this.psDateBox2.Caption = "ULTIMOLOGIN";
            this.psDateBox2.Chave = true;
            this.psDateBox2.DataField = "ULTIMOLOGIN";
            this.psDateBox2.Enabled = false;
            this.psDateBox2.Location = new System.Drawing.Point(159, 172);
            this.psDateBox2.Mascara = "00/00/0000 00:00";
            this.psDateBox2.MaxLength = 32767;
            this.psDateBox2.Name = "psDateBox2";
            this.psDateBox2.Size = new System.Drawing.Size(145, 37);
            this.psDateBox2.TabIndex = 7;
            this.psDateBox2.Value = new System.DateTime(2015, 2, 5, 0, 0, 0, 0);
            // 
            // psTextoBox4
            // 
            this.psTextoBox4.Caption = "EMAIL";
            this.psTextoBox4.DataField = "EMAIL";
            this.psTextoBox4.Edita = true;
            this.psTextoBox4.Location = new System.Drawing.Point(8, 215);
            this.psTextoBox4.MaxLength = 50;
            this.psTextoBox4.Name = "psTextoBox4";
            this.psTextoBox4.PasswordChar = '\0';
            this.psTextoBox4.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox4.TabIndex = 8;
            // 
            // psCheckBox3
            // 
            this.psCheckBox3.Caption = "SUPERVISOR";
            this.psCheckBox3.Chave = true;
            this.psCheckBox3.Checked = false;
            this.psCheckBox3.DataField = "SUPERVISOR";
            this.psCheckBox3.Location = new System.Drawing.Point(432, 21);
            this.psCheckBox3.Name = "psCheckBox3";
            this.psCheckBox3.Size = new System.Drawing.Size(100, 22);
            this.psCheckBox3.TabIndex = 9;
            // 
            // PSPartUsuarioEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 445);
            this.Name = "PSPartUsuarioEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartUsuarioEdit";
            this.Load += new System.EventHandler(this.PSPartUsuarioEdit_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSCheckBox psCheckBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private PS.Lib.WinForms.PSDateBox psDateBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox3;
        private Lib.WinForms.PSCheckBox psCheckBox2;
        private Lib.WinForms.PSDateBox psDateBox2;
        private Lib.WinForms.PSTextoBox psTextoBox4;
        private Lib.WinForms.PSCheckBox psCheckBox3;
    }
}