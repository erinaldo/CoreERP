using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace PS.Glb.Reports
{
    public partial class XrRelComponentesPrestacao : DevExpress.XtraReports.UI.XtraReport
    {
        public System.Data.DataSet dsXML;
        public XrRelComponentesPrestacao()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ArrayList lista = new ArrayList();
            if (dsXML.Tables["Comp"] != null)
            {
                for (int i = 0; i < dsXML.Tables["Comp"].Rows.Count; i++)
                {
                    item item = new item();
                    item.xNomeComponente = dsXML.Tables["Comp"].Rows[i]["xNome"].ToString();
                    item.vComp = dsXML.Tables["Comp"].Rows[i]["vComp"].ToString();
                    lista.Add(item);
                }
                this.DetailReport.DataSource = lista;
                xrLabel164.DataBindings.Add("Text", null, "xNomeComponente");
                xrLabel167.DataBindings.Add("Text", null, "vComp");
            }
          
        }

    }
}
