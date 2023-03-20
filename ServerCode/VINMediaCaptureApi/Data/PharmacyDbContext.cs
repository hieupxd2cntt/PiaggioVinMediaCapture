using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using VINMediaCaptureEntities.Entities;

namespace AspNetCoreApp.Data;

public class VINMediaCaptureDbContext : DbContext
{
    public IConfiguration Configuration { get; }
    public VINMediaCaptureDbContext(DbContextOptions<VINMediaCaptureDbContext> options, IConfiguration configuration) : base(options)
    {
        Configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder == null)
        {
            throw new ArgumentNullException(nameof(modelBuilder));

            base.OnModelCreating(modelBuilder);
        }
    }

    public DbSet<AllCode> AllCode { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<Color> Color { get; set; }
    public DbSet<Model> Model { get; set; }
    public DbSet<Market> Market { get; set; }
    public DbSet<DocTypeItems> DocTypeItems { get; set; }
    public DbSet<DocType> DocType { get; set; }
    public DbSet<DocTypeItemAttr> DocTypeItemAttr  { get; set; }
    public DbSet<DocTypeGuide> DocTypeGuide { get; set; }
    public DbSet<ProductDoc> ProductDoc { get; set; }
    public DbSet<ProductDocVal> ProductDocVal { get; set; }
}
