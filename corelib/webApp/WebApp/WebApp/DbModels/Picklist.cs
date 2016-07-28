using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Models
{
    [Table("fleet_provinces")]
    public class fleet_provinces
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string name { get; set; }


    }

}