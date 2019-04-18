using Common.Exceptions;
using GS1.UI.GTINs.Interfaces;
using GS1.UI.Processes.Interfaces;
using GS1.UI.Processes.Models;
using GS1.UI.Processes.ViewModels;
using MDM.UI.Companies.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace GS1.Processes.Commands
{
    public class GetCommonProcessCommand : BaseCommand<IEnumerable<ProcessInformation>>
    {
        public GetCommonProcessCommand()
        {
        }
    }

    public class GetCommonProcessCommandHandler : BaseCommandHandler<GetCommonProcessCommand, IEnumerable<ProcessInformation>>
    {
        private readonly IProcessRepository processRepository = null;
        private readonly IProcessQueries processQueries = null;

        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        private readonly IGTINService gTINService = null;

        public GetCommonProcessCommandHandler(IProcessRepository processRepository, IProcessQueries processQueries,
                                            ICompanyRepository companyRepository, ICompanyQueries companyQueries,
                                            IGTINService gTINService)
        {
            this.processRepository = processRepository;
            this.processQueries = processQueries;

            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;

            this.gTINService = gTINService;
        }

        public override async Task<IEnumerable<ProcessInformation>> HandleCommand(GetCommonProcessCommand request, CancellationToken cancellationToken)
        {
            var company = await companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            var lst = (await processQueries.GetsAsync(company?.Id)).ToList();

            foreach (var item in lst)
            {
                if (item.Production?.GTIN != null)
                    item.Production.GTIN.Code = await gTINService.GetCodeGTINAsync(item.Production.GTIN);
            }

            return lst;
        }
    }
}
