using MDM.UI.Distributions.Models;
using MDM.UI.Distributions.ViewModels;
using Web.Controllers;

namespace Distributions.Commands.DistributionEmployees
{
    public class UpdateCommand : BaseCommand<int>
    {
        public DistributionEmployee Employee { set; get; }
        public UpdateCommand(DistributionEmployee employee)
        {
            Employee = employee;
        }
    }
}
