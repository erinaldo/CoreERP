using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PS.Glb.Class;

namespace PS.Glb.New.Cadastros
{
    public partial class frmCadastroApontamento : Form
    {
        #region Variáveis

        string sql = "";
        string sqlParalelo = "";
        string tabela = "AAPONTAMENTOTAREFA";

        public string StatusApontamento = "";
        public string idApontamento = "";

        DateTime? totalHoras = null;
        DateTime? horasApontadas = null;
        DateTime? diferencaHoras = null;
        DateTime? data = null;

        TimeSpan diferencaTempoApontamento;
        TimeSpan convertRecebernegativo;


        #endregion

        public frmCadastroApontamento()
        {
            InitializeComponent();
        }

        private void frmCadastroApontamento_Load(object sender, EventArgs e)
        {
            CarregaComboBoxInLoco();
            CarregaComboBoxGeraReembolso();
            CarregaComboBoxMotivo();
            CarregaComboBoxTipoFaturamento();
            AtualizaVisao();
        }

        private void btnSalvarAtual_Click(object sender, EventArgs e)
        {
            ClickSalvar(false);
        }

        private void btnOKAtual_Click(object sender, EventArgs e)
        {
            ClickSalvar(true);
        }

        private void btnCancelarAtual_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #region Tarefas 

        private void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                CalculaData();

                PS.Glb.New.Cadastros.frmCadastroApontamentoTarefa frm = new Cadastros.frmCadastroApontamentoTarefa();
                frm.IDTAREFA = null;
                frm.CODEMPRESA = AppLib.Context.Empresa.ToString();
                frm.IDAPONTAMENTO = idApontamento;
                frm.IDPROJETO = campoLookupIDPROJETO.textBoxCODIGO.Text;
                frm.HORA = Convert.ToDateTime(data + (TimeSpan)teDiferencas.EditValue);
                frm.DATA = Convert.ToDateTime(data);
                frm.ShowDialog();
                AtualizaVisao();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                if (MessageBox.Show("Deseja excluir o registro selecionado?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sql = String.Format(@"delete from AAPONTAMENTOTAREFA where IDAPONTAMENTOTAREFA = '{0}'", row1["IDAPONTAMENTOTAREFA"].ToString());
                    MetodosSQL.ExecQuery(sql);
                    AtualizaVisao();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                //CalculaData();

                DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                PS.Glb.New.Cadastros.frmCadastroApontamentoTarefa frm = new Cadastros.frmCadastroApontamentoTarefa();

                frm.IDTAREFA = row1["IDTAREFA"].ToString();
                frm.CODEMPRESA = row1["CODEMPRESA"].ToString();
                frm.IDAPONTAMENTOTAREFA = row1["IDAPONTAMENTOTAREFA"].ToString();
                frm.IDAPONTAMENTO = idApontamento;
                frm.IDPROJETO = campoLookupIDPROJETO.textBoxCODIGO.Text;
                frm.HORA = Convert.ToDateTime(data + (TimeSpan)teDiferencas.EditValue);
                frm.ShowDialog();
                AtualizaVisao();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAtualizar_Click_1(object sender, EventArgs e)
        {
            AtualizaVisao();
        }

        private void btnSelecionarColunas_Click(object sender, EventArgs e)
        {
            frmSelecaoColunas frm = new frmSelecaoColunas(tabela);
            frm.ShowDialog();
            AtualizaVisao();
        }

        private void btnSalvarLayout_Click(object sender, EventArgs e)
        {
            SalvarLayout();
        }

        #endregion

        #region Eventos

        private void tbValorAdicional_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(tbValorAdicional.EditValue) > 0)
            {
                cbMotivo.Enabled = true;

                cbMotivo.Select();
            }
            else
            {
                cbMotivo.Enabled = false;
                cbMotivo.SelectedValue = "-";
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Tarefas")
            {
                if (ValidarSalvar())
                {
                    Salvar(false);
                }
            }
        }

        #region Eventos - Apos Selecao

        private void campoLookupUSUARIOCONSULTOR_AposSelecao_1(object sender, EventArgs e)
        {
            SetConsultaLookUp();
        }

        private void campoLookupIDUNIDADE_AposSelecao_1(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                XtraMessageBox.Show("Não é possível trocar de Unidade quando já existem tarefas associadas com o Apontamento.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            campoLookupIDPROJETO.Clear();

            SetConsultaLookUp();
            TrazProjetoPreenchido();
        }

        private void campoLookupIDPROJETO_AposSelecao(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                XtraMessageBox.Show("Não é possível trocar de Projeto quando já existem tarefas associadas com o Apontamento.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        #endregion

        #region Eventos - SelectedIndexChanged

        private void cbInLoco_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbInLoco.SelectedValue.ToString() == "1")
            {
                cbGeraReembolso.Enabled = true;
                cbGeraReembolso.SelectedValue = "-";
                Clipboard.SetText(cbGeraReembolso.SelectedValue.ToString());

                tbValorAdicional.Enabled = false;
                cbMotivo.Enabled = false;
                tbValorAdicional.EditValue = 0.00;
            }
            else
            {
                cbGeraReembolso.SelectedValue = "0";
                cbGeraReembolso.Enabled = false;
            }
        }

        private void cbGeraReembolso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTipoFaturamento.SelectedIndex > 0)
            {
                if (cbGeraReembolso.SelectedValue.ToString() == "0")
                {
                    tbValorAdicional.Enabled = false;
                    cbMotivo.Enabled = false;
                    tbValorAdicional.EditValue = 0.00;
                }
                else
                {
                    tbValorAdicional.Enabled = true;
                }
            }
        }

        #endregion

        #endregion

        #region Métodos
        private void DisableControls(Control con)
        {
            foreach (Control c in con.Controls)
            {
                DisableControls(c);
            }
            con.Enabled = false;
        }

        private void EnableControls(Control con)
        {
            if (con != null)
            {
                con.Enabled = true;
                EnableControls(con.Parent);
            }
        }

        private void ConfigurarHorasIniciais()
        {
            if (string.IsNullOrWhiteSpace(idApontamento))
            {
                TimeSpan timeSpan = TimeSpan.FromHours(8);
                teInicio.EditValue = Convert.ToDateTime(timeSpan.ToString());

                timeSpan = TimeSpan.FromHours(17);
                teTermino.EditValue = Convert.ToDateTime(timeSpan.ToString());

                timeSpan = TimeSpan.FromHours(1);
                teAbono.EditValue = Convert.ToDateTime(timeSpan.ToString());
            }
        }

        private void SetConsultaLookUp()
        {          
            campoLookupUSUARIOCONSULTOR.ColunaTabela = String.Format(@"(SELECT 
	                                                                            CODUSUARIO, 
	                                                                            NOME 
                                                                            FROM 
	                                                                            GUSUARIO 
                                                                            WHERE 
	                                                                            ATIVO = 1 
                                                                            AND CODUSUARIO NOT IN (
						                                                                            SELECT 
							                                                                            CODUSUARIO
						                                                                            FROM 
							                                                                            AUNIDADEREEMBOLSO 
						                                                                            WHERE 
							                                                                            CODEMPRESA = "+ AppLib.Context.Empresa +" AND COORDCLIENTE = 1 AND AUNIDADEREEMBOLSO.IDUNIDADE NOT IN (49)) AND CODUSUARIO NOT IN ('MASTER')) Y");

            campoLookupIDUNIDADE.ColunaTabela = String.Format(@"(SELECT DISTINCT AU.IDUNIDADE, AU.NOME FROM AUNIDADEREEMBOLSO AUR

                                                                    inner join AUNIDADE AU
                                                                    on AU.IDUNIDADE = AUR.IDUNIDADE
                                                                    and AU.CODEMPRESA = AUR.CODEMPRESA
                                                                    and AU.CODFILIAL = AUR.CODFILIAL

                                                                    WHERE CODUSUARIO = '{0}' 
                                                                    AND AUR.CODEMPRESA = '{1}'
                                                                    AND AUR.CODFILIAL = '{2}') Y", campoLookupUSUARIOCONSULTOR.textBoxCODIGO.Text, AppLib.Context.Empresa, AppLib.Context.Filial);

            campoLookupIDPROJETO.ColunaTabela = String.Format(@"(SELECT AP.* FROM AUNIDADEPROJETO AUP

                                                                    inner join APROJETO AP
                                                                    on AP.IDPROJETO = AUP.IDPROJETO

                                                                    WHERE CODUSUARIO = '{0}' 
                                                                    AND AUP.IDUNIDADE = '{1}' 
                                                                    AND AUP.CODEMPRESA = '{2}'
                                                                    AND AUP.CODFILIAL = '{3}') Y", campoLookupUSUARIOCONSULTOR.textBoxCODIGO.Text, campoLookupIDUNIDADE.textBoxCODIGO.Text, AppLib.Context.Empresa, AppLib.Context.Filial);
        }

        private void TrazProjetoPreenchido()
        {
            if (!String.IsNullOrWhiteSpace(campoLookupIDUNIDADE.textBoxCODIGO.Text))
            {
                sql = String.Format(@"SELECT AP.* FROM AUNIDADEPROJETO AUP

                                                                    INNER JOIN APROJETO AP
                                                                    ON AP.IDPROJETO = AUP.IDPROJETO

                                                                    WHERE CODUSUARIO = '{0}' 
                                                                    AND AUP.IDUNIDADE = '{1}' 
                                                                    AND AUP.CODEMPRESA = '{2}'
                                                                    AND AUP.CODFILIAL = '{3}'",
                                      /*{0}*/ campoLookupUSUARIOCONSULTOR.textBoxCODIGO.Text,
                                      /*{1}*/ campoLookupIDUNIDADE.textBoxCODIGO.Text,
                                      /*{2}*/ AppLib.Context.Empresa,
                                      /*{3}*/ AppLib.Context.Filial);

                DataTable dt = MetodosSQL.GetDT(sql);

                if (dt.Rows.Count == 1)
                {
                    campoLookupIDPROJETO.textBoxCODIGO.Text = dt.Rows[0]["IDPROJETO"].ToString();
                    campoLookupIDPROJETO.textBoxDESCRICAO.Text = (String)dt.Rows[0]["DESCRICAO"];
                }
            }
        }

        private bool ValidarSalvar()
        {
            if (deDataApontamento.EditValue == null || string.IsNullOrWhiteSpace(deDataApontamento.EditValue.ToString()))
            {
                XtraMessageBox.Show("Favor preencher o campo Data", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (Convert.ToDateTime(teInicio.EditValue) > Convert.ToDateTime(teTermino.EditValue))
            {
                XtraMessageBox.Show("Campo Início não pode ser maior que o Termino", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (cbInLoco.SelectedIndex == 0 || string.IsNullOrEmpty(cbInLoco.SelectedValue.ToString()))
            {
                XtraMessageBox.Show("Favor preencher o campo In Loco", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if ((string.IsNullOrEmpty(cbGeraReembolso.SelectedValue.ToString())) && cbInLoco.SelectedValue.ToString() == "1")
            {
                XtraMessageBox.Show("Favor preencher o campo Reembolso", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (Convert.ToDecimal(tbValorAdicional.EditValue) > 0)
            {
                if (cbMotivo.SelectedIndex <= 0)
                {
                    XtraMessageBox.Show("Favor preencher o campo Motivo corretamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(campoLookupIDUNIDADE.textBoxCODIGO.Text))
            {
                XtraMessageBox.Show("Favor preencher o campo Unidade", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (string.IsNullOrWhiteSpace(campoLookupIDPROJETO.textBoxCODIGO.Text))
            {
                XtraMessageBox.Show("Favor preencher o campo Projeto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            CalculaData();

            return true;
        }

        private void CalculaData()
        {
            DateTime tempoTotalApontamento;
            DateTime tempoApontado;

            TimeSpan tsDdiferenca;

            try
            {
                // Horas totais do Apontamento
                teTotalHoras.EditValue = GetTotalHoras(idApontamento);

                // Horas apontadas do Apontamento
                teHorasApontadas.EditValue = GetTempoApontado(idApontamento);

                if (teTotalHoras.EditValue != null)
                {
                    if (!String.IsNullOrWhiteSpace(teTotalHoras.EditValue.ToString()))
                    {
                        tempoTotalApontamento = Convert.ToDateTime(teTotalHoras.EditValue);
                        tempoApontado = Convert.ToDateTime(teHorasApontadas.EditValue);

                        int minutos = GetDiferencaMinutos(idApontamento);

                        if (tempoTotalApontamento < tempoApontado)
                        {
                            convertRecebernegativo = tempoApontado - tempoTotalApontamento;
                            tsDdiferenca = convertRecebernegativo;
                        }
                        else
                        {
                            tsDdiferenca = tempoTotalApontamento - tempoApontado;

                        }

                        //tsDdiferenca = tempoTotalApontamento - tempoApontado;

                        diferencaTempoApontamento = tsDdiferenca;

                        teDiferencas.EditValue = tsDdiferenca;                       

                        // Validação das horas do Apontamento
                        if (Convert.ToInt32(tsDdiferenca.Minutes) > 0 || Convert.ToInt32(tsDdiferenca.Hours) > 0)
                        {
                            btnNovo.Enabled = true;
                        }
                        else
                        {
                            btnNovo.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DateTime? GetTempoApontado(string idApontamento)
        {
            DateTime? tempoApontado = null;
            DateTime? apontado = null;

            int horasApontamento = 0;
            int minutosApontamento = 0;

            DataTable dtHorasApontadas = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT A.DATA, T.HORAS
                                                                                                FROM AAPONTAMENTO A
                                                                                                LEFT JOIN AAPONTAMENTOTAREFA T
                                                                                                ON T.CODEMPRESA = A.CODEMPRESA AND T.IDAPONTAMENTO = A.IDAPONTAMENTO
                                                                                                WHERE A.CODEMPRESA = ? AND A.IDAPONTAMENTO = ?", new object[] { AppLib.Context.Empresa, idApontamento });

            if (dtHorasApontadas.Rows.Count > 0)
            {
                for (int i = 0; i < dtHorasApontadas.Rows.Count; i++)
                {          
                    if (dtHorasApontadas.Rows[i]["HORAS"] != DBNull.Value)
                    {
                        apontado = Convert.ToDateTime(dtHorasApontadas.Rows[i]["HORAS"]);

                        if (apontado.Value.Minute > 0)
                        {
                            minutosApontamento += apontado.Value.Minute;
                        }

                        if (apontado.Value.Hour > 0)
                        {
                            horasApontamento += apontado.Value.Hour;
                        }
                    }          
                }

                DateTime dataApontamento = Convert.ToDateTime(dtHorasApontadas.Rows[0]["DATA"]);

                if (minutosApontamento > 0)
                {
                    tempoApontado = new DateTime(dataApontamento.Year, dataApontamento.Month, dataApontamento.Day, horasApontamento, minutosApontamento, 0);
                }
                else
                { 
                    tempoApontado = new DateTime(dataApontamento.Year, dataApontamento.Month, dataApontamento.Day, horasApontamento, 0, 0);
                }
            }

            return tempoApontado;
        }

        private DateTime? GetTotalHoras(string idApontamento)
        {
            DateTime? tempoTotalApontamento = null;
            TimeSpan tempoApontamento;

            int horasApontamento = 0;
            int minutosApontamento = 0;

            DataTable dtApontamento = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM AAPONTAMENTO WHERE CODEMPRESA = ? AND IDAPONTAMENTO = ?", new object[] { AppLib.Context.Empresa, idApontamento });

            if (dtApontamento.Rows.Count > 0)
            {
                DateTime dataApontamento = Convert.ToDateTime(dtApontamento.Rows[0]["DATA"]);

                data = dataApontamento;

                DateTime termino = Convert.ToDateTime(dtApontamento.Rows[0]["TERMINO"]);
                DateTime inicio = Convert.ToDateTime(dtApontamento.Rows[0]["INICIO"]);
                DateTime abono = Convert.ToDateTime(dtApontamento.Rows[0]["ABONO"]);

                // Verifica os minutos do apontamento

                if (inicio.Minute > termino.Minute)
                {
                    minutosApontamento = inicio.Minute - termino.Minute;
                }
                else
                {
                    minutosApontamento = termino.Minute - inicio.Minute;
                }
                
                if (minutosApontamento > 0)
                {
                    tempoApontamento = ((termino - inicio));
                    tempoApontamento = (DateTime.Parse(tempoApontamento.ToString()) - abono);
                    tempoTotalApontamento = new DateTime(dataApontamento.Year, dataApontamento.Month, dataApontamento.Day, tempoApontamento.Hours, tempoApontamento.Minutes, 0);
                }
                else
                {
                    // Realiza o cálculo para extrair as horas demandadas no apontamento
                    horasApontamento = ((termino.Hour - inicio.Hour) - abono.Hour);

                    tempoTotalApontamento = new DateTime(dataApontamento.Year, dataApontamento.Month, dataApontamento.Day, horasApontamento, minutosApontamento, 0);
                }        
            }

            return tempoTotalApontamento;
        }

        private bool ValidaDiferencaHoras(int horasTotais, int horasApontadas)
        {
            int diferencaHoras = 0;

            diferencaHoras = horasTotais - horasApontadas;

            if (diferencaHoras > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private int GetDiferencaMinutos(string idApontamento)
        {
            int diferencaMinutos = 0;
            DataTable dtApontamento = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM AAPONTAMENTO WHERE CODEMPRESA = ? AND IDAPONTAMENTO = ?", new object[] { AppLib.Context.Empresa, idApontamento });

            if (dtApontamento.Rows.Count > 0)
            {
                DateTime termino = Convert.ToDateTime(dtApontamento.Rows[0]["TERMINO"]);
                DateTime inicio = Convert.ToDateTime(dtApontamento.Rows[0]["INICIO"]);

                if (inicio.Minute > termino.Minute)
                {
                    diferencaMinutos = inicio.Minute - termino.Minute;
                }
                else
                {
                    diferencaMinutos = termino.Minute - inicio.Minute;
                }
            }

            return diferencaMinutos;
        }

        private void AtualizaVisao()
        {
            try
            {
                clFilial.ColunaTabela = String.Format("(select * from GFILIAL where CODEMPRESA = '{0}') I", AppLib.Context.Empresa);

                if (String.IsNullOrWhiteSpace(idApontamento))
                {
                    ConfigurarHorasIniciais();
                    SetConsultaLookUp();

                    clFilial.textBoxCODIGO.Text = Convert.ToString(AppLib.Context.Filial);

                    if (AppLib.Context.Usuario != "master")
                    {
                        campoLookupUSUARIOCONSULTOR.textBoxCODIGO.Text = AppLib.Context.Usuario;
                    }
    
                    campoLookupUSUARIOCONSULTOR.textBox1_Leave(null, null);

                    sql = String.Format(@"select NOME from GFILIAL where CODEMPRESA = '{0}' and CODFILIAL = '{1}'", AppLib.Context.Empresa, clFilial.textBoxCODIGO.Text);
                    clFilial.textBoxDESCRICAO.Text = MetodosSQL.GetField(sql, "NOME");

                    cbGeraReembolso.SelectedValue = "-";
                    cbInLoco.SelectedValue = "-";
                    cbTipoFaturamento.SelectedValue = "D";
                    cbMotivo.SelectedValue = "-";
                }
                else
                {
                    sql = String.Format(@"select AP.CODUSUARIO,
	                                             AP.TIPOFATURAMENTO,
	                                             AP.INLOCO,
	                                             AP.REEMBOLSO,
	                                             AP.VALORAD,
	                                             AP.MOTVALAD,
	                                             AU.CODCLIFOR,
	                                             AP.IDUNIDADE,
	                                             AP.IDPROJETO,
                                                 AP.DATA as 'DTA',
                                                 AP.CODFILIAL,
                                                 AP.IDSTATUSAPONTAMENTO,
                                                 convert(varchar(10), AP.INICIO, 108) as 'INICIO', 
	                                             convert(varchar(10), AP.TERMINO, 108) as 'TERMINO', 
	                                             convert(varchar(10), AP.ABONO, 108) as 'ABONO',
                                                 CONVERT(varchar(10), ap.TOTALHORAS, 108) as 'TOTALHORAS'
                                          from AAPONTAMENTO AP

                                          inner join AUNIDADE AU
                                          on AU.IDUNIDADE = AP.IDUNIDADE
                                          and AU.CODEMPRESA = AP.CODEMPRESA

                                          where AP.IDAPONTAMENTO = '{0}'
                                          and AP.CODEMPRESA = '{1}'", idApontamento, AppLib.Context.Empresa);

                    if (int.Parse(MetodosSQL.GetField(sql, "IDSTATUSAPONTAMENTO")) > 0)
                    {
                        DesabilitaComponentes();

                        gridControl1.Enabled = true;
                    }
                    else
                    {
                        toolStrip1.Enabled = true;
                        gridControl1.Enabled = true;
                    }

                    campoInteiroIDAPONTAMENTO.Set(int.Parse(idApontamento));

                    campoLookupUSUARIOCONSULTOR.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "CODUSUARIO");
                    cbTipoFaturamento.SelectedValue = MetodosSQL.GetField(sql, "TIPOFATURAMENTO");
                    cbInLoco.SelectedValue = MetodosSQL.GetField(sql, "INLOCO");
                    cbGeraReembolso.SelectedValue = MetodosSQL.GetField(sql, "REEMBOLSO");
                    tbValorAdicional.EditValue = Convert.ToDecimal(MetodosSQL.GetField(sql, "VALORAD"));
                    cbMotivo.SelectedValue = MetodosSQL.GetField(sql, "MOTVALAD");
                    campoLookupIDUNIDADE.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "IDUNIDADE");
                    campoLookupIDPROJETO.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "IDPROJETO");
                    clFilial.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "CODFILIAL");

                    if (!String.IsNullOrWhiteSpace(MetodosSQL.GetField(sql, "DTA")))
                        deDataApontamento.EditValue = Convert.ToDateTime(MetodosSQL.GetField(sql, "DTA"));

                    if (!String.IsNullOrWhiteSpace(MetodosSQL.GetField(sql, "INICIO")))
                        teInicio.EditValue = Convert.ToDateTime(MetodosSQL.GetField(sql, "INICIO"));

                    if (!String.IsNullOrWhiteSpace(MetodosSQL.GetField(sql, "TERMINO")))
                        teTermino.EditValue = Convert.ToDateTime(MetodosSQL.GetField(sql, "TERMINO"));


                    if (!String.IsNullOrWhiteSpace(MetodosSQL.GetField(sql, "ABONO")))
                        teAbono.EditValue = Convert.ToDateTime(MetodosSQL.GetField(sql, "ABONO"));


                    if (!String.IsNullOrEmpty(MetodosSQL.GetField(sql, "TOTALHORAS")))
                        teTotalHoras.EditValue = Convert.ToDateTime(MetodosSQL.GetField(sql, "TOTALHORAS"));
               

                    sql = String.Format(@"select * from GUSUARIO where CODUSUARIO = '{0}'", campoLookupUSUARIOCONSULTOR.textBoxCODIGO.Text);
                    campoLookupUSUARIOCONSULTOR.textBoxDESCRICAO.Text = MetodosSQL.GetField(sql, "NOME");


                    sql = String.Format(@"select NOME from AUNIDADE where IDUNIDADE = '{0}'", campoLookupIDUNIDADE.textBoxCODIGO.Text);
                    campoLookupIDUNIDADE.textBoxDESCRICAO.Text = MetodosSQL.GetField(sql, "NOME");


                    sql = String.Format(@"select DESCRICAO from APROJETO where IDPROJETO = '{0}'", campoLookupIDPROJETO.textBoxCODIGO.Text);
                    campoLookupIDPROJETO.textBoxDESCRICAO.Text = MetodosSQL.GetField(sql, "DESCRICAO");

                    sql = String.Format(@"select NOME from GFILIAL where CODEMPRESA = '{0}' and CODFILIAL = '{1}'", AppLib.Context.Empresa, clFilial.textBoxCODIGO.Text);
                    clFilial.textBoxDESCRICAO.Text = MetodosSQL.GetField(sql, "NOME");

                    sql = String.Format(@"select CODEMPRESA,
	                                             IDAPONTAMENTO,
	                                             IDAPONTAMENTOTAREFA,
	                                             IDTAREFA,
	                                             convert(varchar(10), HORAS, 108) as 'HORAS',
	                                             PERCENTUAL,
	                                             OBSERVACAO,
	                                             MINUTOS  
                                          from AAPONTAMENTOTAREFA where IDAPONTAMENTO = '{0}' and CODEMPRESA = '{1}'", idApontamento, AppLib.Context.Empresa);
                    gridControl1.DataSource = MetodosSQL.GetDT(sql);

                    sql = String.Format(@"select COUNT(1) as 'TOTAL' from AAPONTAMENTOTAREFA where IDAPONTAMENTO = '{0}'", idApontamento);

                    if (int.Parse(MetodosSQL.GetField(sql, "TOTAL")) > 0)
                    {
                        campoLookupUSUARIOCONSULTOR.Enabled = false;
                        campoLookupIDUNIDADE.Enabled = false;
                        campoLookupIDPROJETO.Enabled = false;
                    }
                    else
                    {
                        campoLookupUSUARIOCONSULTOR.Enabled = true;
                        campoLookupIDUNIDADE.Enabled = true;
                        campoLookupIDPROJETO.Enabled = true;
                    }

                }

                CalculaData();

                ConfigurarHorasIniciais();
                SetConsultaLookUp();

                CarregaVisaoUsuario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Salvar(bool sair)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(idApontamento))
                {
                    sql = String.Format(@"INSERT INTO AAPONTAMENTO (TIPOFATURAMENTO, INLOCO, REEMBOLSO, VALORAD, MOTVALAD, IDUNIDADE, IDPROJETO, DATA, INICIO, TERMINO, ABONO, CODEMPRESA, CODUSUARIO, CODFILIAL, TOTALHORAS, IDSTATUSAPONTAMENTO) 
                                          VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', 
                                          CONVERT(DATETIME, '{7}', 103), CONVERT(DATETIME, '{8}', 103), CONVERT(DATETIME, '{9}', 103), CONVERT(DATETIME, '{10}', 103), '{11}', '{12}', '{13}', '{14}', '{15}') SELECT SCOPE_IDENTITY()",
                                          /*{0}*/ cbTipoFaturamento.SelectedValue.ToString(),
                                          /*{1}*/ Convert.ToInt32(cbInLoco.SelectedValue),
                                          /*{2}*/ Convert.ToInt32(cbGeraReembolso.SelectedValue),
                                          /*{3}*/ (string.IsNullOrEmpty(tbValorAdicional.Text) ? "0" : tbValorAdicional.EditValue.ToString().Replace(",", ".")),
                                          /*{4}*/ cbMotivo.SelectedValue.ToString(),
                                          /*{5}*/ campoLookupIDUNIDADE.textBoxCODIGO.Text,
                                          /*{6}*/ campoLookupIDPROJETO.textBoxCODIGO.Text,
                                          /*{7}*/ deDataApontamento.EditValue,
                                          /*{8}*/ teInicio.EditValue,
                                          /*{9}*/ teTermino.EditValue,
                                          /*{10}*/ teAbono.EditValue,
                                          /*{11}*/ AppLib.Context.Empresa,
                                          /*{12}*/ campoLookupUSUARIOCONSULTOR.textBoxCODIGO.Text,
                                          /*{13}*/ clFilial.textBoxCODIGO.Text,
                                          /*{14}*/ StatusApontamento,
                                          /*{15}*/ teTotalHoras.EditValue);

                    idApontamento = MetodosSQL.ExecScalar(sql).ToString();
                }
                else
                {
                    sql = String.Format(@"UPDATE AAPONTAMENTO
                                          SET TIPOFATURAMENTO = '{0}',
                                          	INLOCO = '{1}',
	                                        REEMBOLSO = '{2}',
                                          	VALORAD = '{3}',
                                          	MOTVALAD = '{4}',
                                          	IDUNIDADE = '{5}',
                                          	IDPROJETO = '{6}',
                                          	DATA = CONVERT(DATETIME, '{7}', 103),
                                          	INICIO = CONVERT(DATETIME, '{8}', 103),
                                          	TERMINO = CONVERT(DATETIME, '{9}', 103),
                                          	ABONO = CONVERT(DATETIME, '{10}', 103),
                                            CODFILIAL = '{13}',
                                            IDSTATUSAPONTAMENTO = '{14}',
                                            TOTALHORAS =  CONVERT(DATETIME, '{15}', 103)
                                          WHERE IDAPONTAMENTO = '{11}'
                                          AND CODEMPRESA = '{12}'",
                                           /*{0}*/ cbTipoFaturamento.SelectedValue.ToString(),
                                          /*{1}*/ Convert.ToInt32(cbInLoco.SelectedValue),
                                          /*{2}*/ Convert.ToInt32(cbGeraReembolso.SelectedValue),
                                          /*{3}*/ (string.IsNullOrEmpty(tbValorAdicional.Text) ? "0" : tbValorAdicional.EditValue.ToString().Replace(",", ".")),
                                          /*{4}*/ cbMotivo.SelectedValue,
                                          /*{5}*/ campoLookupIDUNIDADE.textBoxCODIGO.Text,
                                          /*{6}*/ campoLookupIDPROJETO.textBoxCODIGO.Text,
                                          /*{7}*/ deDataApontamento.EditValue,
                                          /*{8}*/ teInicio.EditValue,
                                          /*{9}*/ teTermino.EditValue,
                                          /*{10}*/ teAbono.EditValue,
                                          /*{11}*/ idApontamento,
                                          /*{12}*/ AppLib.Context.Empresa,
                                          /*{13}*/ clFilial.textBoxCODIGO.Text,
                                          /*{14}*/ StatusApontamento,
                                          /*{15}*/ teTotalHoras.EditValue);

                    MetodosSQL.ExecQuery(sql);
                }

                AtualizaVisao();

                if (sair)
                {
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClickSalvar(Boolean sair)
        {
            DateTime Inicio = Convert.ToDateTime(teInicio.EditValue);
            DateTime Termino = Convert.ToDateTime(teTermino.EditValue);

            if (ValidarSalvar())
            {
                if (sair)
                {
                    if (diferencaTempoApontamento.Minutes > 0 || diferencaTempoApontamento.Hours > 0)
                    {
                        MessageBox.Show("O apontamento não será encerrado pois existe uma diferença de horas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        StatusApontamento = "0";
                        Salvar(sair);
                        return;
                    }
                    else
                    {
                        StatusApontamento = "0";

                        DialogResult r = MessageBox.Show("Deseja encerrar o apontamento?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (r == DialogResult.Yes)
                        {
                            StatusApontamento = "1";
                        }
                        else
                        {
                            StatusApontamento = "0";
                        }

                        Salvar(sair);
                    }
                }
                else
                {
                    if ((Termino.Hour - Inicio.Hour) > 0)
                    {
                        if (diferencaTempoApontamento.Minutes > 0 || diferencaTempoApontamento.Hours > 0)
                        {
                            MessageBox.Show("O apontamento não será encerrado pois existe uma diferença de horas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            StatusApontamento = "0";
                            return;
                        }

                        Salvar(sair);
                    }
                    else
                    {
                        Salvar(sair);
                    }
                }
            }
        }

        private void DesabilitaComponentes()
        {
            campoInteiroIDAPONTAMENTO.Enabled = false;
            clFilial.Enabled = false;
            campoLookupUSUARIOCONSULTOR.Enabled = false;
            cbTipoFaturamento.Enabled = false;
            campoLookupIDUNIDADE.Enabled = false;
            campoLookupIDPROJETO.Enabled = false;
            deDataApontamento.Enabled = false;
            teInicio.Enabled = false;
            teTermino.Enabled = false;
            teAbono.Enabled = false;
            cbInLoco.Enabled = false;
            cbGeraReembolso.Enabled = false;
            tbValorAdicional.Enabled = false;
            cbMotivo.Enabled = false;
            teTotalHoras.Enabled = false;
            teHorasApontadas.Enabled = false;
            teDiferencas.Enabled = false;
            btnSalvar.Enabled = false;
            btnOK.Enabled = false;
            toolStrip1.Enabled = false;
            btnNovo.Enabled = false;
            btnExcluir.Enabled = false;
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

        private void CarregaComboBoxGeraReembolso()
        {
            DataTable dtGeraReembolso = new DataTable();

            dtGeraReembolso.Columns.Add("Codigo", typeof(string));
            dtGeraReembolso.Columns.Add("Nome", typeof(string));

            dtGeraReembolso.Rows.Add("-", "- Selecione");
            dtGeraReembolso.Rows.Add("0", "Não");
            dtGeraReembolso.Rows.Add("1", "Sim");

            cbGeraReembolso.DataSource = dtGeraReembolso;
            cbGeraReembolso.ValueMember = "Codigo";
            cbGeraReembolso.DisplayMember = "Nome";
        }

        private void CarregaComboBoxMotivo()
        {
            DataTable dtMotivo = new DataTable();

            dtMotivo.Columns.Add("Codigo", typeof(string));
            dtMotivo.Columns.Add("Nome", typeof(string));

            dtMotivo.Rows.Add("-", "- Selecione");
            dtMotivo.Rows.Add("1", "Estacionamento");
            dtMotivo.Rows.Add("2", "Jantar");
            dtMotivo.Rows.Add("3", "Hospedagem");
            dtMotivo.Rows.Add("4", "Área Azul");
            dtMotivo.Rows.Add("5", "Clientes/Fornecedores");

            cbMotivo.DataSource = dtMotivo;
            cbMotivo.ValueMember = "Codigo";
            cbMotivo.DisplayMember = "Nome";
        }

        private void CarregaComboBoxTipoFaturamento()
        {
            DataTable dtTipoFaturamento = new DataTable();

            dtTipoFaturamento.Columns.Add("Codigo", typeof(string));
            dtTipoFaturamento.Columns.Add("Nome", typeof(string));

            dtTipoFaturamento.Rows.Add("-", "- Selecione");
            dtTipoFaturamento.Rows.Add("D", "Direto");
            dtTipoFaturamento.Rows.Add("P", "Parceiros");

            cbTipoFaturamento.DataSource = dtTipoFaturamento;
            cbTipoFaturamento.ValueMember = "Codigo";
            cbTipoFaturamento.DisplayMember = "Nome";
        }

        private void SalvarLayout()
        {
            try
            {
                AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GVISAOUSUARIO SET VISIVEL = 0 WHERE CODUSUARIO = ? AND VISAO = ?", new object[] { AppLib.Context.Usuario, tabela });

                for (int i = 0; i < gridView1.VisibleColumns.Count; i++)
                {
                    AppLib.ORM.Jit GVISAOUSUARIO = new AppLib.ORM.Jit(AppLib.Context.poolConnection.Get("Start"), "GVISAOUSUARIO");
                    GVISAOUSUARIO.Set("VISAO", tabela);
                    GVISAOUSUARIO.Set("CODUSUARIO", AppLib.Context.Usuario);
                    GVISAOUSUARIO.Set("COLUNA", gridView1.VisibleColumns[i].FieldName);
                    GVISAOUSUARIO.Set("SEQUENCIA", i);
                    GVISAOUSUARIO.Set("LARGURA", gridView1.VisibleColumns[i].Width);
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
                    AtualizaVisao();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregaVisaoUsuario()
        {
            try
            {
                sql = String.Format(@"SELECT COUNT(1) as 'TOTAL' FROM GVISAOUSUARIO WHERE VISAO = '{0}' AND CODUSUARIO = '{1}' AND VISIVEL = 1", tabela, AppLib.Context.Usuario);
                if (MetodosSQL.GetField(sql, "TOTAL") != "0")
                {
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
                }
                else
                {
                    SalvarLayout();
                }
            }
            catch (Exception)
            {

            }
        }

        #endregion
    }
}
