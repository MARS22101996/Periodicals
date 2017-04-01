using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Periodicals.DAL.Context;
using Periodicals.DAL.Entities;
using Periodicals.DAL.Repository.Abstract;
using NLog;
using Periodicals.BLL.Services;

namespace Periodicals.Controllers
{
    public class PersonalAccountsController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IRepositoryFactory _factory;

        public PersonalAccountsController(IRepositoryFactory factory)
        {
            _factory = factory;
        }
        [Authorize]
        public ActionResult Index()
        {
            try
            {
                return View(_factory.PersonalAccountRepository.GetUserAccount( User.Identity.GetUserId()));
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return View("Error", new HandleErrorInfo(ex, "PersonalAccounts", "Index"));

            }
        }

        
    }
}
