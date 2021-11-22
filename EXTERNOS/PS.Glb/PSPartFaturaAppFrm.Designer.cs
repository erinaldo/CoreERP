namespace PS.Glb
{
    partial class PSPartFaturaAppFrm
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
            this.textBoxCODTIPDOC = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNOME = new System.Windows.Forms.TextBox();
            this.dateTimePickerDATAVENCIMENTO = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxVLORIGINAL = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxOBSERVACAO = new System.Windows.Forms.TextBox();
            this.psLookup13 = new PS.Lib.WinForms.PSLookup();
            this.psLookup16 = new PS.Lib.WinForms.PSLookup();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(0, 76);
            this.tabControl1.Size = new System.Drawing.Size(724, 325);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psLookup16);
            this.tabPage1.Controls.Add(this.psLookup13);
            this.tabPage1.Controls.Add(this.textBoxOBSERVACAO);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.textBoxVLORIGINAL);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.dateTimePickerDATAVENCIMENTO);
            this.tabPage1.Controls.Add(this.textBoxNOME);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.textBoxCODTIPDOC);
            this.tabPage1.Size = new System.Drawing.Size(716, 299);
            this.tabPage1.Text = "FrmBaseApp";
            // 
            // textBoxCODTIPDOC
            // 
            this.textBoxCODTIPDOC.Location = new System.Drawing.Point(26, 43);
            this.textBoxCODTIPDOC.Name = "textBoxCODTIPDOC";
            this.textBoxCODTIPDOC.ReadOnly = true;
            this.textBoxCODTIPDOC.Size = new System.Drawing.Size(100, 20);
            this.textBoxCODTIPDOC.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tipo de Documento";
            // 
            // textBoxNOME
            // 
            this.textBoxNOME.Location = new System.Drawing.Point(132, 43);
            this.textBoxNOME.Name = "textBoxNOME";
            this.textBoxNOME.ReadOnly = true;
            this.textBoxNOME.Size = new System.Drawing.Size(529, 20);
            this.textBoxNOME.TabIndex = 2;
            // 
            // dateTimePickerDATAVENCIMENTO
            // 
            this.dateTimePickerDATAVENCIMENTO.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDATAVENCIMENTO.Location = new System.Drawing.Point(26, 82);
            this.dateTimePickerDATAVENCIMENTO.Name = "dateTimePickerDATAVENCIMENTO";
            this.dateTimePickerDATAVENCIMENTO.Size = new System.Drawing.Size(100, 20);
            this.dateTimePickerDATAVENCIMENTO.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Data de Vencimento";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Valor Original";
            // 
            // textBoxVLORIGINAL
            // 
            this.textBoxVLORIGINAL.Location = new System.Drawing.Point(26, 121);
            this.textBoxVLORIGINAL.Name = "textBoxVLORIGINAL";
            this.textBoxVLORIGINAL.ReadOnly = true;
            this.textBoxVLORIGINAL.Size = new System.Drawing.Size(100, 20);
            this.textBoxVLORIGINAL.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Observação";
            // 
            // textBoxOBSERVACAO
            // 
            this.textBoxOBSERVACAO.Location = new System.Drawing.Point(26, 248);
            this.textBoxOBSERVACAO.Name = "textBoxOBSERVACAO";
            this.textBoxOBSERVACAO.Size = new System.Drawing.Size(635, 20);
            this.textBoxOBSERVACAO.TabIndex = 8;
            // 
            // psLookup13
            // 
            this.psLookup13.Caption = "CODCCUSTO";
            this.psLookup13.Chave = true;
            this.psLookup13.DataField = "CODCCUSTO";
            this.psLookup13.Description = "";
            this.psLookup13.DinamicTable = null;
            this.psLookup13.KeyField = "CODCCUSTO";
            this.psLookup13.Location = new System.Drawing.Point(26, 147);
            this.psLookup13.LookupField = "CODCCUSTO;NOME";
            this.psLookup13.LookupFieldResult = "CODCCUSTO;NOME";
            this.psLookup13.MaxLength = 15;
            this.psLookup13.Name = "psLookup13";
            this.psLookup13.PSPart = null;
            this.psLookup13.Size = new System.Drawing.Size(635, 38);
            this.psLookup13.TabIndex = 9;
            this.psLookup13.ValorRetorno = null;
            // 
            // psLookup16
            // 
            this.psLookup16.Caption = "CODNATUREZAORCAMENTO";
            this.psLookup16.Chave = true;
            this.psLookup16.DataField = "CODNATUREZAORCAMENTO";
            this.psLookup16.Description = "";
            this.psLookup16.DinamicTable = null;
            this.psLookup16.KeyField = "CODNATUREZA";
            this.psLookup16.Location = new System.Drawing.Point(26, 191);
            this.psLookup16.LookupField = "CODNATUREZA;DESCRICAO";
            this.psLookup16.LookupFieldResult = "CODNATUREZA;DESCRICAO";
            this.psLookup16.MaxLength = 15;
            this.psLookup16.Name = "psLookup16";
            this.psLookup16.PSPart = null;
            this.psLookup16.Size = new System.Drawing.Size(635, 38);
            this.psLookup16.TabIndex = 10;
            this.psLookup16.ValorRetorno = null;
            // 
            // PSPartFaturaAppFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 451);
            this.Name = "PSPartFaturaAppFrm";
            this.Text = "PSPartFaturaAppFrm";
            this.Load += new System.EventHandler(this.PSPartFaturaAppFrm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxCODTIPDOC;
        private System.Windows.Forms.TextBox textBoxNOME;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePickerDATAVENCIMENTO;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxVLORIGINAL;
        private System.Windows.Forms.TextBox textBoxOBSERVACAO;
        private System.Windows.Forms.Label label5;
        private Lib.WinForms.PSLookup psLookup13;
        private Lib.WinForms.PSLookup psLookup16;

    }
}