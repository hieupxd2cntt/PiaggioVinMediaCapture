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
    public class DocTypeItemService : BaseService, IDocTypeItemsService
    {
        public DocTypeItemService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {

        }

        public async Task<DocTypeItemsViewModel> Index(DocTypeItems docTypeItems)
        {
            var url = "DocTypeItems/Index";
            var data = await PostApi(url, docTypeItems);
            var docTypeItemsData = JsonConvert.DeserializeObject<DocTypeItemsViewModel>(data);
            return docTypeItemsData;
        }
        public async Task<RestOutput<int>> Create(DocTypeItems docTypeItems)
        {
            var url = "DocTypeItems/Create";
            var data = await PostApi(url, docTypeItems);
            var response = JsonConvert.DeserializeObject<RestOutput<int>>(data);
            return response;
        }

        public async Task<DocTypeItems> GetById(int id)
        {
            var url = string.Format("DocTypeItems/GetById?id={0}", id);
            var data = await LoadGetApi(url);
            var module = JsonConvert.DeserializeObject<DocTypeItems>(data);
            return module;
        }

        public async Task<RestOutput<int>> DeleteById(int id)
        {
            var url = "DocTypeItems/DeleteById";
            var data = await PostApi(url, id);
            var response = JsonConvert.DeserializeObject<RestOutput<int>>(data);
            return response;
        }
    }

    public interface IDocTypeItemsService
    {
        Task<DocTypeItemsViewModel> Index(DocTypeItems docTypeItems);
        Task<RestOutput<int>> Create(DocTypeItems docTypeItems);
        Task<DocTypeItems> GetById(int id);
        Task<RestOutput<int>> DeleteById(int id);
    }
}
