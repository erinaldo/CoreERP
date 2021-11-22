using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PS.Lib;

namespace PS.Glb
{
    public partial class PSPartOperFatAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        private PS.Lib.Constantes ct = new PS.Lib.Constantes();
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private PS.Lib.Valida vl = new PS.Lib.Valida();

        private string CodTipOperOri;

        private int ContItensSelecionados = 0;

        public PSPartOperFatAppFrm()
        {
            InitializeComponent();
        }

        private void PSPartOperFatAppFrm_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.Faturado = true;
            Properties.Settings.Default.Save();

            if (this.psPartApp.Access == PS.Lib.AppAccess.View)
            {
                if (psPartApp.DataGrid != null)
                {
                    for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                    {
                        if (psPartApp.DataGrid.Rows[i].Selected)
                        {
                            CodTipOperOri = gb.RetornaDataFieldByCampo(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.Rows[i]), "CODTIPOPER").Valor.ToString();
                            break;
                        }
                    }
                }
            }

            if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
            {
                CodTipOperOri = gb.RetornaDataFieldByCampo(this.psPartApp.DataField, "CODTIPOPER").Valor.ToString();
            }

            CarregaGrid();
            QuantosItensSelecionados();
        }

        private void AlteraNomeColuna()
        {
            DataTable dt = new DataTable();
            dt = gb.NomeDosCampos("GTIPOPER");

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dataGridView1.Columns[i].Name == dt.Rows[j]["COLUNA"].ToString())
                    {
                        dataGridView1.Columns[i].HeaderText = dt.Rows[j]["DESCRICAO"].ToString();
                    }
                }
            }
        }

        private void CarregaGrid()
        {
            try
            {
                dataGridView1.DefaultCellStyle.SelectionBackColor = ct.corSelecaoGrid;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ct.corImparGrid;

                dataGridView1.DataSource = gb.RetornaOperacaoFaturamento(CodTipOperOri);

                AlteraNomeColuna();
            }
            catch (Exception ex)
            {
                PS.Lib.PSMessageBox.ShowError("Ocorreu um erro: " + ex.Message);
            }
        }

        private void QuantosItensSelecionados()
        {
            ContItensSelecionados = 0;

            if (this.psPartApp.Access == PS.Lib.AppAccess.View)
            {
                if (psPartApp.DataGrid != null)
                {
                    for (int i = 0; i < psPartApp.DataGrid.Rows.Count; i++)
                    {
                        if (psPartApp.DataGrid.Rows[i].Selected)
                        {
                            ContItensSelecionados = ContItensSelecionados + 1;
                        }
                    }
                }
            }

            if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
            {
                ContItensSelecionados = 1;
            }
        }

        private List<DataField> buscaGoper(List<string> codOper)
        {
            List<DataField> ListaDataField = new List<DataField>();
            for (int iCodOper = 0; iCodOper < codOper.Count; iCodOper++)
            {

            }
            return ListaDataField;
        }

        private bool verificaParametroFaturamentoParcial()
        {
            return Convert.ToBoolean(dbs.QueryValue(0, "SELECT PERMITEFATURAMENTOPARCIAL FROM GTIPOPER WHERE CODEMPRESA = ? AND CODTIPOPER = ?", new object[] { AppLib.Context.Empresa, psPartApp.DataGrid.SelectedRows[0].Cells["CODTIPOPER"].Value.ToString() }));
        }

        public Boolean ValidarEstoque()
        {
            if (psPartApp.DataGrid != null)
            {
                for (int iSelectedRow = 0; iSelectedRow < psPartApp.DataGrid.SelectedRows.Count; iSelectedRow++)
                {
                    String CONTROLASALDOESTQUE = AppLib.Context.poolConnection.Get("Start").ExecGetField("B", "SELECT CONTROLASALDOESTQUE FROM VPARAMETROS WHERE CODEMPRESA = ?", new Object[] { AppLib.Context.Empresa }).ToString();
                    
                    int CODOPER = Convert.ToInt32(psPartApp.DataGrid.SelectedRows[iSelectedRow].Cells["CODOPER"].Value);

                    System.Data.DataTable dtGOPER = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?", new Object[] { AppLib.Context.Empresa, CODOPER });                    
                    String CODTIPOPER_ORIGEM = dtGOPER.Rows[0]["CODTIPOPER"].ToString();
                    String CODTIPOPER_DESTINO = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CODTIPOPERFAT"].Value.ToString();

                    int CODFILIAL = Convert.ToInt32(dtGOPER.Rows[0]["CODFILIAL"]);

                    int CODFILIALENTREGA = 0;
                    if ( dtGOPER.Rows[0]["CODFILIALENTREGA"] != DBNull.Value )
                    {
                        CODFILIALENTREGA = Convert.ToInt32(dtGOPER.Rows[0]["CODFILIALENTREGA"]);
                    }

                    String CODLOCAL = String.Empty;
                    if (dtGOPER.Rows[0]["CODLOCAL"] != DBNull.Value)
                    {
                        CODLOCAL = dtGOPER.Rows[0]["CODLOCAL"].ToString();
                    }

                    String CODLOCALENTREGA = String.Empty;
                    if (dtGOPER.Rows[0]["CODLOCALENTREGA"] != DBNull.Value)
                    {
                        CODLOCALENTREGA = dtGOPER.Rows[0]["CODLOCALENTREGA"].ToString();
                    }
                    
                    System.Data.DataTable dtGOPERITEM = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?", new Object[] { AppLib.Context.Empresa, CODOPER });

                    String mensagem = "";

                    for (int iGOPERITEM = 0; iGOPERITEM < dtGOPERITEM.Rows.Count; iGOPERITEM++)
                    {
                        String CODPRODUTO = dtGOPERITEM.Rows[iGOPERITEM]["CODPRODUTO"].ToString();
                        decimal QUANTIDADE = Convert.ToDecimal(dtGOPERITEM.Rows[iGOPERITEM]["QUANTIDADE"]);

                        PS.Glb.PSPartOperacaoItemData data = new PSPartOperacaoItemData();

                        if (CODLOCAL != String.Empty)
                        {
                            if (data.TemParametroDiminuiEstoque(AppLib.Context.Empresa, CODTIPOPER_DESTINO, "O"))
                            {
                                if ( ! data.TemSaldoParaItem(AppLib.Context.Empresa, CODFILIAL, CODLOCAL, CODPRODUTO, QUANTIDADE))
                                {
                                    mensagem += "Não existe saldo no estoque " + CODLOCAL + " para atender " + QUANTIDADE + " itens do produto " + CODPRODUTO + ";";
                                    mensagem += "\r\n";
                                }
                            }
                        }

                        if (CODLOCALENTREGA != String.Empty)
                        {
                            if (data.TemParametroDiminuiEstoque(AppLib.Context.Empresa, CODTIPOPER_DESTINO, "D"))
                            {
                                if ( ! data.TemSaldoParaItem(AppLib.Context.Empresa, CODFILIALENTREGA, CODLOCALENTREGA, CODPRODUTO, QUANTIDADE))
                                {
                                    mensagem += "Não existe saldo no estoque " + CODLOCALENTREGA + " para atender " + QUANTIDADE + " itens do produto " + CODPRODUTO + ";";
                                    mensagem += "\r\n";
                                }
                            }
                        }
                    }

                    if ( ! mensagem.Equals(""))
                    {
                        if (CONTROLASALDOESTQUE.ToUpper().Equals("A"))
                        {
                            AppLib.Windows.FormMessageDefault.ShowInfo(mensagem);
                            return true;
                        }

                        if (CONTROLASALDOESTQUE.ToUpper().Equals("B"))
                        {
                            AppLib.Windows.FormMessageDefault.ShowInfo(mensagem);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public override Boolean Execute()
        {
            if ( ! ValidarEstoque() )
            {
                return false;
            }

            Boolean AbrirTelaAposFaturamento = false;
            List<string> ListaCodOper = new List<string>();

            if (PS.Lib.PSMessageBox.ShowQuestion("Gostaria de visualizar a operação após o processo de faturamento do(s) registro(s) selecionado(s) ?") == System.Windows.Forms.DialogResult.Yes)
            {
                AbrirTelaAposFaturamento = true;
            }
            //Verfica o parametro 
            if (verificaParametroFaturamentoParcial().Equals(false))
            {
                for (int i = 0; i < psPartApp.DataGrid.SelectedRows.Count; i++)
                {
                    Faturar(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.SelectedRows[i]), AbrirTelaAposFaturamento);
                }
                PS.Lib.PSMessageBox.ShowInfo("Operação realizada com sucesso.");
            }
            else
            {
                try
                {
                    if (this.psPartApp.Access == PS.Lib.AppAccess.View)
                    {
                        if (psPartApp.DataGrid != null)
                        {
                            //Abrir tela para escolha se é faturamento parcial ou não

                            ERP.Comercial.FormSelecaoFaturamento frmSelecao = new ERP.Comercial.FormSelecaoFaturamento();
                            frmSelecao.ShowDialog();
                            //Alterar com o resultado de retorno da tela
                            if (frmSelecao.cancelado == true)
                            {
                                return false;
                            }
                            if (frmSelecao.rbFaturamentoParcial.Checked == true)
                            {
                                //Verificar se todos os pedidos selecionados são do mesmo cliente.
                                for (int i = 0; i < psPartApp.DataGrid.SelectedRows.Count; i++)
                                {
                                    if (psPartApp.DataGrid.SelectedRows.Count > 1)
                                    {
                                        if (psPartApp.DataGrid.SelectedRows[0].Cells["CODCLIFOR"].Value.ToString() != psPartApp.DataGrid.SelectedRows[i].Cells["CODCLIFOR"].Value.ToString())
                                        {
                                            MessageBox.Show("Faturamento parcial não permitido para clientes diferentes.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return true;
                                        }
                                    }
                                    //Verifica a situação do cliente
                                    if (new Class.FaturaParcial().VerificaSituacao(AppLib.Context.Empresa, psPartApp.DataGrid.SelectedRows[i].Cells["CODOPER"].Value.ToString()).Equals(false))
                                    {
                                        throw new Exception("Operação [ " + psPartApp.DataGrid.SelectedRows[i].Cells["CODOPER"].Value.ToString() + " ] não pode ser faturada pois não está em aberto.");
                                    }
                                    ListaCodOper.Add(psPartApp.DataGrid.SelectedRows[i].Cells["CODOPER"].Value.ToString());
                                }
                                //Atribui os valores da operação ao objeto operacao
                                Class.Goper operacao = new Class.Goper().getGoper(psPartApp.DataGrid.SelectedRows[0].Cells["CODOPER"].Value.ToString(), AppLib.Context.poolConnection.Get("Start"));
                                //Atribui todos os itens dos pedidos selecionados ao objeto itens 
                                List<Class.GoperItem> itens = new Class.GoperItem().getGoperItemFaturamento(ListaCodOper, AppLib.Context.poolConnection.Get("Start"));

                                //Abre o form
                                ERP.Comercial.FormFaturaParcial frm = new ERP.Comercial.FormFaturaParcial(operacao, itens);
                                frm.codTipOperDestino = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CODTIPOPERFAT"].Value.ToString();
                                frm.ShowDialog();
                                if (frm.cancelar == true)
                                {
                                    return false;
                                }
                                if (AbrirTelaAposFaturamento)
                                {
                                    PSPartOperacao _psPartOperacao = new PSPartOperacao();
                                    _psPartOperacao.DefaultFilter.Add(new PS.Lib.PSFilter("CODFILIAL", frm.operacao.CODFILIAL));
                                    _psPartOperacao.DefaultFilter.Add(new PS.Lib.PSFilter("CODTIPOPER", frm.codTipOperDestino));
                                    _psPartOperacao.AllowSave = true;
                                    _psPartOperacao.AllowInsert = false;
                                    _psPartOperacao.AllowDelete = false;
                                    this.Cursor = Cursors.Default;

                                    _psPartOperacao.ExecuteWithParams(frm.operacao.CODEMPRESA, frm.operacao.CODOPER);
                                }

                                this.Cursor = Cursors.Default;
                                PS.Lib.PSMessageBox.ShowInfo("Operação realizada com sucesso.");
                            }
                            else if (frmSelecao.rbFaturamento.Checked == true)
                            {
                                for (int i = 0; i < psPartApp.DataGrid.SelectedRows.Count; i++)
                                {
                                    Faturar(gb.RetornaDataFieldByDataGridViewRow(psPartApp.DataGrid.SelectedRows[i]), AbrirTelaAposFaturamento);
                                }
                                PS.Lib.PSMessageBox.ShowInfo("Operação realizada com sucesso.");
                            }
                            else
                            {
                                PS.Lib.PSMessageBox.ShowInfo("Nenhuma opção Selecionada");
                            }
                        }
                    }

                    //if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
                    //{
                    //    Faturar(this.psPartApp.DataField, AbrirTelaAposFaturamento);
                    //}

                    //para atualizar a visão depois da execução do aplicativo
                    this.psPartApp.Refresh = true;
                }
                catch (Exception ex)
                {
                    PS.Lib.PSMessageBox.ShowError(ex.Message);
                    return false;
                }
            }
            return true;
        }

        private void Faturar(List<DataField> objArr, Boolean AbrirTelaAposFaturamento)
        {

            PSPartOperacaoData psPartOperacaoData = new PSPartOperacaoData();
            psPartOperacaoData._tablename = this.psPartApp.TableName;
            psPartOperacaoData._keys = this.psPartApp.Keys;

            string codTipOper = string.Empty;

            // PRÓXIMA ETAPA : Tipo de Operação
            if (dataGridView1.Rows.Count > 0)
            {
                codTipOper = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["CODTIPOPERFAT"].Value.ToString();

                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    //Aqui que chama o metodo pra realizar o faturamento da operação
                    List<PS.Lib.DataField> objArrDDL = psPartOperacaoData.Faturar(objArr, codTipOper);



                    if (AbrirTelaAposFaturamento)
                    {
                        PS.Lib.DataField dfCODEMPRESA = gb.RetornaDataFieldByCampo(objArrDDL, "CODEMPRESA");
                        PS.Lib.DataField dfCODFILIAL = gb.RetornaDataFieldByCampo(objArrDDL, "CODFILIAL");
                        PS.Lib.DataField dfCODOPER = gb.RetornaDataFieldByCampo(objArrDDL, "CODOPER");

                        PSPartOperacao _psPartOperacao = new PSPartOperacao();
                        _psPartOperacao.DefaultFilter.Add(new PS.Lib.PSFilter("CODFILIAL", dfCODFILIAL.Valor));
                        _psPartOperacao.DefaultFilter.Add(new PS.Lib.PSFilter("CODTIPOPER", codTipOper));
                        _psPartOperacao.AllowSave = true;
                        _psPartOperacao.AllowInsert = false;
                        _psPartOperacao.AllowDelete = false;
                        this.Cursor = Cursors.Default;

                        _psPartOperacao.ExecuteWithParams(dfCODEMPRESA.Valor, dfCODOPER.Valor);
                    }

                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
