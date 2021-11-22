using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PS.Lib.WinForms
{
    public partial class PSDateBox : UserControl
    {
        private string tDataField;

        public PSDateBox()
        {
            InitializeComponent();
        }

        Valida vl = new Valida();

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

        [Category("PSLib"), Description("Mascara"), DefaultValue(false)]
        public string Mascara
        {
            set
            {
                if (value == "00/00/0000 00:00:00.000")
                {
                    dateTimePicker1.CustomFormat = "MM/dd/yyyy HH:mm:ss.fff";
                    maskedTextBox1.Mask = "00/00/0000 00:00:00.000";
                }

                if (value == "00/00/0000 00:00")
                {
                    dateTimePicker1.CustomFormat = "MM/dd/yyyy HH:mm";
                    maskedTextBox1.Mask = "00/00/0000 00:00";
                }

                if (value == "00/00/0000")
                {
                    dateTimePicker1.CustomFormat = "MM/dd/yyyy";
                    maskedTextBox1.Mask = "00/00/0000";
                }
                if (value == "00/00/0000 00:00:00")
                {
                    dateTimePicker1.CustomFormat = "MM/dd/yyyy HH:mm:ss";
                    maskedTextBox1.Mask = "00/00/0000 00:00:00";
                }

            }
            get
            {
                return this.maskedTextBox1.Mask;
            }
        }

        [Category("PSLib"), Description("Chave"), DefaultValue(false)]
        public bool Chave
        {
            set
            {
                this.dateTimePicker1.Enabled = value;
                this.maskedTextBox1.Enabled = value;
                /*
                if (value)
                    this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                else
                    this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                */
            }
            get
            {
                return this.dateTimePicker1.Enabled;
            }
        }

        [Category("PSLib"), Description("ReadOnly"), DefaultValue(false)]
        public bool ReadOnly
        {
            set
            {
                this.maskedTextBox1.ReadOnly = value;
            }
            get
            {
                return this.maskedTextBox1.ReadOnly;
            }
        }

        [Category("PSLib"), Description("Text")]
        public DateTime Value
        {
            set
            {
                this.dateTimePicker1.Value = value;
            }
            get
            {
                return this.dateTimePicker1.Value;
            }
        }

        [Category("PSLib"), Description("Text")]
        public override string Text
        {
            set
            {
                this.maskedTextBox1.Text = value;
            }
            get
            {
                if ((this.maskedTextBox1.Text == "  /  /") || (this.maskedTextBox1.Text == "  /  /       :"))
                {
                    return null;
                }
                else
                {
                    if (this.Mascara == "00/00/0000 00:00:00.000")
                    {
                        try
                        {
                            dateTimePicker1.Value = Convert.ToDateTime(maskedTextBox1.Text);
                            return this.maskedTextBox1.Text;
                        }
                        catch
                        {
                            throw new Exception("Data/Hora não é válida.");
                        }

                    }

                    if (this.Mascara == "00/00/0000 00:00")
                    {
                        try
                        {
                            dateTimePicker1.Value = Convert.ToDateTime(maskedTextBox1.Text);
                            return this.maskedTextBox1.Text;
                        }
                        catch
                        {
                            throw new Exception("Data/Hora não é válida.");
                        }

                    }

                    if (this.Mascara == "00/00/0000")
                    {
                        try
                        {
                            dateTimePicker1.Value = Convert.ToDateTime(maskedTextBox1.Text);
                            this.maskedTextBox1.Text = this.dateTimePicker1.Value.ToShortDateString();
                            return this.maskedTextBox1.Text;
                        }
                        catch
                        {
                            throw new Exception("Data não é válida.");
                        }
                    }
                    if (this.Mascara.Equals("00/00/0000 00:00:00"))
                    {
                        try
                        {
                            dateTimePicker1.Value = Convert.ToDateTime(maskedTextBox1.Text);
                            return this.maskedTextBox1.Text;
                        }
                        catch
                        {
                            throw new Exception("Data/Hora não é válida.");
                        }
                    }

                    return null;
                }
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
                this.maskedTextBox1.MaxLength = value;
            }
            get
            {
                return this.maskedTextBox1.MaxLength;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (this.Mascara == "00/00/0000 00:00:00.000")
            {

                this.maskedTextBox1.Text = this.dateTimePicker1.Value.ToString("dd/MM/yyyy HH:mm:ss.fff");
            }

            if (this.Mascara == "00/00/0000 00:00")
            {

                this.maskedTextBox1.Text = this.dateTimePicker1.Value.ToString("dd/MM/yyyy HH:mm");
            }

            if (this.Mascara == "00/00/0000")
            {
                this.maskedTextBox1.Text = this.dateTimePicker1.Value.ToShortDateString();
            }
            if (this.Mascara == "00/00/0000 00:00:00")
            {
                this.maskedTextBox1.Text = this.dateTimePicker1.Value.ToString("dd/MM/yyyy HH:mm:ss");
            }
        }
    }
}
