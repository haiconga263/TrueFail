using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Distributions.UI.Models
{
    [Table("trip_audit")]
    public class TripAudit
    {
        [Key]
        [Identity]
        [Column("id")]
        public long Id { set; get; }

        [Column("trip_id")]
        public int TripId { set; get; }

        [Column("status_id")]
        public int? StatusId { set; get; }

        [Column("longitude")]
        public double Longitude { set; get; }

        [Column("latitude")]
        public double Latitude { set; get; }

        [Column("created_date")]
        public DateTime CreatedDate { set; get; }

        [Column("created_by")]
        public int CreatedBy { set; get; }
    }
}
