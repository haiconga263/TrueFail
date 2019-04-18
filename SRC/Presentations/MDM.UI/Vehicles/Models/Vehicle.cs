using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Vehicles.Models
{
    [Table("vehicle")]
    public class Vehicle : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("org_code")]
        public string OrgCode { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("image_url")]
        public string ImageURL { set; get; }

        [Column("temperature_type")]
        public string TemperatureType { set; get; }

        [Column("zone_count")]
        public short ZoneCount { set; get; }

        [Column("start_time")]
        public string StartTime { set; get; }

        [Column("end_time")]
        public string EndTime { set; get; }

        [Column("start_lunch_time")]
        public string StartLunchTime { set; get; }

        [Column("end_lunch_time")]
        public string EndLunchTime { set; get; }

        [Column("speed")]
        public int Speed { set; get; }

        [Column("weight")]
        public int Weight { set; get; }

        [Column("vehicle_weight")]
        public int VehicleWeight { set; get; }

        [Column("capacity")]
        public int Capacity { set; get; }

        [Column("type_id")]
        public short TypeId { set; get; }

        [Column("is_used")]
        public bool IsUsed { set; get; }
    }
}
