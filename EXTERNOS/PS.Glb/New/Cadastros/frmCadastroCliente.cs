using ITGProducao.Controles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroCliente : Form
    {
        public bool edita = false;
        public string codCliFor = string.Empty;
        public string Nomee = string.Empty;
        public bool consulta = false;
        string tabelaContato = "VCLIFORCONTATO";
        private List<string> tabelasFilhas = new List<string>();
        //
        public string mask;
        public string lpDescricao;
        public string lpCodcliente;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroCliente()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VCLIFOR");
            new Class.Utilidades().getDicionario(this, tabControl2, "VCLIFOR");

            #region Lookup
            lpCodTipoRua.PSPart = "PSPartTipoRua";
            lpCodTipoBairro.PSPart = "PSPartTipoBairro";
            lpCodPais.PSPart = "PSPartPais";
            lpCodEtd.PSPart = "PSPartEstado";
            lpCodCidade.PSPart = "PSPartCidade";

            lpCodTipoRuaEnt.PSPart = "PSPartTipoRua";
            lpCodTipoBairroEnt.PSPart = "PSPartTipoBairro";
            lpCodPaisEnt.PSPart = "PSPartPais";
            lpCodEtdEnt.PSPart = "PSPartEstado";
            lpCodCidadeEnt.PSPart = "PSPartCidade";

            lpCodTipoRuaPag.PSPart = "PSPartTipoRua";
            lpCodTipoBairroPag.PSPart = "PSPartTipoBairro";
            lpCodPaisPag.PSPart = "PSPartPais";
            lpCodEtdPag.PSPart = "PSPartEstado";
            lpCodCidadePag.PSPart = "PSPartCidade";

            lpCodEstVic.PSPart = "PSPartEstCiv";
            lpCodEtdEmissor.PSPart = "PSPartEstado";
            lpCodRepre.PSPart = "PSPartRepre";
            lpCodVendedor.PSPart = "PSPartVendedor";
            lpCodConta.PSPart = "PSPartConta";
            lpCodTransportadora.PSPart = "PSPartTransportadora";
            lpCodCCusto.PSPart = "PSPartCentroCusto";
            lpCodCondicaoCompra.PSPart = "PSPartCondicaoPgto";
            lpCodCondicaoVenda.PSPart = "PSPartCondicaoPgto";
            lpCodNaturezaOrcamento.PSPart = "PSPartNaturezaOrcamento";
            lpIdTipoCliente.PSPart = "PSPartTipoCliente";

            #endregion

            #region Combo

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = 0;
            list1[0].DisplayMember = "Cliente";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = 1;
            list1[1].DisplayMember = "Fornecedor";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = 2;
            list1[2].DisplayMember = "Ambos";

            cmbcodClassificacao.DataSource = list1;
            cmbcodClassificacao.DisplayMember = "DisplayMember";
            cmbcodClassificacao.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list2 = new List<PS.Lib.ComboBoxItem>();

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[0].ValueMember = 0;
            list2[0].DisplayMember = "Jurídica";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[1].ValueMember = 1;
            list2[1].DisplayMember = "Física";

            cmbFisicoJuridico.DataSource = list2;
            cmbFisicoJuridico.DisplayMember = "DisplayMember";
            cmbFisicoJuridico.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list3 = new List<PS.Lib.ComboBoxItem>();

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[0].ValueMember = 9;
            list3[0].DisplayMember = "";

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[1].ValueMember = 0;
            list3[1].DisplayMember = "Privada";

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[2].ValueMember = 1;
            list3[2].DisplayMember = "Publica";

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[3].ValueMember = 2;
            list3[3].DisplayMember = "Cooperativa";

            cmbCodNatJur.DataSource = list3;
            cmbCodNatJur.DisplayMember = "DisplayMember";
            cmbCodNatJur.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list4 = new List<PS.Lib.ComboBoxItem>();

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[0].ValueMember = 9;
            list4[0].DisplayMember = "";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[1].ValueMember = 0;
            list4[1].DisplayMember = "ME";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[2].ValueMember = 1;
            list4[2].DisplayMember = "EPP";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[3].ValueMember = 2;
            list4[3].DisplayMember = "Normal";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[4].ValueMember = 3;
            list4[4].DisplayMember = "Outros";

            cmbCodRegApuracao.DataSource = list4;
            cmbCodRegApuracao.DisplayMember = "DisplayMember";
            cmbCodRegApuracao.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list5 = new List<PS.Lib.ComboBoxItem>();
            list5.Add(new PS.Lib.ComboBoxItem());
            list5[0].ValueMember = 9;
            list5[0].DisplayMember = "";

            list5.Add(new PS.Lib.ComboBoxItem());
            list5[1].ValueMember = 0;
            list5[1].DisplayMember = "Contribuinte ICMS";
            //1
            list5.Add(new PS.Lib.ComboBoxItem());
            list5[2].ValueMember = 1;
            list5[2].DisplayMember = "Contribuinte Isento";
            //2
            list5.Add(new PS.Lib.ComboBoxItem());
            list5[3].ValueMember = 2;
            list5[3].DisplayMember = "Não Contribuinte";
            //0--9

            cmbContribIcms.DataSource = list5;
            cmbContribIcms.DisplayMember = "DisplayMember";
            cmbContribIcms.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list6 = new List<PS.Lib.ComboBoxItem>();

            list6.Add(new PS.Lib.ComboBoxItem());
            list6[0].ValueMember = 0;
            list6[0].DisplayMember = "Brasileira";

            list6.Add(new PS.Lib.ComboBoxItem());
            list6[1].ValueMember = 1;
            list6[1].DisplayMember = "Estrangeira";

            cmbNacionalidade.DataSource = list6;
            cmbNacionalidade.DisplayMember = "DisplayMember";
            cmbNacionalidade.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list7 = new List<PS.Lib.ComboBoxItem>();

            list7.Add(new PS.Lib.ComboBoxItem());
            list7[0].ValueMember = 0;
            list7[0].DisplayMember = "CIF";

            list7.Add(new PS.Lib.ComboBoxItem());
            list7[1].ValueMember = 1;
            list7[1].DisplayMember = "FOB";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[2].ValueMember = 2;
            list2[2].DisplayMember = "Terceiro";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[3].ValueMember = 3;
            list2[3].DisplayMember = "Sem Frete";

            cmbFreteCifFob.DataSource = list7;
            cmbFreteCifFob.DisplayMember = "DisplayMember";
            cmbFreteCifFob.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> listCLASSVENDA = new List<PS.Lib.ComboBoxItem>();

            listCLASSVENDA.Add(new PS.Lib.ComboBoxItem());
            listCLASSVENDA[0].ValueMember = "V";
            listCLASSVENDA[0].DisplayMember = "Venda/Industrialização";

            listCLASSVENDA.Add(new PS.Lib.ComboBoxItem());
            listCLASSVENDA[1].ValueMember = "R";
            listCLASSVENDA[1].DisplayMember = "Revenda";

            listCLASSVENDA.Add(new PS.Lib.ComboBoxItem());
            listCLASSVENDA[2].ValueMember = "C";
            listCLASSVENDA[2].DisplayMember = "Consumo";

            cmbClassVenda.DataSource = listCLASSVENDA;
            cmbClassVenda.DisplayMember = "DisplayMember";
            cmbClassVenda.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> listAtivo = new List<PS.Lib.ComboBoxItem>();

            listAtivo.Add(new PS.Lib.ComboBoxItem());
            listAtivo[0].ValueMember = 1;
            listAtivo[0].DisplayMember = "Ativo";

            listAtivo.Add(new PS.Lib.ComboBoxItem());
            listAtivo[1].ValueMember = 0;
            listAtivo[1].DisplayMember = "Inativo";

            cmbAtivo.DataSource = listAtivo;
            cmbAtivo.DisplayMember = "DisplayMember";
            cmbAtivo.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> listTabPreco = new List<PS.Lib.ComboBoxItem>();
            DataTable tabPreco = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT TABELA, NOME FROM (
SELECT 
CODEMPRESA, 0 TABELA, 'Nenhum' NOME FROM VPARAMETROS
UNION ALL
SELECT CODEMPRESA, 1 TABELA, PRDTEXTOPRECO1 NOME FROM VPARAMETROS
UNION ALL
SELECT CODEMPRESA, 2 TABELA, PRDTEXTOPRECO2 NOME FROM VPARAMETROS
UNION ALL
SELECT CODEMPRESA, 3 TABELA, PRDTEXTOPRECO3 NOME FROM VPARAMETROS
UNION ALL
SELECT CODEMPRESA, 4 TABELA, PRDTEXTOPRECO4 NOME FROM VPARAMETROS
UNION ALL
SELECT CODEMPRESA, 5 TABELA, PRDTEXTOPRECO5 NOME FROM VPARAMETROS
UNION ALL 
SELECT CODEMPRESA, 6 TABELA, 'Tabela de Preço por Cliente' NOME FROM GEMPRESA
) X
WHERE CODEMPRESA = ?
AND NOT(NOME IS NULL)", new object[] { AppLib.Context.Empresa });


            foreach (DataRow row in tabPreco.Rows)
            {
                listTabPreco.Add(new Lib.ComboBoxItem(row["TABELA"], row["NOME"].ToString()));
            }

            cmbCodTabPreco.DataSource = listTabPreco;
            cmbCodTabPreco.DisplayMember = "DisplayMember";
            cmbCodTabPreco.ValueMember = "ValueMember";

            #endregion
        }

        // Movimentação de Produtos
        string tabela = "VCLIFOR";
        string query = string.Empty;
        //

        public frmCadastroCliente(ref NewLookup lookup)
        {
            InitializeComponent();

            new Class.Utilidades().getDicionario(this, tabControl1, "VCLIFOR");
            new Class.Utilidades().getDicionario(this, tabControl2, "VCLIFOR");

            #region Lookup
            lpCodTipoRua.PSPart = "PSPartTipoRua";
            lpCodTipoBairro.PSPart = "PSPartTipoBairro";
            lpCodPais.PSPart = "PSPartPais";
            lpCodEtd.PSPart = "PSPartEstado";
            lpCodCidade.PSPart = "PSPartCidade";

            lpCodTipoRuaEnt.PSPart = "PSPartTipoRua";
            lpCodTipoBairroEnt.PSPart = "PSPartTipoBairro";
            lpCodPaisEnt.PSPart = "PSPartPais";
            lpCodEtdEnt.PSPart = "PSPartEstado";
            lpCodCidadeEnt.PSPart = "PSPartCidade";

            lpCodTipoRuaPag.PSPart = "PSPartTipoRua";
            lpCodTipoBairroPag.PSPart = "PSPartTipoBairro";
            lpCodPaisPag.PSPart = "PSPartPais";
            lpCodEtdPag.PSPart = "PSPartEstado";
            lpCodCidadePag.PSPart = "PSPartCidade";

            lpCodEstVic.PSPart = "PSPartEstCiv";
            lpCodEtdEmissor.PSPart = "PSPartEstado";
            lpCodRepre.PSPart = "PSPartRepre";
            lpCodVendedor.PSPart = "PSPartVendedor";
            lpCodConta.PSPart = "PSPartConta";
            lpCodTransportadora.PSPart = "PSPartTransportadora";
            lpCodCCusto.PSPart = "PSPartCentroCusto";
            lpCodCondicaoCompra.PSPart = "PSPartCondicaoPgto";
            lpCodCondicaoVenda.PSPart = "PSPartCondicaoPgto";
            lpCodNaturezaOrcamento.PSPart = "PSPartNaturezaOrcamento";
            lpIdTipoCliente.PSPart = "PSPartTipoCliente";

            #endregion

            #region Combo

            List<PS.Lib.ComboBoxItem> list1 = new List<PS.Lib.ComboBoxItem>();

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[0].ValueMember = 0;
            list1[0].DisplayMember = "Cliente";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[1].ValueMember = 1;
            list1[1].DisplayMember = "Fornecedor";

            list1.Add(new PS.Lib.ComboBoxItem());
            list1[2].ValueMember = 2;
            list1[2].DisplayMember = "Ambos";

            cmbcodClassificacao.DataSource = list1;
            cmbcodClassificacao.DisplayMember = "DisplayMember";
            cmbcodClassificacao.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list2 = new List<PS.Lib.ComboBoxItem>();

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[0].ValueMember = 0;
            list2[0].DisplayMember = "Jurídica";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[1].ValueMember = 1;
            list2[1].DisplayMember = "Física";

            cmbFisicoJuridico.DataSource = list2;
            cmbFisicoJuridico.DisplayMember = "DisplayMember";
            cmbFisicoJuridico.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list3 = new List<PS.Lib.ComboBoxItem>();

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[0].ValueMember = 9;
            list3[0].DisplayMember = "";

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[1].ValueMember = 0;
            list3[1].DisplayMember = "Privada";

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[2].ValueMember = 1;
            list3[2].DisplayMember = "Publica";

            list3.Add(new PS.Lib.ComboBoxItem());
            list3[3].ValueMember = 2;
            list3[3].DisplayMember = "Cooperativa";

            cmbCodNatJur.DataSource = list3;
            cmbCodNatJur.DisplayMember = "DisplayMember";
            cmbCodNatJur.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list4 = new List<PS.Lib.ComboBoxItem>();

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[0].ValueMember = 9;
            list4[0].DisplayMember = "";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[1].ValueMember = 0;
            list4[1].DisplayMember = "ME";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[2].ValueMember = 1;
            list4[2].DisplayMember = "EPP";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[3].ValueMember = 2;
            list4[3].DisplayMember = "Normal";

            list4.Add(new PS.Lib.ComboBoxItem());
            list4[4].ValueMember = 3;
            list4[4].DisplayMember = "Outros";

            cmbCodRegApuracao.DataSource = list4;
            cmbCodRegApuracao.DisplayMember = "DisplayMember";
            cmbCodRegApuracao.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list5 = new List<PS.Lib.ComboBoxItem>();
            list5.Add(new PS.Lib.ComboBoxItem());
            list5[0].ValueMember = 9;
            list5[0].DisplayMember = "";

            list5.Add(new PS.Lib.ComboBoxItem());
            list5[1].ValueMember = 0;
            list5[1].DisplayMember = "Contribuinte ICMS";
            //1
            list5.Add(new PS.Lib.ComboBoxItem());
            list5[2].ValueMember = 1;
            list5[2].DisplayMember = "Contribuinte Isento";
            //2
            list5.Add(new PS.Lib.ComboBoxItem());
            list5[3].ValueMember = 2;
            list5[3].DisplayMember = "Não Contribuinte";
            //0--9

            cmbContribIcms.DataSource = list5;
            cmbContribIcms.DisplayMember = "DisplayMember";
            cmbContribIcms.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list6 = new List<PS.Lib.ComboBoxItem>();

            list6.Add(new PS.Lib.ComboBoxItem());
            list6[0].ValueMember = 0;
            list6[0].DisplayMember = "Brasileira";

            list6.Add(new PS.Lib.ComboBoxItem());
            list6[1].ValueMember = 1;
            list6[1].DisplayMember = "Estrangeira";

            cmbNacionalidade.DataSource = list6;
            cmbNacionalidade.DisplayMember = "DisplayMember";
            cmbNacionalidade.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> list7 = new List<PS.Lib.ComboBoxItem>();

            list7.Add(new PS.Lib.ComboBoxItem());
            list7[0].ValueMember = 0;
            list7[0].DisplayMember = "CIF";

            list7.Add(new PS.Lib.ComboBoxItem());
            list7[1].ValueMember = 1;
            list7[1].DisplayMember = "FOB";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[2].ValueMember = 2;
            list2[2].DisplayMember = "Terceiro";

            list2.Add(new PS.Lib.ComboBoxItem());
            list2[3].ValueMember = 3;
            list2[3].DisplayMember = "Sem Frete";

            cmbFreteCifFob.DataSource = list7;
            cmbFreteCifFob.DisplayMember = "DisplayMember";
            cmbFreteCifFob.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> listCLASSVENDA = new List<PS.Lib.ComboBoxItem>();

            listCLASSVENDA.Add(new PS.Lib.ComboBoxItem());
            listCLASSVENDA[0].ValueMember = "V";
            listCLASSVENDA[0].DisplayMember = "Venda";

            listCLASSVENDA.Add(new PS.Lib.ComboBoxItem());
            listCLASSVENDA[1].ValueMember = "R";
            listCLASSVENDA[1].DisplayMember = "Revenda";

            listCLASSVENDA.Add(new PS.Lib.ComboBoxItem());
            listCLASSVENDA[2].ValueMember = "C";
            listCLASSVENDA[2].DisplayMember = "Consumo";

            cmbClassVenda.DataSource = listCLASSVENDA;
            cmbClassVenda.DisplayMember = "DisplayMember";
            cmbClassVenda.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> listAtivo = new List<PS.Lib.ComboBoxItem>();

            listAtivo.Add(new PS.Lib.ComboBoxItem());
            listAtivo[0].ValueMember = 1;
            listAtivo[0].DisplayMember = "Ativo";

            listAtivo.Add(new PS.Lib.ComboBoxItem());
            listAtivo[1].ValueMember = 0;
            listAtivo[1].DisplayMember = "Inativo";

            cmbAtivo.DataSource = listAtivo;
            cmbAtivo.DisplayMember = "DisplayMember";
            cmbAtivo.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> listTabPreco = new List<PS.Lib.ComboBoxItem>();
            DataTable tabPreco = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT TABELA, NOME FROM (
SELECT 
CODEMPRESA, 0 TABELA, 'Nenhum' NOME FROM VPARAMETROS
UNION ALL
SELECT CODEMPRESA, 1 TABELA, PRDTEXTOPRECO1 NOME FROM VPARAMETROS
UNION ALL
SELECT CODEMPRESA, 2 TABELA, PRDTEXTOPRECO2 NOME FROM VPARAMETROS
UNION ALL
SELECT CODEMPRESA, 3 TABELA, PRDTEXTOPRECO3 NOME FROM VPARAMETROS
UNION ALL
SELECT CODEMPRESA, 4 TABELA, PRDTEXTOPRECO4 NOME FROM VPARAMETROS
UNION ALL
SELECT CODEMPRESA, 5 TABELA, PRDTEXTOPRECO5 NOME FROM VPARAMETROS
UNION ALL 
SELECT CODEMPRESA, 6 TABELA, 'Tabela de Preço por Cliente' NOME FROM GEMPRESA
) X
WHERE CODEMPRESA = ?
AND NOT(NOME IS NULL)", new object[] { AppLib.Context.Empresa });


            foreach (DataRow row in tabPreco.Rows)
            {
                listTabPreco.Add(new Lib.ComboBoxItem(row["TABELA"], row["NOME"].ToString()));
            }

            cmbCodTabPreco.DataSource = listTabPreco;
            cmbCodTabPreco.DisplayMember = "DisplayMember";
            cmbCodTabPreco.ValueMember = "ValueMember";

            #endregion

            this.codCliFor = lookup.txtcodigo.Text;
            this.edita = true;

            this.lookup = lookup;
        }

        private void VerificaAcesso()
        {
            if (edita == true)
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT EDICAO FROM GPERMISSAOMENU WHERE CODPERFIL = ? AND CODMENU = ?", new object[] { AppLib.Context.Perfil, "btnCadastrosGlobais_ClientesFornecedores" });
                if (dt.Rows.Count == 0 || Convert.ToBoolean(dt.Rows[0]["EDICAO"]) == false)
                {
                    btnSalvarAtual.Enabled = false;
                    btnOKAtual.Enabled = false;
                    btnNovo.Enabled = false;
                    btnEditar.Enabled = false;
                    btnExcluir.Enabled = false;
                }
            }
        }
        public void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM VCLIFOR WHERE CODCLIFOR = ? AND CODEMPRESA = ?", new object[] { codCliFor, AppLib.Context.Empresa });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                switch (lookup.CampoCodigo_Igual_a)
                {
                    case NewLookup.CampoCodigoIguala.CampoCodigoBD:
                        dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM VCLIFOR WHERE CODCLIFOR = ? AND CODEMPRESA = ?", new object[] { codCliFor, AppLib.Context.Empresa });
                        if (dt.Rows.Count > 0)
                        {
                            carregaObj(dt);
                        }
                        break;

                    case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                        if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                        {
                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM VCLIFOR WHERE CODCLIFOR = ? AND CODEMPRESA = ?", new object[] { codCliFor, AppLib.Context.Empresa });

                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM VCLIFOR WHERE CODEMPRESA = ? AND " + lookup.CampoCodigoInterno + " = ? ", new object[] { AppLib.Context.Empresa, lookup.ValorCodigoInterno });
                            if (dt.Rows.Count > 0)
                            {
                                carregaObj(dt);
                            }
                        }
                        else
                        {
                            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM VCLIFOR WHERE CODEMPRESA = ? AND CODCLIFOR = ? ", new object[] { AppLib.Context.Empresa, codCliFor });
                            if (dt.Rows.Count > 0)
                            {
                                carregaObj(dt);
                            }
                        }
                        break;
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            txtCodClifor.Text = dt.Rows[0]["CODCLIFOR"].ToString();
            txtNome.Text = dt.Rows[0]["NOME"].ToString();
            txtNomeFantasia.Text = dt.Rows[0]["NOMEFANTASIA"].ToString();
            txtCgcCpf.Text = dt.Rows[0]["CGCCPF"].ToString();
            cmbcodClassificacao.SelectedValue = dt.Rows[0]["CODCLASSIFICACAO"];
            cmbFisicoJuridico.SelectedValue = dt.Rows[0]["FISICOJURIDICO"];
            cmbAtivo.SelectedValue = dt.Rows[0]["ATIVO"];
            txtCep.Text = dt.Rows[0]["CEP"].ToString();
            lpCodTipoRua.textBox1.Text = dt.Rows[0]["CODTIPORUA"].ToString();
            if (!string.IsNullOrEmpty(lpCodTipoRua.textBox1.Text))
            {
                lpCodTipoRua.LoadLookup();
            }
            txtRua.Text = dt.Rows[0]["RUA"].ToString();
            txtNumero.Text = dt.Rows[0]["NUMERO"].ToString();
            txtComplemento.Text = dt.Rows[0]["COMPLEMENTO"].ToString();
            lpCodTipoBairro.textBox1.Text = dt.Rows[0]["CODTIPOBAIRRO"].ToString();
            if (!string.IsNullOrEmpty(lpCodTipoBairro.textBox1.Text))
            {
                lpCodTipoBairro.LoadLookup();
            }
            txtBairro.Text = dt.Rows[0]["BAIRRO"].ToString();
            lpCodEtd.textBox1.Text = dt.Rows[0]["CODETD"].ToString();
            if (!string.IsNullOrEmpty(lpCodEtd.textBox1.Text))
            {
                lpCodEtd.LoadLookup();
            }

            lpCodCidade.Text = dt.Rows[0]["CODCIDADE"].ToString();
            if (!string.IsNullOrEmpty(lpCodCidade.textBox1.Text))
            {
                lpCodCidade.LoadLookup();
            }

            lpCodPais.textBox1.Text = dt.Rows[0]["CODPAIS"].ToString();

            txtTelResidencial.Text = dt.Rows[0]["TELRESIDENCIAL"].ToString();
            txtTelComercial.Text = dt.Rows[0]["TELCOMERCIAL"].ToString();
            txtTelCelular.Text = dt.Rows[0]["TELCELULAR"].ToString();
            txtTelFax.Text = dt.Rows[0]["TELFAX"].ToString();
            if (dt.Rows[0]["DATANASCIMENTO"] != DBNull.Value)
            {
                dteDataNascimento.EditValue = Convert.ToDateTime(dt.Rows[0]["DATANASCIMENTO"]);
            }

            lpCodEstVic.textBox1.Text = dt.Rows[0]["CODESTVIC"].ToString();
            if (!string.IsNullOrEmpty(lpCodEstVic.textBox1.Text))
            {
                lpCodEstVic.LoadLookup();
            }
            txtNumeroRg.Text = dt.Rows[0]["NUMERORG"].ToString();
            txtOREmissor.Text = dt.Rows[0]["OREMISSOR"].ToString();
            txtEmail.Text = dt.Rows[0]["EMAIL"].ToString();
            txtInscricaoEstadual.Text = dt.Rows[0]["INSCRICAOESTADUAL"].ToString();
            txtInscricaoMunicipal.Text = dt.Rows[0]["INSCRICAOMUNICIPAL"].ToString();
            txtInscricaoSuframa.Text = dt.Rows[0]["INSCRICAOSUFRAMA"].ToString();
            cmbCodNatJur.SelectedValue = dt.Rows[0]["CODNATJUR"];
            cmbCodRegApuracao.SelectedValue = dt.Rows[0]["CODREGAPURACAO"];
            txtCepEnt.Text = dt.Rows[0]["CEPENT"].ToString();
            lpCodTipoRuaEnt.textBox1.Text = dt.Rows[0]["CODTIPORUAENT"].ToString();
            if (!string.IsNullOrEmpty(lpCodTipoRuaEnt.textBox1.Text))
            {
                lpCodTipoRuaEnt.LoadLookup();
            }
            txtRuaEnt.Text = dt.Rows[0]["RUAENT"].ToString();
            txtNumeroEnt.Text = dt.Rows[0]["NUMEROENT"].ToString();
            txtComplementoEnt.Text = dt.Rows[0]["COMPLEMENTOENT"].ToString();
            lpCodTipoBairroEnt.textBox1.Text = dt.Rows[0]["CODTIPOBAIRROENT"].ToString();
            if (!string.IsNullOrEmpty(lpCodTipoBairroEnt.textBox1.Text))
            {
                lpCodTipoBairroEnt.LoadLookup();
            }
            txtBairroEnt.Text = dt.Rows[0]["BAIRROENT"].ToString();
            lpCodEtdEnt.textBox1.Text = dt.Rows[0]["CODETDENT"].ToString();
            if (!string.IsNullOrEmpty(lpCodEtdEnt.textBox1.Text))
            {
                lpCodEtdEnt.LoadLookup();
            }
            lpCodCidadeEnt.textBox1.Text = dt.Rows[0]["CODCIDADEENT"].ToString();
            if (!string.IsNullOrEmpty(lpCodCidadeEnt.textBox1.Text))
            {
                lpCodCidadeEnt.LoadLookup();
            }

            lpCodPaisEnt.textBox1.Text = dt.Rows[0]["CODPAISENT"].ToString();
            if (!string.IsNullOrEmpty(lpCodPaisEnt.textBox1.Text))
            {
                lpCodPaisEnt.LoadLookup();
            }
            txtCepPag.Text = dt.Rows[0]["CEPPAG"].ToString();
            lpCodTipoRuaPag.textBox1.Text = dt.Rows[0]["CODTIPORUAPAG"].ToString();
            if (!string.IsNullOrEmpty(lpCodTipoRuaPag.textBox1.Text))
            {
                lpCodTipoRuaPag.LoadLookup();
            }
            txtRuaPag.Text = dt.Rows[0]["RUAPAG"].ToString();
            txtNumeroPag.Text = dt.Rows[0]["NUMEROPAG"].ToString();
            txtComplementoPag.Text = dt.Rows[0]["COMPLEMENTOPAG"].ToString();
            lpCodTipoBairroPag.textBox1.Text = dt.Rows[0]["CODTIPOBAIRROPAG"].ToString();
            if (!string.IsNullOrEmpty(lpCodTipoBairroPag.textBox1.Text))
            {
                lpCodTipoBairroPag.LoadLookup();
            }
            txtBairroPag.Text = dt.Rows[0]["BAIRROPAG"].ToString();
            lpCodEtdPag.textBox1.Text = dt.Rows[0]["CODETDPAG"].ToString();
            if (!string.IsNullOrEmpty(lpCodEtdPag.textBox1.Text))
            {
                lpCodEtdPag.LoadLookup();
            }
            lpCodCidadePag.textBox1.Text = dt.Rows[0]["CODCIDADEPAG"].ToString();
            if (!string.IsNullOrEmpty(lpCodCidadePag.textBox1.Text))
            {
                lpCodCidadePag.LoadLookup();
            }

            lpCodPaisPag.textBox1.Text = dt.Rows[0]["CODPAISPAG"].ToString();
            if (!string.IsNullOrEmpty(lpCodPaisPag.textBox1.Text))
            {
                lpCodPaisPag.LoadLookup();
            }
            cmbContribIcms.SelectedValue = dt.Rows[0]["CONTRIBICMS"];
            cmbNacionalidade.SelectedValue = dt.Rows[0]["NACIONALIDADE"];
            lpCodEtdEmissor.textBox1.Text = dt.Rows[0]["CODETDEMISSOR"].ToString();
            if (!string.IsNullOrEmpty(lpCodEtdEmissor.textBox1.Text))
            {
                lpCodEtdEmissor.LoadLookup();
            }
            txtLimiteCredito.Text = string.Format("{0:n2}", dt.Rows[0]["LIMITECREDITO"]);
            lpCodRepre.textBox1.Text = dt.Rows[0]["CODREPRE"].ToString();
            if (!string.IsNullOrEmpty(lpCodRepre.textBox1.Text))
            {
                lpCodRepre.LoadLookup();
            }
            lpCodTransportadora.textBox1.Text = dt.Rows[0]["CODTRANSPORTADORA"].ToString();
            if (!string.IsNullOrEmpty(lpCodTransportadora.textBox1.Text))
            {
                lpCodTransportadora.LoadLookup();
            }
            cmbFreteCifFob.SelectedValue = dt.Rows[0]["FRETECIFFOB"];
            lpCodConta.textBox1.Text = dt.Rows[0]["CODCONTA"].ToString();
            if (!string.IsNullOrEmpty(lpCodConta.textBox1.Text))
            {
                lpCodConta.LoadLookup();
            }
            lpCodCCusto.textBox1.Text = dt.Rows[0]["CODCCUSTO"].ToString();
            if (!string.IsNullOrEmpty(lpCodCCusto.textBox1.Text))
            {
                lpCodCCusto.LoadLookup();
            }
            lpCodCondicaoCompra.textBox1.Text = dt.Rows[0]["CODCONDICAOCOMPRA"].ToString();
            if (!string.IsNullOrEmpty(lpCodCondicaoCompra.textBox1.Text))
            {
                lpCodCondicaoCompra.LoadLookup();
            }
            lpCodCondicaoVenda.textBox1.Text = dt.Rows[0]["CODCONDICAOVENDA"].ToString();
            if (!string.IsNullOrEmpty(lpCodCondicaoVenda.textBox1.Text))
            {
                lpCodCondicaoVenda.LoadLookup();
            }
            txtEmailNfe.Text = dt.Rows[0]["EMAILNFE"].ToString();
            cmbCodTabPreco.SelectedValue = dt.Rows[0]["CODTABPRECO"];
            txtWebSite.Text = dt.Rows[0]["WEBSITE"].ToString();
            if (dt.Rows[0]["DATACRIACAO"].ToString() != string.Empty)
            {
                dteDataCriacao.EditValue = Convert.ToDateTime(dt.Rows[0]["DATACRIACAO"]);
            }
            txtCodUsuarioCriacao.Text = dt.Rows[0]["CODUSUARIOCRIACAO"].ToString();
            lpCodNaturezaOrcamento.textBox1.Text = dt.Rows[0]["CODNATUREZAORCAMENTO"].ToString();
            if (!string.IsNullOrEmpty(lpCodNaturezaOrcamento.textBox1.Text))
            {
                lpCodNaturezaOrcamento.LoadLookup();
            }
            meHistorico.Text = dt.Rows[0]["HISTORICO"].ToString();
            lpIdTipoCliente.textBox1.Text = dt.Rows[0]["IDTIPOCLIENTE"].ToString();
            if (!string.IsNullOrEmpty(lpIdTipoCliente.textBox1.Text))
            {
                lpIdTipoCliente.LoadLookup();
            }
            txtDescMaxVenda.Text = dt.Rows[0]["DESCMAXVENDA"].ToString();
            txtDescMaxCompra.Text = dt.Rows[0]["DESCMAXCOMPRA"].ToString();
            cmbClassVenda.SelectedValue = dt.Rows[0]["CLASSVENDA"];
            chkProdutorRural.Checked = Convert.ToBoolean(dt.Rows[0]["PRODUTORRURAL"]);
            lpCodVendedor.textBox1.Text = dt.Rows[0]["CODVENDEDOR"].ToString();
            if (!string.IsNullOrEmpty(lpCodVendedor.textBox1.Text))
            {
                lpCodVendedor.LoadLookup();
            }
            lpFormaPagamento.txtcodigo.Text = dt.Rows[0]["CODFORMA"].ToString();
            if (!string.IsNullOrEmpty(lpFormaPagamento.txtcodigo.Text))
            {
                lpFormaPagamento.CarregaDescricao(); 
            }
            txtIdEstrangeiro.Text = dt.Rows[0]["IDESTRANGEIRO"].ToString();
            tbPis.Text = dt.Rows[0]["PIS"].ToString();
            chkConstrucaoCivil.Checked = Convert.ToBoolean(dt.Rows[0]["CONSTRUCAOCIVIL"]);
            chkAutomotiva.Checked = Convert.ToBoolean(dt.Rows[0]["AUTOMOTIVA"]);
        }

        private void frmCadastroCliente_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
            }

            ValidaTipoPessoa();
            VerificaAcesso();
            carregaGridContato();

            CarregaGridUnidade();
        }

        private void ValidaTipoPessoa()
        {

        }

        private bool validacao()
        {
            bool _valida = true;

            if (txtCodClifor.Text == "0")
            {
                MessageBox.Show("Não foi possível carregar o próximo número do cliente.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _valida = false;
            }

            PS.Glb.Class.Utilidades util = new PS.Glb.Class.Utilidades();

            if (util.validaCamposObrigatorios(this, ref errorProvider) == false || _valida == false)
            {
                _valida = false;
            }

            return _valida;
        }

        private bool salvar()
        {
            if (edita == false)
            {
                txtCodClifor.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(0, @"SELECT ISNULL(MAX(CODCLIFOR), 0) + 1  AS CODCLIFOR FROM VCLIFOR ", new object[] { }).ToString();
            }
            if (validacao() == false)
            {
                return false;
            }

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VCLIFOR = new AppLib.ORM.Jit(conn, "VCLIFOR");
            conn.BeginTransaction();
            try
            {
                mask = conn.ExecGetField(string.Empty, @"SELECT CLIFORMASKNUMEROSEQ FROM VPARAMETROS WHERE CODEMPRESA = ? ", new object[] { AppLib.Context.Empresa }).ToString();

                VCLIFOR.Set("CODEMPRESA", AppLib.Context.Empresa);
                VCLIFOR.Set("CODCLIFOR", AppLib.Util.Format.CompletarZeroEsquerda(Convert.ToInt32(mask.Length), txtCodClifor.Text));
                VCLIFOR.Set("NOME", txtNome.Text);
                VCLIFOR.Set("NOMEFANTASIA", txtNomeFantasia.Text);
                VCLIFOR.Set("CGCCPF", txtCgcCpf.Text);
                VCLIFOR.Set("CODCLASSIFICACAO", cmbcodClassificacao.SelectedValue);
                VCLIFOR.Set("FISICOJURIDICO", cmbFisicoJuridico.SelectedValue);
                VCLIFOR.Set("ATIVO", cmbAtivo.SelectedValue);
                VCLIFOR.Set("CEP", txtCep.Text);
                VCLIFOR.Set("CODTIPORUA", string.IsNullOrEmpty(lpCodTipoRua.textBox1.Text) ? null : lpCodTipoRua.textBox1.Text);
                VCLIFOR.Set("RUA", txtRua.Text);
                VCLIFOR.Set("NUMERO", txtNumero.Text);
                VCLIFOR.Set("COMPLEMENTO", txtComplemento.Text);
                VCLIFOR.Set("CODTIPOBAIRRO", string.IsNullOrEmpty(lpCodTipoBairro.textBox1.Text) ? null : lpCodTipoBairro.textBox1.Text);
                VCLIFOR.Set("BAIRRO", txtBairro.Text);
                VCLIFOR.Set("CODCIDADE", string.IsNullOrEmpty(lpCodCidade.textBox1.Text) ? null : lpCodCidade.textBox1.Text);
                VCLIFOR.Set("CODETD", string.IsNullOrEmpty(lpCodEtd.textBox1.Text) ? null : lpCodEtd.textBox1.Text);
                VCLIFOR.Set("CODPAIS", string.IsNullOrEmpty(lpCodPais.textBox1.Text) ? null : lpCodPais.textBox1.Text);
                VCLIFOR.Set("DATANASCIMENTO", dteDataNascimento.EditValue);
                VCLIFOR.Set("TELRESIDENCIAL", txtTelResidencial.Text);
                VCLIFOR.Set("TELCOMERCIAL", txtTelComercial.Text);
                VCLIFOR.Set("TELCELULAR", txtTelCelular.Text);
                VCLIFOR.Set("TELFAX", txtTelFax.Text);
                VCLIFOR.Set("CODESTVIC", string.IsNullOrEmpty(lpCodEstVic.textBox1.Text) ? null : lpCodEstVic.textBox1.Text);
                VCLIFOR.Set("NUMERORG", txtNumeroRg.Text);
                VCLIFOR.Set("OREMISSOR", txtOREmissor.Text);
                VCLIFOR.Set("EMAIL", txtEmail.Text);
                VCLIFOR.Set("INSCRICAOESTADUAL", txtInscricaoEstadual.Text);
                VCLIFOR.Set("INSCRICAOMUNICIPAL", txtInscricaoMunicipal.Text);
                VCLIFOR.Set("INSCRICAOSUFRAMA", txtInscricaoSuframa.Text);
                VCLIFOR.Set("CODNATJUR", cmbCodNatJur.SelectedValue);
                VCLIFOR.Set("CODREGAPURACAO", cmbCodRegApuracao.SelectedValue);
                VCLIFOR.Set("CEPENT", txtCepEnt.Text);
                VCLIFOR.Set("CODTIPORUAENT", string.IsNullOrEmpty(lpCodTipoRuaEnt.textBox1.Text) ? null : lpCodTipoRuaEnt.textBox1.Text);
                VCLIFOR.Set("RUAENT", txtRuaEnt.Text);
                VCLIFOR.Set("NUMEROENT", txtNumeroEnt.Text);
                VCLIFOR.Set("COMPLEMENTOENT", txtComplementoEnt.Text);
                VCLIFOR.Set("CODTIPOBAIRROENT", string.IsNullOrWhiteSpace(lpCodTipoBairroEnt.textBox1.Text) ? null : lpCodTipoBairroEnt.textBox1.Text);
                VCLIFOR.Set("BAIRROENT", txtBairroEnt.Text);
                VCLIFOR.Set("CODCIDADEENT", string.IsNullOrEmpty(lpCodCidadeEnt.textBox1.Text) ? null : lpCodCidadeEnt.textBox1.Text);
                VCLIFOR.Set("CODETDENT", string.IsNullOrEmpty(lpCodEtdEnt.textBox1.Text) ? null : lpCodEtdEnt.textBox1.Text);
                VCLIFOR.Set("CODPAISENT", string.IsNullOrEmpty(lpCodPaisEnt.textBox1.Text) ? null : lpCodPaisEnt.textBox1.Text);
                VCLIFOR.Set("CEPPAG", txtCepPag.Text);
                VCLIFOR.Set("CODTIPORUAPAG", string.IsNullOrEmpty(lpCodTipoRuaPag.textBox1.Text) ? null : lpCodTipoRuaPag.textBox1.Text);
                VCLIFOR.Set("RUAPAG", txtRuaPag.Text);
                VCLIFOR.Set("NUMEROPAG", txtNumeroPag.Text);
                VCLIFOR.Set("COMPLEMENTOPAG", txtComplementoPag.Text);
                VCLIFOR.Set("CODTIPOBAIRROPAG", string.IsNullOrWhiteSpace(lpCodTipoBairroPag.textBox1.Text) ? null : lpCodTipoBairroPag.textBox1.Text);
                VCLIFOR.Set("BAIRROPAG", txtBairroPag.Text);
                VCLIFOR.Set("CODCIDADEPAG", string.IsNullOrWhiteSpace(lpCodCidadePag.textBox1.Text) ? null : lpCodCidadePag.textBox1.Text);
                VCLIFOR.Set("CODETDPAG", string.IsNullOrEmpty(lpCodEtdPag.textBox1.Text) ? null : lpCodEtdPag.textBox1.Text);
                VCLIFOR.Set("CODPAISPAG", string.IsNullOrEmpty(lpCodPaisPag.textBox1.Text) ? null : lpCodPaisPag.textBox1.Text);
                VCLIFOR.Set("CONTRIBICMS", cmbContribIcms.SelectedValue);
                VCLIFOR.Set("NACIONALIDADE", cmbNacionalidade.SelectedValue);
                VCLIFOR.Set("CODETDEMISSOR", string.IsNullOrEmpty(lpCodEtdEmissor.textBox1.Text) ? null : lpCodEtdEmissor.textBox1.Text);
                VCLIFOR.Set("LIMITECREDITO", string.IsNullOrEmpty(txtLimiteCredito.Text) ? 0 : Convert.ToDecimal(txtLimiteCredito.Text));
                VCLIFOR.Set("CODREPRE", string.IsNullOrEmpty(lpCodRepre.textBox1.Text) ? null : lpCodRepre.textBox1.Text);
                VCLIFOR.Set("CODTRANSPORTADORA", string.IsNullOrEmpty(lpCodTransportadora.textBox1.Text) ? null : lpCodTransportadora.textBox1.Text);
                VCLIFOR.Set("FRETECIFFOB", cmbFreteCifFob.SelectedValue);
                VCLIFOR.Set("CODCONTA", string.IsNullOrEmpty(lpCodConta.textBox1.Text) ? null : lpCodConta.textBox1.Text);
                VCLIFOR.Set("CODCCUSTO", string.IsNullOrEmpty(lpCodCCusto.textBox1.Text) ? null : lpCodCCusto.textBox1.Text);
                VCLIFOR.Set("CODCONDICAOCOMPRA", string.IsNullOrEmpty(lpCodCondicaoCompra.textBox1.Text) ? null : lpCodCondicaoCompra.textBox1.Text);
                VCLIFOR.Set("CODCONDICAOVENDA", string.IsNullOrEmpty(lpCodCondicaoVenda.textBox1.Text) ? null : lpCodCondicaoVenda.textBox1.Text);
                if (txtEmailNfe.Text.Contains(";"))
                {
                    string NovoEmail = txtEmailNfe.Text;
                    txtEmailNfe.Text = NovoEmail.Replace(";", ",");
                }
                VCLIFOR.Set("EMAILNFE", txtEmailNfe.Text);
                VCLIFOR.Set("CODTABPRECO", cmbCodTabPreco.SelectedValue);
                VCLIFOR.Set("WEBSITE", txtWebSite.Text);
                if (edita == false)
                {
                    VCLIFOR.Set("DATACRIACAO", conn.GetDateTime());
                }
                else
                {
                    VCLIFOR.Set("DATACRIACAO", dteDataCriacao.EditValue);
                }

                VCLIFOR.Set("CODUSUARIOCRIACAO", string.IsNullOrEmpty(txtCodUsuarioCriacao.Text) ? AppLib.Context.Usuario : txtCodUsuarioCriacao.Text);
                VCLIFOR.Set("CODNATUREZAORCAMENTO", string.IsNullOrEmpty(lpCodNaturezaOrcamento.textBox1.Text) ? null : lpCodNaturezaOrcamento.textBox1.Text);
                VCLIFOR.Set("HISTORICO", meHistorico.Text);
                VCLIFOR.Set("IDTIPOCLIENTE", string.IsNullOrEmpty(lpIdTipoCliente.textBox1.Text) ? null : lpIdTipoCliente.textBox1.Text);
                VCLIFOR.Set("DESCMAXVENDA", string.IsNullOrEmpty(txtDescMaxVenda.Text) ? 0 : Convert.ToDecimal(txtDescMaxVenda.Text));
                VCLIFOR.Set("DESCMAXCOMPRA", string.IsNullOrEmpty(txtDescMaxCompra.Text) ? 0 : Convert.ToDecimal(txtDescMaxCompra.Text));
                VCLIFOR.Set("CLASSVENDA", cmbClassVenda.SelectedValue);
                VCLIFOR.Set("PRODUTORRURAL", chkProdutorRural.Checked == true ? 1 : 0);
                VCLIFOR.Set("CODVENDEDOR", string.IsNullOrEmpty(lpCodVendedor.textBox1.Text) ? null : lpCodVendedor.textBox1.Text);
                VCLIFOR.Set("CODFORMA", string.IsNullOrEmpty(lpFormaPagamento.txtcodigo.Text) ? null : lpFormaPagamento.txtcodigo.Text);
                VCLIFOR.Set("IDESTRANGEIRO", txtIdEstrangeiro.Text);
                VCLIFOR.Set("PIS", tbPis.Text);
                VCLIFOR.Set("CONSTRUCAOCIVIL", chkConstrucaoCivil.Checked == true ? 1 : 0);
                VCLIFOR.Set("AUTOMOTIVA", chkAutomotiva.Checked == true ? 1 : 0);

                VCLIFOR.Save();
                conn.Commit();
                edita = true;
            }
            catch (Exception ex)
            {
                conn.Rollback();
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnSiteSintegra_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtCgcCpf.Text.Replace(".", "").ToString().Replace("-", "").Replace("/", ""));
            ERP.Comercial.FormSiteSintegra frm = new ERP.Comercial.FormSiteSintegra();
            frm.ShowDialog();
        }

        private void btnCalculaCredito_Click(object sender, EventArgs e)
        {
            try
            {
                string CodLimiteCredito = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODQUERYLIMITECREDITO FROM VPARAMETROS WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }).ToString();

                if (!string.IsNullOrEmpty(CodLimiteCredito))
                {
                    string query = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT QUERY FROM GQUERY WHERE CODQUERY = ? AND CODEMPRESA = ?", new object[] { CodLimiteCredito, AppLib.Context.Empresa }).ToString();
                    query = query.Replace("@CODEMPRESA", AppLib.Context.Empresa.ToString()).Replace("@CODCLIFOR", codCliFor);

                    decimal resultado = Convert.ToDecimal(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, query, new object[] { }));
                    
                    txtSaldo.Text = string.Format("{0:n2}", resultado);
                    //if (txtSaldo.Text.Contains("-"))
                    //{
                    //    txtSaldo.Text.Replace("-", "");
                    //}
                    return;
                }
                else
                {
                    txtSaldo.Text = string.Format("{0:n2}", 0);
                }

                // João Pedro Luchiari - 29/11/2017 - Comentado por ser uma rotina antiga.
                //if (Convert.ToInt32(txtCodClifor.Text) > 0)
                //{
                //    PSPartCliForData psPartCliForData = new PSPartCliForData();

                //    Decimal? ValorAberto = psPartCliForData.FinanceiroEmAberto(PS.Lib.Contexto.Session.Empresa.CodEmpresa, txtCodClifor.Text);
                //    Decimal? LimiteCredito = Convert.ToDecimal(txtLimiteCredito.Text);
                //    txtSaldo.Text = string.Format("{0:n}", LimiteCredito - ValorAberto);
                //}
                //else
                //{
                //    MessageBox.Show("Salve o registro antes de prosseguir.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                salvar();
            }
            else
            {
                salvar();
                switch (lookup.CampoCodigo_Igual_a)
                {
                    case NewLookup.CampoCodigoIguala.CampoCodigoBD:

                        codCliFor = AppLib.Util.Format.CompletarZeroEsquerda(Convert.ToInt32(mask.Length), txtCodClifor.Text);
                        lpCodcliente = codCliFor;
                        carregaCampos();

                        lookup.txtcodigo.Text = txtCodClifor.Text;
                        lookup.txtconteudo.Text = txtNomeFantasia.Text.ToUpper();
                        lookup.ValorCodigoInterno = txtCodClifor.Text;

                        this.Dispose();
                        break;
                    case NewLookup.CampoCodigoIguala.CampoCodigoInterno:
                        if (lookup.CampoCodigoInterno.ToString() != lookup.CampoCodigoBD.ToString())
                        {
                            codCliFor = txtCodClifor.Text;
                            carregaCampos();

                            lookup.txtcodigo.Text = txtCodClifor.Text.ToUpper();
                            lookup.txtconteudo.Text = txtNomeFantasia.Text.ToUpper();
                            lookup.ValorCodigoInterno = txtCodClifor.Text.ToUpper();

                            this.Dispose();
                        }
                        else
                        {
                            codCliFor = txtCodClifor.Text;
                            carregaCampos();

                            lookup.txtcodigo.Text = txtCodClifor.Text;
                            lookup.txtconteudo.Text = txtNomeFantasia.Text.ToUpper();
                            lookup.ValorCodigoInterno = txtCodClifor.Text;

                            this.Dispose();
                        }
                        break;
                }
            }

        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (salvar() == true)
                {
                    this.Dispose();
                }
            }
            else
            {
                btnSalvarAtual.PerformClick();
            }
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnConsultaReceita_Click(object sender, EventArgs e)
        {
            if (cmbFisicoJuridico.Text == "Física")
            {
                ERP.Comercial.FormConsultaReceitaCPF frmCPF = new ERP.Comercial.FormConsultaReceitaCPF();
                frmCPF.maskedTextBox1.Text = txtCgcCpf.Text;
                frmCPF.maskedTextBox2.Text = dteDataNascimento.Text;
                frmCPF.ShowDialog();
            }
            else
            {
                ERP.Comercial.FormConsultaReceitaCNPJ frmCNPJ = new ERP.Comercial.FormConsultaReceitaCNPJ();
                frmCNPJ.maskedTextBox1.Text = txtCgcCpf.Text;
                frmCNPJ.ShowDialog();
                if (frmCNPJ.copiar == true)
                {
                    //Realiza a cópia dos dados do form
                    txtNome.Text = frmCNPJ.txtRazaoSocial.Text;
                    txtNomeFantasia.Text = frmCNPJ.txtNomeFantasia.Text;
                    txtCep.Text = frmCNPJ.txtCep.Text.Replace(".", "");
                    txtRua.Text = frmCNPJ.txtLogr.Text;
                    txtNumero.Text = frmCNPJ.txtNumero.Text;
                    txtBairro.Text = frmCNPJ.txtBairro.Text;
                    lpCodEtd.textBox1.Text = frmCNPJ.txtUF.Text;
                    lpCodEtd.LoadLookup();
                    lpCodPais.textBox1.Text = "1";
                    lpCodPais.LoadLookup();
                    //lpCodEtd.textBox2.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT NOME FROM GESTADO WHERE CODETD = ?", new object[] { frmCNPJ.txtUF.Text }).ToString();
                    lpCodCidade.textBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODCIDADE FROM GCIDADE WHERE NOME = ? AND CODETD = ?", new object[] { frmCNPJ.txtMunicipio.Text.Replace("'", " ").ToString(), frmCNPJ.txtUF.Text }).ToString();
                    //lpCodCidade.textBox2.Text = frmCNPJ.txtMunicipio.Text.Replace("'", " ").ToString();
                    lpCodCidade.LoadLookup();
                    txtTelComercial.Text = frmCNPJ.txtTelefone.Text;
                }
            }
        }

        private void btnSearchCep_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCep.Text))
            {
                PS.Glb.Class.WebCep web = new Class.WebCep(txtCep.Text);
                txtRua.Text = web.Lagradouro;
                lpCodTipoRua.textBox2.Text = web.TipoLagradouro;
                lpCodTipoRua.textBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODTIPORUA FROM GTIPORUA WHERE NOME = ?", new object[] { web.TipoLagradouro }).ToString();
                txtBairro.Text = web.Bairro;
                lpCodEtd.textBox1.Text = web.UF;
                lpCodEtd.textBox2.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM GESTADO WHERE CODETD = ?", new object[] { web.UF }).ToString();
                lpCodCidade.textBox2.Text = web.Cidade;
                lpCodCidade.textBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODCIDADE FROM GCIDADE WHERE NOME = ?", new object[] { web.Cidade }).ToString();
                lpCodPais.textBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODPAIS FROM GPAIS WHERE NOME = ?", new object[] { "Brasil" }).ToString();
                lpCodPais.textBox2.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM GPAIS WHERE CODPAIS = ?", new object[] { "1" }).ToString();
            }
            else
            {
                MessageBox.Show("Favor preecher o CEP corretamente", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSearchCepEnt_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCepEnt.Text))
            {
                PS.Glb.Class.WebCep web = new Class.WebCep(txtCep.Text);
                txtRuaEnt.Text = web.Lagradouro;
                lpCodTipoRuaEnt.textBox2.Text = web.TipoLagradouro;
                lpCodTipoRuaEnt.textBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODTIPORUA FROM GTIPORUA WHERE NOME = ?", new object[] { web.TipoLagradouro }).ToString();
                txtBairroEnt.Text = web.Bairro;
                lpCodEtdEnt.textBox1.Text = web.UF;
                lpCodEtdEnt.textBox2.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM GESTADO WHERE CODETD = ?", new object[] { web.UF }).ToString();
                lpCodCidadeEnt.textBox2.Text = web.Cidade;
                lpCodCidadeEnt.textBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODCIDADE FROM GCIDADE WHERE NOME = ?", new object[] { web.Cidade }).ToString();
                lpCodPaisEnt.textBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODPAIS FROM GPAIS WHERE NOME = ?", new object[] { "Brasil" }).ToString();
                lpCodPaisEnt.textBox2.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM GPAIS WHERE CODPAIS = ?", new object[] { "1" }).ToString();
            }
            else
            {
                MessageBox.Show("Favor preecher o CEP corretamente", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSearchCepPag_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCepPag.Text))
            {
                PS.Glb.Class.WebCep web = new Class.WebCep(txtCep.Text);
                txtRuaPag.Text = web.Lagradouro;
                lpCodTipoRuaPag.textBox2.Text = web.TipoLagradouro;
                lpCodTipoRuaPag.textBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODTIPORUA FROM GTIPORUA WHERE NOME = ?", new object[] { web.TipoLagradouro }).ToString();
                txtBairroPag.Text = web.Bairro;
                lpCodEtdPag.textBox1.Text = web.UF;
                lpCodEtdPag.textBox2.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM GESTADO WHERE CODETD = ?", new object[] { web.UF }).ToString();
                lpCodCidadePag.textBox2.Text = web.Cidade;
                lpCodCidadePag.textBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODCIDADE FROM GCIDADE WHERE NOME = ?", new object[] { web.Cidade }).ToString();
                lpCodPaisPag.textBox1.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODPAIS FROM GPAIS WHERE NOME = ?", new object[] { "Brasil" }).ToString();
                lpCodPaisPag.textBox2.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM GPAIS WHERE CODPAIS = ?", new object[] { "1" }).ToString();
            }
            else
            {
                MessageBox.Show("Favor preecher o CEP corretamente", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void chkUtilizarMesmoEnd_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUtilizarMesmoEnd.Checked == true)
            {
                txtCepEnt.Text = txtCep.Text;
                lpCodTipoRuaEnt.textBox1.Text = lpCodTipoRua.textBox1.Text;
                lpCodTipoRuaEnt.LoadLookup();
                txtNumeroEnt.Text = txtNumero.Text;
                txtRuaEnt.Text = txtRua.Text;
                txtComplementoEnt.Text = txtComplemento.Text;
                lpCodTipoBairroEnt.textBox1.Text = lpCodTipoBairro.textBox1.Text;
                lpCodTipoBairroEnt.LoadLookup();
                txtBairroEnt.Text = txtBairro.Text;
                lpCodPaisEnt.textBox1.Text = lpCodPais.textBox1.Text;
                lpCodPaisEnt.LoadLookup();
                lpCodEtdEnt.textBox1.Text = lpCodEtd.textBox1.Text;
                lpCodEtdEnt.LoadLookup();
                lpCodCidadeEnt.textBox1.Text = lpCodCidade.textBox1.Text;
                lpCodCidadeEnt.LoadLookup();

                txtCepPag.Text = txtCep.Text;
                lpCodTipoRuaPag.textBox1.Text = lpCodTipoRua.textBox1.Text;
                lpCodTipoRuaPag.LoadLookup();
                txtNumeroPag.Text = txtNumero.Text;
                txtRuaPag.Text = txtRua.Text;
                txtComplementoPag.Text = txtComplemento.Text;
                lpCodTipoBairroPag.textBox1.Text = lpCodTipoBairro.textBox1.Text;
                lpCodTipoBairroPag.LoadLookup();
                txtBairroPag.Text = txtBairro.Text;
                lpCodPaisPag.textBox1.Text = lpCodPais.textBox1.Text;
                lpCodPaisPag.LoadLookup();
                lpCodEtdPag.textBox1.Text = lpCodEtd.textBox1.Text;
                lpCodEtdPag.LoadLookup();
                lpCodCidadePag.textBox1.Text = lpCodCidade.textBox1.Text;
                lpCodCidadePag.LoadLookup();
            }
            else
            {
                txtCepEnt.Text = string.Empty;
                lpCodTipoRuaEnt.textBox1.Text = string.Empty;
                lpCodTipoRuaEnt.textBox2.Text = string.Empty;
                txtNumeroEnt.Text = string.Empty;
                txtRuaEnt.Text = string.Empty;
                txtComplementoEnt.Text = string.Empty;
                lpCodTipoBairroEnt.textBox1.Text = string.Empty;
                lpCodTipoBairroEnt.textBox2.Text = string.Empty;
                txtBairroEnt.Text = string.Empty;
                lpCodPaisEnt.textBox1.Text = string.Empty;
                lpCodPaisEnt.textBox2.Text = string.Empty;
                lpCodEtdEnt.textBox1.Text = string.Empty;
                lpCodEtdEnt.textBox2.Text = string.Empty;
                lpCodCidadeEnt.textBox1.Text = string.Empty;
                lpCodCidadeEnt.textBox2.Text = string.Empty;

                txtCepPag.Text = string.Empty;
                lpCodTipoRuaPag.textBox1.Text = string.Empty;
                lpCodTipoRuaPag.textBox2.Text = string.Empty;
                txtNumeroPag.Text = string.Empty;
                txtRuaPag.Text = string.Empty;
                txtComplementoPag.Text = string.Empty;
                lpCodTipoBairroPag.textBox1.Text = string.Empty;
                lpCodTipoBairroPag.textBox2.Text = string.Empty;
                txtBairroPag.Text = string.Empty;
                lpCodPaisPag.textBox1.Text = string.Empty;
                lpCodPaisPag.textBox2.Text = string.Empty;
                lpCodEtdPag.textBox1.Text = string.Empty;
                lpCodEtdPag.textBox2.Text = string.Empty;
                lpCodCidadePag.textBox1.Text = string.Empty;
                lpCodCidadePag.textBox2.Text = string.Empty;
            }
        }

        private void cmbFisicoJuridico_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCgcCpf.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple;
            if (cmbFisicoJuridico.SelectedIndex == 0)
            {
                txtCgcCpf.Properties.Mask.EditMask = "00.000.000/0000-00";

                groupBox3.Visible = false;
            }

            if (cmbFisicoJuridico.SelectedIndex == 1)
            {
                txtCgcCpf.Properties.Mask.EditMask = "000.000.000-00";

                groupBox3.Visible = true;
            }
        }

        private void lpCodCidade_BeforeLookup(object sender, Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODETD", lpCodEtd.Text));
        }

        private void lpCodCidadeEnt_BeforeLookup(object sender, Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODETD", lpCodEtdEnt.Text));
        }

        private void lpCodCidadePag_BeforeLookup(object sender, Lib.LookupEventArgs e)
        {
            e.Filtro.Add(new PS.Lib.PSFilter("CODETD", lpCodEtdPag.Text));
        }

        private void btnAddHistorico_Click(object sender, EventArgs e)
        {
            string historico = meAddHistorico.Text + "\r\n" + "Usuário: " + AppLib.Context.Usuario.ToString() + "\r\n" + "Data/Hora: " + string.Format("{0:dd/MM/yyyy HH:mm:ss}", AppLib.Context.poolConnection.Get("Start").GetDateTime());
            historico = historico + "\r\n \r\n" + getHistorico(txtCodClifor.Text, Convert.ToInt32(AppLib.Context.Empresa));
            inserirHistórico(txtCodClifor.Text, Convert.ToInt32(AppLib.Context.Empresa), historico);
        }

        private string getHistorico(string codClifor, int codEmpresa)
        {
            return AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT HISTORICO FROM VCLIFOR WHERE CODCLIFOR = ? AND CODEMPRESA = ?", new object[] { txtCodClifor.Text, AppLib.Context.Empresa }).ToString();
        }

        private void inserirHistórico(string codClifor, int codEmpresa, string historico)
        {
            AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE VCLIFOR SET HISTORICO = ? WHERE CODCLIFOR = ? AND CODEMPRESA = ?", new object[] { historico, codClifor, codEmpresa });
            meHistorico.Text = getHistorico(codClifor, codEmpresa);
            meAddHistorico.Text = "";
        }

        private void lpCodPais_AfterLookup(object sender, Lib.LookupEventArgs e)
        {
            if (lpCodPais.textBox2.Text != "Brasil" && lpCodPais.textBox2.Text != "BRASIL")
            {
                txtIdEstrangeiro.Enabled = true;
            }
            else
            {
                txtIdEstrangeiro.Enabled = false;
            }
        }

        private void toolStripDropDownButton2_Click(object sender, EventArgs e)
        {

        }

        #region Contato

        private void carregaGridContato()
        {
            string tabela = "VCLIFORCONTATO";
            string relacionamento = string.Empty;
            string query = string.Empty;
            string where = "WHERE CODCLIFOR = '" + txtCodClifor.Text + "' AND CODEMPRESA = '" + AppLib.Context.Empresa + "'";
            List<string> tabelasFilhas = new List<string>();

            string sql = new Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, where);

            if (string.IsNullOrEmpty(sql))
            {
                MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            gridControl1.DataSource = null;
            gridView1.Columns.Clear();


            DataTable dtGrid = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

            gridControl1.DataSource = dtGrid;

            DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                DataRow result = dic.Rows.Find(new object[] { gridView1.Columns[i].FieldName.ToString() });
                if (result != null)
                {
                    gridView1.Columns[i].Caption = result["DESCRICAO"].ToString();
                }
            }
        }

        private void CarregaGridUnidade()
        {
            DataTable dtUnidade = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT U.IDUNIDADE AS 'Código', U.NOME AS 'Unidade', U.CIDADE AS 'Cidade', U.CODETD AS 'UF', U.RUA AS 'Rua', U.COMPLEMENTO AS 'Complemento', U.BAIRRO AS 'Bairro', U.NUMERO AS 'Número', U.CEP
                                                                                        FROM VCLIFOR C 
                                                                                        INNER JOIN AUNIDADE U
                                                                                        ON U.CODCLIFOR = C.CODCLIFOR AND U.CODEMPRESA = C.CODEMPRESA 
                                                                                        WHERE C.CODEMPRESA = ? AND C.CODCLIFOR = ?", new object[] { AppLib.Context.Empresa, txtCodClifor.Text });

            gcUnidade.DataSource = dtUnidade;
        }

        private void btnAtualizarContato_Click(object sender, EventArgs e)
        {
            carregaGridContato();
        }

        private void btnNovoContato_Click(object sender, EventArgs e)
        {
            New.Cadastros.frmCadastroContatoCliente frm = new New.Cadastros.frmCadastroContatoCliente(txtCodClifor.Text);
            frm.edita = false;
            frm.ShowDialog();
            carregaGridContato();
        }

        private void btnEditarContato_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                New.Cadastros.frmCadastroContatoCliente frm = new New.Cadastros.frmCadastroContatoCliente(txtCodClifor.Text, Convert.ToInt32(row1["VCLIFORCONTATO.CODCONTATO"]));
                frm.edita = true;
                frm.ShowDialog();
                carregaGridContato();
            }
            else
            {
                gridControl1_DoubleClick(sender, e);
            }
        }

        private void btnPesquisarContato_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsFind.AlwaysVisible == true)
            {
                gridView1.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gridView1.OptionsFind.AlwaysVisible = true;
            }
        }

        private void btnAgruparContato_Click(object sender, EventArgs e)
        {
            if (gridView1.OptionsView.ShowGroupPanel == true)
            {
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.ClearGrouping();
                btnAgrupar.Text = "Agrupar";
            }
            else
            {
                gridView1.OptionsView.ShowGroupPanel = true;
                btnAgrupar.Text = "Desagrupar";
            }
        }

        private void btnSelecionarColunasContato_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas("VCLIFORCONTATO");
            frm.ShowDialog();
            carregaGridContato();
        }

        private void btnSalvarLayoutContato_Click(object sender, EventArgs e)
        {
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GVISAOUSUARIO SET VISIVEL = 0 WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, "VCLIFORCONTATO" });


            for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
            {
                AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                GVISAOUSUARIO.Set("VISAO", "VCLIFORCONTATO");
                GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
                GVISAOUSUARIO.Set("COLUNA", gridView1.VisibleColumns[i].FieldName);
                GVISAOUSUARIO.Set("SEQUENCIA", i);
                GVISAOUSUARIO.Set("LARGURA", gridView1.VisibleColumns[i].Width);
                GVISAOUSUARIO.Set("VISIVEL", 1);
                GVISAOUSUARIO.Save();
            }

            DataTable dtFixo = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ? AND FIXO = 1", new object[] { AppLib.Context.Usuario, "VCLIFORCONTATO" });
            if (dtFixo.Rows.Count > 0)
            {
                for (int i = 0; i < dtFixo.Rows.Count; i++)
                {
                    AppLib.ORM.Jit FIXO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                    FIXO.Set("VISAO", "VCLIFORCONTATO");
                    FIXO.Set("CODUSUARIO", AppLib.Context.Usuario);
                    FIXO.Set("COLUNA", dtFixo.Rows[i]["COLUNA"]);
                    FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
                carregaGridContato();
            }
        }

        #endregion

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void txtCgcCpf_Leave(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecGetField(false, @"SELECT CLIFORCONSISTECGCCPF FROM VPARAMETROS WHERE CODEMPRESA = ? ", new object[] { AppLib.Context.Empresa })) == true)
            {
                if (string.IsNullOrEmpty(txtCgcCpf.Text))
                {
                    MessageBox.Show("Favor preencher o campo CNPJ/CPF.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (txtCgcCpf.Text != "__.___.___/____-__" && txtCgcCpf.Text != "___.___.___-__")
            {
                PS.Lib.Valida valida = new Lib.Valida();
                if (Convert.ToString(cmbFisicoJuridico.SelectedValue) == "0")
                {
                    //Valida o CPF
                    if (valida.validarCNPJ(txtCgcCpf.Text).Equals(false))
                    {
                        MessageBox.Show("CNPJ digitado não é válido.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCgcCpf.Focus();
                        return;
                    }
                }
                else if (Convert.ToString(cmbFisicoJuridico.SelectedValue) == "1")
                {
                    //Valida o CNPJ
                    if (valida.validarCPF(txtCgcCpf.Text).Equals(false))
                    {
                        MessageBox.Show("CPF digitado não é válido.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCgcCpf.Focus();
                        return;
                    }
                }

                //Verifica se existe o CPF / CNPJ
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM VCLIFOR WHERE CODEMPRESA = ? AND  CGCCPF = ? AND CODCLIFOR <> ? ", new object[] { AppLib.Context.Empresa, txtCgcCpf.Text, codCliFor });
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Atenção. CGC/CPF informado já esta cadastrado", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCgcCpf.Focus();
                    return;
                }
            }
        }

        private void btnExcluirContato_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir os registros selecionados?", "Confirmação.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                        if (new Class.Utilidades().Excluir("CODCONTATO", "VCLIFORCONTATO", row["VCLIFORCONTATO.CODCONTATO"].ToString()) == true)
                        {
                            if (gridView1.SelectedRowsCount > 0)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Não foi possível excluir registro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    MessageBox.Show("Registro excluído com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    carregaGridContato();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void atualizaColuna(DataRow dr)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VCLIFORCONTATO WHERE CODCLIFOR = ? AND CODEMPRESA = ? AND CODCLIFOR = ?", new object[] { dr["VCLIFORCONTATO.CODCONTATO"], AppLib.Context.Empresa, codCliFor });

            for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
            {
                if (gridView1.Columns[i].ReadOnly == false)
                {
                    try
                    {
                        dr[gridView1.Columns[i].FieldName] = dr[gridView1.Columns[i].FieldName] = dt.Rows[0][gridView1.Columns[i].FieldName.ToString().Remove(0, gridView1.Columns[i].FieldName.ToString().IndexOf(".") + 1)].ToString();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void Atualizar()
        {
            if (this.lookup == null)
            {
                if (gridView1.SelectedRowsCount > 0)
                {

                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    New.Cadastros.frmCadastroContatoCliente frm = new New.Cadastros.frmCadastroContatoCliente(txtCodClifor.Text, Convert.ToInt32(row1["VCLIFORCONTATO.CODCONTATO"]));
                    frm.codContato = Convert.ToInt32(row1["VCLIFORCONTATO.CODCONTATO"]);
                    frm.edita = true;
                    frm.ShowDialog();
                    atualizaColuna(row1);
                    carregaGridContato();
                }
            }
            else
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                lookup.txtconteudo.Text = row1[tabelaContato + ".NOME"].ToString().ToUpper();
                lookup.txtcodigo.Text = row1[tabelaContato + ".CODCONTATO"].ToString();
                lookup.ValorCodigoInterno = row1[tabelaContato + ".CODCONTATO"].ToString();
                this.Dispose();
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                if (consulta == true)
                {
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                    codCliFor = row1["VCLIFORCONTATO.CODCONTATO"].ToString();
                    this.Dispose();
                }
                else
                {
                    Atualizar();
                }
            }
            else
            {
                // João Pedro Luchiari - 02/05/2018
                return;
                Atualizar();
            }
        }

        #region Movimentação de Produtos

        private void btnFiltrosMovProd_Click(object sender, EventArgs e)
        {
            PS.Glb.New.Filtro.frmFiltroClientesMovProd movprod = new Filtro.frmFiltroClientesMovProd();
            movprod.CodCliente = codCliFor;
            movprod.aberto = true;
            movprod.ShowDialog();
            if (this.lookup == null)
            {
                if (!string.IsNullOrEmpty(movprod.condicao))
                {
                    query = movprod.condicao;
                    CarregaGridMovimentacaoProduto(query);
                    btnAgruparMovProd.Enabled = true;
                    btnPesquisarMovProd.Enabled = true;
                    btnVisaoMovProd.Enabled = true;
                }
            }
            else
            {
                query = movprod.condicao;
                CarregaGridMovimentacaoProduto(query);
            }
        }



        private void btnAgruparMovProd_Click(object sender, EventArgs e)
        {
            if (gridView2.OptionsView.ShowGroupPanel == true)
            {
                gridView2.OptionsView.ShowGroupPanel = false;
                gridView2.ClearGrouping();
                btnAgruparMovProd.Text = "Agrupar";
            }
            else
            {
                gridView2.OptionsView.ShowGroupPanel = true;
                btnAgruparMovProd.Text = "Desagrupar";
            }
        }

        private void btnPesquisarMovProd_Click(object sender, EventArgs e)
        {
            if (gridView2.OptionsFind.AlwaysVisible == true)
            {
                gridView2.OptionsFind.AlwaysVisible = false;
            }
            else
            {
                gridView2.OptionsFind.AlwaysVisible = true;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) // Atualizar
        {
            CarregaGridMovimentacaoProduto(query);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e) // Seleção de Colunas
        {
            frmSelecaoColunas frm = new frmSelecaoColunas("MOVIMENTACAO");
            frm.ShowDialog();
            CarregaGridMovimentacaoProduto(query);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e) // Salvar layout
        {
            tabela = "MOVIMENTACAO";
            AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GVISAOUSUARIO SET VISIVEL = 0 WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, tabela });

            for (int i = 0; i < gridView2.VisibleColumns.Count; i++)
            {
                AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                GVISAOUSUARIO.Set("VISAO", tabela);
                GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
                GVISAOUSUARIO.Set("COLUNA", gridView2.VisibleColumns[i].FieldName);
                GVISAOUSUARIO.Set("SEQUENCIA", i);
                GVISAOUSUARIO.Set("LARGURA", gridView2.VisibleColumns[i].Width);
                GVISAOUSUARIO.Set("VISIVEL", 1);
                GVISAOUSUARIO.Save();
            }

            DataTable dtFixo = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA FROM GVISAOUSUARIO WHERE CODUSUARIO = ? AND VISAO = ? AND FIXO = 1", new object[] { AppLib.Context.Usuario, tabela });
            if (dtFixo.Rows.Count > 0)
            {
                for (int i = 0; i < dtFixo.Rows.Count; i++)
                {
                    AppLib.ORM.Jit FIXO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                    FIXO.Set("VISAO", tabela);
                    FIXO.Set("CODUSUARIO", AppLib.Context.Usuario);
                    FIXO.Set("COLUNA", dtFixo.Rows[i]["COLUNA"]);
                    FIXO.Set("LARGURA", 100);
                    FIXO.Set("VISIVEL", 1);
                    FIXO.Save();
                }
                CarregaGridMovimentacaoProduto(query);
            }
            tabela = "VCLIFOR";
        }
        private void CarregaGridMovimentacaoProduto(string where)
        {
            string relacao = "INNER JOIN GOPER ON GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA AND VCLIFOR.CODCLIFOR = GOPER.CODCLIFOR INNER JOIN GTIPOPER ON GTIPOPER.CODEMPRESA = GOPER.CODEMPRESA AND GTIPOPER.CODTIPOPER = GOPER.CODTIPOPER INNER JOIN GOPERITEM ON GOPERITEM.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEM.CODOPER = GOPER.CODOPER INNER JOIN VPRODUTO ON VPRODUTO.CODEMPRESA = GOPERITEM.CODEMPRESA AND VPRODUTO.CODPRODUTO = GOPERITEM.CODPRODUTO LEFT OUTER JOIN VNATUREZA ON VNATUREZA.CODEMPRESA = GOPERITEM.CODEMPRESA AND VNATUREZA.CODNATUREZA = GOPERITEM.CODNATUREZA LEFT OUTER JOIN GCIDADE ON GCIDADE.CODCIDADE = VCLIFOR.CODCIDADE AND GCIDADE.CODETD = VCLIFOR.CODETD";
            List<string> tabelasFilhas = new List<string>();
            tabelasFilhas.Add("GOPER.*");
            tabelasFilhas.Add("GOPERITEM.*");
            tabelasFilhas.Add("VPRODUTO.*");
            tabelasFilhas.Add("VNATUREZA");

            try
            {
                string sql = new Class.Utilidades().getVisao("MOVIMENTACAO", relacao, tabelasFilhas, where);

                sql = sql.Replace("MOVIMENTACAO", "VCLIFOR");
                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                gridControl2.DataSource = null;
                gridView2.Columns.Clear();
                gridControl2.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                if (gridView2.Columns["GOPER.CODSTATUS"] != null)
                {
                    carregaImagemStatus();
                }

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { "MOVIMENTACAO" });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { "MOVIMENTACAO", AppLib.Context.Usuario });
                for (int i = 0; i < gridView2.Columns.Count; i++)
                {
                    gridView2.Columns[i].Width = Convert.ToInt32(dt.Rows[i]["LARGURA"]);
                    dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                    DataRow result = dic.Rows.Find(new object[] { gridView2.Columns[i].FieldName.ToString() });
                    if (result != null)
                    {
                        gridView2.Columns[i].Caption = result["DESCRICAO"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void carregaImagemStatus()
        {
            DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox imageCombo = gridControl2.RepositoryItems.Add("ImageComboBoxEdit") as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            DevExpress.Utils.ImageCollection images = new DevExpress.Utils.ImageCollection();
            DataTable dtImagem = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT IMAGEM, DESCRICAO, CODSTATUS FROM GSTATUS WHERE TABELA = 'GOPER'");
            for (int i = 0; i < dtImagem.Rows.Count; i++)
            {
                byte[] imagemEmBytes = (byte[])dtImagem.Rows[i]["IMAGEM"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagemEmBytes);
                images.AddImage(Image.FromStream(ms));
                imageCombo.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(dtImagem.Rows[i]["DESCRICAO"].ToString(), dtImagem.Rows[i]["CODSTATUS"].ToString(), i));
            }
            imageCombo.SmallImages = images;
            imageCombo.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridView2.Columns["GOPER.CODSTATUS"].ColumnEdit = imageCombo;
        }

        #endregion

        private void btnMovProdExportarExcel_Click(object sender, EventArgs e)
        {
            string reportPath = string.Empty;
            FolderBrowserDialog open = new FolderBrowserDialog();
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                reportPath = open.SelectedPath + "Movimentação.xlsx";
                DevExpress.XtraPrinting.XlsExportOptions xlsxOptions = new DevExpress.XtraPrinting.XlsExportOptions();
                xlsxOptions.ShowGridLines = true;
                xlsxOptions.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Value;
                xlsxOptions.ExportHyperlinks = true;
                xlsxOptions.SheetName = "Movimentação";
                gridView2.ExportToXlsx(reportPath);
                StartProcess(reportPath);
            }
        }

        private void StartProcess(string path)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            try
            {
                process.StartInfo.FileName = path;
                process.Start();
                process.WaitForInputIdle();
            }
            catch { }
        }
    }
}
