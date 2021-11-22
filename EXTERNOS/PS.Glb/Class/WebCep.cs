using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PS.Glb.Class
{
    public class WebCep
    {
        // João Pedro Luchiari = 29/01/2018

        string _uf;
        string _cidade;
        string _bairro;
        string _tipo_lagradouro;
        string _lagradouro;
        string _resultado;
        string _resultado_txt;

        public string UF
        {
            get { return _uf; }
        }

        public string Cidade
        {
            get { return _cidade; }
        }

        public string Bairro
        {
            get { return _bairro; }
        }

        public string TipoLagradouro
        {
            get { return _tipo_lagradouro; }
        }

        public string Lagradouro
        {
            get { return _lagradouro; }
        }

        public string Resultado
        {
            get { return _resultado; }
        }

        public string ResultadoTXT
        {
            get { return _resultado_txt; }
        }

        /// <summary>
        /// Método para fazer a busca do CEP.
        /// </summary>
        /// <param name="CEP">CEP que será utilizado para a busca</param>
        public WebCep(string CEP)
        {
            _uf = "";
            _cidade = "";
            _bairro = "";
            _tipo_lagradouro = "";
            _lagradouro = "";
            _resultado = "0";
            _resultado_txt = "CEP não encontrado";

            DataSet ds = new DataSet();

            ds.ReadXml("http://cep.republicavirtual.com.br/web_cep.php?cep=" + CEP.Replace("-", "").Trim() + "&formato=xml");

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    _resultado = ds.Tables[0].Rows[0]["resultado"].ToString();

                    switch (_resultado)
                    {
                        case "1":
                            _uf = ds.Tables[0].Rows[0]["uf"].ToString().Trim();
                            _cidade = ds.Tables[0].Rows[0]["cidade"].ToString().Trim();
                            _bairro = ds.Tables[0].Rows[0]["bairro"].ToString().Trim();
                            _tipo_lagradouro = ds.Tables[0].Rows[0]["tipo_logradouro"].ToString().Trim();
                            _lagradouro = ds.Tables[0].Rows[0]["logradouro"].ToString().Trim();
                            _resultado_txt = "CEP completo";
                            break;
                        case "2":
                            _uf = ds.Tables[0].Rows[0]["uf"].ToString().Trim();
                            _cidade = ds.Tables[0].Rows[0]["cidade"].ToString().Trim();
                            _bairro = "";
                            _tipo_lagradouro = "";
                            _lagradouro = "";
                            _resultado_txt = "CEP único";
                            break;
                        default:
                            _uf = "";
                            _cidade = "";
                            _bairro = "";
                            _tipo_lagradouro = "";
                            _lagradouro = "";
                            _resultado_txt = "CEP não encontrado";
                            break;
                    }
                }
            }
        }
    }
}
