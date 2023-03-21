using VINMediaCaptureEntities.Entities;
using VINMediaCaptureEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VINMediaCaptureEntities.ViewModel;
using VINMediaCaptureEntities;

namespace VINMediaCapture.Services
{
    public interface IUserService
    {
        Task<UserLoginModel> Login(Users user);
        Task<UserViewModel> Index(Users model);
        Task<RestOutput<int>> Create(Users model);
        Task<Users> GetById(int id);
        Task<RestOutput<int>> DeleteById(int id);
    }
}
