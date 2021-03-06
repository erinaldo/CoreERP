namespace PS.Glb
{
    partial class PSPartTipOperTributoEdit
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
            this.psLookup1 = new PS.Lib.WinForms.PSLookup();
            this.psMoedaBox1 = new PS.Lib.WinForms.PSMoedaBox();
            this.psLookup2 = new PS.Lib.WinForms.PSLookup();
            this.psTextoBox1 = new PS.Lib.WinForms.PSTextoBox();
            this.PSComboCODQUERY = new PS.Lib.WinForms.PSComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(647, 258);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.PSComboCODQUERY);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Controls.Add(this.psLookup1);
            this.tabPage1.Controls.Add(this.psLookup2);
            this.tabPage1.Controls.Add(this.psMoedaBox1);
            this.tabPage1.Size = new System.Drawing.Size(639, 232);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 320);
            this.panel2.Size = new System.Drawing.Size(647, 54);
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(647, 30);
            // 
            // buttonSALVAR
            // 
            this.buttonSALVAR.Location = new System.Drawing.Point(370, 15);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(461, 15);
            // 
            // buttonCANCELAR
            // 
            this.buttonCANCELAR.Location = new System.Drawing.Point(551, 15);
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODTRIBUTO";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODTRIBUTO";
            this.psLookup1.Description = "";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup1.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup1.KeyField = "CODTRIBUTO";
            this.psLookup1.Location = new System.Drawing.Point(11, 6);
            this.psLookup1.LookupField = "CODTRIBUTO;DESCRICAO";
            this.psLookup1.LookupFieldResult = "CODTRIBUTO;DESCRICAO";
            this.psLookup1.MaxLength = 15;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(401, 38);
            this.psLookup1.TabIndex = 0;
            this.psLookup1.ValorRetorno = null;
            // 
            // psMoedaBox1
            // 
            this.psMoedaBox1.Caption = "ALIQUOTA";
            this.psMoedaBox1.CasasDecimais = 2;
            this.psMoedaBox1.DataField = "ALIQUOTA";
            this.psMoedaBox1.Edita = true;
            this.psMoedaBox1.Location = new System.Drawing.Point(11, 50);
            this.psMoedaBox1.Name = "psMoedaBox1";
            this.psMoedaBox1.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox1.TabIndex = 1;
            // 
            // psLookup2
            // 
            this.psLookup2.Caption = "CODFORMULABASECALCULO";
            this.psLookup2.Chave = true;
            this.psLookup2.DataField = "CODFORMULABASECALCULO";
            this.psLookup2.Description = "";
            this.psLookup2.DinamicTable = null;
            this.psLookup2.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup2.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup2.KeyField = "CODFORMULA";
            this.psLookup2.Location = new System.Drawing.Point(11, 93);
            this.psLookup2.LookupField = "CODFORMULA;DESCRICAO";
            this.psLookup2.LookupFieldResult = "CODFORMULA;DESCRICAO";
            this.psLookup2.MaxLength = 15;
            this.psLookup2.Name = "psLookup2";
            this.psLookup2.PSPart = null;
            this.psLookup2.Size = new System.Drawing.Size(401, 38);
            this.psLookup2.TabIndex = 2;
            this.psLookup2.ValorRetorno = null;
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODCST";
            this.psTextoBox1.DataField = "CODCST";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 137);
            this.psTextoBox1.MaxLength = 3;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 4;
            // 
            // PSComboCODQUERY
            // 
            this.PSComboCODQUERY.Caption = "CODQUERY";
            this.PSComboCODQUERY.Chave = true;
            this.PSComboCODQUERY.DataField = "CODQUERY";
            this.PSComboCODQUERY.DataSource = null;
            this.PSComboCODQUERY.DisplayMember = "";
            this.PSComboCODQUERY.Location = new System.Drawing.Point(418, 94);
            this.PSComboCODQUERY.Name = "PSComboCODQUERY";
            this.PSComboCODQUERY.SelectedIndex = -1;
            this.PSComboCODQUERY.Size = new System.Drawing.Size(204, 37);
            this.PSComboCODQUERY.TabIndex = 5;
            this.PSComboCODQUERY.Value = null;
            this.PSComboCODQUERY.ValueMember = "";
            // 
            // PSPartTipOperTributoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartTipOperTributoEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartTipOperTributoEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSLookup psLookup1;
        private PS.Lib.WinForms.PSMoedaBox psMoedaBox1;
        private PS.Lib.WinForms.PSLookup psLookup2;
        private Lib.WinForms.PSTextoBox psTextoBox1;
        private Lib.WinForms.PSComboBox PSComboCODQUERY;
    }
}