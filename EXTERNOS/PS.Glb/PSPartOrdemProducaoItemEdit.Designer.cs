namespace PS.Glb
{
    partial class PSPartOrdemProducaoItemEdit
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
            this.psLookup1 = new PS.Lib.WinForms.PSLookup();
            this.psTextoBox1 = new PS.Lib.WinForms.PSTextoBox();
            this.psLookup2 = new PS.Lib.WinForms.PSLookup();
            this.psMoedaBox1 = new PS.Lib.WinForms.PSMoedaBox();
            this.psLookup6 = new PS.Lib.WinForms.PSLookup();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(647, 280);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.psLookup6);
            this.tabPage1.Controls.Add(this.psMoedaBox1);
            this.tabPage1.Controls.Add(this.psLookup2);
            this.tabPage1.Controls.Add(this.psLookup1);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(639, 254);
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODPRODUTO";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODPRODUTO";
            this.psLookup1.Description = "";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.KeyField = "CODPRODUTO";
            this.psLookup1.Location = new System.Drawing.Point(11, 93);
            this.psLookup1.LookupField = "CODPRODUTO;NOME";
            this.psLookup1.LookupFieldResult = "CODPRODUTO;NOME";
            this.psLookup1.MaxLength = 15;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(401, 38);
            this.psLookup1.TabIndex = 3;
            this.psLookup1.ValorRetorno = null;
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.AutoIncremento = PS.Lib.Global.TypeAutoinc.Max;
            this.psTextoBox1.Caption = "NSEQITEM";
            this.psTextoBox1.DataField = "NSEQITEM";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 32767;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 2;
            // 
            // psLookup2
            // 
            this.psLookup2.Caption = "CODESTRUTURA";
            this.psLookup2.Chave = true;
            this.psLookup2.DataField = "CODESTRUTURA";
            this.psLookup2.Description = "";
            this.psLookup2.DinamicTable = null;
            this.psLookup2.KeyField = "CODESTRUTURA";
            this.psLookup2.Location = new System.Drawing.Point(11, 49);
            this.psLookup2.LookupField = "CODESTRUTURA;NOME;CODPRODUTO;CPRODUTO";
            this.psLookup2.LookupFieldResult = "CODESTRUTURA;NOME";
            this.psLookup2.MaxLength = 15;
            this.psLookup2.Name = "psLookup2";
            this.psLookup2.PSPart = null;
            this.psLookup2.Size = new System.Drawing.Size(401, 38);
            this.psLookup2.TabIndex = 4;
            this.psLookup2.ValorRetorno = null;
            this.psLookup2.AfterLookup += new PS.Lib.WinForms.PSLookup.AfterLookupDelegate(this.psLookup2_AfterLookup);
            // 
            // psMoedaBox1
            // 
            this.psMoedaBox1.Caption = "QUANTIDADE";
            this.psMoedaBox1.CasasDecimais = 9;
            this.psMoedaBox1.DataField = "QUANTIDADE";
            this.psMoedaBox1.Edita = true;
            this.psMoedaBox1.Location = new System.Drawing.Point(11, 184);
            this.psMoedaBox1.Name = "psMoedaBox1";
            this.psMoedaBox1.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox1.TabIndex = 5;
            // 
            // psLookup6
            // 
            this.psLookup6.Caption = "";
            this.psLookup6.Chave = true;
            this.psLookup6.DataField = "";
            this.psLookup6.Description = "";
            this.psLookup6.DinamicTable = null;
            this.psLookup6.KeyField = "CODUNID";
            this.psLookup6.Location = new System.Drawing.Point(11, 137);
            this.psLookup6.LookupField = "CODUNID;NOME";
            this.psLookup6.LookupFieldResult = "CODUNID;NOME";
            this.psLookup6.MaxLength = 5;
            this.psLookup6.Name = "psLookup6";
            this.psLookup6.PSPart = null;
            this.psLookup6.Size = new System.Drawing.Size(401, 38);
            this.psLookup6.TabIndex = 19;
            this.psLookup6.ValorRetorno = null;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Unidade de Controle";
            // 
            // PSPartOrdemProducaoItemEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 396);
            this.Name = "PSPartOrdemProducaoItemEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartOrdemProducaoItemEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSLookup psLookup1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSLookup psLookup2;
        private PS.Lib.WinForms.PSMoedaBox psMoedaBox1;
        private PS.Lib.WinForms.PSLookup psLookup6;
        private System.Windows.Forms.Label label3;
    }
}