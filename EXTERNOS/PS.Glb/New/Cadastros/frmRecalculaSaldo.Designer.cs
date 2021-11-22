namespace PS.Glb.New.Cadastros
{
    partial class frmRecalculaSaldo
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
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao8 = new ITGProducao.Controles.Newlookup_WhereVisao();
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao9 = new ITGProducao.Controles.Newlookup_WhereVisao();
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao10 = new ITGProducao.Controles.Newlookup_WhereVisao();
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao11 = new ITGProducao.Controles.Newlookup_WhereVisao();
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao12 = new ITGProducao.Controles.Newlookup_WhereVisao();
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao13 = new ITGProducao.Controles.Newlookup_WhereVisao();
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao14 = new ITGProducao.Controles.Newlookup_WhereVisao();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRecalculaSaldo));
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbFechamentoEstoque = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lookupfilial = new ITGProducao.Controles.NewLookup();
            this.lookupprodutofinal = new ITGProducao.Controles.NewLookup();
            this.lookupprodutoinicial = new ITGProducao.Controles.NewLookup();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::PS.Glb.ERP.Global.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbFechamentoEstoque.Properties)).BeginInit();
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
            this.splitContainer2.Panel2.Controls.Add(this.btnOk);
            this.splitContainer2.Panel2.Controls.Add(this.btnCancelar);
            this.splitContainer2.Size = new System.Drawing.Size(508, 359);
            this.splitContainer2.SplitterDistance = 286;
            this.splitContainer2.TabIndex = 11;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(508, 286);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(500, 260);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tbFechamentoEstoque);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.lookupfilial);
            this.groupBox1.Controls.Add(this.lookupprodutofinal);
            this.groupBox1.Controls.Add(this.lookupprodutoinicial);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(486, 248);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dados do Filtro";
            // 
            // tbFechamentoEstoque
            // 
            this.tbFechamentoEstoque.EnterMoveNextControl = true;
            this.tbFechamentoEstoque.Location = new System.Drawing.Point(383, 42);
            this.tbFechamentoEstoque.Name = "tbFechamentoEstoque";
            this.tbFechamentoEstoque.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.tbFechamentoEstoque.Properties.Appearance.Options.UseBackColor = true;
            this.tbFechamentoEstoque.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tbFechamentoEstoque.Properties.ReadOnly = true;
            this.tbFechamentoEstoque.Size = new System.Drawing.Size(62, 18);
            this.tbFechamentoEstoque.TabIndex = 41;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(248, 44);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(129, 13);
            this.labelControl1.TabIndex = 40;
            this.labelControl1.Text = "Data fechamento Estoque:";
            // 
            // lookupfilial
            // 
            this.lookupfilial.Abrir_Primeiro = ITGProducao.Controles.NewLookup.AbrirPrimeiro.Visao;
            this.lookupfilial.CampoCodigo_Igual_a = ITGProducao.Controles.NewLookup.CampoCodigoIguala.CampoCodigoBD;
            this.lookupfilial.CampoCodigoBD = "CODFILIAL";
            this.lookupfilial.CampoCodigoInterno = null;
            this.lookupfilial.CampoDescricaoBD = "NOMEFANTASIA";
            this.lookupfilial.CarregaDescricaoSemFiltro = true;
            this.lookupfilial.Codigo_MaxLenght = 10;
            this.lookupfilial.Formulario_Cadastro = null;
            this.lookupfilial.Formulario_Filtro = null;
            this.lookupfilial.Formulario_Visao = "ITGProducao.Visao.FrmVisaoFiliais";
            newlookup_WhereVisao8.NomeColuna = "CODFILIAL";
            newlookup_WhereVisao8.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao8.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.CampoCodigo_NewLookup;
            newlookup_WhereVisao8.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao8.ValorFixo = null;
            newlookup_WhereVisao8.Variavel_Interna = true;
            this.lookupfilial.Grid_WhereVisao.Add(newlookup_WhereVisao8);
            this.lookupfilial.Location = new System.Drawing.Point(37, 54);
            this.lookupfilial.MensagemCodigoVazio = null;
            this.lookupfilial.mensagemErrorProvider = null;
            this.lookupfilial.Name = "lookupfilial";
            this.lookupfilial.Projeto_Formularios = "ITGProducao";
            this.lookupfilial.Size = new System.Drawing.Size(412, 46);
            this.lookupfilial.TabelaBD = "GFILIAL";
            this.lookupfilial.TabIndex = 39;
            this.lookupfilial.Titulo = "Filial";
            this.lookupfilial.ValorCodigoInterno = "";
            this.lookupfilial.whereParametros = null;
            this.lookupfilial.whereVisao = null;
            // 
            // lookupprodutofinal
            // 
            this.lookupprodutofinal.Abrir_Primeiro = ITGProducao.Controles.NewLookup.AbrirPrimeiro.Visao;
            this.lookupprodutofinal.CampoCodigo_Igual_a = ITGProducao.Controles.NewLookup.CampoCodigoIguala.CampoCodigoBD;
            this.lookupprodutofinal.CampoCodigoBD = "CODPRODUTO";
            this.lookupprodutofinal.CampoCodigoInterno = "CODIGOAUXILIAR";
            this.lookupprodutofinal.CampoDescricaoBD = "NOME";
            this.lookupprodutofinal.CarregaDescricaoSemFiltro = false;
            this.lookupprodutofinal.Codigo_MaxLenght = 15;
            this.lookupprodutofinal.Formulario_Cadastro = "PS.Glb.New.Cadastros.frmCadastroProduto";
            this.lookupprodutofinal.Formulario_Filtro = "PS.Glb.New.Filtro.frmFiltroProduto ";
            this.lookupprodutofinal.Formulario_Visao = "PS.Glb.New.Visao.frmVisaoProduto";
            newlookup_WhereVisao9.NomeColuna = "ATIVO";
            newlookup_WhereVisao9.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao9.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.Null;
            newlookup_WhereVisao9.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao9.ValorFixo = "1";
            newlookup_WhereVisao9.Variavel_Interna = false;
            newlookup_WhereVisao10.NomeColuna = "ULTIMONIVEL";
            newlookup_WhereVisao10.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao10.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.Null;
            newlookup_WhereVisao10.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao10.ValorFixo = "1";
            newlookup_WhereVisao10.Variavel_Interna = false;
            newlookup_WhereVisao11.NomeColuna = "VPRODUTO.CODPRODUTO";
            newlookup_WhereVisao11.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao11.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.CampoCodigo_NewLookup;
            newlookup_WhereVisao11.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao11.ValorFixo = null;
            newlookup_WhereVisao11.Variavel_Interna = true;
            this.lookupprodutofinal.Grid_WhereVisao.Add(newlookup_WhereVisao9);
            this.lookupprodutofinal.Grid_WhereVisao.Add(newlookup_WhereVisao10);
            this.lookupprodutofinal.Grid_WhereVisao.Add(newlookup_WhereVisao11);
            this.lookupprodutofinal.Location = new System.Drawing.Point(37, 158);
            this.lookupprodutofinal.MensagemCodigoVazio = null;
            this.lookupprodutofinal.mensagemErrorProvider = null;
            this.lookupprodutofinal.Name = "lookupprodutofinal";
            this.lookupprodutofinal.Projeto_Formularios = "PS.Glb";
            this.lookupprodutofinal.Size = new System.Drawing.Size(412, 46);
            this.lookupprodutofinal.TabelaBD = "VPRODUTO";
            this.lookupprodutofinal.TabIndex = 37;
            this.lookupprodutofinal.Titulo = "Produto final:";
            this.lookupprodutofinal.ValorCodigoInterno = "";
            this.lookupprodutofinal.whereParametros = null;
            this.lookupprodutofinal.whereVisao = null;
            // 
            // lookupprodutoinicial
            // 
            this.lookupprodutoinicial.Abrir_Primeiro = ITGProducao.Controles.NewLookup.AbrirPrimeiro.Visao;
            this.lookupprodutoinicial.CampoCodigo_Igual_a = ITGProducao.Controles.NewLookup.CampoCodigoIguala.CampoCodigoBD;
            this.lookupprodutoinicial.CampoCodigoBD = "CODPRODUTO";
            this.lookupprodutoinicial.CampoCodigoInterno = "CODIGOAUXILIAR";
            this.lookupprodutoinicial.CampoDescricaoBD = "NOME";
            this.lookupprodutoinicial.CarregaDescricaoSemFiltro = false;
            this.lookupprodutoinicial.Codigo_MaxLenght = 15;
            this.lookupprodutoinicial.Formulario_Cadastro = "PS.Glb.New.Cadastros.frmCadastroProduto";
            this.lookupprodutoinicial.Formulario_Filtro = "PS.Glb.New.Filtro.frmFiltroProduto ";
            this.lookupprodutoinicial.Formulario_Visao = "PS.Glb.New.Visao.frmVisaoProduto";
            newlookup_WhereVisao12.NomeColuna = "ATIVO";
            newlookup_WhereVisao12.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao12.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.Null;
            newlookup_WhereVisao12.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao12.ValorFixo = "1";
            newlookup_WhereVisao12.Variavel_Interna = false;
            newlookup_WhereVisao13.NomeColuna = "ULTIMONIVEL";
            newlookup_WhereVisao13.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao13.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.Null;
            newlookup_WhereVisao13.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao13.ValorFixo = "1";
            newlookup_WhereVisao13.Variavel_Interna = false;
            newlookup_WhereVisao14.NomeColuna = "VPRODUTO.CODPRODUTO";
            newlookup_WhereVisao14.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao14.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.CampoCodigo_NewLookup;
            newlookup_WhereVisao14.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao14.ValorFixo = null;
            newlookup_WhereVisao14.Variavel_Interna = true;
            this.lookupprodutoinicial.Grid_WhereVisao.Add(newlookup_WhereVisao12);
            this.lookupprodutoinicial.Grid_WhereVisao.Add(newlookup_WhereVisao13);
            this.lookupprodutoinicial.Grid_WhereVisao.Add(newlookup_WhereVisao14);
            this.lookupprodutoinicial.Location = new System.Drawing.Point(37, 106);
            this.lookupprodutoinicial.MensagemCodigoVazio = null;
            this.lookupprodutoinicial.mensagemErrorProvider = null;
            this.lookupprodutoinicial.Name = "lookupprodutoinicial";
            this.lookupprodutoinicial.Projeto_Formularios = "PS.Glb";
            this.lookupprodutoinicial.Size = new System.Drawing.Size(412, 46);
            this.lookupprodutoinicial.TabelaBD = "VPRODUTO";
            this.lookupprodutoinicial.TabIndex = 36;
            this.lookupprodutoinicial.Titulo = "Produto Inicial:";
            this.lookupprodutoinicial.ValorCodigoInterno = "";
            this.lookupprodutoinicial.whereParametros = null;
            this.lookupprodutoinicial.whereVisao = null;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(340, 27);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Recalcular";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(421, 27);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // frmRecalculaSaldo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 359);
            this.Controls.Add(this.splitContainer2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRecalculaSaldo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recalcula Saldo";
            this.Load += new System.EventHandler(this.frmRecalculaSaldo_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbFechamentoEstoque.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private ITGProducao.Controles.NewLookup lookupprodutofinal;
        private ITGProducao.Controles.NewLookup lookupprodutoinicial;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private ITGProducao.Controles.NewLookup lookupfilial;
        private DevExpress.XtraEditors.TextEdit tbFechamentoEstoque;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}