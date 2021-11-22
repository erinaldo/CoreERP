namespace PS.Glb.New.Cadastros
{
    partial class frmCadastroContaCaixa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastroContaCaixa));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbNumeroCheque = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.chkRelConsolidado = new DevExpress.XtraEditors.CheckEdit();
            this.tbSaldoData = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dteDataBase = new DevExpress.XtraEditors.DateEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.chkAtivo = new DevExpress.XtraEditors.CheckEdit();
            this.tbDescricao = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.tbCodConta = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnCancelarAtual = new DevExpress.XtraEditors.SimpleButton();
            this.btnOKAtual = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalvarAtual = new DevExpress.XtraEditors.SimpleButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lpFilial = new ITGProducao.Controles.NewLookup();
            this.lpContaCorrente = new ITGProducao.Controles.NewLookup();
            this.lpAgencia = new ITGProducao.Controles.NewLookup();
            this.lpBanco = new ITGProducao.Controles.NewLookup();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbNumeroCheque.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRelConsolidado.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSaldoData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDataBase.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDataBase.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAtivo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbCodConta.Properties)).BeginInit();
            this.tabPage2.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(701, 245);
            this.splitContainer1.SplitterDistance = 208;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(701, 208);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lpFilial);
            this.tabPage1.Controls.Add(this.tbNumeroCheque);
            this.tabPage1.Controls.Add(this.labelControl6);
            this.tabPage1.Controls.Add(this.chkRelConsolidado);
            this.tabPage1.Controls.Add(this.tbSaldoData);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.dteDataBase);
            this.tabPage1.Controls.Add(this.labelControl5);
            this.tabPage1.Controls.Add(this.chkAtivo);
            this.tabPage1.Controls.Add(this.tbDescricao);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.tbCodConta);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(693, 182);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbNumeroCheque
            // 
            this.tbNumeroCheque.Location = new System.Drawing.Point(8, 115);
            this.tbNumeroCheque.Name = "tbNumeroCheque";
            this.tbNumeroCheque.Size = new System.Drawing.Size(140, 20);
            this.tbNumeroCheque.TabIndex = 33;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(8, 96);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(84, 13);
            this.labelControl6.TabIndex = 32;
            this.labelControl6.Text = "NUMEROCHEQUE";
            // 
            // chkRelConsolidado
            // 
            this.chkRelConsolidado.Location = new System.Drawing.Point(8, 141);
            this.chkRelConsolidado.Name = "chkRelConsolidado";
            this.chkRelConsolidado.Properties.Caption = "RELCONSOLIDADO";
            this.chkRelConsolidado.Size = new System.Drawing.Size(140, 19);
            this.chkRelConsolidado.TabIndex = 29;
            // 
            // tbSaldoData
            // 
            this.tbSaldoData.Location = new System.Drawing.Point(300, 115);
            this.tbSaldoData.Name = "tbSaldoData";
            this.tbSaldoData.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbSaldoData.Size = new System.Drawing.Size(140, 20);
            this.tbSaldoData.TabIndex = 28;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(300, 96);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(85, 13);
            this.labelControl3.TabIndex = 27;
            this.labelControl3.Text = "SALDODATABASE";
            // 
            // dteDataBase
            // 
            this.dteDataBase.EditValue = null;
            this.dteDataBase.Location = new System.Drawing.Point(154, 115);
            this.dteDataBase.Name = "dteDataBase";
            this.dteDataBase.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDataBase.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDataBase.Properties.DisplayFormat.FormatString = "g";
            this.dteDataBase.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteDataBase.Properties.Mask.EditMask = "g";
            this.dteDataBase.Properties.TodayDate = new System.DateTime(2017, 7, 7, 16, 30, 47, 0);
            this.dteDataBase.Size = new System.Drawing.Size(140, 20);
            this.dteDataBase.TabIndex = 26;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(154, 96);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(38, 13);
            this.labelControl5.TabIndex = 25;
            this.labelControl5.Text = "DTBASE";
            // 
            // chkAtivo
            // 
            this.chkAtivo.Location = new System.Drawing.Point(154, 25);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Properties.Caption = "ATIVO";
            this.chkAtivo.Size = new System.Drawing.Size(64, 19);
            this.chkAtivo.TabIndex = 24;
            // 
            // tbDescricao
            // 
            this.tbDescricao.Location = new System.Drawing.Point(8, 70);
            this.tbDescricao.Name = "tbDescricao";
            this.tbDescricao.Size = new System.Drawing.Size(677, 20);
            this.tbDescricao.TabIndex = 23;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 51);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(59, 13);
            this.labelControl2.TabIndex = 22;
            this.labelControl2.Text = "DESCRICAO";
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
            this.labelControl1.Text = "CODCONTA";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lpContaCorrente);
            this.tabPage2.Controls.Add(this.lpAgencia);
            this.tabPage2.Controls.Add(this.lpBanco);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(693, 182);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Integração Bancária";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnCancelarAtual
            // 
            this.btnCancelarAtual.Location = new System.Drawing.Point(614, 3);
            this.btnCancelarAtual.Name = "btnCancelarAtual";
            this.btnCancelarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnCancelarAtual.TabIndex = 47;
            this.btnCancelarAtual.Text = "Cancelar";
            this.btnCancelarAtual.Click += new System.EventHandler(this.btnCancelarAtual_Click);
            // 
            // btnOKAtual
            // 
            this.btnOKAtual.Location = new System.Drawing.Point(524, 3);
            this.btnOKAtual.Name = "btnOKAtual";
            this.btnOKAtual.Size = new System.Drawing.Size(75, 23);
            this.btnOKAtual.TabIndex = 46;
            this.btnOKAtual.Text = "OK";
            this.btnOKAtual.Click += new System.EventHandler(this.btnOKAtual_Click);
            // 
            // btnSalvarAtual
            // 
            this.btnSalvarAtual.Location = new System.Drawing.Point(433, 3);
            this.btnSalvarAtual.Name = "btnSalvarAtual";
            this.btnSalvarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnSalvarAtual.TabIndex = 45;
            this.btnSalvarAtual.Text = "Salvar";
            this.btnSalvarAtual.Click += new System.EventHandler(this.btnSalvarAtual_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
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
            this.lpFilial.Location = new System.Drawing.Point(205, 3);
            this.lpFilial.MensagemCodigoVazio = null;
            this.lpFilial.mensagemErrorProvider = null;
            this.lpFilial.Name = "lpFilial";
            this.lpFilial.Projeto_Formularios = "ITGProducao";
            this.lpFilial.Size = new System.Drawing.Size(480, 46);
            this.lpFilial.TabelaBD = "GFILIAL";
            this.lpFilial.TabIndex = 43;
            this.lpFilial.Titulo = "Filial";
            this.lpFilial.ValorCodigoInterno = "";
            this.lpFilial.whereParametros = null;
            this.lpFilial.whereVisao = null;
            // 
            // lpContaCorrente
            // 
            this.lpContaCorrente.Abrir_Primeiro = ITGProducao.Controles.NewLookup.AbrirPrimeiro.Visao;
            this.lpContaCorrente.CampoCodigo_Igual_a = ITGProducao.Controles.NewLookup.CampoCodigoIguala.CampoCodigoBD;
            this.lpContaCorrente.CampoCodigoBD = "NUMCONTA";
            this.lpContaCorrente.CampoCodigoInterno = null;
            this.lpContaCorrente.CampoDescricaoBD = "DIGCONTA";
            this.lpContaCorrente.CarregaDescricaoSemFiltro = false;
            this.lpContaCorrente.Codigo_MaxLenght = 0;
            this.lpContaCorrente.Formulario_Cadastro = "PS.Glb.New.Cadastros.frmCadastroContaCorrente";
            this.lpContaCorrente.Formulario_Filtro = null;
            this.lpContaCorrente.Formulario_Visao = "PS.Glb.New.Visao.frmVisaoContaCorrente";
            newlookup_WhereVisao2.NomeColuna = "NUMCONTA";
            newlookup_WhereVisao2.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao2.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.CampoCodigo_NewLookup;
            newlookup_WhereVisao2.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao2.ValorFixo = null;
            newlookup_WhereVisao2.Variavel_Interna = true;
            this.lpContaCorrente.Grid_WhereVisao.Add(newlookup_WhereVisao2);
            this.lpContaCorrente.Location = new System.Drawing.Point(8, 110);
            this.lpContaCorrente.MensagemCodigoVazio = null;
            this.lpContaCorrente.mensagemErrorProvider = null;
            this.lpContaCorrente.Name = "lpContaCorrente";
            this.lpContaCorrente.Projeto_Formularios = "PS.Glb";
            this.lpContaCorrente.Size = new System.Drawing.Size(677, 46);
            this.lpContaCorrente.TabelaBD = "FCCORRENTE";
            this.lpContaCorrente.TabIndex = 28;
            this.lpContaCorrente.Titulo = "Conta Corrente";
            this.lpContaCorrente.ValorCodigoInterno = "";
            this.lpContaCorrente.whereParametros = null;
            this.lpContaCorrente.whereVisao = null;
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
            newlookup_WhereVisao3.NomeColuna = "CODAGENCIA";
            newlookup_WhereVisao3.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao3.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.CampoCodigo_NewLookup;
            newlookup_WhereVisao3.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao3.ValorFixo = null;
            newlookup_WhereVisao3.Variavel_Interna = true;
            this.lpAgencia.Grid_WhereVisao.Add(newlookup_WhereVisao3);
            this.lpAgencia.Location = new System.Drawing.Point(8, 58);
            this.lpAgencia.MensagemCodigoVazio = null;
            this.lpAgencia.mensagemErrorProvider = null;
            this.lpAgencia.Name = "lpAgencia";
            this.lpAgencia.Projeto_Formularios = "PS.Glb";
            this.lpAgencia.Size = new System.Drawing.Size(677, 46);
            this.lpAgencia.TabelaBD = "GAGENCIA";
            this.lpAgencia.TabIndex = 27;
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
            newlookup_WhereVisao4.NomeColuna = "CODBANCO";
            newlookup_WhereVisao4.NomeColunaValor_SelectQuery = null;
            newlookup_WhereVisao4.NomeVariavel_Interna = ITGProducao.Controles.Newlookup_Propriedades.VariavelInterna.CampoCodigo_NewLookup;
            newlookup_WhereVisao4.OperadorComparacao = ITGProducao.Controles.Newlookup_Propriedades.Operador.Igual;
            newlookup_WhereVisao4.ValorFixo = null;
            newlookup_WhereVisao4.Variavel_Interna = true;
            this.lpBanco.Grid_WhereVisao.Add(newlookup_WhereVisao4);
            this.lpBanco.Location = new System.Drawing.Point(6, 6);
            this.lpBanco.MensagemCodigoVazio = null;
            this.lpBanco.mensagemErrorProvider = null;
            this.lpBanco.Name = "lpBanco";
            this.lpBanco.Projeto_Formularios = "PS.Glb";
            this.lpBanco.Size = new System.Drawing.Size(679, 46);
            this.lpBanco.TabelaBD = "GBANCO";
            this.lpBanco.TabIndex = 26;
            this.lpBanco.Titulo = "Banco";
            this.lpBanco.ValorCodigoInterno = "";
            this.lpBanco.whereParametros = null;
            this.lpBanco.whereVisao = null;
            // 
            // frmCadastroContaCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 245);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCadastroContaCaixa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Conta Caixa";
            this.Load += new System.EventHandler(this.frmCadastroContaCaixa_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbNumeroCheque.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRelConsolidado.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSaldoData.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDataBase.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDataBase.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAtivo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbCodConta.Properties)).EndInit();
            this.tabPage2.ResumeLayout(false);
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
        private DevExpress.XtraEditors.TextEdit tbCodConta;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private ITGProducao.Controles.NewLookup lpAgencia;
        private ITGProducao.Controles.NewLookup lpBanco;
        private DevExpress.XtraEditors.TextEdit tbDescricao;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.CheckEdit chkAtivo;
        private DevExpress.XtraEditors.DateEdit dteDataBase;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit tbSaldoData;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit chkRelConsolidado;
        private DevExpress.XtraEditors.TextEdit tbNumeroCheque;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private ITGProducao.Controles.NewLookup lpContaCorrente;
        private ITGProducao.Controles.NewLookup lpFilial;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}