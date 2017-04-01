using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Periodicals.BLL.Services;
using Periodicals.DAL.Entities;
using Periodicals.DAL.Identity;
using Periodicals.ViewModels;
using Periodicals.DAL.Context;
using Periodicals.DAL.Repository.Abstract;
using NLog;

namespace Periodicals.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IRepositoryFactory _factory;
        public AccountController(IRepositoryFactory factory)
        {
            _factory = factory;
            
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IRepositoryFactory factory):
               this(factory)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
       
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                
                //var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                var result = SignInManager.PasswordSignIn(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        {
                            logger.Info("Пользователь {0} вошел в систему", model.Email); 
                            return RedirectToLocal(returnUrl);       
                        }
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Неудачная попытка входа.");
                        return View(model);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Account", "Login"));  
            }

        }


        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        MobilePhone = model.MobilePhone,
                        City = model.City,
                        Index = model.Index
                    };

                    var result = await UserManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await UserManager.AddToRoleAsync(user.Id, "user");
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        PersonalAccount pa = new PersonalAccount();
                        pa.Balance = 0;
                        pa.ApplicationUserId = user.Id;
                        _factory.GetUserRepository(UserManager).AddAccount(pa);
                        UserService.LockUser(UserManager, user.Id, false);
                        logger.Info("Пользователь {0} зарегистрировался в системе", model.Email);
                        //TempData["SuccessMessage"] = "Пользователь успшено зарегистрирован в системе.";
                        return RedirectToAction("Index", "Home");
                    }
                    AddErrors(result);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Account", "Register"));
            }
        }

       
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            try
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Account", "LogOff"));
            }
        }

      

        public ActionResult GetImage(string id)
        {
            try
            {
                var res = UserService.GetImage(_factory, UserManager, id);
                return File(res.Item1, res.Item2);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Account", "GetImage"));
            }

        }
        [Authorize(Roles = "Admin")]
        public ActionResult Block(string id)
        {
            try
            {
                UserService.LockUser(UserManager, id, true);
                var user = UserManager.FindById(id);
                logger.Info("Пользователь {0} заблокирован", user.Email);
                TempData["BlockMessage"] = "Пользователь заблокирован.";
                return RedirectToAction("AllUsers");
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Account", "Block"));
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UnBlock(string id)
        {
            try
            {
                UserService.LockUser(UserManager, id, false);
                var user = UserManager.FindById(id);
                logger.Info("Пользователь {0} разблокирован", user.Email);
                TempData["SuccessMessage"] = "Пользователь разблокирован.";
                return RedirectToAction("AllBlockedUsers");
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Account", "UnBlock"));
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AllUsers()
        {
            try
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                return View(UserService.GetUnBlockedUsers(userManager, _factory));
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Account", "AllUsers"));
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AllBlockedUsers()
        {
            try
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                return View(UserService.GetBlockedUsers(userManager));
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Account", "AllBlockedUsers"));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}