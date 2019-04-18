using Common.Extensions;
using Common.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Web.Controllers;
using Web.Controls;

namespace Users.Commands
{
    public class RemoveUserCommandHandler : BaseCommandHandler<RemoveUserCommand, int>
    {
        private readonly IUserRepository userRepository = null;
        public RemoveUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public override async Task<int> HandleCommand(RemoveUserCommand request, CancellationToken cancellationToken)
        {
             return await userRepository.RemoveUser(request.UserId);
        }
    }
}
