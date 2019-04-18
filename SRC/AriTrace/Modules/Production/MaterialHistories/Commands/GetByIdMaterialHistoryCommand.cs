using Common.Exceptions;
using MDM.UI.Companies.Interfaces;
using Production.UI.MaterialHistories.Interfaces;
using Production.UI.MaterialHistories.Models;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Production.MaterialHistories.Commands
{
    public class GetByIdMaterialHistoryCommand : BaseCommand<MaterialHistory>
    {
        public int Model { get; set; }

        public GetByIdMaterialHistoryCommand(int id)
        {
            Model = id;
        }
    }

    public class GetByIdMaterialHistoryCommandHandler : BaseCommandHandler<GetByIdMaterialHistoryCommand, MaterialHistory>
    {
        private readonly IMaterialHistoryRepository _materialHistoryRepository = null;
        private readonly IMaterialHistoryQueries _materialHistoryQueries = null;

        private readonly ICompanyRepository _companyRepository = null;
        private readonly ICompanyQueries _companyQueries = null;

        public GetByIdMaterialHistoryCommandHandler(IMaterialHistoryRepository materialHistoryRepository, IMaterialHistoryQueries materialHistoryQueries,
                                            ICompanyRepository companyRepository, ICompanyQueries companyQueries)
        {
            this._materialHistoryRepository = materialHistoryRepository;
            this._materialHistoryQueries = materialHistoryQueries;

            this._companyRepository = companyRepository;
            this._companyQueries = companyQueries;
        }

        public override async Task<MaterialHistory> HandleCommand(GetByIdMaterialHistoryCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            var rs = await _materialHistoryQueries.GetByIdAsync(request.Model);

            if (rs == null)
                throw new BusinessException("MaterialHistory.NotExist");
 
            return rs;
        }
    }
}
