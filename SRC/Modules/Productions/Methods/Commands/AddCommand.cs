using Productions.UI.Methods.Models;
using Web.Controllers;

namespace Productions.Methods.Commands
{
    public class AddCommand : BaseCommand<int>
    {
        public Method Method { set; get; }
        public AddCommand(Method method)
        {
            Method = method;
        }
    }
}
