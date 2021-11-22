namespace PS.Glb
{
    partial class PSPartBaixaLancaAppFrm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PSPartBaixaLancaAppFrm));
            this.psDataBaixa = new PS.Lib.WinForms.PSDateBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.psContaCaixa = new PS.Lib.WinForms.PSLookup();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelNUMEROCHEQUE = new System.Windows.Forms.Label();
            this.textBoxNUMEROCHEQUE = new System.Windows.Forms.TextBox();
            this.checkBoxCOMCHEQUE = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.psLookup16 = new PS.Lib.WinForms.PSLookup();
            this.psLookup13 = new PS.Lib.WinForms.PSLookup();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.psDateBox1 = new PS.Lib.WinForms.PSDateBox();
            this.psHistorico = new PS.Lib.WinForms.PSTextoBox();
            this.psNumeroExtrato = new PS.Lib.WinForms.PSTextoBox();
            this.psExtratoComo = new PS.Lib.WinForms.PSComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel6 = new System.Windows.Forms.Panel();
            this.psTotalBaixar = new PS.Lib.WinForms.PSMoedaBox();
            this.button3 = new System.Windows.Forms.Button();
            this.psValorBaixa = new PS.Lib.WinForms.PSMoedaBox();
            this.psValorLiquido = new PS.Lib.WinForms.PSMoedaBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(0, 76);
            this.tabControl1.Size = new System.Drawing.Size(1057, 471);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Size = new System.Drawing.Size(1049, 445);
            this.tabPage1.Text = "FrmBaseApp";
            // 
            // psDataBaixa
            // 
            this.psDataBaixa.Caption = "Data da Baixa";
            this.psDataBaixa.Chave = true;
            this.psDataBaixa.DataField = "Data da Baixa";
            this.psDataBaixa.Location = new System.Drawing.Point(6, 20);
            this.psDataBaixa.Mascara = "00/00/0000";
            this.psDataBaixa.MaxLength = 32767;
            this.psDataBaixa.Name = "psDataBaixa";
            this.psDataBaixa.Size = new System.Drawing.Size(146, 37);
            this.psDataBaixa.TabIndex = 1;
            this.psDataBaixa.Value = new System.DateTime(2015, 9, 4, 0, 0, 0, 0);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1296038729_Calculator.png");
            // 
            // psContaCaixa
            // 
            this.psContaCaixa.Caption = "Conta Caixa";
            this.psContaCaixa.Chave = true;
            this.psContaCaixa.DataField = "Conta Caixa";
            this.psContaCaixa.Description = "";
            this.psContaCaixa.DinamicTable = null;
            this.psContaCaixa.KeyField = "CODCONTA";
            this.psContaCaixa.Location = new System.Drawing.Point(158, 20);
            this.psContaCaixa.LookupField = "CODCONTA;DESCRICAO";
            this.psContaCaixa.LookupFieldResult = "CODCONTA;DESCRICAO";
            this.psContaCaixa.MaxLength = 15;
            this.psContaCaixa.Name = "psContaCaixa";
            this.psContaCaixa.PSPart = "PSPartConta";
            this.psContaCaixa.Size = new System.Drawing.Size(555, 38);
            this.psContaCaixa.TabIndex = 2;
            this.psContaCaixa.ValorRetorno = null;
            this.psContaCaixa.AfterLookup += new PS.Lib.WinForms.PSLookup.AfterLookupDelegate(this.psContaCaixa_AfterLookup);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelNUMEROCHEQUE);
            this.groupBox1.Controls.Add(this.textBoxNUMEROCHEQUE);
            this.groupBox1.Controls.Add(this.checkBoxCOMCHEQUE);
            this.groupBox1.Controls.Add(this.psDataBaixa);
            this.groupBox1.Controls.Add(this.psContaCaixa);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1034, 71);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Baixa";
            // 
            // labelNUMEROCHEQUE
            // 
            this.labelNUMEROCHEQUE.AutoSize = true;
            this.labelNUMEROCHEQUE.Location = new System.Drawing.Point(791, 21);
            this.labelNUMEROCHEQUE.Name = "labelNUMEROCHEQUE";
            this.labelNUMEROCHEQUE.Size = new System.Drawing.Size(99, 13);
            this.labelNUMEROCHEQUE.TabIndex = 7;
            this.labelNUMEROCHEQUE.Text = "Número do Cheque";
            // 
            // textBoxNUMEROCHEQUE
            // 
            this.textBoxNUMEROCHEQUE.Location = new System.Drawing.Point(794, 37);
            this.textBoxNUMEROCHEQUE.Name = "textBoxNUMEROCHEQUE";
            this.textBoxNUMEROCHEQUE.ReadOnly = true;
            this.textBoxNUMEROCHEQUE.Size = new System.Drawing.Size(100, 20);
            this.textBoxNUMEROCHEQUE.TabIndex = 6;
            this.textBoxNUMEROCHEQUE.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxNUMEROCHEQUE_Validating);
            // 
            // checkBoxCOMCHEQUE
            // 
            this.checkBoxCOMCHEQUE.AutoSize = true;
            this.checkBoxCOMCHEQUE.Location = new System.Drawing.Point(719, 39);
            this.checkBoxCOMCHEQUE.Name = "checkBoxCOMCHEQUE";
            this.checkBoxCOMCHEQUE.Size = new System.Drawing.Size(69, 17);
            this.checkBoxCOMCHEQUE.TabIndex = 5;
            this.checkBoxCOMCHEQUE.Text = "Cheque?";
            this.checkBoxCOMCHEQUE.UseVisualStyleBackColor = true;
            this.checkBoxCOMCHEQUE.CheckedChanged += new System.EventHandler(this.checkBoxCOMCHEQUE_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.psLookup16);
            this.groupBox2.Controls.Add(this.psLookup13);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.psDateBox1);
            this.groupBox2.Controls.Add(this.psHistorico);
            this.groupBox2.Controls.Add(this.psNumeroExtrato);
            this.groupBox2.Controls.Add(this.psExtratoComo);
            this.groupBox2.Location = new System.Drawing.Point(6, 83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1034, 114);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Extrato";
            // 
            // psLookup16
            // 
            this.psLookup16.Caption = "Natureza Orçamentária";
            this.psLookup16.Chave = true;
            this.psLookup16.DataField = "Natureza Orçamentária";
            this.psLookup16.Description = "";
            this.psLookup16.DinamicTable = null;
            this.psLookup16.KeyField = "CODNATUREZA";
            this.psLookup16.Location = new System.Drawing.Point(702, 62);
            this.psLookup16.LookupField = "CODNATUREZA;DESCRICAO";
            this.psLookup16.LookupFieldResult = "CODNATUREZA;DESCRICAO";
            this.psLookup16.MaxLength = 15;
            this.psLookup16.Name = "psLookup16";
            this.psLookup16.PSPart = null;
            this.psLookup16.Size = new System.Drawing.Size(332, 38);
            this.psLookup16.TabIndex = 12;
            this.psLookup16.ValorRetorno = null;
            this.psLookup16.Visible = false;
            // 
            // psLookup13
            // 
            this.psLookup13.Caption = "Centro de Custo/Receita";
            this.psLookup13.Chave = true;
            this.psLookup13.DataField = "Centro de Custo/Receita";
            this.psLookup13.Description = "";
            this.psLookup13.DinamicTable = null;
            this.psLookup13.KeyField = "CODCCUSTO";
            this.psLookup13.Location = new System.Drawing.Point(381, 62);
            this.psLookup13.LookupField = "CODCCUSTO;NOME";
            this.psLookup13.LookupFieldResult = "CODCCUSTO;NOME";
            this.psLookup13.MaxLength = 15;
            this.psLookup13.Name = "psLookup13";
            this.psLookup13.PSPart = null;
            this.psLookup13.Size = new System.Drawing.Size(315, 38);
            this.psLookup13.TabIndex = 11;
            this.psLookup13.ValorRetorno = null;
            this.psLookup13.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(521, 37);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(85, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Compensar?";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // psDateBox1
            // 
            this.psDateBox1.Caption = "Data Compensação";
            this.psDateBox1.Chave = true;
            this.psDateBox1.DataField = "Data Compensação";
            this.psDateBox1.Enabled = false;
            this.psDateBox1.Location = new System.Drawing.Point(612, 19);
            this.psDateBox1.Mascara = "00/00/0000";
            this.psDateBox1.MaxLength = 32767;
            this.psDateBox1.Name = "psDateBox1";
            this.psDateBox1.Size = new System.Drawing.Size(101, 37);
            this.psDateBox1.TabIndex = 3;
            this.psDateBox1.Value = new System.DateTime(2016, 5, 4, 0, 0, 0, 0);
            // 
            // psHistorico
            // 
            this.psHistorico.Caption = "Historico";
            this.psHistorico.DataField = "Historico";
            this.psHistorico.Edita = true;
            this.psHistorico.Location = new System.Drawing.Point(6, 61);
            this.psHistorico.MaxLength = 255;
            this.psHistorico.Name = "psHistorico";
            this.psHistorico.PasswordChar = '\0';
            this.psHistorico.Size = new System.Drawing.Size(362, 37);
            this.psHistorico.TabIndex = 2;
            // 
            // psNumeroExtrato
            // 
            this.psNumeroExtrato.Caption = "Número do Extrato";
            this.psNumeroExtrato.DataField = "Número do Extrato";
            this.psNumeroExtrato.Edita = true;
            this.psNumeroExtrato.Location = new System.Drawing.Point(381, 19);
            this.psNumeroExtrato.MaxLength = 15;
            this.psNumeroExtrato.Name = "psNumeroExtrato";
            this.psNumeroExtrato.PasswordChar = '\0';
            this.psNumeroExtrato.Size = new System.Drawing.Size(127, 37);
            this.psNumeroExtrato.TabIndex = 1;
            // 
            // psExtratoComo
            // 
            this.psExtratoComo.Caption = "Gerar como:";
            this.psExtratoComo.Chave = true;
            this.psExtratoComo.DataField = "Gerar como:";
            this.psExtratoComo.DataSource = null;
            this.psExtratoComo.DisplayMember = "";
            this.psExtratoComo.Location = new System.Drawing.Point(6, 17);
            this.psExtratoComo.Name = "psExtratoComo";
            this.psExtratoComo.SelectedIndex = -1;
            this.psExtratoComo.Size = new System.Drawing.Size(362, 37);
            this.psExtratoComo.TabIndex = 0;
            this.psExtratoComo.Value = null;
            this.psExtratoComo.ValueMember = "";
            this.psExtratoComo.SelectedValueChanged += new PS.Lib.WinForms.PSComboBox.SelectedValueChangedDelegate(this.psExtratoComo_SelectedValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel5);
            this.groupBox3.Location = new System.Drawing.Point(6, 203);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1035, 231);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Lançamento Selecionados";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.dataGridView1);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Location = new System.Drawing.Point(6, 20);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1023, 205);
            this.panel5.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(834, 205);
            this.dataGridView1.TabIndex = 18;
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.psTotalBaixar);
            this.panel6.Controls.Add(this.button3);
            this.panel6.Controls.Add(this.psValorBaixa);
            this.panel6.Controls.Add(this.psValorLiquido);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(834, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(189, 205);
            this.panel6.TabIndex = 3;
            // 
            // psTotalBaixar
            // 
            this.psTotalBaixar.Caption = "Total a Baixar";
            this.psTotalBaixar.CasasDecimais = 2;
            this.psTotalBaixar.DataField = "Total a Baixar";
            this.psTotalBaixar.Edita = true;
            this.psTotalBaixar.Location = new System.Drawing.Point(6, 98);
            this.psTotalBaixar.Name = "psTotalBaixar";
            this.psTotalBaixar.Size = new System.Drawing.Size(145, 37);
            this.psTotalBaixar.TabIndex = 6;
            // 
            // button3
            // 
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(155, 69);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(25, 23);
            this.button3.TabIndex = 2;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // psValorBaixa
            // 
            this.psValorBaixa.Caption = "Valor da Baixa";
            this.psValorBaixa.CasasDecimais = 2;
            this.psValorBaixa.DataField = "Valor da Baixa";
            this.psValorBaixa.Edita = true;
            this.psValorBaixa.Location = new System.Drawing.Point(6, 55);
            this.psValorBaixa.Name = "psValorBaixa";
            this.psValorBaixa.Size = new System.Drawing.Size(145, 37);
            this.psValorBaixa.TabIndex = 1;
            this.psValorBaixa.Validating += new System.ComponentModel.CancelEventHandler(this.psValorBaixa_Validating);
            // 
            // psValorLiquido
            // 
            this.psValorLiquido.Caption = "Valor Liquido";
            this.psValorLiquido.CasasDecimais = 2;
            this.psValorLiquido.DataField = "Valor Liquido";
            this.psValorLiquido.Edita = true;
            this.psValorLiquido.Location = new System.Drawing.Point(6, 12);
            this.psValorLiquido.Name = "psValorLiquido";
            this.psValorLiquido.Size = new System.Drawing.Size(145, 37);
            this.psValorLiquido.TabIndex = 0;
            // 
            // PSPartBaixaLancaAppFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 597);
            this.Name = "PSPartBaixaLancaAppFrm";
            this.Text = "PSPartBaixaLancaAppFrm";
            this.Load += new System.EventHandler(this.PSPartBaixaLancaAppFrm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSDateBox psDataBaixa;
        private System.Windows.Forms.ImageList imageList1;
        private Lib.WinForms.PSLookup psContaCaixa;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private Lib.WinForms.PSTextoBox psNumeroExtrato;
        private Lib.WinForms.PSComboBox psExtratoComo;
        private Lib.WinForms.PSTextoBox psHistorico;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel5;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel6;
        private Lib.WinForms.PSMoedaBox psValorBaixa;
        private Lib.WinForms.PSMoedaBox psValorLiquido;
        private Lib.WinForms.PSMoedaBox psTotalBaixar;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox checkBox1;
        private Lib.WinForms.PSDateBox psDateBox1;
        private System.Windows.Forms.CheckBox checkBoxCOMCHEQUE;
        private System.Windows.Forms.Label labelNUMEROCHEQUE;
        private System.Windows.Forms.TextBox textBoxNUMEROCHEQUE;
        private Lib.WinForms.PSLookup psLookup16;
        private Lib.WinForms.PSLookup psLookup13;
    }
}