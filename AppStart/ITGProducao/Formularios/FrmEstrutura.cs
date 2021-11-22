using DevExpress.XtraBars;
using DevExpress.XtraTabbedMdi;
using ITGProducao.Class;
using ITGProducao.Controles;
using ITGProducao.Filtros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ITGProducao.Formularios
{

    public partial class FrmEstrutura : Form
    {
        public bool edita = false;
        public bool editaOperacao = false;
        public string codEstrutura = string.Empty;
        public string CodRevEstrutura = string.Empty;
        public string descAux = string.Empty;
        public int codSequencialRoteiro_Antes = 0;
        public string operacaoSelecionada = "";
        public bool flagCarregaRevisao = false;

        private string mask;
        private List<string> tabelasFilhas = new List<string>();

        //Variaveis para NewLookup
        private NewLookup lookup;

        private DataTable dtTempoIntervalo;
        private DataTable dtOperacao;
        private DataTable dtRecursoEquipamentos;
        private DataTable dtRecursoMaodeObra;
        private DataTable dtRecursoFerramentas;
        private DataTable dtRecursoComponentes;

        public FrmEstrutura()
        {
            InitializeComponent();

            lookupproduto.txtcodigo.Leave += new System.EventHandler(lookupproduto_txtcodigo_Leave);
            lookupproduto.btnprocurar.Click += new System.EventHandler(lookupproduto_txtcodigo_Leave);
            lookupproduto.txtconteudo.Click += new System.EventHandler(lookupproduto_txtcodigo_Leave);

            lookupoperacao.txtcodigo.Leave += new System.EventHandler(lookupoperacao_txtcodigo_Leave);
            lookupoperacao.btnprocurar.Click += new System.EventHandler(lookupoperacao_txtcodigo_Leave);
            lookupoperacao.txtconteudo.Click += new System.EventHandler(lookupoperacao_txtcodigo_Leave);

            new PS.Glb.Class.Utilidades().criaCamposComplementares("PROTEIROESTRUTURACOMPL", tabCamposComplementares);
        }

        public FrmEstrutura(ref NewLookup lookup)
        {
            InitializeComponent();

            this.codEstrutura = lookup.txtcodigo.Text;
            this.edita = true;

            this.lookup = lookup;

            lookupproduto.txtcodigo.Leave += new System.EventHandler(lookupproduto_txtcodigo_Leave);
            lookupproduto.btnprocurar.Click += new System.EventHandler(lookupproduto_txtcodigo_Leave);
            lookupproduto.txtconteudo.Click += new System.EventHandler(lookupproduto_txtcodigo_Leave);

            lookupoperacao.txtcodigo.Leave += new System.EventHandler(lookupoperacao_txtcodigo_Leave);
            lookupoperacao.btnprocurar.Click += new System.EventHandler(lookupoperacao_txtcodigo_Leave);
            lookupoperacao.txtconteudo.Click += new System.EventHandler(lookupoperacao_txtcodigo_Leave);

            new PS.Glb.Class.Utilidades().criaCamposComplementares("PROTEIROESTRUTURACOMPL", tabCamposComplementares);
        }

        private void lookupoperacao_txtcodigo_Leave(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(lookupoperacao.ValorCodigoInterno))
            {
                //txtCodigo.Text = lookupoperacao.ValorCodigoInterno;
                //txtDescricao.Text = lookupoperacao.txtconteudo.Text;

                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM POPERACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupoperacao.ValorCodigoInterno.ToString() });
                if (dt.Rows.Count > 0)
                {
                    lookupcentrotrabalho.txtcodigo.Text = dt.Rows[0]["CODCTRABALHO"].ToString();
                    lookupcentrotrabalho.CarregaDescricao();
                    //lookupcentrotrabalho.Edita(false); 
                }
            }
        }

        private void lookupproduto_txtcodigo_Leave(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(lookupproduto.ValorCodigoInterno))
            {
                txtCodigo.Text = lookupproduto.ValorCodigoInterno;
                string descricaoProduto = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ? ", new object[] { AppLib.Context.Empresa, lookupproduto.ValorCodigoInterno }).ToString();

                if (lookupproduto.txtconteudo.Text.Length <= 255)
                {
                    
                    txtDescricao.Text = descricaoProduto;
                }
                else
                {
                    txtDescricao.Text = descricaoProduto.Substring(0, 255);
                }

                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODUNIDCONTROLE FROM VPRODUTO WHERE CODEMPRESA = ? AND CODPRODUTO = ? ", new object[] { AppLib.Context.Empresa, txtCodigo.Text });
                if (dt.Rows.Count > 0)
                {
                    lookupunidade.txtcodigo.Text = dt.Rows[0]["CODUNIDCONTROLE"].ToString();
                    lookupunidade.CarregaDescricao();
                    lookupunidade.Edita(false);
                }
            }
        }

        void CarregaCombos()
        {
            List<PS.Lib.ComboBoxItem> listTipo = new List<PS.Lib.ComboBoxItem>();

            listTipo.Add(new PS.Lib.ComboBoxItem());
            listTipo[0].ValueMember = "0";
            listTipo[0].DisplayMember = "";

            listTipo.Add(new PS.Lib.ComboBoxItem());
            listTipo[1].ValueMember = "A";
            listTipo[1].DisplayMember = "A - Acabado";

            listTipo.Add(new PS.Lib.ComboBoxItem());
            listTipo[2].ValueMember = "S";
            listTipo[2].DisplayMember = "S - Semi-Acabado";

            cmbTipo.DataSource = listTipo;
            cmbTipo.DisplayMember = "DisplayMember";
            cmbTipo.ValueMember = "ValueMember";

            cmbTipo.SelectedIndex = -1;

            List<PS.Lib.ComboBoxItem> listTipoOperacao = new List<PS.Lib.ComboBoxItem>();

            listTipoOperacao.Add(new PS.Lib.ComboBoxItem());
            listTipoOperacao[0].ValueMember = "0";
            listTipoOperacao[0].DisplayMember = "";

            listTipoOperacao.Add(new PS.Lib.ComboBoxItem());
            listTipoOperacao[1].ValueMember = "C";
            listTipoOperacao[1].DisplayMember = "C - Completo";

            listTipoOperacao.Add(new PS.Lib.ComboBoxItem());
            listTipoOperacao[2].ValueMember = "P";
            listTipoOperacao[2].DisplayMember = "P - Parcial";

            cmbtipoloteoperacao.DataSource = listTipoOperacao;
            cmbtipoloteoperacao.DisplayMember = "DisplayMember";
            cmbtipoloteoperacao.ValueMember = "ValueMember";

            cmbtipoloteoperacao.SelectedIndex = -1;

            List<PS.Lib.ComboBoxItem> listTipoExtra = new List<PS.Lib.ComboBoxItem>();

            listTipoExtra.Add(new PS.Lib.ComboBoxItem());
            listTipoExtra[0].ValueMember = "0";
            listTipoExtra[0].DisplayMember = "";

            listTipoExtra.Add(new PS.Lib.ComboBoxItem());
            listTipoExtra[1].ValueMember = "1";
            listTipoExtra[1].DisplayMember = "1 - Fixo";

            listTipoExtra.Add(new PS.Lib.ComboBoxItem());
            listTipoExtra[2].ValueMember = "2";
            listTipoExtra[2].DisplayMember = "2 - Proporcional";

            cmbtipotempoextra.DataSource = listTipoExtra;
            cmbtipotempoextra.DisplayMember = "DisplayMember";
            cmbtipotempoextra.ValueMember = "ValueMember";

            cmbtipotempoextra.SelectedIndex = -1;
            ////

            List<PS.Lib.ComboBoxItem> listTipoTempo = new List<PS.Lib.ComboBoxItem>();

            listTipoTempo.Add(new PS.Lib.ComboBoxItem());
            listTipoTempo[0].ValueMember = "0";
            listTipoTempo[0].DisplayMember = "";

            listTipoTempo.Add(new PS.Lib.ComboBoxItem());
            listTipoTempo[1].ValueMember = "1";
            listTipoTempo[1].DisplayMember = "1 - Fixo";

            listTipoTempo.Add(new PS.Lib.ComboBoxItem());
            listTipoTempo[2].ValueMember = "2";
            listTipoTempo[2].DisplayMember = "2 - Proporcional";

            listTipoTempo.Add(new PS.Lib.ComboBoxItem());
            listTipoTempo[3].ValueMember = "3";
            listTipoTempo[3].DisplayMember = "3 - Intervalo";

            cmbtipotempo.DataSource = listTipoTempo;
            cmbtipotempo.DisplayMember = "DisplayMember";
            cmbtipotempo.ValueMember = "ValueMember";

            cmbtipotempo.SelectedIndex = -1;
        }

        private void FrmEstrutura_Load(object sender, EventArgs e)
        {
            splitContainer5.Panel2Collapsed = true;
            tabControl2.TabPages.Remove(tabControl2.TabPages["tabCusto"]);
            tabControl2.TabPages.Remove(tabControl2.TabPages["tabLog"]);

            CarregaCombos();

            if (edita == true)
            {
                carregaCampos();
            }
            else
            {
                btnNovo.PerformClick();
                //Global gl = new Global();
                //gl.EnableTab(tabControl2.TabPages["tabCusto"], false); 
            }
        }


        private bool validacaoOperacao()
        {
            bool verifica = true;

            errorProvider.Clear();
            lookupcentrotrabalho.mensagemErrorProvider = "";
            lookupoperacao.mensagemErrorProvider = "";

            if (string.IsNullOrEmpty(lookupoperacao.txtconteudo.Text))
            {
                lookupoperacao.mensagemErrorProvider = "Favor Preencher a Operação";
                verifica = false;
            }
            else
            {
                lookupoperacao.mensagemErrorProvider = "";
            }

            if (string.IsNullOrEmpty(lookupcentrotrabalho.txtconteudo.Text))
            {

                lookupcentrotrabalho.mensagemErrorProvider = "Favor Preencher o Centro de Trabalho";
                verifica = false;
            }
            else
            {
                lookupcentrotrabalho.mensagemErrorProvider = "";
            }

            if ((cmbtipotempo.SelectedIndex == -1) || (cmbtipotempo.SelectedIndex == 0))
            {
                errorProvider.SetError(cmbtipotempo, "Selecione o Tipo de Tempo");
                verifica = false;
            }
            else
            {
                if (cmbtipotempo.SelectedValue.ToString() != "3") //INTERVALO
                {
                    if (string.IsNullOrEmpty(txttempo.Text))
                    {
                        errorProvider.SetError(txttempo, "Favor preencher o Tempo");
                        verifica = false;
                    }
                    //if (string.IsNullOrEmpty(txtsetup.Text))
                    //{
                    //    errorProvider.SetError(txtsetup, "Favor preencher o Setup");
                    //    verifica = false;
                    //}
                }
                else
                {
                    if (dtTempoIntervalo.Rows.Count < 2)
                    {
                        errorProvider.SetError(cmbtipotempo, "Insira as informações do tempo por Intervalo");
                        verifica = false;
                    }
                }
            }

            //if ((cmbtipotempoextra.SelectedIndex == -1) || (cmbtipotempoextra.SelectedIndex == 0))
            //{
            //    errorProvider.SetError(cmbtipotempoextra, "Selecione o Tipo de Tempo Extra");
            //    verifica = false;
            //}
            //else
            //{
            //if (string.IsNullOrEmpty(txtextra.Text))
            //{
            //    errorProvider.SetError(txtextra, "Favor preencher o Tempo");
            //    verifica = false;
            //}
            //}

            if ((cmbtipoloteoperacao.SelectedIndex == -1) || (cmbtipoloteoperacao.SelectedIndex == 0))
            {
                errorProvider.SetError(cmbtipoloteoperacao, "Selecione o Tipo do Lote da Operação");
                verifica = false;
            }
            else
            {
                if (cmbtipoloteoperacao.SelectedValue.ToString() == "P")
                {
                    if (string.IsNullOrEmpty(txtqtdeloteoperacao.Text))
                    {
                        errorProvider.SetError(txtqtdeloteoperacao, "Favor preencher a Quant. do Lote Operação");
                        verifica = false;
                    }
                }
            }

            if (editaOperacao == true)
            {
                string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, codSequencialRoteiro_Antes.ToString("000") }).ToString();
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? AND SEQOPERACAO NOT IN (SELECT SEQOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? AND CODOPERACAO = ?)", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text, txtrevisao.Text, Convert.ToInt16(txtSequencialOperacao.Text).ToString("000"), AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text, txtrevisao.Text, codSequencialRoteiro_Antes.ToString("000"), codOperacao });
                //DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT SEQOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text, txtrevisao.Text, txtSequencialOperacao.Text });
                if (dt.Rows.Count > 0)
                {
                    errorProvider.SetError(txtSequencialOperacao, "Este Sequencial da Operação já existe para este Roteiro");
                    verifica = false;
                }
            }

            return verifica;
        }
        private bool validacao()
        {
            bool verifica = true;

            errorProvider.Clear();

            lookupproduto.mensagemErrorProvider = "";
            lookupunidade.mensagemErrorProvider = "";
            lookupunidadegerencial.mensagemErrorProvider = "";
            lookupprodutorefugo.mensagemErrorProvider = "";
            lookupcentrotrabalho.mensagemErrorProvider = "";
            lookupoperacao.mensagemErrorProvider = "";

            if (string.IsNullOrEmpty(lookupproduto.txtconteudo.Text))
            {
                lookupproduto.mensagemErrorProvider = "Favor Selecionar um Produto";
                verifica = false;
            }
            else
            {
                lookupproduto.mensagemErrorProvider = "";
            }

            if (string.IsNullOrEmpty(lookupunidade.txtconteudo.Text))
            {
                lookupunidade.mensagemErrorProvider = "Favor Selecionar um Unidade de Medida";
                verifica = false;
            }
            else
            {
                lookupunidade.mensagemErrorProvider = "";
                lookupunidadegerencial.mensagemErrorProvider = "";
                if (!string.IsNullOrEmpty(lookupunidadegerencial.txtconteudo.Text))
                {
                    if (lookupunidadegerencial.ValorCodigoInterno == lookupunidade.ValorCodigoInterno)
                    {
                        lookupunidadegerencial.mensagemErrorProvider = "A Unidade de Medida deve ser diferente da Unidade de Medida Gerencial";
                        verifica = false;
                    }
                }
            }

            if (!string.IsNullOrEmpty(lookupunidadegerencial.txtconteudo.Text))
            {
                if (string.IsNullOrEmpty(txtfatorconversao.Text))
                {
                    errorProvider.SetError(txtfatorconversao, "Favor preencher o Fator Conversão");
                    verifica = false;
                }
            }
            else
            {
                lookupunidadegerencial.mensagemErrorProvider = "";
            }

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                errorProvider.SetError(txtCodigo, "Favor preencher o Código");
                verifica = false;
            }

            if (string.IsNullOrEmpty(txtDescricao.Text))
            {
                errorProvider.SetError(txtDescricao, "Favor preencher a Descrição");
                verifica = false;
            }

            if ((cmbTipo.SelectedIndex == -1) || (cmbTipo.SelectedIndex == 0))
            {
                errorProvider.SetError(cmbTipo, "Selecione o Tipo");
                verifica = false;
            }

            if (string.IsNullOrEmpty(txtloteminimo.Text))
            {
                errorProvider.SetError(txtloteminimo, "Favor preencher o Lote Mínimo");
                verifica = false;
            }

            if (string.IsNullOrEmpty(txtlotemultiplo.Text))
            {
                errorProvider.SetError(txtlotemultiplo, "Favor preencher o Lote Múltiplo");
                verifica = false;
            }

            if (chkgerarrefugo.Checked == true)
            {
                //if (string.IsNullOrEmpty(txtporcentagemrefugo.Text))
                //{
                //    errorProvider.SetError(txtporcentagemrefugo, "Favor preencher a Porcentagem do Refugo");
                //    verifica = false;
                //}

                if (string.IsNullOrEmpty(lookupprodutorefugo.txtconteudo.Text))
                {
                    lookupprodutorefugo.mensagemErrorProvider = "Favor Selecionar um Produto de Refugo";
                }
                else
                {
                    lookupprodutorefugo.mensagemErrorProvider = "";
                }
            }
            return verifica;

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            flagCarregaRevisao = false;
            if (Salvar() == true)
            {
                if (this.lookup == null)
                {
                    MessageBox.Show("Cadastro salvo com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    codEstrutura = txtCodigo.Text;
                    carregaCampos();
                }
                else
                {
                    codEstrutura = txtCodigo.Text;
                    carregaCampos();

                    lookup.txtcodigo.Text = txtCodigo.Text;
                    lookup.txtconteudo.Text = txtDescricao.Text.ToUpper();

                    this.Dispose();
                }
            }
        }

        private List<PS.Lib.ComboBoxItem> carregaRevisoesEstruturas()
        {
            List<PS.Lib.ComboBoxItem> revisoes = new List<PS.Lib.ComboBoxItem>();

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT REVESTRUTURA FROM PROTEIROESTRUTURA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? ORDER BY REVESTRUTURA", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura });
            if (dt.Rows.Count > 0)
            {
                int x = 1;

                revisoes.Add(new PS.Lib.ComboBoxItem());
                revisoes[0].ValueMember = "0";
                revisoes[0].DisplayMember = "";

                foreach (DataRow row in dt.Rows)
                {
                    revisoes.Add(new PS.Lib.ComboBoxItem());
                    revisoes[x].ValueMember = row["REVESTRUTURA"].ToString();
                    revisoes[x].DisplayMember = (row["REVESTRUTURA"].ToString());

                    x = x + 1;
                }
            }

            return revisoes;
        }

        private void carregaCampos()
        {

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PROTEIROESTRUTURA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura });
            if (dt.Rows.Count > 0)
            {
                flagCarregaRevisao = false;

                LimpaGrid_Operacao();
                LimpaGrid_TempoIntervalo();
                LimpaGrid_RecursoEquipamento();
                LimpaGrid_RecursoMaodeObra();
                LimpaGrid_RecursoFerramentas();
                LimpaGrid_RecursoComponentes();

                lookupproduto.txtcodigo.Text = dt.Rows[0]["CODPRODUTO"].ToString();
                lookupproduto.CarregaDescricao();

                txtCodigo.Text = dt.Rows[0]["CODESTRUTURA"].ToString();
                txtDescricao.Text = dt.Rows[0]["DESCESTRUTURA"].ToString();
                descAux = dt.Rows[0]["DESCESTRUTURAAUX"].ToString();

                cmbTipo.SelectedValue = dt.Rows[0]["TIPOESTRUTURA"].ToString();

                txtrevisao.Text = dt.Rows[0]["REVESTRUTURA"].ToString();

                lookupunidade.txtcodigo.Text = dt.Rows[0]["UNDCONTROLE"].ToString();
                lookupunidade.CarregaDescricao();
                lookupunidade.Edita(false);

                chkestruturainativa.Checked = Convert.ToBoolean(dt.Rows[0]["ATIVO"]);
                chkestruturabloqueada.Checked = Convert.ToBoolean(dt.Rows[0]["BLOQUEADO"]);

                txtloteminimo.Text = dt.Rows[0]["LOTEMINIMO"].ToString();
                txtlotemultiplo.Text = dt.Rows[0]["LOTEMULTIPLO"].ToString();

                lookupunidadegerencial.txtcodigo.Text = dt.Rows[0]["UNDGERENCIAL"].ToString();
                lookupunidadegerencial.CarregaDescricao();

                txtfatorconversao.Text = dt.Rows[0]["FATORUNDGERENCIAL"].ToString();

                chkgerarrefugo.Checked = Convert.ToBoolean(dt.Rows[0]["GERARREFUGO"]);

                lookupprodutorefugo.txtcodigo.Text = dt.Rows[0]["PRODUTOREFUGO"].ToString();
                lookupprodutorefugo.CarregaDescricao();

                txtporcentagemrefugo.Text = dt.Rows[0]["PORCENTAGEMREFUGO"].ToString();

                cmbnumerorevisao.Enabled = true;
                cmbnumerorevisao.DataSource = carregaRevisoesEstruturas();
                cmbnumerorevisao.DisplayMember = "DisplayMember";
                cmbnumerorevisao.ValueMember = "ValueMember";
                cmbnumerorevisao.SelectedValue = dt.Rows[0]["REVESTRUTURA"].ToString();

                txtcaracteristica.Text = dt.Rows[0]["CARACTERISTICA"].ToString();

                txtdatarevisao.Text = Convert.ToDateTime(dt.Rows[0]["DTREVESTRUTURA"].ToString()).ToString("dd/MM/yyyy");
                txtusuariobloqueio.Text = dt.Rows[0]["USUARIOBLOQUEIO"].ToString();

                if (string.IsNullOrEmpty(dt.Rows[0]["DTBLOQUEIOESTRUTURA"].ToString()))
                {
                    txtdatabloqueio.Text = "";
                }
                else
                {
                    txtdatabloqueio.Text = Convert.ToDateTime(dt.Rows[0]["DTBLOQUEIOESTRUTURA"].ToString()).ToString("dd/MM/yyyy");
                }

                //DTCRIAESTRUTURA
                //DTALTESTRUTURA
                //CUSTOESTRUTURA
                //DTCUSTOESTRUTURA
                //ARREDREFUGO
                //DTATIVOINATIVO

                txtrevisao.Enabled = false;
                txtCodigo.Enabled = false;

                chkestruturabloqueada.Enabled = false;
                chkestruturainativa.Enabled = false;

                txtdatarevisao.Enabled = false;

                //Roteiro

                lookupoperacao.Edita(false);
                lookupcentrotrabalho.Edita(false);
                txttempo.Enabled = false;
                txtsetup.Enabled = false;
                txtextra.Enabled = false;
                txtqtdeloteoperacao.Enabled = false;
                cmbtipotempoextra.Enabled = false;
                cmbtipoloteoperacao.Enabled = false;
                cmbtipotempo.Enabled = false;

                Global gl = new Global();
                gl.EnableTab(tabControl2.TabPages["tabRoteiro"], true);
                gl.EnableTab(tabControl2.TabPages["tabRecurso"], true);
                //gl.EnableTab(tabControl2.TabPages["tabCusto"], true);
                //gl.EnableTab(tabControl2.TabPages["tabLog"], true);

                CarregaGrid_Operacao();
                CarregaGrid_RecursoComponentes(1);
                CarregaGrid_RecursoEquipamentos(2);
                CarregaGrid_RecursoMaodeObra(3);
                CarregaGrid_RecursoFerramentas(4);

                btnnovarevisao.Enabled = true;
                btninativarrevisao.Enabled = true;
                btnliberarroteiro.Enabled = true;
                btnbloquearroteiro.Enabled = true;

                if (chkestruturabloqueada.Checked == true)
                {
                    btnliberarroteiro.Enabled = true;
                    btnbloquearroteiro.Enabled = false;
                }
                else
                {
                    btnliberarroteiro.Enabled = false;
                    btnbloquearroteiro.Enabled = true;
                }

                if (chkestruturainativa.Checked == true)
                {
                    btninativarrevisao.Text = "Inativar Revisão";
                }
                else
                {
                    btninativarrevisao.Text = "Ativar Revisão";
                }

                //if (chkestruturainativa.Checked == true || chkestruturabloqueada.Checked == true)
                if (chkestruturabloqueada.Checked == true)
                {
                    Bloqueia_DesbloqueiaTodosCampos(false);
                }
                else
                {
                    Bloqueia_DesbloqueiaTodosCampos(true);
                }


                flagCarregaRevisao = true;
                //Carregar Grid Tempo por Intervalo
            }

            carregaCamposComplementares();
        }

        private void carregaCamposComplementares()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM PROTEIROESTRUTURACOMPL WHERE CODESTRUTURA = ? AND CODEMPRESA = ? AND CODFILIAL = ? AND REVESTRUTURA = ?", new object[] { txtCodigo.Text, AppLib.Context.Empresa, AppLib.Context.Filial, txtrevisao.Text });
            DataTable dtColunas = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT NOMECAMPO FROM GTABCAMPOCOMPL WHERE CODENTIDADE = ? AND ATIVO = ? ORDER BY ORDEM", new object[] { "PROTEIROESTRUTURACOMPL", 1 });
            if (dt.Rows.Count > 0)
            {
                Control controle;
                for (int iColunas = 0; iColunas < dtColunas.Rows.Count; iColunas++)
                {
                    for (int i = 0; i < tabCamposComplementares.Controls.Count; i++)
                    {
                        controle = tabCamposComplementares.Controls[i];
                        if (controle.GetType().Name.Equals("TextEdit") || controle.GetType().Name.Equals("ComboBox") || controle.GetType().Name.Equals("DateEdit") || controle.GetType().Name.Equals("MemoEdit"))
                        {
                            if (controle.Name.Remove(0, 4) == dtColunas.Rows[iColunas]["NOMECAMPO"].ToString())
                            {
                                controle.Text = dt.Rows[0][dtColunas.Rows[iColunas]["NOMECAMPO"].ToString()].ToString();
                            }
                        }
                        else if (controle.GetType().Name.Equals("CheckEdit"))
                        {
                            if (controle.Name.Remove(0, 4) == dtColunas.Rows[iColunas]["NOMECAMPO"].ToString())
                            {
                                if (dt.Rows[0][dtColunas.Rows[iColunas]["NOMECAMPO"].ToString()].ToString() == "1")
                                {
                                    DevExpress.XtraEditors.CheckEdit ck = (DevExpress.XtraEditors.CheckEdit)controle;
                                    ck.Checked = true;
                                }
                                else
                                {
                                    DevExpress.XtraEditors.CheckEdit ck = (DevExpress.XtraEditors.CheckEdit)controle;
                                    ck.Checked = false;
                                }

                            }
                        }
                    }
                }

            }
        }

        private bool Existe_Estrutura()
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PROTEIROESTRUTURA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text });

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Salvar()
        {
            bool _salvar = false;

            if (validacao() == true)
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PROTEIROESTRUTURA");
                conn.BeginTransaction();
                try
                {
                    v.Set("CODEMPRESA", AppLib.Context.Empresa);
                    v.Set("CODFILIAL", AppLib.Context.Filial);
                    v.Set("CODESTRUTURA", txtCodigo.Text);
                    v.Set("DESCESTRUTURA", txtDescricao.Text);
                    v.Set("CARACTERISTICA", txtcaracteristica.Text);

                    v.Set("DESCESTRUTURAAUX", (string.IsNullOrEmpty(descAux)) ? null : descAux.ToString());

                    v.Set("TIPOESTRUTURA", cmbTipo.SelectedValue.ToString());
                    v.Set("REVESTRUTURA", Convert.ToInt16(txtrevisao.Text));

                    if (Existe_Estrutura() == true)
                    {
                        v.Set("DTALTESTRUTURA", conn.GetDateTime());
                    }
                    else
                    {
                        v.Set("DTALTESTRUTURA", conn.GetDateTime());
                        v.Set("DTREVESTRUTURA", conn.GetDateTime());
                        v.Set("DTCRIAESTRUTURA", conn.GetDateTime());
                    }

                    v.Set("CODPRODUTO", lookupproduto.ValorCodigoInterno.ToString());
                    v.Set("UNDCONTROLE", lookupunidade.ValorCodigoInterno.ToString());
                    v.Set("ATIVO", chkestruturainativa.Checked == true ? 1 : 0);
                    v.Set("BLOQUEADO", chkestruturabloqueada.Checked == true ? 1 : 0);

                    v.Set("LOTEMINIMO", Convert.ToDecimal(txtloteminimo.Text));
                    v.Set("LOTEMULTIPLO", Convert.ToDecimal(txtlotemultiplo.Text));

                    if (string.IsNullOrEmpty(lookupunidadegerencial.ValorCodigoInterno.ToString()))
                    {
                        v.Set("UNDGERENCIAL", null);
                    }
                    else
                    {
                        v.Set("UNDGERENCIAL", lookupunidadegerencial.ValorCodigoInterno.ToString());
                    }

                    v.Set("FATORUNDGERENCIAL", Convert.ToDecimal(txtfatorconversao.Text));

                    if (chkgerarrefugo.Checked == true)
                    {
                        v.Set("GERARREFUGO", chkgerarrefugo.Checked == true ? 1 : 0);
                        v.Set("PRODUTOREFUGO", lookupprodutorefugo.ValorCodigoInterno.ToString());
                        v.Set("PORCENTAGEMREFUGO", Convert.ToDecimal(txtporcentagemrefugo.Text));
                    }
                    else
                    {
                        v.Set("GERARREFUGO", chkgerarrefugo.Checked == true ? 1 : 0);
                        v.Set("PRODUTOREFUGO", null);
                        v.Set("PORCENTAGEMREFUGO", null);
                    }

                    v.Save();

                    if(SalvaCompl() == true)
                    {
                        conn.Commit();
                    }
                    else
                    {
                        throw new Exception("Erro ao gravar estrutura");
                    }

                    codEstrutura = txtCodigo.Text;
                    CodRevEstrutura = txtrevisao.Text;

                    carregaCampos();
                    this.edita = true;

                    _salvar = true;
                }
                catch (Exception ex)
                {
                    conn.Rollback();
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                _salvar = false;
            }

            return _salvar;
        }

        private bool SalvaCompl()
        {
            List<PS.Glb.Class.Parametro> param = new List<PS.Glb.Class.Parametro>();

            PS.Glb.Class.Parametro parametro = new PS.Glb.Class.Parametro();

            parametro.nomeParametro = "CODEMPRESA";
            parametro.valorParametro = AppLib.Context.Empresa.ToString();

            param.Add(parametro);

            parametro = new PS.Glb.Class.Parametro();

            parametro.nomeParametro = "CODFILIAL";
            parametro.valorParametro = AppLib.Context.Filial.ToString();

            param.Add(parametro);

            parametro = new PS.Glb.Class.Parametro();

            parametro.nomeParametro = "CODESTRUTURA";
            parametro.valorParametro = txtCodigo.Text;

            param.Add(parametro);

            parametro = new PS.Glb.Class.Parametro();

            parametro.nomeParametro = "REVESTRUTURA";
            parametro.valorParametro = txtrevisao.Text;

            param.Add(parametro);

            PS.Glb.Class.Utilidades util = new PS.Glb.Class.Utilidades();

            if (tabCamposComplementares.Controls.Count > 0)
            {
                util.salvaCamposComplementares(this, "PROTEIROESTRUTURACOMPL", tabCamposComplementares, param, AppLib.Context.poolConnection.Get("Start"));
                //bool retorno = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, "SELECT COUNT(CODESTRUTURA) FROM PROTEIROESTRUTURACOMPL WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text, txtrevisao.Text }));
                //if (retorno == false)
                //{
                //    AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO PROTEIROESTRUTURACOMPL (CODEMPRESA, CODFILIAL, CODESTRUTURA, REVESTRUTURA) VALUES (?, ?, ?, ?)", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text, txtrevisao.Text });
                //    string query = util.update(this, tabCamposComplementares, "PROTEIROESTRUTURACOMPL");
                //    if (!string.IsNullOrEmpty(query))
                //    {
                //        query = query.Remove(query.Length - 2, 1) + " WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?";
                //        AppLib.Context.poolConnection.Get("Start").ExecTransaction(query, new object[] { AppLib.Context.Empresa, AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text, txtrevisao.Text });
                //    }
                //}

                //// 16/10/2017 João Pedro
                ////PS.Glb.Class.Utilidades util = new PS.Glb.Class.Utilidades();
                //string sql = util.update(this, tabCamposComplementares, "PROTEIROESTRUTURACOMPL");
                //if (!string.IsNullOrEmpty(sql))
                //{
                //    sql = sql.Remove(sql.Length - 2, 1) + " WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?";
                //    AppLib.Context.poolConnection.Get("Start").ExecTransaction(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text, Convert.ToInt32(txtrevisao.Text) });
                //}
                return true;
            }
            else
            {
                return true;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (Salvar() == true)
                {

                    this.Dispose();
                }
            }
            else
            {
                btnSalvar.PerformClick();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cmbtipotempo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (editaOperacao == true)
            {
                if (cmbtipotempo.SelectedIndex > 0)
                {
                    if (cmbtipotempo.SelectedValue.ToString() == "3") //INTERVALO
                    {
                        splitContainer5.Panel2Collapsed = false;
                        txttempo.Text = "";
                        txttempo.Enabled = false;
                        txtsetup.Enabled = false;
                        btnIncluirTempoIntervalo.Enabled = true;
                        btnExcluirTempoIntervalo.Enabled = true;
                    }
                    else
                    {
                        if (dtTempoIntervalo.Rows.Count > 1)
                        {
                            if (MessageBox.Show("Deseja realmente alterar o tipo de tempo?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                LimpaGrid_TempoIntervalo();
                            }
                            else
                            {
                                cmbtipotempo.SelectedValue = "3"; //INTERVALO
                                return;
                            }
                        }
                        splitContainer5.Panel2Collapsed = true;
                        txttempo.Enabled = true;
                        txtsetup.Enabled = true;
                        btnIncluirTempoIntervalo.Enabled = false;
                        btnExcluirTempoIntervalo.Enabled = false;
                    }
                }
            }
            if (cmbtipotempo.SelectedIndex > 0)
            {
                if (cmbtipotempo.SelectedValue.ToString() == "3") //INTERVALO
                {
                    splitContainer5.Panel2Collapsed = false;
                    txttempo.Text = "";
                    txttempo.Enabled = false;
                    txtsetup.Enabled = false;
                    btnIncluirTempoIntervalo.Enabled = true;
                    btnExcluirTempoIntervalo.Enabled = true;
                }
                else
                {
                    splitContainer5.Panel2Collapsed = true;
                    txttempo.Enabled = true;
                    txtsetup.Enabled = true;
                    btnIncluirTempoIntervalo.Enabled = false;
                    btnExcluirTempoIntervalo.Enabled = false;
                }
            }
        }
        void LimpaGrid_TempoIntervalo()
        {
            string tabela = "PROTEIROPORINTERVALO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql = "SELECT TOP 0  PROTEIROPORINTERVALO.FAIXAINICIAL AS 'PROTEIROPORINTERVALO.FAIXAINICIAL', PROTEIROPORINTERVALO.FAIXAFINAL AS 'PROTEIROPORINTERVALO.FAIXAFINAL', PROTEIROPORINTERVALO.TEMPOOPERACAO AS 'PROTEIROPORINTERVALO.TEMPOOPERACAO' FROM PROTEIROPORINTERVALO WHERE PROTEIROPORINTERVALO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PROTEIROPORINTERVALO.CODFILIAL = " + AppLib.Context.Filial + " ORDER BY PROTEIROPORINTERVALO.SEQOPERACAO ";
                //string sql = "SELECT TOP 0  PROTEIROPORINTERVALO.FAIXAINICIAL, PROTEIROPORINTERVALO.FAIXAFINAL, PROTEIROPORINTERVALO.TEMPOOPERACAO FROM PROTEIROPORINTERVALO ORDER BY PROTEIROPORINTERVALO.SEQOPERACAO WHERE PROTEIROPORINTERVALO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PROTEIROPORINTERVALO.CODFILIAL = " + AppLib.Context.Filial + " AND PROTEIROPORINTERVALO.CODESTRUTURA = " + codEstrutura + " AND PROTEIROPORINTERVALO.REVESTRUTURA = " + CodRevEstrutura + " AND PROTEIROPORINTERVALO.CODOPERACAO = " + a + " AND PROTEIROPORINTERVALO.SEQOPERACAO = " + a;

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                gridTempoIntervalo.DataSource = null;
                gridViewTempoIntervalo.Columns.Clear();

                dtTempoIntervalo = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);
                gridTempoIntervalo.DataSource = dtTempoIntervalo;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?)", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewTempoIntervalo.Columns.Count; i++)
                {
                    //gridViewTempoIntervalo.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewTempoIntervalo.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewTempoIntervalo.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewTempoIntervalo.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LimpaGrid_RecursoFerramentas()
        {
            string tabela = "PROTEIRORECURSO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql = "SELECT TOP 0 PROTEIRORECURSO.CODGRUPORECURSO AS 'PROTEIRORECURSO.CODGRUPORECURSO ',PGRUPORECURSO.DESCGRUPORECURSO AS 'PGRUPORECURSO.DESCGRUPORECURSO' FROM PROTEIRORECURSO JOIN PGRUPORECURSO ON PROTEIRORECURSO.CODEMPRESA = PGRUPORECURSO.CODEMPRESA AND PROTEIRORECURSO.CODFILIAL = PGRUPORECURSO.CODFILIAL AND PROTEIRORECURSO.CODGRUPORECURSO = PGRUPORECURSO.CODGRUPORECURSO  WHERE PROTEIRORECURSO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PROTEIRORECURSO.CODFILIAL = " + AppLib.Context.Filial;

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                gridRecursoFerramentas.DataSource = null;
                gridViewRecursoFerramentas.Columns.Clear();
                dtRecursoFerramentas = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql); ;

                gridRecursoFerramentas.DataSource = dtRecursoFerramentas;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?)", new object[] { tabela, "PGRUPORECURSO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewRecursoFerramentas.Columns.Count; i++)
                {
                    //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewRecursoFerramentas.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewRecursoFerramentas.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewRecursoFerramentas.BestFitColumns();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void CarregaGrid_RecursoFerramentas(int tiporecurso)
        {

            string tabela = "PROTEIRORECURSO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                if (dtOperacao.Rows.Count > 0)
                {
                    DataRow row1 = gridViewOperacaoRecurso.GetDataRow(Convert.ToInt32(gridViewOperacaoRecurso.GetSelectedRows().GetValue(0).ToString()));

                    string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                    string sql = "SELECT PROTEIRORECURSO.CODGRUPORECURSO AS 'PROTEIRORECURSO.CODGRUPORECURSO ',PGRUPORECURSO.DESCGRUPORECURSO AS 'PGRUPORECURSO.DESCGRUPORECURSO',PROTEIRORECURSO.QTDRECURSO  AS 'PROTEIRORECURSO.QTDRECURSO' FROM PROTEIRORECURSO JOIN PGRUPORECURSO ON PROTEIRORECURSO.CODEMPRESA = PGRUPORECURSO.CODEMPRESA AND PROTEIRORECURSO.CODFILIAL = PGRUPORECURSO.CODFILIAL AND PROTEIRORECURSO.CODGRUPORECURSO = PGRUPORECURSO.CODGRUPORECURSO  WHERE PROTEIRORECURSO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PROTEIRORECURSO.CODFILIAL = " + AppLib.Context.Filial + " AND PROTEIRORECURSO.CODESTRUTURA = '" + codEstrutura + "' AND PROTEIRORECURSO.REVESTRUTURA = '" + CodRevEstrutura + "' AND PROTEIRORECURSO.TIPORECURSO = " + tiporecurso + " AND PROTEIRORECURSO.CODOPERACAO = '" + codOperacao + "' AND PROTEIRORECURSO.SEQOPERACAO = " + row1[0].ToString();

                    if (string.IsNullOrEmpty(sql))
                    {
                        MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                    if (!string.IsNullOrEmpty(filtroUsuario))
                    {

                        sql = sql + filtroUsuario;
                    }

                    gridRecursoFerramentas.DataSource = null;
                    gridViewRecursoFerramentas.Columns.Clear();
                    dtRecursoFerramentas = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                    gridRecursoFerramentas.DataSource = dtRecursoFerramentas;

                    DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?)", new object[] { tabela, "PGRUPORECURSO" });
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                    for (int i = 0; i < gridViewRecursoFerramentas.Columns.Count; i++)
                    {
                        //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                        dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                        DataRow result = dic.Rows.Find(new object[] { gridViewRecursoFerramentas.Columns[i].FieldName.ToString() });
                        if (result != null)
                        {
                            gridViewRecursoFerramentas.Columns[i].Caption = result["DESCRICAO"].ToString();
                        }
                    }
                    gridViewRecursoFerramentas.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void CarregaGrid_RecursoMaodeObra(int tiporecurso)
        {

            string tabela = "PROTEIRORECURSO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                if (dtOperacao.Rows.Count > 0)
                {
                    DataRow row1 = gridViewOperacaoRecurso.GetDataRow(Convert.ToInt32(gridViewOperacaoRecurso.GetSelectedRows().GetValue(0).ToString()));

                    string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                    string sql = "SELECT PROTEIRORECURSO.CODGRUPORECURSO AS 'PROTEIRORECURSO.CODGRUPORECURSO ',PGRUPORECURSO.DESCGRUPORECURSO AS 'PGRUPORECURSO.DESCGRUPORECURSO',PROTEIRORECURSO.QTDRECURSO  AS 'PROTEIRORECURSO.QTDRECURSO' FROM PROTEIRORECURSO JOIN PGRUPORECURSO ON PROTEIRORECURSO.CODEMPRESA = PGRUPORECURSO.CODEMPRESA AND PROTEIRORECURSO.CODFILIAL = PGRUPORECURSO.CODFILIAL AND PROTEIRORECURSO.CODGRUPORECURSO = PGRUPORECURSO.CODGRUPORECURSO  WHERE PROTEIRORECURSO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PROTEIRORECURSO.CODFILIAL = " + AppLib.Context.Filial + " AND PROTEIRORECURSO.CODESTRUTURA = '" + codEstrutura + "' AND PROTEIRORECURSO.REVESTRUTURA = '" + CodRevEstrutura + "' AND PROTEIRORECURSO.TIPORECURSO = " + tiporecurso + " AND PROTEIRORECURSO.CODOPERACAO = '" + codOperacao + "' AND PROTEIRORECURSO.SEQOPERACAO = " + row1[0].ToString();

                    if (string.IsNullOrEmpty(sql))
                    {
                        MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                    if (!string.IsNullOrEmpty(filtroUsuario))
                    {

                        sql = sql + filtroUsuario;
                    }

                    gridRecursoMaodeObra.DataSource = null;
                    gridViewRecursoMaodeObra.Columns.Clear();
                    dtRecursoMaodeObra = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql); ;

                    gridRecursoMaodeObra.DataSource = dtRecursoMaodeObra;

                    DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?)", new object[] { tabela, "PGRUPORECURSO" });
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                    for (int i = 0; i < gridViewRecursoMaodeObra.Columns.Count; i++)
                    {
                        //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                        dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                        DataRow result = dic.Rows.Find(new object[] { gridViewRecursoMaodeObra.Columns[i].FieldName.ToString() });
                        if (result != null)
                        {
                            gridViewRecursoMaodeObra.Columns[i].Caption = result["DESCRICAO"].ToString();
                        }
                    }
                    gridViewRecursoMaodeObra.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void CarregaGrid_RecursoEquipamentos(int tiporecurso)
        {

            string tabela = "PROTEIRORECURSO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                if (dtOperacao.Rows.Count > 0)
                {
                    DataRow row1 = gridViewOperacaoRecurso.GetDataRow(Convert.ToInt32(gridViewOperacaoRecurso.GetSelectedRows().GetValue(0).ToString()));

                    string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                    string sql = "SELECT PROTEIRORECURSO.CODGRUPORECURSO AS 'PROTEIRORECURSO.CODGRUPORECURSO ',PGRUPORECURSO.DESCGRUPORECURSO AS 'PGRUPORECURSO.DESCGRUPORECURSO' FROM PROTEIRORECURSO JOIN PGRUPORECURSO ON PROTEIRORECURSO.CODEMPRESA = PGRUPORECURSO.CODEMPRESA AND PROTEIRORECURSO.CODFILIAL = PGRUPORECURSO.CODFILIAL AND PROTEIRORECURSO.CODGRUPORECURSO = PGRUPORECURSO.CODGRUPORECURSO  WHERE PROTEIRORECURSO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PROTEIRORECURSO.CODFILIAL = " + AppLib.Context.Filial + " AND PROTEIRORECURSO.CODESTRUTURA = '" + codEstrutura + "' AND PROTEIRORECURSO.REVESTRUTURA = '" + CodRevEstrutura + "' AND PROTEIRORECURSO.TIPORECURSO = " + tiporecurso + " AND PROTEIRORECURSO.CODOPERACAO = '" + codOperacao + "' AND PROTEIRORECURSO.SEQOPERACAO = " + row1[0].ToString();

                    if (string.IsNullOrEmpty(sql))
                    {
                        MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                    if (!string.IsNullOrEmpty(filtroUsuario))
                    {

                        sql = sql + filtroUsuario;
                    }

                    gridRecursoEquipamentos.DataSource = null;
                    gridViewRecursoEquipamentos.Columns.Clear();
                    dtRecursoEquipamentos = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql); ;

                    gridRecursoEquipamentos.DataSource = dtRecursoEquipamentos;

                    DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?)", new object[] { tabela, "PGRUPORECURSO" });
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                    for (int i = 0; i < gridViewRecursoEquipamentos.Columns.Count; i++)
                    {
                        //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                        dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                        DataRow result = dic.Rows.Find(new object[] { gridViewRecursoEquipamentos.Columns[i].FieldName.ToString() });
                        if (result != null)
                        {
                            gridViewRecursoEquipamentos.Columns[i].Caption = result["DESCRICAO"].ToString();
                        }
                    }
                    gridViewRecursoEquipamentos.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void CarregaGrid_RecursoComponentes(int tiporecurso)
        {

            string tabela = "PROTEIRORECURSO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                if (dtOperacao.Rows.Count > 0)
                {
                    DataRow row1 = gridViewOperacaoRecurso.GetDataRow(Convert.ToInt32(gridViewOperacaoRecurso.GetSelectedRows().GetValue(0).ToString()));

                    string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                    string sql = "SELECT PROTEIRORECURSO.CODCOMPONENTE AS 'PROTEIRORECURSO.CODCOMPONENTE',VPRODUTO.NOME AS 'VPRODUTO.NOME',PROTEIRORECURSO.QTDCOMPONENTE AS 'PROTEIRORECURSO.QTDCOMPONENTE',PROTEIRORECURSO.UNDCOMPONENTE AS 'PROTEIRORECURSO.UNDCOMPONENTE' FROM PROTEIRORECURSO JOIN VPRODUTO ON PROTEIRORECURSO.CODEMPRESA = VPRODUTO.CODEMPRESA AND PROTEIRORECURSO.CODCOMPONENTE  = VPRODUTO.CODPRODUTO WHERE PROTEIRORECURSO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PROTEIRORECURSO.CODFILIAL = " + AppLib.Context.Filial + " AND PROTEIRORECURSO.CODESTRUTURA = '" + codEstrutura + "' AND PROTEIRORECURSO.REVESTRUTURA = '" + CodRevEstrutura + "' AND PROTEIRORECURSO.TIPORECURSO = " + tiporecurso + " AND PROTEIRORECURSO.CODOPERACAO = '" + codOperacao + "' AND PROTEIRORECURSO.SEQOPERACAO = " + row1[0].ToString();

                    if (string.IsNullOrEmpty(sql))
                    {
                        MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                    if (!string.IsNullOrEmpty(filtroUsuario))
                    {

                        sql = sql + filtroUsuario;
                    }

                    gridRecursoComponentes.DataSource = null;
                    gridViewRecursoComponentes.Columns.Clear();
                    dtRecursoComponentes = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql); ;

                    gridRecursoComponentes.DataSource = dtRecursoComponentes;

                    DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?)", new object[] { tabela, "VPRODUTO" });
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                    for (int i = 0; i < gridViewRecursoComponentes.Columns.Count; i++)
                    {
                        //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                        dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                        DataRow result = dic.Rows.Find(new object[] { gridViewRecursoComponentes.Columns[i].FieldName.ToString() });
                        if (result != null)
                        {
                            gridViewRecursoComponentes.Columns[i].Caption = result["DESCRICAO"].ToString();
                        }
                    }
                    gridViewRecursoComponentes.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LimpaGrid_RecursoComponentes()
        {
            string tabela = "PROTEIRORECURSO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql = "SELECT TOP 0 PROTEIRORECURSO.CODCOMPONENTE AS 'PROTEIRORECURSO.CODCOMPONENTE ',VPRODUTO.NOME AS 'VPRODUTO.NOME',PROTEIRORECURSO.QTDCOMPONENTE AS 'PROTEIRORECURSO.QTDCOMPONENTE',PROTEIRORECURSO.UNDCOMPONENTE AS 'PROTEIRORECURSO.UNDCOMPONENTE' FROM PROTEIRORECURSO JOIN VPRODUTO ON PROTEIRORECURSO.CODEMPRESA = VPRODUTO.CODEMPRESA AND PROTEIRORECURSO.CODCOMPONENTE  = VPRODUTO.CODPRODUTO WHERE PROTEIRORECURSO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PROTEIRORECURSO.CODFILIAL = " + AppLib.Context.Filial;

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                gridRecursoComponentes.DataSource = null;
                gridViewRecursoComponentes.Columns.Clear();
                dtRecursoComponentes = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql); ;

                gridRecursoComponentes.DataSource = dtRecursoComponentes;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?)", new object[] { tabela, "VPRODUTO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewRecursoComponentes.Columns.Count; i++)
                {
                    //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewRecursoComponentes.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewRecursoComponentes.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewRecursoComponentes.BestFitColumns();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void LimpaGrid_RecursoMaodeObra()
        {
            string tabela = "PROTEIRORECURSO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql = "SELECT TOP 0 PROTEIRORECURSO.CODGRUPORECURSO AS 'PROTEIRORECURSO.CODGRUPORECURSO ',PGRUPORECURSO.DESCGRUPORECURSO AS 'PGRUPORECURSO.DESCGRUPORECURSO',PROTEIRORECURSO.QTDRECURSO AS 'PROTEIRORECURSO.QTDRECURSO' FROM PROTEIRORECURSO JOIN PGRUPORECURSO ON PROTEIRORECURSO.CODEMPRESA = PGRUPORECURSO.CODEMPRESA AND PROTEIRORECURSO.CODFILIAL = PGRUPORECURSO.CODFILIAL AND PROTEIRORECURSO.CODGRUPORECURSO = PGRUPORECURSO.CODGRUPORECURSO  WHERE PROTEIRORECURSO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PROTEIRORECURSO.CODFILIAL = " + AppLib.Context.Filial;

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                gridRecursoMaodeObra.DataSource = null;
                gridViewRecursoMaodeObra.Columns.Clear();
                dtRecursoMaodeObra = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql); ;

                gridRecursoMaodeObra.DataSource = dtRecursoMaodeObra;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?)", new object[] { tabela, "PGRUPORECURSO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewRecursoMaodeObra.Columns.Count; i++)
                {
                    //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewRecursoMaodeObra.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewRecursoMaodeObra.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewRecursoMaodeObra.BestFitColumns();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LimpaGrid_RecursoEquipamento()
        {
            string tabela = "PROTEIRORECURSO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql = "SELECT TOP 0 PROTEIRORECURSO.CODGRUPORECURSO AS 'PROTEIRORECURSO.CODGRUPORECURSO ',PGRUPORECURSO.DESCGRUPORECURSO AS 'PGRUPORECURSO.DESCGRUPORECURSO' FROM PROTEIRORECURSO JOIN PGRUPORECURSO ON PROTEIRORECURSO.CODEMPRESA = PGRUPORECURSO.CODEMPRESA AND PROTEIRORECURSO.CODFILIAL = PGRUPORECURSO.CODFILIAL AND PROTEIRORECURSO.CODGRUPORECURSO = PGRUPORECURSO.CODGRUPORECURSO  WHERE PROTEIRORECURSO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PROTEIRORECURSO.CODFILIAL = " + AppLib.Context.Filial;

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                gridRecursoEquipamentos.DataSource = null;
                gridViewRecursoEquipamentos.Columns.Clear();
                dtRecursoEquipamentos = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql); ;

                gridRecursoEquipamentos.DataSource = dtRecursoEquipamentos;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?)", new object[] { tabela, "PGRUPORECURSO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewRecursoEquipamentos.Columns.Count; i++)
                {
                    //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewRecursoEquipamentos.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewRecursoEquipamentos.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewRecursoEquipamentos.BestFitColumns();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void LimpaGrid_Operacao()
        {
            string tabela = "PROTEIRO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {

                // string sql = "SELECT TOP 0 PROTEIRO.CODOPERACAO,PROTEIRO.SEQOPERACAO,POPERACAO.DESCOPERACAO FROM PROTEIRO JOIN POPERACAO ON PROTEIRO.CODEMPRESA = POPERACAO.CODEMPRESA AND PROTEIRO.CODFILIAL = POPERACAO.CODFILIAL AND PROTEIRO.CODOPERACAO = POPERACAO.CODOPERACAO WHERE PROTEIRO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PROTEIRO.CODFILIAL = " + AppLib.Context.Filial + " AND PROTEIRO.CODESTRUTURA = " + CodEstrutura + " AND PROTEIRO.REVESTRUTURA = " + CodRevEstrutura;
                string sql = "SELECT TOP 0 PROTEIRO.SEQOPERACAO AS 'PROTEIRO.SEQOPERACAO',POPERACAO.DESCOPERACAO AS 'POPERACAO.DESCOPERACAO' FROM PROTEIRO JOIN POPERACAO ON PROTEIRO.CODEMPRESA = POPERACAO.CODEMPRESA AND PROTEIRO.CODFILIAL = POPERACAO.CODFILIAL AND PROTEIRO.CODOPERACAO = POPERACAO.CODOPERACAO WHERE PROTEIRO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PROTEIRO.CODFILIAL = " + AppLib.Context.Filial;

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                gridOperacao.DataSource = null;
                gridViewOperacao.Columns.Clear();
                dtOperacao = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql); ;
                gridOperacao.DataSource = dtOperacao;

                gridOperacaoRecurso.DataSource = dtOperacao;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?)", new object[] { tabela, "POPERACAO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewOperacao.Columns.Count; i++)
                {
                    //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewOperacao.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewOperacao.Columns[i].Caption = result["DESCRICAO"].ToString();
                        gridViewOperacaoRecurso.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewOperacao.BestFitColumns();
                gridViewOperacaoRecurso.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void CarregaGrid_TempoPorIntervalo(string CodOperacao, string seqOperacao)
        {
            string tabela = "PROTEIROPORINTERVALO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql = "SELECT PROTEIROPORINTERVALO.FAIXAINICIAL AS 'PROTEIROPORINTERVALO.FAIXAINICIAL', PROTEIROPORINTERVALO.FAIXAFINAL AS 'PROTEIROPORINTERVALO.FAIXAFINAL', PROTEIROPORINTERVALO.TEMPOOPERACAO AS 'PROTEIROPORINTERVALO.TEMPOOPERACAO' FROM PROTEIROPORINTERVALO WHERE PROTEIROPORINTERVALO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PROTEIROPORINTERVALO.CODFILIAL = " + AppLib.Context.Filial + " AND PROTEIROPORINTERVALO.CODESTRUTURA = '" + codEstrutura + "' AND PROTEIROPORINTERVALO.REVESTRUTURA = " + CodRevEstrutura + " AND PROTEIROPORINTERVALO.CODOPERACAO = '" + CodOperacao + "' AND PROTEIROPORINTERVALO.SEQOPERACAO = " + seqOperacao + " ORDER BY PROTEIROPORINTERVALO.SEQFAIXAOPERACAO ";
                //+ "' AND PROTEIROPORINTERVALO.SEQOPERACAO = " + seqOperacao 
                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                gridTempoIntervalo.DataSource = null;
                gridViewTempoIntervalo.Columns.Clear();

                dtTempoIntervalo = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);
                //gridTempoIntervalo.DataSource = dtTempoIntervalo;
                DataView dv = dtTempoIntervalo.DefaultView;
                dv.Sort = "PROTEIROPORINTERVALO.FAIXAINICIAL asc";
                dtTempoIntervalo = dv.ToTable();
                gridTempoIntervalo.DataSource = dtTempoIntervalo;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?)", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewTempoIntervalo.Columns.Count; i++)
                {
                    //gridViewTempoIntervalo.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewTempoIntervalo.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewTempoIntervalo.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewTempoIntervalo.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void CarregaGrid_Operacao()
        {
            string tabela = "PROTEIRO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            List<string> tabelasFilhas = new List<string>();
            try
            {
                string sql = "SELECT PROTEIRO.SEQOPERACAO AS 'PROTEIRO.SEQOPERACAO', CASE POPERACAO.OPERACAOEXTERNA WHEN 1 THEN '(E) ' + POPERACAO.DESCOPERACAO  WHEN 0 THEN POPERACAO.DESCOPERACAO END AS 'POPERACAO.DESCOPERACAO'FROM PROTEIRO JOIN POPERACAO ON PROTEIRO.CODEMPRESA = POPERACAO.CODEMPRESA AND PROTEIRO.CODFILIAL = POPERACAO.CODFILIAL AND PROTEIRO.CODOPERACAO = POPERACAO.CODOPERACAO WHERE PROTEIRO.CODEMPRESA = " + AppLib.Context.Empresa + " AND PROTEIRO.CODFILIAL = " + AppLib.Context.Filial + " AND PROTEIRO.CODESTRUTURA = '" + codEstrutura + "' AND PROTEIRO.REVESTRUTURA = " + CodRevEstrutura;

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string filtroUsuario = new PS.Glb.Class.Utilidades().getFiltroUsuario(tabela);
                if (!string.IsNullOrEmpty(filtroUsuario))
                {

                    sql = sql + filtroUsuario;
                }

                gridOperacao.DataSource = null;
                gridViewOperacao.Columns.Clear();
                dtOperacao = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql); ;
                gridOperacao.DataSource = dtOperacao;
                gridOperacaoRecurso.DataSource = dtOperacao;

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA in (?,?)", new object[] { tabela, "POPERACAO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
                for (int i = 0; i < gridViewOperacao.Columns.Count; i++)
                {
                    //gridViewOperacao.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridViewOperacao.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridViewOperacao.Columns[i].Caption = result["DESCRICAO"].ToString();
                        gridViewOperacaoRecurso.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
                gridViewOperacao.BestFitColumns();
                gridViewOperacaoRecurso.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void IncluirRecursosDefaults(AppLib.Data.Connection conn, int seqOpe, string CodOpe)
        {
            try
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM POPERACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, CodOpe });

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {

                        AppLib.ORM.Jit t = new AppLib.ORM.Jit(conn, "PROTEIRORECURSO");

                        t.Set("CODEMPRESA", AppLib.Context.Empresa);
                        t.Set("CODFILIAL", AppLib.Context.Filial);
                        t.Set("CODESTRUTURA", txtCodigo.Text);
                        t.Set("REVESTRUTURA", Convert.ToInt16(txtrevisao.Text));
                        t.Set("SEQOPERACAO", seqOpe.ToString("000"));
                        t.Set("CODOPERACAO", lookupoperacao.ValorCodigoInterno.ToString());
                        t.Set("CODCOMPONENTE", null);
                        t.Set("UNDCOMPONENTE", null);
                        t.Set("QTDCOMPONENTE", null);
                        t.Set("QTDRECURSO", 1);

                        if (!string.IsNullOrEmpty(row["GRUPORECURSOEQ"].ToString()))
                        {
                            t.Set("TIPORECURSO", 2);
                            t.Set("CODGRUPORECURSO", row["GRUPORECURSOEQ"].ToString());
                            t.Set("CODRECROTEIRO", row["GRUPORECURSOEQ"].ToString());
                            t.Save();
                        }

                        if (!string.IsNullOrEmpty(row["GRUPORECURSOMO"].ToString()))
                        {
                            t.Set("TIPORECURSO", 3);
                            t.Set("CODGRUPORECURSO", row["GRUPORECURSOMO"].ToString());
                            t.Set("CODRECROTEIRO", row["GRUPORECURSOMO"].ToString());
                            t.Save();
                        }

                        if (!string.IsNullOrEmpty(row["GRUPORECURSOFE"].ToString()))
                        {
                            t.Set("TIPORECURSO", 4);
                            t.Set("CODGRUPORECURSO", row["GRUPORECURSOFE"].ToString());
                            t.Set("CODRECROTEIRO", row["GRUPORECURSOFE"].ToString());
                            t.Save();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private bool SalvarOperacao()
        {
            bool _salvar = false;
            int seqOpe = 0;

            if (validacaoOperacao() == true)
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PROTEIRO");
                conn.BeginTransaction();
                try
                {
                    if (editaOperacao == false)
                    {
                        v.Set("CODEMPRESA", AppLib.Context.Empresa);
                        v.Set("CODFILIAL", AppLib.Context.Filial);
                        v.Set("CODESTRUTURA", txtCodigo.Text);
                        v.Set("REVESTRUTURA", Convert.ToInt16(txtrevisao.Text));

                        DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT TOP 1 SEQOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? ORDER BY SEQOPERACAO DESC ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text, txtrevisao.Text });

                        if (dt.Rows.Count > 0)
                        {
                            v.Set("SEQOPERACAO", (Convert.ToInt16(dt.Rows[0]["SEQOPERACAO"].ToString()) + 10).ToString("000"));
                            seqOpe = (Convert.ToInt16(dt.Rows[0]["SEQOPERACAO"].ToString()) + 10);
                        }
                        else
                        {
                            v.Set("SEQOPERACAO", "010");
                            seqOpe = 10;
                        }

                        v.Set("OPERACAOEXTERNA", (Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT OPERACAOEXTERNA FROM POPERACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupoperacao.ValorCodigoInterno }).ToString()) == false ? 0 : 1));

                        v.Set("CODOPERACAO", lookupoperacao.ValorCodigoInterno);

                        v.Set("CODCTRABALHO", lookupcentrotrabalho.ValorCodigoInterno.ToString());

                        v.Set("TIPOTEMPO", cmbtipotempo.SelectedValue.ToString());

                        v.Set("TEMPOSETUP", Convert.ToDecimal(string.IsNullOrEmpty(txtsetup.Text.Replace(".",",")) ? "0" : txtsetup.Text.Replace(".", ",")));
                        v.Set("TEMPOOPERACAO", Convert.ToDecimal(string.IsNullOrEmpty(txttempo.Text.Replace(".", ",")) ? "0" : txttempo.Text.Replace(".", ",")));
                        v.Set("TEMPOEXTRA", Convert.ToDecimal(string.IsNullOrEmpty(txtextra.Text.Replace(".", ",")) ? "0" : txtextra.Text.Replace(".", ",")));

                        if ((cmbtipotempoextra.SelectedIndex == -1) || (cmbtipotempoextra.SelectedIndex == 0))
                        {
                            v.Set("TIPOTEMPOEXTRA", null);
                        }
                        else
                        {
                            v.Set("TIPOTEMPOEXTRA", cmbtipotempoextra.SelectedValue.ToString());
                        }

                        v.Set("LOTEPRODUZIDO", cmbtipoloteoperacao.SelectedValue.ToString());
                        v.Set("QTDLOTEPRODUZIDO", (string.IsNullOrEmpty(txtqtdeloteoperacao.Text) ? "0" : txtqtdeloteoperacao.Text));

                        v.Save();

                        IncluirRecursosDefaults(conn, seqOpe, lookupoperacao.ValorCodigoInterno.ToString());

                        if (cmbtipotempo.SelectedValue.ToString() == "3")
                        {
                            conn.ExecTransaction("DELETE FROM PROTEIROPORINTERVALO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? AND CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text, Convert.ToInt16(txtrevisao.Text), seqOpe.ToString("000"), lookupoperacao.ValorCodigoInterno });

                            int contaTempoIntervalo = 0;

                            foreach (DataRow row in dtTempoIntervalo.Rows)
                            {
                                AppLib.ORM.Jit t = new AppLib.ORM.Jit(conn, "PROTEIROPORINTERVALO");

                                t.Set("CODEMPRESA", AppLib.Context.Empresa);
                                t.Set("CODFILIAL", AppLib.Context.Filial);
                                t.Set("CODESTRUTURA", txtCodigo.Text);
                                t.Set("REVESTRUTURA", Convert.ToInt16(txtrevisao.Text));

                                t.Set("SEQOPERACAO", seqOpe.ToString("000"));
                                t.Set("CODOPERACAO", lookupoperacao.ValorCodigoInterno);

                                t.Set("SEQFAIXAOPERACAO", contaTempoIntervalo);
                                t.Set("FAIXAINICIAL", row["PROTEIROPORINTERVALO.FAIXAINICIAL"]);
                                t.Set("FAIXAFINAL", row["PROTEIROPORINTERVALO.FAIXAFINAL"]);
                                t.Set("TEMPOOPERACAO", row["PROTEIROPORINTERVALO.TEMPOOPERACAO"]);

                                contaTempoIntervalo = contaTempoIntervalo + 1;

                                t.Save();
                            }
                        }

                        conn.Commit();
                    }
                    else
                    {

                        int OpExterna = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT OPERACAOEXTERNA FROM POPERACAO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, lookupoperacao.ValorCodigoInterno.ToString() }).ToString()) == false ? 0 : 1;

                        string vTipoTempoExtra = "";

                        if ((cmbtipotempoextra.SelectedIndex == -1) || (cmbtipotempoextra.SelectedIndex == 0))
                        {
                            vTipoTempoExtra = "";
                        }
                        else
                        {
                            vTipoTempoExtra = cmbtipotempoextra.SelectedValue.ToString();
                        }

                        conn.ExecQuery(@"UPDATE PROTEIRO SET TIPOTEMPO = ?, SEQOPERACAO = ?, CODCTRABALHO = ? , TEMPOSETUP = ? , TEMPOOPERACAO = ? , TEMPOEXTRA = ?, TIPOTEMPOEXTRA = ?, LOTEPRODUZIDO = ?, QTDLOTEPRODUZIDO = ?, OPERACAOEXTERNA = ? WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO  = ? AND CODOPERACAO = ?  ", new object[] { cmbtipotempo.SelectedValue.ToString(), Convert.ToInt16(txtSequencialOperacao.Text).ToString("000"), lookupcentrotrabalho.ValorCodigoInterno.ToString(), Convert.ToDecimal(txtsetup.Text.Replace(".", ",")), Convert.ToDecimal(txttempo.Text.Replace(".", ",")), Convert.ToDecimal(txtextra.Text.Replace(".", ",")), (string.IsNullOrEmpty(vTipoTempoExtra) ? null : cmbtipotempoextra.SelectedValue.ToString()), cmbtipoloteoperacao.SelectedValue.ToString(), txtqtdeloteoperacao.Text, OpExterna, AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, Convert.ToInt16(CodRevEstrutura), codSequencialRoteiro_Antes.ToString("000"), lookupoperacao.ValorCodigoInterno.ToString() });

                        if (Convert.ToInt16(codSequencialRoteiro_Antes) != Convert.ToInt16(txtSequencialOperacao.Text))
                        {

                            conn.ExecQuery(@"UPDATE PROTEIROPORINTERVALO SET SEQOPERACAO = ? WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO  = ? AND CODOPERACAO = ?  ", new object[] { Convert.ToInt16(txtSequencialOperacao.Text).ToString("000"), AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, Convert.ToInt16(CodRevEstrutura), codSequencialRoteiro_Antes.ToString("000"), lookupoperacao.ValorCodigoInterno.ToString() });

                            conn.ExecQuery(@"UPDATE PROTEIRORECURSO SET SEQOPERACAO = ? WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO  = ? AND CODOPERACAO = ?  ", new object[] { Convert.ToInt16(txtSequencialOperacao.Text).ToString("000"), AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, Convert.ToInt16(CodRevEstrutura), codSequencialRoteiro_Antes.ToString("000"), lookupoperacao.ValorCodigoInterno.ToString() });
                        }

                        conn.ExecTransaction("DELETE FROM PROTEIROPORINTERVALO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? AND CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text, Convert.ToInt16(txtrevisao.Text), Convert.ToInt16(txtSequencialOperacao.Text).ToString("000"), lookupoperacao.ValorCodigoInterno });

                        if (cmbtipotempo.SelectedValue.ToString() == "3")
                        {
                            //conn.ExecTransaction("DELETE FROM PROTEIROPORINTERVALO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? AND CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text, Convert.ToInt16(txtrevisao.Text), Convert.ToInt16(txtSequencialOperacao.Text).ToString("000"), lookupoperacao.ValorCodigoInterno });

                            int contaTempoIntervalo = 0;

                            foreach (DataRow row in dtTempoIntervalo.Rows)
                            {
                                AppLib.ORM.Jit t = new AppLib.ORM.Jit(conn, "PROTEIROPORINTERVALO");

                                t.Set("CODEMPRESA", AppLib.Context.Empresa);
                                t.Set("CODFILIAL", AppLib.Context.Filial);
                                t.Set("CODESTRUTURA", txtCodigo.Text);
                                t.Set("REVESTRUTURA", Convert.ToInt16(txtrevisao.Text));

                                t.Set("SEQOPERACAO", Convert.ToInt16(txtSequencialOperacao.Text).ToString("000"));
                                t.Set("CODOPERACAO", lookupoperacao.ValorCodigoInterno);

                                t.Set("SEQFAIXAOPERACAO", contaTempoIntervalo);
                                t.Set("FAIXAINICIAL", row["PROTEIROPORINTERVALO.FAIXAINICIAL"]);
                                t.Set("FAIXAFINAL", row["PROTEIROPORINTERVALO.FAIXAFINAL"]);
                                t.Set("TEMPOOPERACAO", row["PROTEIROPORINTERVALO.TEMPOOPERACAO"]);

                                contaTempoIntervalo = contaTempoIntervalo + 1;

                                t.Save();
                            }
                        }

                        conn.Commit();

                        MessageBox.Show("Roteiro Alterado com Sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    editaOperacao = false;

                    CarregaGrid_Operacao();

                    //Limpa Roteiro
                    lookupoperacao.Clear();
                    lookupcentrotrabalho.Clear();

                    cmbtipotempo.SelectedValue = "2";
                    cmbtipotempoextra.SelectedIndex = -1;
                    codSequencialRoteiro_Antes = 0;

                    txttempo.Text = string.Empty;
                    txtsetup.Text = string.Empty;
                    txtextra.Text = string.Empty;
                    txtqtdeloteoperacao.Text = string.Empty;
                    cmbtipoloteoperacao.SelectedValue = "C";

                    //Desabilita Roteiro
                    lookupoperacao.Edita(false);
                    lookupcentrotrabalho.Edita(false);

                    cmbtipotempo.Enabled = false;
                    cmbtipotempoextra.Enabled = false;

                    txttempo.Enabled = false;
                    txtsetup.Enabled = false;
                    txtextra.Enabled = false;
                    txtSequencialOperacao.Enabled = false;
                    txtSequencialOperacao.Text = "";

                    cmbtipoloteoperacao.Enabled = false;
                    txtqtdeloteoperacao.Enabled = false;

                    btnIncluirOperacao.Visible = false;
                    btnCancelarOperacao.Visible = false;

                    LimpaGrid_TempoIntervalo();

                    btnIncluirTempoIntervalo.Enabled = false;
                    btnExcluirTempoIntervalo.Enabled = false;

                    CarregaGrid_RecursoComponentes(1);
                    CarregaGrid_RecursoEquipamentos(2);
                    CarregaGrid_RecursoMaodeObra(3);
                    CarregaGrid_RecursoFerramentas(4);

                    _salvar = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Rollback();
                }
            }
            else
            {
                _salvar = false;
            }

            return _salvar;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            lookupproduto.Clear();
            txtCodigo.Text = string.Empty;
            txtDescricao.Text = string.Empty;

            cmbTipo.SelectedIndex = -1;

            txtrevisao.Text = "1";
            lookupunidade.Clear();
            lookupunidade.Edita(true);

            txtlotemultiplo.Text = string.Empty;
            txtloteminimo.Text = string.Empty;
            lookupunidadegerencial.Clear();
            txtfatorconversao.Text = string.Empty;

            chkgerarrefugo.Checked = false;
            lookupprodutorefugo.Clear();
            txtporcentagemrefugo.Text = string.Empty;

            txtdatarevisao.Text = string.Empty;
            cmbnumerorevisao.SelectedIndex = -1;

            txtdatabloqueio.Text = string.Empty;
            txtusuariobloqueio.Text = string.Empty;

            txtCodigo.Enabled = true;

            chkestruturabloqueada.Checked = false;
            chkestruturabloqueada.Enabled = false;
            chkestruturainativa.Enabled = false;
            chkestruturainativa.Checked = false;
            txtdatarevisao.Enabled = false;

            btninativarrevisao.Text = "Ativar Revisão";

            //AbaRoteiro
            lookupoperacao.Clear();
            lookupcentrotrabalho.Clear();

            cmbtipotempo.SelectedValue = "2";
            cmbtipotempoextra.SelectedIndex = -1;

            txttempo.Text = string.Empty;
            txtsetup.Text = string.Empty;
            txtextra.Text = string.Empty;
            txtqtdeloteoperacao.Text = string.Empty;
            cmbtipoloteoperacao.SelectedValue = "C";

            LimpaGrid_TempoIntervalo();
            LimpaGrid_Operacao();
            LimpaGrid_RecursoEquipamento();
            LimpaGrid_RecursoMaodeObra();
            LimpaGrid_RecursoFerramentas();
            LimpaGrid_RecursoComponentes();

            Global gl = new Global();
            gl.EnableTab(tabControl2.TabPages["tabRoteiro"], false);
            gl.EnableTab(tabControl2.TabPages["tabRecurso"], false);
            //gl.EnableTab(tabControl2.TabPages["tabCusto"], false);
            //gl.EnableTab(tabControl2.TabPages["tabLog"], false);

            lookupcentrotrabalho.Edita(false);
            lookupoperacao.Edita(false);

            groupTempo.Enabled = false;
            groupLoteProducao.Enabled = false;

            btnnovarevisao.Enabled = false;
            btninativarrevisao.Enabled = false;
            btnliberarroteiro.Enabled = false;
            btnbloquearroteiro.Enabled = false;

            cmbnumerorevisao.DataSource = null;
            cmbnumerorevisao.SelectedIndex = -1;
            cmbnumerorevisao.Enabled = false;
        }

        private void Bloqueia_DesbloqueiaTodosCampos(bool validacao)
        {
            groupBox1.Enabled = validacao;
            groupBox2.Enabled = validacao;
            groupBox3.Enabled = validacao;
            groupBox4.Enabled = validacao;
            //groupBox5.Enabled = validacao;

            cmbnumerorevisao.Enabled = validacao;
            btnnovarevisao.Enabled = validacao;
            btninativarrevisao.Enabled = true;
            //tabControl2.Enabled = validacao;

            Global gl = new Global();
            gl.EnableTab(tabControl2.TabPages["tabRoteiro"], validacao);
            gl.EnableTab(tabControl2.TabPages["tabRecurso"], validacao);
        }

        void AtivaInativaEstrutura(AppLib.Data.Connection conn)
        {
            conn.ExecQuery(@"UPDATE PROTEIROESTRUTURA SET ATIVO = 0 WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura });

            if (chkestruturainativa.Checked == false)
            {
                btninativarrevisao.Text = "Inativar Revisão";
                //Bloqueia_DesbloqueiaTodosCampos(true);
                conn.ExecQuery(@"UPDATE PROTEIROESTRUTURA SET ATIVO = 1 WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, Convert.ToInt16(CodRevEstrutura) });
            }
            else
            {
                btninativarrevisao.Text = "Ativar Revisão";
                //Bloqueia_DesbloqueiaTodosCampos(false);
                conn.ExecQuery(@"UPDATE PROTEIROESTRUTURA SET ATIVO = 0 WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, Convert.ToInt16(CodRevEstrutura) });
            }
        }
        private void btninativarrevisao_Click(object sender, EventArgs e)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();

            try
            {
                if (chkestruturainativa.Checked == false)
                {
                    DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PROTEIROESTRUTURA WHERE ATIVO = 1 AND CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA <> ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, Convert.ToInt16(CodRevEstrutura) });
                    if (dt.Rows.Count > 0)
                    {
                        if (MessageBox.Show("Já existe uma revisão ativa para esta estrutura, deseja substituí-la?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            AtivaInativaEstrutura(conn);
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        AtivaInativaEstrutura(conn);
                    }
                }
                else
                {
                    AtivaInativaEstrutura(conn);
                }

                conn.Commit();
                carregaCampos();
            }
            catch (Exception)
            {
                conn.Rollback();
                MessageBox.Show("Erro ao alterar status da estrutura", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void NovaOperacao()
        {
            errorProvider.Clear();

            lookupcentrotrabalho.Clear();
            lookupoperacao.Clear();
            txttempo.Text = "";
            txtsetup.Text = "";
            txtextra.Text = "";
            txtSequencialOperacao.Text = "";
            txtqtdeloteoperacao.Text = "";
            cmbtipotempoextra.SelectedIndex = -1;
            codSequencialRoteiro_Antes = 0;

            lookupcentrotrabalho.Edita(true);
            lookupoperacao.Edita(true);

            groupTempo.Enabled = true;
            groupLoteProducao.Enabled = true;

            cmbtipoloteoperacao.SelectedValue = "C";
            cmbtipotempo.SelectedValue = "2";

            cmbtipotempo.Enabled = true;
            cmbtipotempoextra.Enabled = true;
            cmbtipoloteoperacao.Enabled = true;

            txttempo.Enabled = true;
            txtsetup.Enabled = true;
            txtqtdeloteoperacao.Enabled = false;

            editaOperacao = false;
            txtSequencialOperacao.Enabled = false;

            btnCancelarOperacao.Visible = true;
            btnIncluirOperacao.Visible = true;

            btnIncluirTempoIntervalo.Enabled = false;
            btnExcluirTempoIntervalo.Enabled = false;

            LimpaGrid_TempoIntervalo();
        }

        private void btnNovaOperacao_Click(object sender, EventArgs e)
        {
            if (editaOperacao == true)
            {
                if (MessageBox.Show("Deseja descartar as modificações realizadas?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    NovaOperacao();
                }
            }
            else
            {
                NovaOperacao();
            }
        }

        private void cmbtipotempoextra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbtipotempoextra.SelectedIndex > 0)
            {
                txtextra.Text = "";
                txtextra.Enabled = true;
            }
            else
            {
                txtextra.Text = "";
                txtextra.Enabled = false;
            }
        }

        private void cmbtipoloteoperacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtqtdeloteoperacao.Enabled = false;
            if (cmbtipoloteoperacao.SelectedIndex > 0)
            {
                if (cmbtipoloteoperacao.SelectedValue.ToString() == "P")
                {
                    txtqtdeloteoperacao.Text = "";
                    txtqtdeloteoperacao.Enabled = true;
                }
                else
                {
                    txtqtdeloteoperacao.Text = "";
                    txtqtdeloteoperacao.Enabled = false;
                }

            }


        }
        private void AtualizaOperacao()
        {
            try
            {
                if (gridViewOperacao.SelectedRowsCount == 0)
                {
                    return;
                }

                DataRow row1 = gridViewOperacao.GetDataRow(Convert.ToInt32(gridViewOperacao.GetSelectedRows().GetValue(0).ToString()));

                operacaoSelecionada = gridViewOperacao.GetSelectedRows().GetValue(0).ToString();

                string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? AND CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString(), codOperacao });
                if (dt.Rows.Count > 0)
                {
                    splitContainer5.Panel2Collapsed = true;
                    btnNovaOperacao.PerformClick();

                    editaOperacao = true;

                    codSequencialRoteiro_Antes = Convert.ToInt16(dt.Rows[0]["SEQOPERACAO"].ToString());

                    txtSequencialOperacao.Text = dt.Rows[0]["SEQOPERACAO"].ToString();

                    lookupoperacao.txtcodigo.Text = dt.Rows[0]["CODOPERACAO"].ToString();
                    lookupoperacao.CarregaDescricao();

                    lookupcentrotrabalho.txtcodigo.Text = dt.Rows[0]["CODCTRABALHO"].ToString();
                    lookupcentrotrabalho.CarregaDescricao();

                    cmbtipotempo.SelectedValue = dt.Rows[0]["TIPOTEMPO"].ToString();

                    cmbtipotempoextra.SelectedValue = dt.Rows[0]["TIPOTEMPOEXTRA"].ToString();

                    txttempo.Text = dt.Rows[0]["TEMPOOPERACAO"].ToString();
                    txtextra.Text = dt.Rows[0]["TEMPOEXTRA"].ToString();
                    txtsetup.Text = dt.Rows[0]["TEMPOSETUP"].ToString();
                    cmbtipoloteoperacao.SelectedValue = dt.Rows[0]["LOTEPRODUZIDO"].ToString();

                    txtSequencialOperacao.Enabled = true;
                    lookupoperacao.Edita(false);

                    txtqtdeloteoperacao.Text = dt.Rows[0]["QTDLOTEPRODUZIDO"].ToString();

                    editaOperacao = true;

                    btnCancelarOperacao.Visible = true;
                    btnIncluirOperacao.Visible = true;

                    btnIncluirTempoIntervalo.Enabled = true;
                    btnExcluirTempoIntervalo.Enabled = true;

                    CarregaGrid_TempoPorIntervalo(dt.Rows[0]["CODOPERACAO"].ToString(), dt.Rows[0]["SEQOPERACAO"].ToString());
                    //SEQOPERACAO
                    //OPERACAOEXTERNA
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Carregar Operação", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridOperacao_DoubleClick(object sender, EventArgs e)
        {

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (editaOperacao == true)
            {
                if (MessageBox.Show("Deseja descartar as modificações realizadas?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AtualizaOperacao();
                }
                else
                {
                    gridViewOperacao.ClearSelection();
                    gridViewOperacao.SelectRow(Convert.ToInt32(operacaoSelecionada));
                }
            }
            else
            {
                AtualizaOperacao();
            }
        }

        private void splitContainer7_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void toolStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            try
            {
                if (editaOperacao == true)
                {
                    MessageBox.Show("Finalize a alteração deste roteiro antes de excluí-lo", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DataRow row1 = gridViewOperacao.GetDataRow(Convert.ToInt32(gridViewOperacao.GetSelectedRows().GetValue(0).ToString()));

                        string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                        AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                        conn.BeginTransaction();
                        try
                        {
                            conn.ExecTransaction("DELETE FROM PROTEIROPORINTERVALO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? AND CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString(), codOperacao });

                            conn.ExecTransaction("DELETE FROM PROTEIRORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND CODOPERACAO = ? AND SEQOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, Convert.ToInt16(CodRevEstrutura), codOperacao, row1[0].ToString() });

                            conn.ExecTransaction("DELETE FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? AND CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString(), codOperacao });

                            conn.Commit();
                            MessageBox.Show("Cadastro Excluído com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpaRoteiro();
                            CarregaGrid_Operacao();
                        }
                        catch (Exception)
                        {
                            conn.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao Excluir Roteiro, contate o Administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpaRoteiro()
        {
            //Limpa Roteiro
            lookupoperacao.Clear();
            lookupcentrotrabalho.Clear();

            cmbtipotempo.SelectedValue = "2";
            cmbtipotempoextra.SelectedIndex = -1;
            codSequencialRoteiro_Antes = 0;

            txttempo.Text = string.Empty;
            txtsetup.Text = string.Empty;
            txtextra.Text = string.Empty;
            txtqtdeloteoperacao.Text = string.Empty;
            cmbtipoloteoperacao.SelectedValue = "C";

            LimpaGrid_TempoIntervalo();

            //Desabilita Roteiro
            lookupoperacao.Edita(false);
            lookupcentrotrabalho.Edita(false);

            cmbtipotempo.Enabled = false;
            cmbtipotempoextra.Enabled = false;

            txttempo.Enabled = false;
            txtsetup.Enabled = false;
            txtextra.Enabled = false;

            txtSequencialOperacao.Enabled = false;

            cmbtipoloteoperacao.Enabled = false;
            txtqtdeloteoperacao.Enabled = false;

            btnCancelarOperacao.Visible = false;
            btnIncluirOperacao.Visible = false;

            txtSequencialOperacao.Text = "";
            btnIncluirTempoIntervalo.Enabled = false;
            btnExcluirTempoIntervalo.Enabled = false;

            editaOperacao = false;
        }

        private void btnCancelarOperacao_Click(object sender, EventArgs e)
        {
            LimpaRoteiro();
        }

        private void btnIncluirOperacao_Click(object sender, EventArgs e)
        {
            SalvarOperacao();
        }

        private void btnIncluirTempoIntervalo_Click(object sender, EventArgs e)
        {
            FrmEstruturaTempoIntervalo frm = new FrmEstruturaTempoIntervalo(ref dtTempoIntervalo);
            frm.ShowDialog();
            //gridTempoIntervalo.DataSource = dtTempoIntervalo;
            DataView dv = dtTempoIntervalo.DefaultView;
            dv.Sort = "PROTEIROPORINTERVALO.FAIXAINICIAL asc";
            dtTempoIntervalo = dv.ToTable();
            gridTempoIntervalo.DataSource = dtTempoIntervalo;
        }

        private void btndescricaoauxiliar_Click(object sender, EventArgs e)
        {
            FrmEstruturaDescricaoAuxiliar frm = new FrmEstruturaDescricaoAuxiliar(ref this.descAux);
            frm.ShowDialog();
            this.descAux = frm.descAux;
        }

        private void btnExcluirTempoIntervalo_Click(object sender, EventArgs e)
        {
            try
            {
                if (editaOperacao == true)
                {
                    if (dtTempoIntervalo.Rows.Count > 0)
                    {
                        if (MessageBox.Show("Será excluído o último registro de tempo por intervalo, deseja continuar?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            dtTempoIntervalo.Rows.RemoveAt(dtTempoIntervalo.Rows.Count - 1);
                            DataView dv = dtTempoIntervalo.DefaultView;
                            dv.Sort = "PROTEIROPORINTERVALO.FAIXAINICIAL asc";
                            dtTempoIntervalo = dv.ToTable();
                            gridTempoIntervalo.DataSource = dtTempoIntervalo;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao Excluir Tempo de Intervalo, contate o Administrador.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM PORDEM WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, Convert.ToInt16(CodRevEstrutura) });
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Esta Estrutura esta vinculada a uma Ordem de Produção e não pode ser excluída.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                    conn.BeginTransaction();
                    try
                    {
                        conn.ExecTransaction("DELETE FROM PROTEIRORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, Convert.ToInt16(CodRevEstrutura) });

                        conn.ExecTransaction("DELETE FROM PROTEIROPORINTERVALO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, Convert.ToInt16(CodRevEstrutura) });

                        conn.ExecTransaction("DELETE FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura });

                        conn.ExecTransaction("DELETE FROM PROTEIROESTRUTURA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, Convert.ToInt16(CodRevEstrutura) });

                        conn.Commit();
                        MessageBox.Show("Cadastro Excluído com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                    }
                    catch (Exception ex)
                    {
                        conn.Rollback();
                        MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void gridOperacao_Click(object sender, EventArgs e)
        {
            if (editaOperacao == true)
            {
                if (MessageBox.Show("Deseja descartar as modificações realizadas?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    editaOperacao = false;
                    AtualizaOperacao();
                }
                else
                {
                    gridViewOperacao.ClearSelection();

                    gridViewOperacao.SelectRow(Convert.ToInt32(operacaoSelecionada));
                }
            }
            else
            {
                AtualizaOperacao();
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row1 = gridViewOperacaoRecurso.GetDataRow(Convert.ToInt32(gridViewOperacaoRecurso.GetSelectedRows().GetValue(0).ToString()));

                string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? AND CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString(), codOperacao });
                if (dt.Rows.Count > 0)
                {
                    FrmEstruturaRecursoComponentes frm = new FrmEstruturaRecursoComponentes(codEstrutura, CodRevEstrutura, Convert.ToInt16(dt.Rows[0]["SEQOPERACAO"]).ToString("000"), dt.Rows[0]["CODOPERACAO"].ToString(), FrmEstruturaRecursoComponentes.TipoRecurso.Componente, false, "");
                    frm.ShowDialog();
                    CarregaGrid_RecursoComponentes(1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void AtualizaRecursoComponentes()
        {
            try
            {
                DataRow row1 = gridViewOperacaoRecurso.GetDataRow(Convert.ToInt32(gridViewOperacaoRecurso.GetSelectedRows().GetValue(0).ToString()));

                DataRow row2 = gridViewRecursoComponentes.GetDataRow(Convert.ToInt32(gridViewRecursoComponentes.GetSelectedRows().GetValue(0).ToString()));

                string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                FrmEstruturaRecursoComponentes frm = new FrmEstruturaRecursoComponentes(codEstrutura, CodRevEstrutura, row1[0].ToString(), codOperacao, FrmEstruturaRecursoComponentes.TipoRecurso.Componente, true, row2[0].ToString());
                frm.ShowDialog();
                CarregaGrid_RecursoComponentes(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtualizaRecursoEquipamentos()
        {
            try
            {
                DataRow row1 = gridViewOperacaoRecurso.GetDataRow(Convert.ToInt32(gridViewOperacaoRecurso.GetSelectedRows().GetValue(0).ToString()));

                DataRow row2 = gridViewRecursoEquipamentos.GetDataRow(Convert.ToInt32(gridViewRecursoEquipamentos.GetSelectedRows().GetValue(0).ToString()));

                string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                string CentroTrabalho = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODCTRABALHO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? AND CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString(), codOperacao }).ToString();

                FrmEstruturaRecurso frm = new FrmEstruturaRecurso(codEstrutura, CodRevEstrutura, row1[0].ToString(), codOperacao, FrmEstruturaRecurso.TipoRecurso.Equipamento, true, row2[0].ToString(), CentroTrabalho);
                frm.ShowDialog();
                CarregaGrid_RecursoEquipamentos(2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AtualizaRecursoFerramentas()
        {
            try
            {
                DataRow row1 = gridViewOperacaoRecurso.GetDataRow(Convert.ToInt32(gridViewOperacaoRecurso.GetSelectedRows().GetValue(0).ToString()));

                DataRow row2 = gridViewRecursoFerramentas.GetDataRow(Convert.ToInt32(gridViewRecursoFerramentas.GetSelectedRows().GetValue(0).ToString()));

                string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                string CentroTrabalho = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODCTRABALHO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? AND CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString(), codOperacao }).ToString();

                FrmEstruturaRecurso frm = new FrmEstruturaRecurso(codEstrutura, CodRevEstrutura, row1[0].ToString(), codOperacao, FrmEstruturaRecurso.TipoRecurso.Ferramenta, true, row2[0].ToString(), CentroTrabalho);
                frm.ShowDialog();
                CarregaGrid_RecursoFerramentas(4);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtualizaRecursoMaodeObra()
        {
            try
            {
                DataRow row1 = gridViewOperacaoRecurso.GetDataRow(Convert.ToInt32(gridViewOperacaoRecurso.GetSelectedRows().GetValue(0).ToString()));

                DataRow row2 = gridViewRecursoMaodeObra.GetDataRow(Convert.ToInt32(gridViewRecursoMaodeObra.GetSelectedRows().GetValue(0).ToString()));

                string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                string CentroTrabalho = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODCTRABALHO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? AND CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString(), codOperacao }).ToString();

                FrmEstruturaRecurso frm = new FrmEstruturaRecurso(codEstrutura, CodRevEstrutura, row1[0].ToString(), codOperacao, FrmEstruturaRecurso.TipoRecurso.MaodeObra, true, row2[0].ToString(), CentroTrabalho);
                frm.ShowDialog();
                CarregaGrid_RecursoMaodeObra(3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            AtualizaRecursoComponentes();
        }

        private void gridOperacaoRecurso_Click(object sender, EventArgs e)
        {
            //switch (tabRecursoOperacao.SelectedTab.Name)
            //{
            //    case "tabComponentes":
            //        CarregaGrid_RecursoComponentes(1);
            //        break;

            //    case "tabEquipamentos":
            //        CarregaGrid_RecursoEquipamentos(2);
            //        break;

            //    case "tabMaodeObra":
            //        CarregaGrid_RecursoMaodeObra(3);
            //        break;

            //    case "tabFerramentas":
            //        CarregaGrid_RecursoFerramentas(4);
            //        break;
            //}
            CarregaGrid_RecursoComponentes(1);
            CarregaGrid_RecursoEquipamentos(2);
            CarregaGrid_RecursoMaodeObra(3);
            CarregaGrid_RecursoFerramentas(4);

        }

        private void gridRecursoComponentes_DoubleClick(object sender, EventArgs e)
        {
            AtualizaRecursoComponentes();
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                conn.BeginTransaction();
                try
                {
                    DataRow row1 = gridViewOperacaoRecurso.GetDataRow(Convert.ToInt32(gridViewOperacaoRecurso.GetSelectedRows().GetValue(0).ToString()));

                    DataRow row2 = gridViewRecursoComponentes.GetDataRow(Convert.ToInt32(gridViewRecursoComponentes.GetSelectedRows().GetValue(0).ToString()));

                    string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                    conn.ExecTransaction("DELETE FROM PROTEIRORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND TIPORECURSO = ? AND CODRECROTEIRO = ? AND CODOPERACAO = ? AND SEQOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, Convert.ToInt16(CodRevEstrutura), Convert.ToInt16(FrmEstruturaRecursoComponentes.TipoRecurso.Componente), row2[0].ToString(), codOperacao, row1[0].ToString() });

                    conn.Commit();
                    MessageBox.Show("Cadastro Excluído com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregaGrid_RecursoComponentes(1);
                }
                catch (Exception)
                {
                    conn.Rollback();
                    throw;
                }
            }
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row1 = gridViewOperacaoRecurso.GetDataRow(Convert.ToInt32(gridViewOperacaoRecurso.GetSelectedRows().GetValue(0).ToString()));

                string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                string CentroTrabalho = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODCTRABALHO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? AND CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString(), codOperacao }).ToString();

                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? AND CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString(), codOperacao });
                if (dt.Rows.Count > 0)
                {
                    FrmEstruturaRecurso frm = new FrmEstruturaRecurso(codEstrutura, CodRevEstrutura, Convert.ToInt16(dt.Rows[0]["SEQOPERACAO"]).ToString("000"), dt.Rows[0]["CODOPERACAO"].ToString(), FrmEstruturaRecurso.TipoRecurso.Equipamento, false, "", CentroTrabalho);
                    frm.ShowDialog();
                    CarregaGrid_RecursoEquipamentos(2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            AtualizaRecursoEquipamentos();
        }

        private void gridRecursoEquipamentos_DoubleClick(object sender, EventArgs e)
        {
            AtualizaRecursoEquipamentos();
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                conn.BeginTransaction();
                try
                {
                    DataRow row1 = gridViewOperacaoRecurso.GetDataRow(Convert.ToInt32(gridViewOperacaoRecurso.GetSelectedRows().GetValue(0).ToString()));

                    DataRow row2 = gridViewRecursoEquipamentos.GetDataRow(Convert.ToInt32(gridViewRecursoEquipamentos.GetSelectedRows().GetValue(0).ToString()));

                    string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                    conn.ExecTransaction("DELETE FROM PROTEIRORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND TIPORECURSO = ? AND CODRECROTEIRO = ? AND CODOPERACAO = ? AND SEQOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, Convert.ToInt16(CodRevEstrutura), Convert.ToInt16(FrmEstruturaRecurso.TipoRecurso.Equipamento), row2[0].ToString(), codOperacao, row1[0].ToString() });

                    conn.Commit();
                    MessageBox.Show("Cadastro Excluído com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregaGrid_RecursoEquipamentos(2);
                }
                catch (Exception)
                {
                    conn.Rollback();
                    throw;
                }
            }
        }

        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row1 = gridViewOperacaoRecurso.GetDataRow(Convert.ToInt32(gridViewOperacaoRecurso.GetSelectedRows().GetValue(0).ToString()));

                string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                string CentroTrabalho = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODCTRABALHO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? AND CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString(), codOperacao }).ToString();

                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? AND CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString(), codOperacao });
                if (dt.Rows.Count > 0)
                {
                    FrmEstruturaRecurso frm = new FrmEstruturaRecurso(codEstrutura, CodRevEstrutura, Convert.ToInt16(dt.Rows[0]["SEQOPERACAO"]).ToString("000"), dt.Rows[0]["CODOPERACAO"].ToString(), FrmEstruturaRecurso.TipoRecurso.MaodeObra, false, "", CentroTrabalho);
                    frm.ShowDialog();
                    CarregaGrid_RecursoMaodeObra(3);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            AtualizaRecursoMaodeObra();
        }

        private void gridRecursoMaodeObra_DoubleClick(object sender, EventArgs e)
        {
            AtualizaRecursoMaodeObra();
        }

        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                conn.BeginTransaction();
                try
                {
                    DataRow row1 = gridViewOperacaoRecurso.GetDataRow(Convert.ToInt32(gridViewOperacaoRecurso.GetSelectedRows().GetValue(0).ToString()));

                    DataRow row2 = gridViewRecursoMaodeObra.GetDataRow(Convert.ToInt32(gridViewRecursoMaodeObra.GetSelectedRows().GetValue(0).ToString()));

                    string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                    conn.ExecTransaction("DELETE FROM PROTEIRORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND TIPORECURSO = ? AND CODRECROTEIRO = ? AND CODOPERACAO = ? AND SEQOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, Convert.ToInt16(CodRevEstrutura), Convert.ToInt16(FrmEstruturaRecurso.TipoRecurso.MaodeObra), row2[0].ToString(), codOperacao, row1[0].ToString() });

                    conn.Commit();
                    MessageBox.Show("Cadastro Excluído com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregaGrid_RecursoMaodeObra(3);
                }
                catch (Exception)
                {
                    conn.Rollback();
                    throw;
                }
            }
        }

        private void toolStripButton30_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row1 = gridViewOperacaoRecurso.GetDataRow(Convert.ToInt32(gridViewOperacaoRecurso.GetSelectedRows().GetValue(0).ToString()));

                string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                string CentroTrabalho = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODCTRABALHO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? AND CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString(), codOperacao }).ToString();

                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? AND CODOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString(), codOperacao });
                if (dt.Rows.Count > 0)
                {
                    FrmEstruturaRecurso frm = new FrmEstruturaRecurso(codEstrutura, CodRevEstrutura, Convert.ToInt16(dt.Rows[0]["SEQOPERACAO"]).ToString("000"), dt.Rows[0]["CODOPERACAO"].ToString(), FrmEstruturaRecurso.TipoRecurso.Ferramenta, false, "", CentroTrabalho);
                    frm.ShowDialog();
                    CarregaGrid_RecursoFerramentas(4);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton32_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente excluir este registro?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                conn.BeginTransaction();
                try
                {
                    DataRow row1 = gridViewOperacaoRecurso.GetDataRow(Convert.ToInt32(gridViewOperacaoRecurso.GetSelectedRows().GetValue(0).ToString()));

                    DataRow row2 = gridViewRecursoFerramentas.GetDataRow(Convert.ToInt32(gridViewRecursoFerramentas.GetSelectedRows().GetValue(0).ToString()));

                    string codOperacao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODOPERACAO FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND SEQOPERACAO = ? ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura, row1[0].ToString() }).ToString();

                    conn.ExecTransaction("DELETE FROM PROTEIRORECURSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ? AND TIPORECURSO = ? AND CODRECROTEIRO = ? AND CODOPERACAO = ? AND SEQOPERACAO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, Convert.ToInt16(CodRevEstrutura), Convert.ToInt16(FrmEstruturaRecurso.TipoRecurso.Ferramenta), row2[0].ToString(), codOperacao, row1[0].ToString() });

                    conn.Commit();
                    MessageBox.Show("Cadastro Excluído com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregaGrid_RecursoFerramentas(4);
                }
                catch (Exception ex)
                {
                    conn.Rollback();
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnnovarevisao_Click(object sender, EventArgs e)
        {
            if ((cmbnumerorevisao.SelectedIndex == -1) || (cmbnumerorevisao.SelectedIndex == 0))
            {
                MessageBox.Show("Selecione uma revisão para duplicar!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show("Deseja realmente gerar uma nova revisão?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                    conn.BeginTransaction();
                    try
                    {
                        string UltimaRevisao = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT TOP 1 REVESTRUTURA FROM PROTEIRO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? ORDER BY REVESTRUTURA DESC ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura }).ToString();

                        int NovaRevisao = Convert.ToInt16(UltimaRevisao) + 1;

                        conn.ExecTransaction("insert into PROTEIROESTRUTURA select CODEMPRESA,CODFILIAL,CODESTRUTURA,DESCESTRUTURA,DESCESTRUTURAAUX,TIPOESTRUTURA,'" + NovaRevisao.ToString() + "' AS REVESTRUTURA,DTREVESTRUTURA,CODPRODUTO,UNDCONTROLE,ATIVO,BLOQUEADO,DTCRIAESTRUTURA,DTALTESTRUTURA,DTBLOQUEIOESTRUTURA,USUARIOBLOQUEIO,LOTEMINIMO,LOTEMULTIPLO,CUSTOESTRUTURA,DTCUSTOESTRUTURA,UNDGERENCIAL,FATORUNDGERENCIAL,GERARREFUGO,PRODUTOREFUGO,PORCENTAGEMREFUGO,ARREDREFUGO,DTATIVOINATIVO, '' from PROTEIROESTRUTURA where  CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text, cmbnumerorevisao.SelectedValue });

                        conn.ExecTransaction("insert into PROTEIRO select CODEMPRESA,CODFILIAL,CODESTRUTURA,'" + NovaRevisao.ToString() + "' AS REVESTRUTURA,SEQOPERACAO,CODOPERACAO,CODCTRABALHO,TIPOTEMPO,TEMPOSETUP,TEMPOOPERACAO,TEMPOEXTRA,TIPOTEMPOEXTRA,LOTEPRODUZIDO,QTDLOTEPRODUZIDO,OPERACAOEXTERNA from PROTEIRO where  CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text, cmbnumerorevisao.SelectedValue });

                        conn.ExecTransaction("insert into PROTEIROPORINTERVALO select CODEMPRESA,CODFILIAL,CODESTRUTURA,'" + NovaRevisao.ToString() + "' AS REVESTRUTURA,SEQOPERACAO,CODOPERACAO,SEQFAIXAOPERACAO,FAIXAINICIAL,FAIXAFINAL,TEMPOOPERACAO from PROTEIROPORINTERVALO where  CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text, cmbnumerorevisao.SelectedValue });

                        conn.ExecTransaction("insert into PROTEIRORECURSO select CODEMPRESA,CODFILIAL,CODESTRUTURA,'" + NovaRevisao.ToString() + "' AS REVESTRUTURA,SEQOPERACAO,CODRECROTEIRO,CODOPERACAO,TIPORECURSO,CODGRUPORECURSO,CODCOMPONENTE,QTDRECURSO,QTDCOMPONENTE,UNDCOMPONENTE from PROTEIRORECURSO where  CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, txtCodigo.Text, cmbnumerorevisao.SelectedValue });

                        conn.Commit();
                        MessageBox.Show("Nova revisão gerada com sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CodRevEstrutura = NovaRevisao.ToString();
                        carregaCampos();
                    }
                    catch (Exception ex)
                    {
                        conn.Rollback();
                        MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void cmbnumerorevisao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flagCarregaRevisao == true)
            {
                if ((cmbnumerorevisao.SelectedIndex == -1) || (cmbnumerorevisao.SelectedIndex == 0))
                {

                }
                else
                {
                    if (MessageBox.Show("Deseja realmente carregar a revisão selecionada?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        flagCarregaRevisao = false;
                        CodRevEstrutura = cmbnumerorevisao.SelectedValue.ToString();
                        carregaCampos();
                    }
                }
            }

        }

        void LiberarBloquearEstrutura(AppLib.Data.Connection conn)
        {
            if (chkestruturabloqueada.Checked == false)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;

                cmbnumerorevisao.Enabled = false;
                btnnovarevisao.Enabled = false;
                btninativarrevisao.Enabled = true;

                btnliberarroteiro.Enabled = true;
                btnbloquearroteiro.Enabled = false;

                Global gl = new Global();
                gl.EnableTab(tabControl2.TabPages["tabRoteiro"], false);
                gl.EnableTab(tabControl2.TabPages["tabRecurso"], false);

                conn.ExecQuery(@"UPDATE PROTEIROESTRUTURA SET BLOQUEADO = 1, DTBLOQUEIOESTRUTURA = GETDATE(),USUARIOBLOQUEIO=? WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Usuario, AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, Convert.ToInt16(CodRevEstrutura) });
                chkestruturabloqueada.Checked = true;
            }
            else
            {
                if (chkestruturainativa.Checked == true)
                {
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = false;
                    groupBox3.Enabled = false;
                    groupBox4.Enabled = false;

                    cmbnumerorevisao.Enabled = false;
                    btnnovarevisao.Enabled = false;
                    btninativarrevisao.Enabled = true;

                    btnliberarroteiro.Enabled = false;
                    btnbloquearroteiro.Enabled = true;

                    Global gl = new Global();
                    gl.EnableTab(tabControl2.TabPages["tabRoteiro"], false);
                    gl.EnableTab(tabControl2.TabPages["tabRecurso"], false);
                }
                else
                {
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = true;
                    groupBox3.Enabled = true;
                    groupBox4.Enabled = true;

                    cmbnumerorevisao.Enabled = true;
                    btnnovarevisao.Enabled = true;
                    btninativarrevisao.Enabled = true;

                    btnliberarroteiro.Enabled = false;
                    btnbloquearroteiro.Enabled = true;

                    Global gl = new Global();
                    gl.EnableTab(tabControl2.TabPages["tabRoteiro"], true);
                    gl.EnableTab(tabControl2.TabPages["tabRecurso"], true);
                }
                conn.ExecQuery(@"UPDATE PROTEIROESTRUTURA SET BLOQUEADO = 0, DTBLOQUEIOESTRUTURA = null,USUARIOBLOQUEIO=null WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, Convert.ToInt16(CodRevEstrutura) });
                chkestruturabloqueada.Checked = false;
            }
        }
        private void btnliberarroteiro_Click(object sender, EventArgs e)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();

            try
            {
                LiberarBloquearEstrutura(conn);

                conn.Commit();
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT DTBLOQUEIOESTRUTURA,USUARIOBLOQUEIO FROM PROTEIROESTRUTURA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura });
                if (dt.Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dt.Rows[0]["DTBLOQUEIOESTRUTURA"].ToString()))
                    {
                        txtdatabloqueio.Text = "";
                    }
                    else
                    {
                        txtdatabloqueio.Text = Convert.ToDateTime(dt.Rows[0]["DTBLOQUEIOESTRUTURA"].ToString()).ToString("dd/MM/yyyy");
                    }
                    txtusuariobloqueio.Text = dt.Rows[0]["USUARIOBLOQUEIO"].ToString();
                }
            }
            catch (Exception)
            {
                conn.Rollback();
                MessageBox.Show("Erro ao alterar liberar estrutura", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnbloquearroteiro_Click(object sender, EventArgs e)
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();

            try
            {
                LiberarBloquearEstrutura(conn);

                conn.Commit();
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT DTBLOQUEIOESTRUTURA,USUARIOBLOQUEIO FROM PROTEIROESTRUTURA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND CODESTRUTURA = ? AND REVESTRUTURA = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, codEstrutura, CodRevEstrutura });
                if (dt.Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dt.Rows[0]["DTBLOQUEIOESTRUTURA"].ToString()))
                    {
                        txtdatabloqueio.Text = "";
                    }
                    else
                    {
                        txtdatabloqueio.Text = Convert.ToDateTime(dt.Rows[0]["DTBLOQUEIOESTRUTURA"].ToString()).ToString("dd/MM/yyyy");
                    }
                    txtusuariobloqueio.Text = dt.Rows[0]["USUARIOBLOQUEIO"].ToString();
                }
            }
            catch (Exception)
            {
                conn.Rollback();
                MessageBox.Show("Erro ao alterar bloquear estrutura", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
