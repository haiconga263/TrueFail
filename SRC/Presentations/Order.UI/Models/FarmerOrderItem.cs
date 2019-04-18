using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Order.UI.Models
{ 

    [Table("farmer_order_item")]
    public class FarmerOrderItem
    {
        [Key]
        [Identity]
        [Column("id")]
        public long Id { set; get; }

        [Column("farmer_order_id")]
        public long FarmerOrderId { set; get; }

        [Column("product_id")]
        public int ProductId { set; get; }

        [Column("status_id")]
        public int StatusId { set; get; }

        [Column("price")]
        public decimal Price { set; get; }

        [Column("ordered_quantity")]
        public int OrderedQuantity { set; get; }

        [Column("deliveried_quantity")]
        public int DeliveriedQuantity { set; get; }

        [Column("uom_id")]
        public int UoMId { set; get; }
    }
}
