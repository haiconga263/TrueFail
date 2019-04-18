using Productions.UI.CultivationActivities.Models;
using Web.Controllers;

namespace Productions.CultivationActivities.Commands
{
    public class AddCommand : BaseCommand<int>
    {
        public CultivationActivity CultivationActivity { set; get; }
        public AddCommand(CultivationActivity cultivationActivity)
        {
            CultivationActivity = cultivationActivity;
        }
    }
}
