
namespace PS.Glb.New.Cadastros
{
    partial class frmCadastroAtendimento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCadastroAtendimento));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.teHoraRetorno = new DevExpress.XtraEditors.TimeEdit();
            this.cmbInLoco = new System.Windows.Forms.ComboBox();
            this.tbIdentificador = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.dteDataRetorno = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lpUsuario = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.tbHistorico = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.lpContato = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.lpUnidade = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.tbUsuarioAtendimento = new DevExpress.XtraEditors.TextEdit();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.dteDataAtendimento = new DevExpress.XtraEditors.DateEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalvar = new DevExpress.XtraEditors.SimpleButton();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teHoraRetorno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIdentificador.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDataRetorno.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDataRetorno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lpUsuario.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHistorico.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lpContato.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lpUnidade.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbUsuarioAtendimento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDataAtendimento.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDataAtendimento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCancelar);
            this.splitContainer1.Panel2.Controls.Add(this.btnOK);
            this.splitContainer1.Panel2.Controls.Add(this.btnSalvar);
            this.splitContainer1.Size = new System.Drawing.Size(479, 331);
            this.splitContainer1.SplitterDistance = 293;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(479, 293);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.teHoraRetorno);
            this.tabPage1.Controls.Add(this.cmbInLoco);
            this.tabPage1.Controls.Add(this.tbIdentificador);
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.dteDataRetorno);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.lpUsuario);
            this.tabPage1.Controls.Add(this.labelControl7);
            this.tabPage1.Controls.Add(this.tbHistorico);
            this.tabPage1.Controls.Add(this.labelControl17);
            this.tabPage1.Controls.Add(this.lpContato);
            this.tabPage1.Controls.Add(this.labelControl6);
            this.tabPage1.Controls.Add(this.lpUnidade);
            this.tabPage1.Controls.Add(this.labelControl16);
            this.tabPage1.Controls.Add(this.tbUsuarioAtendimento);
            this.tabPage1.Controls.Add(this.labelControl15);
            this.tabPage1.Controls.Add(this.dteDataAtendimento);
            this.tabPage1.Controls.Add(this.labelControl8);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(471, 267);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Atendimento";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // teHoraRetorno
            // 
            this.teHoraRetorno.EditValue = new System.DateTime(2021, 6, 25, 0, 0, 0, 0);
            this.teHoraRetorno.Location = new System.Drawing.Point(293, 236);
            this.teHoraRetorno.Name = "teHoraRetorno";
            this.teHoraRetorno.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.teHoraRetorno.Properties.Mask.EditMask = "t";
            this.teHoraRetorno.Size = new System.Drawing.Size(75, 20);
            this.teHoraRetorno.TabIndex = 17;
            // 
            // cmbInLoco
            // 
            this.cmbInLoco.FormattingEnabled = true;
            this.cmbInLoco.Location = new System.Drawing.Point(374, 235);
            this.cmbInLoco.Name = "cmbInLoco";
            this.cmbInLoco.Size = new System.Drawing.Size(84, 21);
            this.cmbInLoco.TabIndex = 19;
            // 
            // tbIdentificador
            // 
            this.tbIdentificador.Enabled = false;
            this.tbIdentificador.Location = new System.Drawing.Point(8, 25);
            this.tbIdentificador.Name = "tbIdentificador";
            this.tbIdentificador.Size = new System.Drawing.Size(100, 20);
            this.tbIdentificador.TabIndex = 1;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(293, 217);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(65, 13);
            this.labelControl4.TabIndex = 16;
            this.labelControl4.Text = "Hora Retorno";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(180, 217);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(65, 13);
            this.labelControl3.TabIndex = 14;
            this.labelControl3.Text = "Data Retorno";
            // 
            // dteDataRetorno
            // 
            this.dteDataRetorno.EditValue = null;
            this.dteDataRetorno.Location = new System.Drawing.Point(180, 236);
            this.dteDataRetorno.Name = "dteDataRetorno";
            this.dteDataRetorno.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDataRetorno.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDataRetorno.Size = new System.Drawing.Size(107, 20);
            this.dteDataRetorno.TabIndex = 15;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 217);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(54, 13);
            this.labelControl2.TabIndex = 12;
            this.labelControl2.Text = "Atendente ";
            // 
            // lpUsuario
            // 
            this.lpUsuario.Location = new System.Drawing.Point(8, 236);
            this.lpUsuario.Name = "lpUsuario";
            this.lpUsuario.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lpUsuario.Size = new System.Drawing.Size(166, 20);
            this.lpUsuario.TabIndex = 13;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(8, 96);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(90, 13);
            this.labelControl7.TabIndex = 10;
            this.labelControl7.Text = "Histórico Comercial";
            // 
            // tbHistorico
            // 
            this.tbHistorico.Location = new System.Drawing.Point(8, 115);
            this.tbHistorico.Name = "tbHistorico";
            this.tbHistorico.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tbHistorico.Size = new System.Drawing.Size(450, 96);
            this.tbHistorico.TabIndex = 11;
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(235, 51);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(39, 13);
            this.labelControl17.TabIndex = 8;
            this.labelControl17.Text = "Contato";
            // 
            // lpContato
            // 
            this.lpContato.Location = new System.Drawing.Point(235, 70);
            this.lpContato.Name = "lpContato";
            this.lpContato.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lpContato.Size = new System.Drawing.Size(223, 20);
            this.lpContato.TabIndex = 9;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(8, 51);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(39, 13);
            this.labelControl6.TabIndex = 6;
            this.labelControl6.Text = "Unidade";
            // 
            // lpUnidade
            // 
            this.lpUnidade.Location = new System.Drawing.Point(6, 70);
            this.lpUnidade.Name = "lpUnidade";
            this.lpUnidade.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lpUnidade.Size = new System.Drawing.Size(223, 20);
            this.lpUnidade.TabIndex = 7;
            // 
            // labelControl16
            // 
            this.labelControl16.Location = new System.Drawing.Point(114, 6);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(100, 13);
            this.labelControl16.TabIndex = 2;
            this.labelControl16.Text = "Usuário Atendimento";
            // 
            // tbUsuarioAtendimento
            // 
            this.tbUsuarioAtendimento.Enabled = false;
            this.tbUsuarioAtendimento.Location = new System.Drawing.Point(114, 25);
            this.tbUsuarioAtendimento.Name = "tbUsuarioAtendimento";
            this.tbUsuarioAtendimento.Size = new System.Drawing.Size(115, 20);
            this.tbUsuarioAtendimento.TabIndex = 3;
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(235, 6);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(87, 13);
            this.labelControl15.TabIndex = 4;
            this.labelControl15.Text = "Data Atendimento";
            // 
            // dteDataAtendimento
            // 
            this.dteDataAtendimento.EditValue = null;
            this.dteDataAtendimento.Enabled = false;
            this.dteDataAtendimento.Location = new System.Drawing.Point(235, 25);
            this.dteDataAtendimento.Name = "dteDataAtendimento";
            this.dteDataAtendimento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDataAtendimento.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteDataAtendimento.Size = new System.Drawing.Size(107, 20);
            this.dteDataAtendimento.TabIndex = 5;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(374, 217);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(32, 13);
            this.labelControl8.TabIndex = 18;
            this.labelControl8.Text = "In loco";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(61, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Identificador";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(387, 2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(306, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(225, 3);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(75, 23);
            this.btnSalvar.TabIndex = 0;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // frmCadastroAtendimento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 331);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCadastroAtendimento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Atendimento";
            this.Load += new System.EventHandler(this.frmCadastroAtendimento_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.teHoraRetorno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIdentificador.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDataRetorno.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDataRetorno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lpUsuario.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbHistorico.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lpContato.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lpUnidade.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbUsuarioAtendimento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDataAtendimento.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteDataAtendimento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnSalvar;
        private DevExpress.XtraEditors.DateEdit dteDataAtendimento;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.TextEdit tbUsuarioAtendimento;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.LookUpEdit lpContato;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LookUpEdit lpUnidade;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.MemoEdit tbHistorico;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit lpUsuario;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit dteDataRetorno;
        private DevExpress.XtraEditors.TextEdit tbIdentificador;
        private System.Windows.Forms.ComboBox cmbInLoco;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.TimeEdit teHoraRetorno;
    }
}