
using baskidabaski.EmailServices;
using baskidabaski.Models;
using Business.Container;
using Data.Concrete;
using Entity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TraversalCoreProje.Services.LanguageService;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;


var Host = builder.Configuration.GetValue<string>("EmailSender:Host");
var username= builder.Configuration.GetValue<string>("EmailSender:UserName");
var password = builder.Configuration.GetValue<string>("EmailSender:Password");
int port= builder.Configuration.GetValue<int>("EmailSender:Port");

bool EnableSSL = builder.Configuration.GetValue<bool>("EmailSender:EnableSSL");

builder.Services.AddRazorPages();

builder.Services.AddSingleton<LanguageService>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>().AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider).AddEntityFrameworkStores<Context>();

builder.Services.AddDbContext<Context>();
builder.Services.AddLogging(log =>
{
	log.ClearProviders();
	log.AddConsole();
});


builder.Services.AddLogging(x =>
{
	x.ClearProviders();
	x.SetMinimumLevel(LogLevel.Debug);
	x.AddDebug();
});


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;






});







builder.Services.AddControllersWithViews();

            builder.Services.ConfigureApplicationCookie(options => { options.LoginPath = "/Account/Login"; });
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//		.AddCookie(options =>
//		{
//			options.LoginPath = "/account/login";
//			options.LogoutPath = "/account/logout";
//		});



//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = "/account/login";
//    options.LogoutPath = "/account/logout";
//    options.AccessDeniedPath = "/account/accessdenied";
//    options.SlidingExpiration = true;
//    options.Cookie = new CookieBuilder { 
//        HttpOnly = true,
//        Name="baskidabaski.Security.Cookie"
//    };
//});
builder.Services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();


builder.Services.ContainerDependencies();
builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(i=>new SmtpEmailSender(username,password,Host,port,EnableSSL));
var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var supputedCultures = new[] { "en", "fr", "tr" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supputedCultures[2]).AddSupportedCultures(supputedCultures).AddSupportedUICultures(supputedCultures);
app.UseRequestLocalization(localizationOptions);
app.UseAuthentication();

app.UseRouting();
app.UseAuthorization();
app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404","?code={0}");
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	  name: "areas",
	  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
	);
});
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	  name: "areas",
	  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
	);
});

app.UseHttpsRedirection();

app.Run();


app.MapRazorPages();



