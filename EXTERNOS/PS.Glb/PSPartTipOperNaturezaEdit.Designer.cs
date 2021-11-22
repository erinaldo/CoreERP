namespace PS.Glb
{
    partial class PSPartTipOperNaturezaEdit
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
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psLookup1);
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODNATUREZA";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODNATUREZA";
            this.psLookup1.KeyField = "CODNATUREZA";
            this.psLookup1.Location = new System.Drawing.Point(11, 6);
            this.psLookup1.LookupField = "CODNATUREZA;DESCRICAO";
            this.psLookup1.LookupFieldResult = "CODNATUREZA;DESCRICAO";
            this.psLookup1.MaxLength = 15;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(401, 38);
            this.psLookup1.TabIndex = 0;
            this.psLookup1.ValorRetorno = null;
            // 
            // PSPartTipOperNaturezaEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartTipOperNaturezaEdit";
            this.PermiteEditar = true;
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartTipOperNaturezaEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSLookup psLookup1;
    }
}