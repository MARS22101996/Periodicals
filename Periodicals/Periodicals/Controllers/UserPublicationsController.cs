using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Periodicals.BLL.Services;
using Periodicals.DAL.Context;
using Periodicals.DAL.Entities;
using Periodicals.DAL.Repository.Abstract;
using Periodicals.ViewModels;
using NLog;

namespace Periodicals.Controllers
{
    public class UserPublicationsController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IRepositoryFactory _factory;

        public UserPublicationsController(IRepositoryFactory factory)
        {
            _factory = factory;
        }
    
    [HttpPost]
    public ActionResult UserPublicationsSearch(int name, string month, string year)
    {
        try
        {
                int m = int.Parse(month);
                int y = int.Parse(year);
                int day = 1;
                switch (m)
                {
                    case 1:
                        day = 31;
                        break;
                    case 2:
                        {
                            if (y == 2016 || y == 2012) day = 28;
                            else day = 28;
                            break;
                        }
                    case 3:
                        day = 31;
                        break;
                    case 4:
                        day = 30;
                        break;
                    case 5:
                        day = 31;
                        break;
                    case 6:
                        day = 30;
                        break;
                    case 7:
                        day = 31;
                        break;
                    case 8:
                        day = 31;
                        break;
                    case 9:
                        day = 30;
                        break;
                    case 10:
                        day = 31;
                        break;
                    case 11:
                        day = 30;
                        break;
                    case 12:
                        day = 31;
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
                DateTime date1 = new DateTime(y, m, day, 0, 0, 0);
                var userPublications = UserPublicationService.UserPublicationsWithName(_factory,name, date1);               
                ViewBag.SumPayed = UserPublicationService.GetTotalPaidSumForPublication(_factory, name, date1);
                if (userPublications.Count <= 0)
            {
                return PartialView("SearchUserPError");
            }

            return PartialView("AfterSearch", userPublications);
        }
        catch (Exception ex)
        {
            logger.Error(ex, ex.Message);
            return View("Error", new HandleErrorInfo(ex, "Publications", "PublicationSearch"));
        }
    }
        // GET: UserPublications
        [Authorize(Roles = "Admin, Support")]
        [HttpGet]
        public ActionResult IndexByName()
        {
            try
            {
                DateTime date1 = new DateTime(2010, 1, 31, 0, 0, 0);
                var defaultvalue = _factory.PublicationRepository.Get().FirstOrDefault().PublicationId;
                var userPublications = UserPublicationService.UserPublicationsWithName(_factory, defaultvalue, date1);
                ViewBag.SumPayed = UserPublicationService.GetTotalPaidSumForPublication(_factory, defaultvalue, date1);
                ViewBag.AllPublications = _factory.PublicationRepository.Get().OrderBy(o=>o.NameOfPublication);
                return View(userPublications);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "UserPublications", "Index1"));
            }
        }
 
        
        // GET: UserPublications
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var idUser = User.Identity.GetUserId();
                var userPublicationsConcrete =
                   UserPublicationService.GetUserPublicationsForConcreteUser(_factory, idUser);
                ViewBag.Sum = UserPublicationService.GetUnpaidSumForUser(_factory, idUser);

                var account = UserService.GetAccountOfUser(_factory, idUser);
                ViewBag.AccountSum = account.Balance;

                return View(userPublicationsConcrete);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "UserPublications", "Index"));
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult PublicationsOfUser(string userId)
        {
            try
            {
                var idUser = userId;
                var userPublicationsConcrete =
                   UserPublicationService.GetPaidPublicationsForUser(_factory, idUser);
                //ViewBag.SumNotPayed = UserPublicationService.GetUnpaidSumForUser(_factory, idUser);
                ViewBag.SumPayed = UserPublicationService.GetPaidSumForUser(_factory, idUser);
                return View("Index1", userPublicationsConcrete);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "UserPublications", "PublicationsOfUser"));
            }

        }

       
        // GET: UserPublications/Create
        [HttpPost]
        public ActionResult CreateSubscription(string name, int id)
        {
            try
            {
                UserPublicationService.CreateSubscription(_factory, name, id, User.Identity.GetUserId(), logger);
             
                return PartialView("Subscription");
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "UserPublications", "CreateSubscription"));
            }
        }

        // GET: UserPublications/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var userPublication = _factory.UserPublicationRepository.FindById(id);
                if (userPublication == null)
                {
                    return View("ResourceNotFound");
                }
                var userPublicationViewModelObj = new UserPublicationViewModel
                {
                    UserPublicationId = userPublication.UserPublicationId,
                    PublicationId = userPublication.PublicationId,
                    UserId = userPublication.UserId,
                    StartDate = userPublication.StartDate,
                    EndDate = userPublication.EndDate,
                    Period = userPublication.Period,
                    PaymentState = userPublication.PaymentState
                };

                return View(userPublicationViewModelObj);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "UserPublications", "Edit"));
            }
        }

        // POST: UserPublications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserPublicationViewModel userPublicationViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var userPublicationObj = new UserPublication
                    {
                        UserPublicationId = userPublicationViewModel.UserPublicationId,
                        PublicationId = userPublicationViewModel.PublicationId,
                        Period = userPublicationViewModel.Period,
                        PaymentState = userPublicationViewModel.PaymentState,
                        UserId = User.Identity.GetUserId(),
                        StartDate = userPublicationViewModel.StartDate,

                    };
                    DateTime date = userPublicationObj.StartDate.AddMonths(userPublicationObj.Period).Date;
                    userPublicationObj.EndDate = date;
                    _factory.UserPublicationRepository.Edit(userPublicationObj);
                    TempData["SuccessMessage"] = "Вы изменили период подписки.";
                    
                    return RedirectToAction("Index");
                }
                return View(userPublicationViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "UserPublications", "Edit"));
            }
        }

        // GET: UserPublications/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var userPublication = _factory.UserPublicationRepository.FindById(id);
                if (userPublication == null)
                {
                    return View("ResourceNotFound");
                }
                return View(userPublication);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "UserPublications", "Delete"));
            }
        }

        // POST: UserPublications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var userPublication = _factory.UserPublicationRepository.FindById(id);
                var user = userPublication.User.Email;
                var name = userPublication.Publication.NameOfPublication;
                _factory.UserPublicationRepository.Delete(id);
                logger.Info("Удалена подписка пользователя {0} на издание {1} ", user, name);
                TempData["SuccessMessage"] = "Подписка удалена.";
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("AllUsers","Account");
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "UserPublications", "DeleteConfirmed"));
            }
        }

        [HttpGet]
        public ActionResult Delete1(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var userPublication = _factory.UserPublicationRepository.FindById(id);
                if (userPublication == null)
                {
                    return View("ResourceNotFound");
                }
                return View(userPublication);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "UserPublications", "Delete"));
            }
        }

        // POST: UserPublications/Delete/5
        [HttpPost, ActionName("Delete1")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed1(int id)
        {
            try
            {
                var userPublication = _factory.UserPublicationRepository.FindById(id);
                var user = userPublication.User.Email;
                var name = userPublication.Publication.NameOfPublication;
                _factory.UserPublicationRepository.Delete(id);
                logger.Info("Удалена подписка пользователя {0} на издание {1} ", user, name);
                TempData["SuccessMessage"] = "Подписка удалена.";              
                return RedirectToAction("IndexByName");
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "UserPublications", "DeleteConfirmed"));
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _factory.UserPublicationRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
