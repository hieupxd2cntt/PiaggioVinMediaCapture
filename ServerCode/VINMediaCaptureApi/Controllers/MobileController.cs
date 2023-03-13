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
        public async Task<string> GetListAttribute([FromBody]string barcode)
        {
            var data = from d in _context.DocTypeItems
                       join da in _context.DocTypeItemAttr on d.ItemID equals da.ItemID
                       join m in _context.Model on d.ModelID equals m.ModelID
                       join ma in _context.Market on d.MarketID equals ma.MarketID
                       join c in _context.Color on d.ColorID equals c.ColorID
                       where m.ModelCode + ma.MarketCode + c.ColorCode == barcode
                       && d.ManualCollect ==true
                       orderby d.DisplayIDX
                       select new DocTypeItemAddModel {
                        DocTypeItems=d,
                        DocTypeItemAttr=da
                       };
            return JsonSerializer.Serialize(data);
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
        public async Task<MobileResult> InsertDocTypeGuide([FromForm]string docTypeGuides, List<IFormFile> images)
        {
             var outPut = new MobileResult();
            var mobileDoctypeGuides= JsonSerializer.Deserialize<List<DocTypeModelListData>>(docTypeGuides);
            
            var currentSession = "";
            if (mobileDoctypeGuides!= null && mobileDoctypeGuides.Any()) {
                currentSession = mobileDoctypeGuides.FirstOrDefault().currentSession;
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
                var docType=docTypeItems.FirstOrDefault();
                if (docType !=null) {
                    foreach (var item in mobileDoctypeGuides)
                    {
                        var docTypeGuide = new DocTypeGuide { AttrID = item.attrId, ColorID = docType.ColorID ?? 0, DocTypeID = item.attrDocType,
                            GuideImg = Path.GetFileName(item.assetImage),
                            GuideTXT = item.textValue,
                            ItemID=item.itemId,MarketID=docType.MarketID??0,ModelID = docType.ModelID??0
                        };
                        _context.DocTypeGuide.Add(docTypeGuide);
                    }
                }
                var vincode = currentSession.Split('-')[1];
                var physicalPath = String.Format("{0}{1}{2}", _Configuration.GetSection("ConfigApi")["PhysicalPathApp"], @"\uploads\DocTypeGuide\",vincode);
                if(!Directory.Exists(physicalPath))
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