using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Lib
{
    public class ImageProperties
    {
        public object Value { get; set; }
        public string Text { get; set; }
        public System.Drawing.Image Image { get; set; }

        public ImageProperties()
        { 
        
        }

        public ImageProperties(object PropValue, string PropText, System.Drawing.Image PropImage )
        {
            this.Value = PropValue;
            this.Text = PropText;
            this.Image = PropImage;
        }
    }
}
