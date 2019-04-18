using Common.Attributes;
using Common.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Collections.UI.POActivities.Models
{
    [Table("collection_purchase_order_activity")]
    public class POActivity : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public long Id { set; get; }

        [Column("purchase_order_item_id")]
        public long PurchaseOrderItemId { set; get; }

        [Column("farmer_id")]
        public int FarmerId { set; get; }

        [Column("collector_id")]
        public int CollectorId { set; get; }

        [Column("quantity")]
        public double Quantity { set; get; }

        [Column("price_real")]
        public decimal PriceReal { set; get; }

        [Column("description")]
        public string Description { set; get; }

        [Column("purchase_date")]
        public DateTime PurchaseDate { set; get; }

        [Column("image_url")]
        public string ImageUrl { set; get; }

    }
}
