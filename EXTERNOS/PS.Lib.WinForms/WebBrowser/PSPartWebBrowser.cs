using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib.WinForms.WebBrowser
{
    public class PSPartWebBrowser : PS.Lib.WinForms.PSPartGridView
    {
        public PSPartWebBrowser()
        {
            this.TableName = "GWEBBROWSER";
            this.Keys = new string[] { "CODWEBBROWSER" };
            this.FormEditName = "PSPartWebBrowserEdit";
            this.PSPartData = new PSPartWebBrowserData();

            this.SecurityID = "PSPartWebBrowser";
            this.ModuleID = "PG";
        }

        public override List<DataField> GetTableColumn()
        {
            List<DataField> objArr = new List<DataField>();

            objArr.Add(new DataField("CODWEBBROWSER", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("DESCRICAO", null, typeof(PS.Lib.WinForms.PSTextoBox)));
            objArr.Add(new DataField("URL", null, typeof(PS.Lib.WinForms.PSTextoBox)));

            return objArr;
        }
    }
}
