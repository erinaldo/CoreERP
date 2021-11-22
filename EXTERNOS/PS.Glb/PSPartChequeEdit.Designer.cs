namespace PS.Glb
{
    partial class PSPartChequeEdit
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
            this.psTextoBox2 = new PS.Lib.WinForms.PSTextoBox();
            this.psTextoBox3 = new PS.Lib.WinForms.PSTextoBox();
            this.psMoedaBox1 = new PS.Lib.WinForms.PSMoedaBox();
            this.psDateBox1 = new PS.Lib.WinForms.PSDateBox();
            this.psDateBox2 = new PS.Lib.WinForms.PSDateBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.psImageBox1 = new PS.Lib.WinForms.PSImageBox();
            this.psCheckBox1 = new PS.Lib.WinForms.PSCheckBox();
            this.psTextoBox4 = new PS.Lib.WinForms.PSTextoBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Size = new System.Drawing.Size(647, 309);
            this.tabControl1.Controls.SetChildIndex(this.tabPage2, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPage1, 0);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psTextoBox4);
            this.tabPage1.Controls.Add(this.psCheckBox1);
            this.tabPage1.Controls.Add(this.psDateBox2);
            this.tabPage1.Controls.Add(this.psDateBox1);
            this.tabPage1.Controls.Add(this.psMoedaBox1);
            this.tabPage1.Controls.Add(this.psTextoBox3);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Controls.Add(this.psLookup1);
            this.tabPage1.Size = new System.Drawing.Size(639, 232);
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODBANCO";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODBANCO";
            this.psLookup1.Description = "";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.KeyField = "CODBANCO";
            this.psLookup1.Location = new System.Drawing.Point(11, 49);
            this.psLookup1.LookupField = "CODBANCO;NOME";
            this.psLookup1.LookupFieldResult = "CODBANCO;NOME";
            this.psLookup1.MaxLength = 5;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(401, 38);
            this.psLookup1.TabIndex = 1;
            this.psLookup1.ValorRetorno = null;
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODAGENCIA";
            this.psTextoBox1.DataField = "CODAGENCIA";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 93);
            this.psTextoBox1.MaxLength = 15;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 3;
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "NUMCONTA";
            this.psTextoBox2.DataField = "NUMCONTA";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.Location = new System.Drawing.Point(162, 93);
            this.psTextoBox2.MaxLength = 15;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox2.TabIndex = 4;
            // 
            // psTextoBox3
            // 
            this.psTextoBox3.Caption = "NUMERO";
            this.psTextoBox3.DataField = "NUMERO";
            this.psTextoBox3.Edita = true;
            this.psTextoBox3.Location = new System.Drawing.Point(313, 93);
            this.psTextoBox3.MaxLength = 15;
            this.psTextoBox3.Name = "psTextoBox3";
            this.psTextoBox3.PasswordChar = '\0';
            this.psTextoBox3.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox3.TabIndex = 5;
            // 
            // psMoedaBox1
            // 
            this.psMoedaBox1.Caption = "VALOR";
            this.psMoedaBox1.CasasDecimais = 2;
            this.psMoedaBox1.DataField = "VALOR";
            this.psMoedaBox1.Edita = true;
            this.psMoedaBox1.Location = new System.Drawing.Point(464, 93);
            this.psMoedaBox1.Name = "psMoedaBox1";
            this.psMoedaBox1.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox1.TabIndex = 6;
            // 
            // psDateBox1
            // 
            this.psDateBox1.Caption = "DATA";
            this.psDateBox1.Chave = true;
            this.psDateBox1.DataField = "DATA";
            this.psDateBox1.Location = new System.Drawing.Point(11, 136);
            this.psDateBox1.Mascara = "00/00/0000";
            this.psDateBox1.MaxLength = 32767;
            this.psDateBox1.Name = "psDateBox1";
            this.psDateBox1.Size = new System.Drawing.Size(146, 37);
            this.psDateBox1.TabIndex = 7;
            this.psDateBox1.Value = new System.DateTime(2016, 2, 20, 16, 4, 2, 818);
            // 
            // psDateBox2
            // 
            this.psDateBox2.Caption = "DATABOA";
            this.psDateBox2.Chave = true;
            this.psDateBox2.DataField = "DATABOA";
            this.psDateBox2.Location = new System.Drawing.Point(161, 136);
            this.psDateBox2.Mascara = "00/00/0000";
            this.psDateBox2.MaxLength = 32767;
            this.psDateBox2.Name = "psDateBox2";
            this.psDateBox2.Size = new System.Drawing.Size(146, 37);
            this.psDateBox2.TabIndex = 8;
            this.psDateBox2.Value = new System.DateTime(2016, 2, 20, 16, 4, 2, 821);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.psImageBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(639, 283);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Imagem do Cheque";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // psImageBox1
            // 
            this.psImageBox1.DataField = "CODIMAGEM";
            this.psImageBox1.IdImagem = 0;
            this.psImageBox1.Location = new System.Drawing.Point(3, 16);
            this.psImageBox1.Name = "psImageBox1";
            this.psImageBox1.Size = new System.Drawing.Size(628, 262);
            this.psImageBox1.TabIndex = 0;
            // 
            // psCheckBox1
            // 
            this.psCheckBox1.Caption = "VINCULADO";
            this.psCheckBox1.Chave = true;
            this.psCheckBox1.Checked = false;
            this.psCheckBox1.DataField = "VINCULADO";
            this.psCheckBox1.Location = new System.Drawing.Point(459, 65);
            this.psCheckBox1.Name = "psCheckBox1";
            this.psCheckBox1.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox1.TabIndex = 2;
            // 
            // psTextoBox4
            // 
            this.psTextoBox4.AutoIncremento = PS.Lib.Global.TypeAutoinc.AutoInc;
            this.psTextoBox4.Caption = "CODCHEQUE";
            this.psTextoBox4.DataField = "CODCHEQUE";
            this.psTextoBox4.Edita = true;
            this.psTextoBox4.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox4.MaxLength = 32767;
            this.psTextoBox4.Name = "psTextoBox4";
            this.psTextoBox4.PasswordChar = '\0';
            this.psTextoBox4.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox4.TabIndex = 0;
            // 
            // PSPartChequeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 425);
            this.Name = "PSPartChequeEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartChequeEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSLookup psLookup1;
        private PS.Lib.WinForms.PSDateBox psDateBox2;
        private PS.Lib.WinForms.PSDateBox psDateBox1;
        private PS.Lib.WinForms.PSMoedaBox psMoedaBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox3;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private PS.Lib.WinForms.PSImageBox psImageBox1;
        private PS.Lib.WinForms.PSCheckBox psCheckBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox4;
    }
}