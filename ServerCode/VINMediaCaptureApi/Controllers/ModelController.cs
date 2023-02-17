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
    public class ModelController : ControllerBase
    {
        private readonly IRepository _repository;

        private readonly IQueryRepository _queryRepository;
        private readonly VINMediaCaptureDbContext _context;
        private readonly IRepository<VINMediaCaptureDbContext> _VINMediaCaptureRepository;

        private readonly ILogger<UserController> _logger;

        public ModelController(ILogger<UserController> logger, IRepository repository, IQueryRepository queryRepository, VINMediaCaptureDbContext context, IRepository<VINMediaCaptureDbContext> VINMediaCaptureRepository)
        {
            _logger = logger;
            _repository = repository;
            _context = context;
            _queryRepository = queryRepository;
            _VINMediaCaptureRepository = VINMediaCaptureRepository;
        }

        [HttpPost]
        [Route("Index")]
        public async Task<ModelViewModel> Index([FromBody]Model model)
        {
            var data = new ModelViewModel();
            data.Search = model;
            data.Models = _context.Model.Where(x=>(String.IsNullOrEmpty(model.ModelCode) || x.ModelCode.Contains(model.ModelCode)) ||
            (String.IsNullOrEmpty(model.ModelName) || x.ModelName.Contains(model.ModelName)) || (model.Disable<=0 || x.Disable == model.Disable)
            ).ToList();
            return data;
        }
        [HttpGet]
        [Route("GetById")]
        public async Task<Model> GetById(int id)
        {
            var model= _context.Model.Where(x => x.ModelID == id);
            if (model!=null && model.Any())
            {
                return model.First();
            }
            return null;
        }
        [HttpPost]
        [Route("Create")]
        public async Task<RestOutput<int>> Create(Model model)
        {
            var outPut = new RestOutput<int>();
            var check = _context.Model.Where(x => x.ModelCode == model.ModelCode && model.ModelID != x.ModelID);
            if (check!=null && check.Any())
            {
                outPut.ResultCode = -1;
                outPut.Message = "Đã tồn tại mã màu";
                return outPut;
            }
            if (model.ModelID>0)
            {
                var update= _context.Model.FirstOrDefault(x=>x.ModelID==model.ModelID);
                update.ModelName = model.ModelName;
                update.ModelCode = model.ModelCode;
                update.Disable = model.Disable;
                var r=_context.Model.Update(update);
            }
            else
            {
                var res = _context.Model.Add(model);
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
            var check = _context.Model.Where(x => x.ModelID == id).First();
            var res = _context.Model.Remove(check);
            _context.SaveChanges();
            outPut.ResultCode = 1;
            return outPut;
        }

    }
}