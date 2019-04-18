using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Distributions.UI.Models
{
    [Table("router")]
    public class Router : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("name")]
        public string Name { set; get; }

        [Column("description")]
        public string Description { set; get; }

        [Column("current_longitude")]
        public float CurrentLongitude { set; get; }

        [Column("current_latitude")]
        public float CurrentLatitude { set; get; }

        [Column("radius")]
        public float Radius { set; get; }

        [Column("distribution_id")]
        public int DistributionId { set; get; }

        [Column("country_id")]
        public int CountryId { set; get; }

        [Column("province_id")]
        public int ProvinceId { set; get; }

        [Column("is_used")]
        public bool IsUsed { set; get; }
    }
}
