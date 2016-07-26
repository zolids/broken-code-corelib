using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.helpers;
using Newtonsoft.Json;

namespace WebApp.Controllers
{

    public class DealersController : Controller
    {

        DealerModule _dealerModule;

        DealersInformation _dealers = new DealersInformation();

        [Authorize]
        public ActionResult Index()
        {
            using (_dealerModule = new DealerModule())
            {
                ViewBag.Title = Constant.DEALER_PAGE;

                _dealers.dealers = _dealerModule.getDealers();

                return View(_dealers);
            }            
        }

        [Authorize]
        public PartialViewResult dealersList()
        {
            _dealers.dealers = _dealerModule.getDealers();

            return PartialView("dealersTable", _dealers);
        }

        [Authorize]
        public PartialViewResult dealers(string dealerRefCode = null, bool isNew = false)
        {

            if (!isNew)
            {
                using (_dealerModule = new DealerModule())
                {
                    _dealers.dealers                = _dealerModule.getDealers(dealerRefCode);
                    _dealers.referencedFleet        = _dealerModule.getReferenceFleetData(dealerRefCode);
                    _dealers.getDealerAttachment    = _dealerModule.getDealerAttachment(dealerRefCode);
                }
            }

            return PartialView(_dealers);
        }

    }

    public struct DealersInformation
    {
        public IEnumerable<Dealers> dealers { get; set; }

        public IEnumerable<ReferenceFleetData> referencedFleet { get; set; }

        public IEnumerable<Dealer_attachment> getDealerAttachment { get; set; }

        public string dealerSuffixCode { get; set; }

    }

}
