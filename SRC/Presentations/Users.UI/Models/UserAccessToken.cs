using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Users.UI.Models
{
    [Table("user_access_token")]
    public class UserAccessToken
    {
        [Key]
        [Identity]
        [Column("id")]
        public long Id { set; get; }

        [Column("user_id")]
        public int UserId { set; get; }

        [Column("access_token")]
        public string AccessToken { set; get; }

        [Column("login_date")]
        public DateTime LoginDate { set; get; }

        [Column("expired_date")]
        public string ExpiredDate { set; get; }
    }
}
