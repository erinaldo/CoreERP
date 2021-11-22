namespace PS.Glb.ERP.Financeiro
{
    partial class FormCobrancaCadastro
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
            AppLib.Windows.CodigoNome codigoNome1 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.CodigoNome codigoNome2 = new AppLib.Windows.CodigoNome();
            AppLib.Windows.Query query1 = new AppLib.Windows.Query();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.campoLookupCODTIPDOC = new AppLib.Windows.CampoLookup();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.campoLookupCODCONTA = new AppLib.Windows.CampoLookup();
            this.campoDecimalVALOR = new AppLib.Windows.CampoDecimal();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.campoTextoCODMOEDA = new AppLib.Windows.CampoTexto();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.campoDataDATAVENCIMENTO = new AppLib.Windows.CampoData();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.campoDataDATAEMISSAO = new AppLib.Windows.CampoData();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.campoLookupCODCLIFOR = new AppLib.Windows.CampoLookup();
            this.campoTextoNUMERO = new AppLib.Windows.CampoTexto();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiroCODFILIAL = new AppLib.Windows.CampoInteiro();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiroCODLANCA = new AppLib.Windows.CampoInteiro();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiroCODEMPRESA = new AppLib.Windows.CampoInteiro();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.labelControl19 = new DevExpress.XtraEditors.LabelControl();
            this.campoLookupIDBOLETOSTATUS = new AppLib.Windows.CampoLookup();
            this.campoDataDATAREMESSA = new AppLib.Windows.CampoData();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.campoInteiroCODREMESSA = new AppLib.Windows.CampoInteiro();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.campoTextoIPTE = new AppLib.Windows.CampoTexto();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.campoTextoCODIGOBARRAS = new AppLib.Windows.CampoTexto();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.campoTextoNOSSONUMERO = new AppLib.Windows.CampoTexto();
            this.labelControl14 = new DevExpress.XtraEditors.LabelControl();
            this.campoListaACEITE = new AppLib.Windows.CampoLista();
            this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.campoLookupCODCONVENIO = new AppLib.Windows.CampoLookup();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(767, 418);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelControl11);
            this.tabPage1.Controls.Add(this.campoLookupCODTIPDOC);
            this.tabPage1.Controls.Add(this.labelControl10);
            this.tabPage1.Controls.Add(this.campoLookupCODCONTA);
            this.tabPage1.Controls.Add(this.campoDecimalVALOR);
            this.tabPage1.Controls.Add(this.labelControl9);
            this.tabPage1.Controls.Add(this.campoTextoCODMOEDA);
            this.tabPage1.Controls.Add(this.labelControl8);
            this.tabPage1.Controls.Add(this.campoDataDATAVENCIMENTO);
            this.tabPage1.Controls.Add(this.labelControl7);
            this.tabPage1.Controls.Add(this.campoDataDATAEMISSAO);
            this.tabPage1.Controls.Add(this.labelControl6);
            this.tabPage1.Controls.Add(this.labelControl5);
            this.tabPage1.Controls.Add(this.campoLookupCODCLIFOR);
            this.tabPage1.Controls.Add(this.campoTextoNUMERO);
            this.tabPage1.Controls.Add(this.labelControl4);
            this.tabPage1.Controls.Add(this.campoInteiroCODFILIAL);
            this.tabPage1.Controls.Add(this.labelControl3);
            this.tabPage1.Controls.Add(this.campoInteiroCODLANCA);
            this.tabPage1.Controls.Add(this.labelControl2);
            this.tabPage1.Controls.Add(this.campoInteiroCODEMPRESA);
            this.tabPage1.Controls.Add(this.labelControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(759, 392);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificação";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(21, 164);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(77, 13);
            this.labelControl11.TabIndex = 41;
            this.labelControl11.Text = "Tipo Documento";
            // 
            // campoLookupCODTIPDOC
            // 
            this.campoLookupCODTIPDOC.Campo = "CODTIPDOC";
            this.campoLookupCODTIPDOC.ColunaCodigo = "CODTIPDOC";
            this.campoLookupCODTIPDOC.ColunaDescricao = "NOME";
            this.campoLookupCODTIPDOC.ColunaIdentificador = null;
            this.campoLookupCODTIPDOC.ColunaTabela = "FTIPDOC";
            this.campoLookupCODTIPDOC.Conexao = "Start";
            this.campoLookupCODTIPDOC.Default = null;
            this.campoLookupCODTIPDOC.Edita = false;
            this.campoLookupCODTIPDOC.Location = new System.Drawing.Point(21, 183);
            this.campoLookupCODTIPDOC.MaximoCaracteres = null;
            this.campoLookupCODTIPDOC.Name = "campoLookupCODTIPDOC";
            this.campoLookupCODTIPDOC.NomeGrid = null;
            this.campoLookupCODTIPDOC.Query = 0;
            this.campoLookupCODTIPDOC.Size = new System.Drawing.Size(630, 21);
            this.campoLookupCODTIPDOC.Tabela = "FBOLETO";
            this.campoLookupCODTIPDOC.TabIndex = 40;
            this.campoLookupCODTIPDOC.SetDescricao += new AppLib.Windows.CampoLookup.SetDescricaoHandler(this.campoLookupCODTIPDOC_SetDescricao);
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(21, 115);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(29, 13);
            this.labelControl10.TabIndex = 39;
            this.labelControl10.Text = "Conta";
            // 
            // campoLookupCODCONTA
            // 
            this.campoLookupCODCONTA.Campo = "CODCONTA";
            this.campoLookupCODCONTA.ColunaCodigo = "CODCONTA";
            this.campoLookupCODCONTA.ColunaDescricao = "DESCRICAO";
            this.campoLookupCODCONTA.ColunaIdentificador = null;
            this.campoLookupCODCONTA.ColunaTabela = "FCONTA";
            this.campoLookupCODCONTA.Conexao = "Start";
            this.campoLookupCODCONTA.Default = null;
            this.campoLookupCODCONTA.Edita = false;
            this.campoLookupCODCONTA.Location = new System.Drawing.Point(21, 134);
            this.campoLookupCODCONTA.MaximoCaracteres = null;
            this.campoLookupCODCONTA.Name = "campoLookupCODCONTA";
            this.campoLookupCODCONTA.NomeGrid = null;
            this.campoLookupCODCONTA.Query = 0;
            this.campoLookupCODCONTA.Size = new System.Drawing.Size(630, 21);
            this.campoLookupCODCONTA.Tabela = "FBOLETO";
            this.campoLookupCODCONTA.TabIndex = 38;
            this.campoLookupCODCONTA.SetDescricao += new AppLib.Windows.CampoLookup.SetDescricaoHandler(this.campoLookupCODCONTA_SetDescricao);
            // 
            // campoDecimalVALOR
            // 
            this.campoDecimalVALOR.Campo = "VALOR";
            this.campoDecimalVALOR.Decimais = 2;
            this.campoDecimalVALOR.Default = null;
            this.campoDecimalVALOR.Edita = false;
            this.campoDecimalVALOR.Location = new System.Drawing.Point(553, 233);
            this.campoDecimalVALOR.Name = "campoDecimalVALOR";
            this.campoDecimalVALOR.Query = 0;
            this.campoDecimalVALOR.Size = new System.Drawing.Size(100, 20);
            this.campoDecimalVALOR.Tabela = "FBOLETO";
            this.campoDecimalVALOR.TabIndex = 37;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(553, 213);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(24, 13);
            this.labelControl9.TabIndex = 36;
            this.labelControl9.Text = "Valor";
            // 
            // campoTextoCODMOEDA
            // 
            this.campoTextoCODMOEDA.Campo = "CODMOEDA";
            this.campoTextoCODMOEDA.Default = null;
            this.campoTextoCODMOEDA.Edita = false;
            this.campoTextoCODMOEDA.Location = new System.Drawing.Point(445, 233);
            this.campoTextoCODMOEDA.MaximoCaracteres = null;
            this.campoTextoCODMOEDA.Name = "campoTextoCODMOEDA";
            this.campoTextoCODMOEDA.Query = 0;
            this.campoTextoCODMOEDA.Size = new System.Drawing.Size(100, 20);
            this.campoTextoCODMOEDA.Tabela = "FBOLETO";
            this.campoTextoCODMOEDA.TabIndex = 35;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(445, 213);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(32, 13);
            this.labelControl8.TabIndex = 34;
            this.labelControl8.Text = "Moeda";
            // 
            // campoDataDATAVENCIMENTO
            // 
            this.campoDataDATAVENCIMENTO.Campo = "DATAVENCIMENTO";
            this.campoDataDATAVENCIMENTO.Default = null;
            this.campoDataDATAVENCIMENTO.Edita = false;
            this.campoDataDATAVENCIMENTO.Location = new System.Drawing.Point(339, 232);
            this.campoDataDATAVENCIMENTO.Name = "campoDataDATAVENCIMENTO";
            this.campoDataDATAVENCIMENTO.Query = 0;
            this.campoDataDATAVENCIMENTO.Size = new System.Drawing.Size(100, 21);
            this.campoDataDATAVENCIMENTO.Tabela = "FBOLETO";
            this.campoDataDATAVENCIMENTO.TabIndex = 33;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(339, 212);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(81, 13);
            this.labelControl7.TabIndex = 32;
            this.labelControl7.Text = "Data Vencimento";
            // 
            // campoDataDATAEMISSAO
            // 
            this.campoDataDATAEMISSAO.Campo = "DATAEMISSAO";
            this.campoDataDATAEMISSAO.Default = null;
            this.campoDataDATAEMISSAO.Edita = false;
            this.campoDataDATAEMISSAO.Location = new System.Drawing.Point(233, 232);
            this.campoDataDATAEMISSAO.Name = "campoDataDATAEMISSAO";
            this.campoDataDATAEMISSAO.Query = 0;
            this.campoDataDATAEMISSAO.Size = new System.Drawing.Size(100, 21);
            this.campoDataDATAEMISSAO.Tabela = "FBOLETO";
            this.campoDataDATAEMISSAO.TabIndex = 31;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(233, 212);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(64, 13);
            this.labelControl6.TabIndex = 30;
            this.labelControl6.Text = "Data Emissão";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(19, 66);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(92, 13);
            this.labelControl5.TabIndex = 29;
            this.labelControl5.Text = "Cliente/Fornecedor";
            // 
            // campoLookupCODCLIFOR
            // 
            this.campoLookupCODCLIFOR.Campo = "CODCLIFOR";
            this.campoLookupCODCLIFOR.ColunaCodigo = "CODCLIFOR";
            this.campoLookupCODCLIFOR.ColunaDescricao = "NOME";
            this.campoLookupCODCLIFOR.ColunaIdentificador = null;
            this.campoLookupCODCLIFOR.ColunaTabela = "VCLIFOR";
            this.campoLookupCODCLIFOR.Conexao = "Start";
            this.campoLookupCODCLIFOR.Default = null;
            this.campoLookupCODCLIFOR.Edita = false;
            this.campoLookupCODCLIFOR.Location = new System.Drawing.Point(19, 85);
            this.campoLookupCODCLIFOR.MaximoCaracteres = null;
            this.campoLookupCODCLIFOR.Name = "campoLookupCODCLIFOR";
            this.campoLookupCODCLIFOR.NomeGrid = null;
            this.campoLookupCODCLIFOR.Query = 0;
            this.campoLookupCODCLIFOR.Size = new System.Drawing.Size(630, 21);
            this.campoLookupCODCLIFOR.Tabela = "FBOLETO";
            this.campoLookupCODCLIFOR.TabIndex = 28;
            this.campoLookupCODCLIFOR.SetDescricao += new AppLib.Windows.CampoLookup.SetDescricaoHandler(this.campoLookupCODCLIFOR_SetDescricao);
            // 
            // campoTextoNUMERO
            // 
            this.campoTextoNUMERO.Campo = "NUMERO";
            this.campoTextoNUMERO.Default = null;
            this.campoTextoNUMERO.Edita = false;
            this.campoTextoNUMERO.Location = new System.Drawing.Point(21, 232);
            this.campoTextoNUMERO.MaximoCaracteres = null;
            this.campoTextoNUMERO.Name = "campoTextoNUMERO";
            this.campoTextoNUMERO.Query = 0;
            this.campoTextoNUMERO.Size = new System.Drawing.Size(206, 20);
            this.campoTextoNUMERO.Tabela = "FBOLETO";
            this.campoTextoNUMERO.TabIndex = 27;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(21, 213);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(37, 13);
            this.labelControl4.TabIndex = 26;
            this.labelControl4.Text = "Número";
            // 
            // campoInteiroCODFILIAL
            // 
            this.campoInteiroCODFILIAL.Campo = "CODFILIAL";
            this.campoInteiroCODFILIAL.Default = null;
            this.campoInteiroCODFILIAL.Edita = false;
            this.campoInteiroCODFILIAL.Location = new System.Drawing.Point(233, 36);
            this.campoInteiroCODFILIAL.Name = "campoInteiroCODFILIAL";
            this.campoInteiroCODFILIAL.Query = 0;
            this.campoInteiroCODFILIAL.Size = new System.Drawing.Size(100, 20);
            this.campoInteiroCODFILIAL.Tabela = "FBOLETO";
            this.campoInteiroCODFILIAL.TabIndex = 25;
            this.campoInteiroCODFILIAL.Visible = false;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(233, 17);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(20, 13);
            this.labelControl3.TabIndex = 24;
            this.labelControl3.Text = "Filial";
            this.labelControl3.Visible = false;
            // 
            // campoInteiroCODLANCA
            // 
            this.campoInteiroCODLANCA.Campo = "CODLANCA";
            this.campoInteiroCODLANCA.Default = null;
            this.campoInteiroCODLANCA.Edita = false;
            this.campoInteiroCODLANCA.Location = new System.Drawing.Point(19, 36);
            this.campoInteiroCODLANCA.Name = "campoInteiroCODLANCA";
            this.campoInteiroCODLANCA.Query = 0;
            this.campoInteiroCODLANCA.Size = new System.Drawing.Size(100, 20);
            this.campoInteiroCODLANCA.Tabela = "FBOLETO";
            this.campoInteiroCODLANCA.TabIndex = 23;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(19, 17);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(58, 13);
            this.labelControl2.TabIndex = 22;
            this.labelControl2.Text = "Lançamento";
            // 
            // campoInteiroCODEMPRESA
            // 
            this.campoInteiroCODEMPRESA.Campo = "CODEMPRESA";
            this.campoInteiroCODEMPRESA.Default = null;
            this.campoInteiroCODEMPRESA.Edita = false;
            this.campoInteiroCODEMPRESA.Location = new System.Drawing.Point(127, 36);
            this.campoInteiroCODEMPRESA.Name = "campoInteiroCODEMPRESA";
            this.campoInteiroCODEMPRESA.Query = 0;
            this.campoInteiroCODEMPRESA.Size = new System.Drawing.Size(100, 20);
            this.campoInteiroCODEMPRESA.Tabela = "FBOLETO";
            this.campoInteiroCODEMPRESA.TabIndex = 21;
            this.campoInteiroCODEMPRESA.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(127, 17);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(41, 13);
            this.labelControl1.TabIndex = 20;
            this.labelControl1.Text = "Empresa";
            this.labelControl1.Visible = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.labelControl19);
            this.tabPage2.Controls.Add(this.campoLookupIDBOLETOSTATUS);
            this.tabPage2.Controls.Add(this.campoDataDATAREMESSA);
            this.tabPage2.Controls.Add(this.labelControl18);
            this.tabPage2.Controls.Add(this.campoInteiroCODREMESSA);
            this.tabPage2.Controls.Add(this.labelControl17);
            this.tabPage2.Controls.Add(this.campoTextoIPTE);
            this.tabPage2.Controls.Add(this.labelControl16);
            this.tabPage2.Controls.Add(this.campoTextoCODIGOBARRAS);
            this.tabPage2.Controls.Add(this.labelControl15);
            this.tabPage2.Controls.Add(this.campoTextoNOSSONUMERO);
            this.tabPage2.Controls.Add(this.labelControl14);
            this.tabPage2.Controls.Add(this.campoListaACEITE);
            this.tabPage2.Controls.Add(this.labelControl13);
            this.tabPage2.Controls.Add(this.labelControl12);
            this.tabPage2.Controls.Add(this.campoLookupCODCONVENIO);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(759, 392);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Parâmetros";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // labelControl19
            // 
            this.labelControl19.Location = new System.Drawing.Point(21, 270);
            this.labelControl19.Name = "labelControl19";
            this.labelControl19.Size = new System.Drawing.Size(77, 13);
            this.labelControl19.TabIndex = 60;
            this.labelControl19.Text = "Status Remessa";
            // 
            // campoLookupIDBOLETOSTATUS
            // 
            this.campoLookupIDBOLETOSTATUS.Campo = "IDBOLETOSTATUS";
            this.campoLookupIDBOLETOSTATUS.ColunaCodigo = "IDBOLETOSTATUS";
            this.campoLookupIDBOLETOSTATUS.ColunaDescricao = "DESCRICAO";
            this.campoLookupIDBOLETOSTATUS.ColunaIdentificador = null;
            this.campoLookupIDBOLETOSTATUS.ColunaTabela = "FBOLETOSTATUS";
            this.campoLookupIDBOLETOSTATUS.Conexao = "Start";
            this.campoLookupIDBOLETOSTATUS.Default = null;
            this.campoLookupIDBOLETOSTATUS.Edita = false;
            this.campoLookupIDBOLETOSTATUS.Location = new System.Drawing.Point(21, 289);
            this.campoLookupIDBOLETOSTATUS.MaximoCaracteres = null;
            this.campoLookupIDBOLETOSTATUS.Name = "campoLookupIDBOLETOSTATUS";
            this.campoLookupIDBOLETOSTATUS.NomeGrid = null;
            this.campoLookupIDBOLETOSTATUS.Query = 0;
            this.campoLookupIDBOLETOSTATUS.Size = new System.Drawing.Size(630, 21);
            this.campoLookupIDBOLETOSTATUS.Tabela = "FBOLETO";
            this.campoLookupIDBOLETOSTATUS.TabIndex = 59;
            // 
            // campoDataDATAREMESSA
            // 
            this.campoDataDATAREMESSA.Campo = "DATAREMESSA";
            this.campoDataDATAREMESSA.Default = null;
            this.campoDataDATAREMESSA.Edita = false;
            this.campoDataDATAREMESSA.Location = new System.Drawing.Point(127, 340);
            this.campoDataDATAREMESSA.Name = "campoDataDATAREMESSA";
            this.campoDataDATAREMESSA.Query = 0;
            this.campoDataDATAREMESSA.Size = new System.Drawing.Size(100, 21);
            this.campoDataDATAREMESSA.Tabela = "FBOLETO";
            this.campoDataDATAREMESSA.TabIndex = 58;
            // 
            // labelControl18
            // 
            this.labelControl18.Location = new System.Drawing.Point(127, 320);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(69, 13);
            this.labelControl18.TabIndex = 57;
            this.labelControl18.Text = "Data Remessa";
            // 
            // campoInteiroCODREMESSA
            // 
            this.campoInteiroCODREMESSA.Campo = "CODREMESSA";
            this.campoInteiroCODREMESSA.Default = null;
            this.campoInteiroCODREMESSA.Edita = false;
            this.campoInteiroCODREMESSA.Location = new System.Drawing.Point(21, 340);
            this.campoInteiroCODREMESSA.Name = "campoInteiroCODREMESSA";
            this.campoInteiroCODREMESSA.Query = 0;
            this.campoInteiroCODREMESSA.Size = new System.Drawing.Size(100, 20);
            this.campoInteiroCODREMESSA.Tabela = "FBOLETO";
            this.campoInteiroCODREMESSA.TabIndex = 56;
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(21, 321);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(43, 13);
            this.labelControl17.TabIndex = 55;
            this.labelControl17.Text = "Remessa";
            // 
            // campoTextoIPTE
            // 
            this.campoTextoIPTE.Campo = "IPTE";
            this.campoTextoIPTE.Default = null;
            this.campoTextoIPTE.Edita = false;
            this.campoTextoIPTE.Location = new System.Drawing.Point(21, 238);
            this.campoTextoIPTE.MaximoCaracteres = null;
            this.campoTextoIPTE.Name = "campoTextoIPTE";
            this.campoTextoIPTE.Query = 0;
            this.campoTextoIPTE.Size = new System.Drawing.Size(418, 20);
            this.campoTextoIPTE.Tabela = "FBOLETO";
            this.campoTextoIPTE.TabIndex = 54;
            // 
            // labelControl16
            // 
            this.labelControl16.Location = new System.Drawing.Point(21, 219);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(22, 13);
            this.labelControl16.TabIndex = 53;
            this.labelControl16.Text = "IPTE";
            // 
            // campoTextoCODIGOBARRAS
            // 
            this.campoTextoCODIGOBARRAS.Campo = "CODIGOBARRAS";
            this.campoTextoCODIGOBARRAS.Default = null;
            this.campoTextoCODIGOBARRAS.Edita = false;
            this.campoTextoCODIGOBARRAS.Location = new System.Drawing.Point(21, 189);
            this.campoTextoCODIGOBARRAS.MaximoCaracteres = null;
            this.campoTextoCODIGOBARRAS.Name = "campoTextoCODIGOBARRAS";
            this.campoTextoCODIGOBARRAS.Query = 0;
            this.campoTextoCODIGOBARRAS.Size = new System.Drawing.Size(418, 20);
            this.campoTextoCODIGOBARRAS.Tabela = "FBOLETO";
            this.campoTextoCODIGOBARRAS.TabIndex = 52;
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(21, 170);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(82, 13);
            this.labelControl15.TabIndex = 51;
            this.labelControl15.Text = "Código de Barras";
            // 
            // campoTextoNOSSONUMERO
            // 
            this.campoTextoNOSSONUMERO.Campo = "NOSSONUMERO";
            this.campoTextoNOSSONUMERO.Default = null;
            this.campoTextoNOSSONUMERO.Edita = false;
            this.campoTextoNOSSONUMERO.Location = new System.Drawing.Point(21, 140);
            this.campoTextoNOSSONUMERO.MaximoCaracteres = null;
            this.campoTextoNOSSONUMERO.Name = "campoTextoNOSSONUMERO";
            this.campoTextoNOSSONUMERO.Query = 0;
            this.campoTextoNOSSONUMERO.Size = new System.Drawing.Size(206, 20);
            this.campoTextoNOSSONUMERO.Tabela = "FBOLETO";
            this.campoTextoNOSSONUMERO.TabIndex = 49;
            // 
            // labelControl14
            // 
            this.labelControl14.Location = new System.Drawing.Point(21, 121);
            this.labelControl14.Name = "labelControl14";
            this.labelControl14.Size = new System.Drawing.Size(69, 13);
            this.labelControl14.TabIndex = 48;
            this.labelControl14.Text = "Nosso Número";
            // 
            // campoListaACEITE
            // 
            this.campoListaACEITE.Campo = "ACEITE";
            this.campoListaACEITE.Default = null;
            this.campoListaACEITE.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.campoListaACEITE.Enabled = false;
            this.campoListaACEITE.comboBox1.FormattingEnabled = true;
            codigoNome1.Codigo = "0";
            codigoNome1.Nome = "Não";
            codigoNome2.Codigo = "1";
            codigoNome2.Nome = "Sim";
            this.campoListaACEITE.Lista = new AppLib.Windows.CodigoNome[] {
        codigoNome1,
        codigoNome2};
            this.campoListaACEITE.Location = new System.Drawing.Point(21, 89);
            this.campoListaACEITE.Name = "campoListaACEITE";
            this.campoListaACEITE.Query = 0;
            this.campoListaACEITE.Size = new System.Drawing.Size(100, 21);
            this.campoListaACEITE.Tabela = "FBOLETO";
            this.campoListaACEITE.TabIndex = 47;
            // 
            // labelControl13
            // 
            this.labelControl13.Location = new System.Drawing.Point(21, 70);
            this.labelControl13.Name = "labelControl13";
            this.labelControl13.Size = new System.Drawing.Size(30, 13);
            this.labelControl13.TabIndex = 46;
            this.labelControl13.Text = "Aceite";
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(21, 18);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(45, 13);
            this.labelControl12.TabIndex = 45;
            this.labelControl12.Text = "Convênio";
            // 
            // campoLookupCODCONVENIO
            // 
            this.campoLookupCODCONVENIO.Campo = "CODCONVENIO";
            this.campoLookupCODCONVENIO.ColunaCodigo = "CODCONVENIO";
            this.campoLookupCODCONVENIO.ColunaDescricao = "DESCRICAO";
            this.campoLookupCODCONVENIO.ColunaIdentificador = null;
            this.campoLookupCODCONVENIO.ColunaTabela = "FCONVENIO";
            this.campoLookupCODCONVENIO.Conexao = "Start";
            this.campoLookupCODCONVENIO.Default = null;
            this.campoLookupCODCONVENIO.Edita = false;
            this.campoLookupCODCONVENIO.Location = new System.Drawing.Point(21, 37);
            this.campoLookupCODCONVENIO.MaximoCaracteres = null;
            this.campoLookupCODCONVENIO.Name = "campoLookupCODCONVENIO";
            this.campoLookupCODCONVENIO.NomeGrid = null;
            this.campoLookupCODCONVENIO.Query = 0;
            this.campoLookupCODCONVENIO.Size = new System.Drawing.Size(630, 21);
            this.campoLookupCODCONVENIO.Tabela = "FBOLETO";
            this.campoLookupCODCONVENIO.TabIndex = 44;
            this.campoLookupCODCONVENIO.SetDescricao += new AppLib.Windows.CampoLookup.SetDescricaoHandler(this.campoLookupCODCONVENIO_SetDescricao);
            // 
            // FormCobrancaCadastro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 518);
            this.Conexao = "Start";
            this.Controls.Add(this.tabControl1);
            this.Name = "FormCobrancaCadastro";
            query1.Conexao = "Start";
            query1.Consulta = new string[] {
        "SELECT *",
        "FROM FBOLETO",
        "WHERE CODEMPRESA = ?",
        "  AND CODLANCA = ?"};
            query1.Parametros = new string[] {
        "CODEMPRESA",
        "CODLANCA"};
            this.Querys = new AppLib.Windows.Query[] {
        query1};
            this.TabelaPrincipal = "FBOLETO";
            this.Text = "Cadastro de Cobrança";
            this.Load += new System.EventHandler(this.FormCobrancaCadastro_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private AppLib.Windows.CampoInteiro campoInteiroCODEMPRESA;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private AppLib.Windows.CampoInteiro campoInteiroCODFILIAL;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private AppLib.Windows.CampoInteiro campoInteiroCODLANCA;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private AppLib.Windows.CampoTexto campoTextoNUMERO;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private AppLib.Windows.CampoLookup campoLookupCODCLIFOR;
        private AppLib.Windows.CampoData campoDataDATAVENCIMENTO;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private AppLib.Windows.CampoData campoDataDATAEMISSAO;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private AppLib.Windows.CampoTexto campoTextoCODMOEDA;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private AppLib.Windows.CampoDecimal campoDecimalVALOR;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private AppLib.Windows.CampoLookup campoLookupCODCONTA;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private AppLib.Windows.CampoLookup campoLookupCODTIPDOC;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private AppLib.Windows.CampoLookup campoLookupCODCONVENIO;
        private DevExpress.XtraEditors.LabelControl labelControl13;
        private AppLib.Windows.CampoLista campoListaACEITE;
        private AppLib.Windows.CampoTexto campoTextoIPTE;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private AppLib.Windows.CampoTexto campoTextoCODIGOBARRAS;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private AppLib.Windows.CampoTexto campoTextoNOSSONUMERO;
        private DevExpress.XtraEditors.LabelControl labelControl14;
        private AppLib.Windows.CampoInteiro campoInteiroCODREMESSA;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private AppLib.Windows.CampoData campoDataDATAREMESSA;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.LabelControl labelControl19;
        private AppLib.Windows.CampoLookup campoLookupIDBOLETOSTATUS;
    }
}