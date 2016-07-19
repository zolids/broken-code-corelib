using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.helpers;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Title = Constant.DASHBOARD;

            // if role or porject not selected stay @ default page
            //return RedirectToAction("DefaultViewport", "Home");

            return View();
        }

        [Authorize]
        public ActionResult DefaultViewport()
        {
            ViewBag.Title = Constant.DEFAULT_PAGE;

            return View();
        }

    }
}
