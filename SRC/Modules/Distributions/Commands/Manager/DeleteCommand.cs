using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Controls;

namespace Distributions.Commands.Manager
{
    public class DeleteCommand : BaseCommand<int>
    {
        public int DistributionId { set; get; }
        public DeleteCommand(int distributionId)
        {
            DistributionId = distributionId;
        }
    }
}
