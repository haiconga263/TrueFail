using Common.Exceptions;
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
    public class InsertOrUpdateGTINCommand : BaseCommand<GTINInformation>
    {
        public GTIN Model { get; set; }
        public InsertOrUpdateGTINCommand(GTIN gTIN)
        {
            Model = gTIN;
        }
    }

    public class InsertOrUpdateGTINCommandHandler : BaseCommandHandler<InsertOrUpdateGTINCommand, GTINInformation>
    {
        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        private readonly IGTINService gTINService = null;

        public InsertOrUpdateGTINCommandHandler(ICompanyRepository companyRepository, ICompanyQueries companyQueries,
                                            IGTINService gTINService)
        {
            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;

            this.gTINService = gTINService;
        }
        public override async Task<GTINInformation> HandleCommand(InsertOrUpdateGTINCommand request, CancellationToken cancellationToken)
        {
            var company = await companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
                throw new BusinessException("Common.NoPermission");

            GTIN gTIN = (await gTINService.InsertOrUpdateGTINAsync(company.Id, request.Model, request.LoginSession)).ToInformation();

            if (gTIN == null)
                throw new BusinessException("Common.TaskFailed");

            var rs = gTIN.ToInformation();
            rs.Code = await gTINService.GetCodeGTINAsync(gTIN);
            return rs;
        }
    }
}