namespace PS.Glb.Report
{
    partial class PSPartReportEdit
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.psTextoBox5 = new PS.Lib.WinForms.PSTextoBox();
            this.psTextoBox4 = new PS.Lib.WinForms.PSTextoBox();
            this.psTextoBox3 = new PS.Lib.WinForms.PSTextoBox();
            this.psLookup1 = new PS.Lib.WinForms.PSLookup();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.PSPartAcessoRelatorio = new PS.Lib.WinForms.PSBaseVisao();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Size = new System.Drawing.Size(647, 286);
            this.tabControl1.Controls.SetChildIndex(this.tabPage2, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPage1, 0);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(639, 260);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODREPORT";
            this.psTextoBox1.DataField = "CODREPORT";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 25;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 0;
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "DESCRICAO";
            this.psTextoBox2.DataField = "DESCRICAO";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.Location = new System.Drawing.Point(11, 49);
            this.psTextoBox2.MaxLength = 50;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox2.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.psTextoBox5);
            this.groupBox1.Controls.Add(this.psTextoBox4);
            this.groupBox1.Controls.Add(this.psTextoBox3);
            this.groupBox1.Controls.Add(this.psLookup1);
            this.groupBox1.Location = new System.Drawing.Point(6, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(627, 186);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Referência";
            // 
            // psTextoBox5
            // 
            this.psTextoBox5.Caption = "CLASSNAME";
            this.psTextoBox5.DataField = "CLASSNAME";
            this.psTextoBox5.Edita = true;
            this.psTextoBox5.Location = new System.Drawing.Point(221, 106);
            this.psTextoBox5.MaxLength = 80;
            this.psTextoBox5.Name = "psTextoBox5";
            this.psTextoBox5.PasswordChar = '\0';
            this.psTextoBox5.Size = new System.Drawing.Size(209, 37);
            this.psTextoBox5.TabIndex = 3;
            // 
            // psTextoBox4
            // 
            this.psTextoBox4.Caption = "NAMESPACE";
            this.psTextoBox4.DataField = "NAMESPACE";
            this.psTextoBox4.Edita = true;
            this.psTextoBox4.Location = new System.Drawing.Point(6, 106);
            this.psTextoBox4.MaxLength = 80;
            this.psTextoBox4.Name = "psTextoBox4";
            this.psTextoBox4.PasswordChar = '\0';
            this.psTextoBox4.Size = new System.Drawing.Size(209, 37);
            this.psTextoBox4.TabIndex = 2;
            // 
            // psTextoBox3
            // 
            this.psTextoBox3.Caption = "ASSEMBLYNAME";
            this.psTextoBox3.DataField = "ASSEMBLYNAME";
            this.psTextoBox3.Edita = true;
            this.psTextoBox3.Location = new System.Drawing.Point(6, 63);
            this.psTextoBox3.MaxLength = 80;
            this.psTextoBox3.Name = "psTextoBox3";
            this.psTextoBox3.PasswordChar = '\0';
            this.psTextoBox3.Size = new System.Drawing.Size(209, 37);
            this.psTextoBox3.TabIndex = 1;
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODPSPART";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODPSPART";
            this.psLookup1.Description = "";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.KeyField = "CODPSPART";
            this.psLookup1.Location = new System.Drawing.Point(5, 19);
            this.psLookup1.LookupField = "CODPSPART;DESCRICAO";
            this.psLookup1.LookupFieldResult = "CODPSPART;DESCRICAO";
            this.psLookup1.MaxLength = 50;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(401, 38);
            this.psLookup1.TabIndex = 0;
            this.psLookup1.ValorRetorno = null;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.PSPartAcessoRelatorio);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(639, 260);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Perfil";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // PSPartAcessoRelatorio
            // 
            this.PSPartAcessoRelatorio._atualiza = false;
            this.PSPartAcessoRelatorio.aplicativo = null;
            this.PSPartAcessoRelatorio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PSPartAcessoRelatorio.Location = new System.Drawing.Point(3, 3);
            this.PSPartAcessoRelatorio.Name = "PSPartAcessoRelatorio";
            this.PSPartAcessoRelatorio.psPart = null;
            this.PSPartAcessoRelatorio.Size = new System.Drawing.Size(633, 254);
            this.PSPartAcessoRelatorio.TabIndex = 0;
            // 
            // PSPartReportEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 402);
            this.Name = "PSPartReportEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartReportEdit";
            this.Load += new System.EventHandler(this.PSPartReportEdit_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox5;
        private PS.Lib.WinForms.PSTextoBox psTextoBox4;
        private PS.Lib.WinForms.PSTextoBox psTextoBox3;
        private PS.Lib.WinForms.PSLookup psLookup1;
        private System.Windows.Forms.TabPage tabPage2;
        private Lib.WinForms.PSBaseVisao PSPartAcessoRelatorio;
    }
}