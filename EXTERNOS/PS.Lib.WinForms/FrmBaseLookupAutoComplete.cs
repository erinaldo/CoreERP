using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace PS.Lib.WinForms
{
    internal partial class FrmBaseLookupAutoComplete : DevExpress.XtraEditors.XtraForm
    {
        internal delegate void OnHideFormDelegate();

        //public FrmBaseLookupAutoComplete()
        //{
        //    InitializeComponent();
        //}

        static FrmBaseLookupAutoComplete()
        {
    
        }

        internal event FrmBaseLookupAutoComplete.OnHideFormDelegate OnHideForm
        {
            add
            {
                //FrmBaseLookupAutoComplete.OnHideFormDelegate onHideFormDelegate2, onHideFormDelegate3;

                //FrmBaseLookupAutoComplete.OnHideFormDelegate onHideFormDelegate1 = OnHideForm;
                //do
                //{
                //    onHideFormDelegate2 = onHideFormDelegate1;
                //    onHideFormDelegate3 += value;
                //    onHideFormDelegate1 = Interlocked.CompareExchange<FrmBaseLookupAutoComplete.OnHideFormDelegate>(ref OnHideForm, onHideFormDelegate3, onHideFormDelegate2);
                //} while (onHideFormDelegate1 != onHideFormDelegate2);
            }
            remove
            {
                //FrmBaseLookupAutoComplete.OnHideFormDelegate onHideFormDelegate2, onHideFormDelegate3;

                //FrmBaseLookupAutoComplete.OnHideFormDelegate onHideFormDelegate1 = OnHideForm;
                //do
                //{
                //    onHideFormDelegate2 = onHideFormDelegate1;
                //    onHideFormDelegate3 -= value;
                //    onHideFormDelegate1 = Interlocked.CompareExchange<FrmBaseLookupAutoComplete.OnHideFormDelegate>(ref OnHideForm, onHideFormDelegate3, onHideFormDelegate2);
                //} while (onHideFormDelegate1 != onHideFormDelegate2);
            }
        }

        public void SetAutoComplete()
        {
            if (!Visible)
                Show();
        }

    }
}
