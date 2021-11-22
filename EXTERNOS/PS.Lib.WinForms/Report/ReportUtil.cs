using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib.WinForms.Report
{
    public class ReportUtil
    {
        private Data.DBS dbs = new Data.DBS();
        private StaticReport.PSPartStaticReport Report = new StaticReport.PSPartStaticReport(); 
        private PSInstance Instance = new PSInstance();

        public ReportUtil()
        { 
        
        }

        public List<StaticReport.PSPartStaticReport> LoadStaticReportList(string codPSPart, string CodTipoOper)
        {
            List<StaticReport.PSPartStaticReport> lista = new List<StaticReport.PSPartStaticReport>();

            string sSql = @"SELECT GREPORT.* FROM GREPORT, GTIPOPERREPORT 
                                WHERE GREPORT.CODEMPRESA = ? AND GREPORT.CODPSPART = ? 
                                AND GREPORT.CODEMPRESA = GTIPOPERREPORT.CODEMPRESA
                                AND GREPORT.CODREPORT = GTIPOPERREPORT.CODREPORT
                                AND GTIPOPERREPORT.CODTIPOPER = ?";

            System.Data.DataTable dt = dbs.QuerySelect(sSql, PS.Lib.Contexto.Session.Empresa.CodEmpresa, codPSPart, CodTipoOper);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string AssemplyName = dt.Rows[i]["ASSEMBLYNAME"].ToString();
                    string NameSpace = dt.Rows[i]["NAMESPACE"].ToString();
                    string TypeName = dt.Rows[i]["CLASSNAME"].ToString();

                    Report = (StaticReport.PSPartStaticReport)Instance.CreateInstance("Reports", AssemplyName, NameSpace, TypeName);

                    lista.Add(Report);
                }
            }

            return lista;  
        }

        public List<StaticReport.PSPartStaticReport> LoadStaticReportList(string codPSPart)
        {
            List<StaticReport.PSPartStaticReport> lista = new List<StaticReport.PSPartStaticReport>();

            string sSql = "SELECT * FROM GREPORT WHERE CODEMPRESA = ? AND CODPSPART = ?";

            System.Data.DataTable dt = dbs.QuerySelect(sSql, PS.Lib.Contexto.Session.Empresa.CodEmpresa, codPSPart);

            if (dt.Rows.Count > 0)
            {
                for(int i = 0; i < dt.Rows.Count; i++)
                {
                    string AssemplyName = dt.Rows[i]["ASSEMBLYNAME"].ToString();
                    string NameSpace = dt.Rows[i]["NAMESPACE"].ToString();
                    string TypeName = dt.Rows[i]["CLASSNAME"].ToString();

                    Report = (StaticReport.PSPartStaticReport)Instance.CreateInstance("Reports", AssemplyName, NameSpace, TypeName);

                    lista.Add(Report);
                }
            }

            return lista;            
        }

        public StaticReport.PSPartStaticReport LoadStaticReportList(int codEmpresa, string codReport)
        {
            string sSql = "SELECT * FROM GREPORT WHERE CODEMPRESA = ? AND CODREPORT = ?";

            System.Data.DataTable dt = dbs.QuerySelect(sSql, codEmpresa, codReport);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string AssemplyName = dt.Rows[i]["ASSEMBLYNAME"].ToString();
                    string NameSpace = dt.Rows[i]["NAMESPACE"].ToString();
                    string TypeName = dt.Rows[i]["CLASSNAME"].ToString();

                    Report = (StaticReport.PSPartStaticReport)Instance.CreateInstance("Reports", AssemplyName, NameSpace, TypeName);
                }
            }

            return Report;
        }
    }
}
