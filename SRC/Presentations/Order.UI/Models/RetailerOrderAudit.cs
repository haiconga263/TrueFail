using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Order.UI.Models
{
    [Table("retailer_order_audit")]
    public class RetailerOrderAudit
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
        public long? RetailerOrderItemId { set; get; }

        [Column("status_id")]
        public int StatusId { set; get; }

        [Column("note")]
        public string Note { set; get; }

        [Column("created_date")]
        public DateTime CreatedDate { set; get; }

        [Column("created_by")]
        public int CreatedBy { set; get; }
    }
}
