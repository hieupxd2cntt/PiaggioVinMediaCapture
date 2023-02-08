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
    public class CustomerService : BaseService, ICustomerService
    {
        public CustomerService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {

        }
        public async Task<List<Select2Ajax>> SearchSelect2(string term,int type)
        {
            var url = string.Format("Customer/SearchSelect2?term={0}&type={1}",term,type);
            var data = await LoadGetApi(url);
            var module = JsonConvert.DeserializeObject<List<Select2Ajax>>(data);
            return module;
        }
    }
    public interface ICustomerService
    {
        public Task<List<Select2Ajax>> SearchSelect2(string term,int type);
    }
}
