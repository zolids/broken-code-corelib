using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Models
{
    [Table("tbl_requests", Schema = "extras")]
    public class tbl_requests
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string request_type { get; set; }

        public int requestor_id { get; set; }

        [NotMapped]
        public string requestor_name { get; set; }

        public string request_status { get; set; }

        public string department { get; set; }

        public string vehicle_type { get; set; }

        public string vehicle_model { get; set; }

        public int fund_source { get; set; }

        public int under_agreement { get; set; }

        public int add_to_fleet { get; set; }

        public int mod_require { get; set; }

        public int alternative_fuel { get; set; }

        [AllowHtml]
        public string justifications { get; set; }

        public int project_site_id { get; set; }

        public DateTime? request_date { get; set; }

        [AllowHtml]
        public string additional_notes { get; set; }

        public DateTime? modified_date { get; set; }

    }
}