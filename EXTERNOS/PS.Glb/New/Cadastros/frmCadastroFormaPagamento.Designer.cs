namespace PS.Glb.New.Cadastros
{
    partial class frmCadastroFormaPagamento
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
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao3 = new ITGProducao.Controles.Newlookup_WhereVisao();
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao4 = new ITGProducao.Controles.Newlookup_WhereVisao();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastroFormaPagamento));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lpCartao = new ITGProducao.Controles.NewLookup();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.tbNome = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.chkAtivo = new DevExpress.XtraEditors.CheckEdit();
            this.tbCodForma = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
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
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbNome.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAtivo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbCodForma.Properties)).BeginInit();
            this.toolStrip1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCancelarAtual);
            this.splitContainer1.Panel2.Controls.Add(this.btnOKAtual);
            this.splitContainer1.Panel2.Controls.Add(this.btnSalvarAtual);
            this.splitContainer1.Size = new System.Drawing.Size(751, 316);
            this.splitContainer1.SplitterDistance = 279;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 29);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(751, 250);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.cmbTipo);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.tbNome);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.chkAtivo);
            this.tabPage1.Controls.Add(this.tbCodForma);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(743, 224);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lpCartao);
            this.groupBox1.Location = new System.Drawing.Point(8, 144);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(727, 74);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cartão de Crédito/Débito";
            // 
            // lpCartao
            // 
            this.lpCartao.Abrir_Primeiro = ITGProducao.Controles.NewLookup.AbrirPrimeiro.Visao;
            this.lpCartao.CampoCodigo_Igual_a = ITGProducao.Controles.NewLookup.CampoCodigoIguala.CampoCodigoBD;
            this.lpCartao.CampoCodigoBD = "CODREDECARTAO";
            this.lpCartao.CampoCodigoInterno = null;
            this.lpCartao.CampoDescricaoBD = "NOME";
            this.lpCartao.CarregaDescricaoSemFiltro = true;
            this.lpCartao.Codigo_MaxLenght = 0;
            this.lpCartao.Enabled = false;
            this.lpCartao.Formulario_Cadastro = null;
            this.lpCartao.Formulario_Filtro = null;
            this.lpCartao.Formulario_Visao = "PS.Glb.New.Visao.frmVisaoRedeCartao";
            newlookup_WhereVisao3.NomeColuna = "CODEMPRESA";
            newlookup_WhereVisao3.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao3.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.AppLib_Context_Empresa;
            newlookup_WhereVisao3.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao3.ValorFixo = null;
            newlookup_WhereVisao3.Variavel_Interna = true;
            newlookup_WhereVisao4.NomeColuna = "CODREDECARTAO";
            newlookup_WhereVisao4.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao4.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.CampoCodigo_NewLookup;
            newlookup_WhereVisao4.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao4.ValorFixo = null;
            newlookup_WhereVisao4.Variavel_Interna = true;
            this.lpCartao.Grid_WhereVisao.Add(newlookup_WhereVisao3);
            this.lpCartao.Grid_WhereVisao.Add(newlookup_WhereVisao4);
            this.lpCartao.Location = new System.Drawing.Point(6, 19);
            this.lpCartao.MensagemCodigoVazio = null;
            this.lpCartao.mensagemErrorProvider = null;
            this.lpCartao.Name = "lpCartao";
            this.lpCartao.Projeto_Formularios = "PS.Glb";
            this.lpCartao.Size = new System.Drawing.Size(721, 46);
            this.lpCartao.TabelaBD = "VREDECARTAO";
            this.lpCartao.TabIndex = 11;
            this.lpCartao.Titulo = "Rede do Cartão";
            this.lpCartao.ValorCodigoInterno = null;
            this.lpCartao.whereParametros = null;
            this.lpCartao.whereVisao = null;
            // 
            // cmbTipo
            // 
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Location = new System.Drawing.Point(8, 117);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(121, 21);
            this.cmbTipo.TabIndex = 8;
            this.cmbTipo.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(8, 97);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "TIPO";
            // 
            // tbNome
            // 
            this.tbNome.Location = new System.Drawing.Point(8, 71);
            this.tbNome.Name = "tbNome";
            this.tbNome.Size = new System.Drawing.Size(727, 20);
            this.tbNome.TabIndex = 6;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 52);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(29, 13);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "NOME";
            // 
            // chkAtivo
            // 
            this.chkAtivo.Location = new System.Drawing.Point(134, 27);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Properties.Caption = "ATIVO";
            this.chkAtivo.Size = new System.Drawing.Size(75, 19);
            this.chkAtivo.TabIndex = 4;
            // 
            // tbCodForma
            // 
            this.tbCodForma.Location = new System.Drawing.Point(8, 26);
            this.tbCodForma.Name = "tbCodForma";
            this.tbCodForma.Size = new System.Drawing.Size(120, 20);
            this.tbCodForma.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "CODFORMA";
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
            this.btnCancelarAtual.Location = new System.Drawing.Point(664, 2);
            this.btnCancelarAtual.Name = "btnCancelarAtual";
            this.btnCancelarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnCancelarAtual.TabIndex = 35;
            this.btnCancelarAtual.Text = "Cancelar";
            this.btnCancelarAtual.Click += new System.EventHandler(this.btnCancelarAtual_Click);
            // 
            // btnOKAtual
            // 
            this.btnOKAtual.Location = new System.Drawing.Point(574, 2);
            this.btnOKAtual.Name = "btnOKAtual";
            this.btnOKAtual.Size = new System.Drawing.Size(75, 23);
            this.btnOKAtual.TabIndex = 34;
            this.btnOKAtual.Text = "OK";
            this.btnOKAtual.Click += new System.EventHandler(this.btnOKAtual_Click);
            // 
            // btnSalvarAtual
            // 
            this.btnSalvarAtual.Location = new System.Drawing.Point(483, 2);
            this.btnSalvarAtual.Name = "btnSalvarAtual";
            this.btnSalvarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnSalvarAtual.TabIndex = 33;
            this.btnSalvarAtual.Text = "Salvar";
            this.btnSalvarAtual.Click += new System.EventHandler(this.btnSalvarAtual_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmCadastroFormaPagamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 316);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCadastroFormaPagamento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro Forma de Pagamento";
            this.Load += new System.EventHandler(this.frmCadastroFormaPagamento_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbNome.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAtivo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbCodForma.Properties)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton btnCancelarAtual;
        private DevExpress.XtraEditors.SimpleButton btnOKAtual;
        private DevExpress.XtraEditors.SimpleButton btnSalvarAtual;
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
        private DevExpress.XtraEditors.TextEdit tbCodForma;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit chkAtivo;
        private DevExpress.XtraEditors.TextEdit tbNome;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.ComboBox cmbTipo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.GroupBox groupBox1;
        private ITGProducao.Controles.NewLookup lpCartao;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}