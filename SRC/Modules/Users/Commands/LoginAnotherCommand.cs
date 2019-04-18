using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Users.Commands
{
    public class LoginAnotherCommand : BaseCommand<UserSession>
    {
        public int UserId { set; get; }
        public LoginAnotherCommand(int userId)
        {
            UserId = userId;
        }
    }
}
