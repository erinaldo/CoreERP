using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateLib.OBJETOS
{
    public class NfeDifal : OutBoxParams
    {
        public decimal VBCUFDEST { get; set; }
        public decimal PFCPUFDEST { get; set; }
        public decimal PICMSUFDEST { get; set; }
        public decimal PICMSINTER { get; set; }
        public decimal PICMSINTERPART { get; set; }
        public decimal VFCPUFDEST { get; set; }
        public decimal VICMSUFDEST { get; set; }
        public decimal VICMSUFREMET { get; set; }

        public void save()
        {
            try
            {
                AppLib.Context.poolConnection.Get("Start").ExecTransaction("INSERT INTO ZNFEDIFAL (IDOUTBOX, VBCUFDEST, PFCPUFDEST, PICMSUFDEST, PICMSINTER, PICMSINTERPART, VFCPUFDEST, VICMSUFDEST, VICMSUFREMET) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)", new object[] { this.IdOutbox, this.VBCUFDEST, this.PFCPUFDEST, this.PICMSUFDEST, this.PICMSINTER, this.PICMSINTERPART, this.VFCPUFDEST, this.VICMSUFDEST, this.VICMSUFREMET });
            }
            catch (Exception ex)
            {
                string err = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                ValidateLib.Log.SalvarLog("NfeDifal.Save", err);
                throw new Exception(err);
            }
        }
    }
}
