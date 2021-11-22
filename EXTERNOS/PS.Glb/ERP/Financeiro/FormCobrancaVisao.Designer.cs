namespace PS.Glb.ERP.Financeiro
{
    partial class FormCobrancaVisao
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
            AppLib.Windows.GridProps gridProps1 = new AppLib.Windows.GridProps();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid1
            // 
            this.grid1.BotaoEditar = true;
            this.grid1.BotaoExcluir = true;
            this.grid1.BotaoNovo = true;
            this.grid1.Consulta = new string[] {
        "SELECT FBOLETO.*,",
        "",
        "( SELECT NOME FROM VCLIFOR WHERE CODEMPRESA = FBOLETO.CODEMPRESA AND CODCLIFOR = " +
            "FBOLETO.CODCLIFOR ) CLIFOR,",
        "( SELECT DESCRICAO FROM FCONTA WHERE CODEMPRESA = FBOLETO.CODEMPRESA AND CODCONTA" +
            " = FBOLETO.CODCONTA ) CONTA,",
        "( SELECT NOME FROM FTIPDOC WHERE CODEMPRESA = FBOLETO.CODEMPRESA AND CODTIPDOC = " +
            "FBOLETO.CODTIPDOC ) TIPDOC,",
        "( SELECT DESCRICAO FROM FCONVENIO WHERE CODEMPRESA = FBOLETO.CODEMPRESA AND CODCO" +
            "NVENIO = FBOLETO.CODCONVENIO ) CONVENIO,",
        "( SELECT DESCRICAO FROM FBOLETOSTATUS WHERE IDBOLETOSTATUS = FBOLETO.IDBOLETOSTAT" +
            "US ) REMESSASTATUS",
        "",
        "FROM FBOLETO",
        "",
        "WHERE CODEMPRESA = ?"};
            gridProps1.Agrupar = false;
            gridProps1.Alinhamento = AppLib.Windows.Alinhamento.Direita;
            gridProps1.Coluna = "VALOR";
            gridProps1.Formato = AppLib.Windows.Formato.Decimal2;
            gridProps1.Largura = 50;
            gridProps1.Sequencia = 0;
            gridProps1.Visivel = true;
            this.grid1.Formatacao = new AppLib.Windows.GridProps[] {
        gridProps1};
            this.grid1.NomeGrid = "FBOLETO";
            this.grid1.Size = new System.Drawing.Size(934, 417);
            this.grid1.TipoFiltro = AppLib.Global.Types.TipoFiltro.Selecionar;
            this.grid1.SetParametros += new AppLib.Windows.GridData.SetNewParametrosHandler(this.grid1_SetParametros);
            this.grid1.Novo += new AppLib.Windows.GridData.NovoHandler(this.grid1_Novo);
            this.grid1.Editar += new AppLib.Windows.GridData.EditarHandler(this.grid1_Editar);
            this.grid1.Excluir += new AppLib.Windows.GridData.ExcluirHandler(this.grid1_Excluir);
            this.grid1.Load += new System.EventHandler(this.grid1_Load);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Size = new System.Drawing.Size(934, 417);
            // 
            // FormCobrancaVisao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 417);
            this.Name = "FormCobrancaVisao";
            this.Text = "Visão de Cobrança";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormCobrancaVisao_FormClosed);
            this.Load += new System.EventHandler(this.FormCobrancaVisao_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}