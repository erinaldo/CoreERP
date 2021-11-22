namespace PS.Glb
{
    partial class PSPartNaturezaOrcamentoEdit
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
            this.psTextoBox2 = new PS.Lib.WinForms.PSTextoBox();
            this.psCheckBox1 = new PS.Lib.WinForms.PSCheckBox();
            this.psTextoBox1 = new PS.Lib.WinForms.PSTextoBox();
            this.psComboBox1 = new PS.Lib.WinForms.PSComboBox();
            this.psCheckBox2 = new PS.Lib.WinForms.PSCheckBox();
            this.psCheckBox3 = new PS.Lib.WinForms.PSCheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(647, 258);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psCheckBox3);
            this.tabPage1.Controls.Add(this.psCheckBox2);
            this.tabPage1.Controls.Add(this.psComboBox1);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psCheckBox1);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(639, 232);
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "CODNATUREZA";
            this.psTextoBox2.DataField = "CODNATUREZA";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox2.MaxLength = 15;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox2.TabIndex = 3;
            // 
            // psCheckBox1
            // 
            this.psCheckBox1.Caption = "ATIVO";
            this.psCheckBox1.Chave = true;
            this.psCheckBox1.Checked = false;
            this.psCheckBox1.DataField = "ATIVO";
            this.psCheckBox1.Location = new System.Drawing.Point(162, 21);
            this.psCheckBox1.Name = "psCheckBox1";
            this.psCheckBox1.Size = new System.Drawing.Size(76, 22);
            this.psCheckBox1.TabIndex = 4;
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "DESCRICAO";
            this.psTextoBox1.DataField = "DESCRICAO";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 49);
            this.psTextoBox1.MaxLength = 100;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox1.TabIndex = 5;
            // 
            // psComboBox1
            // 
            this.psComboBox1.Caption = "NATUREZA";
            this.psComboBox1.Chave = true;
            this.psComboBox1.DataField = "NATUREZA";
            this.psComboBox1.DataSource = null;
            this.psComboBox1.DisplayMember = "";
            this.psComboBox1.Location = new System.Drawing.Point(11, 92);
            this.psComboBox1.Name = "psComboBox1";
            this.psComboBox1.SelectedIndex = -1;
            this.psComboBox1.Size = new System.Drawing.Size(139, 37);
            this.psComboBox1.TabIndex = 6;
            this.psComboBox1.Value = null;
            this.psComboBox1.ValueMember = "";
            // 
            // psCheckBox2
            // 
            this.psCheckBox2.Caption = "PERMITELANCAMENTO";
            this.psCheckBox2.Chave = true;
            this.psCheckBox2.Checked = false;
            this.psCheckBox2.DataField = "PERMITELANCAMENTO";
            this.psCheckBox2.Location = new System.Drawing.Point(244, 21);
            this.psCheckBox2.Name = "psCheckBox2";
            this.psCheckBox2.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox2.TabIndex = 7;
            // 
            // psCheckBox3
            // 
            this.psCheckBox3.Caption = "CONTROLEFISCAL";
            this.psCheckBox3.Chave = true;
            this.psCheckBox3.Checked = false;
            this.psCheckBox3.DataField = "CONTROLEFISCAL";
            this.psCheckBox3.Location = new System.Drawing.Point(162, 107);
            this.psCheckBox3.Name = "psCheckBox3";
            this.psCheckBox3.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox3.TabIndex = 8;
            // 
            // PSPartNaturezaOrcamentoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartNaturezaOrcamentoEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartNaturezaOrcamentoEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Lib.WinForms.PSTextoBox psTextoBox2;
        private Lib.WinForms.PSCheckBox psCheckBox1;
        private Lib.WinForms.PSTextoBox psTextoBox1;
        private Lib.WinForms.PSComboBox psComboBox1;
        private Lib.WinForms.PSCheckBox psCheckBox2;
        private Lib.WinForms.PSCheckBox psCheckBox3;
    }
}