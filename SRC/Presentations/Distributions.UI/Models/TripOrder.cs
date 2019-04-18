using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Distributions.UI.Models
{
    public class TripOrder
    {
        [Key]
        [Identity]
        [Column("id")]
        public long Id { set; get; }

        [Column("trip_id")]
        public int TripId { set; get; }

        [Column("order_id")]
        public int? OrderId { set; get; }

        [Column("is_completed")]
        public bool IsCompleted { set; get; }
    }
}
