using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Order.UI.Models
{  
    [Table("farmer_buying_calendar_item")]
    public class FarmerBuyingCalendarItem
    {
        [Key]
        [Identity]
        [Column("id")]
        public long Id { set; get; }

        [Column("farmer_buying_calendar_id")]
        public long FarmerBuyingCalendarId { set; get; }

        [Column("product_id")]
        public int ProductId { set; get; }

        [Column("quantity")]
        public int Quantity { set; get; }

        [Column("adap_quantity")]
        public int AdapQuantity { set; get; }

        [Column("uom_id")]
        public int UoMId { set; get; }

        [Column("adap_note")]
        public string AdapNote { set; get; }
    }
}
