using Common.Attributes;
using Common.Models;
using Productions.UI.Cultivations.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Productions.UI.Cultivations.Models
{
    [Table(Cultivation.TABLENAME)]
    public class Cultivation : BaseModel
    {
        public const string TABLENAME = "farm_cultivation";

        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("description")]
        public string Description { set; get; }

        [Column("seed_id")]
        public int SeedId { set; get; }

        [Column("method_id")]
        public int? MethodId { set; get; }

        [Column("plot_id")]
        public int PlotId { set; get; }

        [Column("seeding_date")]
        public DateTime? SeedingDate { set; get; }

        [Column("status")]
        public CultivationStatus Status { set; get; }

        [Column("expected_harvest_date")]
        public DateTime? ExpectedHarvestDate { set; get; }

        [Column("longitude")]
        public double Longitude { set; get; }

        [Column("latitude")]
        public double Latitude { set; get; }

    }
}
