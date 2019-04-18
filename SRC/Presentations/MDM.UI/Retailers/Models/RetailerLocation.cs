using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Retailers.Models
{
    [Table("retailer_location")]
    public class RetailerLocation : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("gln")]
        public string GLN { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("description")]
        public string Description { set; get; }

        [Column("image_url")]
        public string ImageURL { set; get; }

        [Column("retailer_id")]
        public int RetailerId { set; get; }

        [Column("contact_id")]
        public int ContactId { set; get; }

        [Column("address_id")]
        public int AddressId { set; get; }

        [Column("distribution_id")]
        public int? DistributionId { set; get; }

        [Column("is_used")]
        public bool IsUsed { set; get; }
    }
}
