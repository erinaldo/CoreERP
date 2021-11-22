namespace PS.Glb.New.Processos.Financeiro
{
    partial class frmVinculoLancamento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVinculoLancamento));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDESVINCULARTUDO = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonVINCULARTUDO = new System.Windows.Forms.ToolStripButton();
            this.dataGridViewCOMVINCULO = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btnExecutar = new DevExpress.XtraEditors.SimpleButton();
            this.btnFechar = new DevExpress.XtraEditors.SimpleButton();
            this.dataGridViewSEMVINCULO = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCOMVINCULO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSEMVINCULO)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewCOMVINCULO);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(984, 661);
            this.splitContainer1.SplitterDistance = 328;
            this.splitContainer1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.toolStripLabel1,
            this.toolStripSeparator2,
            this.toolStripButtonDESVINCULARTUDO,
            this.toolStripButtonVINCULARTUDO});
            this.toolStrip1.Location = new System.Drawing.Point(0, 303);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(984, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.ReadOnly = true;
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel1.Text = "Saldo:";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonDESVINCULARTUDO
            // 
            this.toolStripButtonDESVINCULARTUDO.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonDESVINCULARTUDO.Image = global::PS.Glb.Properties.Resources.img_extsai;
            this.toolStripButtonDESVINCULARTUDO.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDESVINCULARTUDO.Name = "toolStripButtonDESVINCULARTUDO";
            this.toolStripButtonDESVINCULARTUDO.Size = new System.Drawing.Size(119, 22);
            this.toolStripButtonDESVINCULARTUDO.Text = "Desvincular Tudo";
            this.toolStripButtonDESVINCULARTUDO.Click += new System.EventHandler(this.toolStripButtonDESVINCULARTUDO_Click);
            // 
            // toolStripButtonVINCULARTUDO
            // 
            this.toolStripButtonVINCULARTUDO.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButtonVINCULARTUDO.Image = global::PS.Glb.Properties.Resources.img_extent;
            this.toolStripButtonVINCULARTUDO.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonVINCULARTUDO.Name = "toolStripButtonVINCULARTUDO";
            this.toolStripButtonVINCULARTUDO.Size = new System.Drawing.Size(101, 22);
            this.toolStripButtonVINCULARTUDO.Text = "Vincular Tudo";
            this.toolStripButtonVINCULARTUDO.Click += new System.EventHandler(this.toolStripButtonVINCULARTUDO_Click);
            // 
            // dataGridViewCOMVINCULO
            // 
            this.dataGridViewCOMVINCULO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCOMVINCULO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewCOMVINCULO.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewCOMVINCULO.Name = "dataGridViewCOMVINCULO";
            this.dataGridViewCOMVINCULO.Size = new System.Drawing.Size(984, 303);
            this.dataGridViewCOMVINCULO.TabIndex = 4;
            this.dataGridViewCOMVINCULO.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewCOMVINCULO_CellMouseDoubleClick);
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
            this.splitContainer2.Panel1.Controls.Add(this.dataGridViewSEMVINCULO);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btnExecutar);
            this.splitContainer2.Panel2.Controls.Add(this.btnFechar);
            this.splitContainer2.Size = new System.Drawing.Size(984, 329);
            this.splitContainer2.SplitterDistance = 282;
            this.splitContainer2.TabIndex = 0;
            // 
            // btnExecutar
            // 
            this.btnExecutar.Location = new System.Drawing.Point(818, 8);
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Size = new System.Drawing.Size(75, 23);
            this.btnExecutar.TabIndex = 17;
            this.btnExecutar.Text = "Executar";
            this.btnExecutar.Click += new System.EventHandler(this.btnExecutar_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.Location = new System.Drawing.Point(897, 8);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(75, 23);
            this.btnFechar.TabIndex = 16;
            this.btnFechar.Text = "Cancelar";
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // dataGridViewSEMVINCULO
            // 
            this.dataGridViewSEMVINCULO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSEMVINCULO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSEMVINCULO.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewSEMVINCULO.Name = "dataGridViewSEMVINCULO";
            this.dataGridViewSEMVINCULO.Size = new System.Drawing.Size(984, 282);
            this.dataGridViewSEMVINCULO.TabIndex = 5;
            this.dataGridViewSEMVINCULO.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewSEMVINCULO_CellMouseDoubleClick);
            // 
            // frmVinculoLancamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVinculoLancamento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vincula Lançamento";
            this.Load += new System.EventHandler(this.frmVinculoLancamento_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCOMVINCULO)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSEMVINCULO)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonDESVINCULARTUDO;
        private System.Windows.Forms.ToolStripButton toolStripButtonVINCULARTUDO;
        private System.Windows.Forms.DataGridView dataGridViewCOMVINCULO;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridViewSEMVINCULO;
        private DevExpress.XtraEditors.SimpleButton btnExecutar;
        private DevExpress.XtraEditors.SimpleButton btnFechar;
    }
}