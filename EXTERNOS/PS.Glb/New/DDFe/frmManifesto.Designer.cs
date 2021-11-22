namespace PS.Glb.New.DDFe
{
    partial class frmManifesto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManifesto));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbChave = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbEvento = new System.Windows.Forms.ComboBox();
            this.lbEvento = new DevExpress.XtraEditors.LabelControl();
            this.tbJustificativa = new DevExpress.XtraEditors.MemoEdit();
            this.btnExecutar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::PS.Glb.ERP.Global.WaitForm1), true, true);
            this.lpFilial = new ITGProducao.Controles.NewLookup();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbChave.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbJustificativa.Properties)).BeginInit();
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
            this.splitContainer1.Panel2.Controls.Add(this.btnExecutar);
            this.splitContainer1.Panel2.Controls.Add(this.btnCancelar);
            this.splitContainer1.Size = new System.Drawing.Size(460, 332);
            this.splitContainer1.SplitterDistance = 296;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(460, 296);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lpFilial);
            this.tabPage1.Controls.Add(this.tbChave);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.cmbEvento);
            this.tabPage1.Controls.Add(this.lbEvento);
            this.tabPage1.Controls.Add(this.tbJustificativa);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(452, 270);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Processo";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbChave
            // 
            this.tbChave.Location = new System.Drawing.Point(8, 74);
            this.tbChave.Name = "tbChave";
            this.tbChave.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.tbChave.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.tbChave.Size = new System.Drawing.Size(438, 20);
            this.tbChave.TabIndex = 69;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 100);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(34, 13);
            this.labelControl1.TabIndex = 67;
            this.labelControl1.Text = "Evento";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 55);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(31, 13);
            this.labelControl2.TabIndex = 68;
            this.labelControl2.Text = "Chave";
            // 
            // cmbEvento
            // 
            this.cmbEvento.FormattingEnabled = true;
            this.cmbEvento.Location = new System.Drawing.Point(8, 119);
            this.cmbEvento.Name = "cmbEvento";
            this.cmbEvento.Size = new System.Drawing.Size(178, 21);
            this.cmbEvento.TabIndex = 66;
            this.cmbEvento.SelectedIndexChanged += new System.EventHandler(this.cmbEvento_SelectedIndexChanged);
            // 
            // lbEvento
            // 
            this.lbEvento.Location = new System.Drawing.Point(8, 146);
            this.lbEvento.Name = "lbEvento";
            this.lbEvento.Size = new System.Drawing.Size(57, 13);
            this.lbEvento.TabIndex = 20;
            this.lbEvento.Text = "Justificativa";
            // 
            // tbJustificativa
            // 
            this.tbJustificativa.Location = new System.Drawing.Point(6, 165);
            this.tbJustificativa.Name = "tbJustificativa";
            this.tbJustificativa.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White;
            this.tbJustificativa.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.tbJustificativa.Size = new System.Drawing.Size(440, 99);
            this.tbJustificativa.TabIndex = 65;
            // 
            // btnExecutar
            // 
            this.btnExecutar.Location = new System.Drawing.Point(294, 3);
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Size = new System.Drawing.Size(75, 23);
            this.btnExecutar.TabIndex = 45;
            this.btnExecutar.Text = "Executar";
            this.btnExecutar.Click += new System.EventHandler(this.btnExecutar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(375, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 44;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
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
            this.lpFilial.Location = new System.Drawing.Point(3, 6);
            this.lpFilial.MensagemCodigoVazio = null;
            this.lpFilial.mensagemErrorProvider = null;
            this.lpFilial.Name = "lpFilial";
            this.lpFilial.Projeto_Formularios = "ITGProducao";
            this.lpFilial.Size = new System.Drawing.Size(446, 46);
            this.lpFilial.TabelaBD = "GFILIAL";
            this.lpFilial.TabIndex = 70;
            this.lpFilial.Titulo = "Filial";
            this.lpFilial.ValorCodigoInterno = "";
            this.lpFilial.whereParametros = null;
            this.lpFilial.whereVisao = null;
            // 
            // frmManifesto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 332);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmManifesto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manifestação";
            this.Load += new System.EventHandler(this.frmManifesto_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbChave.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbJustificativa.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.LabelControl lbEvento;
        private DevExpress.XtraEditors.SimpleButton btnExecutar;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.MemoEdit tbJustificativa;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.ComboBox cmbEvento;
        private DevExpress.XtraEditors.TextEdit tbChave;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
        private ITGProducao.Controles.NewLookup lpFilial;
    }
}