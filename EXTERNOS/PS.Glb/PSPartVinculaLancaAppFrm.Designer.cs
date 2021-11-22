namespace PS.Glb
{
    partial class PSPartVinculaLancaAppFrm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewCOMVINCULO = new System.Windows.Forms.DataGridView();
            this.dataGridViewSEMVINCULO = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonDESVINCULARTUDO = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonVINCULARTUDO = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCOMVINCULO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSEMVINCULO)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(0, 76);
            this.tabControl1.Size = new System.Drawing.Size(984, 535);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Size = new System.Drawing.Size(976, 509);
            this.tabPage1.Text = "FrmBaseApp";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewCOMVINCULO);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewSEMVINCULO);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(970, 503);
            this.splitContainer1.SplitterDistance = 249;
            this.splitContainer1.TabIndex = 0;
            // 
            // dataGridViewCOMVINCULO
            // 
            this.dataGridViewCOMVINCULO.AllowUserToAddRows = false;
            this.dataGridViewCOMVINCULO.AllowUserToDeleteRows = false;
            this.dataGridViewCOMVINCULO.AllowUserToOrderColumns = true;
            this.dataGridViewCOMVINCULO.AllowUserToResizeRows = false;
            this.dataGridViewCOMVINCULO.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewCOMVINCULO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCOMVINCULO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewCOMVINCULO.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewCOMVINCULO.Name = "dataGridViewCOMVINCULO";
            this.dataGridViewCOMVINCULO.ReadOnly = true;
            this.dataGridViewCOMVINCULO.Size = new System.Drawing.Size(970, 249);
            this.dataGridViewCOMVINCULO.TabIndex = 0;
            this.dataGridViewCOMVINCULO.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewCOMVINCULO_CellMouseDoubleClick);
            // 
            // dataGridViewSEMVINCULO
            // 
            this.dataGridViewSEMVINCULO.AllowUserToAddRows = false;
            this.dataGridViewSEMVINCULO.AllowUserToDeleteRows = false;
            this.dataGridViewSEMVINCULO.AllowUserToOrderColumns = true;
            this.dataGridViewSEMVINCULO.AllowUserToResizeRows = false;
            this.dataGridViewSEMVINCULO.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewSEMVINCULO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSEMVINCULO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSEMVINCULO.Location = new System.Drawing.Point(0, 25);
            this.dataGridViewSEMVINCULO.Name = "dataGridViewSEMVINCULO";
            this.dataGridViewSEMVINCULO.ReadOnly = true;
            this.dataGridViewSEMVINCULO.Size = new System.Drawing.Size(970, 225);
            this.dataGridViewSEMVINCULO.TabIndex = 1;
            this.dataGridViewSEMVINCULO.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewSEMVINCULO_CellMouseDoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.toolStripLabel1,
            this.toolStripSeparator2,
            this.toolStripButtonDESVINCULARTUDO,
            this.toolStripButtonVINCULARTUDO});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(970, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // PSPartVinculaLancaAppFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Name = "PSPartVinculaLancaAppFrm";
            this.Text = "PSPartVinculaLancaAppFrm";
            this.Load += new System.EventHandler(this.PSPartVinculaLancaAppFrm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCOMVINCULO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSEMVINCULO)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridViewCOMVINCULO;
        private System.Windows.Forms.DataGridView dataGridViewSEMVINCULO;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonDESVINCULARTUDO;
        private System.Windows.Forms.ToolStripButton toolStripButtonVINCULARTUDO;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}