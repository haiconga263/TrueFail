using Common.Exceptions;
using Common.Extensions;
using Common.Models;
using System.Threading;
using System.Threading.Tasks;
using Users.UI.Interfaces;
using Web.Controllers;

namespace Users.Commands
{
    public class UpdateUserCommandHandler : BaseCommandHandler<UpdateUserCommand, int>
    {
        private readonly IUserRepository userRepository = null;
        private readonly IUserQueries userQueries = null;
        public UpdateUserCommandHandler(IUserRepository userRepository, IUserQueries userQueries)
        {
            this.userRepository = userRepository;
            this.userQueries = userQueries;
        }
        public override async Task<int> HandleCommand(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userQueries.GetUserWithEmail(request.User.Email);
            if (user != null && user.Id != request.User.Id)
            {
                throw new BusinessException("ExistedEmail");
            }
            return await userRepository.UpdateUser(request.User);
        }
    }
}
