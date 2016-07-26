using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.helpers;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {

        private Helper _helpers;

        [Authorize]
        public ActionResult Index(string active_project)
        {

            Response.Cookies["activeProject"].Value = active_project;

            ViewBag.Title = Constant.DASHBOARD;
            ViewBag.active_project = active_project;

            ViewBag.isActive = "true";

            // if user or porject not selected stay @ default page
            if (string.IsNullOrEmpty(active_project))
                return RedirectToAction("DefaultViewport", "Home");


            return View();

        }

        [Authorize]
        public ActionResult DefaultViewport()
        {

            ViewBag.Title = Constant.DEFAULT_PAGE;

            using (_helpers = new Helper())
            {
                var modules = new string[0];

                Response.Cookies["activeProject"].Expires = DateTime.Now.AddDays(-1);

                string session = _helpers.DecryptString(Request.Cookies["userInfo"].Value);
                modules = _helpers.UserData(session).module;

                ViewBag.Modules = modules;

            }
            

            return View();
        }

        [Authorize]
        public ActionResult ErrorPage()
        {
            return View();
        }

    }
}
