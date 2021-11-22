namespace PS.Glb.New.Filtro
{
    partial class frmFiltroEstoqueMinimo
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
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao2 = new ITGProducao.Controles.Newlookup_WhereVisao();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFiltroEstoqueMinimo));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dtPeriodo2 = new DevExpress.XtraEditors.DateEdit();
            this.dtPeriodo3 = new DevExpress.XtraEditors.DateEdit();
            this.dtPeriodo4 = new DevExpress.XtraEditors.DateEdit();
            this.dtPeriodo1 = new DevExpress.XtraEditors.DateEdit();
            this.lpFilial = new ITGProducao.Controles.NewLookup();
            this.btnFechar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalvar = new DevExpress.XtraEditors.SimpleButton();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::PS.Glb.ERP.Global.WaitForm1), false, true);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtPeriodo2.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPeriodo2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPeriodo3.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPeriodo3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPeriodo4.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPeriodo4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPeriodo1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPeriodo1.Properties)).BeginInit();
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
            this.splitContainer1.Panel2.Controls.Add(this.btnSalvar);
            this.splitContainer1.Size = new System.Drawing.Size(509, 223);
            this.splitContainer1.SplitterDistance = 181;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(509, 181);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.lpFilial);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(501, 155);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelControl5);
            this.groupBox1.Controls.Add(this.labelControl4);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.dtPeriodo2);
            this.groupBox1.Controls.Add(this.dtPeriodo3);
            this.groupBox1.Controls.Add(this.dtPeriodo4);
            this.groupBox1.Controls.Add(this.dtPeriodo1);
            this.groupBox1.Location = new System.Drawing.Point(9, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(456, 81);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Período";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(346, 20);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(53, 13);
            this.labelControl5.TabIndex = 6;
            this.labelControl5.Text = "Data Inicial";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(110, 42);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(4, 13);
            this.labelControl4.TabIndex = 2;
            this.labelControl4.Text = "-";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(336, 42);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(4, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "-";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(223, 42);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(4, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "-";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(7, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Data Final";
            // 
            // dtPeriodo2
            // 
            this.dtPeriodo2.EditValue = null;
            this.dtPeriodo2.Location = new System.Drawing.Point(120, 39);
            this.dtPeriodo2.Name = "dtPeriodo2";
            this.dtPeriodo2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtPeriodo2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtPeriodo2.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            this.dtPeriodo2.Properties.DisplayFormat.FormatString = "MM/yyyy";
            this.dtPeriodo2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtPeriodo2.Properties.Mask.EditMask = "MM/yyyy";
            this.dtPeriodo2.Properties.VistaCalendarViewStyle = ((DevExpress.XtraEditors.VistaCalendarViewStyle)((DevExpress.XtraEditors.VistaCalendarViewStyle.MonthView | DevExpress.XtraEditors.VistaCalendarViewStyle.YearView)));
            this.dtPeriodo2.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.dtPeriodo2.Size = new System.Drawing.Size(97, 20);
            this.dtPeriodo2.TabIndex = 3;
            this.dtPeriodo2.EditValueChanged += new System.EventHandler(this.dtPeriodo2_EditValueChanged);
            // 
            // dtPeriodo3
            // 
            this.dtPeriodo3.EditValue = null;
            this.dtPeriodo3.Location = new System.Drawing.Point(233, 39);
            this.dtPeriodo3.Name = "dtPeriodo3";
            this.dtPeriodo3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtPeriodo3.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtPeriodo3.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            this.dtPeriodo3.Properties.DisplayFormat.FormatString = "MM/yyyy";
            this.dtPeriodo3.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtPeriodo3.Properties.Mask.EditMask = "MM/yyyy";
            this.dtPeriodo3.Properties.VistaCalendarViewStyle = ((DevExpress.XtraEditors.VistaCalendarViewStyle)((DevExpress.XtraEditors.VistaCalendarViewStyle.MonthView | DevExpress.XtraEditors.VistaCalendarViewStyle.YearView)));
            this.dtPeriodo3.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.dtPeriodo3.Size = new System.Drawing.Size(97, 20);
            this.dtPeriodo3.TabIndex = 5;
            this.dtPeriodo3.EditValueChanged += new System.EventHandler(this.dtPeriodo3_EditValueChanged);
            // 
            // dtPeriodo4
            // 
            this.dtPeriodo4.EditValue = null;
            this.dtPeriodo4.Location = new System.Drawing.Point(346, 39);
            this.dtPeriodo4.Name = "dtPeriodo4";
            this.dtPeriodo4.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtPeriodo4.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtPeriodo4.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            this.dtPeriodo4.Properties.DisplayFormat.FormatString = "MM/yyyy";
            this.dtPeriodo4.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtPeriodo4.Properties.Mask.EditMask = "MM/yyyy";
            this.dtPeriodo4.Properties.VistaCalendarViewStyle = ((DevExpress.XtraEditors.VistaCalendarViewStyle)((DevExpress.XtraEditors.VistaCalendarViewStyle.MonthView | DevExpress.XtraEditors.VistaCalendarViewStyle.YearView)));
            this.dtPeriodo4.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.dtPeriodo4.Size = new System.Drawing.Size(97, 20);
            this.dtPeriodo4.TabIndex = 7;
            this.dtPeriodo4.EditValueChanged += new System.EventHandler(this.dtPeriodo4_EditValueChanged);
            // 
            // dtPeriodo1
            // 
            this.dtPeriodo1.EditValue = null;
            this.dtPeriodo1.Location = new System.Drawing.Point(7, 39);
            this.dtPeriodo1.Name = "dtPeriodo1";
            this.dtPeriodo1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtPeriodo1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtPeriodo1.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            this.dtPeriodo1.Properties.DisplayFormat.FormatString = "MM/yyyy";
            this.dtPeriodo1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dtPeriodo1.Properties.Mask.EditMask = "MM/yyyy";
            this.dtPeriodo1.Properties.VistaCalendarViewStyle = ((DevExpress.XtraEditors.VistaCalendarViewStyle)((DevExpress.XtraEditors.VistaCalendarViewStyle.MonthView | DevExpress.XtraEditors.VistaCalendarViewStyle.YearView)));
            this.dtPeriodo1.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.dtPeriodo1.Size = new System.Drawing.Size(97, 20);
            this.dtPeriodo1.TabIndex = 1;
            this.dtPeriodo1.EditValueChanged += new System.EventHandler(this.dtPeriodo1_EditValueChanged);
            // 
            // lpFilial
            // 
            this.lpFilial.Abrir_Primeiro = ITGProducao.Controles.NewLookup.AbrirPrimeiro.Visao;
            this.lpFilial.CampoCodigo_Igual_a = ITGProducao.Controles.NewLookup.CampoCodigoIguala.CampoCodigoBD;
            this.lpFilial.CampoCodigoBD = "CODFILIAL";
            this.lpFilial.CampoCodigoInterno = null;
            this.lpFilial.CampoDescricaoBD = "NOME";
            this.lpFilial.CarregaDescricaoSemFiltro = true;
            this.lpFilial.Codigo_MaxLenght = 10;
            this.lpFilial.Formulario_Cadastro = null;
            this.lpFilial.Formulario_Filtro = null;
            this.lpFilial.Formulario_Visao = "ITGProducao.Visao.FrmVisaoFiliais";
            newlookup_WhereVisao2.NomeColuna = "CODFILIAL";
            newlookup_WhereVisao2.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao2.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.CampoCodigo_NewLookup;
            newlookup_WhereVisao2.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao2.ValorFixo = null;
            newlookup_WhereVisao2.Variavel_Interna = true;
            this.lpFilial.Grid_WhereVisao.Add(newlookup_WhereVisao2);
            this.lpFilial.Location = new System.Drawing.Point(8, 6);
            this.lpFilial.MensagemCodigoVazio = null;
            this.lpFilial.mensagemErrorProvider = null;
            this.lpFilial.Name = "lpFilial";
            this.lpFilial.Projeto_Formularios = "ITGProducao";
            this.lpFilial.Size = new System.Drawing.Size(485, 46);
            this.lpFilial.TabelaBD = "GFILIAL";
            this.lpFilial.TabIndex = 0;
            this.lpFilial.Titulo = "Filial";
            this.lpFilial.ValorCodigoInterno = null;
            this.lpFilial.whereParametros = null;
            this.lpFilial.whereVisao = null;
            // 
            // btnFechar
            // 
            this.btnFechar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnFechar.ImageOptions.Image")));
            this.btnFechar.Location = new System.Drawing.Point(430, 2);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(75, 29);
            this.btnFechar.TabIndex = 1;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvar.ImageOptions.Image")));
            this.btnSalvar.Location = new System.Drawing.Point(343, 2);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(81, 29);
            this.btnSalvar.TabIndex = 0;
            this.btnSalvar.Text = "Executar";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // frmFiltroEstoqueMinimo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 223);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmFiltroEstoqueMinimo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtro de Estoque Mínimo";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtPeriodo2.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPeriodo2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPeriodo3.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPeriodo3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPeriodo4.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPeriodo4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPeriodo1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtPeriodo1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.SimpleButton btnFechar;
        private DevExpress.XtraEditors.SimpleButton btnSalvar;
        private ITGProducao.Controles.NewLookup lpFilial;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.DateEdit dtPeriodo1;
        private DevExpress.XtraEditors.DateEdit dtPeriodo2;
        private DevExpress.XtraEditors.DateEdit dtPeriodo3;
        private DevExpress.XtraEditors.DateEdit dtPeriodo4;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}