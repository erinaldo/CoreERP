namespace PS.Glb
{
    partial class PSPartTipoTransporteEdit
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
            this.psLookup1 = new PS.Lib.WinForms.PSLookup();
            this.psTextoBox2 = new PS.Lib.WinForms.PSTextoBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(493, 237);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psTextoBox2);
            this.tabPage1.Controls.Add(this.psLookup1);
            this.tabPage1.Controls.Add(this.psTextoBox1);
            this.tabPage1.Size = new System.Drawing.Size(485, 211);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.AutoIncremento = PS.Lib.Global.TypeAutoinc.AutoInc;
            this.psTextoBox1.Caption = "CODTIPOTRANSPORTE";
            this.psTextoBox1.DataField = "CODTIPOTRANSPORTE";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 37);
            this.psTextoBox1.MaxLength = 32767;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.ReadOnly = true;
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 0;
            // 
            // psLookup1
            // 
            this.psLookup1.Caption = "CODTRANSPORTADORA";
            this.psLookup1.Chave = true;
            this.psLookup1.DataField = "CODTRANSPORTADORA";
            this.psLookup1.Description = "";
            this.psLookup1.DinamicTable = null;
            this.psLookup1.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup1.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup1.KeyField = "CODTRANSPORTADORA";
            this.psLookup1.Location = new System.Drawing.Point(11, 80);
            this.psLookup1.LookupField = "CODTRANSPORTADORA;NOME;NOMEFANTASIA";
            this.psLookup1.LookupFieldResult = "CODTRANSPORTADORA;NOME";
            this.psLookup1.MaxLength = 32767;
            this.psLookup1.Name = "psLookup1";
            this.psLookup1.PSPart = null;
            this.psLookup1.Size = new System.Drawing.Size(448, 38);
            this.psLookup1.TabIndex = 1;
            this.psLookup1.ValorRetorno = null;
            // 
            // psTextoBox2
            // 
            this.psTextoBox2.Caption = "DESCRICAO";
            this.psTextoBox2.DataField = "DESCRICAO";
            this.psTextoBox2.Edita = true;
            this.psTextoBox2.Location = new System.Drawing.Point(11, 124);
            this.psTextoBox2.MaxLength = 32767;
            this.psTextoBox2.Name = "psTextoBox2";
            this.psTextoBox2.PasswordChar = '\0';
            this.psTextoBox2.Size = new System.Drawing.Size(448, 37);
            this.psTextoBox2.TabIndex = 2;
            // 
            // PSPartTipoTransporteEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 353);
            this.Name = "PSPartTipoTransporteEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartTipoTransporteEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Lib.WinForms.PSTextoBox psTextoBox1;
        private Lib.WinForms.PSTextoBox psTextoBox2;
        private Lib.WinForms.PSLookup psLookup1;
    }
}