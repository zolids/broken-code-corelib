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

    [Table("fleet_vehicle_type")]
    public class fleet_vehicle_type
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int VehTypeID { get; set; }

        public string Description { get; set; }

        public int ssKM { get; set; }

        public int ssWeek { get; set; }

        public string SiteCode { get; set; }

        public int LicenseCat { get; set; }

        public int ssKMUNIT { get; set; }

        public int ssWeekUNIT { get; set; }

        [NotMapped]
        public List<string> vehicle_type_selected { get; set; }

    }

    [Table("fleet_vehicle_models")]
    public class fleet_vehicle_models
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ModelID { get; set; }

        public string Model { get; set; }

        public string Make { get; set; }

        public int ForAid { get; set; }

        public string VehicleCode { get; set; }

    }

    [Table("fleet_vehicle_make")]
    public class fleet_vehicle_make
    {
        [Key]
        public int id { get; set; }

        public string vehicle_make { get; set; }

    }

    [Table("fleet_vehicle_type_mapping")]
    public class fleet_vehicle_type_mapping
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int VTModelID { get; set; }

        public int VehTypeID { get; set; }

    }

    [Table("fleet_vehicle_series")]
    public class fleet_vehicle_series
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string SeriesNo { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

    }

    public class vehicle_type_make
    {
        public string Make { get; set; }
    }

    public class vehicleModelList
    {
        public int ModelID { get; set; }

        public string Model { get; set; }

        public string Make { get; set; }

        public int ForAid { get; set; }

        public string VehicleCode { get; set; }

    }

}