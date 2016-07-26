using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.helpers;

namespace WebApp.Controllers
{
    public class AcquisitionController : Controller
    {
        dbContext db;
        Helper _helpers;
        RequestsModule _requestsModule;

        [Authorize]
        public ActionResult Index()
        {

            ViewBag.Title = Constant.ACQUISITION;

            using (_requestsModule = new RequestsModule())
            {
                return View(_requestsModule.getAcquisition());
            }

        }

        [Authorize]
        [HttpGet]
        public ActionResult createEditAcquisition(int request_id = 0, bool isNew = false) {

            if (!isNew) {
                using (_requestsModule = new RequestsModule())
                {
                    var request = ((_requestsModule.getAcquisition(request_id) != null) ?
                        _requestsModule.getAcquisition(request_id).SingleOrDefault() : null);

                    return View(request);
                }
            }

            return View();

        }

        [Authorize]
        [HttpPost]
        public JsonResult updateAcquisition(tbl_requests acquisition, bool isNew = false)
        {

            bool isSuccess = true;
            bool emailSent = false;
            string message = string.Empty;

            using (_helpers = new Helper())
            using (db = new dbContext())
            {

                user_models userInfo = _helpers.UserData(_helpers.DecryptString(Request.Cookies["userInfo"].Value));

                acquisition.requestor_id    = Convert.ToInt32(userInfo.users.user_id);
                acquisition.requestor_name  = userInfo.users.name;
                acquisition.request_type    = "VEHICLE REQUISITION";

                var j = acquisition.justifications;
                var a = acquisition.additional_notes;

                acquisition.justifications  = (j != null) ? j.Replace("<p><br></p>", "") : null;
                acquisition.additional_notes = (a != null) ? a.Replace("<p><br></p>", "") : null;

                if (acquisition.id > 0 || !isNew){
                    
                    acquisition.modified_date = DateTime.Now;
                    db.Entry(acquisition).State = EntityState.Modified;

                }else {
                    
                    acquisition.request_status = "Pending";
                    acquisition.request_date   = DateTime.Now;
                    db.requests.Add(acquisition);

                }

                if (db.SaveChanges() <= 0)
                {
                    isSuccess = false;
                    message = Constant.UNABLE_TO_SAVE;
                }
                else {

                    if (isNew)
                    {
                        _helpers.logEmail(Request.Cookies["userInfo"].Value, 
                            this.emailHtml(acquisition.id, acquisition.request_status), acquisition.id.ToString(), "tbl_request", "Vehicle Requesition Form"); // log email

                        emailSent = true;
                    }

                    message = Constant.SUCCESS_SAVE;

                }

                return Json(new { isSuccess = isSuccess, message = message, emailSent = emailSent }, "text/html");

            }

        }

        [Authorize]
        public string emailHtml(int request_id = 0, string status = null)
        {
            string Url = "http://" + Request.Url.Authority.ToString() + "/Acquisition/createEditAcquisition?isNew=false&request_id=" + request_id;

            string html = "<div style='background:#f4f4f4;margin:40px 0;padding:40px 0' bgcolor='#f4f4f4'><div class='adM'></div><table width='550' border='0' cellspacing='0' cellpadding='0' style='background:#fff;border:1px solid #ccc;border-radius:5px' bgcolor='#FFFFFF' align='center'><tbody><tr><td style='padding:20px;font-family:Arial,Helvetica,sans-serif;font-size:12px'><span class='im'><h3 style='font-family:Arial,Helvetica,sans-serif;font-size:19px;font-weight:normal;margin-bottom:20px;margin-top:0;color:#333333'><font face='Arial, Helvetica, sans-serif' color='#333333'>Vechile Requisition Request</font></h3><p>A request has been received to reset the password for the following account:</p></span><p><span class='im'><strong>Username</strong>: jennocabrito<br></span></p><p>To continue, please visit the following link:</p><p><a href='" + Url + "' target='_blank'>" + Url +"</a></p><span class='im'><p>The request is valid only for 24 hours.</p><p>If you did not make this request, simply ignore this email.</p></span><p></p></td></tr><tr><td style='padding:20px;border-top:1px dotted #ccc'><a href='" + Request.Url.Host + "' target='_blank' data-saferedirecturl=''><img src='' alt='' style='display:block;margin:0;border:none' class='CToWUd'></a></td></tr></tbody></table><div class='yj6qo'></div><div class='adL'></div></div>";

            return html;
        }

    }

}
