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
using System.Reflection;

namespace VINMediaCapture.Service
{
    public class TransactionService : BaseService, ITransactionService
    {
        public TransactionService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {

        }

        public async Task<TransactionAddModel> AddTransaction(int id,int tranType)
        {
            var url = string.Format("Transaction/AddTransaction?id={0}&tranType={1}", id,tranType);
            var data = await LoadGetApi(url);
            var module = JsonConvert.DeserializeObject<RestOutput<TransactionAddModel>>(data);
            return module.Data;
        }

        public async Task<RestOutput<int>> SaveTransaction(TransactionAddModel model)
        {
            var url = string.Format("Transaction/SaveTransaction");
            var data = await PostApi(url, model);
            var module = JsonConvert.DeserializeObject<RestOutput<int>>(data);
            return module;
        }

        public async Task<TransactionIndexModel> SearchTransaction(TransactionIndexModel search)
        {
            var url = string.Format("Transaction/SearchTransaction");
            var data = await PostApi(url, search);
            var module = JsonConvert.DeserializeObject<TransactionIndexModel>(data);
            return module;
        }
    }
    public interface ITransactionService
    {
        public Task<TransactionIndexModel> SearchTransaction(TransactionIndexModel search);
        public Task<TransactionAddModel> AddTransaction(int id, int tranType);
        public Task<RestOutput<int>> SaveTransaction(TransactionAddModel model);
    }
}
