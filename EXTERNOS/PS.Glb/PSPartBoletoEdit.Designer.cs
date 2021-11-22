namespace PS.Glb
{
    partial class PSPartBoletoEdit
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
            this.psLookup1 = new PS.Lib.WinForms.PSLookup();
            this.psLookup2 = new PS.Lib.WinForms.PSLookup();
            this.psLookup3 = new PS.Lib.WinForms.PSLookup();
            this.psTextoBox4 = new PS.Lib.WinForms.PSTextoBox();
            this.psDateBox1 = new PS.Lib.WinForms.PSDateBox();
            this.psDateBox2 = new PS.Lib.WinForms.PSDateBox();
            this.psTextoBox5 = new PS.Lib.WinForms.PSTextoBox();
            this.psTextoBox6 = new PS.Lib.WinForms.PSTextoBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.psLookup4 = new PS.Lib.WinForms.PSLookup();
            this.psComboBox1 = new PS.Lib.WinForms.PSComboBox();
            this.psTextoBox7 = new PS.Lib.WinForms.PSTextoBox();
            this.psTextoBox8 = new PS.Lib.WinForms.PSTextoBox();
            this.psTextoBox9 = new PS.Lib.WinForms.PSTextoBox();
            this.psLookup5 = new PS.Lib.WinForms.PSLookup();
            this.psTextoBox10 = new PS.Lib.WinForms.PSTextoBox();
            this.psDateBox3 = new PS.Lib.WinForms.PSDateBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Size = new System.Drawing.Size(767, 456);
            this.tabControl1.Controls.SetChildIndex(this.tabPage2, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPage1, 0);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psTextoBox6);
            this.tabPage1.Controls.Add(this.psTextoBox5);
            this.tabPage1.Controls.Add(this.psDateBox2);
            this.tabPage1.Controls.Add(this.psDateBox1);
            this.tabPage1.Controls.Add(this.psTextoBox4);
            this.tabPage1.Controls.Add(this.psLookup3);
            this.tabPage1.Controls.Add(this.psLookup2);
            this.tabPage1.Controls.Add(this.psLookup1);
            this.tabPage1.Controls.Add(this.psTextoBox3);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(759, 430);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODLANCA";
            this.psTextoBox1.DataField = "CODLANCA";
            this.psTextoBox1.Location = new System.Drawing.Point(11, 24);
            this.psTextoBox1.MaxLength = 32767;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 0;
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "CODEMPRESA";
            this.psTextoBox2.DataField = "CODEMPRESA";
            this.psTextoBox2.Location = new System.Drawing.Point(162, 24);
            this.psTextoBox2.MaxLength = 32767;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox2.TabIndex = 1;
            // 
            // psTextoBox3
            // 
            this.psTextoBox3.Caption = "CODFILIAL";
            this.psTextoBox3.DataField = "CODFILIAL";
            this.psTextoBox3.Edita = true;
            this.psTextoBox3.Location = new System.Drawing.Point(313, 24);
            this.psTextoBox3.MaxLength = 32767;
            this.psTextoBox3.Name = "psTextoBox3";
            this.psTextoBox3.PasswordChar = '\0';
            this.psTextoBox3.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox3.TabIndex = 2;
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODCLIFOR";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODCLIFOR";
            this.psLookup1.Description = "";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.KeyField = "CODCLIFOR";
            this.psLookup1.Location = new System.Drawing.Point(11, 78);
            this.psLookup1.LookupField = "CODCLIFOR;NOME;NOMEFANTASIA";
            this.psLookup1.LookupFieldResult = "CODCLIFOR;NOME";
            this.psLookup1.MaxLength = 32767;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(447, 38);
            this.psLookup1.TabIndex = 3;
            this.psLookup1.ValorRetorno = null;
            // 
            // psLookup2
            // 
            this.psLookup2.Caption = "CODCONTA";
            this.psLookup2.Chave = true;
            this.psLookup2.DataField = "CODCONTA";
            this.psLookup2.Description = "";
            this.psLookup2.DinamicTable = null;
            this.psLookup2.KeyField = "CODCONTA";
            this.psLookup2.Location = new System.Drawing.Point(11, 122);
            this.psLookup2.LookupField = "CODCONTA;DESCRICAO";
            this.psLookup2.LookupFieldResult = "CODCONTA;DESCRICAO";
            this.psLookup2.MaxLength = 32767;
            this.psLookup2.Name = "psLookup2";
            this.psLookup2.PSPart = null;
            this.psLookup2.Size = new System.Drawing.Size(447, 38);
            this.psLookup2.TabIndex = 4;
            this.psLookup2.ValorRetorno = null;
            // 
            // psLookup3
            // 
            this.psLookup3.Caption = "CODTIPDOC";
            this.psLookup3.Chave = true;
            this.psLookup3.DataField = "CODTIPDOC";
            this.psLookup3.Description = "";
            this.psLookup3.DinamicTable = null;
            this.psLookup3.KeyField = "CODTIPDOC";
            this.psLookup3.Location = new System.Drawing.Point(11, 166);
            this.psLookup3.LookupField = "CODTIPDOC;NOME";
            this.psLookup3.LookupFieldResult = "CODTIPDOC;NOME";
            this.psLookup3.MaxLength = 32767;
            this.psLookup3.Name = "psLookup3";
            this.psLookup3.PSPart = null;
            this.psLookup3.Size = new System.Drawing.Size(447, 38);
            this.psLookup3.TabIndex = 5;
            this.psLookup3.ValorRetorno = null;
            // 
            // psTextoBox4
            // 
            this.psTextoBox4.Caption = "NUMERO";
            this.psTextoBox4.DataField = "NUMERO";
            this.psTextoBox4.Edita = true;
            this.psTextoBox4.Location = new System.Drawing.Point(11, 210);
            this.psTextoBox4.MaxLength = 32767;
            this.psTextoBox4.Name = "psTextoBox4";
            this.psTextoBox4.PasswordChar = '\0';
            this.psTextoBox4.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox4.TabIndex = 6;
            // 
            // psDateBox1
            // 
            this.psDateBox1.Caption = "DATAEMISSAO";
            this.psDateBox1.Chave = true;
            this.psDateBox1.DataField = "DATAEMISSAO";
            this.psDateBox1.Location = new System.Drawing.Point(162, 210);
            this.psDateBox1.Mascara = "00/00/0000";
            this.psDateBox1.MaxLength = 32767;
            this.psDateBox1.Name = "psDateBox1";
            this.psDateBox1.Size = new System.Drawing.Size(146, 37);
            this.psDateBox1.TabIndex = 7;
            this.psDateBox1.Value = new System.DateTime(2016, 2, 11, 11, 59, 19, 724);
            // 
            // psDateBox2
            // 
            this.psDateBox2.Caption = "DATAVENCIMENTO";
            this.psDateBox2.Chave = true;
            this.psDateBox2.DataField = "DATAVENCIMENTO";
            this.psDateBox2.Location = new System.Drawing.Point(314, 210);
            this.psDateBox2.Mascara = "00/00/0000";
            this.psDateBox2.MaxLength = 32767;
            this.psDateBox2.Name = "psDateBox2";
            this.psDateBox2.Size = new System.Drawing.Size(146, 37);
            this.psDateBox2.TabIndex = 8;
            this.psDateBox2.Value = new System.DateTime(2016, 2, 11, 11, 59, 23, 866);
            // 
            // psTextoBox5
            // 
            this.psTextoBox5.Caption = "CODMOEDA";
            this.psTextoBox5.DataField = "CODMOEDA";
            this.psTextoBox5.Edita = true;
            this.psTextoBox5.Location = new System.Drawing.Point(11, 253);
            this.psTextoBox5.MaxLength = 32767;
            this.psTextoBox5.Name = "psTextoBox5";
            this.psTextoBox5.PasswordChar = '\0';
            this.psTextoBox5.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox5.TabIndex = 9;
            // 
            // psTextoBox6
            // 
            this.psTextoBox6.Caption = "VALOR";
            this.psTextoBox6.DataField = "VALOR";
            this.psTextoBox6.Edita = true;
            this.psTextoBox6.Location = new System.Drawing.Point(163, 253);
            this.psTextoBox6.MaxLength = 32767;
            this.psTextoBox6.Name = "psTextoBox6";
            this.psTextoBox6.PasswordChar = '\0';
            this.psTextoBox6.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox6.TabIndex = 10;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.psDateBox3);
            this.tabPage2.Controls.Add(this.psTextoBox10);
            this.tabPage2.Controls.Add(this.psLookup5);
            this.tabPage2.Controls.Add(this.psTextoBox9);
            this.tabPage2.Controls.Add(this.psTextoBox8);
            this.tabPage2.Controls.Add(this.psTextoBox7);
            this.tabPage2.Controls.Add(this.psComboBox1);
            this.tabPage2.Controls.Add(this.psLookup4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(759, 430);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Parâmetros";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // psLookup4
            // 
            this.psLookup4.Caption = "CODCONVENIO";
            this.psLookup4.Chave = true;
            this.psLookup4.DataField = "CODCONVENIO";
            this.psLookup4.Description = "";
            this.psLookup4.DinamicTable = null;
            this.psLookup4.KeyField = "CODCONVENIO";
            this.psLookup4.Location = new System.Drawing.Point(11, 19);
            this.psLookup4.LookupField = "CODCONVENIO;DESCRICAO";
            this.psLookup4.LookupFieldResult = "CODCONVENIO;DESCRICAO";
            this.psLookup4.MaxLength = 32767;
            this.psLookup4.Name = "psLookup4";
            this.psLookup4.PSPart = null;
            this.psLookup4.Size = new System.Drawing.Size(401, 38);
            this.psLookup4.TabIndex = 0;
            this.psLookup4.ValorRetorno = null;
            // 
            // psComboBox1
            // 
            this.psComboBox1.Caption = "ACEITE";
            this.psComboBox1.Chave = true;
            this.psComboBox1.DataField = "ACEITE";
            this.psComboBox1.DataSource = null;
            this.psComboBox1.DisplayMember = "";
            this.psComboBox1.Location = new System.Drawing.Point(11, 63);
            this.psComboBox1.Name = "psComboBox1";
            this.psComboBox1.SelectedIndex = -1;
            this.psComboBox1.Size = new System.Drawing.Size(139, 37);
            this.psComboBox1.TabIndex = 1;
            this.psComboBox1.Value = null;
            this.psComboBox1.ValueMember = "";
            // 
            // psTextoBox7
            // 
            this.psTextoBox7.Caption = "NOSSONUMERO";
            this.psTextoBox7.DataField = "NOSSONUMERO";
            this.psTextoBox7.Edita = true;
            this.psTextoBox7.Location = new System.Drawing.Point(11, 106);
            this.psTextoBox7.MaxLength = 32767;
            this.psTextoBox7.Name = "psTextoBox7";
            this.psTextoBox7.PasswordChar = '\0';
            this.psTextoBox7.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox7.TabIndex = 2;
            // 
            // psTextoBox8
            // 
            this.psTextoBox8.Caption = "CODIGOBARRAS";
            this.psTextoBox8.DataField = "CODIGOBARRAS";
            this.psTextoBox8.Edita = true;
            this.psTextoBox8.Location = new System.Drawing.Point(11, 149);
            this.psTextoBox8.MaxLength = 32767;
            this.psTextoBox8.Name = "psTextoBox8";
            this.psTextoBox8.PasswordChar = '\0';
            this.psTextoBox8.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox8.TabIndex = 3;
            // 
            // psTextoBox9
            // 
            this.psTextoBox9.Caption = "IPTE";
            this.psTextoBox9.DataField = "IPTE";
            this.psTextoBox9.Edita = true;
            this.psTextoBox9.Location = new System.Drawing.Point(11, 192);
            this.psTextoBox9.MaxLength = 32767;
            this.psTextoBox9.Name = "psTextoBox9";
            this.psTextoBox9.PasswordChar = '\0';
            this.psTextoBox9.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox9.TabIndex = 4;
            // 
            // psLookup5
            // 
            this.psLookup5.Caption = "IDBOLETOSTATUS";
            this.psLookup5.Chave = true;
            this.psLookup5.DataField = "IDBOLETOSTATUS";
            this.psLookup5.Description = "";
            this.psLookup5.DinamicTable = null;
            this.psLookup5.KeyField = "IDBOLETOSTATUS";
            this.psLookup5.Location = new System.Drawing.Point(11, 235);
            this.psLookup5.LookupField = "IDBOLETOSTATUS;DESCRICAO";
            this.psLookup5.LookupFieldResult = "IDBOLETOSTATUS;DESCRICAO";
            this.psLookup5.MaxLength = 32767;
            this.psLookup5.Name = "psLookup5";
            this.psLookup5.PSPart = null;
            this.psLookup5.Size = new System.Drawing.Size(401, 38);
            this.psLookup5.TabIndex = 5;
            this.psLookup5.ValorRetorno = null;
            // 
            // psTextoBox10
            // 
            this.psTextoBox10.Caption = "CODREMESSA";
            this.psTextoBox10.DataField = "CODREMESSA";
            this.psTextoBox10.Edita = true;
            this.psTextoBox10.Location = new System.Drawing.Point(11, 279);
            this.psTextoBox10.MaxLength = 32767;
            this.psTextoBox10.Name = "psTextoBox10";
            this.psTextoBox10.PasswordChar = '\0';
            this.psTextoBox10.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox10.TabIndex = 6;
            // 
            // psDateBox3
            // 
            this.psDateBox3.Caption = "DATAREMESSA";
            this.psDateBox3.Chave = true;
            this.psDateBox3.DataField = "DATAREMESSA";
            this.psDateBox3.Location = new System.Drawing.Point(162, 279);
            this.psDateBox3.Mascara = "00/00/0000";
            this.psDateBox3.MaxLength = 32767;
            this.psDateBox3.Name = "psDateBox3";
            this.psDateBox3.Size = new System.Drawing.Size(146, 37);
            this.psDateBox3.TabIndex = 7;
            this.psDateBox3.Value = new System.DateTime(2016, 2, 11, 12, 1, 20, 402);
            // 
            // PSPartBoletoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 518);
            this.Name = "PSPartBoletoEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.PermiteSavar = true;
            this.Text = "PSPartBoletoEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Lib.WinForms.PSTextoBox psTextoBox1;
        private Lib.WinForms.PSTextoBox psTextoBox3;
        private Lib.WinForms.PSTextoBox psTextoBox2;
        private Lib.WinForms.PSLookup psLookup3;
        private Lib.WinForms.PSLookup psLookup2;
        private Lib.WinForms.PSLookup psLookup1;
        private Lib.WinForms.PSDateBox psDateBox2;
        private Lib.WinForms.PSDateBox psDateBox1;
        private Lib.WinForms.PSTextoBox psTextoBox4;
        private Lib.WinForms.PSTextoBox psTextoBox6;
        private Lib.WinForms.PSTextoBox psTextoBox5;
        private System.Windows.Forms.TabPage tabPage2;
        private Lib.WinForms.PSLookup psLookup4;
        private Lib.WinForms.PSComboBox psComboBox1;
        private Lib.WinForms.PSTextoBox psTextoBox9;
        private Lib.WinForms.PSTextoBox psTextoBox8;
        private Lib.WinForms.PSTextoBox psTextoBox7;
        private Lib.WinForms.PSLookup psLookup5;
        private Lib.WinForms.PSTextoBox psTextoBox10;
        private Lib.WinForms.PSDateBox psDateBox3;
    }
}