using Common.Exceptions;
using Common.Extensions;
using Common.Helpers;
using Common.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Users.UI.Models;
using Web.Controllers;
using Web.Helpers;

namespace Users.Commands
{
    public class LoginCommandHandler : BaseCommandHandler<LoginCommand, UserSession>
    {
        private readonly IUserRepository userRepository = null;
        private readonly IUserQueries userQueries = null;
        public LoginCommandHandler(IUserRepository userRepository, IUserQueries userQueries)
        {
            this.userRepository = userRepository;
            this.userQueries = userQueries;
        }
        public override async Task<UserSession> HandleCommand(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userQueries.GetUserWithRole(request.UserName);

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

            //check password
            if (!(request.Password.Trim() + user.SecurityPassword).CalculateMD5Hash().Equals(user.Password))
            {
                throw new BusinessException("WrongPasswordAccount");
            }

            var token = SessionHelper.CreateAccessToken(user.UserName, request.IsRememberMe);
            var userSession = await userRepository.Login(user, token);

            try
            {
                var deviceId = await userRepository.InsertDevice(new UserAccountDevice()
                {
                    DeviceInfo = request.DeviceInfo,
                    MessagingToken = request.MessagingToken,
                    Name = request.AppName,
                    TokenId = userSession.SessionId,
                    UserId = userSession.Id,
                });
            }
            catch (Exception ex)
            {
                LogHelper.GetLogger().Warn($"Inserting User's Device is fail. SessionId: {userSession.SessionId}, UserId: {userSession.Id}", ex);
            }
            return userSession;
        }
    }
}
