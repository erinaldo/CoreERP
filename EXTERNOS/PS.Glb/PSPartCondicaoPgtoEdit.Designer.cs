namespace PS.Glb
{
    partial class PSPartCondicaoPgtoEdit
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
            this.psTextoBox1 = new PS.Lib.WinForms.PSTextoBox();
            this.psTextoBox2 = new PS.Lib.WinForms.PSTextoBox();
            this.psCheckBox1 = new PS.Lib.WinForms.PSCheckBox();
            this.psComboBox1 = new PS.Lib.WinForms.PSComboBox();
            this.psMoedaBox1 = new PS.Lib.WinForms.PSMoedaBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.PSPartCondicaoPgtoComposicao = new PS.Lib.WinForms.PSBaseVisao();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.psMoedaBox2 = new PS.Lib.WinForms.PSMoedaBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
           // this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Size = new System.Drawing.Size(647, 258);
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.Controls.SetChildIndex(this.tabPage2, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPage1, 0);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psMoedaBox2);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.psMoedaBox1);
            this.tabPage1.Controls.Add(this.psComboBox1);
            this.tabPage1.Controls.Add(this.psCheckBox1);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(639, 232);
            // 
            // panel2
            // 
            //this.panel2.Location = new System.Drawing.Point(0, 320);
            //this.panel2.Size = new System.Drawing.Size(647, 54);
            //// 
            //// buttonSALVAR
            //// 
            //this.buttonSALVAR.Location = new System.Drawing.Point(398, 15);
            //// 
            //// buttonOK
            //// 
            //this.buttonOK.Location = new System.Drawing.Point(479, 15);
            //// 
            //// buttonCANCELAR
            //// 
            //this.buttonCANCELAR.Location = new System.Drawing.Point(560, 15);
            //// 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(647, 30);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODCONDICAO";
            this.psTextoBox1.DataField = "CODCONDICAO";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 5;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 0;
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "NOME";
            this.psTextoBox2.DataField = "NOME";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.Location = new System.Drawing.Point(11, 49);
            this.psTextoBox2.MaxLength = 80;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox2.TabIndex = 3;
            // 
            // psCheckBox1
            // 
            this.psCheckBox1.Caption = "ATIVO";
            this.psCheckBox1.Chave = true;
            this.psCheckBox1.Checked = false;
            this.psCheckBox1.DataField = "ATIVO";
            this.psCheckBox1.Location = new System.Drawing.Point(162, 21);
            this.psCheckBox1.Name = "psCheckBox1";
            this.psCheckBox1.Size = new System.Drawing.Size(105, 22);
            this.psCheckBox1.TabIndex = 1;
            // 
            // psComboBox1
            // 
            this.psComboBox1.Caption = "TIPO";
            this.psComboBox1.Chave = true;
            this.psComboBox1.DataField = "TIPO";
            this.psComboBox1.DataSource = null;
            this.psComboBox1.DisplayMember = "";
            this.psComboBox1.Location = new System.Drawing.Point(273, 6);
            this.psComboBox1.Name = "psComboBox1";
            this.psComboBox1.SelectedIndex = -1;
            this.psComboBox1.Size = new System.Drawing.Size(139, 37);
            this.psComboBox1.TabIndex = 2;
            this.psComboBox1.Value = null;
            this.psComboBox1.ValueMember = "";
            // 
            // psMoedaBox1
            // 
            this.psMoedaBox1.Caption = "TAXAJUROS";
            this.psMoedaBox1.CasasDecimais = 2;
            this.psMoedaBox1.DataField = "TAXAJUROS";
            this.psMoedaBox1.Edita = true;
            this.psMoedaBox1.Location = new System.Drawing.Point(11, 92);
            this.psMoedaBox1.Name = "psMoedaBox1";
            this.psMoedaBox1.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox1.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.PSPartCondicaoPgtoComposicao);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(639, 232);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Composição das Parcelas";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // PSPartCondicaoPgtoComposicao
            // 
            this.PSPartCondicaoPgtoComposicao._atualiza = false;
            this.PSPartCondicaoPgtoComposicao.aplicativo = null;
            this.PSPartCondicaoPgtoComposicao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PSPartCondicaoPgtoComposicao.Location = new System.Drawing.Point(3, 3);
            this.PSPartCondicaoPgtoComposicao.Name = "PSPartCondicaoPgtoComposicao";
            this.PSPartCondicaoPgtoComposicao.PermiteEditar = false;
            this.PSPartCondicaoPgtoComposicao.PermiteExcluir = false;
            this.PSPartCondicaoPgtoComposicao.PermiteIncluir = false;
            this.PSPartCondicaoPgtoComposicao.psPart = null;
            this.PSPartCondicaoPgtoComposicao.Size = new System.Drawing.Size(633, 226);
            this.PSPartCondicaoPgtoComposicao.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(418, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "% Composição Parcela";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(421, 22);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(117, 21);
            this.textBox2.TabIndex = 9;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(544, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(26, 21);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // psMoedaBox2
            // 
            this.psMoedaBox2.Caption = "VALORMINIMO";
            this.psMoedaBox2.CasasDecimais = 2;
            this.psMoedaBox2.DataField = "VALORMINIMO";
            this.psMoedaBox2.Edita = true;
            this.psMoedaBox2.Location = new System.Drawing.Point(162, 92);
            this.psMoedaBox2.Name = "psMoedaBox2";
            this.psMoedaBox2.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox2.TabIndex = 11;
            // 
            // PSPartCondicaoPgtoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartCondicaoPgtoEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartCondicaoPgtoEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
           // this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private PS.Lib.WinForms.PSComboBox psComboBox1;
        private PS.Lib.WinForms.PSCheckBox psCheckBox1;
        private PS.Lib.WinForms.PSMoedaBox psMoedaBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private Lib.WinForms.PSBaseVisao PSPartCondicaoPgtoComposicao;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Lib.WinForms.PSMoedaBox psMoedaBox2;
    }
}