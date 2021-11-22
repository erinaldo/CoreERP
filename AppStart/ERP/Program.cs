using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ERP
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //using (FormLogin frmLogin = new FormLogin())
            //{
            using (MDILogin login = new MDILogin())
            {
                //switch (frmLogin.ShowDialog())
                //{
                //    case DialogResult.OK:
                //        Application.Run(new FormPrincipal());
                //        break;
                //    case DialogResult.Abort:
                //        break;
                //}
                //if (frmLogin.ShowDialog() == DialogResult.OK)
                //{
                //    MDIPrincipal frmMDI = new MDIPrincipal();
                //    frmMDI.txtVersion.Caption = "Versão: " + frmLogin.Versao;
                //    Application.Run(frmMDI);
                    if (login.ShowDialog() == DialogResult.OK)
                    {
                        MDIPrincipal frmMDI = new MDIPrincipal();
                        frmMDI.txtVersion.Caption = "Versão: " + login.Versao;
                    Application.Run(frmMDI);

                    ERP.Properties.Settings.Default.Save();
                    //    if (frmLogin.ShowDialog() == DialogResult.OK)
                    //{
                    //MDIPrincipal frmMDI = new MDIPrincipal();
                    //frmMDI.txtVersion.Caption = "Versão: " + frmLogin.Versao;
                    //Application.Run(frmMDI);
                    //FormPrincipal frmPrincipal = new FormPrincipal();
                    //frmPrincipal.txtVersion.Caption = "Versão: " + frmLogin.Versao;
                    //Application.Run(frmPrincipal);
                }

            }
        }
    }
}

