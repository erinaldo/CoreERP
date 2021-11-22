using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PS.Lib.WinForms.WebBrowser
{
    public partial class PSPartWebBrowserView : Form
    {
        public string Url;
        public PSPartWebBrowserView()
        {
            InitializeComponent();
        }

        private void PSPartWebBrowserView_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Url)) return;
            if (Url.Equals("about:blank")) return;
            if (!Url.StartsWith("http://") &&
                !Url.StartsWith("https://"))
            {
                Url = "http://" + Url;
            }
            try
            {
                webBrowser1.Navigate(new Uri(Url));
            }
            catch (System.UriFormatException)
            {
                return;
            }
        }
    }
}
