using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.helpers;
using WebApp.Models;

namespace WebApp.Controllers
{

    public struct vehicleTypeAddEditDetails
    {

        public IEnumerable<fleet_vehicle_type> fleetTypes { get; set; }

        public IEnumerable<fleet_vehicle_models> fleetModels { get; set; }

        public IEnumerable<fleet_vehicle_make> fleetVechileMakes { get; set; }

        public IEnumerable<fleet_vehicle_type_mapping> fleetVechileMappings { get; set; }

        public IEnumerable<fleet_vehicle_series> fleetVehicleSeries { get; set; }

        public IEnumerable<vehicle_type_make> fleetVehicleTypeMake { get; set; }

    }

    public class PicklistController : Controller
    {

        dbContext _db;
        Helper _helpers;
        PicklistModule _picklist;

        vehicleTypeAddEditDetails _vehicleDetails;

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

        [Authorize]
        public PartialViewResult getVehicleTypeView()
        {
            using (_picklist = new PicklistModule())
            {
                var types = _picklist.getFleetVehicleTypes();

                return PartialView("VehicleTypeView", types);
            }

        }

        public PartialViewResult getVehicleTypeViewEditForm(int VehTypeID = 0, string actions = null)
        {
            
            _vehicleDetails = new vehicleTypeAddEditDetails();

            using (_picklist = new PicklistModule())
            {
                _vehicleDetails.fleetModels = _picklist.getFleetVehicleModels();

                if (VehTypeID != 0)
                    _vehicleDetails.fleetTypes  = _picklist.getFleetVehicleTypes(VehTypeID);

                if (_vehicleDetails.fleetTypes != null && _vehicleDetails.fleetTypes.Count() > 0)
                {
                    _vehicleDetails.fleetVechileMappings = _picklist.getMappedVehicleTypes(VehTypeID);
                }

                return PartialView("AddEditVehicleType", _vehicleDetails);
            }

        }

        [Authorize]
        [HttpPost]
        public JsonResult addEditProvinces(fleet_provinces province)
        {
            bool isSuccess = true;
            string message = "";

            try
            {
                using (_db = new dbContext())
                {
                    province.name = province.name.ToUpper();

                    if (province.id > 0) 
                        _db.Entry(province).State = EntityState.Modified;
                    else
                        _db.province.Add(province);

                    if (_db.SaveChanges() <= 0)
                    {
                        isSuccess = false;
                        message   = "Error in updating Fleet Provinces information!";
                    }                   
                }

            }
            catch (Exception e)
            {
                using (_helpers = new Helper())
                {
                    isSuccess = false;
                    message   = _helpers.LogException(e);
                }   
            }

            return Json(new { isSuccess = isSuccess, message = message }, "text/html");

        }

        [Authorize]
        public JsonResult deleteRecord(int integerId = 0, string module = null)
        {
            bool isSuccess = true;
            string message = "";

            using (_picklist = new PicklistModule()) 
            {
                try
                {
                    isSuccess = _picklist.deletePicklist(module, integerId);
                }
                catch (Exception e)
                {
                    isSuccess = false;
                    message = (e.InnerException == null) ? e.Message.ToString() : e.InnerException.ToString();
                    message = "Unable to delete record: " + message;
                }

                return Json(new { isSuccess = isSuccess, message = message }, "text/html");                
            }

        }
    
    }

}
