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
            var viewBag = await _docTypeItemsService.GetViewBagModel();
            CreateViewBagData(viewBag);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Index(DocTypeItems search)
        {
            var data= await _docTypeItemsService.Index(search);
            return View(data);
        }
        public void CreateViewBagData(ViewBagDropDownModelModel docTypeItemsViewModel)
        {
            ViewBag.Color = docTypeItemsViewModel.Colors;
            ViewBag.Model = docTypeItemsViewModel.Models;
            ViewBag.Market = docTypeItemsViewModel.Markets;
        }

        public async Task<IActionResult> Create(int id)
        {
            var viewBag = await _docTypeItemsService.GetViewBagModel();
            CreateViewBagData(viewBag);
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