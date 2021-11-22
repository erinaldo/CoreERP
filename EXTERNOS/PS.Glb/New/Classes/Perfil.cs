using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS.Glb.New.Classes
{
    public class Perfil
    {
        #region Variáveis

        private Utilidades util = new Utilidades();

        public Models.GPERFIL perfil;
        public Models.GUSUARIOPERFIL usuarioPerfil;
        public Models.GPERFILTIPOPER perfilTipoOperacao;
        public Models.GPERMISSAOMODULO permissaoModulo;
        public Models.GPERMISSAOMENU permissaoMenu;

        public List<Models.GPERMISSAOMENU> listPermissaoMenus;

        #endregion

        #region Construtor

        public Perfil()
        {
            perfil = new Models.GPERFIL();
            usuarioPerfil = new Models.GUSUARIOPERFIL();
            perfilTipoOperacao = new Models.GPERFILTIPOPER();
            permissaoModulo = new Models.GPERMISSAOMODULO();
            permissaoMenu = new Models.GPERMISSAOMENU();
            listPermissaoMenus = new List<Models.GPERMISSAOMENU>();
        }

        #endregion

        #region Métodos

        #region Preenchimento e validação dos dados

        public void GetPerfil(string codPerfil, string nome, int ativo)
        {
            perfil.CodPerfil = codPerfil;
            perfil.Nome = nome;
            perfil.Ativo = ativo;
        }

        public void GetUsuarioPerfil(int codColigada, string codPerfil, List<string> usuarios)
        {
            usuarioPerfil.CodColigada = codColigada;
            usuarioPerfil.CodPerfil = codPerfil;
            usuarioPerfil.Usuarios = usuarios;
        }

        public void GetPerfilTipOper(int codColigada, string codPerfil, List<string> codTipOper, List<int> incluir, List<int> excluir, List<int> alterar, List<int> faturar, List<int> incluirFat, List<int> consultar, List<int> cancelar, List<int> concluir, List<int> aprovar, List<int> aprovaFinanceiro, List<int> aprovaDesconto, List<int> aprovaLimiteCredito, List<int> reprovar, List<int> gerarBoleto)
        {
            perfilTipoOperacao.CodColigada = codColigada;
            perfilTipoOperacao.CodPerfil = codPerfil;
            perfilTipoOperacao.CodTipOper = codTipOper;
            perfilTipoOperacao.Incluir = incluir;
            perfilTipoOperacao.Excluir = excluir;
            perfilTipoOperacao.Alterar = alterar;
            perfilTipoOperacao.Faturar = faturar;
            perfilTipoOperacao.IncluirFat = incluirFat;
            perfilTipoOperacao.Consultar = consultar;
            perfilTipoOperacao.Cancelar = cancelar;
            perfilTipoOperacao.Concluir = concluir;
            perfilTipoOperacao.Aprovar = aprovar;
            perfilTipoOperacao.AprovaFinanceiro = aprovaFinanceiro;
            perfilTipoOperacao.AprovaDesconto = aprovaDesconto;
            perfilTipoOperacao.AprovaLimiteCredito = aprovaLimiteCredito;
            perfilTipoOperacao.Reprovar = reprovar;
            perfilTipoOperacao.GerarBoleto = gerarBoleto;
        }

        public void GetPermissaoModulo(int idPermissaoMenu, List<string> codModulo, string codPerfil, List<int> acesso)
        {
            permissaoModulo.IDPermissaoMenu = idPermissaoMenu;
            permissaoModulo.CodModulo = codModulo;
            permissaoModulo.CodPerfil = codPerfil;
            permissaoModulo.Acesso = acesso;
        }

        public void GetPermissaoMenu(List<Models.GPERMISSAOMENU> listPermissaoMenu)
        {
            this.listPermissaoMenus = listPermissaoMenu;
        }

        public bool ExisteUsuarioPerfil(string usuario, string codPerfil = "")
        {
            DataTable dtUsuario = null;

            if (string.IsNullOrEmpty(codPerfil))
            {
                dtUsuario = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM GUSUARIOPERFIL WHERE CODEMPRESA = ? AND CODUSUARIO = ? AND CODPERFIL = ?", new object[] { AppLib.Context.Empresa, usuario, usuarioPerfil.CodPerfil });
            }
            else
            {
                dtUsuario = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM GUSUARIOPERFIL WHERE CODEMPRESA = ? AND CODUSUARIO = ? AND CODPERFIL = ?", new object[] { AppLib.Context.Empresa, usuario, codPerfil });
            }

            if (dtUsuario.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ExisteTipoOperacao(string tipoOperacao, string codPerfil)
        {
            DataTable dtTipoOperacao = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT * FROM GPERFILTIPOPER WHERE CODTIPOPER = ? AND CODPERFIL = ?", new object[] { tipoOperacao, codPerfil });

            if (dtTipoOperacao.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Salvar(bool edita)
        {
            bool salvou = false;

            try
            {
                // Perfil
                if (perfil != null)
                {
                    if (edita == false)
                    {
                        // Insert
                        salvou = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GPERFIL VALUES (?, ?, ?)", new object[] { perfil.CodPerfil, perfil.Nome, perfil.Ativo }));
                    }
                    else
                    {
                        // Update
                        salvou = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE GPERFIL SET NOME= ?, ATIVO = ? WHERE CODPERFIL = ?", new object[] { perfil.Nome, perfil.Ativo, perfil.CodPerfil }));
                    }

                    if (salvou)
                    {
                        // Reseta o valor da variável
                        salvou = false;

                        // Usuário/Perfil
                        if (usuarioPerfil != null)
                        {
                            for (int i = 0; i < usuarioPerfil.Usuarios.Count; i++)
                            {
                                if (!ExisteUsuarioPerfil(usuarioPerfil.Usuarios[i]))
                                {
                                    // Insert   
                                    salvou = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GUSUARIOPERFIL VALUES (?, ?, ?)", new object[] { usuarioPerfil.CodColigada, usuarioPerfil.Usuarios[i], usuarioPerfil.CodPerfil }));
                                }
                                else
                                {
                                    salvou = true;
                                }
                            }

                            if (salvou)
                            {
                                // Reseta o valor da variável
                                salvou = false;

                                // Tipo de Operação
                                if (perfilTipoOperacao != null)
                                {
                                    for (int i = 0; i < perfilTipoOperacao.CodTipOper.Count; i++)
                                    {
                                        if (!ExisteTipoOperacao(perfilTipoOperacao.CodTipOper[i], perfilTipoOperacao.CodPerfil))
                                        {
                                            // Insert
                                            salvou = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GPERFILTIPOPER VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { perfilTipoOperacao.CodColigada, perfilTipoOperacao.CodPerfil, perfilTipoOperacao.CodTipOper[i], perfilTipoOperacao.Incluir[i], perfilTipoOperacao.Excluir[i], perfilTipoOperacao.Alterar[i], perfilTipoOperacao.Faturar[i], perfilTipoOperacao.IncluirFat[i], perfilTipoOperacao.Consultar[i], perfilTipoOperacao.Cancelar[i], perfilTipoOperacao.Concluir[i], perfilTipoOperacao.Aprovar[i], perfilTipoOperacao.AprovaFinanceiro[i], perfilTipoOperacao.AprovaDesconto[i], perfilTipoOperacao.AprovaLimiteCredito[i], perfilTipoOperacao.Reprovar[i], perfilTipoOperacao.GerarBoleto[i] }));
                                        }
                                        else
                                        {
                                            // Update
                                            salvou = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE GPERFILTIPOPER SET INCLUIR = ?, EXCLUIR = ?, ALTERAR = ?, FATURAR = ?, INCLUIRFAT = ?, CONSULTAR = ?, CANCELAR = ?, CONCLUIR = ?, APROVAR = ?, APROVAFINCEIRO = ?, APROVADESCONTO = ?, APROVALIMITECREDITO = ?, REPROVAR = ?, GERARBOLETO = ? WHERE CODEMPRESA = ? AND CODPERFIL = ? AND CODTIPOPER = ?", new object[] { perfilTipoOperacao.Incluir[i], perfilTipoOperacao.Excluir[i], perfilTipoOperacao.Alterar[i], perfilTipoOperacao.Faturar[i], perfilTipoOperacao.IncluirFat[i], perfilTipoOperacao.Consultar[i], perfilTipoOperacao.Cancelar[i], perfilTipoOperacao.Concluir[i], perfilTipoOperacao.Aprovar[i], perfilTipoOperacao.AprovaFinanceiro[i], perfilTipoOperacao.AprovaDesconto[i], perfilTipoOperacao.AprovaLimiteCredito[i], perfilTipoOperacao.Reprovar[i], perfilTipoOperacao.GerarBoleto[i], perfilTipoOperacao.CodColigada, perfilTipoOperacao.CodPerfil, perfilTipoOperacao.CodTipOper[i] }));
                                        }
                                    }

                                    if (salvou)
                                    {
                                        // Reseta o valor da variável
                                        salvou = false;

                                        // Permissão Módulo
                                        if (permissaoModulo != null)
                                        {
                                            for (int i = 0; i < permissaoModulo.CodModulo.Count; i++)
                                            {
                                                if (edita == false)
                                                {
                                                    // Insert
                                                    salvou = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GPERMISSAOMODULO VALUES (?, ?, ?)", new object[] { permissaoModulo.CodModulo[i], permissaoModulo.CodPerfil, permissaoModulo.Acesso[i] }));
                                                }
                                                else
                                                {
                                                    // Update
                                                    salvou = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE GPERMISSAOMODULO SET ACESSO = ? WHERE CODMODULO = ? AND CODPERFIL = ?", new object[] { permissaoModulo.Acesso[i], permissaoModulo.CodModulo[i], permissaoModulo.CodPerfil }));
                                                }
                                            }

                                            if (salvou)
                                            {
                                                // Reseta o valor da variável
                                                salvou = false;

                                                if (permissaoMenu != null)
                                                {
                                                    for (int i = 0; i < listPermissaoMenus.Count; i++)
                                                    {
                                                        if (edita == false)
                                                        {
                                                            // Insert
                                                            salvou = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO GPERMISSAOMENU VALUES (?, ?, ?, ?, ?, ?, ?)", new object[] { listPermissaoMenus[i].CodMenu, listPermissaoMenus[i].CodPerfil, listPermissaoMenus[i].Acesso, listPermissaoMenus[i].Edicao, listPermissaoMenus[i].Inclusao, listPermissaoMenus[i].Exclusao, 0 }));
                                                        }
                                                        else
                                                        {
                                                            // Update
                                                            salvou = Convert.ToBoolean(AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"UPDATE GPERMISSAOMENU SET ACESSO = ?, EDICAO = ?, INCLUSAO = ?, EXCLUSAO = ?, CONSULTA = ? WHERE CODMENU = ? AND CODPERFIL = ?", new object[] { listPermissaoMenus[i].Acesso, listPermissaoMenus[i].Edicao, listPermissaoMenus[i].Inclusao, listPermissaoMenus[i].Exclusao, 0, listPermissaoMenus[i].CodMenu, listPermissaoMenus[i].CodPerfil }));
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public DataTable CarregaGrid()
        {
            try
            {
                DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODPERFIL, NOME, CAST(ATIVO AS BIT) AS 'ATIVO' FROM GPERFIL");

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable CriarSchemaUsuario()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Código da Empresa", typeof(int)));
            dt.Columns.Add(new DataColumn("Código do Usuário", typeof(string)));
            dt.Columns.Add(new DataColumn("Usuário", typeof(string)));

            return dt;
        }

        public DataTable CriarTabelaUsuarioPerfil(string codPerfil)
        {
            DataTable dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT CODEMPRESA AS 'Código da Empresa', GUSUARIOPERFIL.CODUSUARIO AS 'Código do Usuário', GUSUARIO.NOME AS 'Usuário' FROM GUSUARIOPERFIL INNER JOIN GUSUARIO ON GUSUARIO.CODUSUARIO = GUSUARIOPERFIL.CODUSUARIO WHERE CODEMPRESA = ? AND CODPERFIL = ?", new object[] { AppLib.Context.Empresa, codPerfil });

            return dt;
        }

        private DataTable CriarSchemaTipoOperacao(DataTable dtTipoOperacao)
        {
            dtTipoOperacao.Columns.Add(new DataColumn("Código do Tipo de Operação", typeof(string)));
            dtTipoOperacao.Columns.Add(new DataColumn("Tipo de Operação", typeof(string)));
            dtTipoOperacao.Columns.Add(new DataColumn("Incluir", typeof(bool)));
            dtTipoOperacao.Columns.Add(new DataColumn("Excluir", typeof(bool)));
            dtTipoOperacao.Columns.Add(new DataColumn("Alterar", typeof(bool)));
            dtTipoOperacao.Columns.Add(new DataColumn("Faturar", typeof(bool)));
            dtTipoOperacao.Columns.Add(new DataColumn("Incluir Fatura", typeof(bool)));
            dtTipoOperacao.Columns.Add(new DataColumn("Consultar", typeof(bool)));
            dtTipoOperacao.Columns.Add(new DataColumn("Cancelar", typeof(bool)));
            dtTipoOperacao.Columns.Add(new DataColumn("Concluir", typeof(bool)));
            dtTipoOperacao.Columns.Add(new DataColumn("Aprovar", typeof(bool)));
            dtTipoOperacao.Columns.Add(new DataColumn("Aprovar Financeiro", typeof(bool)));
            dtTipoOperacao.Columns.Add(new DataColumn("Aprovar Desconto", typeof(bool)));
            dtTipoOperacao.Columns.Add(new DataColumn("Aprovar Limite Crédito", typeof(bool)));
            dtTipoOperacao.Columns.Add(new DataColumn("Reprovar", typeof(bool)));
            dtTipoOperacao.Columns.Add(new DataColumn("Gerar Boleto", typeof(bool)));

            return dtTipoOperacao;
        }

        public DataTable CriarTabelaTipoOperacao(bool edita, string codPerfil)
        {
            DataTable dtTipoOperacao = new DataTable();
            DataTable dt = new DataTable();

            if (edita == false)
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT DISTINCT 
                                                                            TIP.CODTIPOPER AS 'Código do Tipo de Operação', 
                                                                            TIP.DESCRICAO AS 'Descrição'
                                                                            FROM GTIPOPER TIP
                                                                            LEFT JOIN GPERFILTIPOPER P
                                                                            ON P.CODEMPRESA = TIP.CODEMPRESA AND P.CODTIPOPER = TIP.CODTIPOPER
                                                                            WHERE TIP.CODEMPRESA = ?
                                                                            ORDER BY TIP.CODTIPOPER ASC", new object[] { AppLib.Context.Empresa });
            }
            else
            {
                dt = AppLib.Context.poolConnection.Get("Start").ExecQuery(@"SELECT 
                                                                        	GTIPOPER.CODTIPOPER AS 'Código do Tipo de Operação',
                                                                        	GTIPOPER.DESCRICAO AS 'Descrição',
                                                                        	(SELECT INCLUIR FROM GPERFILTIPOPER WHERE  GPERFILTIPOPER.CODEMPRESA = GTIPOPER.CODEMPRESA
                                                                        	AND GPERFILTIPOPER.CODTIPOPER = GTIPOPER.CODTIPOPER AND CODPERFIL = ?) INCLUIR,	
                                                                        	(SELECT EXCLUIR FROM GPERFILTIPOPER WHERE  GPERFILTIPOPER.CODEMPRESA = GTIPOPER.CODEMPRESA
                                                                        	AND GPERFILTIPOPER.CODTIPOPER = GTIPOPER.CODTIPOPER AND CODPERFIL = ?) EXCLUIR,	
                                                                        	(SELECT ALTERAR FROM GPERFILTIPOPER WHERE  GPERFILTIPOPER.CODEMPRESA = GTIPOPER.CODEMPRESA
                                                                        	AND GPERFILTIPOPER.CODTIPOPER = GTIPOPER.CODTIPOPER AND CODPERFIL = ?) ALTERAR,	
                                                                        	(SELECT FATURAR FROM GPERFILTIPOPER WHERE  GPERFILTIPOPER.CODEMPRESA = GTIPOPER.CODEMPRESA
                                                                        	AND GPERFILTIPOPER.CODTIPOPER = GTIPOPER.CODTIPOPER AND CODPERFIL = ?) FATURAR,
                                                                        	(SELECT INCLUIRFAT FROM GPERFILTIPOPER WHERE  GPERFILTIPOPER.CODEMPRESA = GTIPOPER.CODEMPRESA
                                                                        	AND GPERFILTIPOPER.CODTIPOPER = GTIPOPER.CODTIPOPER AND CODPERFIL = ?) INCLUIRFAT,
                                                                        	(SELECT CONSULTAR FROM GPERFILTIPOPER WHERE  GPERFILTIPOPER.CODEMPRESA = GTIPOPER.CODEMPRESA
                                                                        	AND GPERFILTIPOPER.CODTIPOPER = GTIPOPER.CODTIPOPER AND CODPERFIL = ?) CONSULTAR,
                                                                        	(SELECT CANCELAR FROM GPERFILTIPOPER WHERE  GPERFILTIPOPER.CODEMPRESA = GTIPOPER.CODEMPRESA
                                                                        	AND GPERFILTIPOPER.CODTIPOPER = GTIPOPER.CODTIPOPER AND CODPERFIL = ?) CANCELAR,
                                                                        	(SELECT CONCLUIR FROM GPERFILTIPOPER WHERE  GPERFILTIPOPER.CODEMPRESA = GTIPOPER.CODEMPRESA
                                                                        	AND GPERFILTIPOPER.CODTIPOPER = GTIPOPER.CODTIPOPER AND CODPERFIL = ?) CONCLUIR,
                                                                        	(SELECT APROVAR FROM GPERFILTIPOPER WHERE  GPERFILTIPOPER.CODEMPRESA = GTIPOPER.CODEMPRESA
                                                                        	AND GPERFILTIPOPER.CODTIPOPER = GTIPOPER.CODTIPOPER AND CODPERFIL = ?) APROVAR,
                                                                        	(SELECT APROVAFINCEIRO FROM GPERFILTIPOPER WHERE  GPERFILTIPOPER.CODEMPRESA = GTIPOPER.CODEMPRESA
                                                                        	AND GPERFILTIPOPER.CODTIPOPER = GTIPOPER.CODTIPOPER AND CODPERFIL = ?) APROVAFINCEIRO,
                                                                        	(SELECT APROVADESCONTO FROM GPERFILTIPOPER WHERE  GPERFILTIPOPER.CODEMPRESA = GTIPOPER.CODEMPRESA
                                                                        	AND GPERFILTIPOPER.CODTIPOPER = GTIPOPER.CODTIPOPER AND CODPERFIL = ?) APROVADESCONTO,
                                                                        	(SELECT APROVALIMITECREDITO FROM GPERFILTIPOPER WHERE  GPERFILTIPOPER.CODEMPRESA = GTIPOPER.CODEMPRESA
                                                                        	AND GPERFILTIPOPER.CODTIPOPER = GTIPOPER.CODTIPOPER AND CODPERFIL = ?) APROVALIMITECREDITO,
                                                                        	(SELECT REPROVAR FROM GPERFILTIPOPER WHERE  GPERFILTIPOPER.CODEMPRESA = GTIPOPER.CODEMPRESA
                                                                        	AND GPERFILTIPOPER.CODTIPOPER = GTIPOPER.CODTIPOPER AND CODPERFIL = ?) REPROVAR,
                                                                        	(SELECT GERARBOLETO FROM GPERFILTIPOPER WHERE  GPERFILTIPOPER.CODEMPRESA = GTIPOPER.CODEMPRESA
                                                                        	AND GPERFILTIPOPER.CODTIPOPER = GTIPOPER.CODTIPOPER AND CODPERFIL = ?) GERARBOLETO
                                                                        FROM 
                                                                        	GTIPOPER
                                                                        WHERE
                                                                        	GTIPOPER.CODEMPRESA = ?", new object[]
                                                                          {
                                                                              codPerfil,
                                                                              codPerfil,
                                                                              codPerfil,
                                                                              codPerfil,
                                                                              codPerfil,
                                                                              codPerfil,
                                                                              codPerfil,
                                                                              codPerfil,
                                                                              codPerfil,
                                                                              codPerfil,
                                                                              codPerfil,
                                                                              codPerfil,
                                                                              codPerfil,
                                                                              codPerfil,
                                                                              AppLib.Context.Empresa
                                                                          });
            }

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    dtTipoOperacao = CriarSchemaTipoOperacao(dtTipoOperacao);

                    if (edita == false)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dtTipoOperacao.Rows.Add
                                (
                                dt.Rows[i]["Código do Tipo de Operação"].ToString(),
                                dt.Rows[i]["Descrição"].ToString(),
                                false,
                                false,
                                false,
                                false,
                                false,
                                false,
                                false,
                                false,
                                false,
                                false,
                                false,
                                false,
                                false,
                                false
                                );
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dtTipoOperacao.Rows.Add
                                (
                                dt.Rows[i]["Código do Tipo de Operação"].ToString(),
                                dt.Rows[i]["Descrição"].ToString(),
                                dt.Rows[i]["INCLUIR"],
                                dt.Rows[i]["EXCLUIR"],
                                dt.Rows[i]["ALTERAR"],
                                dt.Rows[i]["FATURAR"],
                                dt.Rows[i]["INCLUIRFAT"],
                                dt.Rows[i]["CONSULTAR"],
                                dt.Rows[i]["CANCELAR"],
                                dt.Rows[i]["CONCLUIR"],
                                dt.Rows[i]["APROVAR"],
                                dt.Rows[i]["APROVAFINCEIRO"],
                                dt.Rows[i]["APROVADESCONTO"],
                                dt.Rows[i]["APROVALIMITECREDITO"],
                                dt.Rows[i]["REPROVAR"],
                                dt.Rows[i]["GERARBOLETO"]
                                );
                        }
                    }
                }

                dtTipoOperacao = TratarPermissoesTipoOperacao(dtTipoOperacao);
            }

            return dtTipoOperacao;
        }

        private DataTable TratarPermissoesTipoOperacao(DataTable dtTipoOperacao)
        {
            for (int i = 0; i < dtTipoOperacao.Rows.Count; i++)
            {
                if (dtTipoOperacao.Rows[i]["Incluir"] == DBNull.Value)
                {
                    dtTipoOperacao.Rows[i]["Incluir"] = false;
                }

                if (dtTipoOperacao.Rows[i]["Excluir"] == DBNull.Value)
                {
                    dtTipoOperacao.Rows[i]["Excluir"] = false;
                }

                if (dtTipoOperacao.Rows[i]["Alterar"] == DBNull.Value)
                {
                    dtTipoOperacao.Rows[i]["Alterar"] = false;
                }

                if (dtTipoOperacao.Rows[i]["Faturar"] == DBNull.Value)
                {
                    dtTipoOperacao.Rows[i]["Faturar"] = false;
                }

                if (dtTipoOperacao.Rows[i]["Incluir Fatura"] == DBNull.Value)
                {
                    dtTipoOperacao.Rows[i]["Incluir Fatura"] = false;
                }

                if (dtTipoOperacao.Rows[i]["Consultar"] == DBNull.Value)
                {
                    dtTipoOperacao.Rows[i]["Consultar"] = false;
                }

                if (dtTipoOperacao.Rows[i]["Cancelar"] == DBNull.Value)
                {
                    dtTipoOperacao.Rows[i]["Cancelar"] = false;
                }

                if (dtTipoOperacao.Rows[i]["Concluir"] == DBNull.Value)
                {
                    dtTipoOperacao.Rows[i]["Concluir"] = false;
                }

                if (dtTipoOperacao.Rows[i]["Aprovar"] == DBNull.Value)
                {
                    dtTipoOperacao.Rows[i]["Aprovar"] = false;
                }

                if (dtTipoOperacao.Rows[i]["Aprovar Financeiro"] == DBNull.Value)
                {
                    dtTipoOperacao.Rows[i]["Aprovar Financeiro"] = false;
                }

                if (dtTipoOperacao.Rows[i]["Aprovar Desconto"] == DBNull.Value)
                {
                    dtTipoOperacao.Rows[i]["Aprovar Desconto"] = false;
                }

                if (dtTipoOperacao.Rows[i]["Aprovar Limite Crédito"] == DBNull.Value)
                {
                    dtTipoOperacao.Rows[i]["Aprovar Limite Crédito"] = false;
                }

                if (dtTipoOperacao.Rows[i]["Reprovar"] == DBNull.Value)
                {
                    dtTipoOperacao.Rows[i]["Reprovar"] = false;
                }

                if (dtTipoOperacao.Rows[i]["Gerar Boleto"] == DBNull.Value)
                {
                    dtTipoOperacao.Rows[i]["Gerar Boleto"] = false;
                }
            }

            return dtTipoOperacao;
        }

        public void ConfigurarEdicaoColunasGridView(GridView gridView)
        {
            gridView.Columns["Código do Tipo de Operação"].OptionsColumn.AllowEdit = false;
            gridView.Columns["Tipo de Operação"].OptionsColumn.AllowEdit = false;

            gridView.Columns["Incluir"].OptionsColumn.AllowEdit = true;
            gridView.Columns["Excluir"].OptionsColumn.AllowEdit = true;
            gridView.Columns["Alterar"].OptionsColumn.AllowEdit = true;
            gridView.Columns["Incluir Fatura"].OptionsColumn.AllowEdit = true;
            gridView.Columns["Consultar"].OptionsColumn.AllowEdit = true;
            gridView.Columns["Cancelar"].OptionsColumn.AllowEdit = true;
            gridView.Columns["Concluir"].OptionsColumn.AllowEdit = true;
            gridView.Columns["Aprovar Financeiro"].OptionsColumn.AllowEdit = true;
            gridView.Columns["Aprovar Desconto"].OptionsColumn.AllowEdit = true;
            gridView.Columns["Aprovar Limite Crédito"].OptionsColumn.AllowEdit = true;
            gridView.Columns["Reprovar"].OptionsColumn.AllowEdit = true;
            gridView.Columns["Gerar Boleto"].OptionsColumn.AllowEdit = true;
        }

        #endregion
    }
}
