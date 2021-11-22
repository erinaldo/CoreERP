namespace PS.Glb
{
    partial class PSPartTipOperSerieEdit
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
            this.psLookup2 = new PS.Lib.WinForms.PSLookup();
            this.psLookup13 = new PS.Lib.WinForms.PSLookup();
            this.psCheckBox52 = new PS.Lib.WinForms.PSCheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(647, 258);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psCheckBox52);
            this.tabPage1.Controls.Add(this.psLookup13);
            this.tabPage1.Controls.Add(this.psLookup2);
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
            this.buttonSALVAR.Location = new System.Drawing.Point(155, 19);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(236, 19);
            // 
            // buttonCANCELAR
            // 
            this.buttonCANCELAR.Location = new System.Drawing.Point(317, 19);
            // 
            // psLookup2
            // 
            this.psLookup2.Caption = "CODSERIE";
            this.psLookup2.Chave = true;
            this.psLookup2.DataField = "CODSERIE";
            this.psLookup2.Description = "";
            this.psLookup2.DinamicTable = null;
            this.psLookup2.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup2.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup2.KeyField = "CODSERIE";
            this.psLookup2.Location = new System.Drawing.Point(11, 6);
            this.psLookup2.LookupField = "CODSERIE;DESCRICAO";
            this.psLookup2.LookupFieldResult = "CODSERIE;DESCRICAO";
            this.psLookup2.MaxLength = 3;
            this.psLookup2.Name = "psLookup2";
            this.psLookup2.PSPart = null;
            this.psLookup2.Size = new System.Drawing.Size(401, 38);
            this.psLookup2.TabIndex = 1;
            this.psLookup2.ValorRetorno = null;
            // 
            // psLookup13
            // 
            this.psLookup13.Caption = "CODFILIAL";
            this.psLookup13.Chave = true;
            this.psLookup13.DataField = "CODFILIAL";
            this.psLookup13.Description = "";
            this.psLookup13.DinamicTable = null;
            this.psLookup13.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup13.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup13.KeyField = "CODFILIAL";
            this.psLookup13.Location = new System.Drawing.Point(11, 50);
            this.psLookup13.LookupField = "CODFILIAL;NOME;NOMEFANTASIA";
            this.psLookup13.LookupFieldResult = "CODFILIAL;NOME";
            this.psLookup13.MaxLength = 32767;
            this.psLookup13.Name = "psLookup13";
            this.psLookup13.PSPart = null;
            this.psLookup13.Size = new System.Drawing.Size(401, 38);
            this.psLookup13.TabIndex = 13;
            this.psLookup13.ValorRetorno = null;
            // 
            // psCheckBox52
            // 
            this.psCheckBox52.Caption = "PRINCIPAL";
            this.psCheckBox52.Chave = true;
            this.psCheckBox52.Checked = false;
            this.psCheckBox52.DataField = "PRINCIPAL";
            this.psCheckBox52.Location = new System.Drawing.Point(11, 94);
            this.psCheckBox52.Name = "psCheckBox52";
            this.psCheckBox52.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox52.TabIndex = 14;
            // 
            // PSPartTipOperSerieEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartTipOperSerieEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartTipOperSerieEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSLookup psLookup2;
        private Lib.WinForms.PSLookup psLookup13;
        private Lib.WinForms.PSCheckBox psCheckBox52;
    }
}