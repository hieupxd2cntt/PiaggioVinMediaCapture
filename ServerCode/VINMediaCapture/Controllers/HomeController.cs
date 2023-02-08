using Microsoft.AspNetCore.Mvc;
using VINMediaCapture.Models;
using VINMediaCapture.Services;
using VINMediaCaptureEntities.Entities;
using System.Diagnostics;

namespace VINMediaCapture.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task< IActionResult> Login(User user)
        //{
        //    var userData= await _userService.Login(user);
        //    if (userData != null)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    return View();
        //}

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