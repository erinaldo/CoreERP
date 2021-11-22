namespace PS.Glb
{
    partial class PSPartOperMensagemEdit
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
            this.psMemoBox1 = new PS.Lib.WinForms.PSMemoBox();
            this.psCheckBox1 = new PS.Lib.WinForms.PSCheckBox();
            this.psTextoBox2 = new PS.Lib.WinForms.PSTextoBox();
            this.psLookup19 = new PS.Lib.WinForms.PSLookup();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(647, 340);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.psLookup19);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psCheckBox1);
            this.tabPage1.Controls.Add(this.psMemoBox1);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(639, 314);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODMENSAGEM";
            this.psTextoBox1.DataField = "CODMENSAGEM";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 25;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 0;
            // 
            // psMemoBox1
            // 
            this.psMemoBox1.Caption = "MENSAGEM";
            this.psMemoBox1.Chave = true;
            this.psMemoBox1.DataField = "MENSAGEM";
            this.psMemoBox1.Location = new System.Drawing.Point(11, 92);
            this.psMemoBox1.Name = "psMemoBox1";
            this.psMemoBox1.Size = new System.Drawing.Size(401, 125);
            this.psMemoBox1.TabIndex = 3;
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
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "DESCRICAO";
            this.psTextoBox2.DataField = "DESCRICAO";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.Location = new System.Drawing.Point(11, 49);
            this.psTextoBox2.MaxLength = 80;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox2.TabIndex = 2;
            // 
            // psLookup19
            // 
            this.psLookup19.Caption = "CODFORMULAMENSAGEM";
            this.psLookup19.Chave = true;
            this.psLookup19.DataField = "CODFORMULAMENSAGEM";
            this.psLookup19.Description = "";
            this.psLookup19.DinamicTable = null;
            this.psLookup19.KeyField = "CODFORMULA";
            this.psLookup19.Location = new System.Drawing.Point(11, 223);
            this.psLookup19.LookupField = "CODFORMULA;DESCRICAO";
            this.psLookup19.LookupFieldResult = "CODFORMULA;DESCRICAO";
            this.psLookup19.MaxLength = 15;
            this.psLookup19.Name = "psLookup19";
            this.psLookup19.PSPart = null;
            this.psLookup19.Size = new System.Drawing.Size(401, 38);
            this.psLookup19.TabIndex = 40;
            this.psLookup19.ValorRetorno = null;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 264);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(295, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "Ao informar uma fórmula o campo Mensagem sera ignorado.";
            // 
            // PSPartOperMensagemEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 456);
            this.Name = "PSPartOperMensagemEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartOperMensagemEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSMemoBox psMemoBox1;
        private PS.Lib.WinForms.PSCheckBox psCheckBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private PS.Lib.WinForms.PSLookup psLookup19;
        private System.Windows.Forms.Label label2;
    }
}