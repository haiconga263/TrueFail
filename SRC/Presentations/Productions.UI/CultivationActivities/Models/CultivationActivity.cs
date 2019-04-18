using Common.Attributes;
using Common.Models;
using Productions.UI.CultivationActivities.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Productions.UI.CultivationActivities.Models
{
    [Table(CultivationActivity.TABLENAME)]
    public class CultivationActivity : BaseModel
    {
        public const string TABLENAME = "farm_cultivation_activity";

        [Key]
        [Identity]
        [Column("id")]
        public int Id { set; get; }

        [Column("action_type")]
        public ActionTypes ActionType { set; get; }

        [Column("image_url")]
        public string ImageUrl { set; get; }

        [Column("description")]
        public string Description { set; get; }

        [Column("plot_id")]
        public int PlotId { set; get; }

        [Column("farmer_id")]
        public int? FarmerId { set; get; }

        [Column("other")]
        public string Other { set; get; }

    }
}
