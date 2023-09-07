﻿using AspNetCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VINMediaCaptureEntities.Entities;
using VINMediaCaptureEntities.Model;
using System.Text.Json.Serialization;
using TanvirArjel.EFCore.GenericRepository;
using VINMediaCaptureEntities.ViewModel;
using VINMediaCaptureEntities;
using System.Data.Entity;
using System.Text.Json;
using VINMediaCaptureEntities.Enum;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace VINMediaCaptureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[IdentityBasicAuthentication]
    //[CustomAuthentication]
    public class TaiwainController : ControllerBase
    {
        private readonly IRepository _repository;

        private readonly IQueryRepository _queryRepository;
        private readonly VINMediaCaptureDbContext _context;
        private readonly IRepository<VINMediaCaptureDbContext> _VINMediaCaptureRepository;
        public IConfiguration _Configuration { get; }
        private readonly ILogger<UserController> _logger;

        public TaiwainController(ILogger<UserController> logger, IRepository repository, IQueryRepository queryRepository, VINMediaCaptureDbContext context, IRepository<VINMediaCaptureDbContext> VINMediaCaptureRepository, IConfiguration Configuration)
        {
            _logger = logger;
            _repository = repository;
            _context = context;
            _queryRepository = queryRepository;
            _VINMediaCaptureRepository = VINMediaCaptureRepository;
            _Configuration = Configuration;
        }

        [HttpPost]
        [Route("InsertDoc")]
        public async Task<MobileResult> InsertDoc([FromBody] List<IFormFile> files)
        {

            var outPut = new MobileResult();
            outPut.Message = "";
            outPut.ResultCode = 1;
            return outPut;
        }
        [HttpPost]
        [Route("InsertDocContent")]
        public async Task<MobileResult> InsertDocContent(string Title)
        {

            var outPut = new MobileResult();
            outPut.Message = "";
            outPut.ResultCode = 1;
            return outPut;
        }

        [HttpPost]
        [Route("UploadFileAsync")]
        public async Task<MobileResult> UploadFileAsync([FromForm] string docTypeModelListData)
        {
            var images = HttpContext.Request.Form.Files;
            var outPut = new MobileResult();
            var serialNumber = HttpContext.Request.Host.Host;
            var userName = HttpContext.Request.Headers["UserName"].ToString();
            var users = _context.Users.Where(x => x.LoginName == userName);
            if (users == null || !users.Any())
            {
                return new MobileResult { ResultCode = -1, Message = "Không tìm thấy user" };
            }
            var productDoc = JsonSerializer.Deserialize<List<DocTypeModelListData>>(docTypeModelListData);
            var currentSession = "";
            if (productDoc != null && productDoc.Any())
            {
                currentSession = productDoc.FirstOrDefault().currentSession;
            }
            if (!String.IsNullOrEmpty(currentSession))
            {
                var barcode = currentSession.Split('-')[0];
                var docTypeItems = from d in _context.DocTypeItems
                                   join da in _context.DocTypeItemAttr on d.ItemID equals da.ItemID
                                   join m in _context.Model on d.ModelID equals m.ModelID
                                   join ma in _context.Market on d.MarketID equals ma.MarketID
                                   join c in _context.Color on d.ColorID equals c.ColorID
                                   where m.ModelCode + ma.MarketCode + c.ColorCode == barcode
                                   && d.DocTypeID== (int)EDocType.ThuThapTaiLieu
                                   select d;

                var docType = docTypeItems.FirstOrDefault();
                var proDoc = new ProductDoc
                {
                    MarketID = docType.MarketID ?? 0,
                    ModelID = docType.ModelID ?? 0,
                    ColorID = docType.ColorID ?? 0,
                    DocTypeID =(int)EDocType.ThuThapTaiLieu,
                    DocTypeDate = DateTime.Now,
                    UserID = users.First().UserID,
                    WS_ID = 0,
                    SerialNumber = serialNumber,
                    VINCode = currentSession.Split('-')[1]
                };
                _context.ProductDoc.Add(proDoc);
                _context.SaveChanges();
                var vincode = currentSession.Split('-')[1];
                if (docType != null)
                {
                    var physicalPath = String.Format("{0}{1}", _Configuration.GetSection("ConfigApi")["PhysicalPathApp"], String.Format(@"\uploads\TaiwanScan\{0}\{1}", barcode, vincode));
                    if (!Directory.Exists(physicalPath))
                    {
                        Directory.CreateDirectory(physicalPath);
                    }
                    foreach (var item in productDoc)
                    {
                        foreach (var img in images)
                        {
                            if (img.Length > 0)
                            {
                                string filePath = Path.Combine(physicalPath, img.FileName);
                                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                                {
                                    await img.CopyToAsync(fileStream);
                                }
                            }
                            item.textValue = String.Format(@"\uploads\TaiwanScan\{0}\{1}\{2}", barcode, vincode, Path.GetFileName(img.FileName));
                            var productDocVal = new ProductDocVal
                            {
                                AttrID = item.attrId,
                                //GuideImg = Path.GetFileName(item.assetImage),
                                //GuideTXT = item.textValue,
                                VINCode = currentSession.Split('-')[1],
                                AttrValue = item.textValue,
                                ItemID = item.itemId,
                                ProductDocId = proDoc.Id,
                                DocType_ID = productDoc.FirstOrDefault().attrDocType
                            };
                            _context.ProductDocVal.Add(productDocVal);
                        }                        
                    }
                }
            }

            _context.SaveChanges();
            outPut.Message = "";
            outPut.ResultCode = 1;
            return outPut;
        }
    }
}