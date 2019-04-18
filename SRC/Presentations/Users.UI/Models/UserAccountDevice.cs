using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Users.UI.Models
{
    [Table("user_account_device")]
    public class UserAccountDevice
    {
        [Key]
        [Identity]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { set; get; }

        [Column("device_info")]
        public string DeviceInfo { set; get; }

        [Column("user_id")]
        public int UserId { set; get; }

        [Column("token_id")]
        public long TokenId { set; get; }

        [Column("messaging_token")]
        public string MessagingToken { set; get; }

    }
}
