using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PS.Lib;

namespace PS.Glb
{
    public partial class PSPartAprovaDescontoAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        public List<string> ListaCodOper;
        public PSPartAprovaDescontoAppFrm()
        {
            InitializeComponent();

        }
        private void carregaGridOperacoes()
        {
            string codoper = string.Empty;
            for (int i = 0; i < ListaCodOper.Count; i++)
            {
                if (string.IsNullOrEmpty(codoper))
                {
                    codoper = ListaCodOper[i];
                    //codoper = psPartApp.DataGrid.SelectedRows[i].Cells["CODOPER"].Value.ToString();
                }
                else
                {
                    codoper = codoper + ", " + ListaCodOper[i];// psPartApp.DataGrid.SelectedRows[i].Cells["CODOPER"].Value.ToString();
                }
            }

            string sql = AppLib.Context.poolConnection.Get("Start").ParseCommand(@"SELECT GOPER.CODOPER, VCLIFOR.CODCLIFOR, VCLIFOR.NOMEFANTASIA CLIENTE, GOPER.DATACRIACAO, GOPER.CODUSUARIOCRIACAO, VVENDEDOR.NOMEFANTASIA VENDEDOR, GOPER.TIPOBLOQUEIODESC, GOPER.VALORBRUTO, GOPER.VALORDESCONTO
FROM
GOPER
INNER JOIN VCLIFOR ON GOPER.CODEMPRESA = VCLIFOR.CODEMPRESA AND GOPER.CODCLIFOR = VCLIFOR.CODCLIFOR
LEFT JOIN VVENDEDOR ON GOPER.CODEMPRESA = VVENDEDOR.CODEMPRESA AND GOPER.CODVENDEDOR = VVENDEDOR.CODVENDEDOR
WHERE
GOPER.CODOPER IN (?) AND GOPER.CODEMPRESA = ? AND GOPER.CODSTATUS = 7", new object[] { codoper, AppLib.Context.Empresa });
            sql = sql.Replace("'", "");
            dataGridView1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { });
            //Alinhamento das colunas
            dataGridView1.Columns["VALORBRUTO"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["VALORBRUTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["VALORBRUTO"].DefaultCellStyle.Format = "c";
            dataGridView1.Columns["VALORDESCONTO"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["VALORDESCONTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["VALORDESCONTO"].DefaultCellStyle.Format = "c";
            
            
            //Altera os nomes das colunas
            dataGridView1.Columns["CODOPER"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'GOPER' AND COLUNA = 'CODOPER'", new object[] { }).ToString();
            dataGridView1.Columns["CODCLIFOR"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'VCLIFOR' AND COLUNA = 'CODCLIFOR'", new object[] { }).ToString();
            dataGridView1.Columns["CLIENTE"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'VCLIFOR' AND COLUNA = 'NOMEFANTASIA'", new object[] { }).ToString();
            dataGridView1.Columns["DATACRIACAO"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'GOPER' AND COLUNA = 'DATACRIACAO'", new object[] { }).ToString();
            dataGridView1.Columns["CODUSUARIOCRIACAO"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'GOPER' AND COLUNA = 'CODUSUARIOCRIACAO'", new object[] { }).ToString();
            dataGridView1.Columns["VENDEDOR"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'VVENDEDOR' AND COLUNA = 'NOMEFANTASIA'", new object[] { }).ToString();
            dataGridView1.Columns["TIPOBLOQUEIODESC"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'GOPER' AND COLUNA = 'TIPOBLOQUEIODESC'", new object[] { }).ToString();
            dataGridView1.Columns["VALORBRUTO"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'GOPER' AND COLUNA = 'VALORBRUTO'", new object[] { }).ToString();
            dataGridView1.Columns["VALORDESCONTO"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'GOPER' AND COLUNA = 'VALORDESCONTO'", new object[] { }).ToString();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void carregaGrid()
        {
            //Busca o tipo de Bloqueio da opera
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (string.IsNullOrEmpty(dataGridView1.SelectedRows[0].Cells["TIPOBLOQUEIODESC"].Value.ToString()))
                {
                    dataGridView2.DataSource = null;
                    return;
                }
                if (dataGridView1.SelectedRows[0].Cells["TIPOBLOQUEIODESC"].Value.ToString() == "T")
                {
                    dataGridView2.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT VALORBRUTO, VALORDESCONTO, PERCDESCONTO, LIMITEDESC, TIPOBLOQUEIODESC FROM GOPER WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { dataGridView1.SelectedRows[0].Cells["CODOPER"].Value.ToString(), AppLib.Context.Empresa });
                    // Busca campos da gdicionario
                    dataGridView2.Columns["VALORBRUTO"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField("CÓD. AUXILIAR", @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'GOPER' AND COLUNA = 'VALORBRUTO'", new object[] { }).ToString();
                    dataGridView2.Columns["VALORDESCONTO"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField("CÓD. AUXILIAR", @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'GOPER' AND COLUNA = 'VALORDESCONTO'", new object[] { }).ToString();
                    dataGridView2.Columns["PERCDESCONTO"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField("CÓD. AUXILIAR", @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'GOPER' AND COLUNA = 'PERCDESCONTO'", new object[] { }).ToString();
                    dataGridView2.Columns["LIMITEDESC"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField("CÓD. AUXILIAR", @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'GOPER' AND COLUNA = 'LIMITEDESC'", new object[] { }).ToString();
                    dataGridView2.Columns["TIPOBLOQUEIODESC"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField("CÓD. AUXILIAR", @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'GOPER' AND COLUNA = 'TIPOBLOQUEIODESC'", new object[] { }).ToString();

                    dataGridView2.Columns["VALORBRUTO"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView2.Columns["VALORBRUTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView2.Columns["VALORBRUTO"].DefaultCellStyle.Format = "c";
                    dataGridView2.Columns["VALORDESCONTO"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView2.Columns["VALORDESCONTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView2.Columns["VALORDESCONTO"].DefaultCellStyle.Format = "c";
                    dataGridView2.Columns["PERCDESCONTO"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView2.Columns["PERCDESCONTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView2.Columns["PERCDESCONTO"].DefaultCellStyle.Format = "c";
                    dataGridView2.Columns["LIMITEDESC"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView2.Columns["LIMITEDESC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView2.Columns["LIMITEDESC"].DefaultCellStyle.Format = "c";
                }
                else if (dataGridView1.SelectedRows[0].Cells["TIPOBLOQUEIODESC"].Value.ToString() == "I")
                {
                    dataGridView2.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT VPRODUTO.CODPRODUTO,  VPRODUTO.CODIGOAUXILIAR, VPRODUTO.DESCRICAO, GOPER.LIMITEDESC, GOPERITEM.VLDESCONTO, GOPER.TIPOBLOQUEIODESC
FROM 
GOPERITEM 
INNER JOIN GOPER ON GOPERITEM.CODEMPRESA = GOPER.CODEMPRESA AND GOPERITEM.CODOPER = GOPER.CODOPER
INNER JOIN VPRODUTO ON GOPERITEM.CODPRODUTO = VPRODUTO.CODPRODUTO AND GOPERITEM.CODEMPRESA = VPRODUTO.CODEMPRESA
WHERE 
GOPER.CODOPER = ?
AND GOPER.CODEMPRESA = ?
AND GOPERITEM.PRDESCONTO > GOPER.LIMITEDESC", new object[] { dataGridView1.SelectedRows[0].Cells["CODOPER"].Value.ToString(), AppLib.Context.Empresa });
                    dataGridView2.Columns["LIMITEDESC"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView2.Columns["LIMITEDESC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView2.Columns["LIMITEDESC"].DefaultCellStyle.Format = "c";
                    dataGridView2.Columns["VLDESCONTO"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView2.Columns["VLDESCONTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView2.Columns["VLDESCONTO"].DefaultCellStyle.Format = "c";
                    // Busca campos da gdicionario
                    dataGridView2.Columns["CODPRODUTO"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField("CÓD. AUXILIAR", @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'VPRODUTO' AND COLUNA = 'CODPRODUTO'", new object[] { }).ToString();
                    dataGridView2.Columns["CODIGOAUXILIAR"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField("CÓD. AUXILIAR", @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'VPRODUTO' AND COLUNA = 'CODIGOAUXILIAR'", new object[] { }).ToString();
                    dataGridView2.Columns["DESCRICAO"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField("CÓD. AUXILIAR", @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'VPRODUTO' AND COLUNA = 'DESCRICAO'", new object[] { }).ToString();
                    dataGridView2.Columns["LIMITEDESC"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField("CÓD. AUXILIAR", @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'GOPER' AND COLUNA = 'LIMITEDESC'", new object[] { }).ToString();
                    dataGridView2.Columns["VLDESCONTO"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField("CÓD. AUXILIAR", @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'GOPERITEM' AND COLUNA = 'VLDESCONTO'", new object[] { }).ToString();
                    dataGridView2.Columns["TIPOBLOQUEIODESC"].HeaderText = AppLib.Context.poolConnection.Get("Start").ExecGetField("CÓD. AUXILIAR", @"SELECT DESCRICAO FROM GDICIONARIO WHERE TABELA = 'GOPER' AND COLUNA = 'TIPOBLOQUEIODESC'", new object[] { }).ToString();

                }
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                
                //dataGridView2.AutoResizeColumns();
            }
        }

        public override Boolean Execute()
        {
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GOPERAPROV (CODEMPRESA, CODOPER, USUARIO, DATA, MOTIVO) VALUES (?,?,?,?,?)", new object[] { AppLib.Context.Empresa, dataGridView1.SelectedRows[i].Cells["CODOPER"].Value, AppLib.Context.Usuario, AppLib.Context.poolConnection.Get("Start").GetDateTime(), txtMotivo.Text });

                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE GOPER SET CODSTATUS = 0 WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] {dataGridView1.SelectedRows[i].Cells["CODOPER"].Value, AppLib.Context.Empresa });
            }
            return true;
        }

        private void PSPartAprovaDescontoAppFrm_Load(object sender, EventArgs e)
        {

            carregaGridOperacoes();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            carregaGrid();
        }
    }
}
