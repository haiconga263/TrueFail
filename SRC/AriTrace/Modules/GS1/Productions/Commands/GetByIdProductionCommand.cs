using Common.Exceptions;
using GS1.UI.GTINs.Interfaces;
using GS1.UI.Productions.Interfaces;
using GS1.UI.Productions.Models;
using MDM.UI.Companies.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace GS1.Productions.Commands
{
    public class GetByIdProductionCommand : BaseCommand<Production>
    {
        public int Model { get; set; }

        public GetByIdProductionCommand(int id)
        {
            Model = id;
        }
    }

    public class GetByIdProductionCommandHandler : BaseCommandHandler<GetByIdProductionCommand, Production>
    {
        private readonly IProductionRepository productionRepository = null;
        private readonly IProductionQueries productionQueries = null;

        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        private readonly IGTINService gTINService = null;

        public GetByIdProductionCommandHandler(IProductionRepository productionRepository, IProductionQueries productionQueries,
                                            ICompanyRepository companyRepository, ICompanyQueries companyQueries,
                                            IGTINService gTINService)
        {
            this.productionRepository = productionRepository;
            this.productionQueries = productionQueries;

            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;

            this.gTINService = gTINService;
        }

        public override async Task<Production> HandleCommand(GetByIdProductionCommand request, CancellationToken cancellationToken)
        {
            var company = await companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            var rs = await productionQueries.GetByIdAsync(request.Model);

            if (rs == null)
                throw new BusinessException("Production.NotExist");
            if (rs.GTIN != null)
                rs.GTIN.Code = await gTINService.GetCodeGTINAsync(rs.GTIN);
            return rs;
        }
    }
}
