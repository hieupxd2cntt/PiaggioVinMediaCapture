using TanvirArjel.EFCore.GenericRepository;
using VINMediaCaptureEntities.Entities;
using AspNetCoreApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Oracle.EntityFrameworkCore;
using System.Configuration.Provider;

namespace VINMediaCaptureApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllers();
            //services.AddMvc().AddXmlSerializerFormatters();
            //services.AddAuthentication("BasicAuth").AddScheme<AuthenticationSchemeOptions, CustomAuthentication>("BasicAuth", null);

            services.AddResponseCompression();

            services.AddMvc();

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();            
            var connectionString = Configuration.GetConnectionString("VINMediaCaptureConnection");
            services.AddDbContext<VINMediaCaptureDbContext>(option => option.UseSqlServer(connectionString));
            services.AddGenericRepository<VINMediaCaptureDbContext>(); // Call it just after registering your DbConext.
            services.AddQueryRepository<VINMediaCaptureDbContext>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            //app.UseMiddleware<BasicAuthen>();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseResponseCompression();

        }
    }
}
