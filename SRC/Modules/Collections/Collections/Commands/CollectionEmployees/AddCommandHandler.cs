using Common.Exceptions;
using MDM.UI.Collections.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Collections.Commands.CollectionEmployees
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly ICollectionEmployeeRepository collectionEmployeeRepository = null;
        private readonly ICollectionEmployeeQueries collectionEmployeeQueries = null;
        public AddCommandHandler(ICollectionEmployeeRepository collectionEmployeeRepository, ICollectionEmployeeQueries collectionEmployeeQueries)
        {
            this.collectionEmployeeRepository = collectionEmployeeRepository;
            this.collectionEmployeeQueries = collectionEmployeeQueries;
        }
        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {

            if (request.Employee == null || request.Employee.CollectionId == 0 || request.Employee.EmployeeId == 0)
            {
                throw new BusinessException("AddWrongInformation");
            }

            var checkEmployee = (await collectionEmployeeQueries.Gets($"collection_id = {request.Employee.CollectionId} and employee_id = {request.Employee.EmployeeId}")).FirstOrDefault();
            if(checkEmployee != null)
            {
                throw new BusinessException("Employee.NotExisted");
            }

            return await collectionEmployeeRepository.Add(request.Employee) > 0 ? 0 : throw new BusinessException("Common.AddFail");
        }
    }
}
