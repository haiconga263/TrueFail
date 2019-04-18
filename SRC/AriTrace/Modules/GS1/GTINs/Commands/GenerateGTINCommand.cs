using Common.Exceptions;
using GS1.UI.GTINs.Enumerations;
using GS1.UI.GTINs.Interfaces;
using GS1.UI.GTINs.Mappings;
using GS1.UI.GTINs.Models;
using GS1.UI.GTINs.ViewModels;
using MDM.UI.Companies.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace GS1.GTINs.Commands
{
    public class GenerateGTINCommand : BaseCommand<GTINInformation>
    {
        public GTINTypes Model { get; set; }
        public GenerateGTINCommand(GTINTypes type)
        {
            Model = type;
        }
    }

    public class GenerateGTINCommandHandler : BaseCommandHandler<GenerateGTINCommand, GTINInformation>
    {
        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        private readonly IGTINService gTINService = null;

        public GenerateGTINCommandHandler(ICompanyRepository companyRepository, ICompanyQueries companyQueries,
                                            IGTINService gTINService)
        {
            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;

            this.gTINService = gTINService;
        }
        public override async Task<GTINInformation> HandleCommand(GenerateGTINCommand request, CancellationToken cancellationToken)
        {
            var company = await companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Production.NoPermission");
            }

            GTIN gTIN = await gTINService.GenerateGTINAsync(company.Id, request.Model, request.LoginSession);

            if (gTIN == null)
                throw new BusinessException("Common.TaskFailed");

            var rs = gTIN.ToInformation();
            rs.Code = await gTINService.GetCodeGTINAsync(gTIN);
            return rs;
        }
    }
}
