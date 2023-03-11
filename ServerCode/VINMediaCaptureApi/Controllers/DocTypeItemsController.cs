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
        public async Task<DocTypeItemsViewModel> Index([FromBody] DocTypeItems docTypeItems)
        {
            var data = new DocTypeItemsViewModel();
            data.Search = docTypeItems;
            data.DataDocTypeItems = (from d in _context.DocTypeItems
                                     join da in _context.DocTypeItemAttr on d.ItemID equals da.ItemID
                                     join m in _context.Market on d.MarketID equals m.MarketID
                                     join c in _context.Color on d.ColorID equals c.ColorID
                                     join md in _context.Model on d.ModelID equals md.ModelID
                                     where (d.ItemName.Contains(docTypeItems.ItemName) || String.IsNullOrEmpty(docTypeItems.ItemName))
                                     && (d.ItemDescription.Contains(docTypeItems.ItemDescription) || String.IsNullOrEmpty(docTypeItems.ItemDescription))
                                     && (d.ModelID ==docTypeItems.ModelID || docTypeItems.ModelID<0)
                                     && (d.MarketID ==docTypeItems.MarketID || docTypeItems.MarketID < 0)
                                     && (d.ColorID ==docTypeItems.ColorID || docTypeItems.ColorID < 0)
                                     && (d.ManualCollect ==docTypeItems.ManualCollect)
                                     && (d.Disabled == docTypeItems.Disabled || docTypeItems.Disabled < 0)
                                     select new DocTypeItemsInfo
                                     {
                                         DocTypeItemAttr=da,
                                         DocTypeItems = d,
                                         Color = c,
                                         Model = md,
                                         Market = m
                                     }).ToList();
            return data;
        }
        [HttpGet]
        [Route("GetById")]
        public async Task<DocTypeItemAddModel> GetById(int id)
        {
            var docTypeItems = from d in _context.DocTypeItems.Where(x => x.ItemID == id)
                               join da in _context.DocTypeItemAttr.Where(x => x.ItemID == id) on d.ItemID equals da.ItemID
                               select new DocTypeItemAddModel
                               {
                                   DocTypeItems = d,
                                   DocTypeItemAttr = da
                               };
            if (docTypeItems != null && docTypeItems.Any())
            {
                return docTypeItems.First();
            }
            return null;
        }
        [HttpPost]
        [Route("Create")]
        public async Task<RestOutput<int>> Create(DocTypeItemAddModel docTypeItems)
        {
            var outPut = new RestOutput<int>();
            var check = _context.DocTypeItems.Where(x => x.DocTypeID == docTypeItems.DocTypeItems.DocTypeID && docTypeItems.DocTypeItems.ColorID == x.ColorID && x.ModelID == docTypeItems.DocTypeItems.ModelID
                                && x.MarketID == docTypeItems.DocTypeItems.MarketID && (x.ItemName ?? "").ToLower() == (docTypeItems.DocTypeItems.ItemName ?? "").ToLower() && x.ItemID != docTypeItems.DocTypeItems.ItemID && x.ItemID != docTypeItems.DocTypeItems.ItemID);
            if (check != null && check.Any())
            {
                outPut.ResultCode = -1;
                outPut.Message = "Đã tồn tại thông tin cần thu thập";
                return outPut;
            }
            try
            {
                if (docTypeItems.DocTypeItems.ItemID > 0)
                {
                    var update = _context.DocTypeItems.FirstOrDefault(x => x.ItemID == docTypeItems.DocTypeItems.ItemID);
                    if (update != null)
                    {
                        update.DocTypeID = docTypeItems.DocTypeItems.DocTypeID;
                        update.ItemDescription = docTypeItems.DocTypeItems.ItemDescription;
                        update.ItemName = docTypeItems.DocTypeItems.ItemName;
                        update.DisplayIDX = docTypeItems.DocTypeItems.DisplayIDX;
                        update.Disabled = docTypeItems.DocTypeItems.Disabled;
                        update.ColorID = docTypeItems.DocTypeItems.ColorID;
                        update.MarketID = docTypeItems.DocTypeItems.MarketID;
                        update.ColorID = docTypeItems.DocTypeItems.ColorID;
                        update.MarketID = docTypeItems.DocTypeItems.MarketID;
                        update.DisplayIDX = docTypeItems.DocTypeItems.DisplayIDX;
                        update.IsMandatory = docTypeItems.DocTypeItems.IsMandatory;
                        update.ManualCollect = docTypeItems.DocTypeItems.ManualCollect;
                        if (!String.IsNullOrEmpty(docTypeItems.DocTypeItems.ItemImage))
                        {
                            update.ItemImage = docTypeItems.DocTypeItems.ItemImage;
                        }
                        var r = _context.DocTypeItems.Update(update);
                        var updateDocTypeItemAttr = _context.DocTypeItemAttr.FirstOrDefault(x => x.AttrID == docTypeItems.DocTypeItemAttr.AttrID);
                        if (updateDocTypeItemAttr !=null)
                        {
                            updateDocTypeItemAttr.AttrDataType = docTypeItems.DocTypeItemAttr.AttrDataType;
                            updateDocTypeItemAttr.AttrDescription = docTypeItems.DocTypeItemAttr.AttrDescription;
                            updateDocTypeItemAttr.AttrName = docTypeItems.DocTypeItems.ItemName;
                            updateDocTypeItemAttr.isMandatory = docTypeItems.DocTypeItems.IsMandatory;
                            updateDocTypeItemAttr.isManualCollect = docTypeItems.DocTypeItems.ManualCollect;
                            updateDocTypeItemAttr.ItemID = docTypeItems.DocTypeItems.ItemID;
                            updateDocTypeItemAttr.Disabled = docTypeItems.DocTypeItems.Disabled;
                            updateDocTypeItemAttr.AttrImage = docTypeItems.DocTypeItems.ItemImage;
                        }
                    }
                }
                else
                {
                    var res = _context.DocTypeItems.Add(docTypeItems.DocTypeItems);
                    _context.SaveChanges();
                    docTypeItems.DocTypeItemAttr.AttrName=docTypeItems.DocTypeItems.ItemName;
                    docTypeItems.DocTypeItemAttr.isMandatory=docTypeItems.DocTypeItems.IsMandatory;
                    docTypeItems.DocTypeItemAttr.isManualCollect=docTypeItems.DocTypeItems.ManualCollect;
                    docTypeItems.DocTypeItemAttr.ItemID= docTypeItems.DocTypeItems.ItemID;
                    docTypeItems.DocTypeItemAttr.Disabled = docTypeItems.DocTypeItems.Disabled;
                    docTypeItems.DocTypeItemAttr.AttrImage = docTypeItems.DocTypeItems.ItemImage;
                    _context.DocTypeItemAttr.Add(docTypeItems.DocTypeItemAttr);

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
        public async Task<RestOutput<int>> DeleteById([FromBody] int id)
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