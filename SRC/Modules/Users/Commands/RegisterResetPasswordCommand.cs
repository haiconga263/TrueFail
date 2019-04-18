using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Users.Commands
{
    public class RegisterResetPasswordCommand : BaseCommand<int>
    {
        public string UserName { set; get; }
        public RegisterResetPasswordCommand(string userName)
        {
            UserName = userName;
        }
    }
}
