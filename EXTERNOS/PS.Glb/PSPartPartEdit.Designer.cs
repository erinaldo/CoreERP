namespace PS.Glb
{
    partial class PSPartPartEdit
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
            this.psComboBox1 = new PS.Lib.WinForms.PSComboBox();
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
            this.tabPage1.Controls.Add(this.psComboBox1);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "DESCRICAO";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.DataField = "DESCRICAO";
            this.psTextoBox1.Location = new System.Drawing.Point(11, 49);
            this.psTextoBox1.MaxLength = 80;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox1.TabIndex = 1;
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "CODPSPART";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.DataField = "CODPSPART";
            this.psTextoBox2.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox2.MaxLength = 50;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox2.TabIndex = 2;
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
            // 
            // PSPartPartEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartPartEdit";
            this.PermiteEditar = true;
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartPartEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private PS.Lib.WinForms.PSComboBox psComboBox1;
    }
}