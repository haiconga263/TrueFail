using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Companies.Models
{
    [Table("company")]
    public class Company
    {
        [Key]
        [Column("id")]
        public int Id { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("tax_code")]
        public string TaxCode { set; get; }

        [Column("website")]
        public string Website { set; get; }

        [Column("contact_id")]
        public int? ContactId { set; get; }

        [Column("address_id")]
        public int? AddressId { set; get; }

        [Column("logo_path")]
        public string LogoPath { set; get; }

        [Column("description")]
        public string Description { set; get; }

        [Column("company_type_id")]
        public int? CompanyTypeId { set; get; }

        [Column("is_partner")]
        public bool IsPartner { set; get; }

        [Column("gs1_code")]
        public int GS1Code { set; get; }

        [JsonIgnore]
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("is_used")]
        public bool IsUsed { get; set; }

        [Column("created_by")]
        public int CreatedBy { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("modified_by")]
        public int ModifiedBy { get; set; }

        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }
    }
}
