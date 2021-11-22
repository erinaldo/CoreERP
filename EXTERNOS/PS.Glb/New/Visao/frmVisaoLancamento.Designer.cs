namespace PS.Glb.New.Visao
{
    partial class frmVisaoLancamento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVisaoLancamento));
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
            this.imprimirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnBaixaLancamento = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancelarBaixa = new System.Windows.Forms.ToolStripMenuItem();
            this.btnparcelarlancamento = new System.Windows.Forms.ToolStripMenuItem();
            this.btnlançamentoPadrao = new System.Windows.Forms.ToolStripMenuItem();
            this.btnVinculaLancamento = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancelarLancamento = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGeraFatura = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancelarFatura = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGeraBoleto = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton4 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lbTotal = new DevExpress.XtraEditors.LabelControl();
            this.lbTotalResult = new DevExpress.XtraEditors.LabelControl();
            this.lbResultRegistros = new DevExpress.XtraEditors.LabelControl();
            this.lbRegistrosSelecionados = new DevExpress.XtraEditors.LabelControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(983, 29);
            this.toolStrip1.TabIndex = 4;
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
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(61, 26);
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluir.Image")));
            this.btnExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(64, 26);
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 29);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisar.Image")));
            this.btnPesquisar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(79, 26);
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // btnAgrupar
            // 
            this.btnAgrupar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgrupar.Image")));
            this.btnAgrupar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAgrupar.Name = "btnAgrupar";
            this.btnAgrupar.Size = new System.Drawing.Size(72, 26);
            this.btnAgrupar.Text = "Agrupar";
            this.btnAgrupar.Click += new System.EventHandler(this.btnAgrupar_Click);
            // 
            // btnFiltros
            // 
            this.btnFiltros.Image = ((System.Drawing.Image)(resources.GetObject("btnFiltros.Image")));
            this.btnFiltros.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFiltros.Name = "btnFiltros";
            this.btnFiltros.Size = new System.Drawing.Size(62, 26);
            this.btnFiltros.Text = "Filtros";
            this.btnFiltros.Click += new System.EventHandler(this.btnFiltros_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAtualizar,
            this.btnSelecionarColunas,
            this.btnSalvarLayout});
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
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // btnSelecionarColunas
            // 
            this.btnSelecionarColunas.Name = "btnSelecionarColunas";
            this.btnSelecionarColunas.Size = new System.Drawing.Size(164, 22);
            this.btnSelecionarColunas.Text = "Selecionar Colunas";
            this.btnSelecionarColunas.Click += new System.EventHandler(this.btnSelecionarColunas_Click);
            // 
            // btnSalvarLayout
            // 
            this.btnSalvarLayout.Name = "btnSalvarLayout";
            this.btnSalvarLayout.Size = new System.Drawing.Size(164, 22);
            this.btnSalvarLayout.Text = "Salvar Layout";
            this.btnSalvarLayout.Click += new System.EventHandler(this.btnSalvarLayout_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 29);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imprimirToolStripMenuItem});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(78, 26);
            this.toolStripDropDownButton2.Text = "Anexos";
            // 
            // imprimirToolStripMenuItem
            // 
            this.imprimirToolStripMenuItem.Name = "imprimirToolStripMenuItem";
            this.imprimirToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.imprimirToolStripMenuItem.Text = "Imprimir Boleto";
            this.imprimirToolStripMenuItem.Click += new System.EventHandler(this.imprimirToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBaixaLancamento,
            this.btnCancelarBaixa,
            this.btnparcelarlancamento,
            this.btnlançamentoPadrao,
            this.btnVinculaLancamento,
            this.btnCancelarLancamento,
            this.toolStripSeparator3,
            this.btnGeraFatura,
            this.btnCancelarFatura,
            this.toolStripSeparator4,
            this.btnGeraBoleto});
            this.toolStripDropDownButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton3.Image")));
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(90, 26);
            this.toolStripDropDownButton3.Text = "Processos";
            // 
            // btnBaixaLancamento
            // 
            this.btnBaixaLancamento.Image = ((System.Drawing.Image)(resources.GetObject("btnBaixaLancamento.Image")));
            this.btnBaixaLancamento.Name = "btnBaixaLancamento";
            this.btnBaixaLancamento.Size = new System.Drawing.Size(183, 28);
            this.btnBaixaLancamento.Text = "Baixar Lançamento";
            this.btnBaixaLancamento.Click += new System.EventHandler(this.btnBaixaLancamento_Click);
            // 
            // btnCancelarBaixa
            // 
            this.btnCancelarBaixa.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelarBaixa.Image")));
            this.btnCancelarBaixa.Name = "btnCancelarBaixa";
            this.btnCancelarBaixa.Size = new System.Drawing.Size(183, 28);
            this.btnCancelarBaixa.Text = "Cancelar Baixa";
            this.btnCancelarBaixa.Click += new System.EventHandler(this.btnCancelarBaixa_Click);
            // 
            // btnparcelarlancamento
            // 
            this.btnparcelarlancamento.Name = "btnparcelarlancamento";
            this.btnparcelarlancamento.Size = new System.Drawing.Size(183, 28);
            this.btnparcelarlancamento.Text = "Parcelar Lançamento";
            this.btnparcelarlancamento.Click += new System.EventHandler(this.btnparcelarlancamento_Click);
            // 
            // btnlançamentoPadrao
            // 
            this.btnlançamentoPadrao.Name = "btnlançamentoPadrao";
            this.btnlançamentoPadrao.Size = new System.Drawing.Size(183, 28);
            this.btnlançamentoPadrao.Text = "Lançamento Padrão";
            this.btnlançamentoPadrao.Click += new System.EventHandler(this.btnlançamentoPadrao_Click);
            // 
            // btnVinculaLancamento
            // 
            this.btnVinculaLancamento.Name = "btnVinculaLancamento";
            this.btnVinculaLancamento.Size = new System.Drawing.Size(183, 28);
            this.btnVinculaLancamento.Text = "Vincular Lançamentos";
            this.btnVinculaLancamento.Click += new System.EventHandler(this.btnVinculaLancamento_Click);
            // 
            // btnCancelarLancamento
            // 
            this.btnCancelarLancamento.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelarLancamento.Image")));
            this.btnCancelarLancamento.Name = "btnCancelarLancamento";
            this.btnCancelarLancamento.Size = new System.Drawing.Size(183, 28);
            this.btnCancelarLancamento.Text = "Cancelar Lançamento";
            this.btnCancelarLancamento.Click += new System.EventHandler(this.btnCancelarLancamento_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(180, 6);
            // 
            // btnGeraFatura
            // 
            this.btnGeraFatura.Name = "btnGeraFatura";
            this.btnGeraFatura.Size = new System.Drawing.Size(183, 28);
            this.btnGeraFatura.Text = "Gerar Fatura";
            this.btnGeraFatura.Click += new System.EventHandler(this.btnGeraFatura_Click);
            // 
            // btnCancelarFatura
            // 
            this.btnCancelarFatura.Name = "btnCancelarFatura";
            this.btnCancelarFatura.Size = new System.Drawing.Size(183, 28);
            this.btnCancelarFatura.Text = "Cancelar Fatura";
            this.btnCancelarFatura.Click += new System.EventHandler(this.btnCancelarFatura_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(180, 6);
            // 
            // btnGeraBoleto
            // 
            this.btnGeraBoleto.Image = ((System.Drawing.Image)(resources.GetObject("btnGeraBoleto.Image")));
            this.btnGeraBoleto.Name = "btnGeraBoleto";
            this.btnGeraBoleto.Size = new System.Drawing.Size(183, 28);
            this.btnGeraBoleto.Text = "Gerar Boleto";
            this.btnGeraBoleto.Click += new System.EventHandler(this.btnGeraBoleto_Click);
            // 
            // toolStripDropDownButton4
            // 
            this.toolStripDropDownButton4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportExcel});
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
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(983, 620);
            this.gridControl1.TabIndex = 1;
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
            this.gridView1.Click += new System.EventHandler(this.gridView1_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.gridControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panelControl1);
            this.splitContainer2.Size = new System.Drawing.Size(983, 655);
            this.splitContainer2.SplitterDistance = 620;
            this.splitContainer2.TabIndex = 2;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lbTotal);
            this.panelControl1.Controls.Add(this.lbTotalResult);
            this.panelControl1.Controls.Add(this.lbResultRegistros);
            this.panelControl1.Controls.Add(this.lbRegistrosSelecionados);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(983, 31);
            this.panelControl1.TabIndex = 0;
            // 
            // lbTotal
            // 
            this.lbTotal.Location = new System.Drawing.Point(950, 5);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(28, 13);
            this.lbTotal.TabIndex = 3;
            this.lbTotal.Text = "Total:";
            // 
            // lbTotalResult
            // 
            this.lbTotalResult.Location = new System.Drawing.Point(984, 5);
            this.lbTotalResult.Name = "lbTotalResult";
            this.lbTotalResult.Size = new System.Drawing.Size(0, 13);
            this.lbTotalResult.TabIndex = 2;
            // 
            // lbResultRegistros
            // 
            this.lbResultRegistros.LineStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.lbResultRegistros.LineVisible = true;
            this.lbResultRegistros.Location = new System.Drawing.Point(125, 5);
            this.lbResultRegistros.Name = "lbResultRegistros";
            this.lbResultRegistros.Size = new System.Drawing.Size(0, 13);
            this.lbResultRegistros.TabIndex = 1;
            // 
            // lbRegistrosSelecionados
            // 
            this.lbRegistrosSelecionados.Location = new System.Drawing.Point(5, 5);
            this.lbRegistrosSelecionados.Name = "lbRegistrosSelecionados";
            this.lbRegistrosSelecionados.Size = new System.Drawing.Size(114, 13);
            this.lbRegistrosSelecionados.TabIndex = 0;
            this.lbRegistrosSelecionados.Text = "Registros Selecionados:";
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
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(983, 687);
            this.splitContainer1.SplitterDistance = 28;
            this.splitContainer1.TabIndex = 0;
            // 
            // frmVisaoLancamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 687);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVisaoLancamento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lançamentos Financeiros";
            this.Load += new System.EventHandler(this.frmVisaoLancamento_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripMenuItem btnCancelarLancamento;
        private System.Windows.Forms.ToolStripMenuItem btnBaixaLancamento;
        private System.Windows.Forms.ToolStripMenuItem btnGeraFatura;
        private System.Windows.Forms.ToolStripMenuItem btnCancelarBaixa;
        private System.Windows.Forms.ToolStripMenuItem btnGeraBoleto;
        private System.Windows.Forms.ToolStripMenuItem btnCancelarFatura;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton4;
        private System.Windows.Forms.ToolStripMenuItem btnExportExcel;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ToolStripMenuItem btnVinculaLancamento;
        private System.Windows.Forms.ToolStripMenuItem btnlançamentoPadrao;
        private System.Windows.Forms.ToolStripMenuItem btnparcelarlancamento;
        private System.Windows.Forms.ToolStripMenuItem imprimirToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lbTotal;
        private DevExpress.XtraEditors.LabelControl lbTotalResult;
        private DevExpress.XtraEditors.LabelControl lbResultRegistros;
        private DevExpress.XtraEditors.LabelControl lbRegistrosSelecionados;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}