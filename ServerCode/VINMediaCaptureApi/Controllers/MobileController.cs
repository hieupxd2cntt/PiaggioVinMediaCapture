using AspNetCoreApp.Data;
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

namespace VINMediaCaptureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[IdentityBasicAuthentication]
    //[CustomAuthentication]
    public class MobileController : ControllerBase
    {
        private readonly IRepository _repository;

        private readonly IQueryRepository _queryRepository;
        private readonly VINMediaCaptureDbContext _context;
        private readonly IRepository<VINMediaCaptureDbContext> _VINMediaCaptureRepository;
        public IConfiguration _Configuration { get; }
        private readonly ILogger<UserController> _logger;

        public MobileController(ILogger<UserController> logger, IRepository repository, IQueryRepository queryRepository, VINMediaCaptureDbContext context, IRepository<VINMediaCaptureDbContext> VINMediaCaptureRepository, IConfiguration Configuration)
        {
            _logger = logger;
            _repository = repository;
            _context = context;
            _queryRepository = queryRepository;
            _VINMediaCaptureRepository = VINMediaCaptureRepository;
            _Configuration = Configuration;
        }

        [HttpPost]
        [Route("GetListAttribute")]
        public async Task<string> GetListAttribute([FromBody] string barcode)
        {
            var data = from d in _context.DocTypeItems
                       join da in _context.DocTypeItemAttr on d.ItemID equals da.ItemID
                       join m in _context.Model on d.ModelID equals m.ModelID
                       join ma in _context.Market on d.MarketID equals ma.MarketID
                       join c in _context.Color on d.ColorID equals c.ColorID
                       where m.ModelCode + ma.MarketCode + c.ColorCode == barcode
                       && d.ManualCollect == true
                       orderby d.DisplayIDX
                       select new DocTypeItemAddModel
                       {
                           Model = m,
                           DocTypeItems = d,
                           DocTypeItemAttr = da
                       };
            return JsonSerializer.Serialize(data);
            //return JsonSerializer.Serialize(data, new JsonSerializerOptions { IgnoreNullValues = true });
        }
        [HttpPost]
        [Route("GetListAttributeByVinCode")]
        public async Task<string> GetListAttributeByVinCode([FromBody] string vincode)
        {
            var vincodeData = from p in _context.ProductDoc
                              join pd in _context.ProductDocVal on p.Id equals pd.ProductDocId
                              join m in _context.Model on p.ModelID equals m.ModelID
                              join ma in _context.Market on p.MarketID equals ma.MarketID
                              join c in _context.Color on p.ColorID equals c.ColorID
                              where p.VINCode == vincode
                              orderby p.Id descending
                              select new { Barcode= m.ModelCode + ma.MarketCode + c.ColorCode,
                              p,pd
                              };
            string barcode = "";
            if (vincodeData != null && vincodeData.Any())
            {

                barcode = vincodeData.First().Barcode;
            }
            if (!string.IsNullOrEmpty(barcode))
            {
                var data = (from d in _context.DocTypeItems
                           join da in _context.DocTypeItemAttr on d.ItemID equals da.ItemID
                           join m in _context.Model on d.ModelID equals m.ModelID
                           join ma in _context.Market on d.MarketID equals ma.MarketID
                           join c in _context.Color on d.ColorID equals c.ColorID
                           where m.ModelCode + ma.MarketCode + c.ColorCode == barcode
                           && d.ManualCollect == true
                           orderby d.DisplayIDX
                           select new DocTypeItemAddModel
                           {
                               Model = m,
                               DocTypeItems = d,
                               DocTypeItemAttr = da
                           }).ToList();
                foreach (var item in data)
                {
                    var proDocValue = vincodeData.Where(x => x.pd.AttrID == item.DocTypeItemAttr.AttrID);
                    if (proDocValue !=null && proDocValue.Any())
                    {
                        item.ProdocValue = proDocValue.First().pd.AttrValue;
                    }
                }
                return JsonSerializer.Serialize(data);
            }
            return "";
            //return JsonSerializer.Serialize(data, new JsonSerializerOptions { IgnoreNullValues = true });
        }
        //[HttpPost]
        //[Route("InsertDocTypeGuide")]
        //public async Task<MobileResult> InsertDocTypeGuide(MobileDoctypeGuideInsertModel docType)
        //{
        //    var outPut = new MobileResult();
        //    outPut.Message = "AAAAAAAAAAAAAA";
        //    outPut.ResultCode = 1;
        //    return outPut;
        //}
        [HttpPost]
        [Route("InsertDocTypeGuide")]
        public async Task<MobileResult> InsertDocTypeGuide([FromForm] string docTypeGuides, List<IFormFile> images)
        {
            var serialNumber = HttpContext.Request.Headers["deviceid"].ToString();
            var userName = HttpContext.Request.Headers["UserName"].ToString();
            var users = _context.Users.Where(x => x.LoginName == userName);
            if (users == null && !users.Any())
            {
                return new MobileResult { ResultCode = -1, Message = "Không tìm thấy user" };
            }
            var outPut = new MobileResult();
            var productDoc = JsonSerializer.Deserialize<List<DocTypeModelListData>>(docTypeGuides);
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
                                   select d;

                var docType = docTypeItems.FirstOrDefault();
                var proDoc = new ProductDoc
                {
                    MarketID = docType.MarketID ?? 0,
                    ModelID = docType.ModelID ?? 0,
                    ColorID = docType.ColorID ?? 0,
                    DocTypeID = productDoc.FirstOrDefault().attrDocType,
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

                    foreach (var item in productDoc)
                    {
                        if (item.attrDataType == (int)EAttrDataType.IMGCAPT)
                        {
                            item.textValue = String.Format(@"\uploads\ProductDocVal\{0}\{1}", vincode, Path.GetFileName(item.textValue));
                        }
                        else if (item.attrDataType == (int)EAttrDataType.BOOLEAN)
                        {
                            item.textValue = item.valueCheckBox.ToString();
                        }
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

                var physicalPath = String.Format("{0}{1}{2}", _Configuration.GetSection("ConfigApi")["PhysicalPathApp"], @"\uploads\ProductDocVal\", vincode);
                if (!Directory.Exists(physicalPath))
                {
                    Directory.CreateDirectory(physicalPath);
                }
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
                }
            }

            _context.SaveChanges();
            outPut.Message = "";
            outPut.ResultCode = 1;
            return outPut;
        }
    }
}