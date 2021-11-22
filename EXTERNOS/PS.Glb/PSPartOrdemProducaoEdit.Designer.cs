namespace PS.Glb
{
    partial class PSPartOrdemProducaoEdit
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
            this.psDateBox1 = new PS.Lib.WinForms.PSDateBox();
            this.psComboBox1 = new PS.Lib.WinForms.PSComboBox();
            this.psLookup14 = new PS.Lib.WinForms.PSLookup();
            this.psTextoBox1 = new PS.Lib.WinForms.PSTextoBox();
            this.psTextoBox2 = new PS.Lib.WinForms.PSTextoBox();
            this.psMemoBox1 = new PS.Lib.WinForms.PSMemoBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.PSPartOrdemProducaoItem = new PS.Lib.WinForms.PSBaseVisao();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Size = new System.Drawing.Size(647, 348);
            this.tabControl1.Controls.SetChildIndex(this.tabPage2, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPage1, 0);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psMemoBox1);
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Controls.Add(this.psLookup14);
            this.tabPage1.Controls.Add(this.psComboBox1);
            this.tabPage1.Controls.Add(this.psDateBox1);
            this.tabPage1.Size = new System.Drawing.Size(639, 322);
            // 
            // psDateBox1
            // 
            this.psDateBox1.Caption = "DATAHORACRIACAO";
            this.psDateBox1.Chave = true;
            this.psDateBox1.DataField = "DATAHORACRIACAO";
            this.psDateBox1.Location = new System.Drawing.Point(418, 243);
            this.psDateBox1.Mascara = "00/00/0000 00:00";
            this.psDateBox1.MaxLength = 32767;
            this.psDateBox1.Name = "psDateBox1";
            this.psDateBox1.Size = new System.Drawing.Size(145, 37);
            this.psDateBox1.TabIndex = 3;
            this.psDateBox1.Value = new System.DateTime(2016, 2, 20, 12, 48, 43, 671);
            // 
            // psComboBox1
            // 
            this.psComboBox1.Caption = "CODSTATUS";
            this.psComboBox1.Chave = true;
            this.psComboBox1.DataField = "CODSTATUS";
            this.psComboBox1.DataSource = null;
            this.psComboBox1.DisplayMember = "";
            this.psComboBox1.Location = new System.Drawing.Point(161, 6);
            this.psComboBox1.Name = "psComboBox1";
            this.psComboBox1.SelectedIndex = -1;
            this.psComboBox1.Size = new System.Drawing.Size(144, 37);
            this.psComboBox1.TabIndex = 4;
            this.psComboBox1.Value = null;
            this.psComboBox1.ValueMember = "";
            // 
            // psLookup14
            // 
            this.psLookup14.Caption = "CODUSUARIOCRIACAO";
            this.psLookup14.Chave = true;
            this.psLookup14.DataField = "CODUSUARIOCRIACAO";
            this.psLookup14.Description = "";
            this.psLookup14.DinamicTable = null;
            this.psLookup14.KeyField = "CODUSUARIO";
            this.psLookup14.Location = new System.Drawing.Point(11, 242);
            this.psLookup14.LookupField = "CODUSUARIO;NOME";
            this.psLookup14.LookupFieldResult = "CODUSUARIO;NOME";
            this.psLookup14.MaxLength = 20;
            this.psLookup14.Name = "psLookup14";
            this.psLookup14.PSPart = null;
            this.psLookup14.Size = new System.Drawing.Size(401, 38);
            this.psLookup14.TabIndex = 2;
            this.psLookup14.ValorRetorno = null;
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODORDEM";
            this.psTextoBox1.DataField = "CODORDEM";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 32767;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(144, 37);
            this.psTextoBox1.TabIndex = 5;
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "DESCRICAO";
            this.psTextoBox2.DataField = "DESCRICAO";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.Location = new System.Drawing.Point(11, 49);
            this.psTextoBox2.MaxLength = 32767;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(396, 37);
            this.psTextoBox2.TabIndex = 6;
            // 
            // psMemoBox1
            // 
            this.psMemoBox1.Caption = "OBSERVACAO";
            this.psMemoBox1.Chave = true;
            this.psMemoBox1.DataField = "OBSERVACAO";
            this.psMemoBox1.Location = new System.Drawing.Point(11, 92);
            this.psMemoBox1.Name = "psMemoBox1";
            this.psMemoBox1.Size = new System.Drawing.Size(552, 141);
            this.psMemoBox1.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.PSPartOrdemProducaoItem);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(639, 322);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Itens da Ordem de Produção";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // PSPartOrdemProducaoItem
            // 
            this.PSPartOrdemProducaoItem._atualiza = false;
            this.PSPartOrdemProducaoItem.aplicativo = null;
            this.PSPartOrdemProducaoItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PSPartOrdemProducaoItem.Location = new System.Drawing.Point(3, 3);
            this.PSPartOrdemProducaoItem.Name = "PSPartOrdemProducaoItem";
            this.PSPartOrdemProducaoItem.PermiteEditar = false;
            this.PSPartOrdemProducaoItem.PermiteExcluir = false;
            this.PSPartOrdemProducaoItem.PermiteIncluir = false;
            this.PSPartOrdemProducaoItem.psPart = null;
            this.PSPartOrdemProducaoItem.Size = new System.Drawing.Size(633, 316);
            this.PSPartOrdemProducaoItem.TabIndex = 1;
            // 
            // PSPartOrdemProducaoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 464);
            this.Name = "PSPartOrdemProducaoEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartOrdemProducaoEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSLookup psLookup14;
        private PS.Lib.WinForms.PSComboBox psComboBox1;
        private PS.Lib.WinForms.PSDateBox psDateBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox2;
        private PS.Lib.WinForms.PSMemoBox psMemoBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private PS.Lib.WinForms.PSBaseVisao PSPartOrdemProducaoItem;

    }
}