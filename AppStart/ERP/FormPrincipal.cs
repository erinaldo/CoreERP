using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PS.Lib;
using PS.Glb;

namespace ERP
{
    public partial class FormPrincipal : System.Windows.Forms.Form // DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Global gb;
        private List<PS.Lib.WebBrowser> ListWB;
        private ERP.Properties.Settings sett = new Properties.Settings();

      
        public FormPrincipal()
        {
            InitializeComponent();

            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();

            //try
            //{
            //    if (sett.TipoGestaoMemoria == 0)
            //    {
            //        // Não faz nada.
            //        Memoria.Caption = "Memória: sem gestão";
            //        timer1.Interval = (1000 * 10);
            //        timer1.Enabled = true;
            //    }
            //    if (sett.TipoGestaoMemoria == 1)
            //    {
            //        // Minutos
            //        Memoria.Caption = "Memória: gestão por minutos";
            //        timer1.Interval = (1000 * 60 * sett.QuantGestaoMemoria);
            //        timer1.Enabled = true;
            //    }
            //    if (sett.TipoGestaoMemoria == 2)
            //    {
            //        // MB memória Ram
            //        Memoria.Caption = "Memória: gestão por megabyte";
            //        timer1.Interval = (1000 * 10);
            //        timer1.Enabled = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    AppLib.Windows.FormMessageDefault.ShowError("Revisar as confiugrações de memória.\r\nDetalhe técnico: " + ex.Message);
            //}
        }

        private void CarregaWebBrowser()
        {
            ListWB = gb.CarregaWebBrowser();

            repositoryItemComboBox3.Items.Clear();

            for (int i = 0; i < ListWB.Count; i++)
            {
                repositoryItemComboBox3.Items.Add(ListWB[i].descricao);
            }
        }

        private void CarregaTela()
        {
            foreach (DevExpress.Skins.SkinContainer cnt in DevExpress.Skins.SkinManager.Default.Skins)
            {
                repositoryItemComboBox1.Items.Add(cnt.SkinName);
            }

            this.Skin();

            PSVersion vrs = new PSVersion();

            // Versão.Caption = string.Empty;
            Empresa.Caption = string.Empty;
            Usuário.Caption = string.Empty;
            Alias.Caption = string.Empty;

            // Versão.Caption = string.Concat("Versão: ",PS.Lib.Contexto.Session.VersaoApp);

            // this.ribbonControl1.SelectedPage = this.ribbonPage1;
        }

        private void ValidaSessao()
        {
            if (Contexto.Session.CodUsuario == null)
            {
                return;
            }
            else
            {
                PS.Lib.Login login = new PS.Lib.Login();
                gb = new Global();

                DataTable dt = login.EmpresaUserList();

                // Se o usuário tem permissão para mais de uma empresa então abre a tela de selação
                if (dt.Rows.Count > 1)
                {
                    FormSelecaoEmpresa frmSelecionaEmpresa = new FormSelecaoEmpresa();
                    frmSelecionaEmpresa.ShowDialog();
                }
                else
                {
                    // Se o usuário tem acesso a apenas uma empresa então não abre a tela de seleção
                    if (dt.Rows.Count == 1)
                    {
                        Empresa emp = new Empresa();
                        emp.CodEmpresa = int.Parse(dt.Rows[0][0].ToString());
                        emp.NomeFantasia = dt.Rows[0][1].ToString();
                        emp.Nome = dt.Rows[0][2].ToString();
                        emp.CNPJCPF = dt.Rows[0][3].ToString();
                        emp.InscricaoEstadual = dt.Rows[0][4].ToString();
                        emp.CodControle = dt.Rows[0][5].ToString();
                        emp.CodChave1 = dt.Rows[0][6].ToString();
                        emp.CodChave2 = dt.Rows[0][7].ToString();

                        Contexto.Session.Empresa = emp;
                        Contexto.Session.Empresa.GetPerfilList();
                    }
                }

                if (Contexto.Session.Empresa != null)
                {
                    //this.Text = string.Concat(" | ", Contexto.Session.Empresa.nomeFantasia);
                    Empresa.Caption = string.Concat("Empresa: ", Contexto.Session.Empresa.NomeFantasia);
                    Usuário.Caption = string.Concat("Usuário: ", Contexto.Session.CodUsuario);
                    Alias.Caption = string.Concat("Alias: ", PS.Lib.Contexto.Alias.Name);
                }
                else
                {
                    //this.Text = "ERP";
                    Empresa.Caption = string.Concat("Empresa: ", string.Empty);
                    Usuário.Caption = string.Concat("Usuário: ", Contexto.Session.CodUsuario);
                    Alias.Caption = string.Concat("Alias: ", PS.Lib.Contexto.Alias.Name);
                }

                this.CarregaWebBrowser();
            }
        }

        private void ValidaFormularioAberto()
        {
            bool Flag = false;
            foreach (Form form in this.MdiChildren)
            {
                Flag = true;
            }

            if (Flag)
            {
                throw new Exception("Para fazer esta ação é necessário fechar todas as janelas.");
            }
        }

        private void Skin()
        {
            switch (dlfSkinPadrao.LookAndFeel.SkinName)
            {
                case "Black":
                    bciSkinBlack.Checked = true;
                    break;
                case "Blue":
                    bciSkinBlue.Checked = true;
                    break;
                case "Caramel":
                    bciSkinCaramel.Checked = true;
                    break;
                case "iMaginary":
                    bciSkiniMaginary.Checked = true;
                    break;
                case "Lilian":
                    bciSkinLilian.Checked = true;
                    break;
                case "Money Twins":
                    bciSkinMoneyTwins.Checked = true;
                    break;
                case "The Asphalt World":
                    bciSkinTheAsphaltWorld.Checked = true;
                    break;
            }
        }

        private void CarregaOperacao(int Tipo, string codMenu)
        {
            /*
            if (PS.Lib.Contexto.Session.TipoOperacao != null)
            {
                string msg = gb.RetornaParametrosOperacao(PS.Lib.Contexto.Session.TipoOperacao, "DESCRICAO").ToString();

                PSMessageBox.ShowInfo(string.Concat("Atenção. Ja existe uma tela de operação [", msg, "] ativa."));
                return;
            }
            */

            using (PSSelTipoOperacao psSelTipoOperacao = new PSSelTipoOperacao())
            {
                psSelTipoOperacao.Tipo = Tipo;
                psSelTipoOperacao.pai = this;
                psSelTipoOperacao.TipoOper = string.Empty;
                psSelTipoOperacao.CodFilial = 0;
                psSelTipoOperacao.codMenu = codMenu;
                switch (psSelTipoOperacao.ShowDialog())
                {
                    case DialogResult.OK:
                        if (!string.IsNullOrEmpty(psSelTipoOperacao.TipoOper))
                        {
                            PSPartOperacao psPartOperacao = new PSPartOperacao();
                            psPartOperacao.DefaultFilter.Add(new PS.Lib.PSFilter("CODFILIAL", psSelTipoOperacao.CodFilial));
                            psPartOperacao.DefaultFilter.Add(new PS.Lib.PSFilter("CODTIPOPER", psSelTipoOperacao.TipoOper));
                            psPartOperacao.MainForm = this;
                            
                            psPartOperacao.Execute();
                            break;
                        }
                        else
                        {
                            break;
                        }
                    case DialogResult.Abort:
                        break;
                }
            }

        }

        private void bciSkinBlack_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dlfSkinPadrao.LookAndFeel.SkinName = e.Item.Caption;
        }

        private void bciSkinBlue_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dlfSkinPadrao.LookAndFeel.SkinName = e.Item.Caption;
        }

        private void bciSkinCaramel_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dlfSkinPadrao.LookAndFeel.SkinName = e.Item.Caption;
        }

        private void bciSkiniMaginary_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dlfSkinPadrao.LookAndFeel.SkinName = e.Item.Caption;
        }

        private void bciSkinLilian_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dlfSkinPadrao.LookAndFeel.SkinName = e.Item.Caption;
        }

        private void bciSkinMoneyTwins_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dlfSkinPadrao.LookAndFeel.SkinName = e.Item.Caption;
        }

        private void bciSkinTheAsphaltWorld_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dlfSkinPadrao.LookAndFeel.SkinName = e.Item.Caption;
        }

        private void FrmMainRibbon_Load(object sender, EventArgs e)
        {
            this.CarregaTela();
            this.ValidaSessao();

            #region SETA OS DADOS DO CONTEXTO DA APP LIB

            AppLib.Context.ControlarAcessos = true;
            AppLib.Context.Empresa = PS.Lib.Contexto.Session.Empresa.CodEmpresa;
            AppLib.Context.Usuario = PS.Lib.Contexto.Session.CodUsuario;
            AppLib.Context.Filial = 1;
            AppLib.Context.Perfil = PS.Lib.Contexto.Session.CodPerfil[0];

            #endregion

            timerLogin.Interval = (1000 * 3 * 60);
            timerLogin.Start();
        }

        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.ValidaFormularioAberto();

                if (Contexto.Session.CodUsuario == null)
                {
                    FrmMainRibbon_Load(this, null);
                    return;
                }

                //PS.Lib.Login login = new PS.Lib.Login();
                //DataTable dt = login.EmpresaUserList();

                // Se o usuário tem permissão para mais de uma empresa então abre a tela de selação
                //if (dt.Rows.Count > 1)
                //{
                FormSelecaoEmpresa frmSelecionaEmpresa = new FormSelecaoEmpresa();
                frmSelecionaEmpresa.ShowDialog();
                //}
                //else
                //{
                // Se o usuário tem acesso a apenas uma empresa então não abre a tela de seleção
                //if (dt.Rows.Count == 1)
                //{
                //Empresa emp = new Empresa();
                //emp.codEmpresa = int.Parse(dt.Rows[0][0].ToString());
                //emp.nomeFantasia = dt.Rows[0][1].ToString();
                //emp.nome = dt.Rows[0][2].ToString();
                //emp.CGCCPF = dt.Rows[0][3].ToString();
                //emp.InscricaoEstadual = dt.Rows[0][4].ToString();
                //emp.codControle = dt.Rows[0][5].ToString();
                //emp.codChave1 = dt.Rows[0][6].ToString();
                //emp.codChave2 = dt.Rows[0][7].ToString();

                //Contexto.Session.Empresa = emp;
                //Contexto.Session.Empresa.GetPerfilList();
                //}
                //}

                if (Contexto.Session.Empresa != null)
                {
                    //this.Text = string.Concat(" | ", Contexto.Session.Empresa.nomeFantasia);
                    Empresa.Caption = string.Concat("Empresa: ", Contexto.Session.Empresa.NomeFantasia);
                    Usuário.Caption = string.Concat("Usuário: ", Contexto.Session.CodUsuario);
                    Alias.Caption = string.Concat("Alias: ", PS.Lib.Contexto.Alias.Name);
                }
                else
                {
                    //this.Text = "ERP";
                    Empresa.Caption = string.Concat("Empresa: ", string.Empty);
                    Usuário.Caption = string.Concat("Usuário: ", Contexto.Session.CodUsuario);
                    Alias.Caption = string.Concat("Alias: ", PS.Lib.Contexto.Alias.Name);
                }

            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
            }
        }

        private void barButtonItem24_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.ValidaFormularioAberto();

                FormLogin frmLogin = new FormLogin();
                frmLogin.ShowDialog();

                this.ValidaSessao();
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError(ex.Message);
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //PSPartCliFor PSPartCliFor = new PSPartCliFor();
            //PSPartCliFor.MainForm = this;
            //PSPartCliFor.Execute();
            if (verificaAcessoMenu("PSPartCliFor") == true)
            {
                PS.Glb.New.Filtro.frmFiltroCliente frm = new PS.Glb.New.Filtro.frmFiltroCliente();
                frm.pai = this;
                frm.ShowDialog();
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //PSPartProduto psPartProduto = new PSPartProduto();
            //psPartProduto.MainForm = this;
            //psPartProduto.Execute();


            if (verificaAcessoMenu("PSPartProduto") == true)
            {
                PS.Glb.New.Filtro.frmFiltroProduto frm = new PS.Glb.New.Filtro.frmFiltroProduto();
                frm.pai = this;
                frm.ShowDialog();
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartRegCaixa psPartRegCaixa = new PSPartRegCaixa();
            psPartRegCaixa.MainForm = this;
            psPartRegCaixa.Execute();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //PS.Glb.PDV.PSPartPDVEdit psPartPDVEdit = new PS.Glb.PDV.PSPartPDVEdit();
            //psPartPDVEdit.ShowDialog();
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartCaixa psPartCaixa = new PSPartCaixa();
            psPartCaixa.MainForm = this;
            psPartCaixa.Execute();
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartEmpresa psEmpresa = new PSPartEmpresa();
            psEmpresa.MainForm = this;
            psEmpresa.Execute();
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartFilial psPartFilial = new PSPartFilial();
            psPartFilial.MainForm = this;
            psPartFilial.Execute();
        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartTipOper psPartTipOper = new PSPartTipOper();
            psPartTipOper.MainForm = this;
            psPartTipOper.Execute();
        }

        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartParamVarejo psPartParamVarejo = new PSPartParamVarejo();
            psPartParamVarejo.MainForm = this;
            psPartParamVarejo.Execute();
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartPerfil psPerfil = new PSPartPerfil();
            psPerfil.MainForm = this;
            psPerfil.Execute();
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartUsuario psUsuario = new PSPartUsuario();
            psUsuario.MainForm = this;
            psUsuario.Execute();
        }

        private void barButtonItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartCondicaoPgto psPartCondicaoPgto = new PSPartCondicaoPgto();
            psPartCondicaoPgto.MainForm = this;
            psPartCondicaoPgto.Execute();
        }

        private void barButtonItem27_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartFormaPgto psPartFormaPgto = new PSPartFormaPgto();
            psPartFormaPgto.MainForm = this;
            psPartFormaPgto.Execute();
        }

        private void barButtonItem28_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartTipDoc psPartTipDoc = new PSPartTipDoc();
            psPartTipDoc.MainForm = this;
            psPartTipDoc.Execute();
        }

        private void barButtonItem29_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartBanco psPartBanco = new PSPartBanco();
            psPartBanco.MainForm = this;
            psPartBanco.Execute();
        }

        private void barButtonItem30_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartConta psPartConta = new PSPartConta();
            psPartConta.MainForm = this;
            psPartConta.Execute();
        }

        private void barButtonItem31_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartNatureza psPartNatureza = new PSPartNatureza();
            psPartNatureza.MainForm = this;
            psPartNatureza.Execute();
        }

        private void barButtonItem32_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartTributo psPartTributo = new PSPartTributo();
            psPartTributo.MainForm = this;
            psPartTributo.Execute();
        }

        private void barButtonItem33_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartSerie psPartSerie = new PSPartSerie();
            psPartSerie.MainForm = this;
            psPartSerie.Execute();
        }

        private void barButtonItem34_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PS.Glb.Formula.PSPartFormula psPartFormula = new PS.Glb.Formula.PSPartFormula();
            psPartFormula.MainForm = this;
            psPartFormula.Execute();
        }

        private void barButtonItem35_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //PS.Lib.WinForms.Query.PSPartQuery psPartQuery = new PS.Lib.WinForms.Query.PSPartQuery();
            //psPartQuery.MainForm = this;
            //psPartQuery.Execute();
            PS.Glb.New.Visao.Globais.frmVisaoQuery frm = new PS.Glb.New.Visao.Globais.frmVisaoQuery(this);
            frm.Show();
        }

        private void barButtonItem36_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PS.Glb.Report.PSPartReport psPartReport = new PS.Glb.Report.PSPartReport();
            psPartReport.MainForm = this;
            psPartReport.Execute();
        }

        private void barButtonItem37_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //PSPartLanca psPartLanca = new PSPartLanca();
            //psPartLanca.MainForm = this;
            //psPartLanca.Execute();

            //PS.Glb.New.frmFiltro frm = new PS.Glb.New.frmFiltro("FLANCA");
            //frm.pai = this;
            //frm.ShowDialog();

            if (verificaAcessoMenu("PSPartLanca") == true)
            {
                PS.Glb.New.Filtro.frmFiltroLancamento frm = new PS.Glb.New.Filtro.frmFiltroLancamento();
                frm.pai = this;
                frm.ShowDialog();
            }
        }

        private void barButtonItem38_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //PSPartCheque psPartCheque = new PSPartCheque();
            //psPartCheque.MainForm = this;
            //psPartCheque.Execute();

            PS.Glb.ERP.Financeiro.FormChequeVisao f = PS.Glb.ERP.Financeiro.FormChequeVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItem39_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //PSPartFiscalImp psPartFiscalImp = new PSPartFiscalImp();
            //psPartFiscalImp.MainForm = this;
            //psPartFiscalImp.Execute();
        }

        private void barButtonItem41_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem46_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartCentroCusto psPartCentroCusto = new PSPartCentroCusto();
            psPartCentroCusto.MainForm = this;
            psPartCentroCusto.Execute();
        }

        private void barButtonItem42_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartFatura psPartFatura = new PSPartFatura();
            psPartFatura.MainForm = this;
            psPartFatura.Execute();
        }

        private void barButtonItem47_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartNaturezaOrcamento psPartNaturezaOrcamento = new PSPartNaturezaOrcamento();
            psPartNaturezaOrcamento.MainForm = this;
            psPartNaturezaOrcamento.Execute();
        }

        private void barEditItem1_EditValueChanged(object sender, EventArgs e)
        {
            dlfSkinPadrao.LookAndFeel.SkinName = barEditItem1.EditValue.ToString();
            Skin();
        }

        private void barEditItem2_EditValueChanged(object sender, EventArgs e)
        {
            if (barEditItem2.EditValue.ToString() == "Janelas")
                xtraTabbedMdiManager1.MdiParent = null;
            else
                xtraTabbedMdiManager1.MdiParent = this;
        }

        private void barButtonItem49_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barButtonItem24_ItemClick(this, null);
        }

        private void barButtonItem48_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            barButtonItem23_ItemClick(this, null);
        }

        private void barButtonItem50_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < Application.OpenForms.Count; i++)
            {
                this.Close();
            }

            Application.Exit();
        }

        private void barButtonItem53_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PS.Lib.WinForms.WebBrowser.PSPartWebBrowser psPartWebBrowser = new PS.Lib.WinForms.WebBrowser.PSPartWebBrowser();
            psPartWebBrowser.MainForm = this;
            psPartWebBrowser.Execute();

            //this.CarregaWebBrowser();
        }

        private void barEditItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem54_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PS.Glb.PSPartNFERegCab psPartNFERegCab = new PS.Glb.PSPartNFERegCab();
            psPartNFERegCab.MainForm = this;
            psPartNFERegCab.Execute();
        }

        private void barButtonItem43_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PS.Glb.PSPartExtratoCaixa psPartExtratoCaixa = new PS.Glb.PSPartExtratoCaixa();
            psPartExtratoCaixa.MainForm = this;
            psPartExtratoCaixa.Execute();
        }

        private void barButtonItem55_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PS.Glb.PSPartOperMensagem psPartOperMensagem = new PS.Glb.PSPartOperMensagem();
            psPartOperMensagem.MainForm = this;
            psPartOperMensagem.Execute();
        }

        private void barButtonItem57_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PS.Glb.PSPartTabCampoCompl psPartTabCampoCompl = new PS.Glb.PSPartTabCampoCompl();
            psPartTabCampoCompl.MainForm = this;
            psPartTabCampoCompl.Execute();
        }

        private void barButtonItem58_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PS.Glb.PSPartTabDinamica psPartTabDinamica = new PS.Glb.PSPartTabDinamica();
            psPartTabDinamica.MainForm = this;
            psPartTabDinamica.Execute();
        }

        private void barButtonItem56_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PS.Glb.PSPartEstrutura psPartPSPartEstrutura = new PS.Glb.PSPartEstrutura();
            psPartPSPartEstrutura.MainForm = this;
            psPartPSPartEstrutura.Execute();
        }

        private void barButtonItem59_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PS.Glb.PSPartComponente psPartPSPartComponente = new PS.Glb.PSPartComponente();
            psPartPSPartComponente.MainForm = this;
            psPartPSPartComponente.Execute();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*
            if (PS.Lib.Contexto.Session.tipOperacao != null)
            {
                string msg = gb.RetornaParametrosOperacao(PS.Lib.Contexto.Session.tipOperacao, "DESCRICAO").ToString();

                PSMessageBox.ShowInfo(string.Concat("Atenção. Ja existe uma tela de operação [", msg, "] ativa."));
                return;
            }

            PSSelTipoOperacao psSelTipoOperacao = new PSSelTipoOperacao();
            psSelTipoOperacao.TipOper = 1;
            psSelTipoOperacao.ShowDialog();

            if (PS.Lib.Contexto.Session.tipOperacao != null)
            {
                PSPartOperacao psPartOperacao = new PSPartOperacao();
                psPartOperacao.MainForm = this;
                psPartOperacao.Execute();
            }
            */
            CarregaOperacao(1, "btnOperacoes_Estoque");
        }

        private void barButtonItem44_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*
            if (PS.Lib.Contexto.Session.tipOperacao != null)
            {
                string msg = gb.RetornaParametrosOperacao(PS.Lib.Contexto.Session.tipOperacao, "DESCRICAO").ToString();

                PSMessageBox.ShowInfo(string.Concat("Atenção. Ja existe uma tela de operação [", msg, "] ativa."));
                return;
            }

            PSSelTipoOperacao psSelTipoOperacao = new PSSelTipoOperacao();
            psSelTipoOperacao.TipOper = 2;
            psSelTipoOperacao.ShowDialog();

            if (PS.Lib.Contexto.Session.tipOperacao != null)
            {
                PSPartOperacao psPartOperacao = new PSPartOperacao();
                psPartOperacao.MainForm = this;
                psPartOperacao.Execute();
            }
            */
            CarregaOperacao(2, "btnOperacoes_Entradas");
        }

        private void barButtonItem60_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*
            if (PS.Lib.Contexto.Session.tipOperacao != null)
            {
                string msg = gb.RetornaParametrosOperacao(PS.Lib.Contexto.Session.tipOperacao, "DESCRICAO").ToString();

                PSMessageBox.ShowInfo(string.Concat("Atenção. Ja existe uma tela de operação [", msg, "] ativa."));
                return;
            }

            PSSelTipoOperacao psSelTipoOperacao = new PSSelTipoOperacao();
            psSelTipoOperacao.TipOper = 3;
            psSelTipoOperacao.ShowDialog();

            if (PS.Lib.Contexto.Session.tipOperacao != null)
            {
                PSPartOperacao psPartOperacao = new PSPartOperacao();
                psPartOperacao.MainForm = this;
                psPartOperacao.Execute();
            }
            */
            CarregaOperacao(3, "btnOperacoes_Saidas");

        }

        private void barButtonItem61_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*
            if (PS.Lib.Contexto.Session.tipOperacao != null)
            {
                string msg = gb.RetornaParametrosOperacao(PS.Lib.Contexto.Session.tipOperacao, "DESCRICAO").ToString();

                PSMessageBox.ShowInfo(string.Concat("Atenção. Ja existe uma tela de operação [", msg, "] ativa."));
                return;
            }

            PSSelTipoOperacao psSelTipoOperacao = new PSSelTipoOperacao();
            psSelTipoOperacao.TipOper = 0;
            psSelTipoOperacao.ShowDialog();

            if (PS.Lib.Contexto.Session.tipOperacao != null)
            {
                PSPartOperacao psPartOperacao = new PSPartOperacao();
                psPartOperacao.MainForm = this;
                psPartOperacao.Execute();
            }
            */
            CarregaOperacao(0, "btnOperacoes_Contratos");
        }

        private void barButtonItem63_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartOrdemProducao psOrdemProducao = new PSPartOrdemProducao();
            psOrdemProducao.MainForm = this;
            psOrdemProducao.Execute();
        }

        private void barButtonItem62_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartAtividadeProducao psAtividadeProducao = new PSPartAtividadeProducao();
            psAtividadeProducao.MainForm = this;
            psAtividadeProducao.Execute();
        }

        private void barButtonItem64_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartObjeto psPartObjeto = new PSPartObjeto();
            psPartObjeto.MainForm = this;
            psPartObjeto.Execute();
        }

        private void barButtonItem67_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartTipoObjeto psPartTipoObjeto = new PSPartTipoObjeto();
            psPartTipoObjeto.MainForm = this;
            psPartTipoObjeto.Execute();
        }

        private void barButtonItem65_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartModelo psPartModelo = new PSPartModelo();
            psPartModelo.MainForm = this;
            psPartModelo.Execute();
        }

        private void barButtonItem66_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartSubModelo psPartSubModelo = new PSPartSubModelo();
            psPartSubModelo.MainForm = this;
            psPartSubModelo.Execute();
        }

        private void barButtonItem59_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartRegiao psPSPartRegiao = new PSPartRegiao();
            psPSPartRegiao.MainForm = this;
            psPSPartRegiao.Execute();
        }

        private void FrmMainRibbon_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult opcaoSelecionada;
            opcaoSelecionada = MessageBox.Show("Deseja sair do sistema?", "Mensagem", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (opcaoSelecionada.Equals(DialogResult.No))
            {
                e.Cancel = true;
                Properties.Settings.Default.Save();
            }
        }

        private void barButtonItem68_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartNFEstadual psPSPartNFEstadual = new PSPartNFEstadual();
            psPSPartNFEstadual.MainForm = this;
            psPSPartNFEstadual.Execute();
        }

        private void barButtonItem71_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartDepartamento psPartDepartamento = new PSPartDepartamento();
            psPartDepartamento.MainForm = this;
            psPartDepartamento.Execute();
        }

        private void barButtonItem72_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartBoleto psPartBoleto = new PSPartBoleto();
            psPartBoleto.MainForm = this;
            psPartBoleto.Execute();
            //ERP.Financeiro.FormCobrancaVisao f = Financeiro.FormCobrancaVisao.GetInstance();
            //f.MdiParent = this;
            //f.Mostrar();
        }

        private void barButtonItem74_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PS.Glb.ERP.Financeiro.FormConvenioVisao f = PS.Glb.ERP.Financeiro.FormConvenioVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItem75_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PS.Glb.ERP.Comercial.FormIBPTaxVisao f = PS.Glb.ERP.Comercial.FormIBPTaxVisao.GetInstance();
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItem77_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem78_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem79_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem80_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem77_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem78_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AppLib.Padrao.FormUsuarioVisao f = AppLib.Padrao.FormUsuarioVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItem79_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AppLib.Padrao.FormProcessoVisao f = AppLib.Padrao.FormProcessoVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItem80_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AppLib.Padrao.FormPerfilVisao f = AppLib.Padrao.FormPerfilVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.MdiParent = this;
            f.Mostrar();
        }

        private void barButtonItem82_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartRepre psPartRepre = new PSPartRepre();
            psPartRepre.MainForm = this;
            psPartRepre.Execute();
        }

        private void barButtonItem83_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartTransportadora psPartTransportadora = new PSPartTransportadora();
            psPartTransportadora.MainForm = this;
            psPartTransportadora.Execute();
        }

        private void barButtonItem84_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartLocalEstoque psPartLocalEstoque = new PSPartLocalEstoque();
            psPartLocalEstoque.MainForm = this;
            psPartLocalEstoque.Execute();
        }

        private void barButtonItem85_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartUnidade psPartUnidade = new PSPartUnidade();
            psPartUnidade.MainForm = this;
            psPartUnidade.Execute();
        }

        private void barButtonItem86_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartMoeda psPartMoeda = new PSPartMoeda();
            psPartMoeda.MainForm = this;
            psPartMoeda.Execute();
        }

        private void barButtonItem87_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartOperador psPartOperador = new PSPartOperador();
            psPartOperador.MainForm = this;
            psPartOperador.Execute();
        }

        private void barButtonItem88_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartFabricante psPartFabricante = new PSPartFabricante();
            psPartFabricante.MainForm = this;
            psPartFabricante.Execute();
        }

        private void barButtonItem89_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartCalendario psPartCalendario = new PSPartCalendario();
            psPartCalendario.MainForm = this;
            psPartCalendario.Execute();
        }

        private void barButtonItem90_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartCidade psCidade = new PSPartCidade();
            psCidade.MainForm = this;
            psCidade.Execute();
        }

        private void barButtonItem91_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartEstado psEstado = new PSPartEstado();
            psEstado.MainForm = this;
            psEstado.Execute();
        }

        private void barButtonItem92_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartPais psPais = new PSPartPais();
            psPais.MainForm = this;
            psPais.Execute();
        }

        //public int GetMemoria()
        //{
        //    System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
        //    int memsize = 0; // memsize in Megabyte
        //    System.Diagnostics.PerformanceCounter PC = new System.Diagnostics.PerformanceCounter();
        //    PC.CategoryName = "Process";
        //    PC.CounterName = "Working Set - Private";
        //    PC.InstanceName = proc.ProcessName;
        //    memsize = Convert.ToInt32(PC.NextValue()) / (int)(1024);
        //    PC.Close();
        //    PC.Dispose();
        //    return memsize;
        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            // MB memória Ram
            //decimal mem = this.GetMemoria();
            //Memoria.Caption = "Memória: " + (mem / 1024).ToString() + "Mb";

            //if (sett.TipoGestaoMemoria == 1)
            //{
            //    // Minutos
            //    AppLib.MemoryManager.ReleaseUnusedMemory();
            //}
            //if (sett.TipoGestaoMemoria == 2)
            //{
            //    if (mem > (sett.QuantGestaoMemoria * 1024))
            //    {
            //        AppLib.MemoryManager.ReleaseUnusedMemory();
            //    }
            //}
            





            //Atualiza a tabela de cotação 
            //atualizaCotacao();

        }

        private void timerLogin_Tick(object sender, EventArgs e)
        {
            //Controle controle = new Controle();
            //controle.AtualizarDataLogin(AppLib.Context.Usuario);
        }

        private void FormPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Controle controle = new Controle();
            controle.AtualizarLogoff(AppLib.Context.Usuario);
        }

        private void btnAlteraSenha_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormAlteraSenha form = new FormAlteraSenha();
            form.Show();
        }
        //private void atualizaCotacao()
        //{
        //    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODMOEDA, CODCOMPRA, CODVENDA FROM VMOEDA WHERE CODMOEDA <> ? AND CODEMPRESA = ?", new object[] { "R$", AppLib.Context.Empresa });
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        BancoCentral.FachadaWSSGSService bc = new BancoCentral.FachadaWSSGSService();
        //        string vlCompra = bc.getUltimoValorVO(Convert.ToInt32(dt.Rows[i]["CODCOMPRA"])).ultimoValor.svalor;
        //        string vlVenda = bc.getUltimoValorVO(Convert.ToInt32(dt.Rows[i]["CODVENDA"])).ultimoValor.svalor;
        //        AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO VCOTACAO (CODEMPRESA, CODMOEDA, VLVENDA, VLCOMPRA) VALUES (?,?,?,?)", new object[] { AppLib.Context.Empresa, dt.Rows[i]["CODMOEDA"], vlVenda, vlCompra });
        //    }
        //}

        private void barButtonItem93_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartVendedor psPartVendedor = new PSPartVendedor();
            psPartVendedor.MainForm = this;
            psPartVendedor.Execute();
        }

        private void barButtonItemREGRACFOP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PS.Glb.ERP.Comercial.FormRegraCFOPVisao f = new PS.Glb.ERP.Comercial.FormRegraCFOPVisao();
            f.Mostrar(this);
        }

        private void barButtonItem95_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSPartTipoTransporte PSPartTipoTransporte = new PSPartTipoTransporte();
            PSPartTipoTransporte.MainForm = this;
            PSPartTipoTransporte.Execute();
        }

        #region BUSINESS INTELLIGENCE

        private void barButtonItem96_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (new AppLib.Security.Access().Processo("Start", "IMPDADOS", AppLib.Context.Perfil))
            {
                AppLib.Windows.FormImportarDados f = new AppLib.Windows.FormImportarDados();
                // f.Conexao = "Start";
                f.ShowDialog();
            }
        }

        private void barButtonItem97_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AppLib.Padrao.FormConexaoVisao f = AppLib.Padrao.FormConexaoVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void barButtonItem98_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AppLib.Padrao.FormSQLVisao f = AppLib.Padrao.FormSQLVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void barButtonItem99_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AppLib.Padrao.FormReportVisao f = AppLib.Padrao.FormReportVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void barButtonItem100_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AppLib.Padrao.FormExcelVisao f = AppLib.Padrao.FormExcelVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void barButtonItem101_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AppLib.Padrao.FormCuboVisao f = AppLib.Padrao.FormCuboVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        private void barButtonItem102_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AppLib.Padrao.FormDashboardVisao f = AppLib.Padrao.FormDashboardVisao.GetInstance();
            f.grid1.Conexao = "Start";
            f.Mostrar(this);
        }

        #endregion

        private void barButtonItem103_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CarregaOperacao(3, "btnOperacoes_Saidas");

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            PSPartLanca psPartLanca = new PSPartLanca();
            psPartLanca.MainForm = this;
            psPartLanca.Execute();
        }

        private void barButtonItem104_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.Globais.frmVisaoMotivo frm = new PS.Glb.New.Visao.Globais.frmVisaoMotivo(this);
            frm.Show();
        }

        private void barButtonItem105_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.Globais.frmVisaoStatus frm = new PS.Glb.New.Visao.Globais.frmVisaoStatus(this);
            frm.Show();
        }

        private void barButtonItem106_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PS.Glb.New.Visao.Globais.frmVisaoSituacao frm = new PS.Glb.New.Visao.Globais.frmVisaoSituacao(this);
            frm.Show();
        }

        private void FormPrincipal_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void FormPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.Shift && e.KeyCode == Keys.F12)
            {
                MDIPrincipal mdiForm = new MDIPrincipal();
                mdiForm.Show();
            }

            switch (e.KeyCode)
            {
                    case Keys.F1:
                        break;
                    case Keys.F10:
                        break;
                    case Keys.F11:
                        break;
                    case Keys.F12:
                        break;
                    case Keys.F13:
                        break;
                    case Keys.F14:
                        break;
                    case Keys.F15:
                        break;
                    case Keys.F16:
                        break;
                    case Keys.F17:
                        break;
                    case Keys.F18:
                        break;
                    case Keys.F19:
                        break;
                    case Keys.F2:
                        break;
                    case Keys.F20:
                        break;
                    case Keys.F21:
                        break;
                    case Keys.F22:
                        break;
                    case Keys.F23:
                        break;
                    case Keys.F24:
                        break;
                    case Keys.F3:
                        break;
                    case Keys.F4:
                        break;
                    case Keys.F5:
                        break;
                    case Keys.F6:
                        PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque frm = new PS.Glb.New.Processos.Operacao.frmConsultaSaldoEstoque();
                        frm.ShowDialog();
                        break;
                    case Keys.F7:
                        break;
                    case Keys.F8:
                        break;
                    case Keys.F9:
                        break;
              
                    default:
                        break;
            }
        }

        private void barButtonItem107_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private bool verificaAcessoMenu(string psPart)
        {
            bool permissao = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, @"SELECT ACESSO FROM GACESSOMENU INNER JOIN GUSUARIOPERFIL ON GACESSOMENU.CODPERFIL = GUSUARIOPERFIL.CODPERFIL AND GACESSOMENU.CODEMPRESA = GUSUARIOPERFIL.CODEMPRESA WHERE CODPSPART = ? AND GACESSOMENU.CODEMPRESA = ? AND GUSUARIOPERFIL.CODUSUARIO = ? ", new object[] { psPart, AppLib.Context.Empresa, AppLib.Context.Usuario }));
            if (permissao == false)
            {
                MessageBox.Show("Usuário sem permissão de acesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void barButtonItem73_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}
