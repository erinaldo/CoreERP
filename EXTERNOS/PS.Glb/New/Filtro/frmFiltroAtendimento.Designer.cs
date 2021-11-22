
namespace PS.Glb.New.Filtro
{
    partial class frmFiltroAtendimento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFiltroAtendimento));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dteFinal = new DevExpress.XtraEditors.DateEdit();
            this.lblDataFinal = new DevExpress.XtraEditors.LabelControl();
            this.dteInicial = new DevExpress.XtraEditors.DateEdit();
            this.lblDataInicial = new DevExpress.XtraEditors.LabelControl();
            this.lblValor = new DevExpress.XtraEditors.LabelControl();
            this.cmbValorFiltro = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbContato = new System.Windows.Forms.RadioButton();
            this.rbUnidade = new System.Windows.Forms.RadioButton();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.rbPeriodo = new System.Windows.Forms.RadioButton();
            this.btnFechar = new DevExpress.XtraEditors.SimpleButton();
            this.btnExecutar = new DevExpress.XtraEditors.SimpleButton();
            this.rbHoje = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteFinal.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFinal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteInicial.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteInicial.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.splitContainer1.Panel2.Controls.Add(this.btnFechar);
            this.splitContainer1.Panel2.Controls.Add(this.btnExecutar);
            this.splitContainer1.Size = new System.Drawing.Size(316, 382);
            this.splitContainer1.SplitterDistance = 329;
            this.splitContainer1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(316, 329);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(308, 303);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dteFinal);
            this.groupBox2.Controls.Add(this.lblDataFinal);
            this.groupBox2.Controls.Add(this.dteInicial);
            this.groupBox2.Controls.Add(this.lblDataInicial);
            this.groupBox2.Controls.Add(this.lblValor);
            this.groupBox2.Controls.Add(this.cmbValorFiltro);
            this.groupBox2.Location = new System.Drawing.Point(8, 141);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(290, 149);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Valores";
            // 
            // dteFinal
            // 
            this.dteFinal.EditValue = null;
            this.dteFinal.Location = new System.Drawing.Point(147, 35);
            this.dteFinal.Name = "dteFinal";
            this.dteFinal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFinal.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFinal.Size = new System.Drawing.Size(135, 20);
            this.dteFinal.TabIndex = 12;
            this.dteFinal.Visible = false;
            // 
            // lblDataFinal
            // 
            this.lblDataFinal.Location = new System.Drawing.Point(147, 19);
            this.lblDataFinal.Name = "lblDataFinal";
            this.lblDataFinal.Size = new System.Drawing.Size(48, 13);
            this.lblDataFinal.TabIndex = 13;
            this.lblDataFinal.Text = "Data Final";
            this.lblDataFinal.Visible = false;
            // 
            // dteInicial
            // 
            this.dteInicial.EditValue = null;
            this.dteInicial.Location = new System.Drawing.Point(6, 35);
            this.dteInicial.Name = "dteInicial";
            this.dteInicial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteInicial.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteInicial.Size = new System.Drawing.Size(135, 20);
            this.dteInicial.TabIndex = 10;
            this.dteInicial.Visible = false;
            // 
            // lblDataInicial
            // 
            this.lblDataInicial.Location = new System.Drawing.Point(6, 19);
            this.lblDataInicial.Name = "lblDataInicial";
            this.lblDataInicial.Size = new System.Drawing.Size(53, 13);
            this.lblDataInicial.TabIndex = 11;
            this.lblDataInicial.Text = "Data Inicial";
            this.lblDataInicial.Visible = false;
            // 
            // lblValor
            // 
            this.lblValor.Location = new System.Drawing.Point(16, 61);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(24, 13);
            this.lblValor.TabIndex = 1;
            this.lblValor.Text = "Valor";
            this.lblValor.Visible = false;
            // 
            // cmbValorFiltro
            // 
            this.cmbValorFiltro.FormattingEnabled = true;
            this.cmbValorFiltro.Location = new System.Drawing.Point(16, 80);
            this.cmbValorFiltro.Name = "cmbValorFiltro";
            this.cmbValorFiltro.Size = new System.Drawing.Size(256, 21);
            this.cmbValorFiltro.TabIndex = 0;
            this.cmbValorFiltro.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbHoje);
            this.groupBox1.Controls.Add(this.rbContato);
            this.groupBox1.Controls.Add(this.rbUnidade);
            this.groupBox1.Controls.Add(this.rbTodos);
            this.groupBox1.Controls.Add(this.rbPeriodo);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 129);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // rbContato
            // 
            this.rbContato.AutoSize = true;
            this.rbContato.Location = new System.Drawing.Point(207, 56);
            this.rbContato.Name = "rbContato";
            this.rbContato.Size = new System.Drawing.Size(77, 17);
            this.rbContato.TabIndex = 9;
            this.rbContato.TabStop = true;
            this.rbContato.Text = "Atendente ";
            this.rbContato.UseVisualStyleBackColor = true;
            this.rbContato.CheckedChanged += new System.EventHandler(this.rbContato_CheckedChanged);
            // 
            // rbUnidade
            // 
            this.rbUnidade.AutoSize = true;
            this.rbUnidade.Location = new System.Drawing.Point(207, 33);
            this.rbUnidade.Name = "rbUnidade";
            this.rbUnidade.Size = new System.Drawing.Size(65, 17);
            this.rbUnidade.TabIndex = 8;
            this.rbUnidade.TabStop = true;
            this.rbUnidade.Text = "Unidade";
            this.rbUnidade.UseVisualStyleBackColor = true;
            this.rbUnidade.CheckedChanged += new System.EventHandler(this.rbUnidade_CheckedChanged);
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Location = new System.Drawing.Point(6, 33);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(55, 17);
            this.rbTodos.TabIndex = 6;
            this.rbTodos.TabStop = true;
            this.rbTodos.Text = "Todos";
            this.rbTodos.UseVisualStyleBackColor = true;
            this.rbTodos.CheckedChanged += new System.EventHandler(this.rbTodos_CheckedChanged);
            // 
            // rbPeriodo
            // 
            this.rbPeriodo.AutoSize = true;
            this.rbPeriodo.Location = new System.Drawing.Point(6, 56);
            this.rbPeriodo.Name = "rbPeriodo";
            this.rbPeriodo.Size = new System.Drawing.Size(63, 17);
            this.rbPeriodo.TabIndex = 0;
            this.rbPeriodo.TabStop = true;
            this.rbPeriodo.Text = "Período";
            this.rbPeriodo.UseVisualStyleBackColor = true;
            this.rbPeriodo.CheckedChanged += new System.EventHandler(this.rbPeriodo_CheckedChanged);
            // 
            // btnFechar
            // 
            this.btnFechar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnFechar.ImageOptions.Image")));
            this.btnFechar.Location = new System.Drawing.Point(229, 8);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(75, 29);
            this.btnFechar.TabIndex = 19;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // btnExecutar
            // 
            this.btnExecutar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExecutar.ImageOptions.Image")));
            this.btnExecutar.Location = new System.Drawing.Point(142, 8);
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Size = new System.Drawing.Size(81, 29);
            this.btnExecutar.TabIndex = 18;
            this.btnExecutar.Text = "Executar";
            this.btnExecutar.Click += new System.EventHandler(this.btnExecutar_Click);
            // 
            // rbHoje
            // 
            this.rbHoje.AutoSize = true;
            this.rbHoje.Location = new System.Drawing.Point(6, 79);
            this.rbHoje.Name = "rbHoje";
            this.rbHoje.Size = new System.Drawing.Size(47, 17);
            this.rbHoje.TabIndex = 10;
            this.rbHoje.TabStop = true;
            this.rbHoje.Text = "Hoje";
            this.rbHoje.UseVisualStyleBackColor = true;
            this.rbHoje.CheckedChanged += new System.EventHandler(this.rbHoje_CheckedChanged);
            // 
            // frmFiltroAtendimento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 382);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmFiltroAtendimento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtro de Atendimento";
            this.Load += new System.EventHandler(this.frmFiltroAtendimento_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteFinal.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFinal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteInicial.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteInicial.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.DateEdit dteInicial;
        private DevExpress.XtraEditors.LabelControl lblDataInicial;
        private DevExpress.XtraEditors.LabelControl lblValor;
        private System.Windows.Forms.ComboBox cmbValorFiltro;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbUnidade;
        private System.Windows.Forms.RadioButton rbTodos;
        private System.Windows.Forms.RadioButton rbPeriodo;
        private DevExpress.XtraEditors.SimpleButton btnFechar;
        private DevExpress.XtraEditors.SimpleButton btnExecutar;
        private DevExpress.XtraEditors.DateEdit dteFinal;
        private DevExpress.XtraEditors.LabelControl lblDataFinal;
        private System.Windows.Forms.RadioButton rbContato;
        private System.Windows.Forms.RadioButton rbHoje;
    }
}