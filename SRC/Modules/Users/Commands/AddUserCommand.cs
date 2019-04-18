using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Users.UI.Models;

namespace Users.Commands
{
    public class AddUserCommand : BaseCommand<int>
    {
        public string UserName { set; get; }
        public string Password { set; get; }
        public string Email { set; get; }
        public string Role { set; get; } //for customer or comsumer (is't a staff)
        public AddUserCommand(string userName, string password, string email)
        {
            UserName = userName;
            Password = password;
            Email = email;
        }
    }
}
