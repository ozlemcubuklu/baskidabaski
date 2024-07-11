using baskidabaski.Models;
using Business.Abstract;
using Business.Concrete;
using Data.Concrete.EfCore;
using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using TraversalCoreProje.Services.LanguageService;

namespace baskidabaski.Controllers
{
    public class HomeController : Controller
    { 
        private readonly IProductService _productService;
        private readonly LanguageService _languageService;
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		public HomeController(IProductService productService, LanguageService languageService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {    _languageService = languageService;
            _productService = productService;
            _userManager = userManager;
            signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;

            var products =_productService.GethomePageProducts();
            return View(new ProductListViewModel() { Products=products});
        }
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
            { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
