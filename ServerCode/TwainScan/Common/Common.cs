using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using VINMediaCaptureEntities.Entities;
using VINMediaCaptureEntities.Model;
using VINMediaCaptureEntities.TaiwainAppModel;
using VINMediaCaptureEntities.ViewModel;

namespace TwainScan.Common
{
    public class Common
    {
        public static ConfigModel GetConfig()
        {
            try
            {
                if (CurrentValue.CurrConfig !=null)
                {
                    return CurrentValue.CurrConfig;
                }
                var xml = new XMLProcess();
                var configModel = new ConfigModel();
                var configFile = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData) + ConstantConfig.DefaultScanConfig;
                if (!Directory.Exists(Path.GetDirectoryName(configFile)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(configFile));
                }
                if (File.Exists(configFile))
                {
                    var xmlString = xml.LoadXmlFromFile(configFile);
                    configModel = (ConfigModel)xml.LoadObjectFromXMLString(xmlString, typeof(ConfigModel));

                }
                if (String.IsNullOrEmpty(configModel.ScanFolder))
                {
                    configModel.ScanFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + ConstantConfig.DefaultScanFolder;
                }
                if (String.IsNullOrEmpty(configModel.ScanFailFolder))
                {
                    configModel.ScanFailFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + ConstantConfig.DefaultScanFailFolder;
                }
                if (String.IsNullOrEmpty(configModel.ScanSuccessFolder))
                {
                    configModel.ScanSuccessFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + ConstantConfig.DefaultScanSuccessFolder;
                }
                if (String.IsNullOrEmpty(configModel.WebApi))
                {
                    configModel.WebApi = ConstantConfig.ApiUrl;
                }
                if (String.IsNullOrEmpty(configModel.WebApp))
                {
                    configModel.WebApp = ConstantConfig.HostUrl;
                }
                if (String.IsNullOrEmpty(configModel.LogPath))
                {
                    configModel.LogPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + ConstantConfig.LogPath;
                }
                CurrentValue.CurrConfig = configModel;
                return configModel;
            }
            catch (Exception ex)
            {
                return new ConfigModel();
               
            }
        }
    }
    public class CurrentValue
    {
        public static ConfigModel CurrConfig { get; set; }
        public static String Barcode { get; set; }
        public static String VinCode { get; set; }
        public static UserLoginModel User { get; set; }
        public static List<DocTypeItemAddModel> CurrentAttributeModel { get; set; }
        public static void ClearCurrentBarcodeValue()
        {
            Barcode = "";
            VinCode = "";
        }
    }
}
