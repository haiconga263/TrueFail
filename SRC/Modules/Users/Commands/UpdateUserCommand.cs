using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Users.UI.Models;
using Users.UI.ViewModels;
using Web.Controllers;

namespace Users.Commands
{
    public class UpdateUserCommand : BaseCommand<int>
    {
        public UserAccountViewModel User { set; get; }
        public UpdateUserCommand(UserAccountViewModel user)
        {
            User = user;
        }
    }
}
