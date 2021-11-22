using ITGProducao.Controles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PS.Glb.Class;

namespace PS.Glb.New.Filtro
{
    public partial class frmFiltroApontamento : Form
    {
        public string condicao = string.Empty;
        public Form pai = null;
        public bool aberto = false;
        public string codMenu = string.Empty;

        //Variaveis para NewLookup
        private NewLookup lookup;

        public frmFiltroApontamento()
        {
            InitializeComponent();
        }

        public frmFiltroApontamento(ref NewLookup lookup)
        {
            InitializeComponent();

            this.lookup = lookup;
        }

        private void frmFiltroOperacao_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabPage2);

            CarregaLookUpStatus();
            SetConsultaLookUp();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            getCondicao();
            this.Dispose();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
            condicao = null;
            GC.Collect();
        }

        #region Evento - CheckedChanged

        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTodos.Checked == true)
            {
                ConfigurarVisibilidadeFiltros(rbTodos.Text);
            }
        }

        private void rbIdApontamento_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIdApontamento.Checked)
            {
                ConfigurarVisibilidadeFiltros(rbIdApontamento.Text);
            }
        }

        private void rbPeriodo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPeriodo.Checked)
            {
                ConfigurarVisibilidadeFiltros(rbPeriodo.Text);
            }
        }

        private void rbHoje_CheckedChanged(object sender, EventArgs e)
        {
            ConfigurarVisibilidadeFiltros(rbHoje.Text);
        }

        private void rbOntem_CheckedChanged(object sender, EventArgs e)
        {
            ConfigurarVisibilidadeFiltros(rbOntem.Text);
        }

        private void rbStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStatus.Checked)
            {
                ConfigurarVisibilidadeFiltros(rbStatus.Text);
            }
        }

        private void rbAnalistaUsuario_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAnalistaUsuario.Checked)
            {
                ConfigurarVisibilidadeFiltros(rbAnalistaUsuario.Text);
            }
        }

        private void rbQuinzenal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbQuinzenal.Checked)
            {
                ConfigurarVisibilidadeFiltros(rbQuinzenal.Text);
            }         
        }

        private void rbNaoIntegrado_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNaoIntegrado.Checked)
            {
                ConfigurarVisibilidadeFiltros(rbNaoIntegrado.Text);
            }
        }

        #endregion

        #region Métodos

        private void getCondicao()
        {
            try
            {
                if (rbTodos.Checked)
                {
                    condicao = "WHERE AP.CODEMPRESA = '" + AppLib.Context.Empresa + "' and AP.CODFILIAL = '" + AppLib.Context.Filial + "'";
                }

                else if (rbIdApontamento.Checked)
                {
                    condicao = "WHERE AP.IDAPONTAMENTO = '" + tbValor.EditValue.ToString() + "'";
                }

                else if (rbPeriodo.Checked)
                {
                    condicao = "WHERE AP.CODEMPRESA = '" + AppLib.Context.Empresa + "' and AP.CODFILIAL = '" + AppLib.Context.Filial + "' AND AP.DATA BETWEEN CONVERT(DATETIME, '" + dteInicial.DateTime + "', 103) AND CONVERT(DATETIME, '" + dteFinal.DateTime + "', 103)";
                }

                else if (rbHoje.Checked)
                {
                    condicao = "WHERE DAY(AP.DATA) = DAY(GETDATE()) AND MONTH(AP.DATA) = MONTH(GETDATE()) AND YEAR(AP.DATA) = YEAR(GETDATE())";
                }

                else if (rbOntem.Checked)
                {
                    condicao = @"WHERE DAY(AP.DATA) = DAY(DATEADD(DD,     
                                                CASE WHEN DATEPART(DW, GETDATE()) = 2 THEN - 3
                                                WHEN DATEPART(DW, GETDATE()) BETWEEN 3 AND 6 THEN - 1 ELSE 0 END, GETDATE())) AND MONTH(AP.DATA) = MONTH(GETDATE()) AND YEAR(AP.DATA) = YEAR(GETDATE())";
                }

                else if (rbStatus.Checked)
                {
                    condicao = "WHERE AP.IDSTATUSAPONTAMENTO = " + lpStatus.EditValue + "";
                }

                else if (rbAnalistaUsuario.Checked)
                {
                    condicao = "WHERE AP.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND AP.CODFILIAL = '" + AppLib.Context.Filial + "' AND AP.CODUSUARIO LIKE '%" + clAnalistaUsuario.textBoxCODIGO.Text + "%'";
                }

                else if (rbQuinzenal.Checked)
                {
                    if (rbPrimeiraQuinzena.Checked)
                    {
                        condicao = @"WHERE AP.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND MONTH(AP.DATA) = MONTH(GETDATE()) AND DAY(AP.DATA) BETWEEN 1 AND 15 GROUP BY AP.CODEMPRESA, AP.CODFILIAL, IDAPONTAMENTO, AAPONTAMENTO.IDPROJETO, AP.IDUNIDADE, AUNIDADE.NOME, APROJETO.DESCRICAO, CODUSUARIO, DATA, AP.INICIO, AP.TERMINO, AP.ABONO, VALORAD, MOTVALAD, INLOCO, REEMBOLSO, DATAENVIO, DATARETORNO, IDSTATUSAPONTAMENTO, MOTIVOREPROVACAO, CODOPERDEMANDA, AP.CODEMPRESA, CODOPERREEMBOLSOC, CODOPERREEMBOLSOA, PENALIDADE, DATAPENALIDADE, DATAPROCESSAMENTO, AP.DATADIGITACAO, AP.TIPOFATURAMENTO, DATEADD(DAY, CEILING(DATEDIFF(DAY, 4, AP.DATA) / 14.0) * 14, 4), AP.TOTALHORAS, AUNIDADE.IDUNIDADE, AUNIDADE.NOME";
                    }
                    else
                    {
                        condicao = @"WHERE AP.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND MONTH(AP.DATA) = MONTH(GETDATE()) AND DAY(AP.DATA) BETWEEN 16 AND DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1, GETDATE()),0))) GROUP BY AP.CODEMPRESA, AP.CODFILIAL, IDAPONTAMENTO, AAPONTAMENTO.IDPROJETO, AP.IDUNIDADE, AUNIDADE.NOME, APROJETO.DESCRICAO, CODUSUARIO, DATA, AP.INICIO, AP.TERMINO, AP.ABONO, VALORAD, MOTVALAD, INLOCO, REEMBOLSO, DATAENVIO, DATARETORNO, IDSTATUSAPONTAMENTO, MOTIVOREPROVACAO, CODOPERDEMANDA, AP.CODEMPRESA, CODOPERREEMBOLSOC, CODOPERREEMBOLSOA, PENALIDADE, DATAPENALIDADE, DATAPROCESSAMENTO, AP.DATADIGITACAO, AP.TIPOFATURAMENTO, DATEADD(DAY, CEILING(DATEDIFF(DAY, 4, AP.DATA) / 14.0) * 14, 4), AP.TOTALHORAS, AUNIDADE.IDUNIDADE, AUNIDADE.NOME";
                    }                
                }

                else if (rbNaoIntegrado.Checked)
                {
                    condicao = "WHERE AP.CODEMPRESA = '" + AppLib.Context.Empresa + "' AND AP.IDSTATUSAPONTAMENTO <> 3";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não foi possível carregar as condições do filtro.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetConsultaLookUp()
        {
            clAnalistaUsuario.ColunaTabela = String.Format(@"(SELECT 
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
							                                                                            CODEMPRESA = " + AppLib.Context.Empresa + " AND COORDCLIENTE = 1 AND AUNIDADEREEMBOLSO.IDUNIDADE NOT IN (49)) AND CODUSUARIO NOT IN ('MASTER')) Y");
        }

        private DataTable CriaTabelaStatus()
        {
            DataTable dtStatus = new DataTable();

            dtStatus.Columns.Add("Código", typeof(int));
            dtStatus.Columns.Add("Descrição", typeof(string));

            dtStatus.Rows.Add(0, "Em Digitação");
            dtStatus.Rows.Add(1, "Concluído");
            dtStatus.Rows.Add(2, "Aprovado");
            dtStatus.Rows.Add(3, "Integrado");

            return dtStatus;
        }

        private void CarregaLookUpStatus()
        {
            DataTable dt = CriaTabelaStatus();

            lpStatus.Properties.DataSource = dt;
            lpStatus.Properties.DisplayMember = dt.Columns["Descrição"].ToString();
            lpStatus.Properties.ValueMember = dt.Columns["Código"].ToString();
            lpStatus.Properties.NullText = "Selecione...";

            lpStatus.CalcBestSize();
        }

        private void ConfigurarVisibilidadeFiltros(string filtroSelecionado)
        {
            switch (filtroSelecionado)
            {
                case "Todos":

                    // Analista/Usuário
                    lblAnalistaUsuario.Visible = false;
                    clAnalistaUsuario.Visible = false;

                    // Data Inicial, Data Final
                    lblInicial.Visible = false;
                    dteInicial.Visible = false;
                    lblFinal.Visible = false;
                    dteFinal.Visible = false;

                    // Identificador
                    lbValor.Visible = false;
                    tbValor.Visible = false;

                    // Status
                    lblStatus.Visible = false;
                    lpStatus.Visible = false;

                    // Quinzenal 
                    gbPeriodo.Visible = false;

                    break;

                case "Identificador":

                    ConfiguraLocalizacaoValorFiltro(filtroSelecionado);

                    // Identificador
                    lbValor.Visible = true;
                    tbValor.Visible = true;

                    // Status
                    lblStatus.Visible = false;
                    lpStatus.Visible = false;

                    // Analista/Usuário
                    lblAnalistaUsuario.Visible = false;
                    clAnalistaUsuario.Visible = false;

                    // Data Inicial, Data Final
                    lblInicial.Visible = false;
                    dteInicial.Visible = false;
                    lblFinal.Visible = false;
                    dteFinal.Visible = false;

                    // Quinzenal 
                    gbPeriodo.Visible = false;

                    break;

                case "Período":

                    ConfiguraLocalizacaoValorFiltro(filtroSelecionado);

                    // Data Inicial, Data Final
                    lblInicial.Visible = true;
                    dteInicial.Visible = true;
                    lblFinal.Visible = true;
                    dteFinal.Visible = true;

                    // Status
                    lblStatus.Visible = false;
                    lpStatus.Visible = false;

                    // Analista/Usuário
                    lblAnalistaUsuario.Visible = false;
                    clAnalistaUsuario.Visible = false;

                    // Identificador
                    lbValor.Visible = false;
                    tbValor.Visible = false;

                    // Quinzenal 
                    gbPeriodo.Visible = false;

                    break;

                case "Hoje":

                    // Analista/Usuário
                    lblAnalistaUsuario.Visible = false;
                    clAnalistaUsuario.Visible = false;

                    // Data Inicial, Data Final
                    lblInicial.Visible = false;
                    dteInicial.Visible = false;
                    lblFinal.Visible = false;
                    dteFinal.Visible = false;

                    // Identificador
                    lbValor.Visible = false;
                    tbValor.Visible = false;

                    // Status
                    lblStatus.Visible = false;
                    lpStatus.Visible = false;

                    // Quinzenal 
                    gbPeriodo.Visible = false;

                    break;

                case "Ontem":

                    // Analista/Usuário
                    lblAnalistaUsuario.Visible = false;
                    clAnalistaUsuario.Visible = false;

                    // Data Inicial, Data Final
                    lblInicial.Visible = false;
                    dteInicial.Visible = false;
                    lblFinal.Visible = false;
                    dteFinal.Visible = false;

                    // Identificador
                    lbValor.Visible = false;
                    tbValor.Visible = false;

                    // Status
                    lblStatus.Visible = false;
                    lpStatus.Visible = false;

                    // Quinzenal 
                    gbPeriodo.Visible = false;

                    break;

                case "Status":

                    ConfiguraLocalizacaoValorFiltro(filtroSelecionado);

                    // Status
                    lblStatus.Visible = true;
                    lpStatus.Visible = true;

                    // Analista/Usuário
                    lblAnalistaUsuario.Visible = false;
                    clAnalistaUsuario.Visible = false;

                    // Data Inicial, Data Final
                    lblInicial.Visible = false;
                    dteInicial.Visible = false;
                    lblFinal.Visible = false;
                    dteFinal.Visible = false;

                    // Identificador
                    lbValor.Visible = false;
                    tbValor.Visible = false;

                    // Quinzenal 
                    gbPeriodo.Visible = false;

                    break;

                case "Analista/Usuário":

                    ConfiguraLocalizacaoValorFiltro(filtroSelecionado);

                    // Analista/Usuário
                    lblAnalistaUsuario.Visible = true;
                    clAnalistaUsuario.Visible = true;

                    // Data Inicial, Data Final
                    lblInicial.Visible = false;
                    dteInicial.Visible = false;
                    lblFinal.Visible = false;
                    dteFinal.Visible = false;

                    // Identificador
                    lbValor.Visible = false;
                    tbValor.Visible = false;

                    // Status
                    lblStatus.Visible = false;
                    lpStatus.Visible = false;

                    // Quinzenal 
                    gbPeriodo.Visible = false;

                    break;

                case "Quinzenal":

                    ConfiguraLocalizacaoValorFiltro(filtroSelecionado);

                    // Quinzenal 
                    gbPeriodo.Visible = true;

                    // Status
                    lblStatus.Visible = false;
                    lpStatus.Visible = false;

                    // Analista/Usuário
                    lblAnalistaUsuario.Visible = false;
                    clAnalistaUsuario.Visible = false;

                    // Data Inicial, Data Final
                    lblInicial.Visible = false;
                    dteInicial.Visible = false;
                    lblFinal.Visible = false;
                    dteFinal.Visible = false;

                    // Identificador
                    lbValor.Visible = false;
                    tbValor.Visible = false;     

                    break;

                case "Não Integrado":

                    // Analista/Usuário
                    lblAnalistaUsuario.Visible = false;
                    clAnalistaUsuario.Visible = false;

                    // Data Inicial, Data Final
                    lblInicial.Visible = false;
                    dteInicial.Visible = false;
                    lblFinal.Visible = false;
                    dteFinal.Visible = false;

                    // Identificador
                    lbValor.Visible = false;
                    tbValor.Visible = false;

                    // Status
                    lblStatus.Visible = false;
                    lpStatus.Visible = false;

                    // Quinzenal 
                    gbPeriodo.Visible = false;

                    break;

                default:
                    break;
            }
        }

        private void ConfiguraLocalizacaoValorFiltro(string filtroSelecionado)
        {
            switch (filtroSelecionado)
            {
                case "Identificador":

                    gbValores.Controls.Add(lbValor);
                    gbValores.Controls.Add(tbValor);

                    lbValor.Location = new Point(96, 31);
                    tbValor.Location = new Point(99, 47);

                    break;

                case "Período":

                    gbValores.Controls.Add(lblInicial);
                    gbValores.Controls.Add(dteInicial);
                    gbValores.Controls.Add(lblFinal);
                    gbValores.Controls.Add(dteFinal);

                    lblInicial.Location = new Point(35, 27);
                    dteInicial.Location = new Point(38, 43);

                    lblFinal.Location = new Point(154, 27);
                    dteFinal.Location = new Point(157, 43);

                    break;

                case "Status":

                    gbValores.Controls.Add(lblStatus);
                    gbValores.Controls.Add(lpStatus);

                    lblStatus.Location = new Point(75, 31);
                    lpStatus.Location = new Point(78, 47);

                    break;

                case "Analista/Usuário":

                    gbValores.Controls.Add(lblAnalistaUsuario);
                    gbValores.Controls.Add(clAnalistaUsuario);

                    lblAnalistaUsuario.Location = new Point(6, 27);
                    clAnalistaUsuario.Location = new Point(6, 43);

                    break;

                case "Quinzenal":

                    gbValores.Controls.Add(gbPeriodo);

                    gbPeriodo.Location = new Point(17,19);

                    break;

                default:
                    break;
            }
        }

        #endregion
    }
}
