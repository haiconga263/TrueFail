using Common.Extensions;
using Common.Helpers;
using Common.Models;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Web.Controllers;

namespace Users.Commands
{
    public class RegisterResetPasswordCommandHandler : BaseCommandHandler<RegisterResetPasswordCommand, int>
    {
        private readonly IUserRepository userRepository = null;
        public RegisterResetPasswordCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public override async Task<int> HandleCommand(RegisterResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var pinCode = CommonHelper.GenerateRandomString(8, 2);
            var rs = await userRepository.RegisterResetPassword(request.UserName, pinCode);

            //Send notify by email here

            return rs;
        }
    }
}
