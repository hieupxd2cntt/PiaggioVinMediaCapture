using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using VINMediaCapture.Services;
using VINMediaCaptureEntities.Entities;
using VINMediaCaptureEntities.Model;
using VINMediaCaptureEntities;
using System.Drawing;

namespace VINMediaCapture.Service
{
    public class AllCodeService : BaseService, IAllCodeService
    {
        public AllCodeService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {

        }
        public async Task<List<AllCode>> LoadAllCodeByType(string type)
        {
            var url = string.Format("AllCode/LoadAllCodeByType?type={0}", type);
            var data = await LoadGetApi(url);
            var module = JsonConvert.DeserializeObject<List<AllCode>>(data);
            return module;
        }

        public async Task<RestOutput<int>> SaveScanSettings(List<AllCode> allCodes)
        {
            var url = string.Format("AllCode/SaveScanSettings");
            var data = await PostApi(url, allCodes);
            var module = JsonConvert.DeserializeObject<RestOutput<int>>(data);
            return module;
        }
    }
    public interface IAllCodeService
    {
        public Task<List<AllCode>> LoadAllCodeByType(string type);
        public Task<RestOutput<int>> SaveScanSettings(List<AllCode> allCodes);
    }
}
