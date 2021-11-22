namespace PS.Glb
{
    partial class PSPartTipoTributoEdit
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
            this.psCheckBox1 = new PS.Lib.WinForms.PSCheckBox();
            this.psTextoBox2 = new PS.Lib.WinForms.PSTextoBox();
            this.psComboBox3 = new PS.Lib.WinForms.PSComboBox();
            this.psComboBox1 = new PS.Lib.WinForms.PSComboBox();
            this.psComboBox2 = new PS.Lib.WinForms.PSComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.psDateBox1 = new PS.Lib.WinForms.PSDateBox();
            this.psDateBox2 = new PS.Lib.WinForms.PSDateBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.psComboBox2);
            this.tabPage1.Controls.Add(this.psComboBox1);
            this.tabPage1.Controls.Add(this.psComboBox3);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psCheckBox1);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODTIPOTRIBUTO";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.DataField = "CODTIPOTRIBUTO";
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 15;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 1;
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
            this.psCheckBox1.TabIndex = 2;
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
            this.psTextoBox2.TabIndex = 3;
            // 
            // psComboBox3
            // 
            this.psComboBox3.Caption = "TIPOALIQUOTA";
            this.psComboBox3.Chave = true;
            this.psComboBox3.DataField = "TIPOALIQUOTA";
            this.psComboBox3.DataSource = null;
            this.psComboBox3.DisplayMember = "";
            this.psComboBox3.Location = new System.Drawing.Point(11, 92);
            this.psComboBox3.Name = "psComboBox3";
            this.psComboBox3.SelectedIndex = -1;
            this.psComboBox3.Size = new System.Drawing.Size(139, 37);
            this.psComboBox3.TabIndex = 4;
            this.psComboBox3.Value = null;
            this.psComboBox3.ValueMember = "";
            // 
            // psComboBox1
            // 
            this.psComboBox1.Caption = "ABRANGENCIA";
            this.psComboBox1.Chave = true;
            this.psComboBox1.DataField = "ABRANGENCIA";
            this.psComboBox1.DataSource = null;
            this.psComboBox1.DisplayMember = "";
            this.psComboBox1.Location = new System.Drawing.Point(162, 92);
            this.psComboBox1.Name = "psComboBox1";
            this.psComboBox1.SelectedIndex = -1;
            this.psComboBox1.Size = new System.Drawing.Size(139, 37);
            this.psComboBox1.TabIndex = 8;
            this.psComboBox1.Value = null;
            this.psComboBox1.ValueMember = "";
            // 
            // psComboBox2
            // 
            this.psComboBox2.Caption = "PERIODICIDADE";
            this.psComboBox2.Chave = true;
            this.psComboBox2.DataField = "PERIODICIDADE";
            this.psComboBox2.DataSource = null;
            this.psComboBox2.DisplayMember = "";
            this.psComboBox2.Location = new System.Drawing.Point(307, 92);
            this.psComboBox2.Name = "psComboBox2";
            this.psComboBox2.SelectedIndex = -1;
            this.psComboBox2.Size = new System.Drawing.Size(139, 37);
            this.psComboBox2.TabIndex = 9;
            this.psComboBox2.Value = null;
            this.psComboBox2.ValueMember = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.psDateBox1);
            this.groupBox1.Controls.Add(this.psDateBox2);
            this.groupBox1.Location = new System.Drawing.Point(11, 135);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 63);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vigência";
            // 
            // psDateBox1
            // 
            this.psDateBox1.Caption = "DTINIVIGENCIA";
            this.psDateBox1.Chave = true;
            this.psDateBox1.DataField = "DTINIVIGENCIA";
            this.psDateBox1.Location = new System.Drawing.Point(22, 19);
            this.psDateBox1.Mascara = "00/00/0000";
            this.psDateBox1.MaxLength = 32767;
            this.psDateBox1.Name = "psDateBox1";
            this.psDateBox1.Size = new System.Drawing.Size(146, 37);
            this.psDateBox1.TabIndex = 0;
            this.psDateBox1.Value = new System.DateTime(2015, 8, 20, 0, 0, 0, 0);
            // 
            // psDateBox2
            // 
            this.psDateBox2.Caption = "DTFIMVIGENCIA";
            this.psDateBox2.Chave = true;
            this.psDateBox2.DataField = "DTFIMVIGENCIA";
            this.psDateBox2.Location = new System.Drawing.Point(174, 19);
            this.psDateBox2.Mascara = "00/00/0000";
            this.psDateBox2.MaxLength = 32767;
            this.psDateBox2.Name = "psDateBox2";
            this.psDateBox2.Size = new System.Drawing.Size(146, 37);
            this.psDateBox2.TabIndex = 1;
            this.psDateBox2.Value = new System.DateTime(2015, 8, 20, 0, 0, 0, 0);
            // 
            // PSPartTipoTributoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartTipoTributoEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.PermiteSavar = true;
            this.Text = "PSPartTipoTributoEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Lib.WinForms.PSTextoBox psTextoBox1;
        private Lib.WinForms.PSCheckBox psCheckBox1;
        private Lib.WinForms.PSTextoBox psTextoBox2;
        private Lib.WinForms.PSComboBox psComboBox3;
        private Lib.WinForms.PSComboBox psComboBox1;
        private Lib.WinForms.PSComboBox psComboBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private Lib.WinForms.PSDateBox psDateBox1;
        private Lib.WinForms.PSDateBox psDateBox2;
    }
}