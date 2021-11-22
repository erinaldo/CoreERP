namespace PS.Glb
{
    partial class PSPartNaturezaRegraTributacaoEdit
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
            this.psComboBox8 = new PS.Lib.WinForms.PSComboBox();
            this.psLookup4 = new PS.Lib.WinForms.PSLookup();
            this.psLookup1 = new PS.Lib.WinForms.PSLookup();
            this.psComboBox1 = new PS.Lib.WinForms.PSComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(475, 265);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psComboBox1);
            this.tabPage1.Controls.Add(this.psLookup1);
            this.tabPage1.Controls.Add(this.psLookup4);
            this.tabPage1.Controls.Add(this.psComboBox8);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(467, 239);
            // 
            // buttonSALVAR
            // 
            // 
            // buttonOK
            // 
            // 
            // buttonCANCELAR
            // 
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.AutoIncremento = PS.Lib.Global.TypeAutoinc.Max;
            this.psTextoBox1.Caption = "NSEQREGRA";
            this.psTextoBox1.DataField = "NSEQREGRA";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 32767;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 1;
            // 
            // psComboBox8
            // 
            this.psComboBox8.Caption = "TIPOREGRA";
            this.psComboBox8.Chave = true;
            this.psComboBox8.DataField = "TIPOREGRA";
            this.psComboBox8.DataSource = null;
            this.psComboBox8.DisplayMember = "";
            this.psComboBox8.Location = new System.Drawing.Point(11, 49);
            this.psComboBox8.Name = "psComboBox8";
            this.psComboBox8.SelectedIndex = -1;
            this.psComboBox8.Size = new System.Drawing.Size(139, 37);
            this.psComboBox8.TabIndex = 4;
            this.psComboBox8.Value = null;
            this.psComboBox8.ValueMember = "";
            this.psComboBox8.SelectedValueChanged += new PS.Lib.WinForms.PSComboBox.SelectedValueChangedDelegate(this.psComboBox8_SelectedValueChanged);
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
            this.psLookup4.Location = new System.Drawing.Point(11, 92);
            this.psLookup4.LookupField = "CODETD;NOME";
            this.psLookup4.LookupFieldResult = "CODETD;NOME";
            this.psLookup4.MaxLength = 2;
            this.psLookup4.Name = "psLookup4";
            this.psLookup4.PSPart = null;
            this.psLookup4.Size = new System.Drawing.Size(401, 38);
            this.psLookup4.TabIndex = 31;
            this.psLookup4.ValorRetorno = null;
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODREGIAO";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODREGIAO";
            this.psLookup1.Description = "";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup1.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup1.KeyField = "CODREGIAO";
            this.psLookup1.Location = new System.Drawing.Point(11, 136);
            this.psLookup1.LookupField = "CODREGIAO;DESCRICAO";
            this.psLookup1.LookupFieldResult = "CODREGIAO;DESCRICAO";
            this.psLookup1.MaxLength = 15;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(401, 38);
            this.psLookup1.TabIndex = 32;
            this.psLookup1.ValorRetorno = null;
            // 
            // psComboBox1
            // 
            this.psComboBox1.Caption = "CONTRIBUINTE";
            this.psComboBox1.Chave = true;
            this.psComboBox1.DataField = "CONTRIBUINTE";
            this.psComboBox1.DataSource = null;
            this.psComboBox1.DisplayMember = "";
            this.psComboBox1.Location = new System.Drawing.Point(11, 180);
            this.psComboBox1.Name = "psComboBox1";
            this.psComboBox1.SelectedIndex = -1;
            this.psComboBox1.Size = new System.Drawing.Size(139, 37);
            this.psComboBox1.TabIndex = 33;
            this.psComboBox1.Value = null;
            this.psComboBox1.ValueMember = "";
            // 
            // PSPartNaturezaRegraTributacaoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 381);
            this.MaximizeBox = true;
            this.Name = "PSPartNaturezaRegraTributacaoEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartNaturezaRegraTributacaoEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Lib.WinForms.PSTextoBox psTextoBox1;
        private Lib.WinForms.PSComboBox psComboBox8;
        private Lib.WinForms.PSLookup psLookup4;
        private Lib.WinForms.PSLookup psLookup1;
        private Lib.WinForms.PSComboBox psComboBox1;
    }
}