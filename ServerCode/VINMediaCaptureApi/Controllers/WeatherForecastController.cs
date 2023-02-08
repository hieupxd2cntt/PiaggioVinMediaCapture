using AspNetCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyManagementApi.Common;
using PharmacyManagementEntities.Entities;
using System.Data.Common;
using System.Data.Entity;
using System.Net.WebSockets;
using System.Threading;
using TanvirArjel.EFCore.GenericRepository;

namespace PharmacyManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[IdentityBasicAuthentication]
    [CustomAuthentication]
    public class User1Controller : ControllerBase
    {
        private readonly IRepository _repository;

        private readonly IQueryRepository _queryRepository;
        private readonly PharmacyDbContext _context;
        private readonly IRepository<PharmacyDbContext> _pharmacyRepository;

      
        private readonly ILogger<User1Controller> _logger;

        public User1Controller(ILogger<User1Controller> logger, IRepository repository, IQueryRepository queryRepository,PharmacyDbContext context,IRepository<PharmacyDbContext> pharmacyRepository)
        {
            _logger = logger;
            _repository = repository;
            _context = context;
            _queryRepository = queryRepository;
            _pharmacyRepository = pharmacyRepository;
        }
    }
}