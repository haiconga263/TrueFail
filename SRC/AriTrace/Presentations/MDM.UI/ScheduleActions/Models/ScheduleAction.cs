using MDM.UI.ScheduleActions.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.ScheduleActions.Models
{
    [Table("schedule_action")]
    public class ScheduleAction
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("action_type")]
        public ActionTypes ActionType { get; set; }

        [Column("data")]
        public string Data { get; set; }

        [Column("action_result")]
        public ActionResults ActionResult { get; set; }

        [Column("down_count")]
        public int DownCount { get; set; }

        [Column("message")]
        public string message { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("created_by")]
        public int CreatedBy { get; set; }
    }
}
