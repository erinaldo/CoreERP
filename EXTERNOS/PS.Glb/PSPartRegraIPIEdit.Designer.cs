namespace PS.Glb
{
    partial class PSPartRegraIPIEdit
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
            this.psTextoBox3 = new PS.Lib.WinForms.PSTextoBox();
            this.psTextoBox4 = new PS.Lib.WinForms.PSTextoBox();
            this.psTextoBox5 = new PS.Lib.WinForms.PSTextoBox();
            this.psComboBox1 = new PS.Lib.WinForms.PSComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            //this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(647, 258);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psComboBox1);
            this.tabPage1.Controls.Add(this.psTextoBox5);
            this.tabPage1.Controls.Add(this.psTextoBox4);
            this.tabPage1.Controls.Add(this.psTextoBox3);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psCheckBox1);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(639, 232);
            // 
            // panel2
            // 
            //this.panel2.Location = new System.Drawing.Point(0, 320);
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
            //// 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(647, 30);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.AutoIncremento = PS.Lib.Global.TypeAutoinc.AutoInc;
            this.psTextoBox1.Caption = "IDREGRA";
            this.psTextoBox1.DataField = "IDREGRA";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 32767;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 2;
            // 
            // psCheckBox1
            // 
            this.psCheckBox1.Caption = "ATIVO";
            this.psCheckBox1.Chave = true;
            this.psCheckBox1.Checked = true;
            this.psCheckBox1.DataField = "ATIVO";
            this.psCheckBox1.Location = new System.Drawing.Point(162, 21);
            this.psCheckBox1.Name = "psCheckBox1";
            this.psCheckBox1.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox1.TabIndex = 4;
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "DESCRICAO";
            this.psTextoBox2.DataField = "DESCRICAO";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.Location = new System.Drawing.Point(11, 49);
            this.psTextoBox2.MaxLength = 50;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(432, 37);
            this.psTextoBox2.TabIndex = 5;
            // 
            // psTextoBox3
            // 
            this.psTextoBox3.Caption = "CODCSTENT";
            this.psTextoBox3.DataField = "CODCSTENT";
            this.psTextoBox3.Edita = true;
            this.psTextoBox3.Location = new System.Drawing.Point(11, 92);
            this.psTextoBox3.MaxLength = 3;
            this.psTextoBox3.Name = "psTextoBox3";
            this.psTextoBox3.PasswordChar = '\0';
            this.psTextoBox3.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox3.TabIndex = 13;
            // 
            // psTextoBox4
            // 
            this.psTextoBox4.Caption = "CODCSTSAI";
            this.psTextoBox4.DataField = "CODCSTSAI";
            this.psTextoBox4.Edita = true;
            this.psTextoBox4.Location = new System.Drawing.Point(167, 92);
            this.psTextoBox4.MaxLength = 3;
            this.psTextoBox4.Name = "psTextoBox4";
            this.psTextoBox4.PasswordChar = '\0';
            this.psTextoBox4.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox4.TabIndex = 14;
            // 
            // psTextoBox5
            // 
            this.psTextoBox5.Caption = "CENQ";
            this.psTextoBox5.DataField = "CENQ";
            this.psTextoBox5.Edita = true;
            this.psTextoBox5.Location = new System.Drawing.Point(318, 92);
            this.psTextoBox5.MaxLength = 3;
            this.psTextoBox5.Name = "psTextoBox5";
            this.psTextoBox5.PasswordChar = '\0';
            this.psTextoBox5.Size = new System.Drawing.Size(125, 37);
            this.psTextoBox5.TabIndex = 15;
            // 
            // psComboBox1
            // 
            this.psComboBox1.Caption = "TIPOTRIBUTACAO";
            this.psComboBox1.Chave = true;
            this.psComboBox1.DataField = "TIPOTRIBUTACAO";
            this.psComboBox1.DataSource = null;
            this.psComboBox1.DisplayMember = "";
            this.psComboBox1.Location = new System.Drawing.Point(11, 135);
            this.psComboBox1.Name = "psComboBox1";
            this.psComboBox1.SelectedIndex = -1;
            this.psComboBox1.Size = new System.Drawing.Size(145, 37);
            this.psComboBox1.TabIndex = 16;
            this.psComboBox1.Value = null;
            this.psComboBox1.ValueMember = "";
            // 
            // PSPartRegraIPIEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartRegraIPIEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartRegraIPIEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            //this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Lib.WinForms.PSTextoBox psTextoBox1;
        private Lib.WinForms.PSCheckBox psCheckBox1;
        private Lib.WinForms.PSTextoBox psTextoBox2;
        private Lib.WinForms.PSTextoBox psTextoBox4;
        private Lib.WinForms.PSTextoBox psTextoBox3;
        private Lib.WinForms.PSTextoBox psTextoBox5;
        private Lib.WinForms.PSComboBox psComboBox1;
    }
}