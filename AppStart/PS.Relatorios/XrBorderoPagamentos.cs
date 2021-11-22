using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Relatorios
{
    public partial class XrBorderoPagamentos : DevExpress.XtraReports.UI.XtraReport
    {
        public XrBorderoPagamentos()
        {
            InitializeComponent();
        }
        PS.Lib.Global gb = new PS.Lib.Global();
        PS.Lib.Data.DBS dbs = new PS.Lib.Data.DBS();
        private List<PS.Lib.DataField> Parametros { get; set; }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sSql = @"SELECT IMAGEM FROM GIMAGEM WHERE CODIMAGEM = (SELECT CODIMAGEM FROM GEMPRESA WHERE CODEMPRESA = ?)";
            byte[] arrayimagem = (byte[])dbs.QueryValue(null, sSql, this.Parametros[0].Valor);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(arrayimagem);
            logo.Image = Image.FromStream(ms);
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

    }
}
