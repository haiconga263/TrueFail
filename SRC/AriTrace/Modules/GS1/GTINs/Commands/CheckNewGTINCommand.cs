using Common.Exceptions;
using GS1.GTINs.Factories;
using GS1.UI.GTINs.Enumerations;
using GS1.UI.GTINs.Interfaces;
using GS1.UI.GTINs.Models;
using MDM.UI.Companies.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace GS1.GTINs.Commands
{
    public class CheckNewGTINCommand : BaseCommand<int>
    {
        public GTIN Model { get; set; }
        public CheckNewGTINCommand(GTIN gTIN)
        {
            Model = gTIN;
        }
    }

    public class CheckNewGTINCommandHandler : BaseCommandHandler<CheckNewGTINCommand, int>
    {
        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        private readonly IGTINService gTINService = null;

        public CheckNewGTINCommandHandler(ICompanyRepository companyRepository, ICompanyQueries companyQueries,
                                            IGTINService gTINService)
        {
            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;

            this.gTINService = gTINService;
        }
        public override async Task<int> HandleCommand(CheckNewGTINCommand request, CancellationToken cancellationToken)
        {
            var company = await companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            var rs = await gTINService.CheckNewGTINAsync(company.Id, request.Model, request.LoginSession);

            var ex = rs.GetException();
            if (ex != null)
                throw ex;
            return 0;
        }
    }
}
