namespace PS.Glb
{
    partial class PSPartExtratoCaixaEdit
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
            this.psLookup2 = new PS.Lib.WinForms.PSLookup();
            this.psLookup4 = new PS.Lib.WinForms.PSLookup();
            this.psTextoBox2 = new PS.Lib.WinForms.PSTextoBox();
            this.psMoedaBox1 = new PS.Lib.WinForms.PSMoedaBox();
            this.psDateBox1 = new PS.Lib.WinForms.PSDateBox();
            this.psTextoBox3 = new PS.Lib.WinForms.PSTextoBox();
            this.psComboBox1 = new PS.Lib.WinForms.PSComboBox();
            this.psLookup1 = new PS.Lib.WinForms.PSLookup();
            this.psLookup3 = new PS.Lib.WinForms.PSLookup();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.psLookup16 = new PS.Lib.WinForms.PSLookup();
            this.psLookup6 = new PS.Lib.WinForms.PSLookup();
            this.psCheckBox1 = new PS.Lib.WinForms.PSCheckBox();
            this.psDateBox2 = new PS.Lib.WinForms.PSDateBox();
            this.psTextoBox4 = new PS.Lib.WinForms.PSTextoBox();
            this.psLookup5 = new PS.Lib.WinForms.PSLookup();
            this.psLookup7 = new PS.Lib.WinForms.PSLookup();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
           // this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Size = new System.Drawing.Size(647, 330);
            this.tabControl1.Controls.SetChildIndex(this.tabPage2, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPage1, 0);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psTextoBox4);
            this.tabPage1.Controls.Add(this.psDateBox2);
            this.tabPage1.Controls.Add(this.psCheckBox1);
            this.tabPage1.Controls.Add(this.psLookup3);
            this.tabPage1.Controls.Add(this.psLookup1);
            this.tabPage1.Controls.Add(this.psComboBox1);
            this.tabPage1.Controls.Add(this.psTextoBox3);
            this.tabPage1.Controls.Add(this.psDateBox1);
            this.tabPage1.Controls.Add(this.psMoedaBox1);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psLookup4);
            this.tabPage1.Controls.Add(this.psLookup2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(639, 304);
            // 
            // panel2
            // 
            //this.panel2.Location = new System.Drawing.Point(0, 392);
            //this.panel2.Size = new System.Drawing.Size(647, 54);
            //// 
            //// buttonSALVAR
            //// 
            //this.buttonSALVAR.Location = new System.Drawing.Point(370, 15);
            //// 
            //// buttonOK
            //// 
            //this.buttonOK.Location = new System.Drawing.Point(461, 15);
            //// 
            //// buttonCANCELAR
            //// 
            //this.buttonCANCELAR.Location = new System.Drawing.Point(551, 15);
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(647, 30);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.AutoIncremento = PS.Lib.Global.TypeAutoinc.AutoInc;
            this.psTextoBox1.Caption = "IDEXTRATO";
            this.psTextoBox1.DataField = "IDEXTRATO";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 32767;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 0;
            // 
            // psLookup2
            // 
            this.psLookup2.Caption = "CODFILIAL";
            this.psLookup2.Chave = true;
            this.psLookup2.DataField = "CODFILIAL";
            this.psLookup2.Description = "";
            this.psLookup2.DinamicTable = null;
            this.psLookup2.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup2.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup2.KeyField = "CODFILIAL";
            this.psLookup2.Location = new System.Drawing.Point(11, 98);
            this.psLookup2.LookupField = "CODFILIAL;NOME;NOMEFANTASIA";
            this.psLookup2.LookupFieldResult = "CODFILIAL;NOME";
            this.psLookup2.MaxLength = 32767;
            this.psLookup2.Name = "psLookup2";
            this.psLookup2.PSPart = null;
            this.psLookup2.Size = new System.Drawing.Size(298, 38);
            this.psLookup2.TabIndex = 6;
            this.psLookup2.ValorRetorno = null;
            // 
            // psLookup4
            // 
            this.psLookup4.Caption = "CODCONTA";
            this.psLookup4.Chave = true;
            this.psLookup4.DataField = "CODCONTA";
            this.psLookup4.Description = "";
            this.psLookup4.DinamicTable = null;
            this.psLookup4.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup4.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup4.KeyField = "CODCONTA";
            this.psLookup4.Location = new System.Drawing.Point(11, 142);
            this.psLookup4.LookupField = "CODCONTA;DESCRICAO";
            this.psLookup4.LookupFieldResult = "CODCONTA;DESCRICAO";
            this.psLookup4.MaxLength = 15;
            this.psLookup4.Name = "psLookup4";
            this.psLookup4.PSPart = null;
            this.psLookup4.Size = new System.Drawing.Size(298, 38);
            this.psLookup4.TabIndex = 7;
            this.psLookup4.ValorRetorno = null;
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "NUMERODOCUMENTO";
            this.psTextoBox2.DataField = "NUMERODOCUMENTO";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.Location = new System.Drawing.Point(164, 49);
            this.psTextoBox2.MaxLength = 25;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox2.TabIndex = 8;
            // 
            // psMoedaBox1
            // 
            this.psMoedaBox1.Caption = "VALOR";
            this.psMoedaBox1.CasasDecimais = 2;
            this.psMoedaBox1.DataField = "VALOR";
            this.psMoedaBox1.Edita = true;
            this.psMoedaBox1.Location = new System.Drawing.Point(13, 186);
            this.psMoedaBox1.Name = "psMoedaBox1";
            this.psMoedaBox1.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox1.TabIndex = 9;
            // 
            // psDateBox1
            // 
            this.psDateBox1.Caption = "DATA";
            this.psDateBox1.Chave = true;
            this.psDateBox1.DataField = "DATA";
            this.psDateBox1.Location = new System.Drawing.Point(164, 186);
            this.psDateBox1.Mascara = "00/00/0000";
            this.psDateBox1.MaxLength = 32767;
            this.psDateBox1.Name = "psDateBox1";
            this.psDateBox1.Size = new System.Drawing.Size(146, 37);
            this.psDateBox1.TabIndex = 10;
            this.psDateBox1.Value = new System.DateTime(2015, 9, 30, 13, 54, 44, 807);
            // 
            // psTextoBox3
            // 
            this.psTextoBox3.Caption = "HISTORICO";
            this.psTextoBox3.DataField = "HISTORICO";
            this.psTextoBox3.Edita = true;
            this.psTextoBox3.Location = new System.Drawing.Point(11, 229);
            this.psTextoBox3.MaxLength = 80;
            this.psTextoBox3.Name = "psTextoBox3";
            this.psTextoBox3.PasswordChar = '\0';
            this.psTextoBox3.Size = new System.Drawing.Size(620, 37);
            this.psTextoBox3.TabIndex = 11;
            // 
            // psComboBox1
            // 
            this.psComboBox1.Caption = "TIPO";
            this.psComboBox1.Chave = true;
            this.psComboBox1.DataField = "TIPO";
            this.psComboBox1.DataSource = null;
            this.psComboBox1.DisplayMember = "";
            this.psComboBox1.Location = new System.Drawing.Point(11, 49);
            this.psComboBox1.Name = "psComboBox1";
            this.psComboBox1.SelectedIndex = -1;
            this.psComboBox1.Size = new System.Drawing.Size(147, 37);
            this.psComboBox1.TabIndex = 12;
            this.psComboBox1.Value = null;
            this.psComboBox1.ValueMember = "";
            this.psComboBox1.SelectedValueChanged += new PS.Lib.WinForms.PSComboBox.SelectedValueChangedDelegate(this.psComboBox1_SelectedValueChanged);
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODFILIALTRF";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODFILIALTRF";
            this.psLookup1.Description = "";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup1.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup1.KeyField = "CODFILIAL";
            this.psLookup1.Location = new System.Drawing.Point(333, 98);
            this.psLookup1.LookupField = "CODFILIAL;NOME;NOMEFANTASIA";
            this.psLookup1.LookupFieldResult = "CODFILIAL;NOME";
            this.psLookup1.MaxLength = 32767;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(298, 38);
            this.psLookup1.TabIndex = 13;
            this.psLookup1.ValorRetorno = null;
            // 
            // psLookup3
            // 
            this.psLookup3.Caption = "CODCONTATRF";
            this.psLookup3.Chave = true;
            this.psLookup3.DataField = "CODCONTATRF";
            this.psLookup3.Description = "";
            this.psLookup3.DinamicTable = null;
            this.psLookup3.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup3.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup3.KeyField = "CODCONTA";
            this.psLookup3.Location = new System.Drawing.Point(333, 142);
            this.psLookup3.LookupField = "CODCONTA;DESCRICAO";
            this.psLookup3.LookupFieldResult = "CODCONTA;DESCRICAO";
            this.psLookup3.MaxLength = 15;
            this.psLookup3.Name = "psLookup3";
            this.psLookup3.PSPart = null;
            this.psLookup3.Size = new System.Drawing.Size(298, 38);
            this.psLookup3.TabIndex = 14;
            this.psLookup3.ValorRetorno = null;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.psLookup5);
            this.tabPage2.Controls.Add(this.psLookup7);
            this.tabPage2.Controls.Add(this.psLookup16);
            this.tabPage2.Controls.Add(this.psLookup6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(639, 304);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Dados Adicionais";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // psLookup16
            // 
            this.psLookup16.Caption = "CODNATUREZAORCAMENTO";
            this.psLookup16.Chave = true;
            this.psLookup16.DataField = "CODNATUREZAORCAMENTO";
            this.psLookup16.Description = "";
            this.psLookup16.DinamicTable = null;
            this.psLookup16.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup16.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup16.KeyField = "CODNATUREZA";
            this.psLookup16.Location = new System.Drawing.Point(11, 50);
            this.psLookup16.LookupField = "CODNATUREZA;DESCRICAO";
            this.psLookup16.LookupFieldResult = "CODNATUREZA;DESCRICAO";
            this.psLookup16.MaxLength = 15;
            this.psLookup16.Name = "psLookup16";
            this.psLookup16.PSPart = null;
            this.psLookup16.Size = new System.Drawing.Size(401, 38);
            this.psLookup16.TabIndex = 9;
            this.psLookup16.ValorRetorno = null;
            // 
            // psLookup6
            // 
            this.psLookup6.Caption = "CODCCUSTO";
            this.psLookup6.Chave = true;
            this.psLookup6.DataField = "CODCCUSTO";
            this.psLookup6.Description = "";
            this.psLookup6.DinamicTable = null;
            this.psLookup6.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup6.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup6.KeyField = "CODCCUSTO";
            this.psLookup6.Location = new System.Drawing.Point(11, 6);
            this.psLookup6.LookupField = "CODCCUSTO;NOME";
            this.psLookup6.LookupFieldResult = "CODCCUSTO;NOME";
            this.psLookup6.MaxLength = 15;
            this.psLookup6.Name = "psLookup6";
            this.psLookup6.PSPart = null;
            this.psLookup6.Size = new System.Drawing.Size(401, 38);
            this.psLookup6.TabIndex = 1;
            this.psLookup6.ValorRetorno = null;
            // 
            // psCheckBox1
            // 
            this.psCheckBox1.Caption = "COMPENSADO";
            this.psCheckBox1.Chave = true;
            this.psCheckBox1.Checked = false;
            this.psCheckBox1.DataField = "COMPENSADO";
            this.psCheckBox1.Location = new System.Drawing.Point(333, 201);
            this.psCheckBox1.Name = "psCheckBox1";
            this.psCheckBox1.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox1.TabIndex = 15;
            this.psCheckBox1.CheckedChanged += new PS.Lib.WinForms.PSCheckBox.CheckedChangedDelegate(this.psCheckBox1_CheckedChanged);
            // 
            // psDateBox2
            // 
            this.psDateBox2.Caption = "DATACOMPENSACAO";
            this.psDateBox2.Chave = true;
            this.psDateBox2.DataField = "DATACOMPENSACAO";
            this.psDateBox2.Location = new System.Drawing.Point(485, 186);
            this.psDateBox2.Mascara = "00/00/0000";
            this.psDateBox2.MaxLength = 32767;
            this.psDateBox2.Name = "psDateBox2";
            this.psDateBox2.Size = new System.Drawing.Size(146, 37);
            this.psDateBox2.TabIndex = 16;
            this.psDateBox2.Value = new System.DateTime(2015, 9, 30, 14, 3, 4, 58);
            // 
            // psTextoBox4
            // 
            this.psTextoBox4.Caption = "IDEXTRATOTRF";
            this.psTextoBox4.DataField = "IDEXTRATOTRF";
            this.psTextoBox4.Edita = true;
            this.psTextoBox4.Location = new System.Drawing.Point(165, 6);
            this.psTextoBox4.MaxLength = 32767;
            this.psTextoBox4.Name = "psTextoBox4";
            this.psTextoBox4.PasswordChar = '\0';
            this.psTextoBox4.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox4.TabIndex = 17;
            // 
            // psLookup5
            // 
            this.psLookup5.Caption = "CODNATUREZAORCAMENTOTRANSF";
            this.psLookup5.Chave = true;
            this.psLookup5.DataField = "CODNATUREZAORCAMENTOTRANSF";
            this.psLookup5.Description = "";
            this.psLookup5.DinamicTable = null;
            this.psLookup5.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup5.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup5.KeyField = "CODNATUREZA";
            this.psLookup5.Location = new System.Drawing.Point(11, 151);
            this.psLookup5.LookupField = "CODNATUREZA;DESCRICAO";
            this.psLookup5.LookupFieldResult = "CODNATUREZA;DESCRICAO";
            this.psLookup5.MaxLength = 15;
            this.psLookup5.Name = "psLookup5";
            this.psLookup5.PSPart = null;
            this.psLookup5.Size = new System.Drawing.Size(401, 38);
            this.psLookup5.TabIndex = 11;
            this.psLookup5.ValorRetorno = null;
            // 
            // psLookup7
            // 
            this.psLookup7.Caption = "CODCCUSTOTRANSF";
            this.psLookup7.Chave = true;
            this.psLookup7.DataField = "CODCCUSTOTRANSF";
            this.psLookup7.Description = "";
            this.psLookup7.DinamicTable = null;
            this.psLookup7.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup7.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup7.KeyField = "CODCCUSTO";
            this.psLookup7.Location = new System.Drawing.Point(11, 107);
            this.psLookup7.LookupField = "CODCCUSTO;NOME";
            this.psLookup7.LookupFieldResult = "CODCCUSTO;NOME";
            this.psLookup7.MaxLength = 15;
            this.psLookup7.Name = "psLookup7";
            this.psLookup7.PSPart = null;
            this.psLookup7.Size = new System.Drawing.Size(401, 38);
            this.psLookup7.TabIndex = 10;
            this.psLookup7.ValorRetorno = null;
            // 
            // PSPartExtratoCaixaEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 446);
            this.Name = "PSPartExtratoCaixaEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartExtratoCaixaEdit";
            this.Load += new System.EventHandler(this.PSPartExtratoCaixaEdit_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            //this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSLookup psLookup2;
        private PS.Lib.WinForms.PSLookup psLookup4;
        private PS.Lib.WinForms.PSTextoBox psTextoBox3;
        private PS.Lib.WinForms.PSDateBox psDateBox1;
        private PS.Lib.WinForms.PSMoedaBox psMoedaBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private Lib.WinForms.PSComboBox psComboBox1;
        private Lib.WinForms.PSLookup psLookup3;
        private Lib.WinForms.PSLookup psLookup1;
        private System.Windows.Forms.TabPage tabPage2;
        private Lib.WinForms.PSLookup psLookup6;
        private Lib.WinForms.PSDateBox psDateBox2;
        private Lib.WinForms.PSCheckBox psCheckBox1;
        private Lib.WinForms.PSTextoBox psTextoBox4;
        private Lib.WinForms.PSLookup psLookup16;
        private Lib.WinForms.PSLookup psLookup5;
        private Lib.WinForms.PSLookup psLookup7;
    }
}