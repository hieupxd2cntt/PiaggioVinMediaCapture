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
using VINMediaCaptureEntities.Enum;

namespace VINMediaCaptureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[IdentityBasicAuthentication]
    //[CustomAuthentication]
    public class DocTypeItemsController : ControllerBase
    {
        private readonly IRepository _repository;

        private readonly IQueryRepository _queryRepository;
        private readonly VINMediaCaptureDbContext _context;
        private readonly IRepository<VINMediaCaptureDbContext> _VINMediaCaptureRepository;

        private readonly ILogger<UserController> _logger;

        public DocTypeItemsController(ILogger<UserController> logger, IRepository repository, IQueryRepository queryRepository, VINMediaCaptureDbContext context, IRepository<VINMediaCaptureDbContext> VINMediaCaptureRepository)
        {
            _logger = logger;
            _repository = repository;
            _context = context;
            _queryRepository = queryRepository;
            _VINMediaCaptureRepository = VINMediaCaptureRepository;
        }
        [HttpGet]
        [Route("GetViewBagModel")]
        public async Task<ViewBagDropDownModelModel> GetViewBagModel()
        {
            var data = new ViewBagDropDownModelModel();
            data.Models = _context.Model.Where(x => x.Disable == (int)EStatus.Active).ToList();
            data.Colors = _context.Color.Where(x => x.Disable == (int)EStatus.Active).ToList();
            data.Markets = _context.Market.Where(x => x.Disabled == (int)EStatus.Active).ToList();
            data.DocTypes = _context.DocType.Where(x => x.Disabled == (int)EStatus.Active).ToList();
            return data;
        }
        [HttpPost]
        [Route("Index")]
        public async Task<DocTypeItemsViewModel> Index([FromBody]DocTypeItems docTypeItems)
        {
            var data = new DocTypeItemsViewModel();
            data.Search = docTypeItems;
            data.DataDocTypeItems = (from d in _context.DocTypeItems
                                     join m in _context.Market on d.MarketID equals m.MarketID
                                     join c in _context.Color on d.ColorID equals c.ColorID
                                     join md in _context.Model on d.ModelID equals md.ModelID
                                     select new DocTypeItemsInfo { DocTypeItems=d,Color= c,Model=md,Market=m
                                     }).ToList();
            return data;
        }
        [HttpGet]
        [Route("GetById")]
        public async Task<DocTypeItems> GetById(int id)
        {
            var docTypeItems= _context.DocTypeItems.Where(x => x.ItemID == id);
            if (docTypeItems!=null && docTypeItems.Any())
            {
                return docTypeItems.First();
            }
            return null;
        }
        [HttpPost]
        [Route("Create")]
        public async Task<RestOutput<int>> Create(DocTypeItems docTypeItems)
        {
            var outPut = new RestOutput<int>();
            var check = _context.DocTypeItems.Where(x => x.DocTypeID == docTypeItems.DocTypeID && docTypeItems.ColorID == x.ColorID && x.ModelID == docTypeItems.ModelID 
                                && x.MarketID == docTypeItems.MarketID && (x.ItemName??"").ToLower() == (docTypeItems.ItemName??"").ToLower() && x.ItemID != docTypeItems.ItemID && x.ItemID != docTypeItems.ItemID);
            if (check != null && check.Any())
            {
                outPut.ResultCode = -1;
                outPut.Message = "Đã tồn tại thông tin cần thu thập";
                return outPut;
            }
            try
            {
                if (docTypeItems.ItemID > 0)
                {
                    var update = _context.DocTypeItems.FirstOrDefault(x => x.ItemID == docTypeItems.ItemID);
                    if (update != null)
                    {
                        update.DocTypeID = docTypeItems.DocTypeID;
                        update.ItemDescription = docTypeItems.ItemDescription;
                        update.ItemName = docTypeItems.ItemName;
                        update.DisplayIDX = docTypeItems.DisplayIDX;
                        update.Disabled = docTypeItems.Disabled;
                        update.ColorID = docTypeItems.ColorID;
                        update.MarketID = docTypeItems.MarketID;
                        update.ColorID = docTypeItems.ColorID;
                        update.MarketID = docTypeItems.MarketID;
                        update.DisplayIDX = docTypeItems.DisplayIDX;
                        update.IsMandatory = docTypeItems.IsMandatory;
                        update.ManualCollect = docTypeItems.ManualCollect;
                        if (!String.IsNullOrEmpty(docTypeItems.ItemImage))
                        {
                            update.ItemImage = docTypeItems.ItemImage;
                        }
                        var r = _context.DocTypeItems.Update(update);
                    }
                }
                else
                {
                    var res = _context.DocTypeItems.Add(docTypeItems);
                }
            }
            catch (Exception ex)
            {

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
            var check = _context.DocTypeItems.Where(x => x.ItemID == id).First();
            var res = _context.DocTypeItems.Remove(check);
            _context.SaveChanges();
            outPut.ResultCode = 1;
            return outPut;
        }

    }
}