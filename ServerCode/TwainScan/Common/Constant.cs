using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwainScan.Common
{
    public class ConstantConfig
    {
        public static string DefaultScanConfig = "/TaiwanScan/TaiwanScanConfig.Xml";
        public static string DefaultScanFolder = @"/TaiwanScan/Scan";
        public static string DefaultScanFailFolder = @"/TaiwanScan/ScanFail";
        public static string DefaultScanSuccessFolder = @"/TaiwanScan/ScanSuccess";
        public static string ApiUrl = "https://localhost:7262/api/";
    }
}
