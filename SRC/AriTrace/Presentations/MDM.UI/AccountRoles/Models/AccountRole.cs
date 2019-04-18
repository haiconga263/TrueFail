using MDM.UI.Enumerates;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.AccountRoles.Models
{
    [Table("user_account_role")]
    public class AccountRole
    {
        [Key]
        [Column("id")]
        public int Id { set; get; }

        [Column("user_account_id")]
        public int UserAccountId { set; get; }

        [Column("role_id")]
        public int RoleId { set; get; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
    }
}
