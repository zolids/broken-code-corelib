using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.helpers;
using Newtonsoft.Json;
using System.Data;

namespace WebApp.Controllers
{

    public class DealersController : Controller
    {

        DealerModule _dealerModule;
        dbContext _db;

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
            using (_dealerModule = new DealerModule())
            {
                _dealers.dealers = _dealerModule.getDealers();

                return PartialView("dealersTable", _dealers);   
            }
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

        [Authorize]
        public PartialViewResult addEditAttachment(int attachmentId = 0)
        {
            return PartialView("attachment");
        }

        [Authorize]
        public JsonResult uploadFileAttachement(string dealerRefCode = null, string Description = null)
        {
            bool isSuccess = true;
            string message = string.Empty;
            string FileName = string.Empty;

            string TargetLocation = Server.MapPath("~/Content/files/");

            Dealer_attachment attachment = new Dealer_attachment();

            using(_db = new dbContext())
	        {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase myFile = Request.Files[i];

                    if (myFile.ContentLength > 0 && !string.IsNullOrEmpty(myFile.FileName))
                    {
                        FileName = dealerRefCode + "-" + myFile.FileName;

                        int FileSize = myFile.ContentLength;
                        byte[] FileByteArray = new byte[FileSize];

                        myFile.InputStream.Read(FileByteArray, 0, FileSize);
                        myFile.SaveAs(TargetLocation + FileName);

                        attachment.DealerRefCode = dealerRefCode;
                        attachment.FileName = FileName;
                        attachment.FileSize = FileSize;
                        attachment.DateUpload = DateTime.Now;
                        attachment.Description = Description;
                        attachment.file_source_location = TargetLocation;

                        _db.dealer_attachment.Add(attachment);
                        _db.SaveChanges();
                    }
                }		 
	        }

            return Json(new { isSuccess = isSuccess, message = message }, "text/html");
        }

        // this will return the 6 sequencial digit of an equipment
        public string generateDealerCode()
        {

            string code = string.Empty, prefixCode = string.Empty;
            string dealerCount = string.Empty;

            int codeLength = 6;

            _dealerModule = new DealerModule();

            var dealers = _dealerModule.getDealers();
            var count = ((dealers != null) ? ((dealers.Count() <= 0) ? "1" : (dealers.Count() + 1).ToString()) : "1");

            while (dealerCount.Length < codeLength)
            {
                prefixCode += "0";
                dealerCount = prefixCode + count;
            }

            return dealerCount;
        }

        public void removeFiles(int dealerID)
        {
            using (_db = new dbContext())
            {
                var files = from f in _db.dealer_attachment
                            where f.DealerAttID == dealerID
                            select f;

                if (files.Count() > 0)
                {
                    foreach (var file in files)
                    {
                        _db.dealer_attachment.Remove(file);
                    }

                    _db.SaveChanges();
                }
            }
        }

        public void DeleteFileFromFolder(string filePath)
        {

            dynamic files = JsonConvert.DeserializeObject<dynamic>(filePath);

            foreach (var file in files)
            {
                var Path = file.filePath.ToString();
                string[] fileDetails = Path.Split('`');

                if (System.IO.File.Exists(fileDetails[2].ToString()))
                {
                    System.IO.File.Delete(fileDetails[2]);
                    this.removeFiles(Int32.Parse(fileDetails[0]));
                }
            }

        }


        [Authorize]
        [HttpPost]
        public JsonResult saveDealerInformation(Dealers dealer)
        {

            bool isSuccess = true;
            string message = string.Empty;

            // get user define fields
            //this.mappeduserDefinedField(dealer);

            // delete uploaded files
            if (!string.IsNullOrEmpty(dealer.deleted_files) && dealer.deleted_files != "[]")
                this.DeleteFileFromFolder(dealer.deleted_files);

            using (_db = new dbContext())
            {
                if (dealer.id > 0)

                    _db.Entry(dealer).State = EntityState.Modified;

                else
                {
                    dealer.DealerRefCode = this.generateDealerCode();
                    _db.Dealers.Add(dealer);
                }

                if (_db.SaveChanges() <= 0)
                {
                    isSuccess = false;
                    message = "Error in updating Dealers information!";
                }
            }

            return Json(new { isSuccess = isSuccess, message = message }, "text/html");

        }

        [Authorize]
        public ActionResult downloadFile(string path)
        {

            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string files = System.IO.Path.GetFileName(path);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, files);

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
