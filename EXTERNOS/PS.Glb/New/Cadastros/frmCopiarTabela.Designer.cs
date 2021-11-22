namespace PS.Glb.New.Cadastros
{
    partial class frmCopiarTabela
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCopiarTabela));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnCancelarAtual = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalvarAtual = new DevExpress.XtraEditors.SimpleButton();
            this.lpCliente = new ITGProducao.Controles.NewLookup();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(471, 190);
            this.splitContainer1.SplitterDistance = 147;
            this.splitContainer1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(471, 147);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lpCliente);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(463, 121);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnCancelarAtual
            // 
            this.btnCancelarAtual.Location = new System.Drawing.Point(381, 4);
            this.btnCancelarAtual.Name = "btnCancelarAtual";
            this.btnCancelarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnCancelarAtual.TabIndex = 45;
            this.btnCancelarAtual.Text = "Cancelar";
            // 
            // btnSalvarAtual
            // 
            this.btnSalvarAtual.Location = new System.Drawing.Point(300, 4);
            this.btnSalvarAtual.Name = "btnSalvarAtual";
            this.btnSalvarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnSalvarAtual.TabIndex = 43;
            this.btnSalvarAtual.Text = "Inserir";
            this.btnSalvarAtual.Click += new System.EventHandler(this.btnSalvarAtual_Click);
            // 
            // lpCliente
            // 
            this.lpCliente.Abrir_Primeiro = ITGProducao.Controles.NewLookup.AbrirPrimeiro.Visao;
            this.lpCliente.CampoCodigo_Igual_a = ITGProducao.Controles.NewLookup.CampoCodigoIguala.CampoCodigoBD;
            this.lpCliente.CampoCodigoBD = "CODCLIFOR";
            this.lpCliente.CampoCodigoInterno = null;
            this.lpCliente.CampoDescricaoBD = "NOME";
            this.lpCliente.CarregaDescricaoSemFiltro = true;
            this.lpCliente.Codigo_MaxLenght = 15;
            this.lpCliente.Formulario_Cadastro = "PS.Glb.New.Cadastros.frmCadastroCliente";
            this.lpCliente.Formulario_Filtro = "PS.Glb.New.Filtro.frmClienteFiltro";
            this.lpCliente.Formulario_Visao = "PS.Glb.New.Visao.frmVisaoCliente";
            newlookup_WhereVisao1.NomeColuna = "CODEMPRESA";
            newlookup_WhereVisao1.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao1.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.AppLib_Context_Empresa;
            newlookup_WhereVisao1.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao1.ValorFixo = null;
            newlookup_WhereVisao1.Variavel_Interna = true;
            newlookup_WhereVisao2.NomeColuna = "ATIVO";
            newlookup_WhereVisao2.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao2.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.Null;
            newlookup_WhereVisao2.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao2.ValorFixo = "1";
            newlookup_WhereVisao2.Variavel_Interna = false;
            newlookup_WhereVisao3.NomeColuna = "CODCLIFOR";
            newlookup_WhereVisao3.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao3.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.CampoCodigo_NewLookup;
            newlookup_WhereVisao3.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao3.ValorFixo = null;
            newlookup_WhereVisao3.Variavel_Interna = true;
            newlookup_WhereVisao4.NomeColuna = "CODCLASSIFICACAO";
            newlookup_WhereVisao4.NomeColunaValor_SelectQuery = "CLASSIFICACAOCLIFOR";
            newlookup_WhereVisao4.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.SelectQuery;
            newlookup_WhereVisao4.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.In;
            newlookup_WhereVisao4.ValorFixo = "select \'2\' as \'CLASSIFICACAOCLIFOR\' union all select CLASSIFICACAOCLIFOR from GTI" +
    "POPER";
            newlookup_WhereVisao4.Variavel_Interna = true;
            this.lpCliente.Grid_WhereVisao.Add(newlookup_WhereVisao1);
            this.lpCliente.Grid_WhereVisao.Add(newlookup_WhereVisao2);
            this.lpCliente.Grid_WhereVisao.Add(newlookup_WhereVisao3);
            this.lpCliente.Grid_WhereVisao.Add(newlookup_WhereVisao4);
            this.lpCliente.Location = new System.Drawing.Point(3, 33);
            this.lpCliente.MensagemCodigoVazio = null;
            this.lpCliente.mensagemErrorProvider = null;
            this.lpCliente.Name = "lpCliente";
            this.lpCliente.Projeto_Formularios = "PS.Glb";
            this.lpCliente.Size = new System.Drawing.Size(449, 46);
            this.lpCliente.TabelaBD = "VCLIFOR";
            this.lpCliente.TabIndex = 16;
            this.lpCliente.Tag = "CODCLIFOR";
            this.lpCliente.Titulo = "Código do Cliente";
            this.lpCliente.ValorCodigoInterno = null;
            this.lpCliente.whereParametros = null;
            this.lpCliente.whereVisao = "";
            // 
            // frmCopiarTabela
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 190);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCopiarTabela";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cópia de Tabela";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.SimpleButton btnCancelarAtual;
        private DevExpress.XtraEditors.SimpleButton btnSalvarAtual;
        private ITGProducao.Controles.NewLookup lpCliente;
    }
}