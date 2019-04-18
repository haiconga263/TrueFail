using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fulfillments.UI.Models
{
    [Table("retailer_order_item")]
    public class FulfillmentRetailerOrderItem
    {
        [Key]
        [Identity]
        [Column("id")]
        public long Id { set; get; }

        [Column("retailer_order_id")]
        public long RetailerOrderId { set; get; }

        [Column("product_id")]
        public int ProductId { set; get; }

        [Column("status_id")]
        public int StatusId { set; get; }

        [Column("price")]
        public decimal Price { set; get; }

        [Column("ordered_quantity")]
        public int OrderedQuantity { set; get; }

        [Column("adap_quantity")]
        public int AdapQuantity { set; get; }

        [Column("deliveried_quantity")]
        public int DeliveriedQuantity { set; get; }

        [Column("uom_id")]
        public int UoMId { set; get; }
    }
}
