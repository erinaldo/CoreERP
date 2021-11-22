using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PS.Lib
{
    public class Filter : ICloneable
    {
        // OBJETOS
        private Data.DBS dbs = new Data.DBS();
        private Global gb = new Global();

        // PROPERTIES
        public int codEmpresa { get; set; }
        public int id { get; set; }
        public string tabela { get; set; }
        public string descricao { get; set; }
        public string codUsuario { get; set; }
        public int selecionado { get; set; }
        public string condicao { get; set; }
        public int padrao { get; set; }
        public List<FilterCondition> listaCondicao { get; set; }

        public Filter()
        {

        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public Filter Copiar()
        {
            Filter filter = new Filter();

            filter.dbs = this.dbs;
            filter.gb = this.gb;

            filter.codEmpresa = this.codEmpresa;
            filter.id = this.id;
            filter.tabela = this.tabela;
            filter.descricao = this.descricao;
            filter.codUsuario = this.codUsuario;
            filter.selecionado = this.selecionado;
            filter.condicao = this.condicao;
            filter.padrao = this.padrao;

            filter.listaCondicao = new List<FilterCondition>();

            if (this.listaCondicao != null)
            {
                for (int i = 0; i < this.listaCondicao.Count; i++)
                {
                    filter.listaCondicao.Add(this.listaCondicao[i].Copiar());
                }
    
            }
            
            return filter;
        }

        private Boolean VerificaDuplicidade()
        {
            string sSql = "";
            int contador = 0;

            sSql = "SELECT COUNT(ID) CAMPO FROM GFILTRO WHERE TABELA = ? AND DESCRICAO = ? AND CODEMPRESA = ? AND ID <> ? AND CODUSUARIO = ?";

            contador = int.Parse(dbs.QueryValue(null, sSql, this.tabela, this.descricao, this.codEmpresa, this.id, this.codUsuario).ToString());

            if (contador > 0)
                return true;
            else
                return false;
        }

        private void Validar()
        {
            if ((this.descricao == "") || (this.descricao == null))
            {
                throw new Exception("Operação Cancelada. Informe o nome do filtro.");
            }

            if (VerificaDuplicidade())
            {
                throw new Exception("Operação Cancelada. Já existe um filtro com esse nome.");
            }

            if (this.listaCondicao == null)
            {
                throw new Exception("Operação Cancelada. O filtro deve conter no mínimo uma condição.");
            }

            if (this.listaCondicao.Count <= 0)
            {
                throw new Exception("Operação Cancelada. O filtro deve conter no mínimo uma condição.");
            }
        }

        private void Incluir()
        {
            string sSql = "";

            sSql = "INSERT INTO GFILTRO (CODEMPRESA, ID, CODMODULO, TABELA, DESCRICAO, CODUSUARIO) " +
                   "VALUES (?,?,?,?,?,?)";

            dbs.QueryExec(sSql, this.codEmpresa, this.id, Contexto.Session.CodModulo, this.tabela, this.descricao, this.codUsuario);

            for (int i = 0; i < this.listaCondicao.Count; i++)
            {
                FilterCondition condicao = new FilterCondition();
                condicao = listaCondicao[i];
                condicao.Salvar();
            }
        }

        private void Alterar()
        {
            string sSql = "";

            sSql = " UPDATE GFILTRO SET " +
                   "  CODMODULO = ?, " +
                   "  TABELA = ?, " +
                   "  DESCRICAO = ?, " +
                   "  CODUSUARIO = ? "  +
                   " WHERE CODEMPRESA = ? " +
                   "  AND ID = ? ";

            dbs.QueryExec(sSql, Contexto.Session.CodModulo, this.tabela, this.descricao, this.codUsuario, this.codEmpresa, this.id);

            for (int i = 0; i < this.listaCondicao.Count; i++)
            {
                FilterCondition condicao = new FilterCondition();
                condicao = listaCondicao[i];
                condicao.Salvar();
            }
        }

        public void Salvar()
        {
            Validar();

            try
            {
                //dbs.Begin();

                ExcluirCondicao();

                string sSql = "";
                int contador = 0;

                sSql = "SELECT COUNT(ID) CAMPO FROM GFILTRO WHERE ID = ? AND CODEMPRESA = ?";

                contador = int.Parse(dbs.QueryValue(null, sSql, this.id, this.codEmpresa).ToString());

                if (contador == 0)
                {
                    Incluir();
                }
                else
                {
                    Alterar();
                }

                //dbs.Commit();
            }
            catch (Exception ex)
            {
                //dbs.Rollback();
                throw new Exception(ex.Message);                
            }
        }

        public void Excluir()
        {
            string sSql = "DELETE FROM GFILTRO WHERE ID = ? AND CODEMPRESA = ?";

            for (int i = 0; i < this.listaCondicao.Count; i++)
            {
                FilterCondition condicao = new FilterCondition();
                condicao = listaCondicao[i];
                condicao.Excluir();
            }

            dbs.QueryExec(sSql, this.id, this.codEmpresa);
        }

        public void ExcluirCondicao()
        {
            string sSql = "DELETE FROM GCONDICAO WHERE IDFILTRO = ? AND CODEMPRESA = ?";

            dbs.QueryExec(sSql, this.id, this.codEmpresa);
        }

        public int BuscaProximoId()
        {
            string sSql = "";
            int id = 0;

            sSql = "SELECT ISNULL(MAX(ID) + 1,1) FROM GFILTRO WHERE CODEMPRESA = ?";

            id = int.Parse(dbs.QueryValue(null, sSql, this.codEmpresa).ToString());

            return id;
        }

        public void BuscaCondicao(List<DataField> campos)
        {
            string sSql = "SELECT * FROM GCONDICAO WHERE IDFILTRO = ? AND CODEMPRESA = ? ORDER BY ORDEM";

            string operadorlogico = string.Empty;
            string operadorcondicional = string.Empty;
            string strCondicao = string.Empty;

            DataTable dt = new DataTable();

            dt = dbs.QuerySelect(sSql, this.id, this.codEmpresa);

            List<FilterCondition> ListaCondicaoFiltro = new List<FilterCondition>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // Define as variáveis
                operadorcondicional = dt.Rows[i]["OPERADORLOGICO"].ToString().Trim();

                if (dt.Rows[i]["OPERADORLOGICO"].ToString().Trim().Equals("E"))
                    operadorlogico = " AND ";

                if (dt.Rows[i]["OPERADORLOGICO"].ToString().Trim().Equals("OU"))
                    operadorlogico = " OR ";

                if (i == 0)
                {
                    operadorlogico = string.Empty;
                    operadorcondicional = string.Empty;
                }

                // Define o objeto condicao de filtro usado para criar uma lista de condições de filtro

                FilterCondition cf = new FilterCondition();
                cf.codEmpresa = this.codEmpresa;
                cf.idFiltro = this.id;
                cf.id = int.Parse(dt.Rows[i]["ID"].ToString());
                cf.ordem = int.Parse(dt.Rows[i]["ORDEM"].ToString());
                cf.operador = dt.Rows[i]["OPERADOR"].ToString().Trim();
                cf.valor = dt.Rows[i]["VALOR"].ToString().Trim();
                cf.operadorlogicoText = operadorcondicional;
                cf.operadorlogicoValue = operadorlogico;
                cf.operador2 = dt.Rows[i]["OPERADOR2"].ToString().Trim();
                cf.valor2 = dt.Rows[i]["VALOR2"].ToString().Trim();
                cf.campoValue = dt.Rows[i]["CAMPO"].ToString().Trim();
                cf.campoText = gb.NomeDoCampo(this.tabela, cf.campoValue);
                cf.DefineConficao(campos);

                strCondicao = strCondicao + cf.condicaoValue + " ";

                // Adiciona o objeto a lista de condição de filtro
                ListaCondicaoFiltro.Add(cf);
            }
            
            this.condicao = strCondicao;
            this.listaCondicao = ListaCondicaoFiltro;
        }

        public void BuscaCondicao()
        {
            string sSql = "SELECT * FROM GCONDICAO WHERE IDFILTRO = ? AND CODEMPRESA = ? ORDER BY ORDEM";

            string operadorlogico = string.Empty;
            string operadorcondicional = string.Empty;
            string strCondicao = string.Empty;

            DataTable dt = new DataTable();

            dt = dbs.QuerySelect(sSql, this.id, this.codEmpresa);

            List<FilterCondition> ListaCondicaoFiltro = new List<FilterCondition>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // Define as variáveis
                operadorcondicional = dt.Rows[i]["OPERADORLOGICO"].ToString().Trim();

                if (dt.Rows[i]["OPERADORLOGICO"].ToString().Trim().Equals("E"))
                    operadorlogico = " AND ";

                if (dt.Rows[i]["OPERADORLOGICO"].ToString().Trim().Equals("OU"))
                    operadorlogico = " OR ";

                if (i == 0)
                {
                    operadorlogico = string.Empty;
                    operadorcondicional = string.Empty;
                }

                // Define o objeto condicao de filtro usado para criar uma lista de condições de filtro

                FilterCondition cf = new FilterCondition();
                cf.codEmpresa = this.codEmpresa;
                cf.idFiltro = this.id;
                cf.id = int.Parse(dt.Rows[i]["ID"].ToString());
                cf.ordem = int.Parse(dt.Rows[i]["ORDEM"].ToString());
                cf.operador = dt.Rows[i]["OPERADOR"].ToString().Trim();
                cf.valor = dt.Rows[i]["VALOR"].ToString().Trim();
                cf.operadorlogicoText = operadorcondicional;
                cf.operadorlogicoValue = operadorlogico;
                cf.operador2 = dt.Rows[i]["OPERADOR2"].ToString().Trim();
                cf.valor2 = dt.Rows[i]["VALOR2"].ToString().Trim();
                cf.campoValue = dt.Rows[i]["CAMPO"].ToString().Trim();
                cf.campoText = gb.NomeDoCampo(this.tabela, cf.campoValue);
                cf.DefineConficao();

                strCondicao = strCondicao + cf.condicaoValue + " ";

                // Adiciona o objeto a lista de condição de filtro
                ListaCondicaoFiltro.Add(cf);
            }

            this.condicao = strCondicao;
            this.listaCondicao = ListaCondicaoFiltro;
        }

        public bool ExisteParametro()
        {
            for (int j = 0; j < this.listaCondicao.Count; j++)
            {
                if (this.listaCondicao[j].ExisteParametro())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
