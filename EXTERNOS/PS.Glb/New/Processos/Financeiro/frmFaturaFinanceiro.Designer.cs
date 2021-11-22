namespace PS.Glb.New.Processos.Financeiro
{
    partial class frmFaturaFinanceiro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFaturaFinanceiro));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtobservacao = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtVlOriginal = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dtDataVencimento = new DevExpress.XtraEditors.DateEdit();
            this.btnExecutar = new DevExpress.XtraEditors.SimpleButton();
            this.btnFechar = new DevExpress.XtraEditors.SimpleButton();
            this.txtNumero = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.psLookup5 = new PS.Lib.WinForms.PSLookup();
            this.psLookup16 = new PS.Lib.WinForms.PSLookup();
            this.psLookup13 = new PS.Lib.WinForms.PSLookup();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtobservacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVlOriginal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDataVencimento.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDataVencimento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumero.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnExecutar);
            this.splitContainer1.Panel2.Controls.Add(this.btnFechar);
            this.splitContainer1.Size = new System.Drawing.Size(543, 385);
            this.splitContainer1.SplitterDistance = 335;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(543, 335);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtNumero);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.psLookup5);
            this.tabPage1.Controls.Add(this.txtobservacao);
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.psLookup16);
            this.tabPage1.Controls.Add(this.psLookup13);
            this.tabPage1.Controls.Add(this.txtVlOriginal);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.dtDataVencimento);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(535, 309);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtobservacao
            // 
            this.txtobservacao.Location = new System.Drawing.Point(9, 273);
            this.txtobservacao.Name = "txtobservacao";
            this.txtobservacao.Size = new System.Drawing.Size(514, 20);
            this.txtobservacao.TabIndex = 14;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(9, 254);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(68, 13);
            this.labelControl4.TabIndex = 13;
            this.labelControl4.Text = "OBSERVACAO";
            // 
            // txtVlOriginal
            // 
            this.txtVlOriginal.Enabled = false;
            this.txtVlOriginal.Location = new System.Drawing.Point(9, 137);
            this.txtVlOriginal.Name = "txtVlOriginal";
            this.txtVlOriginal.Size = new System.Drawing.Size(100, 20);
            this.txtVlOriginal.TabIndex = 6;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(9, 118);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 13);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "VLORIGINAL";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(115, 73);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(92, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "DATAVENCIMENTO";
            // 
            // dtDataVencimento
            // 
            this.dtDataVencimento.EditValue = null;
            this.dtDataVencimento.Location = new System.Drawing.Point(115, 92);
            this.dtDataVencimento.Name = "dtDataVencimento";
            this.dtDataVencimento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDataVencimento.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDataVencimento.Size = new System.Drawing.Size(100, 20);
            this.dtDataVencimento.TabIndex = 3;
            // 
            // btnExecutar
            // 
            this.btnExecutar.Location = new System.Drawing.Point(373, 11);
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Size = new System.Drawing.Size(75, 23);
            this.btnExecutar.TabIndex = 13;
            this.btnExecutar.Text = "Executar";
            this.btnExecutar.Click += new System.EventHandler(this.btnExecutar_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.Location = new System.Drawing.Point(452, 11);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(75, 23);
            this.btnFechar.TabIndex = 12;
            this.btnFechar.Text = "Cancelar";
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // txtNumero
            // 
            this.txtNumero.Enabled = false;
            this.txtNumero.Location = new System.Drawing.Point(9, 92);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(100, 20);
            this.txtNumero.TabIndex = 17;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 73);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(43, 13);
            this.labelControl1.TabIndex = 16;
            this.labelControl1.Text = "NUMERO";
            // 
            // psLookup5
            // 
            this.psLookup5.Caption = "CODTIPDOC";
            this.psLookup5.Chave = true;
            this.psLookup5.DataField = "CODTIPDOC";
            this.psLookup5.Description = "";
            this.psLookup5.DinamicTable = null;
            this.psLookup5.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup5.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup5.KeyField = "CODTIPDOC";
            this.psLookup5.Location = new System.Drawing.Point(9, 29);
            this.psLookup5.LookupField = "CODTIPDOC;NOME";
            this.psLookup5.LookupFieldResult = "CODTIPDOC;NOME";
            this.psLookup5.MaxLength = 15;
            this.psLookup5.Name = "psLookup5";
            this.psLookup5.PSPart = null;
            this.psLookup5.Size = new System.Drawing.Size(514, 38);
            this.psLookup5.TabIndex = 15;
            this.psLookup5.ValorRetorno = null;
            this.psLookup5.AfterLookup += new PS.Lib.WinForms.PSLookup.AfterLookupDelegate(this.psLookup5_AfterLookup);
            // 
            // psLookup16
            // 
            this.psLookup16.Caption = "CODNATUREZAORCAMENTO";
            this.psLookup16.Chave = true;
            this.psLookup16.DataField = "CODNATUREZAORCAMENTO";
            this.psLookup16.Description = "";
            this.psLookup16.DinamicTable = null;
            this.psLookup16.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup16.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup16.KeyField = "CODNATUREZA";
            this.psLookup16.Location = new System.Drawing.Point(9, 210);
            this.psLookup16.LookupField = "CODNATUREZA;DESCRICAO";
            this.psLookup16.LookupFieldResult = "CODNATUREZA;DESCRICAO";
            this.psLookup16.MaxLength = 15;
            this.psLookup16.Name = "psLookup16";
            this.psLookup16.PSPart = null;
            this.psLookup16.Size = new System.Drawing.Size(514, 38);
            this.psLookup16.TabIndex = 12;
            this.psLookup16.ValorRetorno = null;
            // 
            // psLookup13
            // 
            this.psLookup13.Caption = "CODCCUSTO";
            this.psLookup13.Chave = true;
            this.psLookup13.DataField = "CODCCUSTO";
            this.psLookup13.Description = "";
            this.psLookup13.DinamicTable = null;
            this.psLookup13.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup13.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup13.KeyField = "CODCCUSTO";
            this.psLookup13.Location = new System.Drawing.Point(9, 166);
            this.psLookup13.LookupField = "CODCCUSTO;NOME";
            this.psLookup13.LookupFieldResult = "CODCCUSTO;NOME";
            this.psLookup13.MaxLength = 15;
            this.psLookup13.Name = "psLookup13";
            this.psLookup13.PSPart = null;
            this.psLookup13.Size = new System.Drawing.Size(514, 38);
            this.psLookup13.TabIndex = 11;
            this.psLookup13.ValorRetorno = null;
            // 
            // frmFaturaFinanceiro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 385);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFaturaFinanceiro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gera Fatura";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtobservacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVlOriginal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDataVencimento.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDataVencimento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumero.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.SimpleButton btnExecutar;
        private DevExpress.XtraEditors.SimpleButton btnFechar;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dtDataVencimento;
        private DevExpress.XtraEditors.TextEdit txtVlOriginal;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private Lib.WinForms.PSLookup psLookup16;
        private Lib.WinForms.PSLookup psLookup13;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtobservacao;
        private Lib.WinForms.PSLookup psLookup5;
        private DevExpress.XtraEditors.TextEdit txtNumero;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}