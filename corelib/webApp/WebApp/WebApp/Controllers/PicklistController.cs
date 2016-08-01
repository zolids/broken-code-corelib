using Newtonsoft.Json;
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

        public PartialViewResult getVehicleTypeViewEditForm(int VehTypeID = 0)
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
        public PartialViewResult getVehicleTypeModelEditForm(int ModelID = 0)
        {

            _vehicleDetails = new vehicleTypeAddEditDetails();

            using (_picklist = new PicklistModule())
            {
                if (ModelID != 0)
                    _vehicleDetails.fleetModels = _picklist.getFleetVehicleModels(ModelID);

                _vehicleDetails.fleetVechileMakes = _picklist.getFleetVehicleMake();

                return PartialView("AddEditVehicleModel", _vehicleDetails);
            }

        }

        [Authorize]
        [HttpPost]
        public JsonResult updateVehicleModels(fleet_vehicle_models models)
        {
            bool isSuccess = true;
            string message = "";

            try
            {
                using (_db = new dbContext())
                {
                    if (models.ModelID > 0)
                        _db.Entry(models).State = EntityState.Modified;
                   
                    else
                        _db.fleetVehicleModel.Add(models);

                    if (_db.SaveChanges() <= 0)
                    {

                        isSuccess = false;
                        message = "Error in updating Vehicle information!";

                    }
                }

            }
            catch (Exception e)
            {
                using (_helpers = new Helper())
                {
                    isSuccess = false;
                    message = _helpers.LogException(e);
                }
            }

            return Json(new { isSuccess = isSuccess, message = message }, "text/html");

        }

        [Authorize]
        [HttpPost]
        public JsonResult updateVehicleType(fleet_vehicle_type type)
        {
            bool isSuccess = true;
            string message = "";

            try
            {
                using (_db = new dbContext())
                using (_picklist = new PicklistModule())
                using (_helpers = new Helper())
                {
                    if (type.VehTypeID > 0)

                        _db.Entry(type).State = EntityState.Modified;

                    else
                        _db.fleet_vehicle_types.Add(type);

                    if (_db.SaveChanges() <= 0)
                    {

                        isSuccess = false;
                        message = "Error in updating Vehicle information!";

                    }
                    else
                    {
                        _picklist.deletePicklist("fleet_vehicle_type_mapping", Convert.ToInt32(type.VehTypeID), "VehTypeID");

                        if (type.vehicle_type_selected != null)
                        {
                            if (type.vehicle_type_selected.Count() > 0)
                            {
                                fleet_vehicle_type_mapping mappings = new
                                    fleet_vehicle_type_mapping();

                                foreach (var item in type.vehicle_type_selected)
                                {
                                    string[] str;

                                    if (!_helpers.IsNumber(item))
                                    {
                                        str = item.Split('-');
                                        mappings.VTModelID = Int32.Parse(str[0]);
                                    }
                                    else
                                        mappings.VTModelID = Int32.Parse(item);

                                    mappings.VehTypeID = type.VehTypeID;

                                    _db.fleetVehicleMappings.Add(mappings);
                                    _db.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                using (_helpers = new Helper())
                {
                    isSuccess = false;
                    message = _helpers.LogException(e);
                }
            }

            return Json(new { isSuccess = isSuccess, message = message }, "text/html");
        }

        [Authorize]
        [HttpPost]
        public JsonResult updateProvinces(fleet_provinces province)
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
        public PartialViewResult getFleetColors()
        {
            using (_picklist = new PicklistModule())
            {
                var colors = _picklist.getColors();

                return PartialView("ColorsView", colors);
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult updateFleetColors(fleet_colors color)
        {
            bool isSuccess = true;
            string message = "";

            try
            {
                using (_db = new dbContext())
                {
                    color.color_name = color.color_name.ToUpper();

                    if (color.id > 0) // edit record
                    {
                        _db.Entry(color).State = EntityState.Modified;
                    }
                    else
                        _db.fleet_colors.Add(color);

                    if (_db.SaveChanges() <= 0)
                    {

                        isSuccess = false;
                        message = "Error in updating Fleet Vehicle Color information!";

                    }
                }
            }
            catch (Exception e)
            {
                using (_helpers = new Helper())
                {
                    isSuccess = false;
                    message = _helpers.LogException(e);
                } 
            }



            return Json(new { isSuccess = isSuccess, message = message }, "text/html");

        }

        [Authorize]

        public PartialViewResult getVehicleMakeView()
        {

            using (_picklist = new PicklistModule())
            {
                var make = _picklist.getFleetVehicleMake();

                return PartialView("vehicleMakeView", make);
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult updateVehicleMake(fleet_vehicle_make make)
        {
            bool isSuccess = true;
            string message = "";

            try
            {
                using (_db = new dbContext())
                {
                    make.vehicle_make = make.vehicle_make.ToUpper();
                    if (make.id > 0)
                       _db.Entry(make).State = EntityState.Modified;
                    else
                        _db.fleetVehicleMake.Add(make);

                    if (_db.SaveChanges() <= 0)
                    {
                        isSuccess = false;
                        message = "Error in updating Vehicle information!";
                    }
                }
            }
            catch (Exception e)
            {
                using (_helpers = new Helper())
                {
                    isSuccess = false;
                    message = _helpers.LogException(e);
                } 
            }
            
            return Json(new { isSuccess = isSuccess, message = message }, "text/html");
        }

        [Authorize]
        public PartialViewResult getVehicleModelView()
        {
            using (_picklist = new PicklistModule())
            {
                var models = _picklist.getFleetVehicleModels();

                return PartialView("vehicleModelView", models);
            }
        }

        [Authorize]
        public PartialViewResult getVehicleSeriesView()
        {
            using (_picklist = new PicklistModule())
            {
                var series = _picklist.getFleetVehicleSeries();

                return PartialView("vehicleSeriesView", series);
            }
        }

        [Authorize]
        public PartialViewResult getVehicleSeriesViewEdit(int id = 0)
        {
            _vehicleDetails = new vehicleTypeAddEditDetails();

            using (_picklist = new PicklistModule())
            {

                if (id != 0)
                    _vehicleDetails.fleetVehicleSeries = _picklist.getFleetVehicleSeries(id);

                _vehicleDetails.fleetVechileMakes = _picklist.getFleetVehicleMake();

                return PartialView("AddEditVehicleSeries", _vehicleDetails);
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult updateVehicleSeries(fleet_vehicle_series series, string actions = null)
        {
            bool isSuccess = true;
            string message = "";

            try
            {
                using (_db = new dbContext())
                {
                    if (series.id > 0) // edit record
                    {
                        _db.Entry(series).State = EntityState.Modified;
                    }
                    else
                        _db.fleetVehicleSeries.Add(series);

                    if (_db.SaveChanges() <= 0)
                    {

                        isSuccess = false;
                        message = "Error in updating Vehicle information!";

                    }
                }
                
            }
            catch (Exception e)
            {
                using (_helpers = new Helper())
                {
                    isSuccess = false;
                    message = _helpers.LogException(e);
                } 
            }

            return Json(new { isSuccess = isSuccess, message = message }, "text/html");

        }


        [Authorize]
        public string populateDropDownWithValues(string module = null, string referenceId = null, string referenceId2 = null) {
            
            string json = string.Empty;

            _vehicleDetails = new vehicleTypeAddEditDetails();

            using (_picklist = new PicklistModule())
            {
                switch (module)
                {
                    case "models":
                        
                        if(!string.IsNullOrEmpty(referenceId))
                            _vehicleDetails.fleetModels = _picklist.getFleetVehicleModels(0, referenceId);
                        break;

                    case "make":

                        if (!string.IsNullOrEmpty(referenceId))
                            _vehicleDetails.fleetVehicleTypeMake = _picklist.getVehicleTypeMake(referenceId);
                        break;

                    case "series":

                        if (!string.IsNullOrEmpty(referenceId) && !string.IsNullOrEmpty(referenceId2))
                            _vehicleDetails.fleetVehicleSeries = _picklist.getFleetVehicleSeries(0, referenceId, referenceId2, module);
                        break;

                    default:
                        break;
                }
                json = JsonConvert.SerializeObject(_vehicleDetails, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
            }

            return json;
        }

        [Authorize]
        public JsonResult deleteRecord(int integerId = 0, string module = null, string filterBy = null)
        {
            bool isSuccess = true;
            string message = "";

            using (_picklist = new PicklistModule()) 
            {
                try
                {
                    isSuccess = _picklist.deletePicklist(module, integerId, filterBy);
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
