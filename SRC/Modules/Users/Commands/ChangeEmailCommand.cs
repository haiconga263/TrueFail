using Common.Attributes;
using Web.Controllers;

namespace Users.Commands
{
    public class ChangeEmailCommand : BaseCommand<int>
    {
        public string Email { set; get; }
        public ChangeEmailCommand(string email)
        {
            Email = email;
        }
    }
}
