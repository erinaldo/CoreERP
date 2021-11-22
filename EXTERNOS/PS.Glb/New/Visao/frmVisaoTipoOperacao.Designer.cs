namespace PS.Glb.New.Visao
{
    partial class frmVisaoTipoOperacao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVisaoTipoOperacao));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
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
            this.btnLancamentoFinanceiro = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNfe = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEventos = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNfeDados = new System.Windows.Forms.ToolStripMenuItem();
            this.btnItens = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMotivos = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFecharAnexo = new System.Windows.Forms.ToolStripMenuItem();
            this.btnImprimirOperacao = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnFaturarOperacao = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCopiaOperacao = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAjustarValorFinanceiro = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancelarOperacao = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRastreamentoOper = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConcluirOperacao = new System.Windows.Forms.ToolStripMenuItem();
            this.nFeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGerarNfe = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCartaCorrecao = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConsultaEventoNfe = new System.Windows.Forms.ToolStripMenuItem();
            this.liberaçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAprovaDesconto = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAprovaLimiteCredito = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCopiaReferencia = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton4 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1208, 541);
            this.splitContainer1.SplitterDistance = 29;
            this.splitContainer1.TabIndex = 0;
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
            this.toolStrip1.Size = new System.Drawing.Size(1208, 26);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNovo
            // 
            this.btnNovo.Enabled = false;
            this.btnNovo.Image = ((System.Drawing.Image)(resources.GetObject("btnNovo.Image")));
            this.btnNovo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(58, 23);
            this.btnNovo.Text = "Novo";
            // 
            // btnEditar
            // 
            this.btnEditar.Enabled = false;
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(61, 23);
            this.btnEditar.Text = "Editar";
            // 
            // btnExcluir
            // 
            this.btnExcluir.Enabled = false;
            this.btnExcluir.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluir.Image")));
            this.btnExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(64, 23);
            this.btnExcluir.Text = "Excluir";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Enabled = false;
            this.btnPesquisar.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisar.Image")));
            this.btnPesquisar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(79, 23);
            this.btnPesquisar.Text = "Pesquisar";
            // 
            // btnAgrupar
            // 
            this.btnAgrupar.Enabled = false;
            this.btnAgrupar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgrupar.Image")));
            this.btnAgrupar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAgrupar.Name = "btnAgrupar";
            this.btnAgrupar.Size = new System.Drawing.Size(72, 23);
            this.btnAgrupar.Text = "Agrupar";
            // 
            // btnFiltros
            // 
            this.btnFiltros.Enabled = false;
            this.btnFiltros.Image = ((System.Drawing.Image)(resources.GetObject("btnFiltros.Image")));
            this.btnFiltros.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFiltros.Name = "btnFiltros";
            this.btnFiltros.Size = new System.Drawing.Size(62, 23);
            this.btnFiltros.Text = "Filtros";
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
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(67, 23);
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
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLancamentoFinanceiro,
            this.btnNfe,
            this.btnItens,
            this.btnMotivos,
            this.toolStripSeparator3,
            this.btnFecharAnexo,
            this.btnImprimirOperacao});
            this.toolStripDropDownButton2.Enabled = false;
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(78, 23);
            this.toolStripDropDownButton2.Text = "Anexos";
            // 
            // btnLancamentoFinanceiro
            // 
            this.btnLancamentoFinanceiro.Name = "btnLancamentoFinanceiro";
            this.btnLancamentoFinanceiro.Size = new System.Drawing.Size(194, 22);
            this.btnLancamentoFinanceiro.Text = "Lançamentos Financeiros";
            // 
            // btnNfe
            // 
            this.btnNfe.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnEventos,
            this.btnNfeDados});
            this.btnNfe.Name = "btnNfe";
            this.btnNfe.Size = new System.Drawing.Size(194, 22);
            this.btnNfe.Text = "NF-e";
            // 
            // btnEventos
            // 
            this.btnEventos.Name = "btnEventos";
            this.btnEventos.Size = new System.Drawing.Size(113, 22);
            this.btnEventos.Text = "Eventos";
            // 
            // btnNfeDados
            // 
            this.btnNfeDados.Name = "btnNfeDados";
            this.btnNfeDados.Size = new System.Drawing.Size(113, 22);
            this.btnNfeDados.Text = "NF-e";
            // 
            // btnItens
            // 
            this.btnItens.Name = "btnItens";
            this.btnItens.Size = new System.Drawing.Size(194, 22);
            this.btnItens.Text = "Itens";
            // 
            // btnMotivos
            // 
            this.btnMotivos.Name = "btnMotivos";
            this.btnMotivos.Size = new System.Drawing.Size(194, 22);
            this.btnMotivos.Text = "Motivos";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(191, 6);
            // 
            // btnFecharAnexo
            // 
            this.btnFecharAnexo.Name = "btnFecharAnexo";
            this.btnFecharAnexo.Size = new System.Drawing.Size(194, 22);
            this.btnFecharAnexo.Text = "Fechar Anexos";
            // 
            // btnImprimirOperacao
            // 
            this.btnImprimirOperacao.Name = "btnImprimirOperacao";
            this.btnImprimirOperacao.Size = new System.Drawing.Size(194, 22);
            this.btnImprimirOperacao.Text = "Imprimir Operação";
            // 
            // toolStripDropDownButton3
            // 
            this.toolStripDropDownButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFaturarOperacao,
            this.btnCopiaOperacao,
            this.btnAjustarValorFinanceiro,
            this.btnCancelarOperacao,
            this.btnRastreamentoOper,
            this.btnConcluirOperacao,
            this.nFeToolStripMenuItem,
            this.liberaçõesToolStripMenuItem,
            this.btnCopiaReferencia});
            this.toolStripDropDownButton3.Enabled = false;
            this.toolStripDropDownButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton3.Image")));
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(90, 23);
            this.toolStripDropDownButton3.Text = "Processos";
            // 
            // btnFaturarOperacao
            // 
            this.btnFaturarOperacao.Name = "btnFaturarOperacao";
            this.btnFaturarOperacao.Size = new System.Drawing.Size(212, 22);
            this.btnFaturarOperacao.Text = "Transferir Operação";
            // 
            // btnCopiaOperacao
            // 
            this.btnCopiaOperacao.Name = "btnCopiaOperacao";
            this.btnCopiaOperacao.Size = new System.Drawing.Size(212, 22);
            this.btnCopiaOperacao.Text = "Copiar Operação";
            // 
            // btnAjustarValorFinanceiro
            // 
            this.btnAjustarValorFinanceiro.Name = "btnAjustarValorFinanceiro";
            this.btnAjustarValorFinanceiro.Size = new System.Drawing.Size(212, 22);
            this.btnAjustarValorFinanceiro.Text = "Ajustar Financeiro";
            // 
            // btnCancelarOperacao
            // 
            this.btnCancelarOperacao.Name = "btnCancelarOperacao";
            this.btnCancelarOperacao.Size = new System.Drawing.Size(212, 22);
            this.btnCancelarOperacao.Text = "Cancelar Operação";
            // 
            // btnRastreamentoOper
            // 
            this.btnRastreamentoOper.Name = "btnRastreamentoOper";
            this.btnRastreamentoOper.Size = new System.Drawing.Size(212, 22);
            this.btnRastreamentoOper.Text = "Rastreamento de Operações";
            // 
            // btnConcluirOperacao
            // 
            this.btnConcluirOperacao.Name = "btnConcluirOperacao";
            this.btnConcluirOperacao.Size = new System.Drawing.Size(212, 22);
            this.btnConcluirOperacao.Text = "Concluir Operação";
            // 
            // nFeToolStripMenuItem
            // 
            this.nFeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGerarNfe,
            this.btnCartaCorrecao,
            this.btnConsultaEventoNfe});
            this.nFeToolStripMenuItem.Name = "nFeToolStripMenuItem";
            this.nFeToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.nFeToolStripMenuItem.Text = "NF-e";
            // 
            // btnGerarNfe
            // 
            this.btnGerarNfe.Name = "btnGerarNfe";
            this.btnGerarNfe.Size = new System.Drawing.Size(163, 22);
            this.btnGerarNfe.Text = "Gerar NF-e";
            // 
            // btnCartaCorrecao
            // 
            this.btnCartaCorrecao.Name = "btnCartaCorrecao";
            this.btnCartaCorrecao.Size = new System.Drawing.Size(163, 22);
            this.btnCartaCorrecao.Text = "Carta de Correção";
            // 
            // btnConsultaEventoNfe
            // 
            this.btnConsultaEventoNfe.Name = "btnConsultaEventoNfe";
            this.btnConsultaEventoNfe.Size = new System.Drawing.Size(163, 22);
            this.btnConsultaEventoNfe.Text = "Consulta";
            // 
            // liberaçõesToolStripMenuItem
            // 
            this.liberaçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAprovaDesconto,
            this.btnAprovaLimiteCredito});
            this.liberaçõesToolStripMenuItem.Name = "liberaçõesToolStripMenuItem";
            this.liberaçõesToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.liberaçõesToolStripMenuItem.Text = "Aprovações";
            // 
            // btnAprovaDesconto
            // 
            this.btnAprovaDesconto.Name = "btnAprovaDesconto";
            this.btnAprovaDesconto.Size = new System.Drawing.Size(192, 22);
            this.btnAprovaDesconto.Text = "Aprova Desconto";
            // 
            // btnAprovaLimiteCredito
            // 
            this.btnAprovaLimiteCredito.Name = "btnAprovaLimiteCredito";
            this.btnAprovaLimiteCredito.Size = new System.Drawing.Size(192, 22);
            this.btnAprovaLimiteCredito.Text = "Aprova Limite de Crédito";
            // 
            // btnCopiaReferencia
            // 
            this.btnCopiaReferencia.Name = "btnCopiaReferencia";
            this.btnCopiaReferencia.Size = new System.Drawing.Size(212, 22);
            this.btnCopiaReferencia.Text = "Cópia de Referência";
            // 
            // toolStripDropDownButton4
            // 
            this.toolStripDropDownButton4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportExcel});
            this.toolStripDropDownButton4.Enabled = false;
            this.toolStripDropDownButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton4.Image")));
            this.toolStripDropDownButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton4.Name = "toolStripDropDownButton4";
            this.toolStripDropDownButton4.Size = new System.Drawing.Size(84, 23);
            this.toolStripDropDownButton4.Text = "Exportar";
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.Image")));
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(105, 28);
            this.btnExportExcel.Text = "Excel";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1208, 508);
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
            // 
            // frmVisaoTipoOperacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 541);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVisaoTipoOperacao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visão Tipo de Operação";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem btnLancamentoFinanceiro;
        private System.Windows.Forms.ToolStripMenuItem btnNfe;
        private System.Windows.Forms.ToolStripMenuItem btnEventos;
        private System.Windows.Forms.ToolStripMenuItem btnNfeDados;
        private System.Windows.Forms.ToolStripMenuItem btnItens;
        private System.Windows.Forms.ToolStripMenuItem btnMotivos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem btnFecharAnexo;
        private System.Windows.Forms.ToolStripMenuItem btnImprimirOperacao;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripMenuItem btnFaturarOperacao;
        private System.Windows.Forms.ToolStripMenuItem btnCopiaOperacao;
        private System.Windows.Forms.ToolStripMenuItem btnAjustarValorFinanceiro;
        private System.Windows.Forms.ToolStripMenuItem btnCancelarOperacao;
        private System.Windows.Forms.ToolStripMenuItem btnRastreamentoOper;
        private System.Windows.Forms.ToolStripMenuItem btnConcluirOperacao;
        private System.Windows.Forms.ToolStripMenuItem nFeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnGerarNfe;
        private System.Windows.Forms.ToolStripMenuItem btnCartaCorrecao;
        private System.Windows.Forms.ToolStripMenuItem btnConsultaEventoNfe;
        private System.Windows.Forms.ToolStripMenuItem liberaçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnAprovaDesconto;
        private System.Windows.Forms.ToolStripMenuItem btnAprovaLimiteCredito;
        private System.Windows.Forms.ToolStripMenuItem btnCopiaReferencia;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton4;
        private System.Windows.Forms.ToolStripMenuItem btnExportExcel;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}