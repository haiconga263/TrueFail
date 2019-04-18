using Common.Attributes;
using Common.Exceptions;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Users.UI;
using Users.UI.Interfaces;
using Users.UI.Models;
using Web.Controllers;

namespace Users.Commands
{
    public class ResetPasswordByAdminCommandHandler : BaseCommandHandler<ResetPasswordByAdminCommand, int>
    {
        private readonly IUserRepository userRepository = null;
        private readonly IUserQueries userQueries = null;
        public ResetPasswordByAdminCommandHandler(IUserRepository userRepository, IUserQueries userQueries)
        {
            this.userRepository = userRepository;
            this.userQueries = userQueries;
        }
        public override async Task<int> HandleCommand(ResetPasswordByAdminCommand request, CancellationToken cancellationToken)
        {
            //check existed username
            var user = await userQueries.GetUser(request.UserId);
            if (user == null)
            {
                throw new BusinessException("ExistedAccount");
            }

            return await userRepository.ChangePassword(request.UserId, (Const.DefaultResetPassword + user.SecurityPassword.ToString()).CalculateMD5Hash());
        }
    }
}
