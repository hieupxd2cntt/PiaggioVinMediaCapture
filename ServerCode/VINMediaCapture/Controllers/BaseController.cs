using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using VINMediaCaptureEntities.Entities;
using VINMediaCaptureEntities.Enum;
using VINMediaCaptureEntities.Model;
using System.Net.WebSockets;

namespace VINMediaCapture.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userLogin = new UserLoginModel { User = new User { Id=3, UserName="HieuPx",BranchId=1},Branch = new Branch {PharmacyBranchName="Quầy thuốc Xuân Hiếu" } };
            HttpContext.Session.SetString(ESession.User.ToString(), JsonConvert.SerializeObject(userLogin));
            if (HttpContext.Session.GetString(ESession.User.ToString()) ==null)
            {
                filterContext.Result = new RedirectResult(Url.Action("Login", "Login"), true);
            }
            else
            {
                var session = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString(ESession.User.ToString()));
                if (session == null)
                {
                    filterContext.Result = new RedirectResult(Url.Action("Login", "Login"), true);
                }
            }
            
            base.OnActionExecuting(filterContext);
        }
    }
}
