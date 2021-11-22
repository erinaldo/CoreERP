using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib
{
    public class ComboBoxItem
    {
        public object ValueMember { get; set; }
        public object DisplayMember { get; set; }

        public ComboBoxItem()
        { 
        
        }

        public ComboBoxItem(object value, object display)
        {
            this.ValueMember = value;
            this.DisplayMember = display;
        }
    }
}
