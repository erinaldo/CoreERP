namespace PS.Glb
{
    partial class PSPartOperacaoItemRecursoEdit
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
            this.psDateBox1 = new PS.Lib.WinForms.PSDateBox();
            this.psDateBox2 = new PS.Lib.WinForms.PSDateBox();
            this.psDateBox3 = new PS.Lib.WinForms.PSDateBox();
            this.psDateBox4 = new PS.Lib.WinForms.PSDateBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psDateBox4);
            this.tabPage1.Controls.Add(this.psDateBox3);
            this.tabPage1.Controls.Add(this.psDateBox2);
            this.tabPage1.Controls.Add(this.psDateBox1);
            this.tabPage1.Controls.Add(this.psLookup1);
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODOPERADOR";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODOPERADOR";
            this.psLookup1.KeyField = "CODOPERADOR";
            this.psLookup1.Location = new System.Drawing.Point(11, 6);
            this.psLookup1.LookupField = "CODOPERADOR;NOME";
            this.psLookup1.LookupFieldResult = "CODOPERADOR;NOME";
            this.psLookup1.MaxLength = 15;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(401, 38);
            this.psLookup1.TabIndex = 0;
            this.psLookup1.ValorRetorno = null;
            // 
            // psDateBox1
            // 
            this.psDateBox1.Caption = "DATAPRVINICIAL";
            this.psDateBox1.Chave = true;
            this.psDateBox1.DataField = "DATAPRVINICIAL";
            this.psDateBox1.Location = new System.Drawing.Point(11, 50);
            this.psDateBox1.Mascara = "00/00/0000 00:00";
            this.psDateBox1.Name = "psDateBox1";
            this.psDateBox1.Size = new System.Drawing.Size(146, 37);
            this.psDateBox1.TabIndex = 1;
            // 
            // psDateBox2
            // 
            this.psDateBox2.Caption = "DATAPRVFINAL";
            this.psDateBox2.Chave = true;
            this.psDateBox2.DataField = "DATAPRVFINAL";
            this.psDateBox2.Location = new System.Drawing.Point(163, 50);
            this.psDateBox2.Mascara = "00/00/0000 00:00";
            this.psDateBox2.Name = "psDateBox2";
            this.psDateBox2.Size = new System.Drawing.Size(146, 37);
            this.psDateBox2.TabIndex = 2;
            // 
            // psDateBox3
            // 
            this.psDateBox3.Caption = "DATAREAINICIAL";
            this.psDateBox3.Chave = true;
            this.psDateBox3.DataField = "DATAREAINICIAL";
            this.psDateBox3.Location = new System.Drawing.Point(11, 93);
            this.psDateBox3.Mascara = "00/00/0000 00:00";
            this.psDateBox3.Name = "psDateBox3";
            this.psDateBox3.Size = new System.Drawing.Size(146, 37);
            this.psDateBox3.TabIndex = 3;
            // 
            // psDateBox4
            // 
            this.psDateBox4.Caption = "DATAREAFINAL";
            this.psDateBox4.Chave = true;
            this.psDateBox4.DataField = "DATAREAFINAL";
            this.psDateBox4.Location = new System.Drawing.Point(163, 93);
            this.psDateBox4.Mascara = "00/00/0000 00:00";
            this.psDateBox4.Name = "psDateBox4";
            this.psDateBox4.Size = new System.Drawing.Size(146, 37);
            this.psDateBox4.TabIndex = 4;
            // 
            // PSPartOperacaoItemRecursoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartOperacaoItemRecursoEdit";
            this.PermiteEditar = true;
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartOperacaoItemRecursoEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSLookup psLookup1;
        private PS.Lib.WinForms.PSDateBox psDateBox4;
        private PS.Lib.WinForms.PSDateBox psDateBox3;
        private PS.Lib.WinForms.PSDateBox psDateBox2;
        private PS.Lib.WinForms.PSDateBox psDateBox1;
    }
}