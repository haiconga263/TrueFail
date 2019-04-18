using MDM.UI.Distributions.ViewModels;
using Web.Controllers;

namespace Distributions.Commands.Manager
{
    public class UpdateCommand : BaseCommand<int>
    {
        public DistributionViewModel Distribution { set; get; }
        public UpdateCommand(DistributionViewModel distribution)
        {
            Distribution = distribution;
        }
    }
}
