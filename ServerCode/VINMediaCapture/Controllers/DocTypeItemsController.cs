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
        private IWebHostEnvironment _hostingEnvironment;

        public DocTypeItemsController(ILogger<DocTypeItemsController> logger, IDocTypeItemsService docTypeItemsService, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _docTypeItemsService = docTypeItemsService;
            _hostingEnvironment = hostingEnvironment;
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
            var viewBag = await _docTypeItemsService.GetViewBagModel();
            CreateViewBagData(viewBag);
            var data= await _docTypeItemsService.Index(search);

            return View(data);
        }
        public void CreateViewBagData(ViewBagDropDownModelModel docTypeItemsViewModel)
        {
            ViewBag.Color = docTypeItemsViewModel.Colors;
            ViewBag.Model = docTypeItemsViewModel.Models;
            ViewBag.Market = docTypeItemsViewModel.Markets;
            ViewBag.DocTypes = docTypeItemsViewModel.DocTypes;
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
        public async Task<IActionResult> Create(DocTypeItems docTypeItems,IFormFile image)
        {
            if(image != null)
            {
                var folderPath = @"uploads\\DocTypeItems";
                string uploads = Path.Combine(_hostingEnvironment.WebRootPath, folderPath);
                if (image.Length > 0)
                {
                    var imgName= docTypeItems.ItemName + "_" + image.FileName;
                    docTypeItems.ItemImage = folderPath + @"\" + imgName;
                    string filePath = Path.Combine(uploads, imgName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                }
            }
            var data = await _docTypeItemsService.Create(docTypeItems);
            if (data.ResultCode<=0)
            {
                var viewBag = await _docTypeItemsService.GetViewBagModel();
                CreateViewBagData(viewBag);
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