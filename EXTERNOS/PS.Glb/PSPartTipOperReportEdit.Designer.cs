namespace PS.Glb
{
    partial class PSPartTipOperReportEdit
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
            this.psLookup2 = new PS.Lib.WinForms.PSLookup();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psLookup2);
            // 
            // psLookup2
            // 
            this.psLookup2.Caption = "CODREPORT";
            this.psLookup2.Chave = true;
            this.psLookup2.DataField = "CODREPORT";
            this.psLookup2.Description = "";
            this.psLookup2.DinamicTable = null;
            this.psLookup2.KeyField = "CODREPORT";
            this.psLookup2.Location = new System.Drawing.Point(11, 6);
            this.psLookup2.LookupField = "CODREPORT;DESCRICAO";
            this.psLookup2.LookupFieldResult = "CODREPORT;DESCRICAO";
            this.psLookup2.MaxLength = 25;
            this.psLookup2.Name = "psLookup2";
            this.psLookup2.PSPart = null;
            this.psLookup2.Size = new System.Drawing.Size(401, 38);
            this.psLookup2.TabIndex = 2;
            this.psLookup2.ValorRetorno = null;
            // 
            // PSPartTipOperReportEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartTipOperReportEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.PermiteSavar = true;
            this.Text = "PSPartTipOperReportEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Lib.WinForms.PSLookup psLookup2;
    }
}