using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Order.UI.Models
{
    [Table("retailer_order")]
    public class RetailerOrder : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public long Id { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("distribution_id_to")]
        public int? DistributionIdTo { set; get; }

        [Column("trip_id")]
        public int? TripId { set; get; }

        [Column("retailer_id")]
        public int RetailerId { set; get; }

        [Column("retailer_buying_calendar_id")]
        public long? RetailerBuyingCalendarId { set; get; }

        [Column("status_id")]
        public int StatusId { set; get; }

        [Column("buying_date")]
        public DateTime BuyingDate { set; get; }

        [Column("bill_to")]
        public int BillTo { set; get; }

        [Column("ship_to")]
        public int ShipTo { set; get; }

        [Column("total_amount")]
        public decimal TotalAmount { set; get; }
    }
}
