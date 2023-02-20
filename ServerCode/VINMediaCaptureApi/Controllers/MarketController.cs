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

namespace VINMediaCaptureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[IdentityBasicAuthentication]
    //[CustomAuthentication]
    public class MarketController : ControllerBase
    {
        private readonly IRepository _repository;

        private readonly IQueryRepository _queryRepository;
        private readonly VINMediaCaptureDbContext _context;
        private readonly IRepository<VINMediaCaptureDbContext> _VINMediaCaptureRepository;

        private readonly ILogger<UserController> _logger;

        public MarketController(ILogger<UserController> logger, IRepository repository, IQueryRepository queryRepository, VINMediaCaptureDbContext context, IRepository<VINMediaCaptureDbContext> VINMediaCaptureRepository)
        {
            _logger = logger;
            _repository = repository;
            _context = context;
            _queryRepository = queryRepository;
            _VINMediaCaptureRepository = VINMediaCaptureRepository;
        }

        [HttpPost]
        [Route("Index")]
        public async Task<MarketViewModel> Index([FromBody]Market market)
        {
            var data = new MarketViewModel();
            data.Search = market;
            data.Markets = _context.Market.Where(x=>(String.IsNullOrEmpty(market.MarketCode) || x.MarketCode.Contains(market.MarketCode)) &&
            (String.IsNullOrEmpty(market.MarketName) || x.MarketName.Contains(market.MarketName)) && ((market.Disabled??0)<=0 || x.Disabled == market.Disabled)
            ).OrderByDescending(x=>x.MarketID).ToList();
            return data;
        }
        [HttpGet]
        [Route("GetById")]
        public async Task<Market> GetById(int id)
        {
            var market= _context.Market.Where(x => x.MarketID == id);
            if (market!=null && market.Any())
            {
                return market.First();
            }
            return null;
        }
        [HttpPost]
        [Route("Create")]
        public async Task<RestOutput<int>> Create(Market market)
        {
            var outPut = new RestOutput<int>();
            var check = _context.Market.Where(x => x.MarketCode == market.MarketCode && market.MarketID != x.MarketID);
            if (check!=null && check.Any())
            {
                outPut.ResultCode = -1;
                outPut.Message = "Đã tồn tại mã màu";
                return outPut;
            }
            if (market.MarketID>0)
            {
                var update= _context.Market.FirstOrDefault(x=>x.MarketID==market.MarketID);
                update.MarketName = market.MarketName;
                update.MarketCode = market.MarketCode;
                update.Disabled = market.Disabled;
                var r=_context.Market.Update(update);
            }
            else
            {
                var res = _context.Market.Add(market);
            }
            
            _context.SaveChanges();
            outPut.ResultCode = 1;
            return outPut;
        }
        [HttpPost]
        [Route("DeleteById")]
        public async Task<RestOutput<int>> DeleteById([FromBody]int id)
        {
            var outPut = new RestOutput<int>();
            var check = _context.Market.Where(x => x.MarketID == id).First();
            var res = _context.Market.Remove(check);
            _context.SaveChanges();
            outPut.ResultCode = 1;
            return outPut;
        }

    }
}