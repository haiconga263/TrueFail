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
    public class ActiveUserPasswordCommandHandler : BaseCommandHandler<ActiveUserPasswordCommand, int>
    {
        private readonly IUserRepository userRepository = null;
        private readonly IUserQueries userQueries = null;
        public ActiveUserPasswordCommandHandler(IUserRepository userRepository, IUserQueries userQueries)
        {
            this.userRepository = userRepository;
            this.userQueries = userQueries;
        }
        public override async Task<int> HandleCommand(ActiveUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userQueries.GetUser(request.UserName);
            if(user.IsActived)
            {
                throw new BusinessException("ActivedAccount");
            }
            if (user != null)
            {
                return await userRepository.ActiveUser(user.Id, (request.Password + user.SecurityPassword.ToString()).CalculateMD5Hash());
            }
            throw new BusinessException("NotExistedAccount");
        }
    }
}
