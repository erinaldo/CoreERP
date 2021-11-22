namespace PS.Glb
{
    partial class PSPartProdutoFiscalEdit
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
            this.psLookup4 = new PS.Lib.WinForms.PSLookup();
            this.psMoedaBox1 = new PS.Lib.WinForms.PSMoedaBox();
            this.psMoedaBox2 = new PS.Lib.WinForms.PSMoedaBox();
            this.psMoedaBox3 = new PS.Lib.WinForms.PSMoedaBox();
            this.psComboBox1 = new PS.Lib.WinForms.PSComboBox();
            this.psComboBox2 = new PS.Lib.WinForms.PSComboBox();
            this.psMoedaBox4 = new PS.Lib.WinForms.PSMoedaBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(647, 360);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psMoedaBox4);
            this.tabPage1.Controls.Add(this.psComboBox2);
            this.tabPage1.Controls.Add(this.psComboBox1);
            this.tabPage1.Controls.Add(this.psMoedaBox3);
            this.tabPage1.Controls.Add(this.psMoedaBox2);
            this.tabPage1.Controls.Add(this.psMoedaBox1);
            this.tabPage1.Controls.Add(this.psLookup4);
            this.tabPage1.Size = new System.Drawing.Size(639, 334);
            // 
            // psLookup4
            // 
            this.psLookup4.Caption = "CODETD";
            this.psLookup4.Chave = true;
            this.psLookup4.DataField = "CODETD";
            this.psLookup4.Description = "";
            this.psLookup4.DinamicTable = null;
            this.psLookup4.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup4.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup4.KeyField = "CODETD";
            this.psLookup4.Location = new System.Drawing.Point(11, 6);
            this.psLookup4.LookupField = "CODETD;NOME";
            this.psLookup4.LookupFieldResult = "CODETD;NOME";
            this.psLookup4.MaxLength = 2;
            this.psLookup4.Name = "psLookup4";
            this.psLookup4.PSPart = null;
            this.psLookup4.Size = new System.Drawing.Size(401, 38);
            this.psLookup4.TabIndex = 9;
            this.psLookup4.ValorRetorno = null;
            // 
            // psMoedaBox1
            // 
            this.psMoedaBox1.Caption = "ALIQUOTAINTICMS";
            this.psMoedaBox1.CasasDecimais = 2;
            this.psMoedaBox1.DataField = "ALIQUOTAINTICMS";
            this.psMoedaBox1.Edita = true;
            this.psMoedaBox1.Location = new System.Drawing.Point(11, 50);
            this.psMoedaBox1.Name = "psMoedaBox1";
            this.psMoedaBox1.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox1.TabIndex = 10;
            // 
            // psMoedaBox2
            // 
            this.psMoedaBox2.Caption = "PERCREDUCAOICMS";
            this.psMoedaBox2.CasasDecimais = 2;
            this.psMoedaBox2.DataField = "PERCREDUCAOICMS";
            this.psMoedaBox2.Edita = true;
            this.psMoedaBox2.Location = new System.Drawing.Point(162, 50);
            this.psMoedaBox2.Name = "psMoedaBox2";
            this.psMoedaBox2.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox2.TabIndex = 11;
            // 
            // psMoedaBox3
            // 
            this.psMoedaBox3.Caption = "PERCMARGEMST";
            this.psMoedaBox3.CasasDecimais = 2;
            this.psMoedaBox3.DataField = "PERCMARGEMST";
            this.psMoedaBox3.Edita = true;
            this.psMoedaBox3.Location = new System.Drawing.Point(313, 50);
            this.psMoedaBox3.Name = "psMoedaBox3";
            this.psMoedaBox3.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox3.TabIndex = 12;
            // 
            // psComboBox1
            // 
            this.psComboBox1.Caption = "MODDETBCICMS";
            this.psComboBox1.Chave = true;
            this.psComboBox1.DataField = "MODDETBCICMS";
            this.psComboBox1.DataSource = null;
            this.psComboBox1.DisplayMember = "";
            this.psComboBox1.Location = new System.Drawing.Point(11, 93);
            this.psComboBox1.Name = "psComboBox1";
            this.psComboBox1.SelectedIndex = -1;
            this.psComboBox1.Size = new System.Drawing.Size(296, 37);
            this.psComboBox1.TabIndex = 13;
            this.psComboBox1.Value = null;
            this.psComboBox1.ValueMember = "";
            // 
            // psComboBox2
            // 
            this.psComboBox2.Caption = "MODDETBCICMSST";
            this.psComboBox2.Chave = true;
            this.psComboBox2.DataField = "MODDETBCICMSST";
            this.psComboBox2.DataSource = null;
            this.psComboBox2.DisplayMember = "";
            this.psComboBox2.Location = new System.Drawing.Point(11, 136);
            this.psComboBox2.Name = "psComboBox2";
            this.psComboBox2.SelectedIndex = -1;
            this.psComboBox2.Size = new System.Drawing.Size(296, 37);
            this.psComboBox2.TabIndex = 14;
            this.psComboBox2.Value = null;
            this.psComboBox2.ValueMember = "";
            // 
            // psMoedaBox4
            // 
            this.psMoedaBox4.Caption = "PDIF";
            this.psMoedaBox4.CasasDecimais = 4;
            this.psMoedaBox4.DataField = "PDIF";
            this.psMoedaBox4.Edita = true;
            this.psMoedaBox4.Location = new System.Drawing.Point(313, 92);
            this.psMoedaBox4.Name = "psMoedaBox4";
            this.psMoedaBox4.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox4.TabIndex = 15;
            // 
            // PSPartProdutoFiscalEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 476);
            this.Name = "PSPartProdutoFiscalEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartProdutoFiscalEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Lib.WinForms.PSLookup psLookup4;
        private Lib.WinForms.PSComboBox psComboBox2;
        private Lib.WinForms.PSComboBox psComboBox1;
        private Lib.WinForms.PSMoedaBox psMoedaBox3;
        private Lib.WinForms.PSMoedaBox psMoedaBox2;
        private Lib.WinForms.PSMoedaBox psMoedaBox1;
        private Lib.WinForms.PSMoedaBox psMoedaBox4;
    }
}