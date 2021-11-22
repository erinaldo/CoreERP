namespace PS.Glb.New.Cadastros
{
    partial class frmCadastroContaCorrente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastroContaCorrente));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lpAgencia = new ITGProducao.Controles.NewLookup();
            this.lpBanco = new ITGProducao.Controles.NewLookup();
            this.tbDigito = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.tbCodConta = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
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
            ((System.ComponentModel.ISupportInitialize)(this.tbDigito.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbCodConta.Properties)).BeginInit();
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
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCancelarAtual);
            this.splitContainer1.Panel2.Controls.Add(this.btnOKAtual);
            this.splitContainer1.Panel2.Controls.Add(this.btnSalvarAtual);
            this.splitContainer1.Size = new System.Drawing.Size(503, 228);
            this.splitContainer1.SplitterDistance = 188;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(503, 188);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lpAgencia);
            this.tabPage1.Controls.Add(this.lpBanco);
            this.tabPage1.Controls.Add(this.tbDigito);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.tbCodConta);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(495, 162);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lpAgencia
            // 
            this.lpAgencia.Abrir_Primeiro = ITGProducao.Controles.NewLookup.AbrirPrimeiro.Visao;
            this.lpAgencia.CampoCodigo_Igual_a = ITGProducao.Controles.NewLookup.CampoCodigoIguala.CampoCodigoBD;
            this.lpAgencia.CampoCodigoBD = "CODAGENCIA";
            this.lpAgencia.CampoCodigoInterno = null;
            this.lpAgencia.CampoDescricaoBD = "NOME";
            this.lpAgencia.CarregaDescricaoSemFiltro = false;
            this.lpAgencia.Codigo_MaxLenght = 0;
            this.lpAgencia.Formulario_Cadastro = "PS.Glb.New.Cadastros.frmCadastroAgencia";
            this.lpAgencia.Formulario_Filtro = null;
            this.lpAgencia.Formulario_Visao = "PS.Glb.New.Visao.frmVisaoAgencia";
            newlookup_WhereVisao1.NomeColuna = "CODAGENCIA";
            newlookup_WhereVisao1.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao1.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.CampoCodigo_NewLookup;
            newlookup_WhereVisao1.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao1.ValorFixo = null;
            newlookup_WhereVisao1.Variavel_Interna = true;
            this.lpAgencia.Grid_WhereVisao.Add(newlookup_WhereVisao1);
            this.lpAgencia.Location = new System.Drawing.Point(6, 104);
            this.lpAgencia.MensagemCodigoVazio = null;
            this.lpAgencia.mensagemErrorProvider = null;
            this.lpAgencia.Name = "lpAgencia";
            this.lpAgencia.Projeto_Formularios = "PS.Glb";
            this.lpAgencia.Size = new System.Drawing.Size(481, 46);
            this.lpAgencia.TabelaBD = "GAGENCIA";
            this.lpAgencia.TabIndex = 25;
            this.lpAgencia.Titulo = "Agência";
            this.lpAgencia.ValorCodigoInterno = "";
            this.lpAgencia.whereParametros = null;
            this.lpAgencia.whereVisao = null;
            // 
            // lpBanco
            // 
            this.lpBanco.Abrir_Primeiro = ITGProducao.Controles.NewLookup.AbrirPrimeiro.Visao;
            this.lpBanco.CampoCodigo_Igual_a = ITGProducao.Controles.NewLookup.CampoCodigoIguala.CampoCodigoBD;
            this.lpBanco.CampoCodigoBD = "CODBANCO";
            this.lpBanco.CampoCodigoInterno = null;
            this.lpBanco.CampoDescricaoBD = "NOME";
            this.lpBanco.CarregaDescricaoSemFiltro = false;
            this.lpBanco.Codigo_MaxLenght = 0;
            this.lpBanco.Formulario_Cadastro = "PS.Glb.New.Cadastros.frmCadastroBanco";
            this.lpBanco.Formulario_Filtro = null;
            this.lpBanco.Formulario_Visao = "PS.Glb.New.Visao.frmVisaoBanco";
            newlookup_WhereVisao2.NomeColuna = "CODBANCO";
            newlookup_WhereVisao2.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao2.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.CampoCodigo_NewLookup;
            newlookup_WhereVisao2.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao2.ValorFixo = null;
            newlookup_WhereVisao2.Variavel_Interna = true;
            this.lpBanco.Grid_WhereVisao.Add(newlookup_WhereVisao2);
            this.lpBanco.Location = new System.Drawing.Point(6, 52);
            this.lpBanco.MensagemCodigoVazio = null;
            this.lpBanco.mensagemErrorProvider = null;
            this.lpBanco.Name = "lpBanco";
            this.lpBanco.Projeto_Formularios = "PS.Glb";
            this.lpBanco.Size = new System.Drawing.Size(481, 46);
            this.lpBanco.TabelaBD = "GBANCO";
            this.lpBanco.TabIndex = 24;
            this.lpBanco.Titulo = "Banco";
            this.lpBanco.ValorCodigoInterno = "";
            this.lpBanco.whereParametros = null;
            this.lpBanco.whereVisao = null;
            // 
            // tbDigito
            // 
            this.tbDigito.Location = new System.Drawing.Point(154, 26);
            this.tbDigito.Name = "tbDigito";
            this.tbDigito.Size = new System.Drawing.Size(83, 20);
            this.tbDigito.TabIndex = 23;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(154, 6);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(53, 13);
            this.labelControl3.TabIndex = 22;
            this.labelControl3.Text = "DIGCONTA";
            // 
            // tbCodConta
            // 
            this.tbCodConta.Location = new System.Drawing.Point(8, 25);
            this.tbCodConta.Name = "tbCodConta";
            this.tbCodConta.Size = new System.Drawing.Size(140, 20);
            this.tbCodConta.TabIndex = 21;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(57, 13);
            this.labelControl1.TabIndex = 20;
            this.labelControl1.Text = "NUMCONTA";
            // 
            // btnCancelarAtual
            // 
            this.btnCancelarAtual.Location = new System.Drawing.Point(425, 3);
            this.btnCancelarAtual.Name = "btnCancelarAtual";
            this.btnCancelarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnCancelarAtual.TabIndex = 44;
            this.btnCancelarAtual.Text = "Cancelar";
            this.btnCancelarAtual.Click += new System.EventHandler(this.btnCancelarAtual_Click);
            // 
            // btnOKAtual
            // 
            this.btnOKAtual.Location = new System.Drawing.Point(335, 3);
            this.btnOKAtual.Name = "btnOKAtual";
            this.btnOKAtual.Size = new System.Drawing.Size(75, 23);
            this.btnOKAtual.TabIndex = 43;
            this.btnOKAtual.Text = "OK";
            this.btnOKAtual.Click += new System.EventHandler(this.btnOKAtual_Click);
            // 
            // btnSalvarAtual
            // 
            this.btnSalvarAtual.Location = new System.Drawing.Point(244, 3);
            this.btnSalvarAtual.Name = "btnSalvarAtual";
            this.btnSalvarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnSalvarAtual.TabIndex = 42;
            this.btnSalvarAtual.Text = "Salvar";
            this.btnSalvarAtual.Click += new System.EventHandler(this.btnSalvarAtual_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmCadastroContaCorrente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 228);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCadastroContaCorrente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Conta Corrente";
            this.Load += new System.EventHandler(this.frmCadastroContaCorrente_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDigito.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbCodConta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton btnCancelarAtual;
        private DevExpress.XtraEditors.SimpleButton btnOKAtual;
        private DevExpress.XtraEditors.SimpleButton btnSalvarAtual;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.TextEdit tbDigito;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit tbCodConta;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private ITGProducao.Controles.NewLookup lpBanco;
        private ITGProducao.Controles.NewLookup lpAgencia;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}