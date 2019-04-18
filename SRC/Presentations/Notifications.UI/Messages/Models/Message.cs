using Notifications.UI.Messages.Enumerations;
using Common.Attributes;
using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Notifications.UI.Messages.Models
{
    [Table(MessageConfigs.TABLE_NAME)]
    public class Message : BaseModel
    {
        [Key]
        [Identity]
        [Column("id")]
        public long Id { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("topic")]
        public string Topic { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [Column("method")]
        public MessageMethods Method { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("body")]
        public string Body { get; set; }

        [Column("status")]
        public MessageStatus Status { get; set; }

        [Column("scheduled_time")]
        public DateTime? ScheduledTime { get; set; }
    }
}
