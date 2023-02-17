using AspNetCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VINMediaCaptureApi.Common;
using VINMediaCaptureEntities.Entities;
using VINMediaCaptureEntities.Model;
using System.Data.Common;
using System.Data.Entity;
using System.Net.WebSockets;
using System.Threading;
using TanvirArjel.EFCore.GenericRepository;

namespace VINMediaCaptureApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[IdentityBasicAuthentication]
    //[CustomAuthentication]
    public class AllCodeController : ControllerBase
    {
        private readonly IRepository _repository;

        private readonly IQueryRepository _queryRepository;
        private readonly VINMediaCaptureDbContext _context;
        private readonly IRepository<VINMediaCaptureDbContext> _pharmacyRepository;

        private readonly ILogger<UserController> _logger;

        public AllCodeController(ILogger<UserController> logger, IRepository repository, IQueryRepository queryRepository,VINMediaCaptureDbContext context,IRepository<VINMediaCaptureDbContext> pharmacyRepository)
        {
            _logger = logger;
            _repository = repository;
            _context = context;
            _queryRepository = queryRepository;
            _pharmacyRepository = pharmacyRepository;
        }

        [HttpGet]
        [Route("LoadAllCodeByType")]
        public async Task<List<AllCode>> LoadAllCodeByType(string type)
        {
            try
            {
                await _context.Database.OpenConnectionAsync(default);
                var allCodes=_context.AllCode.Where(x => x.CodeName == type);
                if(allCodes!=null && allCodes.Any())
                    return allCodes.ToList();
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
        [HttpGet]
        [Route("LoadAllAllCode")]
        public async Task<List<AllCode>> LoadAllAllCode()
        {
            try
            {
                var test = new List<AllCode>();
                test.Add(new AllCode { CodeIDX = 1, CodeName = "Hieu" });
                return test;
                await _context.Database.OpenConnectionAsync(default);
                var allCode1s = await _queryRepository.GetCountAsync<AllCode>(default);
                var allCodes = _context.AllCode;
                if (allCodes != null && allCodes.Any())
                    return allCodes.ToList();
                var a = 1;
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
    }
}