using MDM.UI.Accounts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MDM.UI.Accounts.ViewModels
{
    public class AccountSingleRole : Account
    {
        [Column("role_id")]
        public int? RoleId { get; set; }

        public string NewPassword { get; set; }
    }
}
