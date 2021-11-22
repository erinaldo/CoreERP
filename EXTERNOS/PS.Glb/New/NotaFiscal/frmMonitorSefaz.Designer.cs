namespace PS.Glb.New.NotaFiscal
{
    partial class frmMonitorSefaz
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMonitorSefaz));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbModelo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lpFilial = new ITGProducao.Controles.NewLookup();
            this.btnExecutar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbModelo.Properties)).BeginInit();
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
            this.splitContainer1.Panel2.Controls.Add(this.btnExecutar);
            this.splitContainer1.Panel2.Controls.Add(this.btnCancelar);
            this.splitContainer1.Size = new System.Drawing.Size(404, 168);
            this.splitContainer1.SplitterDistance = 130;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(404, 130);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbModelo);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.lpFilial);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(396, 104);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Monitor";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbModelo
            // 
            this.tbModelo.Location = new System.Drawing.Point(8, 25);
            this.tbModelo.Name = "tbModelo";
            this.tbModelo.Size = new System.Drawing.Size(378, 20);
            this.tbModelo.TabIndex = 19;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(34, 13);
            this.labelControl2.TabIndex = 18;
            this.labelControl2.Text = "Modelo";
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
            this.lpFilial.Location = new System.Drawing.Point(6, 51);
            this.lpFilial.MensagemCodigoVazio = null;
            this.lpFilial.mensagemErrorProvider = null;
            this.lpFilial.Name = "lpFilial";
            this.lpFilial.Projeto_Formularios = "ITGProducao";
            this.lpFilial.Size = new System.Drawing.Size(384, 46);
            this.lpFilial.TabelaBD = "GFILIAL";
            this.lpFilial.TabIndex = 13;
            this.lpFilial.Titulo = "Filial";
            this.lpFilial.ValorCodigoInterno = "";
            this.lpFilial.whereParametros = null;
            this.lpFilial.whereVisao = null;
            // 
            // btnExecutar
            // 
            this.btnExecutar.Location = new System.Drawing.Point(238, 3);
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Size = new System.Drawing.Size(75, 23);
            this.btnExecutar.TabIndex = 43;
            this.btnExecutar.Text = "Executar";
            this.btnExecutar.Click += new System.EventHandler(this.btnExecutar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(319, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 42;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // frmMonitorSefaz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 168);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMonitorSefaz";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monitor SEFAZ";
            this.Load += new System.EventHandler(this.frmMonitorSefaz_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbModelo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.SimpleButton btnExecutar;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ITGProducao.Controles.NewLookup lpFilial;
        private DevExpress.XtraEditors.TextEdit tbModelo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}