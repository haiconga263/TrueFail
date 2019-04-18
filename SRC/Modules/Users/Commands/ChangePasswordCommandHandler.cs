using Common.Exceptions;
using Common.Extensions;
using Common.Models;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Web.Controllers;

namespace Users.Commands
{
    public class ChangePasswordCommandHandler : BaseCommandHandler<ChangePasswordCommand, int>
    {
        private readonly IUserRepository userRepository = null;
        private readonly IUserQueries userQueries = null;
        public ChangePasswordCommandHandler(IUserRepository userRepository, IUserQueries userQueries)
        {
            this.userRepository = userRepository;
            this.userQueries = userQueries;
        }
        public override async Task<int> HandleCommand(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userQueries.GetUser(request.LoginSession.UserName);
            if (user == null)
            {
                throw new BusinessException("NotExistedAccount");
            }

            if (user.Password != (request.OldPassword.Trim() + user.SecurityPassword.ToString()).CalculateMD5Hash())
            {
                throw new BusinessException("WrongOldPasswordAccount");
            }

            return await userRepository.ChangePassword(request.LoginSession.Id, (request.NewPassword.Trim() + user.SecurityPassword.ToString()).CalculateMD5Hash());
        }
    }
}
