using System;
using System.Web;
using System.Linq;
using WebApp.Models;
using WebApp.helpers;
using System.Collections.Generic;

namespace WebApp.Models
{

    public class ReferenceFleetData
    {
        public long FleetID { get; set; }
        public string NLID { get; set; }
        public string VIN { get; set; }
        public string OwnershipType { get; set; }
        public string EngineNo { get; set; }
        public string PlateNo { get; set; }
    }

    public class DealerModule : Helper, IDisposable
    {
        private dbContext _db = new dbContext();

        public IEnumerable<Dealers> getDealers(string dealerCode = null)
        {

            string where = string.Empty;

            if (!string.IsNullOrEmpty(dealerCode))

                where = "WHERE DealerRefCode = '" + dealerCode + "' ";
         
            string sql = "SELECT * FROM [dbo].[dealers] " + where + " ORDER BY DateCreated DESC;";

            var dealers = _db.Database.SqlQuery<Dealers>(sql);

            return dealers;

        }

        public IEnumerable<Dealer_attachment> getDealerAttachment(string refCode = null)
        {

            string where = string.Empty;

            if (!string.IsNullOrEmpty(refCode))

                where = "WHERE DealerRefCode = '" + refCode + "' ";

            string sql = "SELECT * FROM [dbo].[dealers_attachment] " + where + " ORDER BY DateUpload DESC;";

            var attachment = _db.Database.SqlQuery<Dealer_attachment>(sql);

            return attachment;

        }

        public IEnumerable<ReferenceFleetData> getReferenceFleetData(string refCode = null)
        {

            string sql = string.Empty;

            if (!string.IsNullOrEmpty(refCode))
                sql = "SELECT FleetID,NLID,VIN,OwnershipType,EngineNo,PlateNo FROM [dbo].[fleet] WHERE DealerRefCode = '" + refCode + "' ";

            var fleet = _db.Database.SqlQuery<ReferenceFleetData>(sql);

            return fleet;

        }

    }

}