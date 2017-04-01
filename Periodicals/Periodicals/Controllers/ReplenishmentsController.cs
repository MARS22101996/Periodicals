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
    public class ReplenishmentsController : Controller
    {
        private readonly IRepositoryFactory _factory;

        Logger logger = LogManager.GetCurrentClassLogger();

        public ReplenishmentsController(IRepositoryFactory factory)
        {
            _factory = factory;
        }


        // GET: Replenishments/Create
        [HttpGet]
        public ActionResult Create()
        {
            ReplenishmentViewModel replenishmentViewModel = new ReplenishmentViewModel();
            return View(replenishmentViewModel);
        }

        // POST: Replenishments/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReplenishmentViewModel replenishmentViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var replenishmentObj = new Replenishment
                    {
                        Amount = replenishmentViewModel.Amount,
                        MobileNumber = replenishmentViewModel.MobileNumber,
                        NumberOfCard = replenishmentViewModel.NumberOfCard,
                        ExpirationDate = replenishmentViewModel.ExpirationDate,
                        Cvc = replenishmentViewModel.Cvc,
                        NameOfCard = replenishmentViewModel.NameOfCard
                    };
                    ReplenishmentService.ExecutionOfReplenishment(_factory, User.Identity.GetUserId(), replenishmentObj, logger);
                    TempData["SuccessMessage"] = "Счет успешно пополнен.";
                    return RedirectToAction("Index", "PersonalAccounts");
                }
                return View(replenishmentViewModel);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "Replenishments", "Create"));
            }
        }

    }
}
