using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PS.Lib.WinForms
{
    public partial class PSMoedaBox : UserControl
    {
        private string tDataField;

        public PSMoedaBox()
        {
            InitializeComponent();
        }

        private int _CasasDecimais = 0;
        private string _Mascara;

        private string RetornarValor(string valor)
        {
            if (_CasasDecimais == 0)
                return Convert.ToInt32(valor).ToString();
            else
                return Convert.ToDouble(valor).ToString(RetornaMascara());
        }

        private string RetornaMascara()
        {
            char c1 = '#';
            char c2 = '0';

            string s1 = new string(c1, _CasasDecimais);
            string s2 = new string(c2, _CasasDecimais);

            if (_CasasDecimais == 0)
                return string.Empty;
            else
                return this._Mascara = string.Concat("###,###,", s1, "0.", s2);
        }


        [Category("PSLib"), Description("DataField")]
        public string DataField
        {
            set
            {
                this.tDataField = value;
                if (this.tDataField != string.Empty) 
                    this.label1.Text = value;
            }
            get
            {
                return this.tDataField;
            }
        }

        [Category("PSLib"), Description("Edita"), DefaultValue(false)]
        public bool Edita
        {
            set
            {
                this.textBox1.Enabled = value;
                /*
                if (value)
                    this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                else
                    this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                */
            }
            get
            {
                return this.textBox1.Enabled;
            }
        }

        [Category("PSLib"), Description("Text")]
        public override string  Text
        {
	        get 
	        { 
		        try
                {
                    this.textBox1.Text = RetornarValor(this.textBox1.Text);
                    return this.textBox1.Text;
                }
                catch
                {
                    throw new Exception("Valor digitado é inválido, apenas números são aceitos.");
                }
	        }
	        set 
	        {
                this.textBox1.Text = RetornarValor(value);
	        }
        }

        [Category("PSLib"), Description("Caption")]
        public string Caption
        {
            set
            {
                this.label1.Text = value;
            }
            get
            {
                return this.label1.Text;
            }
        }

        [Category("PSLib"), Description("Casas Decimais"), DefaultValue(0)]
        public int CasasDecimais
        {
            set
            {
                this._CasasDecimais = value;
            }
            get
            {
                return this._CasasDecimais;
            }        
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                this.textBox1.Text = RetornarValor(this.textBox1.Text);
            }
            catch
            {
                PS.Lib.PSMessageBox.ShowError("Valor digitado é inválido, apenas números são aceitos.");
                this.textBox1.Focus();
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("^(a)");
        }
    }
}
