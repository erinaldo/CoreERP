namespace PS.Glb
{
    partial class PSPartAcessoRelatorioEdit
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
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(611, 259);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psLookup1);
            this.tabPage1.Size = new System.Drawing.Size(603, 233);
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODPERFIL";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODPERFIL";
            this.psLookup1.Description = "";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.KeyField = "CODPERFIL";
            this.psLookup1.Location = new System.Drawing.Point(11, 19);
            this.psLookup1.LookupField = "CODPERFIL;NOME";
            this.psLookup1.LookupFieldResult = "CODPERFIL;NOME";
            this.psLookup1.MaxLength = 25;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(401, 38);
            this.psLookup1.TabIndex = 1;
            this.psLookup1.ValorRetorno = null;
            // 
            // PSPartAcessoRelatorioEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 375);
            this.Name = "PSPartAcessoRelatorioEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartAcessoRelatorioEdit";
            this.Load += new System.EventHandler(this.PSPartAcessoRelatorioEdit_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Lib.WinForms.PSLookup psLookup1;
    }
}