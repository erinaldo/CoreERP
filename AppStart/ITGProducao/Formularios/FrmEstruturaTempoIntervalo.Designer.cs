namespace ITGProducao.Formularios
{
    partial class FrmEstruturaTempoIntervalo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEstruturaTempoIntervalo));
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
            this.btnItens = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFecharAnexo = new System.Windows.Forms.ToolStripMenuItem();
            this.btnImprimirOperacao = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnCancelarLancamento = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBaixaLancamento = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGeraFatura = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancelarBaixa = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancelarFatura = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGeraBoleto = new System.Windows.Forms.ToolStripMenuItem();
            this.btnVinculaLancamento = new System.Windows.Forms.ToolStripMenuItem();
            this.btnImprimirCopiaCheque = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton4 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txttempo = new DevExpress.XtraEditors.TextEdit();
            this.txtqtdefinal = new DevExpress.XtraEditors.TextEdit();
            this.txtqtdeinicial = new DevExpress.XtraEditors.TextEdit();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.btnSalvar = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txttempo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtqtdefinal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtqtdeinicial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
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
            this.toolStrip1.Size = new System.Drawing.Size(295, 29);
            this.toolStrip1.TabIndex = 11;
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
            this.btnEditar.Visible = false;
            // 
            // btnExcluir
            // 
            this.btnExcluir.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluir.Image")));
            this.btnExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(64, 26);
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.Visible = false;
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
            this.btnPesquisar.Visible = false;
            // 
            // btnAgrupar
            // 
            this.btnAgrupar.Enabled = false;
            this.btnAgrupar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgrupar.Image")));
            this.btnAgrupar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAgrupar.Name = "btnAgrupar";
            this.btnAgrupar.Size = new System.Drawing.Size(72, 26);
            this.btnAgrupar.Text = "Agrupar";
            this.btnAgrupar.Visible = false;
            // 
            // btnFiltros
            // 
            this.btnFiltros.Enabled = false;
            this.btnFiltros.Image = ((System.Drawing.Image)(resources.GetObject("btnFiltros.Image")));
            this.btnFiltros.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFiltros.Name = "btnFiltros";
            this.btnFiltros.Size = new System.Drawing.Size(62, 26);
            this.btnFiltros.Text = "Filtros";
            this.btnFiltros.Visible = false;
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
            this.toolStripDropDownButton1.Visible = false;
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
            this.toolStripSeparator2.Visible = false;
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLancamentoFinanceiro,
            this.btnNfe,
            this.btnItens,
            this.toolStripSeparator3,
            this.btnFecharAnexo,
            this.btnImprimirOperacao});
            this.toolStripDropDownButton2.Enabled = false;
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(78, 26);
            this.toolStripDropDownButton2.Text = "Anexos";
            this.toolStripDropDownButton2.Visible = false;
            // 
            // btnLancamentoFinanceiro
            // 
            this.btnLancamentoFinanceiro.Name = "btnLancamentoFinanceiro";
            this.btnLancamentoFinanceiro.Size = new System.Drawing.Size(194, 22);
            this.btnLancamentoFinanceiro.Text = "Lançamentos Financeiros";
            // 
            // btnNfe
            // 
            this.btnNfe.Name = "btnNfe";
            this.btnNfe.Size = new System.Drawing.Size(194, 22);
            this.btnNfe.Text = "NF-e";
            // 
            // btnItens
            // 
            this.btnItens.Name = "btnItens";
            this.btnItens.Size = new System.Drawing.Size(194, 22);
            this.btnItens.Text = "Itens";
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
            this.btnCancelarLancamento,
            this.btnBaixaLancamento,
            this.btnGeraFatura,
            this.btnCancelarBaixa,
            this.btnCancelarFatura,
            this.btnGeraBoleto,
            this.btnVinculaLancamento,
            this.btnImprimirCopiaCheque});
            this.toolStripDropDownButton3.Enabled = false;
            this.toolStripDropDownButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton3.Image")));
            this.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(90, 26);
            this.toolStripDropDownButton3.Text = "Processos";
            this.toolStripDropDownButton3.Visible = false;
            // 
            // btnCancelarLancamento
            // 
            this.btnCancelarLancamento.Name = "btnCancelarLancamento";
            this.btnCancelarLancamento.Size = new System.Drawing.Size(197, 22);
            this.btnCancelarLancamento.Text = "Cancelar Lançamento";
            // 
            // btnBaixaLancamento
            // 
            this.btnBaixaLancamento.Name = "btnBaixaLancamento";
            this.btnBaixaLancamento.Size = new System.Drawing.Size(197, 22);
            this.btnBaixaLancamento.Text = "Baixar Lançamento";
            // 
            // btnGeraFatura
            // 
            this.btnGeraFatura.Name = "btnGeraFatura";
            this.btnGeraFatura.Size = new System.Drawing.Size(197, 22);
            this.btnGeraFatura.Text = "Gerar Fatura";
            // 
            // btnCancelarBaixa
            // 
            this.btnCancelarBaixa.Name = "btnCancelarBaixa";
            this.btnCancelarBaixa.Size = new System.Drawing.Size(197, 22);
            this.btnCancelarBaixa.Text = "Cancelar Baixa";
            // 
            // btnCancelarFatura
            // 
            this.btnCancelarFatura.Name = "btnCancelarFatura";
            this.btnCancelarFatura.Size = new System.Drawing.Size(197, 22);
            this.btnCancelarFatura.Text = "Cancelar Fatura";
            // 
            // btnGeraBoleto
            // 
            this.btnGeraBoleto.Name = "btnGeraBoleto";
            this.btnGeraBoleto.Size = new System.Drawing.Size(197, 22);
            this.btnGeraBoleto.Text = "Gerar Boleto";
            // 
            // btnVinculaLancamento
            // 
            this.btnVinculaLancamento.Name = "btnVinculaLancamento";
            this.btnVinculaLancamento.Size = new System.Drawing.Size(197, 22);
            this.btnVinculaLancamento.Text = "Vincular Lançamentos";
            // 
            // btnImprimirCopiaCheque
            // 
            this.btnImprimirCopiaCheque.Name = "btnImprimirCopiaCheque";
            this.btnImprimirCopiaCheque.Size = new System.Drawing.Size(197, 22);
            this.btnImprimirCopiaCheque.Text = "Imprimir Cópia de Cheque";
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
            this.toolStripDropDownButton4.Visible = false;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.Image")));
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(105, 28);
            this.btnExportExcel.Text = "Excel";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 29);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btnSalvar);
            this.splitContainer2.Panel2.Controls.Add(this.btnOk);
            this.splitContainer2.Panel2.Controls.Add(this.btnCancelar);
            this.splitContainer2.Size = new System.Drawing.Size(295, 235);
            this.splitContainer2.SplitterDistance = 165;
            this.splitContainer2.TabIndex = 12;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(295, 165);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txttempo);
            this.tabPage1.Controls.Add(this.txtqtdefinal);
            this.tabPage1.Controls.Add(this.txtqtdeinicial);
            this.tabPage1.Controls.Add(this.labelControl19);
            this.tabPage1.Controls.Add(this.labelControl18);
            this.tabPage1.Controls.Add(this.labelControl17);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(287, 139);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txttempo
            // 
            this.txttempo.Location = new System.Drawing.Point(105, 98);
            this.txttempo.Name = "txttempo";
            this.txttempo.Properties.Mask.EditMask = "n0";
            this.txttempo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txttempo.Properties.MaxLength = 10;
            this.txttempo.Size = new System.Drawing.Size(135, 20);
            this.txttempo.TabIndex = 2;
            // 
            // txtqtdefinal
            // 
            this.txtqtdefinal.Location = new System.Drawing.Point(104, 63);
            this.txtqtdefinal.Name = "txtqtdefinal";
            this.txtqtdefinal.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtqtdefinal.Properties.Mask.EditMask = "n0";
            this.txtqtdefinal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtqtdefinal.Properties.MaxLength = 10;
            this.txtqtdefinal.Size = new System.Drawing.Size(135, 20);
            this.txtqtdefinal.TabIndex = 1;
            // 
            // txtqtdeinicial
            // 
            this.txtqtdeinicial.Enabled = false;
            this.txtqtdeinicial.Location = new System.Drawing.Point(104, 26);
            this.txtqtdeinicial.Name = "txtqtdeinicial";
            this.txtqtdeinicial.Properties.Mask.EditMask = "n0";
            this.txtqtdeinicial.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtqtdeinicial.Properties.MaxLength = 10;
            this.txtqtdeinicial.Size = new System.Drawing.Size(135, 20);
            this.txtqtdeinicial.TabIndex = 0;
            this.txtqtdeinicial.EditValueChanged += new System.EventHandler(this.txtqtdeinicial_EditValueChanged);
            // 
            // labelControl19
            // 
            this.labelControl19.Location = new System.Drawing.Point(35, 101);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(63, 13);
            this.labelControl19.TabIndex = 30;
            this.labelControl19.Text = "Tempo (min):";
            // 
            // labelControl18
            // 
            this.labelControl18.Location = new System.Drawing.Point(45, 66);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(53, 13);
            this.labelControl18.TabIndex = 28;
            this.labelControl18.Text = "Qtde Final:";
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(40, 29);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(58, 13);
            this.labelControl17.TabIndex = 26;
            this.labelControl17.Text = "Qtde Inicial:";
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.Location = new System.Drawing.Point(46, 27);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 3;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(127, 27);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(208, 27);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // FrmEstruturaTempoIntervalo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 264);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmEstruturaTempoIntervalo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Tempo por Intervalo";
            this.Load += new System.EventHandler(this.FrmEstruturaTempoIntervalo_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txttempo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtqtdefinal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtqtdeinicial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem btnLancamentoFinanceiro;
        private System.Windows.Forms.ToolStripMenuItem btnNfe;
        private System.Windows.Forms.ToolStripMenuItem btnItens;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem btnFecharAnexo;
        private System.Windows.Forms.ToolStripMenuItem btnImprimirOperacao;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton3;
        private System.Windows.Forms.ToolStripMenuItem btnCancelarLancamento;
        private System.Windows.Forms.ToolStripMenuItem btnBaixaLancamento;
        private System.Windows.Forms.ToolStripMenuItem btnGeraFatura;
        private System.Windows.Forms.ToolStripMenuItem btnCancelarBaixa;
        private System.Windows.Forms.ToolStripMenuItem btnCancelarFatura;
        private System.Windows.Forms.ToolStripMenuItem btnGeraBoleto;
        private System.Windows.Forms.ToolStripMenuItem btnVinculaLancamento;
        private System.Windows.Forms.ToolStripMenuItem btnImprimirCopiaCheque;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton4;
        private System.Windows.Forms.ToolStripMenuItem btnExportExcel;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.SimpleButton btnSalvar;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.TextEdit txttempo;
        private DevExpress.XtraEditors.TextEdit txtqtdefinal;
        private DevExpress.XtraEditors.TextEdit txtqtdeinicial;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}