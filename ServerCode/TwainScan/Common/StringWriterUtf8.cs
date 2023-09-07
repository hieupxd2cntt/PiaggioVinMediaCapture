using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwainScan
{
    // Subclass the StringWriter class and override the default encoding.  This 
    // allows us to produce XML encoded as UTF-8. 
    public class StringWriterUtf8 : System.IO.StringWriter
    {
        public override Encoding Encoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }
    }
}
