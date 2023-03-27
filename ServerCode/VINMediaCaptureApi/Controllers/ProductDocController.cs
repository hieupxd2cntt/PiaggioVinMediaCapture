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
using VINMediaCaptureEntities.CommonFunction;

namespace VINMediaCaptureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[IdentityBasicAuthentication]
    //[CustomAuthentication]
    public class ProductDocController : ControllerBase
    {
        private readonly IRepository _repository;

        private readonly IQueryRepository _queryRepository;
        private readonly VINMediaCaptureDbContext _context;
        private readonly IRepository<VINMediaCaptureDbContext> _VINMediaCaptureRepository;

        private readonly ILogger<UserController> _logger;

        public ProductDocController(ILogger<UserController> logger, IRepository repository, IQueryRepository queryRepository, VINMediaCaptureDbContext context, IRepository<VINMediaCaptureDbContext> VINMediaCaptureRepository)
        {
            _logger = logger;
            _repository = repository;
            _context = context;
            _queryRepository = queryRepository;
            _VINMediaCaptureRepository = VINMediaCaptureRepository;
        }

        [HttpPost]
        [Route("Index")]
        public async Task<ProductDocViewModel> Index([FromBody] ProductDocViewModel search)
        {
            var fromDate= search.FromDate.StringToDateTime();
            var toDate = search.ToDate.StringToDateTime();
            var data = from pd in _context.ProductDoc.Where(x=>(x.MarketID == search.Search.MarketID || search.Search.MarketID<=0)
                       && (x.ModelID == search.Search.ModelID || search.Search.ModelID <= 0)
                       && (x.ColorID == search.Search.ColorID || search.Search.ColorID <= 0)
                       && (x.VINCode.Contains(search.Search.VINCode) || String.IsNullOrEmpty( search.Search.VINCode))
                       && (x.DocTypeDate >= fromDate || x.DocTypeDate <= toDate)
                       )
                       join dt in _context.DocType on pd.DocTypeID equals dt.DocTypeID
                       //join pdv in _context.ProductDocVal on pd.Id equals pdv.ProductDocId
                       join c in _context.Color on pd.ColorID equals c.ColorID
                       join m in _context.Market on pd.MarketID equals m.MarketID
                       join u in _context.Users on pd.UserID equals u.UserID
                       join md in _context.Model on pd.ModelID equals md.ModelID
                       select new ProductDocInfo
                       {
                           Color=c,
                           Market=m,
                           Model=md,
                           //ProductDocVal=pdv,
                           ProductDoc=pd,
                           DocType= dt,
                           User=u
                       };
            search.TotalRecord = data.Count();
            if (search.TotalRecord>search.PageSize)
            {
                search.ProductDocInfos =data.Skip((search.CurrPage - 1) * search.PageSize).Take(search.PageSize).ToList();
            }
            else
            {
                search.ProductDocInfos = data.ToList();
            }
            return search;
        }

        [HttpGet]
        [Route("LoadDetailProductDoc")]
        public async Task<DetailProductDocModel> LoadDetailProductDoc(string vinCode, int productDoc)
        {
            var model = new DetailProductDocModel();
            var data = from pd in _context.ProductDoc.Where(x => (x.VINCode.ToLower() == vinCode.ToLower() && x.Id==productDoc))
                       join dt in _context.DocType on pd.DocTypeID equals dt.DocTypeID
                       join pdv in _context.ProductDocVal on pd.Id equals pdv.ProductDocId
                       join di in _context.DocTypeItems on pdv.ItemID equals di.ItemID
                       join u in _context.Users on pd.UserID equals u.UserID
                       join dia in _context.DocTypeItemAttr on pdv.AttrID equals dia.AttrID
                       select new ProductDocValInfo
                       {
                           ProductDocVal= pdv,
                           DocTypeItemAttr=dia,
                           DocTypeItem=di,
                           User=u,
                       };
            model.ProductDocValInfo = data.ToList();
            return model;
        }
    }
}