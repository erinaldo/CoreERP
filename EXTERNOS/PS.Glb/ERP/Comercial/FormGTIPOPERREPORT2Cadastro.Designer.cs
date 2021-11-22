namespace PS.Glb.ERP.Comercial
{
    partial class FormGTIPOPERREPORT2Cadastro
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
            AppLib.Windows.Query query1 = new AppLib.Windows.Query();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.campoLookup1 = new AppLib.Windows.CampoLookup();
            this.campoTexto1 = new AppLib.Windows.CampoTexto();
            this.campoInteiro1 = new AppLib.Windows.CampoInteiro();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(665, 165);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.campoLookup1);
            this.tabPage2.Controls.Add(this.campoTexto1);
            this.tabPage2.Controls.Add(this.campoInteiro1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(657, 139);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Identificação";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Relatório";
            // 
            // campoLookup1
            // 
            this.campoLookup1.Campo = "IDREPORT";
            this.campoLookup1.ColunaCodigo = "IDREPORT";
            this.campoLookup1.ColunaDescricao = "NOME";
            this.campoLookup1.ColunaIdentificador = null;
            this.campoLookup1.ColunaTabela = "ZREPORT";
            this.campoLookup1.Conexao = "Start";
            this.campoLookup1.Default = null;
            this.campoLookup1.Edita = true;
            this.campoLookup1.EditaLookup = false;
            this.campoLookup1.Location = new System.Drawing.Point(20, 39);
            this.campoLookup1.MaximoCaracteres = null;
            this.campoLookup1.Name = "campoLookup1";
            this.campoLookup1.NomeGrid = "";
            this.campoLookup1.Query = 0;
            this.campoLookup1.Size = new System.Drawing.Size(581, 21);
            this.campoLookup1.Tabela = "GTIPOPERREPORT2";
            this.campoLookup1.TabIndex = 0;
            this.campoLookup1.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookup1_SetFormConsulta);
            // 
            // campoTexto1
            // 
            this.campoTexto1.Campo = "CODTIPOPER";
            this.campoTexto1.Default = null;
            this.campoTexto1.Edita = true;
            this.campoTexto1.Location = new System.Drawing.Point(126, 66);
            this.campoTexto1.MaximoCaracteres = null;
            this.campoTexto1.Name = "campoTexto1";
            this.campoTexto1.Query = 0;
            this.campoTexto1.Size = new System.Drawing.Size(100, 20);
            this.campoTexto1.Tabela = "GTIPOPERREPORT2";
            this.campoTexto1.TabIndex = 2;
            this.campoTexto1.Visible = false;
            // 
            // campoInteiro1
            // 
            this.campoInteiro1.Campo = "CODEMPRESA";
            this.campoInteiro1.Default = null;
            this.campoInteiro1.Edita = true;
            this.campoInteiro1.Location = new System.Drawing.Point(20, 66);
            this.campoInteiro1.Name = "campoInteiro1";
            this.campoInteiro1.Query = 0;
            this.campoInteiro1.Size = new System.Drawing.Size(100, 20);
            this.campoInteiro1.Tabela = "GTIPOPERREPORT2";
            this.campoInteiro1.TabIndex = 1;
            this.campoInteiro1.Visible = false;
            // 
            // FormGTIPOPERREPORT2Cadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 265);
            this.Conexao = "Start";
            this.Controls.Add(this.tabControl1);
            this.Name = "FormGTIPOPERREPORT2Cadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT CODEMPRESA, CODTIPOPER, IDREPORT",
        "FROM GTIPOPERREPORT2",
        "WHERE CODEMPRESA = ?",
        "  AND CODTIPOPER = ?",
        "  AND IDREPORT = ?"};
            query1.Parametros = new string[] {
        "CODEMPRESA",
        "CODTIPOPER",
        "IDREPORT"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "GTIPOPERREPORT2";
            this.Text = "Cadastro de Relatórios";
            this.Load += new System.EventHandler(this.FormGTIPOPERREPORT2Cadastro_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private AppLib.Windows.CampoLookup campoLookup1;
        private AppLib.Windows.CampoTexto campoTexto1;
        private AppLib.Windows.CampoInteiro campoInteiro1;
    }
}