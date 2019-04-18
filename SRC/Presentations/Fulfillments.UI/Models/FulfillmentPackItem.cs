using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fulfillments.UI.Models
{
    [Table("fulfillment_fr_order_item")]
    public class FulfillmentPackItem
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }
        [Column("fulfillment_fr_order_id")]
        public int FulfillmentFrOrderId { set; get; }
        [Column("uom_id")]
        public int UomId { set; get; }
        [Column("product_id")]
        public int ProductId { set; get; }
        [Column("adap_quantity")]
        public int AdapQuantity { set; get; }
        [Column("deliveried_quantity")]
        public int DeliveriedQuantity { set; get; }
    }
}
