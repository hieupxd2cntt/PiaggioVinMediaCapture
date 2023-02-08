using VINMediaCaptureEntities.Entities;
using VINMediaCaptureEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace VINMediaCapture.Services
{
    public interface IUserService
    {
        public Task<UserLoginModel> Login(User user);
       
    }
}
