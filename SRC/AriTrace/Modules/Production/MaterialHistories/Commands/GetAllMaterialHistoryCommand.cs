using Common.Exceptions;
using MDM.UI.Companies.Interfaces;
using Production.UI.MaterialHistories.Interfaces;
using Production.UI.MaterialHistories.Models;
using Production.UI.MaterialHistories.ViewModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Production.MaterialHistories.Commands
{
    public class GetAllMaterialHistoryCommand : BaseCommand<IEnumerable<MaterialHistoryInfomation>>
    {
        public int Model { get; set; }

        public GetAllMaterialHistoryCommand(int materialId)
        {
            Model = materialId;
        }
    }

    public class GetAllMaterialHistoryCommandHandler : BaseCommandHandler<GetAllMaterialHistoryCommand, IEnumerable<MaterialHistoryInfomation>>
    {
        private readonly IMaterialHistoryQueries _materialHistoryQueries = null;

        private readonly ICompanyQueries _companyQueries = null;

        public GetAllMaterialHistoryCommandHandler(IMaterialHistoryQueries materialHistoryQueries, ICompanyQueries companyQueries)
        {
            this._materialHistoryQueries = materialHistoryQueries;
            this._companyQueries = companyQueries;
        }

        public override async Task<IEnumerable<MaterialHistoryInfomation>> HandleCommand(GetAllMaterialHistoryCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            var lst = (await _materialHistoryQueries.GetAllAsync(request.Model));

            return lst;
        }
    }
}
