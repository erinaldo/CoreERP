namespace PS.Glb
{
    partial class PSPartCondicaoPgtoComposicaoEdit
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
            this.psMoedaBox1 = new PS.Lib.WinForms.PSMoedaBox();
            this.psMoedaBox2 = new PS.Lib.WinForms.PSMoedaBox();
            this.psMoedaBox3 = new PS.Lib.WinForms.PSMoedaBox();
            this.psMoedaBox4 = new PS.Lib.WinForms.PSMoedaBox();
            this.psComboBox1 = new PS.Lib.WinForms.PSComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
           // this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(647, 258);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psComboBox1);
            this.tabPage1.Controls.Add(this.psMoedaBox4);
            this.tabPage1.Controls.Add(this.psMoedaBox3);
            this.tabPage1.Controls.Add(this.psMoedaBox2);
            this.tabPage1.Controls.Add(this.psMoedaBox1);
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
            //this.buttonSALVAR.Location = new System.Drawing.Point(370, 15);
            //// 
            //// buttonOK
            //// 
            //this.buttonOK.Location = new System.Drawing.Point(461, 15);
            //// 
            //// buttonCANCELAR
            //// 
            //this.buttonCANCELAR.Location = new System.Drawing.Point(551, 15);
            //// 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(647, 30);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.AutoIncremento = PS.Lib.Global.TypeAutoinc.Max;
            this.psTextoBox1.Caption = "IDCOMPOSICAO";
            this.psTextoBox1.DataField = "IDCOMPOSICAO";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 32767;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(130, 37);
            this.psTextoBox1.TabIndex = 10;
            // 
            // psMoedaBox1
            // 
            this.psMoedaBox1.Caption = "PERCVALOR";
            this.psMoedaBox1.CasasDecimais = 2;
            this.psMoedaBox1.DataField = "PERCVALOR";
            this.psMoedaBox1.Edita = true;
            this.psMoedaBox1.Location = new System.Drawing.Point(11, 49);
            this.psMoedaBox1.Name = "psMoedaBox1";
            this.psMoedaBox1.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox1.TabIndex = 11;
            // 
            // psMoedaBox2
            // 
            this.psMoedaBox2.Caption = "NUMPARCELAS";
            this.psMoedaBox2.DataField = "NUMPARCELAS";
            this.psMoedaBox2.Edita = true;
            this.psMoedaBox2.Location = new System.Drawing.Point(162, 49);
            this.psMoedaBox2.Name = "psMoedaBox2";
            this.psMoedaBox2.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox2.TabIndex = 12;
            // 
            // psMoedaBox3
            // 
            this.psMoedaBox3.Caption = "NUMPRAZO";
            this.psMoedaBox3.DataField = "NUMPRAZO";
            this.psMoedaBox3.Edita = true;
            this.psMoedaBox3.Location = new System.Drawing.Point(313, 49);
            this.psMoedaBox3.Name = "psMoedaBox3";
            this.psMoedaBox3.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox3.TabIndex = 13;
            // 
            // psMoedaBox4
            // 
            this.psMoedaBox4.Caption = "NUMINTERVALO";
            this.psMoedaBox4.DataField = "NUMINTERVALO";
            this.psMoedaBox4.Edita = true;
            this.psMoedaBox4.Location = new System.Drawing.Point(464, 49);
            this.psMoedaBox4.Name = "psMoedaBox4";
            this.psMoedaBox4.Size = new System.Drawing.Size(145, 37);
            this.psMoedaBox4.TabIndex = 14;
            // 
            // psComboBox1
            // 
            this.psComboBox1.Caption = "TIPO";
            this.psComboBox1.Chave = true;
            this.psComboBox1.DataField = "TIPO";
            this.psComboBox1.DataSource = null;
            this.psComboBox1.DisplayMember = "";
            this.psComboBox1.Location = new System.Drawing.Point(162, 6);
            this.psComboBox1.Name = "psComboBox1";
            this.psComboBox1.SelectedIndex = -1;
            this.psComboBox1.Size = new System.Drawing.Size(139, 37);
            this.psComboBox1.TabIndex = 15;
            this.psComboBox1.Value = null;
            this.psComboBox1.ValueMember = "";
            // 
            // PSPartCondicaoPgtoComposicaoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartCondicaoPgtoComposicaoEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartCondicaoPgtoComposicaoEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            //this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Lib.WinForms.PSMoedaBox psMoedaBox1;
        private Lib.WinForms.PSTextoBox psTextoBox1;
        private Lib.WinForms.PSMoedaBox psMoedaBox4;
        private Lib.WinForms.PSMoedaBox psMoedaBox3;
        private Lib.WinForms.PSMoedaBox psMoedaBox2;
        private Lib.WinForms.PSComboBox psComboBox1;
    }
}