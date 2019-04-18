using MDM.UI.Collections.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Collections.Commands.CollectionEmployees
{
    public class DeleteCommandHandler : BaseCommandHandler<DeleteCommand, int>
    {
        private readonly ICollectionEmployeeRepository collectionEmployeeRepository = null;
        public DeleteCommandHandler(ICollectionEmployeeRepository collectionEmployeeRepository)
        {
            this.collectionEmployeeRepository = collectionEmployeeRepository;
        }
        public override async Task<int> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
        {
            return await collectionEmployeeRepository.Delete(request.EmployeeId);
        }
    }
}
