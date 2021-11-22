namespace PS.Glb
{
    partial class PSPartObjetoEdit
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
            this.psLookup1 = new PS.Lib.WinForms.PSLookup();
            this.psLookup3 = new PS.Lib.WinForms.PSLookup();
            this.psLookup4 = new PS.Lib.WinForms.PSLookup();
            this.psComboBox1 = new PS.Lib.WinForms.PSComboBox();
            this.psTextoBox2 = new PS.Lib.WinForms.PSTextoBox();
            this.psTextoBox3 = new PS.Lib.WinForms.PSTextoBox();
            this.psLookup5 = new PS.Lib.WinForms.PSLookup();
            this.psTextoBox4 = new PS.Lib.WinForms.PSTextoBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(647, 362);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psTextoBox4);
            this.tabPage1.Controls.Add(this.psLookup5);
            this.tabPage1.Controls.Add(this.psTextoBox3);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psComboBox1);
            this.tabPage1.Controls.Add(this.psLookup4);
            this.tabPage1.Controls.Add(this.psLookup3);
            this.tabPage1.Controls.Add(this.psLookup1);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(639, 336);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODOBJETO";
            this.psTextoBox1.DataField = "CODOBJETO";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 30;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 0;
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODTIPOBJETO";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODTIPOBJETO";
            this.psLookup1.Description = "";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.KeyField = "CODTIPOBJETO";
            this.psLookup1.Location = new System.Drawing.Point(11, 49);
            this.psLookup1.LookupField = "CODTIPOBJETO;DESCRICAO";
            this.psLookup1.LookupFieldResult = "CODTIPOBJETO;DESCRICAO";
            this.psLookup1.MaxLength = 32767;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(401, 38);
            this.psLookup1.TabIndex = 1;
            this.psLookup1.ValorRetorno = null;
            // 
            // psLookup3
            // 
            this.psLookup3.Caption = "CODMODELO";
            this.psLookup3.Chave = true;
            this.psLookup3.DataField = "CODMODELO";
            this.psLookup3.Description = "";
            this.psLookup3.DinamicTable = null;
            this.psLookup3.KeyField = "CODMODELO";
            this.psLookup3.Location = new System.Drawing.Point(11, 93);
            this.psLookup3.LookupField = "CODMODELO;DESCRICAO";
            this.psLookup3.LookupFieldResult = "CODMODELO;DESCRICAO";
            this.psLookup3.MaxLength = 32767;
            this.psLookup3.Name = "psLookup3";
            this.psLookup3.PSPart = null;
            this.psLookup3.Size = new System.Drawing.Size(401, 38);
            this.psLookup3.TabIndex = 2;
            this.psLookup3.ValorRetorno = null;
            // 
            // psLookup4
            // 
            this.psLookup4.Caption = "CODSUBMODELO";
            this.psLookup4.Chave = true;
            this.psLookup4.DataField = "CODSUBMODELO";
            this.psLookup4.Description = "";
            this.psLookup4.DinamicTable = null;
            this.psLookup4.KeyField = "CODSUBMODELO";
            this.psLookup4.Location = new System.Drawing.Point(11, 137);
            this.psLookup4.LookupField = "CODSUBMODELO;DESCRICAO";
            this.psLookup4.LookupFieldResult = "CODSUBMODELO;DESCRICAO";
            this.psLookup4.MaxLength = 32767;
            this.psLookup4.Name = "psLookup4";
            this.psLookup4.PSPart = null;
            this.psLookup4.Size = new System.Drawing.Size(401, 38);
            this.psLookup4.TabIndex = 3;
            this.psLookup4.ValorRetorno = null;
            // 
            // psComboBox1
            // 
            this.psComboBox1.Caption = "CODSITUACAO";
            this.psComboBox1.Chave = true;
            this.psComboBox1.DataField = "CODSITUACAO";
            this.psComboBox1.DataSource = null;
            this.psComboBox1.DisplayMember = "";
            this.psComboBox1.Location = new System.Drawing.Point(11, 181);
            this.psComboBox1.Name = "psComboBox1";
            this.psComboBox1.SelectedIndex = -1;
            this.psComboBox1.Size = new System.Drawing.Size(139, 37);
            this.psComboBox1.TabIndex = 4;
            this.psComboBox1.Value = null;
            this.psComboBox1.ValueMember = "";
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "ANOFABRICACAO";
            this.psTextoBox2.DataField = "ANOFABRICACAO";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.Location = new System.Drawing.Point(156, 181);
            this.psTextoBox2.MaxLength = 32767;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(130, 37);
            this.psTextoBox2.TabIndex = 5;
            // 
            // psTextoBox3
            // 
            this.psTextoBox3.Caption = "ANOMODELO";
            this.psTextoBox3.DataField = "ANOMODELO";
            this.psTextoBox3.Edita = true;
            this.psTextoBox3.Location = new System.Drawing.Point(292, 181);
            this.psTextoBox3.MaxLength = 32767;
            this.psTextoBox3.Name = "psTextoBox3";
            this.psTextoBox3.PasswordChar = '\0';
            this.psTextoBox3.Size = new System.Drawing.Size(122, 37);
            this.psTextoBox3.TabIndex = 6;
            // 
            // psLookup5
            // 
            this.psLookup5.Caption = "CODCLIFOR";
            this.psLookup5.Chave = true;
            this.psLookup5.DataField = "CODCLIFOR";
            this.psLookup5.Description = "";
            this.psLookup5.DinamicTable = null;
            this.psLookup5.KeyField = "CODCLIFOR";
            this.psLookup5.Location = new System.Drawing.Point(11, 224);
            this.psLookup5.LookupField = "CODCLIFOR;NOME;NOMEFANTASIA";
            this.psLookup5.LookupFieldResult = "CODCLIFOR;NOME";
            this.psLookup5.MaxLength = 10;
            this.psLookup5.Name = "psLookup5";
            this.psLookup5.PSPart = null;
            this.psLookup5.Size = new System.Drawing.Size(401, 38);
            this.psLookup5.TabIndex = 7;
            this.psLookup5.ValorRetorno = null;
            // 
            // psTextoBox4
            // 
            this.psTextoBox4.Caption = "DESCRICAO";
            this.psTextoBox4.DataField = "DESCRICAO";
            this.psTextoBox4.Edita = true;
            this.psTextoBox4.Location = new System.Drawing.Point(11, 268);
            this.psTextoBox4.MaxLength = 80;
            this.psTextoBox4.Name = "psTextoBox4";
            this.psTextoBox4.PasswordChar = '\0';
            this.psTextoBox4.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox4.TabIndex = 8;
            // 
            // PSPartObjetoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 478);
            this.Name = "PSPartObjetoEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartObjetoEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSLookup psLookup5;
        private PS.Lib.WinForms.PSTextoBox psTextoBox3;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private PS.Lib.WinForms.PSComboBox psComboBox1;
        private PS.Lib.WinForms.PSLookup psLookup4;
        private PS.Lib.WinForms.PSLookup psLookup3;
        private PS.Lib.WinForms.PSLookup psLookup1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox4;
    }
}