using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Order.UI.Models
{
    [Table("farmer_retailer_order_items")]
    public class FarmerRetailerOrderItems
    {
        [Key]
        [Identity]
        [Column("id")]
        public long Id { set; get; }

        [Column("retailer_id")]
        public int RetailerId { set; get; }

        [Column("retailer_order_id")]
        public long RetailerOrderId { set; get; }

        [Column("retailer_order_item_id")]
        public long RetailerOrderItemId { set; get; }

        [Column("is_planning")]
        public bool IsPlanning { set; get; }

        [Column("farmer_id")]
        public int FarmerId { set; get; }

        [Column("farmer_order_id")]
        public long FarmerOrderId { set; get; }

        [Column("farmer_order_item_id")]
        public long FarmerOrderItemId { set; get; }

        [Column("product_id")]
        public int ProductId { set; get; }

        [Column("quantity")]
        public int Quantity { set; get; }

        [Column("uom_id")]
        public int UoMId { set; get; }
    }
}
