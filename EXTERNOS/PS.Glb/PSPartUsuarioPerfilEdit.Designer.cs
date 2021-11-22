namespace PS.Glb
{
    partial class PSPartUsuarioPerfilEdit
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
            this.psLookup2 = new PS.Lib.WinForms.PSLookup();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(647, 258);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psLookup2);
            this.tabPage1.Size = new System.Drawing.Size(639, 232);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 320);
            this.panel2.Size = new System.Drawing.Size(647, 54);
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(647, 30);
            // 
            // buttonSALVAR
            // 
            this.buttonSALVAR.Location = new System.Drawing.Point(379, 19);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(470, 19);
            // 
            // buttonCANCELAR
            // 
            this.buttonCANCELAR.Location = new System.Drawing.Point(560, 19);
            // 
            // psLookup2
            // 
            this.psLookup2.Caption = "CODUSUARIO";
            this.psLookup2.Chave = true;
            this.psLookup2.DataField = "CODUSUARIO";
            this.psLookup2.Description = "";
            this.psLookup2.DinamicTable = null;
            this.psLookup2.FormCadastro = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup2.FormConsulta = PS.Lib.WinForms.Framework.PSFramework;
            this.psLookup2.KeyField = "CODUSUARIO";
            this.psLookup2.Location = new System.Drawing.Point(11, 9);
            this.psLookup2.LookupField = "CODUSUARIO;NOME";
            this.psLookup2.LookupFieldResult = "CODUSUARIO;NOME";
            this.psLookup2.MaxLength = 25;
            this.psLookup2.Name = "psLookup2";
            this.psLookup2.PSPart = null;
            this.psLookup2.Size = new System.Drawing.Size(401, 38);
            this.psLookup2.TabIndex = 1;
            this.psLookup2.ValorRetorno = null;
            // 
            // PSPartUsuarioPerfilEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartUsuarioPerfilEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartUsuarioPerfilEditcs";
            this.Load += new System.EventHandler(this.PSPartUsuarioPerfilEdit_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSLookup psLookup2;
    }
}