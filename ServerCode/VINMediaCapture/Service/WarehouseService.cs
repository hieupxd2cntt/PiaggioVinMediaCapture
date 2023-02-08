using Newtonsoft.Json;
using VINMediaCapture.Service;
using VINMediaCaptureEntities;
using VINMediaCaptureEntities.Entities;
using VINMediaCaptureEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace VINMediaCapture.Services
{
    public class WarehouseService :BaseService , IWarehouseService
    {
        public WarehouseService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {
        }

        public async Task<NotificationModel> GetDrugNotification()
        {
            var url = string.Format("Warehouse/GetDrugNotification");
            var data = await LoadGetApi(url);
            var module = JsonConvert.DeserializeObject<NotificationModel>(data);
            return module;
        }

        public async Task<WarehouseIndexModel> SearchWarehouse(WarehouseIndexModel search)
        {
            var url = string.Format("Warehouse/SearchWarehouse");
            var data = await PostApi(url, search);
            var module = JsonConvert.DeserializeObject<WarehouseIndexModel>(data);
            return module;
            
        }

        public async Task<RestOutput<int>> SetStatus(List<Color> warehouse)
        {
            var url = string.Format("Warehouse/SetStatus");
            var data = await PostApi(url, warehouse);
            var module = JsonConvert.DeserializeObject<RestOutput<int>>(data);
            return module;
        }
    }
    public interface IWarehouseService
    {
        public Task<WarehouseIndexModel> SearchWarehouse(WarehouseIndexModel search);
        public Task<RestOutput<int>> SetStatus(List<Color> warehouse);
        public Task<NotificationModel> GetDrugNotification();

    }
}