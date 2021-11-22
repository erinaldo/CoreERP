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
    public partial class frmRecalculaSaldo : Form
    {
        public frmRecalculaSaldo()
        {
            InitializeComponent();
        }

        private void frmRecalculaSaldo_Load(object sender, EventArgs e)
        {
            string campo = AppLib.Context.poolConnection.Get("Start").ExecGetField("CODPRODUTO", @"SELECT BUSCAPRODUTOPOR FROM VPARAMETROS WHERE CODEMPRESA = ? ", new object[] { AppLib.Context.Empresa }).ToString();
            switch (campo)
            {
                case "0":
                    lookupprodutoinicial.CampoCodigo_Igual_a = NewLookup.CampoCodigoIguala.CampoCodigoBD;
                    lookupprodutofinal.CampoCodigo_Igual_a = NewLookup.CampoCodigoIguala.CampoCodigoBD;
                    break;
                case "1":
                    lookupprodutoinicial.CampoCodigo_Igual_a = NewLookup.CampoCodigoIguala.CampoCodigoInterno;
                    lookupprodutofinal.CampoCodigo_Igual_a = NewLookup.CampoCodigoIguala.CampoCodigoInterno;
                    break;
                default:
                    lookupprodutoinicial.CampoCodigo_Igual_a = NewLookup.CampoCodigoIguala.CampoCodigoBD;
                    lookupprodutofinal.CampoCodigo_Igual_a = NewLookup.CampoCodigoIguala.CampoCodigoBD;
                    break;
            }

            DateTime FechamentoEstoque = Convert.ToDateTime(AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, "SELECT DATAFECHAMENTOESTOQUE FROM VPARAMETROS WHERE CODEMPRESA = ?", new object[] { AppLib.Context.Empresa }));
            tbFechamentoEstoque.Text = FechamentoEstoque.ToShortDateString();
        }

        private bool validacao()
        {
            bool verifica = true;

            lookupfilial.mensagemErrorProvider = "";
            lookupprodutoinicial.mensagemErrorProvider = "";
            lookupprodutofinal.mensagemErrorProvider = "";

            if (string.IsNullOrEmpty(lookupfilial.ValorCodigoInterno))
            {
                lookupfilial.mensagemErrorProvider = "Favor selecionar a filial";
                verifica = false;
            }

            if (!string.IsNullOrEmpty(lookupprodutoinicial.ValorCodigoInterno) || !string.IsNullOrEmpty(lookupprodutofinal.ValorCodigoInterno)){

                if (string.IsNullOrEmpty(lookupprodutoinicial.ValorCodigoInterno))
                {
                    lookupprodutoinicial.mensagemErrorProvider = "Favor selecionar o produto inicial";
                    verifica = false;
                }

                if (string.IsNullOrEmpty(lookupprodutofinal.ValorCodigoInterno))
                {
                    lookupprodutofinal.mensagemErrorProvider = "Favor selecionar o produto final";
                    verifica = false;
                }

                if (!string.IsNullOrEmpty(lookupprodutoinicial.ValorCodigoInterno) && !string.IsNullOrEmpty(lookupprodutofinal.ValorCodigoInterno))
                { 
                    List<string> lista = new List<string>();

                    lista.Add(lookupprodutoinicial.ValorCodigoInterno);
                    lista.Add(lookupprodutofinal.ValorCodigoInterno);
                    lista.Sort();

                    if (lista[0].ToString() != lookupprodutoinicial.ValorCodigoInterno.ToString())
                    {
                        lookupprodutofinal.mensagemErrorProvider = "O produto final deve ser maior que o produto inicial";
                        verifica = false;
                    }
                }
            }

                return verifica;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (validacao() == true)
                {
                    if (MessageBox.Show("Deseja realmente iniciar o recálculo de saldo deste(s) produto(s)?", "Informação do Sistema.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        splashScreenManager1.ShowWaitForm();

                        splashScreenManager1.SetWaitFormCaption("Recalculando saldo...");

                        try
                        {
                            // Instancia o objeto de conexão
                            AppLib.Data.Connection conn = AppLib.Context.poolConnection.Get("Start").Clone();

                            // Deleta regitros da tabela VFICHAESTOQUE_ANTERIOR
                            conn.ExecTransaction("DELETE FROM VFICHAESTOQUE_ANTERIOR", new object[] { });

                            // Inseri regitros na tabela VFICHAESTOQUE_ANTERIOR com base no SELECT da tabela VFICHAESTOQUE
                            conn.ExecTransaction(@"INSERT INTO VFICHAESTOQUE_ANTERIOR 

SELECT VFICHAESTOQUE.*
FROM VFICHAESTOQUE (NOLOCK)
INNER JOIN VPARAMETROS ON VPARAMETROS.CODEMPRESA = VFICHAESTOQUE.CODEMPRESA
WHERE VFICHAESTOQUE.DATAENTSAI > VPARAMETROS.DATAFECHAMENTOESTOQUE
AND VFICHAESTOQUE.CODEMPRESA = ?
AND VFICHAESTOQUE.CODFILIAL = ?
AND VFICHAESTOQUE.CODPRODUTO >= ? 	  
AND VFICHAESTOQUE.CODPRODUTO <= ?", new object[] { AppLib.Context.Empresa, lookupfilial.txtcodigo.Text, lookupprodutoinicial.ValorCodigoInterno, lookupprodutofinal.ValorCodigoInterno });

                            // Deleta registros da tebala VFICHAESTOQUE
                            conn.ExecTransaction(@"DELETE FROM VFICHAESTOQUE
WHERE VFICHAESTOQUE.DATAENTSAI > (SELECT VPARAMETROS.DATAFECHAMENTOESTOQUE FROM VPARAMETROS WHERE VPARAMETROS.CODEMPRESA = VFICHAESTOQUE.CODEMPRESA)
AND VFICHAESTOQUE.CODEMPRESA = ?
AND VFICHAESTOQUE.CODFILIAL = ? 
AND VFICHAESTOQUE.CODPRODUTO >= ? 
AND VFICHAESTOQUE.CODPRODUTO <= ?", new object[] { AppLib.Context.Empresa, lookupfilial.txtcodigo.Text, lookupprodutoinicial.ValorCodigoInterno, lookupprodutofinal.ValorCodigoInterno });

                            /// Monta o Datatable 
                            DataTable dtRecalSaldo = conn.ExecQuery(@"SELECT 
 GOPERITEM.CODEMPRESA
,GOPERITEM.CODOPER
,GOPERITEM.NSEQITEM

,GOPER.DATAENTSAI
,GOPERITEM.CODPRODUTO

FROM
GOPERITEM (NOLOCK)
INNER JOIN GOPER (NOLOCK) ON GOPER.CODEMPRESA = GOPERITEM.CODEMPRESA AND GOPER.CODOPER = GOPERITEM.CODOPER
INNER JOIN GTIPOPER (NOLOCK) ON GTIPOPER.CODEMPRESA = GOPER.CODEMPRESA AND GTIPOPER.CODTIPOPER = GOPER.CODTIPOPER
INNER JOIN VPARAMETROS ON VPARAMETROS.CODEMPRESA = GOPERITEM.CODEMPRESA

WHERE
GOPERITEM.CODEMPRESA = ?
AND GOPER.CODFILIAL = ? 
AND GTIPOPER.OPERESTOQUE <> 'N'
AND GOPERITEM.CODPRODUTO >= ?
AND GOPERITEM.CODPRODUTO <= ?
AND GOPER.DATAENTSAI > VPARAMETROS.DATAFECHAMENTOESTOQUE
AND GOPER.CODSTATUS <> 2

ORDER BY 
 GOPERITEM.CODEMPRESA
,GOPERITEM.CODPRODUTO
,GOPER.DATAENTSAI
,GOPERITEM.CODOPER
,GOPERITEM.NSEQITEM", new object[] { AppLib.Context.Empresa, lookupfilial.txtcodigo.Text, lookupprodutoinicial.ValorCodigoInterno, lookupprodutofinal.ValorCodigoInterno });

                            for (int i = 0; i < dtRecalSaldo.Rows.Count; i++)
                            {
                                PSPartLocalEstoqueSaldoData psPartLocalEstoqueSaldoData = new PSPartLocalEstoqueSaldoData();
                                PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque Estoque;
                                Estoque = PSPartLocalEstoqueSaldoData.AcaoMovimentouEstoque.ItemInclusao;

                                psPartLocalEstoqueSaldoData.MovimentaEstoque2(Estoque, AppLib.Context.Empresa, Convert.ToInt32(dtRecalSaldo.Rows[i]["CODOPER"]), Convert.ToInt32(dtRecalSaldo.Rows[i]["NSEQITEM"]));                             
                            }
                            splashScreenManager1.CloseWaitForm();
                            MessageBox.Show("Concluído com sucesso!", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {

                            throw new Exception(ex.Message, ex.InnerException);
                        }

                        // João Pedro Luchiari - Descomentar caso preciso
                        //System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(AppLib.Context.poolConnection.Get("Start").ConnectionString);
                        //try
                        //{
                        //    System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("recalcula_estoque2", conn);
                        //    command.CommandType = CommandType.StoredProcedure;
                        //    command.CommandTimeout = 6000;

                        //    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EMPRESA", SqlDbType.Int)).Value = AppLib.Context.Empresa;
                        //    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FILIAL", SqlDbType.Int)).Value = lookupfilial.ValorCodigoInterno;
                        //    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRODUTOINI", SqlDbType.VarChar)).Value = lookupprodutoinicial.ValorCodigoInterno;
                        //    command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRODUTOFIM", SqlDbType.VarChar)).Value = lookupprodutofinal.ValorCodigoInterno;
                        //    conn.Open();

                        //    command.ExecuteNonQuery();
                        //}
                        //catch (Exception ex)
                        //{
                        //    throw new Exception(ex.Message, ex.InnerException);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);  
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
