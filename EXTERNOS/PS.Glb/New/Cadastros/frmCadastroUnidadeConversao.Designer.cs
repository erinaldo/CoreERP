namespace PS.Glb.New.Cadastros
{
    partial class frmCadastroUnidadeConversao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastroUnidadeConversao));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbDescricao = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tbFatorConversao = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lpUnidadeMedida = new ITGProducao.Controles.NewLookup();
            this.lpUnidadeConversao = new ITGProducao.Controles.NewLookup();
            this.btnCancelarAtual = new DevExpress.XtraEditors.SimpleButton();
            this.btnOKAtual = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalvarAtual = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFatorConversao.Properties)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(606, 325);
            this.splitContainer1.SplitterDistance = 283;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(606, 283);
            this.tabControl1.TabIndex = 20;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbDescricao);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.tbFatorConversao);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.lpUnidadeMedida);
            this.tabPage1.Controls.Add(this.lpUnidadeConversao);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(598, 257);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbDescricao
            // 
            this.tbDescricao.Location = new System.Drawing.Point(12, 177);
            this.tbDescricao.Name = "tbDescricao";
            this.tbDescricao.Size = new System.Drawing.Size(578, 74);
            this.tbDescricao.TabIndex = 19;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 157);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(59, 13);
            this.labelControl1.TabIndex = 18;
            this.labelControl1.Text = "DESCRICAO";
            // 
            // tbFatorConversao
            // 
            this.tbFatorConversao.Location = new System.Drawing.Point(12, 129);
            this.tbFatorConversao.Name = "tbFatorConversao";
            this.tbFatorConversao.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbFatorConversao.Size = new System.Drawing.Size(99, 20);
            this.tbFatorConversao.TabIndex = 16;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 110);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(96, 13);
            this.labelControl3.TabIndex = 15;
            this.labelControl3.Text = "FATORCONVERSAO";
            // 
            // lpUnidadeMedida
            // 
            this.lpUnidadeMedida.Abrir_Primeiro = ITGProducao.Controles.NewLookup.AbrirPrimeiro.Visao;
            this.lpUnidadeMedida.CampoCodigo_Igual_a = ITGProducao.Controles.NewLookup.CampoCodigoIguala.CampoCodigoBD;
            this.lpUnidadeMedida.CampoCodigoBD = "CODUNID";
            this.lpUnidadeMedida.CampoCodigoInterno = null;
            this.lpUnidadeMedida.CampoDescricaoBD = "NOME";
            this.lpUnidadeMedida.CarregaDescricaoSemFiltro = true;
            this.lpUnidadeMedida.Codigo_MaxLenght = 10;
            this.lpUnidadeMedida.Formulario_Cadastro = "";
            this.lpUnidadeMedida.Formulario_Filtro = null;
            this.lpUnidadeMedida.Formulario_Visao = "PS.Glb.New.Visao.frmVisaoUnidadeBase";
            newlookup_WhereVisao1.NomeColuna = "CODEMPRESA";
            newlookup_WhereVisao1.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao1.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.AppLib_Context_Empresa;
            newlookup_WhereVisao1.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao1.ValorFixo = null;
            newlookup_WhereVisao1.Variavel_Interna = true;
            newlookup_WhereVisao2.NomeColuna = "CODUNID";
            newlookup_WhereVisao2.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao2.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.CampoCodigo_NewLookup;
            newlookup_WhereVisao2.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao2.ValorFixo = null;
            newlookup_WhereVisao2.Variavel_Interna = true;
            this.lpUnidadeMedida.Grid_WhereVisao.Add(newlookup_WhereVisao1);
            this.lpUnidadeMedida.Grid_WhereVisao.Add(newlookup_WhereVisao2);
            this.lpUnidadeMedida.Location = new System.Drawing.Point(6, 6);
            this.lpUnidadeMedida.MensagemCodigoVazio = null;
            this.lpUnidadeMedida.mensagemErrorProvider = null;
            this.lpUnidadeMedida.Name = "lpUnidadeMedida";
            this.lpUnidadeMedida.Projeto_Formularios = "PS.Glb";
            this.lpUnidadeMedida.Size = new System.Drawing.Size(584, 46);
            this.lpUnidadeMedida.TabelaBD = "VUNID";
            this.lpUnidadeMedida.TabIndex = 14;
            this.lpUnidadeMedida.Titulo = "Unidade de Medida";
            this.lpUnidadeMedida.ValorCodigoInterno = null;
            this.lpUnidadeMedida.whereParametros = null;
            this.lpUnidadeMedida.whereVisao = null;
            // 
            // lpUnidadeConversao
            // 
            this.lpUnidadeConversao.Abrir_Primeiro = ITGProducao.Controles.NewLookup.AbrirPrimeiro.Visao;
            this.lpUnidadeConversao.CampoCodigo_Igual_a = ITGProducao.Controles.NewLookup.CampoCodigoIguala.CampoCodigoBD;
            this.lpUnidadeConversao.CampoCodigoBD = "CODUNID";
            this.lpUnidadeConversao.CampoCodigoInterno = null;
            this.lpUnidadeConversao.CampoDescricaoBD = "NOME";
            this.lpUnidadeConversao.CarregaDescricaoSemFiltro = true;
            this.lpUnidadeConversao.Codigo_MaxLenght = 10;
            this.lpUnidadeConversao.Formulario_Cadastro = "";
            this.lpUnidadeConversao.Formulario_Filtro = null;
            this.lpUnidadeConversao.Formulario_Visao = "PS.Glb.New.Visao.frmVisaoUnidadeBase";
            newlookup_WhereVisao3.NomeColuna = "CODEMPRESA";
            newlookup_WhereVisao3.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao3.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.AppLib_Context_Empresa;
            newlookup_WhereVisao3.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao3.ValorFixo = null;
            newlookup_WhereVisao3.Variavel_Interna = true;
            newlookup_WhereVisao4.NomeColuna = "CODUNID";
            newlookup_WhereVisao4.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao4.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.CampoCodigo_NewLookup;
            newlookup_WhereVisao4.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao4.ValorFixo = null;
            newlookup_WhereVisao4.Variavel_Interna = true;
            this.lpUnidadeConversao.Grid_WhereVisao.Add(newlookup_WhereVisao3);
            this.lpUnidadeConversao.Grid_WhereVisao.Add(newlookup_WhereVisao4);
            this.lpUnidadeConversao.Location = new System.Drawing.Point(8, 58);
            this.lpUnidadeConversao.MensagemCodigoVazio = null;
            this.lpUnidadeConversao.mensagemErrorProvider = null;
            this.lpUnidadeConversao.Name = "lpUnidadeConversao";
            this.lpUnidadeConversao.Projeto_Formularios = "PS.Glb";
            this.lpUnidadeConversao.Size = new System.Drawing.Size(582, 46);
            this.lpUnidadeConversao.TabelaBD = "VUNID";
            this.lpUnidadeConversao.TabIndex = 13;
            this.lpUnidadeConversao.Titulo = "Unidade de Conversão";
            this.lpUnidadeConversao.ValorCodigoInterno = null;
            this.lpUnidadeConversao.whereParametros = null;
            this.lpUnidadeConversao.whereVisao = null;
            // 
            // btnCancelarAtual
            // 
            this.btnCancelarAtual.Location = new System.Drawing.Point(527, 3);
            this.btnCancelarAtual.Name = "btnCancelarAtual";
            this.btnCancelarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnCancelarAtual.TabIndex = 38;
            this.btnCancelarAtual.Text = "Cancelar";
            this.btnCancelarAtual.Click += new System.EventHandler(this.btnCancelarAtual_Click);
            // 
            // btnOKAtual
            // 
            this.btnOKAtual.Location = new System.Drawing.Point(446, 3);
            this.btnOKAtual.Name = "btnOKAtual";
            this.btnOKAtual.Size = new System.Drawing.Size(75, 23);
            this.btnOKAtual.TabIndex = 37;
            this.btnOKAtual.Text = "OK";
            this.btnOKAtual.Click += new System.EventHandler(this.btnOKAtual_Click);
            // 
            // btnSalvarAtual
            // 
            this.btnSalvarAtual.Location = new System.Drawing.Point(365, 3);
            this.btnSalvarAtual.Name = "btnSalvarAtual";
            this.btnSalvarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnSalvarAtual.TabIndex = 36;
            this.btnSalvarAtual.Text = "Salvar";
            this.btnSalvarAtual.Click += new System.EventHandler(this.btnSalvarAtual_Click);
            // 
            // frmCadastroUnidadeConversao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 325);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCadastroUnidadeConversao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Unidade de Conversão";
            this.Load += new System.EventHandler(this.frmCadastroUnidadeConversao_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbFatorConversao.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton btnCancelarAtual;
        private DevExpress.XtraEditors.SimpleButton btnOKAtual;
        private DevExpress.XtraEditors.SimpleButton btnSalvarAtual;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ITGProducao.Controles.NewLookup lpUnidadeMedida;
        private ITGProducao.Controles.NewLookup lpUnidadeConversao;
        private DevExpress.XtraEditors.TextEdit tbFatorConversao;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.MemoEdit tbDescricao;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}