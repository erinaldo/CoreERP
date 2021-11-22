namespace PS.Glb.New.Cadastros
{
    partial class frmCadastroApontamentoTarefa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastroApontamentoTarefa));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.teHora = new DevExpress.XtraEditors.TimeEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.campoLookupIDTAREFA = new AppLib.Windows.CampoLookup();
            this.btnCancelarAtual = new DevExpress.XtraEditors.SimpleButton();
            this.btnOKAtual = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalvarAtual = new DevExpress.XtraEditors.SimpleButton();
            this.tbDescricao = new DevExpress.XtraEditors.MemoEdit();
            this.tbIdentificador = new DevExpress.XtraEditors.TextEdit();
            this.tbPercentual = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teHora.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIdentificador.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPercentual.Properties)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(710, 292);
            this.splitContainer1.SplitterDistance = 250;
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
            this.tabControl1.Size = new System.Drawing.Size(710, 250);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbPercentual);
            this.tabPage1.Controls.Add(this.tbIdentificador);
            this.tabPage1.Controls.Add(this.tbDescricao);
            this.tabPage1.Controls.Add(this.teHora);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.labelControl14);
            this.tabPage1.Controls.Add(this.labelControl13);
            this.tabPage1.Controls.Add(this.labelControl11);
            this.tabPage1.Controls.Add(this.campoLookupIDTAREFA);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(702, 224);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tarefa";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // teHora
            // 
            this.teHora.EditValue = new System.DateTime(2021, 6, 25, 0, 0, 0, 0);
            this.teHora.Location = new System.Drawing.Point(546, 26);
            this.teHora.Name = "teHora";
            this.teHora.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teHora.Properties.Mask.EditMask = "t";
            this.teHora.Size = new System.Drawing.Size(65, 20);
            this.teHora.TabIndex = 70;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(546, 7);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(28, 13);
            this.labelControl2.TabIndex = 67;
            this.labelControl2.Text = "Horas";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 13);
            this.labelControl1.TabIndex = 64;
            this.labelControl1.Text = "Identificador";
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(8, 51);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(46, 13);
            this.labelControl14.TabIndex = 61;
            this.labelControl14.Text = "Descrição";
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(617, 7);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(51, 13);
            this.labelControl13.TabIndex = 59;
            this.labelControl13.Text = "Percentual";
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(88, 6);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(32, 13);
            this.labelControl11.TabIndex = 58;
            this.labelControl11.Text = "Tarefa";
            // 
            // campoLookupIDTAREFA
            // 
            this.campoLookupIDTAREFA.Campo = "IDTAREFA";
            this.campoLookupIDTAREFA.ColunaCodigo = "IDTAREFA";
            this.campoLookupIDTAREFA.ColunaDescricao = "NOMETAREFA";
            this.campoLookupIDTAREFA.ColunaIdentificador = null;
            this.campoLookupIDTAREFA.ColunaTabela = "";
            this.campoLookupIDTAREFA.Conexao = "Start";
            this.campoLookupIDTAREFA.Default = null;
            this.campoLookupIDTAREFA.Edita = true;
            this.campoLookupIDTAREFA.EditaLookup = false;
            this.campoLookupIDTAREFA.Location = new System.Drawing.Point(88, 25);
            this.campoLookupIDTAREFA.MaximoCaracteres = null;
            this.campoLookupIDTAREFA.Name = "campoLookupIDTAREFA";
            this.campoLookupIDTAREFA.NomeGrid = null;
            this.campoLookupIDTAREFA.Query = 0;
            this.campoLookupIDTAREFA.Size = new System.Drawing.Size(452, 21);
            this.campoLookupIDTAREFA.Tabela = "ZAPONTAMENTOITEM";
            this.campoLookupIDTAREFA.TabIndex = 1;
            this.campoLookupIDTAREFA.AposSelecao += new AppLib.Windows.CampoLookup.AposSelecaoHandler(this.campoLookupIDTAREFA_AposSelecao);
            // 
            // btnCancelarAtual
            // 
            this.btnCancelarAtual.Location = new System.Drawing.Point(620, 3);
            this.btnCancelarAtual.Name = "btnCancelarAtual";
            this.btnCancelarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnCancelarAtual.TabIndex = 7;
            this.btnCancelarAtual.Text = "Cancelar";
            this.btnCancelarAtual.Click += new System.EventHandler(this.btnCancelarAtual_Click);
            // 
            // btnOKAtual
            // 
            this.btnOKAtual.Location = new System.Drawing.Point(530, 3);
            this.btnOKAtual.Name = "btnOKAtual";
            this.btnOKAtual.Size = new System.Drawing.Size(75, 23);
            this.btnOKAtual.TabIndex = 6;
            this.btnOKAtual.Text = "OK";
            this.btnOKAtual.Click += new System.EventHandler(this.btnOKAtual_Click);
            // 
            // btnSalvarAtual
            // 
            this.btnSalvarAtual.Location = new System.Drawing.Point(439, 3);
            this.btnSalvarAtual.Name = "btnSalvarAtual";
            this.btnSalvarAtual.Size = new System.Drawing.Size(75, 23);
            this.btnSalvarAtual.TabIndex = 5;
            this.btnSalvarAtual.Text = "Salvar";
            this.btnSalvarAtual.Click += new System.EventHandler(this.btnSalvarAtual_Click);
            // 
            // tbDescricao
            // 
            this.tbDescricao.Location = new System.Drawing.Point(8, 70);
            this.tbDescricao.Name = "tbDescricao";
            this.tbDescricao.Size = new System.Drawing.Size(683, 148);
            this.tbDescricao.TabIndex = 71;
            // 
            // tbIdentificador
            // 
            this.tbIdentificador.Location = new System.Drawing.Point(8, 25);
            this.tbIdentificador.Name = "tbIdentificador";
            this.tbIdentificador.Properties.ReadOnly = true;
            this.tbIdentificador.Size = new System.Drawing.Size(74, 20);
            this.tbIdentificador.TabIndex = 72;
            // 
            // tbPercentual
            // 
            this.tbPercentual.Location = new System.Drawing.Point(617, 26);
            this.tbPercentual.Name = "tbPercentual";
            this.tbPercentual.Properties.Mask.EditMask = "P";
            this.tbPercentual.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.tbPercentual.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbPercentual.Size = new System.Drawing.Size(74, 20);
            this.tbPercentual.TabIndex = 73;
            // 
            // frmCadastroApontamentoTarefa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 292);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCadastroApontamentoTarefa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Tarefa do Apontamento";
            this.Load += new System.EventHandler(this.frmCadastroApontamentoTarefa_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teHora.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIdentificador.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbPercentual.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancelarAtual;
        private DevExpress.XtraEditors.SimpleButton btnOKAtual;
        private DevExpress.XtraEditors.SimpleButton btnSalvarAtual;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private AppLib.Windows.CampoLookup campoLookupIDTAREFA;
        private DevExpress.XtraEditors.TimeEdit teHora;
        private DevExpress.XtraEditors.MemoEdit tbDescricao;
        private DevExpress.XtraEditors.TextEdit tbIdentificador;
        private DevExpress.XtraEditors.TextEdit tbPercentual;
    }
}