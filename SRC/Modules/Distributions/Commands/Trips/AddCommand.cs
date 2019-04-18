using Distributions.UI.Models;
using Distributions.UI.ViewModels;
using MDM.UI.Distributions.ViewModels;
using Web.Controllers;

namespace Distributions.Commands.Trips
{
    public class AddCommand : BaseCommand<int>
    {
        public Trip Trip { set; get; }
        public AddCommand(Trip trip)
        {
            Trip = trip;
        }
    }
}
