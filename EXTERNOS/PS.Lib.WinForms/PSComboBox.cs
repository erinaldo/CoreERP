using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PS.Lib.WinForms
{
    public partial class PSComboBox : UserControl
    {
        private string tDataField;

        public PSComboBox()
        {
            InitializeComponent();
        }

        public delegate void SelectedValueChangedDelegate(object sender, EventArgs e);

        [Category("PSLib"), Description("")]
        public event SelectedValueChangedDelegate SelectedValueChanged;

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

        [Category("PSLib"), Description("Chave"), DefaultValue(false)]
        public bool Chave
        {
            set
            {
                this.comboBox1.Enabled = value;
                /*
                if (value)
                    this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                else
                    this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                */
            }
            get
            {
                return this.comboBox1.Enabled;
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

        [Category("PSLib"), Description("Text")]
        public override string  Text
        {
            set
            {
                this.comboBox1.Text = value;
            }
            get
            {
                return this.comboBox1.Text;
            }
        }

        [Category("PSLib"), Description("Value")]
        public object Value
        {
            set
            {
                this.comboBox1.SelectedValue = value;
            }
            get
            {
                return this.comboBox1.SelectedValue;
            }
        }

        [Category("PSLib"), Description("DataSource")]
        public object DataSource
        {
            set
            {
                this.comboBox1.DataSource = value;
            }
            get
            {
                return this.comboBox1.DataSource;
            }
        }

        [Category("PSLib"), Description("ValueMember"), DefaultValue("ValueMember")]
        public string ValueMember
        {
            set
            {
                this.comboBox1.ValueMember = value;
            }
            get
            {
                return this.comboBox1.ValueMember;
            }
        }

        [Category("PSLib"), Description("DisplayMember"), DefaultValue("DisplayMember")]
        public string DisplayMember
        {
            set
            {
                this.comboBox1.DisplayMember = value;
            }
            get
            {
                return this.comboBox1.DisplayMember;
            }
        }

        [Category("PSLib"), Description("SelectedIndex")]
        public int SelectedIndex
        {
            set
            {
                this.comboBox1.SelectedIndex = value;
            }
            get
            {
                return this.comboBox1.SelectedIndex;
            }
        }

        private void OnSelectedValueChanged(EventArgs e)
        {
            if (SelectedValueChanged != null)
            {
                SelectedValueChanged(this, e);
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            OnSelectedValueChanged(new EventArgs());
        }
    }
}
