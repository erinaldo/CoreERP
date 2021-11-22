namespace PS.Glb
{
    partial class PSPartGerTarefaLogEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PSPartGerTarefaLogEdit));
            this.psMoedaBox1 = new PS.Lib.WinForms.PSMoedaBox();
            this.psDateBox1 = new PS.Lib.WinForms.PSDateBox();
            this.psDateBox2 = new PS.Lib.WinForms.PSDateBox();
            this.psMemoBox1 = new PS.Lib.WinForms.PSMemoBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(647, 352);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.psMemoBox1);
            this.tabPage1.Controls.Add(this.psDateBox2);
            this.tabPage1.Controls.Add(this.psDateBox1);
            this.tabPage1.Controls.Add(this.psMoedaBox1);
            this.tabPage1.Size = new System.Drawing.Size(639, 326);
            // 
            // psMoedaBox1
            // 
            this.psMoedaBox1.Caption = "NSEQ";
            this.psMoedaBox1.DataField = "NSEQ";
            this.psMoedaBox1.Edita = true;
            this.psMoedaBox1.Location = new System.Drawing.Point(11, 6);
            this.psMoedaBox1.Name = "psMoedaBox1";
            this.psMoedaBox1.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox1.TabIndex = 0;
            // 
            // psDateBox1
            // 
            this.psDateBox1.Caption = "DATAHORAEXECINICIAL";
            this.psDateBox1.Chave = true;
            this.psDateBox1.DataField = "DATAHORAEXECINICIAL";
            this.psDateBox1.Location = new System.Drawing.Point(12, 49);
            this.psDateBox1.Mascara = "00/00/0000 00:00:00.000";
            this.psDateBox1.MaxLength = 32767;
            this.psDateBox1.Name = "psDateBox1";
            this.psDateBox1.Size = new System.Drawing.Size(172, 37);
            this.psDateBox1.TabIndex = 1;
            this.psDateBox1.Value = new System.DateTime(2016, 2, 20, 12, 44, 3, 624);
            // 
            // psDateBox2
            // 
            this.psDateBox2.Caption = "DATAHORAEXECFINAL";
            this.psDateBox2.Chave = true;
            this.psDateBox2.DataField = "DATAHORAEXECFINAL";
            this.psDateBox2.Location = new System.Drawing.Point(190, 49);
            this.psDateBox2.Mascara = "00/00/0000 00:00:00.000";
            this.psDateBox2.MaxLength = 32767;
            this.psDateBox2.Name = "psDateBox2";
            this.psDateBox2.Size = new System.Drawing.Size(172, 37);
            this.psDateBox2.TabIndex = 2;
            this.psDateBox2.Value = new System.DateTime(2016, 2, 20, 12, 44, 3, 624);
            // 
            // psMemoBox1
            // 
            this.psMemoBox1.Caption = "LOGEXECUCAO";
            this.psMemoBox1.Chave = true;
            this.psMemoBox1.DataField = "LOGEXECUCAO";
            this.psMemoBox1.Location = new System.Drawing.Point(12, 92);
            this.psMemoBox1.Name = "psMemoBox1";
            this.psMemoBox1.ReadOnly = true;
            this.psMemoBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.psMemoBox1.Size = new System.Drawing.Size(431, 186);
            this.psMemoBox1.TabIndex = 3;
            this.psMemoBox1.WordWrap = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Window;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderColor = System.Drawing.SystemColors.Window;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Window;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Window;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(449, 109);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(35, 28);
            this.button2.TabIndex = 4;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // PSPartGerTarefaLogEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 468);
            this.Name = "PSPartGerTarefaLogEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartGerTarefaLogEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Lib.WinForms.PSMoedaBox psMoedaBox1;
        private Lib.WinForms.PSDateBox psDateBox2;
        private Lib.WinForms.PSDateBox psDateBox1;
        private Lib.WinForms.PSMemoBox psMemoBox1;
        private System.Windows.Forms.Button button2;
    }
}