using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Collections.Models
{
    [Table("collection_inventory_history")]
    public class CollectionInventoryHistory
    {
        [Key]
        [Identity]
        [Column("id")]
        public long Id { set; get; }

        [Column("trace_code")]
        public string TraceCode { set; get; }

        [Column("direction")]
        public short Direction { set; get; }

        [Column("collection_id")]
        public int CollectionId { set; get; }

        [Column("product_id")]
        public int ProductId { set; get; }

        [Column("uom_id")]
        public int UoMId { set; get; }

        [Column("quantity")]
        public long Quantity { set; get; }

        [Column("last_quantity")]
        public long LastQuantity { set; get; }

        [Column("reason")]
        public string Reason { set; get; }

        [Column("created_date")]
        public DateTime CreatedDate { set; get; }

        [Column("created_by")]
        public int CreatedBy { set; get; }
    }
}
