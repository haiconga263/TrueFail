using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Users.UI.Models;

namespace Users.Commands
{
    public class ResetPasswordByAdminCommand : BaseCommand<int>
    {
        public int UserId { set; get; }
        public ResetPasswordByAdminCommand(int userId)
        {
            UserId = userId;
        }
    }
}
