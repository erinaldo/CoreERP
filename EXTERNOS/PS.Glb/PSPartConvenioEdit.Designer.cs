namespace PS.Glb
{
    partial class PSPartConvenioEdit
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
            this.psTextoBox2 = new PS.Lib.WinForms.PSTextoBox();
            this.psTextoBox3 = new PS.Lib.WinForms.PSTextoBox();
            this.psCheckBox1 = new PS.Lib.WinForms.PSCheckBox();
            this.psLookup1 = new PS.Lib.WinForms.PSLookup();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.psCheckBox2 = new PS.Lib.WinForms.PSCheckBox();
            this.psTextoBox4 = new PS.Lib.WinForms.PSTextoBox();
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
            this.tabControl1.Size = new System.Drawing.Size(597, 255);
            this.tabControl1.Controls.SetChildIndex(this.tabPage2, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPage1, 0);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psTextoBox4);
            this.tabPage1.Controls.Add(this.psLookup1);
            this.tabPage1.Controls.Add(this.psCheckBox1);
            this.tabPage1.Controls.Add(this.psTextoBox3);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(589, 229);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 317);
            this.panel2.Size = new System.Drawing.Size(597, 54);
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(597, 30);
            // 
            // buttonSALVAR
            // 
            this.buttonSALVAR.Location = new System.Drawing.Point(320, 15);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(411, 15);
            // 
            // buttonCANCELAR
            // 
            this.buttonCANCELAR.Location = new System.Drawing.Point(501, 15);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODCONVENIO";
            this.psTextoBox1.DataField = "CODCONVENIO";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(6, 6);
            this.psTextoBox1.MaxLength = 10;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 0;
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "DIGITOCONVENIO";
            this.psTextoBox2.DataField = "DIGITOCONVENIO";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.Location = new System.Drawing.Point(157, 6);
            this.psTextoBox2.MaxLength = 2;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox2.TabIndex = 1;
            // 
            // psTextoBox3
            // 
            this.psTextoBox3.Caption = "DESCRICAO";
            this.psTextoBox3.DataField = "DESCRICAO";
            this.psTextoBox3.Edita = true;
            this.psTextoBox3.Location = new System.Drawing.Point(6, 49);
            this.psTextoBox3.MaxLength = 100;
            this.psTextoBox3.Name = "psTextoBox3";
            this.psTextoBox3.PasswordChar = '\0';
            this.psTextoBox3.Size = new System.Drawing.Size(447, 37);
            this.psTextoBox3.TabIndex = 2;
            // 
            // psCheckBox1
            // 
            this.psCheckBox1.Caption = "ATIVO";
            this.psCheckBox1.Chave = true;
            this.psCheckBox1.Checked = true;
            this.psCheckBox1.DataField = "ATIVO";
            this.psCheckBox1.Location = new System.Drawing.Point(6, 92);
            this.psCheckBox1.Name = "psCheckBox1";
            this.psCheckBox1.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox1.TabIndex = 3;
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODCONTA";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODCONTA";
            this.psLookup1.Description = "";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup1.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup1.KeyField = "CODCONTA";
            this.psLookup1.Location = new System.Drawing.Point(6, 120);
            this.psLookup1.LookupField = "CODCONTA;DESCRICAO;CODBANCO;CODAGENCIA;NUMCONTA";
            this.psLookup1.LookupFieldResult = "CODCONTA;DESCRICAO";
            this.psLookup1.MaxLength = 32767;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(447, 38);
            this.psLookup1.TabIndex = 4;
            this.psLookup1.ValorRetorno = null;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.psCheckBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(589, 229);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Parâmetros";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // psCheckBox2
            // 
            this.psCheckBox2.Caption = "COMREGISTRO";
            this.psCheckBox2.Chave = true;
            this.psCheckBox2.Checked = false;
            this.psCheckBox2.DataField = "COMREGISTRO";
            this.psCheckBox2.Location = new System.Drawing.Point(26, 31);
            this.psCheckBox2.Name = "psCheckBox2";
            this.psCheckBox2.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox2.TabIndex = 0;
            // 
            // psTextoBox4
            // 
            this.psTextoBox4.Caption = "CODMODALIDADEIMPRESSAO";
            this.psTextoBox4.DataField = "CODMODALIDADEIMPRESSAO";
            this.psTextoBox4.Edita = true;
            this.psTextoBox4.Location = new System.Drawing.Point(308, 6);
            this.psTextoBox4.MaxLength = 32767;
            this.psTextoBox4.Name = "psTextoBox4";
            this.psTextoBox4.PasswordChar = '\0';
            this.psTextoBox4.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox4.TabIndex = 5;
            // 
            // PSPartConvenioEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 371);
            this.Name = "PSPartConvenioEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartConvenioEdit";
            this.Load += new System.EventHandler(this.PSPartConvenioEdit_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Lib.WinForms.PSTextoBox psTextoBox1;
        private Lib.WinForms.PSTextoBox psTextoBox2;
        private Lib.WinForms.PSTextoBox psTextoBox3;
        private Lib.WinForms.PSCheckBox psCheckBox1;
        private Lib.WinForms.PSLookup psLookup1;
        private System.Windows.Forms.TabPage tabPage2;
        private Lib.WinForms.PSCheckBox psCheckBox2;
        private Lib.WinForms.PSTextoBox psTextoBox4;
    }
}