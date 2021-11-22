using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb.ERP.Financeiro
{
    public partial class FormCobrancaVisao : AppLib.Windows.FormVisao
    {
        private static FormCobrancaVisao _instance = null;

        public static FormCobrancaVisao GetInstance()
        {
            if (_instance == null)
                _instance = new FormCobrancaVisao();

            return _instance;
        }

        private void FormCobrancaVisao_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
        }

        public FormCobrancaVisao()
        {
            InitializeComponent();
        }

        private void FormCobrancaVisao_Load(object sender, EventArgs e)
        {
            //this.grid1.GetProcessos().Add("Gerar Nosso Número", null, GerarNossoNumero);
            //this.grid1.GetProcessos().Add("Gerar Código de Barras", null, GerarCodigoBarras);
            this.grid1.GetProcessos().Add("Gerar Remessa", null, GerarArquivoRemessa);
            this.grid1.GetProcessos().Add("Imprimir Boleto", null, ImprimirBoleto);
            this.grid1.GetProcessos().Add(new ToolStripSeparator());
            this.grid1.GetProcessos().Add("Cancelar Remessa", null, CancelarRemessa);
        }

        private void grid1_SetParametros(object sender, EventArgs e)
        {
            grid1.Parametros = new Object[] { AppLib.Context.Empresa };
        }

        private void grid1_Novo(object sender, EventArgs e)
        {
            FormCobrancaCadastro f = new FormCobrancaCadastro();
            f.Novo();
        }

        private void grid1_Editar(object sender, EventArgs e)
        {
            FormCobrancaCadastro f = new FormCobrancaCadastro();
            f.Editar(grid1);
        }

        private void grid1_Excluir(object sender, EventArgs e)
        {
            Boolean podeExcluir = true;

            DataRowCollection drc = grid1.GetDataRows();

            for (int i = 0; i < drc.Count; i++)
            {
                int IDBOLETOSTATUS = int.Parse(drc[i]["IDBOLETOSTATUS"].ToString());

                if ((IDBOLETOSTATUS == 0) || (IDBOLETOSTATUS == 5))
                {
                    // ok
                }
                else
                {
                    podeExcluir = false;
                }
            }

            if (podeExcluir)
            {
               
                FormCobrancaCadastro f = new FormCobrancaCadastro();
                f.Excluir(grid1);
                
            }
            else
            {
                AppLib.Windows.FormMessageDefault.ShowError("Somente remessa não remetida ou cancelada pode ser excluída.");
            }
        }

        public void GerarNossoNumero(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    for (int i = 0; i < drc.Count; i++)
                    {
                        int CODEMPRESA = int.Parse(drc[i]["CODEMPRESA"].ToString());
                        int CODLANCA = int.Parse(drc[i]["CODLANCA"].ToString());
                        String NOSSONUMERO = drc[i]["NOSSONUMERO"].ToString();

                        if (NOSSONUMERO == String.Empty)
                        {
                            ERP.Financeiro.BO.GerarNossoNumero(CODEMPRESA, CODLANCA);
                            ERP.Financeiro.BO.GerarDigitoNossoNumero(CODEMPRESA, CODLANCA);
                        }
                        else
                        {
                            AppLib.Windows.FormMessageDefault.ShowError("Lançamento " + CODLANCA + " já possui o nosso número gerado.");
                        }
                    }

                    grid1.Atualizar();
                }
            }
            catch (Exception ex)
            {
                AppLib.Windows.FormMessageDefault.ShowError(ex.Message);
            }            
        }

        public void GerarCodigoBarras(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    for (int i = 0; i < drc.Count; i++)
                    {
                        int CODEMPRESA = int.Parse(drc[i]["CODEMPRESA"].ToString());
                        int CODLANCA = int.Parse(drc[i]["CODLANCA"].ToString());
                        ERP.Financeiro.BO.GerarCodigoBarras(CODEMPRESA, CODLANCA);
                    }
                }

                this.GerarIPTE(this, null);

                grid1.Atualizar();
            }
            catch (Exception ex)
            {
                AppLib.Windows.FormMessageDefault.ShowError(ex.Message);
            }            
        }

        public void GerarIPTE(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    for (int i = 0; i < drc.Count; i++)
                    {
                        int CODEMPRESA = int.Parse(drc[i]["CODEMPRESA"].ToString());
                        int CODLANCA = int.Parse(drc[i]["CODLANCA"].ToString());
                        ERP.Financeiro.BO.GerarIPTE(CODEMPRESA, CODLANCA);
                    }
                }
            }
            catch (Exception ex)
            {
                AppLib.Windows.FormMessageDefault.ShowError(ex.Message);
            }
        }

        public void GerarArquivoRemessa(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    int CODEMPRESA = int.Parse(drc[0]["CODEMPRESA"].ToString());
                    String CODCONTA = drc[0]["CODCONTA"].ToString();
                    List<int> CODLANCA = new List<int>();

                    for (int i = 0; i < drc.Count; i++)
                    {
                        String CODCONTA_TEMP = drc[i]["CODCONTA"].ToString();
                        int IDBOLETOSTATUS = int.Parse(drc[i]["IDBOLETOSTATUS"].ToString());

                        // Valida
                        if (CODCONTA.Equals(CODCONTA_TEMP))
                        {
                            if ((IDBOLETOSTATUS == 0) || (IDBOLETOSTATUS == 5))
                            {
                                // Prepara
                                CODLANCA.Add(int.Parse(drc[i]["CODLANCA"].ToString()));
                            }
                            else
                            {
                                throw new Exception("O arquivo de remessa pode ser gerado apenas para boletos com status não remetido.");
                            }                            
                        }
                        else
                        {
                            throw new Exception("Não é possível gerar remessa de bancos distintos.\r\nDica: Faça um filtro por banco.");
                        }
                    }

                    // Executa
                    ERP.Financeiro.BO.GerarArquivoRemessa(CODEMPRESA, CODLANCA, CODCONTA);

                    grid1.Atualizar();
                }
            }
            catch (Exception ex)
            {
                AppLib.Windows.FormMessageDefault.ShowError(ex.Message);
            }
        }

        public void ImprimirBoleto(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    int CODEMPRESA = int.Parse(drc[0]["CODEMPRESA"].ToString());
                    String CODLANCA = "( ";

                    for (int i = 0; i < drc.Count; i++)
                    {
                        CODLANCA += drc[i]["CODLANCA"].ToString();
                        CODLANCA += ", ";
                    }

                    if (CODLANCA.Length > 2)
                    {
                        CODLANCA = CODLANCA.Substring(0, CODLANCA.Length - 2);
                        CODLANCA += ")";
                    }
                    Relatorios.XrBoletoBancario rel = new Relatorios.XrBoletoBancario(CODEMPRESA, CODLANCA);
                    new DevExpress.XtraReports.UI.ReportPrintTool(rel).ShowPreviewDialog();                    
                }
            }
            catch (Exception ex)
            {
                AppLib.Windows.FormMessageDefault.ShowError(ex.Message);
            }
        }

        public void CancelarRemessa(object sender, EventArgs e)
        {
            if (AppLib.Windows.FormMessageDefault.ShowQuestion("Confirma o cancelamento deste e dos demais boletos da mesma remessa?\r\nATENÇÃO: TODOS OS BOLETOS DA MESMA REMESSA SERÃO CANCELADOS.") == System.Windows.Forms.DialogResult.Yes)
            {
                System.Data.DataRowCollection drc = grid1.GetDataRows();

                if (drc != null)
                {
                    List<int> lista = new List<int>();

                    int CODEMPRESA = 0;
                    String CODCONVENIO = String.Empty;

                    for (int i = 0; i < drc.Count; i++)
                    {
                        if ( drc[i]["CODREMESSA"] != DBNull.Value )
                        {
                            CODEMPRESA = int.Parse(drc[i]["CODEMPRESA"].ToString());
                            int CODLANCA = int.Parse(drc[i]["CODLANCA"].ToString());
                            int CODREMESSA = int.Parse(drc[i]["CODREMESSA"].ToString());
                            CODCONVENIO = drc[i]["CODCONVENIO"].ToString();
                            int IDBOLETOSTATUS = int.Parse(drc[i]["IDBOLETOSTATUS"].ToString());

                            if (IDBOLETOSTATUS == 0)
                            {
                                AppLib.Windows.FormMessageDefault.ShowInfo("Referente lançamento " + CODLANCA + ", não é possível cancelar remessa não remetida.");
                            }

                            if ((IDBOLETOSTATUS == 1) || (IDBOLETOSTATUS == 3))
                            {
                                if (!lista.Contains(CODREMESSA))
                                {
                                    lista.Add(CODREMESSA);
                                }
                            }

                            if (IDBOLETOSTATUS == 2)
                            {
                                AppLib.Windows.FormMessageDefault.ShowInfo("Referente lançamento " + CODLANCA + ", não é possível cancelar remessa registrada.");
                            }

                            if (IDBOLETOSTATUS == 4)
                            {
                                AppLib.Windows.FormMessageDefault.ShowInfo("Referente lançamento " + CODLANCA + ", não é possível cancelar remessa baixada.");
                            }

                            if (IDBOLETOSTATUS == 5)
                            {
                                AppLib.Windows.FormMessageDefault.ShowInfo("Referente lançamento " + CODLANCA + ", não é possível cancelar remessa já cancelada.");
                            }
                        }                    
                    }

                    for (int i = 0; i < lista.Count; i++)
                    {
                        String comando = @"
UPDATE FBOLETO
SET CODREMESSA = NULL, DATAREMESSA = NULL, IDBOLETOSTATUS = 5
WHERE CODEMPRESA = ?
  AND CODCONVENIO = ?
  AND CODREMESSA = ?";

                        int temp = AppLib.Context.poolConnection.Get("Start").ExecTransaction(comando, new Object[] { CODEMPRESA, CODCONVENIO, lista[i] });

                        if (temp >= 1)
                        {
                            // ok
                        }
                        else
                        {
                            AppLib.Windows.FormMessageDefault.ShowError("Erro ao cancelar a remessa " + lista[i]);
                        }
                    }
                }

                grid1.Atualizar();

            }
        }

        private void grid1_Load(object sender, EventArgs e)
        {

        }

    }
}
