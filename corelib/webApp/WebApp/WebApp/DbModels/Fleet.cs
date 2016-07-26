using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Models
{
    [Table("fleet")]
    public class Fleet
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long FleetID { get; set; }

        [Display(Name = "Equipment ID")]
        public string NLID { get; set; }

        [Display(Name = "VIN")]
        public string VIN { get; set; }

        [Display(Name = "Plate No.")]
        public string PlateNo { get; set; }

        [Display(Name = "Registration Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        [DataType(DataType.Date)]
        public Nullable<DateTime> RegistrationDate { get; set; }

        [Display(Name = "Registration No.")]
        public string RegistrationNo { get; set; }

        [Display(Name = "Engine No.")]
        public string EngineNo { get; set; }

        public string EngineCode { get; set; }

        [Display(Name = "Engine Type")]
        public string EngineType { get; set; }

        [Display(Name = "Year")]
        public string Year { get; set; }

        [Display(Name = "Make")]
        public string Make { get; set; }

        [Display(Name = "Model")]
        public string Model { get; set; }

        [Display(Name = "Series")]
        public string Series { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }

        [Display(Name = "Vehicle Category")]
        public string VehCat { get; set; }

        [Display(Name = "Vehicle Type")]
        public string VehType { get; set; }

        [Display(Name = "Non-Vehicle Type")]
        public string NonVehType { get; set; }

        [Display(Name = "Requisition Status")]
        public string ReqStatus { get; set; }

        [Display(Name = "Lease Status")]
        public string UtilStatus { get; set; }

        [Display(Name = "Maintenance Status")]
        public string Status { get; set; }

        [Display(Name = "Last Service Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> LastService { get; set; }

        [Display(Name = "Next Service Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> NextServiceDate { get; set; }

        [Display(Name = "Service Release Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> ReleasedDate { get; set; }

        public Nullable<int> Released { get; set; }

        [Display(Name = "Released")]
        public string ReleasedYN { get; set; }

        [Display(Name = "Odometer Reading")]
        public decimal? Mileage { get; set; }

        [Display(Name = "Odometer Reading (Mile)")]
        public string MileConversionValue { get; set; }

        [Display(Name = "Meter Type")]
        public string MeterType { get; set; }

        [Display(Name = "Fuel Type")]
        public string FuelType { get; set; }

        [Display(Name = "Transmission")]
        public string Transmission { get; set; }

        [Display(Name = "Doors")]
        public int Doors { get; set; }

        [Display(Name = "Tyre Size")]
        public string TyreSize { get; set; }

        [Display(Name = "UdfLbl1")]
        public string UdfLbl1 { get; set; }

        [Display(Name = "UdfLbl2")]
        public string UdfLbl2 { get; set; }

        [Display(Name = "UdfLbl3")]
        public string UdfLbl3 { get; set; }

        [Display(Name = "UdfLbl4")]
        public string UdfLbl4 { get; set; }

        [Display(Name = "UdfLbl5")]
        public string UdfLbl5 { get; set; }

        [Display(Name = "UdfLbl6")]
        public string UdfLbl6 { get; set; }

        [Display(Name = "UdfLbl7")]
        public string UdfLbl7 { get; set; }

        [Display(Name = "UdfLbl8")]
        public string UdfLbl8 { get; set; }

        [Display(Name = "UdfLbl9")]
        public string UdfLbl9 { get; set; }

        [Display(Name = "UdfLbl10")]
        public string UdfLbl10 { get; set; }

        [Display(Name = "Udf1")]
        public string Udf1 { get; set; }

        [Display(Name = "Udf2")]
        public string Udf2 { get; set; }

        [Display(Name = "Udf3")]
        public string Udf3 { get; set; }

        [Display(Name = "Udf4")]
        public string Udf4 { get; set; }

        [Display(Name = "Udf5")]
        public string Udf5 { get; set; }

        [Display(Name = "Udf6")]
        public string Udf6 { get; set; }

        [Display(Name = "Udf7")]
        public string Udf7 { get; set; }

        [Display(Name = "Udf8")]
        public string Udf8 { get; set; }

        [Display(Name = "Udf9")]
        public string Udf9 { get; set; }

        [Display(Name = "Udf10")]
        public string Udf10 { get; set; }


        [Display(Name = "Ownership Type")]
        public string OwnershipType { get; set; }

        [Display(Name = "Province")]
        public string Province { get; set; }

        public string Notes { get; set; }

        [Display(Name = "Passenger Capacity")]
        public long PassCap { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Barcode")]
        public string Barcode { get; set; }

        [Display(Name = "Engine Size")]
        public string EngineSize { get; set; }

        [Display(Name = "Fuel Sticker")]
        public string FuelSticker { get; set; }

        [Display(Name = "Fuel Capacity")]
        public string FuelCapacity { get; set; }

        [Display(Name = "Key Tag")]
        public string KeyTag { get; set; }

        [Display(Name = "Transmission Code")]
        public string TransmissionCode { get; set; }

        public Nullable<byte> Armored { get; set; }

        [Display(Name = "Site Code")]
        public string SiteCode { get; set; }

        [Display(Name = "Accessories")]
        public string Accessories { get; set; }

        [Display(Name = "GVWR")]
        public string GVWR { get; set; }

        [Display(Name = "Others")]
        public string Others { get; set; }

        [Display(Name = "WSD")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> WSD { get; set; }

        [Display(Name = "Vehicle Tag")]
        public string VehicleTag { get; set; }

        [Display(Name = "TPE No.")]
        public string TPEno { get; set; }

        [Display(Name = "Regional Command")]
        public string RegionalCommand { get; set; }

        public string OLDNLID { get; set; }

        public string OldSiteCode { get; set; }

        public Nullable<byte> Active { get; set; }

        public Nullable<byte> ExcludeInspect { get; set; }

        [Display(Name = "Total Repair Count")]
        public long RepairCount { get; set; }

        public decimal PurchasePrice { get; set; }

        [Display(Name = "DateOfPurchase")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> DateOfPurchase { get; set; }

        public string DealerRefCode { get; set; }

        public byte DepreciationPeriod { get; set; }

        public byte MainFollow { get; set; }

        public byte ReturnFollow { get; set; }

        public Nullable<byte> IsArchive { get; set; }

        public Nullable<byte> TempClose { get; set; }

        public string MainFollowNotes { get; set; }

        public string ReturnFollowNotes { get; set; }

        [Display(Name = "Actual Location")]
        public string ActLocation { get; set; }

        public string UIC { get; set; }

        public string TransferRemarks { get; set; }

        public Nullable<byte> Flagged { get; set; }

        public string FlagReason { get; set; }

        public Nullable<byte> FlaggedUtil { get; set; }

        public string FlagUtilReason { get; set; }

        [NotMapped]
        public string console_value { get; set; }

        [NotMapped]
        public string deleted_files { get; set; }

        [NotMapped]
        public string deleted_attachment { get; set; }

    }
}