using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Farmers.Models
{
    [Table("farmer")]
    public class Farmer : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("user_account_id")]
        public int UserId { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("is_company")]
        public bool IsCompany { set; get; }

        [Column("tax_code")]
        public string TaxCode { set; get; }

        [Column("image_url")]
        public string ImageURL { set; get; }

        [Column("contact_id")]
        public int? ContactId { set; get; }

        [Column("address_id")]
        public int? AddressId { set; get; }

        [Column("is_used")]
        public bool IsUsed { set; get; }
    }
}
