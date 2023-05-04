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
    public class UserService : BaseService, IUserService
    {
        public UserService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration, httpContextAccessor)
        {

        }

        public async Task<UserLoginModel> Login(Users user)
        {
            var url = "User/Login";
            var data = await PostApi(url, user);
            var userData = JsonConvert.DeserializeObject<Users>(data);
            return new UserLoginModel { User=userData};
        }
        
        public async Task<UserViewModel> Index(Users User)
        {
            var url = "User/Index";
            var data = await PostApi(url, User);
            var UserData = JsonConvert.DeserializeObject<UserViewModel>(data);
            return UserData;
        }
        public async Task<RestOutput<int>> Create(Users User)
        {
            var url = "User/Create";
            var data = await PostApi(url, User);
            var response = JsonConvert.DeserializeObject<RestOutput<int>>(data);
            return response;
        }

        public async Task<Users> GetById(int id)
        {
            var url = string.Format("User/GetById?id={0}", id);
            var data = await LoadGetApi(url);
            var module = JsonConvert.DeserializeObject<Users>(data);
            return module;
        }

        public async Task<RestOutput<int>> DeleteById(int id)
        {
            var url = "User/DeleteById";
            var data = await PostApi(url, id);
            var response = JsonConvert.DeserializeObject<RestOutput<int>>(data);
            return response;
        }
    }
     
}
