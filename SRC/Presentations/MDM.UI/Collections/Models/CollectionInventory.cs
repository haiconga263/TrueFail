using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Collections.Models
{
    [Table("collection_inventory")]
    public class CollectionInventory
    {
        [Key]
        [Identity]
        [Column("id")]
        public long Id { set; get; }

        [Column("trace_code")]
        public string TraceCode { set; get; }

        [Column("collection_id")]
        public int CollectionId { set; get; }

        [Column("product_id")]
        public int ProductId { set; get; }

        [Column("uom_id")]
        public int UoMId { set; get; }

        [Column("quantity")]
        public long Quantity { set; get; }

        [Column("modified_date")]
        public DateTime ModifiedDate { set; get; }

        [Column("modified_by")]
        public int ModifiedBy { set; get; }
    }
}
