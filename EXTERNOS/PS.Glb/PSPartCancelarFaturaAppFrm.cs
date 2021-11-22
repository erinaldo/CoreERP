using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Glb
{
    public partial class PSPartCancelarFaturaAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        public PSPartCancelarFaturaAppFrm()
        {
            InitializeComponent();
        }
        public override Boolean Execute()
        {
            int sim = 0, nao = 0;
            if (PS.Lib.PSMessageBox.ShowQuestion("Confirma o cancelamento da(s) faturas(s) selecionada(s) ?") == System.Windows.Forms.DialogResult.Yes)
            {
                for (int i = 0; i < psPartApp.DataGrid.SelectedRows.Count; i++)
                {
                    
                    if (verificaStatusLancamento(Convert.ToInt32(psPartApp.DataGrid.SelectedRows[i].Cells["CODLANCA"].Value)).Equals(false))
                    {
                        MessageBox.Show("Somente faturas em aberto podem ser canceladas.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }
                    string nfoudup = verificaNFOUDUP(Convert.ToInt32(psPartApp.DataGrid.SelectedRows[i].Cells["CODLANCA"].Value));
                    if (string.IsNullOrEmpty(nfoudup))
                    {
                        MessageBox.Show("Somente lançamento de fatura podem ser cancelados.", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }
                    if (nfoudup.Equals("0"))
                    {

                        DataTable dtCodLanca = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODLANCA, NFOUDUP FROM FLANCA WHERE CODFATURA = ? AND CODEMPRESA = ?", new object[] { Convert.ToInt32(psPartApp.DataGrid.SelectedRows[i].Cells["CODLANCA"].Value), AppLib.Context.Empresa });
                        for (int ii = 0; ii < dtCodLanca.Rows.Count; ii++)
                        {
                            if (CancelaFatura(Convert.ToInt32(dtCodLanca.Rows[ii]["CODLANCA"].ToString()), Convert.ToInt32(psPartApp.DataGrid.SelectedRows[i].Cells["CODLANCA"].Value)))
                            {
                                if (psPartApp.DataGrid.SelectedRows[i].Cells["NFOUDUP"].Value.Equals("0"))
                                {
                                    sim++;    
                                }
                            }
                            else
                            {
                                nao++;
                            }
                        }
                    }
                    else
                    {
                        int CodFatura = VerificaCodFatura(Convert.ToInt32(psPartApp.DataGrid.SelectedRows[i].Cells["CODLANCA"].Value));
                        if (!CodFatura.Equals(0))
                        {
                            if (CancelaFatura(Convert.ToInt32(psPartApp.DataGrid.SelectedRows[i].Cells["CODLANCA"].Value), CodFatura))
                            {
                                if (psPartApp.DataGrid.SelectedRows[i].Cells["NFOUDUP"].Value.Equals("0"))
                                {
                                    sim++;
                                }
                            }
                            else
                            {
                                nao++;
                            }
                        }
                        else
                        {
                            nao++;
                        }
                    }
                }
                MessageBox.Show("Total de Faturas Canceladas com sucesso: "+ sim +"\n Total de Faturas não Canceladas: "+ nao +"", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return true;
        }
        /// <summary>
        /// Verifica o status do lançamento, retorna true para aberto
        /// </summary>
        /// <param name="codlanca">Número do Lançamento</param>
        /// <returns>True ou False</returns>
        private bool verificaStatusLancamento(int codlanca)
        {
            try
            {
                string codStatus = AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT CODSTATUS FROM FLANCA WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] {codlanca, AppLib.Context.Empresa }).ToString();
                if (codStatus.Equals("0"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
        /// <summary>
        /// Verifica o campo NFOUDUP, retorna true para 0
        /// </summary>
        /// <param name="codlanca">Número do Lançamento</param>
        /// <returns>True ou False</returns>
        private string verificaNFOUDUP(int codlanca)
        {
            try
            {
                return AppLib.Context.poolConnection.Get("Start").ExecGetField(string.Empty, @"SELECT NFOUDUP FROM FLANCA WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { codlanca, AppLib.Context.Empresa }).ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        private bool CancelaFatura(int codLanca, int codFatura)
        {
            try
            {
                if (!AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FLANCA SET CODFATURA = NULL, NFOUDUP = ? WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { "", codLanca, AppLib.Context.Empresa }).Equals(0))
                {
                    if (AlteraStatusFatura(codFatura))
                    {
                        return true;    
                    }
                    return false;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }
        private bool AlteraStatusFatura(int codFatura)
        {
            try
            {
                if (!AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE FLANCA SET CODSTATUS = 2 WHERE CODLANCA = ? AND CODEMPRESA = ?", new object[] { codFatura, AppLib.Context.Empresa }).Equals(0))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private int VerificaCodFatura(int codLanca)
        {
            return Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(0, "SELECT CODFATURA FROM FLANCA WHERE CODLANCA = ?", new object[] { codLanca }));
        }
    }
}
