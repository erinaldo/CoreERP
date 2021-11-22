namespace PS.Glb
{
    partial class PSPartEstruturaEdit
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
            this.label2 = new System.Windows.Forms.Label();
            this.psCheckBox1 = new PS.Lib.WinForms.PSCheckBox();
            this.psLookup1 = new PS.Lib.WinForms.PSLookup();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.psLookup6 = new PS.Lib.WinForms.PSLookup();
            this.label3 = new System.Windows.Forms.Label();
            this.psTextoBox2 = new PS.Lib.WinForms.PSTextoBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(647, 351);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.psCheckBox1);
            this.tabPage1.Controls.Add(this.psLookup6);
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.psLookup1);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(639, 325);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODESTRUTURA";
            this.psTextoBox1.DataField = "CODESTRUTURA";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 32767;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Descrição do Produto";
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
            this.psCheckBox1.TabIndex = 15;
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODPRODUTO";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODPRODUTO";
            this.psLookup1.Description = "";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.KeyField = "CODPRODUTO";
            this.psLookup1.Location = new System.Drawing.Point(11, 92);
            this.psLookup1.LookupField = "CODPRODUTO;NOME";
            this.psLookup1.LookupFieldResult = "CODPRODUTO;NOME";
            this.psLookup1.MaxLength = 15;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(401, 38);
            this.psLookup1.TabIndex = 11;
            this.psLookup1.ValorRetorno = null;
            this.psLookup1.AfterLookup += new PS.Lib.WinForms.PSLookup.AfterLookupDelegate(this.psLookup1_AfterLookup);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(11, 148);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(552, 89);
            this.textBox2.TabIndex = 12;
            // 
            // psLookup6
            // 
            this.psLookup6.Caption = "";
            this.psLookup6.Chave = true;
            this.psLookup6.DataField = "";
            this.psLookup6.Description = "";
            this.psLookup6.DinamicTable = null;
            this.psLookup6.KeyField = "CODUNID";
            this.psLookup6.Location = new System.Drawing.Point(11, 243);
            this.psLookup6.LookupField = "CODUNID;NOME";
            this.psLookup6.LookupFieldResult = "CODUNID;NOME";
            this.psLookup6.MaxLength = 5;
            this.psLookup6.Name = "psLookup6";
            this.psLookup6.PSPart = null;
            this.psLookup6.Size = new System.Drawing.Size(401, 38);
            this.psLookup6.TabIndex = 14;
            this.psLookup6.ValorRetorno = null;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Unidade de Controle";
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
            this.psTextoBox2.TabIndex = 22;
            // 
            // PSPartEstruturaEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 467);
            this.Name = "PSPartEstruturaEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartEstruturaEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private System.Windows.Forms.Label label2;
        private PS.Lib.WinForms.PSCheckBox psCheckBox1;
        private PS.Lib.WinForms.PSLookup psLookup6;
        private System.Windows.Forms.TextBox textBox2;
        private PS.Lib.WinForms.PSLookup psLookup1;
        private System.Windows.Forms.Label label3;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
    }
}