namespace PS.Glb.New.NotaFiscal
{
    partial class frmInutilizacao
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
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao1 = new ITGProducao.Controles.Newlookup_WhereVisao();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInutilizacao));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cbSerie = new System.Windows.Forms.ComboBox();
            this.dteInicial = new DevExpress.XtraEditors.DateEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btnCarregar = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lbEvento = new DevExpress.XtraEditors.LabelControl();
            this.tbObservacao = new DevExpress.XtraEditors.MemoEdit();
            this.tbFaixaInicial = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.tbModelo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lpFilial = new ITGProducao.Controles.NewLookup();
            this.btnExecutar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::PS.Glb.ERP.Global.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteInicial.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteInicial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbObservacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFaixaInicial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbModelo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
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
            this.splitContainer1.Panel2.Controls.Add(this.btnCancelar);
            this.splitContainer1.Size = new System.Drawing.Size(524, 381);
            this.splitContainer1.SplitterDistance = 340;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(524, 340);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cbSerie);
            this.tabPage1.Controls.Add(this.dteInicial);
            this.tabPage1.Controls.Add(this.labelControl5);
            this.tabPage1.Controls.Add(this.btnCarregar);
            this.tabPage1.Controls.Add(this.gridControl1);
            this.tabPage1.Controls.Add(this.lbEvento);
            this.tabPage1.Controls.Add(this.tbObservacao);
            this.tabPage1.Controls.Add(this.tbFaixaInicial);
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.tbModelo);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.lpFilial);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(516, 314);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Inutilização";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cbSerie
            // 
            this.cbSerie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSerie.FormattingEnabled = true;
            this.cbSerie.Location = new System.Drawing.Point(13, 75);
            this.cbSerie.Name = "cbSerie";
            this.cbSerie.Size = new System.Drawing.Size(98, 21);
            this.cbSerie.TabIndex = 45;
            // 
            // dteInicial
            // 
            this.dteInicial.EditValue = null;
            this.dteInicial.Location = new System.Drawing.Point(117, 77);
            this.dteInicial.Name = "dteInicial";
            this.dteInicial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteInicial.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteInicial.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            this.dteInicial.Properties.DisplayFormat.FormatString = "y";
            this.dteInicial.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteInicial.Properties.EditFormat.FormatString = "y";
            this.dteInicial.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteInicial.Properties.Mask.EditMask = "y";
            this.dteInicial.Properties.TodayDate = new System.DateTime(2018, 6, 4, 0, 0, 0, 0);
            this.dteInicial.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.dteInicial.Size = new System.Drawing.Size(98, 20);
            this.dteInicial.TabIndex = 44;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(117, 58);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(56, 13);
            this.labelControl5.TabIndex = 43;
            this.labelControl5.Text = "Data Inicial ";
            // 
            // btnCarregar
            // 
            this.btnCarregar.Location = new System.Drawing.Point(429, 75);
            this.btnCarregar.Name = "btnCarregar";
            this.btnCarregar.Size = new System.Drawing.Size(79, 23);
            this.btnCarregar.TabIndex = 42;
            this.btnCarregar.Text = "Carregar";
            this.btnCarregar.Click += new System.EventHandler(this.btnCarregar_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(13, 163);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(495, 145);
            this.gridControl1.TabIndex = 24;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.DoubleClick += new System.EventHandler(this.gridControl1_DoubleClick);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsFind.FindDelay = 100;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // lbEvento
            // 
            this.lbEvento.Location = new System.Drawing.Point(13, 103);
            this.lbEvento.Name = "lbEvento";
            this.lbEvento.Size = new System.Drawing.Size(58, 13);
            this.lbEvento.TabIndex = 23;
            this.lbEvento.Text = "Observacão";
            // 
            // tbObservacao
            // 
            this.tbObservacao.Location = new System.Drawing.Point(13, 122);
            this.tbObservacao.Name = "tbObservacao";
            this.tbObservacao.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
            this.tbObservacao.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.tbObservacao.Size = new System.Drawing.Size(495, 35);
            this.tbObservacao.TabIndex = 22;
            // 
            // tbFaixaInicial
            // 
            this.tbFaixaInicial.Location = new System.Drawing.Point(221, 77);
            this.tbFaixaInicial.Name = "tbFaixaInicial";
            this.tbFaixaInicial.Size = new System.Drawing.Size(98, 20);
            this.tbFaixaInicial.TabIndex = 21;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(221, 58);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(37, 13);
            this.labelControl4.TabIndex = 20;
            this.labelControl4.Text = "Número";
            // 
            // tbModelo
            // 
            this.tbModelo.Location = new System.Drawing.Point(325, 77);
            this.tbModelo.Name = "tbModelo";
            this.tbModelo.Size = new System.Drawing.Size(98, 20);
            this.tbModelo.TabIndex = 17;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(325, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(34, 13);
            this.labelControl2.TabIndex = 16;
            this.labelControl2.Text = "Modelo";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 58);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 13);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "Série";
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
            newlookup_WhereVisao1.NomeColuna = "CODFILIAL";
            newlookup_WhereVisao1.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao1.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.CampoCodigo_NewLookup;
            newlookup_WhereVisao1.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao1.ValorFixo = null;
            newlookup_WhereVisao1.Variavel_Interna = true;
            this.lpFilial.Grid_WhereVisao.Add(newlookup_WhereVisao1);
            this.lpFilial.Location = new System.Drawing.Point(8, 6);
            this.lpFilial.MensagemCodigoVazio = null;
            this.lpFilial.mensagemErrorProvider = null;
            this.lpFilial.Name = "lpFilial";
            this.lpFilial.Projeto_Formularios = "ITGProducao";
            this.lpFilial.Size = new System.Drawing.Size(500, 46);
            this.lpFilial.TabelaBD = "GFILIAL";
            this.lpFilial.TabIndex = 13;
            this.lpFilial.Titulo = "Filial";
            this.lpFilial.ValorCodigoInterno = "";
            this.lpFilial.whereParametros = null;
            this.lpFilial.whereVisao = null;
            this.lpFilial.Leave += new System.EventHandler(this.lpFilial_Leave);
            // 
            // btnExecutar
            // 
            this.btnExecutar.Location = new System.Drawing.Point(364, 5);
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Size = new System.Drawing.Size(75, 23);
            this.btnExecutar.TabIndex = 41;
            this.btnExecutar.Text = "Executar";
            this.btnExecutar.Click += new System.EventHandler(this.btnExecutar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(445, 5);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 40;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // frmInutilizacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 381);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmInutilizacao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inutilização de Nota Fiscal";
            this.Load += new System.EventHandler(this.frmInutilizacao_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteInicial.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteInicial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbObservacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFaixaInicial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbModelo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton btnExecutar;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ITGProducao.Controles.NewLookup lpFilial;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit tbFaixaInicial;
        private DevExpress.XtraEditors.TextEdit tbModelo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lbEvento;
        private DevExpress.XtraEditors.MemoEdit tbObservacao;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnCarregar;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.DateEdit dteInicial;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private System.Windows.Forms.ComboBox cbSerie;
    }
}