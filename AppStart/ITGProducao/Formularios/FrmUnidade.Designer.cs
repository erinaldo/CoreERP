namespace ITGProducao.Formularios
{
    partial class FrmUnidade
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUnidade));
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao1 = new ITGProducao.Controles.Newlookup_WhereVisao();
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao2 = new ITGProducao.Controles.Newlookup_WhereVisao();
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
            this.txtFatorConversao = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lookupunidade = new ITGProducao.Controles.NewLookup();
            this.txtDescricao = new DevExpress.XtraEditors.TextEdit();
            this.txtCodigo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnSalvar = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFatorConversao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(813, 485);
            this.splitContainer1.SplitterDistance = 38;
            this.splitContainer1.TabIndex = 1;
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
            this.toolStrip1.Size = new System.Drawing.Size(813, 29);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNovo
            // 
            this.btnNovo.Image = ((System.Drawing.Image)(resources.GetObject("btnNovo.Image")));
            this.btnNovo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(67, 26);
            this.btnNovo.Text = "Novo";
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Enabled = false;
            this.btnEditar.Image = ((System.Drawing.Image)(resources.GetObject("btnEditar.Image")));
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(69, 26);
            this.btnEditar.Text = "Editar";
            this.btnEditar.Visible = false;
            // 
            // btnExcluir
            // 
            this.btnExcluir.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluir.Image")));
            this.btnExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(74, 26);
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
            this.btnPesquisar.Enabled = false;
            this.btnPesquisar.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisar.Image")));
            this.btnPesquisar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(91, 26);
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.Visible = false;
            // 
            // btnAgrupar
            // 
            this.btnAgrupar.Enabled = false;
            this.btnAgrupar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgrupar.Image")));
            this.btnAgrupar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAgrupar.Name = "btnAgrupar";
            this.btnAgrupar.Size = new System.Drawing.Size(83, 26);
            this.btnAgrupar.Text = "Agrupar";
            this.btnAgrupar.Visible = false;
            // 
            // btnFiltros
            // 
            this.btnFiltros.Enabled = false;
            this.btnFiltros.Image = ((System.Drawing.Image)(resources.GetObject("btnFiltros.Image")));
            this.btnFiltros.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFiltros.Name = "btnFiltros";
            this.btnFiltros.Size = new System.Drawing.Size(69, 26);
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
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(75, 26);
            this.toolStripDropDownButton1.Text = "Visão";
            this.toolStripDropDownButton1.Visible = false;
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(195, 26);
            this.btnAtualizar.Text = "Atualizar";
            // 
            // btnSelecionarColunas
            // 
            this.btnSelecionarColunas.Name = "btnSelecionarColunas";
            this.btnSelecionarColunas.Size = new System.Drawing.Size(195, 26);
            this.btnSelecionarColunas.Text = "Selecionar Colunas";
            // 
            // btnSalvarLayout
            // 
            this.btnSalvarLayout.Name = "btnSalvarLayout";
            this.btnSalvarLayout.Size = new System.Drawing.Size(195, 26);
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
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(89, 26);
            this.toolStripDropDownButton2.Text = "Anexos";
            this.toolStripDropDownButton2.Visible = false;
            // 
            // btnLancamentoFinanceiro
            // 
            this.btnLancamentoFinanceiro.Name = "btnLancamentoFinanceiro";
            this.btnLancamentoFinanceiro.Size = new System.Drawing.Size(235, 26);
            this.btnLancamentoFinanceiro.Text = "Lançamentos Financeiros";
            // 
            // btnNfe
            // 
            this.btnNfe.Name = "btnNfe";
            this.btnNfe.Size = new System.Drawing.Size(235, 26);
            this.btnNfe.Text = "NF-e";
            // 
            // btnItens
            // 
            this.btnItens.Name = "btnItens";
            this.btnItens.Size = new System.Drawing.Size(235, 26);
            this.btnItens.Text = "Itens";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(232, 6);
            // 
            // btnFecharAnexo
            // 
            this.btnFecharAnexo.Name = "btnFecharAnexo";
            this.btnFecharAnexo.Size = new System.Drawing.Size(235, 26);
            this.btnFecharAnexo.Text = "Fechar Anexos";
            // 
            // btnImprimirOperacao
            // 
            this.btnImprimirOperacao.Name = "btnImprimirOperacao";
            this.btnImprimirOperacao.Size = new System.Drawing.Size(235, 26);
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
            this.toolStripDropDownButton3.Size = new System.Drawing.Size(105, 26);
            this.toolStripDropDownButton3.Text = "Processos";
            this.toolStripDropDownButton3.Visible = false;
            // 
            // btnCancelarLancamento
            // 
            this.btnCancelarLancamento.Name = "btnCancelarLancamento";
            this.btnCancelarLancamento.Size = new System.Drawing.Size(240, 26);
            this.btnCancelarLancamento.Text = "Cancelar Lançamento";
            // 
            // btnBaixaLancamento
            // 
            this.btnBaixaLancamento.Name = "btnBaixaLancamento";
            this.btnBaixaLancamento.Size = new System.Drawing.Size(240, 26);
            this.btnBaixaLancamento.Text = "Baixar Lançamento";
            // 
            // btnGeraFatura
            // 
            this.btnGeraFatura.Name = "btnGeraFatura";
            this.btnGeraFatura.Size = new System.Drawing.Size(240, 26);
            this.btnGeraFatura.Text = "Gerar Fatura";
            // 
            // btnCancelarBaixa
            // 
            this.btnCancelarBaixa.Name = "btnCancelarBaixa";
            this.btnCancelarBaixa.Size = new System.Drawing.Size(240, 26);
            this.btnCancelarBaixa.Text = "Cancelar Baixa";
            // 
            // btnCancelarFatura
            // 
            this.btnCancelarFatura.Name = "btnCancelarFatura";
            this.btnCancelarFatura.Size = new System.Drawing.Size(240, 26);
            this.btnCancelarFatura.Text = "Cancelar Fatura";
            // 
            // btnGeraBoleto
            // 
            this.btnGeraBoleto.Name = "btnGeraBoleto";
            this.btnGeraBoleto.Size = new System.Drawing.Size(240, 26);
            this.btnGeraBoleto.Text = "Gerar Boleto";
            // 
            // btnVinculaLancamento
            // 
            this.btnVinculaLancamento.Name = "btnVinculaLancamento";
            this.btnVinculaLancamento.Size = new System.Drawing.Size(240, 26);
            this.btnVinculaLancamento.Text = "Vincular Lançamentos";
            // 
            // btnImprimirCopiaCheque
            // 
            this.btnImprimirCopiaCheque.Name = "btnImprimirCopiaCheque";
            this.btnImprimirCopiaCheque.Size = new System.Drawing.Size(240, 26);
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
            this.toolStripDropDownButton4.Size = new System.Drawing.Size(98, 26);
            this.toolStripDropDownButton4.Text = "Exportar";
            this.toolStripDropDownButton4.Visible = false;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.Image")));
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(116, 28);
            this.btnExportExcel.Text = "Excel";
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
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btnSalvar);
            this.splitContainer2.Panel2.Controls.Add(this.btnOk);
            this.splitContainer2.Panel2.Controls.Add(this.btnCancelar);
            this.splitContainer2.Size = new System.Drawing.Size(813, 443);
            this.splitContainer2.SplitterDistance = 376;
            this.splitContainer2.TabIndex = 10;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(813, 376);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtFatorConversao);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.lookupunidade);
            this.tabPage1.Controls.Add(this.txtDescricao);
            this.tabPage1.Controls.Add(this.txtCodigo);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(805, 350);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtFatorConversao
            // 
            this.txtFatorConversao.EditValue = "0";
            this.txtFatorConversao.Location = new System.Drawing.Point(19, 180);
            this.txtFatorConversao.Name = "txtFatorConversao";
            this.txtFatorConversao.Properties.Mask.EditMask = "n0";
            this.txtFatorConversao.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtFatorConversao.Properties.MaxLength = 5;
            this.txtFatorConversao.Size = new System.Drawing.Size(178, 20);
            this.txtFatorConversao.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(19, 161);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(100, 13);
            this.labelControl3.TabIndex = 23;
            this.labelControl3.Text = "Fator de Conversão:";
            // 
            // lookupunidade
            // 
            this.lookupunidade.Abrir_Primeiro = ITGProducao.Controles.NewLookup.AbrirPrimeiro.Visao;
            this.lookupunidade.CampoCodigo_Igual_a = ITGProducao.Controles.NewLookup.CampoCodigoIguala.CampoCodigoBD;
            this.lookupunidade.CampoCodigoBD = "CODUNID";
            this.lookupunidade.CampoCodigoInterno = null;
            this.lookupunidade.CampoDescricaoBD = "NOME";
            this.lookupunidade.Codigo_MaxLenght = 5;
            this.lookupunidade.Formulario_Cadastro = "ITGProducao.Formularios.FrmUnidade";
            this.lookupunidade.Formulario_Filtro = "ITGProducao.Filtros.FrmFiltroUnidade";
            this.lookupunidade.Formulario_Visao = "ITGProducao.Visao.FrmVisaoUnidade";
            newlookup_WhereVisao1.NomeColuna = "VUNID.CODEMPRESA";
            newlookup_WhereVisao1.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao1.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.AppLib_Context_Empresa;
            newlookup_WhereVisao1.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao1.ValorFixo = null;
            newlookup_WhereVisao1.Variavel_Interna = true;
            newlookup_WhereVisao2.NomeColuna = "VUNID.CODUNID";
            newlookup_WhereVisao2.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao2.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.CampoCodigo_NewLookup;
            newlookup_WhereVisao2.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao2.ValorFixo = null;
            newlookup_WhereVisao2.Variavel_Interna = true;
            this.lookupunidade.Grid_WhereVisao.Add(newlookup_WhereVisao1);
            this.lookupunidade.Grid_WhereVisao.Add(newlookup_WhereVisao2);
            this.lookupunidade.Location = new System.Drawing.Point(16, 108);
            this.lookupunidade.MensagemCodigoVazio = null;
            this.lookupunidade.mensagemErrorProvider = null;
            this.lookupunidade.Name = "lookupunidade";
            this.lookupunidade.Projeto_Formularios = "ITGProducao";
            this.lookupunidade.Size = new System.Drawing.Size(366, 46);
            this.lookupunidade.TabelaBD = "VUNID";
            this.lookupunidade.TabIndex = 2;
            this.lookupunidade.Titulo = "Unidade:";
            this.lookupunidade.ValorCodigoInterno = null;
            this.lookupunidade.whereParametros = null;
            this.lookupunidade.whereVisao = null;
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(19, 80);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Properties.MaxLength = 100;
            this.txtDescricao.Size = new System.Drawing.Size(288, 20);
            this.txtDescricao.TabIndex = 1;
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(19, 34);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Properties.MaxLength = 5;
            this.txtCodigo.Size = new System.Drawing.Size(178, 20);
            this.txtCodigo.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(19, 60);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Descrição:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(19, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(37, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Código:";
            // 
            // btnSalvar
            // 
            this.btnSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvar.Location = new System.Drawing.Point(564, 27);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 4;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(645, 27);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(726, 27);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // FrmUnidade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 485);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmUnidade";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Unidade";
            this.Load += new System.EventHandler(this.FrmUnidade_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFatorConversao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
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
        private DevExpress.XtraEditors.TextEdit txtDescricao;
        private DevExpress.XtraEditors.TextEdit txtCodigo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSalvar;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private Controles.NewLookup lookupunidade;
        private DevExpress.XtraEditors.TextEdit txtFatorConversao;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}