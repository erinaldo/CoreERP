using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PS.Glb.Class;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroUnidadeCliente : Form
    {
        string IDUNIDADE = String.Empty;
        string CODCLIFOR = String.Empty;
        string sql = String.Empty;
        bool Editar = false;

        public frmCadastroUnidadeCliente(String _IDUNIDADE, String _CODCLIFOR)
        {
            InitializeComponent();
            IDUNIDADE = _IDUNIDADE;
            CODCLIFOR = _CODCLIFOR;
        }

        private void frmCadastroUnidadeCliente_Load(object sender, EventArgs e)
        {
            CarregaCampos();
        }

        private void clClienteFaturamento_AposSelecao(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(clClienteFaturamento.Get()))
            {
                CarregaEnderecoCliente(clClienteFaturamento.Get());
            }
        }

        private void clEstado_AposSelecao(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(clEstado.textBoxCODIGO.Text))
            {
                clCidade.ColunaTabela = String.Format("(SELECT CODCIDADE, NOME FROM GCIDADE WHERE CODETD = '{0}') I", clEstado.textBoxCODIGO.Text);
                clCidade.Enabled = true;
            }
            else
            {
                clCidade.Enabled = false;
            }
        }

        private void btnSearchCep_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCep.Text))
            {
                PS.Glb.Class.WebCep web = new Class.WebCep(txtCep.Text);
                txtRua.Text = web.Lagradouro;
                txtBairro.Text = web.Bairro;
                clEstado.textBoxCODIGO.Text = web.UF;
                clEstado.textBoxDESCRICAO.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT NOME FROM GESTADO WHERE CODETD = ?", new object[] { web.UF }).ToString();
                clCidade.textBoxDESCRICAO.Text = web.Cidade;
                clCidade.textBoxCODIGO.Text = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT CODCIDADE FROM GCIDADE WHERE NOME = ?", new object[] { web.Cidade }).ToString();
            }
            else
            {
                MessageBox.Show("Favor preecher o CEP corretamente", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            if (Salvar())
            {
                this.Dispose();
            }
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Reembolso/Projeto

        private void btnNovo_Click(object sender, EventArgs e)
        {
            frmCadastroPedagio frm = new frmCadastroPedagio(IDUNIDADE, null, false, clFilial.textBoxCODIGO.Text);
            frm.ShowDialog();
            AtualizarGrid();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gridView1.SelectedRowsCount; i++)
                {
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));

                    frmCadastroPedagio frm = new frmCadastroPedagio(IDUNIDADE, row1["IDREEMBOLSO"].ToString(), true, row1["CODFILIAL"].ToString());
                    frm.ShowDialog();
                    AtualizarGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.SelectedRowsCount; i++)
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(i).ToString()));
                if (MessageBox.Show("Você deseja mesmo apagar este registro?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MetodosSQL.ExecQuery(String.Format(@"delete from AUNIDADEREEMBOLSO where IDREEMBOLSO = '{0}'", row1["IDREEMBOLSO"].ToString()));
                    AtualizarGrid();
                }
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            btnEditar_Click(sender, e);
        }

        #endregion

        #region Métodos

        private void AtualizarGrid()
        {
            sql = String.Format(@"select * from AUNIDADEREEMBOLSO where IDUNIDADE = '{0}'", IDUNIDADE);
            gridControl1.DataSource = MetodosSQL.GetDT(sql);

            FormataGridview();
        }

        private void FormataGridview()
        {
            gridView1.Columns["IDREEMBOLSO"].Caption = "ID Reembolso";
            gridView1.Columns["IDUNIDADE"].Caption = "ID Unidade";
            gridView1.Columns["CODUSUARIO"].Caption = "Código do Usuário";
            gridView1.Columns["DISTANCIAKM"].Caption = "Distância KM";
            gridView1.Columns["VALORKM"].Caption = "Valor KM";
            gridView1.Columns["VALORREFEICAO"].Caption = "Valor Refeição";
            gridView1.Columns["VALORPEDAGIO"].Caption = "Valor Pedágio";
            gridView1.Columns["CODPRODUTO"].Caption = "Código do Produto";
            gridView1.Columns["VALORHORAA"].Caption = "Valor Hora";

            gridView1.Columns["CODEMPRESA"].Visible = false;
            gridView1.Columns["CODFILIAL"].Visible = false;
            gridView1.Columns["COORDCLIENTE"].Visible = false;

            gridView1.BestFitColumns();
        }

        private void CarregaCampos()
        {
            try
            {
                clClienteFaturamento.ColunaTabela = String.Format("(select * from VCLIFOR where CODEMPRESA = '{0}') I", AppLib.Context.Empresa);
                clFilial.ColunaTabela = String.Format("(select * from GFILIAL where CODEMPRESA = '{0}') I", AppLib.Context.Empresa);

                tbDistanciaKM.EditValue = 0;
                tbValorKM.EditValue = 0.00;
                tbValorRefeicao.EditValue = 0.00;
                tbValorPedagio.EditValue = 0.00;
                tbValorHora.EditValue = 0.00;

                if (String.IsNullOrWhiteSpace(IDUNIDADE) && String.IsNullOrWhiteSpace(CODCLIFOR))
                {
                    toolStrip1.Enabled = false;
                    sql = String.Format(@"select NOME from GFILIAL where CODEMPRESA = '{0}' and CODFILIAL = '{1}'", AppLib.Context.Empresa, AppLib.Context.Filial);
                    clFilial.textBoxCODIGO.Text = AppLib.Context.Filial.ToString();
                    clFilial.textBoxDESCRICAO.Text = MetodosSQL.GetField(sql, "NOME");
                }
                else
                {
                    clCidade.Enabled = true;
                    txtIDUNIDADE.Set(int.Parse(IDUNIDADE));
                    sql = String.Format(@"select NOMEFANTASIA from VCLIFOR where CODCLIFOR = '{0}' and CODEMPRESA = '{1}'", CODCLIFOR, AppLib.Context.Empresa, AppLib.Context.Filial);
                    clClienteFaturamento.textBoxCODIGO.Text = CODCLIFOR;
                    clClienteFaturamento.textBoxDESCRICAO.Text = MetodosSQL.GetField(sql, "NOMEFANTASIA");
                    sql = String.Format(@"select * from AUNIDADE where IDUNIDADE = '{0}' and CODEMPRESA = '{1}'", IDUNIDADE, AppLib.Context.Empresa);
                    txtNomeUnidade.Text = MetodosSQL.GetField(sql, "NOME");
                    txtRua.Text = MetodosSQL.GetField(sql, "RUA");
                    txtNumero.Text = MetodosSQL.GetField(sql, "NUMERO");
                    txtComplemento.Text = MetodosSQL.GetField(sql, "COMPLEMENTO");
                    txtBairro.Text = MetodosSQL.GetField(sql, "BAIRRO");
                    clCidade.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "CIDADE");
                    txtCep.Text = MetodosSQL.GetField(sql, "CEP");
                    txtObservacao.Text = MetodosSQL.GetField(sql, "OBSERVACAO");
                    tbDistanciaKM.EditValue = int.Parse(MetodosSQL.GetField(sql, "DISTANCIAKM"));
                    tbValorKM.EditValue = Convert.ToDecimal(MetodosSQL.GetField(sql, "VALORKM"));
                    tbValorRefeicao.EditValue = Convert.ToDecimal(MetodosSQL.GetField(sql, "VALORREFEICAO"));
                    tbValorPedagio.EditValue = Convert.ToDecimal(MetodosSQL.GetField(sql, "VALORPEDAGIO"));
                    tbValorHora.EditValue = Convert.ToDecimal(MetodosSQL.GetField(sql, "VALORHORAD"));

                    clReembolso.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "CODPRODUTO");

                    clFilial.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "CODFILIAL");

                    clEstado.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "CODETD");
                    sql = String.Format(@"select NOME from GESTADO where CODETD = '{0}'", clEstado.textBoxCODIGO.Text);
                    clEstado.textBoxDESCRICAO.Text = MetodosSQL.GetField(sql, "NOME");

                    sql = String.Format(@"select NOMEFANTASIA from GFILIAL where CODFILIAL = '{0}'", clFilial.textBoxCODIGO.Text);
                    clFilial.textBoxDESCRICAO.Text = MetodosSQL.GetField(sql, "NOMEFANTASIA");

                    sql = String.Format(@"select NOME from VPRODUTO where CODPRODUTO = '{0}' and CODEMPRESA = '{1}'", clReembolso.textBoxCODIGO.Text, AppLib.Context.Empresa);
                    clReembolso.textBoxDESCRICAO.Text = MetodosSQL.GetField(sql, "NOME");

                    sql = String.Format(@"SELECT NOME FROM GCIDADE WHERE CODCIDADE = '{0}'", clCidade.textBoxCODIGO.Text);
                    clCidade.textBoxDESCRICAO.Text = MetodosSQL.GetField(sql, "NOME");

                    AtualizarGrid();

                    Editar = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CarregaEnderecoCliente(string codCliente)
        {
            DataTable dtEnderecoCliente = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CLI.CEP, CLI.RUA, CLI.NUMERO, CLI.COMPLEMENTO, CLI.BAIRRO, CID.CODCIDADE,  CID.NOME, CID.CODETD
                                                                                                    FROM VCLIFOR CLI
                                                                                                    INNER JOIN GCIDADE CID
                                                                                                    ON CID.CODCIDADE = CLI.CODCIDADE
                                                                                                    WHERE CODEMPRESA = ? AND CODCLIFOR = ?", new object[] { AppLib.Context.Empresa, codCliente });

            txtCep.Text = dtEnderecoCliente.Rows[0]["CEP"].ToString();
            txtRua.Text = dtEnderecoCliente.Rows[0]["RUA"].ToString();
            txtNumero.Text = dtEnderecoCliente.Rows[0]["NUMERO"].ToString();
            txtComplemento.Text = dtEnderecoCliente.Rows[0]["COMPLEMENTO"].ToString();
            txtBairro.Text = dtEnderecoCliente.Rows[0]["BAIRRO"].ToString();
            clEstado.Set(dtEnderecoCliente.Rows[0]["CODETD"].ToString());
            clCidade.Set(dtEnderecoCliente.Rows[0]["CODCIDADE"].ToString());
            clCidade.textBoxDESCRICAO.Text = dtEnderecoCliente.Rows[0]["NOME"].ToString();
        }

        private bool ValidaSalvar()
        {
            if (clReembolso.Get() == null)
            {
                MessageBox.Show("Favor preencher o produto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool Salvar()
        {
            if (ValidaSalvar())
            {
                try
                {
                    if (Editar)
                    {
                        sql = String.Format(@"update AUNIDADE
                                            set CODEMPRESA = '{0}',
	                                            CODFILIAL = '{1}',
	                                            CODCLIFOR = '{2}',
	                                            NOME = '{3}',
	                                            CODETD = '{4}',
                                            	CIDADE = '{5}',
	                                            OBSERVACAO = '{6}',
                                            	RUA = '{7}',
	                                            NUMERO = '{8}',
                                            	BAIRRO = '{9}',
	                                            COMPLEMENTO = '{10}',
	                                            CEP = '{11}',
                                                DISTANCIAKM = '{12}',
                                                VALORKM = '{13}',
                                                VALORREFEICAO = '{14}',
                                                VALORPEDAGIO = '{15}',
                                                VALORHORAD = '{16}',
                                                CODPRODUTO = '{17}'
                                            where IDUNIDADE = '{18}'",
                                                /*{0}*/ AppLib.Context.Empresa,
                                                /*{1}*/ clFilial.textBoxCODIGO.Text,
                                                /*{2}*/ clClienteFaturamento.textBoxCODIGO.Text,
                                                /*{3}*/ txtNomeUnidade.Text,
                                                /*{4}*/ clEstado.textBoxCODIGO.Text,
                                                /*{5}*/ clCidade.textBoxCODIGO.Text,
                                                /*{6}*/ txtObservacao.Text,
                                                /*{7}*/ txtRua.Text,
                                                /*{8}*/ txtNumero.Text,
                                                /*{9}*/ txtBairro.Text,
                                                /*{10}*/ txtComplemento.Text,
                                                /*{11}*/ txtCep.Text,
                                                /*{12}*/ tbDistanciaKM.EditValue,
                                                /*{13}*/ tbValorKM.EditValue.ToString().Replace(",", "."),
                                                /*{14}*/ tbValorRefeicao.EditValue.ToString().Replace(",", "."),
                                                /*{15}*/ tbValorPedagio.EditValue.ToString().Replace(",", "."),
                                                /*{16}*/ tbValorHora.EditValue.ToString().Replace(",", "."),
                                                /*{17}*/ clReembolso.textBoxCODIGO.Text,
                                                /*{18}*/ IDUNIDADE);

                        MetodosSQL.ExecQuery(sql);
                    }
                    else
                    {
                        sql = String.Format(@"insert into AUNIDADE values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}') select SCOPE_IDENTITY()",
                                                /*{0}*/ AppLib.Context.Empresa,
                                                /*{1}*/ clFilial.textBoxCODIGO.Text,
                                                /*{2}*/ clClienteFaturamento.textBoxCODIGO.Text,
                                                /*{3}*/ txtNomeUnidade.Text,
                                                /*{4}*/ clEstado.textBoxCODIGO.Text,
                                                /*{5}*/ clCidade.textBoxCODIGO.Text,
                                                /*{6}*/ txtObservacao.Text,
                                                /*{7}*/ txtRua.Text,
                                                /*{8}*/ txtNumero.Text,
                                                /*{9}*/ txtBairro.Text,
                                                /*{10}*/ txtComplemento.Text,
                                                /*{11}*/ txtCep.Text,
                                                /*{12}*/ tbDistanciaKM.EditValue,
                                                /*{13}*/ tbValorKM.EditValue.ToString().Replace(",", "."),
                                                /*{14}*/ tbValorRefeicao.EditValue.ToString().Replace(",", "."),
                                                /*{15}*/ tbValorPedagio.EditValue.ToString().Replace(",", "."),
                                                /*{16}*/ clReembolso.textBoxCODIGO.Text,
                                                /*{17}*/ tbValorHora.EditValue.ToString().Replace(",", "."));

                        IDUNIDADE = txtIDUNIDADE.textEdit1.Text = MetodosSQL.ExecScalar(sql).ToString();
                        toolStrip1.Enabled = true;
                        Editar = true;
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }
            else 
            {
                return false;
            }
        }

        #endregion
    }
}
