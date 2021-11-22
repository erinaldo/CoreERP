namespace PS.Glb
{
    partial class PSPartTabCampoComplEdit
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
            this.psComboBox1 = new PS.Lib.WinForms.PSComboBox();
            this.psTextoBox1 = new PS.Lib.WinForms.PSTextoBox();
            this.psTextoBox2 = new PS.Lib.WinForms.PSTextoBox();
            this.psComboBox2 = new PS.Lib.WinForms.PSComboBox();
            this.psMoedaBox1 = new PS.Lib.WinForms.PSMoedaBox();
            this.psMoedaBox2 = new PS.Lib.WinForms.PSMoedaBox();
            this.psLookup1 = new PS.Lib.WinForms.PSLookup();
            this.psCheckBox1 = new PS.Lib.WinForms.PSCheckBox();
            this.psMoedaBox3 = new PS.Lib.WinForms.PSMoedaBox();
            this.psMoedaBox4 = new PS.Lib.WinForms.PSMoedaBox();
            this.psTextoBox3 = new PS.Lib.WinForms.PSTextoBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(647, 286);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psTextoBox3);
            this.tabPage1.Controls.Add(this.psMoedaBox4);
            this.tabPage1.Controls.Add(this.psMoedaBox3);
            this.tabPage1.Controls.Add(this.psCheckBox1);
            this.tabPage1.Controls.Add(this.psLookup1);
            this.tabPage1.Controls.Add(this.psMoedaBox2);
            this.tabPage1.Controls.Add(this.psMoedaBox1);
            this.tabPage1.Controls.Add(this.psComboBox2);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Controls.Add(this.psComboBox1);
            this.tabPage1.Size = new System.Drawing.Size(639, 260);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 348);
            this.panel2.Size = new System.Drawing.Size(647, 54);
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(647, 30);
            // 
            // buttonSALVAR
            // 
            this.buttonSALVAR.Location = new System.Drawing.Point(398, 19);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(479, 19);
            // 
            // buttonCANCELAR
            // 
            this.buttonCANCELAR.Location = new System.Drawing.Point(560, 19);
            // 
            // psComboBox1
            // 
            this.psComboBox1.Caption = "CODENTIDADE";
            this.psComboBox1.Chave = true;
            this.psComboBox1.DataField = "CODENTIDADE";
            this.psComboBox1.DataSource = null;
            this.psComboBox1.DisplayMember = "";
            this.psComboBox1.Location = new System.Drawing.Point(11, 6);
            this.psComboBox1.Name = "psComboBox1";
            this.psComboBox1.SelectedIndex = -1;
            this.psComboBox1.Size = new System.Drawing.Size(139, 37);
            this.psComboBox1.TabIndex = 0;
            this.psComboBox1.Value = null;
            this.psComboBox1.ValueMember = "";
            this.psComboBox1.Load += new System.EventHandler(this.psComboBox1_Load);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "NOMECAMPO";
            this.psTextoBox1.DataField = "NOMECAMPO";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 49);
            this.psTextoBox1.MaxLength = 50;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 1;
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "DESCRICAO";
            this.psTextoBox2.DataField = "DESCRICAO";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.Location = new System.Drawing.Point(162, 49);
            this.psTextoBox2.MaxLength = 25;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(286, 37);
            this.psTextoBox2.TabIndex = 2;
            // 
            // psComboBox2
            // 
            this.psComboBox2.Caption = "TIPO";
            this.psComboBox2.Chave = true;
            this.psComboBox2.DataField = "TIPO";
            this.psComboBox2.DataSource = null;
            this.psComboBox2.DisplayMember = "";
            this.psComboBox2.Location = new System.Drawing.Point(11, 92);
            this.psComboBox2.Name = "psComboBox2";
            this.psComboBox2.SelectedIndex = -1;
            this.psComboBox2.Size = new System.Drawing.Size(139, 37);
            this.psComboBox2.TabIndex = 3;
            this.psComboBox2.Value = null;
            this.psComboBox2.ValueMember = "";
            this.psComboBox2.SelectedValueChanged += new PS.Lib.WinForms.PSComboBox.SelectedValueChangedDelegate(this.psComboBox2_SelectedValueChanged);
            // 
            // psMoedaBox1
            // 
            this.psMoedaBox1.Caption = "TAMANHO";
            this.psMoedaBox1.DataField = "TAMANHO";
            this.psMoedaBox1.Edita = true;
            this.psMoedaBox1.Location = new System.Drawing.Point(11, 135);
            this.psMoedaBox1.Name = "psMoedaBox1";
            this.psMoedaBox1.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox1.TabIndex = 6;
            // 
            // psMoedaBox2
            // 
            this.psMoedaBox2.Caption = "ORDEM";
            this.psMoedaBox2.DataField = "ORDEM";
            this.psMoedaBox2.Edita = true;
            this.psMoedaBox2.Location = new System.Drawing.Point(11, 178);
            this.psMoedaBox2.Name = "psMoedaBox2";
            this.psMoedaBox2.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox2.TabIndex = 7;
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODTABELA";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODTABELA";
            this.psLookup1.Description = "";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup1.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup1.KeyField = "CODTABELA";
            this.psLookup1.Location = new System.Drawing.Point(162, 91);
            this.psLookup1.LookupField = "CODTABELA;DESCRICAO";
            this.psLookup1.LookupFieldResult = "CODTABELA;DESCRICAO";
            this.psLookup1.MaxLength = 32767;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(286, 38);
            this.psLookup1.TabIndex = 8;
            this.psLookup1.ValorRetorno = null;
            // 
            // psCheckBox1
            // 
            this.psCheckBox1.Caption = "ATIVO";
            this.psCheckBox1.Chave = true;
            this.psCheckBox1.Checked = false;
            this.psCheckBox1.DataField = "ATIVO";
            this.psCheckBox1.Location = new System.Drawing.Point(162, 21);
            this.psCheckBox1.Name = "psCheckBox1";
            this.psCheckBox1.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox1.TabIndex = 9;
            // 
            // psMoedaBox3
            // 
            this.psMoedaBox3.Caption = "CASASDECIMAIS";
            this.psMoedaBox3.DataField = "CASASDECIMAIS";
            this.psMoedaBox3.Edita = true;
            this.psMoedaBox3.Location = new System.Drawing.Point(454, 92);
            this.psMoedaBox3.Name = "psMoedaBox3";
            this.psMoedaBox3.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox3.TabIndex = 10;
            // 
            // psMoedaBox4
            // 
            this.psMoedaBox4.Caption = "TAMANHOCAMPO";
            this.psMoedaBox4.DataField = "TAMANHOCAMPO";
            this.psMoedaBox4.Edita = true;
            this.psMoedaBox4.Location = new System.Drawing.Point(162, 135);
            this.psMoedaBox4.Name = "psMoedaBox4";
            this.psMoedaBox4.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox4.TabIndex = 11;
            // 
            // psTextoBox3
            // 
            this.psTextoBox3.Caption = "ALTURACAMPO";
            this.psTextoBox3.DataField = "ALTURACAMPO";
            this.psTextoBox3.Edita = true;
            this.psTextoBox3.Location = new System.Drawing.Point(313, 135);
            this.psTextoBox3.MaxLength = 50;
            this.psTextoBox3.Name = "psTextoBox3";
            this.psTextoBox3.PasswordChar = '\0';
            this.psTextoBox3.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox3.TabIndex = 12;
            this.psTextoBox3.Visible = false;
            // 
            // PSPartTabCampoComplEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 402);
            this.Name = "PSPartTabCampoComplEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartTabCampoComplEdit";
            this.Load += new System.EventHandler(this.PSPartTabCampoComplEdit_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSComboBox psComboBox1;
        private PS.Lib.WinForms.PSComboBox psComboBox2;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSMoedaBox psMoedaBox2;
        private PS.Lib.WinForms.PSMoedaBox psMoedaBox1;
        private PS.Lib.WinForms.PSLookup psLookup1;
        private PS.Lib.WinForms.PSCheckBox psCheckBox1;
        private PS.Lib.WinForms.PSMoedaBox psMoedaBox3;
        private Lib.WinForms.PSMoedaBox psMoedaBox4;
        private Lib.WinForms.PSTextoBox psTextoBox3;
    }
}