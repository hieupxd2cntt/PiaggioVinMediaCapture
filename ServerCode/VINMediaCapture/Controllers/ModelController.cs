using Microsoft.AspNetCore.Mvc;
using VINMediaCapture.Models;
using VINMediaCapture.Services;
using VINMediaCaptureEntities.Entities;
using System.Diagnostics;
using VINMediaCaptureEntities.ViewModel;
using VINMediaCapture.Service;

namespace VINMediaCapture.Controllers
{
    public class ModelController : BaseController
    {
        private readonly ILogger<ModelController> _logger;
        private readonly IModelService _modelService;
        public ModelController(ILogger<ModelController> logger, IModelService modelService)
        {
            _logger = logger;
            _modelService = modelService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _modelService.Index(new Model());
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Index(Model search)
        {
            var data= await _modelService.Index(search);
            return View(data);
        }

        public async Task<IActionResult> Create(int id)
        {
            if (id>0)
            {
                var model = await _modelService.GetById(id);
                return View(model);
            }
            return View(new Model());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Model model)
        {
            var data = await _modelService.Create(model);
            if (data.ResultCode<=0)
            {
                ViewBag.ErrorMsg = data.Message;
                return View(model);
            }

            return RedirectToAction("Index","Model");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _modelService.DeleteById(id);
            return Json(data);
        }
    }
}