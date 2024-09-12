

using FluentValidation;
using FreeCodeCampAcademy;
using FreeCodeCampAcademy.APIService;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Session;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("FreeCodeCampAcadServSettings.json", optional: false, reloadOnChange: true);


builder.ConfigureServiceOptions();
var assembly = typeof(Program).Assembly;
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddXPCaching();
builder.Services.AddAPIServices();

builder.Services.AddControllersWithViews().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null).AddRazorOptions(opt =>
{
opt.ViewLocationExpanders.Add(new ViewLocationExpander());

//Area Locations
opt.AreaViewLocationFormats.Clear();
opt.AreaViewLocationFormats.Add("/Areas/{2}/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
    opt.AreaViewLocationFormats.Union(AppViewConfig.CustomSharedDirectories());
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
builder.Services.AddSingleton<IStartSession, InitSession>();
builder.Services.AddSingleton<ISessionStore, DistributedSessionStoreWithStart>();


//Add services to the container
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseRewriter();
app.UseSession();
app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Strict});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
