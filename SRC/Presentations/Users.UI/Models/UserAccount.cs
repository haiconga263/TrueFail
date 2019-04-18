using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Users.UI.Models
{
    [Table("user_account")]
    public class UserAccount
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }

        [Column("password")]
        [JsonIgnore]
        public string Password { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("security_password")]
        public Guid SecurityPassword { get; set; }

        [Column("password_reset_code")]
        public string PasswordResetCode { get; set; }

        [Column("is_external_user")]
        public bool IsExternalUser { get; set; }

        [Column("is_superadmin")]
        public bool IsSuperAdmin { get; set; }

        [JsonIgnore]
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("is_actived")]
        public bool IsActived { get; set; }

        [Column("is_used")]
        public bool IsUsed { get; set; }

        [Column("created_by")]
        public int CreatedBy { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
    }
}
