using Microsoft.AspNetCore.Mvc;
using VINMediaCapture.Models;
using VINMediaCapture.Services;
using VINMediaCaptureEntities.Entities;
using System.Diagnostics;
using VINMediaCaptureEntities.Enum;
using Newtonsoft.Json;
using VINMediaCapture.Service;

namespace VINMediaCapture.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public LoginController(ILogger<HomeController> logger, IUserService userServicee)
        {
            _logger = logger;
            _userService = userServicee;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }
        [HttpPost]
        public async Task< IActionResult> Login(Users user)
        {
            if (user.LoginName !="admin")
            {
                ViewBag.Message = "Tên đăng nhập hoặc mật khẩu không chính xác";
                return View(user);
            }
            var userData= await _userService.Login(user);
            
            if (userData != null)
            {
                if (userData.User==null)
                {
                    ViewBag.Message = "Tên đăng nhập hoặc mật khẩu không chính xác";
                    return View(user);
                }
                HttpContext.Session.SetString(ESession.User.ToString(), JsonConvert.SerializeObject(userData));
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Message = "Tên đăng nhập hoặc mật khẩu không chính xác";
            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}