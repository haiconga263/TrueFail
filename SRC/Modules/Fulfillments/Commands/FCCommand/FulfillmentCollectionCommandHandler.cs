using Common.Exceptions;
using DAL;
using Fulfillments.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Fulfillments.Commands.FCCommand
{
    public class FulfillmentCollectionCommandHandler : BaseCommandHandler<FulfillmentCollectionCommand, int>
    {
        private readonly IFulfillmentCollectionRepository fulfillmentRepository = null;

        public FulfillmentCollectionCommandHandler(IFulfillmentCollectionRepository fulfillmentRepository)
        {
            this.fulfillmentRepository = fulfillmentRepository;
        }

        public override Task<int> HandleCommand(FulfillmentCollectionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
