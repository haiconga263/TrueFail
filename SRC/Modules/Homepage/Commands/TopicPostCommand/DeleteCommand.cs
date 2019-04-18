using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Controls;

namespace Homepage.Commands.TopicPostCommand
{
    public class DeleteCommand : BaseCommand<int>
    {
        public int PostId { set; get; }
        public DeleteCommand(int postId)
        {
            this.PostId = postId;
        }
    }
}
