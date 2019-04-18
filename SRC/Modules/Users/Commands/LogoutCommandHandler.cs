using Common.Extensions;
using Common.Models;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Web.Controllers;

namespace Users.Commands
{
    public class LogoutCommandHandler : BaseCommandHandler<LogoutCommand, int>
    {
        private readonly IUserRepository userRepository = null;
        public LogoutCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public override async Task<int> HandleCommand(LogoutCommand request, CancellationToken cancellationToken)
        {
            await userRepository.Logout(request.LoginSession);
            return 0;
        }
    }
}
