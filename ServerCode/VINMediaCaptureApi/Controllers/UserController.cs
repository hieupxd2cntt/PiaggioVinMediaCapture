using AspNetCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VINMediaCaptureEntities.Entities;
using VINMediaCaptureEntities.Model;
using System.Text.Json.Serialization;
using TanvirArjel.EFCore.GenericRepository;

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
                var passWord = user.Password;
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
    }
}