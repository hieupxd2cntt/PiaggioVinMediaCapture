using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VINMediaCaptureEntities.Entities;
using VINMediaCaptureEntities;
using VINMediaCaptureEntities.Enum;
using VINMediaCaptureEntities.Model;
using Microsoft.VisualBasic.ApplicationServices;

namespace TwainScan.Common
{
    public class ApiMethod: ApiMethodBase
    {
        public string GetModelByBarcode(string barcode)
        {
            var url = "Mobile/GetListAttribute?docTypeId="+(int)EDocType.ThuThapTaiLieu;
            var data = PostApi(url, barcode);
            
            return data;
        }
        public List<AllCode> LoadScanSetting()
        {
            try
            {
                var url = String.Format("AllCode/LoadAllCodeByType?type={0}", EAllCode.ScanSetting.GetMapping());
                var data = LoadGetApi(url);
                if (data == null)
                {
                    return null;
                }
                var allCodes = JsonConvert.DeserializeObject<List<AllCode>>(data);
                return allCodes;
            }
            catch (Exception e)
            {

                throw e ;
            }
        }
        public UserLoginModel Login(Users user)
        {
            var url = "User/Login";
            var data = PostApi(url, user);
            if (data==null)
            {
                return null;
            }
            var userData = JsonConvert.DeserializeObject<Users>(data);
            return new UserLoginModel { User = userData };
        }

        public DateTime? GetSystemDate()
        {
            var url = "AllCode/GetSystemDate";
            var data = LoadGetApi(url);
            return Convert.ToDateTime(data);
        }
    }
}
