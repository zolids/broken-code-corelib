﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Table("gsa_users")]
    public class Users
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Display(Name = "User ID")]
        public long user_id { get; set; }

        [Required(ErrorMessage = "Username Required", AllowEmptyStrings = false)]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password Required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [NotMapped]
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        [Display(Name = "Full Name")]
        public string name { get; set; }

        [Display(Name = "Position")]
        public string position { get; set; }

        public int usertype { get; set; }

        [Display(Name = "User Type")]
        public string usertypedisp { get; set; }

        public int rfqApprover { get; set; }

        [Display(Name = "RFQ Approver")]
        public string rfqApproveryn { get; set; }

        [Display(Name = "RFQ Amount")]
        [Required(ErrorMessage = "RFQ Approver Required")]
        public int rfqAmount { get; set; }

        [Display(Name = "E-Mail")]
        public string email { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Email Recipient")]
        public int? email_recipient { get; set; }

        [NotMapped]
        public bool rfqApproverBool
        {
            get
            {
                return rfqApprover == 0 ? false : true;
            }
            set
            {
                rfqApprover = value ? 1 : 0;
            }
        }

        public string accessible_page { get; set; }

    }

    [Table("gsa_user_sites")]
    public class UserSites
    {
        [Key]
        [Column(Order = 1)]
        public long user_id { get; set; }

        [Key]
        [Column(Order = 0)]
        public long gus_site_id { get; set; }
    }

    [Table("wp_pages")]
    public class wp_pages
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long PageID { get; set; }
        public string Page { get; set; }
        public string SubPage { get; set; }
        public byte? View { get; set; }
        public byte? Download { get; set; }
        public byte? Upload { get; set; }
        public int? page_type { get; set; }
    }

    [Table("gsa_user_pages")]
    public class gsa_user_pages
    {
        [Key]
        [Column(Order = 0)]
        public long user_id { get; set; }

        [Key]
        [Column(Order = 1)]
        public long? PageID { get; set; }

        public byte? View { get; set; }

        public byte? Download { get; set; }

        public byte Upload { get; set; }

    }

    public class user_pages_available
    {
      
        public long user_id { get; set; }
        
        public long? PageID { get; set; }

        public byte? View { get; set; }

        public byte? Download { get; set; }

        public byte Upload { get; set; }

        public string Page { get; set; }

        public string SubPage { get; set; }

        public int page_type { get; set; }

    }

}