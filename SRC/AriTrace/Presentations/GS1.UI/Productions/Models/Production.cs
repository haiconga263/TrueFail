using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GS1.UI.Productions.Models
{
    [Table("production")]
    public class Production
    {
        [Key]
        [Column("id")]
        public int Id { set; get; }

        [Column("partner_id")]
        public int PartnerId { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("product_id")]
        public int ProductId { set; get; }

        [Column("gtin_id")]
        public int GTINId { set; get; }

        [Column("growing_method_id")]
        public int? GrowingMethodId { set; get; }

        [Column("production_image_id")]
        public int? ProductionImageId { set; get; }

        [Column("country_of_origin")]
        public string CountryOfOrigin { set; get; }

        [Column("trademark")]
        public string Trademark { set; get; }

        [Column("commercial_claim")]
        public string CommercialClaim { set; get; }

        [Column("product_size")]
        public string ProductSize { set; get; }

        [Column("grade")]
        public string Grade { set; get; }

        [Column("colour")]
        public string Colour { set; get; }

        [Column("shape")]
        public string Shape { set; get; }

        [Column("variety")]
        public string Variety { set; get; }

        [Column("commercial_type")]
        public string CommercialType { set; get; }

        [Column("colour_of_flesh")]
        public string ColourOfFlesh { set; get; }

        [Column("post_harvest_treatment")]
        public string PostHarvestTreatment { set; get; }

        [Column("post_harvest_processing")]
        public string PostHarvestProcessing { set; get; }

        [Column("cooking_type")]
        public string CookingType { set; get; }

        [Column("seed_properties")]
        public string SeedProperties { set; get; }

        [Column("trade_package_content_quantity")]
        public string TradePackageContentQuantity { set; get; }

        [Column("trade_unit_package_type")]
        public string TradeUnitPackageType { set; get; }

        [Column("consumer_unit_content_quantity")]
        public string ConsumerUnitContentQuantity { set; get; }

        [Column("trade_unit")]
        public string TradeUnit { set; get; }

        [Column("comsumer_unit_package_type")]
        public string ComsumerUnitPackageType { set; get; }

        [Column("comsumer_unit")]
        public string ComsumerUnit { set; get; }

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
