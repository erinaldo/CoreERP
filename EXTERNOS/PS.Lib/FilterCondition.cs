using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PS.Lib
{
    public class FilterCondition
    {
        // OBJETOS
        private Data.DBS dbs = new Data.DBS();
        Global gb = new Global();

        // PROPERTIES
        public int codEmpresa { get; set; }
        public int idFiltro { get; set; }
        public int id { get; set; }
        public int ordem { get; set; }
        public string operador { get; set; }
        public object valor { get; set; }
        public string operadorlogicoText { get; set; }
        public string operadorlogicoValue { get; set; }
        public string operador2 { get; set; }
        public object valor2 { get; set; }
        public string campoValue { get; set; }
        public string campoText { get; set; }
        public string condicaoText { get; set; }
        public string condicaoValue { get; set; }

        public FilterCondition()
        {

        }

        public FilterCondition Copiar()
        {
            FilterCondition condicao = new FilterCondition();

            condicao.dbs = this.dbs;
            condicao.gb = this.gb;

            condicao.codEmpresa = this.codEmpresa;
            condicao.idFiltro = this.idFiltro;
            condicao.id = this.id;
            condicao.ordem = this.ordem;
            condicao.operador = this.operador;
            condicao.valor = this.valor;
            condicao.operadorlogicoText = this.operadorlogicoText;
            condicao.operadorlogicoValue = this.operadorlogicoValue;
            condicao.operador2 = this.operador2;
            condicao.valor2 = this.valor2;
            condicao.campoValue = this.campoValue;
            condicao.campoText = this.campoText;
            condicao.condicaoText = this.condicaoText;
            condicao.condicaoValue = this.condicaoValue;

            return condicao;
        }

        public void Salvar()
        {
            string sSql = "";
            int contador = 0;

            sSql = "SELECT COUNT(ID) CAMPO FROM GCONDICAO WHERE IDFILTRO = ? AND ID = ? AND CODEMPRESA = ?";

            contador = int.Parse(dbs.QueryValue(null, sSql, this.idFiltro, this.id, this.codEmpresa).ToString());

            if (contador == 0)
            {
                Incluir();
            }
            else
            {
                Alterar();
            }
        }

        private void Incluir()
        {
            string sSql = "";

            sSql = "INSERT INTO GCONDICAO (CODEMPRESA, IDFILTRO, ID, OPERADOR, VALOR, OPERADORLOGICO, ORDEM, OPERADOR2, VALOR2, CAMPO) " +
                   "VALUES (?,?,?,?,?,?,?,?,?,?)";

            dbs.QueryExec(sSql, this.codEmpresa, this.idFiltro, this.id, this.operador, this.valor, this.operadorlogicoText, this.ordem, this.operador2, this.valor2, this.campoValue);
     
        }

        private void Alterar()
        {
            string sSql = "";

            sSql = " UPDATE GCONDICAO SET " +
                   "  OPERADOR = ?, " +
                   "  VALOR = ?, " +
                   "  OPERADORLOGICO = ?, " +
                   "  ORDEM = ?, " +
                   "  OPERADOR2 = ?, " +
                   "  VALOR2 = ?, " +
                   "  CAMPO = ? " +
                   " WHERE CODEMPRESA = ? " +
                   "  AND IDFILTRO = ? " +
                   "  AND ID = ? ";

            dbs.QueryExec(sSql, this.operador, this.valor, this.operadorlogicoText, this.ordem, this.operador2, this.valor2, this.campoValue, this.codEmpresa, this.idFiltro, this.id);

        }

        public void Excluir()
        {
            string sSql = "";

            sSql = "DELETE FROM GCONDICAO WHERE IDFILTRO = ? AND ID = ? AND CODEMPRESA = ?";

            dbs.QueryExec(sSql, this.idFiltro, this.id, this.codEmpresa);
        }

        public String[] RetornaConsultaSQL(string valor)
        {
            string codColigada = "";
            int sepColigada = 0;
            string codAplicacao = "";
            int sepAplicacao = 0;
            string codSentenca = "";

            for (int i = 0; i < valor.Length; i++)
            {
                if (valor[i].ToString() == ":")
                {
                    codColigada = valor.Substring(0, i);
                    sepColigada = i;
                }

                if (valor[i].ToString() == ";")
                {
                    codAplicacao = valor.Substring(sepColigada + 1, (i - (sepColigada + 1)));
                    sepAplicacao = i;

                    codSentenca = valor.Substring(sepAplicacao + 1, (valor.Length - (sepAplicacao + 1)));
                }
            }

            string sSql = "SELECT * FROM GCONSSQL WHERE CODEMPRESA = ? AND APLICACAO = ? AND CODSENTENCA = ?";
            string titulo = "";
            string sentenca = "";

            DataTable dt = new DataTable();

            dt = dbs.QuerySelect(sSql, codColigada, codAplicacao, codSentenca);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                titulo = dt.Rows[i]["TITULO"].ToString().Trim();
                sentenca = dt.Rows[i]["SENTENCA"].ToString().Trim();
            }

            return new String[] { titulo, sentenca };
        }

        public void DefineConficao(List<DataField> campos)
        {
            // Tratamento de condições separados para facilitar na manutenção
            string param = " ? ";
            object parametro1 = null;
            object parametro2 = null; 

            DataField dfCAMPO = gb.RetornaDataFieldByCampo(campos, this.campoValue);
            Type ObjetoTipo = (Type)dfCAMPO.Tipo;
            string TipoCampo = ObjetoTipo.Name;

            if (TipoCampo == "PSTextoBox")
            {
                if(this.valor != null)
                    parametro1 = this.valor;
                if (this.valor2 != null)
                    parametro2 = this.valor2;
            }

            if (TipoCampo == "PSLookup")
            {
                if (this.valor != null)
                    parametro1 = this.valor;
                if (this.valor2 != null)
                    parametro2 = this.valor2;
            }

            if (TipoCampo == "PSMaskedTextBox")
            {
                if (this.valor != string.Empty)
                    parametro1 = this.valor;
                if (this.valor2 != string.Empty)
                    parametro2 = this.valor2;
            }

            if (TipoCampo == "PSMemoBox")
            {
                if (this.valor != string.Empty)
                    parametro1 = this.valor;
                if (this.valor2 != string.Empty)
                    parametro2 = this.valor2;
            }

            if (TipoCampo == "PSMoedaBox")
            {
                if (this.valor != string.Empty)
                    parametro1 = Convert.ToDouble(this.valor).ToString();
                if (this.valor2 != string.Empty)
                    parametro2 = Convert.ToDouble(this.valor2).ToString();
            }

            if (TipoCampo == "PSComboBox")
            {
                if (this.valor != string.Empty)
                    parametro1 = this.valor;
                if (this.valor2 != string.Empty)
                    parametro2 = this.valor2;
            }

            if (TipoCampo == "PSCheckBox")
            {
                if (this.valor != string.Empty)
                    parametro1 = this.valor;
                if (this.valor2 != string.Empty)
                    parametro2 = this.valor2;
            }

            if (TipoCampo == "PSDateBox")
            {
                if (this.valor != null)
                    parametro1 = this.valor;
                if (this.valor2 != null)
                    parametro2 = this.valor2;
            }

            if (this.operador.Equals("IN"))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + " ( " + RetornaConsultaSQL(this.valor.ToString())[1] + " ) ";
                this.condicaoText = this.campoText + " " + this.operador + " (" + RetornaConsultaSQL(this.valor.ToString())[0] + ")";
            }

            if (this.operador.Equals("BETWEEN"))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + " '" + this.valor + "' AND '" + this.valor2 + "' ";
                this.condicaoText = this.campoText + " " + this.operador + " " + parametro1 + " E " + parametro2;
            }

            if (this.operador.Equals("IS NOT NULL"))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador;
                //this.condicaoText = this.campoText + " " + this.operador;
                this.condicaoText = this.campoText + " Não Nulo";
            }

            if (this.operador.Equals("IS NULL"))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador;
                //this.condicaoText = this.campoText + " " + this.operador;
                this.condicaoText = this.campoText + " Nulo";
            }

            if (this.operador.Equals("LIKE"))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + " '" + this.valor + "' ";
                //this.condicaoText = this.campoText + " " + this.operador + " " + this.valor;
                this.condicaoText = this.campoText + " Contem " + this.valor;
            }

            if (this.operador.Equals("NOT LIKE"))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + " '" + this.valor + "' ";
                //this.condicaoText = this.campoText + " " + this.operador + " " + this.valor;
                this.condicaoText = this.campoText + " Não Contem " + this.valor;
            }

            if (this.operador.Equals("="))
            {
                //this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + " '" + this.valor + "' ";
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + param;
                this.condicaoText = this.campoText + " " + this.operador + " " + parametro1;
            }

            if (this.operador.Equals(">"))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + param;
                this.condicaoText = this.campoText + " " + this.operador + " " + parametro1;
            }

            if (this.operador.Equals("<"))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + param;
                this.condicaoText = this.campoText + " " + this.operador + " " + parametro1;
            }

            if (this.operador.Equals("<>"))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + param;
                this.condicaoText = this.campoText + " " + this.operador + " " + parametro1;
            }

            if (this.operador.Equals(">="))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + param;
                this.condicaoText = this.campoText + " " + this.operador + " " + parametro1;
            }

            if (this.operador.Equals("<="))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + param;
                this.condicaoText = this.campoText + " " + this.operador + " " + parametro1;
            }
        }

        public void DefineConficao()
        {
            // Tratamento de condições separados para facilitar na manutenção
            string param = " ? ";

            if (this.operador.Equals("IN"))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + " ( " + RetornaConsultaSQL(this.valor.ToString())[1] + " ) ";
                this.condicaoText = this.campoText + " " + this.operador + " (" + RetornaConsultaSQL(this.valor.ToString())[0] + ")";
            }

            if (this.operador.Equals("BETWEEN"))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + " '" + this.valor + "' AND '" + this.valor2 + "' ";
                this.condicaoText = this.campoText + " " + this.operador + " " + this.valor + " E " + this.valor2;
            }

            if (this.operador.Equals("IS NOT NULL"))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador;
                //this.condicaoText = this.campoText + " " + this.operador;
                this.condicaoText = this.campoText + " Não Nulo";
            }

            if (this.operador.Equals("IS NULL"))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador;
                //this.condicaoText = this.campoText + " " + this.operador;
                this.condicaoText = this.campoText + " Nulo";
            }

            if (this.operador.Equals("LIKE"))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + " '" + this.valor + "' ";
                //this.condicaoText = this.campoText + " " + this.operador + " " + this.valor;
                this.condicaoText = this.campoText + " Contem " + this.valor;
            }

            if (this.operador.Equals("NOT LIKE"))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + " '" + this.valor + "' ";
                //this.condicaoText = this.campoText + " " + this.operador + " " + this.valor;
                this.condicaoText = this.campoText + " Não Contem " + this.valor;
            }

            if (this.operador.Equals("="))
            {
                //this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + " '" + this.valor + "' ";
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + param;
                this.condicaoText = this.campoText + " " + this.operador + " " + this.valor;
            }

            if (this.operador.Equals(">"))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + param;
                this.condicaoText = this.campoText + " " + this.operador + " " + this.valor;
            }

            if (this.operador.Equals("<"))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + param;
                this.condicaoText = this.campoText + " " + this.operador + " " + this.valor;
            }

            if (this.operador.Equals("<>"))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + param;
                this.condicaoText = this.campoText + " " + this.operador + " " + this.valor;
            }

            if (this.operador.Equals(">="))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + param;
                this.condicaoText = this.campoText + " " + this.operador + " " + this.valor;
            }

            if (this.operador.Equals("<="))
            {
                this.condicaoValue = this.operadorlogicoValue + this.campoValue + " " + this.operador + param;
                this.condicaoText = this.campoText + " " + this.operador + " " + this.valor;
            }
        }

        public int BuscaProximoId()
        {
            string sSql = "";
            int id = 0;

            sSql = "SELECT ISNULL(MAX(ID),0) FROM GCONDICAO WHERE CODEMPRESA = ? AND IDFILTRO = ?";

            id = int.Parse(dbs.QueryValue("0", sSql, this.codEmpresa, this.idFiltro).ToString());

            return id;
        }

        public bool ExisteParametro()
        {
            /*
            int indiceAbre = 0;
            int indiceFecha = 0;

            for (int i = 0; i < this.valor.Length; i++)
            {
                if (this.valor[i].ToString() == "[")
                {
                    indiceAbre = i;
                }

                if (this.valor[i].ToString() == "]")
                {
                    indiceFecha = i;
                }
            }

            if (indiceAbre >= indiceFecha)
                return false;
            else
                return true;
            */
            return false;
        }
    }
}
