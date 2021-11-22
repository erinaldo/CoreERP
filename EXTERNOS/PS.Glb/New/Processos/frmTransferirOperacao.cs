using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.New.Processos
{
    public partial class frmTransferirOperacao : Form
    {
        private string codOper = string.Empty;
        private int codFilial = 0;
        public string codMenu = string.Empty;
        string tabela = "GTIPOPERFAT";
        List<string> m_codOper;
        //public frmTransferirOperacao(string _codOper)
        //{
        //    InitializeComponent();
        //    codOper = _codOper;
        //    carregaGrid();
        //}

        public frmTransferirOperacao(List<string> _mCodOper, int _codFilial)
        {
            InitializeComponent();

            m_codOper = _mCodOper;
            codFilial = _codFilial;
            codOper = m_codOper[0].ToString();
            carregaGrid();
        }

        private void carregaGrid()
        {
            //            string sql = @"SELECT 
            //GTIPOPERFAT.CODTIPOPERFAT, 
            //GTIPOPER.DESCRICAO 
            //FROM 
            //GTIPOPERFAT 
            //INNER JOIN GTIPOPER ON GTIPOPERFAT.CODEMPRESA = GTIPOPER.CODEMPRESA AND GTIPOPERFAT.CODTIPOPERFAT = GTIPOPER.CODTIPOPER
            //INNER JOIN GOPER ON GTIPOPERFAT.CODTIPOPER = GOPER.CODTIPOPER AND GTIPOPERFAT.CODEMPRESA = GOPER.CODEMPRESA
            //WHERE 
            //GOPER.CODOPER = ?
            //AND GTIPOPERFAT.CODEMPRESA = ?";

            //            gridControl1.DataSource = null;
            //            gridView1.Columns.Clear();
            //            gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { codOper, AppLib.Context.Empresa });

            string sql = @"SELECT DISTINCT(GTIPOPER.CODTIPOPER), GTIPOPER.DESCRICAO 
FROM GTIPOPER
INNER JOIN GPERFILTIPOPER ON GTIPOPER.CODEMPRESA = GPERFILTIPOPER.CODEMPRESA AND GTIPOPER.CODTIPOPER = GPERFILTIPOPER.CODTIPOPER
INNER JOIN GUSUARIOPERFIL ON GPERFILTIPOPER.CODEMPRESA = GUSUARIOPERFIL.CODEMPRESA AND GPERFILTIPOPER.CODPERFIL = GUSUARIOPERFIL.CODPERFIL
INNER JOIN GTIPOPERFAT ON  GPERFILTIPOPER.CODTIPOPER = GTIPOPERFAT.CODTIPOPERFAT
INNER JOIN GOPER ON GTIPOPERFAT.CODTIPOPER = GOPER.CODTIPOPER 
WHERE GUSUARIOPERFIL.CODEMPRESA = ?
AND GUSUARIOPERFIL.CODUSUARIO = ?
AND GOPER.CODOPER = ?
";

            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = AppLib.Context.poolConnection.Get("Start").ExecQuery(sql, new object[] { AppLib.Context.Empresa, AppLib.Context.Usuario, codOper });

            DataTable dic = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GDICIONARIO WHERE TABELA = ?", new object[] { tabela });
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                gridView1.BestFitColumns();
                dic.PrimaryKey = new DataColumn[] { dic.Columns["COLUNA"] };
                DataRow result = dic.Rows.Find(new object[] { gridView1.Columns[i].FieldName.ToString() });
                if (result != null)
                {
                    gridView1.Columns[i].Caption = result["DESCRICAO"].ToString();
                }
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            // Variável que define que o botão Cancelar do fomrulário de Operações será inativo.
            bool Cancela = false;

            try
            {
                if (gridView1.SelectedRowsCount > 0)
                {

                    if (rbTotal.Checked == true)
                    {
                        DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                        for (int i = 0; i < m_codOper.Count; i++)
                        {

                            //AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE GOPERITEM SET QUANTIDADE_FATURADO = QUANTIDADE, QUANTIDADE_SALDO = 0 WHERE CODEMPRESA = ? AND CODOPER = ?", new object[] { AppLib.Context.Empresa, m_codOper[i] });

                            //Copia a operação
                            codOper = new Class.CopiaOperacao().TransferiOperacao(m_codOper[i].ToString(), row1["CODTIPOPER"].ToString(), codFilial);

                            if (!string.IsNullOrEmpty(codOper))
                            {
                                //abre a tela
                                PSPartOperacaoEdit frm = new PSPartOperacaoEdit();
                                frm.codoper = Convert.ToInt32(codOper);
                                frm.codFilial = codFilial;
                                frm.btnFechar.Enabled = false;
                                frm.edita = true;
                                frm.faturamento = true;
                                frm.codMenu = codMenu;
                                frm.VerificaCancela = Cancela;
                                frm.FaturamentoOperacao = true;
                                frm.ShowDialog();
                                this.Dispose();
                            }
                            else
                            {
                                MessageBox.Show("Não foi possível transferir essa operação.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    // Parcial
                    else
                    {
                        //RECALCULAR O FINANCEIRO

                        //Atribui os valores da operação ao objeto operacao
                        Class.Goper operacao = new Class.Goper().getGoper(codOper, AppLib.Context.poolConnection.Get("Start"));
                        //Atribui todos os itens dos pedidos selecionados ao objeto itens 
                        List<Class.GoperItem> itens = new Class.GoperItem().getGoperItemFaturamento(m_codOper, AppLib.Context.poolConnection.Get("Start"));
                        //Abre o form
                        ERP.Comercial.FormFaturaParcial frm = new ERP.Comercial.FormFaturaParcial(operacao, itens);
                        DataRow row1 = gridView1.GetDataRow(Convert.ToInt32(gridView1.GetSelectedRows().GetValue(0).ToString()));
                        frm.codFilial = codFilial;
                        // João Pedro Luchiari - 17/07/2018 - Acerto permissão
                        //frm.codTipOperDestino = row1["CODTIPOPERFAT"].ToString();
                        frm.codTipOperDestino = row1["CODTIPOPER"].ToString();
                        frm.ShowDialog();
                        this.Dispose();
                    }
                }

                else
                {
                    MessageBox.Show("Favor selecionar a operação de destino.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
