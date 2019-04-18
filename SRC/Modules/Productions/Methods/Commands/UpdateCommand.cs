using Productions.UI.Methods.Models;
using Web.Controllers;

namespace Productions.Methods.Commands
{
    public class UpdateCommand : BaseCommand<int>
    {
        public Method Method { set; get; }
        public UpdateCommand(Method method)
        {
            Method = method;
        }
    }
}
