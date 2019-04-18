using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Productions.UI.Plots.Models
{
    [Table(Plot.TABLENAME)]
    public class Plot : BaseModel
    {
        public const string TABLENAME = "farm_plot";

        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("code")]
        public string Code { set; get; }

        [Column("description")]
        public string Description { set; get; }

        [Column("farmer_id")]
        public int FarmerId { set; get; }

        [Column("is_glass_house")]
        public bool IsGlassHouse { set; get; }

        [Column("area")]
        public double Area { set; get; }

        [Column("longitude")]
        public double? Longitude { set; get; }

        [Column("latitude")]
        public double? Latitude { set; get; }
    }
}
