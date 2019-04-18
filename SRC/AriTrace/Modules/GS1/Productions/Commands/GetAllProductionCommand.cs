using Common.Exceptions;
using GS1.UI.GTINs.Interfaces;
using GS1.UI.Productions.Interfaces;
using GS1.UI.Productions.Models;
using MDM.UI.Companies.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace GS1.Productions.Commands
{
    public class GetAllProductionCommand : BaseCommand<IEnumerable<Production>>
    {
        public GetAllProductionCommand()
        {
        }
    }

    public class GetAllProductionCommandHandler : BaseCommandHandler<GetAllProductionCommand, IEnumerable<Production>>
    {
        private readonly IProductionRepository productionRepository = null;
        private readonly IProductionQueries productionQueries = null;

        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        private readonly IGTINService gTINService = null;

        public GetAllProductionCommandHandler(IProductionRepository productionRepository, IProductionQueries productionQueries,
                                            ICompanyRepository companyRepository, ICompanyQueries companyQueries,
                                            IGTINService gTINService)
        {
            this.productionRepository = productionRepository;
            this.productionQueries = productionQueries;

            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;

            this.gTINService = gTINService;
        }

        public override async Task<IEnumerable<Production>> HandleCommand(GetAllProductionCommand request, CancellationToken cancellationToken)
        {
            var company = await companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            var lst = (await productionQueries.GetAllAsync(company?.Id)).ToList();

            foreach (var item in lst)
            {
                if (item.GTIN != null)
                    item.GTIN.Code = await gTINService.GetCodeGTINAsync(item.GTIN);
            }

            return lst;
        }
    }
}
