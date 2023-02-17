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
using VINMediaCaptureEntities.ViewModel;
using VINMediaCaptureEntities;
namespace VINMediaCapture.Service
{
    public class ColorService : BaseService, IColorService
    {
        public ColorService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {

        }

        public async Task<ColorViewModel> Index(Color color)
        {
            var url = "Color/Index";
            var data = await PostApi(url, color);
            var colorData = JsonConvert.DeserializeObject<ColorViewModel>(data);
            return colorData;
        }
        public async Task<RestOutput<int>> Create(Color color)
        {
            var url = "Color/Create";
            var data = await PostApi(url, color);
            var response = JsonConvert.DeserializeObject<RestOutput<int>>(data);
            return response;
        }

        public async Task<Color> GetById(int id)
        {
            var url = string.Format("Color/GetById?id={0}", id);
            var data = await LoadGetApi(url);
            var module = JsonConvert.DeserializeObject<Color>(data);
            return module;
        }

        public async Task<RestOutput<int>> DeleteById(int id)
        {
            var url = "Color/DeleteById";
            var data = await PostApi(url, id);
            var response = JsonConvert.DeserializeObject<RestOutput<int>>(data);
            return response;
        }
    }

    public interface IColorService
    {
        Task<ColorViewModel> Index(Color color);
        Task<RestOutput<int>> Create(Color color);
        Task<Color> GetById(int id);
        Task<RestOutput<int>> DeleteById(int id);
    }
}
