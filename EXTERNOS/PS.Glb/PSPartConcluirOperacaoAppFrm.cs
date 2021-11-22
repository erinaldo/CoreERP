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
    public partial class PSPartConcluirOperacaoAppFrm : PS.Lib.WinForms.FrmBaseApp
    {
        private int sim = 0, nao = 0;
        public PSPartConcluirOperacaoAppFrm()
        {
            InitializeComponent();
        }
        public override bool Execute()
        {
            if (psPartApp.Access == PS.Lib.AppAccess.View)
            {
                if (psPartApp.DataGrid != null)
                {
                    for (int i = 0; i < psPartApp.DataGrid.SelectedRows.Count; i++)
                    {
                        if (psPartApp.DataGrid.SelectedRows[i].Cells["CODSTATUS"].Value.ToString() == "5")
                        {
                            AppLib.Context.poolConnection.Get("Start").BeginTransaction();
                            if (AlteraStatus(Convert.ToInt32(psPartApp.DataGrid.SelectedRows[i].Cells["CODOPER"].Value)) == true)
                            {
                                if (AlteraQuantidadeSaldo(Convert.ToInt32(psPartApp.DataGrid.SelectedRows[i].Cells["CODOPER"].Value)) == true)
                                {
                                    if (InseriMotivo(Convert.ToInt32(psPartApp.DataGrid.SelectedRows[i].Cells["CODOPER"].Value)) == true)
                                    {
                                        AppLib.Context.poolConnection.Get("Start").Commit();
                                        sim = sim + 1;
                                    }
                                    else
                                    {
                                        AppLib.Context.poolConnection.Get("Start").Rollback();
                                        nao = nao + 1;
                                    }
                                }
                                else
                                {
                                    AppLib.Context.poolConnection.Get("Start").Rollback();
                                    nao = nao + 1;
                                }
                            }
                            else
                            {
                                AppLib.Context.poolConnection.Get("Start").Rollback();
                                nao = nao + 1;
                            }
                        }
                    }
                }
            }
            MessageBox.Show("Qtd. de Operações concluídas: " + sim + "\nQtd. de Operações não concluídas: " + nao + "\n", "Informação do Sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }
        private bool AlteraStatus(int codoper)
        {
            try
            {
                int retorno = AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE GOPER SET CODSTATUS = '6' WHERE CODOPER = ? AND CODEMPRESA = ? ", new object[] { codoper, AppLib.Context.Empresa });
                if (retorno == 1)
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
        private bool AlteraQuantidadeSaldo(int codoper)
        {
            try
            {
                int retorno = AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE GOPERITEM SET QUANTIDADE_SALDO = 0 WHERE CODOPER = ? AND CODEMPRESA = ?", new object[] { codoper, AppLib.Context.Empresa });
                if (retorno == 1)
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
        private bool InseriMotivo(int codoper)
        {
            try
            {
                int retorno = AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GMOTIVOFINALIZACAO (CODOPER, CODEMPRESA, CODUSUARIO, MOTIVO) VALUES (?, ?, ?, ?)", new object[] { codoper, AppLib.Context.Empresa, AppLib.Context.Usuario, txtMotivo.Text });
                if (retorno == 1)
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
    }
}
