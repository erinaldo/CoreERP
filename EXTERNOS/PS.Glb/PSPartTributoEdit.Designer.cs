namespace PS.Glb
{
    partial class PSPartTributoEdit
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
            this.psMoedaBox1 = new PS.Lib.WinForms.PSMoedaBox();
            this.psComboBox5 = new PS.Lib.WinForms.PSComboBox();
            this.psLookup1 = new PS.Lib.WinForms.PSLookup();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psLookup1);
            this.tabPage1.Controls.Add(this.psComboBox5);
            this.tabPage1.Controls.Add(this.psMoedaBox1);
            this.tabPage1.Controls.Add(this.psCheckBox1);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODTRIBUTO";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.DataField = "CODTRIBUTO";
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
            this.psTextoBox2.MaxLength = 50;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(432, 37);
            this.psTextoBox2.TabIndex = 2;
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
            this.psCheckBox1.TabIndex = 1;
            // 
            // psMoedaBox1
            // 
            this.psMoedaBox1.Caption = "ALIQUOTA";
            this.psMoedaBox1.CasasDecimais = 2;
            this.psMoedaBox1.Edita = true;
            this.psMoedaBox1.DataField = "ALIQUOTA";
            this.psMoedaBox1.Location = new System.Drawing.Point(156, 92);
            this.psMoedaBox1.Name = "psMoedaBox1";
            this.psMoedaBox1.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox1.TabIndex = 4;
            // 
            // psComboBox5
            // 
            this.psComboBox5.Caption = "ALIQUOTAEM";
            this.psComboBox5.Chave = true;
            this.psComboBox5.DataField = "ALIQUOTAEM";
            this.psComboBox5.DataSource = null;
            this.psComboBox5.DisplayMember = "";
            this.psComboBox5.Location = new System.Drawing.Point(11, 92);
            this.psComboBox5.Name = "psComboBox5";
            this.psComboBox5.SelectedIndex = -1;
            this.psComboBox5.Size = new System.Drawing.Size(139, 37);
            this.psComboBox5.TabIndex = 6;
            this.psComboBox5.Value = null;
            this.psComboBox5.ValueMember = "";
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODTIPOTRIBUTO";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODTIPOTRIBUTO";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.KeyField = "CODTIPOTRIBUTO";
            this.psLookup1.Location = new System.Drawing.Point(11, 135);
            this.psLookup1.LookupField = "CODTIPOTRIBUTO;DESCRICAO";
            this.psLookup1.LookupFieldResult = "CODTIPOTRIBUTO;DESCRICAO";
            this.psLookup1.MaxLength = 15;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(401, 38);
            this.psLookup1.TabIndex = 7;
            this.psLookup1.ValorRetorno = null;
            // 
            // PSPartTributoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartTributoEdit";
            this.PermiteEditar = true;
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.PermiteSavar = true;
            this.Text = "PSPartTributoEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private PS.Lib.WinForms.PSCheckBox psCheckBox1;
        private PS.Lib.WinForms.PSMoedaBox psMoedaBox1;
        private PS.Lib.WinForms.PSComboBox psComboBox5;
        private Lib.WinForms.PSLookup psLookup1;
    }
}