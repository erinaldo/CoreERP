namespace PS.Glb
{
    partial class PSPartAcessoMenuEdit
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
            this.psCheckBox1 = new PS.Lib.WinForms.PSCheckBox();
            this.psCheckBox2 = new PS.Lib.WinForms.PSCheckBox();
            this.psCheckBox3 = new PS.Lib.WinForms.PSCheckBox();
            this.psCheckBox4 = new PS.Lib.WinForms.PSCheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(647, 258);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psCheckBox4);
            this.tabPage1.Controls.Add(this.psCheckBox3);
            this.tabPage1.Controls.Add(this.psCheckBox2);
            this.tabPage1.Controls.Add(this.psCheckBox1);
            this.tabPage1.Controls.Add(this.psLookup1);
            this.tabPage1.Size = new System.Drawing.Size(639, 232);
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODPSPART";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODPSPART";
            this.psLookup1.Description = "";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.KeyField = "CODPSPART";
            this.psLookup1.Location = new System.Drawing.Point(11, 6);
            this.psLookup1.LookupField = "CODPSPART;DESCRICAO";
            this.psLookup1.LookupFieldResult = "CODPSPART;DESCRICAO";
            this.psLookup1.MaxLength = 50;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(401, 37);
            this.psLookup1.TabIndex = 0;
            this.psLookup1.ValorRetorno = null;
            this.psLookup1.AfterLookup += new PS.Lib.WinForms.PSLookup.AfterLookupDelegate(this.psLookup1_AfterLookup);
            // 
            // psCheckBox1
            // 
            this.psCheckBox1.Caption = "ACESSO";
            this.psCheckBox1.Chave = true;
            this.psCheckBox1.Checked = false;
            this.psCheckBox1.DataField = "ACESSO";
            this.psCheckBox1.Location = new System.Drawing.Point(11, 49);
            this.psCheckBox1.Name = "psCheckBox1";
            this.psCheckBox1.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox1.TabIndex = 1;
            this.psCheckBox1.CheckedChanged += new PS.Lib.WinForms.PSCheckBox.CheckedChangedDelegate(this.psCheckBox1_CheckedChanged);
            // 
            // psCheckBox2
            // 
            this.psCheckBox2.Caption = "PERMITEINCLUIR";
            this.psCheckBox2.Chave = true;
            this.psCheckBox2.Checked = false;
            this.psCheckBox2.DataField = "PERMITEINCLUIR";
            this.psCheckBox2.Location = new System.Drawing.Point(11, 77);
            this.psCheckBox2.Name = "psCheckBox2";
            this.psCheckBox2.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox2.TabIndex = 2;
            // 
            // psCheckBox3
            // 
            this.psCheckBox3.Caption = "PERMITEALTERAR";
            this.psCheckBox3.Chave = true;
            this.psCheckBox3.Checked = false;
            this.psCheckBox3.DataField = "PERMITEALTERAR";
            this.psCheckBox3.Location = new System.Drawing.Point(167, 77);
            this.psCheckBox3.Name = "psCheckBox3";
            this.psCheckBox3.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox3.TabIndex = 3;
            // 
            // psCheckBox4
            // 
            this.psCheckBox4.Caption = "PERMITEEXCLUIR";
            this.psCheckBox4.Chave = true;
            this.psCheckBox4.Checked = false;
            this.psCheckBox4.DataField = "PERMITEEXCLUIR";
            this.psCheckBox4.Location = new System.Drawing.Point(323, 77);
            this.psCheckBox4.Name = "psCheckBox4";
            this.psCheckBox4.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox4.TabIndex = 4;
            // 
            // PSPartAcessoMenuEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartAcessoMenuEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartAcessoMenuEdit";
            this.Load += new System.EventHandler(this.PSPartAcessoMenuEdit_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSLookup psLookup1;
        private PS.Lib.WinForms.PSCheckBox psCheckBox1;
        private PS.Lib.WinForms.PSCheckBox psCheckBox4;
        private PS.Lib.WinForms.PSCheckBox psCheckBox3;
        private PS.Lib.WinForms.PSCheckBox psCheckBox2;
    }
}