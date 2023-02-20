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
    public class ColorController : ControllerBase
    {
        private readonly IRepository _repository;

        private readonly IQueryRepository _queryRepository;
        private readonly VINMediaCaptureDbContext _context;
        private readonly IRepository<VINMediaCaptureDbContext> _VINMediaCaptureRepository;

        private readonly ILogger<UserController> _logger;

        public ColorController(ILogger<UserController> logger, IRepository repository, IQueryRepository queryRepository, VINMediaCaptureDbContext context, IRepository<VINMediaCaptureDbContext> VINMediaCaptureRepository)
        {
            _logger = logger;
            _repository = repository;
            _context = context;
            _queryRepository = queryRepository;
            _VINMediaCaptureRepository = VINMediaCaptureRepository;
        }

        [HttpPost]
        [Route("Index")]
        public async Task<ColorViewModel> Index([FromBody]Color color)
        {
            var data = new ColorViewModel();
            data.Search = color;
            data.Colors = _context.Color.Where(x=>(String.IsNullOrEmpty(color.ColorCode) || x.ColorCode.Contains(color.ColorCode)) &&
            (String.IsNullOrEmpty(color.ColorName) || x.ColorName.Contains(color.ColorName)) && ((color.Disable??0)<=0 || x.Disable == color.Disable)
            ).OrderByDescending(x => x.ColorID).ToList();
            return data;
        }
        [HttpGet]
        [Route("GetById")]
        public async Task<Color> GetById(int id)
        {
            var color= _context.Color.Where(x => x.ColorID == id);
            if (color!=null && color.Any())
            {
                return color.First();
            }
            return null;
        }
        [HttpPost]
        [Route("Create")]
        public async Task<RestOutput<int>> Create(Color color)
        {
            var outPut = new RestOutput<int>();
            var check = _context.Color.Where(x => x.ColorCode == color.ColorCode && color.ColorID != x.ColorID);
            if (check!=null && check.Any())
            {
                outPut.ResultCode = -1;
                outPut.Message = "Đã tồn tại mã màu";
                return outPut;
            }
            if (color.ColorID>0)
            {
                var update= _context.Color.FirstOrDefault(x=>x.ColorID==color.ColorID);
                update.ColorName = color.ColorName;
                update.ColorCode = color.ColorCode;
                update.Disable = color.Disable;
                var r=_context.Color.Update(update);
            }
            else
            {
                var res = _context.Color.Add(color);
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
            var check = _context.Color.Where(x => x.ColorID == id).First();
            var res = _context.Color.Remove(check);
            _context.SaveChanges();
            outPut.ResultCode = 1;
            return outPut;
        }

    }
}