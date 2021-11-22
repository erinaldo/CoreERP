namespace PS.Glb.New.Cadastros
{
    partial class frmProcessosVisaoTabelaPreco
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
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao1 = new ITGProducao.Controles.Newlookup_WhereVisao();
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao2 = new ITGProducao.Controles.Newlookup_WhereVisao();
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao3 = new ITGProducao.Controles.Newlookup_WhereVisao();
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao4 = new ITGProducao.Controles.Newlookup_WhereVisao();
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao5 = new ITGProducao.Controles.Newlookup_WhereVisao();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProcessosVisaoTabelaPreco));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lpPorcentagem = new DevExpress.XtraEditors.LabelControl();
            this.chkProdutoEspecifico = new DevExpress.XtraEditors.CheckEdit();
            this.tbValorProcesso = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancelarAtual = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalvarAtual = new DevExpress.XtraEditors.SimpleButton();
            this.lpFilial = new ITGProducao.Controles.NewLookup();
            this.lpProduto = new ITGProducao.Controles.NewLookup();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkProdutoEspecifico.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbValorProcesso.Properties)).BeginInit();
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
            this.splitContainer1.Panel2.Controls.Add(this.btnCancelarAtual);
            this.splitContainer1.Panel2.Controls.Add(this.btnSalvarAtual);
            this.splitContainer1.Size = new System.Drawing.Size(471, 267);
            this.splitContainer1.SplitterDistance = 226;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(471, 226);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lpPorcentagem);
            this.tabPage1.Controls.Add(this.lpFilial);
            this.tabPage1.Controls.Add(this.chkProdutoEspecifico);
            this.tabPage1.Controls.Add(this.lpProduto);
            this.tabPage1.Controls.Add(this.tbValorProcesso);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(463, 200);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lpPorcentagem
            // 
            this.lpPorcentagem.Location = new System.Drawing.Point(151, 29);
            this.lpPorcentagem.Name = "lpPorcentagem";
            this.lpPorcentagem.Size = new System.Drawing.Size(11, 13);
            this.lpPorcentagem.TabIndex = 21;
            this.lpPorcentagem.Text = "%";
            this.lpPorcentagem.Visible = false;
            // 
            // chkProdutoEspecifico
            // 
            this.chkProdutoEspecifico.Location = new System.Drawing.Point(9, 53);
            this.chkProdutoEspecifico.Name = "chkProdutoEspecifico";
            this.chkProdutoEspecifico.Properties.Caption = "Produto específiico?";
            this.chkProdutoEspecifico.Size = new System.Drawing.Size(136, 19);
            this.chkProdutoEspecifico.TabIndex = 19;
            this.chkProdutoEspecifico.CheckedChanged += new System.EventHandler(this.chkProdutoEspecifico_CheckedChanged);
            // 
            // tbValorProcesso
            // 
            this.tbValorProcesso.Location = new System.Drawing.Point(6, 26);
            this.tbValorProcesso.Name = "tbValorProcesso";
            this.tbValorProcesso.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbValorProcesso.Size = new System.Drawing.Size(139, 20);
            this.tbValorProcesso.TabIndex = 17;
            this.tbValorProcesso.Leave += new System.EventHandler(this.tbValorProcesso_Leave);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 7);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(28, 13);
            this.labelControl1.TabIndex = 16;
            this.labelControl1.Text = "Valor:";
            // 
            // btnCancelarAtual
            // 
            this.btnCancelarAtual.Location = new System.Drawing.Point(392, 3);
            this.btnCancelarAtual.Name = "btnCancelarAtual";
            this.btnCancelarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnCancelarAtual.TabIndex = 45;
            this.btnCancelarAtual.Text = "Cancelar";
            this.btnCancelarAtual.Click += new System.EventHandler(this.btnCancelarAtual_Click);
            // 
            // btnSalvarAtual
            // 
            this.btnSalvarAtual.Location = new System.Drawing.Point(311, 3);
            this.btnSalvarAtual.Name = "btnSalvarAtual";
            this.btnSalvarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnSalvarAtual.TabIndex = 43;
            this.btnSalvarAtual.Text = "Inserir";
            this.btnSalvarAtual.Click += new System.EventHandler(this.btnSalvarAtual_Click);
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
            this.lpFilial.Enabled = false;
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
            this.lpFilial.Location = new System.Drawing.Point(6, 78);
            this.lpFilial.MensagemCodigoVazio = null;
            this.lpFilial.mensagemErrorProvider = null;
            this.lpFilial.Name = "lpFilial";
            this.lpFilial.Projeto_Formularios = "ITGProducao";
            this.lpFilial.Size = new System.Drawing.Size(454, 46);
            this.lpFilial.TabelaBD = "GFILIAL";
            this.lpFilial.TabIndex = 20;
            this.lpFilial.Titulo = "Filial";
            this.lpFilial.ValorCodigoInterno = null;
            this.lpFilial.whereParametros = null;
            this.lpFilial.whereVisao = null;
            this.lpFilial.Leave += new System.EventHandler(this.lpFilial_Leave);
            // 
            // lpProduto
            // 
            this.lpProduto.Abrir_Primeiro = ITGProducao.Controles.NewLookup.AbrirPrimeiro.Visao;
            this.lpProduto.CampoCodigo_Igual_a = ITGProducao.Controles.NewLookup.CampoCodigoIguala.CampoCodigoBD;
            this.lpProduto.CampoCodigoBD = "CODPRODUTO";
            this.lpProduto.CampoCodigoInterno = "";
            this.lpProduto.CampoDescricaoBD = "NOME";
            this.lpProduto.CarregaDescricaoSemFiltro = false;
            this.lpProduto.Codigo_MaxLenght = 15;
            this.lpProduto.Enabled = false;
            this.lpProduto.Formulario_Cadastro = "PS.Glb.New.Cadastros.frmCadastroProduto";
            this.lpProduto.Formulario_Filtro = "PS.Glb.New.Filtro.frmFiltroProduto ";
            this.lpProduto.Formulario_Visao = "PS.Glb.New.Visao.frmVisaoProduto";
            newlookup_WhereVisao2.NomeColuna = "ATIVO";
            newlookup_WhereVisao2.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao2.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.Null;
            newlookup_WhereVisao2.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao2.ValorFixo = "1";
            newlookup_WhereVisao2.Variavel_Interna = false;
            newlookup_WhereVisao3.NomeColuna = "ULTIMONIVEL";
            newlookup_WhereVisao3.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao3.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.Null;
            newlookup_WhereVisao3.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao3.ValorFixo = "1";
            newlookup_WhereVisao3.Variavel_Interna = false;
            newlookup_WhereVisao4.NomeColuna = "VPRODUTO.CODPRODUTO";
            newlookup_WhereVisao4.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao4.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.CampoCodigo_NewLookup;
            newlookup_WhereVisao4.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao4.ValorFixo = null;
            newlookup_WhereVisao4.Variavel_Interna = true;
            newlookup_WhereVisao5.NomeColuna = "CODPRODUTO";
            newlookup_WhereVisao5.NomeColunaValor_SelectQuery = "CODPRODUTO";
            newlookup_WhereVisao5.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.SelectQuery;
            newlookup_WhereVisao5.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.In;
            newlookup_WhereVisao5.ValorFixo = "SELECT DISTINCT(CODPRODUTO) FROM VCLIFORTABPRECOITEM WHERE IDTABELA IN (\'\')";
            newlookup_WhereVisao5.Variavel_Interna = true;
            this.lpProduto.Grid_WhereVisao.Add(newlookup_WhereVisao2);
            this.lpProduto.Grid_WhereVisao.Add(newlookup_WhereVisao3);
            this.lpProduto.Grid_WhereVisao.Add(newlookup_WhereVisao4);
            this.lpProduto.Grid_WhereVisao.Add(newlookup_WhereVisao5);
            this.lpProduto.Location = new System.Drawing.Point(7, 130);
            this.lpProduto.MensagemCodigoVazio = null;
            this.lpProduto.mensagemErrorProvider = null;
            this.lpProduto.Name = "lpProduto";
            this.lpProduto.Projeto_Formularios = "PS.Glb";
            this.lpProduto.Size = new System.Drawing.Size(452, 46);
            this.lpProduto.TabelaBD = "VPRODUTO";
            this.lpProduto.TabIndex = 18;
            this.lpProduto.Titulo = "Código do Produto:";
            this.lpProduto.ValorCodigoInterno = null;
            this.lpProduto.whereParametros = null;
            this.lpProduto.whereVisao = null;
            // 
            // frmProcessosVisaoTabelaPreco
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 267);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmProcessosVisaoTabelaPreco";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Processos";
            this.Load += new System.EventHandler(this.frmProcessosTabelaPreco_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkProdutoEspecifico.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbValorProcesso.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.SimpleButton btnSalvarAtual;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit tbValorProcesso;
        private DevExpress.XtraEditors.SimpleButton btnCancelarAtual;
        private ITGProducao.Controles.NewLookup lpProduto;
        private DevExpress.XtraEditors.CheckEdit chkProdutoEspecifico;
        private ITGProducao.Controles.NewLookup lpFilial;
        private DevExpress.XtraEditors.LabelControl lpPorcentagem;
    }
}