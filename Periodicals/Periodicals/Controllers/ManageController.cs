using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Periodicals.DAL.Entities;
using Periodicals.DAL.Identity;
using Periodicals.DAL.Repository.Abstract;
using Periodicals.ViewModels;
using NLog;
using Periodicals.BLL.Services;

namespace Periodicals.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IRepositoryFactory _factory;

        public ManageController(IRepositoryFactory factory)
        {
            _factory = factory;
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IRepositoryFactory factory)
        : this(factory)
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
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    logger.Info("Пользователь {0} изменил пароль", user.Email);
                    TempData["SuccessMessage"] = "Изменен пароль.";
                    return RedirectToAction("Details");
                }
                AddErrors(result);
                return View(model);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Manage", "ChangePassword"));
            }
        }

        public ActionResult Details(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id) || !(User.IsInRole("Admin") || User.IsInRole("Support")))
                    id = User.Identity.GetUserId();
                var user = _factory.GetUserRepository(UserManager).FindById(id);
                if (user == null)
                    return View("ResourceNotFound");
                return View(user);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Manage", "Details"));
            }
        }
    
        public ActionResult Edit()
        {
            try
            {
                string userId = User.Identity.GetUserId();
                var user = _factory.GetUserRepository(UserManager).FindById(userId);
                var res = UserService.GetImage(_factory, UserManager, userId);

                var userViewModelObj = new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MobilePhone = user.MobilePhone,
                    City = user.City,
                    Index = user.Index,
                    Email = user.Email,
                    ImageBytes = res.Item1,
                    ImgMimeType = res.Item2
                };
                return View(userViewModelObj);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Manage", "Edit"));
            }
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel userViewModel, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userObj = new ApplicationUser
                    {
                        Id = userViewModel.Id,
                        FirstName = userViewModel.FirstName,
                        LastName = userViewModel.LastName,
                        MobilePhone = userViewModel.MobilePhone,
                        Email = userViewModel.Email,
                        City = userViewModel.City,
                        Index = userViewModel.Index,
                       
                    };
                    if (file != null && file.ContentType.StartsWith("image"))
                    {
                        userObj.ImgMimeType = file.ContentType;
                        userObj.ImageBytes = new byte[file.ContentLength];
                        file.InputStream.Read(userObj.ImageBytes, 0, file.ContentLength);
                    }
                    _factory.GetUserRepository(UserManager).Edit(userObj, User.Identity.GetUserId());
                    logger.Info("Пользователь {0} изменил информацию о себе", userObj.Email);
                    TempData["SuccessMessage"] = "Изменена информация о пользователе.";
                    return RedirectToAction("Details");
                }
                return View(userViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Manage", "Edit"));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
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

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}