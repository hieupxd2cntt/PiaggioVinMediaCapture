using Microsoft.AspNetCore.Mvc;
using VINMediaCapture.Models;
using VINMediaCapture.Services;
using VINMediaCaptureEntities.Entities;
using System.Diagnostics;
using VINMediaCaptureEntities.ViewModel;
using VINMediaCapture.Service;

namespace VINMediaCapture.Controllers
{
    public class ColorController : BaseController
    {
        private readonly ILogger<ColorController> _logger;
        private readonly IColorService _colorService;
        public ColorController(ILogger<ColorController> logger, IColorService colorService)
        {
            _logger = logger;
            _colorService = colorService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _colorService.Index(new Color());
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Index(Color search)
        {
            var data= await _colorService.Index(search);
            return View(data);
        }

        public async Task<IActionResult> Create(int id)
        {
            if (id>0)
            {
                var color = await _colorService.GetById(id);
                return View(color);
            }
            return View(new Color());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Color color)
        {
            var data = await _colorService.Create(color);
            if (data.ResultCode<=0)
            {
                ViewBag.ErrorMsg = data.Message;
                return View(color);
            }

            return RedirectToAction("Index","Color");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _colorService.DeleteById(id);
            return Json(data);
        }
    }
}