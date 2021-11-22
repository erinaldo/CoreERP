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
    public partial class FrmCentroTrabalhoCalendario : Form
    {
        string tabela = "PCALENDARIO";
        string relacionamento = string.Empty;
        string query = string.Empty;
        List<string> tabelasFilhas = new List<string>();

        //Variaveis para usar quando a tela abre para consulta.
        public string CodCTrabalho = string.Empty;
        public FrmCentroTrabalhoCalendario()
        {
            InitializeComponent();
        }

        public FrmCentroTrabalhoCalendario(string _where)
        {
            InitializeComponent();
            query = _where;
            carregaGrid(query);
        }

        public void carregaGrid(string where)
        {
            try
            {
                string sql = new PS.Glb.Class.Utilidades().getVisao(tabela, relacionamento, tabelasFilhas, where);

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

                grid.DataSource = null;
                gridView1.Columns.Clear();
                grid.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql);

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                MessageBox.Show("Cadastro salvo com Sucesso.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
        }

        private bool Salvar()
        {
            bool _salvar = false;

            if (validacao() == true)
            {
                AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
                AppLib.ORM.Jit v = new AppLib.ORM.Jit(conn, "PCENTROTRABALHOCALENDARIO");
                conn.BeginTransaction();

                try
                {
                    DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

                    v.Set("CODEMPRESA", AppLib.Context.Empresa);
                    v.Set("CODFILIAL", AppLib.Context.Filial);
                    v.Set("CODCTRABALHO", CodCTrabalho);
                    v.Set("CODCALEND", row1["PCALENDARIO.CODCALEND"].ToString());

                    v.Save();
                    conn.Commit();
                    _salvar = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Rollback();
                    _salvar = false;
                }
            }
            else
            {
                _salvar = false;
            }

            return _salvar;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Salvar() == true)
            {
                this.Dispose();
            }
        }

        private bool validacao()
        {
            bool verifica = true;

            if (gridView1.SelectedRowsCount == 0)
            {
                MessageBox.Show("Selecione um calendário.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                verifica = false;
            }

            DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));

            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM PCENTROTRABALHOCALENDARIO JOIN PCALENDARIO ON  PCALENDARIO.CODEMPRESA = PCENTROTRABALHOCALENDARIO.CODEMPRESA	AND PCALENDARIO.CODFILIAL = PCENTROTRABALHOCALENDARIO.CODFILIAL AND PCALENDARIO.CODCALEND = PCENTROTRABALHOCALENDARIO.CODCALEND WHERE PCENTROTRABALHOCALENDARIO.CODEMPRESA = ? AND PCENTROTRABALHOCALENDARIO.CODFILIAL = ?   AND PCALENDARIO.ANOCALEND = ?   AND PCENTROTRABALHOCALENDARIO.CODCTRABALHO = ?   AND PCALENDARIO.CODCALEND <> ?  ", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, row1["PCALENDARIO.ANOCALEND"].ToString(), CodCTrabalho, row1["PCALENDARIO.CODCALEND"].ToString() });
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Já existe um Calendário deste Ano de Referência para este Centro de Trabalho.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                verifica = false;
            }

            return verifica;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FrmCentroTrabalhoCalendario_Load(object sender, EventArgs e)
        {

        }
    }
}
