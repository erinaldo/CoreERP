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
    public partial class frmCadastroProjetoTarefa : Form
    {
        string sql = String.Empty;
        string sqlParalelo = String.Empty;

        public String IDPROJETO { get; set; }
        public String IDTAREFA { get; set; }
        public int IDUNIDADE { get; set; }
        public String CODEMPRESA { get; set; }
        public String CODFILIAL { get; set; }

        public frmCadastroProjetoTarefa()
        {
            InitializeComponent();
        }

        private void frmCadastroProjetoTarefa_Load(object sender, EventArgs e)
        {
            CarregaComboBoxTipoFaturamento();
            CarregaComboBoxInLoco();

            SetConsultaLookUp();

            AtualizaVisao();
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            Salvar();
            this.Dispose();
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Métodos

        private void SetConsultaLookUp()
        {
            campoLookupIDCONTATOCC.ColunaTabela = String.Format(@"(SELECT AUR.CODUSUARIO, GU.NOME FROM AUNIDADEREEMBOLSO AUR

                                                                  inner join GUSUARIO GU
                                                                  on GU.CODUSUARIO = AUR.CODUSUARIO

                                                                  where AUR.IDUNIDADE = '{0}') Y", IDUNIDADE);
        }

        private void AtualizaVisao()
        {
            try
            {

                if (String.IsNullOrWhiteSpace(IDTAREFA))
                {
                    tbIdProejto.EditValue = int.Parse(IDPROJETO);
                }
                else
                {
                    sql = String.Format(@"select CODEMPRESA, 
                                                 IDTAREFA, 
                                                 NOMETAREFA, 
                                                 PREVISAOENTREGA, 
                                                 PRIORIDADE, 
                                                 INLOCO, 
                                                 DATACONCLUSAO, 
                                                 TIPOFATURAMENTO,
                                                 PREVISAOHORAS,
                                                 DESCRICAO
                                          from APROJETOTAREFA where IDPROJETO = '{0}' and CODEMPRESA = '{1}' and IDTAREFA = '{2}'", IDPROJETO, CODEMPRESA, IDTAREFA);

                    tbIdProejto.EditValue = int.Parse(IDPROJETO);
                    tbIdTarefaProjeto.EditValue = int.Parse(IDTAREFA);

                    tbTarefa.Text = MetodosSQL.GetField(sql, "NOMETAREFA");
                    cbInLoco.SelectedValue = MetodosSQL.GetField(sql, "INLOCO");
                    cbTipoFaturamento.SelectedValue = MetodosSQL.GetField(sql, "TIPOFATURAMENTO");
                    campoInteiroPREVISAOHORAS.Set(int.Parse(MetodosSQL.GetField(sql, "PREVISAOHORAS")));

                    if (!String.IsNullOrWhiteSpace(MetodosSQL.GetField(sql, "PREVISAOENTREGA")))

                        dtePrevisaoEntrega.DateTime = Convert.ToDateTime(MetodosSQL.GetField(sql, "PREVISAOENTREGA"));

                    if (!String.IsNullOrWhiteSpace(MetodosSQL.GetField(sql, "DATACONCLUSAO")))

                        dteDataConclusao.DateTime = Convert.ToDateTime(MetodosSQL.GetField(sql, "DATACONCLUSAO"));

                    tbDescricao.Text = MetodosSQL.GetField(sql, "DESCRICAO");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateDatas()
        {
            //Verifica e atualiza o campo DATAENTREGA
            if (!string.IsNullOrWhiteSpace(dtePrevisaoEntrega.Text))
            {
                sql = String.Format(@"update APROJETOTAREFA  
                                          set PREVISAOENTREGA = Convert(datetime, '{0}', 103) 
                                          where IDTAREFA = '{1}'
                                          and IDPROJETO = '{2}'
                                          and CODEMPRESA = '{3}'
                                          and CODFILIAL = '{4}'",
                                  /*{0}*/ dtePrevisaoEntrega.EditValue,
                                  /*{1}*/ IDTAREFA,
                                  /*{2}*/ IDPROJETO,
                                  /*{3}*/ CODEMPRESA,
                                  /*{4}*/ CODFILIAL);
                MetodosSQL.ExecQuery(sql);
            }
            else
            {
                sql = String.Format(@"update APROJETOTAREFA  
                                          set PREVISAOENTREGA = NULL 
                                          where IDTAREFA = '{0}'
                                          and IDPROJETO = '{1}'
                                          and CODEMPRESA = '{2}'
                                          and CODFILIAL = '{3}'",
                                 /*{0}*/ IDTAREFA,
                                 /*{1}*/ IDPROJETO,
                                 /*{2}*/ CODEMPRESA,
                                 /*{3}*/ CODFILIAL);
                MetodosSQL.ExecQuery(sql);
            }

            //Verifica e atualiza campo DATACONCLUSAO
            if (!String.IsNullOrWhiteSpace(dteDataConclusao.Text))
            {
                sql = String.Format(@"update APROJETOTAREFA  
                                          set DATACONCLUSAO = Convert(datetime, '{0}', 103)
                                          where IDTAREFA = '{1}'
                                          and IDPROJETO = '{2}'
                                          and CODEMPRESA = '{3}'
                                          and CODFILIAL = '{4}'",
                                  /*{0}*/ dteDataConclusao.EditValue,
                                  /*{1}*/ IDTAREFA,
                                  /*{2}*/ IDPROJETO,
                                  /*{3}*/ CODEMPRESA,
                                  /*{4}*/ CODFILIAL);
                MetodosSQL.ExecQuery(sql);
            }
        }

        private Boolean ValidarSalvar()
        {
            try
            {
                if (string.IsNullOrEmpty(cbTipoFaturamento.SelectedValue.ToString()))
                {
                    throw new Exception("Preencher campo Tipo Faturamento.");
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private void Salvar()
        {
            try
            {
                if (ValidarSalvar())
                {
                    if (String.IsNullOrWhiteSpace(IDTAREFA))
                    {
                        sql = String.Format(@"insert into APROJETOTAREFA values('{0}', '{1}', '{2}', '{3}', '{4}', null, '1', '{5}', null, '{6}', '{7}', '{8}') select SCOPE_IDENTITY()",
                                              /*{0}*/ CODEMPRESA,
                                              /*{1}*/ CODFILIAL,
                                              /*{2}*/ IDPROJETO,
                                              /*{3}*/ tbTarefa.Text,
                                              /*{4}*/ campoLookupIDCONTATOCC.Get(),
                                              /*{5}*/ cbInLoco.SelectedValue,
                                              /*{6}*/ cbTipoFaturamento.SelectedValue.ToString(),
                                              /*{7}*/ campoInteiroPREVISAOHORAS.Get(),
                                              /*{8}*/ tbDescricao.Text);

                        IDTAREFA = MetodosSQL.ExecScalar(sql).ToString();
                        UpdateDatas();
                        AtualizaVisao();
                    }
                    else
                    {
                        sql = String.Format(@"update APROJETOTAREFA  
                                          set NOMETAREFA = '{0}', 
                                              INLOCO = '{1}',  
                                              TIPOFATURAMENTO = '{2}',
                                              PREVISAOHORAS = '{3}',
                                              DESCRICAO = '{4}'
                                          where IDTAREFA = '{5}'
                                          and IDPROJETO = '{6}'
                                          and CODEMPRESA = '{7}'
                                          and CODFILIAL = '{8}'",
                                              /*{0}*/ tbTarefa.Text,
                                              /*{1}*/ cbInLoco.SelectedValue,
                                              /*{2}*/ cbTipoFaturamento.SelectedValue.ToString(),
                                              /*{3}*/ campoInteiroPREVISAOHORAS.Get(),
                                              /*{4}*/ tbDescricao.Text,
                                              /*{5}*/ IDTAREFA,
                                              /*{6}*/ IDPROJETO,
                                              /*{7}*/ CODEMPRESA,
                                              /*{8}*/ CODFILIAL);

                        MetodosSQL.ExecQuery(sql);

                        UpdateDatas();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CarregaComboBoxTipoFaturamento()
        {
            DataTable dtTipoFaturamento = new DataTable();

            dtTipoFaturamento.Columns.Add("Codigo", typeof(string));
            dtTipoFaturamento.Columns.Add("Nome", typeof(string));

            dtTipoFaturamento.Rows.Add("-", "- Selecione");
            dtTipoFaturamento.Rows.Add("P", "Projeto Fechado");
            dtTipoFaturamento.Rows.Add("D", "Demanda Conforme OS");

            cbTipoFaturamento.DataSource = dtTipoFaturamento;
            cbTipoFaturamento.ValueMember = "Codigo";
            cbTipoFaturamento.DisplayMember = "Nome";
        }

        private void CarregaComboBoxInLoco()
        {
            DataTable dtInLoco = new DataTable();

            dtInLoco.Columns.Add("Codigo", typeof(string));
            dtInLoco.Columns.Add("Nome", typeof(string));

            dtInLoco.Rows.Add("-", "- Selecione");
            dtInLoco.Rows.Add("0", "Não");
            dtInLoco.Rows.Add("1", "Sim");

            cbInLoco.DataSource = dtInLoco;
            cbInLoco.ValueMember = "Codigo";
            cbInLoco.DisplayMember = "Nome";
        }

        #endregion
    }
}
