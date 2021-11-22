namespace PS.Glb
{
    partial class PSPartNaturezaEdit
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.psCheckBoxCONTRIBUINTEICMS = new PS.Lib.WinForms.PSCheckBox();
            this.psComboBoxCLASSVENDA2 = new PS.Lib.WinForms.PSComboBox();
            this.psCheckBoxDENTRODOESTADO = new PS.Lib.WinForms.PSCheckBox();
            this.psLookup3 = new PS.Lib.WinForms.PSLookup();
            this.psLookup2 = new PS.Lib.WinForms.PSLookup();
            this.psComboBox8 = new PS.Lib.WinForms.PSComboBox();
            this.psLookup1 = new PS.Lib.WinForms.PSLookup();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.PSPartNaturezaRegraTributacao = new PS.Lib.WinForms.PSBaseVisao();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.PSPartNaturezaTributo = new PS.Lib.WinForms.PSBaseVisao();
            this.psCheckBox8 = new PS.Lib.WinForms.PSCheckBox();
            this.psMemoBox1 = new PS.Lib.WinForms.PSMemoBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
          //  this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Size = new System.Drawing.Size(478, 277);
            this.tabControl1.Controls.SetChildIndex(this.tabPage4, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPage3, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPage2, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPage1, 0);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psMemoBox1);
            this.tabPage1.Controls.Add(this.psCheckBox8);
            this.tabPage1.Controls.Add(this.psCheckBox1);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(470, 251);
            // 
            // panel2
            // 
            //this.panel2.Location = new System.Drawing.Point(0, 339);
            //this.panel2.Size = new System.Drawing.Size(478, 54);
            //// 
            //// buttonSALVAR
            //// 
            //this.buttonSALVAR.Location = new System.Drawing.Point(210, 19);
            //// 
            //// buttonOK
            //// 
            //this.buttonOK.Location = new System.Drawing.Point(291, 19);
            //// 
            //// buttonCANCELAR
            //// 
            //this.buttonCANCELAR.Location = new System.Drawing.Point(372, 19);
            //// 
            //// panel1
            // 
            this.panel1.Size = new System.Drawing.Size(478, 30);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODNATUREZA";
            this.psTextoBox1.DataField = "CODNATUREZA";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 15;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 0;
            this.psTextoBox1.Validating += new System.ComponentModel.CancelEventHandler(this.psTextoBox1_Validating);
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "DESCRICAO";
            this.psTextoBox2.DataField = "DESCRICAO";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.Location = new System.Drawing.Point(11, 49);
            this.psTextoBox2.MaxLength = 80;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(432, 37);
            this.psTextoBox2.TabIndex = 1;
            // 
            // psCheckBox1
            // 
            this.psCheckBox1.Caption = "ATIVO";
            this.psCheckBox1.Chave = true;
            this.psCheckBox1.Checked = false;
            this.psCheckBox1.DataField = "ATIVO";
            this.psCheckBox1.Location = new System.Drawing.Point(162, 21);
            this.psCheckBox1.Name = "psCheckBox1";
            this.psCheckBox1.Size = new System.Drawing.Size(76, 22);
            this.psCheckBox1.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.psCheckBoxCONTRIBUINTEICMS);
            this.tabPage2.Controls.Add(this.psComboBoxCLASSVENDA2);
            this.tabPage2.Controls.Add(this.psCheckBoxDENTRODOESTADO);
            this.tabPage2.Controls.Add(this.psLookup3);
            this.tabPage2.Controls.Add(this.psLookup2);
            this.tabPage2.Controls.Add(this.psComboBox8);
            this.tabPage2.Controls.Add(this.psLookup1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(470, 251);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Parâmetros";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // psCheckBoxCONTRIBUINTEICMS
            // 
            this.psCheckBoxCONTRIBUINTEICMS.Caption = "CONTRIBUINTEICMS";
            this.psCheckBoxCONTRIBUINTEICMS.Chave = true;
            this.psCheckBoxCONTRIBUINTEICMS.Checked = false;
            this.psCheckBoxCONTRIBUINTEICMS.DataField = "CONTRIBUINTEICMS";
            this.psCheckBoxCONTRIBUINTEICMS.Location = new System.Drawing.Point(162, 207);
            this.psCheckBoxCONTRIBUINTEICMS.Name = "psCheckBoxCONTRIBUINTEICMS";
            this.psCheckBoxCONTRIBUINTEICMS.Size = new System.Drawing.Size(250, 22);
            this.psCheckBoxCONTRIBUINTEICMS.TabIndex = 9;
            // 
            // psComboBoxCLASSVENDA2
            // 
            this.psComboBoxCLASSVENDA2.Caption = "CLASSVENDA2";
            this.psComboBoxCLASSVENDA2.Chave = true;
            this.psComboBoxCLASSVENDA2.DataField = "CLASSVENDA2";
            this.psComboBoxCLASSVENDA2.DataSource = null;
            this.psComboBoxCLASSVENDA2.DisplayMember = "";
            this.psComboBoxCLASSVENDA2.Location = new System.Drawing.Point(11, 192);
            this.psComboBoxCLASSVENDA2.Name = "psComboBoxCLASSVENDA2";
            this.psComboBoxCLASSVENDA2.SelectedIndex = -1;
            this.psComboBoxCLASSVENDA2.Size = new System.Drawing.Size(139, 37);
            this.psComboBoxCLASSVENDA2.TabIndex = 8;
            this.psComboBoxCLASSVENDA2.Value = null;
            this.psComboBoxCLASSVENDA2.ValueMember = "";
            this.psComboBoxCLASSVENDA2.SelectedValueChanged += new PS.Lib.WinForms.PSComboBox.SelectedValueChangedDelegate(this.psComboBoxCLASSVENDA2_SelectedValueChanged);
            // 
            // psCheckBoxDENTRODOESTADO
            // 
            this.psCheckBoxDENTRODOESTADO.Caption = "DENTRODOESTADO";
            this.psCheckBoxDENTRODOESTADO.Chave = true;
            this.psCheckBoxDENTRODOESTADO.Checked = false;
            this.psCheckBoxDENTRODOESTADO.DataField = "DENTRODOESTADO";
            this.psCheckBoxDENTRODOESTADO.Location = new System.Drawing.Point(162, 21);
            this.psCheckBoxDENTRODOESTADO.Name = "psCheckBoxDENTRODOESTADO";
            this.psCheckBoxDENTRODOESTADO.Size = new System.Drawing.Size(250, 22);
            this.psCheckBoxDENTRODOESTADO.TabIndex = 7;
            // 
            // psLookup3
            // 
            this.psLookup3.Caption = "IDREGRAIPI";
            this.psLookup3.Chave = true;
            this.psLookup3.DataField = "IDREGRAIPI";
            this.psLookup3.Description = "";
            this.psLookup3.DinamicTable = null;
            this.psLookup3.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup3.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup3.KeyField = "IDREGRA";
            this.psLookup3.Location = new System.Drawing.Point(11, 137);
            this.psLookup3.LookupField = "IDREGRA;DESCRICAO";
            this.psLookup3.LookupFieldResult = "IDREGRA;DESCRICAO";
            this.psLookup3.MaxLength = 32767;
            this.psLookup3.Name = "psLookup3";
            this.psLookup3.PSPart = null;
            this.psLookup3.Size = new System.Drawing.Size(401, 38);
            this.psLookup3.TabIndex = 6;
            this.psLookup3.ValorRetorno = null;
            // 
            // psLookup2
            // 
            this.psLookup2.Caption = "IDREGRAICMS";
            this.psLookup2.Chave = true;
            this.psLookup2.DataField = "IDREGRAICMS";
            this.psLookup2.Description = "";
            this.psLookup2.DinamicTable = null;
            this.psLookup2.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup2.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup2.KeyField = "IDREGRA";
            this.psLookup2.Location = new System.Drawing.Point(11, 93);
            this.psLookup2.LookupField = "IDREGRA;DESCRICAO";
            this.psLookup2.LookupFieldResult = "IDREGRA;DESCRICAO";
            this.psLookup2.MaxLength = 32767;
            this.psLookup2.Name = "psLookup2";
            this.psLookup2.PSPart = null;
            this.psLookup2.Size = new System.Drawing.Size(401, 38);
            this.psLookup2.TabIndex = 5;
            this.psLookup2.ValorRetorno = null;
            // 
            // psComboBox8
            // 
            this.psComboBox8.Caption = "TIPENTSAI";
            this.psComboBox8.Chave = true;
            this.psComboBox8.DataField = "TIPENTSAI";
            this.psComboBox8.DataSource = null;
            this.psComboBox8.DisplayMember = "";
            this.psComboBox8.Location = new System.Drawing.Point(11, 6);
            this.psComboBox8.Name = "psComboBox8";
            this.psComboBox8.SelectedIndex = -1;
            this.psComboBox8.Size = new System.Drawing.Size(139, 37);
            this.psComboBox8.TabIndex = 4;
            this.psComboBox8.Value = null;
            this.psComboBox8.ValueMember = "";
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODMENSAGEM";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODMENSAGEM";
            this.psLookup1.Description = "";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup1.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup1.KeyField = "CODMENSAGEM";
            this.psLookup1.Location = new System.Drawing.Point(11, 49);
            this.psLookup1.LookupField = "CODMENSAGEM;DESCRICAO";
            this.psLookup1.LookupFieldResult = "CODMENSAGEM;DESCRICAO";
            this.psLookup1.MaxLength = 32767;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(401, 38);
            this.psLookup1.TabIndex = 0;
            this.psLookup1.ValorRetorno = null;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.PSPartNaturezaRegraTributacao);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(882, 295);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Regra de Seleção";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // PSPartNaturezaRegraTributacao
            // 
            this.PSPartNaturezaRegraTributacao._atualiza = false;
            this.PSPartNaturezaRegraTributacao.aplicativo = null;
            this.PSPartNaturezaRegraTributacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PSPartNaturezaRegraTributacao.Location = new System.Drawing.Point(3, 3);
            this.PSPartNaturezaRegraTributacao.Name = "PSPartNaturezaRegraTributacao";
            this.PSPartNaturezaRegraTributacao.PermiteEditar = false;
            this.PSPartNaturezaRegraTributacao.PermiteExcluir = false;
            this.PSPartNaturezaRegraTributacao.PermiteIncluir = false;
            this.PSPartNaturezaRegraTributacao.psPart = null;
            this.PSPartNaturezaRegraTributacao.Size = new System.Drawing.Size(876, 289);
            this.PSPartNaturezaRegraTributacao.TabIndex = 19;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.PSPartNaturezaTributo);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(882, 295);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Tributos da Natureza";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // PSPartNaturezaTributo
            // 
            this.PSPartNaturezaTributo._atualiza = false;
            this.PSPartNaturezaTributo.aplicativo = null;
            this.PSPartNaturezaTributo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PSPartNaturezaTributo.Location = new System.Drawing.Point(3, 3);
            this.PSPartNaturezaTributo.Name = "PSPartNaturezaTributo";
            this.PSPartNaturezaTributo.PermiteEditar = false;
            this.PSPartNaturezaTributo.PermiteExcluir = false;
            this.PSPartNaturezaTributo.PermiteIncluir = false;
            this.PSPartNaturezaTributo.psPart = null;
            this.PSPartNaturezaTributo.Size = new System.Drawing.Size(876, 289);
            this.PSPartNaturezaTributo.TabIndex = 20;
            // 
            // psCheckBox8
            // 
            this.psCheckBox8.Caption = "ULTIMONIVEL";
            this.psCheckBox8.Chave = true;
            this.psCheckBox8.Checked = false;
            this.psCheckBox8.DataField = "ULTIMONIVEL";
            this.psCheckBox8.Location = new System.Drawing.Point(244, 21);
            this.psCheckBox8.Name = "psCheckBox8";
            this.psCheckBox8.Size = new System.Drawing.Size(150, 22);
            this.psCheckBox8.TabIndex = 5;
            // 
            // psMemoBox1
            // 
            this.psMemoBox1.Caption = "DESCRICAOINTERNA";
            this.psMemoBox1.Chave = true;
            this.psMemoBox1.DataField = "DESCRICAOINTERNA";
            this.psMemoBox1.Location = new System.Drawing.Point(11, 92);
            this.psMemoBox1.Name = "psMemoBox1";
            this.psMemoBox1.Size = new System.Drawing.Size(432, 114);
            this.psMemoBox1.TabIndex = 6;
            // 
            // PSPartNaturezaEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 393);
            this.Name = "PSPartNaturezaEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartNaturezaEdit";
            this.Load += new System.EventHandler(this.PSPartNaturezaEdit_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            //this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private PS.Lib.WinForms.PSCheckBox psCheckBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private PS.Lib.WinForms.PSLookup psLookup1;
        private System.Windows.Forms.TabPage tabPage3;
        private Lib.WinForms.PSBaseVisao PSPartNaturezaRegraTributacao;
        private System.Windows.Forms.TabPage tabPage4;
        private Lib.WinForms.PSBaseVisao PSPartNaturezaTributo;
        private Lib.WinForms.PSComboBox psComboBox8;
        private Lib.WinForms.PSLookup psLookup3;
        private Lib.WinForms.PSLookup psLookup2;
        private Lib.WinForms.PSCheckBox psCheckBox8;
        private Lib.WinForms.PSCheckBox psCheckBoxDENTRODOESTADO;
        private Lib.WinForms.PSComboBox psComboBoxCLASSVENDA2;
        private Lib.WinForms.PSCheckBox psCheckBoxCONTRIBUINTEICMS;
        private Lib.WinForms.PSMemoBox psMemoBox1;
    }
}