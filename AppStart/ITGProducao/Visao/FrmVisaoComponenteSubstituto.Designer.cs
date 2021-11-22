namespace ITGProducao.Visao
{
    partial class FrmVisaoComponenteSubstituto
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
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao7 = new ITGProducao.Controles.Newlookup_WhereVisao();
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao8 = new ITGProducao.Controles.Newlookup_WhereVisao();
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao9 = new ITGProducao.Controles.Newlookup_WhereVisao();
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao10 = new ITGProducao.Controles.Newlookup_WhereVisao();
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao11 = new ITGProducao.Controles.Newlookup_WhereVisao();
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao12 = new ITGProducao.Controles.Newlookup_WhereVisao();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVisaoComponenteSubstituto));
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtFator = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtSaldoCalculado = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtSaldoDisponivel = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtNecessidade = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.newLookupprodutosubstituto = new ITGProducao.Controles.NewLookup();
            this.txtQuantidade = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lookupproduto = new ITGProducao.Controles.NewLookup();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridComponentes = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnIncluiComponenteOP = new DevExpress.XtraEditors.SimpleButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSaldoCalculado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSaldoDisponivel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNecessidade.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantidade.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridComponentes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
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
            this.splitContainer2.Panel2.Controls.Add(this.btnCancelar);
            this.splitContainer2.Panel2.Controls.Add(this.btnIncluiComponenteOP);
            this.splitContainer2.Size = new System.Drawing.Size(737, 554);
            this.splitContainer2.SplitterDistance = 469;
            this.splitContainer2.TabIndex = 10;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(737, 469);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtFator);
            this.tabPage1.Controls.Add(this.labelControl5);
            this.tabPage1.Controls.Add(this.txtSaldoCalculado);
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.txtSaldoDisponivel);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.txtNecessidade);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.newLookupprodutosubstituto);
            this.tabPage1.Controls.Add(this.txtQuantidade);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.lookupproduto);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(729, 443);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtFator
            // 
            this.txtFator.EditValue = "0";
            this.txtFator.Enabled = false;
            this.txtFator.Location = new System.Drawing.Point(630, 82);
            this.txtFator.Name = "txtFator";
            this.txtFator.Properties.Mask.EditMask = "n4";
            this.txtFator.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtFator.Properties.MaxLength = 15;
            this.txtFator.Size = new System.Drawing.Size(86, 22);
            this.txtFator.TabIndex = 17;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(630, 62);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(30, 13);
            this.labelControl5.TabIndex = 18;
            this.labelControl5.Text = "Fator:";
            // 
            // txtSaldoCalculado
            // 
            this.txtSaldoCalculado.Enabled = false;
            this.txtSaldoCalculado.Location = new System.Drawing.Point(528, 30);
            this.txtSaldoCalculado.Name = "txtSaldoCalculado";
            this.txtSaldoCalculado.Properties.Mask.EditMask = "n4";
            this.txtSaldoCalculado.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtSaldoCalculado.Properties.MaxLength = 15;
            this.txtSaldoCalculado.Size = new System.Drawing.Size(86, 22);
            this.txtSaldoCalculado.TabIndex = 15;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(528, 10);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(79, 13);
            this.labelControl4.TabIndex = 16;
            this.labelControl4.Text = "Saldo Calculado:";
            // 
            // txtSaldoDisponivel
            // 
            this.txtSaldoDisponivel.Enabled = false;
            this.txtSaldoDisponivel.Location = new System.Drawing.Point(528, 82);
            this.txtSaldoDisponivel.Name = "txtSaldoDisponivel";
            this.txtSaldoDisponivel.Properties.Mask.EditMask = "n4";
            this.txtSaldoDisponivel.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtSaldoDisponivel.Properties.MaxLength = 15;
            this.txtSaldoDisponivel.Size = new System.Drawing.Size(86, 22);
            this.txtSaldoDisponivel.TabIndex = 13;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(528, 62);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(81, 13);
            this.labelControl3.TabIndex = 14;
            this.labelControl3.Text = "Saldo Disponível:";
            // 
            // txtNecessidade
            // 
            this.txtNecessidade.Enabled = false;
            this.txtNecessidade.Location = new System.Drawing.Point(413, 30);
            this.txtNecessidade.Name = "txtNecessidade";
            this.txtNecessidade.Size = new System.Drawing.Size(86, 22);
            this.txtNecessidade.TabIndex = 12;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(413, 10);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(64, 13);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "Necessidade:";
            // 
            // newLookupprodutosubstituto
            // 
            this.newLookupprodutosubstituto.Abrir_Primeiro = ITGProducao.Controles.NewLookup.AbrirPrimeiro.Visao;
            this.newLookupprodutosubstituto.CampoCodigo_Igual_a = ITGProducao.Controles.NewLookup.CampoCodigoIguala.CampoCodigoBD;
            this.newLookupprodutosubstituto.CampoCodigoBD = "CODPRODUTO";
            this.newLookupprodutosubstituto.CampoCodigoInterno = "CODIGOAUXILIAR";
            this.newLookupprodutosubstituto.CampoDescricaoBD = "NOME";
            this.newLookupprodutosubstituto.CarregaDescricaoSemFiltro = false;
            this.newLookupprodutosubstituto.Codigo_MaxLenght = 15;
            this.newLookupprodutosubstituto.Formulario_Cadastro = "PS.Glb.New.Cadastros.frmCadastroProduto";
            this.newLookupprodutosubstituto.Formulario_Filtro = "PS.Glb.New.Filtro.frmFiltroProduto ";
            this.newLookupprodutosubstituto.Formulario_Visao = "PS.Glb.New.Visao.frmVisaoProduto";
            newlookup_WhereVisao7.NomeColuna = "ATIVO";
            newlookup_WhereVisao7.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao7.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.Null;
            newlookup_WhereVisao7.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao7.ValorFixo = "1";
            newlookup_WhereVisao7.Variavel_Interna = false;
            newlookup_WhereVisao8.NomeColuna = "ULTIMONIVEL";
            newlookup_WhereVisao8.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao8.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.Null;
            newlookup_WhereVisao8.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao8.ValorFixo = "1";
            newlookup_WhereVisao8.Variavel_Interna = false;
            newlookup_WhereVisao9.NomeColuna = "VPRODUTO.CODPRODUTO";
            newlookup_WhereVisao9.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao9.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.CampoCodigo_NewLookup;
            newlookup_WhereVisao9.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao9.ValorFixo = null;
            newlookup_WhereVisao9.Variavel_Interna = true;
            this.newLookupprodutosubstituto.Grid_WhereVisao.Add(newlookup_WhereVisao7);
            this.newLookupprodutosubstituto.Grid_WhereVisao.Add(newlookup_WhereVisao8);
            this.newLookupprodutosubstituto.Grid_WhereVisao.Add(newlookup_WhereVisao9);
            this.newLookupprodutosubstituto.Location = new System.Drawing.Point(11, 58);
            this.newLookupprodutosubstituto.MensagemCodigoVazio = null;
            this.newLookupprodutosubstituto.mensagemErrorProvider = null;
            this.newLookupprodutosubstituto.Name = "newLookupprodutosubstituto";
            this.newLookupprodutosubstituto.Projeto_Formularios = "PS.Glb";
            this.newLookupprodutosubstituto.Size = new System.Drawing.Size(374, 46);
            this.newLookupprodutosubstituto.TabelaBD = "VPRODUTO";
            this.newLookupprodutosubstituto.TabIndex = 8;
            this.newLookupprodutosubstituto.Titulo = "Produto Substituto:";
            this.newLookupprodutosubstituto.ValorCodigoInterno = "";
            this.newLookupprodutosubstituto.whereParametros = null;
            this.newLookupprodutosubstituto.whereVisao = null;
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.EditValue = "0";
            this.txtQuantidade.Location = new System.Drawing.Point(413, 82);
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Properties.Mask.EditMask = "n4";
            this.txtQuantidade.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtQuantidade.Properties.MaxLength = 15;
            this.txtQuantidade.Size = new System.Drawing.Size(86, 22);
            this.txtQuantidade.TabIndex = 6;
            this.txtQuantidade.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtQuantidade_KeyUp);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(413, 62);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 13);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Quantidade:";
            // 
            // lookupproduto
            // 
            this.lookupproduto.Abrir_Primeiro = ITGProducao.Controles.NewLookup.AbrirPrimeiro.Visao;
            this.lookupproduto.CampoCodigo_Igual_a = ITGProducao.Controles.NewLookup.CampoCodigoIguala.CampoCodigoBD;
            this.lookupproduto.CampoCodigoBD = "CODPRODUTO";
            this.lookupproduto.CampoCodigoInterno = "CODIGOAUXILIAR";
            this.lookupproduto.CampoDescricaoBD = "NOME";
            this.lookupproduto.CarregaDescricaoSemFiltro = false;
            this.lookupproduto.Codigo_MaxLenght = 15;
            this.lookupproduto.Formulario_Cadastro = "PS.Glb.New.Cadastros.frmCadastroProduto";
            this.lookupproduto.Formulario_Filtro = "PS.Glb.New.Filtro.frmFiltroProduto ";
            this.lookupproduto.Formulario_Visao = "PS.Glb.New.Visao.frmVisaoProduto";
            newlookup_WhereVisao10.NomeColuna = "ATIVO";
            newlookup_WhereVisao10.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao10.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.Null;
            newlookup_WhereVisao10.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao10.ValorFixo = "1";
            newlookup_WhereVisao10.Variavel_Interna = false;
            newlookup_WhereVisao11.NomeColuna = "ULTIMONIVEL";
            newlookup_WhereVisao11.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao11.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.Null;
            newlookup_WhereVisao11.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao11.ValorFixo = "1";
            newlookup_WhereVisao11.Variavel_Interna = false;
            newlookup_WhereVisao12.NomeColuna = "VPRODUTO.CODPRODUTO";
            newlookup_WhereVisao12.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao12.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.CampoCodigo_NewLookup;
            newlookup_WhereVisao12.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao12.ValorFixo = null;
            newlookup_WhereVisao12.Variavel_Interna = true;
            this.lookupproduto.Grid_WhereVisao.Add(newlookup_WhereVisao10);
            this.lookupproduto.Grid_WhereVisao.Add(newlookup_WhereVisao11);
            this.lookupproduto.Grid_WhereVisao.Add(newlookup_WhereVisao12);
            this.lookupproduto.Location = new System.Drawing.Point(11, 6);
            this.lookupproduto.MensagemCodigoVazio = null;
            this.lookupproduto.mensagemErrorProvider = null;
            this.lookupproduto.Name = "lookupproduto";
            this.lookupproduto.Projeto_Formularios = "PS.Glb";
            this.lookupproduto.Size = new System.Drawing.Size(374, 46);
            this.lookupproduto.TabelaBD = "VPRODUTO";
            this.lookupproduto.TabIndex = 0;
            this.lookupproduto.Titulo = "Produto:";
            this.lookupproduto.ValorCodigoInterno = "";
            this.lookupproduto.whereParametros = null;
            this.lookupproduto.whereVisao = null;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.gridComponentes);
            this.groupBox2.Location = new System.Drawing.Point(6, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(713, 317);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Componentes Substitutos";
            // 
            // gridComponentes
            // 
            this.gridComponentes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridComponentes.Location = new System.Drawing.Point(3, 16);
            this.gridComponentes.MainView = this.gridView1;
            this.gridComponentes.Name = "gridComponentes";
            this.gridComponentes.Size = new System.Drawing.Size(707, 298);
            this.gridComponentes.TabIndex = 0;
            this.gridComponentes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridComponentes.DoubleClick += new System.EventHandler(this.gridComponentes_DoubleClick);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridComponentes;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsDetail.ShowDetailTabs = false;
            this.gridView1.OptionsFind.FindDelay = 100;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.MasterRowExpanded += new DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventHandler(this.gridView1_MasterRowExpanded);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(438, 14);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 42);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Visible = false;
            // 
            // btnIncluiComponenteOP
            // 
            this.btnIncluiComponenteOP.Image = ((System.Drawing.Image)(resources.GetObject("btnIncluiComponenteOP.Image")));
            this.btnIncluiComponenteOP.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnIncluiComponenteOP.Location = new System.Drawing.Point(544, 14);
            this.btnIncluiComponenteOP.Name = "btnIncluiComponenteOP";
            this.btnIncluiComponenteOP.Size = new System.Drawing.Size(176, 42);
            this.btnIncluiComponenteOP.TabIndex = 9;
            this.btnIncluiComponenteOP.Text = "Incluir Componente na OP";
            this.btnIncluiComponenteOP.Click += new System.EventHandler(this.btnIncluiComponenteOP_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // FrmVisaoComponenteSubstituto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 554);
            this.Controls.Add(this.splitContainer2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmVisaoComponenteSubstituto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Componente Substituto";
            this.Load += new System.EventHandler(this.FrmVisaoComponenteSubstituto_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSaldoCalculado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSaldoDisponivel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNecessidade.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuantidade.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridComponentes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private Controles.NewLookup lookupproduto;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl gridComponentes;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.TextEdit txtQuantidade;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnIncluiComponenteOP;
        private Controles.NewLookup newLookupprodutosubstituto;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtNecessidade;
        private DevExpress.XtraEditors.TextEdit txtSaldoDisponivel;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtSaldoCalculado;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtFator;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}