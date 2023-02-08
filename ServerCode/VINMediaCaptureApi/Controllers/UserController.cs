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
        private readonly PharmacyDbContext _context;
        private readonly IRepository<PharmacyDbContext> _pharmacyRepository;

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IRepository repository, IQueryRepository queryRepository, PharmacyDbContext context, IRepository<PharmacyDbContext> pharmacyRepository)
        {
            _logger = logger;
            _repository = repository;
            _context = context;
            _queryRepository = queryRepository;
            _pharmacyRepository = pharmacyRepository;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<UserLoginModel> Login(User user)
        {
            try
            {
                await _context.Database.OpenConnectionAsync(default);
                var userData = (from u in _context.User.Where(x => x.UserName.ToLower() == user.UserName && x.Password == user.Password)
                                join b in _context.Branch on u.BranchId equals b.Id
                                select new UserLoginModel
                                {
                                    User = u,
                                    Branch = b
                                })
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
        public string Login1(User user)
        {

            return "Json";


        }
    }
}