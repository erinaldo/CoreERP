namespace PS.Glb
{
    partial class PSPartProdutoTributoEstadoEdit
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
            this.psLookup4 = new PS.Lib.WinForms.PSLookup();
            this.psMoedaBox1 = new PS.Lib.WinForms.PSMoedaBox();
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
            this.tabPage1.Controls.Add(this.psMoedaBox1);
            this.tabPage1.Controls.Add(this.psLookup4);
            this.tabPage1.Controls.Add(this.psLookup1);
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODTRIBUTO";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODTRIBUTO";
            this.psLookup1.KeyField = "CODTRIBUTO";
            this.psLookup1.Location = new System.Drawing.Point(11, 6);
            this.psLookup1.LookupField = "CODTRIBUTO;DESCRICAO";
            this.psLookup1.LookupFieldResult = "CODTRIBUTO;DESCRICAO";
            this.psLookup1.MaxLength = 15;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(401, 38);
            this.psLookup1.TabIndex = 6;
            this.psLookup1.ValorRetorno = null;
            // 
            // psLookup4
            // 
            this.psLookup4.Caption = "CODESTADO";
            this.psLookup4.Chave = true;
            this.psLookup4.DataField = "CODESTADO";
            this.psLookup4.KeyField = "CODETD";
            this.psLookup4.Location = new System.Drawing.Point(11, 50);
            this.psLookup4.LookupField = "CODETD;NOME";
            this.psLookup4.LookupFieldResult = "CODETD;NOME";
            this.psLookup4.MaxLength = 2;
            this.psLookup4.Name = "psLookup4";
            this.psLookup4.PSPart = null;
            this.psLookup4.Size = new System.Drawing.Size(401, 38);
            this.psLookup4.TabIndex = 29;
            this.psLookup4.ValorRetorno = null;
            // 
            // psMoedaBox1
            // 
            this.psMoedaBox1.Caption = "ALIQUOTA";
            this.psMoedaBox1.CasasDecimais = 2;
            this.psMoedaBox1.Edita = true;
            this.psMoedaBox1.DataField = "ALIQUOTA";
            this.psMoedaBox1.Location = new System.Drawing.Point(11, 94);
            this.psMoedaBox1.Name = "psMoedaBox1";
            this.psMoedaBox1.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox1.TabIndex = 30;
            // 
            // PSPartProdutoTributoEstadoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartProdutoTributoEstadoEdit";
            this.PermiteEditar = true;
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartProdutoTributoEstadoEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSLookup psLookup1;
        private PS.Lib.WinForms.PSMoedaBox psMoedaBox1;
        private PS.Lib.WinForms.PSLookup psLookup4;
    }
}