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
        private readonly PharmacyDbContext _context;
        private readonly IRepository<PharmacyDbContext> _pharmacyRepository;

        private readonly ILogger<UserController> _logger;

        public AllCodeController(ILogger<UserController> logger, IRepository repository, IQueryRepository queryRepository,PharmacyDbContext context,IRepository<PharmacyDbContext> pharmacyRepository)
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
                var allCodes=_context.AllCode.Where(x => x.Code == type);
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
        public async Task<List<ALLCODE>> LoadAllAllCode()
        {
            try
            {
                await _context.Database.OpenConnectionAsync(default);
                var allCode1s = await _queryRepository.GetCountAsync<ALLCODE>(default);
                var allCodes = _context.ALLCODE;
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