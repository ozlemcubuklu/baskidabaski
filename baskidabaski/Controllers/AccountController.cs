
using baskidabaski.EmailServices;
using baskidabaski.Extensitons;
using baskidabaski.Identity;
using baskidabaski.Models;
using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace baskidabaski.Controllers
{
    public class AccountController : Controller
    {
        private  IEmailSender _emailSender;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        // private IEmailSender _emailSender;
        
        public AccountController(IEmailSender emailSender, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,ILogger<AccountController> logger)
        {
            _emailSender = emailSender;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {

			return View(new LoginModel() { ReturnUrl = returnUrl });


		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Kullanıcıyı e-posta adresine göre bulun
            var user = await _userManager.FindByEmailAsync(model.EMail);

            if (user == null)
            {
                ModelState.AddModelError("", "Bu e-posta ile daha önce kayıt yapılmamıştır.");
                return View(model);
            }

            // E-posta doğrulaması yapılmış mı kontrol edin
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Lütfen mail hesabınıza gelen linki onaylayın.");
                return View(model);
            }

            // Kullanıcıyı parola ile giriş yapmaya çalışın
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                DateTime d = Convert.ToDateTime(DateTime.Now.ToLongDateString());
                // Giriş başarılı ise, kullanıcıyı istediğiniz sayfaya yönlendirin
                _logger.LogInformation(DateTime.Now.ToLongDateString()+" oturum açıldı");
				return Redirect(model.ReturnUrl ?? "~/");
			}




            ModelState.AddModelError("", "Girilen e-posta veya parola yanlış.");
            return View(model);


		}
        private IActionResult RedirectToLocal(string returnUrl)
        {

            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel register)
        {
            //sadık turan
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            var user = new AppUser()
            {
                Name = register.FirstName,
                Surname = register.LastName,
                Email = register.Email,
                UserName = register.UserName,
                Gender = "-"
            };

            var result = await _userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = code });


   var gonder= _emailSender.SendEmailAsync(register.Email,"Hesabınızı Onaylayınız.",$"Lütfen email hesabınızı onaylamak için <a href='https://localhost:44328{url}'>linke tıklayınız.</a>");
                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("", "Bilinmeyen bir hata oldu. Lütfen tekrar deneyiniz.");


            return View(register);
        }


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
				TempData.Put("message", new AlertMessage() { AlertType = "danger", Title = "Geçersiz Token", Message = "Geçersiz Token" });

				return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
				TempData.Put("message", new AlertMessage() { AlertType = "danger", Title = "Böyle bir kullanıcı yok", Message = "Böyle bir kullanıcı yok" });

				return View();
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
				TempData.Put("message", new AlertMessage() { AlertType = "success", Title = "Hesap Onayı", Message = "Hesabınız Onaylandı" });

				return RedirectToAction("Login","Account");
            }
            return View();

        }


        [HttpGet]
        public IActionResult ConfirmMail()
        {
            var deger = TempData["Mail"];
            ViewBag.v = deger;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmMail(ConfirmMailViewModel confirmMailViewModel)
        {

            var user = await _userManager.FindByEmailAsync(confirmMailViewModel.email);
            if (user != null)
            {
                if (user.ConfirmCode == confirmMailViewModel.ConfirmCode)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
					TempData.Put("message", new AlertMessage() { AlertType = "success", Title = "Hesap Onayı", Message = "oturum Hesabınız Onaylandı." });

					return RedirectToAction("Login");
                }
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
			TempData.Put("message", new AlertMessage() { AlertType = "success", Title = "Oturum Sonlandırma", Message = "oturum kapandı." });

			await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
          

        }
        private void CreateMessage(string message, string alerttype)
        {
            var msg = new AlertMessage() { Message = message, AlertType = alerttype };
            TempData["message"] = JsonConvert.SerializeObject(msg);
        }

        public IActionResult ForgotPassword() {


            return View();
        }
        [HttpPost]

		public async Task<IActionResult> ForgotPassword(string email)
		{

            if (string.IsNullOrEmpty(email))
            {
                return View();
            }
            var user=await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return View();
            }
            var code=await _userManager.GeneratePasswordResetTokenAsync(user);
			var url = Url.Action("ResetPassword", "Account", new { userId = user.Id, token = code });


			var result=_emailSender.SendEmailAsync(email, "Parola Sıfırlama", $"Parolanızı sıfırlamak için <a href='https://localhost:44328{url}'>linke tıklayınız.</a>");
            if (result)
            {
            return RedirectToAction("Index","Home");
            }
            return View();
			
		}


        [HttpGet]
        public IActionResult ResetPassword(string userId,string token)
        {
            if (userId==null || token==null)
            {
                return RedirectToAction("Index","Home");
            }
            var model=new ResetPasswordModel() { Token = token };
            return View(model);
        }
        [HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
		{
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
				return RedirectToAction("Index", "Home");
			}
            var result = await _userManager.ResetPasswordAsync(user,model.Token,model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login","Account");
            }
			return View(model);
		}
        public IActionResult accessdenied()
        {
            return View();
        }
	}
}

