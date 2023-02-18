using Microsoft.AspNetCore.Mvc;
using VINMediaCapture.Models;
using VINMediaCapture.Services;
using VINMediaCaptureEntities.Entities;
using System.Diagnostics;
using VINMediaCaptureEntities.ViewModel;
using VINMediaCapture.Service;

namespace VINMediaCapture.Controllers
{
    public class MarketController : BaseController
    {
        private readonly ILogger<MarketController> _logger;
        private readonly IMarketService _marketService;
        public MarketController(ILogger<MarketController> logger, IMarketService marketService)
        {
            _logger = logger;
            _marketService = marketService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _marketService.Index(new Market());
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Index(Market search)
        {
            var data= await _marketService.Index(search);
            return View(data);
        }

        public async Task<IActionResult> Create(int id)
        {
            if (id>0)
            {
                var market = await _marketService.GetById(id);
                return View(market);
            }
            return View(new Market());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Market market)
        {
            var data = await _marketService.Create(market);
            if (data.ResultCode<=0)
            {
                ViewBag.ErrorMsg = data.Message;
                return View(market);
            }

            return RedirectToAction("Index","Market");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _marketService.DeleteById(id);
            return Json(data);
        }
    }
}