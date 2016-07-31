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
                where = "WHERE Make = " + makerName;

            sql = "SELECT Make, Model, ModelID, ForAid, VehicleCode FROM [dbo].[fleet_vehicle_models] " + where +
                  " GROUP BY Make, Model, ModelID, ForAid, VehicleCode";

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

        public bool deletePicklist(string table_name, int reference_id)
        {

            bool success = true;

            if (string.IsNullOrEmpty(table_name) || reference_id <= 0)
                return false;

            string sql = "DELETE FROM [dbo].[" + table_name + "] WHERE id = " + reference_id;

            if (_db.Database.ExecuteSqlCommand(sql) <= 0)
                success = false;

            return success;

        }

    }
}