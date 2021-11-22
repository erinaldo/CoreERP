using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PS.Validate.Services
{
    public class ConsultaEventos
    {
        private PS.Lib.Global gb = new PS.Lib.Global();
        private PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private AppLib.Data.Connection _conn;


        public int idOutBox { get; set; }
        public string Evento { get; set; }
        public string protocolo { get; set; }
        public DateTime? dataProtocolo { get; set; }
        public string xmlEnv { get; set; }
        public string xmlProt { get; set; }
        public string recibo { get; set; }
        public DateTime? dataRecibo { get; set; }
        public string codStatus { get; set; }
        public string Motivo { get; set; }
        public int nseqItem { get; set; }
        public string Chave { get; set; }
        public string xmlRec { get; set; }

        public ConsultaEventos()
        {

        }
        private void InitValidateServer()
        {
            System.Data.DataRow PARAMVAREJO = gb.RetornaParametrosVarejo();
            if (PARAMVAREJO == null)
            {
                throw new Exception("Não foi possível carregar os parâmetros do módulo.");
            }
            else
            {
                string ServerName = (PARAMVAREJO["VALIDATESERVERNAME"] == DBNull.Value) ? null : PARAMVAREJO["VALIDATESERVERNAME"].ToString();
                string DataBaseName = (PARAMVAREJO["VALIDATEDATABASENAME"] == DBNull.Value) ? null : PARAMVAREJO["VALIDATEDATABASENAME"].ToString();
                string UserName = (PARAMVAREJO["VALIDATEUSERNAME"] == DBNull.Value) ? null : PARAMVAREJO["VALIDATEUSERNAME"].ToString();
                string Password = (PARAMVAREJO["VALIDATEPASSWORD"] == DBNull.Value) ? null : PARAMVAREJO["VALIDATEPASSWORD"].ToString();

                if (string.IsNullOrEmpty(ServerName) || string.IsNullOrEmpty(DataBaseName) || string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                {
                    throw new Exception("A integração do Validate não está parametrizada corretamente, verifique se todos os campos foram preenchidos.");
                }
                else
                {
                    AppLib.Context.poolConnection.Add("Start", AppLib.Global.Types.Database.SqlClient, new AppLib.Data.AssistantConnection().SqlClient(ServerName, DataBaseName, UserName, Password));

                    Boolean testou = AppLib.Context.poolConnection.Get("Start").Test();

                    if (testou)
                    {
                        _conn = AppLib.Context.poolConnection.Get("Start");
                    }
                    else
                    {
                        throw new Exception("Erro de conexão com o banco de dados do APP VALIDATE");
                    }
                }
            }
        }

        public List<ConsultaEventos> ConsultaEventosNfe(int _idOutBox)
        {
            InitValidateServer();

            List<ConsultaEventos> lista = new List<ConsultaEventos>();
            //CCe
            System.Data.DataTable dtCCE = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM ZEVENTOCORRECAONF WHERE IDOUTBOX = ?", new object[] { _idOutBox });
            for (int i = 0; i < dtCCE.Rows.Count; i++)
            {
                ConsultaEventos con = new ConsultaEventos();
                con.Evento = "CCE";
                con.protocolo = dtCCE.Rows[i]["NPROT"].ToString();
                if (dtCCE.Rows[i]["DATAULTIMOLOG"] != DBNull.Value)
                {
                    con.dataProtocolo = Convert.ToDateTime(dtCCE.Rows[i]["DATAULTIMOLOG"]);
                }
              
                con.xmlEnv = dtCCE.Rows[i]["XMLENV"].ToString();
                con.xmlProt = dtCCE.Rows[i]["XMLPROT"].ToString();
                con.codStatus = dtCCE.Rows[i]["CODSTATUS"].ToString();
                con.nseqItem = Convert.ToInt32(dtCCE.Rows[i]["NSEQITEM"]);
                con.idOutBox = Convert.ToInt32(dtCCE.Rows[i]["IDOUTBOX"]);
                lista.Add(con);
            }
          
            //Can
            System.Data.DataTable dtCAN = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM ZEVENTOCANNF WHERE IDOUTBOX = ?", new object[] { _idOutBox });
            if (dtCAN.Rows.Count > 0)
            {
                ConsultaEventos con = new ConsultaEventos();
                con.Evento = "CAN";
                con.protocolo = dtCAN.Rows[0]["NPROT"].ToString();
                if (dtCAN.Rows[0]["DATAULTIMOLOG"].ToString() == string.Empty)
                {
                    con.dataProtocolo = null;
                }
                else
                {
                    con.dataProtocolo = Convert.ToDateTime(dtCAN.Rows[0]["DATAULTIMOLOG"]);
                }
                
                con.xmlEnv = dtCAN.Rows[0]["XMLENV"].ToString();
                con.xmlEnv = dtCAN.Rows[0]["XMLPROT"].ToString();
                lista.Add(con);
            }
            //Nfe
            System.Data.DataTable dtNFE = AppLib.Context.poolConnection.Get("Start").ExecQuery("SELECT * FROM ZNFEDOC INNER JOIN ZOUTBOX ON ZNFEDOC.IDOUTBOX = ZOUTBOX.IDOUTBOX WHERE ZNFEDOC.IDOUTBOX = ?", new object[] { _idOutBox });
            if (dtNFE.Rows.Count > 0)
            {
                ConsultaEventos con = new ConsultaEventos();
                con.Evento = "NFE";
                con.protocolo = dtNFE.Rows[0]["NPROT"].ToString();
                if (!string.IsNullOrEmpty(dtNFE.Rows[0]["DATANPROT"].ToString()))
                {
                     con.dataProtocolo = Convert.ToDateTime(dtNFE.Rows[0]["DATANPROT"]);
                }
                else
                {
                    con.dataProtocolo = null;
                }

                con.xmlEnv = dtNFE.Rows[0]["XMLNFE"].ToString();
                con.xmlProt = dtNFE.Rows[0]["XMLPROT"].ToString();
                con.xmlRec = dtNFE.Rows[0]["XMLREC"].ToString();
                con.recibo = dtNFE.Rows[0]["NREC"].ToString();
                con.Chave = dtNFE.Rows[0]["CHAVE"].ToString();
                con.dataRecibo = Convert.ToDateTime(dtNFE.Rows[0]["DATANREC"]);
                con.codStatus = dtNFE.Rows[0]["CODSTATUS"].ToString();
                lista.Add(con);
            }
            return lista;
        }
    }
}
