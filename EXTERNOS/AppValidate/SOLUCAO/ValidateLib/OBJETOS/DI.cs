using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib.OBJETOS
{
   public class DI
    {
        public DI()
        { }
        public int IDOUTBOX { get; set; }
        public int NITEM { get; set; }
        public string NDI { get; set; }
        public string DDI { get; set; }
        public string XLOCDESEMB { get; set; }
        public string UFDESEMB { get; set; }
        public string DDESEMB { get; set; }
        public string CEXPORTADOR { get; set; }
        public string TPVIATRANSP { get; set; }
        public decimal VAFRMM { get; set; }
        public string TPINTERMEDIO { get; set; }
        public string CNPJ { get; set; }
        public string UFTERCEIRO { get; set; }
        public ADI ADI { get; set; }
        public string VDESCDI { get; set; }
       
        public void save()
        {
            AppLib.Data.Connection _conn = AppLib.Context.poolConnection.Get("Start").Clone();
            _conn.BeginTransaction();
            try
            {
                AppLib.ORM.Jit ZNFEIMPORTA = new AppLib.ORM.Jit(_conn, "ZNFEIMPORTA");
                ZNFEIMPORTA.Set("IDOUTBOX", IDOUTBOX);
                ZNFEIMPORTA.Set("NITEM", NITEM);
                ZNFEIMPORTA.Set("NDI", NDI);

                ZNFEIMPORTA.Set("XLOCDESEMB", XLOCDESEMB);
                ZNFEIMPORTA.Set("UFDESEMB", UFDESEMB);
                if (!string.IsNullOrEmpty(DDESEMB))
                {
                    ZNFEIMPORTA.Set("DDESEMB", Convert.ToDateTime(Convert.ToDateTime(DDESEMB).ToShortDateString()));
                }
                if (!string.IsNullOrEmpty(DDI))
                {
                    ZNFEIMPORTA.Set("DDI", Convert.ToDateTime(Convert.ToDateTime(DDI).ToShortDateString()));
                }
                ZNFEIMPORTA.Set("CEXPORTADOR", CEXPORTADOR);
                ZNFEIMPORTA.Set("NADICAO", ADI.NADICAO);
                ZNFEIMPORTA.Set("NSEQADIC", ADI.NSEQADIC);
                ZNFEIMPORTA.Set("CFABRICANTE", ADI.CFABRICANTE);
                ZNFEIMPORTA.Set("NDRAW", ADI.NDRAW);
                ZNFEIMPORTA.Set("VDESCDI", Convert.ToDecimal(VDESCDI));
                ZNFEIMPORTA.Set("TPVIATRANSP", TPVIATRANSP);
                ZNFEIMPORTA.Set("VAFRMM", VAFRMM);
                ZNFEIMPORTA.Set("TPINTERMEDIO", TPINTERMEDIO);
                ZNFEIMPORTA.Set("CNPJ", CNPJ);
                ZNFEIMPORTA.Set("UFTERCEIRO", UFTERCEIRO);
                ZNFEIMPORTA.Save();
                _conn.Commit();
            }
            catch (Exception ex)
            {
                _conn.Rollback();
                Log.SalvarLog("DI", ex.ToString());
            }
        }
    }
}
