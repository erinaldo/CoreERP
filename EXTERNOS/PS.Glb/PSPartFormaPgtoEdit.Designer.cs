namespace PS.Glb
{
    partial class PSPartFormaPgtoEdit
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
            this.psComboBox1 = new PS.Lib.WinForms.PSComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.psMoedaBox1 = new PS.Lib.WinForms.PSMoedaBox();
            this.psLookup1 = new PS.Lib.WinForms.PSLookup();
            this.psComboBox2 = new PS.Lib.WinForms.PSComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(647, 312);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.psComboBox1);
            this.tabPage1.Controls.Add(this.psCheckBox1);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(639, 286);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODFORMA";
            this.psTextoBox1.DataField = "CODFORMA";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 5;
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
            // psComboBox1
            // 
            this.psComboBox1.Caption = "TIPO";
            this.psComboBox1.Chave = true;
            this.psComboBox1.DataField = "TIPO";
            this.psComboBox1.DataSource = null;
            this.psComboBox1.DisplayMember = "";
            this.psComboBox1.Location = new System.Drawing.Point(11, 92);
            this.psComboBox1.Name = "psComboBox1";
            this.psComboBox1.SelectedIndex = -1;
            this.psComboBox1.Size = new System.Drawing.Size(139, 37);
            this.psComboBox1.TabIndex = 3;
            this.psComboBox1.Value = null;
            this.psComboBox1.ValueMember = "";
            this.psComboBox1.SelectedValueChanged += new PS.Lib.WinForms.PSComboBox.SelectedValueChangedDelegate(this.psComboBox1_SelectedValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.psMoedaBox1);
            this.groupBox1.Controls.Add(this.psLookup1);
            this.groupBox1.Controls.Add(this.psComboBox2);
            this.groupBox1.Location = new System.Drawing.Point(6, 135);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(625, 143);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cartão de Crédito/Débito";
            // 
            // psMoedaBox1
            // 
            this.psMoedaBox1.Caption = "TAXAADM";
            this.psMoedaBox1.CasasDecimais = 2;
            this.psMoedaBox1.DataField = "TAXAADM";
            this.psMoedaBox1.Edita = true;
            this.psMoedaBox1.Location = new System.Drawing.Point(413, 63);
            this.psMoedaBox1.Name = "psMoedaBox1";
            this.psMoedaBox1.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox1.TabIndex = 2;
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODREDECARTAO";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODREDECARTAO";
            this.psLookup1.Description = "";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.KeyField = "CODREDECARTAO";
            this.psLookup1.Location = new System.Drawing.Point(6, 62);
            this.psLookup1.LookupField = "CODREDECARTAO;NOME";
            this.psLookup1.LookupFieldResult = "CODREDECARTAO;NOME";
            this.psLookup1.MaxLength = 15;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(401, 38);
            this.psLookup1.TabIndex = 1;
            this.psLookup1.ValorRetorno = null;
            // 
            // psComboBox2
            // 
            this.psComboBox2.Caption = "TIPOTRANSACAO";
            this.psComboBox2.Chave = true;
            this.psComboBox2.DataField = "TIPOTRANSACAO";
            this.psComboBox2.DataSource = null;
            this.psComboBox2.DisplayMember = "";
            this.psComboBox2.Location = new System.Drawing.Point(5, 19);
            this.psComboBox2.Name = "psComboBox2";
            this.psComboBox2.SelectedIndex = -1;
            this.psComboBox2.Size = new System.Drawing.Size(139, 37);
            this.psComboBox2.TabIndex = 0;
            this.psComboBox2.Value = null;
            this.psComboBox2.ValueMember = "";
            // 
            // PSPartFormaPgtoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 428);
            this.Name = "PSPartFormaPgtoEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartFormaPgtoEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSCheckBox psCheckBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private PS.Lib.WinForms.PSComboBox psComboBox1;
        private PS.Lib.WinForms.PSComboBox psComboBox2;
        private PS.Lib.WinForms.PSLookup psLookup1;
        private PS.Lib.WinForms.PSMoedaBox psMoedaBox1;
    }
}