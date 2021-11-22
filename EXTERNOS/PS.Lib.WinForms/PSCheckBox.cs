using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PS.Lib.WinForms
{
    public partial class PSCheckBox : UserControl
    {
        private string tDataField;

        public PSCheckBox()
        {
            InitializeComponent();

            
        }

        public delegate void CheckedChangedDelegate(object sender, EventArgs e);

        [Category("PSLib"), Description("")]
        public event CheckedChangedDelegate CheckedChanged;

        [Category("PSLib"), Description("DataField")]
        public string DataField
        {
            set
            {
                this.tDataField = value;
                if (this.tDataField != string.Empty)
                    this.checkBox1.Text = value;
            }
            get
            {
                return this.tDataField;
            }
        }

        [Category("PSLib"), Description("Chave"), DefaultValue(false)]
        public bool Chave
        {
            set
            {
                this.checkBox1.Enabled = value;
                /*
                if (value)
                    this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                else
                    this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                */
            }
            get
            {
                return this.checkBox1.Enabled;
            }
        }

        [Category("PSLib"), Description("Caption")]
        public string Caption
        {
            set
            {
                this.checkBox1.Text = value;
            }
            get
            {
                
                return this.checkBox1.Text;
                
            }
        }

        [Category("PSLib"), Description("Checked")]
        public bool Checked
        {
            set
            {
                this.checkBox1.Checked = value;
            }
            get
            {
                return this.checkBox1.Checked;
            }
        }

        private void OnCheckedChanged(EventArgs e)
        {
            if (CheckedChanged != null)
            {
                CheckedChanged(this, e);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            OnCheckedChanged(new EventArgs());
        }   

    }
}
