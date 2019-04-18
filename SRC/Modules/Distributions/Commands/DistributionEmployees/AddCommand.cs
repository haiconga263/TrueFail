using MDM.UI.Distributions.Models;
using MDM.UI.Distributions.ViewModels;
using Web.Controllers;

namespace Distributions.Commands.DistributionEmployees
{
    public class AddCommand : BaseCommand<int>
    {
        public DistributionEmployee Employee { set; get; }
        public AddCommand(DistributionEmployee employee)
        {
            Employee = employee;
        }
    }
}
