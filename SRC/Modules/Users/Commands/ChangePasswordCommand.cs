using Web.Controllers;

namespace Users.Commands
{
    public class ChangePasswordCommand : BaseCommand<int>
    {
        public string OldPassword { set; get; }
        public string NewPassword { set; get; }

        public ChangePasswordCommand(string oldPassword, string newPassword)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
    }
}
