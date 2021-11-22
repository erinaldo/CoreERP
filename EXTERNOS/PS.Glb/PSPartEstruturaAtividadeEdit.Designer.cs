namespace PS.Glb
{
    partial class PSPartEstruturaAtividadeEdit
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
            this.psMoedaBox1 = new PS.Lib.WinForms.PSMoedaBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psMoedaBox1);
            this.tabPage1.Controls.Add(this.psLookup1);
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODATIVIDADE";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODATIVIDADE";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.KeyField = "CODATIVIDADE";
            this.psLookup1.Location = new System.Drawing.Point(11, 6);
            this.psLookup1.LookupField = "CODATIVIDADE;NOME";
            this.psLookup1.LookupFieldResult = "CODATIVIDADE;NOME";
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(401, 38);
            this.psLookup1.TabIndex = 0;
            this.psLookup1.ValorRetorno = null;
            // 
            // psMoedaBox1
            // 
            this.psMoedaBox1.Caption = "TEMPOGASTO";
            this.psMoedaBox1.Edita = true;
            this.psMoedaBox1.DataField = "TEMPOGASTO";
            this.psMoedaBox1.Location = new System.Drawing.Point(11, 50);
            this.psMoedaBox1.Name = "psMoedaBox1";
            this.psMoedaBox1.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox1.TabIndex = 1;
            // 
            // PSPartEstruturaAtividadeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartEstruturaAtividadeEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.PermiteSavar = true;
            this.Text = "PSPartEstruturaAtividadeEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSLookup psLookup1;
        private PS.Lib.WinForms.PSMoedaBox psMoedaBox1;
    }
}