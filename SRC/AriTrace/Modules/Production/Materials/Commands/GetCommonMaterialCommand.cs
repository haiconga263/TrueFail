using Common.Exceptions;
using MDM.UI.Companies.Interfaces;
using Production.UI.Materials.Interfaces;
using Production.UI.Materials.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Production.Materials.Commands
{
    public class GetCommonMaterialCommand : BaseCommand<IEnumerable<Material>>
    {
        public GetCommonMaterialCommand()
        {
        }
    }

    public class GetCommonMaterialCommandHandler : BaseCommandHandler<GetCommonMaterialCommand, IEnumerable<Material>>
    {
        private readonly IMaterialRepository materialRepository = null;
        private readonly IMaterialQueries materialQueries = null;

        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        public GetCommonMaterialCommandHandler(IMaterialRepository materialRepository, IMaterialQueries materialQueries,
                                            ICompanyRepository companyRepository, ICompanyQueries companyQueries)
        {
            this.materialRepository = materialRepository;
            this.materialQueries = materialQueries;

            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;
        }

        public override async Task<IEnumerable<Material>> HandleCommand(GetCommonMaterialCommand request, CancellationToken cancellationToken)
        {
            var company = await companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            var lst = (await materialQueries.GetsAsync(company?.Id));

            return lst;
        }
    }
}
