using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.helpers;

namespace WebApp.Models
{
    public class FleetModule : Helper, IDisposable
    {

        dbContext _fleet = new dbContext();

        public IEnumerable<Fleet> getFleet(string nlid = null)
        {

            string where = string.Empty;

            if (string.IsNullOrEmpty(nlid)) where = "WHERE NLID = " + nlid;

            string sql = "SELECT * FROM [dbo].[fleet] " + where + " ORDER BY request_date;";

            var users = _fleet.Database.SqlQuery<Fleet>(sql);

            return users;

        }
    }
}