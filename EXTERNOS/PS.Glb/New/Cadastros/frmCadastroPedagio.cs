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
    public partial class frmCadastroPedagio : Form
    {
        string IDUNIDADE = String.Empty;
        string IDREEMBOLSO = String.Empty;
        string IDFILIAL = String.Empty;
        string sql = String.Empty;
        Boolean Editar = false;
        List<string> projetos = new List<string>();

        public frmCadastroPedagio(String _IDUNIDADE, String _IDREEMBOLSO, Boolean _Editar, String _IDFILIAL)
        {
            InitializeComponent();
            IDUNIDADE = _IDUNIDADE;
            IDREEMBOLSO = _IDREEMBOLSO;
            Editar = _Editar;
            IDFILIAL = _IDFILIAL;
        }    

        private void frmCadastroPedagio_Load(object sender, EventArgs e)
        {
            CarregaCampos();
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

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            DesabilitaCampos();
        }

        #region Projetos Associados

        private void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                PS.Glb.New.Visao.frmVisaoUnidadeProjeto frm = new Visao.frmVisaoUnidadeProjeto("");
                frm.CODEMPRESA = AppLib.Context.Empresa;
                frm.CODFILIAL = clFilial.textBoxCODIGO.Text;
                frm.CODUNIDADE = clUnidade.textBoxCODIGO.Text;
                frm.ShowDialog();

                if (frm.drc != null)
                {
                    DataRowCollection drc = frm.drc;

                    foreach (DataRow row in drc)
                    {
                        InsereProjeto((String)row["IDPROJETO"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            DataRowCollection drc = GetDataRows(true);

            foreach (DataRow row in drc)
            {
                RemoveProjeto((String)row["IDUNIDPROJETO"]);
            }
        }

        #endregion

        #region Métodos

        private void CarregaCampos()
        {
            try
            {
                clFilial.ColunaTabela = String.Format("(select * from GFILIAL where CODEMPRESA = '{0}') I", AppLib.Context.Empresa);

                if (!String.IsNullOrWhiteSpace(IDUNIDADE))
                {
                    sql = String.Format(@"select IDUNIDADE, NOME from AUNIDADE where IDUNIDADE = '{0}' and CODEMPRESA = '{1}'", IDUNIDADE, AppLib.Context.Empresa);
                    clUnidade.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "IDUNIDADE");
                    clUnidade.textBoxDESCRICAO.Text = MetodosSQL.GetField(sql, "NOME");

                    if (!String.IsNullOrWhiteSpace(IDREEMBOLSO))
                    {
                        sql = String.Format(@"select CODUSUARIO from AUNIDADEREEMBOLSO where IDREEMBOLSO = '{0}'", IDREEMBOLSO);
                        clUsuario.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "CODUSUARIO");
                        sql = String.Format(@"select NOME from GUSUARIO where CODUSUARIO =  '{0}'", clUsuario.textBoxCODIGO.Text);
                        clUsuario.textBoxDESCRICAO.Text = MetodosSQL.GetField(sql, "NOME");

                        sql = String.Format(@"select * from AUNIDADEREEMBOLSO where IDREEMBOLSO = '{0}'", IDREEMBOLSO);
                        txtDistancia.Text = MetodosSQL.GetField(sql, "DISTANCIAKM");
                        txtValorKM.Text = MetodosSQL.GetField(sql, "VALORKM");
                        txtValorPedagio.Text = MetodosSQL.GetField(sql, "VALORPEDAGIO");
                        txtRefeicao.Text = MetodosSQL.GetField(sql, "VALORREFEICAO");

                        clProduto.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "CODPRODUTO");
                        clProduto.textBox1_Leave(null, null);

                        if (MetodosSQL.GetField(sql, "COORDCLIENTE") == "1")
                        {
                            cbCOORDCLI.Checked = true;
                        }
                        else
                        {
                            cbCOORDCLI.Checked = false;
                        }

                        clFilial.textBoxCODIGO.Text = MetodosSQL.GetField(sql, "CODFILIAL");
                        sql = String.Format(@"select NOME from GFILIAL where CODEMPRESA = '{0}' and CODFILIAL = '{1}'", AppLib.Context.Empresa, MetodosSQL.GetField(sql, "CODFILIAL"));
                        clFilial.textBoxDESCRICAO.Text = MetodosSQL.GetField(sql, "NOME");

                        AtualizaGrid();

                        if (ContaProjetoAssociado() > 0)
                        {
                            clUsuario.Enabled = false;
                        }
                    }
                    else
                    {
                        clFilial.textBoxCODIGO.Text = IDFILIAL;
                        sql = String.Format(@"select NOME from GFILIAL where CODEMPRESA = '{0}' and CODFILIAL = '{1}'", AppLib.Context.Empresa, IDFILIAL);
                        clFilial.textBoxDESCRICAO.Text = MetodosSQL.GetField(sql, "NOME");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void DesabilitaCampos()
        {
            clProduto.Enabled = !cbCOORDCLI.Checked;
            txtDistancia.Enabled = !cbCOORDCLI.Checked;
            txtRefeicao.Enabled = !cbCOORDCLI.Checked;
            txtValorKM.Enabled = !cbCOORDCLI.Checked;
            txtValorPedagio.Enabled = !cbCOORDCLI.Checked;
            txtValorHoraA.Enabled = !cbCOORDCLI.Checked;

            clProduto.textBoxCODIGO.Text = "";
            txtDistancia.Text = "0,00";
            txtRefeicao.Text = "0,00";
            txtValorKM.Text = "0,00";
            txtValorPedagio.Text = "0,00";
            txtValorHoraA.Text = "0,00";
        }

        private int ContaProjetoAssociado()
        {
            try
            {
                sql = String.Format(@"select count(1) as 'TOTAL' from AUNIDADEPROJETO where CODEMPRESA = '{0}' and CODFILIAL = '{1}' and IDUNIDADE = {2} and CODUSUARIO = '{3}'", AppLib.Context.Empresa, clFilial.textBoxCODIGO.Text, clUnidade.textBoxCODIGO.Text, clUsuario.textBoxCODIGO.Text);
                return int.Parse(MetodosSQL.GetField(sql, "TOTAL"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void InsereProjeto(String Projeto)
        {
            sql = String.Format(@"select count(1) as 'TOTAL' from AUNIDADEPROJETO where CODEMPRESA = '{0}' and CODFILIAL = '{1}' and IDUNIDADE = {2} and CODUSUARIO = '{3}' and IDPROJETO = '{4}'", AppLib.Context.Empresa, clFilial.textBoxCODIGO.Text, clUnidade.textBoxCODIGO.Text, clUsuario.textBoxCODIGO.Text, Projeto);

            if (int.Parse(MetodosSQL.GetField(sql, "TOTAL")) > 0)
            {
                MessageBox.Show("Projeto já foi inserido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                sql = String.Format("insert into AUNIDADEPROJETO values ('{0}', '{1}', '{2}', '{3}', '{4}')",
                                        /*{0}*/ AppLib.Context.Empresa,
                                        /*{1}*/ clFilial.textBoxCODIGO.Text,
                                        /*{2}*/ clUnidade.textBoxCODIGO.Text,
                                        /*{3}*/ clUsuario.textBoxCODIGO.Text,
                                        /*{4}*/ Projeto);
                MetodosSQL.ExecQuery(sql);
                AtualizaGrid();
            }
        }

        private void RemoveProjeto(String IDUNIDPROJETO)
        {
            sql = String.Format("delete from AUNIDADEPROJETO where IDUNIDPROJETO = '{0}'",
                                        /*{0}*/ IDUNIDPROJETO);
            MetodosSQL.ExecQuery(sql);
            AtualizaGrid();
        }

        public System.Data.DataRowCollection GetDataRows(Boolean ValidarSelecao)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                System.Data.DataTable dt = new DataTable();
                int[] handles = gridView1.GetSelectedRows();

                for (int i = 0; i < handles.Length; i++)
                {
                    if (i == 0)
                    {
                        for (int col = 0; col < gridView1.GetDataRow(handles[i]).Table.Columns.Count; col++)
                        {
                            dt.Columns.Add(gridView1.GetDataRow(handles[i]).Table.Columns[col].ColumnName);
                        }
                    }

                    if (handles[i] >= 0)
                    {
                        dt.Rows.Add(gridView1.GetDataRow(handles[i]).ItemArray);
                    }
                }

                return dt.Rows;
            }
            else
            {
                if (ValidarSelecao)
                {
                    MessageBox.Show("Selecione o(s) registro(s).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return null;
            }
        }

        private void AtualizaGrid()
        {
            try
            {
                if (Editar)
                {
                    toolStrip1.Enabled = true;
                    gridControl1.Enabled = true;
                    sql = String.Format(@"select AUP.IDUNIDPROJETO, AUP.IDPROJETO, AP.DESCRICAO, AP.STATUS from AUNIDADEPROJETO AUP

                                            inner join APROJETO AP
                                            on AP.IDPROJETO = AUP.IDPROJETO 

                                            where AUP.CODEMPRESA = '{0}' 
                                            and AUP.CODFILIAL = '{1}' 
                                            and AUP.IDUNIDADE = {2} 
                                            and AUP.CODUSUARIO = '{3}'", AppLib.Context.Empresa, clFilial.textBoxCODIGO.Text, clUnidade.textBoxCODIGO.Text, clUsuario.textBoxCODIGO.Text);

                    gridControl1.DataSource = MetodosSQL.GetDT(sql);

                    gridView1.Columns["IDUNIDPROJETO"].Caption = "ID Unidade";
                    gridView1.Columns["IDPROJETO"].Caption = "ID Projeto";
                    gridView1.Columns["DESCRICAO"].Caption = "Descrição";
                    gridView1.Columns["STATUS"].Caption = "Status";

                    gridView1.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool Salvar()
        {
            if (ValidaSalvar())
            {
                try
                {
                    string CoordCli = String.Empty;

                    if (cbCOORDCLI.Checked)
                    {
                        CoordCli = "1";
                    }
                    else
                    {
                        CoordCli = "0";
                    }

                    if (Editar)
                    {
                        sql = String.Format(@"update AUNIDADEREEMBOLSO
                                            set CODUSUARIO = '{0}',
                                                DISTANCIAKM = {1},
	                                            VALORKM = {2},
	                                            VALORREFEICAO = {3},
	                                            VALORPEDAGIO = {4},
                                                CODFILIAL = '{5}',
                                                CODPRODUTO = '{6}',
                                                VALORHORAA = '{7}',
                                                COORDCLIENTE = '{8}'
                                            where IDREEMBOLSO = {9}",
                                             /*{0}*/ clUsuario.textBoxCODIGO.Text,
                                             /*{1}*/ txtDistancia.Text.Replace(",", "."),
                                             /*{2}*/ txtValorKM.Text.Replace(",", "."),
                                             /*{3}*/ txtRefeicao.Text.Replace(",", "."),
                                             /*{4}*/ txtValorPedagio.Text.Replace(",", "."),
                                             /*{5}*/ clFilial.textBoxCODIGO.Text,
                                             /*{6}*/ clProduto.textBoxCODIGO.Text,
                                             /*{7}*/ txtValorHoraA.Text.Replace(",", "."),
                                             /*{8}*/ CoordCli,
                                             /*{9}*/ IDREEMBOLSO);

                        MetodosSQL.ExecQuery(sql);
                    }
                    else
                    {
                        sql = String.Format("insert into AUNIDADEREEMBOLSO values ('{0}','{1}','{2}','{3}',{4},{5},{6},{7},'{8}','{9}', '{10}') select SCOPE_IDENTITY()",
                                             /*{0}*/ AppLib.Context.Empresa,
                                             /*{1}*/ clFilial.textBoxCODIGO.Text,
                                             /*{2}*/ IDUNIDADE,
                                             /*{3}*/ clUsuario.textBoxCODIGO.Text,
                                             /*{4}*/ txtDistancia.Text.Replace(",", "."),
                                             /*{5}*/ txtValorKM.Text.Replace(",", "."),
                                             /*{6}*/ txtRefeicao.Text.Replace(",", "."),
                                             /*{7}*/ txtValorPedagio.Text.Replace(",", "."),
                                             /*{8}*/ clProduto.textBoxCODIGO.Text,
                                             /*{9}*/ txtValorHoraA.Text.Replace(",", "."),
                                             /*{10}*/ CoordCli);

                        IDREEMBOLSO = Convert.ToString(MetodosSQL.ExecScalar(sql));

                        Editar = true;

                        AtualizaGrid();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private bool ValidaSalvar()
        {
            if (Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, @"SELECT * FROM AUNIDADEREEMBOLSO WHERE IDUNIDADE = ? AND CODUSUARIO = ?", new object[] { IDUNIDADE, clUsuario.Get() })) > 1)
            {
                XtraMessageBox.Show("O usuário selecionado já está cadastrado nesta Unidade.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        #endregion
    }
}
