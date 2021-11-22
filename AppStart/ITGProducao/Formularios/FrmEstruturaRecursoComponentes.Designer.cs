namespace ITGProducao.Formularios
{
    partial class FrmEstruturaRecursoComponentes
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
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao2 = new ITGProducao.Controles.Newlookup_WhereVisao();
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao3 = new ITGProducao.Controles.Newlookup_WhereVisao();
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao4 = new ITGProducao.Controles.Newlookup_WhereVisao();
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao5 = new ITGProducao.Controles.Newlookup_WhereVisao();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEstruturaRecursoComponentes));
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtquantidade = new DevExpress.XtraEditors.TextEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.txtCodRecRoteiro = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.lookupunidade = new ITGProducao.Controles.NewLookup();
            this.lookupproduto = new ITGProducao.Controles.NewLookup();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtquantidade.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodRecRoteiro.Properties)).BeginInit();
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
            this.splitContainer2.Size = new System.Drawing.Size(745, 423);
            this.splitContainer2.SplitterDistance = 337;
            this.splitContainer2.TabIndex = 14;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(745, 337);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtquantidade);
            this.tabPage1.Controls.Add(this.lookupunidade);
            this.tabPage1.Controls.Add(this.labelControl17);
            this.tabPage1.Controls.Add(this.txtCodRecRoteiro);
            this.tabPage1.Controls.Add(this.labelControl6);
            this.tabPage1.Controls.Add(this.lookupproduto);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(737, 311);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtquantidade
            // 
            this.txtquantidade.Location = new System.Drawing.Point(28, 90);
            this.txtquantidade.Name = "txtquantidade";
            this.txtquantidade.Properties.Mask.EditMask = "n4";
            this.txtquantidade.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtquantidade.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtquantidade.Properties.MaxLength = 40;
            this.txtquantidade.Size = new System.Drawing.Size(135, 20);
            this.txtquantidade.TabIndex = 34;
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(26, 71);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(60, 13);
            this.labelControl17.TabIndex = 32;
            this.labelControl17.Text = "Quantidade:";
            // 
            // txtCodRecRoteiro
            // 
            this.txtCodRecRoteiro.Location = new System.Drawing.Point(28, 189);
            this.txtCodRecRoteiro.Name = "txtCodRecRoteiro";
            this.txtCodRecRoteiro.Properties.MaxLength = 50;
            this.txtCodRecRoteiro.Size = new System.Drawing.Size(358, 20);
            this.txtCodRecRoteiro.TabIndex = 29;
            this.txtCodRecRoteiro.Visible = false;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(28, 170);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(103, 13);
            this.labelControl6.TabIndex = 30;
            this.labelControl6.Text = "Cód Recurso Roteiro:";
            this.labelControl6.Visible = false;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(577, 27);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(658, 27);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
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
            this.lookupunidade.Location = new System.Drawing.Point(24, 116);
            this.lookupunidade.MensagemCodigoVazio = null;
            this.lookupunidade.mensagemErrorProvider = null;
            this.lookupunidade.Name = "lookupunidade";
            this.lookupunidade.Projeto_Formularios = "ITGProducao";
            this.lookupunidade.Size = new System.Drawing.Size(361, 46);
            this.lookupunidade.TabelaBD = "VUNID";
            this.lookupunidade.TabIndex = 33;
            this.lookupunidade.Titulo = "Unidade:";
            this.lookupunidade.ValorCodigoInterno = null;
            this.lookupunidade.whereParametros = null;
            this.lookupunidade.whereVisao = null;
            // 
            // lookupproduto
            // 
            this.lookupproduto.Abrir_Primeiro = ITGProducao.Controles.NewLookup.AbrirPrimeiro.Visao;
            this.lookupproduto.CampoCodigo_Igual_a = ITGProducao.Controles.NewLookup.CampoCodigoIguala.CampoCodigoBD;
            this.lookupproduto.CampoCodigoBD = "CODPRODUTO";
            this.lookupproduto.CampoCodigoInterno = "CODIGOAUXILIAR";
            this.lookupproduto.CampoDescricaoBD = "NOME";
            this.lookupproduto.Codigo_MaxLenght = 15;
            this.lookupproduto.Formulario_Cadastro = "PS.Glb.New.Cadastros.frmCadastroProduto";
            this.lookupproduto.Formulario_Filtro = "PS.Glb.New.Filtro.frmFiltroProduto ";
            this.lookupproduto.Formulario_Visao = "PS.Glb.New.Visao.frmVisaoProduto";
            newlookup_WhereVisao3.NomeColuna = "ATIVO";
            newlookup_WhereVisao3.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao3.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.Null;
            newlookup_WhereVisao3.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao3.ValorFixo = "1";
            newlookup_WhereVisao3.Variavel_Interna = false;
            newlookup_WhereVisao4.NomeColuna = "ULTIMONIVEL";
            newlookup_WhereVisao4.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao4.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.Null;
            newlookup_WhereVisao4.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao4.ValorFixo = "1";
            newlookup_WhereVisao4.Variavel_Interna = false;
            newlookup_WhereVisao5.NomeColuna = "VPRODUTO.CODPRODUTO";
            newlookup_WhereVisao5.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao5.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.CampoCodigo_NewLookup;
            newlookup_WhereVisao5.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao5.ValorFixo = null;
            newlookup_WhereVisao5.Variavel_Interna = true;
            this.lookupproduto.Grid_WhereVisao.Add(newlookup_WhereVisao3);
            this.lookupproduto.Grid_WhereVisao.Add(newlookup_WhereVisao4);
            this.lookupproduto.Grid_WhereVisao.Add(newlookup_WhereVisao5);
            this.lookupproduto.Location = new System.Drawing.Point(23, 19);
            this.lookupproduto.MensagemCodigoVazio = null;
            this.lookupproduto.mensagemErrorProvider = null;
            this.lookupproduto.Name = "lookupproduto";
            this.lookupproduto.Projeto_Formularios = "PS.Glb";
            this.lookupproduto.Size = new System.Drawing.Size(364, 46);
            this.lookupproduto.TabelaBD = "VPRODUTO";
            this.lookupproduto.TabIndex = 28;
            this.lookupproduto.Titulo = "Produto:";
            this.lookupproduto.ValorCodigoInterno = null;
            this.lookupproduto.whereParametros = null;
            this.lookupproduto.whereVisao = null;
            // 
            // FrmEstruturaRecursoComponentes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 423);
            this.Controls.Add(this.splitContainer2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmEstruturaRecursoComponentes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Componentes";
            this.Load += new System.EventHandler(this.FrmEstruturaRecursoComponentes_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtquantidade.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodRecRoteiro.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private Controles.NewLookup lookupproduto;
        private DevExpress.XtraEditors.TextEdit txtCodRecRoteiro;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private Controles.NewLookup lookupunidade;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private DevExpress.XtraEditors.TextEdit txtquantidade;
    }
}