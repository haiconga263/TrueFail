using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Distributions.UI.Models
{
    [Table("trip")]
    public class Trip : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("router_id")]
        public int? RouterId { set; get; }

        [Column("distribution_id")]
        public int DistributionId { set; get; }

        [Column("status_id")]
        public short StatusId { set; get; }

        [Column("deliveryman_id")]
        public int? DeliveryManId { set; get; }

        [Column("driver_id")]
        public int? DriverId { set; get; }

        [Column("vehicle_id")]
        public int? VehicleId { set; get; }

        [Column("current_longitude")]
        public double? CurrentLongitude { set; get; }

        [Column("current_latitude")]
        public double? CurrentLatitude { set; get; }

        [Column("delivery_date")]
        public DateTime DeliveryDate { set; get; }
    }
}
