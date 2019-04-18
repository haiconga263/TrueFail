using MDM.UI.Distributions.ViewModels;
using Web.Controllers;

namespace Distributions.Commands.Manager
{
    public class AddCommand : BaseCommand<int>
    {
        public DistributionViewModel Distribution { set; get; }
        public AddCommand(DistributionViewModel distribution)
        {
            Distribution = distribution;
        }
    }
}
