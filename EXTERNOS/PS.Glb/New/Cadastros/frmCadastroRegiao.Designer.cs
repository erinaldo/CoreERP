namespace PS.Glb.New.Cadastros
{
    partial class frmCadastroRegiao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastroRegiao));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbDescricao = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.tbCodRegiao = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnNovo1 = new System.Windows.Forms.ToolStripButton();
            this.btnEditar1 = new System.Windows.Forms.ToolStripButton();
            this.btnExcluir1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPesquisar1 = new System.Windows.Forms.ToolStripButton();
            this.btnAgrupar1 = new System.Windows.Forms.ToolStripButton();
            this.btnFiltros1 = new System.Windows.Forms.ToolStripButton();
            this.btnVisao1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnAtualizar1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSelecionarColunas1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSalvarLayout1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAnexos1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnProcessos1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnExportar1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNovo = new System.Windows.Forms.ToolStripButton();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.btnExcluir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPesquisar = new System.Windows.Forms.ToolStripButton();
            this.btnAgrupar = new System.Windows.Forms.ToolStripButton();
            this.btnFiltros = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnAtualizar = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSelecionarColunas = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSalvarLayout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnProdutoTributo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButton4 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancelarAtual = new DevExpress.XtraEditors.SimpleButton();
            this.btnOKAtual = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalvarAtual = new DevExpress.XtraEditors.SimpleButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbCodRegiao.Properties)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCancelarAtual);
            this.splitContainer1.Panel2.Controls.Add(this.btnOKAtual);
            this.splitContainer1.Panel2.Controls.Add(this.btnSalvarAtual);
            this.splitContainer1.Size = new System.Drawing.Size(751, 308);
            this.splitContainer1.SplitterDistance = 261;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 29);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(751, 232);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbDescricao);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.tbCodRegiao);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(743, 206);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Região";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbDescricao
            // 
            this.tbDescricao.Location = new System.Drawing.Point(9, 81);
            this.tbDescricao.Name = "tbDescricao";
            this.tbDescricao.Size = new System.Drawing.Size(258, 20);
            this.tbDescricao.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(9, 62);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(59, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "DESCRICAO";
            // 
            // tbCodRegiao
            // 
            this.tbCodRegiao.Location = new System.Drawing.Point(9, 36);
            this.tbCodRegiao.Name = "tbCodRegiao";
            this.tbCodRegiao.Size = new System.Drawing.Size(117, 20);
            this.tbCodRegiao.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 16);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "CODREGIAO";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gridControl1);
            this.tabPage2.Controls.Add(this.toolStrip2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(743, 186);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Região/Estado";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(3, 33);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(737, 150);
            this.gridControl1.TabIndex = 12;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
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
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNovo1,
            this.btnEditar1,
            this.btnExcluir1,
            this.toolStripSeparator3,
            this.btnPesquisar1,
            this.btnAgrupar1,
            this.btnFiltros1,
            this.btnVisao1,
            this.toolStripSeparator4,
            this.btnAnexos1,
            this.btnProcessos1,
            this.btnExportar1});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip2.Size = new System.Drawing.Size(737, 30);
            this.toolStrip2.TabIndex = 11;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnNovo1
            // 
            this.btnNovo1.Image = ((System.Drawing.Image)(resources.GetObject("btnNovo1.Image")));
            this.btnNovo1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNovo1.Name = "btnNovo1";
            this.btnNovo1.Size = new System.Drawing.Size(58, 27);
            this.btnNovo1.Text = "Novo";
            this.btnNovo1.Click += new System.EventHandler(this.btnNovo1_Click);
            // 
            // btnEditar1
            // 
            this.btnEditar1.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar1.Image")));
            this.btnEditar1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar1.Name = "btnEditar1";
            this.btnEditar1.Size = new System.Drawing.Size(61, 27);
            this.btnEditar1.Text = "Editar";
            // 
            // btnExcluir1
            // 
            this.btnExcluir1.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluir1.Image")));
            this.btnExcluir1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcluir1.Name = "btnExcluir1";
            this.btnExcluir1.Size = new System.Drawing.Size(64, 27);
            this.btnExcluir1.Text = "Excluir";
            this.btnExcluir1.Click += new System.EventHandler(this.btnExcluir1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 30);
            // 
            // btnPesquisar1
            // 
            this.btnPesquisar1.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisar1.Image")));
            this.btnPesquisar1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPesquisar1.Name = "btnPesquisar1";
            this.btnPesquisar1.Size = new System.Drawing.Size(79, 27);
            this.btnPesquisar1.Text = "Pesquisar";
            this.btnPesquisar1.Click += new System.EventHandler(this.btnPesquisar1_Click);
            // 
            // btnAgrupar1
            // 
            this.btnAgrupar1.Image = ((System.Drawing.Image)(resources.GetObject("btnAgrupar1.Image")));
            this.btnAgrupar1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAgrupar1.Name = "btnAgrupar1";
            this.btnAgrupar1.Size = new System.Drawing.Size(72, 27);
            this.btnAgrupar1.Text = "Agrupar";
            this.btnAgrupar1.Click += new System.EventHandler(this.btnAgrupar1_Click);
            // 
            // btnFiltros1
            // 
            this.btnFiltros1.Image = ((System.Drawing.Image)(resources.GetObject("btnFiltros1.Image")));
            this.btnFiltros1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFiltros1.Name = "btnFiltros1";
            this.btnFiltros1.Size = new System.Drawing.Size(62, 27);
            this.btnFiltros1.Text = "Filtros";
            // 
            // btnVisao1
            // 
            this.btnVisao1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAtualizar1,
            this.btnSelecionarColunas1,
            this.btnSalvarLayout1});
            this.btnVisao1.Image = ((System.Drawing.Image)(resources.GetObject("btnVisao1.Image")));
            this.btnVisao1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVisao1.Name = "btnVisao1";
            this.btnVisao1.Size = new System.Drawing.Size(67, 27);
            this.btnVisao1.Text = "Visão";
            // 
            // btnAtualizar1
            // 
            this.btnAtualizar1.Name = "btnAtualizar1";
            this.btnAtualizar1.Size = new System.Drawing.Size(164, 22);
            this.btnAtualizar1.Text = "Atualizar";
            this.btnAtualizar1.Click += new System.EventHandler(this.btnAtualizar1_Click);
            // 
            // btnSelecionarColunas1
            // 
            this.btnSelecionarColunas1.Name = "btnSelecionarColunas1";
            this.btnSelecionarColunas1.Size = new System.Drawing.Size(164, 22);
            this.btnSelecionarColunas1.Text = "Selecionar Colunas";
            this.btnSelecionarColunas1.Click += new System.EventHandler(this.btnSelecionarColunas1_Click);
            // 
            // btnSalvarLayout1
            // 
            this.btnSalvarLayout1.Name = "btnSalvarLayout1";
            this.btnSalvarLayout1.Size = new System.Drawing.Size(164, 22);
            this.btnSalvarLayout1.Text = "Salvar Layout";
            this.btnSalvarLayout1.Click += new System.EventHandler(this.btnSalvarLayout1_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 30);
            // 
            // btnAnexos1
            // 
            this.btnAnexos1.Image = ((System.Drawing.Image)(resources.GetObject("btnAnexos1.Image")));
            this.btnAnexos1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAnexos1.Name = "btnAnexos1";
            this.btnAnexos1.Size = new System.Drawing.Size(78, 27);
            this.btnAnexos1.Text = "Anexos";
            // 
            // btnProcessos1
            // 
            this.btnProcessos1.Image = ((System.Drawing.Image)(resources.GetObject("btnProcessos1.Image")));
            this.btnProcessos1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProcessos1.Name = "btnProcessos1";
            this.btnProcessos1.Size = new System.Drawing.Size(90, 27);
            this.btnProcessos1.Text = "Processos";
            // 
            // btnExportar1
            // 
            this.btnExportar1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4});
            this.btnExportar1.Image = ((System.Drawing.Image)(resources.GetObject("btnExportar1.Image")));
            this.btnExportar1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportar1.Name = "btnExportar1";
            this.btnExportar1.Size = new System.Drawing.Size(84, 27);
            this.btnExportar1.Text = "Exportar";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem4.Image")));
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(105, 28);
            this.toolStripMenuItem4.Text = "Excel";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(22, 22);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNovo,
            this.btnEditar,
            this.btnExcluir,
            this.toolStripSeparator1,
            this.btnPesquisar,
            this.btnAgrupar,
            this.btnFiltros,
            this.toolStripDropDownButton1,
            this.toolStripSeparator2,
            this.toolStripDropDownButton2,
            this.toolStripDropDownButton3,
            this.toolStripDropDownButton4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(751, 29);
            this.toolStrip1.TabIndex = 18;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNovo
            // 
            this.btnNovo.Image = ((System.Drawing.Image)(resources.GetObject("btnNovo.Image")));
            this.btnNovo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(58, 26);
            this.btnNovo.Text = "Novo";
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Enabled = false;
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(61, 26);
            this.btnEditar.Text = "Editar";
            // 
            // btnExcluir
            // 
            this.btnExcluir.Enabled = false;
            this.btnExcluir.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluir.Image")));
            this.btnExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(64, 26);
            this.btnExcluir.Text = "Excluir";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 29);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Enabled = false;
            this.btnPesquisar.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisar.Image")));
            this.btnPesquisar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(79, 26);
            this.btnPesquisar.Text = "Pesquisar";
            // 
            // btnAgrupar
            // 
            this.btnAgrupar.Enabled = false;
            this.btnAgrupar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgrupar.Image")));
            this.btnAgrupar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAgrupar.Name = "btnAgrupar";
            this.btnAgrupar.Size = new System.Drawing.Size(72, 26);
            this.btnAgrupar.Text = "Agrupar";
            // 
            // btnFiltros
            // 
            this.btnFiltros.Enabled = false;
            this.btnFiltros.Image = ((System.Drawing.Image)(resources.GetObject("btnFiltros.Image")));
            this.btnFiltros.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFiltros.Name = "btnFiltros";
            this.btnFiltros.Size = new System.Drawing.Size(62, 26);
            this.btnFiltros.Text = "Filtros";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAtualizar,
            this.btnSelecionarColunas,
            this.btnSalvarLayout});
            this.toolStripDropDownButton1.Enabled = false;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(67, 26);
            this.toolStripDropDownButton1.Text = "Visão";
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(164, 22);
            this.btnAtualizar.Text = "Atualizar";
            // 
            // btnSelecionarColunas
            // 
            this.btnSelecionarColunas.Name = "btnSelecionarColunas";
            this.btnSelecionarColunas.Size = new System.Drawing.Size(164, 22);
            this.btnSelecionarColunas.Text = "Selecionar Colunas";
            // 
            // btnSalvarLayout
            // 
            this.btnSalvarLayout.Name = "btnSalvarLayout";
            this.btnSalvarLayout.Size = new System.Drawing.Size(164, 22);
            this.btnSalvarLayout.Text = "Salvar Layout";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 29);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnProdutoTributo});
            this.toolStripDropDownButton2.Enabled = false;
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(78, 26);
            this.toolStripDropDownButton2.Text = "Anexos";
            // 
            // btnProdutoTributo
            // 
            this.btnProdutoTributo.Name = "btnProdutoTributo";
            this.btnProdutoTributo.Size = new System.Drawing.Size(149, 22);
            this.btnProdutoTributo.Text = "Produto Tributo";
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.Enabled = false;
            this.toolStripDropDownButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton3.Image")));
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(90, 26);
            this.toolStripDropDownButton3.Text = "Processos";
            // 
            // toolStripDropDownButton4
            // 
            this.toolStripDropDownButton4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportExcel});
            this.toolStripDropDownButton4.Enabled = false;
            this.toolStripDropDownButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton4.Image")));
            this.toolStripDropDownButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton4.Name = "toolStripDropDownButton4";
            this.toolStripDropDownButton4.Size = new System.Drawing.Size(84, 26);
            this.toolStripDropDownButton4.Text = "Exportar";
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.Image")));
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(105, 28);
            this.btnExportExcel.Text = "Excel";
            // 
            // btnCancelarAtual
            // 
            this.btnCancelarAtual.Location = new System.Drawing.Point(672, 7);
            this.btnCancelarAtual.Name = "btnCancelarAtual";
            this.btnCancelarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnCancelarAtual.TabIndex = 35;
            this.btnCancelarAtual.Text = "Cancelar";
            this.btnCancelarAtual.Click += new System.EventHandler(this.btnCancelarAtual_Click);
            // 
            // btnOKAtual
            // 
            this.btnOKAtual.Location = new System.Drawing.Point(582, 7);
            this.btnOKAtual.Name = "btnOKAtual";
            this.btnOKAtual.Size = new System.Drawing.Size(75, 23);
            this.btnOKAtual.TabIndex = 34;
            this.btnOKAtual.Text = "OK";
            this.btnOKAtual.Click += new System.EventHandler(this.btnOKAtual_Click);
            // 
            // btnSalvarAtual
            // 
            this.btnSalvarAtual.Location = new System.Drawing.Point(491, 7);
            this.btnSalvarAtual.Name = "btnSalvarAtual";
            this.btnSalvarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnSalvarAtual.TabIndex = 33;
            this.btnSalvarAtual.Text = "Salvar";
            this.btnSalvarAtual.Click += new System.EventHandler(this.btnSalvarAtual_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // frmCadastroRegiao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 308);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCadastroRegiao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Região";
            this.Load += new System.EventHandler(this.frmCadastroRegiao_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbCodRegiao.Properties)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNovo;
        private System.Windows.Forms.ToolStripButton btnEditar;
        private System.Windows.Forms.ToolStripButton btnExcluir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnPesquisar;
        private System.Windows.Forms.ToolStripButton btnAgrupar;
        private System.Windows.Forms.ToolStripButton btnFiltros;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem btnAtualizar;
        private System.Windows.Forms.ToolStripMenuItem btnSelecionarColunas;
        private System.Windows.Forms.ToolStripMenuItem btnSalvarLayout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem btnProdutoTributo;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton4;
        private System.Windows.Forms.ToolStripMenuItem btnExportExcel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.TextEdit tbDescricao;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit tbCodRegiao;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private DevExpress.XtraEditors.SimpleButton btnCancelarAtual;
        private DevExpress.XtraEditors.SimpleButton btnOKAtual;
        private DevExpress.XtraEditors.SimpleButton btnSalvarAtual;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnNovo1;
        private System.Windows.Forms.ToolStripButton btnEditar1;
        private System.Windows.Forms.ToolStripButton btnExcluir1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnPesquisar1;
        private System.Windows.Forms.ToolStripButton btnAgrupar1;
        private System.Windows.Forms.ToolStripButton btnFiltros1;
        private System.Windows.Forms.ToolStripDropDownButton btnVisao1;
        private System.Windows.Forms.ToolStripMenuItem btnAtualizar1;
        private System.Windows.Forms.ToolStripMenuItem btnSelecionarColunas1;
        private System.Windows.Forms.ToolStripMenuItem btnSalvarLayout1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripDropDownButton btnAnexos1;
        private System.Windows.Forms.ToolStripDropDownButton btnProcessos1;
        private System.Windows.Forms.ToolStripDropDownButton btnExportar1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}