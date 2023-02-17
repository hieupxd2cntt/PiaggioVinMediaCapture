using Microsoft.AspNetCore.Mvc;
using VINMediaCapture.Models;
using VINMediaCapture.Services;
using VINMediaCaptureEntities.Entities;
using System.Diagnostics;
using VINMediaCaptureEntities.ViewModel;
using VINMediaCapture.Service;

namespace VINMediaCapture.Controllers
{
    public class DocTypeItemsController : BaseController
    {
        private readonly ILogger<DocTypeItemsController> _logger;
        private readonly IDocTypeItemsService _docTypeItemsService;
        public DocTypeItemsController(ILogger<DocTypeItemsController> logger, IDocTypeItemsService docTypeItemsService)
        {
            _logger = logger;
            _docTypeItemsService = docTypeItemsService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _docTypeItemsService.Index(new DocTypeItems());
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Index(DocTypeItems search)
        {
            var data= await _docTypeItemsService.Index(search);
            return View(data);
        }

        public async Task<IActionResult> Create(int id)
        {
            if (id>0)
            {
                var docTypeItems = await _docTypeItemsService.GetById(id);
                return View(docTypeItems);
            }
            return View(new DocTypeItems());
        }
        [HttpPost]
        public async Task<IActionResult> Create(DocTypeItems docTypeItems)
        {
            var data = await _docTypeItemsService.Create(docTypeItems);
            if (data.ResultCode<=0)
            {
                ViewBag.ErrorMsg = data.Message;
                return View(docTypeItems);
            }

            return RedirectToAction("Index","DocTypeItems");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _docTypeItemsService.DeleteById(id);
            return Json(data);
        }
    }
}