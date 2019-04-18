using Distributions.UI.Interfaces;
using Distributions.UI.Models;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Distributions.Models;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Distributions.Commands.Routers
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IRouterRepository routerRepository = null;
        public DeleteCommandHandler(IRouterRepository routerRepository)
        {
            this.routerRepository = routerRepository;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            return await routerRepository.Delete(UpdateBuild(new Router()
            {
                Id = request.RouterId
            }, request.LoginSession));
        }
    }
}
