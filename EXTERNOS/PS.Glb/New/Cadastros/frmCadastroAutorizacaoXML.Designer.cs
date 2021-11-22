namespace PS.Glb.New.Cadastros
{
    partial class frmCadastroAutorizacaoXML
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastroAutorizacaoXML));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.cmbFisicoJuridico = new System.Windows.Forms.ComboBox();
            this.tbDescricao = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.tbCNPJCPF = new DevExpress.XtraEditors.TextEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.lpFilial = new ITGProducao.Controles.NewLookup();
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
            ((System.ComponentModel.ISupportInitialize)(this.tbCNPJCPF.Properties)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(602, 173);
            this.splitContainer1.SplitterDistance = 132;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(602, 132);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelControl7);
            this.tabPage1.Controls.Add(this.cmbFisicoJuridico);
            this.tabPage1.Controls.Add(this.tbDescricao);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.tbCNPJCPF);
            this.tabPage1.Controls.Add(this.labelControl11);
            this.tabPage1.Controls.Add(this.lpFilial);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(594, 106);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(293, 55);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(109, 13);
            this.labelControl7.TabIndex = 42;
            this.labelControl7.Text = "Pessoa Física / Jurídica";
            // 
            // cmbFisicoJuridico
            // 
            this.cmbFisicoJuridico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFisicoJuridico.FormattingEnabled = true;
            this.cmbFisicoJuridico.Items.AddRange(new object[] {
            "Fisico",
            "Jurídico"});
            this.cmbFisicoJuridico.Location = new System.Drawing.Point(293, 74);
            this.cmbFisicoJuridico.Name = "cmbFisicoJuridico";
            this.cmbFisicoJuridico.Size = new System.Drawing.Size(136, 21);
            this.cmbFisicoJuridico.TabIndex = 41;
            this.cmbFisicoJuridico.Tag = "FISICOJURIDICO";
            this.cmbFisicoJuridico.SelectedIndexChanged += new System.EventHandler(this.cmbFisicoJuridico_SelectedIndexChanged);
            // 
            // tbDescricao
            // 
            this.tbDescricao.Location = new System.Drawing.Point(13, 75);
            this.tbDescricao.Name = "tbDescricao";
            this.tbDescricao.Size = new System.Drawing.Size(274, 20);
            this.tbDescricao.TabIndex = 40;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(13, 56);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(59, 13);
            this.labelControl3.TabIndex = 39;
            this.labelControl3.Text = "DESCRICAO";
            // 
            // tbCNPJCPF
            // 
            this.tbCNPJCPF.Location = new System.Drawing.Point(435, 75);
            this.tbCNPJCPF.Name = "tbCNPJCPF";
            this.tbCNPJCPF.Size = new System.Drawing.Size(151, 20);
            this.tbCNPJCPF.TabIndex = 38;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(435, 56);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(44, 13);
            this.labelControl11.TabIndex = 37;
            this.labelControl11.Text = "CNPJCPF";
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
            this.lpFilial.Location = new System.Drawing.Point(8, 6);
            this.lpFilial.MensagemCodigoVazio = null;
            this.lpFilial.mensagemErrorProvider = null;
            this.lpFilial.Name = "lpFilial";
            this.lpFilial.Projeto_Formularios = "ITGProducao";
            this.lpFilial.Size = new System.Drawing.Size(578, 46);
            this.lpFilial.TabelaBD = "GFILIAL";
            this.lpFilial.TabIndex = 12;
            this.lpFilial.Titulo = "Filial";
            this.lpFilial.ValorCodigoInterno = "";
            this.lpFilial.whereParametros = null;
            this.lpFilial.whereVisao = null;
            // 
            // btnCancelarAtual
            // 
            this.btnCancelarAtual.Location = new System.Drawing.Point(515, 3);
            this.btnCancelarAtual.Name = "btnCancelarAtual";
            this.btnCancelarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnCancelarAtual.TabIndex = 44;
            this.btnCancelarAtual.Text = "Cancelar";
            this.btnCancelarAtual.Click += new System.EventHandler(this.btnCancelarAtual_Click);
            // 
            // btnOKAtual
            // 
            this.btnOKAtual.Location = new System.Drawing.Point(425, 3);
            this.btnOKAtual.Name = "btnOKAtual";
            this.btnOKAtual.Size = new System.Drawing.Size(75, 23);
            this.btnOKAtual.TabIndex = 43;
            this.btnOKAtual.Text = "OK";
            this.btnOKAtual.Click += new System.EventHandler(this.btnOKAtual_Click);
            // 
            // btnSalvarAtual
            // 
            this.btnSalvarAtual.Location = new System.Drawing.Point(334, 3);
            this.btnSalvarAtual.Name = "btnSalvarAtual";
            this.btnSalvarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnSalvarAtual.TabIndex = 42;
            this.btnSalvarAtual.Text = "Salvar";
            this.btnSalvarAtual.Click += new System.EventHandler(this.btnSalvarAtual_Click);
            // 
            // frmCadastroAutorizacaoXML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 173);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCadastroAutorizacaoXML";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Autorização do XML";
            this.Load += new System.EventHandler(this.frmCadastroAutorizacaoXML_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbCNPJCPF.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ITGProducao.Controles.NewLookup lpFilial;
        private DevExpress.XtraEditors.TextEdit tbCNPJCPF;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.TextEdit tbDescricao;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnCancelarAtual;
        private DevExpress.XtraEditors.SimpleButton btnOKAtual;
        private DevExpress.XtraEditors.SimpleButton btnSalvarAtual;
        private System.Windows.Forms.ComboBox cmbFisicoJuridico;
        private DevExpress.XtraEditors.LabelControl labelControl7;
    }
}