using MDM.UI.Accounts.Models;
using MDM.UI.Accounts.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDM.UI.Accounts.Mappings
{
    public static class AccountMapping
    {
        public static AccountSingleRole ToSingleRole(this Account account)
        {
            var serializedParent = JsonConvert.SerializeObject(account);
            var accountSingleRole = JsonConvert.DeserializeObject<AccountSingleRole>(serializedParent);

            accountSingleRole.Password = account.Password;
            accountSingleRole.SecurityPassword = account.SecurityPassword;
            accountSingleRole.IsExternalUser = account.IsExternalUser;
            accountSingleRole.IsSuperAdmin = account.IsSuperAdmin;

            return accountSingleRole;
        }
    }
}
