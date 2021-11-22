namespace PS.Glb.New.Filtro
{
    partial class frmFiltroExtrato
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFiltroExtrato));
            ITGProducao.Controles.Newlookup_WhereVisao newlookup_WhereVisao1 = new ITGProducao.Controles.Newlookup_WhereVisao();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbExtrato = new DevExpress.XtraEditors.TextEdit();
            this.dteFinal = new DevExpress.XtraEditors.DateEdit();
            this.lbDataFinal = new DevExpress.XtraEditors.LabelControl();
            this.dteInicial = new DevExpress.XtraEditors.DateEdit();
            this.lbData = new DevExpress.XtraEditors.LabelControl();
            this.lblValor = new DevExpress.XtraEditors.LabelControl();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbTodos = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbDataCompensacao = new System.Windows.Forms.RadioButton();
            this.rbCompensado = new System.Windows.Forms.RadioButton();
            this.rbPeriodoCompensacao = new System.Windows.Forms.RadioButton();
            this.rbNaoCompensado = new System.Windows.Forms.RadioButton();
            this.rbExtrato = new System.Windows.Forms.RadioButton();
            this.rbData = new System.Windows.Forms.RadioButton();
            this.rbContaCaixa = new System.Windows.Forms.RadioButton();
            this.btnFechar = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalvar = new DevExpress.XtraEditors.SimpleButton();
            this.lpContaCaixa = new ITGProducao.Controles.NewLookup();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbExtrato.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFinal.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFinal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteInicial.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteInicial.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.splitContainer1.Panel2.Controls.Add(this.btnFechar);
            this.splitContainer1.Panel2.Controls.Add(this.btnSalvar);
            this.splitContainer1.Size = new System.Drawing.Size(316, 382);
            this.splitContainer1.SplitterDistance = 340;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(316, 340);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(308, 314);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lpContaCaixa);
            this.groupBox2.Controls.Add(this.tbExtrato);
            this.groupBox2.Controls.Add(this.dteFinal);
            this.groupBox2.Controls.Add(this.lbDataFinal);
            this.groupBox2.Controls.Add(this.dteInicial);
            this.groupBox2.Controls.Add(this.lbData);
            this.groupBox2.Controls.Add(this.lblValor);
            this.groupBox2.Controls.Add(this.cmbStatus);
            this.groupBox2.Location = new System.Drawing.Point(8, 154);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(290, 154);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Valores";
            // 
            // tbExtrato
            // 
            this.tbExtrato.Location = new System.Drawing.Point(149, 86);
            this.tbExtrato.Name = "tbExtrato";
            this.tbExtrato.Size = new System.Drawing.Size(135, 20);
            this.tbExtrato.TabIndex = 18;
            this.tbExtrato.Visible = false;
            // 
            // dteFinal
            // 
            this.dteFinal.EditValue = null;
            this.dteFinal.Location = new System.Drawing.Point(149, 128);
            this.dteFinal.Name = "dteFinal";
            this.dteFinal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFinal.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFinal.Size = new System.Drawing.Size(135, 20);
            this.dteFinal.TabIndex = 12;
            this.dteFinal.Visible = false;
            // 
            // lbDataFinal
            // 
            this.lbDataFinal.Location = new System.Drawing.Point(149, 109);
            this.lbDataFinal.Name = "lbDataFinal";
            this.lbDataFinal.Size = new System.Drawing.Size(48, 13);
            this.lbDataFinal.TabIndex = 13;
            this.lbDataFinal.Text = "Data Final";
            this.lbDataFinal.Visible = false;
            // 
            // dteInicial
            // 
            this.dteInicial.EditValue = null;
            this.dteInicial.Location = new System.Drawing.Point(6, 128);
            this.dteInicial.Name = "dteInicial";
            this.dteInicial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteInicial.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteInicial.Size = new System.Drawing.Size(135, 20);
            this.dteInicial.TabIndex = 10;
            this.dteInicial.Visible = false;
            // 
            // lbData
            // 
            this.lbData.Location = new System.Drawing.Point(6, 112);
            this.lbData.Name = "lbData";
            this.lbData.Size = new System.Drawing.Size(53, 13);
            this.lbData.TabIndex = 11;
            this.lbData.Text = "Data Inicial";
            this.lbData.Visible = false;
            // 
            // lblValor
            // 
            this.lblValor.Location = new System.Drawing.Point(6, 66);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(24, 13);
            this.lblValor.TabIndex = 1;
            this.lblValor.Text = "Valor";
            this.lblValor.Visible = false;
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(6, 85);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(135, 21);
            this.cmbStatus.TabIndex = 0;
            this.cmbStatus.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbTodos);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.rbExtrato);
            this.groupBox1.Controls.Add(this.rbData);
            this.groupBox1.Controls.Add(this.rbContaCaixa);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 142);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtros";
            // 
            // rbTodos
            // 
            this.rbTodos.AutoSize = true;
            this.rbTodos.Location = new System.Drawing.Point(168, 115);
            this.rbTodos.Name = "rbTodos";
            this.rbTodos.Size = new System.Drawing.Size(55, 17);
            this.rbTodos.TabIndex = 10;
            this.rbTodos.TabStop = true;
            this.rbTodos.Text = "Todos";
            this.rbTodos.UseVisualStyleBackColor = true;
            this.rbTodos.CheckedChanged += new System.EventHandler(this.rbTodos_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbDataCompensacao);
            this.groupBox3.Controls.Add(this.rbCompensado);
            this.groupBox3.Controls.Add(this.rbPeriodoCompensacao);
            this.groupBox3.Controls.Add(this.rbNaoCompensado);
            this.groupBox3.Location = new System.Drawing.Point(6, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(278, 67);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Compensação";
            // 
            // rbDataCompensacao
            // 
            this.rbDataCompensacao.AutoSize = true;
            this.rbDataCompensacao.Location = new System.Drawing.Point(6, 42);
            this.rbDataCompensacao.Name = "rbDataCompensacao";
            this.rbDataCompensacao.Size = new System.Drawing.Size(119, 17);
            this.rbDataCompensacao.TabIndex = 3;
            this.rbDataCompensacao.TabStop = true;
            this.rbDataCompensacao.Text = "Data Compensação";
            this.rbDataCompensacao.UseVisualStyleBackColor = true;
            this.rbDataCompensacao.CheckedChanged += new System.EventHandler(this.rbDataCompensacao_CheckedChanged);
            // 
            // rbCompensado
            // 
            this.rbCompensado.AutoSize = true;
            this.rbCompensado.Location = new System.Drawing.Point(6, 19);
            this.rbCompensado.Name = "rbCompensado";
            this.rbCompensado.Size = new System.Drawing.Size(87, 17);
            this.rbCompensado.TabIndex = 2;
            this.rbCompensado.TabStop = true;
            this.rbCompensado.Text = "Compensado";
            this.rbCompensado.UseVisualStyleBackColor = true;
            this.rbCompensado.CheckedChanged += new System.EventHandler(this.rbCompensado_CheckedChanged);
            // 
            // rbPeriodoCompensacao
            // 
            this.rbPeriodoCompensacao.AutoSize = true;
            this.rbPeriodoCompensacao.Location = new System.Drawing.Point(162, 42);
            this.rbPeriodoCompensacao.Name = "rbPeriodoCompensacao";
            this.rbPeriodoCompensacao.Size = new System.Drawing.Size(66, 17);
            this.rbPeriodoCompensacao.TabIndex = 7;
            this.rbPeriodoCompensacao.TabStop = true;
            this.rbPeriodoCompensacao.Text = "Período ";
            this.rbPeriodoCompensacao.UseVisualStyleBackColor = true;
            this.rbPeriodoCompensacao.CheckedChanged += new System.EventHandler(this.rbPeriodoCompensacao_CheckedChanged);
            // 
            // rbNaoCompensado
            // 
            this.rbNaoCompensado.AutoSize = true;
            this.rbNaoCompensado.Location = new System.Drawing.Point(162, 19);
            this.rbNaoCompensado.Name = "rbNaoCompensado";
            this.rbNaoCompensado.Size = new System.Drawing.Size(110, 17);
            this.rbNaoCompensado.TabIndex = 1;
            this.rbNaoCompensado.TabStop = true;
            this.rbNaoCompensado.Text = "Não Compensado";
            this.rbNaoCompensado.UseVisualStyleBackColor = true;
            this.rbNaoCompensado.CheckedChanged += new System.EventHandler(this.rbNaoCompensado_CheckedChanged);
            // 
            // rbExtrato
            // 
            this.rbExtrato.AutoSize = true;
            this.rbExtrato.Location = new System.Drawing.Point(12, 115);
            this.rbExtrato.Name = "rbExtrato";
            this.rbExtrato.Size = new System.Drawing.Size(77, 17);
            this.rbExtrato.TabIndex = 6;
            this.rbExtrato.TabStop = true;
            this.rbExtrato.Text = "Referência";
            this.rbExtrato.UseVisualStyleBackColor = true;
            this.rbExtrato.CheckedChanged += new System.EventHandler(this.rbExtrato_CheckedChanged);
            // 
            // rbData
            // 
            this.rbData.AutoSize = true;
            this.rbData.Location = new System.Drawing.Point(12, 92);
            this.rbData.Name = "rbData";
            this.rbData.Size = new System.Drawing.Size(48, 17);
            this.rbData.TabIndex = 8;
            this.rbData.TabStop = true;
            this.rbData.Text = "Data";
            this.rbData.UseVisualStyleBackColor = true;
            this.rbData.CheckedChanged += new System.EventHandler(this.rbData_CheckedChanged);
            // 
            // rbContaCaixa
            // 
            this.rbContaCaixa.AutoSize = true;
            this.rbContaCaixa.Location = new System.Drawing.Point(168, 92);
            this.rbContaCaixa.Name = "rbContaCaixa";
            this.rbContaCaixa.Size = new System.Drawing.Size(82, 17);
            this.rbContaCaixa.TabIndex = 1;
            this.rbContaCaixa.TabStop = true;
            this.rbContaCaixa.Text = "Conta Caixa";
            this.rbContaCaixa.UseVisualStyleBackColor = true;
            this.rbContaCaixa.CheckedChanged += new System.EventHandler(this.rbContaCaixa_CheckedChanged);
            // 
            // btnFechar
            // 
            this.btnFechar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnFechar.ImageOptions.Image")));
            this.btnFechar.Location = new System.Drawing.Point(229, 3);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(75, 29);
            this.btnFechar.TabIndex = 19;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSalvar.ImageOptions.Image")));
            this.btnSalvar.Location = new System.Drawing.Point(142, 3);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(81, 29);
            this.btnSalvar.TabIndex = 18;
            this.btnSalvar.Text = "Executar";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lpContaCaixa
            // 
            this.lpContaCaixa.Abrir_Primeiro = ITGProducao.Controles.NewLookup.AbrirPrimeiro.Visao;
            this.lpContaCaixa.CampoCodigo_Igual_a = ITGProducao.Controles.NewLookup.CampoCodigoIguala.CampoCodigoBD;
            this.lpContaCaixa.CampoCodigoBD = "CODCONTA";
            this.lpContaCaixa.CampoCodigoInterno = null;
            this.lpContaCaixa.CampoDescricaoBD = "DESCRICAO";
            this.lpContaCaixa.CarregaDescricaoSemFiltro = false;
            this.lpContaCaixa.Codigo_MaxLenght = 0;
            this.lpContaCaixa.Formulario_Cadastro = "PS.Glb.New.Cadastros.frmCadastroContaCaixa";
            this.lpContaCaixa.Formulario_Filtro = null;
            this.lpContaCaixa.Formulario_Visao = "PS.Glb.New.Visao.frmVisaoContaCaixa";
            newlookup_WhereVisao1.NomeColuna = "CODCONTA";
            newlookup_WhereVisao1.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao1.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.CampoCodigo_NewLookup;
            newlookup_WhereVisao1.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao1.ValorFixo = null;
            newlookup_WhereVisao1.Variavel_Interna = true;
            this.lpContaCaixa.Grid_WhereVisao.Add(newlookup_WhereVisao1);
            this.lpContaCaixa.Location = new System.Drawing.Point(0, 19);
            this.lpContaCaixa.MensagemCodigoVazio = null;
            this.lpContaCaixa.mensagemErrorProvider = null;
            this.lpContaCaixa.Name = "lpContaCaixa";
            this.lpContaCaixa.Projeto_Formularios = "PS.Glb";
            this.lpContaCaixa.Size = new System.Drawing.Size(284, 46);
            this.lpContaCaixa.TabelaBD = "FCONTA";
            this.lpContaCaixa.TabIndex = 46;
            this.lpContaCaixa.Titulo = "Conta Caixa";
            this.lpContaCaixa.ValorCodigoInterno = "";
            this.lpContaCaixa.Visible = false;
            this.lpContaCaixa.whereParametros = null;
            this.lpContaCaixa.whereVisao = null;
            // 
            // frmFiltroExtrato
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 382);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmFiltroExtrato";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtro de Extrato";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbExtrato.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFinal.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFinal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteInicial.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteInicial.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton btnFechar;
        private DevExpress.XtraEditors.SimpleButton btnSalvar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.DateEdit dteFinal;
        private DevExpress.XtraEditors.LabelControl lbDataFinal;
        private DevExpress.XtraEditors.DateEdit dteInicial;
        private DevExpress.XtraEditors.LabelControl lbData;
        private DevExpress.XtraEditors.LabelControl lblValor;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbData;
        private System.Windows.Forms.RadioButton rbPeriodoCompensacao;
        private System.Windows.Forms.RadioButton rbExtrato;
        private System.Windows.Forms.RadioButton rbContaCaixa;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbCompensado;
        private System.Windows.Forms.RadioButton rbNaoCompensado;
        private System.Windows.Forms.RadioButton rbDataCompensacao;
        private System.Windows.Forms.RadioButton rbTodos;
        private DevExpress.XtraEditors.TextEdit tbExtrato;
        private ITGProducao.Controles.NewLookup lpContaCaixa;
    }
}