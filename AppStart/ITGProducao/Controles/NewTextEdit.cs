using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Globalization;

namespace ITGProducao.Controles
{
    public class NewTextEdit : TextEdit
    {
        [Category("ItInit_Propriedades"), Description("")] public bool Obrigatorio { get; set; }
        [Category("ItInit_Propriedades"), Description("")] public string CampoBD { get; set; }
        [Category("ItInit_Propriedades"), Description("")] public string Mensagem { get; set; }
        [Category("ItInit_Propriedades"), Description("")] public bool SóNumeros { get; set; }
        [Category("ItInit_Propriedades"), Description("")] public TipoConteudo Tipo { get; set; }
        public enum TipoConteudo
        {
            Texto,Valor,Moeda,Data,Email,Telefone,CPF,CNPJ
        }

        public event EventHandler PropertyChanged;

        string[] validaSoNumeros = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "\r" };

        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            AtualizaCaracteresNumericos();

            if (this.SóNumeros == true)
            {
                if (Array.IndexOf(validaSoNumeros, e.KeyChar) != -1)
                {
                    return;
                }
                e.Handled = true;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            AtualizaCaracteresNumericos();

            if (this.Tipo == TipoConteudo.Data)
            {
                if (this.Text.Length == 2 | this.Text.Length == 5)
                {
                    this.Text = this.Text + "/";
                    this.Select(this.Text.Length, this.Text.Length);
                }
            }
        }

        private void AtualizaCaracteresNumericos()
        {
            switch (this.Tipo)
            {
                case TipoConteudo.Texto:

                    break;
                case TipoConteudo.Data:
                    validaSoNumeros = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "/","\r" };
                    break;
                case TipoConteudo.Moeda:
                    validaSoNumeros = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ",", ".", "\r" };
                    break;
                case TipoConteudo.Valor:
                    validaSoNumeros = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ",", ".", "\r" };
                    break;
                case TipoConteudo.Email:
                    break;
                case TipoConteudo.Telefone:
                    validaSoNumeros = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "-", "(", ")", "\r" };
                    break;
                case TipoConteudo.CPF:
                    validaSoNumeros = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", "-", "\r" };
                    break;
                case TipoConteudo.CNPJ:
                    validaSoNumeros = new string[] {"0","1","2","3","4","5","6","7","8","9",".","-","/","\r"};
                    break;
                default:
                    break;
            }
        }

        protected virtual void OnPropertyChanged(EventArgs e, TipoConteudo Tipo)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }

            switch (this.Tipo)
            {
                case TipoConteudo.Texto:

                    break;
                case TipoConteudo.Data:
                    validaSoNumeros = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "/","\r" };
                    break;
                case TipoConteudo.Moeda:
                    validaSoNumeros = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", ",", "\r" };
                    break;
                case TipoConteudo.Valor:
                    validaSoNumeros = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", "," , "\r" };
                    this.Text = string.Format("{0:n2}", Convert.ToDecimal(this.Text));
                    break;
                case TipoConteudo.Email:
                    break;
                case TipoConteudo.Telefone:
                    validaSoNumeros = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "-", "(",")", "\r" };
                    break;
                case TipoConteudo.CPF:
                    validaSoNumeros = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".", "\r" };
                    break;
                case TipoConteudo.CNPJ:
                    validaSoNumeros = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ".","\r" };
                    break;
                default:
                    break;

            }
            this.Update();

        }
    }
}
