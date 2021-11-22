namespace PS.Glb.New.Cadastros
{
    partial class frmCadastroParcelamento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastroParcelamento));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grpParcelamento = new System.Windows.Forms.GroupBox();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.dtVencimento = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtNumeroDocumento = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtValorLiquidoAtual = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtQtdeParcelas = new DevExpress.XtraEditors.TextEdit();
            this.btnGerarParcelas = new DevExpress.XtraEditors.SimpleButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageParcelamento = new System.Windows.Forms.TabPage();
            this.gridParcelas = new DevExpress.XtraGrid.GridControl();
            this.gridViewParcelas = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnCancelarAtual = new DevExpress.XtraEditors.SimpleButton();
            this.btnOKAtual = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalvarAtual = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpParcelamento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtVencimento.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtVencimento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroDocumento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValorLiquidoAtual.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQtdeParcelas.Properties)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageParcelamento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridParcelas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewParcelas)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.grpParcelamento);
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCancelarAtual);
            this.splitContainer1.Panel2.Controls.Add(this.btnOKAtual);
            this.splitContainer1.Panel2.Controls.Add(this.btnSalvarAtual);
            this.splitContainer1.Size = new System.Drawing.Size(694, 538);
            this.splitContainer1.SplitterDistance = 444;
            this.splitContainer1.TabIndex = 0;
            // 
            // grpParcelamento
            // 
            this.grpParcelamento.Controls.Add(this.labelControl4);
            this.grpParcelamento.Controls.Add(this.dtVencimento);
            this.grpParcelamento.Controls.Add(this.labelControl3);
            this.grpParcelamento.Controls.Add(this.txtNumeroDocumento);
            this.grpParcelamento.Controls.Add(this.labelControl2);
            this.grpParcelamento.Controls.Add(this.txtValorLiquidoAtual);
            this.grpParcelamento.Controls.Add(this.labelControl1);
            this.grpParcelamento.Controls.Add(this.txtQtdeParcelas);
            this.grpParcelamento.Controls.Add(this.btnGerarParcelas);
            this.grpParcelamento.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpParcelamento.Location = new System.Drawing.Point(0, 0);
            this.grpParcelamento.Name = "grpParcelamento";
            this.grpParcelamento.Size = new System.Drawing.Size(694, 100);
            this.grpParcelamento.TabIndex = 1;
            this.grpParcelamento.TabStop = false;
            this.grpParcelamento.Text = "Lançamento Selecionado";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(240, 27);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(55, 13);
            this.labelControl4.TabIndex = 18;
            this.labelControl4.Text = "Vencimento";
            // 
            // dtVencimento
            // 
            this.dtVencimento.EditValue = null;
            this.dtVencimento.Enabled = false;
            this.dtVencimento.Location = new System.Drawing.Point(240, 44);
            this.dtVencimento.Name = "dtVencimento";
            this.dtVencimento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtVencimento.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtVencimento.Size = new System.Drawing.Size(111, 20);
            this.dtVencimento.TabIndex = 17;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(28, 27);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(94, 13);
            this.labelControl3.TabIndex = 16;
            this.labelControl3.Text = "Número Documento";
            // 
            // txtNumeroDocumento
            // 
            this.txtNumeroDocumento.Enabled = false;
            this.txtNumeroDocumento.Location = new System.Drawing.Point(28, 44);
            this.txtNumeroDocumento.Name = "txtNumeroDocumento";
            this.txtNumeroDocumento.Size = new System.Drawing.Size(100, 20);
            this.txtNumeroDocumento.TabIndex = 15;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(134, 27);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(88, 13);
            this.labelControl2.TabIndex = 14;
            this.labelControl2.Text = "Valor Liquido Atual";
            // 
            // txtValorLiquidoAtual
            // 
            this.txtValorLiquidoAtual.Enabled = false;
            this.txtValorLiquidoAtual.Location = new System.Drawing.Point(134, 44);
            this.txtValorLiquidoAtual.Name = "txtValorLiquidoAtual";
            this.txtValorLiquidoAtual.Properties.Mask.EditMask = "n2";
            this.txtValorLiquidoAtual.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtValorLiquidoAtual.Size = new System.Drawing.Size(100, 20);
            this.txtValorLiquidoAtual.TabIndex = 13;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(569, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(71, 13);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "Qtde. Parcelas";
            // 
            // txtQtdeParcelas
            // 
            this.txtQtdeParcelas.Location = new System.Drawing.Point(569, 44);
            this.txtQtdeParcelas.Name = "txtQtdeParcelas";
            this.txtQtdeParcelas.Properties.Mask.EditMask = "n0";
            this.txtQtdeParcelas.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtQtdeParcelas.Properties.MaxLength = 3;
            this.txtQtdeParcelas.Size = new System.Drawing.Size(100, 20);
            this.txtQtdeParcelas.TabIndex = 8;
            // 
            // btnGerarParcelas
            // 
            this.btnGerarParcelas.Location = new System.Drawing.Point(569, 71);
            this.btnGerarParcelas.Name = "btnGerarParcelas";
            this.btnGerarParcelas.Size = new System.Drawing.Size(100, 23);
            this.btnGerarParcelas.TabIndex = 7;
            this.btnGerarParcelas.Text = "Gerar Parcelas";
            this.btnGerarParcelas.Click += new System.EventHandler(this.btnGerarParcelas_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageParcelamento);
            this.tabControl1.Location = new System.Drawing.Point(6, 106);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(685, 335);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageParcelamento
            // 
            this.tabPageParcelamento.Controls.Add(this.gridParcelas);
            this.tabPageParcelamento.Location = new System.Drawing.Point(4, 22);
            this.tabPageParcelamento.Name = "tabPageParcelamento";
            this.tabPageParcelamento.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageParcelamento.Size = new System.Drawing.Size(677, 309);
            this.tabPageParcelamento.TabIndex = 1;
            this.tabPageParcelamento.Text = "Parcelamento";
            this.tabPageParcelamento.UseVisualStyleBackColor = true;
            // 
            // gridParcelas
            // 
            this.gridParcelas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridParcelas.Location = new System.Drawing.Point(3, 3);
            this.gridParcelas.MainView = this.gridViewParcelas;
            this.gridParcelas.Name = "gridParcelas";
            this.gridParcelas.Size = new System.Drawing.Size(671, 303);
            this.gridParcelas.TabIndex = 10;
            this.gridParcelas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewParcelas});
            // 
            // gridViewParcelas
            // 
            this.gridViewParcelas.GridControl = this.gridParcelas;
            this.gridViewParcelas.Name = "gridViewParcelas";
            this.gridViewParcelas.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewParcelas.OptionsFind.FindDelay = 100;
            this.gridViewParcelas.OptionsSelection.MultiSelect = true;
            this.gridViewParcelas.OptionsView.ColumnAutoWidth = false;
            this.gridViewParcelas.OptionsView.ShowAutoFilterRow = true;
            this.gridViewParcelas.OptionsView.ShowGroupPanel = false;
            this.gridViewParcelas.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewParcelas_FocusedRowChanged);
            this.gridViewParcelas.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewParcelas_CellValueChanging);
            this.gridViewParcelas.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridViewParcelas_RowUpdated);
            // 
            // btnCancelarAtual
            // 
            this.btnCancelarAtual.Location = new System.Drawing.Point(591, 26);
            this.btnCancelarAtual.Name = "btnCancelarAtual";
            this.btnCancelarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnCancelarAtual.TabIndex = 8;
            this.btnCancelarAtual.Text = "Cancelar";
            this.btnCancelarAtual.Click += new System.EventHandler(this.btnCancelarAtual_Click);
            // 
            // btnOKAtual
            // 
            this.btnOKAtual.Location = new System.Drawing.Point(510, 26);
            this.btnOKAtual.Name = "btnOKAtual";
            this.btnOKAtual.Size = new System.Drawing.Size(75, 23);
            this.btnOKAtual.TabIndex = 7;
            this.btnOKAtual.Text = "OK";
            this.btnOKAtual.Click += new System.EventHandler(this.btnOKAtual_Click);
            // 
            // btnSalvarAtual
            // 
            this.btnSalvarAtual.Location = new System.Drawing.Point(429, 26);
            this.btnSalvarAtual.Name = "btnSalvarAtual";
            this.btnSalvarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnSalvarAtual.TabIndex = 6;
            this.btnSalvarAtual.Text = "Salvar";
            this.btnSalvarAtual.Visible = false;
            this.btnSalvarAtual.Click += new System.EventHandler(this.btnSalvarAtual_Click);
            // 
            // frmCadastroParcelamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 538);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCadastroParcelamento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Parcelamento";
            this.Load += new System.EventHandler(this.frmCadastroParcelamento_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grpParcelamento.ResumeLayout(false);
            this.grpParcelamento.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtVencimento.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtVencimento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroDocumento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValorLiquidoAtual.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQtdeParcelas.Properties)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageParcelamento.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridParcelas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewParcelas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton btnCancelarAtual;
        private DevExpress.XtraEditors.SimpleButton btnOKAtual;
        private DevExpress.XtraEditors.SimpleButton btnSalvarAtual;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageParcelamento;
        private System.Windows.Forms.GroupBox grpParcelamento;
        private DevExpress.XtraEditors.SimpleButton btnGerarParcelas;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtQtdeParcelas;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtNumeroDocumento;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtValorLiquidoAtual;
        public DevExpress.XtraGrid.GridControl gridParcelas;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewParcelas;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.DateEdit dtVencimento;
    }
}