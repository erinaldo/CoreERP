using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartOperSimFatAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        PS.Lib.Constantes ct = new PS.Lib.Constantes();
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();

        public PSPartOperSimFatAppFrm()
        {
            InitializeComponent();

            psLookup6.PSPart = "PSPartCondicaoPgto";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            panel3.Visible = false;
            panel3.Dock = DockStyle.None;
        }

        public override Boolean Execute()
        {
            if (PS.Lib.PSMessageBox.ShowQuestion("Deseja realizar a simulação financeira da operação ?") == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    if (this.psPartApp.Access == PS.Lib.AppAccess.View)
                    {
                        PS.Lib.PSMessageBox.ShowInfo("Para executar este aplicativo utilize a opção que encontra-se na edição.");
                        return false;
                    }

                    if (this.psPartApp.Access == PS.Lib.AppAccess.Edit)
                    {
                        int codEmpresa = 0;
                        int codOper = 0;

                        if (psLookup6.Text == string.Empty)
                        {
                            throw new Exception("Informe a condição de pagamento.");                                                    
                        }

                        PS.Lib.DataField dataField = new PS.Lib.DataField();
                        dataField = gb.RetornaDataFieldByCampo(this.psPartApp.DataField, "CODEMPRESA");

                        codEmpresa = int.Parse(dataField.Valor.ToString());

                        dataField = gb.RetornaDataFieldByCampo(this.psPartApp.DataField, "CODOPER");

                        codOper = int.Parse(dataField.Valor.ToString());

                        SimulaFinanceiro(codEmpresa, codOper);

                        panel3.Visible = true;
                        panel3.Dock = DockStyle.Fill;
                    }
                }
                catch (Exception ex)
                {
                    PS.Lib.PSMessageBox.ShowError(ex.Message);
                    //dbs.Rollback();
                    return false;
                }
            }


            return true;
        }

        private void SimulaFinanceiro(int codEmpresa, int codOper)
        {
            List<string> Texto = new List<string>();

            Texto.Add("*** RESULTADO ***");
            Texto.Add("");

            string sSql = "";

            //Gera Operação
            sSql = @"SELECT * FROM GOPER WHERE CODEMPRESA = ? AND CODOPER = ?";

            DataTable GOPER = dbs.QuerySelect(sSql,codEmpresa, codOper);

            DateTime emissao = Convert.ToDateTime(GOPER.Rows[0]["DATAEMISSAO"].ToString());

            sSql = @"SELECT * FROM GOPERITEM WHERE CODEMPRESA = ? AND CODOPER = ?";

            DataTable GOPERITEM = dbs.QuerySelect(sSql, codEmpresa, codOper);

            //Busca parametros da condição de Pagamento
            sSql = @"SELECT * FROM VCONDICAOPGTO WHERE CODEMPRESA = ? AND CODCONDICAO = ?";

            DataTable VCONDICAOPGTO = dbs.QuerySelect(sSql, codEmpresa, psLookup6.Text);

            if (VCONDICAOPGTO.Rows.Count <= 0)
            {
                throw new Exception("Não foi possível carregar os parâmetros da Condição de Pagamento.");
            }

            Texto.Add("Valor Bruto: " + Convert.ToDouble(GOPER.Rows[0]["VALORBRUTO"].ToString()).ToString("###,###,##0.00"));
            Texto.Add("Valor Liquido: " + Convert.ToDouble(GOPER.Rows[0]["VALORLIQUIDO"].ToString()).ToString("###,###,##0.00"));
            Texto.Add("Emissão: " + emissao.ToShortDateString());
            Texto.Add("Condição de Pagamento: " + VCONDICAOPGTO.Rows[0]["NOME"].ToString());
            Texto.Add("");
            Texto.Add("=======================================================");
            Texto.Add("");

            int nParcelas = int.Parse(VCONDICAOPGTO.Rows[0]["NUMPARCELAS"].ToString());
            int nPrazo = int.Parse(VCONDICAOPGTO.Rows[0]["NUMPRAZO"].ToString());
            int nIntervalo = int.Parse(VCONDICAOPGTO.Rows[0]["NUMINTERVALO"].ToString());
            decimal nTxJuros = Convert.ToDecimal(VCONDICAOPGTO.Rows[0]["TAXAJUROS"].ToString());

            decimal nliquido = Convert.ToDecimal(GOPER.Rows[0]["VALORLIQUIDO"].ToString());
            decimal vlOriginal = 0;

            decimal pcJuros = nTxJuros;
            decimal vlJuros = 0;

            //decimal pcDesconto = 0;
            decimal vlDesconto = 0;

            //decimal pcMulta = 0;
            decimal vlMulta = 0;

            decimal vlLiquido = 0;

            DateTime vencimento = emissao;

            Texto.Add("Parâmetro da Condição de Pagamento");
            Texto.Add("");
            Texto.Add("Parcelas : " + nParcelas);
            Texto.Add("Prazo : " + nPrazo);
            Texto.Add("Taxa de Juros : " + nTxJuros);
            Texto.Add("");
            Texto.Add("Detalhamento das Parcelas");
            Texto.Add("");
                
            for (int i = 0; i < nParcelas; i++)
            {
                //Prazo
                if (i == 0)
                {
                    vencimento = vencimento.AddDays(nPrazo);
                }
                else
                {
                    vencimento = vencimento.AddDays(nIntervalo);
                }

                //Valor Original
                vlOriginal = (nliquido / nParcelas);

                //Juros
                if (nTxJuros > 0)
                {
                    vlJuros = ((vlOriginal * nTxJuros) / 100);
                }

                //Valor Liquido
                vlLiquido = (vlOriginal + vlMulta + vlJuros) - vlDesconto;

                Texto.Add(i + 1 + " - " + vencimento.ToShortDateString() + 
                          " - Juros : " + Convert.ToDouble(vlJuros).ToString("###,###,##0.00") + 
                          " - Valor :" + Convert.ToDouble(vlLiquido).ToString("###,###,##0.00"));
            }

            Texto.Add("");

            string[] arr = new string[Texto.Count];
            for(int i = 0;i < Texto.Count; i++)
            {
                arr[i] = Texto[i];
            }

            richTextBox1.Lines = arr;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel3.Dock = DockStyle.None;
        }
    }
}
