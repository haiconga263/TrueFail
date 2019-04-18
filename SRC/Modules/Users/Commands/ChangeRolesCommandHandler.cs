using Common.Exceptions;
using Common.Extensions;
using Common.Models;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Web.Controllers;

namespace Users.Commands
{
    public class ChangeRolesCommandHandler : BaseCommandHandler<ChangeRolesCommand, int>
    {
        private readonly IUserRepository userRepository = null;
        private readonly IUserQueries userQueries = null;
        public ChangeRolesCommandHandler(IUserRepository userRepository, IUserQueries userQueries)
        {
            this.userRepository = userRepository;
            this.userQueries = userQueries;
        }
        public override async Task<int> HandleCommand(ChangeRolesCommand request, CancellationToken cancellationToken)
        {
            var user = await userQueries.GetUser(request.UserId);
            if(user == null)
            {
                throw new BusinessException("NotExistedAccount");
            }
            if(user.IsExternalUser)
            {
                throw new BusinessException("ExternalAccount");
            }
            return await userRepository.SaveChangeRoles(request.UserId, request.RoleIds);
        }
    }
}
