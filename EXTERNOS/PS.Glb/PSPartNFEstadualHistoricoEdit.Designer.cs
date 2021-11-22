namespace PS.Glb
{
    partial class PSPartNFEstadualHistoricoEdit
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
            this.psDateBox1 = new PS.Lib.WinForms.PSDateBox();
            this.psLookup14 = new PS.Lib.WinForms.PSLookup();
            this.psMemoBox1 = new PS.Lib.WinForms.PSMemoBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(647, 334);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psMemoBox1);
            this.tabPage1.Controls.Add(this.psLookup14);
            this.tabPage1.Controls.Add(this.psDateBox1);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(639, 308);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "IDHISTORICO";
            this.psTextoBox1.DataField = "IDHISTORICO";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 32767;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 0;
            // 
            // psDateBox1
            // 
            this.psDateBox1.Caption = "DATA";
            this.psDateBox1.Chave = true;
            this.psDateBox1.DataField = "DATA";
            this.psDateBox1.Location = new System.Drawing.Point(11, 49);
            this.psDateBox1.Mascara = "00/00/0000 00:00";
            this.psDateBox1.MaxLength = 32767;
            this.psDateBox1.Name = "psDateBox1";
            this.psDateBox1.Size = new System.Drawing.Size(146, 37);
            this.psDateBox1.TabIndex = 1;
            this.psDateBox1.Value = new System.DateTime(2015, 11, 3, 15, 10, 25, 51);
            // 
            // psLookup14
            // 
            this.psLookup14.Caption = "CODUSUARIO";
            this.psLookup14.Chave = true;
            this.psLookup14.DataField = "CODUSUARIO";
            this.psLookup14.Description = "";
            this.psLookup14.DinamicTable = null;
            this.psLookup14.KeyField = "CODUSUARIO";
            this.psLookup14.Location = new System.Drawing.Point(11, 92);
            this.psLookup14.LookupField = "CODUSUARIO;NOME";
            this.psLookup14.LookupFieldResult = "CODUSUARIO;NOME";
            this.psLookup14.MaxLength = 20;
            this.psLookup14.Name = "psLookup14";
            this.psLookup14.PSPart = null;
            this.psLookup14.Size = new System.Drawing.Size(401, 38);
            this.psLookup14.TabIndex = 3;
            this.psLookup14.ValorRetorno = null;
            // 
            // psMemoBox1
            // 
            this.psMemoBox1.Caption = "OBSERVACAO";
            this.psMemoBox1.Chave = true;
            this.psMemoBox1.DataField = "OBSERVACAO";
            this.psMemoBox1.Location = new System.Drawing.Point(12, 136);
            this.psMemoBox1.Name = "psMemoBox1";
            this.psMemoBox1.Size = new System.Drawing.Size(619, 142);
            this.psMemoBox1.TabIndex = 4;
            // 
            // PSPartNFEstadualHistoricoEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 450);
            this.Name = "PSPartNFEstadualHistoricoEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartNFEstadualHistoricoEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Lib.WinForms.PSTextoBox psTextoBox1;
        private Lib.WinForms.PSDateBox psDateBox1;
        private Lib.WinForms.PSMemoBox psMemoBox1;
        private Lib.WinForms.PSLookup psLookup14;
    }
}