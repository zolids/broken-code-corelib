using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    [Table("dealers_attachment")]
    public class Dealer_attachment
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DealerAttID { get; set; }

        public string DealerRefCode { get; set; }

        public string FileName { get; set; }

        public long FileSize { get; set; }

        public string file_source_location { get; set; }

        public string Description { get; set; }


        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateUpload { get; set; }

    }

    [Table("Dealers")]
    public class Dealers
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public string DealerRefCode { get; set; }

        public string DealerNo { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PostalCode { get; set; }

        public string Province { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string OPCName { get; set; }

        public string OPCEmail { get; set; }

        public string OPCPhone { get; set; }

        public string OPCFax { get; set; }

        public string FPCName { get; set; }

        public string FPCEmail { get; set; }

        public string FPCPhone { get; set; }

        public string FPCFax { get; set; }

        public string Person1 { get; set; }

        public string Person2 { get; set; }

        public string Person3 { get; set; }

        public string Person4 { get; set; }

        public string DeliveryAddress { get; set; }

        public string CurrencyCode { get; set; }

        public decimal Rate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> DateFrom { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> DateTo { get; set; }

        public string Privilege { get; set; }

        public int TierID { get; set; }

        public int ConTypeID { get; set; }

        public string CGrpID { get; set; }

        public int DealerTypeID { get; set; }

        public int CreditTermID { get; set; }

        public decimal CreditLimit { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> DateApproved { get; set; }

        public byte Blocked { get; set; }

        public int InterestID { get; set; }

        [DisplayFormat(DataFormatString = "0:yyyy-MM-dd")]
        public Nullable<DateTime> DateBlocked { get; set; }

        public string Reasons { get; set; }

        public string lblUDF1 { get; set; }

        public string lblUDF2 { get; set; }

        public string lblUDF3 { get; set; }

        public string lblUDF4 { get; set; }

        public string lblUDF5 { get; set; }

        public string Udf1 { get; set; }

        public string Udf2 { get; set; }

        public string Udf3 { get; set; }

        public string Udf4 { get; set; }

        public string Udf5 { get; set; }

        public string Notes { get; set; }

        public int CreatedByID { get; set; }

        [DisplayFormat(DataFormatString = "0:yyyy-MM-dd")]
        public Nullable<DateTime> DateCreated { get; set; }

        public string ModifiedByID { get; set; }

        [DisplayFormat(DataFormatString = "0:yyyy-MM-dd")]
        public Nullable<DateTime> DateModified { get; set; }

        public int AccountID { get; set; }

        [NotMapped]
        public string console_value { get; set; }

        [NotMapped]
        public string deleted_files { get; set; }

    }

}