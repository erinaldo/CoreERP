namespace PS.Glb
{
    partial class PSPartContaEdit
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
            this.psTextoBox1 = new PS.Lib.WinForms.PSTextoBox();
            this.psCheckBox1 = new PS.Lib.WinForms.PSCheckBox();
            this.psTextoBox2 = new PS.Lib.WinForms.PSTextoBox();
            this.psDateBox1 = new PS.Lib.WinForms.PSDateBox();
            this.psMoedaBox1 = new PS.Lib.WinForms.PSMoedaBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.psLookup3 = new PS.Lib.WinForms.PSLookup();
            this.psLookup2 = new PS.Lib.WinForms.PSLookup();
            this.psLookup1 = new PS.Lib.WinForms.PSLookup();
            this.psRelConsolidado = new PS.Lib.WinForms.PSCheckBox();
            this.psOrdemConsolidado = new PS.Lib.WinForms.PSTextoBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Size = new System.Drawing.Size(647, 258);
            this.tabControl1.Controls.SetChildIndex(this.tabPage2, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPage1, 0);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psOrdemConsolidado);
            this.tabPage1.Controls.Add(this.psRelConsolidado);
            this.tabPage1.Controls.Add(this.psMoedaBox1);
            this.tabPage1.Controls.Add(this.psDateBox1);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psCheckBox1);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(639, 232);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 320);
            this.panel2.Size = new System.Drawing.Size(647, 54);
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(647, 30);
            // 
            // buttonSALVAR
            // 
            this.buttonSALVAR.Location = new System.Drawing.Point(370, 15);
            this.buttonSALVAR.Click += new System.EventHandler(this.buttonSALVAR_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(461, 15);
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCANCELAR
            // 
            this.buttonCANCELAR.Location = new System.Drawing.Point(551, 15);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODCONTA";
            this.psTextoBox1.DataField = "CODCONTA";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 15;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 0;
            // 
            // psCheckBox1
            // 
            this.psCheckBox1.Caption = "ATIVO";
            this.psCheckBox1.Chave = true;
            this.psCheckBox1.Checked = false;
            this.psCheckBox1.DataField = "ATIVO";
            this.psCheckBox1.Location = new System.Drawing.Point(162, 21);
            this.psCheckBox1.Name = "psCheckBox1";
            this.psCheckBox1.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox1.TabIndex = 1;
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "DESCRICAO";
            this.psTextoBox2.DataField = "DESCRICAO";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.Location = new System.Drawing.Point(11, 49);
            this.psTextoBox2.MaxLength = 80;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox2.TabIndex = 2;
            // 
            // psDateBox1
            // 
            this.psDateBox1.Caption = "DTBASE";
            this.psDateBox1.Chave = true;
            this.psDateBox1.DataField = "DTBASE";
            this.psDateBox1.Location = new System.Drawing.Point(11, 92);
            this.psDateBox1.Mascara = "00/00/0000";
            this.psDateBox1.MaxLength = 32767;
            this.psDateBox1.Name = "psDateBox1";
            this.psDateBox1.Size = new System.Drawing.Size(146, 37);
            this.psDateBox1.TabIndex = 3;
            this.psDateBox1.Value = new System.DateTime(2016, 1, 28, 11, 57, 17, 802);
            // 
            // psMoedaBox1
            // 
            this.psMoedaBox1.Caption = "SALDODATABASE";
            this.psMoedaBox1.CasasDecimais = 2;
            this.psMoedaBox1.DataField = "SALDODATABASE";
            this.psMoedaBox1.Edita = true;
            this.psMoedaBox1.Location = new System.Drawing.Point(167, 92);
            this.psMoedaBox1.Name = "psMoedaBox1";
            this.psMoedaBox1.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox1.TabIndex = 4;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.psLookup3);
            this.tabPage2.Controls.Add(this.psLookup2);
            this.tabPage2.Controls.Add(this.psLookup1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(882, 295);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Integração Bancária";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // psLookup3
            // 
            this.psLookup3.Caption = "NUMCONTA";
            this.psLookup3.Chave = true;
            this.psLookup3.DataField = "NUMCONTA";
            this.psLookup3.Description = "";
            this.psLookup3.DinamicTable = null;
            this.psLookup3.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup3.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup3.KeyField = "NUMCONTA";
            this.psLookup3.Location = new System.Drawing.Point(11, 94);
            this.psLookup3.LookupField = "CODBANCO;CODAGENCIA;NUMCONTA;";
            this.psLookup3.LookupFieldResult = "NUMCONTA;DIGCONTA";
            this.psLookup3.MaxLength = 15;
            this.psLookup3.Name = "psLookup3";
            this.psLookup3.PSPart = null;
            this.psLookup3.Size = new System.Drawing.Size(401, 38);
            this.psLookup3.TabIndex = 2;
            this.psLookup3.ValorRetorno = null;
            this.psLookup3.BeforeLookup += new PS.Lib.WinForms.PSLookup.BeforeLookupDelegate(this.psLookup3_BeforeLookup);
            // 
            // psLookup2
            // 
            this.psLookup2.Caption = "CODAGENCIA";
            this.psLookup2.Chave = true;
            this.psLookup2.DataField = "CODAGENCIA";
            this.psLookup2.Description = "";
            this.psLookup2.DinamicTable = null;
            this.psLookup2.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup2.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup2.KeyField = "CODAGENCIA";
            this.psLookup2.Location = new System.Drawing.Point(11, 50);
            this.psLookup2.LookupField = "CODBANCO;CODAGENCIA;NOME";
            this.psLookup2.LookupFieldResult = "CODAGENCIA;NOME";
            this.psLookup2.MaxLength = 15;
            this.psLookup2.Name = "psLookup2";
            this.psLookup2.PSPart = null;
            this.psLookup2.Size = new System.Drawing.Size(401, 38);
            this.psLookup2.TabIndex = 1;
            this.psLookup2.ValorRetorno = null;
            this.psLookup2.BeforeLookup += new PS.Lib.WinForms.PSLookup.BeforeLookupDelegate(this.psLookup2_BeforeLookup);
            this.psLookup2.AfterLookup += new PS.Lib.WinForms.PSLookup.AfterLookupDelegate(this.psLookup2_AfterLookup);
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODBANCO";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODBANCO";
            this.psLookup1.Description = "";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup1.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup1.KeyField = "CODBANCO";
            this.psLookup1.Location = new System.Drawing.Point(11, 6);
            this.psLookup1.LookupField = "CODBANCO;NOME";
            this.psLookup1.LookupFieldResult = "CODBANCO;CODFEBRABAN;NOME";
            this.psLookup1.MaxLength = 5;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(401, 38);
            this.psLookup1.TabIndex = 0;
            this.psLookup1.ValorRetorno = null;
            this.psLookup1.AfterLookup += new PS.Lib.WinForms.PSLookup.AfterLookupDelegate(this.psLookup1_AfterLookup);
            // 
            // psRelConsolidado
            // 
            this.psRelConsolidado.Caption = "RELCONSOLIDADO";
            this.psRelConsolidado.Chave = true;
            this.psRelConsolidado.Checked = false;
            this.psRelConsolidado.DataField = "RELCONSOLIDADO";
            this.psRelConsolidado.Location = new System.Drawing.Point(11, 135);
            this.psRelConsolidado.Name = "psRelConsolidado";
            this.psRelConsolidado.Size = new System.Drawing.Size(150, 22);
            this.psRelConsolidado.TabIndex = 8;
            // 
            // psOrdemConsolidado
            // 
            this.psOrdemConsolidado.Caption = "ORDEMCONSOLIDADO";
            this.psOrdemConsolidado.DataField = "ORDEMCONSOLIDADO";
            this.psOrdemConsolidado.Edita = true;
            this.psOrdemConsolidado.Location = new System.Drawing.Point(11, 163);
            this.psOrdemConsolidado.MaxLength = 80;
            this.psOrdemConsolidado.Name = "psOrdemConsolidado";
            this.psOrdemConsolidado.PasswordChar = '\0';
            this.psOrdemConsolidado.Size = new System.Drawing.Size(146, 37);
            this.psOrdemConsolidado.TabIndex = 9;
            // 
            // PSPartContaEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartContaEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartContaEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private PS.Lib.WinForms.PSLookup psLookup1;
        private PS.Lib.WinForms.PSLookup psLookup2;
        private PS.Lib.WinForms.PSLookup psLookup3;
        private PS.Lib.WinForms.PSMoedaBox psMoedaBox1;
        private PS.Lib.WinForms.PSDateBox psDateBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private PS.Lib.WinForms.PSCheckBox psCheckBox1;
        private Lib.WinForms.PSTextoBox psOrdemConsolidado;
        private Lib.WinForms.PSCheckBox psRelConsolidado;
    }
}