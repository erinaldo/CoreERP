using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib.OBJETOS
{
    public class EventoCorrecaoNFParams:ParamsBase
    {

        public int IDEVENTOCORRECAO { get; set; }
        public DateTime DATA { get; set; }
        public string CNPJEMITENTE { get; set; }
        public string CHNFE { get; set; }
        public string NPROTAUT { get; set; }
        public string XJUST { get; set; }
        public string CODSTATUS { get; set; }
        public string LOG { get; set; }
        public DateTime? DATAULTIMOLOG { get; set; }
        public int IDOUTBOX { get; set; }
        public string XMLENV { get; set; }
        public string NPROT { get; set; }
        public string XMLPROT { get; set; }
        public int NSEQITEM { get; set; }

        public void save()
        {
            if (this.IDEVENTOCORRECAO == 0)
            {
                //INSERT
                AppLib.Context.poolConnection.Get("Start").ExecTransaction(@"INSERT INTO ZEVENTOCORRECAONF 
(DATA, CNPJEMITENTE, CHNFE, NPROTAUT, XJUST, CODSTATUS, LOG, DATAULTIMOLOG, IDOUTBOX, XMLENV, NPROT, XMLPROT, NSEQITEM) 
VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?) ", new object[] {this.DATA, this.CNPJEMITENTE, this.CHNFE, this.NPROTAUT, this.XJUST, this.CODSTATUS, this.LOG,  this.DATAULTIMOLOG, this.IDOUTBOX, this.XMLENV, this.NPROT, this.XMLPROT, this.NSEQITEM });

            }
            else
            {
                //UPDATE
                AppLib.Context.poolConnection.Get("Start").ExecTransaction("UPDATE ZEVENTOCORRECAONF SET DATA = ?, CNPJEMITENTE = ?, CHNFE = ?, NPROTAUT = ?, XJUST = ?, CODSTATUS = ?, LOG = ?, DATAULTIMOLOG = ?, IDOUTBOX = ?, XMLENV = ?, NPROT = ?, XMLPROT = ?, NSEQITEM = ? WHERE IDEVENTOCORRECAO = ?", new object[] { this.DATA, this.CNPJEMITENTE, this.CHNFE, this.NPROTAUT, this.XJUST,  this.CODSTATUS, this.LOG, this.DATAULTIMOLOG, this.IDOUTBOX, this.XMLENV, this.NPROT, this.XMLPROT, this.NSEQITEM, this.IDEVENTOCORRECAO });
            }
        }

        public List<EventoCorrecaoNFParams> getRegistrosPendentes(EmpresaParams empresa)
        {
            List<EventoCorrecaoNFParams> _list = new List<EventoCorrecaoNFParams>();
            string sSql = @"SELECT * FROM ZEVENTOCORRECAONF
WHERE REPLACE(REPLACE(REPLACE(CNPJEMITENTE, '.', ''), '/', ''), '-', '') = REPLACE(REPLACE(REPLACE(?, '.', ''), '/', ''), '-', '') 
    AND CODSTATUS = 'ENV'";

            try
            {
                System.Data.DataTable _dados = AppLib.Context.poolConnection.Get().ExecQuery(sSql, empresa.CNPJ);
                foreach (System.Data.DataRow row in _dados.Rows)
                {
                    _list.Add(EventoCorrecaoNFParams.ReadByIDEventoCorrecaoNF(Convert.ToInt32(row["IDEVENTOCORRECAO"].ToString())));
                }
            }
            catch (Exception ex)
            {
                ValidateLib.Log.SalvarLog("EventoCanNFParams.GetArquivosPendentes", ex.Message);
            }

            return _list;
        }

        public static EventoCorrecaoNFParams ReadByIDEventoCorrecaoNF(int _IDEVENTOCORRECAO)
        {
            EventoCorrecaoNFParams evento = new EventoCorrecaoNFParams();

            EventoCorrecaoNFParams EventoCorrecaoNFParams = new EventoCorrecaoNFParams();
            string sSql = @"SELECT * FROM ZEVENTOCORRECAONF WHERE IDEVENTOCORRECAO = ?";
            System.Data.DataTable dt = AppLib.Context.poolConnection.Get().ExecQuery(sSql, new object[] { _IDEVENTOCORRECAO });
            if (dt.Rows.Count > 0)
            {
                evento.CHNFE = dt.Rows[0]["CHNFE"].ToString();
                evento.CNPJEMITENTE = dt.Rows[0]["CNPJEMITENTE"].ToString();
                evento.CODSTATUS = dt.Rows[0]["CODSTATUS"].ToString();
                evento.DATA = Convert.ToDateTime(dt.Rows[0]["DATA"].ToString());
                evento.DATAULTIMOLOG = null;
                evento.IDEVENTOCORRECAO = _IDEVENTOCORRECAO;
                evento.IDOUTBOX = Convert.ToInt32(dt.Rows[0]["IDOUTBOX"].ToString());
                evento.LOG = dt.Rows[0]["LOG"].ToString();
                evento.NPROT = dt.Rows[0]["NPROT"].ToString();
                evento.NPROTAUT = dt.Rows[0]["NPROTAUT"].ToString();
                evento.XJUST = dt.Rows[0]["XJUST"].ToString();
                evento.XMLENV = dt.Rows[0]["XMLENV"].ToString();
                evento.XMLPROT = dt.Rows[0]["XMLPROT"].ToString();
                evento.NSEQITEM = Convert.ToInt32(dt.Rows[0]["NSEQITEM"]);
            }


            return evento;
        }

        public static EventoCorrecaoNFParams ReadyByIdOutBox(params object[] parameters)
        {
            EventoCorrecaoNFParams EventoCorrecaoNFParams = new EventoCorrecaoNFParams();
            string sSql = @"SELECT * FROM ZEVENTOCORRECAONF WHERE IDOUTBOX = ?";
            EventoCorrecaoNFParams.ReadFromCommand(sSql, parameters);
            return EventoCorrecaoNFParams;
        }

    }
}
