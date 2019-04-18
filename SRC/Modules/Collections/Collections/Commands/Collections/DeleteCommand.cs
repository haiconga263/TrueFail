using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Controls;

namespace Collections.Commands.Collections
{
    public class DeleteCommand : BaseCommand<int>
    {
        public int CollectionId { set; get; }
        public DeleteCommand(int collectionId)
        {
            CollectionId = collectionId;
        }
    }
}
