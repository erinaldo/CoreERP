using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.Glb.New.Classes
{
    public class Projeto
    {
        #region Variáveis

        private Classes.Utilidades util = new Utilidades();
        private string sql = "";

        public New.Classes.Models.APROJETO projeto;

        #endregion

        #region Construtor

        public Projeto() 
        {
            projeto = new Models.APROJETO();
        }

        #endregion

        #region Métodos

        public bool Excluir(int idProjeto)
        {
            bool excluiu = true;
            int delete = 0;

            if (!ValidarUtilizacaoProjeto(idProjeto))
            {
                XtraMessageBox.Show("O Projeto selecionado possui vínculos com Apontamento.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Verifica se o Projeto possui tarefas em sua composição
            if (VerificarTarefasProjeto(idProjeto))
            {
                // Tarefas do Projeto
                delete = AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM APROJETOTAREFA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND IDPROJETO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, idProjeto });

                if (delete > 0)
                {
                    // Projeto
                    delete = AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM APROJETO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND IDPROJETO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, idProjeto });

                    if (delete < 0)
                    {
                        excluiu = false;
                    }
                }
                else
                {
                    excluiu = false;
                }
            }
            else
            {
                // Projeto
                delete = AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM APROJETO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND IDPROJETO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, idProjeto });

                if (delete < 0)
                {
                    excluiu = false;
                }
            }

            return excluiu;
        }

        private bool VerificarTarefasProjeto(int idProjeto)
        {
            int count = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, @"SELECT COUNT(*) FROM APROJETOTAREFA WHERE CODEMPRESA = ? AND CODFILIAL = ? AND IDPROJETO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, idProjeto }));

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidarUtilizacaoProjeto(int idProjeto)
        {
            bool relacionado = true;
            int existe = 0;

            // Apontamento
            existe = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, @"SELECT * FROM AAPONTAMENTO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND IDPROJETO = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, idProjeto }));

            if (existe > 0)
            {
                relacionado = false;
            }

            return relacionado;
        }

        #endregion
    }
}
