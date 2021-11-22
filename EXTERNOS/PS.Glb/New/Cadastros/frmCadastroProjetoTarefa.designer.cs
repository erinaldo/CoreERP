namespace PS.Glb.New.Cadastros
{
    partial class frmCadastroProjetoTarefa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastroProjetoTarefa));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dtePrevisaoEntrega = new DevExpress.XtraEditors.DateEdit();
            this.dteDataConclusao = new DevExpress.XtraEditors.DateEdit();
            this.campoLookupIDCONTATOCC = new AppLib.Windows.CampoLookup();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiroPREVISAOHORAS = new AppLib.Windows.CampoInteiro();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancelarAtual = new DevExpress.XtraEditors.SimpleButton();
            this.btnOKAtual = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalvarAtual = new DevExpress.XtraEditors.SimpleButton();
            this.tbIdTarefaProjeto = new DevExpress.XtraEditors.TextEdit();
            this.tbTarefa = new DevExpress.XtraEditors.TextEdit();
            this.tbIdProejto = new DevExpress.XtraEditors.TextEdit();
            this.tbDescricao = new DevExpress.XtraEditors.MemoEdit();
            this.cbTipoFaturamento = new System.Windows.Forms.ComboBox();
            this.cbInLoco = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtePrevisaoEntrega.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtePrevisaoEntrega.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDataConclusao.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDataConclusao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIdTarefaProjeto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTarefa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIdProejto.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDescricao.Properties)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(740, 363);
            this.splitContainer1.SplitterDistance = 318;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(740, 318);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cbInLoco);
            this.tabPage1.Controls.Add(this.cbTipoFaturamento);
            this.tabPage1.Controls.Add(this.tbDescricao);
            this.tabPage1.Controls.Add(this.tbIdProejto);
            this.tabPage1.Controls.Add(this.tbTarefa);
            this.tabPage1.Controls.Add(this.tbIdTarefaProjeto);
            this.tabPage1.Controls.Add(this.dtePrevisaoEntrega);
            this.tabPage1.Controls.Add(this.dteDataConclusao);
            this.tabPage1.Controls.Add(this.campoLookupIDCONTATOCC);
            this.tabPage1.Controls.Add(this.labelControl6);
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.campoInteiroPREVISAOHORAS);
            this.tabPage1.Controls.Add(this.labelControl12);
            this.tabPage1.Controls.Add(this.labelControl11);
            this.tabPage1.Controls.Add(this.labelControl8);
            this.tabPage1.Controls.Add(this.labelControl7);
            this.tabPage1.Controls.Add(this.labelControl5);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(732, 292);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Cadastro";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dtePrevisaoEntrega
            // 
            this.dtePrevisaoEntrega.EditValue = null;
            this.dtePrevisaoEntrega.Location = new System.Drawing.Point(114, 119);
            this.dtePrevisaoEntrega.Name = "dtePrevisaoEntrega";
            this.dtePrevisaoEntrega.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtePrevisaoEntrega.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtePrevisaoEntrega.Size = new System.Drawing.Size(100, 20);
            this.dtePrevisaoEntrega.TabIndex = 73;
            // 
            // dteDataConclusao
            // 
            this.dteDataConclusao.EditValue = null;
            this.dteDataConclusao.Location = new System.Drawing.Point(220, 119);
            this.dteDataConclusao.Name = "dteDataConclusao";
            this.dteDataConclusao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDataConclusao.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDataConclusao.Size = new System.Drawing.Size(87, 20);
            this.dteDataConclusao.TabIndex = 72;
            // 
            // campoLookupIDCONTATOCC
            // 
            this.campoLookupIDCONTATOCC.AutoSize = true;
            this.campoLookupIDCONTATOCC.Campo = "USUARIOCHAVE";
            this.campoLookupIDCONTATOCC.ColunaCodigo = "CODUSUARIO";
            this.campoLookupIDCONTATOCC.ColunaDescricao = "NOME";
            this.campoLookupIDCONTATOCC.ColunaIdentificador = null;
            this.campoLookupIDCONTATOCC.ColunaTabela = "";
            this.campoLookupIDCONTATOCC.Conexao = "Start";
            this.campoLookupIDCONTATOCC.Default = null;
            this.campoLookupIDCONTATOCC.Edita = true;
            this.campoLookupIDCONTATOCC.EditaLookup = false;
            this.campoLookupIDCONTATOCC.Location = new System.Drawing.Point(8, 71);
            this.campoLookupIDCONTATOCC.MaximoCaracteres = null;
            this.campoLookupIDCONTATOCC.Name = "campoLookupIDCONTATOCC";
            this.campoLookupIDCONTATOCC.NomeGrid = null;
            this.campoLookupIDCONTATOCC.Query = 0;
            this.campoLookupIDCONTATOCC.Size = new System.Drawing.Size(462, 24);
            this.campoLookupIDCONTATOCC.Tabela = "ZPROJETO";
            this.campoLookupIDCONTATOCC.TabIndex = 70;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(8, 51);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(106, 13);
            this.labelControl6.TabIndex = 71;
            this.labelControl6.Text = "Usuário Chave Cliente";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(8, 142);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(46, 13);
            this.labelControl4.TabIndex = 50;
            this.labelControl4.Text = "Descrição";
            // 
            // campoInteiroPREVISAOHORAS
            // 
            this.campoInteiroPREVISAOHORAS.Campo = "PREVISAOHORAS";
            this.campoInteiroPREVISAOHORAS.Default = 0;
            this.campoInteiroPREVISAOHORAS.Edita = true;
            this.campoInteiroPREVISAOHORAS.Location = new System.Drawing.Point(8, 119);
            this.campoInteiroPREVISAOHORAS.Name = "campoInteiroPREVISAOHORAS";
            this.campoInteiroPREVISAOHORAS.Query = 0;
            this.campoInteiroPREVISAOHORAS.Size = new System.Drawing.Size(100, 20);
            this.campoInteiroPREVISAOHORAS.Tabela = "ZTAREFA";
            this.campoInteiroPREVISAOHORAS.TabIndex = 4;
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(8, 100);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(87, 13);
            this.labelControl12.TabIndex = 48;
            this.labelControl12.Text = "Previsão de Horas";
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(476, 51);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(100, 13);
            this.labelControl11.TabIndex = 46;
            this.labelControl11.Text = "Tipo de Faturamento";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(220, 100);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(75, 13);
            this.labelControl8.TabIndex = 43;
            this.labelControl8.Text = "Data Conclusão";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(631, 51);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(32, 13);
            this.labelControl7.TabIndex = 42;
            this.labelControl7.Text = "In loco";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(114, 100);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(82, 13);
            this.labelControl5.TabIndex = 40;
            this.labelControl5.Text = "Previsão Entrega";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(220, 6);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(32, 13);
            this.labelControl3.TabIndex = 33;
            this.labelControl3.Text = "Tarefa";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(114, 6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 13);
            this.labelControl2.TabIndex = 30;
            this.labelControl2.Text = "Id. Projeto";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 13);
            this.labelControl1.TabIndex = 27;
            this.labelControl1.Text = "Identificador";
            // 
            // btnCancelarAtual
            // 
            this.btnCancelarAtual.Location = new System.Drawing.Point(650, 6);
            this.btnCancelarAtual.Name = "btnCancelarAtual";
            this.btnCancelarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnCancelarAtual.TabIndex = 10;
            this.btnCancelarAtual.Text = "Cancelar";
            this.btnCancelarAtual.Click += new System.EventHandler(this.btnCancelarAtual_Click);
            // 
            // btnOKAtual
            // 
            this.btnOKAtual.Location = new System.Drawing.Point(569, 6);
            this.btnOKAtual.Name = "btnOKAtual";
            this.btnOKAtual.Size = new System.Drawing.Size(75, 23);
            this.btnOKAtual.TabIndex = 9;
            this.btnOKAtual.Text = "OK";
            this.btnOKAtual.Click += new System.EventHandler(this.btnOKAtual_Click);
            // 
            // btnSalvarAtual
            // 
            this.btnSalvarAtual.Location = new System.Drawing.Point(488, 6);
            this.btnSalvarAtual.Name = "btnSalvarAtual";
            this.btnSalvarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnSalvarAtual.TabIndex = 8;
            this.btnSalvarAtual.Text = "Salvar";
            this.btnSalvarAtual.Click += new System.EventHandler(this.btnSalvarAtual_Click);
            // 
            // tbIdTarefaProjeto
            // 
            this.tbIdTarefaProjeto.Location = new System.Drawing.Point(8, 25);
            this.tbIdTarefaProjeto.Name = "tbIdTarefaProjeto";
            this.tbIdTarefaProjeto.Properties.ReadOnly = true;
            this.tbIdTarefaProjeto.Size = new System.Drawing.Size(100, 20);
            this.tbIdTarefaProjeto.TabIndex = 74;
            // 
            // tbTarefa
            // 
            this.tbTarefa.Location = new System.Drawing.Point(220, 25);
            this.tbTarefa.Name = "tbTarefa";
            this.tbTarefa.Size = new System.Drawing.Size(501, 20);
            this.tbTarefa.TabIndex = 75;
            // 
            // tbIdProejto
            // 
            this.tbIdProejto.Location = new System.Drawing.Point(114, 25);
            this.tbIdProejto.Name = "tbIdProejto";
            this.tbIdProejto.Properties.ReadOnly = true;
            this.tbIdProejto.Size = new System.Drawing.Size(100, 20);
            this.tbIdProejto.TabIndex = 76;
            // 
            // tbDescricao
            // 
            this.tbDescricao.Location = new System.Drawing.Point(8, 161);
            this.tbDescricao.Name = "tbDescricao";
            this.tbDescricao.Size = new System.Drawing.Size(713, 125);
            this.tbDescricao.TabIndex = 77;
            // 
            // cbTipoFaturamento
            // 
            this.cbTipoFaturamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoFaturamento.FormattingEnabled = true;
            this.cbTipoFaturamento.Location = new System.Drawing.Point(476, 70);
            this.cbTipoFaturamento.Name = "cbTipoFaturamento";
            this.cbTipoFaturamento.Size = new System.Drawing.Size(149, 21);
            this.cbTipoFaturamento.TabIndex = 109;
            // 
            // cbInLoco
            // 
            this.cbInLoco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInLoco.FormattingEnabled = true;
            this.cbInLoco.Location = new System.Drawing.Point(631, 70);
            this.cbInLoco.Name = "cbInLoco";
            this.cbInLoco.Size = new System.Drawing.Size(90, 21);
            this.cbInLoco.TabIndex = 110;
            // 
            // frmCadastroProjetoTarefa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 363);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCadastroProjetoTarefa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Tarefa do Projeto";
            this.Load += new System.EventHandler(this.frmCadastroProjetoTarefa_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtePrevisaoEntrega.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtePrevisaoEntrega.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDataConclusao.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDataConclusao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIdTarefaProjeto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTarefa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIdProejto.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDescricao.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.SimpleButton btnCancelarAtual;
        private DevExpress.XtraEditors.SimpleButton btnOKAtual;
        private DevExpress.XtraEditors.SimpleButton btnSalvarAtual;
        private AppLib.Windows.CampoInteiro campoInteiroPREVISAOHORAS;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private AppLib.Windows.CampoLookup campoLookupIDCONTATOCC;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.DateEdit dtePrevisaoEntrega;
        private DevExpress.XtraEditors.DateEdit dteDataConclusao;
        private DevExpress.XtraEditors.TextEdit tbIdTarefaProjeto;
        private DevExpress.XtraEditors.TextEdit tbTarefa;
        private DevExpress.XtraEditors.TextEdit tbIdProejto;
        private DevExpress.XtraEditors.MemoEdit tbDescricao;
        private System.Windows.Forms.ComboBox cbTipoFaturamento;
        private System.Windows.Forms.ComboBox cbInLoco;
    }
}