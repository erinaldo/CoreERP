namespace ERP
{
    partial class FormAlteraSenha
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAlteraSenha));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtAtual = new DevExpress.XtraEditors.TextEdit();
            this.txtNova = new DevExpress.XtraEditors.TextEdit();
            this.txtConfirmacaoNova = new DevExpress.XtraEditors.TextEdit();
            this.btnAlterar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAtual.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNova.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmacaoNova.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(290, 261);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnCancelar);
            this.tabPage1.Controls.Add(this.btnAlterar);
            this.tabPage1.Controls.Add(this.txtConfirmacaoNova);
            this.tabPage1.Controls.Add(this.txtNova);
            this.tabPage1.Controls.Add(this.txtAtual);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(282, 235);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(62, 32);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Senha Atual";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(62, 77);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(58, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Nova Senha";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(62, 122);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(58, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Nova Senha";
            // 
            // txtAtual
            // 
            this.txtAtual.Location = new System.Drawing.Point(62, 51);
            this.txtAtual.Name = "txtAtual";
            this.txtAtual.Properties.PasswordChar = '*';
            this.txtAtual.Properties.UseSystemPasswordChar = true;
            this.txtAtual.Size = new System.Drawing.Size(157, 20);
            this.txtAtual.TabIndex = 3;
            // 
            // txtNova
            // 
            this.txtNova.Location = new System.Drawing.Point(62, 96);
            this.txtNova.Name = "txtNova";
            this.txtNova.Properties.PasswordChar = '*';
            this.txtNova.Properties.UseSystemPasswordChar = true;
            this.txtNova.Size = new System.Drawing.Size(157, 20);
            this.txtNova.TabIndex = 4;
            // 
            // txtConfirmacaoNova
            // 
            this.txtConfirmacaoNova.Location = new System.Drawing.Point(62, 141);
            this.txtConfirmacaoNova.Name = "txtConfirmacaoNova";
            this.txtConfirmacaoNova.Properties.PasswordChar = '*';
            this.txtConfirmacaoNova.Properties.UseSystemPasswordChar = true;
            this.txtConfirmacaoNova.Size = new System.Drawing.Size(157, 20);
            this.txtConfirmacaoNova.TabIndex = 5;
            // 
            // btnAlterar
            // 
            this.btnAlterar.Location = new System.Drawing.Point(62, 168);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(75, 23);
            this.btnAlterar.TabIndex = 6;
            this.btnAlterar.Text = "Alterar";
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(144, 167);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FormAlteraSenha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 261);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormAlteraSenha";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alteração de Senha";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAtual.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNova.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtConfirmacaoNova.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.SimpleButton btnAlterar;
        private DevExpress.XtraEditors.TextEdit txtConfirmacaoNova;
        private DevExpress.XtraEditors.TextEdit txtNova;
        private DevExpress.XtraEditors.TextEdit txtAtual;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}