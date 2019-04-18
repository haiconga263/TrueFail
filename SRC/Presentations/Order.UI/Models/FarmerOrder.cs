using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Order.UI.Models
{
    [Table("farmer_order")]
    public class FarmerOrder : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public long Id { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("farmer_id")]
        public int FarmerId { set; get; }

        [Column("farmer_buying_calendar_id")]
        public long? FarmerBuyingCalendarId { set; get; }

        [Column("status_id")]
        public int StatusId { set; get; }

        [Column("buying_date")]
        public DateTime BuyingDate { set; get; }

        [Column("collection_id")]
        public int CollectionId { set; get; }

        [Column("ship_to")]
        public int ShipTo { set; get; }

        [Column("total_amount")]
        public decimal TotalAmount { set; get; }
    }
}
