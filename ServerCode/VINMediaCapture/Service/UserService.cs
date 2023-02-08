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

namespace VINMediaCapture.Service
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {

        }

        public async Task<UserLoginModel> Login(User user)
        {
            var url = "User/Login";
            var data = await PostApi(url, user);
            var userData = JsonConvert.DeserializeObject<UserLoginModel>(data);
            return userData; ;
        }
        //public async Task<List<MenuItemInfo>> GetAllMenu(int userId)
        //{
        //    var url = string.Format("Menu/GetAllMenu?userId=" + userId);
        //    var data = await LoadGetApi(url);
        //    var module = JsonConvert.DeserializeObject<RestOutput<List<MenuItemInfo>>>(data);
        //    return module.Data;
        //}
    }
     
}
