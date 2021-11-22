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
    public partial class PSPartVinculaLancaAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        PS.Lib.Constantes ct = new PS.Lib.Constantes();
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        PS.Lib.Valida vl = new PS.Lib.Valida();

        private int CODEMPRESA { get; set; }
        private int CODLANCA { get; set; }
        private int CODRELLANCA { get; set; }
        
        private int CODSTATUS { get; set; }

        private String CODCLIFOR { get; set; }
        private int TIPOPAGREC { get; set; }
        private int CLASSIFICACAO { get; set; }
        private decimal VLORIGINAL { get; set; }
        private decimal VLJUROS { get; set; }
        private decimal VLMULTA { get; set; }
        private decimal VLDESCONTO { get; set; }

        private List<FLancaVinculo> listaComVinculo { get; set; }
        private List<FLancaVinculo> listaSemVinculo { get; set; }
        
        public PSPartVinculaLancaAppFrm()
        {
            InitializeComponent();

            dataGridViewCOMVINCULO.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCOMVINCULO.DefaultCellStyle.SelectionBackColor = ct.corSelecaoGrid;
            dataGridViewCOMVINCULO.AlternatingRowsDefaultCellStyle.BackColor = ct.corImparGrid;

            dataGridViewSEMVINCULO.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCOMVINCULO.DefaultCellStyle.SelectionBackColor = ct.corSelecaoGrid;
            dataGridViewCOMVINCULO.AlternatingRowsDefaultCellStyle.BackColor = ct.corImparGrid;
        }

        public int CarregaCODRELLANCA( int CODEMPRESA, int CODLANCA)
        {
            String consultaCODRELLANCA = "SELECT CODRELLANCA FROM FRELLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?";
            return int.Parse(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, consultaCODRELLANCA, new Object[] { CODEMPRESA, CODLANCA }).ToString());
        }

        private void PSPartVinculaLancaAppFrm_Load(object sender, EventArgs e)
        {
            // LIMPA AS GRIDS
            listaComVinculo = new List<FLancaVinculo>();
            listaSemVinculo = new List<FLancaVinculo>();
            dataGridViewCOMVINCULO.DataSource = null;
            dataGridViewSEMVINCULO.DataSource = null;



            // OBTÉM OS CAMPOS CHAVE
            if (psPartApp.DataGrid != null)
            {
                for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                {
                    if (psPartApp.DataGrid.Rows[i].Selected)
                    {
                        CODEMPRESA = int.Parse(gb.RetornaDataFieldByCampo(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.Rows[i]), "CODEMPRESA").Valor.ToString());
                        CODLANCA = int.Parse(gb.RetornaDataFieldByCampo(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.Rows[i]), "CODLANCA").Valor.ToString());

                        // INTERROMPE O LOOP
                        i = psPartApp.DataGrid.Rows.Count;
                    }
                }
            }



            // CARREGA O CODIGO DO RELACIONAMENTO DE LANÇAMENTOS
            CODRELLANCA = this.CarregaCODRELLANCA(CODEMPRESA, CODLANCA);



            // CARREGA CAMPOS DO LANÇAMENTO
            String consultaFLANCA = @"
SELECT FLANCA.*,
( SELECT CLASSIFICACAO FROM FTIPDOC WHERE FTIPDOC.CODEMPRESA = FLANCA.CODEMPRESA AND FTIPDOC.CODTIPDOC = FLANCA.CODTIPDOC ) CLASSIFICACAO
FROM FLANCA
WHERE CODEMPRESA = ?
  AND CODLANCA = ?";

            System.Data.DataTable dtFLANCA = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaFLANCA, new Object[] { CODEMPRESA, CODLANCA });

            if (dtFLANCA.Rows.Count == 1)
            {
                CODCLIFOR = dtFLANCA.Rows[0]["CODCLIFOR"].ToString();
                TIPOPAGREC = int.Parse(dtFLANCA.Rows[0]["TIPOPAGREC"].ToString());
                CLASSIFICACAO = int.Parse(dtFLANCA.Rows[0]["CLASSIFICACAO"].ToString());
                CODSTATUS = int.Parse(dtFLANCA.Rows[0]["CODSTATUS"].ToString());
                VLORIGINAL = Convert.ToDecimal(dtFLANCA.Rows[0]["VLORIGINAL"]);
                VLJUROS = Convert.ToDecimal(dtFLANCA.Rows[0]["VLJUROS"]);
                VLMULTA = Convert.ToDecimal(dtFLANCA.Rows[0]["VLMULTA"]);
                VLDESCONTO = Convert.ToDecimal(dtFLANCA.Rows[0]["VLDESCONTO"]);
            }



            #region VALIDAÇÕES

            if (CODSTATUS == 1)
            {
                AppLib.Windows.FormMessageDefault.ShowError("Lançamento baixado não pode ser vinculado.");
                this.Close();
            }

            if (CODSTATUS == 2)
            {
                AppLib.Windows.FormMessageDefault.ShowError("Lançamento cancelado não pode ser vinculado.");
                this.Close();
            }

            if (CLASSIFICACAO == 0)
            {
                AppLib.Windows.FormMessageDefault.ShowError("Lançamento de adiantamento não pode ser vinculado.\r\nDica: Selecione um lançamento com tipo de documento sem classificação.");
                this.Close();
            }

            if (CLASSIFICACAO == 1)
            {
                AppLib.Windows.FormMessageDefault.ShowError("Lançamento de devolução não pode ser vinculado.\r\nDica: Selecione um lançamento com tipo de documento sem classificação.");
                this.Close();
            }

            if (CLASSIFICACAO == 3)
            {
                AppLib.Windows.FormMessageDefault.ShowError("Lançamento de previsão não pode ser vinculado.\r\nDica: Selecione um lançamento com tipo de documento sem classificação.");
                this.Close();
            }

            #endregion



            // CARREGA AS GRIDS
            this.CarregarGridComVinculo(CODEMPRESA, CODLANCA);
            this.CarregarGridSemVinculo(CODEMPRESA, CODLANCA);
            this.FormatarValoresGrid();
            this.RecalculaSaldo();
        }

        private void CarregarGridComVinculo(int CODEMPRESA, int CODLANCA)
        {
            // SE NÃO POSSUI VINCULOS AINDA
            if (CODRELLANCA == 0)
            {
                // NÃO NECESSITA DE CARREGAR A GRID DE CIMA
            }
            else
            {
                String consultaGridComVinculo = @"
SELECT CODLANCA, CODTIPDOC, NUMERO, DATAEMISSAO, VLORIGINAL, DATAVENCIMENTO, VLLIQUIDO, DATABAIXA, VLBAIXADO
FROM FLANCA
WHERE CODEMPRESA = ?
  AND CODLANCA <> ?
  AND CODLANCA IN ( SELECT CODLANCA FROM FRELLANCA WHERE CODEMPRESA = ? AND CODRELLANCA = ? )";

                System.Data.DataTable dtGridComVinculo = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaGridComVinculo, new Object[] { CODEMPRESA, CODLANCA, CODEMPRESA, CODRELLANCA });

                for (int i = 0; i < dtGridComVinculo.Rows.Count; i++)
                {
                    FLancaVinculo item = new FLancaVinculo();
                    item.CODLANCA = int.Parse(dtGridComVinculo.Rows[i]["CODLANCA"].ToString());
                    item.CODTIPDOC = dtGridComVinculo.Rows[i]["CODTIPDOC"].ToString();
                    item.NUMERO = dtGridComVinculo.Rows[i]["NUMERO"].ToString();
                    item.DATAEMISSAO = Convert.ToDateTime(dtGridComVinculo.Rows[i]["DATAEMISSAO"]);
                    item.VLORIGINAL = Convert.ToDecimal(dtGridComVinculo.Rows[i]["VLORIGINAL"]);
                    item.DATAVENCIMENTO = Convert.ToDateTime(dtGridComVinculo.Rows[i]["DATAVENCIMENTO"]);
                    item.VLLIQUIDO = Convert.ToDecimal(dtGridComVinculo.Rows[i]["VLLIQUIDO"]);

                    if (dtGridComVinculo.Rows[i]["DATABAIXA"] != DBNull.Value)
                    {
                        item.DATABAIXA = Convert.ToDateTime(dtGridComVinculo.Rows[i]["DATABAIXA"]);
                    }

                    if (dtGridComVinculo.Rows[i]["VLBAIXADO"] != DBNull.Value)
                    {
                        item.VLBAIXADO = Convert.ToDecimal(dtGridComVinculo.Rows[i]["VLBAIXADO"]);
                    }

                    listaComVinculo.Add(item);
                }

                dataGridViewCOMVINCULO.DataSource = listaComVinculo;
            }
        }
        public int CarregaTIPOPAGREC(int CODEMPRESA, int CODLANCA)
        {
            String consultaCODRELLANCA = "SELECT TIPOPAGREC FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ?";
            return int.Parse(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, consultaCODRELLANCA, new Object[] { CODEMPRESA, CODLANCA }).ToString());
        }

        public System.Data.DataTable GetRegistrosSemVinculo(int CODEMPRESA, int CODLANCA)
        {
            // CODRELLANCA = this.CarregaCODRELLANCA(CODEMPRESA, CODLANCA);
            TIPOPAGREC = this.CarregaTIPOPAGREC(CODEMPRESA, CODLANCA);

            String consultaGridSemVinculo = @"
SELECT CODLANCA, CODTIPDOC, NUMERO, DATAEMISSAO, VLORIGINAL, DATAVENCIMENTO, VLLIQUIDO, DATABAIXA, VLBAIXADO
FROM FLANCA
WHERE CODEMPRESA = ?
  AND CODCLIFOR = ( SELECT CODCLIFOR FROM FLANCA WHERE CODEMPRESA = ? AND CODLANCA = ? )
  AND CODLANCA <> ?
  AND CODLANCA NOT IN ( SELECT CODLANCA FROM FRELLANCA WHERE CODEMPRESA = ? )
  AND ((TIPOPAGREC = [TIPOPAGREC_CONTRARIO] AND CODSTATUS = 0 AND CODTIPDOC IN ( SELECT CODTIPDOC FROM FTIPDOC WHERE CODEMPRESA = ? AND CLASSIFICACAO = 1 )) OR
       (TIPOPAGREC = [TIPOPAGREC_IGUAL] AND CODSTATUS = 1 AND CODTIPDOC IN ( SELECT CODTIPDOC FROM FTIPDOC WHERE CODEMPRESA = ? AND CLASSIFICACAO = 0 )))";

            String TIPOPAGREC_CONTRARIO = "";

            if (TIPOPAGREC == 0)
            {
                TIPOPAGREC_CONTRARIO += "1";
            }
            else
            {
                TIPOPAGREC_CONTRARIO += "0";
            }

            consultaGridSemVinculo = consultaGridSemVinculo.Replace("[TIPOPAGREC_IGUAL]", TIPOPAGREC.ToString());
            consultaGridSemVinculo = consultaGridSemVinculo.Replace("[TIPOPAGREC_CONTRARIO]", TIPOPAGREC_CONTRARIO);

            System.Data.DataTable dtGridSemVinculo = AppLib.Context.poolConnection.Get("Start").ExecQuery(consultaGridSemVinculo, new Object[] { CODEMPRESA, CODEMPRESA, CODLANCA, CODLANCA, CODEMPRESA, CODEMPRESA, CODEMPRESA });
            return dtGridSemVinculo;
        }

        private void CarregarGridSemVinculo(int CODEMPRESA, int CODLANCA)
        {
            System.Data.DataTable dtGridSemVinculo = this.GetRegistrosSemVinculo(CODEMPRESA, CODLANCA);

            for (int i = 0; i < dtGridSemVinculo.Rows.Count; i++)
            {
                FLancaVinculo item = new FLancaVinculo();
                item.CODLANCA = int.Parse(dtGridSemVinculo.Rows[i]["CODLANCA"].ToString());
                item.CODTIPDOC = dtGridSemVinculo.Rows[i]["CODTIPDOC"].ToString();
                item.NUMERO = dtGridSemVinculo.Rows[i]["NUMERO"].ToString();
                item.DATAEMISSAO = Convert.ToDateTime(dtGridSemVinculo.Rows[i]["DATAEMISSAO"]);
                item.VLORIGINAL = Convert.ToDecimal(dtGridSemVinculo.Rows[i]["VLORIGINAL"]);
                item.DATAVENCIMENTO = Convert.ToDateTime(dtGridSemVinculo.Rows[i]["DATAVENCIMENTO"]);
                item.VLLIQUIDO = Convert.ToDecimal(dtGridSemVinculo.Rows[i]["VLLIQUIDO"]);

                if (dtGridSemVinculo.Rows[i]["DATABAIXA"] != DBNull.Value)
                {
                    item.DATABAIXA = Convert.ToDateTime(dtGridSemVinculo.Rows[i]["DATABAIXA"]);
                }

                if (dtGridSemVinculo.Rows[i]["VLBAIXADO"] != DBNull.Value)
                {
                    item.VLBAIXADO = Convert.ToDecimal(dtGridSemVinculo.Rows[i]["VLBAIXADO"]);
                }

                listaSemVinculo.Add(item);
            }

            dataGridViewSEMVINCULO.DataSource = listaSemVinculo;
        }

        private void FormatarValoresGrid()
        {
            // String formatacao = "{0:0.00}";
            String formatacao = "0.00##";
            // String formatacao = "{0:0,0.00}";

            try
            {
                dataGridViewCOMVINCULO.Columns["VLORIGINAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridViewCOMVINCULO.Columns["VLORIGINAL"].DefaultCellStyle.Format = formatacao;

                dataGridViewCOMVINCULO.Columns["VLLIQUIDO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridViewCOMVINCULO.Columns["VLLIQUIDO"].DefaultCellStyle.Format = formatacao;

                dataGridViewCOMVINCULO.Columns["VLBAIXADO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridViewCOMVINCULO.Columns["VLBAIXADO"].DefaultCellStyle.Format = formatacao;
            }
            catch { }

            try
            {
                dataGridViewSEMVINCULO.Columns["VLORIGINAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridViewSEMVINCULO.Columns["VLORIGINAL"].DefaultCellStyle.Format = formatacao;

                dataGridViewSEMVINCULO.Columns["VLLIQUIDO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridViewSEMVINCULO.Columns["VLLIQUIDO"].DefaultCellStyle.Format = formatacao;

                dataGridViewSEMVINCULO.Columns["VLBAIXADO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridViewSEMVINCULO.Columns["VLBAIXADO"].DefaultCellStyle.Format = formatacao;
            }
            catch { }
        }

        private void AtualizaAsGrids()
        {
            dataGridViewCOMVINCULO.DataSource = null;
            dataGridViewCOMVINCULO.DataSource = listaComVinculo;
            dataGridViewSEMVINCULO.DataSource = null;
            dataGridViewSEMVINCULO.DataSource = listaSemVinculo;

            this.FormatarValoresGrid();

            this.RecalculaSaldo();
        }

        public void RecalculaSaldo()
        {
            decimal saldo = ( VLORIGINAL + VLJUROS + VLMULTA ) - VLDESCONTO;

            for (int i = 0; i < listaComVinculo.Count; i++)
            {
                saldo += (listaComVinculo[i].VLLIQUIDO * -1);
            }

            // SE A PAGAR
            if (TIPOPAGREC == 0)
            {
                // INVERTER O SINAL
                saldo = (saldo * -1);
            }

            toolStripTextBox1.Text = string.Format("{0:0,0.00}", saldo);
        }

        private void dataGridViewCOMVINCULO_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                listaSemVinculo.Add(listaComVinculo[e.RowIndex]);
                listaComVinculo.RemoveAt(e.RowIndex);
                this.AtualizaAsGrids();
            }
            catch { }            
        }

        private void dataGridViewSEMVINCULO_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                listaComVinculo.Add(listaSemVinculo[e.RowIndex]);
                listaSemVinculo.RemoveAt(e.RowIndex);
                this.AtualizaAsGrids();
            }
            catch { }
        }

        private void toolStripButtonVINCULARTUDO_Click(object sender, EventArgs e)
        {
            this.MoverTudo(listaSemVinculo, listaComVinculo);
        }

        private void toolStripButtonDESVINCULARTUDO_Click(object sender, EventArgs e)
        {
            this.MoverTudo(listaComVinculo, listaSemVinculo);
        }

        private void MoverTudo(List<FLancaVinculo> listaOrigem, List<FLancaVinculo> listaDestino)
        {
            while (listaOrigem.Count > 0)
            {
                listaDestino.Add(listaOrigem[0]);
                listaOrigem.RemoveAt(0);
            }

            this.AtualizaAsGrids();
        }

        public override Boolean Execute()
        {
            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();
            conn.BeginTransaction();

            try
            {
                // SE NÃO POSSUI CÓDIGO DE VÍNCULO
                if (CODRELLANCA == 0)
                {
                    // OBTÉM O PRÓXIMO
                    String consulta = "SELECT ISNULL(MAX(CODRELLANCA),0)+1 FROM FRELLANCA WHERE CODEMPRESA = ?";
                    CODRELLANCA = int.Parse(conn.ExecGetField(1, consulta, new Object[] { CODEMPRESA }).ToString());
                }

                // LIMPA TUDO
                String comando1 = "DELETE FRELLANCA WHERE CODEMPRESA = ? AND CODRELLANCA = ?";
                int temp1 = conn.ExecTransaction(comando1, new Object[] { CODEMPRESA, CODRELLANCA });

                // SE FOI CRIADO VINCULO
                if (listaComVinculo.Count > 0)
                {
                    // INSERE O PRINCIPAL
                    String comando2 = "INSERT INTO FRELLANCA (CODEMPRESA, CODRELLANCA, CODLANCA) VALUES (?, ?, ?)";
                    int temp2 = conn.ExecTransaction(comando2, new Object[] { CODEMPRESA, CODRELLANCA, CODLANCA });

                    // INSERE OS VINCULOS
                    for (int i = 0; i < listaComVinculo.Count; i++)
                    {
                        String comando3 = "INSERT INTO FRELLANCA (CODEMPRESA, CODRELLANCA, CODLANCA) VALUES (?, ?, ?)";
                        int temp3 = conn.ExecTransaction(comando3, new Object[] { CODEMPRESA, CODRELLANCA, listaComVinculo[i].CODLANCA });
                    }
                }

                conn.Commit();
                AppLib.Windows.FormMessageDefault.ShowInfo("Processo executado com sucesso.");
                this.Close();
            }
            catch (Exception ex)
            {
                conn.Rollback();
                AppLib.Windows.FormMessageDefault.ShowError("Erro ao efetivar o vinculo.\r\nDetalhe técnico: " + ex.Message);
                return false;
            }

            return true;
        }

    }

}
