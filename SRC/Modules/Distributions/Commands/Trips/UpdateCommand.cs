using Distributions.UI.ViewModels;
using MDM.UI.Distributions.ViewModels;
using Web.Controllers;

namespace Distributions.Commands.Trips
{
    public class UpdateCommand : BaseCommand<int>
    {
        public TripViewModel Trip { set; get; }
        public UpdateCommand(TripViewModel trip)
        {
            Trip = trip;
        }
    }
}
