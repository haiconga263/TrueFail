using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Controls;

namespace Users.Commands
{
    public class RemoveUserCommand : BaseCommand<int>
    {
        public int UserId { set; get; }
        public RemoveUserCommand(int userId)
        {
            UserId = userId;
        }
    }
}
