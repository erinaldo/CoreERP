namespace PS.Glb
{
    partial class PSPartLocalEstoqueEdit
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.psLookup4 = new PS.Lib.WinForms.PSLookup();
            this.psLookup5 = new PS.Lib.WinForms.PSLookup();
            this.psLookup1 = new PS.Lib.WinForms.PSLookup();
            this.psTextoBox9 = new PS.Lib.WinForms.PSTextoBox();
            this.psLookup2 = new PS.Lib.WinForms.PSLookup();
            this.psTextoBox8 = new PS.Lib.WinForms.PSTextoBox();
            this.psTextoBox7 = new PS.Lib.WinForms.PSTextoBox();
            this.psTextoBox6 = new PS.Lib.WinForms.PSTextoBox();
            this.psLookup6 = new PS.Lib.WinForms.PSLookup();
            this.psTextoBox5 = new PS.Lib.WinForms.PSTextoBox();
            this.psLookup3 = new PS.Lib.WinForms.PSLookup();
            this.psTextoBox1 = new PS.Lib.WinForms.PSTextoBox();
            this.psTextoBox2 = new PS.Lib.WinForms.PSTextoBox();
            this.psTextoBox3 = new PS.Lib.WinForms.PSTextoBox();
            this.psCheckBox1 = new PS.Lib.WinForms.PSCheckBox();
            this.psComboBoxCODTIPLOC = new PS.Lib.WinForms.PSComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Size = new System.Drawing.Size(647, 320);
            this.tabControl1.Controls.SetChildIndex(this.tabPage2, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPage1, 0);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psComboBoxCODTIPLOC);
            this.tabPage1.Controls.Add(this.psCheckBox1);
            this.tabPage1.Controls.Add(this.psTextoBox3);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Controls.Add(this.psLookup3);
            this.tabPage1.Size = new System.Drawing.Size(639, 294);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.psLookup4);
            this.tabPage2.Controls.Add(this.psLookup5);
            this.tabPage2.Controls.Add(this.psLookup1);
            this.tabPage2.Controls.Add(this.psTextoBox9);
            this.tabPage2.Controls.Add(this.psLookup2);
            this.tabPage2.Controls.Add(this.psTextoBox8);
            this.tabPage2.Controls.Add(this.psTextoBox7);
            this.tabPage2.Controls.Add(this.psTextoBox6);
            this.tabPage2.Controls.Add(this.psLookup6);
            this.tabPage2.Controls.Add(this.psTextoBox5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(506, 105);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Endereço";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // psLookup4
            // 
            this.psLookup4.Caption = "CODETD";
            this.psLookup4.Chave = true;
            this.psLookup4.DataField = "CODETD";
            this.psLookup4.Description = "";
            this.psLookup4.DinamicTable = null;
            this.psLookup4.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup4.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup4.KeyField = "CODETD";
            this.psLookup4.Location = new System.Drawing.Point(257, 179);
            this.psLookup4.LookupField = "CODETD;NOME";
            this.psLookup4.LookupFieldResult = "CODETD;NOME";
            this.psLookup4.MaxLength = 2;
            this.psLookup4.Name = "psLookup4";
            this.psLookup4.PSPart = null;
            this.psLookup4.Size = new System.Drawing.Size(303, 38);
            this.psLookup4.TabIndex = 8;
            this.psLookup4.ValorRetorno = null;
            // 
            // psLookup5
            // 
            this.psLookup5.Caption = "CODCIDADE";
            this.psLookup5.Chave = true;
            this.psLookup5.DataField = "CODCIDADE";
            this.psLookup5.Description = "";
            this.psLookup5.DinamicTable = null;
            this.psLookup5.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup5.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup5.KeyField = "CODCIDADE";
            this.psLookup5.Location = new System.Drawing.Point(11, 223);
            this.psLookup5.LookupField = "CODETD;CODCIDADE;NOME";
            this.psLookup5.LookupFieldResult = "CODCIDADE;NOME";
            this.psLookup5.MaxLength = 25;
            this.psLookup5.Name = "psLookup5";
            this.psLookup5.PSPart = null;
            this.psLookup5.Size = new System.Drawing.Size(401, 38);
            this.psLookup5.TabIndex = 9;
            this.psLookup5.ValorRetorno = null;
            this.psLookup5.BeforeLookup += new PS.Lib.WinForms.PSLookup.BeforeLookupDelegate(this.psLookup5_BeforeLookup);
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODPAIS";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODPAIS";
            this.psLookup1.Description = "";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup1.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup1.KeyField = "CODPAIS";
            this.psLookup1.Location = new System.Drawing.Point(11, 179);
            this.psLookup1.LookupField = "CODPAIS;NOME";
            this.psLookup1.LookupFieldResult = "CODPAIS;NOME";
            this.psLookup1.MaxLength = 32767;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(227, 38);
            this.psLookup1.TabIndex = 7;
            this.psLookup1.ValorRetorno = null;
            // 
            // psTextoBox9
            // 
            this.psTextoBox9.Caption = "BAIRRO";
            this.psTextoBox9.DataField = "BAIRRO";
            this.psTextoBox9.Edita = true;
            this.psTextoBox9.Location = new System.Drawing.Point(257, 136);
            this.psTextoBox9.MaxLength = 35;
            this.psTextoBox9.Name = "psTextoBox9";
            this.psTextoBox9.PasswordChar = '\0';
            this.psTextoBox9.Size = new System.Drawing.Size(303, 37);
            this.psTextoBox9.TabIndex = 6;
            // 
            // psLookup2
            // 
            this.psLookup2.Caption = "CODTIPOBAIRRO";
            this.psLookup2.Chave = true;
            this.psLookup2.DataField = "CODTIPOBAIRRO";
            this.psLookup2.Description = "";
            this.psLookup2.DinamicTable = null;
            this.psLookup2.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup2.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup2.KeyField = "CODTIPOBAIRRO";
            this.psLookup2.Location = new System.Drawing.Point(11, 135);
            this.psLookup2.LookupField = "CODTIPORUA;NOME";
            this.psLookup2.LookupFieldResult = "CODTIPOBAIRRO;NOME";
            this.psLookup2.MaxLength = 32767;
            this.psLookup2.Name = "psLookup2";
            this.psLookup2.PSPart = null;
            this.psLookup2.Size = new System.Drawing.Size(227, 38);
            this.psLookup2.TabIndex = 5;
            this.psLookup2.ValorRetorno = null;
            // 
            // psTextoBox8
            // 
            this.psTextoBox8.Caption = "COMPLEMENTO";
            this.psTextoBox8.DataField = "COMPLEMENTO";
            this.psTextoBox8.Edita = true;
            this.psTextoBox8.Location = new System.Drawing.Point(11, 92);
            this.psTextoBox8.MaxLength = 35;
            this.psTextoBox8.Name = "psTextoBox8";
            this.psTextoBox8.PasswordChar = '\0';
            this.psTextoBox8.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox8.TabIndex = 4;
            // 
            // psTextoBox7
            // 
            this.psTextoBox7.Caption = "NUMERO";
            this.psTextoBox7.DataField = "NUMERO";
            this.psTextoBox7.Edita = true;
            this.psTextoBox7.Location = new System.Drawing.Point(415, 49);
            this.psTextoBox7.MaxLength = 15;
            this.psTextoBox7.Name = "psTextoBox7";
            this.psTextoBox7.PasswordChar = '\0';
            this.psTextoBox7.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox7.TabIndex = 3;
            // 
            // psTextoBox6
            // 
            this.psTextoBox6.Caption = "RUA";
            this.psTextoBox6.DataField = "RUA";
            this.psTextoBox6.Edita = true;
            this.psTextoBox6.Location = new System.Drawing.Point(11, 49);
            this.psTextoBox6.MaxLength = 80;
            this.psTextoBox6.Name = "psTextoBox6";
            this.psTextoBox6.PasswordChar = '\0';
            this.psTextoBox6.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox6.TabIndex = 2;
            // 
            // psLookup6
            // 
            this.psLookup6.Caption = "CODTIPORUA";
            this.psLookup6.Chave = true;
            this.psLookup6.DataField = "CODTIPORUA";
            this.psLookup6.Description = "";
            this.psLookup6.DinamicTable = null;
            this.psLookup6.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup6.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup6.KeyField = "CODTIPORUA";
            this.psLookup6.Location = new System.Drawing.Point(162, 6);
            this.psLookup6.LookupField = "CODTIPORUA;NOME";
            this.psLookup6.LookupFieldResult = "CODTIPORUA;NOME";
            this.psLookup6.MaxLength = 32767;
            this.psLookup6.Name = "psLookup6";
            this.psLookup6.PSPart = null;
            this.psLookup6.Size = new System.Drawing.Size(250, 38);
            this.psLookup6.TabIndex = 1;
            this.psLookup6.ValorRetorno = null;
            // 
            // psTextoBox5
            // 
            this.psTextoBox5.Caption = "CEP";
            this.psTextoBox5.DataField = "CEP";
            this.psTextoBox5.Edita = true;
            this.psTextoBox5.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox5.MaxLength = 9;
            this.psTextoBox5.Name = "psTextoBox5";
            this.psTextoBox5.PasswordChar = '\0';
            this.psTextoBox5.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox5.TabIndex = 0;
            // 
            // psLookup3
            // 
            this.psLookup3.Caption = "CODFILIAL";
            this.psLookup3.Chave = true;
            this.psLookup3.DataField = "CODFILIAL";
            this.psLookup3.Description = "";
            this.psLookup3.DinamicTable = null;
            this.psLookup3.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup3.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup3.KeyField = "CODFILIAL";
            this.psLookup3.Location = new System.Drawing.Point(11, 6);
            this.psLookup3.LookupField = "CODFILIAL;NOME;NOMEFANTASIA";
            this.psLookup3.LookupFieldResult = "CODFILIAL;NOMEFANTASIA";
            this.psLookup3.MaxLength = 32767;
            this.psLookup3.Name = "psLookup3";
            this.psLookup3.PSPart = null;
            this.psLookup3.Size = new System.Drawing.Size(401, 38);
            this.psLookup3.TabIndex = 0;
            this.psLookup3.ValorRetorno = null;
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODLOCAL";
            this.psTextoBox1.DataField = "CODLOCAL";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 50);
            this.psTextoBox1.MaxLength = 25;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 1;
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "NOME";
            this.psTextoBox2.DataField = "NOME";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.Location = new System.Drawing.Point(11, 93);
            this.psTextoBox2.MaxLength = 50;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox2.TabIndex = 3;
            // 
            // psTextoBox3
            // 
            this.psTextoBox3.Caption = "DESCRIÇÃO";
            this.psTextoBox3.DataField = "DESCRIÇÃO";
            this.psTextoBox3.Edita = true;
            this.psTextoBox3.Location = new System.Drawing.Point(11, 136);
            this.psTextoBox3.MaxLength = 80;
            this.psTextoBox3.Name = "psTextoBox3";
            this.psTextoBox3.PasswordChar = '\0';
            this.psTextoBox3.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox3.TabIndex = 4;
            // 
            // psCheckBox1
            // 
            this.psCheckBox1.Caption = "ATIVO";
            this.psCheckBox1.Chave = true;
            this.psCheckBox1.Checked = false;
            this.psCheckBox1.DataField = "ATIVO";
            this.psCheckBox1.Location = new System.Drawing.Point(162, 65);
            this.psCheckBox1.Name = "psCheckBox1";
            this.psCheckBox1.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox1.TabIndex = 2;
            // 
            // psComboBoxCODTIPLOC
            // 
            this.psComboBoxCODTIPLOC.Caption = "CODTIPLOC";
            this.psComboBoxCODTIPLOC.Chave = true;
            this.psComboBoxCODTIPLOC.DataField = "CODTIPLOC";
            this.psComboBoxCODTIPLOC.DataSource = null;
            this.psComboBoxCODTIPLOC.DisplayMember = "";
            this.psComboBoxCODTIPLOC.Location = new System.Drawing.Point(11, 179);
            this.psComboBoxCODTIPLOC.Name = "psComboBoxCODTIPLOC";
            this.psComboBoxCODTIPLOC.SelectedIndex = -1;
            this.psComboBoxCODTIPLOC.Size = new System.Drawing.Size(145, 37);
            this.psComboBoxCODTIPLOC.TabIndex = 9;
            this.psComboBoxCODTIPLOC.Value = null;
            this.psComboBoxCODTIPLOC.ValueMember = "";
            // 
            // PSPartLocalEstoqueEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 436);
            this.Name = "PSPartLocalEstoqueEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartLocalEstoqueEdit";
            this.Load += new System.EventHandler(this.PSPartLocalEstoqueEdit_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage2;
        private PS.Lib.WinForms.PSLookup psLookup3;
        private PS.Lib.WinForms.PSTextoBox psTextoBox3;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSLookup psLookup5;
        private PS.Lib.WinForms.PSLookup psLookup4;
        private PS.Lib.WinForms.PSLookup psLookup1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox9;
        private PS.Lib.WinForms.PSLookup psLookup2;
        private PS.Lib.WinForms.PSTextoBox psTextoBox8;
        private PS.Lib.WinForms.PSTextoBox psTextoBox7;
        private PS.Lib.WinForms.PSTextoBox psTextoBox6;
        private PS.Lib.WinForms.PSLookup psLookup6;
        private PS.Lib.WinForms.PSTextoBox psTextoBox5;
        private PS.Lib.WinForms.PSCheckBox psCheckBox1;
        private Lib.WinForms.PSComboBox psComboBoxCODTIPLOC;
    }
}