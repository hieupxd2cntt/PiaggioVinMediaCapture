using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwainScan.Common
{
    public class ConstantConfig
    {
        public static string DefaultScanConfig = @"\DaisyEdoc\DaisyEdoc.Xml";
        public static string DefaultScanFolder = @"\DaisyEdoc\Scan";
        public static string DefaultScanFailFolder = @"\DaisyEdoc\ScanFail";
        public static string DefaultScanSuccessFolder = @"\DaisyEdoc\ScanSuccess";
        public static string ApiUrl = ConfigurationManager.AppSettings["UrlApi"].ToString();
        public static string HostUrl = ConfigurationManager.AppSettings["WebAppUrl"].ToString();
        public static string LogPath = @"\DaisyEdoc\Logs";
    }
}
