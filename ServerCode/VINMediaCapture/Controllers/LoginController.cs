using Microsoft.AspNetCore.Mvc;
using VINMediaCapture.Models;
using VINMediaCapture.Services;
using VINMediaCaptureEntities.Entities;
using System.Diagnostics;
using VINMediaCaptureEntities.Enum;
using Newtonsoft.Json;

namespace VINMediaCapture.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IWarehouseService _warehouseService;
        public LoginController(ILogger<HomeController> logger, IUserService userService, IWarehouseService warehouseService)
        {
            _logger = logger;
            _userService = userService;
            _warehouseService = warehouseService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Login(User user)
        {
            var userData= await _userService.Login(user);
            if (userData != null)
            {
                #region Lấy thông tin đưa vào thông báo
                var notification = await _warehouseService.GetDrugNotification();
                HttpContext.Session.SetString("Notification", JsonConvert.SerializeObject(notification));
                #endregion

                HttpContext.Session.SetString(ESession.User.ToString(), JsonConvert.SerializeObject(userData));
                return RedirectToAction("Index", "Home");
            }
            
            return View();
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