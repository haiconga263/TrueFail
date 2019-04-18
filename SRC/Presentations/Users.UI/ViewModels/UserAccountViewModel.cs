using System;
using System.Collections.Generic;
using System.Text;
using Users.UI.Models;

namespace Users.UI.ViewModels
{
    public class UserAccountViewModel : UserAccount
    {
        public List<Role> Roles { set; get; } = new List<Role>();
    }
}
