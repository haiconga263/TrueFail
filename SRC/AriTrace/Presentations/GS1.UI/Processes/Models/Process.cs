using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GS1.UI.Processes.Models
{
    [Table("production_process")]
    public class Process
    {
        [Key]
        [Column("id")]
        public int Id { set; get; }

        [Column("partner_id")]
        public int PartnerId { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("point_id")]
        public int? PointId { set; get; }

        [Column("production_id")]
        public int? ProductionId { set; get; }

        [Column("product_id")]
        public int? ProductId { set; get; }

        [Column("farmer_id")]
        public int? FarmerId { set; get; }

        [Column("company_cultivation_id")]
        public int? CompanyCultivationId { set; get; }

        [Column("company_collection_id")]
        public int? CompanyCollectionId { set; get; }

        [Column("company_fulfillment_id")]
        public int? CompanyFulfillmentId { set; get; }

        [Column("company_distribution_id")]
        public int? CompanyDistributionId { set; get; }

        [Column("company_retailer_id")]
        public int? CompanyRetailerId { set; get; }

        [Column("collection_date")]
        public DateTime? CollectionDate { set; get; }

        [Column("fulfillment_date")]
        public DateTime? FulfillmentDate { set; get; }

        [Column("distribution_date")]
        public DateTime? DistributionDate { set; get; }

        [Column("retailer_date")]
        public DateTime? RetailerDate { set; get; }

        [Column("expiry_date")]
        public DateTime? ExpiryDate { set; get; }

        [Column("manufacturing_date")]
        public DateTime? ManufacturingDate { set; get; }

        [Column("growing_method_id")]
        public int? growingMethodId { set; get; }

        [Column("standard_expiry_date")]
        public DateTime? StandardExpiryDate { set; get; }

        [Column("description")]
        public string Description { set; get; }

        [Column("quantity")]
        public double Quantity { set; get; }

        [Column("uom")]
        public string Uom { set; get; }

        [Column("is_new")]
        public bool IsNew { get; set; }

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
