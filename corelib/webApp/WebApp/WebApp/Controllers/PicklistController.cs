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

        dbContext _db;
        Helper _helpers;
        PicklistModule _picklist;

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Title = Constant.PICKLIST;

            return View();
        }

        [Authorize]
        public PartialViewResult getAssignedLocation()
        {

            using (_picklist = new PicklistModule())
            {
                var province = _picklist.getFleetProvinces();

                return PartialView("ProvincesView", province);
            }

        }
    
    }

}
