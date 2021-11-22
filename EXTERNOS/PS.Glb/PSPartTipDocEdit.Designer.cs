namespace PS.Glb
{
    partial class PSPartTipDocEdit
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
            this.psCheckBox2 = new PS.Lib.WinForms.PSCheckBox();
            this.psTextoBox4 = new PS.Lib.WinForms.PSTextoBox();
            this.psComboBox1 = new PS.Lib.WinForms.PSComboBox();
            this.psComboBox2 = new PS.Lib.WinForms.PSComboBox();
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
            this.tabPage1.Controls.Add(this.psComboBox2);
            this.tabPage1.Controls.Add(this.psComboBox1);
            this.tabPage1.Controls.Add(this.psTextoBox4);
            this.tabPage1.Controls.Add(this.psCheckBox2);
            this.tabPage1.Controls.Add(this.psCheckBox1);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODTIPDOC";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.DataField = "CODTIPDOC";
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 15;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 0;
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "NOME";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.DataField = "NOME";
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
            this.psCheckBox1.Size = new System.Drawing.Size(120, 22);
            this.psCheckBox1.TabIndex = 1;
            // 
            // psCheckBox2
            // 
            this.psCheckBox2.Caption = "USANUMEROSEQ";
            this.psCheckBox2.Chave = true;
            this.psCheckBox2.Checked = false;
            this.psCheckBox2.DataField = "USANUMEROSEQ";
            this.psCheckBox2.Location = new System.Drawing.Point(6, 107);
            this.psCheckBox2.Name = "psCheckBox2";
            this.psCheckBox2.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox2.TabIndex = 3;
            // 
            // psTextoBox4
            // 
            this.psTextoBox4.Caption = "ULTIMONUMERO";
            this.psTextoBox4.Edita = true;
            this.psTextoBox4.DataField = "ULTIMONUMERO";
            this.psTextoBox4.Location = new System.Drawing.Point(162, 92);
            this.psTextoBox4.MaxLength = 15;
            this.psTextoBox4.Name = "psTextoBox4";
            this.psTextoBox4.PasswordChar = '\0';
            this.psTextoBox4.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox4.TabIndex = 5;
            // 
            // psComboBox1
            // 
            this.psComboBox1.Caption = "PAGREC";
            this.psComboBox1.Chave = true;
            this.psComboBox1.DataField = "PAGREC";
            this.psComboBox1.DataSource = null;
            this.psComboBox1.DisplayMember = "";
            this.psComboBox1.Location = new System.Drawing.Point(11, 135);
            this.psComboBox1.Name = "psComboBox1";
            this.psComboBox1.SelectedIndex = -1;
            this.psComboBox1.Size = new System.Drawing.Size(139, 37);
            this.psComboBox1.TabIndex = 6;
            this.psComboBox1.Value = null;
            this.psComboBox1.ValueMember = "";
            // 
            // psComboBox2
            // 
            this.psComboBox2.Caption = "CLASSIFICACAO";
            this.psComboBox2.Chave = true;
            this.psComboBox2.DataField = "CLASSIFICACAO";
            this.psComboBox2.DataSource = null;
            this.psComboBox2.DisplayMember = "";
            this.psComboBox2.Location = new System.Drawing.Point(162, 135);
            this.psComboBox2.Name = "psComboBox2";
            this.psComboBox2.SelectedIndex = -1;
            this.psComboBox2.Size = new System.Drawing.Size(139, 37);
            this.psComboBox2.TabIndex = 7;
            this.psComboBox2.Value = null;
            this.psComboBox2.ValueMember = "";
            // 
            // PSPartTipDocEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartTipDocEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.PermiteSavar = true;
            this.Text = "PSPartTipDocEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSCheckBox psCheckBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private PS.Lib.WinForms.PSCheckBox psCheckBox2;
        private PS.Lib.WinForms.PSTextoBox psTextoBox4;
        private Lib.WinForms.PSComboBox psComboBox2;
        private Lib.WinForms.PSComboBox psComboBox1;
    }
}