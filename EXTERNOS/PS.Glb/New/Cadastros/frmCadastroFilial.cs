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
    public partial class frmCadastroFilial : Form
    {
        public bool edita = false;
        public string Codfilial = string.Empty;
        string tabela = "GFILIALAUTXML";
        private List<string> tabelasFilhas = new List<string>();
        public string codMenu;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmCadastroFilial()
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "GFILIAL");

            #region Regime Tributário

            List<PS.Lib.ComboBoxItem> ListRegime = new List<PS.Lib.ComboBoxItem>();

            ListRegime.Add(new PS.Lib.ComboBoxItem());
            ListRegime[0].ValueMember = 1;
            ListRegime[0].DisplayMember = "Simples Nacional";

            ListRegime.Add(new PS.Lib.ComboBoxItem());
            ListRegime[1].ValueMember = 3;
            ListRegime[1].DisplayMember = "Regime Normal";

            cmbCodRegimeTributario.DataSource = ListRegime;
            cmbCodRegimeTributario.DisplayMember = "DisplayMember";
            cmbCodRegimeTributario.ValueMember = "ValueMember";

            #endregion

            #region Fiscal

            List<PS.Lib.ComboBoxItem> ListVersao = new List<Lib.ComboBoxItem>();

            ListVersao.Add(new PS.Lib.ComboBoxItem());
            ListVersao[0].ValueMember = "3.10";
            ListVersao[0].DisplayMember = "3.10";

            ListVersao.Add(new PS.Lib.ComboBoxItem());
            ListVersao[1].ValueMember = "4.00";
            ListVersao[1].DisplayMember = "4.00";

            cmbVersao.DataSource = ListVersao;
            cmbVersao.DisplayMember = "DisplayMember";
            cmbVersao.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> ListTipoAmbiente = new List<Lib.ComboBoxItem>();

            ListTipoAmbiente.Add(new PS.Lib.ComboBoxItem());
            ListTipoAmbiente[0].ValueMember = 1;
            ListTipoAmbiente[0].DisplayMember = "Produção";

            ListTipoAmbiente.Add(new PS.Lib.ComboBoxItem());
            ListTipoAmbiente[1].ValueMember = 2;
            ListTipoAmbiente[1].DisplayMember = "Homologação";

            cmbTpAmb.DataSource = ListTipoAmbiente;
            cmbTpAmb.ValueMember = "ValueMember";
            cmbTpAmb.DisplayMember = "DisplayMember";

            List<PS.Lib.ComboBoxItem> ListModalidade = new List<Lib.ComboBoxItem>();

            ListModalidade.Add(new PS.Lib.ComboBoxItem());
            ListModalidade[0].ValueMember = 1;
            ListModalidade[0].DisplayMember = "Normal";

            ListModalidade.Add(new PS.Lib.ComboBoxItem());
            ListModalidade[1].ValueMember = 6;
            ListModalidade[1].DisplayMember = "SVC-AN";

            cmbModalidade.DataSource = ListModalidade;
            cmbModalidade.ValueMember = "ValueMember";
            cmbModalidade.DisplayMember = "DisplayMember";

            #endregion
        }

        public frmCadastroFilial(ref NewLookup lookup)
        {
            InitializeComponent();
            new Class.Utilidades().getDicionario(this, tabControl1, "GFILIAL");

            #region Regime Tributário

            List<PS.Lib.ComboBoxItem> ListRegime = new List<PS.Lib.ComboBoxItem>();

            ListRegime.Add(new PS.Lib.ComboBoxItem());
            ListRegime[0].ValueMember = 1;
            ListRegime[0].DisplayMember = "Simples Nacional";

            ListRegime.Add(new PS.Lib.ComboBoxItem());
            ListRegime[1].ValueMember = 3;
            ListRegime[1].DisplayMember = "Regime Normal";

            cmbCodRegimeTributario.DataSource = ListRegime;
            cmbCodRegimeTributario.DisplayMember = "DisplayMember";
            cmbCodRegimeTributario.ValueMember = "ValueMember";

            #endregion

            #region Fiscal

            List<PS.Lib.ComboBoxItem> ListVersao = new List<Lib.ComboBoxItem>();

            ListVersao.Add(new PS.Lib.ComboBoxItem());
            ListVersao[0].ValueMember = "3.10";
            ListVersao[0].DisplayMember = "3.10";

            ListVersao.Add(new PS.Lib.ComboBoxItem());
            ListVersao[1].ValueMember = "4.00";
            ListVersao[1].DisplayMember = "4.00";

            cmbVersao.DataSource = ListVersao;
            cmbVersao.DisplayMember = "DisplayMember";
            cmbVersao.ValueMember = "ValueMember";

            List<PS.Lib.ComboBoxItem> ListTipoAmbiente = new List<Lib.ComboBoxItem>();

            ListTipoAmbiente.Add(new PS.Lib.ComboBoxItem());
            ListTipoAmbiente[0].ValueMember = 1;
            ListTipoAmbiente[0].DisplayMember = "Produção";

            ListTipoAmbiente.Add(new PS.Lib.ComboBoxItem());
            ListTipoAmbiente[1].ValueMember = 2;
            ListTipoAmbiente[1].DisplayMember = "Homologação";

            cmbTpAmb.DataSource = ListTipoAmbiente;
            cmbTpAmb.ValueMember = "ValueMember";
            cmbTpAmb.DisplayMember = "DisplayMember";

            List<PS.Lib.ComboBoxItem> ListModalidade = new List<Lib.ComboBoxItem>();

            ListModalidade.Add(new PS.Lib.ComboBoxItem());
            ListModalidade[0].ValueMember = 1;
            ListModalidade[0].DisplayMember = "Normal";

            ListModalidade.Add(new PS.Lib.ComboBoxItem());
            ListModalidade[1].ValueMember = 6;
            ListModalidade[1].DisplayMember = "SVC-AN";

            cmbModalidade.DataSource = ListModalidade;
            cmbModalidade.ValueMember = "ValueMember";
            cmbModalidade.DisplayMember = "DisplayMember";

            #endregion

            this.edita = true;
            this.lookup = lookup;
            Codfilial = lookup.ValorCodigoInterno;
            carregaCampos();
        }

        private void frmCadastroFilial_Load(object sender, EventArgs e)
        {
            if (edita == true)
            {
                carregaCampos();
                tbCodFilial.Enabled = false;
                CarregaGridAutXML("WHERE CODEMPRESA = " + AppLib.Context.Empresa + " AND CODFILIAL = " + Codfilial + "");
            }
        }

        private bool validaCodigo()
        {
            bool verifica = true;

            errorProvider1.Clear();

            if (string.IsNullOrEmpty(tbCodFilial.Text))
            {
                errorProvider1.SetError(tbCodFilial, "Favor informar o código.");
                verifica = false;
            }
            return verifica;
        }

        private void carregaCampos()
        {
            DataTable dt;
            if (this.lookup == null)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GFILIAL WHERE CODFILIAL = ?", new object[] { Codfilial });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GFILIAL WHERE CODFILIAL = ?", new object[] { Codfilial });
                if (dt.Rows.Count > 0)
                {
                    carregaObj(dt);
                }
            }
        }

        private void carregaObj(DataTable dt)
        {
            tbCodFilial.Text = dt.Rows[0]["CODFILIAL"].ToString();
            tbIdentificacao.Text = dt.Rows[0]["IDENTIFICACAO"].ToString();
            cmbCodRegimeTributario.SelectedValue = dt.Rows[0]["CODREGIMETRIBUTARIO"];
            tbNome.Text = dt.Rows[0]["NOME"].ToString();
            tbCGCCPF.Text = dt.Rows[0]["CGCCPF"].ToString();
            tbNomeFantasia.Text = dt.Rows[0]["NOMEFANTASIA"].ToString();
            tbPercentual.Text = dt.Rows[0]["PCREDSN"].ToString();
            tbEmail.Text = dt.Rows[0]["EMAIL"].ToString();
            tbWebSite.Text = dt.Rows[0]["WEBSITE"].ToString();
            tbInscricaoEstadual.Text = dt.Rows[0]["INSCRICAOESTADUAL"].ToString();
            tbInscricaoMunicipal.Text = dt.Rows[0]["INSCRICAOMUNICIPAL"].ToString();
            tbTelefone.Text = dt.Rows[0]["TELEFONE"].ToString();

            lpTipoRua.txtcodigo.Text = dt.Rows[0]["CODTIPORUA"].ToString();
            lpTipoRua.CarregaDescricao();
            tbRua.Text = dt.Rows[0]["RUA"].ToString();
            tbNumero.Text = dt.Rows[0]["NUMERO"].ToString();
            tbComplemento.Text = dt.Rows[0]["COMPLEMENTO"].ToString();
            lpTipoBairro.txtcodigo.Text = dt.Rows[0]["CODTIPOBAIRRO"].ToString();
            lpTipoBairro.CarregaDescricao();
            tbBairro.Text = dt.Rows[0]["BAIRRO"].ToString();
            tbCep.Text = dt.Rows[0]["CEP"].ToString();
            lpPais.txtcodigo.Text = dt.Rows[0]["CODPAIS"].ToString();
            lpPais.CarregaDescricao();
            lpEstado.txtcodigo.Text = dt.Rows[0]["CODETD"].ToString();
            lpEstado.CarregaDescricao();
            lpCidade.txtcodigo.Text = dt.Rows[0]["CODCIDADE"].ToString();
            lpCidade.CarregaDescricao();

            chkSeqSegundoNumero.Checked = Convert.ToBoolean(dt.Rows[0]["SEQSEGUNDONUMERO"]);
            tbQtdSegundoNumero.Text = dt.Rows[0]["QTDSEGUNDONUMERO"].ToString();
            tbSegundoNumero.Text = dt.Rows[0]["SEGUNDONUMERO"].ToString();

            cmbVersao.SelectedValue = dt.Rows[0]["VERSAO"];
            cmbTpAmb.SelectedValue = dt.Rows[0]["TPAMB"];
            cmbModalidade.SelectedValue = dt.Rows[0]["MODALIDADE"];
            tbPastaDestino.Text = dt.Rows[0]["PASTADESTINO"].ToString();
            tbToken.Text = dt.Rows[0]["TOKEN"].ToString();
            MemoTextoEmailNfe.Text = dt.Rows[0]["TEXTOEMAILNFE"].ToString();
        }

        public void CarregaGridAutXML(string where)
        {
            try
            {
                string sql = string.Empty;

                sql = new Class.Utilidades().getVisao(tabela, string.Empty, tabelasFilhas, where);

                if (string.IsNullOrEmpty(sql))
                {
                    MessageBox.Show("Não foi possível obter o retorno da visão.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

                DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT COLUNA, LARGURA FROM GVISAOUSUARIO WHERE VISAO = ? AND CODUSUARIO = ? AND VISIVEL = 1 ORDER BY SEQUENCIA", new object[] { tabela, AppLib.Context.Usuario });
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

                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchCep_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbCep.Text))
            {
                PS.Glb.Class.WebCep web = new Class.WebCep(tbCep.Text);
                tbRua.Text = web.Lagradouro;
                lpTipoRua.txtconteudo.Text = web.TipoLagradouro;
                lpTipoRua.txtcodigo.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODTIPORUA FROM GTIPORUA WHERE NOME = ?", new object[] { web.TipoLagradouro }).ToString();
                tbBairro.Text = web.Bairro;
                lpEstado.txtcodigo.Text = web.UF;
                lpEstado.txtconteudo.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM GESTADO WHERE CODETD = ?", new object[] { web.UF }).ToString();
                lpCidade.txtconteudo.Text = web.Cidade;
                lpCidade.txtcodigo.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODCIDADE FROM GCIDADE WHERE NOME = ?", new object[] { web.Cidade }).ToString();
                lpPais.txtcodigo.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODPAIS FROM GPAIS WHERE NOME = ?", new object[] { "Brasil" }).ToString();
                lpPais.txtconteudo.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM GPAIS WHERE CODPAIS = ?", new object[] { "1" }).ToString();
            }
        }

        #region Autorização 

        private void btnNovoAutorizacao_Click(object sender, EventArgs e)
        {
            PS.Glb.New.Cadastros.frmCadastroAutorizacaoXML AutXML = new frmCadastroAutorizacaoXML();
            AutXML.Codfilial = tbCodFilial.Text;
            AutXML.edita = false;
            AutXML.ShowDialog();
            CarregaGridAutXML("WHERE CODEMPRESA = " + AppLib.Context.Empresa + " AND CODFILIAL = " + Codfilial + "");
        }

        private void btnEditarAutorizacao_Click(object sender, EventArgs e)
        {
            PS.Glb.New.Cadastros.frmCadastroAutorizacaoXML AutXML = new frmCadastroAutorizacaoXML();
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            AutXML.Codfilial = row1["GFILIALAUTXML.CODFILIAL"].ToString();
            AutXML.Identificador = Convert.ToInt32(row1["GFILIALAUTXML.IDENTIFICADOR"]);
            AutXML.edita = true;
            AutXML.ShowDialog();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            PS.Glb.New.Cadastros.frmCadastroAutorizacaoXML AutXML = new frmCadastroAutorizacaoXML();
            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
            AutXML.Codfilial = row1["GFILIALAUTXML.CODFILIAL"].ToString();
            AutXML.Identificador = Convert.ToInt32(row1["GFILIALAUTXML.IDENTIFICADOR"]);
            AutXML.edita = true;
            AutXML.ShowDialog();
        }

        private void btnExcluirAutorizacao_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir os registros selecionados?", "Confirmação.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                    {
                        DataRow row = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                        AppLib.Context.poolConnection.Get("Start").ExecTransaction("DELETE FROM GFILIALAUTXML WHERE IDENTIFICADOR = ? AND CODFILIAL = ? AND CODEMPRESA = ?", new object[] { row["GFILIALAUTXML.IDENTIFICADOR"], row["GFILIALAUTXML.CODFILIAL"], AppLib.Context.Empresa });
                    }
                    MessageBox.Show("Registro excluído com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregaGridAutXML("WHERE CODEMPRESA = " + AppLib.Context.Empresa + " AND CODFILIAL = " + Codfilial + "");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPesquisarAutorizacao_Click(object sender, EventArgs e)
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

        private void btnAgruparAutorizacao_Click(object sender, EventArgs e)
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

        #endregion

        private bool Salvar()
        {
            if (validaCodigo() == false)
            {
                return false;
            }

            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            AppLib.ORM.Jit GFILIAL = new AppLib.ORM.Jit(conn, "GFILIAL");
            conn.BeginTransaction();

            try
            {
                GFILIAL.Set("CODEMPRESA", AppLib.Context.Empresa);
                GFILIAL.Set("CODFILIAL", Convert.ToInt32(tbCodFilial.Text));
                GFILIAL.Set("IDENTIFICACAO", tbIdentificacao.Text);
                GFILIAL.Set("CODREGIMETRIBUTARIO", cmbCodRegimeTributario.SelectedValue);
                GFILIAL.Set("NOME", tbNome.Text);
                GFILIAL.Set("CGCCPF", tbCGCCPF.Text);
                GFILIAL.Set("NOMEFANTASIA", tbNomeFantasia.Text);
                GFILIAL.Set("PCREDSN", Convert.ToDecimal(tbPercentual.Text));
                GFILIAL.Set("EMAIL", tbEmail.Text);
                GFILIAL.Set("WEBSITE", tbWebSite.Text);
                GFILIAL.Set("INSCRICAOESTADUAL", tbInscricaoEstadual.Text);
                GFILIAL.Set("INSCRICAOMUNICIPAL", tbInscricaoMunicipal.Text);
                GFILIAL.Set("TELEFONE", tbTelefone.Text);

                if (!string.IsNullOrEmpty(lpTipoRua.ValorCodigoInterno))
                {
                    GFILIAL.Set("CODTIPORUA", lpTipoRua.ValorCodigoInterno);
                }
                else
                {
                    GFILIAL.Set("CODTIPORUA", null);
                }

                GFILIAL.Set("RUA", tbRua.Text);
                GFILIAL.Set("NUMERO", tbNumero.Text);
                GFILIAL.Set("COMPLEMENTO", tbComplemento.Text);

                if (!string.IsNullOrEmpty(lpTipoBairro.ValorCodigoInterno))
                {
                    GFILIAL.Set("CODTIPOBAIRRO", lpTipoBairro.ValorCodigoInterno);
                }
                else
                {
                    GFILIAL.Set("CODTIPOBAIRRO", null);
                }

                GFILIAL.Set("BAIRRO", tbBairro.Text);
                GFILIAL.Set("CEP", tbCep.Text);

                if (!string.IsNullOrEmpty(lpPais.ValorCodigoInterno))
                {
                    GFILIAL.Set("CODPAIS", lpPais.ValorCodigoInterno);
                }
                else
                {
                    GFILIAL.Set("CODPAIS", null);
                }

                if (!string.IsNullOrEmpty(lpEstado.ValorCodigoInterno))
                {
                    GFILIAL.Set("CODETD", lpEstado.ValorCodigoInterno);
                }
                else
                {
                    GFILIAL.Set("CODETD", null);
                }

                if (!string.IsNullOrEmpty(lpCidade.ValorCodigoInterno))
                {
                    GFILIAL.Set("CODCIDADE", lpCidade.ValorCodigoInterno);
                }
                else
                {
                    GFILIAL.Set("CODCIDADE", null);
                }

                GFILIAL.Set("SEQSEGUNDONUMERO", chkSeqSegundoNumero.Checked == true ? 1 : 0);
                GFILIAL.Set("QTDSEGUNDONUMERO", tbQtdSegundoNumero.Text);
                GFILIAL.Set("SEGUNDONUMERO", tbSegundoNumero.Text);

                GFILIAL.Set("VERSAO", cmbVersao.SelectedValue);
                GFILIAL.Set("TPAMB", cmbTpAmb.SelectedValue);
                GFILIAL.Set("MODALIDADE", cmbModalidade.SelectedValue);
                GFILIAL.Set("PASTADESTINO", tbPastaDestino.Text);
                GFILIAL.Set("TEXTOEMAILNFE", MemoTextoEmailNfe.Text);

                GFILIAL.Save();
                conn.Commit();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            if (this.lookup == null)
            {
                Salvar();
            }
            else
            {
                if (Salvar() == true)
                {
                    carregaCampos();
                    this.Dispose();
                }
            }
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                this.Dispose();
            }
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}

