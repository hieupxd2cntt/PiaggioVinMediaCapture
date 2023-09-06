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
    public class ProductDocService : BaseService, IProductDocService
    {
        public ProductDocService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {

        }

        public async Task<ProductDocViewModel> Index(ProductDocViewModel search)
        {
            var url = "ProductDoc/Index";
            var data = await PostApi(url, search);
            var docTypeItemsData = JsonConvert.DeserializeObject<ProductDocViewModel>(data);
            return docTypeItemsData;
        }

        public async Task<DetailProductDocModel> LoadDetailProductDoc(string vinCode, int productDoc, int docType)
        {
            var url = String.Format("ProductDoc/LoadDetailProductDoc?vinCode={0}&productDoc={1}&docType={2}", vinCode,productDoc, docType);
            var data = await LoadGetApi(url);
            var docTypeItemsData = JsonConvert.DeserializeObject<DetailProductDocModel>(data);
            return docTypeItemsData;
        }
    }

    public interface IProductDocService
    {
        Task<ProductDocViewModel> Index(ProductDocViewModel search);
        Task<DetailProductDocModel> LoadDetailProductDoc(string vinCode, int productDoc,int docType);
    }
}
