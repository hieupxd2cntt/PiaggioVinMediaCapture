using Microsoft.AspNetCore.Mvc;
using VINMediaCapture.Models;
using VINMediaCapture.Services;
using VINMediaCaptureEntities.Entities;
using System.Diagnostics;
using VINMediaCaptureEntities.ViewModel;
using VINMediaCapture.Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using VINMediaCaptureEntities.Enum;
using System.Reflection;

namespace VINMediaCapture.Controllers
{
    public class ProductDocController : BaseController
    {
        private readonly ILogger<DocTypeItemsController> _logger;
        private readonly IProductDocService _productDocService;
        private readonly IDocTypeItemsService _docTypeItemsService;
        private IWebHostEnvironment _hostingEnvironment;

        public ProductDocController(ILogger<DocTypeItemsController> logger, IDocTypeItemsService docTypeItemsService, IProductDocService productDocService, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _docTypeItemsService = docTypeItemsService;
            _productDocService = productDocService;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _productDocService.Index(new ProductDocViewModel());
            var viewBag = await _docTypeItemsService.GetViewBagModel();
            CreateViewBagData(viewBag);
            var fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            data.ToDate = fromDate.AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");
            data.FromDate = fromDate.ToString("dd/MM/yyyy");
            return View(data);
        }
        public async Task<IActionResult> LoadDetailProduct(string vinCode,int productDoc)
        {
            var data = await _productDocService.LoadDetailProductDoc(vinCode, productDoc);
            return PartialView(@"~/Views/ProductDoc/DetailProductDoc.cshtml", data);
        }
        [HttpPost]
        public async Task<IActionResult> Index(ProductDocViewModel model)
        {
            var viewBag = await _docTypeItemsService.GetViewBagModel();
            CreateViewBagData(viewBag);
            var data= await _productDocService.Index(model);
            model.ProductDocInfos = data.ProductDocInfos;
            model.TotalRecord = data.TotalRecord;
            return View(model);
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
    }
}