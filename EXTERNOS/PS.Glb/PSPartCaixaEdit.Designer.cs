namespace PS.Glb
{
    partial class PSPartCaixaEdit
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
            this.psLookup1 = new PS.Lib.WinForms.PSLookup();
            this.psLookup2 = new PS.Lib.WinForms.PSLookup();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.psLookup3 = new PS.Lib.WinForms.PSLookup();
            this.psCheckBox4 = new PS.Lib.WinForms.PSCheckBox();
            this.psTextoBox7 = new PS.Lib.WinForms.PSTextoBox();
            this.psCheckBox5 = new PS.Lib.WinForms.PSCheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.psCheckBox3 = new PS.Lib.WinForms.PSCheckBox();
            this.psCheckBox2 = new PS.Lib.WinForms.PSCheckBox();
            this.psCheckBox1 = new PS.Lib.WinForms.PSCheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.SetChildIndex(this.tabPage2, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPage1, 0);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psLookup2);
            this.tabPage1.Controls.Add(this.psLookup1);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODCAIXA";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.DataField = "CODCAIXA";
            this.psTextoBox1.Location = new System.Drawing.Point(11, 50);
            this.psTextoBox1.MaxLength = 15;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 1;
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "DESCRICAO";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.DataField = "DESCRICAO";
            this.psTextoBox2.Location = new System.Drawing.Point(11, 93);
            this.psTextoBox2.MaxLength = 30;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox2.TabIndex = 2;
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODFILIAL";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODFILIAL";
            this.psLookup1.KeyField = "CODFILIAL";
            this.psLookup1.Location = new System.Drawing.Point(11, 6);
            this.psLookup1.LookupField = "CODFILIAL;NOME;NOMEFANTASIA";
            this.psLookup1.LookupFieldResult = "CODFILIAL;NOME";
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(401, 38);
            this.psLookup1.TabIndex = 0;
            this.psLookup1.ValorRetorno = null;
            // 
            // psLookup2
            // 
            this.psLookup2.Caption = "CODIMPRESSORA";
            this.psLookup2.Chave = true;
            this.psLookup2.DataField = "CODIMPRESSORA";
            this.psLookup2.KeyField = "CODIMPRESSORA";
            this.psLookup2.Location = new System.Drawing.Point(11, 136);
            this.psLookup2.LookupField = "CODIMPRESSORA;DESCRICAO;MARCA;MODELO";
            this.psLookup2.LookupFieldResult = "CODIMPRESSORA;DESCRICAO";
            this.psLookup2.MaxLength = 15;
            this.psLookup2.Name = "psLookup2";
            this.psLookup2.PSPart = null;
            this.psLookup2.Size = new System.Drawing.Size(401, 38);
            this.psLookup2.TabIndex = 3;
            this.psLookup2.ValorRetorno = null;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.psLookup3);
            this.tabPage2.Controls.Add(this.psCheckBox4);
            this.tabPage2.Controls.Add(this.psTextoBox7);
            this.tabPage2.Controls.Add(this.psCheckBox5);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(639, 286);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Parâmetros";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // psLookup3
            // 
            this.psLookup3.Caption = "CODTIPOPERPDV";
            this.psLookup3.Chave = true;
            this.psLookup3.DataField = "CODTIPOPERPDV";
            this.psLookup3.KeyField = "CODTIPOPER";
            this.psLookup3.Location = new System.Drawing.Point(11, 132);
            this.psLookup3.LookupField = "CODTIPOPER;DESCRICAO";
            this.psLookup3.LookupFieldResult = "CODTIPOPER;DESCRICAO";
            this.psLookup3.MaxLength = 15;
            this.psLookup3.Name = "psLookup3";
            this.psLookup3.PSPart = null;
            this.psLookup3.Size = new System.Drawing.Size(401, 38);
            this.psLookup3.TabIndex = 3;
            this.psLookup3.ValorRetorno = null;
            // 
            // psCheckBox4
            // 
            this.psCheckBox4.Caption = "SOLCLIPDV";
            this.psCheckBox4.Chave = true;
            this.psCheckBox4.Checked = false;
            this.psCheckBox4.DataField = "SOLCLIPDV";
            this.psCheckBox4.Location = new System.Drawing.Point(11, 49);
            this.psCheckBox4.Name = "psCheckBox4";
            this.psCheckBox4.Size = new System.Drawing.Size(242, 22);
            this.psCheckBox4.TabIndex = 2;
            this.psCheckBox4.CheckedChanged += new PS.Lib.WinForms.PSCheckBox.CheckedChangedDelegate(this.psCheckBox4_CheckedChanged);
            // 
            // psTextoBox7
            // 
            this.psTextoBox7.Caption = "TIMEOUTPDV";
            this.psTextoBox7.Edita = true;
            this.psTextoBox7.DataField = "TIMEOUTPDV";
            this.psTextoBox7.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox7.Name = "psTextoBox7";
            this.psTextoBox7.PasswordChar = '\0';
            this.psTextoBox7.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox7.TabIndex = 0;
            // 
            // psCheckBox5
            // 
            this.psCheckBox5.Caption = "MOSTRAPRODUTOPDV";
            this.psCheckBox5.Chave = true;
            this.psCheckBox5.Checked = false;
            this.psCheckBox5.DataField = "MOSTRAPRODUTOPDV";
            this.psCheckBox5.Location = new System.Drawing.Point(166, 21);
            this.psCheckBox5.Name = "psCheckBox5";
            this.psCheckBox5.Size = new System.Drawing.Size(227, 22);
            this.psCheckBox5.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.psCheckBox3);
            this.groupBox1.Controls.Add(this.psCheckBox2);
            this.groupBox1.Controls.Add(this.psCheckBox1);
            this.groupBox1.Location = new System.Drawing.Point(6, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(628, 49);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dados do Cliente";
            // 
            // psCheckBox3
            // 
            this.psCheckBox3.Caption = "USAENDERECO";
            this.psCheckBox3.Chave = true;
            this.psCheckBox3.Checked = false;
            this.psCheckBox3.DataField = "USAENDERECO";
            this.psCheckBox3.Location = new System.Drawing.Point(317, 19);
            this.psCheckBox3.Name = "psCheckBox3";
            this.psCheckBox3.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox3.TabIndex = 2;
            // 
            // psCheckBox2
            // 
            this.psCheckBox2.Caption = "USANOME";
            this.psCheckBox2.Chave = true;
            this.psCheckBox2.Checked = false;
            this.psCheckBox2.DataField = "USANOME";
            this.psCheckBox2.Location = new System.Drawing.Point(161, 19);
            this.psCheckBox2.Name = "psCheckBox2";
            this.psCheckBox2.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox2.TabIndex = 1;
            // 
            // psCheckBox1
            // 
            this.psCheckBox1.Caption = "USACGCCPF";
            this.psCheckBox1.Chave = true;
            this.psCheckBox1.Checked = false;
            this.psCheckBox1.DataField = "USACGCCPF";
            this.psCheckBox1.Location = new System.Drawing.Point(6, 19);
            this.psCheckBox1.Name = "psCheckBox1";
            this.psCheckBox1.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox1.TabIndex = 0;
            // 
            // PSPartCaixaEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartCaixaEdit";
            this.PermiteEditar = true;
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartCaixaEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private PS.Lib.WinForms.PSLookup psLookup1;
        private PS.Lib.WinForms.PSLookup psLookup2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private PS.Lib.WinForms.PSCheckBox psCheckBox3;
        private PS.Lib.WinForms.PSCheckBox psCheckBox2;
        private PS.Lib.WinForms.PSCheckBox psCheckBox1;
        private PS.Lib.WinForms.PSCheckBox psCheckBox4;
        private PS.Lib.WinForms.PSTextoBox psTextoBox7;
        private PS.Lib.WinForms.PSCheckBox psCheckBox5;
        private PS.Lib.WinForms.PSLookup psLookup3;
    }
}