using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS.Glb.Class
{
    public static class Funcoes
    {
        public static void Moeda(ref DevExpress.XtraEditors.TextEdit txt)
        {
            string n = string.Empty;
            double v = 0;

            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");

                if (n.Equals(""))
                {
                    n = "";
                    n = n.PadLeft(3, '0');
                }

                if (n.Length > 3 & n.Substring(0, 1) == "0")
                {
                    n = n.Substring(1, n.Length - 1);
                    v = Convert.ToDouble(n) / 100;
                    txt.Text = string.Format("{0:N}", v);
                    txt.SelectionStart = txt.Text.Length;
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
