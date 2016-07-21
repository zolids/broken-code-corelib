using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.helpers;

namespace WebApp.Controllers
{
    public class AcquisitionController : Controller
    {
        //
        // GET: /Acquisition/

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Title = Constant.ACQUISITION;

            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult createEditAcquisition(bool isNew = false) {

            return View();

        }

        [Authorize]
        [HttpPost]
        public JsonResult updateAcquisition(tbl_requests acquisition, bool isNew = false)
        {

            bool isSuccess = true;
            string message = string.Empty;



            return Json(new { isSuccess = isSuccess, message = message }, "text/html");

        }


    }

}
