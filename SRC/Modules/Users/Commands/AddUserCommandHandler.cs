using Common.Attributes;
using Common.Exceptions;
using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Users.UI.Models;
using Web.Controllers;

namespace Users.Commands
{
    public class AddUserCommandHandler : BaseCommandHandler<AddUserCommand, int>
    {
        private readonly IUserRepository userRepository = null;
        private readonly IUserQueries userQueries = null;
        public AddUserCommandHandler(IUserRepository userRepository, IUserQueries userQueries)
        {
            this.userRepository = userRepository;
            this.userQueries = userQueries;
        }
        public override async Task<int> HandleCommand(AddUserCommand request, CancellationToken cancellationToken)
        {
            // Role is null that this action is made by the staff. But LoginSession must not null
            if(request.LoginSession == null && string.IsNullOrEmpty(request.Role))
            {
                throw new NotPermissionException();
            }

            //check existed username
            var user = await userQueries.GetUser(request.UserName);
            if (user != null)
            {
                throw new BusinessException("ExistedAccount");
            }

            //check existed email
            user = await userQueries.GetUserWithEmail(request.Email);
            if (user != null)
            {
                throw new BusinessException("ExistedEmail");
            }

            //Add user
            Guid securityPassword = Guid.NewGuid();
            var userId = await this.userRepository.AddUser(new UserAccount() {
                UserName = request.UserName,
                Password = (request.Password.Trim() + securityPassword.ToString()).CalculateMD5Hash(),
                Email = request.Email,
                SecurityPassword = securityPassword,
                IsExternalUser = (!string.IsNullOrEmpty(request.Role) ? true : false)
            }, request.LoginSession == null ? null : (int?)request.LoginSession.Id);

            // for who is't the staff
            if(userId > 0 && !string.IsNullOrEmpty(request.Role))
            {
                var role = await userQueries.GetRole(request.Role);
                await userRepository.SaveChangeRoles(userId, new List<int>
                {
                    role.Id
                });
            }

            //Send email for active here


            return userId > 0 ? 0 : -1;
        }
    }
}
