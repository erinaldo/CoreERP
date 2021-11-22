namespace PS.Glb.ERP.Comercial
{
    partial class FormRegraCFOPCadastro
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.campoLookupCODFILIAL = new AppLib.Windows.CampoLookup();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.campoLookupUFDESTINO = new AppLib.Windows.CampoLookup();
            this.campoLookupNCM = new AppLib.Windows.CampoLookup();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.campoDecimalMVAAJUSTADOMATIMPORT = new AppLib.Windows.CampoDecimal();
            this.campoDecimalMVAAJUSTADO = new AppLib.Windows.CampoDecimal();
            this.campoDecimalMVAORIGINAL = new AppLib.Windows.CampoDecimal();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.campoDecimalALIQINTERMATIMPORT = new AppLib.Windows.CampoDecimal();
            this.campoDecimalALIQINTERESTADUAL = new AppLib.Windows.CampoDecimal();
            this.campoDecimalALIQINTERNA = new AppLib.Windows.CampoDecimal();
            this.campoTextoMODALIDADEICMSST = new AppLib.Windows.CampoTexto();
            this.campoTextoMODALIDADEICMS = new AppLib.Windows.CampoTexto();
            this.campoInteiroCODEMPRESA = new AppLib.Windows.CampoInteiro();
            this.campoCheck1 = new AppLib.Windows.CampoCheck();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.campoCheck2 = new AppLib.Windows.CampoCheck();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(647, 369);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelControl13);
            this.tabPage1.Controls.Add(this.campoCheck2);
            this.tabPage1.Controls.Add(this.labelControl12);
            this.tabPage1.Controls.Add(this.campoCheck1);
            this.tabPage1.Controls.Add(this.campoLookupCODFILIAL);
            this.tabPage1.Controls.Add(this.labelControl11);
            this.tabPage1.Controls.Add(this.campoLookupUFDESTINO);
            this.tabPage1.Controls.Add(this.campoLookupNCM);
            this.tabPage1.Controls.Add(this.labelControl9);
            this.tabPage1.Controls.Add(this.labelControl10);
            this.tabPage1.Controls.Add(this.labelControl6);
            this.tabPage1.Controls.Add(this.labelControl7);
            this.tabPage1.Controls.Add(this.labelControl8);
            this.tabPage1.Controls.Add(this.campoDecimalMVAAJUSTADOMATIMPORT);
            this.tabPage1.Controls.Add(this.campoDecimalMVAAJUSTADO);
            this.tabPage1.Controls.Add(this.campoDecimalMVAORIGINAL);
            this.tabPage1.Controls.Add(this.labelControl5);
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Controls.Add(this.campoDecimalALIQINTERMATIMPORT);
            this.tabPage1.Controls.Add(this.campoDecimalALIQINTERESTADUAL);
            this.tabPage1.Controls.Add(this.campoDecimalALIQINTERNA);
            this.tabPage1.Controls.Add(this.campoTextoMODALIDADEICMSST);
            this.tabPage1.Controls.Add(this.campoTextoMODALIDADEICMS);
            this.tabPage1.Controls.Add(this.campoInteiroCODEMPRESA);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(639, 343);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // campoLookupCODFILIAL
            // 
            this.campoLookupCODFILIAL.Campo = "CODFILIAL";
            this.campoLookupCODFILIAL.ColunaCodigo = "CODFILIAL";
            this.campoLookupCODFILIAL.ColunaDescricao = "NOMEFANTASIA";
            this.campoLookupCODFILIAL.ColunaIdentificador = null;
            this.campoLookupCODFILIAL.ColunaTabela = "GFILIAL";
            this.campoLookupCODFILIAL.Conexao = "Start";
            this.campoLookupCODFILIAL.Default = null;
            this.campoLookupCODFILIAL.Edita = true;
            this.campoLookupCODFILIAL.EditaLookup = false;
            this.campoLookupCODFILIAL.Location = new System.Drawing.Point(17, 31);
            this.campoLookupCODFILIAL.MaximoCaracteres = null;
            this.campoLookupCODFILIAL.Name = "campoLookupCODFILIAL";
            this.campoLookupCODFILIAL.NomeGrid = "GFILIAL";
            this.campoLookupCODFILIAL.Query = 0;
            this.campoLookupCODFILIAL.Size = new System.Drawing.Size(514, 21);
            this.campoLookupCODFILIAL.Tabela = "VREGRAVARCFOP";
            this.campoLookupCODFILIAL.TabIndex = 0;
            this.campoLookupCODFILIAL.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookupCODFILIAL_SetFormConsulta);
            this.campoLookupCODFILIAL.SetDescricao += new AppLib.Windows.CampoLookup.SetDescricaoHandler(this.campoLookupCODFILIAL_SetDescricao);
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(17, 12);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(20, 13);
            this.labelControl11.TabIndex = 26;
            this.labelControl11.Text = "Filial";
            // 
            // campoLookupUFDESTINO
            // 
            this.campoLookupUFDESTINO.Campo = "UFDESTINO";
            this.campoLookupUFDESTINO.ColunaCodigo = "CODETD";
            this.campoLookupUFDESTINO.ColunaDescricao = "NOME";
            this.campoLookupUFDESTINO.ColunaIdentificador = null;
            this.campoLookupUFDESTINO.ColunaTabela = "GESTADO";
            this.campoLookupUFDESTINO.Conexao = "Start";
            this.campoLookupUFDESTINO.Default = null;
            this.campoLookupUFDESTINO.Edita = true;
            this.campoLookupUFDESTINO.EditaLookup = false;
            this.campoLookupUFDESTINO.Location = new System.Drawing.Point(17, 80);
            this.campoLookupUFDESTINO.MaximoCaracteres = null;
            this.campoLookupUFDESTINO.Name = "campoLookupUFDESTINO";
            this.campoLookupUFDESTINO.NomeGrid = "GESTADO";
            this.campoLookupUFDESTINO.Query = 0;
            this.campoLookupUFDESTINO.Size = new System.Drawing.Size(514, 21);
            this.campoLookupUFDESTINO.Tabela = "VREGRAVARCFOP";
            this.campoLookupUFDESTINO.TabIndex = 1;
            this.campoLookupUFDESTINO.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookupUFDESTINO_SetFormConsulta);
            // 
            // campoLookupNCM
            // 
            this.campoLookupNCM.Campo = "NCM";
            this.campoLookupNCM.ColunaCodigo = "CODIGO";
            this.campoLookupNCM.ColunaDescricao = "DESCRICAO";
            this.campoLookupNCM.ColunaIdentificador = null;
            this.campoLookupNCM.ColunaTabela = "VIBPTAX";
            this.campoLookupNCM.Conexao = "Start";
            this.campoLookupNCM.Default = null;
            this.campoLookupNCM.Edita = true;
            this.campoLookupNCM.EditaLookup = false;
            this.campoLookupNCM.Location = new System.Drawing.Point(17, 130);
            this.campoLookupNCM.MaximoCaracteres = null;
            this.campoLookupNCM.Name = "campoLookupNCM";
            this.campoLookupNCM.NomeGrid = "NCM";
            this.campoLookupNCM.Query = 0;
            this.campoLookupNCM.Size = new System.Drawing.Size(514, 21);
            this.campoLookupNCM.Tabela = "VREGRAVARCFOP";
            this.campoLookupNCM.TabIndex = 2;
            this.campoLookupNCM.SetFormConsulta += new AppLib.Windows.CampoLookup.SetFormConsultaHandler(this.campoLookupNCM_SetFormConsulta);
            this.campoLookupNCM.SetDescricao += new AppLib.Windows.CampoLookup.SetDescricaoHandler(this.campoLookupNCM_SetDescricao);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(153, 265);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(97, 13);
            this.labelControl9.TabIndex = 24;
            this.labelControl9.Text = "Modalidade ICMS ST";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(17, 265);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(82, 13);
            this.labelControl10.TabIndex = 23;
            this.labelControl10.Text = "Modalidade ICMS";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(431, 209);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(120, 13);
            this.labelControl6.TabIndex = 22;
            this.labelControl6.Text = "MVA Material Importação";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(290, 209);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(67, 13);
            this.labelControl7.TabIndex = 21;
            this.labelControl7.Text = "MVA Ajustado";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(153, 209);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(60, 13);
            this.labelControl8.TabIndex = 20;
            this.labelControl8.Text = "MVA Original";
            // 
            // campoDecimalMVAAJUSTADOMATIMPORT
            // 
            this.campoDecimalMVAAJUSTADOMATIMPORT.Campo = "MVAAJUSTADOMATIMPORT";
            this.campoDecimalMVAAJUSTADOMATIMPORT.Decimais = 2;
            this.campoDecimalMVAAJUSTADOMATIMPORT.Default = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.campoDecimalMVAAJUSTADOMATIMPORT.Edita = true;
            this.campoDecimalMVAAJUSTADOMATIMPORT.Location = new System.Drawing.Point(431, 228);
            this.campoDecimalMVAAJUSTADOMATIMPORT.Name = "campoDecimalMVAAJUSTADOMATIMPORT";
            this.campoDecimalMVAAJUSTADOMATIMPORT.Query = 0;
            this.campoDecimalMVAAJUSTADOMATIMPORT.Size = new System.Drawing.Size(100, 20);
            this.campoDecimalMVAAJUSTADOMATIMPORT.Tabela = "VREGRAVARCFOP";
            this.campoDecimalMVAAJUSTADOMATIMPORT.TabIndex = 8;
            // 
            // campoDecimalMVAAJUSTADO
            // 
            this.campoDecimalMVAAJUSTADO.Campo = "MVAAJUSTADO";
            this.campoDecimalMVAAJUSTADO.Decimais = 2;
            this.campoDecimalMVAAJUSTADO.Default = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.campoDecimalMVAAJUSTADO.Edita = true;
            this.campoDecimalMVAAJUSTADO.Location = new System.Drawing.Point(290, 228);
            this.campoDecimalMVAAJUSTADO.Name = "campoDecimalMVAAJUSTADO";
            this.campoDecimalMVAAJUSTADO.Query = 0;
            this.campoDecimalMVAAJUSTADO.Size = new System.Drawing.Size(100, 20);
            this.campoDecimalMVAAJUSTADO.Tabela = "VREGRAVARCFOP";
            this.campoDecimalMVAAJUSTADO.TabIndex = 7;
            // 
            // campoDecimalMVAORIGINAL
            // 
            this.campoDecimalMVAORIGINAL.Campo = "MVAORIGINAL";
            this.campoDecimalMVAORIGINAL.Decimais = 2;
            this.campoDecimalMVAORIGINAL.Default = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.campoDecimalMVAORIGINAL.Edita = true;
            this.campoDecimalMVAORIGINAL.Location = new System.Drawing.Point(153, 228);
            this.campoDecimalMVAORIGINAL.Name = "campoDecimalMVAORIGINAL";
            this.campoDecimalMVAORIGINAL.Query = 0;
            this.campoDecimalMVAORIGINAL.Size = new System.Drawing.Size(100, 20);
            this.campoDecimalMVAORIGINAL.Tabela = "VREGRAVARCFOP";
            this.campoDecimalMVAORIGINAL.TabIndex = 6;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(290, 164);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(138, 13);
            this.labelControl5.TabIndex = 16;
            this.labelControl5.Text = "Alíquota Material Importação";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(153, 164);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(107, 13);
            this.labelControl4.TabIndex = 15;
            this.labelControl4.Text = "Alíquota Interestadual";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(17, 164);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(78, 13);
            this.labelControl3.TabIndex = 14;
            this.labelControl3.Text = "Alíquota Interna";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(17, 61);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 13);
            this.labelControl2.TabIndex = 13;
            this.labelControl2.Text = "UF Destino";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(17, 111);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(22, 13);
            this.labelControl1.TabIndex = 12;
            this.labelControl1.Text = "NCM";
            // 
            // campoDecimalALIQINTERMATIMPORT
            // 
            this.campoDecimalALIQINTERMATIMPORT.Campo = "ALIQINTERMATIMPORT";
            this.campoDecimalALIQINTERMATIMPORT.Decimais = 2;
            this.campoDecimalALIQINTERMATIMPORT.Default = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.campoDecimalALIQINTERMATIMPORT.Edita = true;
            this.campoDecimalALIQINTERMATIMPORT.Location = new System.Drawing.Point(290, 183);
            this.campoDecimalALIQINTERMATIMPORT.Name = "campoDecimalALIQINTERMATIMPORT";
            this.campoDecimalALIQINTERMATIMPORT.Query = 0;
            this.campoDecimalALIQINTERMATIMPORT.Size = new System.Drawing.Size(100, 20);
            this.campoDecimalALIQINTERMATIMPORT.Tabela = "VREGRAVARCFOP";
            this.campoDecimalALIQINTERMATIMPORT.TabIndex = 5;
            // 
            // campoDecimalALIQINTERESTADUAL
            // 
            this.campoDecimalALIQINTERESTADUAL.Campo = "ALIQINTERESTADUAL";
            this.campoDecimalALIQINTERESTADUAL.Decimais = 2;
            this.campoDecimalALIQINTERESTADUAL.Default = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.campoDecimalALIQINTERESTADUAL.Edita = true;
            this.campoDecimalALIQINTERESTADUAL.Location = new System.Drawing.Point(153, 183);
            this.campoDecimalALIQINTERESTADUAL.Name = "campoDecimalALIQINTERESTADUAL";
            this.campoDecimalALIQINTERESTADUAL.Query = 0;
            this.campoDecimalALIQINTERESTADUAL.Size = new System.Drawing.Size(100, 20);
            this.campoDecimalALIQINTERESTADUAL.Tabela = "VREGRAVARCFOP";
            this.campoDecimalALIQINTERESTADUAL.TabIndex = 4;
            // 
            // campoDecimalALIQINTERNA
            // 
            this.campoDecimalALIQINTERNA.Campo = "ALIQINTERNA";
            this.campoDecimalALIQINTERNA.Decimais = 2;
            this.campoDecimalALIQINTERNA.Default = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.campoDecimalALIQINTERNA.Edita = true;
            this.campoDecimalALIQINTERNA.Location = new System.Drawing.Point(17, 183);
            this.campoDecimalALIQINTERNA.Name = "campoDecimalALIQINTERNA";
            this.campoDecimalALIQINTERNA.Query = 0;
            this.campoDecimalALIQINTERNA.Size = new System.Drawing.Size(100, 20);
            this.campoDecimalALIQINTERNA.Tabela = "VREGRAVARCFOP";
            this.campoDecimalALIQINTERNA.TabIndex = 3;
            // 
            // campoTextoMODALIDADEICMSST
            // 
            this.campoTextoMODALIDADEICMSST.Campo = "MODALIDADEICMSST";
            this.campoTextoMODALIDADEICMSST.Default = "4";
            this.campoTextoMODALIDADEICMSST.Edita = true;
            this.campoTextoMODALIDADEICMSST.Location = new System.Drawing.Point(153, 284);
            this.campoTextoMODALIDADEICMSST.MaximoCaracteres = null;
            this.campoTextoMODALIDADEICMSST.Name = "campoTextoMODALIDADEICMSST";
            this.campoTextoMODALIDADEICMSST.Query = 0;
            this.campoTextoMODALIDADEICMSST.Size = new System.Drawing.Size(100, 20);
            this.campoTextoMODALIDADEICMSST.Tabela = "VREGRAVARCFOP";
            this.campoTextoMODALIDADEICMSST.TabIndex = 10;
            // 
            // campoTextoMODALIDADEICMS
            // 
            this.campoTextoMODALIDADEICMS.Campo = "MODALIDADEICMS";
            this.campoTextoMODALIDADEICMS.Default = "3";
            this.campoTextoMODALIDADEICMS.Edita = true;
            this.campoTextoMODALIDADEICMS.Location = new System.Drawing.Point(17, 284);
            this.campoTextoMODALIDADEICMS.MaximoCaracteres = null;
            this.campoTextoMODALIDADEICMS.Name = "campoTextoMODALIDADEICMS";
            this.campoTextoMODALIDADEICMS.Query = 0;
            this.campoTextoMODALIDADEICMS.Size = new System.Drawing.Size(100, 20);
            this.campoTextoMODALIDADEICMS.Tabela = "VREGRAVARCFOP";
            this.campoTextoMODALIDADEICMS.TabIndex = 9;
            // 
            // campoInteiroCODEMPRESA
            // 
            this.campoInteiroCODEMPRESA.Campo = "CODEMPRESA";
            this.campoInteiroCODEMPRESA.Default = null;
            this.campoInteiroCODEMPRESA.Edita = true;
            this.campoInteiroCODEMPRESA.Location = new System.Drawing.Point(546, 80);
            this.campoInteiroCODEMPRESA.Name = "campoInteiroCODEMPRESA";
            this.campoInteiroCODEMPRESA.Query = 0;
            this.campoInteiroCODEMPRESA.Size = new System.Drawing.Size(30, 20);
            this.campoInteiroCODEMPRESA.Tabela = "VREGRAVARCFOP";
            this.campoInteiroCODEMPRESA.TabIndex = 11;
            this.campoInteiroCODEMPRESA.Visible = false;
            // 
            // campoCheck1
            // 
            this.campoCheck1.Campo = "DIFALICMSST";
            this.campoCheck1.Default = "0";
            this.campoCheck1.Edita = true;
            this.campoCheck1.Location = new System.Drawing.Point(289, 284);
            this.campoCheck1.Name = "campoCheck1";
            this.campoCheck1.Query = 0;
            this.campoCheck1.Rotulo = "Usa Difal ICMS-ST";
            this.campoCheck1.Size = new System.Drawing.Size(20, 20);
            this.campoCheck1.Tabela = "VREGRAVARCFOP";
            this.campoCheck1.TabIndex = 27;
            this.campoCheck1.TipoCampoCheck = AppLib.Global.Types.TipoCampoCheck.Booleano;
            this.campoCheck1.ValorFalso = "0";
            this.campoCheck1.ValorVerdadeiro = "1";
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(315, 288);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(86, 13);
            this.labelControl12.TabIndex = 28;
            this.labelControl12.Text = "Usa Difal ICMS-ST";
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(46, 232);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(62, 13);
            this.labelControl13.TabIndex = 30;
            this.labelControl13.Text = "Usa ICMS-ST";
            // 
            // campoCheck2
            // 
            this.campoCheck2.Campo = "USAICMSST";
            this.campoCheck2.Default = "0";
            this.campoCheck2.Edita = true;
            this.campoCheck2.Location = new System.Drawing.Point(20, 228);
            this.campoCheck2.Name = "campoCheck2";
            this.campoCheck2.Query = 0;
            this.campoCheck2.Rotulo = "Usa Difal ICMS-ST";
            this.campoCheck2.Size = new System.Drawing.Size(20, 20);
            this.campoCheck2.Tabela = "VREGRAVARCFOP";
            this.campoCheck2.TabIndex = 29;
            this.campoCheck2.TipoCampoCheck = AppLib.Global.Types.TipoCampoCheck.Booleano;
            this.campoCheck2.ValorFalso = "0";
            this.campoCheck2.ValorVerdadeiro = "1";
            // 
            // FormRegraCFOPCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 469);
            this.Conexao = "Start";
            this.Controls.Add(this.tabControl1);
            this.Name = "FormRegraCFOPCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM VREGRAVARCFOP",
        "WHERE CODEMPRESA = ?",
        "  AND NCM = ?",
        "  AND UFDESTINO = ?"};
            query1.Parametros = new string[] {
        "CODEMPRESA",
        "NCM",
        "UFDESTINO"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "VREGRAVARCFOP";
            this.Text = "Cadastro de Regra CFOP";
            this.AntesSalvar += new AppLib.Windows.FormCadastroData.AntesSalvarHandler(this.FormRegraCFOPCadastro_AntesSalvar);
            this.AposSalvar += new AppLib.Windows.FormCadastroData.AposSalvarHandler(this.FormRegraCFOPCadastro_AposSalvar);
            this.Load += new System.EventHandler(this.FormRegraCFOPCadastro_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private AppLib.Windows.CampoDecimal campoDecimalALIQINTERMATIMPORT;
        private AppLib.Windows.CampoDecimal campoDecimalALIQINTERESTADUAL;
        private AppLib.Windows.CampoDecimal campoDecimalALIQINTERNA;
        private AppLib.Windows.CampoTexto campoTextoMODALIDADEICMSST;
        private AppLib.Windows.CampoTexto campoTextoMODALIDADEICMS;
        private AppLib.Windows.CampoInteiro campoInteiroCODEMPRESA;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private AppLib.Windows.CampoDecimal campoDecimalMVAAJUSTADOMATIMPORT;
        private AppLib.Windows.CampoDecimal campoDecimalMVAAJUSTADO;
        private AppLib.Windows.CampoDecimal campoDecimalMVAORIGINAL;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private AppLib.Windows.CampoLookup campoLookupNCM;
        private AppLib.Windows.CampoLookup campoLookupUFDESTINO;
        private AppLib.Windows.CampoLookup campoLookupCODFILIAL;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private AppLib.Windows.CampoCheck campoCheck2;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private AppLib.Windows.CampoCheck campoCheck1;
    }
}