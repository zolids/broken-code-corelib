using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TarsierEyes;
using WebApp.helpers;

namespace WebApp.Models
{
    public class RequestsModule : Helper, IDisposable
    {

        dbContext _db = new dbContext();

        public IEnumerable<tbl_requests> getAcquisition(int request_id = 0)
        {

            string where = string.Empty;

            if (request_id > 0) where = "WHERE id = " + request_id;

            string sql = "SELECT * FROM [extras].[tbl_requests] " + where + " ORDER BY request_date DESC;";

            var users = _db.Database.SqlQuery<tbl_requests>(sql);

            return users;

        }

    }

}