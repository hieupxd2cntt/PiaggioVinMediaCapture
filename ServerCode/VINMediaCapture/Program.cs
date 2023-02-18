using VINMediaCapture.Service;
using VINMediaCapture.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddSession(cfg => {            // Đăng ký dịch vụ Session
    cfg.Cookie.Name = "VINMediaCapture";     // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
    cfg.IdleTimeout = new TimeSpan(0, 60, 0);   // Thời gian tồn tại của Session
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IColorService, ColorService>();
builder.Services.AddSingleton<IModelService, ModelService>();
builder.Services.AddSingleton<IMarketService, MarketService>();
builder.Services.AddSingleton<IAllCodeService, AllCodeService>();
builder.Services.AddSingleton<IDocTypeItemsService, DocTypeItemService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
