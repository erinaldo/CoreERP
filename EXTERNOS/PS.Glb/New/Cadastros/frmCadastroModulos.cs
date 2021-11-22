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
    public partial class frmCadastroModulos : Form
    {
        public bool edita = false;
        public int IdParametro;

        public frmCadastroModulos()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "VPARAMETROS");

            #region Cliente/Fornecedor

            List<PS.Lib.ComboBoxItem> ListaQuantidadeDecimal = new List<PS.Lib.ComboBoxItem>();

            ListaQuantidadeDecimal.Add(new PS.Lib.ComboBoxItem());
            ListaQuantidadeDecimal[0].ValueMember = 2;
            ListaQuantidadeDecimal[0].DisplayMember = "2";

            ListaQuantidadeDecimal.Add(new PS.Lib.ComboBoxItem());
            ListaQuantidadeDecimal[1].ValueMember = 3;
            ListaQuantidadeDecimal[1].DisplayMember = "3";

            ListaQuantidadeDecimal.Add(new PS.Lib.ComboBoxItem());
            ListaQuantidadeDecimal[2].ValueMember = 4;
            ListaQuantidadeDecimal[2].DisplayMember = "4";

            ListaQuantidadeDecimal.Add(new PS.Lib.ComboBoxItem());
            ListaQuantidadeDecimal[3].ValueMember = 5;
            ListaQuantidadeDecimal[3].DisplayMember = "5";

            ListaQuantidadeDecimal.Add(new PS.Lib.ComboBoxItem());
            ListaQuantidadeDecimal[4].ValueMember = 6;
            ListaQuantidadeDecimal[4].DisplayMember = "6";

            cmbDecimalTabPreco.DataSource = ListaQuantidadeDecimal;
            cmbDecimalTabPreco.DisplayMember = "DisplayMember";
            cmbDecimalTabPreco.ValueMember = "ValueMember";

            #endregion

            #region Produtos/Serviços

            List<PS.Lib.ComboBoxItem> LisUsaPreco1 = new List<Lib.ComboBoxItem>();

            LisUsaPreco1.Add(new PS.Lib.ComboBoxItem());
            LisUsaPreco1[0].ValueMember = "E";
            LisUsaPreco1[0].DisplayMember = "Edita";

            LisUsaPreco1.Add(new PS.Lib.ComboBoxItem());
            LisUsaPreco1[1].ValueMember = "M";
            LisUsaPreco1[1].DisplayMember = "Não Mostra";

            cmbUsaPreco1.DataSource = LisUsaPreco1;
            cmbUsaPreco1.DisplayMember = "DisplayMember";
            cmbUsaPreco1.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> LisUsaPreco2 = new List<Lib.ComboBoxItem>();

            LisUsaPreco2.Add(new PS.Lib.ComboBoxItem());
            LisUsaPreco2[0].ValueMember = "E";
            LisUsaPreco2[0].DisplayMember = "Edita";

            LisUsaPreco2.Add(new PS.Lib.ComboBoxItem());
            LisUsaPreco2[1].ValueMember = "M";
            LisUsaPreco2[1].DisplayMember = "Não Mostra";

            cmbUsaPreco2.DataSource = LisUsaPreco2;
            cmbUsaPreco2.DisplayMember = "DisplayMember";
            cmbUsaPreco2.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> LisUsaPreco3 = new List<Lib.ComboBoxItem>();

            LisUsaPreco3.Add(new PS.Lib.ComboBoxItem());
            LisUsaPreco3[0].ValueMember = "E";
            LisUsaPreco3[0].DisplayMember = "Edita";

            LisUsaPreco3.Add(new PS.Lib.ComboBoxItem());
            LisUsaPreco3[1].ValueMember = "M";
            LisUsaPreco3[1].DisplayMember = "Não Mostra";

            cmbUsaPreco3.DataSource = LisUsaPreco3;
            cmbUsaPreco3.DisplayMember = "DisplayMember";
            cmbUsaPreco3.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> LisUsaPreco4 = new List<Lib.ComboBoxItem>();

            LisUsaPreco4.Add(new PS.Lib.ComboBoxItem());
            LisUsaPreco4[0].ValueMember = "E";
            LisUsaPreco4[0].DisplayMember = "Edita";

            LisUsaPreco4.Add(new PS.Lib.ComboBoxItem());
            LisUsaPreco4[1].ValueMember = "M";
            LisUsaPreco4[1].DisplayMember = "Não Mostra";

            cmbUsaPreco4.DataSource = LisUsaPreco4;
            cmbUsaPreco4.DisplayMember = "DisplayMember";
            cmbUsaPreco4.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> LisUsaPreco5 = new List<Lib.ComboBoxItem>();

            LisUsaPreco5.Add(new PS.Lib.ComboBoxItem());
            LisUsaPreco5[0].ValueMember = "E";
            LisUsaPreco5[0].DisplayMember = "Edita";

            LisUsaPreco5.Add(new PS.Lib.ComboBoxItem());
            LisUsaPreco5[1].ValueMember = "M";
            LisUsaPreco5[1].DisplayMember = "Não Mostra";

            cmbUsaPreco5.DataSource = LisUsaPreco5;
            cmbUsaPreco5.DisplayMember = "DisplayMember";
            cmbUsaPreco5.ValueMember = "ValueMember";

            #endregion

            #region Estoque

            List<PS.Lib.ComboBoxItem> listControlaSaldoEstoque = new List<PS.Lib.ComboBoxItem>();

            listControlaSaldoEstoque.Add(new PS.Lib.ComboBoxItem());
            listControlaSaldoEstoque[0].ValueMember = "B";
            listControlaSaldoEstoque[0].DisplayMember = "Bloqueia";

            listControlaSaldoEstoque.Add(new PS.Lib.ComboBoxItem());
            listControlaSaldoEstoque[1].ValueMember = "A";
            listControlaSaldoEstoque[1].DisplayMember = "Avisa";

            cmbControlaSaldoEstoque.DataSource = listControlaSaldoEstoque;
            cmbControlaSaldoEstoque.DisplayMember = "DisplayMember";
            cmbControlaSaldoEstoque.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> listBuscaProdutoPor = new List<PS.Lib.ComboBoxItem>();

            listBuscaProdutoPor.Add(new PS.Lib.ComboBoxItem());
            listBuscaProdutoPor[0].ValueMember = "0";
            listBuscaProdutoPor[0].DisplayMember = "Cód. Produto";

            listBuscaProdutoPor.Add(new PS.Lib.ComboBoxItem());
            listBuscaProdutoPor[1].ValueMember = "1";
            listBuscaProdutoPor[1].DisplayMember = "Cód. Auxiliar";

            cmbBuscaProdutoPor.DataSource = listBuscaProdutoPor;
            cmbBuscaProdutoPor.DisplayMember = "DisplayMember";
            cmbBuscaProdutoPor.ValueMember = "ValueMember";

            cmbCodQueryEstoqueMinimo.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODQUERY, DESCRICAO FROM GQUERY WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa });
            cmbCodQueryEstoqueMinimo.ValueMember = "CODQUERY";
            cmbCodQueryEstoqueMinimo.DisplayMember = "DESCRICAO";

            cmbFormulaCustoMedio.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODQUERY, DESCRICAO FROM GQUERY WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa });
            cmbFormulaCustoMedio.ValueMember = "CODQUERY";
            cmbFormulaCustoMedio.DisplayMember = "DESCRICAO";

            #endregion

            #region Financeiro

            List<PS.Lib.ComboBoxItem> listControlaLimiteCredito = new List<PS.Lib.ComboBoxItem>();

            listControlaLimiteCredito.Add(new PS.Lib.ComboBoxItem());
            listControlaLimiteCredito[0].ValueMember = 0;
            listControlaLimiteCredito[0].DisplayMember = "Não";

            listControlaLimiteCredito.Add(new PS.Lib.ComboBoxItem());
            listControlaLimiteCredito[1].ValueMember = 1;
            listControlaLimiteCredito[1].DisplayMember = "Sim";

            cmbControlaLimiteCredito.DataSource = listControlaLimiteCredito;
            cmbControlaLimiteCredito.DisplayMember = "DisplayMember";
            cmbControlaLimiteCredito.ValueMember = "ValueMember";

            cmbCodQueryLimiteCredito.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT CODQUERY, DESCRICAO FROM GQUERY WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa });
            cmbCodQueryLimiteCredito.ValueMember = "CODQUERY";
            cmbCodQueryLimiteCredito.DisplayMember = "DESCRICAO";

            #endregion

            #region E-mail

            List<PS.Lib.ComboBoxItem> listUsaSSL = new List<PS.Lib.ComboBoxItem>();

            listUsaSSL.Add(new PS.Lib.ComboBoxItem(0, "Não"));
            listUsaSSL.Add(new PS.Lib.ComboBoxItem(1, "Sim"));
            cmbEmailUsaSsl.DataSource = listUsaSSL;
            cmbEmailUsaSsl.DisplayMember = "DisplayMember";
            cmbEmailUsaSsl.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> listEmailAutentica = new List<PS.Lib.ComboBoxItem>();

            listEmailAutentica.Add(new PS.Lib.ComboBoxItem(0, "Não"));
            listEmailAutentica.Add(new PS.Lib.ComboBoxItem(1, "Sim"));
            cmbEmailAutentica.DataSource = listEmailAutentica;
            cmbEmailAutentica.DisplayMember = "DisplayMember";
            cmbEmailAutentica.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> ListEmailRemetente = new List<PS.Lib.ComboBoxItem>();

            ListEmailRemetente.Add(new PS.Lib.ComboBoxItem(0, "E-mail da Empresa corrente"));
            ListEmailRemetente.Add(new PS.Lib.ComboBoxItem(1, "E-mail do Usuário corrente"));
            cmbEmailRemetente.DataSource = ListEmailRemetente;
            cmbEmailRemetente.DisplayMember = "DisplayMember";
            cmbEmailRemetente.ValueMember = "ValueMember";

            #endregion
        }

        private void frmCadastroModulos_Load(object sender, EventArgs e)
        {
            carregaCampos();
        }

        private void carregaCampos()
        {
            DataTable dt;
            dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM VPARAMETROS WHERE CODEMPRESA = ? AND IDPARAMETRO = ?", new object[] { AppLib.Context.Empresa, IdParametro });
            if (dt.Rows.Count > 0)
            {
                carregaObj(dt);
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbMaskCliforNumeroSeq.Text = dt.Rows[0]["CLIFORMASKNUMEROSEQ"].ToString();
            tbCliforUltimoNumero.Text = dt.Rows[0]["CLIFORULTIMONUMERO"].ToString();
            chkConsisteCGCCPF.Checked = Convert.ToBoolean(dt.Rows[0]["CLIFORCONSISTECGCCPF"]);

            if (!string.IsNullOrEmpty(dt.Rows[0]["DECIMALTABPRECO"].ToString()))
            {
                cmbDecimalTabPreco.SelectedValue = Convert.ToInt32(dt.Rows[0]["DECIMALTABPRECO"]);
            }      

            cmbUsaPreco1.SelectedValue = dt.Rows[0]["PRDUSAPRECO1"];
            cmbUsaPreco2.SelectedValue = dt.Rows[0]["PRDUSAPRECO2"];
            cmbUsaPreco3.SelectedValue = dt.Rows[0]["PRDUSAPRECO3"];
            cmbUsaPreco4.SelectedValue = dt.Rows[0]["PRDUSAPRECO4"];
            cmbUsaPreco5.SelectedValue = dt.Rows[0]["PRDUSAPRECO5"];
            tbTextoPreco1.Text = dt.Rows[0]["PRDTEXTOPRECO1"].ToString();
            tbTextoPreco2.Text = dt.Rows[0]["PRDTEXTOPRECO2"].ToString();
            tbTextoPreco3.Text = dt.Rows[0]["PRDTEXTOPRECO3"].ToString();
            tbTextoPreco4.Text = dt.Rows[0]["PRDTEXTOPRECO4"].ToString();
            tbTextoPreco5.Text = dt.Rows[0]["PRDTEXTOPRECO5"].ToString();
            tbMaskProdServ.Text = dt.Rows[0]["MASKPRODSERV"].ToString();

            tbCodtipOperEntrada.Text = dt.Rows[0]["CODTIPOPERINVENTRADA"].ToString();
            tbCodtipOperSaida.Text = dt.Rows[0]["CODTIPOPERINVSAIDA"].ToString();
            cmbCodQueryEstoqueMinimo.SelectedValue = dt.Rows[0]["CODQUERYESTOQUEMINIMO"];
            cmbFormulaCustoMedio.SelectedValue = dt.Rows[0]["FORMULACUSTOMEDIO"];
            cmbControlaSaldoEstoque.SelectedValue = dt.Rows[0]["CONTROLASALDOESTQUE"];
            cmbBuscaProdutoPor.SelectedValue = dt.Rows[0]["BUSCAPRODUTOPOR"];
            dteDataFechamentoEstoque.DateTime = Convert.ToDateTime(dt.Rows[0]["DATAFECHAMENTOESTOQUE"]);

            tbMaskNumeroSeq.Text = dt.Rows[0]["MASKNUMEROSEQ"].ToString();
            lpMoeda.txtcodigo.Text = dt.Rows[0]["CODMOEDAPADRAO"].ToString();
            lpMoeda.CarregaDescricao();
            lpTipDocRec.txtcodigo.Text = dt.Rows[0]["DOCDEFAULTFATR"].ToString();
            lpTipDocRec.CarregaDescricao();
            lpTipDocPag.txtcodigo.Text = dt.Rows[0]["DOCDEFAULTFATP"].ToString();
            lpTipDocPag.CarregaDescricao();
            cmbControlaLimiteCredito.SelectedValue = dt.Rows[0]["CONTROLALIMITECREDITO"];
            cmbCodQueryLimiteCredito.SelectedValue = dt.Rows[0]["CODQUERYLIMITECREDITO"];
            tbMaskCentroCusto.Text = dt.Rows[0]["MASKCENTROCUSTO"].ToString();
            tbMaskNaturezaOrcamento.Text = dt.Rows[0]["MASKNATUREZAORCAMENTO"].ToString();
            tbMaskDepartamento.Text = dt.Rows[0]["MASKDEPARTAMENTO"].ToString();

            tbEmailHost.Text = dt.Rows[0]["EMAILHOST"].ToString();
            tbEmailPorta.Text = dt.Rows[0]["EMAILPORTA"].ToString();
            tbEmailUsuario.Text = dt.Rows[0]["EMAILUSUARIO"].ToString();
            tbEmailSenha.Text = dt.Rows[0]["EMAILSENHA"].ToString();
            cmbEmailRemetente.SelectedValue = dt.Rows[0]["EMAILREMETENTE"];
            cmbEmailUsaSsl.SelectedValue = dt.Rows[0]["EMAILUSASSL"];
            cmbEmailAutentica.SelectedValue = dt.Rows[0]["EMAILAUTENTICA"];

            chkIntegrarReembolsoAnalista.Checked = Convert.ToBoolean(dt.Rows[0]["INTEGRAREEMBOLSOANALISTA"]);
        }

        private bool Salvar()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit VPARAMETROS = new AppLib.ORM.Jit(conn, "VPARAMETROS");
            conn.BeginTransaction();

            try
            {
                VPARAMETROS.Set("CODEMPRESA", AppLib.Context.Empresa);
                VPARAMETROS.Set("IDPARAMETRO", 1);
                VPARAMETROS.Set("CLIFORMASKNUMEROSEQ", tbMaskCliforNumeroSeq.Text);
                VPARAMETROS.Set("CLIFORULTIMONUMERO", tbCliforUltimoNumero.Text);
                VPARAMETROS.Set("CLIFORCONSISTECGCCPF", chkConsisteCGCCPF.Checked == true ? 1 :0);
                VPARAMETROS.Set("DECIMALTABPRECO", cmbDecimalTabPreco.SelectedValue);

                VPARAMETROS.Set("PRDUSAPRECO1", cmbUsaPreco1.SelectedValue);
                VPARAMETROS.Set("PRDUSAPRECO2", cmbUsaPreco2.SelectedValue);
                VPARAMETROS.Set("PRDUSAPRECO3", cmbUsaPreco3.SelectedValue);
                VPARAMETROS.Set("PRDUSAPRECO4", cmbUsaPreco4.SelectedValue);
                VPARAMETROS.Set("PRDUSAPRECO5", cmbUsaPreco5.SelectedValue);
                VPARAMETROS.Set("PRDTEXTOPRECO1", tbTextoPreco1.Text);
                VPARAMETROS.Set("PRDTEXTOPRECO2", tbTextoPreco2.Text);
                VPARAMETROS.Set("PRDTEXTOPRECO3", tbTextoPreco3.Text);
                VPARAMETROS.Set("PRDTEXTOPRECO4", tbTextoPreco4.Text);
                VPARAMETROS.Set("PRDTEXTOPRECO5", tbTextoPreco5.Text);
                VPARAMETROS.Set("MASKPRODSERV", tbMaskProdServ.Text);

                VPARAMETROS.Set("CODTIPOPERINVENTRADA", tbCodtipOperEntrada.Text);
                VPARAMETROS.Set("CODTIPOPERINVSAIDA", tbCodtipOperSaida.Text);
                VPARAMETROS.Set("CODQUERYESTOQUEMINIMO", cmbCodQueryEstoqueMinimo.SelectedValue);
                VPARAMETROS.Set("FORMULACUSTOMEDIO", cmbFormulaCustoMedio.SelectedValue);
                VPARAMETROS.Set("CONTROLASALDOESTQUE", cmbControlaSaldoEstoque.SelectedValue);
                VPARAMETROS.Set("BUSCAPRODUTOPOR", cmbBuscaProdutoPor.SelectedValue);
                VPARAMETROS.Set("DATAFECHAMENTOESTOQUE", Convert.ToDateTime(dteDataFechamentoEstoque.DateTime));

                VPARAMETROS.Set("MASKNUMEROSEQ", tbMaskNumeroSeq.Text);

                if (!string.IsNullOrEmpty(lpMoeda.txtcodigo.Text))
                {
                    VPARAMETROS.Set("CODMOEDAPADRAO", lpMoeda.txtcodigo.Text);
                }
                else
                {
                    VPARAMETROS.Set("CODMOEDAPADRAO", null);
                }

                if (!string.IsNullOrEmpty(lpTipDocRec.txtcodigo.Text))
                {
                    VPARAMETROS.Set("DOCDEFAULTFATR", lpTipDocRec.txtcodigo.Text);
                }
                else
                {
                    VPARAMETROS.Set("DOCDEFAULTFATR", null);
                }

                if (!string.IsNullOrEmpty(lpTipDocPag.txtcodigo.Text))
                {
                    VPARAMETROS.Set("DOCDEFAULTFATP", lpTipDocPag.txtcodigo.Text);
                }
                else
                { 
                    VPARAMETROS.Set("DOCDEFAULTFATP", null);
                }

                VPARAMETROS.Set("CONTROLALIMITECREDITO", cmbControlaLimiteCredito.SelectedValue);
                VPARAMETROS.Set("CODQUERYLIMITECREDITO", cmbCodQueryLimiteCredito.SelectedValue);
                VPARAMETROS.Set("MASKCENTROCUSTO", tbMaskCentroCusto.Text);
                VPARAMETROS.Set("MASKNATUREZAORCAMENTO", tbMaskNaturezaOrcamento.Text);
                VPARAMETROS.Set("MASKDEPARTAMENTO", tbMaskDepartamento.Text);

                VPARAMETROS.Set("EMAILHOST", tbEmailHost.Text);
                VPARAMETROS.Set("EMAILPORTA", tbEmailPorta.Text);
                VPARAMETROS.Set("EMAILUSUARIO", tbEmailUsuario.Text);
                VPARAMETROS.Set("EMAILSENHA", tbEmailSenha.Text);
                VPARAMETROS.Set("EMAILREMETENTE", cmbEmailRemetente.SelectedValue);
                VPARAMETROS.Set("EMAILUSASSL", cmbEmailUsaSsl.SelectedValue);
                VPARAMETROS.Set("EMAILAUTENTICA", cmbEmailAutentica.SelectedValue);

                VPARAMETROS.Set("INTEGRAREEMBOLSOANALISTA", chkIntegrarReembolsoAnalista.Checked == true ? 1 : 0);

                VPARAMETROS.Save();
                conn.Commit();
                edita = true;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Rollback();
                return false;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                this.Dispose();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
