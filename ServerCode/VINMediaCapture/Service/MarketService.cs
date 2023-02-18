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
    public class MarketService : BaseService, IMarketService
    {
        public MarketService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {

        }

        public async Task<MarketViewModel> Index(Market market)
        {
            var url = "Market/Index";
            var data = await PostApi(url, market);
            var marketData = JsonConvert.DeserializeObject<MarketViewModel>(data);
            return marketData;
        }
        public async Task<RestOutput<int>> Create(Market market)
        {
            var url = "Market/Create";
            var data = await PostApi(url, market);
            var response = JsonConvert.DeserializeObject<RestOutput<int>>(data);
            return response;
        }

        public async Task<Market> GetById(int id)
        {
            var url = string.Format("Market/GetById?id={0}", id);
            var data = await LoadGetApi(url);
            var module = JsonConvert.DeserializeObject<Market>(data);
            return module;
        }

        public async Task<RestOutput<int>> DeleteById(int id)
        {
            var url = "Market/DeleteById";
            var data = await PostApi(url, id);
            var response = JsonConvert.DeserializeObject<RestOutput<int>>(data);
            return response;
        }
    }

    public interface IMarketService
    {
        Task<MarketViewModel> Index(Market market);
        Task<RestOutput<int>> Create(Market market);
        Task<Market> GetById(int id);
        Task<RestOutput<int>> DeleteById(int id);
    }
}
