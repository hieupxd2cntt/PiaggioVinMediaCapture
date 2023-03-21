using Microsoft.AspNetCore.Mvc;
using VINMediaCapture.Models;
using VINMediaCapture.Services;
using VINMediaCaptureEntities.Entities;
using System.Diagnostics;
using VINMediaCaptureEntities.ViewModel;
using VINMediaCapture.Service;

namespace VINMediaCapture.Controllers
{
    public class UserController : BaseController
    {
        private readonly ILogger<ModelController> _logger;
        private readonly IUserService _userlService;
        public UserController(ILogger<ModelController> logger, IUserService userlService)
        {
            _logger = logger;
            _userlService = userlService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _userlService.Index(new Users());
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Index(Users search)
        {
            var data= await _userlService.Index(search);
            return View(data);
        }

        public async Task<IActionResult> Create(int id)
        {
            if (id>0)
            {
                var model = await _userlService.GetById(id);
                return View(model);
            }
            return View(new Users());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Users model)
        {
            var data = await _userlService.Create(model);
            if (data.ResultCode<=0)
            {
                ViewBag.ErrorMsg = data.Message;
                return View(model);
            }

            return RedirectToAction("Index","User");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _userlService.DeleteById(id);
            return Json(data);
        }
    }
}