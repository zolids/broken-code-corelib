using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{


    [Table("tbl_options", Schema = "extras")]
    public class tbl_config
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string name { get; set; }

        public string value { get; set; }
    }

    [Table("tbl_projects", Schema = "extras")]
    public class tbl_projects
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
    }

    [Table("tbl_projects_sites", Schema = "extras")]
    public class tbl_project_sites
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        public int project_id { get; set; }
    }

    [Table("tbl_logs", Schema = "extras")]
    public class tbl_logs
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string type { get; set; } // email or logs
        public string source { get; set; } // source of event
        public string event_type { get; set; } // U,I,D
        public string message { get; set; }
        public string reference_code { get; set; }
        public DateTime? datetime { get; set; }
        public int user_id { get; set; }
    }

    [Table("tbl_tokens", Schema = "extras")]
    public class tbl_tokens
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int user_id { get; set; }
        public string module { get; set; }
        public string token { get; set; }
        public DateTime? timestamp { get; set; }
        public int used { get; set; }
    }

    [Table("tbl_emails", Schema = "extras")]
    public class tbl_emails
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string reference_code { get; set; }
        public string reference_table { get; set; }
        public string body { get; set; }
        public string subject { get; set; }
        public string include_this_email { get; set; }
        public DateTime? datetime { get; set; }
        public DateTime? modified_date { get; set; }
        public int retry { get; set; }
        public int sent { get; set; }
    }

}