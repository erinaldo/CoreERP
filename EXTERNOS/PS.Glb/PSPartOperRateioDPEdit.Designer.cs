namespace PS.Glb
{
    partial class PSPartOperRateioDPEdit
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
            this.psLookup7 = new PS.Lib.WinForms.PSLookup();
            this.psMoedaBox1 = new PS.Lib.WinForms.PSMoedaBox();
            this.psMoedaBox2 = new PS.Lib.WinForms.PSMoedaBox();
            this.psLookup2 = new PS.Lib.WinForms.PSLookup();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psLookup2);
            this.tabPage1.Controls.Add(this.psMoedaBox2);
            this.tabPage1.Controls.Add(this.psMoedaBox1);
            this.tabPage1.Controls.Add(this.psLookup7);
            // 
            // psLookup7
            // 
            this.psLookup7.Caption = "CODDEPTO";
            this.psLookup7.Chave = true;
            this.psLookup7.DataField = "CODDEPTO";
            this.psLookup7.Description = "";
            this.psLookup7.DinamicTable = null;
            this.psLookup7.KeyField = "CODDEPTO";
            this.psLookup7.Location = new System.Drawing.Point(11, 50);
            this.psLookup7.LookupField = "CODDEPTO;NOME";
            this.psLookup7.LookupFieldResult = "CODDEPTO;NOME";
            this.psLookup7.MaxLength = 15;
            this.psLookup7.Name = "psLookup7";
            this.psLookup7.PSPart = null;
            this.psLookup7.Size = new System.Drawing.Size(401, 38);
            this.psLookup7.TabIndex = 2;
            this.psLookup7.ValorRetorno = null;
            // 
            // psMoedaBox1
            // 
            this.psMoedaBox1.Caption = "PERCENTUAL";
            this.psMoedaBox1.CasasDecimais = 2;
            this.psMoedaBox1.Edita = true;
            this.psMoedaBox1.DataField = "PERCENTUAL";
            this.psMoedaBox1.Location = new System.Drawing.Point(11, 94);
            this.psMoedaBox1.Name = "psMoedaBox1";
            this.psMoedaBox1.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox1.TabIndex = 3;
            // 
            // psMoedaBox2
            // 
            this.psMoedaBox2.Caption = "VALOR";
            this.psMoedaBox2.CasasDecimais = 2;
            this.psMoedaBox2.Edita = true;
            this.psMoedaBox2.DataField = "VALOR";
            this.psMoedaBox2.Location = new System.Drawing.Point(267, 94);
            this.psMoedaBox2.Name = "psMoedaBox2";
            this.psMoedaBox2.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox2.TabIndex = 4;
            // 
            // psLookup2
            // 
            this.psLookup2.Caption = "CODFILIAL";
            this.psLookup2.Chave = true;
            this.psLookup2.DataField = "CODFILIAL";
            this.psLookup2.Description = "";
            this.psLookup2.DinamicTable = null;
            this.psLookup2.KeyField = "CODFILIAL";
            this.psLookup2.Location = new System.Drawing.Point(11, 6);
            this.psLookup2.LookupField = "CODFILIAL;NOME;NOMEFANTASIA";
            this.psLookup2.LookupFieldResult = "CODFILIAL;NOMEFANTASIA";
            this.psLookup2.MaxLength = 32767;
            this.psLookup2.Name = "psLookup2";
            this.psLookup2.PSPart = null;
            this.psLookup2.Size = new System.Drawing.Size(401, 38);
            this.psLookup2.TabIndex = 8;
            this.psLookup2.ValorRetorno = null;
            // 
            // PSPartOperRateioDPEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartOperRateioDPEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.PermiteSavar = true;
            this.Text = "PSPartOperRateioDPEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSLookup psLookup7;
        private PS.Lib.WinForms.PSMoedaBox psMoedaBox2;
        private PS.Lib.WinForms.PSMoedaBox psMoedaBox1;
        private Lib.WinForms.PSLookup psLookup2;
    }
}