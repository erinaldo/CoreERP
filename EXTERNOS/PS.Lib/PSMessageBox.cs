using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib
{
    public class PSMessageBox
    {
        private static string Message = "Mensagem";

        public static System.Windows.Forms.DialogResult ShowError(string text)
        {
            return System.Windows.Forms.MessageBox.Show(text, Message, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        public static System.Windows.Forms.DialogResult ShowInfo(string text)
        {
            return System.Windows.Forms.MessageBox.Show(text, Message, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }

        public static System.Windows.Forms.DialogResult ShowQuestion(string text)
        {
            return System.Windows.Forms.MessageBox.Show(text, Message, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
        }
    }
}
