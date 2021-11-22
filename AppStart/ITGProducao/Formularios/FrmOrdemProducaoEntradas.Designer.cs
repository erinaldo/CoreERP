namespace ITGProducao.Formularios
{
    partial class FrmOrdemProducaoEntradas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOrdemProducaoEntradas));
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnGerarEntradas = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.gridSubproduto = new DevExpress.XtraGrid.GridControl();
            this.gridViewSubproduto = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView7 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSubproduto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSubproduto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView7)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btnGerarEntradas);
            this.splitContainer2.Panel2.Controls.Add(this.btnCancelar);
            this.splitContainer2.Size = new System.Drawing.Size(834, 498);
            this.splitContainer2.SplitterDistance = 421;
            this.splitContainer2.TabIndex = 11;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(834, 421);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridSubproduto);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(826, 395);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnGerarEntradas
            // 
            this.btnGerarEntradas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGerarEntradas.Location = new System.Drawing.Point(653, 27);
            this.btnGerarEntradas.Name = "btnGerarEntradas";
            this.btnGerarEntradas.Size = new System.Drawing.Size(88, 23);
            this.btnGerarEntradas.TabIndex = 6;
            this.btnGerarEntradas.Text = "Gerar Entradas";
            this.btnGerarEntradas.Click += new System.EventHandler(this.btnGerarEntradas_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(747, 27);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // gridSubproduto
            // 
            this.gridSubproduto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSubproduto.Location = new System.Drawing.Point(3, 3);
            this.gridSubproduto.MainView = this.gridViewSubproduto;
            this.gridSubproduto.Name = "gridSubproduto";
            this.gridSubproduto.Size = new System.Drawing.Size(820, 389);
            this.gridSubproduto.TabIndex = 18;
            this.gridSubproduto.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSubproduto,
            this.gridView7});
            // 
            // gridViewSubproduto
            // 
            this.gridViewSubproduto.GridControl = this.gridSubproduto;
            this.gridViewSubproduto.Name = "gridViewSubproduto";
            this.gridViewSubproduto.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewSubproduto.OptionsBehavior.Editable = false;
            this.gridViewSubproduto.OptionsFind.FindDelay = 100;
            this.gridViewSubproduto.OptionsSelection.MultiSelect = true;
            this.gridViewSubproduto.OptionsView.ColumnAutoWidth = false;
            this.gridViewSubproduto.OptionsView.ShowAutoFilterRow = true;
            this.gridViewSubproduto.OptionsView.ShowGroupPanel = false;
            // 
            // gridView7
            // 
            this.gridView7.GridControl = this.gridSubproduto;
            this.gridView7.Name = "gridView7";
            // 
            // FrmOrdemProducaoEntradas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 498);
            this.Controls.Add(this.splitContainer2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmOrdemProducaoEntradas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gerar Entradas da Ordem de Produção";
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSubproduto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSubproduto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.SimpleButton btnGerarEntradas;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraGrid.GridControl gridSubproduto;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSubproduto;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView7;
    }
}