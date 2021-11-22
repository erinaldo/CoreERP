using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.Glb.New.Classes
{
    public class Unidade
    {
        #region Variáveis

        private Classes.Utilidades util = new Utilidades();
        private string sql = "";

        public New.Classes.Models.AUNIDADE unidade;

        #endregion

        #region Construtor

        public Unidade() { }

        #endregion

        #region Métodos

        public bool Excluir(int idUnidade)
        {
            bool excluiu = true;
            int delete = 0;

            if (!ValidarUtilizacaoUnidade(idUnidade))
            {
                XtraMessageBox.Show("A Unidade selecionada possui vínculos com Apontamento.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Verifica se a Unidade possui projetos em sua composição
            if (VerificaProjetosUnidade(idUnidade))
            {
                // Projetos da Unidade
                delete = AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM AUNIDADEPROJETO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND IDUNIDADE = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, idUnidade });

                if (delete > 0)
                {
                    // Projeto
                    delete = AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM AUNIDADE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND IDUNIDADE = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, idUnidade });

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
            if (VerificaReembolsoUnidade(idUnidade))
            {
                // Reembolso da Unidade
                delete = AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM AUNIDADEREEMBOLSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND IDUNIDADE = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, idUnidade });

                if (delete > 0)
                {
                    // Projeto
                    delete = AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM AUNIDADE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND IDUNIDADE = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, idUnidade });

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
                delete = AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"DELETE FROM AUNIDADE WHERE CODEMPRESA = ? AND CODFILIAL = ? AND IDUNIDADE = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, idUnidade });

                if (delete < 0)
                {
                    excluiu = false;
                }
            }

            return excluiu;
        }

        private bool VerificaProjetosUnidade(int idUnidade)
        {
            int count = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, @"SELECT COUNT(*) FROM AUNIDADEPROJETO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND IDUNIDADE = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, idUnidade }));

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool VerificaReembolsoUnidade(int idUnidade)
        {
            int count = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, @"SELECT COUNT(*) FROM AUNIDADEREEMBOLSO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND IDUNIDADE = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, idUnidade }));

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidarUtilizacaoUnidade(int idUnidade)
        {
            bool relacionado = true;
            int existe = 0;

            // Apontamento
            existe = Convert.ToInt32(AppLib.Context.poolConnection.Get("Start").ExecGetField(-1, @"SELECT * FROM AAPONTAMENTO WHERE CODEMPRESA = ? AND CODFILIAL = ? AND IDUNIDADE = ?", new object[] { AppLib.Context.Empresa, AppLib.Context.Filial, idUnidade }));

            if (existe > 0)
            {
                relacionado = false;
            }

            return relacionado;
        }

        #endregion
    }
}
