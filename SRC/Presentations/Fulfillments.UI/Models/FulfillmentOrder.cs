using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fulfillments.UI.Models
{
    [Table("fulfillment_order")]
    public class FulfillmentOrder
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("fulfillment_id")]
        public int FulfillmentId { get; set; }

        [Column("retailer_order_id")]
        public int RetailerOrderId { get; set; }

        [Column("team_id")]
        public int TeamId { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("date_input")]
        public DateTime DateInput { get; set; }

        [Column("date_ship")]
        public DateTime DateShip { get; set; }
    }
}
