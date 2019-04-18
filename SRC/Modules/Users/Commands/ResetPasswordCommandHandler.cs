using Common.Exceptions;
using Common.Extensions;
using Common.Helpers;
using Common.Models;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Web.Controllers;

namespace Users.Commands
{
    public class ResetPasswordCommandHandler : BaseCommandHandler<ResetPasswordCommand, int>
    {
        private readonly IUserRepository userRepository = null;
        private readonly IUserQueries userQueries = null;
        public ResetPasswordCommandHandler(IUserRepository userRepository, IUserQueries userQueries)
        {
            this.userRepository = userRepository;
            this.userQueries = userQueries;
        }
        public override async Task<int> HandleCommand(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userQueries.GetUser(request.UserName);

            if (user == null || user.PasswordResetCode == null || !user.IsUsed || !user.IsActived)
            {
                throw new BusinessException("NotEnoughConditionResetPassword");
            }

            if (user.PasswordResetCode != request.PinCode)
            {
                throw new BusinessException("WrongPinCodeAccount");
            }

            return await userRepository.ResetPassword(user.Id, (request.Password.Trim() + user.SecurityPassword.ToString()).CalculateMD5Hash());
        }
    }
}
