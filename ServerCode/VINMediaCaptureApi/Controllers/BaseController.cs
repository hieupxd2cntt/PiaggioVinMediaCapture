using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace VINMediaCaptureApi.Controllers
{
    public class BaseController : ControllerBase
    {
       
        public object GetUserLogin()
        {
            var a = Request.Headers.Keys;
            return "";
        }
        //public string ConnectionString { get { return Configuration["ConfigApp:DbContext"];} }
        //public string UserName {
        //    get {

        //        var header= HttpContext.Request.Headers["Authorization"];
        //        if (header.Count>0) {
        //            byte[] b = Convert.FromBase64String(header.First().Replace("Basic", "").Trim());
        //            var strOriginal = System.Text.Encoding.UTF8.GetString(b);
        //            var split = strOriginal.Split(":");
        //            if (split.Length > 2) {
        //                return split[2];
        //            }
        //        }
        //        return "";
        //    }
        //}
    }
}