namespace PS.Glb
{
    partial class PSPartPerfilEdit
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
            this.psTextoBox3 = new PS.Lib.WinForms.PSTextoBox();
            this.psCheckBox1 = new PS.Lib.WinForms.PSCheckBox();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.PSPartAcessoMenu = new PS.Lib.WinForms.PSBaseVisao();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.PSPartUsuarioPerfil = new PS.Lib.WinForms.PSBaseVisao();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.PSPartAcessoTipOper = new PS.Lib.WinForms.PSBaseVisao();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.TabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Size = new System.Drawing.Size(647, 258);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Controls.SetChildIndex(this.tabPage4, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPage3, 0);
            this.tabControl1.Controls.SetChildIndex(this.TabPage2, 0);
            this.tabControl1.Controls.SetChildIndex(this.tabPage1, 0);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.psCheckBox1);
            this.tabPage1.Controls.Add(this.psTextoBox3);
            this.tabPage1.Controls.Add(this.psTextoBox1);
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
            this.buttonSALVAR.Location = new System.Drawing.Point(163, 19);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(244, 19);
            // 
            // buttonCANCELAR
            // 
            this.buttonCANCELAR.Location = new System.Drawing.Point(325, 19);
            // 
            // psTextoBox1
            // 
            this.psTextoBox1.Caption = "CODPERFIL";
            this.psTextoBox1.DataField = "CODPERFIL";
            this.psTextoBox1.Edita = true;
            this.psTextoBox1.Location = new System.Drawing.Point(11, 6);
            this.psTextoBox1.MaxLength = 25;
            this.psTextoBox1.Name = "psTextoBox1";
            this.psTextoBox1.PasswordChar = '\0';
            this.psTextoBox1.Size = new System.Drawing.Size(145, 37);
            this.psTextoBox1.TabIndex = 0;
            // 
            // psTextoBox3
            // 
            this.psTextoBox3.Caption = "NOME";
            this.psTextoBox3.DataField = "NOME";
            this.psTextoBox3.Edita = true;
            this.psTextoBox3.Location = new System.Drawing.Point(8, 49);
            this.psTextoBox3.MaxLength = 255;
            this.psTextoBox3.Name = "psTextoBox3";
            this.psTextoBox3.PasswordChar = '\0';
            this.psTextoBox3.Size = new System.Drawing.Size(401, 37);
            this.psTextoBox3.TabIndex = 2;
            // 
            // psCheckBox1
            // 
            this.psCheckBox1.Caption = "ATIVO";
            this.psCheckBox1.Chave = true;
            this.psCheckBox1.Checked = false;
            this.psCheckBox1.DataField = "ATIVO";
            this.psCheckBox1.Location = new System.Drawing.Point(313, 21);
            this.psCheckBox1.Name = "psCheckBox1";
            this.psCheckBox1.Size = new System.Drawing.Size(96, 22);
            this.psCheckBox1.TabIndex = 3;
            // 
            // TabPage2
            // 
            this.TabPage2.Controls.Add(this.PSPartAcessoMenu);
            this.TabPage2.Location = new System.Drawing.Point(4, 22);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(639, 232);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Acesso ao Menu";
            this.TabPage2.UseVisualStyleBackColor = true;
            // 
            // PSPartAcessoMenu
            // 
            this.PSPartAcessoMenu._atualiza = false;
            this.PSPartAcessoMenu.aplicativo = null;
            this.PSPartAcessoMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PSPartAcessoMenu.Location = new System.Drawing.Point(3, 3);
            this.PSPartAcessoMenu.Name = "PSPartAcessoMenu";
            this.PSPartAcessoMenu.PermiteEditar = false;
            this.PSPartAcessoMenu.PermiteExcluir = false;
            this.PSPartAcessoMenu.PermiteIncluir = false;
            this.PSPartAcessoMenu.psPart = null;
            this.PSPartAcessoMenu.Size = new System.Drawing.Size(633, 226);
            this.PSPartAcessoMenu.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.PSPartUsuarioPerfil);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(639, 232);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Usuário/Perfil";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // PSPartUsuarioPerfil
            // 
            this.PSPartUsuarioPerfil._atualiza = false;
            this.PSPartUsuarioPerfil.aplicativo = null;
            this.PSPartUsuarioPerfil.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PSPartUsuarioPerfil.Location = new System.Drawing.Point(3, 3);
            this.PSPartUsuarioPerfil.Name = "PSPartUsuarioPerfil";
            this.PSPartUsuarioPerfil.PermiteEditar = false;
            this.PSPartUsuarioPerfil.PermiteExcluir = false;
            this.PSPartUsuarioPerfil.PermiteIncluir = false;
            this.PSPartUsuarioPerfil.psPart = null;
            this.PSPartUsuarioPerfil.Size = new System.Drawing.Size(633, 226);
            this.PSPartUsuarioPerfil.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.PSPartAcessoTipOper);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(639, 232);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Tipo de Operação/Perfil";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // PSPartAcessoTipOper
            // 
            this.PSPartAcessoTipOper._atualiza = false;
            this.PSPartAcessoTipOper.aplicativo = null;
            this.PSPartAcessoTipOper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PSPartAcessoTipOper.Location = new System.Drawing.Point(3, 3);
            this.PSPartAcessoTipOper.Name = "PSPartAcessoTipOper";
            this.PSPartAcessoTipOper.PermiteEditar = false;
            this.PSPartAcessoTipOper.PermiteExcluir = false;
            this.PSPartAcessoTipOper.PermiteIncluir = false;
            this.PSPartAcessoTipOper.psPart = null;
            this.PSPartAcessoTipOper.Size = new System.Drawing.Size(633, 226);
            this.PSPartAcessoTipOper.TabIndex = 0;
            this.PSPartAcessoTipOper.Load += new System.EventHandler(this.PSPartAcessoTipOper_Load);
            // 
            // PSPartPerfilEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 374);
            this.Name = "PSPartPerfilEdit";
            this.PermiteExcluir = true;
            this.PermiteIncluir = true;
            this.Text = "PSPartPerfilEditcs";
            this.Load += new System.EventHandler(this.PSPartPerfilEdit_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PS.Lib.WinForms.PSTextoBox psTextoBox1;
        private PS.Lib.WinForms.PSTextoBox psTextoBox3;
        private PS.Lib.WinForms.PSCheckBox psCheckBox1;
        private System.Windows.Forms.TabPage TabPage2;
        private PS.Lib.WinForms.PSBaseVisao PSPartAcessoMenu;
        private System.Windows.Forms.TabPage tabPage3;
        private PS.Lib.WinForms.PSBaseVisao PSPartUsuarioPerfil;
        private System.Windows.Forms.TabPage tabPage4;
        private PS.Lib.WinForms.PSBaseVisao PSPartAcessoTipOper;
    }
}