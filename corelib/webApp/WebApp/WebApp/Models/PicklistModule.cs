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
    }
}