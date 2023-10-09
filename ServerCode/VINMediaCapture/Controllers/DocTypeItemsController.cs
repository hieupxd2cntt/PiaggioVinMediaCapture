using Microsoft.AspNetCore.Mvc;
using VINMediaCapture.Models;
using VINMediaCapture.Services;
using VINMediaCaptureEntities.Entities;
using System.Diagnostics;
using VINMediaCaptureEntities.ViewModel;
using VINMediaCapture.Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using VINMediaCaptureEntities.Enum;

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
            var data = await _docTypeItemsService.Index(new DocTypeItemsViewModel());
            var viewBag = await _docTypeItemsService.GetViewBagModel();
            CreateViewBagData(viewBag);
            data.Search.ManualCollect = true;
            data.Search.IsMandatory = true;
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Index(DocTypeItemsViewModel model)
        {
            var viewBag = await _docTypeItemsService.GetViewBagModel();
            CreateViewBagData(viewBag);
            var data= await _docTypeItemsService.Index(model);
            data.CurrPage = model.CurrPage;
            return View(data);
        }
        public void CreateViewBagData(ViewBagDropDownModelModel docTypeItemsViewModel)
        {
            ViewBag.Color = docTypeItemsViewModel.Colors;
            ViewBag.Model = docTypeItemsViewModel.Models;
            ViewBag.Market = docTypeItemsViewModel.Markets;
            ViewBag.DocTypes = docTypeItemsViewModel.DocTypes;
            var attrDataTypes = new List<SelectListItem>();
            attrDataTypes.Add(new SelectListItem { Value=((int)EAttrDataType.VARCHAR).ToString(),Text= EAttrDataType.VARCHAR.GetDescription()});
            attrDataTypes.Add(new SelectListItem { Value = ((int)EAttrDataType.INTEGER).ToString(), Text = EAttrDataType.INTEGER.GetDescription() });
            attrDataTypes.Add(new SelectListItem { Value = ((int)EAttrDataType.BOOLEAN).ToString(), Text = EAttrDataType.BOOLEAN.GetDescription() });
            attrDataTypes.Add(new SelectListItem { Value = ((int)EAttrDataType.DATE).ToString(), Text = EAttrDataType.DATE.GetDescription() });
            attrDataTypes.Add(new SelectListItem { Value = ((int)EAttrDataType.FLOAT).ToString(), Text = EAttrDataType.FLOAT.GetDescription() });
            attrDataTypes.Add(new SelectListItem { Value = ((int)EAttrDataType.IMGCAPT).ToString(), Text = EAttrDataType.IMGCAPT.GetDescription() });
            ViewBag.AttrDataTypes = attrDataTypes;
            //attrDataTypes.Add(new SelectListItem { Value = ((int)EAttrDataType.DATFILE).ToString(), Text = EAttrDataType.DATFILE.GetDescription() });

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
            return View(new DocTypeItemAddModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(DocTypeItemAddModel docTypeItemAdd,IFormFile image)
        {
            if(image != null)
            {
                var folderPath = @"uploads\\DocTypeItems";
                string uploads = Path.Combine(_hostingEnvironment.WebRootPath, folderPath);
                if (image.Length > 0)
                {
                    var imgName= docTypeItemAdd.DocTypeItems.ItemName + "_" + image.FileName;
                    docTypeItemAdd.DocTypeItems.ItemImage = folderPath + @"\" + imgName;
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
            var data = await _docTypeItemsService.Create(docTypeItemAdd);
            if (data.ResultCode<=0)
            {
                var viewBag = await _docTypeItemsService.GetViewBagModel();
                CreateViewBagData(viewBag);
                ViewBag.ErrorMsg = data.Message;
                return View(docTypeItemAdd);
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