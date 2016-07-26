using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.helpers;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PicklistController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Title = Constant.PICKLIST;

            return View();
        }

    }
}
