using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fulfillments.UI.Models
{
    [Table("fulfillment_order_item")]
    public class FulfillmentOrderItem
    {
        [Key]
        [Identity]
        [Column("id")]
        public string Id { set; get; }
        [Column("trace_code")]
        public string TraceCode { set; get; }
        [Column("product_id")]
        public string ProductId { set; get; }
        [Column("uom_id")]
        public string UomId { set; get; }
        [Column("shipped_quantity")]
        public string ShippedQuantity { set; get; }
        [Column("deliveried_quantity")]
        public string DeliveriedQuantity { set; get; }
        [Column("fulfillment_order_id")]
        public string FulfillmentOrderId { set; get; }

    }
}
