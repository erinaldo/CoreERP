namespace PS.Glb
{
    partial class PSPartUnidadeEdit
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
            this.psLookup2 = new PS.Lib.WinForms.PSLookup();
            this.psMoedaBox1 = new PS.Lib.WinForms.PSMoedaBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psMoedaBox1);
            this.tabPage1.Controls.Add(this.psLookup2);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODUNID";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.DataField = "CODUNID";
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
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.DataField = "NOME";
            this.psTextoBox2.Location = new System.Drawing.Point(11, 49);
            this.psTextoBox2.MaxLength = 15;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox2.TabIndex = 1;
            // 
            // psLookup2
            // 
            this.psLookup2.Caption = "CODUNIDBASE";
            this.psLookup2.Chave = true;
            this.psLookup2.DataField = "CODUNIDBASE";
            this.psLookup2.KeyField = "CODUNID";
            this.psLookup2.Location = new System.Drawing.Point(11, 92);
            this.psLookup2.LookupField = "CODUNID;NOME";
            this.psLookup2.LookupFieldResult = "CODUNID;NOME";
            this.psLookup2.MaxLength = 5;
            this.psLookup2.Name = "psLookup2";
            this.psLookup2.PSPart = null;
            this.psLookup2.Size = new System.Drawing.Size(401, 38);
            this.psLookup2.TabIndex = 2;
            this.psLookup2.ValorRetorno = null;
            // 
            // psMoedaBox1
            // 
            this.psMoedaBox1.Caption = "FATORCONVERSAO";
            this.psMoedaBox1.CasasDecimais = 4;
            this.psMoedaBox1.Edita = true;
            this.psMoedaBox1.DataField = "FATORCONVERSAO";
            this.psMoedaBox1.Location = new System.Drawing.Point(418, 93);
            this.psMoedaBox1.Name = "psMoedaBox1";
            this.psMoedaBox1.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox1.TabIndex = 3;
            // 
            // PSPartUnidadeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartUnidadeEdit";
            this.PermiteEditar = true;
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartUnidadeEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private PS.Lib.WinForms.PSMoedaBox psMoedaBox1;
        private PS.Lib.WinForms.PSLookup psLookup2;
    }
}