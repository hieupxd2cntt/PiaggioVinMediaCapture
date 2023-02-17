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
    public class ModelService : BaseService, IModelService
    {
        public ModelService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {

        }

        public async Task<ModelViewModel> Index(Model model)
        {
            var url = "Model/Index";
            var data = await PostApi(url, model);
            var modelData = JsonConvert.DeserializeObject<ModelViewModel>(data);
            return modelData;
        }
        public async Task<RestOutput<int>> Create(Model model)
        {
            var url = "Model/Create";
            var data = await PostApi(url, model);
            var response = JsonConvert.DeserializeObject<RestOutput<int>>(data);
            return response;
        }

        public async Task<Model> GetById(int id)
        {
            var url = string.Format("Model/GetById?id={0}", id);
            var data = await LoadGetApi(url);
            var module = JsonConvert.DeserializeObject<Model>(data);
            return module;
        }

        public async Task<RestOutput<int>> DeleteById(int id)
        {
            var url = "Model/DeleteById";
            var data = await PostApi(url, id);
            var response = JsonConvert.DeserializeObject<RestOutput<int>>(data);
            return response;
        }
    }

    public interface IModelService
    {
        Task<ModelViewModel> Index(Model model);
        Task<RestOutput<int>> Create(Model model);
        Task<Model> GetById(int id);
        Task<RestOutput<int>> DeleteById(int id);
    }
}
