using Distributions.UI.ViewModels;
using MDM.UI.Distributions.ViewModels;
using Web.Controllers;

namespace Distributions.Commands.Routers
{
    public class UpdateCommand : BaseCommand<int>
    {
        public RouterViewModel Router { set; get; }
        public UpdateCommand(RouterViewModel router)
        {
            Router = router;
        }
    }
}
