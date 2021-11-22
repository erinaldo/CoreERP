namespace PS.Glb
{
    partial class PSPartRegCaixaEdit
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
            this.psMoedaBox1 = new PS.Lib.WinForms.PSMoedaBox();
            this.psDateBox2 = new PS.Lib.WinForms.PSDateBox();
            this.psMoedaBox2 = new PS.Lib.WinForms.PSMoedaBox();
            this.psLookup2 = new PS.Lib.WinForms.PSLookup();
            this.psLookup3 = new PS.Lib.WinForms.PSLookup();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(647, 273);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psLookup3);
            this.tabPage1.Controls.Add(this.psLookup2);
            this.tabPage1.Controls.Add(this.psMoedaBox2);
            this.tabPage1.Controls.Add(this.psDateBox2);
            this.tabPage1.Controls.Add(this.psMoedaBox1);
            this.tabPage1.Controls.Add(this.psDateBox1);
            this.tabPage1.Controls.Add(this.psLookup1);
            this.tabPage1.Size = new System.Drawing.Size(639, 247);
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODCAIXA";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODCAIXA";
            this.psLookup1.Description = "";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.KeyField = "CODCAIXA";
            this.psLookup1.Location = new System.Drawing.Point(11, 50);
            this.psLookup1.LookupField = "CODCAIXA;DESCRICAO";
            this.psLookup1.LookupFieldResult = "CODCAIXA;DESCRICAO";
            this.psLookup1.MaxLength = 15;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(401, 38);
            this.psLookup1.TabIndex = 1;
            this.psLookup1.ValorRetorno = null;
            // 
            // psDateBox1
            // 
            this.psDateBox1.Caption = "DATAABERTURA";
            this.psDateBox1.Chave = true;
            this.psDateBox1.DataField = "DATAABERTURA";
            this.psDateBox1.Location = new System.Drawing.Point(11, 94);
            this.psDateBox1.Mascara = "00/00/0000 00:00";
            this.psDateBox1.MaxLength = 32767;
            this.psDateBox1.Name = "psDateBox1";
            this.psDateBox1.Size = new System.Drawing.Size(146, 37);
            this.psDateBox1.TabIndex = 2;
            this.psDateBox1.Value = new System.DateTime(2016, 2, 20, 12, 50, 11, 296);
            // 
            // psMoedaBox1
            // 
            this.psMoedaBox1.Caption = "VALORABERTURA";
            this.psMoedaBox1.CasasDecimais = 2;
            this.psMoedaBox1.DataField = "VALORABERTURA";
            this.psMoedaBox1.Edita = true;
            this.psMoedaBox1.Location = new System.Drawing.Point(163, 94);
            this.psMoedaBox1.Name = "psMoedaBox1";
            this.psMoedaBox1.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox1.TabIndex = 3;
            // 
            // psDateBox2
            // 
            this.psDateBox2.Caption = "DATAFECHAMENTO";
            this.psDateBox2.Chave = true;
            this.psDateBox2.DataField = "DATAFECHAMENTO";
            this.psDateBox2.Location = new System.Drawing.Point(11, 137);
            this.psDateBox2.Mascara = "00/00/0000 00:00";
            this.psDateBox2.MaxLength = 32767;
            this.psDateBox2.Name = "psDateBox2";
            this.psDateBox2.Size = new System.Drawing.Size(146, 37);
            this.psDateBox2.TabIndex = 4;
            this.psDateBox2.Value = new System.DateTime(2016, 2, 20, 12, 50, 11, 311);
            // 
            // psMoedaBox2
            // 
            this.psMoedaBox2.Caption = "VALORFECHAMENTO";
            this.psMoedaBox2.CasasDecimais = 2;
            this.psMoedaBox2.DataField = "VALORFECHAMENTO";
            this.psMoedaBox2.Edita = true;
            this.psMoedaBox2.Location = new System.Drawing.Point(163, 137);
            this.psMoedaBox2.Name = "psMoedaBox2";
            this.psMoedaBox2.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox2.TabIndex = 5;
            // 
            // psLookup2
            // 
            this.psLookup2.Caption = "CODOPERADOR";
            this.psLookup2.Chave = true;
            this.psLookup2.DataField = "CODOPERADOR";
            this.psLookup2.Description = "";
            this.psLookup2.DinamicTable = null;
            this.psLookup2.KeyField = "CODOPERADOR";
            this.psLookup2.Location = new System.Drawing.Point(11, 180);
            this.psLookup2.LookupField = "CODOPERADOR;NOME";
            this.psLookup2.LookupFieldResult = "CODOPERADOR;NOME";
            this.psLookup2.MaxLength = 15;
            this.psLookup2.Name = "psLookup2";
            this.psLookup2.PSPart = null;
            this.psLookup2.Size = new System.Drawing.Size(401, 38);
            this.psLookup2.TabIndex = 6;
            this.psLookup2.ValorRetorno = null;
            // 
            // psLookup3
            // 
            this.psLookup3.Caption = "CODFILIAL";
            this.psLookup3.Chave = true;
            this.psLookup3.DataField = "CODFILIAL";
            this.psLookup3.Description = "";
            this.psLookup3.DinamicTable = null;
            this.psLookup3.KeyField = "CODFILIAL";
            this.psLookup3.Location = new System.Drawing.Point(11, 6);
            this.psLookup3.LookupField = "CODFILIAL;NOME;NOMEFANTASIA";
            this.psLookup3.LookupFieldResult = "CODFILIAL;NOME";
            this.psLookup3.MaxLength = 32767;
            this.psLookup3.Name = "psLookup3";
            this.psLookup3.PSPart = null;
            this.psLookup3.Size = new System.Drawing.Size(401, 38);
            this.psLookup3.TabIndex = 0;
            this.psLookup3.ValorRetorno = null;
            // 
            // PSPartRegCaixaEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 389);
            this.Name = "PSPartRegCaixaEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartRegCaixaEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSLookup psLookup1;
        private PS.Lib.WinForms.PSDateBox psDateBox1;
        private PS.Lib.WinForms.PSLookup psLookup2;
        private PS.Lib.WinForms.PSMoedaBox psMoedaBox2;
        private PS.Lib.WinForms.PSDateBox psDateBox2;
        private PS.Lib.WinForms.PSMoedaBox psMoedaBox1;
        private PS.Lib.WinForms.PSLookup psLookup3;
    }
}