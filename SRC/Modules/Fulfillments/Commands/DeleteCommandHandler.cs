using MDM.UI.Fulfillments.Interfaces;
using MDM.UI.Fulfillments.Models;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Fulfillments.Commands
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IFulfillmentRepository fulfillmentRepository = null;
        public DeleteCommandHandler(IFulfillmentRepository fulfillmentRepository)
        {
            this.fulfillmentRepository = fulfillmentRepository;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            return await fulfillmentRepository.Delete(UpdateBuild(new Fulfillment()
            {
                Id = request.FulfillmentId
            }, request.LoginSession));
        }
    }
}
