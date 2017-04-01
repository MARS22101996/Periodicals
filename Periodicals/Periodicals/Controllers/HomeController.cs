using NLog;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Periodicals.Controllers
{
    public class HomeController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();

      
        public ActionResult Index()
        {
            try
            { 
          
                return View();
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                logger.Info("Feedback successfully sent by");
                return View("Error", new HandleErrorInfo(ex, "Home", "Index"));
              
            }
         
        }
      

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}