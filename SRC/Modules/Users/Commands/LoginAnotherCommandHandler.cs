using Common.Exceptions;
using Common.Extensions;
using Common.Models;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Web.Controllers;
using Web.Helpers;

namespace Users.Commands
{
    public class LoginAnotherCommandHandler : BaseCommandHandler<LoginAnotherCommand, UserSession>
    {
        private readonly IUserRepository userRepository = null;
        private readonly IUserQueries userQueries = null;
        public LoginAnotherCommandHandler(IUserRepository userRepository, IUserQueries userQueries)
        {
            this.userRepository = userRepository;
            this.userQueries = userQueries;
        }
        public override async Task<UserSession> HandleCommand(LoginAnotherCommand request, CancellationToken cancellationToken)
        {
            if (!request.LoginSession.IsSuperAdmin)
            {
                throw new NotPermissionException();
            }

            var user = await userQueries.GetUserWithRole(request.UserId);
            if (user == null)
            {
                throw new BusinessException("NotExistedAccount");
            }

            if (!user.IsUsed)
            {
                throw new BusinessException("LockedAccount");
            }

            if (!user.IsActived)
            {
                throw new BusinessException("NotActivedAccount");
            }

            var token = SessionHelper.CreateAccessToken(user.UserName);
            return await userRepository.LoginAnotherUser(request.LoginSession.SessionId, user, token);
        }
    }
}
