using DevExpress.XtraEditors;

namespace ERP
{
    partial class MDILogin
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
            this.components = new System.ComponentModel.Container();
            DevExpress.Utils.SuperToolTip superToolTip7 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem7 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip8 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem8 = new DevExpress.Utils.ToolTipTitleItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDILogin));
            this.cb_Alias = new System.Windows.Forms.ComboBox();
            this.tb_Usuario = new DevExpress.XtraEditors.TextEdit();
            this.tb_Senha = new DevExpress.XtraEditors.TextEdit();
            this.panel_Login = new System.Windows.Forms.Panel();
            this.panel_Sair = new System.Windows.Forms.Panel();
            this.panel_Suporte = new System.Windows.Forms.Panel();
            this.panel_Alias = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.lblVersao = new System.Windows.Forms.Label();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tb_Usuario.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Senha.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cb_Alias
            // 
            this.cb_Alias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Alias.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Alias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(53)))));
            this.cb_Alias.FormattingEnabled = true;
            this.cb_Alias.Location = new System.Drawing.Point(46, 242);
            this.cb_Alias.Name = "cb_Alias";
            this.cb_Alias.Size = new System.Drawing.Size(224, 22);
            this.cb_Alias.TabIndex = 2;
            this.cb_Alias.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cb_Alias_KeyDown);
            // 
            // tb_Usuario
            // 
            this.tb_Usuario.EditValue = "";
            this.tb_Usuario.Location = new System.Drawing.Point(46, 153);
            this.tb_Usuario.Name = "tb_Usuario";
            this.tb_Usuario.Properties.Appearance.BackColor = System.Drawing.SystemColors.Menu;
            this.tb_Usuario.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Usuario.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.tb_Usuario.Properties.Appearance.Options.UseBackColor = true;
            this.tb_Usuario.Properties.Appearance.Options.UseFont = true;
            this.tb_Usuario.Properties.Appearance.Options.UseForeColor = true;
            this.tb_Usuario.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tb_Usuario.Properties.NullValuePrompt = "Usuário";
            this.tb_Usuario.Properties.NullValuePromptShowForEmptyValue = true;
            this.tb_Usuario.Size = new System.Drawing.Size(262, 24);
            this.tb_Usuario.TabIndex = 0;
            this.tb_Usuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_Usuario_KeyDown);
            // 
            // tb_Senha
            // 
            this.tb_Senha.Location = new System.Drawing.Point(46, 196);
            this.tb_Senha.Name = "tb_Senha";
            this.tb_Senha.Properties.Appearance.BackColor = System.Drawing.SystemColors.Menu;
            this.tb_Senha.Properties.Appearance.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Senha.Properties.Appearance.Options.UseBackColor = true;
            this.tb_Senha.Properties.Appearance.Options.UseFont = true;
            this.tb_Senha.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tb_Senha.Properties.NullValuePrompt = "Senha";
            this.tb_Senha.Properties.NullValuePromptShowForEmptyValue = true;
            this.tb_Senha.Properties.PasswordChar = '*';
            this.tb_Senha.Size = new System.Drawing.Size(262, 24);
            this.tb_Senha.TabIndex = 1;
            this.tb_Senha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_Senha_KeyDown);
            // 
            // panel_Login
            // 
            this.panel_Login.BackColor = System.Drawing.Color.Transparent;
            this.panel_Login.Location = new System.Drawing.Point(46, 282);
            this.panel_Login.Name = "panel_Login";
            this.panel_Login.Size = new System.Drawing.Size(263, 31);
            this.panel_Login.TabIndex = 4;
            this.panel_Login.Click += new System.EventHandler(this.panel_Login_Click);
            this.panel_Login.MouseEnter += new System.EventHandler(this.panel_Login_MouseEnter);
            this.panel_Login.MouseLeave += new System.EventHandler(this.panel_Login_MouseLeave);
            // 
            // panel_Sair
            // 
            this.panel_Sair.BackColor = System.Drawing.Color.Transparent;
            this.panel_Sair.Location = new System.Drawing.Point(780, 0);
            this.panel_Sair.Name = "panel_Sair";
            this.panel_Sair.Size = new System.Drawing.Size(21, 17);
            toolTipTitleItem7.Text = "Fechar";
            superToolTip7.Items.Add(toolTipTitleItem7);
            this.toolTipController1.SetSuperTip(this.panel_Sair, superToolTip7);
            this.panel_Sair.TabIndex = 9;
            this.panel_Sair.Click += new System.EventHandler(this.panel_Sair_Click);
            this.panel_Sair.MouseEnter += new System.EventHandler(this.panel_Sair_MouseEnter);
            this.panel_Sair.MouseLeave += new System.EventHandler(this.panel_Sair_MouseLeave);
            // 
            // panel_Suporte
            // 
            this.panel_Suporte.BackColor = System.Drawing.Color.Transparent;
            this.panel_Suporte.Location = new System.Drawing.Point(780, 416);
            this.panel_Suporte.Name = "panel_Suporte";
            this.panel_Suporte.Size = new System.Drawing.Size(21, 19);
            toolTipTitleItem8.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            toolTipTitleItem8.Text = "Acesso ao portal!";
            superToolTip8.Items.Add(toolTipTitleItem8);
            this.toolTipController1.SetSuperTip(this.panel_Suporte, superToolTip8);
            this.panel_Suporte.TabIndex = 10;
            this.panel_Suporte.Click += new System.EventHandler(this.panel_Suporte_Click);
            this.panel_Suporte.MouseEnter += new System.EventHandler(this.panel_Suporte_MouseEnter);
            this.panel_Suporte.MouseLeave += new System.EventHandler(this.panel_Suporte_MouseLeave);
            // 
            // panel_Alias
            // 
            this.panel_Alias.BackColor = System.Drawing.Color.Transparent;
            this.panel_Alias.Location = new System.Drawing.Point(284, 241);
            this.panel_Alias.Name = "panel_Alias";
            this.panel_Alias.Size = new System.Drawing.Size(24, 23);
            this.panel_Alias.TabIndex = 3;
            this.panel_Alias.Click += new System.EventHandler(this.panel_Alias_Click);
            this.panel_Alias.MouseEnter += new System.EventHandler(this.panel_Alias_MouseEnter);
            this.panel_Alias.MouseLeave += new System.EventHandler(this.panel_Alias_MouseLeave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label5.Location = new System.Drawing.Point(84, 332);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 16);
            this.label5.TabIndex = 31;
            this.label5.Text = "Versão:";
            // 
            // lblVersao
            // 
            this.lblVersao.AutoSize = true;
            this.lblVersao.BackColor = System.Drawing.Color.Transparent;
            this.lblVersao.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersao.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblVersao.Location = new System.Drawing.Point(150, 332);
            this.lblVersao.Name = "lblVersao";
            this.lblVersao.Size = new System.Drawing.Size(123, 16);
            this.lblVersao.TabIndex = 32;
            this.lblVersao.Text = "dd/mm/yyyy.nnn";
            // 
            // MDILogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(800, 437);
            this.Controls.Add(this.lblVersao);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel_Alias);
            this.Controls.Add(this.panel_Suporte);
            this.Controls.Add(this.panel_Sair);
            this.Controls.Add(this.panel_Login);
            this.Controls.Add(this.tb_Senha);
            this.Controls.Add(this.tb_Usuario);
            this.Controls.Add(this.cb_Alias);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MDILogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MDILogin";
            this.Load += new System.EventHandler(this.MDILogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tb_Usuario.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tb_Senha.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cb_Alias;
        private TextEdit tb_Usuario;
        private TextEdit tb_Senha;
        private System.Windows.Forms.Panel panel_Login;
        private System.Windows.Forms.Panel panel_Sair;
        private System.Windows.Forms.Panel panel_Suporte;
        private System.Windows.Forms.Panel panel_Alias;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblVersao;
        private DevExpress.Utils.ToolTipController toolTipController1;
    }
}