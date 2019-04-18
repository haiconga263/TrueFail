using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Common.Models
{
    [Table("cf_shipping_item")]
    public class CFShippingItem
    {
        [Key]
        [Identity]
        [Column("id")]
        public long Id { set; get; }

        [Column("cf_shipping_id")]
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
