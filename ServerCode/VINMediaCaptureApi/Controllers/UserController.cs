using AspNetCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VINMediaCaptureEntities.Entities;
using VINMediaCaptureEntities.Model;
using System.Text.Json.Serialization;
using TanvirArjel.EFCore.GenericRepository;
using VINMediaCaptureEntities;
using VINMediaCaptureEntities.CommonFunction;
using VINMediaCaptureEntities.ViewModel;

namespace VINMediaCaptureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[IdentityBasicAuthentication]
    //[CustomAuthentication]
    public class UserController : ControllerBase
    {
        private readonly IRepository _repository;

        private readonly IQueryRepository _queryRepository;
        private readonly VINMediaCaptureDbContext _context;
        private readonly IRepository<VINMediaCaptureDbContext> _VINMediaCaptureRepository;

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IRepository repository, IQueryRepository queryRepository, VINMediaCaptureDbContext context, IRepository<VINMediaCaptureDbContext> VINMediaCaptureRepository)
        {
            _logger = logger;
            _repository = repository;
            _context = context;
            _queryRepository = queryRepository;
            _VINMediaCaptureRepository = VINMediaCaptureRepository;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<Users> Login(Users user)
        {
            try
            {
                await _context.Database.OpenConnectionAsync(default);
                var passWord = SystemMethod.sha256_hash(user.Password);
                var userData = (from u in _context.Users.Where(x => x.LoginName.ToLower() == user.LoginName && x.Password == passWord)
                                select u)
                               .ToList();
                if (userData != null && userData.Any())
                {
                    return userData.FirstOrDefault();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                await _context.Database.CloseConnectionAsync();
            }
            return null;
        }
        [HttpPost]
        [Route("Login1")]
        public UserLoginModel Login1()
        {
            var userData = (from u in _context.Users
                            select new UserLoginModel
                            {
                                User = u

                            })
                              .ToList();
            return userData.FirstOrDefault();


        }

        [HttpPost]
        [Route("Index")]
        public async Task<UserViewModel> Index([FromBody] Users model)
        {
            var data = new UserViewModel();
            data.Search = model;
            data.Users = _context.Users.Where(x => (String.IsNullOrEmpty(model.LoginName) || x.LoginName.Contains(model.LoginName)) &&
            ((model.Disabled ?? 0) <= 0 || x.Disabled == model.Disabled)
            ).OrderByDescending(x => x.UserID).ToList();
            return data;
        }
        [HttpGet]
        [Route("GetById")]
        public async Task<Users> GetById(int id)
        {
            var model = _context.Users.Where(x => x.UserID == id);
            if (model != null && model.Any())
            {
                return model.First();
            }
            return null;
        }
        [HttpPost]
        [Route("Create")]
        public async Task<RestOutput<int>> Create(Users model)
        {
            var outPut = new RestOutput<int>();
            var check = _context.Users.Where(x => x.LoginName == model.LoginName && model.UserID != x.UserID);
            if (check != null && check.Any())
            {
                outPut.ResultCode = -1;
                outPut.Message = "Đã tồn tại người dùng";
                return outPut;
            }
            if (model.UserID > 0)
            {
                var update = _context.Users.FirstOrDefault(x => x.UserID == model.UserID);
                update.LoginName = model.LoginName;
                if (!string.IsNullOrEmpty(model.Password))
                {
                    update.Password = SystemMethod.sha256_hash(model.Password);
                }
                
                update.Disabled = model.Disabled;
                var r = _context.Users.Update(update);
            }
            else
            {
                if (!string.IsNullOrEmpty(model.Password))
                {
                    model.Password = SystemMethod.sha256_hash("123456");
                }
                model.Created = DateTime.Now;
                model.PWLastUpdated = DateTime.Now;
                var res = _context.Users.Add(model);
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
            var check = _context.Users.Where(x => x.UserID == id).First();
            var res = _context.Users.Remove(check);
            _context.SaveChanges();
            outPut.ResultCode = 1;
            return outPut;
        }
    }
}