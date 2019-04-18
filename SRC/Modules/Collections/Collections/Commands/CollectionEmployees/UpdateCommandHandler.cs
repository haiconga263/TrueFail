using Common.Exceptions;
using MDM.UI.Collections.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Collections.Commands.CollectionEmployees
{
    public class UpdateCommandHandler : BaseCommandHandler<UpdateCommand, int>
    {
        private readonly ICollectionEmployeeRepository collectionEmployeeRepository = null;
        private readonly ICollectionEmployeeQueries collectionEmployeeQueries = null;
        public UpdateCommandHandler(ICollectionEmployeeRepository collectionEmployeeRepository, ICollectionEmployeeQueries collectionEmployeeQueries)
        {
            this.collectionEmployeeRepository = collectionEmployeeRepository;
            this.collectionEmployeeQueries = collectionEmployeeQueries;
        }
        public override async Task<int> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
        {
            if (request.Employee == null || request.Employee.Id == 0 || request.Employee.EmployeeId == 0)
            {
                throw new BusinessException("AddWrongInformation");
            }

            var collectionEmployee = (await collectionEmployeeQueries.Gets($"id = {request.Employee.Id}")).FirstOrDefault();
            if(collectionEmployee != null)
            {
                collectionEmployee.EmployeeId = request.Employee.EmployeeId;
                var rs = await collectionEmployeeRepository.Update(collectionEmployee);
                return rs == 0 ? 0 : throw new BusinessException("Common.UpdateFail");
            }

            // CollectionEmployee isn't exsited. Of course, we don't need update it.
            return 0;
        }
    }
}
