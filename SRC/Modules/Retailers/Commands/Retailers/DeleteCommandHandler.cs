using MDM.UI.Farmers.Interfaces;
using MDM.UI.Farmers.Models;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using MDM.UI.Retailers.Interfaces;
using MDM.UI.Retailers.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Controls;

namespace Retailers.Commands.Commands.Retailers
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly IRetailerRepository retailerRepository = null;
        public DeleteCommandHandler(IRetailerRepository retailerRepository)
        {
            this.retailerRepository = retailerRepository;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            return await retailerRepository.Delete(DeleteBuild(new Retailer()
            {
                Id = request.RetailerId
            }, request.LoginSession));
        }
    }
}
