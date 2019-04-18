using Distributions.UI.ViewModels;
using MDM.UI.Distributions.ViewModels;
using Web.Controllers;

namespace Distributions.Commands.Routers
{
    public class AddCommand : BaseCommand<int>
    {
        public RouterViewModel Router { set; get; }
        public AddCommand(RouterViewModel router)
        {
            Router = router;
        }
    }
}
