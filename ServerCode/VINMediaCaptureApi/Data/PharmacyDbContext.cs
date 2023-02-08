using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using VINMediaCaptureEntities.Entities;

namespace AspNetCoreApp.Data;

public class PharmacyDbContext : DbContext
{
    public IConfiguration Configuration { get; }
    public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options, IConfiguration configuration) : base(options)
    {
        Configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder == null)
        {
            throw new ArgumentNullException(nameof(modelBuilder));
        }
        
        var dbType = Configuration.GetSection("ConfigureDB")["DbType"];

        if (dbType == "2")
        {
            modelBuilder.HasDefaultSchema("SYS");
        }
            
        base.OnModelCreating(modelBuilder);
        //modelBuilder.Entity<AllCode>().HasKey(e => e.Id);
    }

    public DbSet<AllCode> AllCode { get; set; }
    public DbSet<ALLCODE> ALLCODE { get; set; }
    public DbSet<Branch> Branch { get; set; }
    public DbSet<Drugs> Drugs { get; set; }
    public DbSet<Transaction> Transaction { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<TransactionDetail> TransactionDetail { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Warehouse> Warehouse { get; set; }
}
