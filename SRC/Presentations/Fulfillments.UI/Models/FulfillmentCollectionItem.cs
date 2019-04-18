using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fulfillments.UI.Models
{
    [Table("fulfillment_collection_item")]
    public class FulfillmentCollectionItem
    {
        [Key]
        [Identity]
        [Column("id")]
        public long Id { set; get; }

        [Column("fulfillment_collection_id")]
        public long ShippingId { set; get; }

        [Column("trace_code")]
        public string TraceCode { set; get; }

        [Column("product_id")]
        public int ProductId { set; get; }

        [Column("uom_id")]
        public int UoMId { set; get; }

        [Column("shipped_quantity")]
        public long ShippedQuantity { set; get; }

        [Column("deliveried_quantity")]
        public long? DeliveriedQuantity { set; get; }
    }
}
