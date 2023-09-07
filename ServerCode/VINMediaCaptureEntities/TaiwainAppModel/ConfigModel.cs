using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VINMediaCaptureEntities.TaiwainAppModel
{
    public class ConfigModel
    {
        public string ScanFolder { get; set; }
        public string ScanFailFolder { get; set; }
        public string ScanSuccessFolder { get; set; }
        public string WebApp { get; set; }
        public string WebApi { get; set; }
    }
}
