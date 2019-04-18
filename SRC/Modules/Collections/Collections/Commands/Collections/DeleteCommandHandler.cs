using MDM.UI.Collections.Interfaces;
using MDM.UI.Collections.Models;
using MDM.UI.Employees.Interfaces;
using MDM.UI.Employees.Models;
using MDM.UI.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Controls;

namespace Collections.Commands.Collections
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly ICollectionRepository collectionRepository = null;
        public DeleteCommandHandler(ICollectionRepository collectionRepository)
        {
            this.collectionRepository = collectionRepository;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            return await collectionRepository.Delete(UpdateBuild(new Collection()
            {
                Id = request.CollectionId
            }, request.LoginSession));
        }
    }
}
