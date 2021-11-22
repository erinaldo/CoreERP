namespace PS.Glb
{
    partial class PSPartRegiaoEstadoEdit
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
            this.psLookup4 = new PS.Lib.WinForms.PSLookup();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psLookup4);
            // 
            // psLookup4
            // 
            this.psLookup4.Caption = "CODETD";
            this.psLookup4.Chave = true;
            this.psLookup4.DataField = "CODETD";
            this.psLookup4.DinamicTable = null;
            this.psLookup4.KeyField = "CODETD";
            this.psLookup4.Location = new System.Drawing.Point(11, 6);
            this.psLookup4.LookupField = "CODETD;NOME";
            this.psLookup4.LookupFieldResult = "CODETD;NOME";
            this.psLookup4.MaxLength = 2;
            this.psLookup4.Name = "psLookup4";
            this.psLookup4.PSPart = null;
            this.psLookup4.Size = new System.Drawing.Size(401, 38);
            this.psLookup4.TabIndex = 30;
            this.psLookup4.ValorRetorno = null;
            // 
            // PSPartRegiaoEstadoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartRegiaoEstadoEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.PermiteSavar = true;
            this.Text = "PSPartRegiaoEstadoEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Lib.WinForms.PSLookup psLookup4;
    }
}