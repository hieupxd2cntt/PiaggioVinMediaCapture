using Microsoft.AspNetCore.Mvc;
using VINMediaCapture.Models;
using VINMediaCapture.Services;
using VINMediaCaptureEntities.Entities;
using System.Diagnostics;
using VINMediaCaptureEntities.ViewModel;
using VINMediaCapture.Service;
using VINMediaCaptureEntities.Enum;

namespace VINMediaCapture.Controllers
{
    public class SettingController : BaseController
    {
        private readonly ILogger<ColorController> _logger;
        private readonly IColorService _colorService;
        private readonly IAllCodeService _allCodeService;
        public SettingController(ILogger<ColorController> logger, IColorService colorService, IAllCodeService allCodeService)
        {
            _logger = logger;
            _colorService = colorService;
            _allCodeService = allCodeService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _allCodeService.LoadAllCodeByType(EAllCode.ScanSetting.GetMapping());
            if (data == null)
            {
                return View(new List<AllCode>());
            }
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Index(List<AllCode> model)
        {
            var data= await _allCodeService.SaveScanSettings(model);
            if (data.ResultCode>0)
            {
                return RedirectToAction("Index","Setting");
            }
            ViewBag.ErrorMsg = "Không thể lưu cấu hình";
            return View(model);
        }

       
    }
}