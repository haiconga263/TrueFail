using Common.Exceptions;
using GS1.UI.GTINs.Interfaces;
using GS1.UI.GTINs.Mappings;
using GS1.UI.GTINs.Models;
using MDM.UI.Companies.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace GS1.GTINs.Commands
{
    public class CalculateCheckDigitGTINCommand : BaseCommand<GTIN>
    {
        public GTIN Model { get; set; }
        public CalculateCheckDigitGTINCommand(GTIN gTIN)
        {
            Model = gTIN;
        }
    }

    public class CalculateCheckDigitGTINCommandHandler : BaseCommandHandler<CalculateCheckDigitGTINCommand, GTIN>
    {
        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        private readonly IGTINService gTINService = null;

        public CalculateCheckDigitGTINCommandHandler(ICompanyRepository companyRepository, ICompanyQueries companyQueries,
                                            IGTINService gTINService)
        {
            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;

            this.gTINService = gTINService;
        }
        public override async Task<GTIN> HandleCommand(CalculateCheckDigitGTINCommand request, CancellationToken cancellationToken)
        {
            var company = await companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            var gTIN = (await gTINService.CalculateCheckDigitAsync(request.Model)).ToInformation();

            gTIN.Code = await gTINService.GetCodeGTINAsync(gTIN);
            return gTIN;
        }
    }
}
