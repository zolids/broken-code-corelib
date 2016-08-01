using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.helpers;

namespace WebApp.Models
{
    public class PicklistModule : Helper, IDisposable
    {
        dbContext _db = new dbContext();

        public IEnumerable<fleet_provinces> getFleetProvinces(int id = 0)
        {

            string where = string.Empty;

            if (id > 0) where = "WHERE id = " + id;

            string sql = "SELECT * FROM [dbo].[fleet_provinces] " + where;

            var provinces = _db.Database.SqlQuery<fleet_provinces>(sql);

            return provinces;

        }

        public IEnumerable<fleet_vehicle_type> getFleetVehicleTypes(int id = 0)
        {

            string where = string.Empty;

            if (id > 0) where = "WHERE VehTypeID = " + id;

            string sql = "SELECT * FROM [dbo].[fleet_vehicle_type] " + where;

            var types = _db.Database.SqlQuery<fleet_vehicle_type>(sql);

            return types;

        }

        public IEnumerable<fleet_vehicle_models> getFleetVehicleModels(int id = 0, string makerName = null)
        {
            string where    = string.Empty;
            string groupBy  = string.Empty;
            string sql      = string.Empty;

            if (id > 0)
                where = "WHERE ModelID = " + id;

            if (!string.IsNullOrEmpty(makerName))
                where = "WHERE Make = '" + makerName + "'";

            sql = "SELECT Make, Model, ModelID, ForAid, VehicleCode FROM [dbo].[fleet_vehicle_models] " + where +
                  " GROUP BY Make, Model, ModelID, ForAid, VehicleCode ORDER BY Make";

            var fleetVehicleModel = _db.Database.SqlQuery<fleet_vehicle_models>(sql);

            return fleetVehicleModel;

        }

        public IEnumerable<fleet_vehicle_type_mapping> getMappedVehicleTypes(int vehicleId = 0)
        {

            string where = string.Empty;

            if (vehicleId > 0) where = "WHERE VehTypeID = " + vehicleId;

            string sql = "SELECT * FROM [dbo].[fleet_vehicle_type_mapping] " + where;

            var mappings = _db.Database.SqlQuery<fleet_vehicle_type_mapping>(sql);

            return mappings;

        }

        public IEnumerable<fleet_vehicle_make> getFleetVehicleMake(int id = 0)
        {
            string where = string.Empty;

            if (id > 0) where = "WHERE VehTypeID = " + id;

            string sql = "SELECT * FROM [dbo].[fleet_vehicle_make] " + where;

            var makes = _db.Database.SqlQuery<fleet_vehicle_make>(sql);

            return makes;

        }

        public IEnumerable<fleet_vehicle_series> getFleetVehicleSeries(int id = 0, string make = null, string model = null, string module = null)
        {
            string where = string.Empty;

            if (!string.IsNullOrEmpty(module) && module == "series")

                where = "WHERE Make = '" + make + "' AND Model = '" + model + "' ";

            else if (id > 0)

                where = "WHERE id = " + id;
            
            string sql = "SELECT * FROM [dbo].[fleet_vehicle_series] " + where + " ORDER by Make;";

            var result = _db.Database.SqlQuery<fleet_vehicle_series>(sql);

            return result;

        }

        public IEnumerable<fleet_colors> getColors(string colorname = null)
        {
            string where = string.Empty;

            if (!string.IsNullOrEmpty(colorname)) where = "WHERE color_name = '" + colorname + "'; ";

            string sql = "SELECT * FROM [dbo].[fleet_colors] " + where;

            var colors = _db.Database.SqlQuery<fleet_colors>(sql);

            return colors;

        }

        public IEnumerable<vehicle_type_make> getVehicleTypeMake(string vehicleTypeName = null)
        {
            string where = string.Empty, sql = string.Empty;

            if (!string.IsNullOrEmpty(vehicleTypeName)) {
                    sql = "SELECT DISTINCT t1.Make FROM [dbo].[fleet_vehicle_models] t1 WHERE t1.ModelID IN ( " +
                            "(SELECT t2.VTModelID FROM [dbo].[fleet_vehicle_type_mapping] t2 WHERE VehTypeID = " +
                            "(SELECT t3.VehTypeID FROM [dbo].[fleet_vehicle_type] t3 WHERE t3.Description = '" + vehicleTypeName + "')));";
            }
            
            var typeName = _db.Database.SqlQuery<vehicle_type_make>(sql);

            return typeName;

        }

        public IEnumerable<parts_category> partsCategory(int id = 0)
        {

            string where = string.Empty;

            if (id > 0) where = "WHERE id = " + id;

            string sql = "SELECT * FROM [dbo].[parts_category] " + where;

            var parts_category = _db.Database.SqlQuery<parts_category>(sql);

            return parts_category;

        }

        public IEnumerable<parts_stype> partsType(int id = 0)
        {

            string where = string.Empty;

            if (id > 0) where = "WHERE id = " + id;

            string sql = "SELECT * FROM [dbo].[parts_stype] " + where;

            var parts_type = _db.Database.SqlQuery<parts_stype>(sql);

            return parts_type;

        }
        
        public IEnumerable<unit_measures> UnitMeasures(int id = 0)
        {

            string where = string.Empty;

            if (id > 0) where = "WHERE id = " + id;

            string sql = "SELECT * FROM [dbo].[unit_measures] " + where;

            var unit_m = _db.Database.SqlQuery<unit_measures>(sql);

            return unit_m;

        }

        public bool deletePicklist(string table_name = null, int reference_id = 0, string filter = null)
        {

            bool success = true;

            if (string.IsNullOrEmpty(table_name) || reference_id <= 0)
                return false;

            filter = ((filter != null && filter != "undefined") ? filter.ToString() : "id");

            string sql = "DELETE FROM [dbo].[" + table_name + "] WHERE " + filter + " = " + reference_id;

            if (_db.Database.ExecuteSqlCommand(sql) <= 0)
                success = false;

            return success;

        }

    }
}