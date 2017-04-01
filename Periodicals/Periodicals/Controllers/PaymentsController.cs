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
    public class PaymentsController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IRepositoryFactory _factory;

        public PaymentsController(IRepositoryFactory factory)
        {
            _factory = factory;
        }
        // GET: Payments/Create
        [HttpGet]
        public ActionResult Create()
        {
            var paymentViewModel = new PaymentViewModel();
            paymentViewModel.Sum = PaymentService.CalculateUnpaidAmountForTheUser(_factory, User.Identity.GetUserId());
            return View(paymentViewModel);
        }

        // POST: Payments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaymentViewModel paymentViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var sum = PaymentService.CalculateUnpaidAmountForTheUser(_factory, User.Identity.GetUserId());
                    paymentViewModel.Sum = sum;
                    var account = UserService.GetAccountOfUser(_factory,User.Identity.GetUserId());
                    var paymentObj = new Payment
                    {
                       
                    };
                    if (account.Balance >= sum)
                    {
                        PaymentService.ExecutionOfPayment(_factory, User.Identity.GetUserId(), paymentObj, account, sum, logger);
                        TempData["SuccessMessage"] = "Платеж осуществлен.";
                        return RedirectToAction("Index", "UserPublications");
                    }
                    else
                    {
                        return PartialView("Error");
                    }
                }

                return View(paymentViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Payments", "Create"));
            }
        }

    }
}
