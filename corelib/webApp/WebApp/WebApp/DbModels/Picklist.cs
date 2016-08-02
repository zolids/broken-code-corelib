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

    [Table("ownership_types")]
    public class OwnershipTypes
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string code { get; set; }

        public string description { get; set; }
    }

    public class vehicle_type_make
    {
        public string Make { get; set; }
    }

    [Table("fleet_colors")]
    public class fleet_colors
    {

        [Key]
        [HiddenInput(DisplayValue = false)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string color_name { get; set; }

    }

    [Table("parts_category")]
    public class parts_category
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string category_name { get; set; }

    }

    [Table("parts_locations")]
    public class parts_locations
    {

        private DateTime? startdate = null;

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string location_name { get; set; }

        public int status { get; set; }

        public string remarks { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> date_updated
        {
            get
            {
                return this.startdate.HasValue
                   ? this.startdate.Value
                   : DateTime.Now;
            }
            set { this.startdate = value; }
        }

    }

    [Table("parts_stype")]
    public class parts_stype
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string type { get; set; }

    }

    [Table("parts")]
    public class parts_details
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PartID { get; set; }
        public string PartNo { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string VehicleType { get; set; }
        public int Qty { get; set; }
        public string UOM { get; set; }
        public int Incoming { get; set; }
        public string PORef { get; set; }
        public int Outgoing { get; set; }
        public int ReOrderPoint { get; set; }
        public int ReOrderQty { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? ProfitPercentage { get; set; }
        public decimal? SalesPrice { get; set; }
        public string PartType { get; set; }
        public string PartTypeSC { get; set; }
        public string AccGrpID { get; set; }
        public Double? PartsTotVal { get; set; }
        public Double? SalesVAT { get; set; }
        public Double? SalesVATFree { get; set; }
        public string SalesVATCode { get; set; }
        public Double? PurchaseVAT { get; set; }
        public Double? PurchaseVATFree { get; set; }
        public string PurchaseVATCode { get; set; }
        public string AttachedItem { get; set; }
        public int? AttachedQty { get; set; }
        public string Location { get; set; }
        public int CatID { get; set; }
        public string VIN { get; set; }
        public string SupRefCode { get; set; }
        public string UnitType { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public int Ordered { get; set; }
        public int? PGrpID { get; set; }
        public int PartPGrpID { get; set; }
        public int? PCatID { get; set; }
        public string Movement { get; set; }
        public string UDFL1 { get; set; }
        public string UDFL2 { get; set; }
        public string UDFL3 { get; set; }
        public string UDFL4 { get; set; }
        public string UDFL5 { get; set; }
        public string UDF1 { get; set; }
        public string UDF2 { get; set; }
        public string UDF3 { get; set; }
        public string UDF4 { get; set; }
        public string UDF5 { get; set; }
        public int? RandomStockChecker { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? RandomStockCheckerDateTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? DateModified { get; set; }
        public string Shelf { get; set; }
        public decimal GSACostPriceUSD { get; set; }

        [NotMapped]
        public string console_value { get; set; }

    }

    [Table("unit_measures")]
    public class unit_measures
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string Unit { get; set; }

    }

    [Table("employees_local")]
    public class employees_local
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public string EmpCode { get; set; }
        public string LName { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string DisplayName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string SkillLevel { get; set; }
        public string Status { get; set; }
        public string ContactNo { get; set; }
        public string SiteCode { get; set; }
    }

    [Table("employees_managers")]
    public class employees_managers
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public string EmpCode { get; set; }
        public string LName { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string DisplayName { get; set; }
        public string Category { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }
        public string ContactNo { get; set; }
        public int? ShowAssignedTo { get; set; }
        public string TypeID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Switch { get; set; }
    }

    [Table("department")]
    public class department
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string Department { get; set; }

    }

    [Table("category")]
    public class categories
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string category { get; set; }

    }

    [Table("positions")]
    public class positions
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string position { get; set; }

        public string category { get; set; }

    }

    [Table("skill_level")]
    public class skill_level
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string level { get; set; }

        public string category { get; set; }

    }

    [Table("repair_type")]
    public class repair_type
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string RepairType { get; set; }
        public decimal LaborCost { get; set; }
        public decimal ProfitPercentage { get; set; }

    }

}