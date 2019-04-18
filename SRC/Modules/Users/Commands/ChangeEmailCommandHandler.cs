using Common.Exceptions;
using Common.Extensions;
using Common.Models;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Web.Controllers;

namespace Users.Commands
{
    public class ChangeEmailCommandHandler : BaseCommandHandler<ChangeEmailCommand, int>
    {
        private readonly IUserRepository userRepository = null;
        private readonly IUserQueries userQueries = null;
        public ChangeEmailCommandHandler(IUserRepository userRepository, IUserQueries userQueries)
        {
            this.userRepository = userRepository;
            this.userQueries = userQueries;
        }
        public override async Task<int> HandleCommand(ChangeEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await userQueries.GetUserWithEmail(request.Email);
            if (user != null && user.Id != request.LoginSession.Id)
            {
                throw new BusinessException("ExistedEmail");
            }
            return await userRepository.ChangeEmail(request.LoginSession.Id, request.Email);
        }
    }
}
