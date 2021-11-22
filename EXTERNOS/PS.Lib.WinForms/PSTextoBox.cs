using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PS.Lib.WinForms
{
    public partial class PSTextoBox : UserControl
    {
        private Constantes ct = new Constantes();
        private string tDataField;

        public PSTextoBox()
        {
            InitializeComponent();
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

        [Category("PSLib"), Description("AutoIncremento"), DefaultValue(Global.TypeAutoinc.None)]
        public Global.TypeAutoinc AutoIncremento { get; set; }

        [Category("PSLib"), Description("Edita"), DefaultValue(false)]
        public bool Edita 
        {
            set
            {
                this.textBox1.Enabled = value;
                /*
                if(value)
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

        [Category("PSLib"), Description("PasswordChar"), DefaultValue(false)]
        public char PasswordChar
        {
            set
            {
                this.textBox1.PasswordChar = value;
            }
            get
            {
                return this.textBox1.PasswordChar;
            }
        }

        [Category("PSLib"),Description("Text")]
        public override string  Text
        {
	        get 
	        { 
		         return this.textBox1.Text;
	        }
	        set 
	        { 
		        this.textBox1.Text = value;
	        }
        }

        [Category("PSLib"), Description("ReadOnly")]
        [DefaultValue(false)]
        public bool ReadOnly
        {
            set
            {
                this.textBox1.ReadOnly = value;
            }
            get
            {
                return this.textBox1.ReadOnly;
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

        [Category("PSLib"), Description("MaxLength"), DefaultValue(Int32.MaxValue)]
        public int MaxLength
        {
            set
            {
                this.textBox1.MaxLength = value;
            }
            get
            {
                return this.textBox1.MaxLength;
            }
        }
    }
}
